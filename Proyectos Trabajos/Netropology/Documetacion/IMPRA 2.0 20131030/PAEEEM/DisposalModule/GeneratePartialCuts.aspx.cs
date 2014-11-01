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
using PAEEEM.Entities;
using PAEEEM.BussinessLayer;
using PAEEEM.DataAccessLayer;
using PAEEEM.Helpers;

namespace PAEEEM.DisposalModule
{
    public partial class GeneratePartialCuts : System.Web.UI.Page
    {
        #region Global Variables
        /// <summary>
        /// property
        /// </summary>
        private DataTable CompleteProducts
        {
            get
            {
                return ViewState["CompleteProducts"] == null ? null : ViewState["CompleteProducts"] as DataTable;
            }
            set
            {
                ViewState["CompleteProducts"] = value;
            }
        }

        private DataTable SelectedProducts
        {
            get
            {
                return ViewState["SelectedProducts"] == null ? null : ViewState["SelectedProducts"] as DataTable;
            }
            set
            {
                ViewState["SelectedProducts"] = value;
            }
        }
        #endregion

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
            DataTable dtCompleteProducts = null;

            string DisposalType = string.Empty;
            if (UserModel.Tipo_Usuario == GlobalVar.DISPOSAL_CENTER)
            {
                DisposalType = "M";
            }
            else
            {
                DisposalType = "B";
            }

            dtCompleteProducts = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetCompleteProducts(UserModel.Id_Departamento, DisposalType, "", this.AspNetPager.CurrentPageIndex, this.AspNetPager.PageSize, out PageCount);
            if (dtCompleteProducts != null)
            {
                if (dtCompleteProducts.Rows.Count == 0)
                {
                    dtCompleteProducts.Rows.Add(dtCompleteProducts.NewRow());

                    btnGenerate.Enabled = false;
                }
                //Bind to grid view
                this.AspNetPager.RecordCount = PageCount;
                this.grdCompleteducts.DataSource = dtCompleteProducts;
                this.grdCompleteducts.DataBind();
            }
            CompleteProducts = dtCompleteProducts;
        }

        private void LoadProductsDataFromUI(out List<K_CORTE_PARCIALModel> listProducts, out List<string[]> listUpdate, out List<string[]> listUpdateStatus)
        {
            listProducts = new List<K_CORTE_PARCIALModel>(); // list of products for generate partial cut
            listUpdate = new List<string[]>(); // list of products for update K_RECUP_RESIDUOS status
            listUpdateStatus = new List<string[]>(); // list of products for update K_CREDITO_SUSTITUCION status
            DataTable dtMaterial = null;

            string PartialCutCode = string.Empty;
            PartialCutCode = DateTime.Now.Year.ToString().Substring(2, 2) + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + string.Format("{0:0000}", Convert.ToInt32(LsUtility.GetNumberSequence("PARTIALCUT")));

            ArrayList selectedProducts = new ArrayList();
            if (Session["SelectProducts"] != null)
            {
                selectedProducts = (ArrayList)Session["SelectProducts"];
            }

            foreach (DataRow dr in SelectedProducts.Rows)
            {
                if (selectedProducts.Contains(dr["Cod_Producto"].ToString()))
                {
                    dtMaterial = K_RECUP_RESIDUOSDAL.ClassInstance.Get_MaterialByCreditAndProduct(dr["No_Credito"].ToString(), dr["Cod_Producto"].ToString());
                    if (dtMaterial != null && dtMaterial.Rows.Count > 0)
                    {
                        foreach (DataRow drMaterial in dtMaterial.Rows)
                        {
                            K_CORTE_PARCIALModel model = new K_CORTE_PARCIALModel();
                            if (dr["Id_Centro_Disp"] != DBNull.Value)
                            {
                                model.ID_Centro_Disp = Convert.ToInt32(dr["Id_Centro_Disp"].ToString());
                            }
                            model.Codigo_Partial = PartialCutCode;
                            model.Codigo_Producto = dr["Cod_Producto"] == DBNull.Value ? "" : dr["Cod_Producto"].ToString();
                            if (dr["Cve_Tecnologia"] != DBNull.Value)
                            {
                                model.Cve_Tecnologia = Convert.ToInt32(dr["Cve_Tecnologia"].ToString());
                            }
                            model.Fg_Tipo_Centro_Disp = dr["Fg_Tipo_Centro_Disp"] == DBNull.Value ? "" : dr["Fg_Tipo_Centro_Disp"].ToString();

                            if (drMaterial["Cve_Material"] != DBNull.Value)
                            {
                                model.Cve_Material = Convert.ToInt32(drMaterial["Cve_Material"].ToString());
                            }
                            model.Dt_Fecha_Creacion = DateTime.Now;
                            model.ID_Estatus = (int)DisposalStatus.PENDIENTE;
                            if (drMaterial["Valor_Material"] != DBNull.Value)
                            {
                                model.Peso_Inicial = Convert.ToDecimal(drMaterial["Valor_Material"].ToString());
                            }

                            listProducts.Add(model);
                        }
                    }

                    string[] update = new string[2];
                    update[0] = dr["No_Credito"].ToString();
                    update[1] = dr["Cod_Producto"].ToString();
                    listUpdate.Add(update);

                    string[] updateStatus = new string[2];
                    updateStatus[0] = dr["No_Credito"].ToString();
                    updateStatus[1] = dr["Cve_Tecnologia"].ToString();
                    listUpdateStatus.Add(updateStatus);
                }
            }
        }

        protected void grdCompleteducts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox ckbSelect = (CheckBox)e.Row.FindControl("ckbSelect");
                if (e.Row.Cells[0].Text.Replace("&nbsp;", "") == "" && e.Row.Cells[1].Text.Replace("&nbsp;", "") == "")
                {
                    ckbSelect.Visible = false;
                }
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            RememberOldSelectedProducts();

            if (Session["SelectProducts"] != null && ((ArrayList)Session["SelectProducts"]).Count > 0)
            {
                int result = 0;
                List<K_CORTE_PARCIALModel> listProducts = new List<K_CORTE_PARCIALModel>();
                List<string[]> listUpdate = new List<string[]>();
                List<string[]> listUpdateStatus = new List<string[]>();
                LoadProductsDataFromUI(out listProducts, out listUpdate, out listUpdateStatus);

                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    // save data to partial cut
                    result = K_CORTE_PARCIALBLL.ClassInstance.Insert_K_CORTE_PARCIAL(listProducts);

                    // update K_RECUP_RESIDUOS status
                    result += K_RECUP_RESIDUOSBLL.ClassInstance.UpdateK_RECUP_RESIDUOSStatus(listUpdate);

                    // update K_CREDITO_SUSTITUCION status to inhabilitación
                    result += K_CREDITO_SUSTITUCIONBLL.ClassInstance.UpdateK_CREDITO_SUSTITUCIONStatus(listUpdateStatus);

                    scope.Complete();
                }
                if (result > 0)
                {
                    Response.Redirect("CreditMonitor.aspx");
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreditMonitor.aspx");
        }

        protected void AspNetPager_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            if (IsPostBack)
            {
                RememberOldSelectedProducts();
            }
        }

        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                LoadGridViewData();
                RePopulateSelectedProducts();
            }
        }

        /// <summary>
        /// remember the selected products
        /// </summary>
        private void RememberOldSelectedProducts()
        {
            int count = 0;
            ArrayList listProducts = new ArrayList();

            foreach (GridViewRow row in grdCompleteducts.Rows)
            {
                if (SelectedProducts == null)
                {
                    SelectedProducts = CompleteProducts.Clone();
                }

                string productCode = row.Cells[0].Text;
                bool result = ((CheckBox)row.FindControl("ckbSelect")).Checked;

                if (Session["SelectProducts"] != null)
                {
                    listProducts = (ArrayList)Session["SelectProducts"];
                }
                if (result)
                {
                    // save selected product code
                    if (!listProducts.Contains(productCode))
                    {
                        listProducts.Add(productCode);
                    }

                    // save selected product detail
                    foreach (DataRow drComplete in CompleteProducts.Rows)
                    {
                        if (SelectedProducts.Rows.Count > 0)
                        {
                            if (drComplete["Cod_Producto"].ToString() == productCode)
                            {
                                foreach (DataRow drSelected in SelectedProducts.Rows)
                                {
                                    if (drSelected["Cod_Producto"].ToString() != productCode)
                                    {
                                        count++;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                    if (count == SelectedProducts.Rows.Count)
                                    {
                                        SelectedProducts.ImportRow(drComplete);
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        else
                        {
                            SelectedProducts.ImportRow(drComplete);
                            break;
                        }
                    }
                }
                else
                {
                    if (listProducts.Contains(productCode))
                    {
                        listProducts.Remove(productCode);

                        foreach (DataRow dr in SelectedProducts.Rows)
                        {
                            if (dr["Cod_Producto"].ToString() == productCode)
                            {
                                SelectedProducts.Rows.Remove(dr);
                                break;
                            }
                        }
                    }
                }

                if (listProducts != null && listProducts.Count > 0)
                {
                    Session["SelectProducts"] = listProducts;
                }
            }
        }

        /// <summary>
        /// repopulate checkbox status
        /// </summary>
        private void RePopulateSelectedProducts()
        {
            ArrayList listProducts = (ArrayList)Session["SelectProducts"];

            if (listProducts != null && listProducts.Count > 0)
            {
                foreach (GridViewRow row in grdCompleteducts.Rows)
                {
                    string productCode = row.Cells[0].Text;

                    if (listProducts.Contains(productCode))
                    {
                        CheckBox ckbSelect = (CheckBox)row.FindControl("ckbSelect");

                        ckbSelect.Checked = true;
                    }
                }
            }
        }       
    }
}
