using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Alta_Solicitud;
using PAEEEM.LogicaNegocios.SolicitudCredito;

namespace PAEEEM.SupplierModule.Controls
{
    public partial class WucValidaPyme : UserControl
    {
        public List<Colonia> LstColoniasAsentamientos
        {
            get { return (List<Colonia>)ViewState["LstColoniasAsentamientos"]; }
            set { ViewState["LstColoniasAsentamientos"] = value; }
        }

        #region carga Inicial

        protected void Page_Load(Object sender, EventArgs e)
        {
            if (IsPostBack) return;
            LlenarDdxEstado();
            LledarDDxGiroEmpresa();
            LLenaDDxSectorEconomico();
            LlenarDDxTipoPersona();

            // TxtNoRPU.Text = "001960400999"; 
            //Session["ValidRPU"].ToString();
            TxtNoRPU.Text = Session["ValidRPU"].ToString();
        }

        #endregion

        #region llenar Catalogos

        protected void LlenarDdxEstado()
        {
            
            var lstEstado = CatalogosSolicitud.ObtenCatEstadosRepublica();
            if (lstEstado == null) return;
            DDXEstado.DataSource = lstEstado;
            DDXEstado.DataValueField = "Cve_Estado";
            DDXEstado.DataTextField = "Dx_Nombre_Estado";
            DDXEstado.DataBind();

            DDXEstado.Items.Insert(0, "Seleccione");
            DDXEstado.SelectedIndex = 0;
            DDXMunicipio.Items.Insert(0, "Seleccione");
            DDXMunicipio.SelectedIndex = 0;
            DDXColonia.Items.Insert(0, "Seleccione");
            DDXColonia.SelectedIndex = 0;
        }

        protected void LlenarDDxMunicipo(int idEstado)
        {
            try
            {
                DDXMunicipio.Items.Clear();
                DDXColonia.Items.Clear();

                var lstMunicipio = CatalogosSolicitud.ObtenDelegMunicipios(idEstado);

                if (lstMunicipio == null) return;
                DDXMunicipio.DataSource = lstMunicipio;
                DDXMunicipio.DataValueField = "Cve_Deleg_Municipio";
                DDXMunicipio.DataTextField = "Dx_Deleg_Municipio";
                DDXMunicipio.DataBind();

                DDXMunicipio.Items.Insert(0, "Seleccione");
                DDXMunicipio.SelectedIndex = 0;
                DDXColonia.Items.Insert(0, "Seleccione");
                DDXColonia.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                TxtCP.Text = ex.Message;
            }
            
        }

        protected void LlenarDDxColonias(int idEstado, int idMunicipio)
        {
            try
            {
                DDXColonia.Items.Clear();
                DDXColoniaHidden.Items.Clear();

                var lstColonia = CatalogosSolicitud.ObtenCatCodigoPostals(idEstado, idMunicipio);

                if (lstColonia == null) return;
                DDXColonia.DataSource = lstColonia;
                DDXColonia.DataValueField = "Cve_CP";
                DDXColonia.DataTextField = "Dx_Colonia";
                DDXColonia.DataBind();

                DDXColonia.Items.Insert(0,"Seleccione");
                DDXColonia.SelectedIndex = 0;

                DDXColoniaHidden.DataSource = lstColonia;
                DDXColoniaHidden.DataValueField = "Cve_CP";
                DDXColoniaHidden.DataTextField = "Codigo_Postal";
                DDXColoniaHidden.DataBind();
            }
            catch (Exception ex)
            {

                TxtCP.Text = ex.Message;
            }
            
        }

        protected void LLenaDDxSectorEconomico()
        {
            DDXSector.Items.Clear();

            var lstSector = CatalogosSolicitud.ObtenCatSectorEconomico();
            if (lstSector == null) return;
            DDXSector.DataSource = lstSector;
            DDXSector.DataValueField = "Cve_Sector";
            DDXSector.DataTextField = "Dx_Sector";
            DDXSector.DataBind();

            DDXSector.Items.Insert(0, "Seleccione");
            DDXSector.SelectedIndex = 0;
        }

        protected void LledarDDxGiroEmpresa()
        {
            var lstGiroEmpresa = CatalogosSolicitud.ObtenCatTipoIndustrias();

            if (lstGiroEmpresa == null) return;
            DDXGiroEmpresa.DataSource = lstGiroEmpresa;
            DDXGiroEmpresa.DataValueField = "Cve_Tipo_Industria";
            DDXGiroEmpresa.DataTextField = "Dx_Tipo_Industria";
            DDXGiroEmpresa.DataBind();

            DDXGiroEmpresa.Items.Insert(0, "Seleccione");
            DDXGiroEmpresa.SelectedIndex = 0;
        }
        #endregion

        #region Eventos Controles
        protected void DDXTipoPersona_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDXTipoPersona.SelectedValue == "1")
            {
                divRFCFisica.Visible = true;
                TxtRFCFisica.Enabled = true;
                //lblRFCPErsonaFisica.Visible = true;
                //TxtRFCFisica.Visible = true;

                divRFCMoral.Visible = false;
                TxtRFCMoral.Enabled = false;
                //lblRFCPErsonaMoral.Visible = false;
                //TxtRFCMoral.Visible = false;
            }

            if (DDXTipoPersona.SelectedValue == "2")
            {
                
                divRFCMoral.Visible = true;
                TxtRFCMoral.Enabled = true;
                //lblRFCPErsonaMoral.Visible = true;
                //TxtRFCMoral.Visible = true;
                
                divRFCFisica.Visible = false;
                TxtRFCFisica.Enabled = false;
                //lblRFCPErsonaFisica.Visible = false;
                //TxtRFCFisica.Visible = false;
            }
        }
        protected void DDXEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarDDxMunicipo(int.Parse(DDXEstado.SelectedValue));
        }

        protected void LlenarDDxTipoPersona()
        {
            var lstTipoPersona = CatalogosSolicitud.ObtenCatTipoSociedad();

            if (lstTipoPersona != null)
            {
                DDXTipoPersona.DataSource = lstTipoPersona;
                DDXTipoPersona.DataValueField = "Cve_Tipo_Sociedad";
                DDXTipoPersona.DataTextField = "Dx_Tipo_Sociedad";
                DDXTipoPersona.DataBind();

                DDXTipoPersona.Items.Insert(0, "Seleccione");
                DDXTipoPersona.SelectedIndex = 0;
            }
        }
        protected void DDXMunicipio_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarDDxColonias(int.Parse(DDXEstado.SelectedValue),
                int.Parse(DDXMunicipio.SelectedValue));
        }

        protected void DDXColonia_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDXColoniaHidden.SelectedValue = DDXColonia.SelectedValue;
            TxtCP.Text = DDXColoniaHidden.SelectedItem.Text;
        }

        //protected void ImgBtnBuscarCP_Click(object sender, ImageClickEventArgs e)
        //{
        //    if(TxtCP.Text != "")
        //        CargaGridColonias(TxtCP.Text);
        //}

        //protected void BtnImgSeleccionar_Click(object sender, ImageClickEventArgs e)
        //{
        //    try
        //    {
        //        var gridViewRow = (GridViewRow)((ImageButton)sender).NamingContainer;
        //        var dataKey = grdColonias.DataKeys[gridViewRow.RowIndex];

        //        if (dataKey != null)
        //        {
        //            var cveCampoSelec = (int)dataKey[0];
        //            var colonia = LstColoniasAsentamientos.FirstOrDefault(me => me.CveCp == cveCampoSelec);

        //            if (colonia != null)
        //            {
        //                TxtCP.Text = colonia.CodigoPostal;
        //                DDXEstado.SelectedValue = colonia.CveEstado.ToString(CultureInfo.InvariantCulture);
        //                LlenarDDxMunicipo(colonia.CveEstado);
        //                DDXMunicipio.SelectedValue = colonia.CveDelegMunicipio.ToString();
        //                if (colonia.CveDelegMunicipio != null)
        //                    LlenarDDxColonias(colonia.CveEstado, (int)colonia.CveDelegMunicipio);
        //                DDXColonia.SelectedValue = colonia.CveCp.ToString(CultureInfo.InvariantCulture);
        //            }

        //            TxtCP.Focus();
        //        }

        //        OcultaColonias();
        //    }
        //    catch (Exception ex)
        //    {
                
        //        TxtCP.Text = ex.Message;
        //    }
        //}

        //protected void BtnRegresar_Click(object sender, EventArgs e)
        //{
        //    OcultaColonias();
        //}

        #endregion

        #region Metodos Protegidos
        //protected void OcultaColonias()
        //{
        //    grdColonias.DataSource = null;
        //    grdColonias.DataBind();

        //    panelBusquedaColonia.Visible = false;
        //    BtnRegresar.Visible = false;
        //    panelDatosEmpresa.Visible = true;
        //}

        //private void CargaGridColonias(string codigoPostal)
        //{
        //    try
        //    {
        //        LstColoniasAsentamientos = CatalogosSolicitud.ObtenColoniasXCp(codigoPostal);

        //        if (LstColoniasAsentamientos != null)
        //        {
        //            if (LstColoniasAsentamientos.Count > 0)
        //            {
        //                grdColonias.DataSource = LstColoniasAsentamientos;
        //                grdColonias.DataBind();
        //                panelBusquedaColonia.Visible = true;
        //                BtnRegresar.Visible = true;
        //                panelDatosEmpresa.Visible = false;
        //            }
        //            else
        //            {
        //                const string sScript = "<script language='JavaScript'>alert('No se encuentra el Codigo Postal, contactar al Agente FIDE.');</script>";
        //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mensaje", sScript, false);
        //            }

        //            //var siguiente = (Button)this.Parent.FindControl("btnStepNext");
        //            //var cancelar = (Button)this.Parent.FindControl("BtnCacel");

        //            //siguiente.Visible = cancelar.Visible = false;
        //        }
        //        else
        //        {
        //            const string sScript = "<script language='JavaScript'>alert('No se encuentra el Codigo Postal, contactar al Agente FIDE.');</script>";
        //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mensaje", sScript, false);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        TxtCP.Text = ex.Message;
        //    }

        //}

        #endregion

        #region Metodos Publicos

        public K_DATOS_PYME NewDatosPyme()
        {
            var datosPyme = new K_DATOS_PYME
            {
                No_RPU = Session["ValidRPU"].ToString(),
                Cve_Tipo_Industria = (DDXGiroEmpresa.SelectedIndex == 0 ||
                                      DDXGiroEmpresa.SelectedIndex == -1)
                    ? 0
                    : int.Parse(DDXGiroEmpresa.SelectedValue),
                Cve_Sector_Economico = (DDXSector.SelectedIndex == 0 ||
                                        DDXSector.SelectedIndex == -1)
                    ? 0
                    : int.Parse(DDXSector.SelectedValue),
                Dx_Nombre_Comercial = TxtNombreComercial.Text,
                No_Empleados = (TxtNoEmpleados.Text == "") ? 0 : int.Parse(TxtNoEmpleados.Text),
                Tot_Gastos_Mensuales = (TxtGastosMensuales.Text == "")
                    ? 0
                    : decimal.Parse(TxtGastosMensuales.Text),
                Prom_Vtas_Mensuales = (TxtVentasAnuales.Text == "") ? 0 : decimal.Parse(TxtVentasAnuales.Text),
                Cve_Es_Pyme = 0,
                Cve_CP = (DDXColonia.SelectedIndex == 0 || DDXColonia.SelectedIndex == -1)
                    ? 0
                    : int.Parse(DDXColonia.SelectedValue),
                Codigo_Postal = TxtCP.Text,
                Cve_Deleg_Municipio = (DDXMunicipio.SelectedIndex == 0 || DDXMunicipio.SelectedIndex == -1)
                    ? 0
                    : int.Parse(DDXMunicipio.SelectedValue),
                Cve_Estado = (DDXEstado.SelectedIndex == 0 || DDXEstado.SelectedIndex == -1)
                    ? 0
                    : int.Parse(DDXEstado.SelectedValue),
                    Cve_Tipo_Sociedad = (byte?) DDXTipoPersona.SelectedIndex,
                    RFC = DDXTipoPersona.SelectedIndex == 1 ? TxtRFCFisica.Text : TxtRFCMoral.Text
            };

            return datosPyme;
        }

        public void EnableDatos()
        {
            DDXGiroEmpresa.Enabled = false;
            DDXSector.Enabled = false;
            TxtNombreComercial.Enabled = false;
            TxtNoEmpleados.Enabled = false;
            TxtGastosMensuales.Enabled = false;
            TxtVentasAnuales.Enabled = false;
            DDXColonia.Enabled = false;
            TxtCP.Enabled = false;
            DDXMunicipio.Enabled = false;
            DDXMunicipio.Enabled = false;
            DDXEstado.Enabled = false;
            DDXTipoPersona.Enabled = false;
            TxtRFCFisica.Enabled = false;
            TxtRFCMoral.Enabled = false;
        }

        #endregion

    }
}