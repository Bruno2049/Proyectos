using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

namespace eClockMobile.Controllers
{
    public class ReportesController : AsyncController
    {
        //
        // GET: /Reportes/

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
                AsyncManager.Parameters["FechaInicial"] = FI;
                AsyncManager.Parameters["FechaFinal"] = FF;


                eClockBase.Controladores.Reportes cReportes = new eClockBase.Controladores.Reportes(Sesion);
                cReportes.ObtenListadoEvent +=
                    delegate(List<eClockBase.Modelos.Reportes.Model_Reportes> Reportes)
                    {
                        AsyncManager.Parameters["Reportes"] = Reportes;
                        AsyncManager.OutstandingOperations.Decrement();
                    };
                AsyncManager.OutstandingOperations.Increment();
                cReportes.ObtenListado("eClockBase.Modelos.Asistencias.Reporte_Asistencias,eClockBase.Modelos.Asistencias.Reporte_Asistencia31,eClockBase.Modelos.Asistencias.Reporte_AsistenciaAbr31,eClockBase.Modelos.HorasExtras.Reporte_Semanal_HET_DT,eClockBase.Modelos.HorasExtras.Reporte_Semanal_HET,eClockBase.Modelos.PreNomina.Reporte_PreNomina");

            }
        }


        public ActionResult AgrupacionCompleted(
            string Agrupacion, List<eClockBase.Modelos.Reportes.Model_Reportes> Reportes,
            DateTime? FechaInicial, DateTime? FechaFinal)
        {
            ViewBag.PersonaID = -1;
            ViewBag.Agrupacion = Agrupacion;
            ViewBag.Reportes = Reportes;
            ViewBag.FechaInicial = FechaInicial;
            ViewBag.FechaFinal = FechaFinal;

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
                AsyncManager.Parameters["FechaInicial"] = FI;
                AsyncManager.Parameters["FechaFinal"] = FF;
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

                eClockBase.Controladores.Reportes cReportes = new eClockBase.Controladores.Reportes(Sesion);
                cReportes.ObtenListadoEvent +=
                    delegate(List<eClockBase.Modelos.Reportes.Model_Reportes> Reportes)
                    {
                        AsyncManager.Parameters["Reportes"] = Reportes;
                        AsyncManager.OutstandingOperations.Decrement();
                    };
                AsyncManager.OutstandingOperations.Increment();
                cReportes.ObtenListado("eClockBase.Modelos.Asistencias.Reporte_Asistencias,eClockBase.Modelos.Asistencias.Reporte_Asistencia31,eClockBase.Modelos.Asistencias.Reporte_AsistenciaAbr31,eClockBase.Modelos.HorasExtras.Reporte_Semanal_HET_DT,eClockBase.Modelos.HorasExtras.Reporte_Semanal_HET,eClockBase.Modelos.PreNomina.Reporte_PreNomina");

            }
        }


        public ActionResult EmpleadoCompleted(
            eClockBase.Modelos.Model_PERSONAS DatosPersona, List<eClockBase.Modelos.Reportes.Model_Reportes> Reportes,
            DateTime? FechaInicial, DateTime? FechaFinal)
        {
            if (DatosPersona != null)
            {
                ViewBag.Persona = DatosPersona.PERSONA_NOMBRE + "(" + DatosPersona.PERSONA_LINK_ID + ")";
                ViewBag.PersonaID = DatosPersona.PERSONA_ID;
            }
            ViewBag.DatosPersona = DatosPersona;
            ViewBag.Reportes = Reportes;
            ViewBag.FechaInicial = FechaInicial;
            ViewBag.FechaFinal = FechaFinal;
            ViewBag.Agrupacion = "";
            return View();
        }


        [OutputCache(Duration = 36000)]
        public void ImagenAsync(string id)
        {
            Debug.WriteLine("ImagenAsync " + id);
            if (id == null || id == "")
                return;

            int ReporteID = 0;
            id = System.IO.Path.GetFileNameWithoutExtension(id);
            ReporteID = eClockBase.CeC.Convierte2Int(id);
            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            if (!Sesion.EstaLogeado())
            {
                RedirectToAction("Login", "Account");
                return;
            }
            eClockBase.Controladores.Sesion cSesion = new eClockBase.Controladores.Sesion(Sesion);
            cSesion.ObtenImagenThumbnailFinalizado +=
                delegate(byte[] Imagen)
                {
                    Debug.WriteLine("ImagenAsync Finalizado" + id);
                    AsyncManager.Parameters["Imagen"] = Imagen;
                    AsyncManager.OutstandingOperations.Decrement();
                };
            AsyncManager.OutstandingOperations.Increment();
            cSesion.ObtenImagenThumbnail("EC_REPORTES", "REPORTE_ID", id, "REPORTE_IMAGEN");

        }




        public FileResult ImagenCompleted(byte[] Imagen)
        {
            Debug.WriteLine("ImagenAsync ImagenCompleted");
            if (Imagen != null)
            {

                System.IO.MemoryStream MS = new System.IO.MemoryStream(Imagen);
                return new FileStreamResult(MS, "image/jpeg");
            }
            return new FileStreamResult(new System.IO.MemoryStream(), "image/jpeg");
        }

        public void ReporteAsisAsync(int id, int? PersonaID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int? FormatoRepID, string OtrosParametros)
        {
            if (Agrupacion == null)
                Agrupacion = "";
            if (PersonaID == null)
                PersonaID = -1;
            if (FormatoRepID == null)
                FormatoRepID = 0;
            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Reportes cReporte = new eClockBase.Controladores.Reportes(Sesion);
            var ParamAsistencia = new eClockBase.Modelos.Asistencias.Model_Parametros(eClockBase.CeC.Convierte2Int(PersonaID), Agrupacion, FechaInicial, FechaFinal);
            string Parametros = Newtonsoft.Json.JsonConvert.SerializeObject(ParamAsistencia);
            cReporte.ObtenReporteEvent +=
                delegate(byte[] ArchivoReporte, string PathArchivo)
                {
                    AsyncManager.Parameters["ArchivoReporte"] = ArchivoReporte;
                    AsyncManager.Parameters["PathArchivo"] = PathArchivo;
                    AsyncManager.OutstandingOperations.Decrement();
                };
            AsyncManager.OutstandingOperations.Increment();
            cReporte.ObtenReporte(id, Parametros, eClockBase.CeC.Convierte2Int(FormatoRepID));

        }


        public FileResult ReporteAsisCompleted(byte[] ArchivoReporte, string PathArchivo)
        {
            System.IO.MemoryStream MS = null;
            if(ArchivoReporte == null)
                MS= new System.IO.MemoryStream();
            else
                MS = new System.IO.MemoryStream(ArchivoReporte);
            return new FileStreamResult(MS, "application/pdf");
        }

        public void eMailAsisAsync(int id, int? PersonaID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int? FormatoRepID, string OtrosParametros, string Titulo)
        {
            if (Agrupacion == null)
                Agrupacion = "";
            if (PersonaID == null)
                PersonaID = -1;
            if (FormatoRepID == null)
                FormatoRepID = 0;
            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Reportes cReporte = new eClockBase.Controladores.Reportes(Sesion);
            var ParamAsistencia = new eClockBase.Modelos.Asistencias.Model_Parametros(eClockBase.CeC.Convierte2Int(PersonaID), Agrupacion, FechaInicial, FechaFinal);
            string Parametros = Newtonsoft.Json.JsonConvert.SerializeObject(ParamAsistencia);

            cReporte.ObtenReporteeMailEvent +=
                delegate(bool Estado)
                {
                    AsyncManager.Parameters["Estado"] = Estado;
                    AsyncManager.OutstandingOperations.Decrement();
                };
            AsyncManager.OutstandingOperations.Increment();
            if (Titulo == null)
                Titulo = "_";
            cReporte.ObtenReporteeMail("Reporte (" + Titulo + ") Generado el " + DateTime.Now.ToString(), "", id, Parametros, eClockBase.CeC.Convierte2Int(FormatoRepID));

        }



        public ActionResult eMailAsisCompleted(bool Estado)
        {
            return Json(Estado, JsonRequestBehavior.AllowGet);
        }
    }
}
