using System;
using System.Collections;
using System.Collections.Generic;
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
using PAEEEM.DataAccessLayer;
using PAEEEM.Entities;

namespace PAEEEM.RegionalModule
{
    public partial class AssignDisposalToSupplier : System.Web.UI.Page
    {
        #region Global Variables
        /// <summary>
        /// property
        /// </summary>
        private int SupplierID
        {
            get
            {
                return ViewState["SupplierID"] == null ? 0 : Convert.ToInt32(ViewState["SupplierID"].ToString());
            }
            set
            {
                ViewState["SupplierID"] = value;
            }
        }
        private string SupplierType
        {
            get
            {
                return ViewState["SupplierType"] == null ? "" : ViewState["SupplierType"].ToString();
            }
            set
            {
                ViewState["SupplierType"] = value;
            }
        }
        private List<string> AssignedDisposal
        {
            get
            {
                return ViewState["AssignedDisposal"] == null ? null : ViewState["AssignedDisposal"] as List<string>;
            }
            set
            {
                ViewState["AssignedDisposal"] = value;
            }
        }
        #endregion

        #region Initialize Components
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserInfo"] == null)
                {
                    Response.Redirect("../Login/Login.aspx");
                    return;
                }
                //Init date control
                this.literalFecha.Text = DateTime.Now.ToString("dd-MM-yyyy");
                if (Request.QueryString["SupplierID"] != null && Request.QueryString["SupplierID"].ToString() != "" && Request.QueryString["Type"] != null && Request.QueryString["Type"].ToString() != "")
                {
                    SupplierID = Convert.ToInt32(System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["SupplierID"].ToString().Replace("%2B", "+"))));
                    SupplierType = System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["Type"].ToString().Replace("%2B", "+")));
                    //Init default data
                    InitDefaultData();
                }
                //this.treeViewDisposal.Attributes.Add("onclick", "OnTreeNodeChecked()"); 
            }
        }

        private void InitDefaultData()
        {
            InitHeaderData();
            RenderLoadTree();
            LoadDiposalAssigned();
        }

        private void InitHeaderData()
        {
            // RSA 2012-10-23 Read data depending on supplier type
            DataTable dtSupplierInfo;
            if (SupplierType.ToLower() == "proveedor")
            {
                dtSupplierInfo = CAT_PROVEEDORDal.ClassInstance.GetSupplierByPk(SupplierID);
            }
            else
            {
                dtSupplierInfo = SupplierBrancheDal.ClassInstance.GetSupplierBranch(SupplierID);
            }

            if (dtSupplierInfo != null && dtSupplierInfo.Rows.Count > 0)
            {
                txtSociaNamel.Text = dtSupplierInfo.Rows[0]["Dx_Razon_Social"].ToString();
                txtBusinessName.Text = dtSupplierInfo.Rows[0]["Dx_Nombre_Comercial"].ToString();
            }
        }

        /// <summary>
        /// Load navigation panel
        /// </summary>
        private void RenderLoadTree()
        {
            US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;

            // DataTable dtDisposalCenter = CAT_CENTRO_DISPDAL.ClassInstance.GetDisposalsByRegional(UserModel.Id_Departamento);
            DataTable dtDisposalCenter = CAT_CENTRO_DISPDAL.ClassInstance.GetDisposalsActive();
            if (dtDisposalCenter != null && dtDisposalCenter.Rows.Count > 0)
            {
                //Clear exist tree nodes
                if (treeViewDisposal.Nodes.Count > 0)
                {
                    treeViewDisposal.Nodes.Clear();
                }

                foreach (DataRow row in dtDisposalCenter.Rows)
                {
                    //Instantiate a new TreeNode instance as the root node
                    TreeNode ParentNode = new TreeNode(row["Dx_Nombre_Comercial"].ToString(), row["Id_Centro_Disp"].ToString(), "..\\Resources\\Images\\page.gif");
                    ParentNode.ToolTip = row["Id_Centro_Disp"].ToString() + "-(Matriz)"; // updated by tina 2012-02-29
                    if (ParentNode != null)
                    {
                        treeViewDisposal.Nodes.Add(ParentNode);
                        //Load child nodes for the root node by Role
                        LoadChildrenNodeToParent(ParentNode);
                    }
                }
            }
        }

        private void LoadChildrenNodeToParent(TreeNode ParentNode)
        {
            US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;

            DataTable dtDisposalBranch = CAT_CENTRO_DISP_SUCURSALDAL.ClassInstance.GetDisposalBranchByDisposal(ParentNode.Value);
            if (dtDisposalBranch != null && dtDisposalBranch.Rows.Count > 0)
            {
                foreach (DataRow row in dtDisposalBranch.Rows)
                {
                    //Instantiate a new TreeNode instance as the root node
                    TreeNode ChildNode = new TreeNode(row["Dx_Nombre_Comercial"].ToString(), row["Id_Centro_Disp_Sucursal"].ToString(), "..\\Resources\\Images\\page.gif");
                    ChildNode.ToolTip = row["Id_Centro_Disp_Sucursal"].ToString() + "-(Sucursal)"; // updated by tina 2012-02-29
                    if (ChildNode != null)
                    {
                        //Add child node to node collection
                        ParentNode.ChildNodes.Add(ChildNode);
                    }
                }
            }
        }

        private void LoadDiposalAssigned()
        {
            List<string> listAssignedDisposal = new List<string>();

            US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;
            DataTable dtDisposalOfSupplier = K_CAT_PROVEEDOR_CAT_CENTRO_DISPDal.ClassInstance.GetDisposalRelatedSupplier(SupplierID, SupplierType == "Proveedor" ? "M" : "B", UserModel.Id_Departamento);

            if (dtDisposalOfSupplier != null && dtDisposalOfSupplier.Rows.Count > 0)
            {
                foreach (DataRow row in dtDisposalOfSupplier.Rows)
                {
                    string disposalId = row["Id_Centro_Disp"].ToString();
                    string disposalType = row["Fg_Tipo_Centro_Disp"].ToString();
                    string mainCenterId = row["MainCenterID"] == DBNull.Value ? "" : row["MainCenterID"].ToString();

                    if (mainCenterId == "" && disposalType == "M") // main center
                    {
                        foreach (TreeNode ParentNode in treeViewDisposal.Nodes)
                        {
                            if (disposalId == ParentNode.Value)
                            {
                                listAssignedDisposal.Add(disposalId + ",M");
                                ParentNode.Checked = true;
                                break;
                            }
                        }
                    }
                    else // Branch
                    {
                        foreach (TreeNode ParentNode in treeViewDisposal.Nodes)
                        {
                            if (mainCenterId == ParentNode.Value)
                            {
                                foreach (TreeNode ChildNode in ParentNode.ChildNodes)
                                {
                                    if (disposalId == ChildNode.Value)
                                    {
                                        listAssignedDisposal.Add(disposalId + ",B");
                                        ChildNode.Checked = true;
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
            }
            AssignedDisposal = listAssignedDisposal;
        }
        #endregion

        #region Button Clicked
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int result = 0;
            List<string> listCheckedDisposal = GetDisposalChecked();
            List<string> listDeleteDisposal = GetDisposalDelete();

            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                if (listCheckedDisposal != null && listCheckedDisposal.Count > 0)
                {
                    result += K_CAT_PROVEEDOR_CAT_CENTRO_DISPBLL.ClassInstance.Insert_K_CAT_PROVEEDOR_CAT_CENTRO_DISP(SupplierID, SupplierType.ToLower() == "proveedor" ? "M" : "B", listCheckedDisposal);
                }
                if (listDeleteDisposal != null && listDeleteDisposal.Count > 0)
                {
                    result += K_CAT_PROVEEDOR_CAT_CENTRO_DISPBLL.ClassInstance.Delete_K_CAT_PROVEEDOR_CAT_CENTRO_DISP(SupplierID, SupplierType.ToLower() == "proveedor" ? "M" : "B", listDeleteDisposal);
                }
                scope.Complete();
            }
            if (result > 0)
            {
                RenderLoadTree();
                LoadDiposalAssigned();
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "AssignDisposal2Supplier", "alert('Los cambios han sido guardados.');", true);
            }
            else
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "AssignDisposal2Supplier", "alert('No hubo cambios.');", true);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("SupplierMonitor.aspx");
        }
        #endregion

        #region Private Methods
        private List<string> GetDisposalChecked()
        {
            List<string> listDisposal = new List<string>();
            foreach (TreeNode parentNode in treeViewDisposal.Nodes)
            {
                if (parentNode.Checked)
                {
                    if (!AssignedDisposal.Contains(parentNode.Value + ",M"))
                    {
                        listDisposal.Add(parentNode.Value + ",M");
                    }
                }

                if (parentNode.ChildNodes.Count > 0)
                {
                    foreach (TreeNode childNode in parentNode.ChildNodes)
                    {
                        if (childNode.Checked)
                        {
                            if (!AssignedDisposal.Contains(childNode.Value + ",B"))
                            {
                                listDisposal.Add(childNode.Value + ",B");
                            }
                        }
                    }
                }
            }
            return listDisposal;
        }

        private List<string> GetDisposalDelete()
        {
            List<string> listDeleteDisposal = new List<string>();
            foreach (TreeNode parentNode in treeViewDisposal.Nodes)
            {
                if (!parentNode.Checked)
                {
                    if (AssignedDisposal.Contains(parentNode.Value + ",M"))
                    {
                        listDeleteDisposal.Add(parentNode.Value + ",M");
                    }
                }

                if (parentNode.ChildNodes.Count > 0)
                {
                    foreach (TreeNode childNode in parentNode.ChildNodes)
                    {
                        if (!childNode.Checked)
                        {
                            if (AssignedDisposal.Contains(childNode.Value + ",B"))
                            {
                                listDeleteDisposal.Add(childNode.Value + ",B");
                            }
                        }
                    }
                }
            }
            return listDeleteDisposal;
        }
        #endregion
    }
}
