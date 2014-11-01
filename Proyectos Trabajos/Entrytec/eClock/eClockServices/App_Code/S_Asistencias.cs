using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Services;
using Newtonsoft.Json;


[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class S_Asistencias
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
    /// 
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="MostrarAgrupacion"></param>
    /// <param name="MostrarEmpleado"></param>
    /// <param name="MostrarFecha"></param>
    /// <param name="Persona_ID">Podrá ser sustituido si el usuario esta asignado a una personaID</param>
    /// <param name="Agrupacion"></param>
    /// <param name="FechaInicial"></param>
    /// <param name="FechaFinal"></param>
    /// <returns></returns>
    [OperationContract]
    public string ObtenAsistenciaGrafica(string SesionSeguridad, bool MostrarAgrupacion, bool MostrarEmpleado, bool MostrarFecha, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        if (Sesion.PERSONA_ID > 0)
            Persona_ID = Sesion.PERSONA_ID;
        return CeC_BD.DataSet2JsonV2(CeC_Asistencias.ObtenAsistenciaGrafica(MostrarAgrupacion, MostrarEmpleado, MostrarFecha, Persona_ID, Agrupacion, FechaInicial, FechaFinal, Sesion));
    }

    [WebMethod(Description = "Obtiene la asistencia de empleados", MessageName = "ObtenAsistencia", EnableSession = true)]
    [OperationContract]
    public string ObtenAsistencia(string SesionSeguridad, bool EntradaSalida, bool Comida, bool HorasExtras, bool Totales, bool Incidencia, bool TurnoDia, string TiposIncidenciasSistemaIDs, string TiposIncidenciasIDs, bool MuestraAgrupacion, bool MuestraEmpleado, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        return CeC_BD.DataSet2JsonV2(CeC_Asistencias.ObtenAsistencia(EntradaSalida, Comida, HorasExtras, Totales, Incidencia, TurnoDia, TiposIncidenciasSistemaIDs, TiposIncidenciasIDs, MuestraAgrupacion, MuestraEmpleado, Persona_ID, Agrupacion, FechaInicial, FechaFinal, Sesion));
    }

    [WebMethod(Description = "Obtiene la asistencia de empleados", MessageName = "ObtenAsistenciaLineal", EnableSession = true)]
    [OperationContract]
    public string ObtenAsistenciaLineal(string SesionSeguridad, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, string Campos, string OrdenarPor, string TiposIncidenciasSistemaIDs, string TiposIncidenciasIDs)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        return CeC_BD.DataSet2JsonV2(CeC_Asistencias.ObtenAsistenciaLineal(Persona_ID, Agrupacion, FechaInicial, FechaFinal, Campos, OrdenarPor, TiposIncidenciasSistemaIDs, TiposIncidenciasIDs, Sesion));
    }
    /// <summary>
    /// Esta funcion se encarga de obtener la asistencia de forma lineal
    /// </summary>
    /// <param name="SesionSeguridad">Firma para comprobar que estes Logeado</param>
    /// <param name="PERSONA_DIARIO_ID_INICIO">Id de inicio</param>
    /// <param name="PERSONA_DIARIO_ID_FIN">Id de fin</param>
    /// <param name="Lang">Lenguage, "es" indica español</param>
    /// <returns></returns>
    [OperationContract]
    public string ObtenAsistenciaLinealN(string SesionSeguridad, int PERSONA_DIARIO_ID_INICIO, int PERSONA_DIARIO_ID_FIN, string Lang)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        return CeC_BD.DataSet2JsonV2(CeC_Asistencias.ObtenAsistenciaLinealN(PERSONA_DIARIO_ID_INICIO, PERSONA_DIARIO_ID_FIN, Lang, Sesion.USUARIO_ID));
    }

    [OperationContract]
    public string ObtenAsistenciaLinealV5(string SesionSeguridad, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, string OrdenarPor, string TiposIncidenciasSistemaIDs, string TiposIncidenciasIDs, string Lang)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        System.Data.DataSet DS = CeC_Asistencias.ObtenAsistenciaLinealV5(Persona_ID, Agrupacion, FechaInicial, FechaFinal, OrdenarPor, TiposIncidenciasSistemaIDs, TiposIncidenciasIDs, Lang, Sesion.USUARIO_ID);
        string Json = CeC_BD.DataSet2JsonV2(DS);
        string zJson = eClockBase.Controladores.CeC_ZLib.Json2ZJson(Json);
        return zJson;
    }

    [OperationContract]
    public string ObtenAsistenciaHorizontal(string SesionSeguridad, bool EntradaSalida, bool TurnoDia, bool IncidenciaAbr, bool MuestraAgrupacion, bool MuestraEmpleado, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        return CeC_BD.DataSet2JsonV2(CeC_Asistencias.ObtenAsistenciaHorizontal(EntradaSalida, TurnoDia, IncidenciaAbr, MuestraAgrupacion, MuestraEmpleado, Persona_ID, Agrupacion, FechaInicial, FechaFinal, Sesion));
    }
    /// <summary>
    /// Esta función se encarga de obtener la asistencia en formato horizontal
    /// </summary>
    /// <param name="SesionSeguridad">Firma para comprobar que estes Logeado</param>
    /// <param name="EntradaSalida">Mostrara o no la entrada y salida</param>
    /// <param name="TurnoDia">Mostrara o no los turnos</param>
    /// <param name="IncidenciaAbr"></param>
    /// <param name="ColorTurno">Mostrara o no colores en los turnos</param>
    /// <param name="ColorIncidencia">Mostrara o no colores en las incidencias</param>
    /// <param name="MuestraAgrupacion">Mostrara o no la agrupacion</param>
    /// <param name="MuestraEmpleado">Mostrara o no el empleado</param>
    /// <param name="Persona_ID">Id de persona a consultar, -1 indicara todos</param>
    /// <param name="Agrupacion">Nombre de la agrupacion a consultar, comillas vacias indicara todas</param>
    /// <param name="FechaInicial">Fecha inicial de la consulta</param>
    /// <param name="FechaFinal">Fecha final de la consulta</param>
    /// <param name="Lang">Lenguage, "es" indica español</param>
    /// <returns>String en formato JSon con el resultado de la consulta</returns>
    [OperationContract]
    public string ObtenAsistenciaHorizontalN(string SesionSeguridad, bool EntradaSalida, bool TurnoDia, bool IncidenciaAbr, bool ColorTurno, bool ColorIncidencia, bool MuestraAgrupacion, bool MuestraEmpleado, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, string Lang)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;

        return CeC_BD.DataSet2JsonV2(CeC_Asistencias.ObtenAsistenciaHorizontalN(EntradaSalida, TurnoDia, IncidenciaAbr, ColorTurno, ColorIncidencia, MuestraAgrupacion, MuestraEmpleado, Persona_ID, Agrupacion, FechaInicial, FechaFinal, Lang, Sesion));
    }

    [OperationContract]
    public string ObtenAsistenciaSemanal(string SesionSeguridad, bool EntradaSalida, bool TurnoDia, bool MuestraAgrupacion, bool MuestraEmpleado, int Persona_ID, string Agrupacion, DateTime FechaInicial)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        return CeC_BD.DataSet2JsonV2(CeC_Asistencias.ObtenAsistenciaSemanal(EntradaSalida, TurnoDia, MuestraAgrupacion, MuestraEmpleado, Persona_ID, Agrupacion, FechaInicial, Sesion));
    }
    [WebMethod(Description = "Obtiene las horas extras de los empleados", MessageName = "ObtenHorasExtras", EnableSession = true)]
    [OperationContract]
    public string ObtenHorasExtras(string SesionSeguridad, bool EntradaSalida, bool Comida, bool Totales, bool Incidencia, bool TurnoDia, bool MuestraAgrupacion, bool MuestraEmpleado, bool MuestraCeros, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        return CeC_BD.DataSet2JsonV2(CeC_AsistenciasHE.ObtenHorasExtras(EntradaSalida, Comida, Totales, Incidencia, TurnoDia, MuestraAgrupacion, MuestraEmpleado, MuestraCeros, Persona_ID, Agrupacion, FechaInicial, FechaFinal, Sesion.USUARIO_ID));
    }

    [OperationContract]
    public string AplicaHorasExtras(string SesionSeguridad, string ListadoAplicaHorasExtras, bool EsAvanzada)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        string Respuesta = "";
        int TipoIncidenciaHorasExtrasSuscripcion = Sesion.ConfiguraSuscripcion.TipoIncidenciaHorasExtras;


        if (EsAvanzada)
        {
            bool PermitirMasHorasExtras = Sesion.ConfiguraSuscripcion.PermitirMasHorasExtras;
            List<eClockBase.Modelos.Asistencias.Model_AplicaHorasExtrasAv> HorasExtras = JsonConvert.DeserializeObject<List<eClockBase.Modelos.Asistencias.Model_AplicaHorasExtrasAv>>(ListadoAplicaHorasExtras);
            foreach (eClockBase.Modelos.Asistencias.Model_AplicaHorasExtrasAv HoraExtra in HorasExtras)
            {

                int TipoIncidenciaHorasExtras = TipoIncidenciaHorasExtrasSuscripcion;
                if (HoraExtra.TIPO_INCIDENCIA_ID > 0)
                    TipoIncidenciaHorasExtras = HoraExtra.TIPO_INCIDENCIA_ID;

                if (!CeC_TiempoXTiempos.EsHorasExtras(TipoIncidenciaHorasExtras))
                {
                    if (!CeC_IncidenciasRegla.sTieneSaldoDisponible(CeC_AsistenciasHE.ObtenPersonaID(HoraExtra.PERSONA_D_HE_ID), TipoIncidenciaHorasExtras, 1))
                    {
                        continue;
                    }
                }
                if (!PermitirMasHorasExtras)
                {
                    TimeSpan HorasExtrasCalculadas = CeC_BD.DateTime2TimeSpan(CeC_AsistenciasHE.ObtenHorasExtrasCalculadas(HoraExtra.PERSONA_D_HE_ID));
                    if (HoraExtra.PERSONA_D_HE_APL > HorasExtrasCalculadas)
                        HoraExtra.PERSONA_D_HE_APL = HorasExtrasCalculadas;

                }

                if (TipoIncidenciaHorasExtrasSuscripcion == TipoIncidenciaHorasExtras)
                    TipoIncidenciaHorasExtras = 0;

                if (CeC_AsistenciasHE.AsignaHorasExtrasApl(Sesion, HoraExtra.PERSONA_D_HE_ID, HoraExtra.PERSONA_D_HE_APL, TipoIncidenciaHorasExtras, HoraExtra.PERSONA_D_HE_COMEN) > 0)
                    Respuesta = CeC.AgregaSeparador(Respuesta, HoraExtra.PERSONA_D_HE_ID.ToString(), ",");
            }
        }
        else
        {
            List<eClockBase.Modelos.Asistencias.Model_AplicaHorasExtras> HorasExtras = JsonConvert.DeserializeObject<List<eClockBase.Modelos.Asistencias.Model_AplicaHorasExtras>>(ListadoAplicaHorasExtras);
            foreach (eClockBase.Modelos.Asistencias.Model_AplicaHorasExtras HoraExtra in HorasExtras)
            {
                if (!CeC_IncidenciasRegla.sTieneSaldoDisponible(CeC_AsistenciasHE.ObtenPersonaID(HoraExtra.PERSONA_D_HE_ID), TipoIncidenciaHorasExtrasSuscripcion, 1))
                {
                    continue;
                }

                if (CeC_AsistenciasHE.AsignaHorasExtrasApl(Sesion, HoraExtra.PERSONA_D_HE_ID, HoraExtra.PERSONA_D_HE_COMEN) > 0)
                    Respuesta = CeC.AgregaSeparador(Respuesta, HoraExtra.PERSONA_D_HE_ID.ToString(), ",");
            }
        }
        return Respuesta;
    }

    [OperationContract]
    public string QuitaHorasExtras(string SesionSeguridad, string PERSONA_D_HE_IDs, bool Forzar)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        string[] sPERSONA_D_HE_IDs = CeC.ObtenArregoSeparador(PERSONA_D_HE_IDs, ",");
        string Respuesta = "";
        foreach (string sPERSONA_D_HE_ID in sPERSONA_D_HE_IDs)
        {
            if (CeC_AsistenciasHE.QuitaHoraExtra(CeC.Convierte2Int(sPERSONA_D_HE_ID), Forzar) > 0)
                Respuesta = CeC.AgregaSeparador(Respuesta, sPERSONA_D_HE_ID, ",");
        }
        return Respuesta;
    }

    [OperationContract]
    public string ObtenAsistenciaTotalesSaldos(string SesionSeguridad, bool MostrarAgrupacion, bool MostrarEmpleado, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        System.Data.DataSet DS = CeC_Asistencias.ObtenAsistenciaTotalesSaldos(MostrarAgrupacion, MostrarEmpleado, Persona_ID, Agrupacion, FechaInicial, FechaFinal, Sesion);
        string Json = CeC_BD.DataSet2JsonV2(DS);
        string zJson = eClockBase.Controladores.CeC_ZLib.Json2ZJson(Json);
        return zJson;
    }
    [OperationContract]
    public string ObtenAsistenciaTotales(string SesionSeguridad, bool MostrarAgrupacion, bool MostrarEmpleado, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        System.Data.DataSet DS = CeC_Asistencias.ObtenAsistenciaTotales(MostrarAgrupacion, MostrarEmpleado, Persona_ID, Agrupacion, FechaInicial, FechaFinal, Sesion);
        string Json = CeC_BD.DataSet2JsonV2(DS);
        string zJson = eClockBase.Controladores.CeC_ZLib.Json2ZJson(Json);
        return zJson;
    }

    [OperationContract]
    public string ObtenTiempos(string SesionSeguridad, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        eClockBase.Modelos.Asistencias.Model_Tiempos R = CeC_Asistencias.ObtenTiempos(Persona_ID, Agrupacion, FechaInicial, FechaFinal, Sesion.USUARIO_ID);
        if (R == null)
            return null;
        return Newtonsoft.Json.JsonConvert.SerializeObject(R);
    }
}
