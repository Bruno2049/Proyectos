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
    public partial class PermissionTreeManage : System.Web.UI.Page
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
        /// <summary>
        /// Init data when page load
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
                //Load left part permission tree
                RenderPermissionTree();
                //Disable right part controls
                DisableNodeDetailInfo();
            }
        }
        /// <summary>
        /// disable controls
        /// </summary>
        private void DisableNodeDetailInfo()
        {
            txtNodeInfo.Enabled = false;
            txtNodeName.Enabled = false;
            txtNodeNo.Enabled = false;
            txtNodeSequence.Enabled = false;
            ddlParentNode.Enabled = false;
            ddlNodeType.Enabled = false;
        }
        /// <summary>
        /// load parent root for tree view
        /// </summary>
        private void InitNavigationNodes()
        {
            DataTable dtPagePermission = US_PERMISOBll.ClassInstance.Get_AllPagePermission();
            if (dtPagePermission != null)
            {
                ddlParentNode.DataSource = dtPagePermission;
                ddlParentNode.DataTextField = "Nombre_Navegacion";
                ddlParentNode.DataValueField = "Id_Navegacion";
                ddlParentNode.DataBind();
                ddlParentNode.Items.Insert(0, new ListItem("", ""));
            }

            if (ddlNodeType.SelectedValue == "P")
            {
                lblNodeInfo.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "lblNodeInfoPage") as string;
                lblParentNode.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "lblParentNodePage") as string;
            }
            else
            {
                lblNodeInfo.Text = "Button Name";
                lblParentNode.Text = "On Where Page";
            }
        }

        /// <summary>
        /// Load navigation panel by role
        /// </summary>
        /// <param name="roleID">Role ID</param>
        private void RenderPermissionTree()
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
                //Load child nodes for root
                LoadChildrenNodeToRoot(rootNode);
            }
        }

        /// <summary>
        /// Build child nodes for root
        /// </summary>
        /// <param name="rootNode"></param>
        /// <param name="roleID"></param>
        private void LoadChildrenNodeToRoot(TreeNode rootNode)
        {
            DataTable dtNavigationRoot = US_PERMISOBll.ClassInstance.Get_AllPermissionByParentID(rootNode.ToolTip);
            if (dtNavigationRoot != null && dtNavigationRoot.Rows.Count > 0)
            {
                for (int i = 0; i < dtNavigationRoot.Rows.Count; i++)
                {                        
                    TreeNode childNode = new TreeNode(dtNavigationRoot.Rows[i]["Nombre_Navegacion"].ToString(), dtNavigationRoot.Rows[i]["Id_Permiso"].ToString(), "..\\Resources\\Images\\page.gif");
                    childNode.ToolTip = dtNavigationRoot.Rows[i]["Id_Navegacion"].ToString();
                    rootNode.ChildNodes.Add(childNode);
                    DataTable operationList =  US_OPERACIONBll.ClassInstance.Get_OperationPermissionByNavCode(childNode.ToolTip);
                    if (operationList != null && operationList.Rows.Count > 0)
                    {
                        for (int m = 0; m < operationList.Rows.Count; m++)
                        {
                            childNode.ChildNodes.Add(new TreeNode(operationList.Rows[m]["Nombre_Navegacion"].ToString(), operationList.Rows[m]["Id_Permiso"].ToString(), "..\\Resources\\Images\\btn.gif"));
                        }
                    }
                    //Load child nodes for each node
                    LoadChildrenNodeToRoot(childNode);
                }
            }
        }
        /// <summary>
        /// Refresh permission tree after nodes changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            this.tvPemissionTree.Nodes.Clear();
            RenderPermissionTree();
        }
        /// <summary>
        /// Refresh navigation nodes when node type changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlNodeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitNavigationNodes();
        }
        /// <summary>
        /// Save node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int iCount = 0;
            if (Flag == 1 && ddlNodeType.SelectedValue == "P") //add node
            {
                US_NAVEGACIONModel navEntity = GetNavientityFromUI();
                US_PERMISOModel perEntity = GetPerEntityFromUI(ddlNodeType.SelectedValue);
                int navID=US_NAVEGACIONBll.ClassInstance.Insert_US_NAVEGACION(navEntity);
                perEntity.Valor_Permiso = navID;
                iCount = US_PERMISOBll.ClassInstance.Insert_US_PERMISO(perEntity);
                if (iCount > 0)
                {
                    string strMsg1 = HttpContext.GetGlobalResourceObject("DefaultResource", "mgNodeSuccess") as string;
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "failse", "alert('" + strMsg1 + "');", true);
                }
            }
            else if (Flag == 2 && ddlNodeType.SelectedValue == "P") //edit node
            {
                US_NAVEGACIONModel oldNavEntity = US_NAVEGACIONBll.ClassInstance.Get_US_NAVEGACIONByPKID(txtNodeNo.Text.Trim());
                string oldPath = oldNavEntity.Ruta_Padres;
                
                US_NAVEGACIONModel navEntity = GetNavientityFromUI();
                navEntity.Id_Navegacion = oldNavEntity.Id_Navegacion;
                US_PERMISOModel perEntity = GetPerEntityFromUI(ddlNodeType.SelectedValue);
                US_NAVEGACIONBll.ClassInstance.Update_US_NAVEGACION(navEntity);
                US_PERMISOBll.ClassInstance.Update_US_PERMISO(perEntity);

                List<US_NAVEGACIONModel> childNavList = US_NAVEGACIONBll.ClassInstance.GetChildrenNavListByParentCode(navEntity.Id_Navegacion);
                foreach (US_NAVEGACIONModel childNav in childNavList)
                {
                    childNav.Ruta_Padres = childNav.Ruta_Padres.Replace(oldPath, navEntity.Ruta_Padres);
                    iCount += US_NAVEGACIONBll.ClassInstance.Update_US_NAVEGACION(childNav);
                }
                if (iCount > 0)
                {
                    string strMsg2 = HttpContext.GetGlobalResourceObject("DefaultResource", "mgEditNodeSuccess") as string;
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "success", "alert('" + strMsg2 + "');", true);
                }
            }
        }
        /// <summary>
        /// check tree node
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private bool ExistNodeCode(string code)
        {
            return US_NAVEGACIONBll.ClassInstance.ExistNodeCode(code);
        }
        /// <summary>
        /// obtain data from ui
        /// </summary>
        /// <param name="nodeType"></param>
        /// <returns></returns>
        private US_PERMISOModel GetPerEntityFromUI(string nodeType)
        {
            US_PERMISOModel perEntity = new US_PERMISOModel();
            perEntity.Estatus_Permiso = "O";

            perEntity.Tipo_Permiso = nodeType;
            int val = 0;
            perEntity.Valor_Permiso = val;

            return perEntity;
        }
        /// <summary>
        /// get navigation from ui
        /// </summary>
        /// <returns></returns>
        private US_NAVEGACIONModel GetNavientityFromUI()
        {
            US_NAVEGACIONModel navEntity = new US_NAVEGACIONModel();
 
            navEntity.Nombre_Navegacion = txtNodeName.Text.Trim();
            navEntity.Url_Navegacion = txtNodeInfo.Text.Trim();
            navEntity.Codigo_Padres = ddlParentNode.SelectedValue;
            US_NAVEGACIONModel parentNav = US_NAVEGACIONBll.ClassInstance.Get_US_NAVEGACIONByPKID(navEntity.Codigo_Padres);
            if (parentNav.Codigo_Padres == "") navEntity.Ruta_Padres = ",0,";
            else
                navEntity.Ruta_Padres = parentNav.Ruta_Padres + parentNav.Id_Navegacion.ToString() + ",";
            int lev = 0;
            int.TryParse(parentNav.Nivel_Navegacion.ToString(), out lev);
            navEntity.Nivel_Navegacion = lev + 1;
            navEntity.Estatus = "A";
            navEntity.Secuencia = txtNodeSequence.Text.Trim();
            return navEntity;
        }
        /// <summary>
        /// add node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            txtNodeInfo.Enabled = true;
            txtNodeName.Enabled = true;
            txtNodeNo.Enabled = false;
            txtNodeSequence.Enabled = true;
            ddlParentNode.Enabled = true;
            ddlNodeType.Enabled = true;

            txtNodeInfo.Text = string.Empty;
            txtNodeName.Text = string.Empty;
            txtNodeNo.Text = string.Empty;
            txtNodeSequence.Text = string.Empty;
            ddlNodeType.SelectedValue = "P";
            //Init all navigations
            InitNavigationNodes();

            Flag = 1;
        }
        /// <summary>
        /// tree node selection change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvPemissionTree_SelectedNodeChanged(object sender, EventArgs e)
        {
            string navCode = tvPemissionTree.SelectedNode.ToolTip;
            DataTable dt = US_ROL_PERMISOBll.ClassInstance.Get_PermissionByNavCode(navCode);
            if (dt != null && dt.Rows.Count == 1)
            {
                txtNodeInfo.Text = dt.Rows[0]["Url_Navegacion"].ToString();
                txtNodeName.Text = dt.Rows[0]["Nombre_Navegacion"].ToString();
                txtNodeNo.Text = dt.Rows[0]["Id_Navegacion"].ToString();
                txtNodeSequence.Text = dt.Rows[0]["Nivel_Navegacion"].ToString();
                ddlNodeType.SelectedValue = dt.Rows[0]["Tipo_Permiso"].ToString();

                ddlParentNode.SelectedValue = dt.Rows[0]["Codigo_Padres"].ToString();
                txtNodeSequence.Text = dt.Rows[0]["Secuencia"].ToString();
            }

            DisableNodeDetailInfo();
        }
        /// <summary>
        /// edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (tvPemissionTree.SelectedNode != null && tvPemissionTree.SelectedNode.Value != string.Empty)
            {
                txtNodeInfo.Enabled = true;
                txtNodeName.Enabled = true;
                txtNodeNo.Enabled = false;
                txtNodeSequence.Enabled = true;
                ddlParentNode.Enabled = true;
                ddlNodeType.Enabled = true;
                //Init all navigations
                InitNavigationNodes();//Added by Jerry 2011/08/08
                this.ddlParentNode.SelectedItem.Text = this.tvPemissionTree.SelectedNode.Parent.Text;//Added by Jerry 2011/08/09
                this.ddlParentNode.SelectedValue = this.tvPemissionTree.SelectedNode.Parent.ToolTip;
                Flag = 2;
            }
            else
            {
                string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "mgPleaseSelectNode") as string;
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "NoItemSelected", "alert('" + strMsg + "');", true);
            }
        }
        /// <summary>
        /// delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (tvPemissionTree.SelectedNode != null && tvPemissionTree.SelectedNode.Value != string.Empty)
            {
                if (ExistChildrenNode(txtNodeNo.Text.Trim()))
                {
                    string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "mgNotDeleteNode") as string;
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "ExistChildNodes", "alert('" + strMsg + "');", true);
                    return;
                }
                US_NAVEGACIONBll.ClassInstance.Delete_US_NAVEGACION(txtNodeNo.Text.Trim());
                US_PERMISOBll.ClassInstance.Delete_Permission(txtNodeNo.Text.Trim(), ddlNodeType.SelectedValue.Trim());
            }
            else
            {
                string strMsg1 = HttpContext.GetGlobalResourceObject("DefaultResource", "mgPleaseSelectNode") as string;
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "NoSelected", "alert('" + strMsg1 + "');", true);
            }
        }
        /// <summary>
        /// check child tree node
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private bool ExistChildrenNode(string code)
        {
            return US_NAVEGACIONBll.ClassInstance.ExistChildrenNode(code);
        }
    }
}
