using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.DataAccessLayer;

namespace PAEEEM.DisposalModule
{
    public partial class OldProductList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (null == Session["UserInfo"])
                {
                    Response.Redirect("../Login/Login.aspx");
                    return;
                }
                //setup current date to date control
                this.literalFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
                //Load technology and Tipo Product
                if (!string.IsNullOrEmpty(Request.QueryString["RecuperacionID"]))
                {
                    LoadGridViewData();
                }
            }
        }
        private void LoadGridViewData()
        {
            try
            {
                //obtain old products list related with the specific Material 
                int PageCount = 0;
                DataTable OldProducts = null;

                OldProducts = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetMeterialOldEquipment(Request.QueryString["RecuperacionID"].ToString(), this.AspNetPager.CurrentPageIndex, this.AspNetPager.PageSize, out PageCount);

                if (OldProducts != null)
                {
                    if (OldProducts.Rows.Count == 0)
                    {                       
                        OldProducts.Rows.Add(OldProducts.NewRow());
                    }                  
                   
                    //Bind to grid view
                    this.AspNetPager.RecordCount = PageCount;
                    this.grdProductList.DataSource = OldProducts;
                    this.grdProductList.DataBind();
                }               
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "GridViewInitError",
                                                                        "alert('Excepción que se produce durante la vista de cuadrícula de inicialización:" + ex.Message + "');", true);
            }
        }
        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                LoadGridViewData();
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("MaterialRecoveryRegistryMonitor.aspx?Flag=0");
        }
    }
}
