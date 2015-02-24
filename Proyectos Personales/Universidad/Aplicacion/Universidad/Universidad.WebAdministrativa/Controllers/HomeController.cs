﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Universidad.Controlador.GestionCatalogos;
using Universidad.Controlador.Login;
using Universidad.Controlador.MenuSistema;
using Universidad.Controlador.Personas;
using Universidad.Entidades;
using Universidad.Entidades.ControlUsuario;

namespace Universidad.WebAdministrativa.Controllers
{
    public class HomeController : AsyncController
    {
        public void DefaultAsync()
        {
            var sesion = (Sesion)TempData["sesion"];
            var usuario = (US_USUARIOS)TempData["usuario"];
            var servicioLogin = new SVC_LoginAdministrativos(sesion);
            var serviciosCatalogos = new SVC_GestionCatalogos(sesion);

            AsyncManager.Parameters["sesion"] = sesion;
            AsyncManager.Parameters["usuario"] = usuario;

            servicioLogin.ObtenNombreCompletoFinalizado += delegate(PER_PERSONAS persona)
            {
                AsyncManager.Parameters["persona"] = persona;
                AsyncManager.OutstandingOperations.Decrement();
            };

            AsyncManager.OutstandingOperations.Increment();
            servicioLogin.ObtenNombreCompleto(usuario);


            serviciosCatalogos.ObtenTipoUsuarioFinalizado += delegate(US_CAT_TIPO_USUARIO tipoUsuario)
            {
                AsyncManager.Parameters["tipoUsuario"] = tipoUsuario;
                AsyncManager.OutstandingOperations.Decrement();
            };

            AsyncManager.OutstandingOperations.Increment();
            serviciosCatalogos.ObtenTipoUsuario(usuario.ID_USUARIO);
        }

        public ActionResult DefaultCompleted(Sesion sesion, US_USUARIOS usuario, PER_PERSONAS persona, US_CAT_TIPO_USUARIO tipoUsuario)
        {
            ViewBag.Nombre = persona.NOMBRE_COMPLETO;
            ViewBag.TipoUsuario = tipoUsuario.TIPO_USUARIO;
            TempData["sesion"] = sesion;
            TempData["usuario"] = usuario;
            TempData["persona"] = persona;
            return View();
        }

        public void ObtenArbolMenuAsync()
        {
            var sesion = (Sesion) TempData["sesion"];
            var usuario = (US_USUARIOS) TempData["usuario"];

            var serviciosSistema = new SvcMenuSistemaC(sesion);
            serviciosSistema.TraeArbolMenuWadmFinalizado += delegate(List<SIS_WADM_ARBOLMENU> lista)
            {
                AsyncManager.OutstandingOperations.Decrement();
            };
            serviciosSistema.TraeArbolMenuWadm(sesion);
        }

        public ActionResult ObtenArbolMenuCompleted()
        {
            return PartialView();
        }
    }
}