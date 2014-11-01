using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using iTextSharp.text.pdf.security;
using PAEEEM.AccesoDatos.CreditosAutorizadosA;
using PAEEEM.Entidades;
using PAEEEM.Entities;
using PAEEEM.Entidades.CreditosAutorizadosE;
using PAEEEM.LogicaNegocios.CreditosAutorizadosL;
using Telerik.Web.UI;

namespace PAEEEM.CentralModule
{
    public partial class CreditosLiberados : System.Web.UI.Page
    {
        List<CAT_REGION> ListaRegion = CreditosAutorizadosL.ClassInstance.ObtenListaRegion();
        List<CAT_ZONA> ListaZona = CreditosAutorizadosL.ClassInstance.ObtenListaZona();
        List<CAT_ESTADO> ListaEstados = CreditosAutorizadosL.ClassInstance.ObtenListaEstados();
        private List<GridCreditosLiberados> ListaCreditosLiberados = new List<GridCreditosLiberados>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (Session["UserInfo"] == null)
            {
                Response.Redirect("../Login/Login.aspx");
                return;
            }

            RDPKR_Desda.MaxDate = DateTime.Today;
            RDPKR_Desda.SelectedDate = DateTime.Today;
            RDPKR_Hasta.MaxDate = DateTime.Today;
            RDPKR_Hasta.SelectedDate = DateTime.Today;

            RadLlenaComboBox();

            RCB_Region.SelectedValue = "0";
            RCB_Zona.SelectedValue = "0";
            RCB_Estado.SelectedValue = "0";
            RCB_Municipio.SelectedValue = "0";

            RCB_Municipio.Enabled = false;
            RCB_Zona.Enabled = false;

            var Usuario = ((US_USUARIOModel)Session["UserInfo"]);

            ComboUsuario(Usuario);
            LlenaGrid();
        }

        private void ComboUsuario(US_USUARIOModel Usuario)
        {
            var Ubicacion = CreditosAutorizadosL.ClassInstance.ObtenUbicacion((int)Usuario.Id_Departamento);

            switch (Usuario.Id_Rol)
            {
                case 1:
                    break;

                case 6:
                    RCB_Region.Enabled = false;
                    RCB_Region.SelectedValue = Convert.ToString(Ubicacion.Cve_Region);
                    Llena_RCB_Zona(Convert.ToInt32(RCB_Region.SelectedValue));
                    RCB_Zona.Enabled = true;
                    
                    break;

                case 2:
                    RCB_Region.Enabled = false;
                    RCB_Zona.Enabled = false;
                    RCB_Region.SelectedValue = Convert.ToString(Ubicacion.Cve_Region);
                    RCB_Zona.SelectedValue = Convert.ToString(Ubicacion.Cve_Zona);
                    break;
            }
        }

        private void Llena_RCB_Zona(int Cve_Region)
        {
            var ListaZona = CreditosAutorizadosL.ClassInstance.ObtenListaZonaFiltro(Cve_Region);
            CAT_ZONA DZona = new CAT_ZONA();
            DZona.Cve_Zona = 0;
            DZona.Dx_Nombre_Zona = "TODOS";
            ListaZona.Add(DZona);
            
            RCB_Zona.DataSource = ListaZona;
            RCB_Zona.DataBind();
        }

        private void RadLlenaComboBox()
        {
            CAT_REGION DRegion = new CAT_REGION();
            DRegion.Cve_Region = 0;
            DRegion.Dx_Nombre_Region = "TODOS";
            ListaRegion.Add(DRegion);

            RCB_Region.DataSource = ListaRegion;
            RCB_Region.DataValueField = "Cve_Region";
            RCB_Region.DataTextField = "Dx_Nombre_Region";

            CAT_ZONA DZona = new CAT_ZONA();
            DZona.Cve_Zona = 0;
            DZona.Dx_Nombre_Zona = "TODOS";
            ListaZona.Add(DZona);

            RCB_Zona.DataSource = ListaZona;
            RCB_Zona.DataValueField = "Cve_Zona";
            RCB_Zona.DataTextField = "Dx_Nombre_Zona";

            CAT_ESTADO CEA = new CAT_ESTADO();
            CEA.Cve_Estado = -1;
            CEA.Dx_Nombre_Estado = "DesSeleccionar";
            ListaEstados.Add(CEA);

            CAT_ESTADO DEstado = new CAT_ESTADO();
            DEstado.Cve_Estado = 0;
            DEstado.Dx_Nombre_Estado = "TODOS";
            ListaEstados.Add(DEstado);

            RCB_Estado.DataSource = ListaEstados;
            RCB_Estado.DataValueField = "Cve_Estado";
            RCB_Estado.DataTextField = "Dx_Nombre_Estado";


            //RadComboBoxItem item1 = new RadComboBoxItem();
            //item1.Value = "1";
            //item1.Text = "S";
            //RCB_TipoDistribuidor.Items.Add(item1);

            //RadComboBoxItem item2 = new RadComboBoxItem();
            //item2.Value = "2";
            //item2.Text = "S_B";
            //RCB_TipoDistribuidor.Items.Add(item2);

            RCB_Municipio.DataValueField = "Cve_Deleg_Municipio";
            RCB_Municipio.DataTextField = "Dx_Deleg_Municipio";



            RCB_Region.DataBind();
            RCB_Zona.DataBind();
            RCB_Estado.DataBind();
            RCB_TipoDistribuidor.DataBind();
        }

        public void LlenaGrid()
        {
            DateTime? Desde = RDPKR_Desda.SelectedDate;
            DateTime? Hasta = RDPKR_Hasta.SelectedDate;

            int Region = (Convert.ToInt32(RCB_Region.SelectedValue));
            int Zona = (Convert.ToInt32(RCB_Zona.SelectedValue));
            int Estado = (Convert.ToInt32(RCB_Estado.SelectedValue));
            int Municipio = (Convert.ToInt32(RCB_Municipio.SelectedValue));
            string No_Credito = TXB_NoCredito.Text;

            ListaCreditosLiberados = CreditosAutorizadosL.ClassInstance.ObteGridCreditosLiberados(Region, Zona, Estado, Municipio, No_Credito, Desde, Hasta);
            DGV_CL.DataSource = ListaCreditosLiberados;
            DGV_CL.DataBind();
        }


        protected void RBTN_GeneraReporte_OnClick(object sender, EventArgs e)
        {
            DGV_CL.DataSource = ListaCreditosLiberados;
            DGV_CL.DataBind();
        }

        protected void RCB_Region_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var Cve_Zona = RCB_Region.SelectedValue;

            ListaZona = CreditosAutorizadosL.ClassInstance.ObtenListaZonaFiltro(Convert.ToInt32(Cve_Zona));
            var DZona = new CAT_ZONA();
            DZona.Cve_Zona = 0;
            DZona.Dx_Nombre_Zona = "TODOS";
            ListaZona.Add(DZona);

            RCB_Zona.DataSource = ListaZona;
            RCB_Zona.DataBind();
            RCB_Zona.Enabled = true;
        }
        
        protected void GridViewCreditosL_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            LlenaGrid();
        }

        protected void DGV_CreditosLiberados_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            LlenaGrid();
        }

        protected void DGV_CreditosLiberados_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            LlenaGrid();
        }

        protected void RCB_Estado_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (Convert.ToInt32(RCB_Estado.SelectedValue) == -1)
            {
                RCB_Estado.Enabled = false;
                RCB_Municipio.Enabled = false;
            }
            else
            {

                var Cve_Estado = RCB_Estado.SelectedValue;
                var ListaMunicipios =
                    CreditosAutorizadosL.ClassInstance.ObtenListaMunicipios(Convert.ToInt32(Cve_Estado));

                var Municipio = new CAT_DELEG_MUNICIPIO();
                Municipio.Cve_Deleg_Municipio = 0;
                Municipio.Dx_Deleg_Municipio = "TODOS";
                ListaMunicipios.Add(Municipio);

                RCB_Municipio.DataSource = ListaMunicipios;
                RCB_Municipio.DataBind();

                RCB_Municipio.Enabled = true;
            }
        }

        
    }
}