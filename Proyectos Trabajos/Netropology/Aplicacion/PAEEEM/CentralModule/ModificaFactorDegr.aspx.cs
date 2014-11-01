using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.Entidades.Tarifas;
using PAEEEM.LogicaNegocios.ModuloCentral;
using PAEEEM.LogicaNegocios.Tarifas;
using PAEEEM.Entities;

namespace PAEEEM.CentralModule
{
    public partial class ModificaFactorDegr : System.Web.UI.Page
    {
        #region Propiedades
        public string UserName
        {
            get { return ViewState["UserName"] == null ? string.Empty : ViewState["UserName"].ToString(); }
            set { ViewState["UserName"] = value; }
        }

        List<CAT_Tecnologia_FD> _lista = new List<CAT_Tecnologia_FD>();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            UserName = Session["UserName"].ToString();

            if (IsPostBack) return;
            if (Session["UserInfo"] == null)
            {
                Response.Redirect("../Login/Login.aspx");
                return;
            }

            LlenaRegion();
            LlenaTecnologia();
            
            LlenaGrid();
        }

        private void LlenaGrid()
        {
            var datos = FactorDegradacion.ClassInstance.IrPorDatos(drpRegion.SelectedIndex,drpTecnologia.SelectedItem.Value == @"Seleccione" ? 0 : Convert.ToInt32(drpTecnologia.SelectedItem.Value));
            gvFactorDegradacion.DataSource = datos;
            gvFactorDegradacion.DataBind();
        }

        private void LlenaTecnologia()
        {
            var tecnologias = CatalogosTecnologia.OCatTecnologiasSustitucion();
            if (tecnologias == null) return;

            
            drpTecnologia.DataSource = tecnologias;            
            drpTecnologia.DataTextField = "Dx_Nombre_General";
            drpTecnologia.DataValueField = "Cve_Tecnologia";
            drpTecnologia.DataBind();
            drpTecnologia.Items.Insert(0, "Seleccione");
            drpTecnologia.SelectedValue = "Seleccione";
        }

        private void LlenaRegion()
        {
            var dtRegionBio = CatalogosTecnologia.CatRegionBioclimaticas();
            if (dtRegionBio == null) return;

            drpRegion.DataSource = dtRegionBio;
            drpRegion.DataTextField = "Region";
            drpRegion.DataValueField = "idregion_bioclima";
            drpRegion.DataBind();
            drpRegion.Items.Insert(0, "Seleccione");
            drpRegion.SelectedValue = "Seleccione";
        }

        protected void btnSalir_Click1(object sender, EventArgs e)
        {
            Response.Redirect("../Login/Login.aspx");
        }

        protected void CancelEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvFactorDegradacion.EditIndex = -1;
            LlenaGrid();
        }

        protected void EditCustomer(object sender, GridViewEditEventArgs e)
        {
            gvFactorDegradacion.EditIndex = e.NewEditIndex;
            LlenaGrid();
        }

//        protected void gvFactorDegradacion_RowDataBound(object sender, GridViewRowEventArgs e)
//{
//    if (e.Row.RowType == DataControlRowType.DataRow)
//    {
//         reference the Delete LinkButton
//        LinkButton db = (LinkButton)e.Row.Cells[0].Controls[6];

//         Get information about the product bound to the row
//        Northwind.ProductsRow product =
//            (Northwind.ProductsRow) ((System.Data.DataRowView) 
//e.Row.DataItem).Row;

//        db.OnClientClick = string.Format(
//            "return confirm('Are you certain you want to delete the {0} 
//product?');",
//            product.ProductName.Replace("'", @"\'"));
//    }
//}

        protected void GuardaDatos(object sender, GridViewUpdateEventArgs e)
        {
           //ScriptManager.RegisterStartupScript(Page, e.GetType(), "ConfirmOnDelete", "lockScreen();", true);
            //Page.ClientScript.RegisterStartupScript(GetType(), "Call my function", "lockScreen();", true);
            var fdTxt = ((TextBox)gvFactorDegradacion.Rows[e.RowIndex].FindControl("txtFactorDeg")).Text;
            var cveTecnologia =((Label)gvFactorDegradacion.Rows[e.RowIndex].FindControl("lblCveTec")).Text;
            var idRegionBc = ((Label)gvFactorDegradacion.Rows[e.RowIndex].FindControl("lblIdRegionClima")).Text;

            var usuario = ((US_USUARIOModel) Session["UserInfo"]).Nombre_Usuario;
            
            try
            {                
                var fd = Decimal.Parse(fdTxt);
                FactorDegradacion.ClassInstance.Actualiza_FD(fd, Convert.ToInt32(idRegionBc), Convert.ToInt32(cveTecnologia), usuario);
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page), "AlertInform",
                   "alert(' Se Actualizó la Información Exitosamente');",
                   true);

                gvFactorDegradacion.EditIndex = -1;
                LlenaGrid();
            }

            catch (Exception err)
            {
                var validacion = "Ocurrió un error al actualizar. Motivo: El valor insertado no es valido";

                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page), "AlertInform",
                     "alert('" + validacion + "');",
                    true);
                gvFactorDegradacion.EditIndex = -1;
                LlenaGrid();
            }
        }

        protected void drpRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenaGrid();
        }

        protected void drpTecnologia_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenaGrid();
        }

        protected void BTN_Buscar_Click(object sender, EventArgs e)
        {
            LlenaGrid();
        }

        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            CargaGridCustomizablesPlantilla2();
        }

        protected void CargaGridCustomizablesPlantilla2()
        {
            var paginaActual = AspNetPager.CurrentPageIndex;
            var tamanioPagina = AspNetPager.PageSize;
            var Lista = FactorDegradacion.ClassInstance.IrPorDatos(drpRegion.SelectedIndex, drpTecnologia.SelectedIndex);
            //gvFactorDegradacion.DataSource = Plantilla.LstCamposCustomizables.FindAll(
            //        me =>
            //            me.Rownum >= (((paginaActual - 1) * tamanioPagina) + 1) &&
            //            me.Rownum <= (paginaActual * tamanioPagina));
            gvFactorDegradacion.DataSource = Lista;

            gvFactorDegradacion.DataBind();
        }
    }
}