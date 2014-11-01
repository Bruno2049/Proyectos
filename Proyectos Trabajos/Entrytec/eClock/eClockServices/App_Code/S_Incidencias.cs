using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using System.Web.Services;
using Newtonsoft.Json;
[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

public class S_Incidencias
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

    /// <summary>
    /// Esta funcion ejecuta un proceso de obtención;
    /// de un menú de incidencias por medio del Suscripción ID.
    /// </summary>
    /// <param name="SuscripcionID"></param>
    /// <param name=""></param>
    /// <returns></returns>
    [WebMethod(Description = "Obtiene un menu con el conjunto de la incidencias", MessageName = "ObtenTiposIncidenciasMenu", EnableSession = true)]
    [OperationContract]
    public string ObtenTiposIncidenciasMenu(string SesionSeguridad, int SuscripcionID)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        if (SuscripcionID != Sesion.SUSCRIPCION_ID && Sesion.SUSCRIPCION_ID > 1)
            return null;
        return CeC_BD.DataSet2JsonV2(Cec_Incidencias.ObtenTiposIncidenciasMenu(SuscripcionID));
    }
    /// <summary>
    /// Funcion que agregara un nuevo tipo de incidencia
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="NombreIncidencia"></param>
    /// <param name="AbreviaturaIncidencia"></param>
    /// <returns></returns>
    [WebMethod(Description = "Agrega una nuevo Tipo de Incidencia, con nombre y comentario", MessageName = "TipoIncidenciaAgrega", EnableSession = true)]
    [OperationContract]
    public int TipoIncidenciaAgrega(string SesionSeguridad, string NombreIncidencia, string AbreviaturaIncidencia)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return -1;
        return Cec_Incidencias.TipoIncidenciaAgrega(Sesion, NombreIncidencia, AbreviaturaIncidencia);
    }
    /// <summary>
    /// Funcion que crea una nueva incidencia
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="TipoIncidenciaID"></param>
    /// <param name="ComentarioIncidencia"></param>
    /// <returns></returns>
    [WebMethod(Description = "Crea una nueva Incidencia", MessageName = "CreaIncidencia", EnableSession = true)]
    [OperationContract]
    public int CreaIncidencia(string SesionSeguridad, int TipoIncidenciaID, string ComentarioIncidencia)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return -1;
        return Cec_Incidencias.CreaIncidencia(TipoIncidenciaID, ComentarioIncidencia, Sesion);
    }
    /// <summary>
    /// Funcion que asigna las incidencias creadas
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="FechaInicial"></param>
    /// <param name="FechaFinal"></param>
    /// <param name="Persona_ID"></param>
    /// <param name="Incidencia_ID"></param>
    /// <returns></returns>
    [WebMethod(Description = "Asigna las Incidencias a un empleado en un rango de fechas", MessageName = "AsignaIncidenciaPersona", EnableSession = true)]
    [OperationContract]
    public int AsignaIncidenciaPersona(string SesionSeguridad, DateTime FechaInicial, DateTime FechaFinal, int Persona_ID, int Incidencia_ID)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return -1;
        return Cec_Incidencias.AsignaIncidencia(FechaInicial, FechaFinal, Persona_ID, Incidencia_ID, Sesion);
    }

    /// <summary>
    /// Funcion que procesa la asignacion de incidencias.
    /// a un empleado en espcifico.
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="DatosIncidencia"></param>
    /// <returns></returns>
    [WebMethod(Description = "Asigna las Incidencias a un empleado", MessageName = "CargaIncidencia", EnableSession = true)]
    [OperationContract]
    public string CargaIncidencias(string SesionSeguridad, string DatosIncidencia)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        List<eClockBase.Modelos.Incidencias.Model_Incidencias> ListaIncidencias = JsonConvert.DeserializeObject<List<eClockBase.Modelos.Incidencias.Model_Incidencias>>(DatosIncidencia);
        string IncidenciasID = null;
        int IncidenciaID;
        try
        {
            foreach (eClockBase.Modelos.Incidencias.Model_Incidencias Incidencia in ListaIncidencias)
            {
                int PersonaID = CeC_Personas.ObtenPersonaID(Incidencia.Persona_Link_ID, Sesion.USUARIO_ID);
                if (PersonaID < 0)
                    continue;
                int TipoIncidenciaID = Incidencia.TipoIncidenciaID;
                if (TipoIncidenciaID <= 0)
                    Cec_Incidencias.TipoIncidenciaAgrega(Sesion, Incidencia.Nombre, Incidencia.Abreviatura);
                if (TipoIncidenciaID <= 0)
                    continue;
                Cec_Incidencias.CreaIncidencia(TipoIncidenciaID, Incidencia.Comentario, Sesion);
                IncidenciaID = Cec_Incidencias.AsignaIncidencia(Incidencia.FInicio, Incidencia.FFin, PersonaID, TipoIncidenciaID, Sesion);
                IncidenciasID += IncidenciaID + ",";
            }
            return IncidenciasID;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
            return null;
        }
    }
    /// <summary>
    /// Funcion que asignará una nueva incidencia,
    /// a un rango de fechas de un empleado.
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="TipoIncidenciaID"></param>
    /// <param name="PersonasDiariosIDs"></param>
    /// <param name="IncidenciaComentario"></param>
    /// <returns></returns>
    [WebMethod(Description = "Asigna una nueva Incidencia a un rango de fechas de un empleado", MessageName = "AsignaIncidenciaPersonasDiario", EnableSession = true)]
    [OperationContract]
    public int AsignaIncidenciaPersonasDiario(string SesionSeguridad, int TipoIncidenciaID, string PersonasDiariosIDs, string IncidenciaComentario)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return -1;
        if (PersonasDiariosIDs == "")
            return -2;
        return Cec_Incidencias.AsignaIncidencia(TipoIncidenciaID, PersonasDiariosIDs, IncidenciaComentario, Sesion);
    }

    [OperationContract]
    public string StatusVacaciones(string SesionSeguridad, int PersonaID)
    {
        try
        {
            CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
            if (Sesion == null)
                return null;
            int TipoInciendiaIDVacaciones = Sesion.ConfiguraSuscripcion.TipoIncidenciaID_Vacaciones;
            if (TipoInciendiaIDVacaciones <= 0)
                return "SIN_INCIDENCIA_VACACIONES";
            int TipoIncidenciaReglaID = CeC_IncidenciasRegla.ObtenTipo_Incidencia_R_ID(PersonaID, TipoInciendiaIDVacaciones);
            if (TipoIncidenciaReglaID <= 0)
                return "SIN_INCIDENCIA_REGLA";
            eClockBase.Modelos.Incidencias.Model_Vacaciones Vacaciones = new eClockBase.Modelos.Incidencias.Model_Vacaciones();
            Vacaciones.SaldoVacaciones = CeC_IncidenciasInventario.ObtenSaldo(PersonaID, TipoIncidenciaReglaID);
            Vacaciones.SiguienteCorte = CeC_IncidenciasInventario.ObtenFechaSiguiente(PersonaID, TipoIncidenciaReglaID);
            Vacaciones.SiguienteIncremento = CeC_IncidenciasInventario.ObtenIncrementoPersonaID(TipoIncidenciaReglaID, PersonaID);

            CeC_IncidenciasRegla IncRegla = new CeC_IncidenciasRegla(TipoIncidenciaReglaID);
            Vacaciones.Credito = IncRegla.TIPO_INCIDENCIA_R_CRED;
            Vacaciones.PierdeVacaciones = IncRegla.TIPO_INCIDENCIA_R_LIMPIAR;
            return JsonConvert.SerializeObject(Vacaciones);
        }
        catch { }
        return "ERROR";
    }

    [OperationContract]
    public string SolicitaIncidencia(string SesionSeguridad, int PersonaID, string FechasJson, int TipoIncidenciaID, string Comentario)
    {
        try
        {
            CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
            if (Sesion == null)
                return null;
            List<DateTime> Dias = JsonConvert.DeserializeObject<List<DateTime>>(FechasJson);
            CeC_Asistencias.GeneraPrevioPersonaDiario(PersonaID, Dias.Min<DateTime>(), Dias.Max<DateTime>());
            int NoDias = Dias.Count;
            int TipoIncidenciaReglaID = CeC_IncidenciasRegla.ObtenTipo_Incidencia_R_ID(PersonaID, TipoIncidenciaID);
            if (TipoIncidenciaReglaID > 0)
            {
                CeC_IncidenciasRegla IncRegla = new CeC_IncidenciasRegla(TipoIncidenciaReglaID);
                if (!IncRegla.TieneSaldoDisponible(PersonaID, NoDias))
                    return "NO_TIENE_SALDO";
            }
            if (CeC_Solicitudes.SolicitaJustificacion(TipoIncidenciaID, eClockBase.CeC.PersonaID2PersonasDiarioIDs(PersonaID, Dias), Comentario, Sesion))
                return "OK";
            return "NOK";
        }
        catch { }
        return "ERROR";
    }

    [OperationContract]
    public string SolicitaVacaciones(string SesionSeguridad, int PersonaID, string FechasJson, string Comentario)
    {
        try
        {

            CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
            if (Sesion == null)
                return null;
            int TipoInciendiaIDVacaciones = Sesion.ConfiguraSuscripcion.TipoIncidenciaID_Vacaciones;
            if (TipoInciendiaIDVacaciones <= 0)
                return "SIN_INCIDENCIA_VACACIONES";
            return SolicitaIncidencia(SesionSeguridad, PersonaID, FechasJson, TipoInciendiaIDVacaciones, Comentario);
        }
        catch { }
        return "ERROR";
    }

    [OperationContract]
    public string StatusDias(string SesionSeguridad, int PersonaID, DateTime FechaInicio, DateTime FechaFin, string Filtro)
    {
        try
        {

            CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
            if (Sesion == null)
                return null;
            List<eClockBase.Modelos.Incidencias.Model_StatusDia> StatusDias = new List<eClockBase.Modelos.Incidencias.Model_StatusDia>();
            string OtroFiltro = "";
            if (Filtro != null && Filtro != "")
                OtroFiltro = " AND " + Filtro;
            string Qry = "SELECT        EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA,SOLICITUD_EXTRA_ID, EDO_SOLICITUD_ID " +
            " FROM            EC_SOLICITUDES INNER JOIN " +
            " EC_SOLICITUDES_P_DIARIO ON EC_SOLICITUDES.SOLICITUD_ID = EC_SOLICITUDES_P_DIARIO.SOLICITUD_ID INNER JOIN " +
            " EC_PERSONAS_DIARIO ON EC_SOLICITUDES_P_DIARIO.PERSONA_DIARIO_ID = EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID " +
            " WHERE EC_PERSONAS_DIARIO.PERSONA_ID = " + PersonaID + OtroFiltro + " AND PERSONA_DIARIO_FECHA >= " + CeC_BD.SqlFecha(FechaInicio) +
            " AND PERSONA_DIARIO_FECHA <= " + CeC_BD.SqlFecha(FechaFin) + " \n" +

            " \n ORDER BY EC_SOLICITUDES.SOLICITUD_ID DESC";

            DataSet DSDiasSolicitados = CeC_BD.EjecutaDataSet(Qry);
            if (DSDiasSolicitados != null && DSDiasSolicitados.Tables.Count > 0)
            {
                foreach (DataRow Fila in DSDiasSolicitados.Tables[0].Rows)
                {
                    int Edo = CeC.Convierte2Int(Fila["EDO_SOLICITUD_ID"]);
                    switch (Edo)
                    {
                        case 2:
                            Edo = 12;
                            break;
                        case 3:
                            Edo = 13;
                            break;
                        case 4:
                            Edo = 14;
                            break;
                        default:
                            Edo = 0;
                            break;
                    }
                    bool Existe = false;
                    DateTime Fecha = CeC.Convierte2DateTime(Fila["PERSONA_DIARIO_FECHA"]);
                    foreach (eClockBase.Modelos.Incidencias.Model_StatusDia ED in StatusDias)
                    {
                        if (ED.Dia == Fecha)
                        {
                            Existe = true;
                            break;
                        }
                    }
                    if (!Existe)
                        StatusDias.Add(new eClockBase.Modelos.Incidencias.Model_StatusDia(Fecha,
                            CeC.Convierte2String(Fila["SOLICITUD_EXTRA_ID"]), Edo, 0));
                }
            }
            string Agrupacion = CeC_Personas.ObtenAgrupacionNombre(PersonaID);
            int SuscripcionID = CeC_Personas.ObtenSuscripcionID(PersonaID);
            int ObtenPersonasNO = CeC_Personas.ObtenPersonasNO(SuscripcionID, Agrupacion);
            int OcupacionAceptable = Sesion.ConfiguraSuscripcion.OcupacionAceptable;
            int OcupacionAlerta = Sesion.ConfiguraSuscripcion.OcupacionAlerta;
            int OcupacionMinima = Sesion.ConfiguraSuscripcion.OcupacionMinima;


            DataSet DSPt = CeC_BD.EjecutaDataSet("SELECT   PERSONA_DIARIO_FECHA, COUNT(*) AS TOTAL " +
            " FROM    EC_V_P_DIARIO_INC_SOL " +
            " WHERE PERSONA_DIARIO_FECHA >= " + CeC_BD.SqlFecha(FechaInicio) +
            " AND PERSONA_DIARIO_FECHA <= " + CeC_BD.SqlFecha(FechaFin) +
            " AND PERSONA_ID IN (SELECT PERSONA_ID FROM EC_PERSONAS WHERE SUSCRIPCION_ID = " + SuscripcionID + " AND AGRUPACION_NOMBRE = '" + Agrupacion + "') " +
            " GROUP BY PERSONA_DIARIO_FECHA");
            if (DSPt != null && DSPt.Tables.Count > 0)
            {
                foreach (DataRow Fila in DSPt.Tables[0].Rows)
                {
                    string Llave = "0";
                    DateTime Fecha = CeC.Convierte2DateTime(Fila["PERSONA_DIARIO_FECHA"]);
                    int Total = CeC.Convierte2Int(Fila["TOTAL"]);
                    int Pt = Total * 100 / ObtenPersonasNO;
                    if (Pt > OcupacionAceptable)
                        Llave = "1";
                    if (Pt > OcupacionAlerta)
                        Llave = "2";
                    if (Pt > OcupacionMinima)
                        Llave = "3";
                    StatusDias.Add(new eClockBase.Modelos.Incidencias.Model_StatusDia(Fecha,
                        Llave, 1, 0));
                }
            }

            DataSet DSDiasFestivos = CeC_DiasFestivos.ObtenDias(PersonaID);
            if (DSDiasFestivos != null && DSDiasFestivos.Tables.Count > 0)
            {
                foreach (DataRow Fila in DSDiasFestivos.Tables[0].Rows)
                {
                    DateTime Fecha = CeC.Convierte2DateTime(Fila["DIA_FESTIVO_FECHA"]);
                    string Nombre = CeC.Convierte2String(Fila["DIA_FESTIVO_NOMBRE"]);
                    StatusDias.Add(new eClockBase.Modelos.Incidencias.Model_StatusDia(Fecha,
                        Nombre, -1, 0));
                }
            }
            return JsonConvert.SerializeObject(StatusDias);
        }
        catch { }
        return "ERROR";
    }

    [OperationContract]
    public string StatusHoras(string SesionSeguridad, string PersonasDiariosIDs)
    {
        try
        {

            CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
            if (Sesion == null)
                return null;

            return JsonConvert.SerializeObject(Cec_Incidencias.ObtenStatusHoras(PersonasDiariosIDs, Sesion));
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return "ERROR";
    }

    [OperationContract]
    public string StatusRegla(string SesionSeguridad, int TipoIncidenciaID, string PersonasDiariosIDs)
    {
        try
        {

            CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
            if (Sesion == null)
                return null;

            return JsonConvert.SerializeObject(Cec_Incidencias.ObtenStatusRegla(TipoIncidenciaID, PersonasDiariosIDs, Sesion));
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return "ERROR";
    }

    [OperationContract]
    public string StatusReglaHoras(string SesionSeguridad, int TipoIncidenciaID, int PersonaDiarioID, decimal Horas)
    {
        try
        {
            CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
            if (Sesion == null)
                return null;
            return JsonConvert.SerializeObject(Cec_Incidencias.ObtenStatusReglaHoras(TipoIncidenciaID, PersonaDiarioID, Horas, Sesion));
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return "ERROR";
    }

    [OperationContract]
    public string ObtenerSaldos(string SesionSeguridad, int PersonaID, string Agrupacion, string TiposIncidenciasIDS)
    {
        try
        {
            CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
            if (Sesion == null)
                return null;
            return eClockBase.Controladores.CeC_ZLib.Object2ZJson(CeC_IncidenciasInventario.ObtenSaldos(PersonaID, Agrupacion, TiposIncidenciasIDS, Sesion));
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return "ERROR";
    }

    [OperationContract]
    public string ObtenerHistorial(string SesionSeguridad, int PersonaID, string Agrupacion, DateTime FechaInicio, DateTime FechaFin, string TiposIncidenciasIDS)
    {
        try
        {
            CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
            if (Sesion == null)
                return null;
            return eClockBase.Controladores.CeC_ZLib.Json2ZJson(CeC_BD.DataSet2JsonV2(CeC_Asistencias.ObtenAsistenciaTotalesHistorial(false, true, PersonaID, Agrupacion, FechaInicio, FechaFin, Sesion)));
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return "ERROR";
    }

    [OperationContract]
    public int CorrigeMovimientoInventario(string SesionSeguridad, int Almacen_IncID)
    {
        try
        {
            CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
            if (Sesion == null)
                return -1;
            return CeC_IncidenciasInventario.CorrigeMovimientoAlmacenIncID(Almacen_IncID, Sesion.SESION_ID, Sesion.SUSCRIPCION_ID);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return -2;

    }

    [OperationContract]
    public string ObtenIncidencias(string SesionSeguridad, DateTime Desde, DateTime Hasta, int PeriodoID, int SuscripcionID)
    {
        try
        {
            CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
            if (Sesion == null)
                return null;
            return eClockBase.Controladores.CeC_ZLib.Json2ZJson(CeC_BD.DataSet2JsonV2(CeC_Incidencias_Ex.ObtenIncidenciasExAsis(Desde, Hasta, PeriodoID, SuscripcionID)));
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return "ERROR";
    }

    [OperationContract]
    public bool EnviaANomina(string SesionSeguridad, int PeriodoID, int SuscripcionID)
    {

        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return false;
        return CeC_Incidencias_Ex.EnviaANomina(PeriodoID, SuscripcionID);
    }
}
