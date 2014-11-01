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
using PAEEEM.Entities;
using PAEEEM.DataAccessLayer;
using PAEEEM.Helpers;

namespace PAEEEM.CentralModule
{
    public partial class DisposalPartialCutsApproval : System.Web.UI.Page
    {
        #region Global Variables
        /// <summary>
        /// property
        /// </summary>
        private string PartialCode
        {
            get
            {
                return ViewState["PartialCode"] == null ? "" : ViewState["PartialCode"].ToString();
            }
            set
            {
                ViewState["PartialCode"] = value;
            }
        }

        private ArrayList SelectProductRowNum
        {
            get
            {
                return ViewState["SelectProductRowNum"] == null ? null : (ArrayList)ViewState["SelectProductRowNum"];
            }
            set
            {
                ViewState["SelectProductRowNum"] = value;
            }
        }

        private DataTable ProductsForApproval
        {
            get
            {
                return ViewState["ProductsForApproval"] == null ? null : ViewState["ProductsForApproval"] as DataTable;
            }
            set
            {
                ViewState["ProductsForApproval"] = value;
            }
        }

        private int ProductRowCount
        {
            get
            {
                return ViewState["ProductRowCount"] == null ? 0 : Convert.ToInt32(ViewState["ProductRowCount"]);
            }
            set
            {
                ViewState["ProductRowCount"] = value;
            }
        }
        #endregion

        /// <summary>
        /// Init Default Data When page Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (null == Session["UserInfo"])
                {
                    Response.Redirect("../Login/Login.aspx");
                    return;
                }
                //Init date control
                this.literalFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
                //Session is not null to load default data                
                if (Request.QueryString["PartialCode"] != null && Request.QueryString["PartialCode"].ToString() != "")
                {
                    PartialCode = System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["PartialCode"].ToString().Replace("%2B", "+")));
                    txtPartialCode.Text = PartialCode;
                }
                LoadGridViewData();
            }
        }

        /// <summary>
        /// Load default data when page load
        /// </summary>
        private void LoadGridViewData()
        {
            US_USUARIOModel UserModel = null;
            UserModel = Session["UserInfo"] as US_USUARIOModel;

            int PageCount = 0;
            DataTable dtProductsForApproval = null;

            dtProductsForApproval = K_CORTE_PARCIALDAL.ClassInstance.GetPartialCutsProductsForApproval(PartialCode, this.AspNetPager.CurrentPageIndex, this.AspNetPager.PageSize, out PageCount);
            if (dtProductsForApproval != null)
            {
                if (dtProductsForApproval.Rows.Count == 0)
                {
                    dtProductsForApproval.Rows.Add(dtProductsForApproval.NewRow());
                    ckbSelectAll.Enabled = false;
                }
                //Bind to grid view
                this.AspNetPager.RecordCount = PageCount;
                this.grdProductsForApproval.DataSource = dtProductsForApproval;
                this.grdProductsForApproval.DataBind();
            }

            ProductRowCount = PageCount;
            ProductsForApproval = dtProductsForApproval;
        }

        protected void grdProductsForApproval_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            CheckBox ckbSelect = (CheckBox)e.Row.FindControl("ckbSelect");
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.Cells[0].Text.Replace("&nbsp;", "") == "")
            {
                ckbSelect.Visible = false;
            }
        }

        protected void ckbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbSelectAll.Checked)
            {
                SelectAllProducts();
            }
            else
            {
                ClearAllProducts();
            }
        }

        /// <summary>
        /// when the selectall checkbox is checked
        /// </summary>
        private void SelectAllProducts()
        {
            foreach (GridViewRow row in grdProductsForApproval.Rows)
            {
                CheckBox ckbSelect = (CheckBox)row.FindControl("ckbSelect");
                ckbSelect.Checked = true;
            }

            if (SelectProductRowNum == null)
            {
                SelectProductRowNum = new ArrayList(ProductRowCount);
            }

            SelectProductRowNum.Clear();
            for (int i = 1; i <= ProductRowCount; i++)
            {
                SelectProductRowNum.Add(i);
            }
        }

        /// <summary>
        /// when the selectall checkbox is not checked
        /// </summary>
        private void ClearAllProducts()
        {
            foreach (GridViewRow row in grdProductsForApproval.Rows)
            {
                CheckBox ckbSelect = (CheckBox)row.FindControl("ckbSelect");
                ckbSelect.Checked = false;
            }

            if (SelectProductRowNum != null && SelectProductRowNum.Count > 0)
            {
                SelectProductRowNum.Clear();
            }
        }

        protected void btnApproval_Click(object sender, EventArgs e)
        {
            RememberOldSelectedProducts();
            if (SelectProductRowNum != null && SelectProductRowNum.Count > 0)
            {
                int result = 0;
                string strRowNum = "";
                foreach (int rowNum in SelectProductRowNum)
                {
                    strRowNum += rowNum + ",";
                }
                strRowNum = strRowNum.TrimEnd(',');
                if (SelectProductRowNum.Count == ProductRowCount)
                {
                    result = K_CORTE_PARCIALDAL.ClassInstance.ApprovalProductsAndStatus(PartialCode, (int)DisposalStatus.COMPLETADO);
                }
                else
                {
                    result = K_CORTE_PARCIALDAL.ClassInstance.ApprovalProducts(PartialCode, strRowNum);
                }

                if (result > 0)
                {
                    Response.Redirect("DisposalPartialCutsApprovalMonitor.aspx");
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DisposalPartialCutsApprovalMonitor.aspx");
        }

        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                LoadGridViewData();
                RePopulateSelectedProducts();
            }
        }

        protected void AspNetPager_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            if (IsPostBack)
            {
                RememberOldSelectedProducts();
            }
        }

        /// <summary>
        /// remember the selected products
        /// </summary>
        private void RememberOldSelectedProducts()
        {
            if (SelectProductRowNum == null)
            {
                SelectProductRowNum = new ArrayList(ProductRowCount);
            }
            foreach (GridViewRow row in grdProductsForApproval.Rows)
            {
                CheckBox ckbSelect = (CheckBox)row.FindControl("ckbSelect");
                int rowNum = (this.AspNetPager.CurrentPageIndex - 1) * this.AspNetPager.PageSize + row.RowIndex + 1;
                if (ckbSelect.Checked)
                {
                    if (!SelectProductRowNum.Contains(rowNum))
                    {
                        SelectProductRowNum.Add(rowNum);
                    }
                }
                else
                {
                    if (SelectProductRowNum.Contains(rowNum))
                    {
                        SelectProductRowNum.Remove(rowNum);
                    }
                }
            }
        }

        /// <summary>
        /// repopulate checkbox status
        /// </summary>
        private void RePopulateSelectedProducts()
        {
            if (SelectProductRowNum != null)
            {
                foreach (GridViewRow row in grdProductsForApproval.Rows)
                {
                    CheckBox ckbSelect = (CheckBox)row.FindControl("ckbSelect");
                    int rowNum = (this.AspNetPager.CurrentPageIndex - 1) * this.AspNetPager.PageSize + row.RowIndex + 1;

                    if (SelectProductRowNum.Contains(rowNum))
                    {
                        ckbSelect.Checked = true;
                    }
                    else
                    {
                        ckbSelect.Checked = false;
                    }
                }
            }
        }
    }
}
