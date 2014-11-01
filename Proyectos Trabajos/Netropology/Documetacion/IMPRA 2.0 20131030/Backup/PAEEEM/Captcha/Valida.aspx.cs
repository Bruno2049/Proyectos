using System;
using System.Web;
using PAEEEM.Entities;

namespace PAEEEM.Captcha
{
    public partial class Valida : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblCheckResult.Text = "";
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            string error = "";
            string estado = "";

            try
            {
                if (MyCaptcha1.IsValid)
                {
                    CodeValidator validator = new CodeValidator();

                    //lblCheckResult.Text = "El código es correcto";
                    if (validator.ValidateServiceCode(this.txtServiceCode.Text, out error, ref estado))
                    {
                        lblCheckResult.Text = string.Format(
                            HttpContext.GetGlobalResourceObject("DefaultResource", "CaptchaResultRPUValid") as string
                            , this.txtServiceCode.Text);
                                          
                        US_USUARIOModel UserModel = (US_USUARIOModel)Session["UserInfo"];
                        if (UserModel != null && UserModel.Id_Rol == 3) // distribuidor
                        {
                            Session["ValidRPU"] = this.txtServiceCode.Text;
                            Response.Redirect("../SupplierModule/CreditRequest.aspx");
                        }
                    }
                    else
                    {
                        lblCheckResult.Text = string.Format(HttpContext.GetGlobalResourceObject("DefaultResource", "CaptchaResultRPUInvalid") as string
                            , this.txtServiceCode.Text, error);
                    }

                    // Generar nueva imagen para que no pueda ser reutilizada
                    MyCaptcha1.TryNew();

                    // limpiar formulario?
                    this.txtServiceCode.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "CaptchaClear") as string;
                }
                else
                    lblCheckResult.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "CaptchaResultTextInvalid") as string;
            }
            catch (Exception ex)
            {
                lblCheckResult.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "CaptchaFatal") as string;
            }
        }
    }
}