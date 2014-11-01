using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

namespace eClockMobile.Controllers
{
    public class PersonasController : AsyncController
    {
        //
        // GET: /Personas/

        public ActionResult Index()
        {
            return View();
        }

        [OutputCache(Duration = 36000)]
        public void FotoAsync(string id)
        {
            if (id == null || id == "")
                return;


            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            if (!Sesion.EstaLogeado())
            {
                RedirectToAction("Login", "Account");
                return;
            }
            AsyncManager.Parameters["NombreArchivo"] = id;
            eClockBase.Controladores.Personas cPersonas = new eClockBase.Controladores.Personas(Sesion);
            cPersonas.ObtenFotoThumbnailEvent +=
                delegate(byte[] Foto)
                {
                    AsyncManager.Parameters["Foto"] = Foto;
                    AsyncManager.OutstandingOperations.Decrement();
                };
            AsyncManager.OutstandingOperations.Increment();
            cPersonas.ObtenFotoThumbnail(id);
        }


        public FileResult FotoCompleted(string NombreArchivo, byte[] Foto)
        {
            Debug.WriteLine("FotoCompleted");
            if (Foto != null)
            {

                System.IO.MemoryStream MS = new System.IO.MemoryStream(Foto);
                return new FileStreamResult(MS, "image/jpeg");
            }
            //Se mostrará en un futuro la foto en blanco
            return new FileStreamResult(new System.IO.MemoryStream(), "image/jpeg"); 
        }
    }
}
