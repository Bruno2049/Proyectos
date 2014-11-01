using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using PAEEEM.BussinessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using PAEEEM.LogicaNegocios.AdminUsuarios;


namespace PAEEEM
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        private string strUserName;
        private string strPassword;
        
        /// <summary>
        /// Reset
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtUserName.Text = "";
            txtOldPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
        }
        /// <summary>
        /// Submit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string strMessage;
            strUserName = txtUserName.Text;
            strPassword = txtOldPassword.Text;

            if (txtNewPassword.Text == txtConfirmPassword.Text)
            {
                if (AuthenticationMethod(strUserName, strPassword))
                {
                    int iCount = 0;
                    iCount = US_USUARIOBll.ClassInstance.UpdatePassword(GetEntityFromUI());
                    if (iCount > 0)
                    {
                        strMessage = (string)GetGlobalResourceObject("DefaultResource","msgChangePasswordSuccess");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "confirm('"+strMessage+"');", true);
                    }
                    else
                    {
                        strMessage = (string)GetGlobalResourceObject("DefaultResource", "msgChangePasswordFailure");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "confirm('"+strMessage+"');", true);
                    }
                }
                else 
                {
                    strMessage = (string)GetGlobalResourceObject("DefaultResource", "msgAccountInvalid");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "sudcess", "alert('"+strMessage+"');", true);
                }
            }
        }
        /// <summary>
        /// Authenticate
        /// </summary>
        /// <param name="strUserName"></param>
        /// <param name="strPassword"></param>
        /// <returns></returns>
        private bool AuthenticationMethod(string strUserName, string strPassword)
        {
            var encryptPassword = ValidacionesUsuario.Encriptar(strPassword);
            bool boolReturnValue = false;
            US_USUARIOModel user = new US_USUARIOModel();
            user.Nombre_Usuario = strUserName;
            user.Contrasena = encryptPassword;
            //user.Contrasena = strPassword;
            int result= US_USUARIOBll.ClassInstance.AuthenticationUser(user);
            if (result > 0)
            {
                boolReturnValue = true;
            }
            return boolReturnValue;            
        }

        private US_USUARIOModel GetEntityFromUI()
        {
            var encryptPassword = ValidacionesUsuario.Encriptar(txtNewPassword.Text);

            US_USUARIOModel umUser = new US_USUARIOModel();
            umUser.Nombre_Usuario = strUserName;
            umUser.Contrasena = encryptPassword;
            //umUser.Contrasena = txtNewPassword.Text;
            return umUser;
        }
        /// <summary>
        /// return login lin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Login/Login.aspx");
        }
    }
}
