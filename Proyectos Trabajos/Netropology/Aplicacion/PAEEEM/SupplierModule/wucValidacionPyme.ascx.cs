using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Alta_Solicitud;
using PAEEEM.LogicaNegocios;
using PAEEEM.LogicaNegocios.SolicitudCredito;

namespace PAEEEM.SupplierModule
{
    public partial class wucValidacionPyme : System.Web.UI.UserControl
    {
        public List<Colonia> LstColoniasAsentamientos
        {
            get { return (List<Colonia>)ViewState["LstColoniasAsentamientos"]; }
            set { ViewState["LstColoniasAsentamientos"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LlenarDdxEstado();
            LledarDDxGiroEmpresa();
            LLenaDDxSectorEconomico();
        }

        #region llenar Catalogos

        protected void LlenarDdxEstado()
        {
            var lstEstado = CatalogosSolicitud.ObtenCatEstadosReoublica();
            if (lstEstado != null)
            {
                DDXEstado.DataSource = lstEstado;
                DDXEstado.DataValueField = "Cve_Estado";
                DDXEstado.DataTextField = "Dx_Nombre_Estado";
                DDXEstado.DataBind();

                DDXEstado.Items.Insert(0, new ListItem("", ""));
                DDXEstado.SelectedIndex = 0;
            }
        }

        protected void LlenarDDxMunicipo(int idEstado)
        {
            DDXMunicipio.Items.Clear();

            var lstMunicipio = CatalogosSolicitud.ObtenDelegMunicipios(idEstado);

            if (lstMunicipio != null)
            {
                DDXMunicipio.DataSource = lstMunicipio;
                DDXMunicipio.DataValueField = "Cve_Deleg_Municipio";
                DDXMunicipio.DataTextField = "Dx_Deleg_Municipio";
                DDXMunicipio.DataBind();

                DDXMunicipio.Items.Insert(0, new ListItem("", ""));
                DDXMunicipio.SelectedIndex = 0;
            }
        }

        protected void LlenarDDxColonias(int idEstado, int idMunicipio)
        {
            DDXColonia.Items.Clear();
            DDXColoniaHidden.Items.Clear();

            var lstColonia = CatalogosSolicitud.ObtenCatCodigoPostals(idEstado, idMunicipio);

            if (lstColonia != null)
            {
                DDXColonia.DataSource = lstColonia;
                DDXColonia.DataValueField = "Cve_CP";
                DDXColonia.DataTextField = "Dx_Colonia";
                DDXColonia.DataBind();

                DDXColonia.Items.Insert(0, new ListItem("", ""));
                DDXColonia.SelectedIndex = 0;

                DDXColoniaHidden.DataSource = lstColonia;
                DDXColoniaHidden.DataValueField = "Cve_CP";
                DDXColoniaHidden.DataTextField = "Codigo_Postal";
                DDXColoniaHidden.DataBind();
            }
        }

        protected void LLenaDDxSectorEconomico()
        {
            DDXSector.Items.Clear();

            var lstSector = CatalogosSolicitud.ObtenCatSectorEconomico();
            if (lstSector != null)
            {
                DDXSector.DataSource = lstSector;
                DDXSector.DataValueField = "Cve_Sector";
                DDXSector.DataTextField = "Dx_Sector";
                DDXSector.DataBind();

                DDXSector.Items.Insert(0, new ListItem("", ""));
                DDXSector.SelectedIndex = 0;
            }
        }

        protected void LledarDDxGiroEmpresa()
        {
            var lstGiroEmpresa = CatalogosSolicitud.ObteCatTipoIndustrias();

            if (lstGiroEmpresa != null)
            {
                DDXGiroEmpresa.DataSource = lstGiroEmpresa;
                DDXGiroEmpresa.DataValueField = "Cve_Tipo_Industria";
                DDXGiroEmpresa.DataTextField = "Dx_Tipo_Industria";
                DDXGiroEmpresa.DataBind();

                DDXGiroEmpresa.Items.Insert(0, new ListItem("", ""));
                DDXGiroEmpresa.SelectedIndex = 0;
            }
        }

        private void CargaGridColonias(string codigoPostal)
        {
            LstColoniasAsentamientos = CatalogosSolicitud.ObtenColoniasXCp(codigoPostal);

            if (LstColoniasAsentamientos != null)
            {
                grdColonias.DataSource = LstColoniasAsentamientos;
                grdColonias.DataBind();
                panelBusquedaColonia.Visible = true;
                panelDatosEmpresa.Visible = false;
            }
        }

        protected void OcultaColonias()
        {
            grdColonias.DataSource = null;
            grdColonias.DataBind();

            panelBusquedaColonia.Visible = false;
            panelDatosEmpresa.Visible = true;
        }

        #endregion      

        #region Eventos DDL

        protected void DDXEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarDDxMunicipo(int.Parse(DDXEstado.SelectedValue));
        }

        protected void DDXMunicipio_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarDDxColonias(int.Parse(DDXEstado.SelectedValue),
                int.Parse(DDXMunicipio.SelectedValue));
        }

        protected void DDXColonia_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDXColoniaHidden.SelectedValue = DDXColonia.SelectedValue;
            TxtCP.Text = DDXColonia.SelectedItem.Text;
        }

        protected void ImgBtnBuscarCP_Click(object sender, ImageClickEventArgs e)
        {
            if(TxtCP.Text != "")
                CargaGridColonias(TxtCP.Text);
        }

        protected void BtnImgSeleccionar_Click(object sender, ImageClickEventArgs e)
        {
            var gridViewRow = (GridViewRow)((ImageButton)sender).NamingContainer;
            var dataKey = grdColonias.DataKeys[gridViewRow.RowIndex];

            if (dataKey != null)
            {
                var cveCampoSelec = (int)dataKey[0];
                var colonia = LstColoniasAsentamientos.FirstOrDefault(me => me.CveCp == cveCampoSelec);

                if (colonia != null)
                {
                    TxtCP.Text = colonia.CodigoPostal;
                    DDXEstado.SelectedValue = colonia.CveEstado.ToString();
                    LlenarDDxMunicipo(colonia.CveEstado);
                    DDXMunicipio.SelectedValue = colonia.CveDelegMunicipio.ToString();
                    LlenarDDxColonias(colonia.CveEstado, (int) colonia.CveDelegMunicipio);
                    DDXColonia.SelectedValue = colonia.CveCp.ToString();
                }

                OcultaColonias();
                TxtCP.Focus();
            }
            
        }

        protected void BtnRegresar_Click(object sender, EventArgs e)
        {
            OcultaColonias();
        }

        private void GuardaDatosPyme()
        {
            var datosPyme = new K_DATOS_PYME
            {
                No_RPU = TxtNoRPU.Text,
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
                    : int.Parse(DDXEstado.SelectedValue)
            };

            var esPyme = CatalogosSolicitud.ValidaClasificacionPyme(datosPyme);

            if (esPyme)
                datosPyme.Cve_Es_Pyme = 1;

            var resultado = SolicitudCreditoAcciones.GuarDatosPyme(datosPyme);
        }

        #endregion               

        
    }
}