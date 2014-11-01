using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Web.Mvc;

namespace eClockMobile.Controllers
{
    public class TramitesController : AsyncController
    {
        //
        // GET: /Tramites/

        public ActionResult Index()
        {
            return View();
        }

        public void TramitesAsync(string id)
        {
            id = eClockMobile.BaseModificada.CeC.HtmlQuita(id);
            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Sesion cSesion = new eClockBase.Controladores.Sesion(Sesion);
            eClockBase.Modelos.Model_PERSONAS DatosPersona = new eClockBase.Modelos.Model_PERSONAS();

            DatosPersona.PERSONA_ID = eClockBase.CeC.Convierte2Int(id);
            cSesion.ObtenDatosEvent += cSesion_ObtenDatosEvent;

            AsyncManager.OutstandingOperations.Increment();
            cSesion.ObtenDatos("EC_PERSONAS", "PERSONA_ID", DatosPersona);

            cSesion.CambioListadoEvent += cSesion_CambioListadoEvent;
            AsyncManager.OutstandingOperations.Increment();
            cSesion.ObtenListado("EC_TIPO_TRAMITE", "TIPO_TRAMITE_ID", "TIPO_TRAMITE_NOMBRE", "TIPO_TRAMITE_CAMPOS", "TIPO_TRAMITE_DESCRIPCION", "", false, "");



        }

        void cSesion_ObtenDatosEvent(int Resultado, string Datos)
        {
            try
            {
                if (Resultado == 1)
                {
                    AsyncManager.Parameters["DatosPersona"] = Newtonsoft.Json.JsonConvert.DeserializeObject<eClockBase.Modelos.Model_PERSONAS>(Datos);
                }

            }
            catch { }
            AsyncManager.OutstandingOperations.Decrement();
        }

        void cSesion_CambioListadoEvent(string Tramite)
        {
            try
            {
                List<eClockBase.Controladores.ListadoJson> ListaTramites = null;
                if (Tramite != null)
                {
                    ListaTramites = Newtonsoft.Json.JsonConvert.DeserializeObject<List<eClockBase.Controladores.ListadoJson>>(Tramite);
                }

                AsyncManager.Parameters["Tramites"] = ListaTramites;
            }
            catch { }
            AsyncManager.OutstandingOperations.Decrement();
        }

        public ActionResult TramitesCompleted(List<eClockBase.Controladores.ListadoJson> Tramites, eClockBase.Modelos.Model_PERSONAS DatosPersona)
        {
            ViewBag.Title = "Tramites";
            ViewBag.Tramites = Tramites;
            ViewBag.DatosPersona = DatosPersona;

            return View();
        }

        public void NuevoAsync(string id, int? Tramite_ID)
        {
            if (Tramite_ID != null)
            {
                id = eClockMobile.BaseModificada.CeC.HtmlQuita(id);
                eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
                eClockBase.Controladores.Sesion cSesion = new eClockBase.Controladores.Sesion(Sesion);
                eClockBase.Modelos.Model_TRAMITES Tramite = new eClockBase.Modelos.Model_TRAMITES();
                Tramite.TRAMITE_ID = (int)Tramite_ID;
                Tramite.PERSONA_ID = Convert.ToInt32(id);
                Tramite.TIPO_PRIORIDAD_ID = 1;
                Tramite.TRAMITE_DESCRIPCION = "";

                eClockBase.Controladores.Tramites TTS = new eClockBase.Controladores.Tramites(Sesion);

                TTS.NuevoEvent += TTS_NuevoEvent;
                TTS.Nuevo(Tramite.TRAMITE_ID, Tramite.PERSONA_ID, Tramite.TIPO_PRIORIDAD_ID, Tramite.TRAMITE_DESCRIPCION);
                AsyncManager.OutstandingOperations.Increment();
            }
        }

        void TTS_NuevoEvent(int Resultado)
        {
            bool Correcto = false;
            if (Resultado > 0)
                Correcto = true;
            AsyncManager.Parameters["Exitoso"] = Correcto;
            AsyncManager.OutstandingOperations.Decrement();
        }

        public ActionResult NuevoCompleted(bool Exitoso)
        {
            ViewBag.TramiteExitoso = Exitoso;
            return View();
        }
    }
}
