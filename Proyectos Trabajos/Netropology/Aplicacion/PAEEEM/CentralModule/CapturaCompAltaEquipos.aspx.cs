using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.AccesoDatos.AltaBajaEquipos;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entidades;
using PAEEEM.Entidades.AltaBajaEquipos;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using PAEEEM.LogicaNegocios.AltaBajaEquipos;
using PAEEEM.LogicaNegocios.SolicitudCredito;
using Telerik.Web.UI;

namespace PAEEEM.CentralModule
{
    public partial class CapturaCompAltaEquipos : Page
    {

        #region Propiedades
            public int IdCredProducto
            {
                get
                {
                    return ViewState["IdCredSustitucion"] == null ? 1 : int.Parse(ViewState["IdCredSustitucion"].ToString());
                }
                set
                {
                    ViewState["IdCredSustitucion"] = value;
                }
            }
            public int Tipo_Sociedad
            {
                get
                {
                    return ViewState["Tipo_Sociedad"] == null ? 1 : int.Parse(ViewState["Tipo_Sociedad"].ToString());
                }
                set
                {
                    ViewState["Tipo_Sociedad"] = value;
                }
            }
            public string CreditNumber
            {
                get { return ViewState["CreditNumber"] == null ? "" : ViewState["CreditNumber"].ToString(); }
                set { ViewState["CreditNumber"] = value; }
            }
        #endregion

        #region Inicio
            protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
            if (Session["UserInfo"] == null)
            {
                Response.Redirect("../Login/Login.aspx");
                return;
            }
            IniciaCapturaCompAltaEquipos();
        }
            private void IniciaCapturaCompAltaEquipos()
        {
            CreditNumber = "PAEEEMDB03A08236";
            // PAEEEMDB03A08236
            // Adquisicion "PAEEEMDB01A04682"; 
            // Sustitucion  "PAEEEMDX12J00120";
            txtCreditoNumAltaEquipos.Text = CreditNumber;
            txtRazonSocialAltaEquipos.Text = SolicitudCreditoAcciones.ObtenNombreComercial(CreditNumber);
            var dt = InfoCompAltaBajaEquipos.Get_Info_Equipos_Alta(CreditNumber);
            var lstEquipos = InfoCompAltaBajaEquipos.Get_Info_Equipos_Alta_Por_Cantidad(dt);
            grdEquiposAlta.DataSource = lstEquipos;
            grdEquiposAlta.DataBind();
            datosComplementariosAlta.CssClass = "PanelNoVisible";
        }
            protected void CargaInformacionInicial(int idCreditoProducto, byte idConsecutivo)
            {

                // Carga Inicial equipos Viejos
                for (byte a = 0; a <= 7; a++)
                {
                    var existeHorario = CapturaSolicitud.ObtenHorariosOperacionPorDiaOperacion_IdCredProd(CreditNumber, 3, a,
                        idCreditoProducto, idConsecutivo);
                    if (existeHorario == null) continue;

                    switch (a)
                    {
                        case 1:
                            DDXInicioLunesAltaEquipoViejo.SelectedItem.Text = existeHorario.Hora_Inicio;
                            hlabor1.Value = (double?) existeHorario.Horas_Laborables;
                            break;
                        case 2:
                            DDXInicioMartesAltaEquipoViejo.SelectedItem.Text = existeHorario.Hora_Inicio;
                            hlabor2.Value = (double?) existeHorario.Horas_Laborables;
                            break;
                        case 3:
                            DDXInicioMiercolesAltaEquipoViejo.SelectedItem.Text = existeHorario.Hora_Inicio;
                            hlabor3.Value = (double?) existeHorario.Horas_Laborables;
                            break;
                        case 4:
                            DDXInicioJuevesAltaEquipoViejo.SelectedItem.Text = existeHorario.Hora_Inicio;
                            hlabor4.Value = (double?) existeHorario.Horas_Laborables;
                            break;
                        case 5:
                            DDXInicioViernesAltaEquipoViejo.SelectedItem.Text = existeHorario.Hora_Inicio;
                            hlabor5.Value = (double?) existeHorario.Horas_Laborables;
                            break;
                        case 6:
                            DDXInicioSabadoAltaEquipoViejo.SelectedItem.Text = existeHorario.Hora_Inicio;
                            hlabor6.Value = (double?) existeHorario.Horas_Laborables;
                            break;
                        case 7:
                            DDXInicioDomingoAltaEquipoViejo.SelectedItem.Text = existeHorario.Hora_Inicio;
                            hlabor7.Value = (double?) existeHorario.Horas_Laborables;
                            break;
                    }
                }
                var totHorasOperacion = CapturaSolicitud.ObtenTotalHorariosOperacion_idCredProd(CreditNumber, idCreditoProducto, idConsecutivo, 3);
                if (totHorasOperacion != null)
                {
                    TxtHorasAnioAltaEquipoViejo.Text = totHorasOperacion.HORAS_AÑO.ToString();
                    TxtHorasSemanaAltaEquipoViejo.Text = totHorasOperacion.HORAS_SEMANA.ToString();
                    noSemanasAltaEquipoViejo.Value = totHorasOperacion.SEMANAS_AÑO;
                }

                // Carga Inicial equipos Nuevo
                for (byte a = 0; a <= 7; a++)
                {
                    var existeHorario = CapturaSolicitud.ObtenHorariosOperacionPorDiaOperacion_IdCredProd(CreditNumber, 2, a,
                        idCreditoProducto, idConsecutivo);
                    if (existeHorario == null) continue;

                    switch (a)
                    {
                        case 1:
                            DDXInicioLunesAltaEquipoNuevo.SelectedItem.Text = existeHorario.Hora_Inicio;
                            hlaborNuevo1.Value = (double?) existeHorario.Horas_Laborables;
                            break;
                        case 2:
                            DDXInicioMartesAltaEquipoNuevo.SelectedItem.Text = existeHorario.Hora_Inicio;
                            hlaborNuevo2.Value = (double?)existeHorario.Horas_Laborables;
                            break;
                        case 3:
                            DDXInicioMiercolesAltaEquipoNuevo.SelectedItem.Text = existeHorario.Hora_Inicio;
                            hlaborNuevo3.Value = (double?)existeHorario.Horas_Laborables;
                            break;
                        case 4:
                            DDXInicioJuevesAltaEquipoNuevo.SelectedItem.Text = existeHorario.Hora_Inicio;
                            hlaborNuevo4.Value = (double?)existeHorario.Horas_Laborables;
                            break;
                        case 5:
                            DDXInicioViernesAltaEquipoNuevo.SelectedItem.Text = existeHorario.Hora_Inicio;
                            hlaborNuevo5.Value = (double?)existeHorario.Horas_Laborables;
                            break;
                        case 6:
                            DDXInicioSabadoAltaEquipoNuevo.SelectedItem.Text = existeHorario.Hora_Inicio;
                            hlaborNuevo6.Value = (double?)existeHorario.Horas_Laborables;
                            break;
                        case 7:
                            DDXInicioDomingoAltaEquipoNuevo.SelectedItem.Text = existeHorario.Hora_Inicio;
                            hlaborNuevo7.Value = (double?)existeHorario.Horas_Laborables;
                            break;
                    }
                }
                var totHorasOperacionNuevo = CapturaSolicitud.ObtenTotalHorariosOperacion_idCredProd(CreditNumber, idCreditoProducto, idConsecutivo, 2);
                if (totHorasOperacionNuevo != null)
                {
                    TxtHorasAnioAltaEquipoNuevo.Text = totHorasOperacionNuevo.HORAS_AÑO.ToString();
                    TxtHorasSemanaAltaEquipoNuevo.Text = totHorasOperacionNuevo.HORAS_SEMANA.ToString();
                    TxtSemanasAnioAltaEquipoNuevo.Value = totHorasOperacionNuevo.SEMANAS_AÑO;
                }

            }
            protected void grdEquiposAlta_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int idTecnologia = ((GrdEquiposAlta)(e.Row.DataItem)).Cve_Tecnologia;
                    int idConsecutivo = ((GrdEquiposAlta)(e.Row.DataItem)).idConsecutivo;
                    int idCreditoProducto = ((GrdEquiposAlta)(e.Row.DataItem)).ID_CREDITO_PRODUCTO;

                    if (InfoCompAltaBajaEquipos.CargaCompletaCreditoProducto(txtCreditoNumAltaEquipos.Text, idConsecutivo, idTecnologia, idCreditoProducto))
                    {
                        e.Row.BackColor = Color.Chartreuse;
                    }

                }
            }


        #endregion
        
        #region Llenar Catalogos

        protected void LLenaHorariosEquiposNuevos()
        {
            var listHorarios = CatalogosSolicitud.ObtenHorariosTrabajo();

            DDXInicioLunesAltaEquipoNuevo.DataSource = listHorarios;
            DDXInicioLunesAltaEquipoNuevo.DataValueField = "CveValorCatalogo";
            DDXInicioLunesAltaEquipoNuevo.DataTextField = "DescripcionCatalogo";
            DDXInicioLunesAltaEquipoNuevo.DataBind();
            DDXInicioLunesAltaEquipoNuevo.Items.Insert(0, "Seleccione");
            DDXInicioLunesAltaEquipoNuevo.SelectedIndex = 0;

            DDXInicioMartesAltaEquipoNuevo.DataSource = listHorarios;
            DDXInicioMartesAltaEquipoNuevo.DataValueField = "CveValorCatalogo";
            DDXInicioMartesAltaEquipoNuevo.DataTextField = "DescripcionCatalogo";
            DDXInicioMartesAltaEquipoNuevo.DataBind();
            DDXInicioMartesAltaEquipoNuevo.Items.Insert(0, "Seleccione");
            DDXInicioMartesAltaEquipoNuevo.SelectedIndex = 0;

            DDXInicioMiercolesAltaEquipoNuevo.DataSource = listHorarios;
            DDXInicioMiercolesAltaEquipoNuevo.DataValueField = "CveValorCatalogo";
            DDXInicioMiercolesAltaEquipoNuevo.DataTextField = "DescripcionCatalogo";
            DDXInicioMiercolesAltaEquipoNuevo.DataBind();
            DDXInicioMiercolesAltaEquipoNuevo.Items.Insert(0, "Seleccione");
            DDXInicioMiercolesAltaEquipoNuevo.SelectedIndex = 0;

            DDXInicioJuevesAltaEquipoNuevo.DataSource = listHorarios;
            DDXInicioJuevesAltaEquipoNuevo.DataValueField = "CveValorCatalogo";
            DDXInicioJuevesAltaEquipoNuevo.DataTextField = "DescripcionCatalogo";
            DDXInicioJuevesAltaEquipoNuevo.DataBind();
            DDXInicioJuevesAltaEquipoNuevo.Items.Insert(0, "Seleccione");
            DDXInicioJuevesAltaEquipoNuevo.SelectedIndex = 0;

            DDXInicioViernesAltaEquipoNuevo.DataSource = listHorarios;
            DDXInicioViernesAltaEquipoNuevo.DataValueField = "CveValorCatalogo";
            DDXInicioViernesAltaEquipoNuevo.DataTextField = "DescripcionCatalogo";
            DDXInicioViernesAltaEquipoNuevo.DataBind();
            DDXInicioViernesAltaEquipoNuevo.Items.Insert(0, "Seleccione");
            DDXInicioViernesAltaEquipoNuevo.SelectedIndex = 0;

            DDXInicioSabadoAltaEquipoNuevo.DataSource = listHorarios;
            DDXInicioSabadoAltaEquipoNuevo.DataValueField = "CveValorCatalogo";
            DDXInicioSabadoAltaEquipoNuevo.DataTextField = "DescripcionCatalogo";
            DDXInicioSabadoAltaEquipoNuevo.DataBind();
            DDXInicioSabadoAltaEquipoNuevo.Items.Insert(0, "Seleccione");
            DDXInicioSabadoAltaEquipoNuevo.SelectedIndex = 0;

            DDXInicioDomingoAltaEquipoNuevo.DataSource = listHorarios;
            DDXInicioDomingoAltaEquipoNuevo.DataValueField = "CveValorCatalogo";
            DDXInicioDomingoAltaEquipoNuevo.DataTextField = "DescripcionCatalogo";
            DDXInicioDomingoAltaEquipoNuevo.DataBind();
            DDXInicioDomingoAltaEquipoNuevo.Items.Insert(0, "Seleccione");
            DDXInicioDomingoAltaEquipoNuevo.SelectedIndex = 0;

        }
        protected void LLenaHorariosEquiposViejos()
        {
            var listHorarios = CatalogosSolicitud.ObtenHorariosTrabajo();

            DDXInicioLunesAltaEquipoViejo.DataSource = listHorarios;
            DDXInicioLunesAltaEquipoViejo.DataValueField = "CveValorCatalogo";
            DDXInicioLunesAltaEquipoViejo.DataTextField = "DescripcionCatalogo";
            DDXInicioLunesAltaEquipoViejo.DataBind();
            DDXInicioLunesAltaEquipoViejo.Items.Insert(0, "Seleccione");
            DDXInicioLunesAltaEquipoViejo.SelectedIndex = 0;

            DDXInicioMartesAltaEquipoViejo.DataSource = listHorarios;
            DDXInicioMartesAltaEquipoViejo.DataValueField = "CveValorCatalogo";
            DDXInicioMartesAltaEquipoViejo.DataTextField = "DescripcionCatalogo";
            DDXInicioMartesAltaEquipoViejo.DataBind();
            DDXInicioMartesAltaEquipoViejo.Items.Insert(0, "Seleccione");
            DDXInicioMartesAltaEquipoViejo.SelectedIndex = 0;

            DDXInicioMiercolesAltaEquipoViejo.DataSource = listHorarios;
            DDXInicioMiercolesAltaEquipoViejo.DataValueField = "CveValorCatalogo";
            DDXInicioMiercolesAltaEquipoViejo.DataTextField = "DescripcionCatalogo";
            DDXInicioMiercolesAltaEquipoViejo.DataBind();
            DDXInicioMiercolesAltaEquipoViejo.Items.Insert(0, "Seleccione");
            DDXInicioMiercolesAltaEquipoViejo.SelectedIndex = 0;

            DDXInicioJuevesAltaEquipoViejo.DataSource = listHorarios;
            DDXInicioJuevesAltaEquipoViejo.DataValueField = "CveValorCatalogo";
            DDXInicioJuevesAltaEquipoViejo.DataTextField = "DescripcionCatalogo";
            DDXInicioJuevesAltaEquipoViejo.DataBind();
            DDXInicioJuevesAltaEquipoViejo.Items.Insert(0, "Seleccione");
            DDXInicioJuevesAltaEquipoViejo.SelectedIndex = 0;

            DDXInicioViernesAltaEquipoViejo.DataSource = listHorarios;
            DDXInicioViernesAltaEquipoViejo.DataValueField = "CveValorCatalogo";
            DDXInicioViernesAltaEquipoViejo.DataTextField = "DescripcionCatalogo";
            DDXInicioViernesAltaEquipoViejo.DataBind();
            DDXInicioViernesAltaEquipoViejo.Items.Insert(0, "Seleccione");
            DDXInicioViernesAltaEquipoViejo.SelectedIndex = 0;

            DDXInicioSabadoAltaEquipoViejo.DataSource = listHorarios;
            DDXInicioSabadoAltaEquipoViejo.DataValueField = "CveValorCatalogo";
            DDXInicioSabadoAltaEquipoViejo.DataTextField = "DescripcionCatalogo";
            DDXInicioSabadoAltaEquipoViejo.DataBind();
            DDXInicioSabadoAltaEquipoViejo.Items.Insert(0, "Seleccione");
            DDXInicioSabadoAltaEquipoViejo.SelectedIndex = 0;

            DDXInicioDomingoAltaEquipoViejo.DataSource = listHorarios;
            DDXInicioDomingoAltaEquipoViejo.DataValueField = "CveValorCatalogo";
            DDXInicioDomingoAltaEquipoViejo.DataTextField = "DescripcionCatalogo";
            DDXInicioDomingoAltaEquipoViejo.DataBind();
            DDXInicioDomingoAltaEquipoViejo.Items.Insert(0, "Seleccione");
            DDXInicioDomingoAltaEquipoViejo.SelectedIndex = 0;

           
        }

       #endregion

        #region Botones Impresion de Documentos
        protected void btnDisplayCreditCheckList_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=Check List Expediente&CreditNumber=" + Session["CreditNumber"] + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }
        protected void btnDisplayCreditContract_Click(object sender, EventArgs e)
        {
            if (Tipo_Sociedad == (int)CompanyType.PERSONAFISICA)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=ContratoCreditoPF&CreditNumber=" + Session["CreditNumber"] + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
            else if (Tipo_Sociedad == (int)CompanyType.REPECO)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=ContratoCreditoREPECO&CreditNumber=" + Session["CreditNumber"] + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=ContratoCreditoPM&CreditNumber=" + Session["CreditNumber"] + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);

        }
        protected void btnDisplayEquipmentAct_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=Acta Entrega-Recepcion&CreditNumber=" + Session["CreditNumber"] + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }
        protected void btnDisplayCreditRequest1_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=Solicitud de Credito&CreditNumber=" + Session["CreditNumber"] + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }
        protected void btnDisplayPromissoryNote_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=Pagare&CreditNumber=" + Session["CreditNumber"] + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }
        protected void btnDisplayGuaranteeEndorsement_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=Endoso en Garantia&CreditNumber=" + Session["CreditNumber"] + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }
        protected void btnDisplayQuota1_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=Presupuesto de Inversion&CreditNumber=" + Session["CreditNumber"] + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }
        protected void btnDisplayGuarantee_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=Carta Compromiso Aval&CreditNumber=" + Session["CreditNumber"] + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }
        protected void btnDisplayRepaymentSchedule_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=Tabla de Amortizacion&CreditNumber=" + Session["CreditNumber"] + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }
        protected void btnDisplayDisposalBonusReceipt_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=Recibo de Chatarrizacion&CreditNumber=" + Session["CreditNumber"] + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }
        protected void btnDisplayReceiptToSettle_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=EquipoBajaEficiencia&CreditNumber=" + Session["CreditNumber"] + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }
        protected void btnAmortPag_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=TablaAmortizacionPagare&CreditNumber=" + Session["CreditNumber"] + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }
        protected void btnBoletaBajaEficiencia_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=Boleta_EquipoBajaEficiencia&CreditNumber=" + Session["CreditNumber"] + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }
        #endregion

        #region Editar Equipos
          protected void BtnImgEditarAltaEquipo_Click(object sender, ImageClickEventArgs e)
        {

            lblInformacionEA.Text = @"Tecnología";
            LimpiaDatosComplemetariosAlta();
            lblTipoMov.Text = "";
            EquipoViejo.Visible = false;
            EquipoNuevo.Visible = false;

            var gridViewRow = (GridViewRow)((ImageButton)sender).NamingContainer;
            hiddenRowIndexEquiboAlta.Value = gridViewRow.RowIndex.ToString(CultureInfo.InvariantCulture);
            var dataKey = grdEquiposAlta.DataKeys[gridViewRow.RowIndex];

            if (dataKey != null)
            {
                hidDataKey0.Value = dataKey[0].ToString(); //Id_Credito_Producto
                hidDataKey.Value = dataKey[1].ToString(); //idConsecutivo
                datosComplementariosAlta.CssClass = "PanelVisible";
                IdCredProducto = Convert.ToInt32(dataKey[0].ToString());
                var tecnologiaSel = grdEquiposAlta.Rows[gridViewRow.RowIndex].Cells[4].Text;
                var producto = grdEquiposAlta.Rows[gridViewRow.RowIndex].Cells[7].Text;

                lblInformacionEA.Text = @"Tecnología: " + Convert.ToString(tecnologiaSel) + @"  Producto: " +
                                        Convert.ToString(producto);
                var datosCreditoSeleccionado =
                    InfoCompAltaBajaEquipos.ObtieneCveProductoporIdCreditoProducto(IdCredProducto);
                //InfoCompAltaBajaEquipos.ObtieneProductoPorIdCreditoProducto(IdCredProducto);
                var tipoMovimiento = InfoCompAltaBajaEquipos.Get_Tipo_Movimiento(datosCreditoSeleccionado);
                HidTipoMovimiento.Value = tipoMovimiento;

                var ofoto = new CRE_FOTOS()
                {
                    No_Credito = txtCreditoNumAltaEquipos.Text,
                    idConsecutivoFoto = int.Parse(hidDataKey.Value),
                    idCreditoProducto = int.Parse(hidDataKey0.Value)
                };


                switch (tipoMovimiento)
                {
                    case "1":
                        lblTipoMov.Text = @"(Adquisicion)";
                        EquipoNuevo.Visible = true;
                        EquipoViejo.Visible = true;
                        ofoto.idTipoFoto = 2;
                        imgOKLoaderEquipoViejo.Visible = InfoCompAltaBajaEquipos.ImagenAsignada(ofoto);
                        ofoto.idTipoFoto = 3;
                        imgOkEquipoNuevo.Visible = InfoCompAltaBajaEquipos.ImagenAsignada(ofoto);
                        break;
                    case "2":
                        lblTipoMov.Text = @"(Sustitución)";
                        EquipoNuevo.Visible = true;
                        ofoto.idTipoFoto = 3;
                        imgOkEquipoNuevo.Visible = InfoCompAltaBajaEquipos.ImagenAsignada(ofoto);
                        break;
                    default:
                        lblTipoMov.Text = @"";
                        break;

                }

                LLenaHorariosEquiposNuevos();
                LLenaHorariosEquiposViejos();
                CargaInformacionInicial(int.Parse(hidDataKey0.Value), byte.Parse(hidDataKey.Value));

            }
        }
        #endregion

        #region GuardarDatosEquipos
        protected void btnGuardarDatosCompEquipoBaja_Click(object sender, EventArgs e)
        {
            // Apartir del tipo de Movimiento validar que por lo menos un horario sea capturado
            // que la fotografia de la fachada haya sido cargada
            // que la fotografia del equipo haya sido cargada
            
          //Guardar los horarios por dia y totales por tipo de movimiento
           string mensaje = string.Empty;

           if (!ValidaDatosFachada()) mensaje = "Cargue la fotografia de la fachada. \n";
             
            switch (HidTipoMovimiento.Value)
            {
                case "1":
                  mensaje +=  ValidaDatosEquipoNuevos();
                  mensaje += ValidaDatosEquiposViejos();
                    if (mensaje.Equals(string.Empty))
                    {
                        //Alta Horarios de equipo Nuevo
                        if (InsertaHorariosCreditoProducto(2, hlaborNuevo1,
                            DDXInicioLunesAltaEquipoNuevo,
                            hlaborNuevo2,
                            DDXInicioMartesAltaEquipoNuevo,
                            hlaborNuevo3,
                            DDXInicioMiercolesAltaEquipoNuevo,
                            hlaborNuevo4,
                            DDXInicioJuevesAltaEquipoNuevo,
                            hlaborNuevo5,
                            DDXInicioViernesAltaEquipoNuevo,
                            hlaborNuevo6,
                            DDXInicioViernesAltaEquipoNuevo,
                            hlaborNuevo7,
                            DDXInicioDomingoAltaEquipoNuevo,
                            double.Parse(TxtHorasSemanaAltaEquipoNuevo.Text),
                            double.Parse(TxtSemanasAnioAltaEquipoNuevo.Text),
                            double.Parse(TxtHorasAnioAltaEquipoNuevo.Text),
                            byte.Parse(hidDataKey.Value)) &&

                            // Alta horarios de equipo Viejo
                            InsertaHorariosCreditoProducto(3, hlabor1,
                                DDXInicioLunesAltaEquipoViejo,
                                hlabor2,
                                DDXInicioMartesAltaEquipoViejo,
                                hlabor3,
                                DDXInicioMiercolesAltaEquipoViejo,
                                hlabor4,
                                DDXInicioJuevesAltaEquipoViejo,
                                hlabor5,
                                DDXInicioViernesAltaEquipoViejo,
                                hlabor6,
                                DDXInicioViernesAltaEquipoViejo,
                                hlabor7,
                                DDXInicioDomingoAltaEquipoViejo,
                                double.Parse(TxtHorasSemanaAltaEquipoViejo.Text),
                                double.Parse(noSemanasAltaEquipoViejo.Text),
                                double.Parse(TxtHorasAnioAltaEquipoViejo.Text),
                                byte.Parse(hidDataKey.Value)))
                        {
                            datosComplementariosAlta.CssClass = "PanelNoVisible";
                        }


                    }
                    break;
                case "2":
                 mensaje += ValidaDatosEquiposViejos();
                 if (mensaje.Equals(string.Empty))
                 {
                     // Alta horarios de equipo Viejo
                     if(InsertaHorariosCreditoProducto(3, hlabor1,
                        DDXInicioLunesAltaEquipoViejo,
                        hlabor2,
                         DDXInicioMartesAltaEquipoViejo,
                        hlabor3,
                        DDXInicioMiercolesAltaEquipoViejo,
                        hlabor4,
                        DDXInicioJuevesAltaEquipoViejo,
                        hlabor5,
                        DDXInicioViernesAltaEquipoViejo,
                        hlabor6,
                        DDXInicioViernesAltaEquipoViejo,
                        hlabor7,
                        DDXInicioDomingoAltaEquipoViejo,
                        double.Parse(TxtHorasSemanaAltaEquipoViejo.Text),
                        double.Parse(noSemanasAltaEquipoViejo.Text),
                        double.Parse(TxtHorasAnioAltaEquipoViejo.Text),
                        byte.Parse(hidDataKey.Value)))
                       {
                            datosComplementariosAlta.CssClass = "PanelNoVisible";
                        }
                 }

                break;
                    
            }

          
            if(!mensaje.Equals(string.Empty))
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(Page), "NextError",string.Format("alert('{0}');",mensaje),true);
            
         
        }
        private void LimpiaDatosComplemetariosAlta()
        {
            TxtHorasAnioAltaEquipoNuevo.Text = "";
            TxtHorasAnioAltaEquipoViejo.Text = "";
            TxtSemanasAnioAltaEquipoNuevo.Text = "";
            noSemanasAltaEquipoViejo.Text = "";
            TxtHorasSemanaAltaEquipoNuevo.Text = "";
            TxtHorasSemanaAltaEquipoViejo.Text = "";

            HiddenFieldLunesAltaEquipoViejo.Value = "0.00";
            HiddenFieldMartesAltaEquipoViejo.Value = "0.00";
            HiddenFieldMiercolesAltaEquipoViejo.Value = "0.00";
            HiddenFieldJuevesAltaEquipoViejo.Value = "0.00";
            HiddenFieldViernesAltaEquipoViejo.Value = "0.00";
            HiddenFieldSabadoAltaEquipoViejo.Value = "0.00";
            HiddenFieldDomingoAltaEquipoViejo.Value = "0.00";

            //HiddenFieldLunesAltaEquipoNuevo.Value = "0.00";
            HiddenFieldMartesAltaEquipoNuevo.Value = "0.00";
            HiddenFieldMiercolesAltaEquipoNuevo.Value = "0.00";
            HiddenFieldJuevesAltaEquipoNuevo.Value = "0.00";
            HiddenFieldViernesAltaEquipoNuevo.Value = "0.00";
            HiddenFieldSabadoAltaEquipoNuevo.Value = "0.00";
            HiddenFieldDomingoAltaEquipoNuevo.Value = "0.00";
        }
        protected bool InsertaHorariosAltaEquipoNuevo(byte tipoHorario)
        {
            bool bad = false;
            
            var horasOperacionTotal = new H_OPERACION_TOTAL
            {
                No_Credito = CreditNumber,
                IDTIPOHORARIO = tipoHorario,
                HORAS_SEMANA = Convert.ToDouble(TxtHorasSemanaAltaEquipoNuevo.Text),
                SEMANAS_AÑO = Convert.ToDouble(TxtSemanasAnioAltaEquipoNuevo.Text),
                HORAS_AÑO = Convert.ToDouble(TxtHorasAnioAltaEquipoNuevo.Text),
                ID_CREDITO_PRODUCTO = IdCredProducto,
            };

            return bad;
        }
        protected bool InsertaHorariosAltaEquipoViejo(byte tipoHorario)
        {
            var band = false;
            var lstHorarios = new List<CLI_HORARIOS_OPERACION>();
            CLI_HORARIOS_OPERACION horario;

            //if (ChkLunesAltaEquipoViejo.Checked)
            //{
            //    horario = new CLI_HORARIOS_OPERACION
            //    {
            //        No_Credito = CreditNumber,
            //        IDTIPOHORARIO = tipoHorario,
            //        ID_DIA_OPERACION = 1, //Lunes
            //        Hora_Inicio = ChkLunesAltaEquipoViejo.Checked ? DDXInicioLunesAltaEquipoViejo.SelectedItem.Text : "",
            //        ////Hora_Fin = ChkLunesAltaEquipoViejo.Checked ? DDXFinLunesAltaEquipoViejo.SelectedItem.Text : "",
            //        ID_CREDITO_PRODUCTO = IdCredProducto,
            //        IDHORASTRABAJADAS = Convert.ToDecimal(HiddenFieldLunesAltaEquipoViejo.Value)

            //    };
            //    lstHorarios.Add(horario);
            //}

            //if (ChkMartesAltaEquipoViejo.Checked)
            //{
            //    horario = new CLI_HORARIOS_OPERACION
            //    {
            //        No_Credito = CreditNumber,
            //        IDTIPOHORARIO = tipoHorario,
            //        ID_DIA_OPERACION = 2, //Martes
            //        Hora_Inicio = ChkMartesAltaEquipoViejo.Checked ? DDXInicioMartesAltaEquipoViejo.SelectedItem.Text : "",
            //        //Hora_Fin = ChkMartesAltaEquipoViejo.Checked ? DDXFinMartesAltaEquipoViejo.SelectedItem.Text : "",
            //        ID_CREDITO_PRODUCTO = IdCredProducto,
            //        IDHORASTRABAJADAS = Convert.ToDecimal(HiddenFieldMartesAltaEquipoViejo.Value)
            //    };
            //    lstHorarios.Add(horario);
            //}

            //if (ChkMiercolesAltaEquipoViejo.Checked)
            //{
            //    horario = new CLI_HORARIOS_OPERACION
            //    {
            //        No_Credito = CreditNumber,
            //        IDTIPOHORARIO = tipoHorario,
            //        ID_DIA_OPERACION = 3, //Miercoles
            //        Hora_Inicio = ChkMiercolesAltaEquipoViejo.Checked ? DDXInicioMiercolesAltaEquipoViejo.SelectedItem.Text : "",
            //        //Hora_Fin = ChkMiercolesAltaEquipoViejo.Checked ? DDXFinMiercolesAltaEquipoViejo.SelectedItem.Text : "",
            //        ID_CREDITO_PRODUCTO = IdCredProducto,
            //        IDHORASTRABAJADAS = Convert.ToDecimal(HiddenFieldMiercolesAltaEquipoViejo.Value)
            //    };
            //    lstHorarios.Add(horario);
            //}

            //if (ChkJuevesAltaEquipoViejo.Checked)
            //{
            //    horario = new CLI_HORARIOS_OPERACION
            //    {
            //        No_Credito = CreditNumber,
            //        IDTIPOHORARIO = tipoHorario,
            //        ID_DIA_OPERACION = 4, //Jueves
            //        Hora_Inicio = ChkJuevesAltaEquipoViejo.Checked ? DDXInicioJuevesAltaEquipoViejo.SelectedItem.Text : "",
            //        //Hora_Fin = ChkJuevesAltaEquipoViejo.Checked ? DDXFinJuevesAltaEquipoViejo.SelectedItem.Text : "",
            //        ID_CREDITO_PRODUCTO = IdCredProducto,
            //        IDHORASTRABAJADAS = Convert.ToDecimal(HiddenFieldJuevesAltaEquipoViejo.Value)
            //    };
            //    lstHorarios.Add(horario);
            //}

            //if (ChkViernesAltaEquipoViejo.Checked)
            //{
            //    horario = new CLI_HORARIOS_OPERACION
            //    {
            //        No_Credito = CreditNumber,
            //        IDTIPOHORARIO = tipoHorario,
            //        ID_DIA_OPERACION = 5, //Viernes
            //        Hora_Inicio = ChkViernesAltaEquipoViejo.Checked ? DDXInicioViernesAltaEquipoViejo.SelectedItem.Text : "",
            //        //Hora_Fin = ChkViernesAltaEquipoViejo.Checked ? DDXFinViernesAltaEquipoViejo.SelectedItem.Text : "",
            //        ID_CREDITO_PRODUCTO = IdCredProducto,
            //        IDHORASTRABAJADAS = Convert.ToDecimal(HiddenFieldViernesAltaEquipoViejo.Value)
            //    };
            //    lstHorarios.Add(horario);
            //}

            //if (ChkSabadoAltaEquipoViejo.Checked)
            //{
            //    horario = new CLI_HORARIOS_OPERACION
            //    {
            //        No_Credito = CreditNumber,
            //        IDTIPOHORARIO = tipoHorario,
            //        ID_DIA_OPERACION = 6, //Sabado
            //        Hora_Inicio = ChkSabadoAltaEquipoViejo.Checked ? DDXInicioSabadoAltaEquipoViejo.SelectedItem.Text : "",
            //        //Hora_Fin = ChkSabadoAltaEquipoViejo.Checked ? DDXFinSabadoAltaEquipoViejo.SelectedItem.Text : "",
            //        ID_CREDITO_PRODUCTO = IdCredProducto,
            //        IDHORASTRABAJADAS = Convert.ToDecimal(HiddenFieldSabadoAltaEquipoViejo.Value)
            //    };
            //    lstHorarios.Add(horario);
            //}

            //if (ChkDomingoAltaEquipoViejo.Checked)
            //{
            //    horario = new CLI_HORARIOS_OPERACION
            //    {
            //        No_Credito = CreditNumber,
            //        IDTIPOHORARIO = tipoHorario,
            //        ID_DIA_OPERACION = 7, //Domingo
            //        Hora_Inicio = ChkDomingoAltaEquipoViejo.Checked ? DDXInicioDomingoAltaEquipoViejo.SelectedItem.Text : "",
            //        //Hora_Fin = ChkDomingoAltaEquipoViejo.Checked ? DDXFinDomingoAltaEquipoViejo.SelectedItem.Text : "",
            //        ID_CREDITO_PRODUCTO = IdCredProducto,
            //        IDHORASTRABAJADAS = Convert.ToDecimal(HiddenFieldDomingoAltaEquipoViejo.Value)
            //    };
            //    lstHorarios.Add(horario);
            //}

            //var horasOperacionTotal = new H_OPERACION_TOTAL
            //{
            //    No_Credito = CreditNumber,
            //    IDTIPOHORARIO = tipoHorario,
            //    HORAS_SEMANA = Convert.ToDouble(TxtHorasSemanaAltaEquipoViejo.Text),
            //    SEMANAS_AÑO = Convert.ToDouble(TxtSemanasAnioAltaEquipoViejo.Text),
            //    HORAS_AÑO = Convert.ToDouble(TxtHorasAnioAltaEquipoViejo.Text),
            //    ID_CREDITO_PRODUCTO = IdCredProducto,
            //};

            //if (SolicitudCreditoAcciones.ActualizaHorarioOperacion_IdCredSust(lstHorarios, horasOperacionTotal))
            //    band = true;

            return band;
        }
        protected bool InsertaHorariosCreditoProducto(byte tipoHorario,
                RadNumericTextBox hlabor1, DropDownList ddxLunes,
                RadNumericTextBox hlabor2, DropDownList ddxMartes,
                RadNumericTextBox hlabor3, DropDownList ddxMiercoles,
                RadNumericTextBox hlabor4, DropDownList ddxJueves,
                RadNumericTextBox hlabor5, DropDownList ddxViernes,
                RadNumericTextBox hlabor6, DropDownList ddxSabado,
                RadNumericTextBox hlabor7, DropDownList ddxDomingo,
                double horasSemana,
                double totSemanas,
                double horasAño,
                byte idConsecutivo
           )
        {
            var band = false;
            var lstHorarios = new List<CLI_HORARIOS_OPERACION>();
            int idCreditoProducto = int.Parse(hidDataKey0.Value);



            lstHorarios.Clear();
            CLI_HORARIOS_OPERACION horario;

            if (hlabor1.Value > 0 &&
                ddxLunes.SelectedItem.Text != @"Seleccione")     //Lunes
            {
                horario = new CLI_HORARIOS_OPERACION
                {
                    No_Credito = CreditNumber,
                    IDTIPOHORARIO = tipoHorario,
                    ID_DIA_OPERACION = 1,
                    Hora_Inicio = ddxLunes.SelectedItem.Text,
                    Horas_Laborables = Byte.Parse(hlabor1.Value.ToString()),
                    IDCONSECUTIVO = idConsecutivo,
                    ID_CREDITO_PRODUCTO = idCreditoProducto
                };

                lstHorarios.Add(horario);
            }

            if (hlabor2.Value > 0 &&
                ddxMartes.SelectedItem.Text != @"Seleccione")     //Martes
            {
                horario = new CLI_HORARIOS_OPERACION
                {
                    No_Credito = CreditNumber,
                    IDTIPOHORARIO = tipoHorario,
                    ID_DIA_OPERACION = 2,
                    Hora_Inicio = ddxMartes.SelectedItem.Text,
                    Horas_Laborables = Byte.Parse(hlabor2.Value.ToString()),
                    IDCONSECUTIVO = idConsecutivo,
                    ID_CREDITO_PRODUCTO = idCreditoProducto
                };
                lstHorarios.Add(horario);
            }

            if (hlabor3.Value > 0 &&
                ddxMiercoles.SelectedItem.Text != @"Seleccione")     //Miercoles
            {
                horario = new CLI_HORARIOS_OPERACION
                {
                    No_Credito = CreditNumber,
                    IDTIPOHORARIO = tipoHorario,
                    ID_DIA_OPERACION = 3,
                    Hora_Inicio = ddxMiercoles.SelectedItem.Text,
                    Horas_Laborables = Byte.Parse(hlabor3.Value.ToString()),
                    IDCONSECUTIVO = idConsecutivo,
                    ID_CREDITO_PRODUCTO = idCreditoProducto
                };
                lstHorarios.Add(horario);
            }

            if (hlabor4.Value > 0 &&
                ddxJueves.SelectedItem.Text != @"Seleccione")     //Jueves
            {
                horario = new CLI_HORARIOS_OPERACION
                {
                    No_Credito = CreditNumber,
                    IDTIPOHORARIO = tipoHorario,
                    ID_DIA_OPERACION = 4,
                    Hora_Inicio = ddxJueves.SelectedItem.Text,
                    Horas_Laborables = Byte.Parse(hlabor4.Value.ToString()),
                    IDCONSECUTIVO = idConsecutivo,
                    ID_CREDITO_PRODUCTO = idCreditoProducto
                };
                lstHorarios.Add(horario);
            }

            if (hlabor5.Value > 0 &&
                ddxViernes.SelectedItem.Text != @"Seleccione")     //Viernes
            {
                horario = new CLI_HORARIOS_OPERACION
                {
                    No_Credito = CreditNumber,
                    IDTIPOHORARIO = tipoHorario,
                    ID_DIA_OPERACION = 5,
                    Hora_Inicio = ddxViernes.SelectedItem.Text,
                    Horas_Laborables = Byte.Parse(hlabor5.Value.ToString()),
                    IDCONSECUTIVO = idConsecutivo,
                    ID_CREDITO_PRODUCTO = idCreditoProducto
                };
                lstHorarios.Add(horario);
            }

            if (hlabor6.Value > 0 &&
                ddxSabado.SelectedItem.Text != @"Seleccione")     //Sabado
            {
                horario = new CLI_HORARIOS_OPERACION
                {
                    No_Credito = CreditNumber,
                    IDTIPOHORARIO = tipoHorario,
                    ID_DIA_OPERACION = 6,
                    Hora_Inicio = ddxSabado.SelectedItem.Text,
                    Horas_Laborables = Byte.Parse(hlabor6.Value.ToString()),
                    IDCONSECUTIVO = idConsecutivo,
                    ID_CREDITO_PRODUCTO = idCreditoProducto
                };
                lstHorarios.Add(horario);
            }

            if (hlabor7.Value > 0 &&
                ddxDomingo.SelectedItem.Text != @"Seleccione")     //Domingo
            {
                horario = new CLI_HORARIOS_OPERACION
                {
                    No_Credito = CreditNumber,
                    IDTIPOHORARIO = tipoHorario,
                    ID_DIA_OPERACION = 7,
                    Hora_Inicio = ddxDomingo.SelectedItem.Text,
                    Horas_Laborables = Byte.Parse(hlabor7.Value.ToString()),
                    IDCONSECUTIVO = idConsecutivo,
                    ID_CREDITO_PRODUCTO = idCreditoProducto
                };
                lstHorarios.Add(horario);
            }

            var hOperTotal = new H_OPERACION_TOTAL
            {
                No_Credito = CreditNumber,
                IDTIPOHORARIO = tipoHorario,
                HORAS_SEMANA = horasSemana,
                SEMANAS_AÑO = totSemanas,
                HORAS_AÑO = horasAño,
                ID_CREDITO_PRODUCTO = idCreditoProducto,
                IDCONSECUTIVO = idConsecutivo
            };

            if (SolicitudCreditoAcciones.ActualizaHorarioOperacion_IdCredProducto(lstHorarios, hOperTotal))
                band = true;


            return band;
        }

        #endregion

        #region Carga Fotos
        protected void FileUploadCompleteFachada(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            try
            {

                if (AsyncFileUploadFachada.HasFile)
                {
                    var oFoto = new CRE_FOTOS();

                    string extensionDoc = AsyncFileUploadFachada.PostedFile.FileName;
                    int posicion = extensionDoc.LastIndexOf('.') + 1;
                    extensionDoc = extensionDoc.Substring(posicion, extensionDoc.Length - posicion);
                    extensionDoc = extensionDoc.ToLower();
                    oFoto.Extension = extensionDoc;

                    string nombre_archivo = AsyncFileUploadFachada.PostedFile.FileName;
                    int posicion2 = nombre_archivo.LastIndexOf('\\') + 1;
                    nombre_archivo = nombre_archivo.Substring(posicion2, nombre_archivo.Length - posicion2);
                    oFoto.Nombre = nombre_archivo;

                    if (extensionDoc == "pdf" || extensionDoc == "jpg" || extensionDoc == "jpeg")
                    {
                        byte[] b = new byte[AsyncFileUploadFachada.PostedFile.ContentLength];

                        //HttpPostedFile uploadedImage = AsyncFileUploadFachada.PostedFile;
                        //uploadedImage.InputStream.Read(b, 0, b.Length);

                        b = AsyncFileUploadFachada.FileBytes;
                       
                        oFoto.No_Credito = txtCreditoNumAltaEquipos.Text;
                        oFoto.idTipoFoto = 1; // Fachada
                        oFoto.Longitud = b.Length;
                        oFoto.Foto = b;
                        oFoto.Estatus = true;
                        oFoto.FechaAdicion = DateTime.Now;
                        oFoto.AdicionadoPor = Session["UserName"].ToString();
                        oFoto.idConsecutivoFoto = 1;
                        oFoto.idCreditoProducto = null;
                        oFoto.IdCreditoSustitucion = null;
                        
                        if (InfoCompAltaBajaEquipos.GuardarImagenFachada(oFoto))
                            imgOKLoaderFachada.ImageUrl = "~/CentralModule/images/icono_correcto.png";
                        else
                            imgOKLoaderFachada.ImageUrl = "~/CentralModule/images/eliminar-icono.png";

                          
                        
                       
                    }
                }
            }
            catch (Exception ex)
            {
               ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(Page), "NextError",
                                         string.Format("alert('{0}');",ex.Message),true);
            }
        }
        protected void FileUploadCompleteAltaEquipoViejo(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            try
                {

                    if (AsyncFileUploadAltaEquipoViejo.HasFile)
                    {
                        var oFoto = new CRE_FOTOS();

                        string extensionDoc = AsyncFileUploadAltaEquipoViejo.PostedFile.FileName;
                        int posicion = extensionDoc.LastIndexOf('.') + 1;
                        extensionDoc = extensionDoc.Substring(posicion, extensionDoc.Length - posicion);
                        extensionDoc = extensionDoc.ToLower();
                        oFoto.Extension = extensionDoc;

                        string nombre_archivo = AsyncFileUploadAltaEquipoViejo.PostedFile.FileName;
                        int posicion2 = nombre_archivo.LastIndexOf('\\') + 1;
                        nombre_archivo = nombre_archivo.Substring(posicion2, nombre_archivo.Length - posicion2);
                        oFoto.Nombre = nombre_archivo;

                        if (extensionDoc == "pdf" || extensionDoc == "jpg" || extensionDoc == "jpeg")
                        {
                            byte[] b = new byte[AsyncFileUploadAltaEquipoViejo.PostedFile.ContentLength];

                            //HttpPostedFile uploadedImage = AsyncFileUploadFachada.PostedFile;
                            //uploadedImage.InputStream.Read(b, 0, b.Length);

                            b = AsyncFileUploadAltaEquipoViejo.FileBytes;
                           
                            oFoto.No_Credito = txtCreditoNumAltaEquipos.Text;
                            oFoto.idTipoFoto = 2; // Equipo Viejo
                            oFoto.Longitud = b.Length;
                            oFoto.Foto = b;
                            oFoto.Estatus = true;
                            oFoto.FechaAdicion = DateTime.Now;
                            oFoto.AdicionadoPor = Session["UserName"].ToString();
                            oFoto.idConsecutivoFoto = int.Parse(hidDataKey.Value);
                            oFoto.idCreditoProducto = int.Parse(hidDataKey0.Value);
                            if (InfoCompAltaBajaEquipos.GuardarImagenCreditoProducto(oFoto))
                                 imgOKLoaderEquipoViejo.ImageUrl = "~/CentralModule/images/icono_correcto.png";
                            else
                                  imgOKLoaderEquipoViejo.ImageUrl = "~/CentralModule/images/eliminar-icono.png";
                            
                            
                            

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(Page), "NextError",
                                              string.Format("alert('{0}');", "Formato no valido..."), true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(Page), "NextError",
                                              string.Format("alert('{0}');", ex.Message), true);
                }
        }
        protected void FileUploadCompleteAltaEquipoNuevo(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            try
            {

                if (AsyncFileUploadEquipoNuevo.HasFile)
                {
                    var oFoto = new CRE_FOTOS();

                    string extensionDoc = AsyncFileUploadEquipoNuevo.PostedFile.FileName;
                    int posicion = extensionDoc.LastIndexOf('.') + 1;
                    extensionDoc = extensionDoc.Substring(posicion, extensionDoc.Length - posicion);
                    extensionDoc = extensionDoc.ToLower();
                    oFoto.Extension = extensionDoc;

                    string nombre_archivo = AsyncFileUploadEquipoNuevo.PostedFile.FileName;
                    int posicion2 = nombre_archivo.LastIndexOf('\\') + 1;
                    nombre_archivo = nombre_archivo.Substring(posicion2, nombre_archivo.Length - posicion2);
                    oFoto.Nombre = nombre_archivo;

                    imgOkEquipoNuevo.Visible = true;


                    if (extensionDoc == "pdf" || extensionDoc == "jpg" || extensionDoc == "jpeg")
                    {
                        byte[] b = new byte[AsyncFileUploadEquipoNuevo.PostedFile.ContentLength];

                        //HttpPostedFile uploadedImage = AsyncFileUploadFachada.PostedFile;
                        //uploadedImage.InputStream.Read(b, 0, b.Length);

                        b = AsyncFileUploadEquipoNuevo.FileBytes;
                       
                        oFoto.No_Credito = txtCreditoNumAltaEquipos.Text;
                        oFoto.idTipoFoto = 3; // Equipo Nuevo
                        oFoto.Longitud = b.Length;
                        oFoto.Foto = b;
                        oFoto.Estatus = true;
                        oFoto.FechaAdicion = DateTime.Now;
                        oFoto.AdicionadoPor = Session["UserName"].ToString();
                        oFoto.idConsecutivoFoto = int.Parse(hidDataKey.Value);
                        oFoto.idCreditoProducto = int.Parse(hidDataKey0.Value);
                        if (InfoCompAltaBajaEquipos.GuardarImagenCreditoProducto(oFoto))
                        {
                            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "refresh",
                            //             "" , true);

                            //var dt = InfoCompAltaBajaEquipos.Get_Info_Equipos_Alta(CreditNumber);
                            //var lstEquipos = InfoCompAltaBajaEquipos.Get_Info_Equipos_Alta_Por_Cantidad(dt);
                            //grdEquiposAlta.DataSource = lstEquipos;
                            //grdEquiposAlta.DataBind();
                        }
                        
                       
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(Page), "NextError",
                                          string.Format("alert('{0}');", ex.Message), true);
            }
        }
        protected void imgbtnVer_Click(object sender, ImageClickEventArgs e)
        {
            string url = string.Format("window.open('VisorImagenes.aspx?CreditNumber={0}&IdCreditoSustitucion={1}&idTipoFoto={2}&IdConsecutivo={3}','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');",
                                                                      CreditNumber, hidDataKey0.Value, "1", hidDataKey.Value);

            ScriptManager.RegisterStartupScript(this, GetType(), "PrintForm", url, true);

        }
        protected void verEquipoNuevo_Click(object sender, ImageClickEventArgs e)
        {
            string url = string.Format("window.open('VisorImagenes.aspx?CreditNumber={0}&IdCreditoSustitucion={1}&idTipoFoto={2}&IdConsecutivo={3}','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');",
                                                                       CreditNumber, hidDataKey0.Value, "3", hidDataKey.Value);

            ScriptManager.RegisterStartupScript(this, GetType(), "PrintForm", url, true);

            //var oExistfoto = new CRE_FOTOS()
            //{
            //    No_Credito = txtCreditoNumAltaEquipos.Text,
            //    idTipoFoto = 3,
            //    idCreditoProducto = int.Parse(hidDataKey0.Value),
            //    idConsecutivoFoto = int.Parse(hidDataKey.Value)
            //};
            //var oFoto = InfoCompAltaBajaEquipos.ObtenertImagenCreditoProducto(oExistfoto);

            
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(Page), "NextError",
            //                             string.Format("alert('{0}');", "No hay Foto para este producto..."), true);
            //}
        }
        protected void verEquipoViejo_Click(object sender, ImageClickEventArgs e)
        {
            string url = string.Format("window.open('VisorImagenes.aspx?CreditNumber={0}&IdCreditoSustitucion={1}&idTipoFoto={2}&IdConsecutivo={3}','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');",
                                                                      CreditNumber, hidDataKey0.Value, "2", hidDataKey.Value);

            ScriptManager.RegisterStartupScript(this, GetType(), "PrintForm", url, true);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            var status = HidstateLoad.Value;
            switch (HidActualizaOk.Value)
            {
                case "Fachada":
                    imgOKLoaderFachada.ImageUrl = string.Format("~/CentralModule/images/{0}", status.Equals("ok") ? "icono_correcto.png" : "eliminar-icono.png");
                    imgOKLoaderFachada.Visible = true;
                break;
                case "Viejo":
                    imgOKLoaderEquipoViejo.ImageUrl = string.Format("~/CentralModule/images/{0}", status.Equals("ok") ? "icono_correcto.png" : "eliminar-icono.png");
                    imgOKLoaderEquipoViejo.Visible = true;
                break;
                case "Nuevo":
                    imgOkEquipoNuevo.ImageUrl = string.Format("~/CentralModule/images/{0}",status.Equals("ok")?"icono_correcto.png":"eliminar-icono.png");
                    imgOkEquipoNuevo.Visible = true;
                break;
            }

           
        }
        #endregion
        
        #region Validaciones
        protected string ValidaDatosEquipoNuevos()
        {
            // que la fotografia de la fachada haya sido cargada
            string mensaje = string.Empty;
            //Valida los horarios por dia
            decimal hrsSemanal = TxtHorasSemanaAltaEquipoNuevo.Text.Equals(string.Empty)
                ? 0
                : decimal.Parse(TxtHorasSemanaAltaEquipoNuevo.Text);

            if (hrsSemanal < 1) mensaje = "Debe capturar el horario de operación del equipo nuevo, de por lo menos un día.<br>";

            //Validar los Horarios totales
            decimal hrsAnioTotal = TxtHorasAnioAltaEquipoNuevo.Text.Equals(string.Empty)
                ? 0
                : decimal.Parse(TxtHorasAnioAltaEquipoNuevo.Text);

            if (hrsAnioTotal < 1) mensaje += "Debe capturar el numero de semanas del equipo nuevo.<br>";

           //la fotografia del equipo haya sido cargada
            if(!InfoCompAltaBajaEquipos.ExisteFotoEquipoCreditoProducto(new CRE_FOTOS()
                                                                    {
                                                                        No_Credito = CreditNumber,
                                                                        idTipoFoto = 3,
                                                                        idConsecutivoFoto = int.Parse(hidDataKey.Value),
                                                                        idCreditoProducto = int.Parse(hidDataKey0.Value)
                                                                    }))
                mensaje += "Debe cargar la foto del equipo nuevo.<br>";
            return mensaje;
        }
        private string ValidaDatosEquiposViejos()
        {
            string mensaje = string.Empty;
            //Valida los horarios por dia
            decimal hrsSemanal = TxtHorasSemanaAltaEquipoViejo.Text.Equals(string.Empty)
                ? 0
                : decimal.Parse(TxtHorasSemanaAltaEquipoViejo.Text);

            if (hrsSemanal < 1) mensaje = "Debe capturar el horario de operación del equipo viejo, de por lo menos un día.<br>";

            //Validar los Horarios totales
            decimal hrsAnioTotal = TxtHorasAnioAltaEquipoViejo.Text.Equals(string.Empty)
                ? 0
                : decimal.Parse(TxtHorasAnioAltaEquipoViejo.Text);

            if (hrsAnioTotal < 1) mensaje += "Debe capturar el numero de semanas del equipo viejo.<br>";

            //la fotografia del equipo haya sido cargada
            if (!InfoCompAltaBajaEquipos.ExisteFotoEquipoCreditoProducto(new CRE_FOTOS()
            {
                No_Credito = CreditNumber,
                idTipoFoto = 2,
                idConsecutivoFoto = int.Parse(hidDataKey.Value),
                idCreditoProducto = int.Parse(hidDataKey0.Value)
            }))
                mensaje += "Debe cargar la foto del equipo viejo.<br>";
            return mensaje;
        }
        private bool ValidaDatosFachada()
        {
            
           return InfoCompAltaBajaEquipos.ExisteFotoFachada(new CRE_FOTOS()
                                                      {
                                                          No_Credito = CreditNumber,
                                                          idTipoFoto = 1,
                                                          idConsecutivoFoto = 1
                                                      });
            
        }
        #endregion
        
    }
}