using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;

namespace PAEEEM.DisposalModule
{
    public partial class ProductListForDisposal : System.Web.UI.Page
    {
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
                if (!string.IsNullOrEmpty(Request.QueryString["BarCode"]))
                {
                    //Init date control
                    this.literalFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    //Load productList gridview
                    string decryptBarcode = System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["BarCode"].ToString().Replace("%2B", "+")));
                    InitialDataGridview(decryptBarcode, "P");
                }
                else
                {
                    Response.Redirect("../Login/Login.aspx");
                    return;
                }
            }
        }
        /// <summary>
        /// Load productList gridview
        /// </summary>
        private void InitialDataGridview(string BarCode, string Flag)
        {
            try
            {
                int PageCount = 0;
                DataTable dtProductList = null;
                if (Flag == "P")
                {
                    dtProductList = K_CREDITO_SUSTITUCIONDAL.ClassInstance.Get_K_CREDITO_SUSTITUCIONForDisposal(BarCode, string.Empty, 1, this.AspNetPager.PageSize, out PageCount);
                }
                else
                {
                    dtProductList = K_CREDITO_SUSTITUCIONDAL.ClassInstance.Get_K_CREDITO_SUSTITUCIONForDisposal(BarCode, string.Empty, this.AspNetPager.CurrentPageIndex, this.AspNetPager.PageSize, out PageCount);
                }
                if (dtProductList != null)
                {
                    if (dtProductList.Rows.Count == 0)
                    {
                        dtProductList.Rows.Add(dtProductList.NewRow());
                    }
                    //Bind to grid view
                    this.AspNetPager.RecordCount = PageCount;
                    this.grdProductList.DataSource = dtProductList;
                    this.grdProductList.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "PageLoad", "alert('Load Gridview Fail:" + ex.Message + "');", true);
            }
        }
        /// <summary>
        /// Refresh Data When Pager Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string decryptBarcode = System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["BarCode"].ToString().Replace("%2B", "+")));
                InitialDataGridview(decryptBarcode, "");
            }
        }
        /// <summary>
        /// Change Product status into Para Inhabilitación
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonList RadioList = (RadioButtonList)sender;
            GridViewRow gridViewRow = (GridViewRow)RadioList.NamingContainer;
            int RowIndex = Convert.ToInt32(gridViewRow.RowIndex.ToString());
            TextBox txtProductPeso = grdProductList.Rows[RowIndex].FindControl("TxtPeso") as TextBox;
            string NoCredit = grdProductList.DataKeys[gridViewRow.RowIndex][0].ToString();
            string Technology = grdProductList.DataKeys[gridViewRow.RowIndex][1].ToString();

            if (RadioList.Items[0].Selected)
            {
                //generate product inner code
                string ProductCode = "";
                //ProductCode = NoCredit.ToString() + "-" + DateTime.Now.Year.ToString().Substring(2, 2) + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + Convert.ToInt32(LsUtility.GetNumberSequence("PRODUCTC")).ToString().PadLeft(4, '0');
                //get Current User DisposalID and UserType
                int DisposalID = 0;
                string userType = "";
                if (null != Session["UserInfo"])
                {
                    DisposalID = ((US_USUARIOModel)Session["UserInfo"]).Id_Departamento;
                    userType = ((US_USUARIOModel)Session["UserInfo"]).Tipo_Usuario;
                }
                if (userType == GlobalVar.DISPOSAL_CENTER)
                {
                    userType = "M";
                }
                else
                {
                    userType = "B";
                }
                int List = 0;

                for (int i = 0; i < grdProductList.Rows.Count; i++)
                {
                    RadioButtonList radiolist = grdProductList.Rows[i].FindControl("RadioButtonList1") as RadioButtonList;
                    if (radiolist.Items[0].Selected)
                    {
                        List = List + 1;
                    }
                }
                ProductCode = K_FOLIODal.ClassInstance.Get_FOLIO_ByDisposalID_And_DisposalType_AndNocredit(DisposalID, userType, NoCredit) +"-"+ List.ToString();
                //update product status to inhabilitación when conformidad selected
                int Result = 0;
                Result = K_CREDITO_SUSTITUCIONDAL.ClassInstance.Update_K_Credit_SustitucionToParaInhabilitación(NoCredit, ProductCode, 1, Technology);
                if (Result > 0)
                {
                    RadioList.Enabled = false;
                    txtProductPeso.Enabled = true;
                }
            }
        }
        /// <summary>
        /// Initial Para Inhabilitación 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdProductList_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i < grdProductList.Rows.Count; i++)
            {
                string FgComformidad = "0";
                FgComformidad = grdProductList.DataKeys[i][2].ToString();
                string ProductPeso = grdProductList.DataKeys[i][3].ToString();
                if (FgComformidad == "1")
                {
                    RadioButtonList RadioButtonListComfrime = grdProductList.Rows[i].FindControl("RadioButtonList1") as RadioButtonList;
                    TextBox txtProductPeso = grdProductList.Rows[i].FindControl("TxtPeso") as TextBox;
                    RadioButtonListComfrime.Items[0].Selected = true;
                    RadioButtonListComfrime.Enabled = false;
                    txtProductPeso.Enabled = true;
                    txtProductPeso.Text = ProductPeso;
                }
            }
        }
        /// <summary>
        /// select only one radiobutton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdProductList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                RadioButton rb = (RadioButton)e.Row.FindControl("RadioButton1");
                if (rb != null)
                {
                    rb.Attributes.Add("onclick", "judge(this)");
                }
            }
        }
        /// <summary>
        /// Edit Product information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string NoCredit = "";
            string Technology = "";
            for (int i = 0; i < grdProductList.Rows.Count; i++)
            {
                bool blIsSelect = ((RadioButton)grdProductList.Rows[i].FindControl("RadioButton1")).Checked;
                if (blIsSelect)
                {
                    NoCredit = grdProductList.DataKeys[i][0].ToString();
                    Technology = grdProductList.DataKeys[i][1].ToString();

                    Response.Redirect("ProductInformationEdit.aspx?NoCredit=" + Convert.ToBase64String(Encoding.Default.GetBytes(NoCredit)).Replace("+", "%2B") + "&Technology=" + Convert.ToBase64String(Encoding.Default.GetBytes(Technology)).Replace("+", "%2B"));
                }
            }
            //display hint information when no record was selected
            ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NoSelected", "alert('No record has been selected.');", true);
        }
        /// <summary>
        /// Upload image for Product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadImage_Click(object sender, EventArgs e)
        {
            string NoCredit = "";
            string Technology = "";
            for (int i = 0; i < grdProductList.Rows.Count; i++)
            {
                bool blIsSelect = ((RadioButton)grdProductList.Rows[i].FindControl("RadioButton1")).Checked;
                if (blIsSelect)
                {
                    NoCredit = grdProductList.DataKeys[i][0].ToString();
                    Technology = grdProductList.DataKeys[i][1].ToString();
                    Response.Redirect("UploadImageForProduct.aspx?NoCredit=" + Convert.ToBase64String(Encoding.Default.GetBytes(NoCredit)).Replace("+", "%2B") + "&Technology=" + Convert.ToBase64String(Encoding.Default.GetBytes(Technology)).Replace("+", "%2B"));
                }
            }
            //display hint information when no record was selected
            ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NoSelected", "alert('No record has been selected.');", true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void TxtPeso_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtProductPeso = (TextBox)sender;
                if (txtProductPeso.Text != "" && IsNumeric(txtProductPeso.Text))
                {
                    GridViewRow gridViewRow = (GridViewRow)txtProductPeso.NamingContainer;
                    string NoCredit = grdProductList.DataKeys[gridViewRow.RowIndex][0].ToString();
                    string Technology = grdProductList.DataKeys[gridViewRow.RowIndex][1].ToString();
                    K_CREDITO_SUSTITUCIONModel SustitucionModel = new K_CREDITO_SUSTITUCIONModel();
                    SustitucionModel.No_Credito = NoCredit;
                    SustitucionModel.Cve_Tecnologia = Technology == "" ? 0 : Convert.ToInt32(Technology);
                    //SustitucionModel.Peso_Producto = txtProductPeso.Text == "" ? 0 : decimal.Parse(txtProductPeso.Text);
                    int iResult = 0;
                    //iResult = K_CREDITO_SUSTITUCIONDAL.ClassInstance.Update_Product_Peso(SustitucionModel);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "UpdatePeso", "alert('Update Product Pesol:" + ex.Message + "');", true);
            }
        }
        /// <summary>
        /// Is not numeric
        /// </summary>
        /// <param name="strTemp"></param>
        /// <returns></returns>
        private Boolean IsNumeric(string strTemp)
        {
            try
            {
                if (decimal.Parse(strTemp) >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
