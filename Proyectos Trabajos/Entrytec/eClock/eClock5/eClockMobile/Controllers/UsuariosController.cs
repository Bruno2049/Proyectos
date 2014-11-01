using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eClockMobile.Models;
namespace eClockMobile.Controllers
{
    public class UsuariosController : AsyncController
    {
        //
        // GET: /Usuarios/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registro()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void RegistroAsync(RegistroEmpleadoModel model)
        {
            if (ModelState.IsValid)
            {
                eClockBase.CeC_SesionBase Sesion = eClockMobile.BaseModificada.CeC_Sesion.ObtenSesion(this);
                eClockBase.Controladores.Usuarios cUsuarios = new eClockBase.Controladores.Usuarios(Sesion);
                cUsuarios.CreadoUsuarioEmpleado +=
                    delegate(string Resultado)
                    {
                        AsyncManager.Parameters["Resultado"] = Resultado;
                        AsyncManager.OutstandingOperations.Decrement();
                    };
                AsyncManager.OutstandingOperations.Increment();
                cUsuarios.CreaUsuarioEmpleado(model.PersonaLinkID.ToString(), model.Password, model.Email, model.Suscripcion);
                /* if(model.
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
                 */
                //ModelState.AddModelError("", ErrorCodeToString(createStatus));
            }
        }



        public ActionResult RegistroCompleted(string Resultado)
        {
            switch (Resultado)
            {
                case "OK":
                    return RedirectToAction("Login", "Account", new { id = "Se ha creado con exíto" });
                    break;
                case "NO_SUSCRIPCION":
                    ModelState.AddModelError(Resultado, "La suscripción introducida no coincide con ninguna empresa válida, contacte a capital humano o a su superior"); ;
                    break;
                case "USUARIO_YA_EXISTE":
                    ModelState.AddModelError(Resultado, "Ya se ha registrado previamente este no de empleado, si no recuerda su contraseñá intente recuperarla");
                    break;
                case "NO_EMPLEADO":
                    ModelState.AddModelError(Resultado, "El número de empleado no existe, contacte a capital humano o a su superior");
                    break;
                default:
                    ModelState.AddModelError(Resultado, "Error desconocido, intente nuevamente más tarde o contacte a soporte técnico");
                    break;
            }
            return View();
        }

        public ActionResult Olvido_Clave()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Olvido_ClaveAsync(string eMail)
        {
            if (ModelState.IsValid)
            {
                eClockBase.CeC_SesionBase Sesion = eClockMobile.BaseModificada.CeC_Sesion.ObtenSesion(this);
                eClockBase.Controladores.Usuarios cUsuarios = new eClockBase.Controladores.Usuarios(Sesion);
                cUsuarios.OlvidoClaveEvent +=
                    delegate(string Resultado)
                    {
                        AsyncManager.Parameters["Resultado"] = Resultado;
                        AsyncManager.OutstandingOperations.Decrement();
                    };
                AsyncManager.OutstandingOperations.Increment();
                cUsuarios.OlvidoClave(eMail, eMail);
            }
        }
        public ActionResult Olvido_ClaveCompleted(string Resultado)
        {
            switch (Resultado)
            {
                case "OK":
                    return RedirectToAction("Login", "Account", new { id = "Se ha enviado su contraseña a su correo electrónico" });
                    break;
                default:
                    ModelState.AddModelError(Resultado, "Posiblemente el correo no existe, intente nuevamente más tarde o contacte a soporte técnico");
                    break;
            }
            return View();
        }
    }
}
