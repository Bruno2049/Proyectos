using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.BussinessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using System.Data;

namespace PAEEEM
{
    public partial class RoleRelation : System.Web.UI.Page
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
                LoadRoleListFromDB();
                LoadRoleRelation();
                ddlRoleList.SelectedIndex = 0;
                CheckedRoleRelation();
            }
        }

        private void CheckedRoleRelation()
        {

            int roleID = 0;
            int.TryParse(ddlRoleList.SelectedValue, out roleID);
            for (int j = 0; j < cblRelation.Items.Count; j++)
            {
                cblRelation.Items[j].Selected = false;

            }
            US_ROLModel roleEntity =  US_ROLBll.ClassInstance.Get_US_ROLByPKID(roleID.ToString());
            string roleRelation = roleEntity.Relacion_Rol;
            string[] strArray = roleRelation.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < strArray.Length; i++)
            {
                for (int j = 0; j < cblRelation.Items.Count; j++)
                {

                    if (cblRelation.Items[j].Value == strArray[i].Trim())
                    {
                        cblRelation.Items[j].Selected = true;
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void LoadRoleRelation()
        {
            List<US_ROLModel> roleList = US_ROLBll.ClassInstance.Get_AllUS_ROL();
            cblRelation.DataSource = roleList;
            cblRelation.DataTextField = "Nombre_Rol";
            cblRelation.DataValueField = "Id_Rol";
            cblRelation.DataBind();
        }
        /// <summary>
        /// 
        /// </summary>
        private void LoadRoleListFromDB()
        {
            List<US_ROLModel> roleList = US_ROLBll.ClassInstance.Get_AllUS_ROL();
            ddlRoleList.DataSource = roleList;
            ddlRoleList.DataTextField = "Nombre_Rol";
            ddlRoleList.DataValueField = "Id_Rol";
            ddlRoleList.DataBind();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlRoleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckedRoleRelation();
        }
        /// <summary>
        /// save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int roleID = 0;
            int.TryParse(ddlRoleList.SelectedValue, out roleID);
            string relation = string.Empty;
            for (int j = 0; j < cblRelation.Items.Count; j++)
            {

                if (cblRelation.Items[j].Selected)
                {
                    relation += cblRelation.Items[j].Value+",";
                }
            }
            US_ROLModel roleEnty = new US_ROLModel();
            roleEnty.Id_Rol = roleID;
            roleEnty.Relacion_Rol = relation;
            int result = US_ROLBll.ClassInstance.Update_RoleRelation(roleEnty);
            if (result > 0)
            {
                string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgCreatRoleRelationsuccess") as string;
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "relationsave", "alert('"+ strMsg +"');", true);
            }
            else 
            {
                string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgCreatRoleRelationFail") as string;
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "relationfailed", "alert('"+ strMsg +"');", true);
                //ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "relationfailed", "alert('The role relation create failed!');", true);
            }
        }
        /// <summary>
        /// init labels
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
//        protected void Page_PreRender(object sender, EventArgs e)
//        {
//            //gridview header text
//            //Label2.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "Role") as string;
//            Label1.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "RoleRelation") as string;
//            Label3.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "RoleName") as string;           
//            btnSave.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "Save") as string;
//        }
    }
}
