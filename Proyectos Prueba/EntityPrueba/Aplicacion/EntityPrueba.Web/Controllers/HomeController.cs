using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityPrueba.Entidad;
using EntityPrueba.LogicaNegocios;
using EntityPrueba.LogicaNegocios.Login;

namespace EntityPrueba.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult InsertaUsuario()
        {
            US_USUARIOS Usuario = new US_USUARIOS();
            Usuario.USUARIO = "EstebanCL";
            Usuario.NOMBRE_COMPLETO = "Esteban Cruz Lagunes";
            Usuario.CORREO_ELECTRONICO = "ecruzlagunes@hotmail.com";
            Usuario.CONTRASENA = "A@141516";
            var UsuarioR = ControlUsuariosLN.ClassInstance.InsertaUsuario(Usuario);

            ViewBag.Usuario = UsuarioR;

            return View();
        }
    }
}
