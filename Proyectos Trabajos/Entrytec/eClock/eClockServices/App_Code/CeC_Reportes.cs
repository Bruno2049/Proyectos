using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Data;
using System.Collections.Generic;


/// <summary>
/// Descripción breve de CeC_Reportes
/// </summary>
public class CeC_Reportes
{
    public CeC_Reportes()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public static string ObtenRutaServicio(int ServidorRepID)
    {
        return CeC_BD.EjecutaEscalarString("SELECT REPORTE_SRV_URL FROM EC_REPORTES_SRV WHERE REPORTE_SRV_ID = " + ServidorRepID);
    }
    public static byte[] ObtenParametros(int ReporteID, CeC_Sesion Sesion)
    {
        return CeC_BD.ObtenBinario("EC_REPORTES", "REPORTE_ID", ReporteID, "USUARIO_ID", Sesion.USUARIO_ID, "REPORTE_CONFIG");
    }

    public static int EnviaReporte_eMail(string eMails, string eMailsCopias, string Titulo, string Mensaje, int ReporteID, string Parametros, int FormatoRepID, CeC_Sesion Sesion, string SesionSeguridad, string Lang)
    {
        string Reporte = ObtenReporte(ReporteID, Parametros, FormatoRepID, Sesion, SesionSeguridad, Lang);
        if (Reporte.Length > 100)
        {
            CeC_Mails.EnviaMensaje("", eMails, eMailsCopias, Titulo, Mensaje, 1, eClockBase.Controladores.CeC_ZLib.Json2Object<byte[]>(Reporte), Sesion);
            return 1;
        }
        return -1;
    }

    public static string ObtenReporte(int ReporteID, string Parametros, int FormatoRepID, CeC_Sesion Sesion, string SesionSeguridad, string Lang)
    {
        try
        {
            eClockBase.Modelos.Model_REPORTES Reporte = new eClockBase.Modelos.Model_REPORTES();
            Reporte.REPORTE_ID = ReporteID;
            Reporte = JsonConvert.DeserializeObject<eClockBase.Modelos.Model_REPORTES>(CeC_Tabla.ObtenDatos("EC_REPORTES", "REPORTE_ID", JsonConvert.SerializeObject(Reporte), Sesion));
            string Datos = "";
            switch (Reporte.REPORTE_MODELO)
            {
                case "eClockBase.Modelos.Solicitudes.Model_ReporteSolicitudVacaciones":
                    {
                        if (Parametros != null && Parametros.Length > 3)
                        {
                        eClockBase.Modelos.Solicitudes.Model_Parametros Param = JsonConvert.DeserializeObject<eClockBase.Modelos.Solicitudes.Model_Parametros>(Parametros);
                        Datos = CeC_BD.DataSet2JsonV2(CeC_Solicitudes.SolicitudVacaciones());
                        }
                        else
                            Datos = CeC_BD.DataSet2JsonV2(CeC_Solicitudes.SolicitudVacaciones());
                    }
                    break;
                case "eClockBase.Modelos.Asistencias.Reporte_Asistencias":
                    {
                        eClockBase.Modelos.Asistencias.Model_Parametros Param = JsonConvert.DeserializeObject<eClockBase.Modelos.Asistencias.Model_Parametros>(Parametros);
                        DataSet DS = CeC_Asistencias.ObtenAsistencias_V5(Param.PERSONA_ID, Param.AGRUPACION, Param.FECHA_INICIAL, Param.FECHA_FINAL.AddDays(1), "*", "PERSONA_DIARIO_ID", Param.TIPO_INC_SIS_IDs, Param.TIPO_INCIDENCIA_IDs, Sesion);
                        if (DS != null && DS.Tables.Count > 0)
                        {
                            string FH = DateTime.Now.ToString("hhmmss");
                            int NoFilas = DS.Tables[0].Rows.Count;

                            CeC_StreamFile SF = new CeC_StreamFile();

                           /* CIsLog2.AgregaLog("Probando de DataSet2JsonList con " + NoFilas + " Filas");
                            Datos = CeC_BD.DataSet2JsonList(DS);
                            CIsLog2.AgregaLog("Listo DataSet2JsonList con " + NoFilas + " Filas" + Datos.Length);
                            SF.NuevoTexto("DataSet2JsonList" + FH + ".txt", Datos);
                            */

                            //CIsLog2.AgregaLog("Probando de DataSet2JsonV2 con " + NoFilas + " Filas");
                            Datos = CeC_BD.DataSet2JsonV2(DS);
                            //CIsLog2.AgregaLog("Listo DataSet2JsonV2 con " + NoFilas + " Filas " + Datos.Length);
                            
                            //SF.NuevoTexto("DataSet2JsonV2"+FH+".txt", Datos);


                        }
                    }
                    break;
                case "eClockBase.Modelos.Asistencias.Reporte_Asistencia31":
                    {
                        eClockBase.Modelos.Asistencias.Model_Parametros Param = JsonConvert.DeserializeObject<eClockBase.Modelos.Asistencias.Model_Parametros>(Parametros);
                        Datos = CeC_BD.DataSet2JsonV2(CeC_Asistencias.ObtenAsistenciaHorizontalN(true, true, true, true, true, true, true, true, Param.PERSONA_ID, Param.AGRUPACION, Param.FECHA_INICIAL, Param.FECHA_FINAL.AddDays(1), Lang, Sesion));
                    }
                    break;
                case "eClockBase.Modelos.Asistencias.Reporte_AsistenciaAbr31":
                    {
                        eClockBase.Modelos.Asistencias.Model_Parametros Param = JsonConvert.DeserializeObject<eClockBase.Modelos.Asistencias.Model_Parametros>(Parametros);
                        Datos = CeC_BD.DataSet2JsonV2(CeC_Asistencias.ObtenAsistenciaHorizontalN(false, false, true, false, true, true, true, true, Param.PERSONA_ID, Param.AGRUPACION, Param.FECHA_INICIAL, Param.FECHA_FINAL.AddDays(1), Lang, Sesion));
                    }
                    break;
                case "eClockBase.Modelos.HorasExtras.Reporte_Semanal":
                    {
                        eClockBase.Modelos.Asistencias.Model_Parametros Param = JsonConvert.DeserializeObject<eClockBase.Modelos.Asistencias.Model_Parametros>(Parametros);
                        Datos = CeC_BD.DataSet2JsonV2(CeC_AsistenciasHE.ObtenHorasExtrasHorizontalN(false, false, false, true, true, true, Param.PERSONA_ID, Param.AGRUPACION, Param.FECHA_INICIAL, Param.FECHA_FINAL.AddDays(1), Lang, Sesion));
                    }
                    break;
                case "eClockBase.Modelos.HorasExtras.Reporte_Semanal_HET":
                    {
                        eClockBase.Modelos.Asistencias.Model_Parametros Param = JsonConvert.DeserializeObject<eClockBase.Modelos.Asistencias.Model_Parametros>(Parametros);
                        Datos = CeC_BD.DataSet2JsonV2(CeC_AsistenciasHE.ObtenHorasExtrasHorizontalN(true, true, true, true, true, true, Param.PERSONA_ID, Param.AGRUPACION, Param.FECHA_INICIAL, Param.FECHA_FINAL.AddDays(1), Lang, Sesion));
                    }
                    break;
                case "eClockBase.Modelos.Nomina.Reporte_RecNomina":
                    {
                        eClockBase.Modelos.Nomina.Model_Parametros Param = JsonConvert.DeserializeObject<eClockBase.Modelos.Nomina.Model_Parametros>(Parametros);
                        Datos = JsonConvert.SerializeObject(CeC_RecNominas.ObtenRecibosNomina(Param, Lang, Sesion));
                    }
                    break;
                case "eClockBase.Modelos.Actividades.Reporte_NoInscripciones":
                    {
                        if (Parametros != null && Parametros.Length > 3)
                        {
                            eClockBase.Modelos.Actividades.Reporte_NoInscripciones_Param Param = JsonConvert.DeserializeObject<eClockBase.Modelos.Actividades.Reporte_NoInscripciones_Param>(Parametros);
                            Datos = CeC_BD.DataSet2JsonV2(CeC_Actividades.ObtenerNoInscripcionesDS(Param.FechaDesde, Param.FechaHasta, Sesion));
                        }
                        else
                            Datos = CeC_BD.DataSet2JsonV2(CeC_Actividades.ObtenerNoInscripcionesDS(Sesion));
                        //eClockBase.Modelos.Actividades.Reporte_NoInscripciones_Param Param = JsonConvert.DeserializeObject<eClockBase.Modelos.Actividades.Reporte_NoInscripciones_Param>(Parametros);

                    }
                    break;
                case "eClockBase.Modelos.Tramites.Reporte_NoTramites":
                    {
                        if (Parametros != null && Parametros.Length > 3)
                        {
                            eClockBase.Modelos.Tramites.Reporte_NoTramites_Param Param = JsonConvert.DeserializeObject<eClockBase.Modelos.Tramites.Reporte_NoTramites_Param>(Parametros);
                            Datos = CeC_BD.DataSet2JsonV2(CeC_Tramites.ObtenerNoTramitesDS(Param.FechaDesde, Param.FechaHasta, Sesion));
                        }
                        else
                            Datos = CeC_BD.DataSet2JsonV2(CeC_Tramites.ObtenerNoTramitesDS(Sesion));
                    }
                    break;
                case "eClockBase.Modelos.Actividades.Reporte_Detalle":
                    {
                        if (Parametros != null && Parametros.Length > 3)
                        {
                            eClockBase.Modelos.Actividades.Reporte_Detalle_Param Param = JsonConvert.DeserializeObject<eClockBase.Modelos.Actividades.Reporte_Detalle_Param>(Parametros);
                            Datos = CeC_BD.DataSet2JsonV2(CeC_Actividades.ObtenerDetallesDS(Param.FechaDesde, Param.FechaHasta, Sesion));
                        }
                        else
                            Datos = CeC_BD.DataSet2JsonV2(CeC_Actividades.ObtenerDetallesDS(Sesion));
                    }
                    break;
                case "eClockBase.Modelos.Tramites.Reporte_Detalle":
                    {
                        if (Parametros != null && Parametros.Length > 3)
                        {
                            eClockBase.Modelos.Tramites.Reporte_Detalle_Param Param = JsonConvert.DeserializeObject<eClockBase.Modelos.Tramites.Reporte_Detalle_Param>(Parametros);
                            Datos = CeC_BD.DataSet2JsonV2(CeC_Tramites.ObtenerDetallesDS(Param.FechaDesde, Param.FechaHasta, Sesion));
                        }
                        else
                            Datos = CeC_BD.DataSet2JsonV2(CeC_Tramites.ObtenerDetallesDS(Sesion));
                    }
                    break;
                case "eClockBase.Modelos.Nomina.Reporte_NominaFechaConsulta":
                    {
                        if (Parametros != null && Parametros.Length > 3)
                        {
                            eClockBase.Modelos.Nomina.Reporte_NominaFechaConsulta_Param Param = JsonConvert.DeserializeObject<eClockBase.Modelos.Nomina.Reporte_NominaFechaConsulta_Param>(Parametros);
                            Datos = CeC_BD.DataSet2JsonV2(CeC_RecNominas.ObtenNoConsultas(Param.PERSONA_ID, Param.AGRUPACION, Param.TIPO_NOMINA_ID,Param.FechaDesde,Param.FechaHasta, Sesion));
                        }
                        else
                            Datos = CeC_BD.DataSet2JsonV2(CeC_RecNominas.ObtenNoConsultas(Sesion));
                    }
                    break;
                case "eClockBase.Modelos.Nomina.Reporte_SinConsultar":
                    {
                        if (Parametros != null && Parametros.Length > 3)
                        {
                            eClockBase.Modelos.Nomina.Reporte_NominaFechaConsulta_Param Param = JsonConvert.DeserializeObject<eClockBase.Modelos.Nomina.Reporte_NominaFechaConsulta_Param>(Parametros);
                            Datos = CeC_BD.DataSet2JsonV2(CeC_RecNominas.ObtenSinConsultas(Param.PERSONA_ID,Param.AGRUPACION,Param.TIPO_NOMINA_ID, Sesion));
                        }
                        else
                            Datos = CeC_BD.DataSet2JsonV2(CeC_RecNominas.ObtenSinConsultas(Sesion));
                    }
                    break;

                case "eClockBase.Modelos.PreNomina.Reporte_PreNomina":
                    {             
                        eClockBase.Modelos.Asistencias.Model_Parametros Param = JsonConvert.DeserializeObject<eClockBase.Modelos.Asistencias.Model_Parametros>(Parametros);
                        Datos = eClockBase.CeC.Json2JsonList(JsonConvert.SerializeObject(CeC_PreNomina.ObtenSimple(Param.PERSONA_ID, Param.AGRUPACION, Param.FECHA_INICIAL, Param.FECHA_FINAL.AddDays(1), Sesion)));
                    }
                    break;

                case "eClockBase.Modelos.Asistencias.Model_AsistenciaTotales":
                    {
                        eClockBase.Modelos.Asistencias.Model_Parametros Param = JsonConvert.DeserializeObject<eClockBase.Modelos.Asistencias.Model_Parametros>(Parametros);
                        switch (Reporte.REPORTE_CLASE)
                        {
                            case "eClockReports.Reportes.Asistencia.ReporteTotales":
                                Datos = CeC_BD.DataSet2JsonV2(CeC_Asistencias.ObtenAsistenciaTotales(false, true, Param.PERSONA_ID, Param.AGRUPACION, Param.FECHA_INICIAL, Param.FECHA_FINAL.AddDays(1), Sesion));
                                break;
                            case "eClockReports.Reportes.Asistencia.ReporteTotalesAgrupacion":
                                Datos = CeC_BD.DataSet2JsonV2(CeC_Asistencias.ObtenAsistenciaTotales(true, false, Param.PERSONA_ID, Param.AGRUPACION, Param.FECHA_INICIAL, Param.FECHA_FINAL.AddDays(1), Sesion));
                                break;

                            default:
                                Datos = CeC_BD.DataSet2JsonV2(CeC_Asistencias.ObtenAsistenciaTotales(true, true, Param.PERSONA_ID, Param.AGRUPACION, Param.FECHA_INICIAL, Param.FECHA_FINAL.AddDays(1), Sesion));
                                break;
                        }

                        
                    }
                    break;
                case "eClockBase.Modelos.Asistencias.Model_AsistenciaTotalesSaldos":
                    {
                        eClockBase.Modelos.Asistencias.Model_Parametros Param = JsonConvert.DeserializeObject<eClockBase.Modelos.Asistencias.Model_Parametros>(Parametros);
                        switch (Reporte.REPORTE_CLASE)
                        {
                            case "eClockReports.Reportes.Asistencia.ReporteSaldoPersona":
                                Datos = CeC_BD.DataSet2JsonV2(CeC_Asistencias.ObtenAsistenciaTotalesSaldos(false, true, Param.PERSONA_ID, Param.AGRUPACION, Param.FECHA_INICIAL, Param.FECHA_FINAL.AddDays(1), Sesion));
                                break;
                            case "eClockReports.Reportes.Asistencia.ReporteSaldoAgrupacion":
                                Datos = CeC_BD.DataSet2JsonV2(CeC_Asistencias.ObtenAsistenciaTotalesSaldos(true, false, Param.PERSONA_ID, Param.AGRUPACION, Param.FECHA_INICIAL, Param.FECHA_FINAL.AddDays(1), Sesion));
                                break;
                            default:
                                Datos = CeC_BD.DataSet2JsonV2(CeC_Asistencias.ObtenAsistenciaTotalesSaldos(true, true, Param.PERSONA_ID, Param.AGRUPACION, Param.FECHA_INICIAL, Param.FECHA_FINAL.AddDays(1), Sesion));
                                break;
                        }
                    }
                    break;

                     case "eClockBase.Modelos.Asistencias.Model_HorasExtra":
                    {             
                        eClockBase.Modelos.Asistencias.Model_Parametros Param = JsonConvert.DeserializeObject<eClockBase.Modelos.Asistencias.Model_Parametros>(Parametros);
                        Datos = CeC_BD.DataSet2JsonV2(CeC_AsistenciasHE.ObtenHorasExtras(false,false,true,false,false,true,true,true,Param.PERSONA_ID,Param.AGRUPACION,Param.FECHA_INICIAL,Param.FECHA_FINAL,Sesion.USUARIO_ID));
                    }
                    break;

                     case "eClockBase.Modelos.Incidencias.Model_Historial":
                    {
                        eClockBase.Modelos.Asistencias.Model_Parametros Param = JsonConvert.DeserializeObject<eClockBase.Modelos.Asistencias.Model_Parametros>(Parametros);
                        Datos = eClockBase.Controladores.CeC_ZLib.Json2ZJson(CeC_BD.DataSet2JsonV2(CeC_Asistencias.ObtenAsistenciaTotalesHistorial(false, true, Param.PERSONA_ID , Param.AGRUPACION, Param.FECHA_INICIAL, Param.FECHA_FINAL, Sesion)));
                    }
                    break;
                default:
                    break;
            }
            ES_Report.S_ReportClient Cliente = new ES_Report.S_ReportClient("BasicHttpBinding_S_Report", ObtenRutaServicio(Reporte.REPORTE_SRV_ID));
            byte[] Configuracion = ObtenParametros(ReporteID, Sesion);
            string sConfiguracion = "";
            if (Configuracion != null && Configuracion.Length > 0)
                sConfiguracion = JsonConvert.SerializeObject(Configuracion);
            Datos = eClockBase.Controladores.CeC_ZLib.Json2ZJson(Datos);
            string R = Cliente.ObtenReporte(SesionSeguridad, Reporte.REPORTE_CLASE, Datos, Parametros, sConfiguracion, FormatoRepID);
            return R;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);

        }
        return "";
    }

    public static DataSet ObtenListadoReportes(CeC_Sesion Sesion)
    {
        string Qry = "SELECT        EC_REPORTES.REPORTE_ID, EC_REPORTES.REPORTE_TITULO, EC_REPORTES.REPORTE_DESCRIP, " +
                        " EC_REPORTES.REPORTE_IDIOMA, EC_REPORTES.REPORTE_MODELO, EC_REPORTES.REPORTE_PRECIO,  " +
                        " EC_REPORTES.REPORTE_TAMANO, EC_REPORTES.REPORTE_PARAM, EC_REPORTES.REPORTE_FORMATOS,  " +
                        " EC_REPORTES_SRV.REPORTE_SRV_NOMBRE,  " +
                        " EC_REPORTES_SRV.REPORTE_SRV_EMPR, EC_REPORTES_SRV.REPORTE_SRV_DESC,  " +
                        " EC_REPORTES_USUARIOS.REPORTE_USUARIO_SPAGO, EC_REPORTES_USUARIOS.REPORTE_USUARIO_UUSO,  " +
                        " EC_REPORTES_USUARIOS.REPORTE_USUARIO_EDO, EC_REPORTES_USUARIOS.REPORTE_USUARIO_INST,  " +
                        " EC_REPORTES_USUARIOS.REPORTE_USUARIO_UMOD, EC_REPORTES_USUARIOS.REPORTE_USUARIO_ORD " +
                        " FROM            EC_REPORTES INNER JOIN " +
                        " EC_REPORTES_SRV ON EC_REPORTES.REPORTE_SRV_ID = EC_REPORTES_SRV.REPORTE_SRV_ID LEFT OUTER JOIN " +
                        " EC_REPORTES_USUARIOS ON EC_REPORTES.REPORTE_ID = EC_REPORTES_USUARIOS.REPORTE_ID " +
                        " WHERE        (EC_REPORTES.REPORTE_BORRADO = 0) AND (EC_REPORTES_SRV.REPORTE_SRV_BORRADO = 0) AND  " +
                        " (EC_REPORTES_USUARIOS.USUARIO_ID = " + Sesion.USUARIO_ID + " OR " +
                        " EC_REPORTES_USUARIOS.USUARIO_ID IS NULL) " +
                        " \n ORDER BY EC_REPORTES_USUARIOS.REPORTE_USUARIO_ORD ";
        return CeC_BD.EjecutaDataSet(Qry);
    }
}