using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace eClockMobile.Controllers
{
    public class MensajesController : AsyncController
    {
        //
        // GET: /Mensajes/

        public ActionResult Index()
        {
            return View();
        }

        public void MensajesAsync(string id)
        {
            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Mails email = new eClockBase.Controladores.Mails(Sesion);

            email.ObtenChatsEvent += email_ObtenChatsEvent;
            email.ObtenChats();
            AsyncManager.OutstandingOperations.Increment();

        }

        private void email_ObtenChatsEvent(string Resultados)
        {
            List<eClockBase.Controladores.ListadoJson> Lista = null;
            Lista = JsonConvert.DeserializeObject<List<eClockBase.Controladores.ListadoJson>>(Resultados);
            AsyncManager.Parameters["Lista"] = Lista;
            AsyncManager.OutstandingOperations.Decrement();
        }

        public ActionResult MensajesCompleted(List<eClockBase.Controladores.ListadoJson> Lista)
        {
            ViewBag.Lista = Lista;
            return View();
        }

        public void NuevoMsjAsync(string id)
        {
            id = eClockMobile.BaseModificada.CeC.HtmlQuita(id);
            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Sesion cSesion = new eClockBase.Controladores.Sesion(Sesion);

            
            eClockBase.Controladores.Mails email = new eClockBase.Controladores.Mails(Sesion);
            email.ObtenContactosEvent += email_ObtenContactosEvent;
            AsyncManager.OutstandingOperations.Increment();
            email.ObtenContactos();
        }

        private void email_ObtenContactosEvent(string Resultados)
        {
            List<eClockBase.Controladores.ListadoJson> Listado = JsonConvert.DeserializeObject<List<eClockBase.Controladores.ListadoJson>>(Resultados);
            AsyncManager.Parameters["Contactos"] = Listado;

            AsyncManager.OutstandingOperations.Decrement();
        }

        public ActionResult NuevoMsjCompleted(List<eClockBase.Controladores.ListadoJson> Contactos)
        {            
            ViewBag.Contactos = Contactos;
            return View();
        }

        public void DetallesAsync(string id)
        {
            id = eClockMobile.BaseModificada.CeC.HtmlQuita(id);
            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Mails email = new eClockBase.Controladores.Mails(Sesion);

            AsyncManager.Parameters["id"] = id;
            email.ObtenChatsConEvent += email_ObtenChatsConEvent;
            AsyncManager.OutstandingOperations.Increment();
            email.ObtenChatsCon(eClockMobile.BaseModificada.CeC.Convierte2Int(id));
        }

        public void email_ObtenChatsConEvent(string Resultados)
        {
            if (Resultados != null)
            {
                List<eClockBase.Controladores.ListadoJson> Lista = JsonConvert.DeserializeObject<List<eClockBase.Controladores.ListadoJson>>(Resultados);
                AsyncManager.Parameters["Lista"] = Lista;
                AsyncManager.OutstandingOperations.Decrement();
            }
        }

        public ActionResult DetallesCompleted(List<eClockBase.Controladores.ListadoJson> Lista, string id)
        {
            ViewBag.id = id;
            ViewBag.Lista = Lista;
            return View();
        }

        public void EnviarMensajeAsync(int id, string Texto)
        {
            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Mails emails = new eClockBase.Controladores.Mails(Sesion);
            emails.EnviaMensajeEvent += emails_EnviaMensajeEvent;
            AsyncManager.OutstandingOperations.Increment();
            emails.EnviaMensaje(id, Texto);
        }

        private void emails_EnviaMensajeEvent(bool Resultados)
        {
            AsyncManager.Parameters["Exitoso"] = Resultados;
            AsyncManager.OutstandingOperations.Decrement();
        }

        public bool EnviarMensajeCompleted(bool Exitoso)
        {
            return Exitoso;
        }

        public void ObtenChatsConDesdeAsync(int UsuarioIDCon, int MailID)
        {
            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Mails cMails = new eClockBase.Controladores.Mails(Sesion);
            cMails.ObtenChatsConDesdeEvent +=
                delegate(string Resultados)
                {
                    List<eClockBase.Controladores.ListadoJson> NuevosMensajes = JsonConvert.DeserializeObject<List<eClockBase.Controladores.ListadoJson>>(Resultados);
                    AsyncManager.Parameters["NuevosMensajes"] = NuevosMensajes;
                    AsyncManager.Parameters["Resultados"] = Resultados;
                    AsyncManager.OutstandingOperations.Decrement();
                };
            AsyncManager.OutstandingOperations.Increment();
            cMails.ObtenChatsConDesde(UsuarioIDCon, MailID);
        }

        public ActionResult ObtenChatsConDesdeCompleted(List<eClockBase.Controladores.ListadoJson> NuevosMensajes, string Resultados)
        {
            string Resultado = Resultados;

            return Json(Resultado, JsonRequestBehavior.AllowGet);
        }

        public void BorrarMensajesAsync(int id)
        {
            eClockBase.CeC_SesionBase cSesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Mails email = new eClockBase.Controladores.Mails(cSesion);

            email.BorraMensajesConEvent +=
                delegate(string Resultados)
                {
                    if (Resultados != "")
                    {
                        AsyncManager.Parameters["Exitoso"] = true;
                    }
                    else
                    {
                    }
                    AsyncManager.OutstandingOperations.Decrement();
                };
            AsyncManager.OutstandingOperations.Increment();
            email.BorraMensajesCon(id);
        }

        public ActionResult BorrarMensajesCompleted(bool Exitoso)
        {
            return Json(Exitoso, JsonRequestBehavior.AllowGet); ;
        }

    }
}
