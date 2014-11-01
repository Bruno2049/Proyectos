using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.Entidades;
using PAEEEM.Entidades.MRV;
using PAEEEM.Entities;
using PAEEEM.LogicaNegocios.AltaBajaEquipos;
using PAEEEM.LogicaNegocios.MRV;
using Telerik.Web.UI;

namespace PAEEEM.MRV
{
    public partial class wucMediciones : System.Web.UI.UserControl
    {
        #region Atributos

        protected int IdMedicion
        {
            get { return (int)ViewState["IdMedicion"]; }
            set { ViewState["IdMedicion"] = value; }
        }

        protected string NumeroCredito
        {
            get { return ViewState["NumeroCredito"] as string; }
            set { ViewState["NumeroCredito"] = value; }
        }

        private List<MedicionDetalle> LstMedicionDetalle
        {
            get
            {
                return ViewState["LstMedicionDetalle"] == null
                           ? new List<MedicionDetalle>()
                           : ViewState["LstMedicionDetalle"] as List<MedicionDetalle>;
            }
            set { ViewState["LstMedicionDetalle"] = value; }
        }

        private List<GrupoCredito> LstGruposCredito
        {
            get
            {
                return ViewState["LstGruposCredito"] == null
                           ? new List<GrupoCredito>()
                           : ViewState["LstGruposCredito"] as List<GrupoCredito>;
            }
            set { ViewState["LstGruposCredito"] = value; }
        }

        protected int IdGrupo
        {
            get { return (int)ViewState["IdGrupo"]; }
            set { ViewState["IdGrupo"] = value; }
        }

        protected string Grupo
        {
            get { return ViewState["IdGrupo"].ToString(); }
            set { ViewState["Grupo"] = value; }
        }

        protected bool Finaliza
        {
            get { return (bool)ViewState["Finaliza"]; }
            set { ViewState["Finaliza"] = value; }
        }

        #endregion        

        #region CargaInicial

        protected void Page_Load(object sender, EventArgs e)
        {
        }
        
        public void Inicializa(string idCredito, int idMedicion)
        {
            NumeroCredito = idCredito;
            IdMedicion = idMedicion;
            HidIdMedicion.Value = idMedicion.ToString();
            LstMedicionDetalle = new List<MedicionDetalle>();
            LstGruposCredito = new List<GrupoCredito>();
            LLenaPersonalMedicion();
            LlenaGruposCredito(NumeroCredito);
            IdGrupo = 0;
            Grupo = "XYZ";
            Finaliza = false;
            RadTxtFechaMedicionExAnte.MaxDate = DateTime.Now.Date;
            RadTxtFechaMedicionExPost.MaxDate = DateTime.Now.Date;
        }

        public void InicializaConsultaEdit(string idCredito, int idMedicion, bool esEdicion)
        {
            NumeroCredito = idCredito;
            IdMedicion = idMedicion;
            IdGrupo = 0;
            HidIdMedicion.Value = idMedicion.ToString();
            LstMedicionDetalle = new List<MedicionDetalle>();
            LstGruposCredito = new List<GrupoCredito>();
            LLenaPersonalMedicion();
            LlenaGruposCredito(NumeroCredito);
            CargaDatosMedicion();
            RadGrupoSolicitud.SelectedValue = IdGrupo.ToString();
            RadBtnFinalizar.Enabled = esEdicion;
            RadBtnGuardar.Enabled = esEdicion;
            PnlMedicionDetalle.Enabled = esEdicion;
            PnlTrifasico.Enabled = esEdicion;
            ChkTrifasico.Enabled = esEdicion;
            rgEquiposMedicion.Enabled = esEdicion;
            UploadedArchivoMedicion.Visible = esEdicion;
            RadTxtFechaMedicionExAnte.MaxDate = DateTime.Now.Date;
            RadTxtFechaMedicionExPost.MaxDate = DateTime.Now.Date;
            RadTxtCometGenerales.Enabled =
                RadTxtFechaMedicionExAnte.Enabled =
                RadTxtFechaMedicionExPost.Enabled = RadCmbNombreMedicion.Enabled = esEdicion;
            Finaliza = false;
            if(IdGrupo != 0)
                LLenaEquiposBajaAlta(NumeroCredito);
        }

        #endregion

        #region Catalogos

        protected void LLenaPersonalMedicion()
        {
            var lstPersonal = new List<MRV_CAT_PERSONAL_MEDICION>();
            var usuario = ((US_USUARIOModel) Session["UserInfo"]);

            if (usuario.Tipo_Usuario == "R_O")
                lstPersonal = new MedicionesLN().ObtenPersonalMedicion(me => me.Cve_Region == usuario.Id_Departamento);
            else if (usuario.Tipo_Usuario == "Z_O")
                lstPersonal = new MedicionesLN().ObtenPersonalMedicion(me => me.Cve_Zona == usuario.Id_Departamento);
            else
                lstPersonal = new MedicionesLN().ObtenPersonalMedicion(me => me.Estatus == true);
            
            RadCmbNombreMedicion.Items.Clear();
                        
            RadCmbNombreMedicion.DataSource = lstPersonal;
            RadCmbNombreMedicion.DataValueField = "IdPersonalMedicion";
            RadCmbNombreMedicion.DataTextField = "NombrePersonalMedicion";
            RadCmbNombreMedicion.DataBind();

            RadCmbNombreMedicion.Items.Insert(0, new RadComboBoxItem(""));
            RadCmbNombreMedicion.SelectedIndex = 0;
        }

        protected void LlenaGruposCredito(string idCredito)
        {
            RadGrupoSolicitud.Items.Clear();
            var lstGruposCredito = new MedicionesLN().ObtenGruposCredito(idCredito);
            RadGrupoSolicitud.DataSource = lstGruposCredito;
            RadGrupoSolicitud.DataValueField = "IdGrupo";
            RadGrupoSolicitud.DataTextField = "Grupo";
            RadGrupoSolicitud.DataBind();

            RadGrupoSolicitud.Items.Insert(0, new RadComboBoxItem(""));
            RadGrupoSolicitud.SelectedIndex = 0;

            LstGruposCredito = new MedicionesLN().ObtenGrupos(IdMedicion);
        }

        protected void LLenaEquiposBajaAlta(string idCredito)
        {
            var lstAltaEficiencia = new OpEquiposAbEficiencia().ObtenEquiposAltaEficienciaCredito(idCredito);
            var lstBajaEficiencia = new OpEquiposAbEficiencia().ObtenEquiposBajaEficienciaCredito(idCredito);

            BulletedListEquiposBaja.DataSource =
                lstBajaEficiencia.FindAll(me => me.Cve_Grupo == int.Parse(RadGrupoSolicitud.SelectedValue));
            BulletedListEquiposBaja.DataValueField = "ID";
            BulletedListEquiposBaja.DataTextField = "Dx_Tipo_Producto";
            BulletedListEquiposBaja.DataBind();

            BulletedListEquiposAlta.DataSource =
                lstAltaEficiencia.FindAll(me => me.Cve_Grupo == int.Parse(RadGrupoSolicitud.SelectedValue));
            BulletedListEquiposAlta.DataValueField = "ID";
            BulletedListEquiposAlta.DataTextField = "Dx_Modelo";
            BulletedListEquiposAlta.DataBind();
        }

        #endregion

        #region Metodos Protegidos

        protected void GeneraMedicionDetalle()
        {
            var idGrupo = IdGrupo;
            var grupo = Grupo;
            var usuario = ((US_USUARIOModel) Session["UserInfo"]).Nombre_Usuario;

            InsertaMedicionDetalleConcepto(usuario, 1, idGrupo, grupo, "kW", RadDemandMaxEqIneficiente,
                                           RadDemandMaxEqEficiente, RadDemandMaxValor, RadDemandMaxPorcent,
                                           RadDemandMaxObs);
            
            InsertaMedicionDetalleConcepto(usuario, 2, idGrupo, grupo, "kW", RadDemandMedEqIneficiente,
                                           RadDemandMedEqEficiente, RadDemandMedValor, RadDemandMedPorcent,
                                           RadDemandMedObs);

            InsertaMedicionDetalleConcepto(usuario, 3, idGrupo, grupo, "kWh", RadConsumoDiaEqIneficiente,
                                           RadConsumoDiaEqEficiente, RadConsumoDiaValor, RadConsumoDiaPorcent,
                                           RadConsumoDiaObs);

            InsertaMedicionDetalleConcepto(usuario, 4, idGrupo, grupo, "kWh", RadConsumoAnualEqIneficiente,
                                           RadConsumoAnualEqEficiente, RadConsumoAnualValor, RadCosumoAnualPorcent,
                                           RadConsumoAnualObs);

            InsertaMedicionDetalleConcepto(usuario, 5, idGrupo, grupo, "%", RadFactPotenciaEqIneficiente,
                                           RadFactPotenciaEqEficiente, RadFactPotenciaValor, RadFactPotenciaPorcent,
                                           RadFactPotenciaObs);

            InsertaMedicionDetalleConcepto(usuario, 6, idGrupo, grupo, "Volts", RadTensionLineaEqIneficiente,
                                           RadTensionLineaEqEficiente, RadTensionLineaValor, RadTensionLineaPorcent,
                                           RadTensionLineaObs);

            InsertaMedicionDetalleConcepto(usuario, 7, idGrupo, grupo, "Amperes", RadCorrienteLineaEqIneficiente,
                                           RadCorrienteLineaEqEficiente, RadCorrienteLineaValor, RadCorrienteLineaPorcent,
                                           RadCorrienteLineaObs);

            InsertaMedicionDetalleConcepto(usuario, 8, idGrupo, grupo, "°C", RadTemperaturaEqIneficiente,
                                           RadTemperaturaEqEficiente, RadTemperaturaValor, RadTemperaturaPorcent,
                                           RadTemperaturaObs);

            //InsertaMedicionDetalleConcepto(usuario, 9, idGrupo, grupo, "Volts", RadVoltajeLineaEqIneficiente,
            //                               RadVoltajeLineaEqEficiente, RadVoltajeLineaValor, RadVoltajeLineaPorcent,
            //                               RadVoltajeLineaObs);

            InsertaMedicionDetalleConcepto(usuario, 10, idGrupo, grupo, "W", RadFlujoLuminosoEqIneficiente,
                                           RadFlujoLuminosoEqEficiente, RadFlujoLuminosoValor, RadFlujoLuminosoPorcent,
                                           RadFlujoLuminosoObs);

            if (ChkTrifasico.Checked)
            {
                InsertaMedicionDetalleConcepto(usuario, 11, idGrupo, grupo, "Volts", RadTensionLaEqIneficiente,
                                           RadTensionLaEqEficiente, RadTensionLaValor, RadTensionLaPorcent,
                                           RadTensionLaObs);

                InsertaMedicionDetalleConcepto(usuario, 12, idGrupo, grupo, "Volts", RadTensionLbEqIneficiente,
                                           RadTensionLbEqEficiente, RadTensionLbValor, RadTensionLbPorcent,
                                           RadTensionLbObs);

                InsertaMedicionDetalleConcepto(usuario, 13, idGrupo, grupo, "Volts", RadTensionLcEqIneficiente,
                                           RadTensionLcEqEficiente, RadTensionLcValor, RadTensionLcPorcent,
                                           RadTensionLcObs);

                InsertaMedicionDetalleConcepto(usuario, 14, idGrupo, grupo, "Amperes", RadCorrienteLaEqIneficiente,
                                           RadCorrienteLaEqEficiente, RadCorrienteLaValor, RadCorrienteLaPorcent,
                                           RadCorrienteLaObs);

                InsertaMedicionDetalleConcepto(usuario, 15, idGrupo, grupo, "Amperes", RadCorrienteLbEqIneficiente,
                                           RadCorrienteLbEqEficiente, RadCorrienteLbValor, RadCorrienteLbPorcent,
                                           RadCorrienteLbObs);

                InsertaMedicionDetalleConcepto(usuario, 16, idGrupo, grupo, "Amperes", RadCorrienteLcEqIneficiente,
                                           RadCorrienteLcEqEficiente, RadCorrienteLcValor, RadCorrienteLcPorcent,
                                           RadCorrienteLcObs);

                foreach (var grupoCredito in LstGruposCredito.Where(grupoCredito => grupoCredito.IdGrupo == idGrupo))
                {
                    grupoCredito.EsTrifasico = true;
                }
            }
            else
            {
                foreach (var grupoCredito in LstGruposCredito.Where(grupoCredito => grupoCredito.IdGrupo == idGrupo))
                {
                    grupoCredito.EsTrifasico = false;
                }
            }
        }

        protected void InsertaMedicionDetalleConcepto(string usuario, int idCampoMedicion, int idGrupo, string grupo, string unidad,
                                        RadNumericTextBox equipoIneficiente, RadNumericTextBox equipoEficiente, RadNumericTextBox valor, 
                                        RadNumericTextBox porcentage, RadTextBox observaciones)
        {
            var detalle =
                LstMedicionDetalle.FirstOrDefault(me => me.IdGrupo == idGrupo && me.IdCampoMedicion == idCampoMedicion);

            if (detalle != null)
            {
                LstMedicionDetalle.Remove(detalle);

                if (!equipoEficiente.Visible && !equipoIneficiente.Visible && !valor.Visible &&
                    !observaciones.Visible)
                {
                    detalle.EquipoIneficiente = 0.0M;
                    detalle.EquipoEficiente = 0.0M;
                    detalle.Valor = 0.0M;
                    detalle.Porcentaje = 0.0M;
                    detalle.Estatus = false;
                }
                else
                {
                    detalle.EquipoIneficiente =
                    Convert.ToDecimal(equipoIneficiente.Text != "" ? equipoIneficiente.Text : "0.0");
                    detalle.EquipoEficiente =
                        Convert.ToDecimal(equipoEficiente.Text != "" ? equipoEficiente.Text : "0.0");
                    detalle.Unidad = unidad;
                    detalle.Valor = Convert.ToDecimal(valor.Text != "" ? valor.Text : "0.0");
                    detalle.Porcentaje = Convert.ToDecimal(porcentage.Text != "" ? porcentage.Text : "0.0");
                    detalle.Observaciones = observaciones.Text;
                    detalle.Estatus = false;
                }

                LstMedicionDetalle.Add(detalle);
            }
            else
            {

                var medicionDetalle = new MedicionDetalle();
                medicionDetalle.IdMedicion = IdMedicion;
                medicionDetalle.IdCampoMedicion = idCampoMedicion;
                medicionDetalle.IdGrupo = idGrupo;
                medicionDetalle.Grupo = grupo;
                medicionDetalle.EquipoIneficiente =
                    Convert.ToDecimal(equipoIneficiente.Text != "" ? equipoIneficiente.Text : "0.0");
                medicionDetalle.EquipoEficiente =
                    Convert.ToDecimal(equipoEficiente.Text != "" ? equipoEficiente.Text : "0.0");
                medicionDetalle.Unidad = unidad;
                medicionDetalle.Valor = Convert.ToDecimal(valor.Text != "" ? valor.Text : "0.0");
                medicionDetalle.Porcentaje = Convert.ToDecimal(porcentage.Text != "" ? porcentage.Text : "0.0");
                medicionDetalle.Observaciones = observaciones.Text;
                medicionDetalle.Estatus = true;
                medicionDetalle.FechaAdicion = DateTime.Now.Date;
                medicionDetalle.AdicionadoPor = usuario;

                if (!equipoEficiente.Visible && !equipoIneficiente.Visible && !valor.Visible &&
                    !observaciones.Visible)
                {
                    medicionDetalle.EquipoIneficiente = 0.0M;
                    medicionDetalle.EquipoEficiente = 0.0M;
                    medicionDetalle.Valor = 0.0M;
                    medicionDetalle.Porcentaje = 0.0M;
                    medicionDetalle.Estatus = false;
                }

                LstMedicionDetalle.Add(medicionDetalle);
            }
        }

        protected void CargaMedicionDetalle(int idgrupo)
        {
            if (LstMedicionDetalle.FindAll(me => me.IdGrupo == idgrupo).Count > 0)
            {
                var esTrifasico = LstGruposCredito.FirstOrDefault(me => me.IdGrupo == idgrupo).EsTrifasico;

                foreach (var medicionDetalle in LstMedicionDetalle.FindAll(me => me.IdGrupo == idgrupo))
                {
                    switch (medicionDetalle.IdCampoMedicion)
                    {
                        case  1:
                            CargaMedicionDetalleConcepto(medicionDetalle, RadDemandMaxEqIneficiente,
                                           RadDemandMaxEqEficiente, RadDemandMaxValor, RadDemandMaxPorcent,
                                           RadDemandMaxObs);
                            break;
                        case 2:
                            CargaMedicionDetalleConcepto(medicionDetalle, RadDemandMedEqIneficiente,
                                           RadDemandMedEqEficiente, RadDemandMedValor, RadDemandMedPorcent,
                                           RadDemandMedObs);
                            break;
                        case 3:
                            CargaMedicionDetalleConcepto(medicionDetalle, RadConsumoDiaEqIneficiente,
                                           RadConsumoDiaEqEficiente, RadConsumoDiaValor, RadConsumoDiaPorcent,
                                           RadConsumoDiaObs);
                            break;
                        case 4:
                            CargaMedicionDetalleConcepto(medicionDetalle, RadConsumoAnualEqIneficiente,
                                           RadConsumoAnualEqEficiente, RadConsumoAnualValor, RadCosumoAnualPorcent,
                                           RadConsumoAnualObs);
                            break;
                        case 5:
                            CargaMedicionDetalleConcepto(medicionDetalle, RadFactPotenciaEqIneficiente,
                                           RadFactPotenciaEqEficiente, RadFactPotenciaValor, RadFactPotenciaPorcent,
                                           RadFactPotenciaObs);
                            break;
                        case 6: CargaMedicionDetalleConcepto(medicionDetalle, RadTensionLineaEqIneficiente,
                                           RadTensionLineaEqEficiente, RadTensionLineaValor, RadTensionLineaPorcent,
                                           RadTensionLineaObs);
                            break;
                        case 7: CargaMedicionDetalleConcepto(medicionDetalle, RadCorrienteLineaEqIneficiente,
                                           RadCorrienteLineaEqEficiente, RadCorrienteLineaValor, RadCorrienteLineaPorcent,
                                           RadCorrienteLineaObs);
                            break;
                        case 8: CargaMedicionDetalleConcepto(medicionDetalle, RadTemperaturaEqIneficiente,
                                           RadTemperaturaEqEficiente, RadTemperaturaValor, RadTemperaturaPorcent,
                                           RadTemperaturaObs);
                            break;
                        //case 9: CargaMedicionDetalleConcepto(medicionDetalle, RadVoltajeLineaEqIneficiente,
                        //                   RadVoltajeLineaEqEficiente, RadVoltajeLineaValor, RadVoltajeLineaPorcent,
                        //                   RadVoltajeLineaObs);
                            break;
                        case 10: CargaMedicionDetalleConcepto(medicionDetalle, RadFlujoLuminosoEqIneficiente,
                                           RadFlujoLuminosoEqEficiente, RadFlujoLuminosoValor, RadFlujoLuminosoPorcent,
                                           RadFlujoLuminosoObs);
                            break;                       
                    }

                    if (esTrifasico)
                    {
                        switch (medicionDetalle.IdCampoMedicion)
                        {
                            case 11: CargaMedicionDetalleConcepto(medicionDetalle, RadTensionLaEqIneficiente,
                                           RadTensionLaEqEficiente, RadTensionLaValor, RadTensionLaPorcent,
                                           RadTensionLaObs);
                                break;
                            case 12: CargaMedicionDetalleConcepto(medicionDetalle, RadTensionLbEqIneficiente,
                                               RadTensionLbEqEficiente, RadTensionLbValor, RadTensionLbPorcent,
                                               RadTensionLbObs);
                                break;
                            case 13: CargaMedicionDetalleConcepto(medicionDetalle, RadTensionLcEqIneficiente,
                                               RadTensionLcEqEficiente, RadTensionLcValor, RadTensionLcPorcent,
                                               RadTensionLcObs);
                                break;
                            case 14: CargaMedicionDetalleConcepto(medicionDetalle, RadCorrienteLaEqIneficiente,
                                               RadCorrienteLaEqEficiente, RadCorrienteLaValor, RadCorrienteLaPorcent,
                                               RadCorrienteLaObs);
                                break;
                            case 15: CargaMedicionDetalleConcepto(medicionDetalle, RadCorrienteLbEqIneficiente,
                                               RadCorrienteLbEqEficiente, RadCorrienteLbValor, RadCorrienteLbPorcent,
                                               RadCorrienteLbObs);
                                break;
                            case 16: CargaMedicionDetalleConcepto(medicionDetalle, RadCorrienteLcEqIneficiente,
                                               RadCorrienteLcEqEficiente, RadCorrienteLcValor, RadCorrienteLcPorcent,
                                               RadCorrienteLcObs);
                                break;
                        }
                        ChkTrifasico.Checked = true;
                        PnlTrifasico.Visible = true;
                    }
                    else
                    {
                        PnlTrifasico.Visible = false;
                        ChkTrifasico.Checked = false;
                    }
                }

                OcultaControlesMedicionDetalle(idgrupo);
            }
            else
            {
                LimpiaMedicionDetalle();
                PnlTrifasico.Visible = false;
                ChkTrifasico.Checked = false;
            }
        }

        protected void CargaMedicionDetalleConcepto(MedicionDetalle medicionDetalle, RadNumericTextBox equipoIneficiente, 
                                        RadNumericTextBox equipoEficiente, RadNumericTextBox valor,
                                        RadNumericTextBox porcentaje, RadTextBox observaciones)
        {
            equipoIneficiente.Text = medicionDetalle.EquipoIneficiente.ToString();
            equipoEficiente.Text = medicionDetalle.EquipoEficiente.ToString();
            valor.Text = medicionDetalle.Valor.ToString();
            porcentaje.Text = medicionDetalle.Porcentaje.ToString();
            observaciones.Text = medicionDetalle.Observaciones;
        }

        protected void LimpiaMedicionDetalle()
        {
            LimpiaMedicionDetalleConcepto(RadDemandMaxEqIneficiente,
                                           RadDemandMaxEqEficiente, RadDemandMaxValor, RadDemandMaxPorcent,
                                           RadDemandMaxObs);
                            
            LimpiaMedicionDetalleConcepto(RadDemandMedEqIneficiente,
                            RadDemandMedEqEficiente, RadDemandMedValor, RadDemandMedPorcent,
                            RadDemandMedObs);

            LimpiaMedicionDetalleConcepto(RadConsumoDiaEqIneficiente,
                            RadConsumoDiaEqEficiente, RadConsumoDiaValor, RadConsumoDiaPorcent,
                            RadConsumoDiaObs);

            LimpiaMedicionDetalleConcepto(RadConsumoAnualEqIneficiente,
                            RadConsumoAnualEqEficiente, RadConsumoAnualValor, RadCosumoAnualPorcent,
                            RadConsumoAnualObs);

            LimpiaMedicionDetalleConcepto(RadFactPotenciaEqIneficiente,
                            RadFactPotenciaEqEficiente, RadFactPotenciaValor, RadFactPotenciaPorcent,
                            RadFactPotenciaObs);

            LimpiaMedicionDetalleConcepto(RadTensionLineaEqIneficiente,
                                           RadTensionLineaEqEficiente, RadTensionLineaValor, RadTensionLineaPorcent,
                                           RadTensionLineaObs);

            LimpiaMedicionDetalleConcepto(RadCorrienteLineaEqIneficiente,
                                           RadCorrienteLineaEqEficiente, RadCorrienteLineaValor, RadCorrienteLineaPorcent,
                                           RadCorrienteLineaObs);

            LimpiaMedicionDetalleConcepto(RadTemperaturaEqIneficiente,
                                           RadTemperaturaEqEficiente, RadTemperaturaValor, RadTemperaturaPorcent,
                                           RadTemperaturaObs);

            //LimpiaMedicionDetalleConcepto(RadVoltajeLineaEqIneficiente,
            //                               RadVoltajeLineaEqEficiente, RadVoltajeLineaValor, RadVoltajeLineaPorcent,
            //                               RadVoltajeLineaObs);

            LimpiaMedicionDetalleConcepto(RadFlujoLuminosoEqIneficiente,
                                           RadFlujoLuminosoEqEficiente, RadFlujoLuminosoValor, RadFlujoLuminosoPorcent,
                                           RadFlujoLuminosoObs);

            LimpiaMedicionDetalleConcepto(RadTensionLaEqIneficiente,
                                           RadTensionLaEqEficiente, RadTensionLaValor, RadTensionLaPorcent,
                                           RadTensionLaObs);

            LimpiaMedicionDetalleConcepto(RadTensionLbEqIneficiente,
                                           RadTensionLbEqEficiente, RadTensionLbValor, RadTensionLbPorcent,
                                           RadTensionLbObs);

            LimpiaMedicionDetalleConcepto(RadTensionLcEqIneficiente,
                                           RadTensionLcEqEficiente, RadTensionLcValor, RadTensionLcPorcent,
                                           RadTensionLcObs);

            LimpiaMedicionDetalleConcepto(RadCorrienteLaEqIneficiente,
                                           RadCorrienteLaEqEficiente, RadCorrienteLaValor, RadCorrienteLaPorcent,
                                           RadCorrienteLaObs);

            LimpiaMedicionDetalleConcepto(RadCorrienteLbEqIneficiente,
                                           RadCorrienteLbEqEficiente, RadCorrienteLbValor, RadCorrienteLbPorcent,
                                           RadCorrienteLbObs);

            LimpiaMedicionDetalleConcepto(RadCorrienteLcEqIneficiente,
                                           RadCorrienteLcEqEficiente, RadCorrienteLcValor, RadCorrienteLcPorcent,
                                           RadCorrienteLcObs);
        }

        protected void LimpiaMedicionDetalleConcepto(RadNumericTextBox equipoIneficiente,
                                        RadNumericTextBox equipoEficiente, RadNumericTextBox valor,
                                        RadNumericTextBox porcentaje, RadTextBox observaciones)
        {
            equipoIneficiente.Text = "";
            equipoEficiente.Text = "";
            valor.Text = "";
            porcentaje.Text = "";
            observaciones.Text = "";
        }

        protected void MuestraOcultaMedicionDetalleConcepto(RadNumericTextBox equipoIneficiente,
                                        RadNumericTextBox equipoEficiente, RadNumericTextBox valor,
                                        RadNumericTextBox porcentaje, RadTextBox observaciones, Label etiqueta, Label lblUnidad, bool muestra)
        {
            equipoIneficiente.Visible = muestra;
            equipoEficiente.Visible = muestra;
            valor.Visible = muestra;
            porcentaje.Visible = muestra;
            observaciones.Visible = muestra;
            etiqueta.Visible = muestra;
            lblUnidad.Visible = muestra;
        }

        protected void GuardaDatosMedicion(bool estatusMedicion)
        {
            LstMedicionDetalle.RemoveAll(me => me.IdGrupo == IdGrupo && me.IdMedicionDetalle == 0);
            GeneraMedicionDetalle();

            var medicion = new MedicionesLN().ObtenMedicion(IdMedicion);
            medicion.IdPersonalMedicion = int.Parse(RadCmbNombreMedicion.SelectedValue);
            medicion.FechaMedicionExPost = RadTxtFechaMedicionExPost.SelectedDate;
            medicion.FechaMedicionExAnte = RadTxtFechaMedicionExAnte.SelectedDate;
            medicion.ComentariosGenerales = RadTxtCometGenerales.Text;
            medicion.Estatus = estatusMedicion;

            if (new MedicionesLN().ActualizaMedicion(medicion))
            {
                if (LstMedicionDetalle.Count > 0)
                {
                    var guardaMedicionDetalle = new MedicionesLN().GuardaMedicionDetalle(LstMedicionDetalle);
                    new MedicionesLN().ActualizaGruposMedicion(LstGruposCredito, IdMedicion);
                }
            }
        }

        protected void CargaDatosMedicion()
        {
            var medicion = new MedicionesLN().ObtenMedicion(IdMedicion);

            if (medicion != null)
            {
                RadTxtCometGenerales.Text = medicion.ComentariosGenerales;
                RadCmbNombreMedicion.SelectedValue = medicion.IdPersonalMedicion != null
                                                         ? medicion.IdPersonalMedicion.ToString()
                                                         : "";
                if (medicion.FechaMedicionExAnte != null)
                    RadTxtFechaMedicionExAnte.SelectedDate = medicion.FechaMedicionExAnte;

                if (medicion.FechaMedicionExPost != null)
                    RadTxtFechaMedicionExPost.SelectedDate = medicion.FechaMedicionExPost;

                var listMedicionDetalle = new MedicionesLN().ObtenDetalleMedicion(IdMedicion);

                if (listMedicionDetalle != null && listMedicionDetalle.Count > 0)
                {
                    LstMedicionDetalle = listMedicionDetalle;

                    var primerGrupo = LstGruposCredito.FirstOrDefault();
                    IdGrupo = primerGrupo.IdGrupo;
                    Grupo = primerGrupo.Grupo;

                    CargaMedicionDetalle(IdGrupo);
                }

                var foto = new MedicionesLN().ObtenFotoMedicion(IdMedicion);

                if (foto != null)
                {
                    imgOkCuestionario.ImageUrl = "~/CentralModule/images/icono_correcto.png";
                    imgOkCuestionario.Visible = true;
                }
            }
        }

        protected void OcultaControlesMedicionDetalle(int idGrupo)
        {
            var grupoTecnologia = new MedicionesLN().ObtenGrupo(NumeroCredito, idGrupo);
            
            if (grupoTecnologia.IdTecnologia == 1 || grupoTecnologia.IdTecnologia == 2)
            {
                if (!ChkTrifasico.Checked)
                {
                    //MuestraOcultaMedicionDetalleConcepto(RadVoltajeLineaEqIneficiente,
                    //                       RadVoltajeLineaEqEficiente, RadVoltajeLineaValor, RadVoltajeLineaPorcent,
                    //                       RadVoltajeLineaObs, Label9, Label28, false);

                    MuestraOcultaMedicionDetalleConcepto(RadFlujoLuminosoEqIneficiente,
                                                   RadFlujoLuminosoEqEficiente, RadFlujoLuminosoValor, RadFlujoLuminosoPorcent,
                                                   RadFlujoLuminosoObs, Label10, Label29, false);

                    MuestraOcultaMedicionDetalleConcepto(RadTensionLineaEqIneficiente,
                                           RadTensionLineaEqEficiente, RadTensionLineaValor, RadTensionLineaPorcent,
                                           RadTensionLineaObs, Label6, Label25, true);

                    MuestraOcultaMedicionDetalleConcepto(RadCorrienteLineaEqIneficiente,
                                                   RadCorrienteLineaEqEficiente, RadCorrienteLineaValor, RadCorrienteLineaPorcent,
                                                   RadCorrienteLineaObs, Label7, Label26, true);

                    MuestraOcultaMedicionDetalleConcepto(RadTemperaturaEqIneficiente,
                                                   RadTemperaturaEqEficiente, RadTemperaturaValor, RadTemperaturaPorcent,
                                                   RadTemperaturaObs, Label8, Label27, true);
                }
                else
                {
                    //MuestraOcultaMedicionDetalleConcepto(RadVoltajeLineaEqIneficiente,
                    //                       RadVoltajeLineaEqEficiente, RadVoltajeLineaValor, RadVoltajeLineaPorcent,
                    //                       RadVoltajeLineaObs, Label9,Label28, true);

                    MuestraOcultaMedicionDetalleConcepto(RadFlujoLuminosoEqIneficiente,
                                                   RadFlujoLuminosoEqEficiente, RadFlujoLuminosoValor, RadFlujoLuminosoPorcent,
                                                   RadFlujoLuminosoObs, Label10, Label29, false);

                    MuestraOcultaMedicionDetalleConcepto(RadTensionLineaEqIneficiente,
                                           RadTensionLineaEqEficiente, RadTensionLineaValor, RadTensionLineaPorcent,
                                           RadTensionLineaObs, Label6, Label25, false);

                    MuestraOcultaMedicionDetalleConcepto(RadCorrienteLineaEqIneficiente,
                                                   RadCorrienteLineaEqEficiente, RadCorrienteLineaValor, RadCorrienteLineaPorcent,
                                                   RadCorrienteLineaObs, Label7, Label26, true);

                    MuestraOcultaMedicionDetalleConcepto(RadTemperaturaEqIneficiente,
                                                   RadTemperaturaEqEficiente, RadTemperaturaValor, RadTemperaturaPorcent,
                                                   RadTemperaturaObs, Label8, Label27, true);
                }
            }

            if (grupoTecnologia.IdTecnologia == 3 || grupoTecnologia.IdTecnologia == 6 || grupoTecnologia.IdTecnologia == 8)
            {
                if (!ChkTrifasico.Checked)
                {
                    //MuestraOcultaMedicionDetalleConcepto(RadVoltajeLineaEqIneficiente,
                    //                       RadVoltajeLineaEqEficiente, RadVoltajeLineaValor, RadVoltajeLineaPorcent,
                    //                       RadVoltajeLineaObs, Label9, Label28, true);

                    MuestraOcultaMedicionDetalleConcepto(RadFlujoLuminosoEqIneficiente,
                                                   RadFlujoLuminosoEqEficiente, RadFlujoLuminosoValor, RadFlujoLuminosoPorcent,
                                                   RadFlujoLuminosoObs, Label10, Label29, true);

                    MuestraOcultaMedicionDetalleConcepto(RadTensionLineaEqIneficiente,
                                           RadTensionLineaEqEficiente, RadTensionLineaValor, RadTensionLineaPorcent,
                                           RadTensionLineaObs, Label6, Label25, true);

                    MuestraOcultaMedicionDetalleConcepto(RadCorrienteLineaEqIneficiente,
                                                   RadCorrienteLineaEqEficiente, RadCorrienteLineaValor, RadCorrienteLineaPorcent,
                                                   RadCorrienteLineaObs, Label7, Label26, false);

                    MuestraOcultaMedicionDetalleConcepto(RadTemperaturaEqIneficiente,
                                                   RadTemperaturaEqEficiente, RadTemperaturaValor, RadTemperaturaPorcent,
                                                   RadTemperaturaObs, Label8, Label27, false);
                }
                else
                {
                    //MuestraOcultaMedicionDetalleConcepto(RadVoltajeLineaEqIneficiente,
                    //                       RadVoltajeLineaEqEficiente, RadVoltajeLineaValor, RadVoltajeLineaPorcent,
                    //                       RadVoltajeLineaObs, Label9, Label28, true);

                    MuestraOcultaMedicionDetalleConcepto(RadFlujoLuminosoEqIneficiente,
                                                   RadFlujoLuminosoEqEficiente, RadFlujoLuminosoValor, RadFlujoLuminosoPorcent,
                                                   RadFlujoLuminosoObs, Label10, Label29, true);

                    MuestraOcultaMedicionDetalleConcepto(RadTensionLineaEqIneficiente,
                                           RadTensionLineaEqEficiente, RadTensionLineaValor, RadTensionLineaPorcent,
                                           RadTensionLineaObs, Label6, Label25, false);

                    MuestraOcultaMedicionDetalleConcepto(RadCorrienteLineaEqIneficiente,
                                                   RadCorrienteLineaEqEficiente, RadCorrienteLineaValor, RadCorrienteLineaPorcent,
                                                   RadCorrienteLineaObs, Label7, Label26, true);

                    MuestraOcultaMedicionDetalleConcepto(RadTemperaturaEqIneficiente,
                                                   RadTemperaturaEqEficiente, RadTemperaturaValor, RadTemperaturaPorcent,
                                                   RadTemperaturaObs, Label8, Label27, false);
                }
            }

            if (grupoTecnologia.IdTecnologia == 4)
            {
                if (!ChkTrifasico.Checked)
                {
                    //MuestraOcultaMedicionDetalleConcepto(RadVoltajeLineaEqIneficiente,
                    //                       RadVoltajeLineaEqEficiente, RadVoltajeLineaValor, RadVoltajeLineaPorcent,
                    //                       RadVoltajeLineaObs, Label9, Label28, true);

                    MuestraOcultaMedicionDetalleConcepto(RadFlujoLuminosoEqIneficiente,
                                                   RadFlujoLuminosoEqEficiente, RadFlujoLuminosoValor, RadFlujoLuminosoPorcent,
                                                   RadFlujoLuminosoObs, Label10, Label29, false);

                    MuestraOcultaMedicionDetalleConcepto(RadTensionLineaEqIneficiente,
                                           RadTensionLineaEqEficiente, RadTensionLineaValor, RadTensionLineaPorcent,
                                           RadTensionLineaObs, Label6, Label25, true);

                    MuestraOcultaMedicionDetalleConcepto(RadCorrienteLineaEqIneficiente,
                                                   RadCorrienteLineaEqEficiente, RadCorrienteLineaValor, RadCorrienteLineaPorcent,
                                                   RadCorrienteLineaObs, Label7, Label26, false);

                    MuestraOcultaMedicionDetalleConcepto(RadTemperaturaEqIneficiente,
                                                   RadTemperaturaEqEficiente, RadTemperaturaValor, RadTemperaturaPorcent,
                                                   RadTemperaturaObs, Label8, Label27, false);
                }
                else
                {
                    //MuestraOcultaMedicionDetalleConcepto(RadVoltajeLineaEqIneficiente,
                    //                       RadVoltajeLineaEqEficiente, RadVoltajeLineaValor, RadVoltajeLineaPorcent,
                    //                       RadVoltajeLineaObs, Label9, Label28, true);

                    MuestraOcultaMedicionDetalleConcepto(RadFlujoLuminosoEqIneficiente,
                                                   RadFlujoLuminosoEqEficiente, RadFlujoLuminosoValor, RadFlujoLuminosoPorcent,
                                                   RadFlujoLuminosoObs, Label10, Label29, false);

                    MuestraOcultaMedicionDetalleConcepto(RadTensionLineaEqIneficiente,
                                           RadTensionLineaEqEficiente, RadTensionLineaValor, RadTensionLineaPorcent,
                                           RadTensionLineaObs, Label6, Label25, false);

                    MuestraOcultaMedicionDetalleConcepto(RadCorrienteLineaEqIneficiente,
                                                   RadCorrienteLineaEqEficiente, RadCorrienteLineaValor, RadCorrienteLineaPorcent,
                                                   RadCorrienteLineaObs, Label7, Label26, true);

                    MuestraOcultaMedicionDetalleConcepto(RadTemperaturaEqIneficiente,
                                                   RadTemperaturaEqEficiente, RadTemperaturaValor, RadTemperaturaPorcent,
                                                   RadTemperaturaObs, Label8, Label27, false);
                }
            }
        }

        #endregion

        #region Eventos Controles

        protected void rgEquiposMedicion_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var lstEquiposMedicion = new MedicionesLN().ObtenEquiposMedicion(IdMedicion);
            rgEquiposMedicion.DataSource = lstEquiposMedicion;
        }

        protected void rgEquiposMedicion_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                var item = e.Item as GridEditableItem;
                var user = ((US_USUARIOModel)Session["UserInfo"]).Nombre_Usuario;

                var equipo = new MRV_EQUIPOS_MEDICION();
                equipo.IdMedicion = IdMedicion;
                equipo.EquipoMedicion = (item["EquipoMedicion"].Controls[1] as RadTextBox).Text;
                equipo.Modelo = (item["Modelo"].Controls[1] as RadTextBox).Text;
                equipo.Marca = (item["Marca"].Controls[1] as RadTextBox).Text;
                equipo.FechaUltimaCalibracion =
                    Convert.ToDateTime((item["FechaUltimaCalibracion"].Controls[1] as RadDatePicker).SelectedDate);
                equipo.Estatus = true;
                equipo.FechaAdicion = DateTime.Now.Date;
                equipo.AdicionadoPor = user;

                var newEquipo = new MedicionesLN().GuardaEquipoMedicion(equipo);

                if (newEquipo != null)
                {
                    var lstEquiposMedicion = new MedicionesLN().ObtenEquiposMedicion(IdMedicion);
                    rgEquiposMedicion.DataSource = lstEquiposMedicion;
                }
                else
                {
                    rwmVentana.RadAlert("No se pudo Agregar el equipo de Medición", 300, 150, "Alta y Baja de Equipos", null);
                }
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert(ex.Message, 300, 150, "Alta y Baja de Equipos", null);
            }
            
        }

        protected void rgEquiposMedicion_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                var item = e.Item as GridEditableItem;

                var id = int.Parse(item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["IdEquipoMedicion"].ToString());

                var elimina = new MedicionesLN().EliminaEquipoMedicion(id);

                if (elimina)
                {
                    var lstEquiposMedicion = new MedicionesLN().ObtenEquiposMedicion(IdMedicion);
                    rgEquiposMedicion.DataSource = lstEquiposMedicion;
                }
                else
                {
                    rwmVentana.RadAlert("No se pudo eliminar el equipo de Medición", 300, 150, "Alta y Baja de Equipos", null);
                }
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert(ex.Message, 300, 150, "Alta y Baja de Equipos", null);
            }
        }

        protected void RadGrupoSolicitud_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (IdGrupo == 0)
            {
                LLenaEquiposBajaAlta(NumeroCredito);
                IdGrupo = int.Parse(RadGrupoSolicitud.SelectedValue);
                Grupo = RadGrupoSolicitud.SelectedItem.Text;
                PnlMedicionDetalle.Enabled = true;
                OcultaControlesMedicionDetalle(IdGrupo);
            }
            else
            {
                LstMedicionDetalle.RemoveAll(me => me.IdGrupo == IdGrupo && me.IdMedicionDetalle == 0);
                GeneraMedicionDetalle();
                CargaMedicionDetalle(int.Parse(RadGrupoSolicitud.SelectedValue));
                IdGrupo = int.Parse(RadGrupoSolicitud.SelectedValue);
                Grupo = RadGrupoSolicitud.SelectedItem.Text;
                LLenaEquiposBajaAlta(NumeroCredito);
            }
            
        }

        protected void ChkTrifasico_CheckedChanged(object sender, EventArgs e)
        {
            PnlTrifasico.Visible = ChkTrifasico.Checked;
            OcultaControlesMedicionDetalle(IdGrupo);
        }

        protected void RadBtnFinalizar_Click(object sender, EventArgs e)
        {
            rwmVentana.RadConfirm(
                "Al finalizar la captura los datos no se podrán editar. ¿Esta seguro de Finalizar?",
                "confirmCallBackFn", 300, 100, null, "Finaliza Medición");

        }

        protected void RadBtnGuardar_Click(object sender, EventArgs e)
        {
            GuardaDatosMedicion(false);
            rwmVentana.RadAlert("Se Actualizarón los datos correctamente", 300, 150, "Actualización de Datos", null);
            InicializaConsultaEdit(NumeroCredito, IdMedicion, true);         
        }

        

        protected void btnRefresh2_Click(object sender, EventArgs e)
        {

        }

        protected void UploadedArchivoMedicion_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            if (e.File.FileName != null)
            {
                var usuario = ((US_USUARIOModel) Session["UserInfo"]).Nombre_Usuario;
                var medicionLN = new MedicionesLN();
                var foto = new MRV_FOTOS_MEDICION();
                foto.Nombre = e.File.GetName();
                foto.Extension = e.File.GetExtension();
                foto.Longitud = e.File.ContentLength;
                var b = new byte[e.File.ContentLength];
                e.File.InputStream.Read(b, 0, e.File.ContentLength);

                foto.IdMedicion = IdMedicion;
                foto.Foto = b;
                foto.Estatus = true;
                foto.FechaAdicion = DateTime.Now.Date;
                foto.AdicionadoPor = usuario;

                if (medicionLN.GuardaFotoMedicion(foto, IdMedicion))
                {
                    imgOkCuestionario.ImageUrl = "~/CentralModule/images/icono_correcto.png";
                    imgOkCuestionario.Visible = true;
                }
                else
                {
                    imgOkCuestionario.ImageUrl = "~/CentralModule/images/eliminar-icono.png";
                    imgOkCuestionario.Visible = true;
                }

                ClearContents(sender as Control);
            }
            else
            {
                ClearContents(sender as Control);
                rwmVentana.RadAlert("Ocurrió un Problema al cargar el archivo", 300, 150, "Actualización de Datos", null);
            }
        }

        private void ClearContents(Control control)
        {
            for (var i = 0; i < Session.Keys.Count; i++)
            {
                if (Session.Keys[i].Contains(control.ClientID))
                {
                    Session.Remove(Session.Keys[i]);
                    break;
                }
            }
        }

        protected void RadBtnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdmonMRV.aspx?Token=" +
                                          Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(NumeroCredito)));
        }

        protected void hidBtnFinaliza_Click(object sender, EventArgs e)
        {
            GuardaDatosMedicion(true);

            Response.Redirect("AdmonMRV.aspx?Token=" +
                                          Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(NumeroCredito)));
            //InicializaConsultaEdit(NumeroCredito, IdMedicion, false); 
        }

        #endregion                

        protected void rgEquiposMedicion_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (!e.Item.IsInEditMode) return;

            var item = (GridEditableItem)e.Item;

            if ((e.Item is IGridInsertItem))
            {
                var calendario = (RadDatePicker)item.FindControl("FechaUltimaCalibracionRadDatePicker");

                if (calendario != null)
                {
                    calendario.MaxDate = DateTime.Now.Date;
                }
            }
        }

        
    }
}