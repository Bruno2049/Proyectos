﻿using System.Web.Mvc;

namespace Universidad.WebAdministrativa.Controllers
{
    public class ReportesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // GET: Reportes
        public ActionResult Reportes()
        {
            return Redirect("../VisorReportes.aspx");
        }
    }
}