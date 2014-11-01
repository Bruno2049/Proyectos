using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eClockMobile.Controllers
{
    public class AccesosController : AsyncController
    {
        //
        // GET: /Accesos/

        public ActionResult Index()
        {
            return View();
        }

        public void AgrupacionAsync(string id, DateTime? FechaInicial, DateTime? FechaFinal)
        {
            id = eClockMobile.BaseModificada.CeC.HtmlQuita(id);
            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            if (!Sesion.EstaLogeado())
            {
                RedirectToAction("Login", "Account");
                return;
            }

            string Agrupacion = id;
            if (Agrupacion == null || Agrupacion == "")
                Agrupacion = "|";
            AsyncManager.Parameters["Agrupacion"] = Agrupacion;

            {
                DateTime FI = eClockBase.CeC.Convierte2DateTime(FechaInicial);
                DateTime FF = eClockBase.CeC.Convierte2DateTime(FechaFinal);
                eClockMobile.BaseModificada.CeC_Sesion.FechaInicio = FI;
                eClockMobile.BaseModificada.CeC_Sesion.FechaFin = FF;
                eClockBase.Controladores.Accesos cAccesos = new eClockBase.Controladores.Accesos(Sesion);
                cAccesos.EventoObtenAccesosFinalizado +=
                    delegate(List<eClockBase.Modelos.Accesos.Model_Accesos> Accesos)
                    {
                        AsyncManager.Parameters["Accesos"] = Accesos;
                        AsyncManager.OutstandingOperations.Decrement();
                    };
                AsyncManager.OutstandingOperations.Increment();
                cAccesos.ObtenAccesos(false, -1, Agrupacion, FI, FF, "", "");

               
            }
        }


        public ActionResult AgrupacionCompleted(
    string Agrupacion, List<eClockBase.Modelos.Accesos.Model_Accesos> Accesos)
        {

            ViewBag.Agrupacion = Agrupacion;
            ViewBag.Accesos = Accesos;

            return View();
        }

        public void EmpleadoAsync(string id, DateTime? FechaInicial, DateTime? FechaFinal)
        {
            id = eClockMobile.BaseModificada.CeC.HtmlQuita(id);
            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            if (!Sesion.EstaLogeado())
            {
                RedirectToAction("Login", "Account");
                return;
            }

            int PersonaID = eClockBase.CeC.Convierte2Int(id);
            if (PersonaID > 0)
            {
                DateTime FI = eClockBase.CeC.Convierte2DateTime(FechaInicial);
                DateTime FF = eClockBase.CeC.Convierte2DateTime(FechaFinal);
                eClockMobile.BaseModificada.CeC_Sesion.FechaInicio = FI;
                eClockMobile.BaseModificada.CeC_Sesion.FechaFin = FF;
                eClockBase.Controladores.Accesos cAccesos = new eClockBase.Controladores.Accesos(Sesion);
                cAccesos.EventoObtenAccesosFinalizado +=
                    delegate(List<eClockBase.Modelos.Accesos.Model_Accesos> Accesos)
                    {
                        AsyncManager.Parameters["Accesos"] = Accesos;
                        AsyncManager.OutstandingOperations.Decrement();
                    };
                AsyncManager.OutstandingOperations.Increment();
                cAccesos.ObtenAccesos(false, PersonaID, "", FI, FF, "", "");

                eClockBase.Modelos.Model_PERSONAS DatosPersona = new eClockBase.Modelos.Model_PERSONAS();
                DatosPersona.PERSONA_ID = PersonaID;
                eClockBase.Controladores.Sesion cSesion = new eClockBase.Controladores.Sesion(Sesion);
                cSesion.ObtenDatosEvent += delegate(int Resultado, string Datos)
                {
                    if (Resultado == 1)
                    {
                        AsyncManager.Parameters["DatosPersona"] = Newtonsoft.Json.JsonConvert.DeserializeObject<eClockBase.Modelos.Model_PERSONAS>(Datos);
                    }

                    AsyncManager.OutstandingOperations.Decrement();

                };
                AsyncManager.OutstandingOperations.Increment();
                cSesion.ObtenDatos("EC_PERSONAS", "PERSONA_ID", DatosPersona);


            }
        }


        public ActionResult EmpleadoCompleted(
    eClockBase.Modelos.Model_PERSONAS DatosPersona, List<eClockBase.Modelos.Accesos.Model_Accesos> Accesos)
        {
            if (DatosPersona != null)
                ViewBag.Persona = DatosPersona.PERSONA_NOMBRE + "(" + DatosPersona.PERSONA_LINK_ID + ")";
            ViewBag.DatosPersona = DatosPersona;
            ViewBag.Accesos = Accesos;

            return View();
        }

        //[OutputCache(Duration = 1000)]
        public FileResult Color(string id)
        {
            if (id == null || id == "")
                return null;
                        if (id == null || id == "")
                return null;

            int iTipoAcceso = 0;
            id = System.IO.Path.GetFileNameWithoutExtension(id);
            iTipoAcceso = eClockBase.CeC.Convierte2Int(id);
            int iColor = 0;
            System.Drawing.Color Color = new System.Drawing.Color();
            switch (iTipoAcceso)
            {
                case 0:
                    Color = System.Drawing.Color.Black;
                    break;
                case 1:
                    Color = System.Drawing.Color.Green;
                    break;
                case 2:
                    Color = System.Drawing.Color.Blue;
                    break;
                case 3:
                    Color = System.Drawing.Color.Yellow;
                    break;
                case 4:
                    Color = System.Drawing.Color.Pink;
                    break;
                case 5:
                    Color = System.Drawing.Color.RosyBrown;
                    break;
                case 6:
                    Color = System.Drawing.Color.Purple;
                    break;
                case 7:
                    Color = System.Drawing.Color.PeachPuff;
                    break;
                case 8:
                    Color = System.Drawing.Color.Orchid;
                    break;
                default:
                    Color = System.Drawing.Color.Honeydew;
                    break;
            }
            iColor = Color.ToArgb();
            return new FileStreamResult(Controllers.SharedController.ObtenColor(iColor), "image/jpeg");
            //return File(MS, "image/jpeg", id);
        }
    }
}
