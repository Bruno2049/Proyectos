using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using PAEEEM.BussinessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;


namespace PAEEEM
{
    public partial class Site : System.Web.UI.MasterPage
    {
        #region Property
        public string SURL
        {
            get
            {
                return Session["SURL"] == null ? string.Empty : Session["SURL"].ToString();
            }
            set
            {
                Session["SURL"] = value;
            }
        }
        private string _CurrentPath;

        public string BaseURL
        {
            get
            {
                _CurrentPath = HttpContext.Current.Server.MapPath(".").ToLower();//Path de la ruta absoluta(MapPath aquí "." En nombre del servidor actual)   

                if (_CurrentPath.Length == 1)
                {
                    _CurrentPath = "";
                }
                return _CurrentPath;
            }
        }
        #endregion
        /// <summary>
        /// Page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (null != Session["UserInfo"])
                {
                    int iRoleID;
                    US_USUARIOModel UserModel = (US_USUARIOModel)Session["UserInfo"];

                    if (UserModel != null)
                    {
                        aUserName.InnerText = "Bienvenido " + UserModel.Nombre_Usuario
                            +" (" + US_ROLBll.ClassInstance.Get_US_ROLByPKID(UserModel.Id_Rol.ToString()).Nombre_Rol + "), ";
                        //Load navigation menu according to Role
                        iRoleID = UserModel.Id_Rol;
                        var idUsuario = UserModel.Id_Usuario;
                        if (iRoleID != 0)
                        {
                            RenderLoadTree(iRoleID, idUsuario);
                        }
                    }
                }
                else
                {
                    Response.Redirect("/Login/Login.aspx");
                }
            }
        }
        /// <summary>
        /// Load navigation panel by role
        /// </summary>
        /// <param name="roleID">Role ID</param>
        private void RenderLoadTree(int RoleID, int idUsuario)
        {
            DataTable dtNavigationRoot = US_NAVEGACIONBll.ClassInstance.Get_RoleNavigationRootByRoleID(RoleID);
            if (dtNavigationRoot != null && dtNavigationRoot.Rows.Count > 0)
            {
                //Clear exist tree nodes
                if (tvQuickLaunch.Nodes.Count > 0)
                {
                    tvQuickLaunch.Nodes.Clear();
                }
                //Instantiate a new TreeNode instance as the root node
                TreeNode RootNode = new TreeNode(dtNavigationRoot.Rows[0]["Nombre_Navegacion"].ToString(), dtNavigationRoot.Rows[0]["Id_Navegacion"].ToString());
                if (RootNode != null)
                {
                    tvQuickLaunch.Nodes.Add(RootNode);
                    //Load child nodes for the root node by Role
                    LoadChildrenNodeToRoot(RootNode, RoleID, idUsuario);
                }
            }
        }
        /// <summary>
        /// Build child nodes for root
        /// </summary>
        /// <param name="rootNode"></param>
        /// <param name="roleID"></param>
        private void LoadChildrenNodeToRoot(TreeNode RootNode, int RoleID, int idUsuario)
        {
            //DataTable dtNavigationRoot = US_NAVEGACIONBll.ClassInstance.Get_RoleNavigationByRoleID(RoleID, RootNode.Value);
            DataTable dtNavigationRoot = US_NAVEGACIONBll.ClassInstance.ObtenNavegacionRolUsuario(RoleID, idUsuario,
                                                                                                  RootNode.Value);

            if (dtNavigationRoot != null && dtNavigationRoot.Rows.Count > 0)
            {
                //For each all the child nodes
                for (int i = 0; i < dtNavigationRoot.Rows.Count; i++)
                {
                    TreeNode ChildNode = null;
                    string NavigationLabel = (string)HttpContext.GetGlobalResourceObject("DefaultResource", dtNavigationRoot.Rows[i]["Nombre_Navegacion"].ToString());
                    if (!string.IsNullOrEmpty(NavigationLabel))
                    {
                        ChildNode = new TreeNode(NavigationLabel, dtNavigationRoot.Rows[i]["Id_Navegacion"].ToString());
                    }
                    else
                    {
                        ChildNode = new TreeNode(dtNavigationRoot.Rows[i]["Nombre_Navegacion"].ToString(), dtNavigationRoot.Rows[i]["Id_Navegacion"].ToString());
                    }

                    //Search all the nodes to find current selected node for another color style
                    ChildNode.SelectAction = TreeNodeSelectAction.SelectExpand;
                    if (Request["surl"] != null && Request["surl"].Length > 0)
                    {
                        SURL = Request["surl"];
                    }

                    if (SURL != string.Empty && ChildNode.Value == SURL)
                    {
                        ChildNode.Select();
                    }
                    //Add child node to node collection
                    RootNode.ChildNodes.Add(ChildNode);
                    //Recursion to load child nodes for each node if exist
                    LoadChildrenNodeToRoot(ChildNode, RoleID, idUsuario);
                }
            }
        }
        /// <summary>
        /// store view state of main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvMainMenu_DataBound(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["TreeViewState"] != null)
                {
                    TreeViewState tvsTreeViewState = new TreeViewState();
                    tvsTreeViewState.RestoreTreeViewState(tvQuickLaunch.Nodes, (ArrayList)Session["TreeViewState"]);
                }
            }
        }
        /// <summary>
        /// store tree view state when node expanded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvMainMenu_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
        {
            ArrayList list = null;
            if (null == Session["TreeViewState"])
            {
                list = new ArrayList();
            }
            else
            {
                list = (ArrayList)Session["TreeViewState"];
            }

            ArrayList NodesToRemove = new ArrayList();
            foreach (string valuePath in list)
            {
                int nDepth = -1;
                if (!int.TryParse(valuePath.Split('^')[0], out nDepth))
                    nDepth = -1;
                if (nDepth == e.Node.Depth)
                    NodesToRemove.Add(valuePath);
            }
            foreach (string NodeValuePath in NodesToRemove)
                if (list.Contains(NodeValuePath))
                    list.Remove(NodeValuePath);

            if (!list.Contains(e.Node.Depth + "^" + e.Node.ValuePath))
            {
                list.Add(e.Node.Depth + "^" + e.Node.ValuePath);
            }
            Session["TreeViewState"] = list;
            TreeViewState tvs = new TreeViewState();
            tvs.RestoreTreeViewState(tvQuickLaunch.Nodes, list);
        }

        /// <summary>
        /// remove node when node collapsed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvMainMenu_TreeNodeCollapsed(object sender, TreeNodeEventArgs e)
        {
            ArrayList list = null;
            if (Session["TreeViewState"] == null)
            {
                list = new ArrayList();
            }
            else
            {
                list = (ArrayList)Session["TreeViewState"];
            }
            if (list.Contains(e.Node.Depth + "^" + e.Node.ValuePath))
            {
                list.Remove(e.Node.Depth + "^" + e.Node.ValuePath);
            }
        }

        protected void tvQuickLaunch_SelectedNodeChanged(object sender, EventArgs e)
        {
            int RoleID = 0;
            US_USUARIOModel UserModel = (US_USUARIOModel)Session["UserInfo"];
            if (UserModel != null)
            {
                if (UserModel.Id_Rol > 0)
                {
                    RoleID = UserModel.Id_Rol;
                }

                var idUsuario = UserModel.Id_Usuario;
                string url = string.Empty;
                //url = US_NAVEGACIONBll.ClassInstance.Get_URLNavigationByRoleID(RoleID, tvQuickLaunch.SelectedNode.Value);
                url = US_NAVEGACIONBll.ClassInstance.ObtenUrlNavegacion(RoleID, idUsuario,
                                                                        tvQuickLaunch.SelectedNode.Value);
                if (url.Length == 0)
                    return;

                string sep = url.Contains('?') ? "&" : "?";
                url = Request.ApplicationPath + url;
                Response.Redirect(url + sep + "surl=" + tvQuickLaunch.SelectedNode.Value);
            }
        }
        /// <summary>
        /// login out to clear session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbLogOut_Click(object sender, EventArgs e)
        {
            if (Session["UserName"] != null && Session["UserName"].ToString() != string.Empty)
            {
                HttpContext.Current.Cache.Remove(Session["UserName"].ToString());
            }
            Session.Clear();
            Session.Abandon();

            Response.Redirect("/Login/Login.aspx");
        }
    }
    /// <summary>
    /// internal helper class to store tree view state
    /// </summary>
    internal class TreeViewState
    {
        public void RestoreTreeViewState(TreeNodeCollection nodes, ArrayList list)
        {
            foreach (TreeNode node in nodes)
            {
                // Restore the state of one node.   
                if (list.Contains(node.Depth + "^" + node.ValuePath))
                {
                    node.Expanded = true;
                }
                else
                {
                    node.Expanded = false;
                }
                // If the node has child nodes, restore their states, too.   
                if ((node.ChildNodes != null && node.ChildNodes.Count != 0))
                {
                    RestoreTreeViewState(node.ChildNodes, list);
                }
            }
        }
    }
}
