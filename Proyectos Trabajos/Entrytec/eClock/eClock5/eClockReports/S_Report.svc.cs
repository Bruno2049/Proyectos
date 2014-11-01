using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports.Engine;
using Newtonsoft.Json;

namespace eClockReports
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class S_Report
    {
        // Para usar HTTP GET, agregue el atributo [WebGet]. (El valor predeterminado de ResponseFormat es WebMessageFormat.Json)
        // Para crear una operación que devuelva XML,
        //     agregue [WebGet(ResponseFormat=WebMessageFormat.Xml)]
        //     e incluya la siguiente línea en el cuerpo de la operación:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract]
        public void DoWork()
        {
            // Agregue aquí la implementación de la operación
            return;
        }
        public static byte[] ReadFully(System.IO.Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public List<eClockBase.Modelos.Asistencias.Reporte_Asistencias> DeserealizaReporte_Asistencias(string DatosJson)
        {
            List<eClockBase.Modelos.Asistencias.Reporte_Asistencias> Datos = JsonConvert.DeserializeObject<List<eClockBase.Modelos.Asistencias.Reporte_Asistencias>>(DatosJson);
            foreach (eClockBase.Modelos.Asistencias.Reporte_Asistencias Dato in Datos)
            {
                ///Quita el alfa
                Dato.TURNO_COLOR = Dato.TURNO_COLOR & 16777215;
                Dato.INCIDENCIA_COLOR = Dato.INCIDENCIA_COLOR & 16777215;
            }
            return Datos;
        }

        public List<eClockBase.Modelos.Asistencias.Reporte_AsistenciaAbr31> DeserealizaReporte_AsistenciaAbr31(string DatosJson)
        {
            List<eClockBase.Modelos.Asistencias.Reporte_AsistenciaAbr31> Datos = JsonConvert.DeserializeObject<List<eClockBase.Modelos.Asistencias.Reporte_AsistenciaAbr31>>(DatosJson);
            foreach (eClockBase.Modelos.Asistencias.Reporte_AsistenciaAbr31 Dato in Datos)
            {
                ///Quita el alfa
                Dato.IC0 = Dato.IC0 & 16777215;
                Dato.IC1 = Dato.IC1 & 16777215;
                Dato.IC2 = Dato.IC2 & 16777215;
                Dato.IC3 = Dato.IC3 & 16777215;
                Dato.IC4 = Dato.IC4 & 16777215;
                Dato.IC5 = Dato.IC5 & 16777215;
                Dato.IC6 = Dato.IC6 & 16777215;
                Dato.IC7 = Dato.IC7 & 16777215;
                Dato.IC8 = Dato.IC8 & 16777215;
                Dato.IC9 = Dato.IC9 & 16777215;
                Dato.IC10 = Dato.IC10 & 16777215;
                Dato.IC11 = Dato.IC11 & 16777215;
                Dato.IC12 = Dato.IC12 & 16777215;
            http://localhost:51840/S_Report.svc.cs
                Dato.IC13 = Dato.IC13 & 16777215;
                Dato.IC14 = Dato.IC14 & 16777215;
                Dato.IC15 = Dato.IC15 & 16777215;
                Dato.IC16 = Dato.IC16 & 16777215;
                Dato.IC17 = Dato.IC17 & 16777215;
                Dato.IC18 = Dato.IC18 & 16777215;
                Dato.IC19 = Dato.IC19 & 16777215;
                Dato.IC20 = Dato.IC20 & 16777215;
                Dato.IC21 = Dato.IC21 & 16777215;
                Dato.IC22 = Dato.IC22 & 16777215;
                Dato.IC23 = Dato.IC23 & 16777215;
                Dato.IC24 = Dato.IC24 & 16777215;
                Dato.IC25 = Dato.IC25 & 16777215;
                Dato.IC26 = Dato.IC26 & 16777215;
                Dato.IC27 = Dato.IC27 & 16777215;
                Dato.IC28 = Dato.IC28 & 16777215;
                Dato.IC29 = Dato.IC29 & 16777215;
                Dato.IC30 = Dato.IC30 & 16777215;


            }
            return Datos;
        }

        public List<eClockBase.Modelos.Asistencias.Reporte_Asistencia31> DeserealizaReporte_Asistencia31(string DatosJson)
        {
            List<eClockBase.Modelos.Asistencias.Reporte_Asistencia31> Datos = JsonConvert.DeserializeObject<List<eClockBase.Modelos.Asistencias.Reporte_Asistencia31>>(DatosJson);
            foreach (eClockBase.Modelos.Asistencias.Reporte_Asistencia31 Dato in Datos)
            {
                ///Quita el alfa
                Dato.IC0 = Dato.IC0 & 16777215;
                Dato.IC1 = Dato.IC1 & 16777215;
                Dato.IC2 = Dato.IC2 & 16777215;
                Dato.IC3 = Dato.IC3 & 16777215;
                Dato.IC4 = Dato.IC4 & 16777215;
                Dato.IC5 = Dato.IC5 & 16777215;
                Dato.IC6 = Dato.IC6 & 16777215;
                Dato.IC7 = Dato.IC7 & 16777215;
                Dato.IC8 = Dato.IC8 & 16777215;
                Dato.IC9 = Dato.IC9 & 16777215;
                Dato.IC10 = Dato.IC10 & 16777215;
                Dato.IC11 = Dato.IC11 & 16777215;
                Dato.IC12 = Dato.IC12 & 16777215;
                Dato.IC13 = Dato.IC13 & 16777215;
                Dato.IC14 = Dato.IC14 & 16777215;
                Dato.IC15 = Dato.IC15 & 16777215;
                Dato.IC16 = Dato.IC16 & 16777215;
                Dato.IC17 = Dato.IC17 & 16777215;
                Dato.IC18 = Dato.IC18 & 16777215;
                Dato.IC19 = Dato.IC19 & 16777215;
                Dato.IC20 = Dato.IC20 & 16777215;
                Dato.IC21 = Dato.IC21 & 16777215;
                Dato.IC22 = Dato.IC22 & 16777215;
                Dato.IC23 = Dato.IC23 & 16777215;
                Dato.IC24 = Dato.IC24 & 16777215;
                Dato.IC25 = Dato.IC25 & 16777215;
                Dato.IC26 = Dato.IC26 & 16777215;
                Dato.IC27 = Dato.IC27 & 16777215;
                Dato.IC28 = Dato.IC28 & 16777215;
                Dato.IC29 = Dato.IC29 & 16777215;
                Dato.IC30 = Dato.IC30 & 16777215;

                Dato.TC0 = Dato.TC0 & 16777215;
                Dato.TC1 = Dato.TC1 & 16777215;
                Dato.TC2 = Dato.TC2 & 16777215;
                Dato.TC3 = Dato.TC3 & 16777215;
                Dato.TC4 = Dato.TC4 & 16777215;
                Dato.TC5 = Dato.TC5 & 16777215;
                Dato.TC6 = Dato.TC6 & 16777215;
                Dato.TC7 = Dato.TC7 & 16777215;
                Dato.TC8 = Dato.TC8 & 16777215;
                Dato.TC9 = Dato.TC9 & 16777215;
                Dato.TC10 = Dato.TC10 & 16777215;
                Dato.TC11 = Dato.TC11 & 16777215;
                Dato.TC12 = Dato.TC12 & 16777215;
                Dato.TC13 = Dato.TC13 & 16777215;
                Dato.TC14 = Dato.TC14 & 16777215;
                Dato.TC15 = Dato.TC15 & 16777215;
                Dato.TC16 = Dato.TC16 & 16777215;
                Dato.TC17 = Dato.TC17 & 16777215;
                Dato.TC18 = Dato.TC18 & 16777215;
                Dato.TC19 = Dato.TC19 & 16777215;
                Dato.TC20 = Dato.TC20 & 16777215;
                Dato.TC21 = Dato.TC21 & 16777215;
                Dato.TC22 = Dato.TC22 & 16777215;
                Dato.TC23 = Dato.TC23 & 16777215;
                Dato.TC24 = Dato.TC24 & 16777215;
                Dato.TC25 = Dato.TC25 & 16777215;
                Dato.TC26 = Dato.TC26 & 16777215;
                Dato.TC27 = Dato.TC27 & 16777215;
                Dato.TC28 = Dato.TC28 & 16777215;
                Dato.TC29 = Dato.TC29 & 16777215;
                Dato.TC30 = Dato.TC30 & 16777215;

            }
            return Datos;
        }

        public List<eClockBase.Modelos.HorasExtras.Reporte_Semanal> DeserealizaReporte_Semanal(string DatosJson)
        {
            List<eClockBase.Modelos.HorasExtras.Reporte_Semanal> Datos = JsonConvert.DeserializeObject<List<eClockBase.Modelos.HorasExtras.Reporte_Semanal>>(DatosJson);
            foreach (eClockBase.Modelos.HorasExtras.Reporte_Semanal Dato in Datos)
            {

            }
            return Datos;
        }

        //******************************************************************************************************
        public eClockReports.Modelos.Model_ReciboNomina Nomina()
        {
            eClockReports.Modelos.Model_ReciboNomina ReciboNomina = new Modelos.Model_ReciboNomina();
            eClockReports.Modelos.Model_Percepciones Percepciones = new Modelos.Model_Percepciones();
            eClockReports.Modelos.Model_Deducciones Deducciones = new Modelos.Model_Deducciones();
            eClockReports.Modelos.Model_Nomina Nomina = new Modelos.Model_Nomina();

            Percepciones.ID = "1";
            Percepciones.PersonaID = 1;
            Percepciones.Nombre = "Salario Semanal";
            Percepciones.Unidad = 25;
            Percepciones.Importe = 50;

            ReciboNomina.Percepciones.Add(Percepciones);
            ReciboNomina.Percepciones.Add(Percepciones);
            ReciboNomina.Percepciones.Add(Percepciones);
            ReciboNomina.Percepciones.Add(Percepciones);
            ReciboNomina.Percepciones.Add(Percepciones);

            Deducciones.PersonaID = 1;
            Deducciones.ID = "1";
            Deducciones.Nombre = "Impuesto Sobre Renta";
            Deducciones.Unidad = 25;
            Deducciones.Importe = 50;
            ReciboNomina.Deducciones.Add(Deducciones);
            ReciboNomina.Deducciones.Add(Deducciones);
            ReciboNomina.Deducciones.Add(Deducciones);

            Nomina.PersonaID = 1;
            Nomina.PersonaNombre = "Moises Levi Trejo";
            ReciboNomina.Nomina.Add(Nomina);
            return ReciboNomina;
        }

        public eClockReports.Modelos.Model_Percepciones Percepciones()
        {
            eClockReports.Modelos.Model_ReciboNomina ReciboNomina = new Modelos.Model_ReciboNomina();
            eClockReports.Modelos.Model_Percepciones Percepciones = new Modelos.Model_Percepciones();

            Percepciones.ID = "1";
            Percepciones.PersonaID = 1;
            Percepciones.Nombre = "Salario Semanal";
            Percepciones.Unidad = 25;
            Percepciones.Importe = 50;

            ReciboNomina.Percepciones.Add(Percepciones);

            return Percepciones;
        }
        //******************************************************************************************************
        public List<eClockBase.Modelos.Actividades.Reporte_NoInscripciones> DeserealizaReporte_TotalInscripcionesenActividades(string DatosJson)
        {
            List<eClockBase.Modelos.Actividades.Reporte_NoInscripciones> Datos = JsonConvert.DeserializeObject<List<eClockBase.Modelos.Actividades.Reporte_NoInscripciones>>(DatosJson);
            foreach (eClockBase.Modelos.Actividades.Reporte_NoInscripciones Dato in Datos)
            {

            }
            return Datos;
        }
        //******************************************************************************************************
        public List<eClockBase.Modelos.Tramites.Reporte_NoTramites> DeserealizaReporte_TotalSolicituddeTramites(string DatosJson)
        {
            List<eClockBase.Modelos.Tramites.Reporte_NoTramites> Datos = JsonConvert.DeserializeObject<List<eClockBase.Modelos.Tramites.Reporte_NoTramites>>(DatosJson);
            foreach (eClockBase.Modelos.Tramites.Reporte_NoTramites Dato in Datos)
            {

            }
            return Datos;
        }
        //******************************************************************************************************
        public List<eClockBase.Modelos.Actividades.Reporte_Detalle> DeserealizaReporte__Detalle_Actividades(string DatosJson)
        {
            List<eClockBase.Modelos.Actividades.Reporte_Detalle> Datos = JsonConvert.DeserializeObject<List<eClockBase.Modelos.Actividades.Reporte_Detalle>>(DatosJson);
            foreach (eClockBase.Modelos.Actividades.Reporte_Detalle Dato in Datos)
            {

            }
            return Datos;
        }
        //******************************************************************************************************
        public List<eClockBase.Modelos.Tramites.Reporte_Detalle> DeserealizaReporte__Detalle_Tramites(string DatosJson)
        {
            List<eClockBase.Modelos.Tramites.Reporte_Detalle> Datos = JsonConvert.DeserializeObject<List<eClockBase.Modelos.Tramites.Reporte_Detalle>>(DatosJson);
            foreach (eClockBase.Modelos.Tramites.Reporte_Detalle Dato in Datos)
            {

            }
            return Datos;
        }
        //******************************************************************************************************
        public List<eClockBase.Modelos.HorasExtras.Reporte_Semanal_HET> DeserealizaReporte_Semanal_HET(string DatosJson)
        {
            List<eClockBase.Modelos.HorasExtras.Reporte_Semanal_HET> Datos = JsonConvert.DeserializeObject<List<eClockBase.Modelos.HorasExtras.Reporte_Semanal_HET>>(DatosJson);
            foreach (eClockBase.Modelos.HorasExtras.Reporte_Semanal_HET Dato in Datos)
            {
                Dato.IC0 = Dato.IC0 & 16777215;
                Dato.IC1 = Dato.IC1 & 16777215;
                Dato.IC2 = Dato.IC2 & 16777215;
                Dato.IC3 = Dato.IC3 & 16777215;
                Dato.IC4 = Dato.IC4 & 16777215;
                Dato.IC5 = Dato.IC5 & 16777215;
                Dato.IC6 = Dato.IC6 & 16777215;
            }
            return Datos;
        }
        //******************************************************************************************************
        public List<eClockBase.Modelos.Nomina.Reporte_NominaFechaConsulta> DeserealizaReporte_Fecha_Consultada(string DatosJson)
        {
            List<eClockBase.Modelos.Nomina.Reporte_NominaFechaConsulta> Datos = JsonConvert.DeserializeObject<List<eClockBase.Modelos.Nomina.Reporte_NominaFechaConsulta>>(DatosJson);
            foreach (eClockBase.Modelos.Nomina.Reporte_NominaFechaConsulta Dato in Datos)
            {

            }
            return Datos;
        }
        //******************************************************************************************************
        public List<eClockBase.Modelos.Nomina.Reporte_SinConsultar> DeserealizaReporte_SinConsultar(string DatosJson)
        {
            List<eClockBase.Modelos.Nomina.Reporte_SinConsultar> Datos = JsonConvert.DeserializeObject<List<eClockBase.Modelos.Nomina.Reporte_SinConsultar>>(DatosJson);
            foreach (eClockBase.Modelos.Nomina.Reporte_SinConsultar Dato in Datos)
            {

            }
            return Datos;
        }
        //******************************************************************************************************
        public List<eClockBase.Modelos.Solicitudes.Model_ReporteSolicitudVacaciones> DeserealizaReporte_SolicitudVacaciones(string DatosJson)
        {
            List<eClockBase.Modelos.Solicitudes.Model_ReporteSolicitudVacaciones> Datos = JsonConvert.DeserializeObject<List<eClockBase.Modelos.Solicitudes.Model_ReporteSolicitudVacaciones>>(DatosJson);
            foreach (eClockBase.Modelos.Solicitudes.Model_ReporteSolicitudVacaciones Dato in Datos)
            {

            }
            return Datos;
        }
        private bool CambiaTexto(ReportClass Reporte, string Nombre, string Texto)
        {
            try
            {

                CrystalDecisions.CrystalReports.Engine.TextObject txtReportHeader;
                txtReportHeader = Reporte.ReportDefinition.ReportObjects[Nombre] as CrystalDecisions.CrystalReports.Engine.TextObject;
                txtReportHeader.Text = Texto;
                return true;
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        public string DiaDeLaSemana(DateTime DF)
        {
            switch (DF.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    {
                        return "Lunes";
                    }
                    break;

                case DayOfWeek.Tuesday:
                    {
                        return "Martes";
                    }
                    break;

                case DayOfWeek.Wednesday:
                    {
                        return "Miercoles";
                    }
                    break;

                case DayOfWeek.Thursday:
                    {
                        return "Jueves";
                    }
                    break;
                case DayOfWeek.Friday:
                    {
                        return "Viernes";
                    }
                    break;
                
                case DayOfWeek.Saturday:
                    {
                        return "Sabado";
                    }
                    break;

                case DayOfWeek.Sunday:
                    {
                        return "Domingo";
                    }
                    break;

                default:
                    return null;
                    break;
            }            
        }

        public string DiaDeLaSemanaMen(DateTime Fecha)
        { 
            switch (Fecha.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    {
                        return "Lu";
                    }
                    break;

                case DayOfWeek.Tuesday:
                    {
                        return "Ma";
                    }
                    break;

                case DayOfWeek.Wednesday:
                    {
                        return "Mi";
                    }
                    break;

                case DayOfWeek.Thursday:
                    {
                        return "Ju";
                    }
                    break;
                case DayOfWeek.Friday:
                    {
                        return "Vi";
                    }
                    break;
                
                case DayOfWeek.Saturday:
                    {
                        return "Sa";
                    }
                    break;

                case DayOfWeek.Sunday:
                    {
                        return "Do";
                    }
                    break;

                default:
                    return null;
                    break;
            }                    
        }


        public bool Muestrafechas(ReportClass Reporte,DateTime DFS)
        {
            try
            {

            CambiaTexto(Reporte, "Lbl_Dia1", DiaDeLaSemana(DFS) + "\n" + DFS.ToString("dd/MM/yyyy"));
            CambiaTexto(Reporte, "Lbl_Dia2", DiaDeLaSemana(DFS.AddDays(1)) + "\n" + DFS.AddDays(1).ToString("dd/MM/yyyy"));
            CambiaTexto(Reporte, "Lbl_Dia3", DiaDeLaSemana(DFS.AddDays(2)) + "\n" + DFS.AddDays(2).ToString("dd/MM/yyyy"));
            CambiaTexto(Reporte, "Lbl_Dia4", DiaDeLaSemana(DFS.AddDays(3)) + "\n" + DFS.AddDays(3).ToString("dd/MM/yyyy"));
            CambiaTexto(Reporte, "Lbl_Dia5", DiaDeLaSemana(DFS.AddDays(4)) + "\n" + DFS.AddDays(4).ToString("dd/MM/yyyy"));
            CambiaTexto(Reporte, "Lbl_Dia6", DiaDeLaSemana(DFS.AddDays(5)) + "\n" + DFS.AddDays(5).ToString("dd/MM/yyyy"));
            CambiaTexto(Reporte, "Lbl_Dia7", DiaDeLaSemana(DFS.AddDays(6)) + "\n" + DFS.AddDays(6).ToString("dd/MM/yyyy"));
            }

            catch(Exception ex)
            {
            }

            return true;
        }




        [OperationContract]
        public string ObtenReporte(string SesionSeguridad, string ReporteClase, string DatosJson, string Parametros, string Configuracion, int FormatoRepID)
        {

            try
            {
                eClockReports.BaseModificada.CeC_Main.Iniciar();
                DatosJson = eClockBase.Controladores.CeC_ZLib.ZJson2Json(DatosJson);
                ReportClass Reporte = null;
                object ModeloDatos = null;
                switch (ReporteClase)
                {
                    case "eClockReports.Reportes.Asistencia.Lineal":
                        Reporte = new eClockReports.Reportes.Asistencia.Lineal();
                        ModeloDatos = DeserealizaReporte_Asistencias(DatosJson);
                        break;
                    case "eClockReports.Reportes.Asistencia.DetalleCFirma":
                        Reporte = new eClockReports.Reportes.Asistencia.DetalleCFirma();
                        ModeloDatos = DeserealizaReporte_Asistencias(DatosJson);
                        break;
                    case "eClockReports.Reportes.Asistencia.DetalleCompleto":
                        Reporte = new eClockReports.Reportes.Asistencia.DetalleCompleto();
                        ModeloDatos = DeserealizaReporte_Asistencias(DatosJson);
                        break;
                    case "eClockReports.Reportes.Asistencia.DetalleSinComida":
                        Reporte = new eClockReports.Reportes.Asistencia.DetalleSinComida();
                        ModeloDatos = DeserealizaReporte_Asistencias(DatosJson);
                        break;
                    case "eClockReports.Reportes.Asistencia.DetalleSFirma":
                        Reporte = new eClockReports.Reportes.Asistencia.DetalleSFirma();
                        ModeloDatos = DeserealizaReporte_Asistencias(DatosJson);
                        break; /**/
                    case "eClockReports.Reportes.Asistencia.ReporteAsistenciaSimple":
                        Reporte = new eClockReports.Reportes.Asistencia.ReporteAsistenciaSimple();
                        ModeloDatos = DeserealizaReporte_Asistencias(DatosJson);
                        break;
                    case "eClockReports.Reportes.Asistencia.Mensual":
                        Reporte = new eClockReports.Reportes.Asistencia.Mensual();

                        List<eClockBase.Modelos.Asistencias.Reporte_AsistenciaAbr31> DatoMens = DeserealizaReporte_AsistenciaAbr31(DatosJson);
                        DateTime DFMens = eClockBase.CeC.PersonaDiarioID2Fecha(DatoMens[0].ID);
                        CambiaTexto(Reporte, "Lbl_Dia1", DiaDeLaSemanaMen(DFMens) + "\n" + DFMens.ToString("dd"));
                        CambiaTexto(Reporte, "Lbl_Dia2", DiaDeLaSemanaMen(DFMens.AddDays(1)) + "\n" + DFMens.AddDays(1).ToString("dd"));
                        CambiaTexto(Reporte, "Lbl_Dia3", DiaDeLaSemanaMen(DFMens.AddDays(2)) + "\n" + DFMens.AddDays(2).ToString("dd"));
                        CambiaTexto(Reporte, "Lbl_Dia4", DiaDeLaSemanaMen(DFMens.AddDays(3)) + "\n" + DFMens.AddDays(3).ToString("dd"));
                        CambiaTexto(Reporte, "Lbl_Dia5", DiaDeLaSemanaMen(DFMens.AddDays(4)) + "\n" + DFMens.AddDays(4).ToString("dd"));
                        CambiaTexto(Reporte, "Lbl_Dia6", DiaDeLaSemanaMen(DFMens.AddDays(5)) + "\n" + DFMens.AddDays(5).ToString("dd"));
                        CambiaTexto(Reporte, "Lbl_Dia7", DiaDeLaSemanaMen(DFMens.AddDays(6)) + "\n" + DFMens.AddDays(6).ToString("dd"));
                        CambiaTexto(Reporte, "Lbl_Dia8", DiaDeLaSemanaMen(DFMens.AddDays(7)) + "\n" + DFMens.AddDays(7).ToString("dd"));
                        CambiaTexto(Reporte, "Lbl_Dia9", DiaDeLaSemanaMen(DFMens.AddDays(8)) + "\n" + DFMens.AddDays(8).ToString("dd"));
                        CambiaTexto(Reporte, "Lbl_Dia10", DiaDeLaSemanaMen(DFMens.AddDays(9)) + "\n" + DFMens.AddDays(9).ToString("dd"));
                        CambiaTexto(Reporte, "Lbl_Dia11", DiaDeLaSemanaMen(DFMens.AddDays(10)) + "\n" + DFMens.AddDays(10).ToString("dd"));
                        CambiaTexto(Reporte, "Lbl_Dia12", DiaDeLaSemanaMen(DFMens.AddDays(11)) + "\n" + DFMens.AddDays(11).ToString("dd"));
                        CambiaTexto(Reporte, "Lbl_Dia13", DiaDeLaSemanaMen(DFMens.AddDays(12)) + "\n" + DFMens.AddDays(12).ToString("dd"));
                        CambiaTexto(Reporte, "Lbl_Dia14", DiaDeLaSemanaMen(DFMens.AddDays(13)) + "\n" + DFMens.AddDays(13).ToString("dd"));
                        CambiaTexto(Reporte, "Lbl_Dia15", DiaDeLaSemanaMen(DFMens.AddDays(14)) + "\n" + DFMens.AddDays(14).ToString("dd"));
                        CambiaTexto(Reporte, "Lbl_Dia16", DiaDeLaSemanaMen(DFMens.AddDays(15)) + "\n" + DFMens.AddDays(15).ToString("dd"));
                        CambiaTexto(Reporte, "Lbl_Dia17", DiaDeLaSemanaMen(DFMens.AddDays(16)) + "\n" + DFMens.AddDays(16).ToString("dd"));
                        CambiaTexto(Reporte, "Lbl_Dia18", DiaDeLaSemanaMen(DFMens.AddDays(17)) + "\n" + DFMens.AddDays(17).ToString("dd"));
                        CambiaTexto(Reporte, "Lbl_Dia19", DiaDeLaSemanaMen(DFMens.AddDays(18)) + "\n" + DFMens.AddDays(18).ToString("dd"));
                        CambiaTexto(Reporte, "Lbl_Dia20", DiaDeLaSemanaMen(DFMens.AddDays(19)) + "\n" + DFMens.AddDays(19).ToString("dd"));
                        CambiaTexto(Reporte, "Lbl_Dia21", DiaDeLaSemanaMen(DFMens.AddDays(20)) + "\n" + DFMens.AddDays(20).ToString("dd"));
                        CambiaTexto(Reporte, "Lbl_Dia22", DiaDeLaSemanaMen(DFMens.AddDays(21)) + "\n" + DFMens.AddDays(21).ToString("dd"));
                        CambiaTexto(Reporte, "Lbl_Dia23", DiaDeLaSemanaMen(DFMens.AddDays(22)) + "\n" + DFMens.AddDays(22).ToString("dd"));
                        CambiaTexto(Reporte, "Lbl_Dia24", DiaDeLaSemanaMen(DFMens.AddDays(23)) + "\n" + DFMens.AddDays(23).ToString("dd"));
                        CambiaTexto(Reporte, "Lbl_Dia25", DiaDeLaSemanaMen(DFMens.AddDays(24)) + "\n" + DFMens.AddDays(24).ToString("dd"));
                        CambiaTexto(Reporte, "Lbl_Dia26", DiaDeLaSemanaMen(DFMens.AddDays(25)) + "\n" + DFMens.AddDays(25).ToString("dd"));
                        CambiaTexto(Reporte, "Lbl_Dia27", DiaDeLaSemanaMen(DFMens.AddDays(26)) + "\n" + DFMens.AddDays(26).ToString("dd"));
                        CambiaTexto(Reporte, "Lbl_Dia28", DiaDeLaSemanaMen(DFMens.AddDays(27)) + "\n" + DFMens.AddDays(27).ToString("dd"));
                        CambiaTexto(Reporte, "Lbl_Dia29", DiaDeLaSemanaMen(DFMens.AddDays(28)) + "\n" + DFMens.AddDays(28).ToString("dd"));
                        CambiaTexto(Reporte, "Lbl_Dia30", DiaDeLaSemanaMen(DFMens.AddDays(29)) + "\n" + DFMens.AddDays(29).ToString("dd"));
                        CambiaTexto(Reporte, "Lbl_Dia31", DiaDeLaSemanaMen(DFMens.AddDays(30)) + "\n" + DFMens.AddDays(30).ToString("dd"));
                        ModeloDatos = DatoMens;

                        ModeloDatos = DeserealizaReporte_AsistenciaAbr31(DatosJson);
                        break;
                    case "eClockReports.Reportes.Asistencia.Semanal":
                        Reporte = new eClockReports.Reportes.Asistencia.Semanal();
                        List<eClockBase.Modelos.Asistencias.Reporte_Asistencia31> DatosSemanal = DeserealizaReporte_Asistencia31(DatosJson);

                        DateTime DFS = eClockBase.CeC.PersonaDiarioID2Fecha(DatosSemanal[0].ID);
                        Muestrafechas(Reporte, DFS);
                        ModeloDatos = DatosSemanal;
                        break;
                    case "eClockReports.Reportes.Asistencia.SemanalTurno":
                        Reporte = new eClockReports.Reportes.Asistencia.SemanalTurno();
                        List<eClockBase.Modelos.Asistencias.Reporte_Asistencia31> DatosSemanalT = DeserealizaReporte_Asistencia31(DatosJson);

                        DateTime DFST = eClockBase.CeC.PersonaDiarioID2Fecha(DatosSemanalT[0].ID);
                        Muestrafechas(Reporte, DFST);
                        ModeloDatos = DatosSemanalT;
                        break;
                    case "eClockReports.Reportes.HorasExtras.Semanal":
                        Reporte = new eClockReports.Reportes.HorasExtras.Semanal();
                        List<eClockBase.Modelos.HorasExtras.Reporte_Semanal> DatosSemanalHE = DeserealizaReporte_Semanal(DatosJson);

                        DateTime DFHE = eClockBase.CeC.PersonaDiarioID2Fecha(DatosSemanalHE[0].ID);
                        Muestrafechas(Reporte, DFHE);
                        ModeloDatos = DatosSemanalHE;
                        break;
                    //*************************************+
                    case "eClockReports.Reportes.Nomina.Recibo2":
                        Reporte = new eClockReports.Reportes.Nomina.Recibo2();
                        eClockBase.Modelos.Nomina.Reporte_RecNomina ReciboNomina = JsonConvert.DeserializeObject<eClockBase.Modelos.Nomina.Reporte_RecNomina>(DatosJson);
                        ModeloDatos = ReciboNomina.Recibo_Nomina;
                        Reporte.Subreports[0].SetDataSource(ReciboNomina.Deducciones);
                        Reporte.Subreports[1].SetDataSource(ReciboNomina.Percepciones);
                        break;
                    //**************************************
                    case "eClockReports.Reportes.Nomina.ReciboCarta":
                        Reporte = new eClockReports.Reportes.Nomina.ReciboCarta();
                        eClockBase.Modelos.Nomina.Reporte_RecNomina ReciboNominaCarta = JsonConvert.DeserializeObject<eClockBase.Modelos.Nomina.Reporte_RecNomina>(DatosJson);
                        ModeloDatos = ReciboNominaCarta.Recibo_Nomina;
                        Reporte.Subreports[0].SetDataSource(ReciboNominaCarta.Percepciones);
                        Reporte.Subreports[1].SetDataSource(ReciboNominaCarta.Deducciones);
                        break;
                    //**************************************
                    case "eClockReports.Reportes.Movimientos.TotalInscripcionesenActividades":
                        Reporte = new eClockReports.Reportes.Movimientos.TotalInscripcionesenActividades();
                        ModeloDatos = DeserealizaReporte_TotalInscripcionesenActividades(DatosJson);
                        break;
                    case "eClockReports.Reportes.Movimientos.TotalSolicituddeTramites":
                        Reporte = new eClockReports.Reportes.Movimientos.TotalSolicituddeTramites();
                        ModeloDatos = DeserealizaReporte_TotalSolicituddeTramites(DatosJson);
                        break;
                    case "eClockReports.Reportes.Movimientos.Detalle_Actividades":
                        Reporte = new eClockReports.Reportes.Movimientos.Detalle_Actividades();
                        ModeloDatos = DeserealizaReporte__Detalle_Actividades(DatosJson);
                        break;
                    case "eClockReports.Reportes.Movimientos.Detalle_Tramites":
                        Reporte = new eClockReports.Reportes.Movimientos.Detalle_Tramites();
                        ModeloDatos = DeserealizaReporte__Detalle_Tramites(DatosJson);
                        break;
                    case "eClockReports.Reportes.HorasExtras.Semanal_HET_DT":
                        Reporte = new eClockReports.Reportes.HorasExtras.Semanal_HET_DT();

                        List<eClockBase.Modelos.HorasExtras.Reporte_Semanal_HET> Dato = DeserealizaReporte_Semanal_HET(DatosJson);
                        DateTime DFSHEDT = eClockBase.CeC.PersonaDiarioID2Fecha(Dato[0].ID);

                        Muestrafechas(Reporte, DFSHEDT);
                        ModeloDatos = Dato;
                        ModeloDatos = DeserealizaReporte_Semanal_HET(DatosJson);
                        break;
                    case "eClockReports.Reportes.HorasExtras.Semanal_HET":
                        Reporte = new eClockReports.Reportes.HorasExtras.Semanal_HET();

                        List<eClockBase.Modelos.HorasExtras.Reporte_Semanal_HET> DatoHEDT = DeserealizaReporte_Semanal_HET(DatosJson);
                        DateTime DF = eClockBase.CeC.PersonaDiarioID2Fecha(DatoHEDT[0].ID);

                        Muestrafechas(Reporte, DF);
                        ModeloDatos = DatoHEDT;
                        break;
                    case "eClockReports.Reportes.Nomina.FechaConsultada":
                        Reporte = new eClockReports.Reportes.Nomina.FechaConsultada();
                        ModeloDatos = DeserealizaReporte_Fecha_Consultada(DatosJson);
                        break;
                    case "eClockReports.Reportes.Nomina.NoConsultados":
                        Reporte = new eClockReports.Reportes.Nomina.NoConsultados();
                        ModeloDatos = DeserealizaReporte_SinConsultar(DatosJson);
                        break;
                    case "eClockReports.Reportes.Solicitudes.SolicitudVacaciones":
                        Reporte = new eClockReports.Reportes.Solicitudes.SolicitudVacaciones();
                        ModeloDatos = DeserealizaReporte_SolicitudVacaciones(DatosJson);
                        break;
                    case "eClockReports.Reportes.PreNomina.Simple":
                        Reporte = new eClockReports.Reportes.PreNomina.Simple();
                        ModeloDatos = JsonConvert.DeserializeObject<List<eClockBase.Modelos.PreNomina.Reporte_PreNomina>>(DatosJson);
                        break;
                    case "eClockReports.Reportes.Asistencia.ReporteTotales":
                        Reporte = new eClockReports.Reportes.Asistencia.ReporteTotales();
                        ModeloDatos = JsonConvert.DeserializeObject<List<eClockBase.Modelos.Asistencias.Model_AsistenciaTotales>>(DatosJson);
                        break;
                    case "eClockReports.Reportes.Asistencia.ReporteTotalesPersonasAgru":
                        Reporte = new eClockReports.Reportes.Asistencia.ReporteTotalesPersonaAgru();
                        ModeloDatos = JsonConvert.DeserializeObject<List<eClockBase.Modelos.Asistencias.Model_AsistenciaTotales>>(DatosJson);
                        break;
                    case "eClockReports.Reportes.Asistencia.ReporteTotalesAgrupacion":
                        Reporte = new eClockReports.Reportes.Asistencia.ReporteTotalesAgrupacion();
                        ModeloDatos = JsonConvert.DeserializeObject<List<eClockBase.Modelos.Asistencias.Model_AsistenciaTotales>>(DatosJson);
                        break;
                    case "eClockReports.Reportes.Asistencia.ReporteSaldoPersona":
                        Reporte = new eClockReports.Reportes.Asistencia.ReporteSaldoPersona();
                        ModeloDatos = JsonConvert.DeserializeObject<List<eClockBase.Modelos.Asistencias.Model_AsistenciaTotalesSaldos>>(DatosJson);
                        break;
                    case "eClockReports.Reportes.Asistencia.ReporteSaldoPersonasAgru":
                        Reporte = new eClockReports.Reportes.Asistencia.ReporteSaldoPersonasAgru();
                        ModeloDatos = JsonConvert.DeserializeObject<List<eClockBase.Modelos.Asistencias.Model_AsistenciaTotalesSaldos>>(DatosJson);
                        break;
                    case "eClockReports.Reportes.Asistencia.ReporteHorasExtra":
                        Reporte = new eClockReports.Reportes.Asistencia.ReporteHorasExtra();
                        ModeloDatos = JsonConvert.DeserializeObject<List<eClockBase.Modelos.Asistencias.Model_HorasExtra>>(DatosJson);
                        break;
                    case "eClockReports.Reportes.Asistencia.ReporteSaldosHistorial":
                        Reporte = new eClockReports.Reportes.Asistencia.ReporteSaldosHistorial();
                        ModeloDatos = JsonConvert.DeserializeObject<List<eClockBase.Modelos.Incidencias.Model_Historial>>(DatosJson);
                        break;

                }

                Reporte.SetDataSource(ModeloDatos);
                System.IO.Stream Str = null;
                switch (FormatoRepID)
                {

                    case 1:
                        Str = Reporte.ExportToStream(ExportFormatType.Excel);
                        break;
                    default:
                        Str = Reporte.ExportToStream(ExportFormatType.PortableDocFormat);
                        break;
                }
                string JsonReporte = JsonConvert.SerializeObject(ReadFully(Str));
                JsonReporte = eClockBase.Controladores.CeC_ZLib.Json2ZJson(JsonReporte);
                return JsonReporte;
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
            return null;
        }
        // Agregue aquí más operaciones y márquelas con [OperationContract]
    }
}
