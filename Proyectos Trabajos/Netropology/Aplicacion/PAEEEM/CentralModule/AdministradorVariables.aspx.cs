using System;
using System.Collections.Generic; 
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

using PAEEEM.Entidades.ModuloCentral;
using PAEEEM.LogicaNegocios.ModuloCentral;

namespace PAEEEM.CentralModule
{
    public partial class AdministradorVariables : System.Web.UI.Page
    {
        private List<DetalleVariablesGlobales> _variable = new List<DetalleVariablesGlobales>();
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["UserInfo"] == null)
                {
                    Response.Redirect("../Login/Login.aspx");
                    return;
                }
            }

            llenarGrid();
            
        }

        private void llenarGrid()
        {
            //_variable = AdministrarVariablesGlobales.Variables();
            //if (_variable.Count == 0)
            //{
            //    _variable.Add(new DetalleVariablesGlobales());
            //}
            //gvVariablesGlobales.DataSource = _variable;
            //gvVariablesGlobales.DataBind();
            //LstDatVariable = LogicaNegocios.Credito.variables.ClassInstance.obtenerDatos();
            LstDatVariable = LogicaNegocios.ModuloCentral.AdministrarVariablesGlobales.Variables();


            gvVariablesGlobales.DataSource = LstDatVariable;
            gvVariablesGlobales.DataBind();

            var rownum = 1;

            foreach (var VarGlob in LstDatVariable)
            {
                VarGlob.Rownum = rownum;
                rownum++;
            }

            AspNetPager2.RecordCount = LstDatVariable.Count;      
            
        }

        protected void EditVG(object sender, GridViewEditEventArgs e)
        {
            gvVariablesGlobales.EditIndex = e.NewEditIndex;
            //llenarGrid();
            CargaGridCustomizablesPlantilla2();
            
        }

        protected void CancelEditVG(object sender, GridViewCancelEditEventArgs e)
        {
            gvVariablesGlobales.EditIndex = -1;
            //llenarGrid();
            CargaGridCustomizablesPlantilla2();
        }

        protected void GuardaDatosVG(object sender, GridViewUpdateEventArgs e)
        {
            int index = ((AspNetPager2.CurrentPageIndex - 1) * 15) + e.RowIndex;
            var idParametro = LstDatVariable[index].IdParametro;//((Label)gvVariablesGlobales.Rows[e.RowIndex].FindControl("lblIdP")).Text;
            var idSeccion = LstDatVariable[index].IdSeccion;//((Label)gvVariablesGlobales.Rows[e.RowIndex].FindControl("lblIdS")).Text;
            var IdPP = LstDatVariable[index].ID_Prog_Proy.ToString(); //((Label)gvVariablesGlobales.Rows[e.RowIndex].FindControl("lblIdPP")).Text;
            //var Id_TF_TI = LstDatVariable[index].total_incentivo;
            var valor = ((TextBox)gvVariablesGlobales.Rows[e.RowIndex].FindControl("txtValor")).Text;



            bool isNum;

            double retNum;

            isNum = Double.TryParse(Convert.ToString(valor), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);






            if (valor != "" && isNum)
            {

                if (IdPP == "0")
                {
                    var validacion = AdministrarVariablesGlobales.actualizar(Convert.ToInt16(idParametro),
                        Convert.ToInt16(idSeccion), Convert.ToString(valor));

                    ScriptManager.RegisterStartupScript(UpdatePanelVG, typeof(Page), "AlertInform",
                        "alert('" + validacion + "');",
                        true);
                }
                else
                {
                    var validacion = AdministrarVariablesGlobales.actualizarPro(Convert.ToByte(IdPP), Convert.ToString(valor));

                    ScriptManager.RegisterStartupScript(UpdatePanelVG, typeof(Page), "AlertInform",
                        "alert('" + validacion + "');",
                        true);
                }
                gvVariablesGlobales.EditIndex = -1;
                llenarGrid();
                CargaGridCustomizablesPlantilla2();
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanelVG, typeof(Page), "AlertInform",
                    "alert('Introduzca el valor de la variable');",
                    true);
            }

        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void AspNetPager2_PageChanged(object sender, EventArgs e)
        {
            //if(IsPostBack)
            CargaGridCustomizablesPlantilla2();
        }

        public List<DetalleVariablesGlobales> LstDatVariable
        {
            set
            {
                ViewState["LstDat_Variable"] = value;
            }
            get
            {
                return (List<DetalleVariablesGlobales>)ViewState["LstDat_Variable"];
            }
        }

        
        protected void CargaGridCustomizablesPlantilla2()
        {
            var paginaActual = AspNetPager2.CurrentPageIndex;
            var tamanioPagina = AspNetPager2.PageSize;


            gvVariablesGlobales.DataSource = LstDatVariable.FindAll(
                    me =>
                        me.Rownum >= (((paginaActual - 1) * tamanioPagina) + 1) &&
                        me.Rownum <= (paginaActual * tamanioPagina));

            gvVariablesGlobales.DataBind();
        }

    }
}