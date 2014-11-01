using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Alta_Solicitud;
using PAEEEM.Entidades.Trama;
using PAEEEM.Entities;
using PAEEEM.LogicaNegocios.SolicitudCredito;
using PAEEEM.LogicaNegocios.Tarifas;
using PAEEEM.LogicaNegocios.ModuloCentral;
using Telerik.Web.UI;

namespace PAEEEM.SupplierModule
{
    public partial class CapturaAuxiliar : System.Web.UI.Page
    {
        #region Atributos

        private List<CompHistorialDetconsumo> LstHistorialConsumo
        {
            get
            {
                return ViewState["LstHistorialConsumo"] == null
                           ? new List<CompHistorialDetconsumo>()
                           : ViewState["LstHistorialConsumo"] as List<CompHistorialDetconsumo>;
            }
            set { ViewState["LstHistorialConsumo"] = value; }
        }

        protected string Rpu
        {
            get { return ViewState["Rpu"] as string; }
            set { ViewState["Rpu"] = value; }
        }

        protected bool PeriodosValidosHist
        {
            get { return (bool)ViewState["PeriodosValidosHist"]; }
            set { ViewState["PeriodosValidosHist"] = value; }
        }

        protected bool AnioFactValidoHist
        {
            get { return (bool)ViewState["AnioFactValidoHist"]; }
            set { ViewState["AnioFactValidoHist"] = value; }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LstHistorialConsumo = new List<CompHistorialDetconsumo>();
                LLenaCatalogoEstados();
                LLenaCatalogoPeriodos();
                LLenaCatalogosTarifa();
                LlenaRegionCfe();
                LLenaRegionFide();
                
                rwGrid.OpenerElementID = ImbBtnBuscarCP.ClientID;

                if (Request.QueryString["Token"] != null && Request.QueryString["Token"] != "")
                {
                    Rpu = System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["Token"]));
                    var accion = Convert.ToInt32(Request.QueryString["Acc"]);
                    CargaDatosAuxTrama();

                    if (accion == 3)
                    {
                        PanelCaptura.Enabled = false;
                        rgHistoricoConsumo.Enabled = false;
                    }
                }
                else
                {
                    IniciaControles();
                }
            }
        }

        protected void rgHistoricoConsumo_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var lstHistoricoConsumo = new CapturaAuxilialBL().ObtenHistorialConsumoAuxiliar(Rpu);
            rgHistoricoConsumo.DataSource = lstHistoricoConsumo.OrderBy(me => me.IdHistorial);
        }

        protected void rgHistoricoConsumo_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var usuario = ((US_USUARIOModel)Session["UserInfo"]).Nombre_Usuario;
            var item = e.Item as GridEditableItem;
            var fecha = (item["FECHA_PERIODO"].Controls[1] as RadDatePicker).SelectedDate;
            var consumo = Convert.ToDecimal((item["CONSUMO"].Controls[1] as RadNumericTextBox).Text);
            var demanda = Convert.ToInt32((item["DEMANDA"].Controls[1] as RadNumericTextBox).Text);
            var factorPotencia = Convert.ToDecimal((item["FACTORPOTENCIA"].Controls[1] as RadNumericTextBox).Text);
            var idHistorico = new CapturaAuxilialBL().ObtenIDhistorial(Rpu);

            var historicoConsumo = new AUX_HISTORIAL_CONSUMO
                {
                    Rpu = Rpu,
                    IdHistorial = Convert.ToByte(idHistorico),
                    Fecha_Periodo = (DateTime) fecha,
                    Consumo = consumo,
                    Demanda = demanda,
                    FactorPotencia = factorPotencia,
                    Fecha_Adicion = DateTime.Now.Date,
                    Estatus = true,
                    AdicionadoPor = usuario
                };

            var newHistConsumo = new CapturaAuxilialBL().InsertaHistoricoConsumo(historicoConsumo);

            if (newHistConsumo != null)
            {
                var lstHistoricoConsumo = new CapturaAuxilialBL().ObtenHistorialConsumoAuxiliar(Rpu);
                rgHistoricoConsumo.DataSource = lstHistoricoConsumo.OrderBy(me => me.IdHistorial);
            }
        }

        protected void rgHistoricoConsumo_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            int idHistorico = Convert.ToInt32(item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["IdHistorial"].ToString());
            var fecha = (item["FECHA_PERIODO"].Controls[1] as RadDatePicker).SelectedDate;
            var consumo = Convert.ToDecimal((item["CONSUMO"].Controls[1] as RadNumericTextBox).Text);
            var demanda = Convert.ToInt32((item["DEMANDA"].Controls[1] as RadNumericTextBox).Text);
            var factorPotencia = Convert.ToDecimal((item["FACTORPOTENCIA"].Controls[1] as RadNumericTextBox).Text);

            var historicoConsumo = new CapturaAuxilialBL().ObtenRegistroHisConsumo(Rpu, idHistorico);
            historicoConsumo.Fecha_Periodo = (DateTime)fecha;
            historicoConsumo.Consumo = consumo;
            historicoConsumo.Demanda = demanda;
            historicoConsumo.FactorPotencia = factorPotencia;

            var actualiza = new CapturaAuxilialBL().ActalizaHistoricoConsumo(historicoConsumo);

            if (actualiza)
            {
                var lstHistoricoConsumo = new CapturaAuxilialBL().ObtenHistorialConsumoAuxiliar(Rpu);
                rgHistoricoConsumo.DataSource = lstHistoricoConsumo.OrderBy(me => me.IdHistorial);
            }
        }

        protected void rgHistoricoConsumo_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            int idHistorico = Convert.ToInt32(item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["IdHistorial"].ToString());

            var historicoConsumo = new CapturaAuxilialBL().ObtenRegistroHisConsumo(Rpu, idHistorico);

            var elimina = new CapturaAuxilialBL().EliminaHistoricoConsumo(historicoConsumo);

            if (elimina)
            {
                var lstHistoricoConsumo = new CapturaAuxilialBL().ObtenHistorialConsumoAuxiliar(Rpu);
                rgHistoricoConsumo.DataSource = lstHistoricoConsumo.OrderBy(me => me.IdHistorial);
            }
        }

        protected void rgHistoricoConsumo_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (!e.Item.IsInEditMode) return;

            var item = (GridEditableItem)e.Item;

            if (!(e.Item is IGridInsertItem))
            {
                int idHistorico = Convert.ToInt32(item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["IdHistorial"].ToString());

                var historico = new CapturaAuxilialBL().ObtenRegistroHisConsumo(Rpu, idHistorico);

                if (historico != null)
                {
                    var radDatefecha = (RadDatePicker)item.FindControl("RadDateFechaPeriodo");
                    var txtConsumo = (RadNumericTextBox)item.FindControl("RadTxtConsumo");
                    var txtDemanda = (RadNumericTextBox)item.FindControl("RadTxtDEMANDA");
                    var txtFactorPotencia = (RadNumericTextBox)item.FindControl("RadTxtFACTORPOTENCIA");

                    radDatefecha.SelectedDate = historico.Fecha_Periodo;
                    txtConsumo.Text = historico.Consumo.ToString(CultureInfo.InvariantCulture);
                    txtDemanda.Text = historico.Demanda.ToString(CultureInfo.InvariantCulture);
                    txtFactorPotencia.Text = historico.FactorPotencia.ToString(CultureInfo.InvariantCulture);
                }
            }
            else
            {
                var radDatefecha = (RadDatePicker)item.FindControl("RadDateFechaPeriodo");
                radDatefecha.MaxDate = DateTime.Now.Date;
            }
        }

        protected void RadCmbEstado_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (RadCmbEstado.SelectedIndex != 0)
            {
                LlenaCatalogoMunicipios(int.Parse(RadCmbEstado.SelectedValue));
            }
        }

        protected void RadCmbMunicipio_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (RadCmbMunicipio.SelectedIndex != 0)
            {
                LLenaColonias(int.Parse(RadCmbEstado.SelectedValue), int.Parse(RadCmbMunicipio.SelectedValue));
            }
        }

        protected void RadCmbRegionFide_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (RadCmbRegionFide.SelectedIndex != 0)
            {
                LLenaZonasFide(int.Parse(RadCmbRegionFide.SelectedValue));
            }
        }

        protected void RadBtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();
                if (Page.IsValid)
                {
                    GuardaDatosAuxTrama();
                }
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert(ex.Message, 300, 150, "Captura Auxiliar", null);
            }
        }


        protected void RadBtnValida_Click(object sender, EventArgs e)
        {
            var datosAuxTrama = new CapturaAuxilialBL().ObtenDatosAuxTrama(Rpu);
            
            if (datosAuxTrama != null)
            {
                MarcaPeriodosValidos(datosAuxTrama);
                ValidaHistoricoAnioPeriodo(datosAuxTrama);

                if (!PeriodosValidosHist || !AnioFactValidoHist)
                {
                    datosAuxTrama.Estatus = false;
                    new CapturaAuxilialBL().ActualizaDatosAuxTrama(datosAuxTrama);

                    if (!AnioFactValidoHist)
                    {
                        rwmVentana.RadAlert("El usuario no cumple con Año de facturación", 300, 150, "Captura Auxiliar", null);
                    }

                    if (!PeriodosValidosHist)
                    {
                        rwmVentana.RadAlert("El usuario no cumple con en número de Periodos Validos", 300, 150, "Captura Auxiliar", null);
                    }
                }
                else
                {
                    rwmVentana.RadAlert("La información Proporcionada se valido correctamente", 300, 150, "Captura Auxiliar", null);
                }                
            }
        }

        protected void RadCmbColonia_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (RadCmbColonia.SelectedIndex != 0)
            {
                RadCmbColoniaHidden.SelectedValue = RadCmbColonia.SelectedValue;
                RadTxtCP.Text = RadCmbColoniaHidden.SelectedValue;
            }
        }

        #endregion

        #region Metodos Protegidos

        protected int ObtenIdHistorial()
        {
            if (LstHistorialConsumo.Count > 0)
            {
                return LstHistorialConsumo.Max(me => me.IdHistorial) + 1;
            }
            else
            {
                return 1;
            }
        }

        protected void GuardaDatosAuxTrama()
        {
            var usuario = ((US_USUARIOModel)Session["UserInfo"]).Nombre_Usuario;

            if (!string.IsNullOrEmpty(Rpu))
            {
                var auxDatosTrama = new CapturaAuxilialBL().ObtenDatosAuxTrama(Rpu);
                auxDatosTrama.Rpu = RadTxtRPU.Text;
                auxDatosTrama.Nombres = RadTxtNombres.Text;
                auxDatosTrama.ApellidoPaterno = RadTxtApPaterno.Text;
                auxDatosTrama.ApellidoMaterno = RadTxtApMaterno.Text;
                auxDatosTrama.Cve_Estado = int.Parse(RadCmbEstado.SelectedValue);
                auxDatosTrama.Cve_Deleg_Municipio = int.Parse(RadCmbMunicipio.SelectedValue);
                auxDatosTrama.Cve_CP = int.Parse(RadCmbColonia.SelectedValue);
                auxDatosTrama.CodigoPostal = RadTxtCP.Text;
                auxDatosTrama.Calle = RadTxtCalle.Text;
                auxDatosTrama.Numero = RadTxtNumero.Text;
                auxDatosTrama.Cve_Tarifa = int.Parse(RadCmbtarifa.SelectedValue);
                auxDatosTrama.Cuenta = RadTxtCuenta.Text;
                auxDatosTrama.Cve_Periodo_Pago = byte.Parse(RadCmbPeriodo.SelectedValue);
                auxDatosTrama.FechaInicio = RadDateInicio.SelectedDate;
                auxDatosTrama.FechaFin = RadDateFin.SelectedDate;
                auxDatosTrama.TotalPeriodos = byte.Parse(RadTxtTotalPeriodos.Text);
                auxDatosTrama.ZonaCFE = RadTxtZonaCFE.Text;
                auxDatosTrama.Id_RegionTarifa = int.Parse(RadCmbRegionCFE.SelectedValue);
                auxDatosTrama.Cve_Region = int.Parse(RadCmbRegionFide.SelectedValue);
                auxDatosTrama.Cve_Zona = int.Parse(RadCmbZonaFide.SelectedValue);

                if (new CapturaAuxilialBL().ActualizaDatosAuxTrama(auxDatosTrama))
                {
                    rwmVentana.RadAlert("Se actualizarón los datos satisfactoriamente", 300, 150, "Captura Auxiliar", null);
                }
            }
            else
            {
                var auxDatosTrama = new AUX_DATOS_TRAMA();
                auxDatosTrama.Rpu = RadTxtRPU.Text;
                auxDatosTrama.Nombres = RadTxtNombres.Text;
                auxDatosTrama.ApellidoPaterno = RadTxtApPaterno.Text;
                auxDatosTrama.ApellidoMaterno = RadTxtApMaterno.Text;
                auxDatosTrama.Cve_Estado = int.Parse(RadCmbEstado.SelectedValue);
                auxDatosTrama.Cve_Deleg_Municipio = int.Parse(RadCmbMunicipio.SelectedValue);
                auxDatosTrama.Cve_CP = int.Parse(RadCmbColonia.SelectedValue);
                auxDatosTrama.CodigoPostal = RadTxtCP.Text;
                auxDatosTrama.Calle = RadTxtCalle.Text;
                auxDatosTrama.Numero = RadTxtNumero.Text;
                auxDatosTrama.Cve_Tarifa = int.Parse(RadCmbtarifa.SelectedValue);
                auxDatosTrama.Cuenta = RadTxtCuenta.Text;
                auxDatosTrama.Cve_Periodo_Pago = byte.Parse(RadCmbPeriodo.SelectedValue);
                auxDatosTrama.FechaInicio = RadDateInicio.SelectedDate;
                auxDatosTrama.FechaFin = RadDateFin.SelectedDate;
                auxDatosTrama.TotalPeriodos = byte.Parse(RadTxtTotalPeriodos.Text);
                auxDatosTrama.ZonaCFE = RadTxtZonaCFE.Text;
                auxDatosTrama.Id_RegionTarifa = int.Parse(RadCmbRegionCFE.SelectedValue);
                auxDatosTrama.Cve_Region = int.Parse(RadCmbRegionFide.SelectedValue);
                auxDatosTrama.Cve_Zona = int.Parse(RadCmbZonaFide.SelectedValue);
                auxDatosTrama.EstatusCapturaAux = 1;
                auxDatosTrama.Estatus = true;
                auxDatosTrama.FechaAdicion = DateTime.Now.Date;
                auxDatosTrama.AdicionadoPor = usuario;

                var newAuxDatosTrama = new CapturaAuxilialBL().InsertaDatosTrama(auxDatosTrama);

                if (newAuxDatosTrama != null)
                {
                    Rpu = newAuxDatosTrama.Rpu;
                    rgHistoricoConsumo.Enabled = true;
                    RadBtnValida.Enabled = true;

                    rwmVentana.RadAlert("Se guardarón los datos satisfactoriamente", 300, 150, "Captura Auxiliar", null);
                }
            }
        }

        protected void MarcaPeriodosValidos(AUX_DATOS_TRAMA datosAuxTrama)
        {
            var lstAuxHistoricoConsumo = new CapturaAuxilialBL().ObtenHistorialConsumoAuxiliar(Rpu);
            //var datosAuxTrama = new CapturaAuxilialBL().ObtenDatosAuxTrama(Rpu);
            DateTime fechaMinimaConsumo = DateTime.Now;

            if (lstAuxHistoricoConsumo.Count > 0)
            {
                var fechaMaximaConsumo = lstAuxHistoricoConsumo.Max(me => me.Fecha_Periodo);

                if (datosAuxTrama.Cve_Periodo_Pago == 1)
                    fechaMinimaConsumo = fechaMaximaConsumo.AddMonths(-11);
                if (datosAuxTrama.Cve_Periodo_Pago == 2)
                    fechaMinimaConsumo = fechaMaximaConsumo.AddMonths(-10);

                foreach (var auxHistorialConsumo in lstAuxHistoricoConsumo)
                {
                    auxHistorialConsumo.IdValido = auxHistorialConsumo.Fecha_Periodo < fechaMinimaConsumo
                                                       ? Convert.ToByte(0)
                                                       : Convert.ToByte(auxHistorialConsumo.IdHistorial);

                    var actualiza = new CapturaAuxilialBL().ActalizaHistoricoConsumo(auxHistorialConsumo);
                }
            }
        }

        protected void ValidaHistoricoAnioPeriodo(AUX_DATOS_TRAMA datosAuxTrama)
        {
            var periodoMinimo = 0;
            DateTime? detConsumoFechaMin = null;
            var lstAuxHistoricoConsumo = new CapturaAuxilialBL().ObtenHistorialConsumoAuxiliar(Rpu);
            var detConsumoFechaMax = lstAuxHistoricoConsumo.Max(me => me.Fecha_Periodo);

            if (datosAuxTrama.Cve_Periodo_Pago == 1)
            {
                periodoMinimo =
                    int.Parse(
                        new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 2 && p.IDSECCION == 4).VALOR);
                detConsumoFechaMin = detConsumoFechaMax.AddMonths(-11);
            }
            if (datosAuxTrama.Cve_Periodo_Pago == 2)
            {
                periodoMinimo =
                    int.Parse(
                        new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 1 && p.IDSECCION == 4).VALOR);
                detConsumoFechaMin = detConsumoFechaMax.AddMonths(-12);
            }

            var histDetConsumo = lstAuxHistoricoConsumo.FirstOrDefault(me => me.Fecha_Periodo == detConsumoFechaMin);

            if (histDetConsumo == null)
            {
                var fechaMin = lstAuxHistoricoConsumo.Min(p => p.Fecha_Periodo);
                if (fechaMin < detConsumoFechaMin)
                    histDetConsumo = lstAuxHistoricoConsumo.FirstOrDefault(p => p.Fecha_Periodo == detConsumoFechaMin.Value.AddMonths(1));
            }

            if (histDetConsumo != null) //SI ES DISTINTO DE NULL CUMPLE CON EL AÑO DE FACTURACION
            {
                var rstNoTotalConsumo =
                    lstAuxHistoricoConsumo.Count(
                        p =>
                        p.Fecha_Periodo >= histDetConsumo.Fecha_Periodo && p.Fecha_Periodo <= detConsumoFechaMax &&
                        p.IdValido > 0);
                
                AnioFactValidoHist = true;

                if (rstNoTotalConsumo >= periodoMinimo)
                {
                    PeriodosValidosHist = true;
                }
                else
                {
                    PeriodosValidosHist = false;
                }
            }
            else
            {
                AnioFactValidoHist = false;
            }
        }

        protected void CargaDatosAuxTrama()
        {
            if (!string.IsNullOrEmpty(Rpu))
            {
                var datosAuxTrama = new CapturaAuxilialBL().ObtenDatosAuxTrama(Rpu);

                if (datosAuxTrama != null)
                {
                    RadTxtRPU.Text = datosAuxTrama.Rpu;
                    RadTxtNombres.Text = datosAuxTrama.Nombres;
                    RadTxtApPaterno.Text = datosAuxTrama.ApellidoPaterno;
                    RadTxtApMaterno.Text = datosAuxTrama.ApellidoMaterno;
                    RadCmbEstado.SelectedValue = datosAuxTrama.Cve_Estado.ToString();
                    LlenaCatalogoMunicipios((int)datosAuxTrama.Cve_Estado);
                    RadCmbMunicipio.SelectedValue = datosAuxTrama.Cve_Deleg_Municipio.ToString();
                    LLenaColonias((int)datosAuxTrama.Cve_Estado, (int)datosAuxTrama.Cve_Deleg_Municipio);
                    RadCmbColonia.SelectedValue = datosAuxTrama.Cve_CP.ToString();
                    RadTxtCP.Text = datosAuxTrama.CodigoPostal;
                    RadTxtCalle.Text = datosAuxTrama.Calle;
                    RadTxtNumero.Text = datosAuxTrama.Numero;
                    RadCmbtarifa.SelectedValue = datosAuxTrama.Cve_Tarifa.ToString();
                    RadTxtCuenta.Text = datosAuxTrama.Cuenta;
                    RadCmbPeriodo.SelectedValue = datosAuxTrama.Cve_Periodo_Pago.ToString();
                    RadDateInicio.SelectedDate = datosAuxTrama.FechaInicio;
                    RadDateFin.SelectedDate = datosAuxTrama.FechaFin;
                    RadTxtTotalPeriodos.Text = datosAuxTrama.TotalPeriodos.ToString();
                    RadTxtZonaCFE.Text = datosAuxTrama.ZonaCFE;
                    RadCmbRegionCFE.SelectedValue = datosAuxTrama.Id_RegionTarifa.ToString();
                    RadCmbRegionFide.SelectedValue = datosAuxTrama.Cve_Region.ToString();
                    LLenaZonasFide((int)datosAuxTrama.Cve_Region);
                    RadCmbZonaFide.SelectedValue = datosAuxTrama.Cve_Zona.ToString();
                }
            }
        }

        protected void CargaGridColonias(string codigoPostal)
        {
            var lstColonias = CatalogosSolicitud.ObtenColoniasXCp(codigoPostal);

            if (lstColonias.Count > 0)
            {
                rgColonias.DataSource = lstColonias;
                rgColonias.DataBind();
            }
        }

        #endregion

        #region Catalogos

        protected void LLenaCatalogoEstados()
        {
            var lstEstados = CatalogosSolicitud.ObtenCatEstadosRepublica();

            if (lstEstados.Count > 0)
            {
                RadCmbEstado.DataSource = lstEstados;
                RadCmbEstado.DataValueField = "Cve_Estado";
                RadCmbEstado.DataTextField = "Dx_Nombre_Estado";
                RadCmbEstado.DataBind();

                RadCmbEstado.Items.Insert(0, new RadComboBoxItem("Seleccione..."));
                RadCmbEstado.SelectedIndex = 0;
            }
        }

        protected void LlenaCatalogoMunicipios(int idEstado)
        {
            var lstMunicipios = CatalogosSolicitud.ObtenDelegMunicipios(idEstado);

            if (lstMunicipios.Count > 0)
            {
                RadCmbMunicipio.DataSource = lstMunicipios;
                RadCmbMunicipio.DataValueField = "Cve_Deleg_Municipio";
                RadCmbMunicipio.DataTextField = "Dx_Deleg_Municipio";
                RadCmbMunicipio.DataBind();

                RadCmbMunicipio.Items.Insert(0, new RadComboBoxItem("Seleccione..."));
                RadCmbMunicipio.SelectedIndex = 0;
            }
        }

        protected void LLenaColonias(int idEstado, int idMunicipio)
        {
            var lstColonias = CatalogosSolicitud.ObtenCatCodigoPostals(idEstado, idMunicipio);

            if (lstColonias.Count > 0)
            {
                RadCmbColonia.DataSource = lstColonias;
                RadCmbColonia.DataValueField = "Cve_CP";
                RadCmbColonia.DataTextField = "Dx_Colonia";
                RadCmbColonia.DataBind();

                RadCmbColonia.Items.Insert(0, new RadComboBoxItem("Seleccione..."));
                RadCmbColonia.SelectedIndex = 0;

                RadCmbColoniaHidden.DataSource = lstColonias;
                RadCmbColoniaHidden.DataValueField = "Cve_CP";
                RadCmbColoniaHidden.DataTextField = "Codigo_Postal";
                RadCmbColoniaHidden.DataBind();

                RadCmbColoniaHidden.Items.Insert(0, new RadComboBoxItem("Seleccione..."));
                RadCmbColoniaHidden.SelectedIndex = 0;
            }
        }

        protected void LLenaCatalogosTarifa()
        {
            var lstTarifas = RegistrarTarifas.TiposTarifa();

            if (lstTarifas.Count > 0)
            {
                RadCmbtarifa.DataSource = lstTarifas;
                RadCmbtarifa.DataValueField = "Cve_Tarifa";
                RadCmbtarifa.DataTextField = "Dx_Tarifa";
                RadCmbtarifa.DataBind();

                RadCmbtarifa.Items.Insert(0, new RadComboBoxItem("Seleccione..."));
                RadCmbtarifa.SelectedIndex = 0;
            }
        }

        protected void LLenaCatalogoPeriodos()
        {
            RadCmbPeriodo.Items.Add(new RadComboBoxItem("Mensual", "1"));
            RadCmbPeriodo.Items.Add(new RadComboBoxItem("Bimestral", "2"));

            RadCmbPeriodo.Items.Insert(0, new RadComboBoxItem("Seleccione..."));
            RadCmbPeriodo.SelectedIndex = 0;
        }

        protected void LlenaRegionCfe()
        {
            var lstRegionCFE = RegistrarTarifas.Regiones();

            if (lstRegionCFE.Count > 0)
            {
                RadCmbRegionCFE.DataSource = lstRegionCFE;
                RadCmbRegionCFE.DataValueField = "id_region";
                RadCmbRegionCFE.DataTextField = "Descripcion";
                RadCmbRegionCFE.DataBind();

                RadCmbRegionCFE.Items.Insert(0, new RadComboBoxItem("Seleccione..."));
                RadCmbRegionCFE.SelectedIndex = 0;
            }
        }

        protected void LLenaRegionFide()
        {
            var lstRegionFide = CatalogosRegionZona.catRegion();

            if (lstRegionFide.Count > 0)
            {
                RadCmbRegionFide.DataSource = lstRegionFide;
                RadCmbRegionFide.DataValueField = "Cve_Region";
                RadCmbRegionFide.DataTextField = "Dx_Nombre_Region";
                RadCmbRegionFide.DataBind();

                RadCmbRegionFide.Items.Insert(0, new RadComboBoxItem("Seleccione..."));
                RadCmbRegionFide.SelectedIndex = 0;
            }
        }

        protected void LLenaZonasFide(int idRegion)
        {
            var lstZona = CatalogosRegionZona.catZonaxidRegion(idRegion);

            if (lstZona.Count > 0)
            {
                RadCmbZonaFide.DataSource = lstZona;
                RadCmbZonaFide.DataValueField = "Cve_Zona";
                RadCmbZonaFide.DataTextField = "Dx_Nombre_Zona";
                RadCmbZonaFide.DataBind();

                RadCmbZonaFide.Items.Insert(0, new RadComboBoxItem("Seeleccione..."));
                RadCmbZonaFide.SelectedIndex = 0;
            }
        }

        #endregion                              

        protected void rgColonias_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                var item = (GridDataItem)e.Item;
                var cveCp = int.Parse(item.GetDataKeyValue("CveCp").ToString());
                var codigoPostal = item["CodigoPostal"].Text;

                var lstColonias = CatalogosSolicitud.ObtenColoniasXCp(codigoPostal);

                if (lstColonias.Count > 0)
                {
                    var colonia = lstColonias.FirstOrDefault(me => me.CveCp == cveCp);

                    RadCmbEstado.SelectedValue = colonia.CveEstado.ToString(CultureInfo.InvariantCulture);
                    LlenaCatalogoMunicipios(colonia.CveEstado);
                    RadCmbMunicipio.SelectedValue = colonia.CveDelegMunicipio.ToString();

                    RadCmbColonia.DataSource = lstColonias;
                    RadCmbColonia.DataValueField = "CveCp";
                    RadCmbColonia.DataTextField = "DxColonia";
                    RadCmbColonia.DataBind();

                    RadCmbColonia.Items.Insert(0, new RadComboBoxItem("Seleccione..."));
                    RadCmbColonia.SelectedIndex = 0;
                    RadCmbColonia.SelectedValue = cveCp.ToString(CultureInfo.InvariantCulture);
                }
            }
        }

        protected void rgColonias_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var lstColonias = CatalogosSolicitud.ObtenColoniasXCp(RadTxtCP.Text);
            rgColonias.DataSource = lstColonias;
        }

        protected void RadTxtCP_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(RadTxtCP.Text))
                CargaGridColonias(RadTxtCP.Text);
                //rgColonias_NeedDataSource(null, null);
                
        }
 
        protected void IniciaControles()
        {
            RadDateInicio.MaxDate = DateTime.Now.Date;
            RadDateInicio.MinDate = DateTime.Now.Date.AddYears(-1);

            RadDateFin.MaxDate = DateTime.Now.Date;
            RadDateFin.MinDate = DateTime.Now.Date.AddYears(-1).AddDays(1);

            rgHistoricoConsumo.Enabled = false;
            RadBtnValida.Enabled = false;
        }
    }
}