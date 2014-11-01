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
    public partial class RoleEdit : System.Web.UI.Page
    {
        public int Flag
        {
            get
            {
                return ViewState["Flag"] == null ? 0 : int.Parse(ViewState["Flag"].ToString());
            }
            set
            {
                ViewState["Flag"] = value;
            }
        }

        public int RoleID
        {
            get
            {
                return ViewState["RoleID"] == null ? 0 : int.Parse(ViewState["RoleID"].ToString());
            }
            set
            {
                ViewState["RoleID"] = value;
            }
        }
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
                if (!string.IsNullOrEmpty(Request["flag"]))
                {
                    int flag, roleID = 0;
                    int.TryParse(Request["flag"], out flag);
                    Flag = flag;

                    if (!string.IsNullOrEmpty(Request["roleID"]))
                    {
                        int.TryParse(Request["roleID"], out roleID);

                        RoleID = roleID;
                    }
                    if (Flag == 2)//Edit flag
                    {
                        LoadRoleDataFromDB(RoleID);
                    }
                }
            }
        }
        /// <summary>
        /// load role
        /// </summary>
        /// <param name="RoleID"></param>
        private void LoadRoleDataFromDB(int RoleID)
        {
            US_ROLModel roleEntity= US_ROLBll.ClassInstance.Get_US_ROLByPKID(RoleID.ToString());
            txtRoleName.Text = roleEntity.Nombre_Rol;
        }
        /// <summary>
        /// cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelRole_Click(object sender, EventArgs e)
        {
            Response.Redirect("RoleManagement.aspx");
        }
        /// <summary>
        /// save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveRole_Click(object sender, EventArgs e)
        {
            US_ROLModel newRole = GetRoleInfoFromUI();
            if (Flag == 1)
            {
                int result = US_ROLBll.ClassInstance.Insert_US_ROL(newRole);
                if (result > 0)
                {
                    string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "CreatRoleSuccess") as string;
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "SaveUserSuccess", "alert('"+ strMsg +"');", true);
                }
            }
            else
            {
                newRole.Id_Rol = RoleID;
                int result2 = US_ROLBll.ClassInstance.Update_US_ROL(newRole);
                if (result2 > 0)
                {
                    string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "UpdateRoleSuccess") as string;
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "SaveUserSuccess", "alert('"+ strMsg +"');", true);
                }
            }
        }
        /// <summary>
        /// get role information
        /// </summary>
        /// <returns></returns>
        private US_ROLModel GetRoleInfoFromUI()
        {
            US_ROLModel  roleEntity = US_ROLBll.ClassInstance.Get_US_ROLByPKID(RoleID.ToString());
            roleEntity.Nombre_Rol = txtRoleName.Text;
            return roleEntity;
        }
    }
}
