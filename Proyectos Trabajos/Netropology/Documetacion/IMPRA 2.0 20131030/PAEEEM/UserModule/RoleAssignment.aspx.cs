using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.BussinessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;

namespace PAEEEM
{
    public partial class RoleAssignment : System.Web.UI.Page
    {
        /// <summary>
        /// load data when page load
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
                //Load right part roles
                LoadRoleListFromDB();
                //Load left part users
                LoadUserListFromDB();
            }
        }
        /// <summary>
        /// load users
        /// </summary>
        private void LoadUserListFromDB()
        {
            List< US_USUARIOModel> roleList =  US_USUARIOBll.ClassInstance.Get_AllUS_USUARIO();
            listUser.DataSource = roleList;
            listUser.DataTextField = "Nombre_Usuario";
            listUser.DataValueField = "Id_Usuario";
            listUser.DataBind();
        }
        /// <summary>
        /// load roles
        /// </summary>
        private void LoadRoleListFromDB()
        {
            List< US_ROLModel> roleList = US_ROLBll.ClassInstance.Get_AllUS_ROL();
            ddlRoleList.DataSource = roleList;
            ddlRoleList.DataTextField = "Nombre_Rol";
            ddlRoleList.DataValueField = "Id_Rol";            
            ddlRoleList.DataBind();
            ddlRoleList.Items.Insert(0, new ListItem("", "0"));
        }
        /// <summary>
        /// Save the user role
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int userid = 0;
            int.TryParse(listUser.SelectedValue, out userid);
            // Update by Tina 2011/08/02
            if (userid == 0)
            {
                string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgChooseUser") as string;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "SaveRoleAlert", "alert('" + strMsg + "');", true);
            }
            else
            {
                int roleid = 0;
                int.TryParse(ddlRoleList.SelectedValue, out roleid);

                //US_USUARIOModel user = new US_USUARIOModel();
                US_USUARIOModel user = US_USUARIOBll.ClassInstance.Get_US_USUARIOByPKID(listUser.SelectedValue);
                user.Id_Usuario = userid;
                user.Id_Rol = roleid;
                int reslut = US_USUARIOBll.ClassInstance.Update_US_USUARIO(user);
                if (reslut > 0)
                {
                    string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgUpdateUserRoleSuccess") as string;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "SaveRoleSuccess", "alert('" + strMsg + "')", true);
                }
                else
                {
                    string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgUpdateUserRoleFail") as string;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "SaveRoleFailed", "alert('" + strMsg + "');", true);
                }
            }
            // End
        }
        /// <summary>
        /// user changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void listUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            US_USUARIOModel user = US_USUARIOBll.ClassInstance.Get_US_USUARIOByPKID(listUser.SelectedValue);
            if (user != null && user.Id_Rol!=0) 
            {
                ddlRoleList.SelectedValue = user.Id_Rol.ToString();
            }
        }
        /// <summary>
        /// Init labels
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
//        protected void Page_PreRender(object sender, EventArgs e)
//        {
//            //button text
//            Label1.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "Users") as string;
//            Label2.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "RoleAssignment") as string;           
//            btnSave.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "Save") as string;
//        }
    }
}
