using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eClockMobile.Controllers
{
    public class NominaController : AsyncController
    {
        //
        // GET: /Nomina/

        public ActionResult ValidaClv()
        {
            return View();
        }

        public void ValidaPswAsync(string Clave)
        {
            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Usuarios US = new eClockBase.Controladores.Usuarios(Sesion);            
            US.ValidaPasswordEvent += delegate(bool Resultado)
            {
                AsyncManager.Parameters["Result"] = Resultado;
                AsyncManager.OutstandingOperations.Decrement();
            };
            AsyncManager.OutstandingOperations.Increment();
            US.ValidaPassword(Clave);
        }

        public bool ValidaPswCompleted(bool Result)
        {
            return Result;
        }

        public void ListaAsync()
        {
            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Sesion cSesion = new eClockBase.Controladores.Sesion(Sesion);

            int PersonaID = 0;

            if (Sesion.Mdl_Sesion.PERSONA_ID != null && Sesion.Mdl_Sesion.PERSONA_ID > 0)
            {
                PersonaID = Sesion.Mdl_Sesion.PERSONA_ID;
                cSesion.CambioListadoEvent += cSesion_CambioListadoEvent;
                AsyncManager.OutstandingOperations.Increment();
                cSesion.ObtenListado("EC_REC_NOMINAS", "REC_NOMINA_ID", "REC_NOMINA_FFIN", "REC_NOMINA_NO", "REC_NOMINA_IMPRESO", "", false, "PERSONA_ID = " + PersonaID);
            }
        }


        private void cSesion_CambioListadoEvent(string Listado)
        {
            try
            {
                List<eClockBase.Controladores.ListadoJson> ListaNomina = null;
                if (Listado != null)
                {
                    ListaNomina = Newtonsoft.Json.JsonConvert.DeserializeObject<List<eClockBase.Controladores.ListadoJson>>(Listado);
                }

                AsyncManager.Parameters["ListaNomina"] = ListaNomina;
            }
            catch { }
            AsyncManager.OutstandingOperations.Decrement();
        }

        public ActionResult ListaCompleted(List<eClockBase.Controladores.ListadoJson> ListaNomina)
        {
            ViewBag.ListaNomina = ListaNomina;
            return View();
        }

        public void DetallesAsync(string id)
        {
            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Nominas cNomina = new eClockBase.Controladores.Nominas(Sesion);
            cNomina.ObtenDatosReciboEvent += cNomina_ObtenDatosReciboEvent;
            AsyncManager.OutstandingOperations.Increment();
            cNomina.ObtenDatosRecibo(BaseModificada.CeC.Convierte2Int(id));
        }

        private void cNomina_ObtenDatosReciboEvent(eClockBase.Modelos.Nomina.Reporte_RecNomina RecNominas)
        {
            if (RecNominas != null)
            {
                AsyncManager.Parameters["RecNominas"] = RecNominas;
            }
            AsyncManager.OutstandingOperations.Decrement();
        }

        public ActionResult DetallesCompleted(eClockBase.Modelos.Nomina.Reporte_RecNomina RecNominas)
        {
            eClockBase.Modelos.Nomina.Model_Rec_Nominas_Ampliados RecNominaAmpliado = RecNominas.Recibo_Nomina[0];
            List<eClockBase.Modelos.Model_REC_NOMI_MOV> RecNominaPercepciones = RecNominas.Percepciones;
            List<eClockBase.Modelos.Model_REC_NOMI_MOV> RecNominaDeducciones = RecNominas.Deducciones;

            
            ViewBag.RecNominaAmpliado = RecNominaAmpliado;
            ViewBag.RecNominaPercepciones = RecNominaPercepciones;
            ViewBag.RecNominaDeducciones = RecNominaDeducciones;
            
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
