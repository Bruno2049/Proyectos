using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PAEEEM.BussinessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;

namespace PAEEEM
{
    public partial class RoleManagement : System.Web.UI.Page
    {
        /// <summary>
        /// init data when page load
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
                //Load roles
                BindGridViewData(1, AspNetPager1.PageSize);
            }
        }
        /// <summary>
        /// pager change to refresh data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BindGridViewData(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
        }
        /// <summary>
        /// bind data
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        private void BindGridViewData(int pageIndex, int pageSize)
        {
            int pageCount = 0;

            DataTable dtUserList = US_ROLBll.ClassInstance.Get_US_ROLListByPager(string.Empty, pageIndex, pageSize, out pageCount);
            bool isone = false;
            if (dtUserList.Rows.Count < 1)
            {
                dtUserList.Rows.Add(dtUserList.NewRow());
                isone = true;
            }
            gvRoleList.DataSource = dtUserList;
            AspNetPager1.RecordCount = pageCount;
            gvRoleList.DataBind();
            if (isone)
            {
                gvRoleList.Columns[2].Visible = false;
            }
            else
            {
                gvRoleList.Columns[2].Visible = true;
            }
        }
        /// <summary>
        /// Add new role
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("RoleEdit.aspx?flag=1");
        }
        /// <summary>
        /// Show edit form when command button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            //string Message = "";
            //int iResult = 0;
            int RowIndex = Convert.ToInt32(e.CommandArgument);
            string UserID = this.gvRoleList.DataKeys[RowIndex].Value.ToString();

            if (e.CommandName == "Editar")
            {
                Response.Redirect("RoleEdit.aspx?flag=2&roleID=" + UserID);
            }
                /*
            else if (e.CommandName == "Borrar")//Do delete logic
            {
                iResult = US_USUARIODal.ClassInstance.Delete_US_USUARIO(UserID);
                if (iResult > 0)
                {
                    Message = (string)GetGlobalResourceObject("DefaultResource", "msgDeleteUserSuccess");
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "DeleteSuccessfully", "confirm('" + Message + "');", true);
                    //Refresh grid data
                    InitDefaultData();
                }
            }
            else //DO activate user logic
            {
                iResult = US_USUARIODal.ClassInstance.ActivateUser(UserID);
                if (iResult > 0)
                {
                    Message = (string)GetGlobalResourceObject("DefaultResource", "msgActivateUserSuccess");
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "AvtivateSuccessfully", "confirm('" + Message + "');", true);
                    //Refresh grid data
                    InitDefaultData();
                }                 
            }*/
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
                LinkButton linkButton = (LinkButton)e.Row.FindControl("btnEdit");
                if (linkButton != null)
                {
                    linkButton.CommandArgument = e.Row.RowIndex.ToString();
                }
                /*
                LinkButton linkDelete = (LinkButton)e.Row.FindControl("linkButtonDelete");
                if (linkDelete != null)
                {
                    linkDelete.CommandArgument = e.Row.RowIndex.ToString();
                }

                LinkButton linkActivate = (LinkButton)e.Row.FindControl("linkButtonActivate");
                if (linkActivate != null)
                {
                    linkActivate.CommandArgument = e.Row.RowIndex.ToString();
                }
                 */
            }
        }
    }
}
