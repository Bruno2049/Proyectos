using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Diagnostics;

namespace eClockMobile.Controllers
{
    public class HomeController : AsyncController
    {
        public ActionResult Index()
        {
            //HttpCookie authCookie = FormsAuthentication.GetAuthCookie("sar", true);

            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            if (!Sesion.EstaLogeado())
            {
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("eClock", "Home");
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application." + Sesion.SESION_SEGURIDAD;

            return View();
        }

        public ActionResult About(string id)
        {
            Debug.WriteLine("About " + id);
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public void ContactAsync(string id)
        {
            Debug.WriteLine("Contact " + id);
            AsyncManager.OutstandingOperations.Increment();
            AsyncManager.Parameters["id"] = id;
            
            ViewBag.Message = "Your contact page.";
            AsyncManager.OutstandingOperations.Decrement();

            id = eClockMobile.BaseModificada.CeC.HtmlQuita(id);
            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            eClockBase.Modelos.Model_PERSONAS DatosPersona = new eClockBase.Modelos.Model_PERSONAS();
            DatosPersona.PERSONA_ID = eClockBase.CeC.Convierte2Int(id);
            eClockBase.Controladores.Sesion cSesion = new eClockBase.Controladores.Sesion(Sesion);
            cSesion.ObtenDatosEvent += delegate(int Resultado, string Datos)
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

            };
            AsyncManager.OutstandingOperations.Increment();
            cSesion.ObtenDatos("EC_PERSONAS", "PERSONA_ID", DatosPersona);
        }
        public ActionResult ContactCompleted(string id)
        {
            Debug.WriteLine("ContactCompleted " + id);
            return View();
        }
        public void eClockAsync()
        {
            
            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            if (!Sesion.EstaLogeado())
            {
                return;
            }
            eClockBase.Controladores.Sesion cSesion = new eClockBase.Controladores.Sesion(Sesion);
            cSesion.CambioListadoEvent += delegate(string Listado)
            {
                List<eClockBase.Controladores.ListadoJson> ListadoPersonas = null;
                if (Listado != null)
                {
                    ListadoPersonas = Newtonsoft.Json.JsonConvert.DeserializeObject<List<eClockBase.Controladores.ListadoJson>>(Listado);
                }

                AsyncManager.Parameters["ListadoPersonas"] = ListadoPersonas;
                AsyncManager.OutstandingOperations.Decrement();
            };
            AsyncManager.OutstandingOperations.Increment();
            cSesion.ObtenListado("EC_PERSONAS", "PERSONA_ID", "PERSONA_NOMBRE", "PERSONA_LINK_ID", "AGRUPACION_NOMBRE", "", false, "PERSONA_ID IN (SELECT PERSONA_ID FROM EC_PERSONAS, EC_USUARIOS_PERMISOS WHERE USUARIO_ID = @USUARIO_ID AND EC_PERSONAS.SUSCRIPCION_ID = EC_USUARIOS_PERMISOS.SUSCRIPCION_ID AND  AGRUPACION_NOMBRE LIKE USUARIO_PERMISO+'%' AND TIPO_PERSONA_ID = 1)");
        }

        public ActionResult eClockCompleted(List<eClockBase.Controladores.ListadoJson> ListadoPersonas)
        {
            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            if (!Sesion.EstaLogeado())
            {
                return RedirectToAction("Login", "Account");
            }
            
            ViewBag.USUARIO_NOMBRE = Sesion.Mdl_Sesion.USUARIO_NOMBRE;
            ViewBag.PERSONA_ID = Sesion.Mdl_Sesion.PERSONA_ID;
            if (ListadoPersonas != null)
            {
                ViewBag.NoEmpleados = ListadoPersonas.Count;
                ViewBag.Arbol = Listado2Arbol(ListadoPersonas);
            }
            else
                ViewBag.NoEmpleados = 0;
            return View();
        }

        public void AcordionAsync()
        {
            AsyncManager.OutstandingOperations.Increment();
            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Sesion cSesion = new eClockBase.Controladores.Sesion(Sesion);
            cSesion.CambioListadoEvent += delegate(string Listado)
            {
                List<eClockBase.Controladores.ListadoJson> ListadoPersonas = null;
                if (Listado != null)
                {
                    ListadoPersonas = Newtonsoft.Json.JsonConvert.DeserializeObject<List<eClockBase.Controladores.ListadoJson>>(Listado);
                }

                AsyncManager.Parameters["ListadoPersonas"] = ListadoPersonas;
                AsyncManager.OutstandingOperations.Decrement();
            };
            cSesion.ObtenListado("EC_PERSONAS", "PERSONA_ID", "PERSONA_NOMBRE", "PERSONA_LINK_ID", "AGRUPACION_NOMBRE", "", false, "TIPO_PERSONA_ID = 1");
        }

        public ActionResult AcordionCompleted(List<eClockBase.Controladores.ListadoJson> ListadoPersonas)
        {
            ViewBag.Arbol = Listado2Arbol(ListadoPersonas);
            return View();
        }


        public eClockBase.Modelos.Personas.Model_Arbol BuscaItem(eClockBase.Modelos.Personas.Model_Arbol Arbol, string Header)
        {
            if (Arbol == null || Arbol.Agrupaciones == null || Arbol.Agrupaciones.Count < 1)
                return null;
            foreach (eClockBase.Modelos.Personas.Model_Arbol Agrupacion in Arbol.Agrupaciones)
            {
                if (Agrupacion.Agrupacion == Header)
                    return Agrupacion;
            }
            return null;
        }

        public eClockBase.Modelos.Personas.Model_Arbol Listado2Arbol(List<eClockBase.Controladores.ListadoJson> ListadoPersonas)
        {
            if (ListadoPersonas == null)
                return null;
            eClockBase.Modelos.Personas.Model_Arbol Arbol = new eClockBase.Modelos.Personas.Model_Arbol();
            foreach (eClockBase.Controladores.ListadoJson ElementoDato in ListadoPersonas)
            {

                eClockBase.Modelos.Personas.Model_ArbolPersona Persona = new eClockBase.Modelos.Personas.Model_ArbolPersona();
                Persona.PERSONA_ID = eClockBase.CeC.Convierte2Int(ElementoDato.Llave);
                Persona.PERSONA_LINK_ID = eClockBase.CeC.Convierte2Int(ElementoDato.Adicional);
                Persona.PERSONA_NOMBRE = eClockBase.CeC.Convierte2String(ElementoDato.Nombre);
                Persona.AGRUPACION_NOMBRE = eClockBase.CeC.Convierte2String(ElementoDato.Descripcion);

                string[] Separados = Persona.AGRUPACION_NOMBRE.Split(new char[] { '|' });
                string Agrup = "";
                eClockBase.Modelos.Personas.Model_Arbol ArbolActual = Arbol;
                foreach (string Separacion in Separados)
                {
                    Agrup += Separacion + "|";
                    if (Separacion != null && Separacion.Length > 0)
                    {
                        eClockBase.Modelos.Personas.Model_Arbol ArbolSeleccionado = BuscaItem(ArbolActual, Separacion);
                        if (ArbolSeleccionado == null)
                        {
                            ArbolSeleccionado = new eClockBase.Modelos.Personas.Model_Arbol();
                            ArbolSeleccionado.Agrupacion = Separacion;
                            ArbolSeleccionado.AGRUPACION_NOMBRE = Agrup;
                            ArbolActual.Agrupaciones.Add(ArbolSeleccionado);
                        }
                        ArbolActual = ArbolSeleccionado;
                    }
                }

                ArbolActual.Personas.Add(Persona);
            }
            return Arbol;
        }

        [HttpPost]
        public ActionResult Empleado(string id, DateTime? FechaInicial, DateTime? FechaFinal, string Accion)
        {
            return RedirectToAction("Empleado", "Asistencias", new { id = id, FechaInicial = FechaInicial, FechaFinal = FechaFinal });
        }
        
        public void EmpleadoAsync(string id)
        {
            Debug.WriteLine("EmpleadoAsync " + id);
            id = eClockMobile.BaseModificada.CeC.HtmlQuita(id);
            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            eClockBase.Modelos.Model_PERSONAS DatosPersona = new eClockBase.Modelos.Model_PERSONAS();
            DatosPersona.PERSONA_ID = eClockBase.CeC.Convierte2Int(id);
            eClockBase.Controladores.Sesion cSesion = new eClockBase.Controladores.Sesion(Sesion);
            cSesion.ObtenDatosEvent += delegate(int Resultado, string Datos)
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

                };
            AsyncManager.OutstandingOperations.Increment();
            cSesion.ObtenDatos("EC_PERSONAS", "PERSONA_ID", DatosPersona);
        }



        public ActionResult EmpleadoCompleted(eClockBase.Modelos.Model_PERSONAS DatosPersona)
        {
            ViewBag.Title = "Prueba";
            Debug.WriteLine("EmpleadoCompleted " );
            if (DatosPersona != null)
                ViewBag.Persona = DatosPersona.PERSONA_NOMBRE + "(" + DatosPersona.PERSONA_LINK_ID + ")";
            ViewBag.DatosPersona = DatosPersona;
            return View();
        }

        public void AgrupacionAsync(string id)
        {
            if (id == null || id == "")
                id = "|";
            AsyncManager.Parameters["Agrupacion"] = id;
        }
        public ActionResult AgrupacionCompleted(string Agrupacion)
        {


            ViewBag.Agrupacion = eClockMobile.BaseModificada.CeC.HtmlQuita(Agrupacion);
            return View();
        }

        public void KioscoAsync()
        {
            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Sesion cSesion = new eClockBase.Controladores.Sesion(Sesion);
            cSesion.ObtenDatosPersonaEvent +=
                delegate(eClockBase.Modelos.Sesion.Model_DatosPersona Datos)
                {
                    AsyncManager.Parameters["Datos"] = Datos;
                    AsyncManager.OutstandingOperations.Decrement();
                };
            AsyncManager.OutstandingOperations.Increment();
            cSesion.ObtenDatosPersona();

        }

        public ActionResult KioscoCompleted(eClockBase.Modelos.Sesion.Model_DatosPersona Datos)
        {
            
            ViewBag.Datos = Datos;
            return View();
        }





    }
}
