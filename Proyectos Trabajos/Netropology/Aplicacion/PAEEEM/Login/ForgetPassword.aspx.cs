using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Mail;
using System.Text;
using System.IO;
using PAEEEM.BussinessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using PAEEEM.LogicaNegocios.AdminUsuarios;

namespace PAEEEM
{
    public partial class ForgetPassword : System.Web.UI.Page
    {
        //private static int DEFAULT_PASSWORD_LENGTH = 8;

        /// <summary>
        /// Submit password changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            string Message = (string)GetGlobalResourceObject("DefaultResource", "msgUserNameInvalid");
            string Password, Address;

            if (IsUserExist(txtUserName.Text.Trim(), out Password, out Address))
            {
                var decryptPassword = ValidacionesUsuario.Desencriptar(Password);
                MailUtility.PasswordChangeEmail(this.txtUserName.Text, Address, decryptPassword);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "userNotExists", "alert('"+Message+"');", true);
                return;
            }
        }
        /// <summary>
        /// Validate user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private bool IsUserExist(string userName, out string password, out string address)
        {
            return  US_USUARIOBll.ClassInstance.ValidationUserName(userName, out password, out address);
        }
        /// <summary>
        /// Return to login page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}
