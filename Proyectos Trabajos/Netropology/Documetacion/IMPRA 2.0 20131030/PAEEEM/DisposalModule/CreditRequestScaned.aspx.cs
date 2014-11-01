using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.DataAccessLayer;
using PAEEEM.BussinessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using System.Text;

namespace PAEEEM.DisposalModule
{
    public partial class CreditRequestScaned : System.Web.UI.Page
    {
        #region Page Events
        /// <summary>
        /// Init Default Data When page Load
        /// </summary>
        /// <param name="sender">Event Raise Target Object</param>
        /// <param name="e">Event Argument</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)//Check the session and Init date control for the first time
            {
                if (null == Session["UserInfo"])
                {
                    Response.Redirect("../Login/Login.aspx");
                    return;
                }

                //Init date control using current date
                this.txtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");

                this.txtBarCode.Focus();
            }
        }
        #endregion

        #region Button Action
        /// <summary>
        /// Validate the scanned or inputted bar code number when the validate button was clicked
        /// </summary>
        /// <param name="sender">Event Raise Target Object</param>
        /// <param name="e">Event Argument</param>
        protected void btnValidate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtBarCode.Text))
            {
                if (ValidateBarCode())
                {
                    ////edit by coco 20120224
                    if (isCompleteRegister())
                    {
                        ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(Page), "ConfirmInfo", "ContinueOrCancel();", true);
                    }
                    else
                    {
                        RedirectToOldEquipmentListPage();
                    }
                    //end edit
                }
                else//Clear the entry control, and display the warning message
                {
                    InValidationBarCode();
                }
            }
        }

        private void InValidationBarCode()
        {
            txtBarCode.Text = "";
            this.txtBarCode.Focus();
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, typeof(Page),
                                "Warning", "alert('El Número de Crédito ingresado No es válido o se encuentra Cancelado. Por favor, ingrese un Número de Crédito Válido');", true);
        }

        private void RedirectToOldEquipmentListPage()
        {
            Response.Redirect("OldEquipmentReceptionList.aspx?BarCode=" +
                                            Convert.ToBase64String(Encoding.Default.GetBytes(txtBarCode.Text)).Replace("+", "%2B"));
        }
        #endregion

        #region Validation
        //add by coco 20120224
        private bool isCompleteRegister()
        {
            bool result = false;
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
                int ResultCount = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetCountDtFeachReciveptionIsnull(DisposalCenterNumber, DisposalCenterType, txtBarCode.Text.Trim());
                if (ResultCount == 0)
                {
                    result = true;
                }
               
            }
            return result;
        }

        protected void hidConfirm_Click(object sender, EventArgs e)
        {
            RedirectToOldEquipmentListPage();
        }
        //end add
        /// <summary>
        /// Barcode validation logic
        /// </summary>
        /// <returns>Valid: return true; Invalid: return false; Default: false</returns>
        private bool ValidateBarCode()
        {
            Boolean IsValid = false;
            string BarCodeNumber = this.txtBarCode.Text.Trim();

            try
            {
                if (IsCreditInCorrectStatus(BarCodeNumber))
                {
                    if (IsDisposalCenterRelated(BarCodeNumber))
                    {
                        IsValid = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, typeof(Page), "ExceptionMessage",
                                                    "alert('Excepción tiré durante la validación:" + ex.Message + "');", true);
            }

            return IsValid;
        }

        private bool IsCreditInCorrectStatus(string barCode)
        {
            try
            {

                int Count = K_CREDITODal.ClassInstance.IsCreditInValidStatus(barCode);

                if (Count > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }

        private bool IsDisposalCenterRelated(string barCode)
        {
            int Count = 0;
            int DisposalCenterNumber = 0;
            string UserType = "", DisposalCenterType = "";

            try
            {
                if (null != Session["UserInfo"])
                {
                    //Get disposal center number and login user type
                    DisposalCenterNumber = ((US_USUARIOModel)Session["UserInfo"]).Id_Departamento;
                    UserType = ((US_USUARIOModel)Session["UserInfo"]).Tipo_Usuario;
                }

                if (UserType == GlobalVar.DISPOSAL_CENTER)//Disposal center main station
                {
                    DisposalCenterType = "M";
                }
                else//Disposal center branch
                {
                    DisposalCenterType = "B";
                }
                //get record count satisfying the conditions: barcode string, disposal center number and disposal center type
                Count = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetRecordCount(barCode, DisposalCenterNumber, DisposalCenterType);

                if (Count > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }
        #endregion
    }
}
