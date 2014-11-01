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
    public partial class ProductMaterialRegister : System.Web.UI.Page
    {
        #region Global Variables
        /// <summary>
        /// property
        /// </summary>
        private string CreditNo
        {
            get
            {
                return ViewState["CreditNo"] == null ? "" : ViewState["CreditNo"].ToString();
            }
            set
            {
                ViewState["CreditNo"] = value;
            }
        }

        private int TechnologyID
        {
            get
            {
                return ViewState["TechnologyID"] == null ? 0 : Convert.ToInt32(ViewState["TechnologyID"].ToString());
            }
            set
            {
                ViewState["TechnologyID"] = value;
            }
        }
        #endregion

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
                //Check is edit or new
                if (Request.QueryString["CreditNo"] != null && Request.QueryString["CreditNo"].ToString() != "")
                {
                    CreditNo = System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["CreditNo"].ToString().Replace("%2B", "+")));
                    TechnologyID = Convert.ToInt32(System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["TechnologyID"].ToString().Replace("%2B", "+"))));

                    //Init default data
                    InitDefaultData();

                    // Init setting
                    btnSave.Enabled = false;
                }
            }
        }

        private void InitDefaultData()
        {
            // Init program control
            DataTable dtProgram = null;
            dtProgram = CAT_PROGRAMADal.ClassInstance.Get_PROGRAMAByCreditNo(CreditNo);
            if (dtProgram != null && dtProgram.Rows.Count > 0)
            {
                txtProgram.Text = dtProgram.Rows[0]["Dx_Nombre_Programa"].ToString();
            }
            // Init technology control
            CAT_TECNOLOGIAModel modelTechnology = new CAT_TECNOLOGIAModel();
            modelTechnology = CAT_TECNOLOGIADAL.ClassInstance.Get_CAT_TECNOLOGIAByPKID(TechnologyID.ToString());
            txtTechnology.Text = modelTechnology.Dx_Nombre_General;

            // Init material data
            LoadGridViewData();
        }

        private void LoadGridViewData()
        {
            DataTable dtMaterial = null;

            dtMaterial = K_TECNOLOGIA_MATERIALDAL.ClassInstance.GetMaterialByTechnology(TechnologyID);
            if (dtMaterial != null)
            {
                if (dtMaterial.Rows.Count == 0)
                {
                    dtMaterial.Rows.Add(dtMaterial.NewRow());
                }
                //Bind to grid view
                this.grdMaterial.DataSource = dtMaterial;
                this.grdMaterial.DataBind();
            }
        }

        protected void grdMaterial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[5].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox ckbSelect = (CheckBox)e.Row.FindControl("ckbSelect");
                TextBox txtWeight = (TextBox)e.Row.FindControl("txtWeight");

                if (e.Row.Cells[0].Text.Replace("&nbsp;", "") == "" && e.Row.Cells[3].Text.Replace("&nbsp;", "") == "")
                {
                    ckbSelect.Visible = false;
                    txtWeight.Visible = false;
                }
                else
                {
                    if (e.Row.RowIndex == 0)
                    {
                        ckbSelect.Enabled = true;
                        txtWeight.Enabled = false;
                    }
                    else
                    {
                        ckbSelect.Enabled = false;
                        txtWeight.Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// Load material data
        /// </summary>
        /// <param name="listMaterial"></param>
        private void LoadMaterialDataFromUI(out List<K_RECUP_RESIDUOSModel> listMaterial)
        {
            listMaterial = new List<K_RECUP_RESIDUOSModel>();

            // get product data
            DataTable dtProduct = K_CREDITO_SUSTITUCIONDAL.ClassInstance.Get_K_CREDITO_SUSTITUCIONByCreditoAndTechnology(CreditNo, TechnologyID);

            // load material data
            foreach (GridViewRow row in grdMaterial.Rows)
            {
                CheckBox ckbSelect = (CheckBox)row.FindControl("ckbSelect");
                TextBox txtWeight = (TextBox)row.FindControl("txtWeight");
                if (ckbSelect.Checked)
                {
                    K_RECUP_RESIDUOSModel model = new K_RECUP_RESIDUOSModel();
                    model.Codigo = dtProduct.Rows[0]["Codigo"] == DBNull.Value ? "" : dtProduct.Rows[0]["Codigo"].ToString();
                    model.No_Credito = CreditNo;
                    if (dtProduct.Rows[0]["Id_Centro_Disp"] != DBNull.Value)
                    {
                        model.ID_Centro_Disp = Convert.ToInt32(dtProduct.Rows[0]["Id_Centro_Disp"].ToString());
                    }
                    model.Codigo_Producto = dtProduct.Rows[0]["Cod_Producto"] == DBNull.Value ? "" : dtProduct.Rows[0]["Cod_Producto"].ToString();
                    model.Cve_Material = Convert.ToInt32(row.Cells[5].Text);
                    if (txtWeight.Text.Trim() != "")
                    {
                        model.Valor_Material = Convert.ToDecimal(txtWeight.Text.Trim());
                    }
                    model.Fg_Incluido = "N";
                    model.Fg_Tipo_Centro_Disp = dtProduct.Rows[0]["Fg_Tipo_Centro_Disp"] == DBNull.Value ? "" : dtProduct.Rows[0]["Fg_Tipo_Centro_Disp"].ToString();
                    model.Dt_Fecha_Creacion = DateTime.Now;
                    model.ID_Estatus = (int)DisposalStatus.RECUPERACIONDERESIDUOS;

                    listMaterial.Add(model);
                }
            }
        }

        /// <summary>
        /// enable the textbox of the weight and the checkbox of the next row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckbSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckbSelect = null;
            ckbSelect = (CheckBox)sender;
            if (ckbSelect.Checked)
            {
                ckbSelect.Enabled = false;

                GridViewRow row = (GridViewRow)ckbSelect.NamingContainer;

                // enable the textbox of the weight in the current row
                TextBox txtWeight = (TextBox)row.FindControl("txtWeight");
                txtWeight.Enabled = true;

                // enable the checkbox of the next row
                int nextRowIndex = row.RowIndex + 1;
                if (nextRowIndex < grdMaterial.Rows.Count)
                {
                    ckbSelect = (CheckBox)grdMaterial.Rows[nextRowIndex].FindControl("ckbSelect");
                    ckbSelect.Enabled = true;
                }
                else
                {
                    btnSave.Enabled = true;
                }
            }
        }

        /// <summary>
        /// save data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate("weight");
            if (!Page.IsValid)
            {
                return;
            }

            int result = 0;
            List<K_RECUP_RESIDUOSModel> listMaterial = new List<K_RECUP_RESIDUOSModel>();
            LoadMaterialDataFromUI(out listMaterial);

            if (listMaterial.Count == grdMaterial.Rows.Count)
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    // save material data
                    result = K_RECUP_RESIDUOSBLL.ClassInstance.Insert_K_RECUP_RESIDUOS(listMaterial);

                    // update disposed status
                    result += K_CREDITO_SUSTITUCIONDAL.ClassInstance.UpdateK_CREDITO_SUSTITUCIONEstatusToRecuperacion(CreditNo, TechnologyID, DateTime.Now);
                    scope.Complete();
                }

                if (result > 0)
                {
                    Response.Redirect("DisposedProductsMonitor.aspx");
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DisposedProductsMonitor.aspx");
        }
    }
}
