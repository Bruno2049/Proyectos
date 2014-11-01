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
using PAEEEM.BussinessLayer;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;

namespace PAEEEM.DisposalModule
{
    public partial class MaterialRegistryMonitor : System.Web.UI.Page
    {
        #region Global Variables
        private int Id_Recuperacion
        {
            get
            {
                return ViewState["Id_Recuperacion"] == null ? 0 : Convert.ToInt32(ViewState["Id_Recuperacion"].ToString());
            }
            set
            {
                ViewState["Id_Recuperacion"] = value;
            }
        }
        private int Technology
        {
            get
            {
                return ViewState["Technology"] == null ? 0 : Convert.ToInt32(ViewState["Technology"].ToString());
            }
            set
            {
                ViewState["Technology"] = value;
            }
        }
        private int Material
        {
            get
            {
                return ViewState["Material"] == null ? 0 : Convert.ToInt32(ViewState["Material"].ToString());
            }
            set
            {
                ViewState["Material"] = value;
            }
        }
        private DataTable RecoveryProducts
        {
            get
            {
                return ViewState["RecoveryProducts"] == null ? null : ViewState["RecoveryProducts"] as DataTable;
            }
            set
            {
                ViewState["RecoveryProducts"] = value;
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
                InitDefaultData();

                if (Request.QueryString["Id_Recuperacion"] != null && Request.QueryString["Id_Recuperacion"].ToString() != "")
                {
                    Id_Recuperacion = Convert.ToInt32(System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["Id_Recuperacion"].ToString().Replace("%2B", "+"))));
                    Technology = Convert.ToInt32(System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["Technology"].ToString().Replace("%2B", "+"))));
                    Material = Convert.ToInt32(System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["Material"].ToString().Replace("%2B", "+"))));
                }
            }
        }

        /// <summary>
        /// Init drop down list data 
        /// </summary>
        private void InitDefaultData()
        {
            InitProgram();
            InitCredit();
            InitInternalCode();
            InitSupplier();
            InitTechnology();

            AspNetPager.Visible = false;
            btnRegistry.Visible = false;

            txtReceiptFromDate.Text = txtReceiptToDate.Text = txtInhabilitacionFromDate.Text = txtInhabilitacionToDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void InitProgram()
        {
            DataTable dtProgram = null;
            dtProgram = CAT_PROGRAMADal.ClassInstance.Get_ALL_PROGRAMA();
            if (dtProgram != null)
            {
                drpProgram.DataSource = dtProgram;
                drpProgram.DataTextField = "Dx_Nombre_Programa";
                drpProgram.DataValueField = "ID_Prog_Proy";
                drpProgram.DataBind();
                drpProgram.Items.Insert(0, new ListItem(""));
            }
        }

        private void InitCredit()
        {
            DataTable dtCredit = null;
            string program = (drpProgram.SelectedIndex == -1 || drpProgram.SelectedIndex == 0) ? "" : drpProgram.SelectedValue;
            dtCredit = K_CREDITODal.ClassInstance.GetCreditsByProgram(program);
            if (dtCredit != null)
            {
                drpCredit.DataSource = dtCredit;
                drpCredit.DataTextField = "No_Credito";
                drpCredit.DataValueField = "No_Credito";
                drpCredit.DataBind();
                drpCredit.Items.Insert(0, new ListItem(""));
            }
        }

        private void InitInternalCode()
        {
            DataTable dtInternalCode = null;
            string credit = (drpCredit.SelectedIndex == -1 || drpCredit.SelectedIndex == 0) ? "" : drpCredit.SelectedValue;
            dtInternalCode = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetInternalCodeByCredit(credit);
            if (dtInternalCode != null)
            {
                drpInteralCode.DataSource = dtInternalCode;
                drpInteralCode.DataTextField = "Id_Folio";
                drpInteralCode.DataValueField = "Id_Folio";
                drpInteralCode.DataBind();
                drpInteralCode.Items.Insert(0, new ListItem(""));
            }
        }

        private void InitTechnology()
        {
            DataTable dtTechnology = null;

            US_USUARIOModel UserModel = null;
            UserModel = Session["UserInfo"] as US_USUARIOModel;

            string program = (drpProgram.SelectedIndex == -1 || drpProgram.SelectedIndex == 0) ? "" : drpProgram.SelectedValue;
            if (program != "")
            {
                dtTechnology = CAT_TECNOLOGIADAL.ClassInstance.Get_All_CAT_TECNOLOGIAByProgramAndDisposalID(program, UserModel.Id_Departamento, UserModel.Tipo_Usuario == GlobalVar.DISPOSAL_CENTER ? "M" : "B");
            }
            else
            {
                dtTechnology = CAT_TECNOLOGIADAL.ClassInstance.Get_All_CAT_TECNOLOGIAByDisposalID(UserModel.Id_Departamento, UserModel.Tipo_Usuario == GlobalVar.DISPOSAL_CENTER ? "M" : "B");
            }
            if (dtTechnology != null)
            {
                drpTechnology.DataSource = dtTechnology;
                drpTechnology.DataTextField = "Dx_Nombre_General";
                drpTechnology.DataValueField = "Cve_Tecnologia";
                drpTechnology.DataBind();
                drpTechnology.Items.Insert(0, new ListItem(""));
                drpTechnology.SelectedValue = Technology.ToString();
            }

            drpTechnology.Enabled = false;
        }

        private void InitSupplier()
        {
            DataTable dtSupplier = null;

            US_USUARIOModel UserModel = null;
            UserModel = Session["UserInfo"] as US_USUARIOModel;

            dtSupplier = CAT_PROVEEDORDal.ClassInstance.Get_CAT_PROVEEDORbyDisposalID(UserModel.Id_Departamento, UserModel.Tipo_Usuario == GlobalVar.DISPOSAL_CENTER ? "M" : "B");
            if (dtSupplier != null)
            {
                drpDistributor.DataSource = dtSupplier;
                drpDistributor.DataTextField = "Dx_Nombre_Comercial";
                drpDistributor.DataValueField = "Id_Proveedor";
                drpDistributor.DataBind();
                drpDistributor.Items.Insert(0, new ListItem(""));
            }
        }

        protected void drpProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitCredit();
            InitTechnology();
        }

        protected void drpCredit_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitInternalCode();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SaveFilterIntoSession();
            SetOldSelectedProductToEmpty();
            LoadGridViewData("P");
        }

        private void SaveFilterIntoSession()
        {
            Session["CurrentProgram"] = drpProgram.SelectedIndex;
            Session["CurrentCredit"] = drpCredit.SelectedIndex;
            Session["CurrentInternalCode"] = drpInteralCode.SelectedIndex;
            Session["CurrentSupplier"] = drpDistributor.SelectedIndex;
            Session["CurrentTechnology"] = drpTechnology.SelectedIndex;
            Session["CurrentReceiptFromDate"] = txtReceiptFromDate.Text.Trim();
            Session["CurrentReceiptToDate"] = txtReceiptToDate.Text.Trim();
            Session["CurrentInhabilitacionFromDate"] = txtInhabilitacionFromDate.Text.Trim();
            Session["CurrentInhabilitacionToDate"] = txtInhabilitacionToDate.Text.Trim();
        }

        private void LoadGridViewData(string Flag)
        {
            US_USUARIOModel UserModel = null;
            UserModel = Session["UserInfo"] as US_USUARIOModel;

            int PageCount = 0;
            DataTable dtProducts = null;

            string program = (this.drpProgram.SelectedIndex == 0 || this.drpProgram.SelectedIndex == -1) ? "" : this.drpProgram.SelectedValue;
            string credit = (this.drpCredit.SelectedIndex == 0 || this.drpCredit.SelectedIndex == -1) ? "" : this.drpCredit.SelectedValue;
            string internalCode = (this.drpInteralCode.SelectedIndex == 0 || this.drpInteralCode.SelectedIndex == -1) ? "" : this.drpInteralCode.SelectedValue;
            string supplierID = (this.drpDistributor.SelectedIndex == 0 || this.drpDistributor.SelectedIndex == -1) ? "" : this.drpDistributor.SelectedValue.Substring(0, this.drpDistributor.SelectedValue.IndexOf('-'));
            string supplierType = "";
            //if (this.drpDistributor.SelectedIndex != 0 && this.drpDistributor.SelectedIndex != -1)
            {
                if (this.drpDistributor.SelectedItem.Text.Contains("(SUPPLIER)"))
                {
                    supplierType = "S";
                }
                else
                {
                    supplierType = "S_B";
                }
            }
            string technology = (this.drpTechnology.SelectedIndex == 0 || this.drpTechnology.SelectedIndex == -1) ? "" : this.drpTechnology.SelectedValue;
            string receiptFromDate = this.txtReceiptFromDate.Text.Trim() == "" ? DateTime.Now.ToString("yyyy-MM-dd") : this.txtReceiptFromDate.Text.Trim();
            string receiptToDate = this.txtReceiptToDate.Text.Trim() == "" ? DateTime.Now.ToString("yyyy-MM-dd") : this.txtReceiptToDate.Text.Trim();
            string inhabilitacionFromDate = this.txtInhabilitacionFromDate.Text.Trim() == "" ? DateTime.Now.ToString("yyyy-MM-dd") : this.txtInhabilitacionFromDate.Text.Trim();
            string inhabilitacionToDate = this.txtInhabilitacionToDate.Text.Trim() == "" ? DateTime.Now.ToString("yyyy-MM-dd") : this.txtInhabilitacionToDate.Text.Trim();
            int disposalID = UserModel.Id_Departamento;
            string disposalType = UserModel.Tipo_Usuario == GlobalVar.DISPOSAL_CENTER ? "M" : "B";

            int pageIndex = 1;
            if (Flag == "P")
            {
                pageIndex = 1;
                this.AspNetPager.CurrentPageIndex = 1;
            }
            else
            {
                pageIndex = this.AspNetPager.CurrentPageIndex;
            }

            int order = K_TECNOLOGIA_RESIDUO_MATERIALDal.ClassInstance.GetOrderByTechnologyAndMaterial(Technology.ToString(), Material.ToString());
            //if (order == 1)
            //{
            //    dtProducts=K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetInhabilitacionProducts(program,credit,internalCode,supplierID,supplierType,technology,receiptFromDate,receiptToDate,
            //                                                                                                                   inhabilitacionFromDate, inhabilitacionToDate, disposalID, disposalType, "", pageIndex, this.AspNetPager.PageSize, out PageCount);
            //}
            //else if (order > 1)
            //{
            //    dtProducts = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetRecoveryProducts(program, credit, internalCode, supplierID, supplierType, technology, receiptFromDate, receiptToDate, inhabilitacionFromDate, inhabilitacionToDate,
            //                                                                                                                Material, order, disposalID, disposalType, "", pageIndex, this.AspNetPager.PageSize, out PageCount);
            //}

            if (dtProducts != null)
            {
                if (dtProducts.Rows.Count == 0)
                {
                    btnRegistry.Enabled = false;
                    dtProducts.Rows.Add(dtProducts.NewRow());
                }
                else
                {
                    btnRegistry.Enabled = true;
                }
                //Bind to grid view
                this.AspNetPager.RecordCount = PageCount;
                this.grdOldEquipment.DataSource = dtProducts;
                this.grdOldEquipment.DataBind();
            }

            RecoveryProducts = dtProducts;

            AspNetPager.Visible = true;
        }

        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                LoadGridViewData("");
                RePopulateSelectedProducts();
            }
        }

        protected void AspNetPager_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            if (IsPostBack)
            {
                RememberOldSelectedProducts();

                this.drpProgram.SelectedIndex = Session["CurrentProgram"] != null ? (int)Session["CurrentProgram"] : 0;
                this.drpCredit.SelectedIndex = Session["CurrentCredit"] != null ? (int)Session["CurrentCredit"] : 0;
                this.drpInteralCode.SelectedIndex = Session["CurrentInternalCode"] != null ? (int)Session["CurrentInternalCode"] : 0;
                this.drpDistributor.SelectedIndex = Session["CurrentSupplier"] != null ? (int)Session["CurrentSupplier"] : 0;
                this.drpTechnology.SelectedIndex = Session["CurrentTechnology"] != null ? (int)Session["CurrentTechnology"] : 0;
                this.txtReceiptFromDate.Text = Session["CurrentReceiptFromDate"] != null ? (string)Session["CurrentReceiptFromDate"] : DateTime.Now.ToString("yyyy-MM-dd");
                this.txtReceiptToDate.Text = Session["CurrentReceiptToDate"] != null ? (string)Session["CurrentReceiptToDate"] : DateTime.Now.ToString("yyyy-MM-dd");
                this.txtInhabilitacionFromDate.Text = Session["CurrentInhabilitacionFromDate"] != null ? (string)Session["CurrentInhabilitacionFromDate"] : DateTime.Now.ToString("yyyy-MM-dd");
                this.txtInhabilitacionToDate.Text = Session["CurrentInhabilitacionToDate"] != null ? (string)Session["CurrentInhabilitacionToDate"] : DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        /// <summary>
        /// remember the selected products
        /// </summary>
        private void RememberOldSelectedProducts()
        {
            int count = 0;
            ArrayList listProducts = new ArrayList();

            foreach (GridViewRow row in grdOldEquipment.Rows)
            {
                if (SelectedProducts == null)
                {
                    SelectedProducts = RecoveryProducts.Clone();
                }

                string productCode = row.Cells[10].Text;
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
                    foreach (DataRow drReceived in RecoveryProducts.Rows)
                    {
                        if (SelectedProducts.Rows.Count > 0)
                        {
                            if (drReceived["Id_Credito_Sustitucion"].ToString() == productCode)
                            {
                                foreach (DataRow drSelected in SelectedProducts.Rows)
                                {
                                    if (drSelected["Id_Credito_Sustitucion"].ToString() != productCode)
                                    {
                                        count++;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                    if (count == SelectedProducts.Rows.Count)
                                    {
                                        SelectedProducts.ImportRow(drReceived);
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        else
                        {
                            SelectedProducts.ImportRow(drReceived);
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
                            if (dr["Id_Credito_Sustitucion"].ToString() == productCode)
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
                foreach (GridViewRow row in grdOldEquipment.Rows)
                {
                    string productCode = row.Cells[10].Text;

                    if (listProducts.Contains(productCode))
                    {
                        CheckBox ckbSelect = (CheckBox)row.FindControl("ckbSelect");

                        ckbSelect.Checked = true;
                    }
                }
            }
        }

        protected void grdOldEquipment_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[11].Visible = false;
            }
        }

        protected void btnRegistry_Click(object sender, EventArgs e)
        {
            RememberOldSelectedProducts();

            if (Session["SelectProducts"] != null && ((ArrayList)Session["SelectProducts"]).Count > 0)
            {
                int result = 0;
                ArrayList listProducts = new ArrayList();
                listProducts = (ArrayList)Session["SelectProducts"];

                result = K_RECUPERACION_PRODUCTOBLL.ClassInstance.Insert_K_RECUPERACION_PRODUCTO(Id_Recuperacion, listProducts);
                if (result > 0)
                {
                    SetOldSelectedProductToEmpty();
                    LoadGridViewData("");
                }
            }
        }

        private void SetOldSelectedProductToEmpty()
        {
            Session["SelectProducts"] = null;
            SelectedProducts = null;
        }

        protected void grdOldEquipment_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow row in this.grdOldEquipment.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox ckbSelect = (CheckBox)row.FindControl("ckbSelect");
                    if (row.Cells[11].Text.Replace("&nbsp;", "") == "")
                    {
                        ckbSelect.Visible = false;
                    }
                }
            }
        }
    }
}
