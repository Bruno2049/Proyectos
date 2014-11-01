using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using Newtonsoft.Json;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class S_Turnos
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
    /// Obtiene la lista de Turnos para la Suscripción actual.
    /// </summary>
    /// <param name="SesionSeguridad">Variable de Sesion</param>
    /// <returns>DataSet con la colección de los Turnos válidos para la Suscripción</returns>
    [OperationContract]
    public string ObtenTurnosDSMenu(string SesionSeguridad)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        return CeC_BD.DataSet2JsonV2(CeC_Turnos.ObtenTurnosDSMenu(Sesion.SUSCRIPCION_ID));
    }

    /// <summary>
    /// Asigna el Turno Predeterminado a los empleados.
    /// </summary>
    /// <param name="Personas_Link_IDS">Números de empleados separados por coma a los que se les asignara el turno.</param>
    /// <param name="Turno_ID">Identificador único del Turno Predeterminado.</param>
    /// <returns>Lista separada por comas con los números de empleado asignados correctamente. Aquellos que no se hallan asignado correctamente, tendran el prefijo ERROR</returns>
    [OperationContract]
    public String AsignaTurnoPredeterminado(string Persona_Link_IDS, int Turno_ID, string SesionSeguridad)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        String Resultado = null;
        if (Sesion == null)
            return null;
        int Persona_ID = 0;
        string[] Personas_Link_IDS = CeC.ObtenArregoSeparador(Persona_Link_IDS, ",");
        foreach (string Persona_Link_ID in Personas_Link_IDS)
        {
            Persona_ID = CeC_Personas.ObtenPersonaIDBySuscripcion(CeC.Convierte2Int(Persona_Link_ID), Sesion.SUSCRIPCION_ID);
            if (CeC_Turnos.AsignaHorarioPred(Persona_ID, Turno_ID) > 0)
            {
                Resultado += Persona_Link_ID + ",";
            }
            else
            {
                Resultado += "ERROR" + Persona_Link_ID + ",";
            }
        }
        return Resultado;
    }

    /// <summary>
    /// Asigna el Turno del Dia a los Empleados.
    /// </summary>
    /// <param name="Persona_Diario_IDS">Identificador único de asistencia diario.</param>
    /// <param name="Turno_Dia_ID">Turno asignado para el día.</param>
    /// <param name="SesionSeguridad">Variable de Sesion</param>
    /// <returns>Lista separada por comas con los números de empleado asignados correctamente. Aquellos que no se asignaron correctamente, tendran el prefijo ERROR</returns>
    [OperationContract]
    public String AsignaTurnoDia(string Persona_Diario_IDS, int Turno_Dia_ID, string SesionSeguridad)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        int Persona_ID = 0;
        int Persona_Link_ID = 0;
        String Resultado = null;
        string[] Personas_Diario_IDS = CeC.ObtenArregoSeparador(Persona_Diario_IDS, ",");
        foreach (string Persona_Diario_ID in Personas_Diario_IDS)
        {
            Persona_ID = CeC_Asistencias.ObtenPersonaID(CeC.Convierte2Int(Persona_Diario_ID));
            Persona_Link_ID = CeC_BD.ObtenPersonaLinkID(Persona_ID);
            if (CeC_Turnos.AsignaTurnoDia(CeC.Convierte2Int(Persona_Diario_ID), Turno_Dia_ID) > 0)
            {
                Resultado += Persona_Link_ID.ToString() + ",";
            }
            else
            {
                Resultado += "ERROR" + Persona_Link_ID.ToString() + ",";
            }
        }
        return Resultado;
    }

    /// <summary>
    /// Asigna un Turno por rango de fechas.
    /// </summary>
    /// <param name="Persona_Diario_IDS">Identificador único de asistencia diario.</param>
    /// <param name="Turno_ID">Identificador único del Turno</param>
    /// <param name="FechaInicio">Fecha de inicio de aplicación del Turno.</param>
    /// <param name="FechaFin">Fecha de fin de aplicación del Turno.</param>
    /// <param name="SesionSeguridad">Variable de Sesión.</param>
    /// <returns>Lista separada por comas con los números de empleado asignados correctamente. Aquellos que no se asignaron correctamente, tendran el prefijo ERROR</returns>
    [OperationContract]
    public String AsignaTurnoRangoFechas(string Persona_Diario_IDS, int Turno_ID, DateTime FechaInicio, DateTime FechaFin, string SesionSeguridad)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        int Persona_ID = 0;
        int Persona_Link_ID = 0;
        String Resultado = null;
        string[] Personas_Diario_IDS = CeC.ObtenArregoSeparador(Persona_Diario_IDS, ",");
        foreach (string Persona_Diario_ID in Personas_Diario_IDS)
        {
            Persona_ID = CeC_Asistencias.ObtenPersonaID(CeC.Convierte2Int(Persona_Diario_ID));
            Persona_Link_ID = CeC_BD.ObtenPersonaLinkID(Persona_ID);
            if (CeC_Turnos.AsignaHorario(Persona_ID, Turno_ID, FechaInicio, FechaFin, Sesion) > 0)
            {
                Resultado += Persona_Link_ID.ToString() + ",";
            }
            else
            {
                Resultado += "ERROR" + Persona_Link_ID.ToString() + ",";
            }
        }
        return Resultado;
    }

    /// <summary>
    /// Asigna un Turno por rango de fechas.
    /// </summary>
    /// <param name="Persona_Diario_IDS">Identificador único de asistencia diario.</param>
    /// <param name="Turno_ID">Nombre del Turno</param>
    /// <param name="FechaInicio">Fecha de inicio de aplicación del Turno.</param>
    /// <param name="FechaFin">Fecha de fin de aplicación del Turno.</param>
    /// <param name="SesionSeguridad">Variable de Sesión.</param>
    /// <returns>Lista separada por comas con los números de empleado asignados correctamente. Aquellos que no se asignaron correctamente, tendran el prefijo ERROR</returns>
    [OperationContract]
    public String AsignaTurnoNombreRangoFechas(string Persona_Diario_IDS, string Turno_Nombre, DateTime FechaInicio, DateTime FechaFin, string SesionSeguridad)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        int Persona_ID = 0;
        int Persona_Link_ID = 0;
        int Turno_ID = CeC_Turnos.ObtenTurnoID(Turno_Nombre, Sesion.SUSCRIPCION_ID);
        String Resultado = null;
        string[] Personas_Diario_IDS = CeC.ObtenArregoSeparador(Persona_Diario_IDS, ",");
        foreach (string Persona_Diario_ID in Personas_Diario_IDS)
        {
            Persona_ID = CeC_Asistencias.ObtenPersonaID(CeC.Convierte2Int(Persona_Diario_ID));
            Persona_Link_ID = CeC_BD.ObtenPersonaLinkID(Persona_ID);
            if (CeC_Turnos.AsignaHorario(Persona_ID, Turno_ID, FechaInicio, FechaFin, Sesion) > 0)
            {
                Resultado += Persona_Link_ID.ToString() + ",";
            }
            else
            {
                Resultado += "ERROR" + Persona_Link_ID.ToString() + ",";
            }
        }
        return Resultado;
    }

    /// <summary>
    /// Consulta la tabla de turnos para mostrar los turnos existentes 
    /// ya sea por semana o dia para ser desplegados sobre la vista.
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="Turno_ID"></param>
    /// <returns></returns>
    [OperationContract]
    public String ObtenDatos(string SesionSeguridad, int Turno_ID)
    {
        try
        {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        eClockBase.Modelos.Turnos.Model_Turno Turno = new eClockBase.Modelos.Turnos.Model_Turno();

        Turno.Turno.TURNO_ID = Turno_ID;
        string sTurno = CeC_Tabla.ObtenDatos("EC_TURNOS", "TURNO_ID", JsonConvert.SerializeObject(Turno.Turno), Sesion);
        Turno.Turno = JsonConvert.DeserializeObject<eClockBase.Modelos.Model_TURNOS>(sTurno);

        eClockBase.Modelos.Model_TURNOS_SEMANAL_DIA TSD = new eClockBase.Modelos.Model_TURNOS_SEMANAL_DIA();
        TSD.TURNO_ID = Turno_ID;
        string sTurnoDias = CeC_Tabla.ObtenDatosLista("EC_TURNOS_SEMANAL_DIA", "TURNO_ID", JsonConvert.SerializeObject(TSD), Sesion);
        //Agregado por Omar Trejo para mostrar el turno sin problemas.
        try
        {
        Turno.TurnoSemanalDias = JsonConvert.DeserializeObject<List<eClockBase.Modelos.Model_TURNOS_SEMANAL_DIA>>(sTurnoDias);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }//Agregado por Omar Trejo para mostrar el turno sin problemas.
        foreach (eClockBase.Modelos.Model_TURNOS_SEMANAL_DIA TSDL in Turno.TurnoSemanalDias)
        {
            eClockBase.Modelos.Model_TURNOS_DIA TD = new eClockBase.Modelos.Model_TURNOS_DIA();
            TD.TURNO_DIA_ID = TSDL.TURNO_DIA_ID;
            string sTurnoDia = CeC_Tabla.ObtenDatos("EC_TURNOS_DIA", "TURNO_DIA_ID", JsonConvert.SerializeObject(TD), Sesion);
            TD = JsonConvert.DeserializeObject<eClockBase.Modelos.Model_TURNOS_DIA>(sTurnoDia);
            Turno.TurnoDias.Add(TD);
        }
        return JsonConvert.SerializeObject(Turno);
    }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
            return "ERROR";
        }
    }

    /// <summary>
    /// Guarda los datos que se capturan sobre la vista de turnos
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="Datos"></param>
    /// <returns></returns>
    [OperationContract]
    public String GuardaDatos(string SesionSeguridad, string Datos)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        eClockBase.Modelos.Turnos.Model_Turno Turno = JsonConvert.DeserializeObject<eClockBase.Modelos.Turnos.Model_Turno>(Datos);
        try
        {
            bool EsNuevo = true;
            if (Turno.Turno.TURNO_ID > 0)
            {
                CeC_Turnos.LimpiaDiasTurno(Turno.Turno.TURNO_ID);
                EsNuevo = false;
            }
            int TurnoID = CeC_Tabla.GuardaDatos("EC_TURNOS", "TURNO_ID", JsonConvert.SerializeObject(Turno.Turno), EsNuevo, Sesion, Sesion.SUSCRIPCION_ID);
            if (EsNuevo)
                Turno.Turno.TURNO_ID = TurnoID;
            foreach (eClockBase.Modelos.Model_TURNOS_DIA TD in Turno.TurnoDias)
            {
                TD.TURNO_DIA_ID = CeC_Tabla.GuardaDatos("EC_TURNOS_DIA", "TURNO_DIA_ID", JsonConvert.SerializeObject(TD), true, Sesion, Sesion.SUSCRIPCION_ID);
            }
            for (int Cont = 0; Cont < Turno.TurnoSemanalDias.Count; Cont++)
            {
                eClockBase.Modelos.Model_TURNOS_SEMANAL_DIA TSDL = Turno.TurnoSemanalDias[Cont];
                TSDL.TURNO_ID = Turno.Turno.TURNO_ID;
                if (Turno.TurnoDias.Count == 1)
                    TSDL.TURNO_DIA_ID = Turno.TurnoDias[0].TURNO_DIA_ID;
                else
                    TSDL.TURNO_DIA_ID = Turno.TurnoDias[Cont].TURNO_DIA_ID;

                TSDL.TURNO_SEMANAL_DIA_ID = CeC_Tabla.GuardaDatos("EC_TURNOS_SEMANAL_DIA", "TURNO_SEMANAL_DIA_ID", JsonConvert.SerializeObject(TSDL), true, Sesion, Sesion.SUSCRIPCION_ID);
            }

            return JsonConvert.SerializeObject(Turno);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return "ERROR";
    }

    /// <summary>
    /// Asigna los turnos creados a un empleado en especifico
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="DatosTurnoImportacion"></param>
    /// <returns></returns>
    [OperationContract]
    public int AsignacionTurnosAEmpleados(string SesionSeguridad, string DatosTurnoImportacion)
    {
        return AsignaHorario(SesionSeguridad, DatosTurnoImportacion);
    }

    /// <summary>
    /// Funcion que procesa la asignacion de horarios
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="DatosTurnoAsignacion"></param>
    /// <returns></returns>
    [OperationContract]
    public int AsignaHorario(string SesionSeguridad, string DatosTurnoAsignacion)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return 0;
        try
        {
            CeC_Turnos Turno = new CeC_Turnos(Sesion);
            return Turno.AsignaHorario(DatosTurnoAsignacion, Sesion);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
            return 0;
        }
    }

    [OperationContract]
    public int AsignaHorarioAPersonaDiarioIDs(string SesionSeguridad, string Persona_Diario_IDs, int Turno_ID)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return 0;
        try
        {
            return CeC_Turnos.AsignaHorario(Persona_Diario_IDs, Turno_ID, Sesion);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
            return 0;
        }
    }
    [OperationContract]
    public int AsignaHorarioPredeterminadoAPersonaDiarioIDs(string SesionSeguridad, string Persona_Diario_IDs, int Turno_ID)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return 0;
        try
        {            
            return CeC_Turnos.AsignaHorarioPredPersonaDiarioIDs(Persona_Diario_IDs, Turno_ID, Sesion);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
            return 0;
        }
    }
}
