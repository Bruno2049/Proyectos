using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using eClockMobile.Models;

namespace eClockMobile.Controllers
{

    [Authorize]
    public class AccountController : AsyncController 
    {
        //
        // GET: /Account/Index

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string id)
        {
            if(id == null || id == "")
                id = "Iniciar sesión";
            ViewBag.Mensaje = id;
            return View();
        }

        //
        // POST: /Account/Login

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void LoginAsync(LoginModel model, string returnUrl)
        {
            AsyncManager.Parameters["model"] = model;
            if (ModelState.IsValid)
            {
                AsyncManager.OutstandingOperations.Increment();
                eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
                eClockBase.Controladores.Sesion cSesion = new eClockBase.Controladores.Sesion(Sesion);

                cSesion.LogeoFinalizado += delegate(object sender, EventArgs e)
                {
                    AsyncManager.Parameters["EstaLogeado"] = Sesion.EstaLogeado();
                    AsyncManager.Parameters["SESION_SEGURIDAD"] = Sesion.SESION_SEGURIDAD;
                    AsyncManager.Parameters["returnUrl"] = returnUrl;
                    AsyncManager.Parameters["Recordar"] = Sesion.MantenerSesion;
                    AsyncManager.OutstandingOperations.Decrement();
                };
                cSesion.CreaSesion_InicioAdv(model.UserName, model.Password, model.RememberMe);

            }

        }

        void cSesion_LogeoFinalizado(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public ActionResult LoginCompleted(LoginModel model, bool EstaLogeado, bool Recordar, string SESION_SEGURIDAD, string returnUrl)
        {
            if (EstaLogeado)
            {
                
//                eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
                FormsAuthentication.SetAuthCookie(SESION_SEGURIDAD, Recordar);
                //FormsAuthentication.SetAuthCookie(model.UserName, Recordar);
                //FormsAuthentication.GetAuthCookie(
                //HttpCookie authCookie = FormsAuthentication.GetAuthCookie(model.UserName, Recordar);
                //authCookie.Value = SESION_SEGURIDAD;
                //authCookie.Values.Add("SESION_SEGURIDAD", SESION_SEGURIDAD);
                //FormsAuthentication.Decrypt(
                //authCookie.
                /*FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                      model.UserName,
                      DateTime.Now,
                      DateTime.Now.AddDays(30),
                      Recordar,
                      SESION_SEGURIDAD,
                      FormsAuthentication.FormsCookiePath);
                string encTicket = FormsAuthentication.Encrypt(ticket);
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));*/
                if (Recordar)
                {
                    HttpCookie SS = new HttpCookie("SESION_SEGURIDAD", SESION_SEGURIDAD);
                    SS.Expires = DateTime.Now.AddDays(30);
                    Response.SetCookie(SS);
                }
                else
                {
                    HttpCookie SS = new HttpCookie("SESION_SEGURIDAD", null);
                    SS.Expires = DateTime.Now;
                    Response.SetCookie(SS);

                }
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    if (!BaseModificada.CeC_Sesion.EsKiosco)
                        return Redirect("/Home/Index");
                    else
                        return RedirectToAction("Kiosco", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "El nombre de usuario o la contraseña especificados son incorrectos.");
            }
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Sesion cSesion = new eClockBase.Controladores.Sesion(Sesion);
            cSesion.CerrarSesion();

            HttpCookie SS = new HttpCookie("SESION_SEGURIDAD", null);
            SS.Expires = DateTime.Now;
            Response.SetCookie(SS);

            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {

                // Intento de registrar al usuario
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, passwordQuestion: null, passwordAnswer: null, isApproved: true, providerUserKey: null, status: out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, createPersistentCookie: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void ChangePasswordAsync(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                AsyncManager.OutstandingOperations.Increment();
                eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
                eClockBase.Controladores.Usuarios cUsuarios = new eClockBase.Controladores.Usuarios(Sesion);
                cUsuarios.CambioPasswordEvent+=delegate(bool Resultado)
                    {
                        AsyncManager.Parameters["Cambiado"] = Resultado;
                        AsyncManager.OutstandingOperations.Decrement();
                    };
                cUsuarios.CambioPassword(model.OldPassword, model.NewPassword);
                

            }

        }


        public ActionResult ChangePasswordCompleted(bool Cambiado)
        {
            if (Cambiado)
            {
                return RedirectToAction("ChangePasswordSuccess");
            }
            else
            {
                ModelState.AddModelError("", "La contraseña actual es incorrecta o la nueva contraseña no es válida.");
            }
            return View(); 
        }
        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Códigos de estado
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // Vaya a http://go.microsoft.com/fwlink/?LinkID=177550 para
            // obtener una lista completa de códigos de estado.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "El nombre de usuario ya existe. Escriba un nombre de usuario diferente.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Ya existe un nombre de usuario para esa dirección de correo electrónico. Escriba una dirección de correo electrónico diferente.";

                case MembershipCreateStatus.InvalidPassword:
                    return "La contraseña especificada no es válida. Escriba un valor de contraseña válido.";

                case MembershipCreateStatus.InvalidEmail:
                    return "La dirección de correo electrónico especificada no es válida. Compruebe el valor e inténtelo de nuevo.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "La respuesta de recuperación de la contraseña especificada no es válida. Compruebe el valor e inténtelo de nuevo.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "La pregunta de recuperación de la contraseña especificada no es válida. Compruebe el valor e inténtelo de nuevo.";

                case MembershipCreateStatus.InvalidUserName:
                    return "El nombre de usuario especificado no es válido. Compruebe el valor e inténtelo de nuevo.";

                case MembershipCreateStatus.ProviderError:
                    return "El proveedor de autenticación devolvió un error. Compruebe los datos especificados e inténtelo de nuevo. Si el problema continúa, póngase en contacto con el administrador del sistema.";

                case MembershipCreateStatus.UserRejected:
                    return "La solicitud de creación de usuario se ha cancelado. Compruebe los datos especificados e inténtelo de nuevo. Si el problema continúa, póngase en contacto con el administrador del sistema.";

                default:
                    return "Error desconocido. Compruebe los datos especificados e inténtelo de nuevo. Si el problema continúa, póngase en contacto con el administrador del sistema.";
            }
        }
        #endregion
    }
}
