using System;
using System.Globalization;
using System.Web;
using System.Web.UI.WebControls;
using PAEEEM.BussinessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using PAEEEM.LogicaNegocios.AdminUsuarios;

namespace PAEEEM.Login
{
    public partial class Login : System.Web.UI.Page
    {
        /// <summary>
        /// Property User ID
        /// </summary>
        public string UserId
        {
            get
            {
                return ViewState["UserId"] == null ? string.Empty : ViewState["UserId"].ToString();
            }
            set
            {
                ViewState["UserId"] = value;
            }
        }
        /// <summary>
        /// Check if had login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (Session["UserInfo"] != null)
            {
                Response.Redirect("../default.aspx");
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
                var intUserId = AuthenticationMethod(ctlLogin.UserName, ctlLogin.Password);

                if (intUserId > 0)
                {
                    var lUser = US_USUARIOBll.ClassInstance.Get_US_USUARIOByPKID(intUserId.ToString(CultureInfo.InvariantCulture));
                   
                    //Cache user information for later use
                    Session["UserName"] = ctlLogin.UserName;
                    Session["UserInfo"] = lUser;
                    Session["IdUserLogueado"] = lUser.Id_Usuario;
                    Session["IdRolUserLogueado"] = lUser.Id_Rol;
                    Session["IdDepartamento"] = lUser.Id_Departamento;
                    
                    //Check user status, if is not Active, show message for user
                    if (lUser.Estatus.Trim() != "A")
                    {
                        if (!lUser.Estatus.Trim().Equals("P"))
                        {
                            ctlLogin.FailureText =
                                (string) GetGlobalResourceObject("DefaultResource", "msgUserIsInactiveOrCanceled");
                            return;
                        }
                        ctlLogin.FailureText =
                            (string) GetGlobalResourceObject("DefaultResource", "msgUserIsPending");
                        return;
                    }
                    
                    //Cache login user name to prevent repeat login
                    var sKey = ctlLogin.UserName.Trim();

                    if (HttpContext.Current.Cache[sKey] == null || HttpContext.Current.Cache[sKey].ToString() == String.Empty)
                    {                   
                        var sessTimeOut = new TimeSpan(0, 0, HttpContext.Current.Session.Timeout, 0, 0);
                        HttpContext.Current.Cache.Insert(sKey, sKey, null, DateTime.MaxValue, sessTimeOut, System.Web.Caching.CacheItemPriority.NotRemovable, null);
                        e.Authenticated = true;

                        Response.Redirect("../Default.aspx");
                    }
                    else if (HttpContext.Current.Cache[sKey].ToString().Trim() == sKey)
                    {
                        ctlLogin.FailureText = (string)GetGlobalResourceObject("DefaultResource", "msgRepeatLogin");
                    }
                    else
                    {
                        Session.Abandon();
                    } 
                }
                else
                {
                    ctlLogin.FailureText = (string)GetGlobalResourceObject("DefaultResource", "msgInvalidUserAccount");
                }

            }
            catch (Exception ex)
            {
                ctlLogin.FailureText = (string)GetGlobalResourceObject("DefaultResource", "CaptchaFatal");
            }
        }
        /// <summary>
        /// Authenticate
        /// </summary>
        /// <param name="strUserName">User Name</param>
        /// <param name="strPassword">Password</param>
        /// <returns></returns>
        private static int AuthenticationMethod(string strUserName, string strPassword)
        {

            var encryptPassword = ValidacionesUsuario.Encriptar(strPassword);

            var userEntity = new US_USUARIOModel
            {
                Nombre_Usuario = strUserName,
                Contrasena = encryptPassword,
                Estatus = GlobalVar.STATUS_USER_ACTIVE
            };

            var intUserId = US_USUARIOBll.ClassInstance.AuthenticationUser(userEntity);

            return intUserId;
        }
    }
}
