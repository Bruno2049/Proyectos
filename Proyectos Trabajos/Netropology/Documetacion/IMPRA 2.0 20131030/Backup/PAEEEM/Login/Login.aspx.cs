using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using PAEEEM.BussinessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;

namespace PAEEEM
{
    public partial class Login : System.Web.UI.Page
    {
        /// <summary>
        /// Property User ID
        /// </summary>
        public string UserID
        {
            get
            {
                return ViewState["UserID"] == null ? string.Empty : ViewState["UserID"].ToString();
            }
            set
            {
                ViewState["UserID"] = value;
            }
        }
        /// <summary>
        /// Check if had login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserInfo"] != null)
                {
                    Response.Redirect("../default.aspx");
                }
            }
        }
        /// <summary>
        /// Login submit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ctlLogin_Authenticate(object sender, AuthenticateEventArgs e)
        {
            try
            {
                int intUserID = AuthenticationMethod(this.ctlLogin.UserName, this.ctlLogin.Password);

                if (intUserID > 0)
                {
                    US_USUARIOModel  lUser =  US_USUARIOBll.ClassInstance.Get_US_USUARIOByPKID(intUserID.ToString());
                    //Cache user information for later use
                    Session["UserName"] = this.ctlLogin.UserName;
                    Session["UserInfo"] = lUser;
                    
                    //Check user status, if is not Active, show message for user
                    if (lUser.Estatus.Trim() != "A")
                    {
                        if (lUser.Estatus.Trim().Equals("P"))//Show pending message
                        {
                            this.ctlLogin.FailureText = (string)GetGlobalResourceObject("DefaultResource", "msgUserIsPending");
                            return;
                        }
                        else
                        {
                            this.ctlLogin.FailureText = (string)GetGlobalResourceObject("DefaultResource", "msgUserIsInactiveOrCanceled");
                            return;
                        }                   
                    }
                    
                    //Cache login user name to prevent repeat login
                    string sKey = this.ctlLogin.UserName.Trim();

                    if (HttpContext.Current.Cache[sKey] == null || HttpContext.Current.Cache[sKey].ToString() == String.Empty)
                    {                   
                        TimeSpan SessTimeOut = new TimeSpan(0, 0, HttpContext.Current.Session.Timeout, 0, 0);
                        HttpContext.Current.Cache.Insert(sKey, sKey, null, DateTime.MaxValue, SessTimeOut, System.Web.Caching.CacheItemPriority.NotRemovable, null);
                        e.Authenticated = true;

                        Response.Redirect("../Default.aspx");
                    }
                    else if (HttpContext.Current.Cache[sKey].ToString().Trim() == sKey)
                    {
                        this.ctlLogin.FailureText = (string)GetGlobalResourceObject("DefaultResource", "msgRepeatLogin");
                        return;
                    }
                    else
                    {
                        Session.Abandon();
                    } 
                }
                else
                {
                    this.ctlLogin.FailureText = (string)GetGlobalResourceObject("DefaultResource", "msgInvalidUserAccount");
                }

            }
            catch (Exception ex)
            {
                this.ctlLogin.FailureText = (string)GetGlobalResourceObject("DefaultResource", "CaptchaFatal");
            }
        }
        /// <summary>
        /// Authenticate
        /// </summary>
        /// <param name="strUserName">User Name</param>
        /// <param name="strPassword">Password</param>
        /// <returns></returns>
        private int AuthenticationMethod(string strUserName, string strPassword)
        {
            int intUserID = 0;

            US_USUARIOModel userEntity = new US_USUARIOModel();
            userEntity.Nombre_Usuario = strUserName;
            userEntity.Contrasena = strPassword;
            userEntity.Estatus = GlobalVar.STATUS_USER_ACTIVE;

            intUserID = US_USUARIOBll.ClassInstance.AuthenticationUser(userEntity);

            return intUserID;
        }
    }
}
