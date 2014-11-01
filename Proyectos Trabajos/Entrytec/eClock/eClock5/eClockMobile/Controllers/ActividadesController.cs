using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace eClockMobile.Controllers
{
    public class ActividadesController : AsyncController
    {
        public ActionResult Index()
        {
            return View();
        }

        public void ActividadesAsync(string id)
        {
            id = eClockMobile.BaseModificada.CeC.HtmlQuita(id);
            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Sesion cSesion = new eClockBase.Controladores.Sesion(Sesion);
            eClockBase.Modelos.Model_PERSONAS DatosPersona = new eClockBase.Modelos.Model_PERSONAS();
            cSesion.CambioListadoEvent += cSesion_CambioListadoEvent;

            DatosPersona.PERSONA_ID = eClockBase.CeC.Convierte2Int(id);

            AsyncManager.OutstandingOperations.Increment();
            cSesion.ObtenDatosEvent += cSesion_ObtenDatosEvent;
            cSesion.ObtenListado("EC_ACTIVIDADES", "ACTIVIDAD_ID", "ACTIVIDAD_NOMBRE", "ACTIVIDAD_CAMPOS", "ACTIVIDAD_DESCRIPCION", "ACTIVIDAD_IMAGEN", false, "");

            AsyncManager.OutstandingOperations.Increment();
            cSesion.ObtenDatos("EC_PERSONAS", "PERSONA_ID", DatosPersona);
        }

        void cSesion_CambioListadoEvent(string Listado)
        {
            try
            {
                List<eClockBase.Controladores.ListadoJson> ListadoActividades = null;
                ListadoActividades = JsonConvert.DeserializeObject<List<eClockBase.Controladores.ListadoJson>>(Listado);

                if (ListadoActividades != null)
                {
                    AsyncManager.Parameters["ListaActividades"] = ListadoActividades;
                }
            }
            catch
            { }

            AsyncManager.OutstandingOperations.Decrement();

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

        public ActionResult ActividadesCompleted(List<eClockBase.Controladores.ListadoJson> ListaActividades, eClockBase.Modelos.Model_PERSONAS DatosPersona)
        {
            ViewBag.Title = "Actividades";
            ViewBag.ListaActividades = ListaActividades;
            ViewBag.DatosPersona = DatosPersona;

            return View();
        }

        public void NuevoAsync(string id, int? ACTIVIDAD_ID)
        {
            if (ACTIVIDAD_ID != null)
            {
                id = eClockMobile.BaseModificada.CeC.HtmlQuita(id);
                eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
                eClockBase.Controladores.Sesion cSesion = new eClockBase.Controladores.Sesion(Sesion);
                eClockBase.Controladores.Actividades Ads = new eClockBase.Controladores.Actividades(Sesion);
                int Id_Actividad = (int)ACTIVIDAD_ID;
                int Persona_ID = Convert.ToInt32(id);
                Ads.IncribirseEvent += Ads_IncribirseEvent;
                AsyncManager.OutstandingOperations.Increment();
                Ads.Incribirse(Id_Actividad, Persona_ID, 1, "");
                
            }
        }

        private void Ads_IncribirseEvent(int Resultado)
        {
            bool Correcto = false;
            if (Resultado > 0)
                Correcto = true;
            AsyncManager.Parameters["Exitoso"] = Correcto;
            AsyncManager.OutstandingOperations.Decrement();
        }

        public ActionResult NuevoCompleted(bool Exitoso)
        {
            ViewBag.InscripcionExitosa = Exitoso;
            return View();
        }

        [OutputCache(Duration = 36000)]
        public void ImagenAsync(string id)
        {
            id = System.IO.Path.GetFileNameWithoutExtension(id);
            if (id == null || id == "")
                return;


            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            AsyncManager.Parameters["NombreArchivo"] = id;
            try
            {
                eClockBase.Controladores.Actividades Act = new eClockBase.Controladores.Actividades(Sesion);
               
                Act.ObtenImagenFinalizado += Act_ObtenImagenFinalizado;
                AsyncManager.OutstandingOperations.Increment();
                Act.ObtenImagen(eClockBase.CeC.Convierte2Int(id));
                
            }

            catch(Exception ex) {
                eClockBase.CeC_Log.AgregaError(ex);

            }
        }

        void Act_ObtenImagenFinalizado(byte[] Imagen)
        {
            AsyncManager.Parameters["Imagen"] = Imagen;
            AsyncManager.OutstandingOperations.Decrement();
        }

        public FileResult ImagenCompleted(byte[] Imagen)
        {
            if (Imagen != null)
            {

                System.IO.MemoryStream MS = new System.IO.MemoryStream(Imagen);
                return new FileStreamResult(MS, "image/jpeg");
            }
            return new FileStreamResult(new System.IO.MemoryStream(), "image/jpeg");
        }
    }
}
