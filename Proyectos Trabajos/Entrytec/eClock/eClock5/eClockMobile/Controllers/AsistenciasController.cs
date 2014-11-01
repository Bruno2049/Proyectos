using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;

namespace eClockMobile.Controllers
{
    public class AsistenciasController : AsyncController
    {
        //
        // GET: /Asistencias/

        public void IndexAsync(string id, DateTime? FechaInicial, DateTime? FechaFinal)
        {
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
                eClockBase.Controladores.Asistencias cAsistencia = new eClockBase.Controladores.Asistencias(Sesion);
                cAsistencia.EventoObtenAsistenciaLinealNFinalizado += delegate(List<eClockBase.Modelos.Asistencias.Model_Asistencia_Lineal_N> Asistencia)
                {
                    AsyncManager.Parameters["Asistencias"] = Asistencia;
                    AsyncManager.OutstandingOperations.Decrement();
                };
                AsyncManager.OutstandingOperations.Increment();
                cAsistencia.ObtenAsistenciaLinealN(
                     eClockBase.CeC.PersonaID2PersonaDiarioID(PersonaID, FI),
                      eClockBase.CeC.PersonaID2PersonaDiarioID(PersonaID, FF));


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


        public ActionResult IndexCompleted(eClockBase.Modelos.Model_PERSONAS DatosPersona, List<eClockBase.Modelos.Asistencias.Model_Asistencia_Lineal_N> Asistencias)
        {
            if (DatosPersona != null)
                ViewBag.Persona = DatosPersona.PERSONA_NOMBRE + "(" + DatosPersona.PERSONA_LINK_ID + ")";
            ViewBag.DatosPersona = DatosPersona;
            ViewBag.Asistencias = Asistencias;
            return View();
        }

        public void AgrupacionAsync(string id, DateTime? FechaInicial, DateTime? FechaFinal)
        {
            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            if (!Sesion.EstaLogeado())
            {
                RedirectToAction("Login", "Account");
                return;
            }
            id = eClockMobile.BaseModificada.CeC.HtmlQuita(id);
            string Agrupacion = id;
            if (Agrupacion == null || Agrupacion == "")
                Agrupacion = "|";
            AsyncManager.Parameters["Agrupacion"] = Agrupacion;
            DateTime FI = eClockBase.CeC.Convierte2DateTime(FechaInicial);
            DateTime FF = eClockBase.CeC.Convierte2DateTime(FechaFinal);
            eClockMobile.BaseModificada.CeC_Sesion.FechaInicio = FI;
            eClockMobile.BaseModificada.CeC_Sesion.FechaFin = FF;
            eClockBase.Controladores.Asistencias cAsistencia = new eClockBase.Controladores.Asistencias(Sesion);
            cAsistencia.ObtenAsistenciaHorizontalNAbr31Finalizado +=
                delegate(List<eClockBase.Modelos.Asistencias.Reporte_AsistenciaAbr31> AsistenciaHorizontalN)
                {

                    AsyncManager.Parameters["Asistencias"] = AsistenciaHorizontalN;
                    AsyncManager.OutstandingOperations.Decrement();
                };
            AsyncManager.OutstandingOperations.Increment();
            cAsistencia.ObtenAsistenciaHorizontalN(false, false, true, false, true, true, true, -1, Agrupacion, FI, FI.AddDays(8));



            eClockBase.Modelos.Model_PERSONAS DatosPersona = new eClockBase.Modelos.Model_PERSONAS();
            eClockBase.Controladores.Sesion cSesion = new eClockBase.Controladores.Sesion(Sesion);


            cSesion = new eClockBase.Controladores.Sesion(Sesion);
            cSesion.CambioListadoEvent += delegate(string Listado)
            {
                if (Listado != null)
                {
                    List<eClockBase.Controladores.ListadoJson> ListadoJson = eClockBase.Controladores.CeC_ZLib.Json2Object<List<eClockBase.Controladores.ListadoJson>>(Listado);
                    AsyncManager.Parameters["Turnos"] = ListadoJson;

                }
                AsyncManager.OutstandingOperations.Decrement();
            };
            AsyncManager.OutstandingOperations.Increment();
            cSesion.ObtenListado("EC_TURNOS", "TURNO_ID", "TURNO_NOMBRE", "", "", "", false, "");

            /* cSesion = new eClockBase.Controladores.Sesion(Sesion);
             cSesion.CambioListadoEvent += delegate(string Listado)
             {
                 if (Listado != null)
                 {
                     List<eClockBase.Controladores.ListadoJson> ListadoJson = eClockBase.Controladores.CeC_ZLib.Json2Object<List<eClockBase.Controladores.ListadoJson>>(Listado);
                     AsyncManager.Parameters["TipoIncidencias"] = ListadoJson;

                 }
                 AsyncManager.OutstandingOperations.Decrement();
             };
             AsyncManager.OutstandingOperations.Increment();
             cSesion.ObtenListado("EC_TIPO_INCIDENCIAS", "TIPO_INCIDENCIA_ID", "TIPO_INCIDENCIA_NOMBRE", "TIPO_INCIDENCIA_ABR", "", "TIPO_INCIDENCIA_COLOR", false, "");
             */

            cSesion = new eClockBase.Controladores.Sesion(Sesion);
            eClockBase.Modelos.Model_TIPO_INCIDENCIAS TipoIncidencia = new eClockBase.Modelos.Model_TIPO_INCIDENCIAS();
            cSesion.ObtenDatosEvent +=
                delegate(int Resultado, string Datos)
                {
                    if (Resultado > 0)
                    {
                        List<eClockBase.Modelos.Model_TIPO_INCIDENCIAS> TipoIncidencias = JsonConvert.DeserializeObject<List<eClockBase.Modelos.Model_TIPO_INCIDENCIAS>>(eClockBase.CeC.Json2JsonList(Datos));
                        AsyncManager.Parameters["TipoIncidencias"] = TipoIncidencias;
                    }
                    AsyncManager.OutstandingOperations.Decrement();
                };
            string Filtro = "";
            if (BaseModificada.CeC_Sesion.EsKiosco)
                Filtro = "TIPO_INCIDENCIA_KIOSCO=1";
            AsyncManager.OutstandingOperations.Increment();
            cSesion.ObtenDatos("EC_TIPO_INCIDENCIAS", "", TipoIncidencia, "TIPO_INCIDENCIA_NOMBRE", Filtro);


        }


        public ActionResult AgrupacionCompleted(
    string Agrupacion, List<eClockBase.Modelos.Asistencias.Reporte_AsistenciaAbr31> Asistencias,
    List<eClockBase.Controladores.ListadoJson> Turnos, List<eClockBase.Modelos.Model_TIPO_INCIDENCIAS> TipoIncidencias)
        {

            ViewBag.Agrupacion = Agrupacion;
            ViewBag.Asistencias = Asistencias;
            ViewBag.Turnos = Turnos;
            ViewBag.TipoIncidencias = TipoIncidencias;
            return View();
        }

        public void EmpleadoAsync(string id, DateTime? FechaInicial, DateTime? FechaFinal)
        {
            Debug.WriteLine("Asistencias/EmpleadoAsync " + id);
            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            if (!Sesion.EstaLogeado())
            {
                RedirectToAction("Login", "Account");
                return;
            }
            if (id != null && id.Length > 4 && id.Substring(0, 5) == "PLID_")
            {
                return;
            }
            id = eClockMobile.BaseModificada.CeC.HtmlQuita(id);
            int PersonaID = eClockBase.CeC.Convierte2Int(id);
            if (PersonaID > 0)
            {
                DateTime FI = eClockBase.CeC.Convierte2DateTime(FechaInicial);
                DateTime FF = eClockBase.CeC.Convierte2DateTime(FechaFinal);
                eClockMobile.BaseModificada.CeC_Sesion.FechaInicio = FI;
                eClockMobile.BaseModificada.CeC_Sesion.FechaFin = FF;
                eClockBase.Controladores.Asistencias cAsistencia = new eClockBase.Controladores.Asistencias(Sesion);
                cAsistencia.EventoObtenAsistenciaLinealNFinalizado += delegate(List<eClockBase.Modelos.Asistencias.Model_Asistencia_Lineal_N> Asistencia)
                {
                    Debug.WriteLine("Asistencias/EmpleadoAsync Asistencias Listo" + id);
                    AsyncManager.Parameters["Asistencias"] = Asistencia;
                    AsyncManager.OutstandingOperations.Decrement();
                };
                AsyncManager.OutstandingOperations.Increment();
                cAsistencia.ObtenAsistenciaLinealN(
                     eClockBase.CeC.PersonaID2PersonaDiarioID(PersonaID, FI),
                      eClockBase.CeC.PersonaID2PersonaDiarioID(PersonaID, FF));


                eClockBase.Modelos.Model_PERSONAS DatosPersona = new eClockBase.Modelos.Model_PERSONAS();
                DatosPersona.PERSONA_ID = PersonaID;
                eClockBase.Controladores.Sesion cSesion = new eClockBase.Controladores.Sesion(Sesion);
                cSesion.ObtenDatosEvent += delegate(int Resultado, string Datos)
                {
                    Debug.WriteLine("Asistencias/EmpleadoAsync EC_PERSONAS Listo" + id);
                    if (Resultado == 1)
                    {
                        AsyncManager.Parameters["DatosPersona"] = Newtonsoft.Json.JsonConvert.DeserializeObject<eClockBase.Modelos.Model_PERSONAS>(Datos);
                    }

                    AsyncManager.OutstandingOperations.Decrement();

                };
                AsyncManager.OutstandingOperations.Increment();
                cSesion.ObtenDatos("EC_PERSONAS", "PERSONA_ID", DatosPersona);

                eClockBase.Controladores.Sesion cSesionTurnos = new eClockBase.Controladores.Sesion(Sesion);
                cSesionTurnos.CambioListadoEvent += delegate(string Listado)
                {
                    Debug.WriteLine("Asistencias/EmpleadoAsync Turnos Listo " + id);
                    if (Listado != null)
                    {
                        Debug.WriteLine("Asistencias/EmpleadoAsync Turnos Listo " + id + " " + Listado);
                        List<eClockBase.Controladores.ListadoJson> ListadoJson = eClockBase.Controladores.CeC_ZLib.Json2Object<List<eClockBase.Controladores.ListadoJson>>(Listado);
                        AsyncManager.Parameters["Turnos"] = ListadoJson;

                    }
                    else
                        Debug.WriteLine("Asistencias/EmpleadoAsync Turnos No sirve " + id + " ");
                    AsyncManager.OutstandingOperations.Decrement();
                };
                AsyncManager.OutstandingOperations.Increment();
                cSesionTurnos.ObtenListado("EC_TURNOS", "TURNO_ID", "TURNO_NOMBRE", "", "", "", false, "");

                /*eClockBase.Controladores.Sesion cSesionTipoIncidencias = new eClockBase.Controladores.Sesion(Sesion);
                cSesionTipoIncidencias.CambioListadoEvent += delegate(string Listado)
                {
                    Debug.WriteLine("Asistencias/EmpleadoAsync TipoIncidencias Listo" + id);
                    if (Listado != null)
                    {
                        Debug.WriteLine("Asistencias/EmpleadoAsync TipoIncidencias Listo " + id + " " + Listado);
                        List<eClockBase.Controladores.ListadoJson> ListadoJson = eClockBase.Controladores.CeC_ZLib.Json2Object<List<eClockBase.Controladores.ListadoJson>>(Listado);
                        AsyncManager.Parameters["TipoIncidencias"] = ListadoJson;

                    }
                    else
                        Debug.WriteLine("Asistencias/EmpleadoAsync TipoIncidencias No sirve " + id + " ");
                    AsyncManager.OutstandingOperations.Decrement();
                };
                AsyncManager.OutstandingOperations.Increment();
                string Filtro = "";
                if (BaseModificada.CeC_Sesion.EsKiosco)
                    Filtro = "TIPO_INCIDENCIA_KIOSCO=1";
                cSesionTipoIncidencias.ObtenListado("EC_TIPO_INCIDENCIAS", "TIPO_INCIDENCIA_ID", "TIPO_INCIDENCIA_NOMBRE", "TIPO_INCIDENCIA_ABR", "", "TIPO_INCIDENCIA_COLOR", false, Filtro);
                */
                cSesion = new eClockBase.Controladores.Sesion(Sesion);
                eClockBase.Modelos.Model_TIPO_INCIDENCIAS TipoIncidencia = new eClockBase.Modelos.Model_TIPO_INCIDENCIAS();
                cSesion.ObtenDatosEvent +=
                    delegate(int Resultado, string Datos)
                    {
                        if (Resultado > 0)
                        {
                            List<eClockBase.Modelos.Model_TIPO_INCIDENCIAS> TipoIncidencias = JsonConvert.DeserializeObject<List<eClockBase.Modelos.Model_TIPO_INCIDENCIAS>>(eClockBase.CeC.Json2JsonList(Datos));
                            AsyncManager.Parameters["TipoIncidencias"] = TipoIncidencias;
                        }
                        AsyncManager.OutstandingOperations.Decrement();
                    };
                string Filtro = "";
                if (BaseModificada.CeC_Sesion.EsKiosco)
                    Filtro = "TIPO_INCIDENCIA_KIOSCO=1";
                AsyncManager.OutstandingOperations.Increment();
                cSesion.ObtenDatos("EC_TIPO_INCIDENCIAS", "", TipoIncidencia, "TIPO_INCIDENCIA_NOMBRE", Filtro);
            }
        }


        public ActionResult EmpleadoCompleted(
            eClockBase.Modelos.Model_PERSONAS DatosPersona, List<eClockBase.Modelos.Asistencias.Model_Asistencia_Lineal_N> Asistencias,
            List<eClockBase.Controladores.ListadoJson> Turnos, List<eClockBase.Modelos.Model_TIPO_INCIDENCIAS> TipoIncidencias)
        {
            if (DatosPersona != null)
                ViewBag.Persona = DatosPersona.PERSONA_NOMBRE + "(" + DatosPersona.PERSONA_LINK_ID + ")";
            ViewBag.DatosPersona = DatosPersona;
            ViewBag.Asistencias = Asistencias;
            ViewBag.Turnos = Turnos;
            ViewBag.TipoIncidencias = TipoIncidencias;
            return View();
        }

        public void TurnoSelAsync(string id, string ReturnUrl)
        {
            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            if (!Sesion.EstaLogeado())
            {
                RedirectToAction("Login", "Account");
                return;
            }
            eClockBase.Controladores.Sesion cSesion = new eClockBase.Controladores.Sesion(Sesion);
            //cSesion.obtend
            AsyncManager.Parameters["id"] = id;
            AsyncManager.Parameters["ReturnUrl"] = ReturnUrl;
            cSesion.CambioListadoEvent += delegate(string Listado)
                {
                    if (Listado != null)
                    {
                        List<eClockBase.Controladores.ListadoJson> ListadoJson = eClockBase.Controladores.CeC_ZLib.Json2Object<List<eClockBase.Controladores.ListadoJson>>(Listado);
                        AsyncManager.Parameters["Listado"] = ListadoJson;

                    }
                    AsyncManager.OutstandingOperations.Decrement();
                };
            AsyncManager.OutstandingOperations.Increment();
            cSesion.ObtenListado("EC_TURNOS", "TURNO_ID", "TURNO_NOMBRE", "", "", "", false, "");
        }

        public ActionResult TurnoSelCompleted(string id, string ReturnUrl, List<eClockBase.Controladores.ListadoJson> Listado)
        {
            ViewBag.id = id;
            ViewBag.Listado = Listado;
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        public void AsignaTurnoAsync(string PersonasDiarioIDs, int TurnoID, string Tipo)
        {

            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            if (!Sesion.EstaLogeado())
            {
                RedirectToAction("Login", "Account");
                return;
            }

            eClockBase.Controladores.Turnos Turno = new eClockBase.Controladores.Turnos(Sesion);
            if (BaseModificada.CeC_Sesion.EsKiosco)
            {

            }
            else
            {
                Turno.AsignadoHorarioAPersonaDiarioIDsEvent +=
                    delegate(int NoGuardados)
                    {
                        AsyncManager.Parameters["NoGuardados"] = NoGuardados;
                        AsyncManager.OutstandingOperations.Decrement();
                    };
                Turno.AsignaHorarioPredeterminadoAPersonaDiarioIDsEvent +=
                    delegate(int NoGuardados)
                    {
                        AsyncManager.Parameters["NoGuardados"] = NoGuardados;
                        AsyncManager.OutstandingOperations.Decrement();
                    };

                switch (Tipo)
                {
                    case "0":
                        AsyncManager.OutstandingOperations.Increment();
                        Turno.AsignaHorarioAPersonaDiarioIDs(PersonasDiarioIDs, TurnoID);
                        break;
                    case "1":
                        AsyncManager.OutstandingOperations.Increment();
                        Turno.AsignaHorarioPredeterminadoAPersonaDiarioIDs(PersonasDiarioIDs, TurnoID);
                        break;
                }
            }
        }

        public ActionResult AsignaTurnoCompleted(int NoGuardados)
        {
            return Json(NoGuardados, JsonRequestBehavior.AllowGet);
        }




        public void JustificacionAdvAsync(string PersonasDiarioIDs, int TipoIncidenciaID)
        {
            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Sesion cSesion = new eClockBase.Controladores.Sesion(Sesion);
            eClockBase.Modelos.Model_TIPO_INCIDENCIAS TipoIncidencia = new eClockBase.Modelos.Model_TIPO_INCIDENCIAS();
            TipoIncidencia.TIPO_INCIDENCIA_ID = TipoIncidenciaID;
            cSesion.ObtenDatosEvent +=
                delegate(int Resultado, string Datos)
                {
                    if (Resultado > 0)
                    {
                        eClockBase.Modelos.Model_TIPO_INCIDENCIAS l_TipoIncidencia = JsonConvert.DeserializeObject<eClockBase.Modelos.Model_TIPO_INCIDENCIAS>(Datos);
                        AsyncManager.Parameters["TipoIncidencia"] = l_TipoIncidencia;
                    }
                    AsyncManager.OutstandingOperations.Decrement();
                };
            AsyncManager.OutstandingOperations.Increment();
            cSesion.ObtenDatos("EC_TIPO_INCIDENCIAS", "TIPO_INCIDENCIA_ID", TipoIncidencia, "", "");

        }

        public ActionResult JustificacionAdvCompleted(eClockBase.Modelos.Model_TIPO_INCIDENCIAS TipoIncidencia)
        {
            ViewBag.TipoIncidencia = TipoIncidencia;
            return View();
        }

        public void JustificaAdvAsync(string PersonasDiarioIDs, int TipoIncidenciaID)
        {
            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Sesion cSesion = new eClockBase.Controladores.Sesion(Sesion);
            eClockBase.Modelos.Model_TIPO_INCIDENCIAS TipoIncidencia = new eClockBase.Modelos.Model_TIPO_INCIDENCIAS();
            TipoIncidencia.TIPO_INCIDENCIA_ID = TipoIncidenciaID;
            cSesion.ObtenDatosEvent +=
                delegate(int Resultado, string Datos)
                {
                    if (Resultado > 0)
                    {
                        eClockBase.Modelos.Model_TIPO_INCIDENCIAS l_TipoIncidencia = JsonConvert.DeserializeObject<eClockBase.Modelos.Model_TIPO_INCIDENCIAS>(Datos);
                        AsyncManager.Parameters["TipoIncidencia"] = l_TipoIncidencia;
                    }
                    AsyncManager.OutstandingOperations.Decrement();
                };
            AsyncManager.OutstandingOperations.Increment();
            cSesion.ObtenDatos("EC_TIPO_INCIDENCIAS", "TIPO_INCIDENCIA_ID", TipoIncidencia, "", "");

        }

        public ActionResult JustificaAdvCompleted(eClockBase.Modelos.Model_TIPO_INCIDENCIAS TipoIncidencia)
        {
            string Resultado = "";
            Resultado += "<ul data-role=\"listview\" data-inset=\"true\" data-theme=\"b\"> ";

            List<eClockBase.Modelos.Campos.Model_CampoTexto> CamposTexto = JsonConvert.DeserializeObject<List<eClockBase.Modelos.Campos.Model_CampoTexto>>(TipoIncidencia.TIPO_INCIDENCIA_CAMPOS);
            if (CamposTexto.Count > 0)
                Resultado += "<li data-role=\"list-divider\">Campos</li>";
            foreach (eClockBase.Modelos.Campos.Model_CampoTexto CampoTexto in CamposTexto)
            {
                //string NombreControl = "C" + TipoIncidencia.TIPO_INCIDENCIA_ID + "_" + CampoTexto.Nombre;
                string NombreControl = CampoTexto.Nombre;
                Resultado += "<li data-role=\"fieldcontain\">";
                Resultado += "<label for=\"" + NombreControl + "\">" + CampoTexto.Etiqueta + "</label>";
                switch (CampoTexto.TIPO_DATO_ID)
                {
                    case 1:
                        Resultado += "<input type=\"text\" name=\"" + NombreControl + "\" id=\"" + NombreControl + "\" />";
                        break;
                    case 2:
                        Resultado += "<input type=\"number\" name=\"" + NombreControl + "\" id=\"" + NombreControl + "\" />";
                        break;
                    case 8:
                        Resultado += "<input type=\"checkbox\" name=\"" + NombreControl + "\" id=\"" + NombreControl + "\" />";

                        break;
                    case 5:
                        Resultado += "<input type=\"date\" name=\"" + NombreControl + "\" id=\"" + NombreControl + "\" />";
                        break;
                    case 7:
                        Resultado += "<input type=\"time\" name=\"" + NombreControl + "\" id=\"" + NombreControl + "\" />";

                        break;
                    case 3:
                        Resultado += "<input type=\"number\" name=\"" + NombreControl + "\" id=\"" + NombreControl + "\" />";

                        break;
                    case 10:
                        Resultado += "<select name=\"" + NombreControl + "\" id=\"" + NombreControl + "\"></select>";
                        break;
                    default:
                        Resultado += "<input type=\"text\" name=\"" + NombreControl + "\" id=\"" + NombreControl + "\" value=\"No Soportado\" />";
                        break;
                }
                Resultado += "</li>";

            }
            Resultado += "<li><a href=\"#\" class=\"ui-shadow ui-btn ui-corner-all\" onclick=\"AplicaJustificacion(" + TipoIncidencia.TIPO_INCIDENCIA_ID + ");return false;\">Aplicar Justificación<p>" + TipoIncidencia.TIPO_INCIDENCIA_NOMBRE + "</p> </a></li>";
            Resultado += "</ul>";
            return Json(Resultado, JsonRequestBehavior.AllowGet);
        }

        public void JustificaAsync(string PersonasDiarioIDs, int TipoIncidenciaID, string Comentarios)
        {
            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            if (!Sesion.EstaLogeado())
            {
                RedirectToAction("Login", "Account");
                return;
            }
            string Comentario = "";
            if (Comentarios != null)
                Comentario = Comentarios;
            eClockBase.Controladores.Incidencias cIncidencias = new eClockBase.Controladores.Incidencias(Sesion);

            if (BaseModificada.CeC_Sesion.EsKiosco)
            {

                cIncidencias.SolicitaIncidenciaEvent +=
                    delegate(string Solicitados)
                    {
                        if (Solicitados != null && Solicitados == "OK")
                            AsyncManager.Parameters["NoGuardados"] = 1;
                        else
                            AsyncManager.Parameters["NoGuardados"] = -1;
                        AsyncManager.OutstandingOperations.Decrement();
                    };
                AsyncManager.OutstandingOperations.Increment();
                cIncidencias.SolicitaIncidencia(eClockBase.CeC.PersonasDiarioIDs2Fechas(PersonasDiarioIDs), Comentario, TipoIncidenciaID);

            }
            else
            {
                cIncidencias.AsignaIncidenciaPersonasDiarioEvent +=
                    delegate(int NoGuardados)
                    {
                        AsyncManager.Parameters["NoGuardados"] = NoGuardados;
                        AsyncManager.OutstandingOperations.Decrement();
                    };
                AsyncManager.OutstandingOperations.Increment();
                cIncidencias.AsignaIncidenciaPersonasDiario(TipoIncidenciaID, PersonasDiarioIDs, Comentario);
            }

        }



        public ActionResult JustificaCompleted(int NoGuardados)
        {
            return Json(NoGuardados, JsonRequestBehavior.AllowGet);
        }



        public void TurnoAplAsync(string id, string ReturnUrl, int TurnoID, string Tipo)
        {
            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            if (!Sesion.EstaLogeado())
            {
                RedirectToAction("Login", "Account");
                return;
            }
            AsyncManager.Parameters["ReturnUrl"] = ReturnUrl;
            if (BaseModificada.CeC_Sesion.EsKiosco)
            {
                AsyncManager.Parameters["NoGuardados"] = -1;
            }
            else
            {
                eClockBase.Controladores.Turnos Turno = new eClockBase.Controladores.Turnos(Sesion);
                Turno.AsignadoHorarioAPersonaDiarioIDsEvent +=
                    delegate(int NoGuardados)
                    {
                        AsyncManager.Parameters["NoGuardados"] = NoGuardados;
                        AsyncManager.OutstandingOperations.Decrement();
                    };
                Turno.AsignaHorarioPredeterminadoAPersonaDiarioIDsEvent +=
                    delegate(int NoGuardados)
                    {
                        AsyncManager.Parameters["NoGuardados"] = NoGuardados;
                        AsyncManager.OutstandingOperations.Decrement();
                    };

                switch (Tipo)
                {
                    case "0":
                        AsyncManager.OutstandingOperations.Increment();
                        Turno.AsignaHorarioAPersonaDiarioIDs(id, TurnoID);
                        break;
                    case "1":
                        AsyncManager.OutstandingOperations.Increment();
                        Turno.AsignaHorarioPredeterminadoAPersonaDiarioIDs(id, TurnoID);
                        break;
                }
            }

        }



        public ActionResult TurnoAplCompleted(string ReturnUrl, int NoGuardados)
        {
            ViewBag.NoGuardados = NoGuardados;
            ViewBag.ReturnUrl = ReturnUrl;
            return View();

            if (NoGuardados > 0)
            {
                return Redirect(eClockBase.CeC.Pipe2Amp(ReturnUrl));
            }
            return View();
        }




    }
}
