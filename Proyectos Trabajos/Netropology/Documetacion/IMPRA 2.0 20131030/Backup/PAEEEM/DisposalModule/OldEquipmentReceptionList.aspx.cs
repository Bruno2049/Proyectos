using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.Helpers;
using PAEEEM.Entities;
using PAEEEM.DataAccessLayer;
using PAEEEM.BussinessLayer;//added by tina 2012-07-12

namespace PAEEEM.DisposalModule
{
    public partial class OldEquipmentReceptionList : System.Web.UI.Page
    {
        #region Global Varliable
        private string BarCode
        {
            get 
            { 
                return ViewState["BarCode"] == null ? "" : ViewState["BarCode"].ToString();
            }
            set
            {
                ViewState["BarCode"] = value;
            }
        }
        #endregion
        /// <summary>
        /// Load the default data when page was first loaded
        /// </summary>
        /// <param name="sender">Event Raise Target Object</param>
        /// <param name="e">Event Argument</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (null == Session["UserInfo"])
                    {
                        Response.Redirect("../Login/Login.aspx");
                        return;
                    }

                    //Init right-upper corner date control
                    literalFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");

                    //Get request query string
                    if (!string.IsNullOrEmpty(Request.QueryString["BarCode"]))
                    {
                        BarCode = System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["BarCode"].ToString().Replace("%2B", "+")));
                        //Load grid view data with the barcode string
                        InitGridView(BarCode);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "LoadException", "alert('" + ex.Message + "');", true);
            }
        }
        /// <summary>
        /// Init grid view during the page first load
        /// </summary>
        /// <param name="Barcode">barcode number</param>
        private void InitGridView(string barcode)
        {
            try
            {
                US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;                

                if (UserModel != null)
                {
                    string DisposalCenterNumber = UserModel.Id_Departamento.ToString();
                    string UserType = UserModel.Tipo_Usuario;
                    string DisposalCenterType = "";

                    //determine the disposal center type by the disposal center user type
                    if (UserType == GlobalVar.DISPOSAL_CENTER)
                    {
                        DisposalCenterType = "M";
                    }
                    else if (UserType == GlobalVar.DISPOSAL_CENTER_BRANCH)
                    {
                        DisposalCenterType = "B";
                    }
                    
                    //obtain old products list related with the specific credit request
                    int PageCount = 0;
                    DataTable OldProducts = null;
                    bool emptyRow = false;

                    OldProducts = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetOldEquipmentsReceivedList(barcode,
                                                                    DisposalCenterNumber, DisposalCenterType, this.AspNetPager.CurrentPageIndex, this.AspNetPager.PageSize, out PageCount);
                    
                    if (OldProducts != null)
                    {
                        if (OldProducts.Rows.Count == 0)
                        {
                            emptyRow = true;
                            OldProducts.Rows.Add(OldProducts.NewRow());
                        }
                        else  //visible Print button
                        {
                            int ResultCount = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetCountDtFeachReciveptionIsnull(DisposalCenterNumber, DisposalCenterType, barcode);
                            if (ResultCount == 0)
                            {
                                btnPrint.Visible = true;
                            }
                        }
                        //Bind to grid view
                        this.AspNetPager.RecordCount = PageCount;
                        this.grdProductList.DataSource = OldProducts;
                        this.grdProductList.DataBind();
                    }                 
                    
                    //hide the radio button when the row is empty
                    if (emptyRow)
                    {
                        RadioButton radioButton = grdProductList.Rows[0].FindControl("RadioButton1") as RadioButton;
                        radioButton.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "GridViewInitError", 
                                                                        "alert('Excepción que se produce durante la vista de cuadrícula de inicialización:" + ex.Message + "');", true);
            }
        }
        /// <summary>
        /// Register old product reception
        /// </summary>
        /// <param name="sender">Event Raise Target Object</param>
        /// <param name="e">Event Argument</param>
        protected void btnReception_Click(object sender, EventArgs e)
        {
            string CreditSusID;

            if (IsSelected(out CreditSusID))
            {
                if (IsReceived(CreditSusID))//had Reception display warning message
                {
                    ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "Warning", "alert('¡ El Equipo seleccionado ya cuenta con el Registro de Recepción correspondiente !');", true);
                }
                else//Display Confirm Reception Information
                {
                    //update by tina 2012-07-12
                    US_USUARIOModel userModel = (US_USUARIOModel)Session["UserInfo"];
                    if (userModel != null)
                    {
                        int disposalID = userModel.Id_Departamento;
                        string userType = "";
                        if (userModel.Tipo_Usuario == GlobalVar.DISPOSAL_CENTER)
                        {
                            userType = "M";
                        }
                        else
                        {
                            userType = "B";
                        }
                        bool isAllowReceipt = K_CREDITO_SUSTITUCIONBLL.ClassInstance.IsAllowReceiptOldEquipment(disposalID, userType, CreditSusID);
                        if (isAllowReceipt)
                        {
                            ScriptManager.RegisterStartupScript(this.panel, typeof(Page), "ConfirmInfo", "check();", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "NotAllowReceipt", "alert('Por favor, primero desactive el equipo durante 3 días!');", true);
                        }
                    }
                    //end                    
                }
            }
            else//show message if none was selected
            { 
                ScriptManager.RegisterStartupScript(this, typeof(Page), "NoSelectionWarning", "alert('Por favor, hacer una selección para continuar!');", true);
            }
        }

        private bool IsSelected(out string susCreditNumber)
        {
            bool isSelected = false;
            susCreditNumber = "";

            for (int i = 0; i < grdProductList.Rows.Count; i++)
            {
                //get if the check box is checked
                isSelected = ((RadioButton)grdProductList.Rows[i].FindControl("RadioButton1")).Checked;
                if (isSelected)
                {
                    susCreditNumber = grdProductList.DataKeys[i][0].ToString();
                    break;
                }
            }

            return isSelected;
        }

        protected void hidConfirm_Click(object sender, EventArgs e)
        {
            string CreditsusID = "";

            for (int i = 0; i < grdProductList.Rows.Count; i++)
            {
                bool blIsSelect = ((RadioButton)grdProductList.Rows[i].FindControl("RadioButton1")).Checked;

                if (blIsSelect)
                {
                    CreditsusID = grdProductList.DataKeys[i][0].ToString();
                    Response.Redirect("ReceiptionOldEquipmentRegistry.aspx?CreditSusID=" + 
                                                        Convert.ToBase64String(Encoding.Default.GetBytes(CreditsusID)).Replace("+", "%2B"));
                }
            }
        }
        /// <summary>
        /// Check whether the Old product is received 
        /// </summary>
        /// <param name="CreditSusID">substitution record number</param>
        /// <returns></returns>
        private Boolean IsReceived(string CreditSusID)
        {
            int Count = 0;
            bool Received = false;

            Count = K_CREDITO_SUSTITUCIONDAL.ClassInstance.IsOldProductReceived(CreditSusID);
            if (Count > 0)
            {
                Received = true;
            }

            return Received;
        }
        /// <summary>
        /// RowDataBound
        /// </summary>
        /// <param name="sender">Event Raise Target Object</param>
        /// <param name="e">Event Argument</param>
        protected void grdProductList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                RadioButton radioButton = (RadioButton)e.Row.FindControl("RadioButton1");
                if (radioButton != null)
                {
                    radioButton.Attributes.Add("onclick", "judge(this)");
                }
                string IsRegister = grdProductList.DataKeys[e.Row.RowIndex][1].ToString();

                Image Img = e.Row.FindControl("imgRegister") as Image;             

                if (IsRegister == "0")//Old Product is not Register
                {
                    Img.ImageUrl = "~/DisposalModule/images/Image2.png";
                }
                else if (IsRegister == "1")
                {
                    Img.ImageUrl = "~/DisposalModule/images/Image1.png";                 
                }
            }
        }
        /// <summary>
        /// Changed page 
        /// </summary>
        /// <param name="sender">Event Raise Target Object</param>
        /// <param name="e">Event Argument</param>
        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string decryptBarcode = System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["BarCode"].ToString().Replace("%2B", "+")));
                InitGridView(decryptBarcode);
            }
        }
        /// <summary>
        /// Print old product reception report
        /// </summary>
        /// <param name="sender">Event Raise Target Object</param>
        /// <param name="e">Event Argument</param>
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=Boleta_EquipoBajaEficiencia&CreditNumber=" + BarCode + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }
    }
}
