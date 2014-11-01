using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using PAEEEM.BussinessLayer;
using PAEEEM.Helpers;

namespace PAEEEM
{
    public partial class RateandCostManager : System.Web.UI.Page
    {
        /// <summary>
        /// Init default data when page first load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserInfo"] == null)
                {
                    Response.Redirect("../Login/Login.aspx");
                    return;
                }
                //Init the date field showing at the top-right of the page
                this.txtFecha.Text = DateTime.Now.ToString("MM-yyyy");
                //Load Estado and Tarifa
                BindEstadoAndTarifa();
                //Bind grid view data
                BindGridViewData();
                //Clear filter conditions
                ClearFilterConditions();
            }
        }
        
        /// <summary>
        /// Load estado and tarifa
        /// </summary>
        private void BindEstadoAndTarifa()
        {
            InitEstado();
            InitTarifa();            
        }
     
        /// <summary>
        /// Init Estado
        /// </summary>
        private void InitEstado()
        {
            DataTable dtEstado = CAT_ESTADOBLL.ClassInstance.Get_All_CAT_ESTADO();
            if (dtEstado != null)
            {
                this.drpEstado.DataSource = dtEstado;
                this.drpEstado.DataTextField = "Dx_Nombre_Estado";
                this.drpEstado.DataValueField = "Cve_Estado";
                this.drpEstado.DataBind();
                this.drpEstado.Items.Insert(0, "");
                this.drpEstado.SelectedIndex = 0;
            }
        }
      
        /// <summary>
        /// Init Tarifa
        /// </summary>
        private void InitTarifa()
        {
            DataTable dtTarifa = CAT_TARIFABLL.ClassInstance.Get_All_CAT_TARIFA();
            if (dtTarifa != null)
            {
                this.drpTarifa.DataSource = dtTarifa;
                this.drpTarifa.DataTextField = "Dx_Tarifa";
                this.drpTarifa.DataValueField = "Cve_Tarifa";
                this.drpTarifa.DataBind();
                this.drpTarifa.Items.Insert(0, "");
                this.drpTarifa.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Clear Filter Conditions
        /// </summary>
        private void ClearFilterConditions()
        {
            Session["CurrentEstadoRateCostMonitor"] = 0;
            Session["CurrentTarifaRateCostMonitor"] = 0;
            Session["CurrentPeriodRateCostMonitor"] = "";
        }
      
        /// <summary>
        /// Bind grid view data
        /// </summary>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        private void BindGridViewData()
        {
            int pageCount = 0;
            int iEstado = (this.drpEstado.SelectedIndex != 0 && this.drpEstado.SelectedIndex != -1) ? Convert.ToInt32(this.drpEstado.SelectedValue) : -1;

            string strPeriodo = "";
            if (!string.IsNullOrEmpty(txtPeriodo.Text))
            {
                strPeriodo = txtPeriodo.Text.Trim() + "-01";
            }

            int iTarifa = (this.drpTarifa.SelectedIndex != 0 && this.drpTarifa.SelectedIndex != -1) ? Convert.ToInt32(this.drpTarifa.SelectedValue) : -1;

            DataTable dtRateAndCost = K_TARIFA_COSTOBLL.ClassInstance.Get_K_TARIFA_COSTOListByPagerAndEstadoID("", this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize, iEstado, strPeriodo, iTarifa, out pageCount);
            if (dtRateAndCost != null)
            {
                if (dtRateAndCost.Rows.Count == 0)
                {
                    DataRow row = dtRateAndCost.NewRow();

                    foreach (DataColumn col in dtRateAndCost.Columns)
                    {
                        col.AllowDBNull = true;
                        row[col] = DBNull.Value;
                    }

                    dtRateAndCost.Rows.Add(row);
                }
                //Bind grid view data
                this.AspNetPager1.RecordCount = pageCount;
                this.gvTarifaCosto.DataSource = dtRateAndCost;
                gvTarifaCosto.DataBind();
            }
        }
       
        /// <summary>
        /// Add new rate and cost
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddRateandCost.aspx");
        }

        #region Refresh GridView 
        /// <summary>
        /// Refresh when estado changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridViewData();
            //Changed by Jerry 2011/08/09
            Session["CurrentEstadoRateCostMonitor"] = this.drpEstado.SelectedIndex;

            this.AspNetPager1.GoToPage(1);
        }
        /// <summary>
        /// Refresh when tarifa changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpTarifa_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridViewData();
            //Changed by Jerry 2011/08/09
            Session["CurrentTarifaRateCostMonitor"] = this.drpTarifa.SelectedIndex;
            this.AspNetPager1.GoToPage(1);
        }
        /// <summary>
        /// Refresh when period changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtPeriodo_TextChanged(object sender, EventArgs e)
        {
            BindGridViewData();
            //Changed by Jerry 2011/08/09
            Session["CurrentPeriodRateCostMonitor"] = this.txtPeriodo.Text;
            this.AspNetPager1.GoToPage(1);
        }

        protected void AspNetPager_PageChanging(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                this.drpEstado.SelectedIndex = Session["CurrentEstadoRateCostMonitor"] != null ? (int)Session["CurrentEstadoRateCostMonitor"] : 0;
                this.drpTarifa.SelectedIndex = Session["CurrentTarifaRateCostMonitor"] != null ? (int)Session["CurrentTarifaRateCostMonitor"] : 0;
                this.txtPeriodo.Text = Session["CurrentPeriodRateCostMonitor"] != null ? (string)Session["CurrentPeriodRateCostMonitor"] : "";
            }
        }
        /// <summary>
        /// Refresh when page changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                BindGridViewData();
            }
        }
        #endregion

        /// <summary>
        /// Show edit form when command button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            int RowIndex = Convert.ToInt32(e.CommandArgument);
            string ID = this.gvTarifaCosto.DataKeys[RowIndex].Value.ToString();

            if (e.CommandName == "Editar")
            {
                Response.Redirect("AddRateandCost.aspx?ID=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(ID)).Replace("+", "%2B"));
            }
        }

        /// <summary>
        /// Assign e.CommandArgument when row created
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton linkButton = (LinkButton)e.Row.FindControl("linkButtonEdit");
                if (linkButton != null)
                {
                    linkButton.CommandArgument = e.Row.RowIndex.ToString();
                }
            }
        }
    }
}
