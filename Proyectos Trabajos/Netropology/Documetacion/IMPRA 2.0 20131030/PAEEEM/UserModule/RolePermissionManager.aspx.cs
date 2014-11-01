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
    public partial class RolePermissionManager : System.Web.UI.Page
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
                // load left part roles
                LoadRoleListFromDB();
                // load left part permission type
                LoadPermissionType();

                //LoadPermission();
                ddlRoleList.SelectedIndex = 0;
                ddlPermissionType.SelectedIndex = 0;
                // load right part navigation
                RenderLoadTree();
                CheckPermission();
            }
        }
        /// <summary>
        /// Load navigation panel by role
        /// </summary>
        /// <param name="roleID">Role ID</param>
        private void RenderLoadTree()
        {
            try
            {
                DataTable dtNavigationRoot = US_PERMISOBll.ClassInstance.Get_RootPermission();
                if (dtNavigationRoot != null && dtNavigationRoot.Rows.Count > 0)
                {
                    TreeNode rootNode = new TreeNode(dtNavigationRoot.Rows[0]["Nombre_Navegacion"].ToString(), dtNavigationRoot.Rows[0]["Id_Permiso"].ToString(), "..\\Resources\\Images\\page.gif");
                    rootNode.ToolTip = dtNavigationRoot.Rows[0]["Id_Navegacion"].ToString();
                    rootNode.Checked = true;

                    tvPemissionTree.Nodes.Add(rootNode);
                    DataTable operationList = US_OPERACIONBll.ClassInstance.Get_OperationPermissionByNavCode(rootNode.ToolTip);
                    if (operationList != null && operationList.Rows.Count > 0)
                    {
                        for (int m = 0; m < operationList.Rows.Count; m++)
                        {
                            rootNode.ChildNodes.Add(new TreeNode(operationList.Rows[m]["Nombre_Navegacion"].ToString(), operationList.Rows[m]["Id_Permiso"].ToString(), "..\\Resources\\Images\\btn.gif"));
                        }
                    }
                    LoadChildrenNodeToRoot(rootNode);
                }
            }
            catch (LsBLException)
            {
            }
        }
        /// <summary>
        /// Build child nodes for root
        /// </summary>
        /// <param name="rootNode"></param>
        /// <param name="roleID"></param>
        private void LoadChildrenNodeToRoot(TreeNode rootNode)
        {
            try
            {
                DataTable dtNavigationRoot = US_PERMISOBll.ClassInstance.Get_AllPermissionByParentID(rootNode.ToolTip);
                if (dtNavigationRoot != null && dtNavigationRoot.Rows.Count > 0)
                {
                    for (int i = 0; i < dtNavigationRoot.Rows.Count; i++)
                    {
                        TreeNode childNode = new TreeNode(dtNavigationRoot.Rows[i]["Nombre_Navegacion"].ToString(), dtNavigationRoot.Rows[i]["Id_Permiso"].ToString(), "..\\Resources\\Images\\page.gif");
                        childNode.ToolTip = dtNavigationRoot.Rows[i]["Id_Navegacion"].ToString();
                        rootNode.ChildNodes.Add(childNode);
                        DataTable operationList = US_OPERACIONBll.ClassInstance.Get_OperationPermissionByNavCode(childNode.ToolTip);
                        if (operationList != null && operationList.Rows.Count > 0)
                        {
                            for (int m = 0; m < operationList.Rows.Count; m++)
                            {
                                childNode.ChildNodes.Add(new TreeNode(operationList.Rows[m]["Nombre_Navegacion"].ToString(), operationList.Rows[m]["Id_Permiso"].ToString(), "..\\Resources\\Images\\btn.gif"));
                            }
                        }
                        LoadChildrenNodeToRoot(childNode);
                    }
                }
            }
            catch (LsBLException)
            {
                string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "mgLoadDataError") as string;
                ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "BuildNodesError", "alert('"+ strMsg +"');", true);
                //ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "BuildNodesError", "alert('load data error!');", true);
            }
        }
        /// <summary>
        /// check permission
        /// </summary>
        private void CheckPermission()
        {
            CheckAllTree(tvPemissionTree, false);
            int roleID = 0;
            int.TryParse(ddlRoleList.SelectedValue, out roleID);
            DataTable dtPermisson = null;
            dtPermisson = US_PERMISOBll.ClassInstance.Get_RolePagePermissionByRoleID(roleID);
            DataTable dtOperationPermission = US_PERMISOBll.ClassInstance.Get_RoleOperationPermissionByRoleID(roleID);
            if (tvPemissionTree.Nodes.Count > 0)
            {
                for (int i = 0; i < tvPemissionTree.Nodes.Count; i++)
                {

                    if (dtPermisson != null && dtPermisson.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtPermisson.Rows.Count; j++)
                        {
                            if (tvPemissionTree.Nodes[i].Value == dtPermisson.Rows[j]["Id_Permiso"].ToString())
                            {
                                tvPemissionTree.Nodes[i].Checked = true;
                            }
                        }
                        CheckTreeNode(tvPemissionTree.Nodes[i], dtPermisson);
                    }
                    if (dtOperationPermission != null && dtOperationPermission.Rows.Count > 0)
                    {
                        for (int m = 0; m < dtOperationPermission.Rows.Count; m++)
                        {
                            if (tvPemissionTree.Nodes[i].Value == dtOperationPermission.Rows[m]["Id_Permiso"].ToString())
                            {
                                tvPemissionTree.Nodes[i].Checked = true;
                            }
                        }
                        CheckOperationTreeNode(tvPemissionTree.Nodes[i], dtOperationPermission);
                    }
                }
            }
        }
        /// <summary>
        /// check operation
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="dtOperationPermission"></param>
        private void CheckOperationTreeNode(TreeNode treeNode, DataTable dtOperationPermission)
        {
            if (treeNode.ChildNodes.Count > 0)
            {
                for (int i = 0; i < treeNode.ChildNodes.Count; i++)
                {

                    for (int j = 0; j < dtOperationPermission.Rows.Count; j++)
                    {
                        if (dtOperationPermission.Rows[j]["Id_Permiso"].ToString() == treeNode.ChildNodes[i].Value)
                        {
                            treeNode.ChildNodes[i].Checked = true;
                        }
                    }

                    CheckTreeNode(treeNode.ChildNodes[i], dtOperationPermission);
                }
            }
        }
        /// <summary>
        /// check navigation tree
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="dtPermisson"></param>
        private void CheckTreeNode(TreeNode treeNode, DataTable dtPermisson)
        {
            if (treeNode.ChildNodes.Count > 0)
            {
                for (int i = 0; i < treeNode.ChildNodes.Count; i++)
                {

                    for (int j = 0; j < dtPermisson.Rows.Count; j++)
                    {
                        if (dtPermisson.Rows[j]["Id_Permiso"].ToString() == treeNode.ChildNodes[i].Value)
                        {
                            treeNode.ChildNodes[i].Checked = true;
                        }
                    }

                    CheckTreeNode(treeNode.ChildNodes[i], dtPermisson);
                }
            }
        }

        // load permission type
        private void LoadPermissionType()
        {
            ListItem item1 = new ListItem("Operation Permission", "O");
            ListItem item2 = new ListItem("Page Permission", "P");
            ddlPermissionType.Items.Add(item1);
            ddlPermissionType.Items.Add(item2);
        }
        // load roles
        private void LoadRoleListFromDB()
        {
            List<US_ROLModel> roleList = US_ROLBll.ClassInstance.Get_AllUS_ROL();
            ddlRoleList.DataSource = roleList;
            ddlRoleList.DataTextField = "Nombre_Rol";
            ddlRoleList.DataValueField = "Id_Rol";
            ddlRoleList.DataBind();
        }
        // perform the saving
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!tvPemissionTree.Nodes[0].Checked)
            {
                string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "mgPleasecheckrootnodes") as string;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "SaveRoleAerssrt", "alert('"+ strMsg +"');", true);              
                return;
            }
            if (!CheckedSecondLevelNode())
            {
                string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "mgPleasecheckSecondrootnodes") as string;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "SaveRoleAerssrt", "alert('"+ strMsg +"');", true);                
                return;
            }
            US_ROL_PERMISOModel rolePerm = new US_ROL_PERMISOModel();
            int roleID = 0;
            int.TryParse(ddlRoleList.SelectedValue, out roleID);
            rolePerm.Id_Rol = roleID;
            try
            {
                int result = US_ROL_PERMISOBll.ClassInstance.Delete_Role_PermissionByRoleID(roleID);
                List<int> perlist = GetPermissionListFromUI();
                int perid = 0;

                foreach (int pid in perlist)
                {
                    perid++;
                    rolePerm.Id_Permiso = pid;
                    US_ROL_PERMISOBll.ClassInstance.Insert_US_ROL_PERMISO(rolePerm);
                }
                if (perid == perlist.Count)
                {
                    string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "mgSaveRoleRelationSuccess") as string;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "SaveRoleAlert", "alert('" + strMsg + "');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(Page), "InitError", "alert('" + ex.Message + "');", true);
            }
        }
        /// <summary>
        /// check the second level node
        /// </summary>
        /// <returns></returns>
        private bool CheckedSecondLevelNode()
        {
            bool isChecked = false;
            TreeNodeCollection nodes = tvPemissionTree.Nodes[0].ChildNodes;
            foreach (TreeNode item in nodes)
            {
                if (item.Checked)
                {
                    isChecked = true;
                    break;
                }
            }
            return isChecked;
        }
        /// <summary>
        /// get the permission from UI
        /// </summary>
        /// <returns></returns>
        private List<int> GetPermissionListFromUI()
        {
            List<int> intList = new List<int>();
            if (tvPemissionTree.Nodes.Count > 0)
            {
                for (int i = 0; i < tvPemissionTree.Nodes.Count; i++)
                {
                    if (tvPemissionTree.Nodes[i].Checked)
                    {
                        int valueid = 0;
                        int.TryParse(tvPemissionTree.Nodes[i].Value, out valueid);
                        intList.Add(valueid);
                        intList = GetChildValue(tvPemissionTree.Nodes[i], intList);
                    }
                }
            }
            return intList;
        }
        /// <summary>
        /// get the childvalue of the permission tree
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="intList"></param>
        /// <returns></returns>
        private List<int> GetChildValue(TreeNode treeNode, List<int> intList)
        {
            if (treeNode.ChildNodes.Count > 0)
            {
                for (int j = 0; j < treeNode.ChildNodes.Count; j++)
                {
                    if (treeNode.ChildNodes[j].Checked)
                    {
                        int valueid = 0;
                        int.TryParse(treeNode.ChildNodes[j].Value, out valueid);
                        intList.Add(valueid);
                        intList = GetChildValue(treeNode.ChildNodes[j], intList);
                    }
                }
            }
            return intList;
        }
        /// <summary>
        /// select permission type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPermissionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckPermission();
        }
        /// <summary>
        /// select all
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckAllTree(tvPemissionTree, cbSelectAll.Checked);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tvPemissionTree"></param>
        /// <param name="isChecked"></param>
        private void CheckAllTree(TreeView tvPemissionTree, bool isChecked)
        {
            if (tvPemissionTree.Nodes.Count > 0)
            {
                for (int i = 0; i < tvPemissionTree.Nodes.Count; i++)
                {
                    tvPemissionTree.Nodes[i].Checked = true;
                    CheckAllTreeNode(tvPemissionTree.Nodes[i], isChecked);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="isChecked"></param>
        private void CheckAllTreeNode(TreeNode treeNode, bool isChecked)
        {
            if (treeNode.ChildNodes.Count > 0)
            {
                for (int i = 0; i < treeNode.ChildNodes.Count; i++)
                {
                    treeNode.ChildNodes[i].Checked = isChecked;
                    CheckAllTreeNode(treeNode.ChildNodes[i], isChecked);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlRoleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckPermission();
        }
        /// <summary>
        /// init labels
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
//        protected void Page_PreRender(object sender, EventArgs e)
//        {
//            //gridview header text
//            Label2.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "lblRole") as string;
//            Label1.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "lblPermissionTree") as string;
//            cbSelectAll.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "lblSelectAll") as string;
//            Label3.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "lblRoleName") as string;
//            Label4.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "lblPermissionType") as string;
//            btnSave.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "btnSave") as string;
//        }
    }
}
