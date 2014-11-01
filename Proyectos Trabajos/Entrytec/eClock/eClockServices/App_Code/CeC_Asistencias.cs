using System;
using System.Web;
using System.Web.Mail;
using System.Net.Mail;
using eClock;
using System.Threading;
using EntryTec;
using System.Data.OleDb;
using System.Data;
using System.Drawing;
using System.Collections.Generic;

/// <summary>
/// Descripción breve de CeC_Asistencias.
/// </summary>
public class CeC_Asistencias
{
    public enum TipoAccesos
    {
        No_definido = 0,
        Correcto,
        Entrada,
        Salida,
        Salida_a_Comer,
        Regreso_de_comer,
        Incorrecto
    }

    public enum TipoIncComidas
    {
        Sin_calcular = 0,
        Correcta,
        No_Hay_Comida,
        Exedio_el_Tiempo,
        Registro_Incompleto
    }

    public static bool HayAccesosViejosPendientes = true;

    private static bool m_HayAccesosPendientes = true;
    /// <summary>
    /// Esta funcion se deberá llamar cada que se tengan
    /// accesos pendientes por procesar o se incerten nuevos accesos
    /// </summary>
    public static void CambioAccesosPendientes()
    {
        m_HayAccesosPendientes = true;
    }
    private static bool m_HayCambioRecalculaAccesos = true;

    /// <summary>
    /// esta funcion se deberá llamar cuando se manda a llamar a la funcion recalcula accesos
    /// </summary>
    public static void CambioRecalculaAccesos()
    {
        m_HayCambioRecalculaAccesos = true;
    }

    private static bool m_HayDatosEmpleadosDiferentes = true;

    /// <summary>
    /// Obtiene el estatus sobre si hay datos diferentes en los empleados,
    /// Altas, bajas, modificaciónes
    /// </summary>
    public static bool HayDatosEmpleadosDiferentes
    {
        get { return CeC_Asistencias.m_HayDatosEmpleadosDiferentes; }
    }
    /// <summary>
    /// esta funcion se deberá llamar cada que se editaron los datos de los empleados 
    /// ó se importaron datos o se cambio la configuración de grupos
    /// </summary>
    public static void CambioDatosEmp()
    {
        m_HayDatosEmpleadosDiferentes = true;
    }





    /// <summary>
    /// esta funcion se deberá llamar cada que agregen accesos no asignados 
    /// </summary>
    public static void CambioAccesosNAsign()
    {
        m_HayDatosEmpleadosDiferentes = true;
    }

    public enum Tipo_DEXTRAS
    {
        No_definido = 0,
        Accesos,
        Huellas
    }
    #region Servicio de generación de Asistencias

    private static bool m_Parar = false;
    private static bool m_ProcesandoFaltas = false;
    private static bool m_ProcesandoAsistencia = false;


    private static Thread m_ThreadAsistencias = null;
    private static Thread m_ThreadFaltas = null;

    private static bool m_IniciadoGeneracion = false;
    public static bool InicioGeneracion()
    {
        return m_IniciadoGeneracion;
    }

    /// <summary>
    /// Inicia el servicio de generación de asistencias
    /// </summary>
    /// <returns>Regresa falso si no se pudo iniciar</returns>
    public static bool IniciaGeneración()
    {
        if (m_IniciadoGeneracion)
        {
            CIsLog2.AgregaLog("Ya se ha iniciado");
            return false;
        }
        m_IniciadoGeneracion = true;

        //        CIsLog2.S_NombreDestino = HttpRuntime.AppDomainAppPath + CeC_Config.RutaReportesPDF + "eClock.log";
        //        CIsLog2.S_AutoNombreArchivo = true;
        //        CIsLog2.S_NombreDestino = HttpRuntime.AppDomainAppPath + CeC_Config.RutaReportesPDF + "eClock" + DateTime.Now.ToString(" yyMMdd HHmmss") + ".log";
        CIsLog2.AgregaLog("Borrando Sesiones Viejas");
        DateTime HoraParametro = DateTime.Now;
        HoraParametro = HoraParametro.AddDays(-2);
        int idSesionVar = CeC_BD.EjecutaEscalarInt("SELECT MAX(SESION_ID) AS ID FROM EC_SESIONES WHERE SESION_INICIO_FECHAHORA < " + CeC_BD.SqlFechaHora(HoraParametro));
        if (idSesionVar > 0)
            CeC_BD.EjecutaComando("DELETE FROM EC_SESIONES_VAR WHERE SESION_ID <= " + idSesionVar.ToString());
        CIsLog2.AgregaLog("IniciaGeneración");
        //Crea Modulos opcionales
        CMd_Base.CreaModulos();
        //CIsLog2.AgregaLog("Sin inicio");
        //return true;
        //if (DateTime.Now > new DateTime(2013, 11, 24))
        //return false;


        //if (CeC_BD.ObtenCadenaConfig("Asistencias", "True") == "True")
        {
            m_ThreadAsistencias = new Thread(new ThreadStart(GeneraAsistencias));
            m_ThreadAsistencias.Start();
        }
        //if (CeC_BD.ObtenCadenaConfig("Faltas", "True") == "True")
        {
            m_ThreadFaltas = new Thread(new ThreadStart(GeneraFaltas));
            m_ThreadFaltas.Start();
        }
        //if (CeC_BD.ObtenCadenaConfig("Mails", "True") == "True")
        {
            CeC_Mails.IniciaThread();
        }
        return true;
    }




    private static void GeneraAsistencias()
    {
        m_ProcesandoAsistencia = true;
        CIsLog2.AgregaLog("IniciaGeneraAsistencias");
        CeC_BD.CreaRelacionesEmpleados();
        while (!m_Parar)
        {
            try
            {
                if (m_HayDatosEmpleadosDiferentes)
                {
                    m_HayDatosEmpleadosDiferentes = false;
                    if (CeC_Accesos.ProcesaAccesosViejos() > 0)
                        m_HayAccesosPendientes = true;
                }

                if (m_HayCambioRecalculaAccesos)
                {
                    m_HayCambioRecalculaAccesos = false;
                    CeC_Asistencias.RecalculaAccesos();
                    m_HayAccesosPendientes = true;
                }

                if (m_HayAccesosPendientes)
                {
                    m_HayAccesosPendientes = false;
                    CeC_BD.EjecutaComando("UPDATE EC_ACCESOS SET ACCESO_CALCULADO = 1 WHERE ACCESO_CALCULADO = 0 AND TERMINAL_ID IN (SELECT TERMINAL_ID FROM EC_TERMINALES WHERE TERMINAL_ASISTENCIA = 0)");
                    int AccesoID = 1;
                    string QryAccesoID = "";
                    if (CeC_BD.EsOracle)
                        QryAccesoID = "SELECT ACCESO_ID FROM (SELECT ACCESO_ID FROM EC_ACCESOS WHERE TIPO_ACCESO_ID in (1,2,3,4,5) AND ACCESO_CALCULADO = 0 AND TERMINAL_ID IN (SELECT TERMINAL_ID FROM EC_TERMINALES WHERE TERMINAL_ASISTENCIA = 1) ORDER BY ACCESO_FECHAHORA) WHERE ROWNUM <= 1";
                    else
                    {
                        //Creo que quitando el top se incrementará la velocidad
                        //QryAccesoID = "SELECT TOP 1 ACCESO_ID FROM EC_ACCESOS WHERE TIPO_ACCESO_ID in (1,2,3,4,5) AND ACCESO_CALCULADO = 0 AND TERMINAL_ID IN (SELECT TERMINAL_ID FROM EC_TERMINALES WHERE TERMINAL_ASISTENCIA = 1) ORDER BY ACCESO_FECHAHORA ";
                        QryAccesoID = "SELECT ACCESO_ID FROM EC_ACCESOS WHERE TIPO_ACCESO_ID in (1,2,3,4,5) AND ACCESO_CALCULADO = 0 AND TERMINAL_ID IN (SELECT TERMINAL_ID FROM EC_TERMINALES WHERE TERMINAL_ASISTENCIA = 1) ORDER BY ACCESO_FECHAHORA ";
                    }

                    while (AccesoID > 0 && !m_Parar)
                    {

                        AccesoID = CeC_BD.EjecutaEscalarInt(QryAccesoID);
                        if (AccesoID > 0)
                        {
                            CeC_BD.EjecutaComando("UPDATE EC_ACCESOS SET ACCESO_CALCULADO = 1 WHERE ACCESO_ID = " + AccesoID);

                            try
                            {
                                int PERSONA_ID = CeC_BD.EjecutaEscalarInt("SELECT PERSONA_ID FROM EC_ACCESOS WHERE ACCESO_ID = " + AccesoID);
                                DateTime ACCESO_FECHAHORA = CeC_BD.EjecutaEscalarDateTime("SELECT ACCESO_FECHAHORA FROM EC_ACCESOS WHERE ACCESO_ID = " + AccesoID);
                                ProcesaAsistencia(AccesoID, PERSONA_ID, ACCESO_FECHAHORA);

                                // CeC_BD.CreaRelacionesEmpleados();

                            }
                            catch (Exception ex)
                            {
                                CIsLog2.AgregaError(ex);
                                CIsLog2.AgregaError("Acceso " + AccesoID + " No procesado");
                            }

                        }
                        CeC.Sleep(0);
                    }
                }



                CeC.Sleep(5000);
            }
            catch (Exception ex)
            {
                CIsLog2.AgregaError(ex);
            }
        }
        CIsLog2.AgregaLog("ParaGeneraAsistencias");
        m_ProcesandoAsistencia = false;

    }
    /// <summary>
    /// Esta funcion toma las checadas de los usuarios, su hora de entrada
    /// y su hora de salida, posteriormente actualiza la base con el timpo que 
    /// el usuario estuvo dentro del horario de trabajo.
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    private static int GeneraNoAsistencia()
    {
        int Ret = 0;
        DataSet DSPersonas = (DataSet)CeC_BD.EjecutaDataSet("SELECT PERSONA_DIARIO_ID, PERSONA_ID, PERSONA_DIARIO_FECHA, TURNO_DIA_HE, TURNO_DIA_HS FROM EC_PERSONAS_DIARIO,EC_TURNOS_DIA WHERE EC_PERSONAS_DIARIO.TURNO_DIA_ID = EC_TURNOS_DIA.TURNO_DIA_ID AND TIPO_INC_SIS_ID <> 13 AND TURNO_DIA_NO_ASIS = 1");
        if (DSPersonas == null || DSPersonas.Tables.Count < 1 || DSPersonas.Tables[0].Rows.Count < 1)
            return Ret;
        foreach (DataRow DR in DSPersonas.Tables[0].Rows)
        {
            //2 Entrada
            //3 Salida
            int PersonaID = CeC.Convierte2Int(DR["PERSONA_ID"]);
            DateTime Fecha = CeC.Convierte2DateTime(DR["PERSONA_DIARIO_FECHA"]);
            TimeSpan HoraEntrada = CeC_BD.DateTime2TimeSpan(CeC.Convierte2DateTime(DR["TURNO_DIA_HE"]));
            TimeSpan HoraSalida = CeC_BD.DateTime2TimeSpan(CeC.Convierte2DateTime(DR["TURNO_DIA_HS"]));
            int AccesoIDEntrada = CeC_Accesos.AgregarAcceso(PersonaID, -1, 2, Fecha.Add(HoraEntrada));
            int AccesoIDSalida = CeC_Accesos.AgregarAcceso(PersonaID, -1, 3, Fecha.Add(HoraSalida));
            DateTime DTTiempoDentro = CeC_BD.TimeSpan2DateTime(HoraSalida - HoraEntrada);
            CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_DIARIO SET ACCESO_E_ID = " + AccesoIDEntrada + ", ACCESO_S_ID = " + AccesoIDSalida + "," +
            " TIPO_INC_SIS_ID = 13, PERSONA_DIARIO_TT = " + CeC_BD.SqlFechaHora(DTTiempoDentro) +
                ", PERSONA_DIARIO_TE = " + CeC_BD.SqlFechaHora(DTTiempoDentro) + " WHERE PERSONA_DIARIO_ID = " + CeC.Convierte2Int(DR["PERSONA_DIARIO_ID"]));
            Ret++;
        }
        return Ret;
    }

    private static void GeneraFaltas()
    {
        m_ProcesandoFaltas = true;
        DateTime FechaAnterior = new DateTime(2002, 09, 24);
        DateTime UltimaHora = new DateTime(2002, 09, 24);
        while (!m_Parar)
        {
            try
            {
                DateTime FechaActual = DateTime.Now;
                if (FechaAnterior.Date < FechaActual.Date)
                {
                    GeneraNoAsistencia();
                    ///asigna como dia que no checa a todas las personas que no checan
                    //CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_DIARIO SET TIPO_INC_SIS_ID = 13 WHERE TIPO_INC_SIS_ID <> 13 AND TURNO_DIA_ID IN (SELECT TURNO_DIA_ID FROM EC_TURNOS_DIA WHERE TURNO_DIA_NO_ASIS = 1)");
                    try
                    {
                        FechaAnterior = FechaActual;
                        CIsLog2.AgregaDebugF("GeneraFaltas", "Procesando Faltas del día " + FechaAnterior.Date.ToString());
                        object ObjDataSet = CeC_BD.EjecutaDataSet("SELECT PERSONA_ID FROM EC_PERSONAS, EC_TURNOS WHERE PERSONA_BORRADO = 0 AND EC_PERSONAS.TURNO_ID = EC_TURNOS.TURNO_ID AND EC_TURNOS.TURNO_ASISTENCIA <> 0 AND PERSONA_ID IN "
                         + "(SELECT PERSONA_ID FROM EC_PERSONAS_DIARIO WHERE PERSONA_DIARIO_FECHA < " + CeC_BD.SqlFecha(FechaAnterior) + " AND TIPO_INC_SIS_ID = 0 GROUP BY PERSONA_ID)");
                        if (ObjDataSet != null)
                        {
                            DataSet DS = (DataSet)ObjDataSet;
                            CIsLog2.AgregaDebugF("ChecaFaltas", "Procesando Faltas del día " + FechaAnterior.Date.ToString() + " de " + DS.Tables[0].Rows.Count.ToString() + " Empleados");
                            for (int Cont = 0; Cont < DS.Tables[0].Rows.Count && !m_Parar; Cont++)
                            {
                                //Se agrega un día de manera que el día actual (hoy) ponga falta a todo el personal
                                CeC_Asistencias.ProcesaFaltas(Convert.ToInt32(DS.Tables[0].Rows[Cont][0]), FechaAnterior.Date.AddDays(1));
                                //                                    CeC_Asistencias.ProcesaFaltas(Convert.ToInt32(DS.Tables[0].Rows[Cont][0]), FechaAnterior.Date);
                                CeC.Sleep(1);
                            }

                        }
                        CIsLog2.AgregaDebugF("GeneraFaltas", "Fin Procesando Faltas del día " + FechaAnterior.Date.ToString());
                        CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_DIARIO SET PERSONA_DIARIO_TES = (SELECT TURNO_DIA_HTIEMPO " +
    " FROM EC_TURNOS_DIA WHERE EC_TURNOS_DIA.TURNO_DIA_ID = EC_PERSONAS_DIARIO.TURNO_DIA_ID" +
    ")WHERE PERSONA_DIARIO_TES IS NULL AND TURNO_DIA_ID > 0");


                    }
                    catch (Exception ex)
                    {

                        CIsLog2.AgregaError(ex);
                    }
                    ///Deberia moverlo a CMd_Base.gEjecutarUnaVezCadaHora();
                    //EnviaMailsFaltas();
                    CeC_Personas.Borra();
                    CeC_IncidenciasInventario.CreaSaldosPendientes();
                    CMd_Base.gEjecutarUnaVezAlDia();
                }
                if (UltimaHora.AddHours(1) <= DateTime.Now)
                {
                    UltimaHora = DateTime.Now;
                    CMd_Base.gEjecutarUnaVezCadaHora();
                }
                CeC.Sleep(1000);

            }
            catch { }
        }

        m_ProcesandoFaltas = false;

    }


    public static bool ParaGeneración()
    {
        m_Parar = true;
        for (int Cont = 0; Cont < 600 && (m_ProcesandoFaltas || m_ProcesandoAsistencia); Cont++)
            CeC.Sleep(100);
        if (m_ThreadAsistencias != null)
            m_ThreadAsistencias.Abort();
        if (m_ThreadFaltas != null)
            m_ThreadFaltas.Abort();
        CeC_Mails.ParaThread();

        return true;
    }
    #endregion
    //Contiene el numero de dia para cada diaID
    private static int[] m_DiasSql = null;
    /// <summary>
    /// Obtiene el número de dia al que corresponde en SQL un Dia_ID 
    /// este ultimo empieza en domingo como dia_id = 1
    /// </summary>
    /// <param name="DiaID">Identificador de día</param>
    public static int DiaID2DiaSql(int DiaID)
    {
        if (m_DiasSql == null)
        {
            m_DiasSql = new int[8];
            for (int Cont = 0; Cont < m_DiasSql.Length; Cont++)
                m_DiasSql[Cont] = -1;

        }
        int DiaSql = DiaID;
        if (m_DiasSql[DiaID] < 0)
        {
            if (CeC_BD.EsOracle)
                m_DiasSql[DiaID] = CeC_BD.EjecutaEscalarInt(" select to_char(" + CeC_BD.SqlFecha(CeC_BD.FechaNula.AddDays(DiaID - 1)) + ",'D') from EC_TIPO_INC_SIS where TIPO_INC_SIS_ID = 1");
            else
                m_DiasSql[DiaID] = CeC_BD.EjecutaEscalarInt(" select DATEPART(weekday," + CeC_BD.SqlFecha(CeC_BD.FechaNula.AddDays(DiaID - 1)) + ") from EC_TIPO_INC_SIS where TIPO_INC_SIS_ID = 1");
        }
        return m_DiasSql[DiaID];
    }




    public enum TIPO_INC_SIS
    {
        Asistencia_Automatica = -2,
        No_Activo,
        Sin_calcular,//0
        Asistencia_Normal,
        Retardo,
        Falta_Entrada,
        Falta_Salida,
        Salida_Temprano,//5
        Falta_Entrada_y_Salida,
        Falta_Entrada_y_Salida_Temprano,
        Retardo_y_Falta_Salida,
        Retardo_y_Salida_Temprano,
        Dia_no_Laborable,//10
        Dia_Festivo,
        Falta,
        No_Checa,
        Descanso_Trabajado,
        Festivo_Trabajado,//15
        RetardoB,
        RetardoB_y_Falta_Salida,//17
        RetardoB_y_Salida_Temprano,
        RetardoC,
        RetardoC_y_Falta_Salida,//20
        RetardoC_y_Salida_Temprano,
        RetardoD,
        RetardoD_y_Falta_Salida,//23
        RetardoD_y_Salida_Temprano

    }
    /// <summary>
    /// Valida si es retardo, o salida temprano o ambos
    /// </summary>
    /// <param name="TIPO_INC_SIS_ID"></param>
    /// <returns></returns>
    public static bool EsSoloRetardo(int TIPO_INC_SIS_ID)
    {
        try
        {
            TIPO_INC_SIS Inc = (TIPO_INC_SIS)TIPO_INC_SIS_ID;
            if (Inc == TIPO_INC_SIS.Retardo || Inc == TIPO_INC_SIS.Retardo_y_Salida_Temprano || Inc == TIPO_INC_SIS.Salida_Temprano)
                return true;
            if (Inc == TIPO_INC_SIS.RetardoB || Inc == TIPO_INC_SIS.RetardoB_y_Salida_Temprano || Inc == TIPO_INC_SIS.Salida_Temprano)
                return true;
            if (Inc == TIPO_INC_SIS.RetardoC || Inc == TIPO_INC_SIS.RetardoC_y_Salida_Temprano || Inc == TIPO_INC_SIS.Salida_Temprano)
                return true;
            if (Inc == TIPO_INC_SIS.RetardoD || Inc == TIPO_INC_SIS.RetardoD_y_Salida_Temprano || Inc == TIPO_INC_SIS.Salida_Temprano)
                return true;
        }
        catch { }
        return false;
    }
    /// <summary>
    /// Selecciona los dias de trabajo si son mayor a 3600 los devuelve
    /// en caso contrario los inserta sobre la tabla de un rango menor ala FechaFinal.
    /// </summary>
    /// <param name=""></param>
    /// <returns>Devuelve los registros ya se seleccionados o almacenados</returns>
    static int GeneraDiasTrabajo()
    {
        int Registros = CeC_BD.EjecutaEscalarInt("SELECT COUNT (DIAS_TRABAJO) FROM EC_DIAS_TRABAJO");
        if (Registros > 3600)
        {
            return Registros;
        }
        DateTime Fecha = new DateTime(2004, 01, 01);
        DateTime FechaFinal = new DateTime(2020, 01, 01);
        Registros = 0;
        while (Fecha < FechaFinal)
        {

            string Insert = "INSERT INTO EC_DIAS_TRABAJO (DIAS_TRABAJO) VALUES(" + CeC_BD.SqlFecha(Fecha) + ")";
            CeC_BD.EjecutaComando(Insert);
            Fecha = Fecha.AddDays(1);
            Registros++;
        }
        return Registros;
    }
    /// <summary>
    /// Obtiene la fecha de diario de un usuario.
    /// </summary>
    /// <param name="Persona_Diario_ID">Id de usuario a obtener fechas</param>
    /// <returns></returns>
    public static DateTime ObtenFecha(int Persona_Diario_ID)
    {

        return CeC_BD.EjecutaEscalarDateTime("SELECT PERSONA_DIARIO_FECHA FROM EC_PERSONAS_DIARIO WHERE PERSONA_DIARIO_ID = " + Persona_Diario_ID);
    }
    /// <summary>
    /// Obtiene el Id de un usuario en especifico.
    /// </summary>
    /// <param name="Persona_Diario_ID"></param>
    /// <returns></returns>
    public static int ObtenPersonaID(int Persona_Diario_ID)
    {
        return CeC_BD.EjecutaEscalarInt("SELECT PERSONA_ID FROM EC_PERSONAS_diario WHERE persona_diario_ID = " + Persona_Diario_ID);
    }

    /// <summary>
    /// Calcula las asistencias por medio de la FechaInicio
    /// y una FechaFin con respecto a un ID de usuario.
    /// </summary>
    /// <param name="Persona_ID"></param>
    /// <param name="FechaInicio"></param>
    /// <param name="FechaFin"></param>
    /// <returns></returns>
    public static int RecalculaAsistencia(int Persona_ID, DateTime FechaInicio, DateTime FechaFin)
    {
        int Dias = 0;
        DateTime Fecha = FechaInicio;
        while (Fecha <= FechaFin)
        {
            LimpiaPersonaDiario(Persona_ID, Fecha);
            Fecha = Fecha.AddDays(1);
        }
        RecalculaAccesos(Persona_ID, FechaInicio, FechaFin);
        ProcesaFaltas(Persona_ID, FechaFin.AddDays(1));
        return Dias;
    }

    /// <summary>
    /// Calcula las asistencias a diario de un conjunto de personas en especifico.
    /// </summary>
    /// <param name="PersonasDiarioIDs"></param>
    /// <param name="PersonasIDs">si no se conoce se puede usar "" y se obtendrá automaticamente</param>
    /// <returns></returns>
    public static int RecalculaAsistencia(string PersonasDiarioIDs, string PersonasIDs)
    {
        CeC_Asistencias.LimpiaPersonasDiario(PersonasDiarioIDs);
        CeC_Asistencias.RecalculaAccesos(PersonasDiarioIDs);
        if (PersonasIDs == "")
            PersonasIDs = ObtenPersonasIDs(PersonasDiarioIDs);
        CeC_Asistencias.ProcesaFaltas(PersonasIDs, DateTime.Today);
        return 1;
    }

    /// <summary>
    /// Calcula los accesos de un usuario en especifico.
    /// </summary>
    /// <param name="Persona_ID"></param>
    /// <param name="FechaInicio"></param>
    /// <param name="FechaFin"></param>
    /// <returns></returns>
    public static bool RecalculaAccesos(int Persona_ID, DateTime FechaInicio, DateTime FechaFin)
    {
        Console.WriteLine("Inicio de RecalculaAccesos   Per_ID = ({0:G}) {1:G} ", Persona_ID, DateTime.Now.ToString());

        int Registros = 0;
        try
        {
            //if (CeC_Config.Recalcula)
            //{

            CeC_Config.Recalcula = false;
            DateTime FInicial = FechaInicio;
            DateTime FFinal = FechaFin;

            if (FFinal.TimeOfDay.TotalSeconds == 0)
                FFinal = FFinal.AddDays(1);
            else
                FFinal = FFinal.AddMinutes(1);

            string Query_Recalculo = "";
            if (FFinal < FInicial)
            {
                DateTime Fecha = FFinal;
                FFinal = FInicial;
                FInicial = Fecha;
            }

            Query_Recalculo = "SELECT ACCESO_ID, ACCESO_FECHAHORA FROM EC_ACCESOS WHERE ACCESO_FECHAHORA >= "
                + CeC_BD.SqlFechaHora(FInicial) + " AND ACCESO_FECHAHORA < " +
                CeC_BD.SqlFechaHora(FFinal) + " AND TIPO_ACCESO_ID <> 6 AND TIPO_ACCESO_ID <> 0 and Persona_id = "
                + Persona_ID + " AND TERMINAL_ID IN (SELECT TERMINAL_ID FROM EC_TERMINALES WHERE TERMINAL_ASISTENCIA = 1) "
                //            + " \n ORDER BY ACCESO_ID";
            + " \n ORDER BY ACCESO_FECHAHORA";

            DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Query_Recalculo);

            int Total = DS.Tables[0].Rows.Count;
            Console.WriteLine("RecalculaAccesos Por calcular   ({0:G}) {1:G} ", Total, DateTime.Now.ToString());
            for (int Cont = 0; Cont < Total; Cont++)
            {
                int IDAcceso = Convert.ToInt32(DS.Tables[0].Rows[Cont][0]);
                DateTime FechaHora = Convert.ToDateTime(DS.Tables[0].Rows[Cont][1]);
                Console.WriteLine("RecalculaAccesos  ProcesaAsistencia IDAcceso = {0:G}, Persona_ID= {1:G}, FechaHora= {2:G}", IDAcceso, Persona_ID, FechaHora.ToString());
                //Realiza el acomodo correspondiente del acceso en la asistencia que le corresponda
                ProcesaAsistencia(IDAcceso, Persona_ID, FechaHora);
                //	}
            }
            CambioAccesosPendientes();
            return true;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
            return false;
        }
        Console.WriteLine("Fin de RecalculaAccesos   ({0:G}) {1:G} ", Registros, DateTime.Now.ToString());
    }
    /// <summary>
    /// Manda a llamar la funcion de LimpiaPersonasDiario para 
    /// poder pasar los aprametros de ejecucion de la misma funcion.
    /// </summary>
    /// <param name="Persona_Diario_ID"></param>
    /// <returns></returns>
    public static bool LimpiaPersonaDiario(int Persona_Diario_ID)
    {
        return LimpiaPersonasDiario(Persona_Diario_ID.ToString());
    }
    /// <summary>
    /// Limpia el horario diario de usuarios.
    /// </summary>
    /// <param name="Persona_Diario_IDs"></param>
    /// <returns></returns>
    public static bool LimpiaPersonasDiario(string Persona_Diario_IDs)
    {

        try
        {
            //bORRA EL LAS ENTRADAS Y SALIDAS DE UN PERSONA DIARIO
            CeC_BD.EjecutaComando("DELETE EC_PERSONAS_ES WHERE PERSONA_DIARIO_ID in (" + Persona_Diario_IDs + ")");
            //Limpioa el persona Diario
            CeC_BD.EjecutaComando("Update EC_PERSONAS_diario set" +
                " ACCESO_E_ID = 0 ," +
                " ACCESO_S_ID = 0 , " +
                " ACCESO_CS_ID = 0 , " +
                " ACCESO_CR_ID = 0 , " +
                " TIPO_INC_SIS_ID = 0 , " +
                " TIPO_INC_C_SIS_ID = 0 , " +
                " PERSONA_DIARIO_TT = " + CeC_BD.SqlFechaHora(CeC_BD.FechaNula) + " , " +
                " PERSONA_DIARIO_TE = " + CeC_BD.SqlFechaHora(CeC_BD.FechaNula) + " , " +
                " PERSONA_DIARIO_TC = " + CeC_BD.SqlFechaHora(CeC_BD.FechaNula) + " , " +
                " PERSONA_DIARIO_TDE = " + CeC_BD.SqlFechaHora(CeC_BD.FechaNula) + ", " +
                " PERSONA_D_HE_ID = 0 " +
                " WHERE PERSONA_DIARIO_ID in (" + Persona_Diario_IDs + ")");
            CeC_BD.EjecutaComando("DELETE EC_PERSONAS_ES WHERE PERSONA_DIARIO_ID in (" + Persona_Diario_IDs + ")");
            return true;
        }
        catch
        {
            return false;
        }
    }
    /// <summary>
    /// Borra las entradas y salidas a diario de un usuario.
    /// </summary>
    /// <param name="Persona_ID"></param>
    /// <param name="FechaInicio"></param>
    /// <returns></returns>
    public static bool LimpiaPersonaDiario(int Persona_ID, DateTime FechaInicio)
    {

        try
        {
            //bORRA EL LAS ENTRADAS Y SALIDAS DE UN PERSONA DIARIO 
            CeC_BD.EjecutaComando("DELETE EC_PERSONAS_ES WHERE PERSONA_DIARIO_ID in (" +
                "SELECT PERSONA_DIARIO_ID FROM EC_PERSONAS_DIARIO " +
                " WHERE PERSONA_ID = " + Persona_ID + " AND PERSONA_DIARIO_FECHA = " + CeC_BD.SqlFecha(FechaInicio) +
                ")");
            //Limpia el persona Diario

            CeC_BD.EjecutaComando("Update EC_PERSONAS_DIARIO set" +
                " ACCESO_E_ID = 0 ," +
                " ACCESO_S_ID = 0 , " +
                " ACCESO_CS_ID = 0 , " +
                " ACCESO_CR_ID = 0 , " +
                " TIPO_INC_SIS_ID = 0 , " +
                " TIPO_INC_C_SIS_ID = 0 , " +
                " PERSONA_DIARIO_TT = " + CeC_BD.SqlFechaHora(CeC_BD.FechaNula) + " , " +
                " PERSONA_DIARIO_TE = " + CeC_BD.SqlFechaHora(CeC_BD.FechaNula) + " , " +
                " PERSONA_DIARIO_TC = " + CeC_BD.SqlFechaHora(CeC_BD.FechaNula) + " , " +
                " PERSONA_DIARIO_TDE = " + CeC_BD.SqlFechaHora(CeC_BD.FechaNula) + " " +
                " WHERE PERSONA_ID = " + Persona_ID + " AND PERSONA_DIARIO_FECHA = " + CeC_BD.SqlFecha(FechaInicio));

            CeC_AsistenciasHE.QuitaHoraExtra(Persona_ID, FechaInicio, false);
            return true;
        }
        catch
        {
            return false;
        }

    }
    /// <summary>
    /// Borra las entradas y salidas a diario de un usuario con respecto a una FechaInicio y una FechaFin.
    /// </summary>
    /// <param name="FechaInicio"></param>
    /// <param name="FechaFin"></param>
    /// <returns></returns>
    public static bool LimpiaPersonaDiario(DateTime FechaInicio, DateTime FechaFin)
    {

        try
        {
            //bORRA EL LAS ENTRADAS Y SALIDAS DE UN PERSONA DIARIO
            CeC_BD.EjecutaComando("DELETE EC_PERSONAS_ES WHERE PERSONA_DIARIO_ID in (" +
                "SELECT PERSONA_DIARIO_ID FROM EC_PERSONAS_DIARIO " +
                " WHERE PERSONA_DIARIO_FECHA >= " + CeC_BD.SqlFecha(FechaInicio) + " AND PERSONA_DIARIO_FECHA <= " + CeC_BD.SqlFecha(FechaFin) +
                ")");
            //Limpia el persona Diario

            CeC_BD.EjecutaComando("Update EC_PERSONAS_DIARIO set" +
                " ACCESO_E_ID = 0 ," +
                " ACCESO_S_ID = 0 , " +
                " ACCESO_CS_ID = 0 , " +
                " ACCESO_CR_ID = 0 , " +
                " TIPO_INC_SIS_ID = 0 , " +
                " TIPO_INC_C_SIS_ID = 0 , " +
                " PERSONA_DIARIO_TT = " + CeC_BD.SqlFechaHora(CeC_BD.FechaNula) + " , " +
                " PERSONA_DIARIO_TE = " + CeC_BD.SqlFechaHora(CeC_BD.FechaNula) + " , " +
                " PERSONA_DIARIO_TC = " + CeC_BD.SqlFechaHora(CeC_BD.FechaNula) + " , " +
                " PERSONA_DIARIO_TDE = " + CeC_BD.SqlFechaHora(CeC_BD.FechaNula) + " " +
                " WHERE PERSONA_DIARIO_FECHA >= " + CeC_BD.SqlFecha(FechaInicio) + " AND PERSONA_DIARIO_FECHA <= " + CeC_BD.SqlFecha(FechaFin)
                );

            CeC_AsistenciasHE.QuitaHoraExtra(FechaInicio, FechaFin);
            return true;
        }
        catch
        {
            return false;
        }

    }
    /// <summary>
    /// Selecciona un usuario que se encuentra dentro de N horario.
    /// </summary>
    /// <param name="Persona_ID"></param>
    /// <param name="FechaHora"></param>
    /// <returns></returns>
    public static int EstaDetronHorario(int Persona_ID, DateTime FechaHora)
    {
        string Qry = "SELECT PERSONA_DIARIO_ID FROM ( " +
            "select PERSONA_DIARIO_ID,PERSONA_DIARIO_FECHA , " +
            " PERSONA_DIARIO_FECHA + (turno_dia_hemin - " + CeC_BD.SqlFechaNula() + ") AS HEMIN, " +
            " PERSONA_DIARIO_FECHA + (TURNO_DIA_HSMAX - " + CeC_BD.SqlFechaNula() + ") AS HSMAX " +
            " from EC_PERSONAS_diario, EC_TURNOS_DIA where  EC_PERSONAS_diario.TURNO_DIA_ID= EC_TURNOS_DIA.TURNO_DIA_ID " +
            " AND EC_PERSONAS_diario.persona_ID  = " + Persona_ID + ")t where " + CeC_BD.SqlFechaHora(FechaHora) +
            " BETWEEN HEMIN AND HSMAX";
        return CeC_BD.EjecutaEscalarInt(Qry);
    }
    /// <summary>
    /// Recalcula los accesos de un conjunto de usuarios.
    /// </summary>
    /// <param name="Persona_DiarioIDs">ID de los usuarios que se requiere calcular los accesos.</param>
    /// <returns></returns>
    public static bool RecalculaAccesos(string Persona_DiarioIDs)
    {
        try
        {
            string Qry = "SELECT ACCESO_ID, EC_ACCESOS.PERSONA_ID, ACCESO_FECHAHORA FROM EC_ACCESOS, ( " +
        "select PERSONA_ID,PERSONA_DIARIO_FECHA , " +
        " PERSONA_DIARIO_FECHA + (turno_dia_hemin - " + CeC_BD.SqlFechaNula() + ") AS HEMIN, " +
        " PERSONA_DIARIO_FECHA + (TURNO_DIA_HSMAX - " + CeC_BD.SqlFechaNula() + ") AS HSMAX " +
        " from EC_PERSONAS_DIARIO, EC_TURNOS_DIA where  EC_PERSONAS_diario.TURNO_DIA_ID= EC_TURNOS_DIA.TURNO_DIA_ID " +
        " AND EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID  in(" + Persona_DiarioIDs + "))t where " +
        " EC_ACCESOS.PERSONA_ID = t.PERSONA_ID AND ACCESO_FECHAHORA " +
        " BETWEEN HEMIN AND HSMAX  AND TIPO_ACCESO_ID <> 6 AND TIPO_ACCESO_ID <> 0 " +
        "AND TERMINAL_ID IN (SELECT TERMINAL_ID FROM EC_TERMINALES WHERE TERMINAL_ASISTENCIA = 1) " +
        " \n ORDER BY PERSONA_ID, ACCESO_FECHAHORA";
            DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry);
            int Total = DS.Tables[0].Rows.Count;
            Console.WriteLine("RecalculaAccesos Por calcular   ({0:G}) {1:G} ", Total, DateTime.Now.ToString());
            foreach (DataRow Fila in DS.Tables[0].Rows)
            {
                int Persona_ID = CeC.Convierte2Int(Fila["PERSONA_ID"]);
                int IDAcceso = CeC.Convierte2Int(Fila["ACCESO_ID"]);
                DateTime FechaHora = CeC.Convierte2DateTime(Fila["ACCESO_FECHAHORA"]);
                Console.WriteLine("RecalculaAccesos  ProcesaAsistencia IDAcceso = {0:G}, Persona_ID= {1:G}, FechaHora= {2:G}", IDAcceso, Persona_ID, FechaHora.ToString());
                //Realiza el acomodo correspondiente del acceso en la asistencia que le corresponda
                ProcesaAsistencia(IDAcceso, Persona_ID, FechaHora);
                //	}
            }
            CambioAccesosPendientes();
            return true;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
            return false;
        }
    }
    /// <summary>
    /// Envia un e-mail en cuanto un empleado tenga un retardo.
    /// </summary>
    /// <param name="Persona_DiarioIDs">ID de los usuarios que se requiere calcular los accesos.</param>
    /// <param name="Checada"></param>
    /// <param name="HorarioEntrada"></param>
    /// <param name="Persona_Diario_Id"></param>
    /// <returns></returns>
    public static bool EnviaMailRetardo(int Persona_ID, DateTime Checada, DateTime HorarioEntrada, int Persona_Diario_Id)
    {
        try
        {
            if (!CeC_Config.AlertaRetardosEmpleado && !CeC_Config.AlertaRetardosSupervisor)
                return false;


            int PersonaLink = CeC_BD.ObtenPersonaLinkID(Persona_ID);
            string Persona_Nombre = CeC_BD.ObtenPersonaNombre(Persona_ID);


            string Titulo = "Retardo de " + Persona_Nombre + " (" + PersonaLink + ") para el día " + HorarioEntrada.ToShortDateString();
            string Mensaje = "El empleado " + Persona_Nombre + " (" + PersonaLink + ")" + "<BR>";
            Mensaje += "llego tarde, la hora de entrada debio ser: " + HorarioEntrada + "<BR>";
            Mensaje += "sin embargo checo a las: " + Checada + "<BR>";
            if (CeC_Config.AlertaRetardosEmpleado)
                EnviaMail(Persona_ID, Titulo, Mensaje);
            if (CeC_Config.AlertaRetardosSupervisor)
            {
                string Qry = "SELECT    EC_USUARIOS.USUARIO_EMAIL " +
                            "FROM         EC_USUARIOS_PERMISOS INNER JOIN " +
                            "EC_PERSONAS ON EC_USUARIOS_PERMISOS.SUSCRIPCION_ID = EC_PERSONAS.SUSCRIPCION_ID AND  " +
                            "EC_PERSONAS.AGRUPACION_NOMBRE LIKE EC_USUARIOS_PERMISOS.USUARIO_PERMISO + '%' INNER JOIN " +
                            "EC_USUARIOS ON EC_USUARIOS_PERMISOS.USUARIO_ID = EC_USUARIOS.USUARIO_ID " +
                            "WHERE     (EC_USUARIOS_PERMISOS.TIPO_PERMISO_ID > 0) AND (EC_PERSONAS.PERSONA_ID = " + Persona_ID + ") ";
                DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry);
                foreach (DataRow DR in DS.Tables[0].Rows)
                {
                    try
                    {
                        string eMail = DR[0].ToString();
                        EnviaMail(eMail, Titulo, Mensaje);

                    }
                    catch { }
                }
            }
        }
        catch
        {
            return false;
        }
        return true;
    }
    public static bool EnviaMail(string EMail, string Titulo, string Texto)
    {

        string Cuerpo = Texto;
        //        Cuerpo += "<BR> <BR> Mensaje enviado el: " + DateTime.Now.ToString("F");
        //        Cuerpo += "<BR> por el Sistema de asistencia. ";
        //        Cuerpo += "<BR> <hr width=\"300\" align=\"left\"><a href=\"http://www.EntryTec.com.mx\">http://www.EntryTec.com.mx</a> <BR>";
        return CeC_Mails.EnviarMail(EMail, Titulo, Cuerpo);
    }
    /// <summary>
    /// Selecciona la persona ala que se le enviara el e-mail de reporte
    /// de retardos por asistencia, incluyendo Email, Titulo, y cuerpo de dicho reporte.
    /// </summary>
    /// <param name="Persona_ID"></param>
    /// <param name="Titulo"></param>
    /// <param name="Texto"></param>
    /// <returns></returns>
    public static bool EnviaMail(int Persona_ID, string Titulo, string Texto)
    {
        string Email = CeC_BD.EjecutaEscalarString("SELECT PERSONA_EMAIL FROM EC_PERSONAS WHERE PERSONA_ID = " + Persona_ID);
        if (Email.Length < 8)
            return false;


        try
        {
            CeT_EmailAddress EAdd = new CeT_EmailAddress(Email);
            if (!EAdd.IsValid)
                return false;
            string Nombre = CeC_BD.ObtenPersonaNombre(Persona_ID);


            string Cuerpo = Nombre + "<BR><BR>" + Texto;
            Cuerpo += "<BR> <BR> Mensaje enviado el: " + DateTime.Now.ToString("F");
            Cuerpo += "<BR> por el Sistema de asistencia. ";
            Cuerpo += "<BR> <hr width=\"300\" align=\"left\"><a href=\"" + CeC_Config.LinkURL + "\">" + CeC_Config.LinkURL + "</a> <BR>";
            CeC_Mails.EnviarMail(Email, Titulo, Cuerpo);
            return true;

        }
        catch (Exception ex)
        {
            Console.WriteLine("Error EnviaMail " + ex.Message);
            return false;
        }


    }
    /// <summary>
    /// Actualmente esta funcion se encarga de calcular los retardos
    /// mayores al tiempo permitido o estipulado.
    /// </summary>
    /// <param name="RTurnos_Dia"></param>
    /// <param name="FechaHoraEntrada"></param>
    /// <param name="Fecha"></param>
    /// <param name="TipoIncidenciaSisID"></param>
    /// <returns></returns>
    public static int CalculaRetardosMayores(DS_CEC_Asistencias.EC_TURNOS_DIARow RTurnos_Dia, DateTime FechaHoraEntrada, DateTime Fecha, int TipoIncidenciaSisID)
    {
        DateTime HERetardo = Fecha + CeC_BD.DateTime2TimeSpan(RTurnos_Dia.TURNO_DIA_HERETARDO);
        DateTime HERetardoB = Fecha + CeC_BD.DateTime2TimeSpan(RTurnos_Dia.TURNO_DIA_HERETARDO_B);
        DateTime HERetardoC = Fecha + CeC_BD.DateTime2TimeSpan(RTurnos_Dia.TURNO_DIA_HERETARDO_C);
        DateTime HERetardoD = Fecha + CeC_BD.DateTime2TimeSpan(RTurnos_Dia.TURNO_DIA_HERETARDO_D);
        int R = TipoIncidenciaSisID;
        if (HERetardoB > HERetardo && FechaHoraEntrada >= HERetardoB)
        {
            if (TipoIncidenciaSisID == Convert.ToInt32(TIPO_INC_SIS.Retardo_y_Falta_Salida))
                R = Convert.ToInt32(TIPO_INC_SIS.RetardoB_y_Falta_Salida);
            else
                if (TipoIncidenciaSisID == Convert.ToInt32(TIPO_INC_SIS.Retardo_y_Salida_Temprano))
                    R = Convert.ToInt32(TIPO_INC_SIS.RetardoB_y_Salida_Temprano);
                else
                    R = Convert.ToInt32(TIPO_INC_SIS.RetardoB);
            if (HERetardoC > HERetardoB && FechaHoraEntrada >= HERetardoC)
            {
                if (TipoIncidenciaSisID == Convert.ToInt32(TIPO_INC_SIS.Retardo_y_Falta_Salida))
                    R = Convert.ToInt32(TIPO_INC_SIS.RetardoC_y_Falta_Salida);
                else
                    if (TipoIncidenciaSisID == Convert.ToInt32(TIPO_INC_SIS.Retardo_y_Salida_Temprano))
                        R = Convert.ToInt32(TIPO_INC_SIS.RetardoC_y_Salida_Temprano);
                    else
                        R = Convert.ToInt32(TIPO_INC_SIS.RetardoC);
                if (HERetardoD > HERetardoC && FechaHoraEntrada >= HERetardoD)
                {
                    if (TipoIncidenciaSisID == Convert.ToInt32(TIPO_INC_SIS.Retardo_y_Falta_Salida))
                        R = Convert.ToInt32(TIPO_INC_SIS.RetardoD_y_Falta_Salida);
                    else
                        if (TipoIncidenciaSisID == Convert.ToInt32(TIPO_INC_SIS.Retardo_y_Salida_Temprano))
                            R = Convert.ToInt32(TIPO_INC_SIS.RetardoD_y_Salida_Temprano);
                        else
                            R = Convert.ToInt32(TIPO_INC_SIS.RetardoD);
                }
            }
        }
        return R;
    }
    public static int ObtenNoHorasExtras(TimeSpan TiempoExtra)
    {
        int Horas = 0;
        double HorasDecimal = 0;
        if (CeC.Convierte2Int(TiempoExtra.TotalMinutes) >= CeC_Config.MinutosParaHoraExtra)
        {
            Horas = TiempoExtra.Hours;
            if (CeC.Convierte2Int(TiempoExtra.Minutes) >= CeC_Config.MinutosParaHoraExtra)
                Horas++;
        }
        else
            Horas = 0;
        return Horas;
    }
    public static int ObtenNoHorasExtras(TimeSpan TiempoExtra, TimeSpan TiempoExtraDespues)
    {
        int Horas = 0;
        double HorasDecimal = 0;

        if (CeC_Config.HorasExtras_Antes)
            Horas = ObtenNoHorasExtras(TiempoExtra);
        else
            Horas = ObtenNoHorasExtras(TiempoExtraDespues);
        return Horas;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="RPersona_Diario"></param>
    /// <param name="RTurnos_Dia"></param>
    /// <param name="EnviarMail"></param>
    /// <returns></returns>
    public static int CalculaDia(DS_CEC_Asistencias.EC_PERSONAS_DIARIORow RPersona_Diario, DS_CEC_Asistencias.EC_TURNOS_DIARow RTurnos_Dia, bool EnviarMail)
    {
        try
        {
            RPersona_Diario.PERSONA_DIARIO_TDE = CeC_BD.FechaNula;
            RPersona_Diario.PERSONA_DIARIO_TT = CeC_BD.FechaNula;
            RPersona_Diario.PERSONA_DIARIO_TE = CeC_BD.FechaNula;
            RPersona_Diario.PERSONA_DIARIO_TC = CeC_BD.FechaNula;
            RPersona_Diario.PERSONA_DIARIO_TES = CeC_BD.FechaNula;

            DateTime HEMin = RPersona_Diario.PERSONA_DIARIO_FECHA + CeC_BD.DateTime2TimeSpan(RTurnos_Dia.TURNO_DIA_HEMIN);
            DateTime HE = RPersona_Diario.PERSONA_DIARIO_FECHA + CeC_BD.DateTime2TimeSpan(RTurnos_Dia.TURNO_DIA_HE);
            DateTime HERetardo = RPersona_Diario.PERSONA_DIARIO_FECHA + CeC_BD.DateTime2TimeSpan(RTurnos_Dia.TURNO_DIA_HERETARDO);
            DateTime HEMax = RPersona_Diario.PERSONA_DIARIO_FECHA + CeC_BD.DateTime2TimeSpan(RTurnos_Dia.TURNO_DIA_HEMAX);
            DateTime HSMin = RPersona_Diario.PERSONA_DIARIO_FECHA + CeC_BD.DateTime2TimeSpan(RTurnos_Dia.TURNO_DIA_HSMIN);
            DateTime HS = RPersona_Diario.PERSONA_DIARIO_FECHA + CeC_BD.DateTime2TimeSpan(RTurnos_Dia.TURNO_DIA_HS);
            DateTime HSMax = RPersona_Diario.PERSONA_DIARIO_FECHA + CeC_BD.DateTime2TimeSpan(RTurnos_Dia.TURNO_DIA_HSMAX);

            Console.WriteLine("CalculaDia {0:G}, {1:G}", RPersona_Diario.ToString(), RTurnos_Dia.ToString());

            DateTime Entrada = HE;
            DateTime Salida = HSMin;
            if (!RPersona_Diario.IsACCESO_ENull())
                Entrada = RPersona_Diario.ACCESO_E;

            ///Tolerancia extra que tendrá el sistema Esta aplicando por el 
            ///momento solo a retardos y entradas, esta ultima en FlexTime
            TimeSpan ToleranciaExtra = CeC_Config.TiempoToleranciaExtraTS;
            if (!RTurnos_Dia.IsTURNO_DIA_HTIEMPONull())
                RPersona_Diario.PERSONA_DIARIO_TES = RTurnos_Dia.TURNO_DIA_HTIEMPO;

            if (RTurnos_Dia.IsTURNO_DIA_HBLOQUENull() || RTurnos_Dia.TURNO_DIA_HBLOQUE.TimeOfDay.TotalMinutes < 1)
            { //Horario Semanal
                if (!RPersona_Diario.IsACCESO_ENull() && RPersona_Diario.ACCESO_E_ID > 0)
                {
                    //Verifica si es retardo
                    if (RPersona_Diario.ACCESO_E >= (HERetardo + ToleranciaExtra))
                    {
                        RPersona_Diario.PERSONA_DIARIO_TDE = CeC_BD.TimeSpan2DateTime(RPersona_Diario.ACCESO_E - HE);
                    }
                    //RPersona_Diario.PERSONA_DIARIO_TES = CeC_BD.TimeSpan2DateTime(HS - RPersona_Diario.ACCESO_E);
                }


                if (!RPersona_Diario.IsACCESO_ENull() && RPersona_Diario.ACCESO_E_ID > 0
                    && !RPersona_Diario.IsACCESO_SNull() && RPersona_Diario.ACCESO_S_ID > 0)
                {
                    RPersona_Diario.PERSONA_DIARIO_TT = CeC_BD.TimeSpan2DateTime(RPersona_Diario.ACCESO_S - RPersona_Diario.ACCESO_E);
                }
            }
            else
            { //Horario Flexible
                //Verifica que ya tenga entrada
                if (!RPersona_Diario.IsACCESO_ENull() && RPersona_Diario.ACCESO_E_ID > 0)
                {
                    //Obtiene los minutos por bloque y la tolerancia para cada uno de ellos
                    int MinutosBloque = Convert.ToInt32(CeC_BD.DateTime2TimeSpan(RTurnos_Dia.TURNO_DIA_HBLOQUE).TotalMinutes);
                    int MinutosTolera = Convert.ToInt32(CeC_BD.DateTime2TimeSpan(RTurnos_Dia.TURNO_DIA_HBLOQUET).TotalMinutes);
                    //Se obtiene cual sera el bloque de hora de entrada
                    for (DateTime Hora = HE; Hora <= HEMax; Hora = Hora.AddMinutes(MinutosBloque))
                    {
                        if (RPersona_Diario.ACCESO_E < (Hora.AddMinutes(MinutosTolera) + ToleranciaExtra))
                        {
                            Entrada = Hora;
                            break;
                        }
                    }
                    //Verifica si es retardo
                    if (RPersona_Diario.ACCESO_E >= (HERetardo + ToleranciaExtra))
                    {
                        //Asigna los minutos de deuda
                        RPersona_Diario.PERSONA_DIARIO_TDE = CeC_BD.TimeSpan2DateTime(RPersona_Diario.ACCESO_E - HERetardo);
                    }
                    //Calcula la hora de salida en el turno flexible
                    HS = Salida = Entrada + CeC_BD.DateTime2TimeSpan(RTurnos_Dia.TURNO_DIA_HTIEMPO);

                    //se calcula la hora minima, normal y maxima de salida de salida
                    RTurnos_Dia.TURNO_DIA_HS = CeC_BD.TimeSpan2DateTime(Salida - RPersona_Diario.PERSONA_DIARIO_FECHA);
                    RTurnos_Dia.TURNO_DIA_HSMIN = RTurnos_Dia.TURNO_DIA_HS.AddHours(-3);
                    RTurnos_Dia.TURNO_DIA_HSMAX = RTurnos_Dia.TURNO_DIA_HS.AddHours(6);
                    //Si existe la salida se calcula el tiempo en el trabajo
                    if (!RPersona_Diario.IsACCESO_SNull() && RPersona_Diario.ACCESO_S_ID > 0)
                    {
                        RPersona_Diario.PERSONA_DIARIO_TT = CeC_BD.TimeSpan2DateTime(RPersona_Diario.ACCESO_S - RPersona_Diario.ACCESO_E);
                    }
                    else
                    {
                        //#if eClockSync
                        //RPersona_Diario.PERSONA_DIARIO_TES = CeC_BD.TimeSpan2DateTime(Salida - RPersona_Diario.ACCESO_E);

                        if (EnviarMail)
                        {
                            //Envia un email con la hora de salida
                            string Mensage = "";

                            Mensage += "\nSu hora de entrada fue a las " + Entrada.ToString("HH:mm d/MM/yyyy");
                            Mensage += "\npor lo que su hora de salida será " + Salida.ToString("HH:mm d/MM/yyyy");
                            Mensage += "\n\n\n La hora de checada fue a las " + RPersona_Diario.ACCESO_E.ToString("HH:mm:ss");


                            EnviaMail(Convert.ToInt32(RPersona_Diario.PERSONA_ID), "Hora de Salida", Mensage);

                        }

                        //#endif
                    }

                }

            }
            //No hay entrada
            if (RPersona_Diario.ACCESO_E_ID == 0)
            {
                //No hay salida
                if (RPersona_Diario.ACCESO_S_ID == 0)
                    RPersona_Diario.TIPO_INC_SIS_ID = Convert.ToInt32(TIPO_INC_SIS.Falta_Entrada_y_Salida);
                else //Si es retardo en la salida
                    if (RPersona_Diario.ACCESO_S < HS)
                    {
                        RPersona_Diario.TIPO_INC_SIS_ID = Convert.ToInt32(TIPO_INC_SIS.Falta_Entrada_y_Salida_Temprano);
                        RPersona_Diario.PERSONA_DIARIO_TDE += HS - RPersona_Diario.ACCESO_S;
                    }
                    else // si solo falta la entrada
                        RPersona_Diario.TIPO_INC_SIS_ID = Convert.ToInt32(TIPO_INC_SIS.Falta_Entrada);
            }
            else
                if (RPersona_Diario.ACCESO_E >= HERetardo) //Es retardo
                {
                    //No hay salida
                    if (RPersona_Diario.ACCESO_S_ID == 0)
                    {
                        RPersona_Diario.TIPO_INC_SIS_ID = Convert.ToInt32(TIPO_INC_SIS.Retardo_y_Falta_Salida);
                        //Se asume que siempre se procesa primero el retardo y falta salida y se usará para el envio
                        // de emails
                        if (EnviarMail)
                            EnviaMailRetardo(Convert.ToInt32(RPersona_Diario.PERSONA_ID), RPersona_Diario.ACCESO_E, HE, Convert.ToInt32(RPersona_Diario.PERSONA_DIARIO_ID));
                    }
                    else //Si es retardo en la salida
                        if (RPersona_Diario.ACCESO_S < HS)
                        {
                            RPersona_Diario.TIPO_INC_SIS_ID = Convert.ToInt32(TIPO_INC_SIS.Retardo_y_Salida_Temprano);
                            RPersona_Diario.PERSONA_DIARIO_TDE += HS - RPersona_Diario.ACCESO_S;
                        }
                        else // si solo es retardo
                            RPersona_Diario.TIPO_INC_SIS_ID = Convert.ToInt32(TIPO_INC_SIS.Retardo);
                    //Calcula si hay retardos mayores y cual pertenece
                    RPersona_Diario.TIPO_INC_SIS_ID = CalculaRetardosMayores(RTurnos_Dia, RPersona_Diario.ACCESO_E, RPersona_Diario.PERSONA_DIARIO_FECHA, CeC.Convierte2Int(RPersona_Diario.TIPO_INC_SIS_ID));
                }
                else
                {
                    //No hay salida
                    if (RPersona_Diario.ACCESO_S_ID == 0)
                        RPersona_Diario.TIPO_INC_SIS_ID = Convert.ToInt32(TIPO_INC_SIS.Falta_Salida);
                    else //Si es retardo en la salida
                        if (RPersona_Diario.ACCESO_S < HS)
                        {
                            RPersona_Diario.TIPO_INC_SIS_ID = Convert.ToInt32(TIPO_INC_SIS.Salida_Temprano);
                            RPersona_Diario.PERSONA_DIARIO_TDE += HS - RPersona_Diario.ACCESO_S;
                        }
                        else // si es asistencia normal
                            RPersona_Diario.TIPO_INC_SIS_ID = Convert.ToInt32(TIPO_INC_SIS.Asistencia_Normal);

                }

            //Procesa las comidas
            RPersona_Diario.PERSONA_DIARIO_TC = CeC_BD.FechaNula;
            if (!RTurnos_Dia.IsTURNO_DIA_HAYCOMIDANull() && RTurnos_Dia.TURNO_DIA_HAYCOMIDA != 0)
            {
                if (RPersona_Diario.ACCESO_CR_ID == 0 && RPersona_Diario.ACCESO_CS_ID == 0)
                    RPersona_Diario.TIPO_INC_C_SIS_ID = Convert.ToDecimal(TipoIncComidas.No_Hay_Comida);
                else
                {
                    if (RPersona_Diario.ACCESO_CR_ID == 0 || RPersona_Diario.ACCESO_CS_ID == 0)
                        RPersona_Diario.TIPO_INC_C_SIS_ID = Convert.ToDecimal(TipoIncComidas.Registro_Incompleto);
                    else
                    {
                        TimeSpan TC = RPersona_Diario.ACCESO_CR - RPersona_Diario.ACCESO_CS;
                        RPersona_Diario.PERSONA_DIARIO_TC = CeC_BD.TimeSpan2DateTime(TC);

                        if (RTurnos_Dia.IsTURNO_DIA_HCTIEMPONull() || RTurnos_Dia.TURNO_DIA_HCTIEMPO == CeC_BD.FechaNula)
                            RTurnos_Dia.TURNO_DIA_HCTIEMPO = CeC_BD.TimeSpan2DateTime(RTurnos_Dia.TURNO_DIA_HCR - RTurnos_Dia.TURNO_DIA_HCS);
                        TimeSpan TCT = CeC_BD.DateTime2TimeSpan(RTurnos_Dia.TURNO_DIA_HCTIEMPO);
                        if (TCT.Add(CeC_BD.DateTime2TimeSpan(RTurnos_Dia.TURNO_DIA_HCTOLERA)) >= TC)
                            RPersona_Diario.TIPO_INC_C_SIS_ID = Convert.ToDecimal(TipoIncComidas.Correcta);
                        else
                            RPersona_Diario.TIPO_INC_C_SIS_ID = Convert.ToDecimal(TipoIncComidas.Exedio_el_Tiempo);
                    }
                }
            }
            else
            {
                RPersona_Diario.TIPO_INC_C_SIS_ID = 0;
            }
            if (RPersona_Diario.PERSONA_DIARIO_TT != CeC_BD.FechaNula && RPersona_Diario.PERSONA_DIARIO_TC != CeC_BD.FechaNula)
                RPersona_Diario.PERSONA_DIARIO_TE = CeC_BD.TimeSpan2DateTime(RPersona_Diario.PERSONA_DIARIO_TT - RPersona_Diario.PERSONA_DIARIO_TC);

            //Calcula las horas extras si se requieren
            if (!RTurnos_Dia.IsTURNO_DIA_PHEXNull() && RTurnos_Dia.TURNO_DIA_PHEX != 0 &&
                !RPersona_Diario.IsACCESO_ENull() && !RPersona_Diario.IsACCESO_SNull())
            {

                TimeSpan TAntes = CeC_BD.DateTime2TimeSpan(RTurnos_Dia.TURNO_DIA_HE) - (RPersona_Diario.ACCESO_E - RPersona_Diario.PERSONA_DIARIO_FECHA);
                TimeSpan TDespues = (RPersona_Diario.ACCESO_S - RPersona_Diario.PERSONA_DIARIO_FECHA) - CeC_BD.DateTime2TimeSpan(RTurnos_Dia.TURNO_DIA_HS);
                if (TAntes.TotalSeconds < 0)
                    TAntes = new TimeSpan();
                if (TDespues.TotalSeconds < 0)
                    TDespues = new TimeSpan();
                TimeSpan TotalSistema = TAntes + TDespues;

                int Horas = ObtenNoHorasExtras(TotalSistema, TDespues);
                TimeSpan TotalCalculado = new TimeSpan(Horas, 0, 0);
                if (TotalSistema.TotalMinutes > 0)

                    RPersona_Diario.PERSONA_D_HE_ID = CeC_AsistenciasHE.AsignaHorasExtras(Convert.ToInt32(
                    RPersona_Diario.PERSONA_ID),
                        RPersona_Diario.PERSONA_DIARIO_FECHA, TotalSistema,
                        TAntes, TDespues, TotalCalculado);
                else
                    CeC_AsistenciasHE.QuitaHoraExtra(Convert.ToInt32(RPersona_Diario.PERSONA_D_HE_ID), false);

            }
            else
                RPersona_Diario.PERSONA_D_HE_ID = CeC_AsistenciasHE.QuitaHoraExtra(Convert.ToInt32(RPersona_Diario.PERSONA_D_HE_ID), false);
            if (CeC_Config.FestivoTrabajado > 0)
            {
                if (CeC_DiasFestivos.EsDiaFestivo(RPersona_Diario.PERSONA_DIARIO_FECHA, CeC.Convierte2Int(RPersona_Diario.PERSONA_ID)) > 0)
                {
                    if (CeC_Config.FestivoTrabajado == 1)
                    {
                        int Inc = Convert.ToInt32(RPersona_Diario.TIPO_INC_SIS_ID);
                        if (Inc == Convert.ToInt32(TIPO_INC_SIS.Retardo_y_Salida_Temprano) ||
                            Inc == Convert.ToInt32(TIPO_INC_SIS.Retardo) ||
                            Inc == Convert.ToInt32(TIPO_INC_SIS.Asistencia_Normal) ||
                            Inc == Convert.ToInt32(TIPO_INC_SIS.Salida_Temprano))
                        {
                            RPersona_Diario.TIPO_INC_SIS_ID = Convert.ToInt32(TIPO_INC_SIS.Festivo_Trabajado);
                        }
                    }
                    else
                        RPersona_Diario.TIPO_INC_SIS_ID = Convert.ToInt32(TIPO_INC_SIS.Festivo_Trabajado);
                }
            }
            return 0;
        }
        catch (Exception exc)
        {
            string Mensage = exc.Message;
            CIsLog2.AgregaError(exc);
        }
        return -1;
    }
    /// <summary>
    /// Calcula los turnos por dia en base a un ID de persona.
    /// </summary>
    /// <param name="Persona_Diario_ID"></param>
    /// <returns></returns>
    public static int CalculaDia(int Persona_Diario_ID)
    {

        Console.WriteLine("CalculaDia  Persona_Diario_ID = {0:D}", Persona_Diario_ID);

        try
        {
            DS_CEC_Asistencias dsAsistencias = new DS_CEC_Asistencias();

            System.Data.OleDb.OleDbDataAdapter TAPersonas = NuevoEC_PERSONAS_DIARIOTableAdapter();
            System.Data.OleDb.OleDbDataAdapter TATurnos = NuevoEC_TURNOS_DIATableAdapter();

            /*              DS_CEC_AsistenciasTableAdapters.EC_TURNOS_DIATableAdapter TATurnos = new DS_CEC_AsistenciasTableAdapters.EC_TURNOS_DIATableAdapter();
                            DS_CEC_AsistenciasTableAdapters.EC_PERSONAS_DIARIOTableAdapter TAPersonas = new DS_CEC_AsistenciasTableAdapters.EC_PERSONAS_DIARIOTableAdapter();
            */
            TATurnos.SelectCommand.Parameters[0].Value = Persona_Diario_ID;
            TAPersonas.SelectCommand.Parameters[0].Value = Persona_Diario_ID;
            TATurnos.Fill(dsAsistencias.EC_TURNOS_DIA);

            TAPersonas.Fill(dsAsistencias.EC_PERSONAS_DIARIO);
            DS_CEC_Asistencias.EC_PERSONAS_DIARIORow RPersona_Diario = dsAsistencias.EC_PERSONAS_DIARIO[0];
            DS_CEC_Asistencias.EC_TURNOS_DIARow RTurnos_Dia = dsAsistencias.EC_TURNOS_DIA[0];

            CalculaDia(RPersona_Diario, RTurnos_Dia, false);
            TAPersonas.Update(dsAsistencias);

        }
        catch (Exception es)
        {
            string Mensage = es.Message;
            CIsLog2.AgregaError(es);
            return -2;
        }

        return 0;

    }
    /// <summary>
    /// Evia una cadena de e-mail que contendran los datos de los usuarios
    /// tanto comunes como supervisores que hallan tenido algun retardo.
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    public static bool EnviaMailsFaltas()
    {
        try
        {
            if (!CeC_Config.AlertaFaltasEmpleado && !CeC_Config.AlertaFaltasSupervisor)
                return false;
            DateTime DiaAyer = DateTime.Today.AddDays(-1);
            if (CeC_Config.AlertaFaltasUDia >= DiaAyer)
                return false;
        }
        catch
        {
            return false;
        }
        return true;

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="PersonasIDs"></param>
    /// <param name="FechaHora"></param>
    /// <returns></returns>
    public static int ProcesaFaltas(string PersonasIDs, DateTime FechaHora)
    {
        int Faltas = 0;
        string[] sPersonasIDs = CeC.ObtenArregoSeparador(PersonasIDs, ",");
        foreach (string sPersonaID in sPersonasIDs)
        {
            Faltas += CeC_Asistencias.ProcesaFaltas(CeC.Convierte2Int(sPersonaID), FechaHora);
        }
        return Faltas;
    }
    /// <summary>
    /// Funcion que se encarga de procesar todas las faltas que se efectuaron 
    /// de cierta fecha en adelante por Persona.
    /// </summary>
    /// <param name="Persona_ID"></param>
    /// <param name="FechaHora"></param>
    /// <returns></returns>
    public static int ProcesaFaltas(int Persona_ID, DateTime FechaHora)
    {
        GeneraNoAsistencia();
        int Registros = 0;
        if (FechaHora > DateTime.Now)
        {
            //CIsLog2.AgregaLog("Fecha de Faltas mayor a hoy")
            FechaHora = DateTime.Now;
        }
        Console.WriteLine("ProcesaFaltas  Persona_ID = {0:D}, FechaHora = {1:G}", Persona_ID, FechaHora.ToShortDateString());
        if (CeC_BD.EjecutaEscalarInt("SELECT COUNT(PERSONA_DIARIO_ID) FROM EC_PERSONAS_DIARIO " +
                  " WHERE PERSONA_ID = " + Persona_ID.ToString() +
                  " AND PERSONA_DIARIO_FECHA < " + CeC_BD.SqlFecha(FechaHora) +
            //                " AND TIPO_INC_SIS_ID = 0 AND TURNO_DIA_ID = 0 ") <= 0)
                    " AND TIPO_INC_SIS_ID = 0  ") <= 0)
            return 0;
        Console.WriteLine("ProcesaFaltas  Dia Festivo {0:G}", DateTime.Now.ToString());

        int TipoInc = Convert.ToInt32(TIPO_INC_SIS.Dia_Festivo);
        string Qry = "UPDATE EC_PERSONAS_DIARIO SET  TIPO_INC_SIS_ID = " + TipoInc.ToString() +
            " WHERE PERSONA_DIARIO_FECHA in(select DIA_FESTIVO_FECHA as PERSONA_DIARIO_FECHA from " +
            " EC_DIAS_FESTIVOS WHERE DIA_FESTIVO_BORRADO = 0 AND DIA_FESTIVO_FECHA < " + CeC_BD.SqlFecha(FechaHora) + ") AND TIPO_INC_SIS_ID = 0 AND PERSONA_ID = " + Persona_ID;/* +
            " AND TIPO_INC_SIS_ID <> " + TipoInc.ToString() ;*/
        Registros += CeC_BD.EjecutaComando(Qry);
        try
        {

            Console.WriteLine("ProcesaFaltas  TipoTurno {0:G}", DateTime.Now.ToString());
            int TipoTurno = 0;
            TipoTurno = CeC_BD.EjecutaEscalarInt("SELECT     EC_TURNOS.TIPO_TURNO_ID " +
" FROM         EC_TURNOS, EC_PERSONAS, EC_TURNOS_SEMANAL_DIA, EC_TURNOS_DIA " +
" WHERE     EC_TURNOS.TURNO_ID = EC_PERSONAS.TURNO_ID AND EC_TURNOS.TURNO_ID = EC_TURNOS_SEMANAL_DIA.TURNO_ID AND  " +
" EC_TURNOS_SEMANAL_DIA.TURNO_DIA_ID = EC_TURNOS_DIA.TURNO_DIA_ID AND (EC_PERSONAS.PERSONA_ID = " + Persona_ID.ToString() + ") " +
" GROUP BY EC_TURNOS.TIPO_TURNO_ID ");
            if (TipoTurno != 3 && TipoTurno != 0)
            {
                for (int Cont = 1; Cont <= 7; Cont++)
                {
                    int TurnoDiaID = -1;
                    TurnoDiaID = CeC_BD.EjecutaEscalarInt("SELECT    EC_TURNOS_DIA.TURNO_DIA_ID " +
" FROM         EC_TURNOS, EC_PERSONAS, EC_TURNOS_SEMANAL_DIA, EC_TURNOS_DIA " +
" WHERE     EC_TURNOS.TURNO_ID = EC_PERSONAS.TURNO_ID AND EC_TURNOS.TURNO_ID = EC_TURNOS_SEMANAL_DIA.TURNO_ID AND  " +
" EC_TURNOS_SEMANAL_DIA.TURNO_DIA_ID = EC_TURNOS_DIA.TURNO_DIA_ID AND (EC_PERSONAS.PERSONA_ID = " + Persona_ID.ToString() + ") " +
" AND DIA_SEMANA_ID =" + Cont);

                    int DiaSql = DiaID2DiaSql(Cont);
                    if (TurnoDiaID <= 0)
                        TurnoDiaID = -1;
                    string Comando = "UPDATE EC_PERSONAS_DIARIO SET TURNO_DIA_ID = " +
                        TurnoDiaID + ", PERSONA_DIARIO_TES = (SELECT TURNO_DIA_HTIEMPO " +
                        " FROM EC_TURNOS_DIA WHERE TURNO_DIA_ID = " + TurnoDiaID +
                        ") WHERE PERSONA_ID = " + Persona_ID.ToString() +
                        " AND PERSONA_DIARIO_FECHA < " + CeC_BD.SqlFecha(FechaHora) +
                        " AND TIPO_INC_SIS_ID = 0 AND TURNO_DIA_ID = 0 AND ";
                    if (CeC_BD.EsOracle)
                        Comando += "to_char(PERSONA_DIARIO_FECHA,'D')";
                    else
                        Comando += "DATEPART(weekday,PERSONA_DIARIO_FECHA)";

                    CeC_BD.EjecutaComando(Comando + " =" + DiaSql);

                    Console.WriteLine("ProcesaFaltas  Dia ({0:G}) {1:G} ", Cont, DateTime.Now.ToString());
                }
            }
            else
            {
                Console.WriteLine("ProcesaFaltas Turno Anual {0:G}", DateTime.Now.ToString());
                /*System.Data.DataSet Ds = (System.Data.DataSet)CeC_BD.EjecutaDataSet("SELECT PERSONA_DIARIO_ID, PERSONA_DIARIO_FECHA FROM EC_PERSONAS_DIARIO WHERE PERSONA_ID = " + Persona_ID.ToString() + " AND PERSONA_DIARIO_FECHA < " + CeC_BD.SqlFecha(FechaHora) + " AND TIPO_INC_SIS_ID = 0 AND TURNO_DIA_ID = 0");

                for (int Cont = 0; Cont < Ds.Tables[0].Rows.Count; Cont++)
                {
                    int PersonaDiarioID = Convert.ToInt32(Ds.Tables[0].Rows[Cont][0]);
                    DateTime DT = Convert.ToDateTime(Ds.Tables[0].Rows[Cont][1]);
                 * //Asignar Horario
                }
                Ds.Dispose();
                Console.WriteLine("ProcesaFaltas  Fin Turno Anual {0:G}", DateTime.Now.ToString());*/
                CIsLog2.AgregaError("Pendiente Crear codigo para procesar faltas en turno anual");
            }
            Console.WriteLine("ProcesaFaltas  Dia No laborable{0:G}", DateTime.Now.ToString());
            TipoInc = Convert.ToInt32(TIPO_INC_SIS.Dia_no_Laborable);
            Qry = "UPDATE EC_PERSONAS_DIARIO SET  TIPO_INC_SIS_ID = " + TipoInc.ToString() +
            " WHERE PERSONA_DIARIO_FECHA  < " + CeC_BD.SqlFecha(FechaHora) + " AND PERSONA_ID = " + Persona_ID + " AND TURNO_DIA_ID = -1 AND TIPO_INC_SIS_ID = 0 ";
            Registros += CeC_BD.EjecutaComando(Qry);
            TipoInc = Convert.ToInt32(TIPO_INC_SIS.Falta);
            Console.WriteLine("ProcesaFaltas  Dia FAlta {0:G}", DateTime.Now.ToString());
            Qry = "UPDATE EC_PERSONAS_DIARIO SET  TIPO_INC_SIS_ID = " + TipoInc.ToString() +
            " WHERE PERSONA_DIARIO_FECHA < " + CeC_BD.SqlFecha(FechaHora) + " AND PERSONA_ID = " + Persona_ID + " AND TURNO_DIA_ID > 0 AND TIPO_INC_SIS_ID = 0 ";
            Registros += CeC_BD.EjecutaComando(Qry);

        }
        catch
        {
        }
        Console.WriteLine("ProcesaFaltas   ({0:G}) {1:G} ", Registros, DateTime.Now.ToString());

        //Aqui es la validacion de las vacaciones 





        //*******




        return Registros;
    }

    /// <summary>
    /// Se encarga de recalcular los accesos nuevamente.
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    public static int RecalculaAccesos()
    {
        int Registros = 0;
        try
        {
            if (CeC_Config.Recalcula)
            {

                Console.WriteLine("RecalculaAccesos   {0:G} ", DateTime.Now.ToString());
                CeC_Config.Recalcula = false;
                DateTime FInicial = CeC_Config.RecalculaFInicial;
                DateTime FFinal = CeC_Config.RecalculaFFinal;
                if (FFinal.TimeOfDay.TotalSeconds < 1)
                    FFinal = FFinal.AddDays(1);
                else
                    FFinal = FFinal.AddMinutes(1);
                //Indica que los accesos se deberan calcular nuevamente
                CeC_BD.EjecutaComando("UPDATE EC_ACCESOS SET ACCESO_CALCULADO = 0 WHERE ACCESO_FECHAHORA >= " + CeC_BD.SqlFechaHora(FInicial) + " AND ACCESO_FECHAHORA < " + CeC_BD.SqlFechaHora(FFinal) + " AND TIPO_ACCESO_ID <> 6 ");
                LimpiaPersonaDiario(FInicial, FFinal);
                /*
                CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_DIARIO SET  ACCESO_E_ID = 0, ACCESO_S_ID = 0, ACCESO_CS_ID = 0, ACCESO_CR_ID = 0, TIPO_INC_SIS_ID = 0, TIPO_INC_C_SIS_ID = 0, PERSONA_DIARIO_TT = "
                    + CeC_BD.SqlFecha(CeC_BD.FechaNula) + ", PERSONA_DIARIO_TE = "
                    + CeC_BD.SqlFecha(CeC_BD.FechaNula) + ", PERSONA_DIARIO_TC = "
                    + CeC_BD.SqlFecha(CeC_BD.FechaNula) + ", PERSONA_DIARIO_TDE = "
                    + CeC_BD.SqlFecha(CeC_BD.FechaNula) + ", PERSONA_DIARIO_TES = "
                    + CeC_BD.SqlFecha(CeC_BD.FechaNula) + " WHERE PERSONA_DIARIO_FECHA >=" + CeC_BD.SqlFechaHora(FInicial) + " AND PERSONA_DIARIO_FECHA <" + CeC_BD.SqlFechaHora(FFinal) + " ");
                /*
                                System.Data.DataSet DS = (System.Data.DataSet)CeC_BD.EjecutaDataSet("SELECT ACCESO_ID, PERSONA_ID, ACCESO_FECHAHORA FROM EC_ACCESOS WHERE ACCESO_FECHAHORA >= " + CeC_BD.SqlFechaHora(FInicial) + " AND ACCESO_FECHAHORA < " + CeC_BD.SqlFechaHora(FFinal) + " AND TIPO_ACCESO_ID <> 6 AND TIPO_ACCESO_ID <> 0  ORDER BY ACCESO_FECHAHORA");
                                int Total = DS.Tables[0].Rows.Count;
                                if (Total > 0)
                                {
                                    CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_DIARIO SET ACCESO_E_ID = 0 , ACCESO_S_ID = 0 WHERE PERSONA_DIARIO_FECHA >=" + CeC_BD.SqlFechaHora(FInicial) + " AND PERSONA_DIARIO_FECHA <" + CeC_BD.SqlFechaHora(FFinal) + " ");
                                }
                                Console.WriteLine("RecalculaAccesos Por calcular   ({0:G}) {1:G} ", Total, DateTime.Now.ToString());
                                for (int Cont = 0; Cont < Total; Cont++)
                                {
                                    int IDAcceso = Convert.ToInt32(DS.Tables[0].Rows[Cont][0]);
                                    int Persona_ID = Convert.ToInt32(DS.Tables[0].Rows[Cont][1]);
                                    DateTime FechaHora = Convert.ToDateTime(DS.Tables[0].Rows[Cont][2]);
                                    Console.WriteLine("RecalculaAccesos  ProcesaAsistencia IDAcceso = {0:G}, Persona_ID= {1:G}, FechaHora= {2:G}", IDAcceso, Persona_ID, FechaHora.ToString());
                                    //Realiza el acomodo correspondiente del acceso en la asistencia que le corresponda
                                    ProcesaAsistencia(IDAcceso, Persona_ID, FechaHora);
                                }*/

            }
        }
        catch
        {
        }
        Console.WriteLine("RecalculaAccesos   ({0:G}) {1:G} ", Registros, DateTime.Now.ToString());
        return Registros;
    }
    /// <summary>
    /// Obtiene los horarios a diario de personas.
    /// </summary>
    /// <param name="PersonaDiarioID"></param>
    /// <returns></returns>
    public static DateTime ObtenPersona_Diario_TE(int PersonaDiarioID)
    {
        return CeC_BD.EjecutaEscalarDateTime("SELECT PERSONA_DIARIO_TE FROM EC_PERSONAS_DIARIO WHERE PERSONA_DIARIO_ID = " + PersonaDiarioID);
    }
    /// <summary>
    /// Asigna fechas de diario a personas por medio de ID.
    /// </summary>
    /// <param name="PersonaDiarioID"></param>
    /// <param name="Persona_Diario_TE"></param>
    /// <returns></returns>
    public static int AsignaPersona_Diario_TE(int PersonaDiarioID, DateTime Persona_Diario_TE)
    {
        return CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_DIARIO SET PERSONA_DIARIO_TE = " + CeC_BD.SqlFechaHora(Persona_Diario_TE) + " WHERE PERSONA_DIARIO_ID = " + PersonaDiarioID);
    }
    /// <summary>
    /// Agrega una persona mas al horario de diario.
    /// </summary>
    /// <param name="PersonaDiarioID"></param>
    /// <param name="PERSONA_ES_TE"></param>
    /// <returns></returns>
    public static int SumaPersona_Diario_TE(int PersonaDiarioID, DateTime PERSONA_ES_TE)
    {
        TimeSpan TSPersona_Diario_TE = CeC_BD.DateTime2TimeSpan(ObtenPersona_Diario_TE(PersonaDiarioID));
        TimeSpan TSPERSONA_ES_TE = CeC_BD.DateTime2TimeSpan(PERSONA_ES_TE);
        return AsignaPersona_Diario_TE(PersonaDiarioID, CeC_BD.TimeSpan2DateTime(TSPersona_Diario_TE + TSPERSONA_ES_TE));

    }

    /// <summary>
    /// Obtiene la fecha y hora de un acceso desde su acceso_ID
    /// </summary>
    /// <param name="Acceso_ID"></param>
    /// <returns></returns>
    public static DateTime ObtenAccesoFecha(int Acceso_ID)
    {
        return CeC_BD.EjecutaEscalarDateTime("SELECT ACCESO_FECHAHORA FROM EC_ACCESOS WHERE ACCESO_ID = " + Acceso_ID);
    }
    /// <summary>
    /// Obtiene la fecha de un acceso relativa a un dia
    /// </summary>
    /// <param name="Acceso_ID"></param>
    /// <param name="FechaDia"></param>
    /// <returns></returns>
    public static DateTime ObtenAccesoFechaRel(int Acceso_ID, DateTime FechaDia)
    {
        DateTime FechaHora = ObtenAccesoFecha(Acceso_ID);
        return FechaHora.AddDays(-((TimeSpan)(FechaHora - FechaDia)).Days);
    }
    /// <summary>
    /// Obtiene la fecha y la hora de acceso para agregarlo como un dia mas
    /// o fecha relativa.
    /// </summary>
    /// <param name="Acceso_ID"></param>
    /// <returns></returns>
    public static DateTime ObtenAccesoFechaRel(int Acceso_ID)
    {
        DateTime FechaHora = ObtenAccesoFecha(Acceso_ID);
        return FechaHora.AddDays(-((TimeSpan)(FechaHora - CeC_BD.FechaNula)).Days);
    }
    /// <summary>
    /// Obtiene la fecha relativa en la que se obtiene el acceso.
    /// </summary>
    /// <param name="FechaHora"></param>
    /// <returns></returns>
    public static DateTime ObtenFechaRelativa(DateTime FechaHora)
    {
        return FechaHora.AddDays(-((TimeSpan)(FechaHora - CeC_BD.FechaNula)).Days);
    }
    /// <summary>
    /// Clase que almacena los constructores de acceso para usuario la hora y fecha del mismo
    /// para ser procesadas y dar una fecha y hora de acceso.
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    class Acceso
    {
        public TipoAccesos TipoAcceso;
        public decimal ACCESO_ID;
        public DateTime ACCESO_FECHAHORA;
        public decimal TIPO_ACCESO_ID;
        public Acceso(DS_CEC_Asistencias.EC_ACCESOSRow Fila)
        {
            TIPO_ACCESO_ID = Fila.TIPO_ACCESO_ID;
            TipoAcceso = (TipoAccesos)Fila.TIPO_ACCESO_ID;
            ACCESO_ID = Fila.ACCESO_ID;
            ACCESO_FECHAHORA = Fila.ACCESO_FECHAHORA;
        }
    }
    /// <summary>
    /// Procesa las asistencias de entrada y salida, además actualiza el tiempo de Estancia de la Asistencia del Empleado
    /// El sistema verificara que la suscripcion tenga habilitado el calculo de Asitencia de Entrada/Salida
    /// </summary>
    /// <param name="Acceso_ID"></param>
    /// <param name="Persona_ID"></param>
    /// <param name="FechaHora"></param>
    /// <returns></returns>
    public static int ProcesaAsistenciaES(int Acceso_ID, int Persona_ID, DateTime FechaHora, bool NoValidar)
    {
        try
        {
            return -1;
            int PERSONA_ES_ID = 0;
            if (!NoValidar)
            {
                //Verifica si se encuentra procesado el acceso, se uso esta solucion para no recalcular asistencia en caso de proceso de entradas y salidas
                PERSONA_ES_ID = CeC_BD.EjecutaEscalarInt("SELECT PERSONA_ES_ID FROM EC_PERSONAS_ES WHERE ACCESO_E_ES_ID = " + Acceso_ID + " OR ACCESO_S_ES_ID = " + Acceso_ID);
                if (PERSONA_ES_ID > 0)
                    return PERSONA_ES_ID;

                //se debe encontrar una mejor alternativa para para calcular las checadas de entrada y salida
                //cuando arriben las entradas posteriormente a la hora de salidas
                DateTime FechaMaxima = CeC_BD.EjecutaEscalarDateTime("SELECT     MAX(ACCESO_FECHAHORA) AS Expr1 " +
                        " FROM        EC_ACCESOS " +
                        " WHERE     (ACCESO_ID IN " +
                        " (SELECT     ACCESO_E_ES_ID AS ACCESO_ID " +
                        " FROM          EC_PERSONAS_ES " +
                        " WHERE      (ACCESO_E_ES_ID > 0) AND (EC_ACCESOS.PERSONA_ID = " + Persona_ID + "))) OR (ACCESO_ID IN " +
                        " (SELECT     ACCESO_S_ES_ID AS ACCESO_ID " +
                        " FROM          EC_PERSONAS_ES " +
                        " WHERE      (ACCESO_S_ES_ID > 0) AND (EC_ACCESOS.PERSONA_ID = " + Persona_ID + ")))");
                if (FechaMaxima > FechaHora)
                {
                    CIsLog2.AgregaLog("Se cargo una fecha anterior " + FechaHora.ToString() + " la maxima anterior fue " + FechaMaxima.ToString());
                    //bORRA EL LAS ENTRADAS Y SALIDAS DE UN PERSONA DIARIO
                    CeC_BD.EjecutaComando("DELETE EC_PERSONAS_ES WHERE ACCESO_E_ES_ID in (SELECT ACCESO_ID FROM EC_ACCESOS WHERE PERSONA_ID = " +
                        Persona_ID + " AND ACCESO_FECHAHORA >= " + CeC_BD.SqlFechaHora(FechaHora) + ")");

                    CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_ES SET ACCESO_S_ES_ID = 0,PERSONA_ES_TE = " + CeC_BD.SqlFechaNula() + ", TIPO_INC_SIS_ID = 0, INCIDENCIA_ID = 0 " +
                    " WHERE ACCESO_S_ES_ID in (SELECT ACCESO_ID FROM EC_ACCESOS WHERE PERSONA_ID = " +
                        Persona_ID + " AND ACCESO_FECHAHORA >= " + CeC_BD.SqlFechaHora(FechaHora) + ")");

                    string Query_Recalculo = "SELECT ACCESO_ID, ACCESO_FECHAHORA FROM EC_ACCESOS WHERE ACCESO_FECHAHORA >= "
                    + CeC_BD.SqlFechaHora(FechaHora) + " AND TIPO_ACCESO_ID <> 6 AND TIPO_ACCESO_ID <> 0 AND PERSONA_ID = "
                    + Persona_ID + " AND TERMINAL_ID IN (SELECT TERMINAL_ID FROM EC_TERMINALES WHERE TERMINAL_ASISTENCIA = 1) "
                    + " \n ORDER BY ACCESO_FECHAHORA";

                    DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Query_Recalculo);
                    foreach (DataRow DR in DS.Tables[0].Rows)
                    {
                        ProcesaAsistenciaES(CeC.Convierte2Int(DR["ACCESO_ID"]), Persona_ID, CeC.Convierte2DateTime(DR["ACCESO_FECHAHORA"]), true);
                    }
                    //RecalculaAsistencia(Persona_ID, FechaHora.Date, FechaMaxima.Date);
                    return -10;
                }
            }
            DS_CEC_Asistencias dsAsistencias = new DS_CEC_Asistencias();
            DS_CEC_AsistenciasTableAdapters.EC_ACCESOSTableAdapter TAAcceso = new DS_CEC_AsistenciasTableAdapters.EC_ACCESOSTableAdapter();
            TAAcceso.Fill(dsAsistencias.EC_ACCESOS, Acceso_ID);
            DS_CEC_Asistencias.EC_ACCESOSRow RAcceso = dsAsistencias.EC_ACCESOS[0];
            TipoAccesos TipoAcceso = ((TipoAccesos)RAcceso.TIPO_ACCESO_ID);
            if (TipoAcceso != TipoAccesos.Correcto && TipoAcceso != TipoAccesos.Entrada && TipoAcceso != TipoAccesos.Salida)
                return 0;

            //Obtiene el persona Diario, pendiente obtener el real, ya que este solo sirve para horarios que NO terminan al dia siguiente y no funciona dias no laborables
            int Persona_Diario_ID = CeC_BD.EjecutaEscalarInt("SELECT PERSONA_DIARIO_ID FROM EC_PERSONAS_DIARIO WHERE PERSONA_ID = " + Persona_ID + " AND PERSONA_DIARIO_FECHA = " + CeC_BD.SqlFecha(RAcceso.ACCESO_FECHAHORA));
            if (Persona_Diario_ID <= 0)
                return -2;
            DS_CEC_AsistenciasTableAdapters.EC_PERSONAS_ESTableAdapter TAPersonasES = new DS_CEC_AsistenciasTableAdapters.EC_PERSONAS_ESTableAdapter();
            bool EsNuevo = true;
            bool RegistroSuma = false;
            TAPersonasES.FillMaxByPersonaDiarioID(dsAsistencias.EC_PERSONAS_ES, Persona_Diario_ID);
            DS_CEC_Asistencias.EC_PERSONAS_ESRow PersonaES = null;
            if (dsAsistencias.EC_PERSONAS_ES.Rows.Count > 0)
            {
                PersonaES = dsAsistencias.EC_PERSONAS_ES[0];
                if (PersonaES.ACCESO_S_ES_ID == 0 && TipoAcceso == TipoAccesos.Salida)
                    EsNuevo = false;
                if (PersonaES.ACCESO_S_ES_ID == 0 && TipoAcceso == TipoAccesos.Correcto)
                {
                    //Verifica si es diferente entrada
                    if (TipoAcceso != TipoAccesos.Entrada)
                    {
                        //Simula que el acceso es tipo Salida aunque así lo sea
                        TipoAcceso = TipoAccesos.Salida;
                        EsNuevo = false;
                    }
                }
                if (EsNuevo)
                    dsAsistencias.EC_PERSONAS_ES.Rows.Clear();
            }
            if (EsNuevo)
            {
                PersonaES = dsAsistencias.EC_PERSONAS_ES.NewEC_PERSONAS_ESRow();
                PersonaES.PERSONA_ES_ID = PERSONA_ES_ID = CeC_Autonumerico.GeneraAutonumerico("EC_PERSONAS_ES", "PERSONA_ES_ID");
                PersonaES.PERSONA_DIARIO_ID = Persona_Diario_ID;
                PersonaES.ACCESO_E_ES_ID = 0;
                PersonaES.ACCESO_S_ES_ID = 0;
                PersonaES.INCIDENCIA_ID = 0;
                PersonaES.PERSONA_ES_ORD = CeC_BD.FechaNula;
                PersonaES.PERSONA_ES_TE = CeC_BD.FechaNula;
                PersonaES.TIPO_INC_SIS_ID = 0;
                if (TipoAcceso == TipoAccesos.Correcto)
                    TipoAcceso = TipoAccesos.Entrada;
            }


            if (TipoAcceso == TipoAccesos.Entrada)
            {
                PersonaES.ACCESO_E_ES_ID = Acceso_ID;
                PersonaES.PERSONA_ES_ORD = FechaHora;
            }
            else
            {
                PersonaES.ACCESO_S_ES_ID = Acceso_ID;
                if (PersonaES.ACCESO_E_ES_ID == 0)
                    PersonaES.PERSONA_ES_ORD = FechaHora;
                else
                {
                    RegistroSuma = true;
                    PersonaES.PERSONA_ES_TE = CeC_BD.TimeSpan2DateTime(FechaHora - ObtenAccesoFecha(Convert.ToInt32(PersonaES.ACCESO_E_ES_ID)));
                }
            }
            /*if(ACCESO_E_ES_ID != 0)
                PersonaES.PERSONA_ES_ORD = */

            if (dsAsistencias.EC_PERSONAS_ES.Rows.Count == 0)
                dsAsistencias.EC_PERSONAS_ES.AddEC_PERSONAS_ESRow(PersonaES);
            try
            {
                TAPersonasES.Update(dsAsistencias.EC_PERSONAS_ES);
                AsignaPersona_Diario_TE(Persona_Diario_ID, CeC_BD.TimeSpan2DateTime(ObtenTotalTiempoES("PERSONA_ES_TE", Persona_Diario_ID)));
            }
            catch (Exception es)
            {
                CIsLog2.AgregaError(es);
            }
            return PERSONA_ES_ID;
        }
        catch (Exception es)
        {
            CIsLog2.AgregaError(es);
        }
        return -1;
    }
    /// <summary>
    /// Procesa días no laborables usa la primer checada del dia para entrada y la ultima para salida
    /// </summary>
    /// <param name="Acceso_ID"></param>
    /// <param name="Persona_ID"></param>
    /// <param name="FechaHora"></param>
    /// <returns></returns>
    public static int ProcesaAsistenciaDNL(int Acceso_ID, int Persona_ID, DateTime FechaHora)
    {
        DS_CEC_Asistencias dsAsistencias = new DS_CEC_Asistencias();
        DS_CEC_AsistenciasTableAdapters.EC_ACCESOSTableAdapter TAAcceso = new DS_CEC_AsistenciasTableAdapters.EC_ACCESOSTableAdapter();
        TAAcceso.Fill(dsAsistencias.EC_ACCESOS, Acceso_ID);
        DS_CEC_Asistencias.EC_ACCESOSRow RAcceso = dsAsistencias.EC_ACCESOS[0];
        int Persona_Diario_ID = CeC_BD.EjecutaEscalarInt("SELECT PERSONA_DIARIO_ID FROM EC_PERSONAS_DIARIO WHERE PERSONA_ID = " + Persona_ID + " AND PERSONA_DIARIO_FECHA = " + CeC_BD.SqlFecha(RAcceso.ACCESO_FECHAHORA));
        DS_CEC_AsistenciasTableAdapters.EC_PERSONAS_DIARIOTableAdapter TAPersonas = new DS_CEC_AsistenciasTableAdapters.EC_PERSONAS_DIARIOTableAdapter();

        TAPersonas.Fill(dsAsistencias.EC_PERSONAS_DIARIO, Persona_Diario_ID);
        DS_CEC_Asistencias.EC_PERSONAS_DIARIORow RPersona_Diario = dsAsistencias.EC_PERSONAS_DIARIO[0];
        if (RPersona_Diario.TURNO_DIA_ID != -1)
            return -1;
        if (RPersona_Diario.ACCESO_E_ID <= 0)
        {
            RPersona_Diario.ACCESO_E_ID = Acceso_ID;
            RPersona_Diario.ACCESO_E = RAcceso.ACCESO_FECHAHORA;
        }
        else
        {
            if (RPersona_Diario.ACCESO_E > RAcceso.ACCESO_FECHAHORA)
            {
                decimal ID = RPersona_Diario.ACCESO_E_ID;
                DateTime FH = RPersona_Diario.ACCESO_E;

                RPersona_Diario.ACCESO_E_ID = Acceso_ID;
                RPersona_Diario.ACCESO_E = RAcceso.ACCESO_FECHAHORA;
                Acceso_ID = Convert.ToInt32(ID);
                RAcceso.ACCESO_FECHAHORA = FH;
            }
            if (RPersona_Diario.ACCESO_S_ID <= 0 || RPersona_Diario.ACCESO_S < RAcceso.ACCESO_FECHAHORA)
            {
                RPersona_Diario.ACCESO_S_ID = Acceso_ID;
                RPersona_Diario.ACCESO_S = RAcceso.ACCESO_FECHAHORA;
            }

        }
        if (RPersona_Diario.ACCESO_E_ID != 0 && RPersona_Diario.ACCESO_S_ID != 0)
        {
            TimeSpan TS = RPersona_Diario.ACCESO_S - RPersona_Diario.ACCESO_E;
            RPersona_Diario.PERSONA_DIARIO_TT = CeC_BD.TimeSpan2DateTime(TS);
            RPersona_Diario.PERSONA_DIARIO_TE = RPersona_Diario.PERSONA_DIARIO_TT;
            if (!CeC_Config.EsTIP)
            {
                ///Si no es Tip                

                TimeSpan TotalCalculado = new TimeSpan(ObtenNoHorasExtras(TS), 0, 0);
                RPersona_Diario.PERSONA_D_HE_ID = CeC_AsistenciasHE.AsignaHorasExtras(Persona_ID,
                    RPersona_Diario.PERSONA_DIARIO_FECHA, TS,
                    new TimeSpan(0, 0, 0), TS, TotalCalculado);
            }
            else
            {
                ///Es Tip
                RPersona_Diario.TIPO_INC_SIS_ID = CeC.Convierte2Int(TIPO_INC_SIS.Descanso_Trabajado);
                DateTime HoraSalida = CeC_Turnos_Dia.ObtenHS(Persona_ID);
                DateTime FechaRelativa = CeC_BD.FechaNula + (FechaHora - RPersona_Diario.PERSONA_DIARIO_FECHA);
                TimeSpan horasExtraReales = FechaRelativa - HoraSalida;
                TimeSpan horasExtraCalculadas = FechaRelativa - HoraSalida;

                int Horas;
                double HorasDecimal = 0;
                if (CeC.Convierte2Int(horasExtraCalculadas.TotalMinutes) >= CeC.Convierte2Int(CeC_Config.MinutosParaHoraExtra))
                {
                    HorasDecimal = CeC.Convierte2Int(horasExtraCalculadas.TotalMinutes) / CeC.Convierte2Int(CeC_Config.MinutosParaHoraExtra);
                    Horas = CeC.Convierte2Int(Math.Truncate(HorasDecimal));
                    //Horas = CeC.Convierte2Int(TotalSistema.TotalHours);
                }
                else
                    Horas = 0;

                horasExtraCalculadas = TimeSpan.FromHours(Horas);

                //TimeSpan TotalCalculado = new TimeSpan(Convert.ToInt32(TS.TotalHours), 0, 0);
                RPersona_Diario.PERSONA_D_HE_ID = CeC_AsistenciasHE.AsignaHorasExtras(Persona_ID,
                    RPersona_Diario.PERSONA_DIARIO_FECHA, horasExtraReales,
                    new TimeSpan(0, 0, 0), horasExtraReales, horasExtraCalculadas);
            }
        }
        else
        {
            RPersona_Diario.PERSONA_DIARIO_TE = RPersona_Diario.PERSONA_DIARIO_TT = CeC_BD.FechaNula;
            RPersona_Diario.PERSONA_D_HE_ID = 0;
        }



        TAPersonas.Update(dsAsistencias);
        return 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Acceso_ID"></param>
    /// <param name="Persona_ID"></param>
    /// <param name="FechaHora"></param>
    /// <returns></returns>
    public static int ProcesaAsistencia(int Acceso_ID, int Persona_ID, DateTime FechaHora)
    {
        int R = ProcesaSoloAsistencia(Acceso_ID, Persona_ID, FechaHora);
        ProcesaAsistenciaES(Acceso_ID, Persona_ID, FechaHora, false);
        return R;
    }
    /// <summary>
    /// Procesa el horario de cada persona evluando si dicha persona 
    /// tubo accesos de acuerdo a sus horarios establecidos.
    /// </summary>
    /// <param name="Acceso_ID">Id de acceso por usuario</param>
    /// <param name="Persona_ID">Id de la persona a procesar</param>
    /// <param name="FechaHora"></param>
    /// <returns></returns>
    public static int ProcesaSoloAsistencia(int Acceso_ID, int Persona_ID, DateTime FechaHora)
    {
        GeneraPrevioPersonaDiario(Persona_ID, FechaHora);
        CeC_Turnos.AsignaHorario(Persona_ID, FechaHora);
        //por el momento no procesara faltas
        //ProcesaFaltas(Persona_ID, FechaHora);
        int Persona_Diario_ID = EstaDetronHorario(Persona_ID, FechaHora);
        if (Persona_Diario_ID < 0)
        {
            if (!CeC_Config.AsistenciaNoLaborable)
                return -1;
            return ProcesaAsistenciaDNL(Acceso_ID, Persona_ID, FechaHora);
        }
        Console.WriteLine("ProcesaAsistencia  Persona_ID = {0:D} FechaHora = {1:G}", Persona_ID, FechaHora.ToString());
        int DiaFestivo = CeC_DiasFestivos.EsDiaFestivo(FechaHora, Persona_ID);
        if (DiaFestivo > 0)
        {
            CeC_BD.EjecutaComando("update EC_PERSONAS_diario set TIPO_INC_SIS_ID = " + Convert.ToInt32(TIPO_INC_SIS.Dia_Festivo) + " where persona_diario_id =" + Persona_Diario_ID);
            if (CeC_Config.FestivoTrabajado <= 0)
                return 1;
            /*            if (!CeC_Config.AsistenciaNoLaborable)
                            return 1;
                        else
                            return ProcesaAsistenciaDNL(Acceso_ID, Persona_ID, FechaHora);*/
        }
        try
        {
            //#if eClockSync
            DS_CEC_Asistencias dsAsistencias = new DS_CEC_Asistencias();

            //Valida si se generará asistencia
            DS_CEC_AsistenciasTableAdapters.EC_TURNOS_DIATableAdapter TATurnos = new DS_CEC_AsistenciasTableAdapters.EC_TURNOS_DIATableAdapter();
            TATurnos.Fill(dsAsistencias.EC_TURNOS_DIA, Persona_Diario_ID);
            DS_CEC_Asistencias.EC_TURNOS_DIARow RTurnos_Dia = dsAsistencias.EC_TURNOS_DIA[0];
            if (!RTurnos_Dia.IsTURNO_DIA_NO_ASISNull() && RTurnos_Dia.TURNO_DIA_NO_ASIS != 0)
                return 2;

            DS_CEC_AsistenciasTableAdapters.EC_ACCESOSTableAdapter TAAcceso = new DS_CEC_AsistenciasTableAdapters.EC_ACCESOSTableAdapter();
            TAAcceso.Fill(dsAsistencias.EC_ACCESOS, Acceso_ID);

            DS_CEC_AsistenciasTableAdapters.EC_PERSONAS_DIARIOTableAdapter TAPersonas = new DS_CEC_AsistenciasTableAdapters.EC_PERSONAS_DIARIOTableAdapter();

            TAPersonas.Fill(dsAsistencias.EC_PERSONAS_DIARIO, Persona_Diario_ID);

            DS_CEC_Asistencias.EC_PERSONAS_DIARIORow RPersona_Diario = dsAsistencias.EC_PERSONAS_DIARIO[0];
            DS_CEC_Asistencias.EC_ACCESOSRow RAcceso = dsAsistencias.EC_ACCESOS[0];

            DateTime FechaRelativa = CeC_BD.FechaNula + (FechaHora - RPersona_Diario.PERSONA_DIARIO_FECHA);// FechaHora.AddDays(-((TimeSpan)(FechaHora - CeC_BD.FechaNula)).Days);
            bool EsEntrada = false;
            bool Usado = false;

            //////////////////////////////////////////////////////////////////////////
            //SE agrega una validación si la nueva checada es menor o igual a 15 minutos a cualquiera
            //de las checadas guardadas, ej. Hora de Entrada, Salida, Salida a comer o regreso,
            //Será ignorada
            int MinutosLibres = CeC_Config.AsistenciaMinutosLibres;
            if (!RPersona_Diario.IsACCESO_ENull() && ((TimeSpan)(RPersona_Diario.ACCESO_E - FechaHora)).Duration().TotalMinutes <= MinutosLibres)
                return 3;
            if (!RPersona_Diario.IsACCESO_SNull() && ((TimeSpan)(RPersona_Diario.ACCESO_S - FechaHora)).Duration().TotalMinutes <= MinutosLibres)
                return 4;
            if (!RPersona_Diario.IsACCESO_CSNull() && ((TimeSpan)(RPersona_Diario.ACCESO_CS - FechaHora)).Duration().TotalMinutes <= MinutosLibres)
                return 5;
            if (!RPersona_Diario.IsACCESO_CRNull() && ((TimeSpan)(RPersona_Diario.ACCESO_CR - FechaHora)).Duration().TotalMinutes <= MinutosLibres)
                return 6;

            //////////////////////////////////////////////////////////////////////////
            ///Calcula la hora de entrada, si es la primera checada del dia se toma como hora de entrada
            if (FechaRelativa >= RTurnos_Dia.TURNO_DIA_HEMIN && FechaRelativa < RTurnos_Dia.TURNO_DIA_HEMAX)
            {
                if (((TipoAccesos)RAcceso.TIPO_ACCESO_ID) == TipoAccesos.Correcto ||
                    ((TipoAccesos)RAcceso.TIPO_ACCESO_ID) == TipoAccesos.Entrada ||
                    ((TipoAccesos)RAcceso.TIPO_ACCESO_ID) == TipoAccesos.No_definido)
                {
                    if (RPersona_Diario.ACCESO_E_ID <= 0 || FechaHora < RPersona_Diario.ACCESO_E
                        )
                    {
                        if (RPersona_Diario.ACCESO_E_ID > 0)
                            CeC_BD.EjecutaComando("UPDATE EC_ACCESOS SET ACCESO_CALCULADO = 0 WHERE ACCESO_ID = " + RPersona_Diario.ACCESO_E_ID);
                        RPersona_Diario.ACCESO_E_ID = RAcceso.ACCESO_ID;
                        RPersona_Diario.ACCESO_E = RAcceso.ACCESO_FECHAHORA;
                        EsEntrada = true;
                        Usado = true;
                    }
                }

            }

            //////////////////////////////////////////////////////////////////////////
            ///Calcula la hora de salida, si es la ultima checada del dia se toma como hora de salida
            if (FechaRelativa >= RTurnos_Dia.TURNO_DIA_HSMIN && FechaRelativa < RTurnos_Dia.TURNO_DIA_HSMAX && !Usado)
            {
                if (((TipoAccesos)RAcceso.TIPO_ACCESO_ID) == TipoAccesos.Correcto ||
                    ((TipoAccesos)RAcceso.TIPO_ACCESO_ID) == TipoAccesos.Salida ||
                    ((TipoAccesos)RAcceso.TIPO_ACCESO_ID) == TipoAccesos.No_definido)
                {
                    if (RPersona_Diario.ACCESO_S_ID <= 0 || RPersona_Diario.ACCESO_S < RAcceso.ACCESO_FECHAHORA)
                    {
                        bool GuardarSalida = true;
                        Acceso Nuevo = new Acceso(RAcceso);
                        if (RPersona_Diario.ACCESO_S_ID > 0)
                        {
                            if (((TipoAccesos)RAcceso.TIPO_ACCESO_ID) == TipoAccesos.Salida)
                                RAcceso.TIPO_ACCESO_ID = Convert.ToInt32(TipoAccesos.Salida_a_Comer);

                            if (RPersona_Diario.ACCESO_S < RAcceso.ACCESO_FECHAHORA)
                            {
                                RAcceso.ACCESO_ID = RPersona_Diario.ACCESO_S_ID;
                                RAcceso.ACCESO_FECHAHORA = RPersona_Diario.ACCESO_S;
                                FechaRelativa = ObtenFechaRelativa(RPersona_Diario.ACCESO_S);

                            }
                            else
                            {
                                GuardarSalida = false;
                            }

                        }
                        else
                            Usado = true;
                        if (GuardarSalida)
                        {
                            RPersona_Diario.ACCESO_S_ID = Nuevo.ACCESO_ID;
                            RPersona_Diario.ACCESO_S = Nuevo.ACCESO_FECHAHORA;
                            RPersona_Diario.TIPO_ACCESO_S_ID = Nuevo.TIPO_ACCESO_ID;

                        }


                    }
                }
            }
            //Verifica si se debe calcular las horas de comida
            if (!Usado && !RTurnos_Dia.IsTURNO_DIA_HAYCOMIDANull() && RTurnos_Dia.TURNO_DIA_HAYCOMIDA != 0
                && (((TipoAccesos)RAcceso.TIPO_ACCESO_ID) == TipoAccesos.Correcto ||
                    ((TipoAccesos)RAcceso.TIPO_ACCESO_ID) == TipoAccesos.Salida_a_Comer ||
                    ((TipoAccesos)RAcceso.TIPO_ACCESO_ID) == TipoAccesos.Regreso_de_comer ||
                    ((TipoAccesos)RAcceso.TIPO_ACCESO_ID) == TipoAccesos.Salida ||
                    ((TipoAccesos)RAcceso.TIPO_ACCESO_ID) == TipoAccesos.Entrada ||
                    ((TipoAccesos)RAcceso.TIPO_ACCESO_ID) == TipoAccesos.No_definido)
                )
            {

                bool UsarRegreso = false;
                bool UsarSalida = false;
                if (((TipoAccesos)RAcceso.TIPO_ACCESO_ID) == TipoAccesos.Salida_a_Comer || ((TipoAccesos)RAcceso.TIPO_ACCESO_ID) == TipoAccesos.Salida)
                    UsarSalida = true;
                if (((TipoAccesos)RAcceso.TIPO_ACCESO_ID) == TipoAccesos.Regreso_de_comer || ((TipoAccesos)RAcceso.TIPO_ACCESO_ID) == TipoAccesos.Entrada)
                    UsarRegreso = true;
                bool ProcesarComida = false;
                int TipoComida = 1;
                TimeSpan TS_HCTolera = CeC_BD.DateTime2TimeSpan(RTurnos_Dia.TURNO_DIA_HCTOLERA);
                TimeSpan TS_HCTiempo = !RTurnos_Dia.IsTURNO_DIA_HCTIEMPONull() ? CeC_BD.DateTime2TimeSpan(RTurnos_Dia.TURNO_DIA_HCTIEMPO) : CeC_BD.DateTime2TimeSpan(CeC_BD.FechaNula);
                //Verifica si la comida solo se calculara en un horario
                if (!RTurnos_Dia.IsTURNO_DIA_HCRNull() && !RTurnos_Dia.IsTURNO_DIA_HCSNull() &&
                    RTurnos_Dia.TURNO_DIA_HCS != CeC_BD.FechaNula && RTurnos_Dia.TURNO_DIA_HCR != CeC_BD.FechaNula)
                {
                    DateTime HoraMinima = RTurnos_Dia.TURNO_DIA_HCS - TS_HCTolera;
                    DateTime HoraMaxima = RTurnos_Dia.TURNO_DIA_HCR + TS_HCTolera;
                    //Indica que es por horario
                    if (FechaRelativa >= HoraMinima && FechaRelativa < HoraMaxima)
                        ProcesarComida = true;
                    TipoComida = 2;
                    if (TS_HCTiempo.TotalSeconds > 0)
                        TipoComida = 3;
                }
                else
                    //Verifica si la hora de comida es por tiempo 
                    if (TS_HCTiempo.TotalSeconds > 0)
                        ProcesarComida = true;

                if (ProcesarComida)
                {
                    try
                    {
                        if (!UsarRegreso && (RPersona_Diario.ACCESO_CS_ID <= 0 || UsarSalida))
                        {
                            RPersona_Diario.ACCESO_CS_ID = RAcceso.ACCESO_ID;
                            RPersona_Diario.ACCESO_CS = RAcceso.ACCESO_FECHAHORA;
                            RPersona_Diario.TIPO_ACCESO_CS_ID = RAcceso.TIPO_ACCESO_ID;
                            //Si la hora de regreso de comer es menor a la hora de salida a comer, quita el regreso de comer    
                            if (RPersona_Diario.ACCESO_CR_ID > 0 && RPersona_Diario.ACCESO_CR < RPersona_Diario.ACCESO_CS)
                                RPersona_Diario.ACCESO_CR_ID = 0;
                        }
                        else
                        {
                            bool UsarChecada = true;
                            if (((TipoAccesos)RPersona_Diario.TIPO_ACCESO_CR_ID) == TipoAccesos.Regreso_de_comer
                                && ((TipoAccesos)RAcceso.TIPO_ACCESO_ID) != TipoAccesos.Regreso_de_comer)
                            {
                                UsarChecada = false;
                            }
                            if (TipoComida == 3 && RPersona_Diario.ACCESO_CR_ID > 0)
                                UsarChecada = false;
                            if (UsarChecada)
                            {
                                Acceso Nuevo = new Acceso(RAcceso);

                                if (!RPersona_Diario.IsACCESO_CSNull() && RAcceso.ACCESO_FECHAHORA < RPersona_Diario.ACCESO_CS && !UsarRegreso)
                                {
                                    Nuevo.ACCESO_ID = RPersona_Diario.ACCESO_CS_ID;
                                    Nuevo.ACCESO_FECHAHORA = RPersona_Diario.ACCESO_CS;
                                    Nuevo.TIPO_ACCESO_ID = RPersona_Diario.TIPO_ACCESO_CS_ID;
                                    RPersona_Diario.ACCESO_CS_ID = RAcceso.ACCESO_ID;
                                    RPersona_Diario.ACCESO_CS = RAcceso.ACCESO_FECHAHORA;
                                    RPersona_Diario.TIPO_ACCESO_CS_ID = RAcceso.TIPO_ACCESO_ID;
                                }
                                RPersona_Diario.ACCESO_CR_ID = Nuevo.ACCESO_ID;
                                RPersona_Diario.ACCESO_CR = Nuevo.ACCESO_FECHAHORA;
                                RPersona_Diario.TIPO_ACCESO_CR_ID = Nuevo.TIPO_ACCESO_ID;

                                if (TipoComida == 3)
                                    //Si la hora de el tiempo de comida es menor o igual al tiempo de tolerancia    
                                    if (RPersona_Diario.ACCESO_CS_ID > 0 && ((TimeSpan)(RPersona_Diario.ACCESO_CR - RPersona_Diario.ACCESO_CS)) <= TS_HCTolera)
                                    {
                                        RPersona_Diario.ACCESO_CS_ID = 0;
                                        RPersona_Diario.ACCESO_CR_ID = 0;
                                    }

                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        CIsLog2.AgregaError(ex);
                    }

                    Usado = true;
                }

            }

            CalculaDia(RPersona_Diario, RTurnos_Dia, EsEntrada);

            TAPersonas.Update(dsAsistencias);

            //#endif
        }
        catch (Exception es)
        {
            CIsLog2.AgregaError(es);
            string Mensage = es.Message;
            return -2;
        }

        return 0;
    }
    /*        public static int CalculaPersona_Diario(int Persona_Diario_ID )
            {

            }*/
    /// <summary>
    /// Devuelve si existe la persona dentro de personas diario.
    /// </summary>
    /// <param name="Persona_ID"></param>
    /// <returns></returns>
    public static void Existe_Persona_in_PersonaDiario(int Persona_ID)
    {
        if (CeC_BD.EjecutaEscalarInt("Select Count(*) From EC_PERSONAS_DIARIO WHERE PERSONA_ID  = " + Persona_ID) > 0)
            return;
        else
            GeneraPrevioPersonaDiario(Persona_ID);

    }
    /// <summary>
    /// Genera dias de trabajo previos alas fechas indicadas y posteriormente
    /// almacena ese previo de fechas que se generaron.
    /// </summary>
    /// <param name="Persona_ID"></param>
    /// <returns></returns>
    public static int GeneraPrevioPersonaDiario(int Persona_ID)
    {
        return GeneraPrevioPersonaDiario(Persona_ID, DateTime.Today);
    }
    /// <summary>
    /// Genera dias de trabajo previos alas fechas indicadas y posteriormente
    /// almacena ese previo de fechas que se generaron.
    /// </summary>
    /// <param name="Persona_ID"></param>
    /// <param name="FechaDesde"></param>
    /// <param name="FechaHasta"></param>
    /// <returns></returns>
    public static int GeneraPrevioPersonaDiario(int Persona_ID, DateTime FechaDesde, DateTime FechaHasta)
    {
        if (Persona_ID < 0)
            return -1;
        GeneraDiasTrabajo();

        DateTime FechaAnterior = FechaDesde;
        DateTime FechaMaxima = FechaHasta;
        string FechasPPersona = "SELECT PERSONA_DIARIO_FECHA AS DIAS_TRABAJO FROM EC_PERSONAS_DIARIO WHERE PERSONA_ID = " + Persona_ID.ToString();

        string Qry_Persona_Diario_ID = "";
        if (CeC_BD.EsOracle)
            Qry_Persona_Diario_ID = "10000 * " + Persona_ID.ToString() + " + to_char(DIAS_TRABAJO, 'YY') * 366 + to_char(DIAS_TRABAJO, 'DDD') as PERSONA_DIARIO_ID";
        else
            Qry_Persona_Diario_ID = "10000 * " + Persona_ID.ToString() + " + DATEPART(year,DIAS_TRABAJO) % 100 * 366 + DATEPART(dayofyear,DIAS_TRABAJO) as PERSONA_DIARIO_ID";

        string Registos = "select " + Qry_Persona_Diario_ID + ", " + Persona_ID.ToString() +
            " as PERSONA_ID, DIAS_TRABAJO AS PERSONA_DIARIO_FECHA, 0 as TIPO_INC_SIS_ID, 0 as TIPO_INC_C_SIS_ID, 0 as INCIDENCIA_ID,0 as TURNO_DIA_ID, 0 as ACCESO_E_ID, " +
            "0 as ACCESO_S_ID, 0 as ACCESO_CS_ID, 0 as ACCESO_CR_ID, 0 AS PERSONA_D_HE_ID from EC_DIAS_TRABAJO WHERE DIAS_TRABAJO not in(" + FechasPPersona + ") " +
            " AND DIAS_TRABAJO >= " + CeC_BD.SqlFecha(FechaAnterior) + " AND DIAS_TRABAJO <= " + CeC_BD.SqlFecha(FechaMaxima) + " order by dias_trabajo";

        string Qry = "INSERT INTO EC_PERSONAS_DIARIO (PERSONA_DIARIO_ID, PERSONA_ID, PERSONA_DIARIO_FECHA, TIPO_INC_SIS_ID, TIPO_INC_C_SIS_ID, INCIDENCIA_ID, TURNO_DIA_ID, ACCESO_E_ID, ACCESO_S_ID, ACCESO_CS_ID, ACCESO_CR_ID, PERSONA_D_HE_ID) " + Registos;
        string Qry2 = Qry;
        if (CeC_BD.EsOracle)
        {
            return CeC_BD.EjecutaComando(Qry);

        }

        //        int RR = CeC_BD.EjecutaComando("SET IDENTITY_INSERT [EC_PERSONAS_DIARIO] ON; " + Qry +"; SET IDENTITY_INSERT [EC_PERSONAS_DIARIO] OFF");
        int RR = CeC_BD.EjecutaComando(Qry);

        return RR;
    }
    /// <summary>
    /// Utiliza los meses de los dias que se utilizaran para generar
    /// el previo de dias trabajados.
    /// </summary>
    /// <param name="Persona_ID"></param>
    /// <param name="FechaHoy"></param>
    /// <returns></returns>
    public static int GeneraPrevioPersonaDiario(int Persona_ID, DateTime FechaHoy)
    {
        int MesesFuturo = 2;
        int MesesPasado = 2;
        DateTime FechaAnterior = FechaHoy.AddMonths(-MesesPasado);
        DateTime FechaMaxima = FechaHoy.AddMonths(MesesFuturo);
        return GeneraPrevioPersonaDiario(Persona_ID, FechaAnterior, FechaMaxima);
    }

    /// <summary>
    /// Constructor que se encarga de llenar un table adapter de los horarios diarios
    /// de las personas.
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    public static System.Data.OleDb.OleDbDataAdapter NuevoEC_PERSONAS_DIARIOTableAdapter()
    {
        try
        {
            System.Data.OleDb.OleDbConnection oleDbConnection1 = new System.Data.OleDb.OleDbConnection();
            System.Data.OleDb.OleDbDataAdapter EC_PERSONAS_DIARIOTableAdapter;
            System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
            System.Data.OleDb.OleDbCommand oleDbCommand2;
            EC_PERSONAS_DIARIOTableAdapter = new System.Data.OleDb.OleDbDataAdapter();
            oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
            oleDbCommand2 = new System.Data.OleDb.OleDbCommand();
            oleDbConnection1.ConnectionString = CeC_BD.CadenaConexion();

            EC_PERSONAS_DIARIOTableAdapter.SelectCommand = oleDbSelectCommand1;
            EC_PERSONAS_DIARIOTableAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																													  new System.Data.Common.DataTableMapping("Table", "EC_PERSONAS_DIARIO", new System.Data.Common.DataColumnMapping[] {
																																																											 new System.Data.Common.DataColumnMapping("PERSONA_DIARIO_ID", "PERSONA_DIARIO_ID"),
																																																											 new System.Data.Common.DataColumnMapping("ACCESO_E_ID", "ACCESO_E_ID"),
																																																											 new System.Data.Common.DataColumnMapping("ACCESO_S_ID", "ACCESO_S_ID"),
																																																											 new System.Data.Common.DataColumnMapping("ACCESO_CS_ID", "ACCESO_CS_ID"),
																																																											 new System.Data.Common.DataColumnMapping("ACCESO_CR_ID", "ACCESO_CR_ID"),
																																																											 new System.Data.Common.DataColumnMapping("PERSONA_ID", "PERSONA_ID"),
																																																											 new System.Data.Common.DataColumnMapping("PERSONA_DIARIO_FECHA", "PERSONA_DIARIO_FECHA"),
																																																											 new System.Data.Common.DataColumnMapping("TIPO_INC_SIS_ID", "TIPO_INC_SIS_ID"),
																																																											 new System.Data.Common.DataColumnMapping("TIPO_INC_C_SIS_ID", "TIPO_INC_C_SIS_ID"),
																																																											 new System.Data.Common.DataColumnMapping("INCIDENCIA_ID", "INCIDENCIA_ID"),
																																																											 new System.Data.Common.DataColumnMapping("TURNO_DIA_ID", "TURNO_DIA_ID"),
																																																											 new System.Data.Common.DataColumnMapping("PERSONA_DIARIO_TT", "PERSONA_DIARIO_TT"),
																																																											 new System.Data.Common.DataColumnMapping("PERSONA_DIARIO_TE", "PERSONA_DIARIO_TE"),
																																																											 new System.Data.Common.DataColumnMapping("PERSONA_DIARIO_TC", "PERSONA_DIARIO_TC"),
																																																											 new System.Data.Common.DataColumnMapping("PERSONA_DIARIO_TDE", "PERSONA_DIARIO_TDE"),
																																																											 new System.Data.Common.DataColumnMapping("PERSONA_DIARIO_TES", "PERSONA_DIARIO_TES"),
																																																											 new System.Data.Common.DataColumnMapping("ACCESO_E", "ACCESO_E"),
																																																											 new System.Data.Common.DataColumnMapping("ACCESO_S", "ACCESO_S"),
																																																											 new System.Data.Common.DataColumnMapping("ACCESO_CS", "ACCESO_CS"),
																																																											 new System.Data.Common.DataColumnMapping("ACCESO_CR", "ACCESO_CR"),
																																																											 new System.Data.Common.DataColumnMapping("ACCESO_ID", "ACCESO_ID"),
																																																											 new System.Data.Common.DataColumnMapping("EXPR1", "EXPR1"),
																																																											 new System.Data.Common.DataColumnMapping("EXPR2", "EXPR2"),
																																																											 new System.Data.Common.DataColumnMapping("EXPR3", "EXPR3")})});
            EC_PERSONAS_DIARIOTableAdapter.UpdateCommand = oleDbCommand2;
            // 
            // oleDbSelectCommand1
            // 
            oleDbSelectCommand1.CommandText = @"SELECT EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID, EC_PERSONAS_DIARIO.ACCESO_E_ID, EC_PERSONAS_DIARIO.ACCESO_S_ID, EC_PERSONAS_DIARIO.ACCESO_CS_ID, EC_PERSONAS_DIARIO.ACCESO_CR_ID, EC_PERSONAS_DIARIO.PERSONA_ID, EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA, EC_PERSONAS_DIARIO.TIPO_INC_SIS_ID, EC_PERSONAS_DIARIO.TIPO_INC_C_SIS_ID, EC_PERSONAS_DIARIO.INCIDENCIA_ID, EC_PERSONAS_DIARIO.TURNO_DIA_ID, EC_PERSONAS_DIARIO.PERSONA_DIARIO_TT, EC_PERSONAS_DIARIO.PERSONA_DIARIO_TE, EC_PERSONAS_DIARIO.PERSONA_DIARIO_TC, EC_PERSONAS_DIARIO.PERSONA_DIARIO_TDE, EC_PERSONAS_DIARIO.PERSONA_DIARIO_TES, EC_ACCESOS.ACCESO_FECHAHORA AS ACCESO_E, EC_ACCESOS_1.ACCESO_FECHAHORA AS ACCESO_S, EC_ACCESOS_2.ACCESO_FECHAHORA AS ACCESO_CS, EC_ACCESOS_3.ACCESO_FECHAHORA AS ACCESO_CR, EC_ACCESOS.ACCESO_ID, EC_ACCESOS_1.ACCESO_ID AS EXPR1, EC_ACCESOS_2.ACCESO_ID AS EXPR2, EC_ACCESOS_3.ACCESO_ID AS EXPR3 FROM EC_PERSONAS_DIARIO, EC_ACCESOS, EC_ACCESOS EC_ACCESOS_1, EC_ACCESOS EC_ACCESOS_2, EC_ACCESOS EC_ACCESOS_3 WHERE EC_PERSONAS_DIARIO.ACCESO_E_ID = EC_ACCESOS.ACCESO_ID AND EC_PERSONAS_DIARIO.ACCESO_S_ID = EC_ACCESOS_1.ACCESO_ID AND EC_PERSONAS_DIARIO.ACCESO_CS_ID = EC_ACCESOS_2.ACCESO_ID AND EC_PERSONAS_DIARIO.ACCESO_CR_ID = EC_ACCESOS_3.ACCESO_ID AND (EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID = ?)";
            oleDbSelectCommand1.Connection = oleDbConnection1;
            oleDbSelectCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_DIARIO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERSONA_DIARIO_ID", System.Data.DataRowVersion.Current, null));
            // 
            // oleDbCommand2
            // 
            oleDbCommand2.CommandText = @"UPDATE EC_PERSONAS_DIARIO SET PERSONA_DIARIO_ID = ?, ACCESO_E_ID = ?, ACCESO_S_ID = ?, ACCESO_CS_ID = ?, ACCESO_CR_ID = ?, PERSONA_ID = ?, PERSONA_DIARIO_FECHA = ?, TIPO_INC_SIS_ID = ?, TIPO_INC_C_SIS_ID = ?, INCIDENCIA_ID = ?, TURNO_DIA_ID = ?, PERSONA_DIARIO_TT = ?, PERSONA_DIARIO_TE = ?, PERSONA_DIARIO_TC = ?, PERSONA_DIARIO_TDE = ?, PERSONA_DIARIO_TES = ? WHERE (PERSONA_DIARIO_ID = ?)";
            oleDbCommand2.Connection = oleDbConnection1;
            oleDbCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_DIARIO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERSONA_DIARIO_ID", System.Data.DataRowVersion.Current, null));
            oleDbCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("ACCESO_E_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "ACCESO_E_ID", System.Data.DataRowVersion.Current, null));
            oleDbCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("ACCESO_S_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "ACCESO_S_ID", System.Data.DataRowVersion.Current, null));
            oleDbCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("ACCESO_CS_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "ACCESO_CS_ID", System.Data.DataRowVersion.Current, null));
            oleDbCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("ACCESO_CR_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "ACCESO_CR_ID", System.Data.DataRowVersion.Current, null));
            oleDbCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERSONA_ID", System.Data.DataRowVersion.Current, null));
            oleDbCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_DIARIO_FECHA", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "PERSONA_DIARIO_FECHA"));
            oleDbCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_INC_SIS_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_INC_SIS_ID", System.Data.DataRowVersion.Current, null));
            oleDbCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_INC_C_SIS_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_INC_C_SIS_ID", System.Data.DataRowVersion.Current, null));
            oleDbCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("INCIDENCIA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "INCIDENCIA_ID", System.Data.DataRowVersion.Current, null));
            oleDbCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("TURNO_DIA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TURNO_DIA_ID", System.Data.DataRowVersion.Current, null));
            oleDbCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_DIARIO_TT", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "PERSONA_DIARIO_TT"));
            oleDbCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_DIARIO_TE", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "PERSONA_DIARIO_TE"));
            oleDbCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_DIARIO_TC", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "PERSONA_DIARIO_TC"));
            oleDbCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_DIARIO_TDE", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "PERSONA_DIARIO_TDE"));
            oleDbCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_DIARIO_TES", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "PERSONA_DIARIO_TES"));
            oleDbCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERSONA_DIARIO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERSONA_DIARIO_ID", System.Data.DataRowVersion.Original, null));

            return EC_PERSONAS_DIARIOTableAdapter;
        }
        catch
        {
        }
        return null;
    }

    /// <summary>
    /// Constructor que se encarga de llenar un table adapter de los
    /// turnos diarios de las personas.
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    public static System.Data.OleDb.OleDbDataAdapter NuevoEC_TURNOS_DIATableAdapter()
    {
        try
        {
            System.Data.OleDb.OleDbConnection oleDbConnection1 = new System.Data.OleDb.OleDbConnection();
            System.Data.OleDb.OleDbDataAdapter EC_TURNOS_DIATableAdapter = new System.Data.OleDb.OleDbDataAdapter();
            System.Data.OleDb.OleDbCommand oleDbSelectCommand2 = new System.Data.OleDb.OleDbCommand();


            oleDbConnection1.ConnectionString = CeC_BD.CadenaConexion();

            EC_TURNOS_DIATableAdapter.SelectCommand = oleDbSelectCommand2;
            EC_TURNOS_DIATableAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																											new System.Data.Common.DataTableMapping("Table", "EC_TURNOS_DIA", new System.Data.Common.DataColumnMapping[] {
																																																							  new System.Data.Common.DataColumnMapping("TURNO_DIA_ID", "TURNO_DIA_ID"),
																																																							  new System.Data.Common.DataColumnMapping("TURNO_DIA_HEMIN", "TURNO_DIA_HEMIN"),
																																																							  new System.Data.Common.DataColumnMapping("TURNO_DIA_HE", "TURNO_DIA_HE"),
																																																							  new System.Data.Common.DataColumnMapping("TURNO_DIA_HEMAX", "TURNO_DIA_HEMAX"),
																																																							  new System.Data.Common.DataColumnMapping("TURNO_DIA_HERETARDO", "TURNO_DIA_HERETARDO"),
																																																							  new System.Data.Common.DataColumnMapping("TURNO_DIA_HSMIN", "TURNO_DIA_HSMIN"),
																																																							  new System.Data.Common.DataColumnMapping("TURNO_DIA_HS", "TURNO_DIA_HS"),
																																																							  new System.Data.Common.DataColumnMapping("TURNO_DIA_HSMAX", "TURNO_DIA_HSMAX"),
																																																							  new System.Data.Common.DataColumnMapping("TURNO_DIA_HBLOQUE", "TURNO_DIA_HBLOQUE"),
																																																							  new System.Data.Common.DataColumnMapping("TURNO_DIA_HTIEMPO", "TURNO_DIA_HTIEMPO"),
																																																							  new System.Data.Common.DataColumnMapping("TURNO_DIA_HAYCOMIDA", "TURNO_DIA_HAYCOMIDA"),
																																																							  new System.Data.Common.DataColumnMapping("TURNO_DIA_HCS", "TURNO_DIA_HCS"),
																																																							  new System.Data.Common.DataColumnMapping("TURNO_DIA_HCR", "TURNO_DIA_HCR"),
																																																							  new System.Data.Common.DataColumnMapping("TURNO_DIA_HCTIEMPO", "TURNO_DIA_HCTIEMPO"),
																																																							  new System.Data.Common.DataColumnMapping("TURNO_DIA_HCTOLERA", "TURNO_DIA_HCTOLERA"),
																																																							  new System.Data.Common.DataColumnMapping("TURNO_DIA_HBLOQUET", "TURNO_DIA_HBLOQUET"),
																																																							  new System.Data.Common.DataColumnMapping("PERSONA_DIARIO_ID", "PERSONA_DIARIO_ID")})});

            oleDbSelectCommand2.CommandText = @"SELECT EC_TURNOS_DIA.TURNO_DIA_ID, EC_TURNOS_DIA.TURNO_DIA_HEMIN, EC_TURNOS_DIA.TURNO_DIA_HE, EC_TURNOS_DIA.TURNO_DIA_HEMAX, EC_TURNOS_DIA.TURNO_DIA_HERETARDO, EC_TURNOS_DIA.TURNO_DIA_HSMIN, EC_TURNOS_DIA.TURNO_DIA_HS, EC_TURNOS_DIA.TURNO_DIA_HSMAX, EC_TURNOS_DIA.TURNO_DIA_HBLOQUE, EC_TURNOS_DIA.TURNO_DIA_HTIEMPO, EC_TURNOS_DIA.TURNO_DIA_HAYCOMIDA, EC_TURNOS_DIA.TURNO_DIA_HCS, EC_TURNOS_DIA.TURNO_DIA_HCR, EC_TURNOS_DIA.TURNO_DIA_HCTIEMPO, EC_TURNOS_DIA.TURNO_DIA_HCTOLERA, EC_TURNOS_DIA.TURNO_DIA_HBLOQUET, EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID FROM EC_TURNOS_DIA, EC_PERSONAS_DIARIO WHERE EC_TURNOS_DIA.TURNO_DIA_ID = EC_PERSONAS_DIARIO.TURNO_DIA_ID AND (EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID = ?)";
            oleDbSelectCommand2.Connection = oleDbConnection1;
            oleDbSelectCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_DIARIO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERSONA_DIARIO_ID", System.Data.DataRowVersion.Current, null));

            return EC_TURNOS_DIATableAdapter;
        }
        catch
        {
        }
        return null;
    }
    /// <summary>
    /// Valida la agrupacion a la que pertenece el Empleado.
    /// </summary>
    /// <param name="Persona_ID">Identificador único del empleado</param>
    /// <param name="Usuario_ID">Identificador único del usuario actual</param>
    /// <param name="Agrupacion">Agrupación a la que pertenece el empleado</param>
    /// <param name="IncluirPersonaID_IN">True para indicar que valide el Persona_Id en una tabla en particular. False para validar el Persona_ID en la tabla EC_PERSONAS</param>
    /// <returns></returns>
    public static string ValidaAgrupacion(int Persona_ID, int Usuario_ID, string Agrupacion, bool IncluirPersonaID_IN)
    {
        return ValidaAgrupacion(Persona_ID.ToString(), Usuario_ID, Agrupacion, IncluirPersonaID_IN);
    }
    /// <summary>
    /// Regresa un 
    /// </summary>
    /// <param name="PersonasIDs"></param>
    /// <param name="Usuario_ID"></param>
    /// <param name="Agrupacion"></param>
    /// <param name="IncluirPersonaID_IN"></param>
    /// <returns></returns>
    public static string ValidaAgrupacion(string PersonasIDs, int Usuario_ID, string Agrupacion, bool IncluirPersonaID_IN)
    {
        if (Agrupacion == null)
            Agrupacion = "";
        string QryAgrupacion = "";
        string Qry = "";
        if (PersonasIDs != "" && PersonasIDs != "-1" && PersonasIDs != "0")
        {
            if (IncluirPersonaID_IN)
                Qry = "PERSONA_ID IN \n(" + PersonasIDs + ") \n";
            else
                Qry = " AND PERSONA_ID IN \n(" + PersonasIDs + ") \n";
        }
        else
        {
            if (Agrupacion.Trim() == "|")
                Agrupacion = "";
            ///Ahora se verifica en Usuarios Permisos
            //QryAgrupacion = " SUSCRIPCION_ID IN(SELECT SUSCRIPCION_ID FROM EC_PERMISOS_SUSCRIP WHERE USUARIO_ID = @USUARIO_ID@) ";
            if (Usuario_ID > 0)
                QryAgrupacion = " PERSONA_ID IN (" + CeC_Agrupaciones.ObtenSQLPersonaIDsPermisos(Usuario_ID) + " ) ";
            else
                QryAgrupacion = " 1=1 AND PERSONA_ID IN (SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_BORRADO = 0)";
            if (Agrupacion.Length > 0)
            {
                string AgrupacionMayus = Agrupacion.ToUpper();
                if (AgrupacionMayus.IndexOf("=") > 0 || AgrupacionMayus.IndexOf("LIKE") > 0)
                {
                    Agrupacion = Agrupacion.Substring(1, Agrupacion.Length - 2);
                    string SQL = "";
                    if (!CeC_BD.EsOracle && AgrupacionMayus.IndexOf("LIKE") > 0)
                    {
                        SQL = " \nCOLLATE SQL_LATIN1_GENERAL_CP1_CI_AI \n";
                    }
                    QryAgrupacion += " AND PERSONA_ID IN \n(SELECT EC_PERSONAS.PERSONA_ID FROM EC_PERSONAS, EC_PERSONAS_DATOS, EC_TURNOS WHERE EC_PERSONAS.PERSONA_ID = EC_PERSONAS_DATOS.PERSONA_ID AND " +
                        "EC_TURNOS.TURNO_ID = EC_PERSONAS.TURNO_ID AND " +
                        Agrupacion + SQL +
                        " ) ";
                }
                else
                //                    QryAgrupacion += " AND AGRUPACION_NOMBRE LIKE  @AGRUPACION_NOMBRE@";
                {
                    QryAgrupacion += " AND \nAGRUPACION_NOMBRE LIKE  @AGRUPACION_NOMBRE@ ";
                }
            }
            if (IncluirPersonaID_IN)
            {
                Qry = "PERSONA_ID IN \n(SELECT PERSONA_ID FROM EC_PERSONAS WHERE " + QryAgrupacion + " )";
            }
            else
                Qry = " AND " + QryAgrupacion + " ";
        }
        //        Qry = CeC_BD.AsignaParametro(Qry, "PERSONA_ID", Persona_ID);
        Qry = CeC_BD.AsignaParametro(Qry, "USUARIO_ID", Usuario_ID);
        Qry = CeC_BD.AsignaParametro(Qry, "AGRUPACION_NOMBRE", Agrupacion + "%");
        return Qry;
    }
    /// <summary>
    /// Devuelve los ID que corresponden alas faltas.
    /// </summary>
    /// <param name="Sesion"></param>
    /// <returns></returns>
    public static string ObtenIDFaltas(CeC_Sesion Sesion)
    {
        return "3,4,6,7,8,12,17,20,23";
    }
    /// <summary>
    /// Obtiene el total de faltas para un empleado en especifico incluyendo 
    /// </summary>
    /// <param name="Persona_ID">Con referente a este ID de persona (empleado)</param>
    /// <param name="Agrupacion"></param>
    /// <param name="FechaInicial">Fecha de inicio de faltas</param>
    /// <param name="FechaFinal">Fecha de termino de faltas</param>
    /// <param name="Usuario_ID"></param>
    /// <returns></returns>
    public static int ObtenTotalFaltas(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int Usuario_ID)
    {
        string IDFaltas = ObtenIDFaltas(null);
        return CeC_BD.EjecutaEscalarInt("SELECT COUNT(PERSONA_DIARIO_ID) FROM EC_PERSONAS_DIARIO WHERE " +
            " PERSONA_DIARIO_FECHA >= " + CeC_BD.SqlFecha(FechaInicial) + " AND PERSONA_DIARIO_FECHA <" +
            CeC_BD.SqlFecha(FechaFinal) + " AND TIPO_INC_SIS_ID IN (" + IDFaltas + ") AND INCIDENCIA_ID <= 0 " +
            " AND " + ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, true));
    }
    /// <summary>
    /// Devuelve el Id de las Asistencias
    /// </summary>
    /// <param name="Sesion">(empleado)</param>
    /// <returns></returns>
    public static string ObtenIDAsistencias(CeC_Sesion Sesion)
    {
        return "1,-2,13";
    }
    /// <summary>
    /// Devuelve el Id de las Asistencias
    /// </summary>
    /// <param name="Sesion">(empleado)</param>
    /// <returns></returns>
    public static int ObtenTotalAsistencias(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int Usuario_ID)
    {
        string IDAsistencias = ObtenIDAsistencias(null);
        return CeC_BD.EjecutaEscalarInt("SELECT COUNT(PERSONA_DIARIO_ID) FROM EC_PERSONAS_DIARIO WHERE  " +
            " PERSONA_DIARIO_FECHA >= " + CeC_BD.SqlFecha(FechaInicial) + " AND PERSONA_DIARIO_FECHA <" +
            CeC_BD.SqlFecha(FechaFinal) + " AND TIPO_INC_SIS_ID IN (" + IDAsistencias + ") AND INCIDENCIA_ID <= 0 " +
            " AND " + ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, true));
    }
    /// <summary>
    /// Obtiene el ID de los retardos.
    /// </summary>
    /// <param name="Sesion">Mandando una variable de sesion</param>
    /// <returns></returns>
    public static string ObtenIDRetardos(CeC_Sesion Sesion)
    {
        return "2,5,9,16,18,19,21,22,24";
    }
    /// <summary>
    /// Devuelve el Id de las Asistencias
    /// </summary>
    /// <param name="Sesion"></param>
    /// <returns></returns>
    public static int ObtenTotalRetardos(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int Usuario_ID)
    {
        string IDRetardos = ObtenIDRetardos(null);
        return CeC_BD.EjecutaEscalarInt("SELECT COUNT(PERSONA_DIARIO_ID) FROM EC_PERSONAS_DIARIO WHERE " +
            " PERSONA_DIARIO_FECHA >= " + CeC_BD.SqlFecha(FechaInicial) + " AND PERSONA_DIARIO_FECHA <" +
            CeC_BD.SqlFecha(FechaFinal) + " AND TIPO_INC_SIS_ID IN (" + IDRetardos + ") AND INCIDENCIA_ID <= 0 " +
            " AND " + ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, true));
    }

    /// <summary>
    /// Obtiene el total de justificaciones por ID de persona.
    /// </summary>
    /// <param name="Persona_ID"></param>
    /// <param name="Agrupacion"></param>
    /// <param name="Agrupacion"></param>
    /// <param name="FechaInicial"></param>
    /// <param name="FechaFinal"></param>
    /// <param name="Usuario_ID"></param>
    /// <returns></returns>
    public static int ObtenTotalJustificaciones(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int Usuario_ID)
    {

        return CeC_BD.EjecutaEscalarInt("SELECT COUNT(PERSONA_DIARIO_ID) FROM EC_PERSONAS_DIARIO WHERE " +
            " PERSONA_DIARIO_FECHA >= " + CeC_BD.SqlFecha(FechaInicial) + " AND PERSONA_DIARIO_FECHA <" +
            CeC_BD.SqlFecha(FechaFinal) + " AND  INCIDENCIA_ID > 0 " +
            " AND " + ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, true));
    }
    /// <summary>
    /// Obtiene el total de segundos por dia de una fecha en especifico.
    /// </summary>
    /// <param name="Campo"></param>
    /// <returns></returns>
    public static string QryCampoSegundos(string Campo)
    {
        string SegundosDia = "DATEDIFF(second, " + CeC_BD.SqlFechaNula() + ", " + Campo + ")";
        if (CeC_BD.EsOracle)
            SegundosDia = " (" + Campo + " - " + CeC_BD.SqlFechaNula() + ") * 86400";
        return SegundosDia;
    }
    /// <summary>
    /// Obtiene el total de segundos de diario sobre una persona en especifico.
    /// </summary>
    /// <param name="CampoPersonaDiario"></param>
    /// <param name="Persona_ID"></param>
    /// <param name="Agrupacion"></param>
    /// <param name="FechaInicial"></param>
    /// <param name="FechaFinal"></param>
    /// <param name="Usuario_ID"></param>
    /// <returns></returns>
    public static int ObtenTotalSegundos(string CampoPersonaDiario, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int Usuario_ID)
    {

        return CeC_BD.EjecutaEscalarInt("SELECT SUM(" + QryCampoSegundos(CampoPersonaDiario) + ") AS TotalSegundos " +
                " FROM EC_PERSONAS_DIARIO WHERE " +
            " PERSONA_DIARIO_FECHA >= " + CeC_BD.SqlFecha(FechaInicial) + " AND PERSONA_DIARIO_FECHA <" +
            CeC_BD.SqlFecha(FechaFinal) + " " +
            " AND " + ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, true));
    }
    /// <summary>
    /// Se encarga de obtener el total de horas trabajadas para una 
    /// persona en especifico.
    /// </summary>
    /// <param name="CampoPersonaDiario"></param>
    /// <param name="Persona_ID"></param>
    /// <param name="Agrupacion"></param>
    /// <param name="FechaInicial"></param>
    /// <param name="FechaFinal"></param>
    /// <param name="Usuario_ID"></param>
    /// <returns></returns>
    public static string ObtenTotalHoras(string CampoPersonaDiario, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int Usuario_ID)
    {
        int Total = ObtenTotalSegundos(CampoPersonaDiario, Persona_ID, Agrupacion, FechaInicial, FechaFinal, Usuario_ID);
        if (Total <= -9999)
            return "00:00:00";
        TimeSpan TS = new TimeSpan(Total / (60 * 60), (Total / 60) % 60, Total % (60));
        int TotHoras = (TS.Days * 24 + TS.Hours);
        return TotHoras.ToString("#,##0") + ":" + TS.Minutes.ToString("00") + ":" + TS.Seconds.ToString("00");
    }
    /// <summary>
    /// Obtiene el total de segundos por dia de una persona en especifico.
    /// </summary>
    /// <param name="CampoPersonaDiario"></param>
    /// <param name="PERSONA_DIARIO_ID"></param>
    /// <returns></returns>
    public static int ObtenTotalSegundosES(string CampoPersonaDiario, int PERSONA_DIARIO_ID)
    {
        return CeC_BD.EjecutaEscalarInt("SELECT SUM(" + QryCampoSegundos(CampoPersonaDiario) + ") AS TotalSegundos " +
                " FROM EC_PERSONAS_ES WHERE PERSONA_DIARIO_ID =" + PERSONA_DIARIO_ID);
    }
    /// <summary>
    /// Obtiene el total de tiempo para una persona en especifico.
    /// </summary>
    /// <param name="CampoPersonaDiario"></param>
    /// <param name="PERSONA_DIARIO_ID"></param>
    /// <returns></returns>
    public static TimeSpan ObtenTotalTiempoES(string CampoPersonaDiario, int PERSONA_DIARIO_ID)
    {
        int Total = ObtenTotalSegundosES(CampoPersonaDiario, PERSONA_DIARIO_ID);
        if (Total <= -9999)
            return new TimeSpan();
        TimeSpan TS = new TimeSpan(Total / (60 * 60), (Total / 60) % 60, Total % (60));
        return TS;
    }
    /// <summary>
    /// Obtiene el total de Dias Festivos para el periodo selecccionado
    /// </summary>
    /// <param name="Persona_ID"></param>
    /// <param name="Agrupacion"></param>
    /// <param name="FechaInicial"></param>
    /// <param name="FechaFinal"></param>
    /// <param name="Usuario_ID"></param>
    /// <returns></returns>
    public static int ObtenTotalDiasFestivos(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int Usuario_ID)
    {
        return CeC_BD.EjecutaEscalarInt("SELECT COUNT(PERSONA_DIARIO_ID) FROM EC_PERSONAS_DIARIO WHERE " +
            " PERSONA_DIARIO_FECHA >= " + CeC_BD.SqlFecha(FechaInicial) + " AND PERSONA_DIARIO_FECHA <" +
            CeC_BD.SqlFecha(FechaFinal) + " AND  TIPO_INC_SIS_ID = 11 " +
            " AND " + ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, true));
    }
    /// <summary>
    /// Obtiene el total de dias descanzados para una persona en especifico.
    /// </summary>
    /// <param name="Persona_ID"></param>
    /// <param name="Agrupacion"></param>
    /// <param name="FechaInicial"></param>
    /// <param name="FechaFinal"></param>
    /// <param name="Usuario_ID"></param>
    /// 
    /// <returns></returns>
    public static int ObtenTotalDiasDescansados(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int Usuario_ID)
    {
        return CeC_BD.EjecutaEscalarInt("SELECT COUNT(PERSONA_DIARIO_ID) FROM EC_PERSONAS_DIARIO WHERE " +
            " PERSONA_DIARIO_FECHA >= " + CeC_BD.SqlFecha(FechaInicial) + " AND PERSONA_DIARIO_FECHA <" +
            CeC_BD.SqlFecha(FechaFinal) + " AND  TIPO_INC_SIS_ID = 10 " +
            " AND " + ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, true));
    }
    /// <summary>
    /// Obtiene las asistencias y las configura en un DataSet
    /// estas asistencias se encuentran en un dataset.
    /// </summary>
    /// <param name="EntradaSalida"></param>
    /// <param name="TurnoDia"></param>
    /// <param name="IncidenciaAbr"></param>
    /// <param name="MuestraAgrupacion"></param>
    /// <param name="MuestraEmpleado"></param>
    /// <param name="Persona_ID"></param>
    /// <param name="Agrupacion"></param>
    /// <param name="FechaInicial"></param>
    /// <param name="FechaFinal"></param>
    /// <param name="Sesion"></param>
    /// <returns></returns>
    public static DataSet ObtenAsistenciaHorizontal(bool EntradaSalida, bool TurnoDia, bool IncidenciaAbr, bool MuestraAgrupacion, bool MuestraEmpleado, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, CeC_Sesion Sesion)
    {
        string Campos = "";
        string OrdenarPor = "";

        if (MuestraAgrupacion)
        {
            Campos += "AGRUPACION_NOMBRE, ";
            OrdenarPor += "AGRUPACION_NOMBRE, ";
        }
        Campos += "PERSONA_LINK_ID ";
        if (MuestraEmpleado)
        {
            Campos += ",PERSONA_NOMBRE ";
            OrdenarPor += "PERSONA_NOMBRE,";
        }
        OrdenarPor += " PERSONA_LINK_ID";
        if (TurnoDia)
            Campos += ", (SELECT TURNO_NOMBRE FROM  EC_PERSONAS INNER JOIN EC_TURNOS ON EC_PERSONAS.TURNO_ID = EC_TURNOS.TURNO_ID WHERE PERSONA_ID = EC_V_ASISTENCIAS.PERSONA_ID) AS TURNO_NOMBRE ";

        int PersonaDiarioIDSum = 0;
        int Dia = 0;
        for (DateTime Fecha = FechaInicial; Fecha < FechaFinal; Fecha = Fecha.AddDays(1))
        {

            if (Fecha.Year != FechaInicial.Year && Fecha.Day == 1 && Fecha.Month == 1 && ((Fecha.Year - 1) % 4) != 0)
                PersonaDiarioIDSum++;

            if (EntradaSalida)
                Campos += ", (SELECT ENTRADASALIDA " + CeC_BD.Concatenador + " ' / ' " + CeC_BD.Concatenador + " INCIDENCIA_ABR FROM EC_V_PERSONAS_DIARIO WHERE (PERSONA_DIARIO_ID = EC_V_ASISTENCIAS.PERSONA_DIARIO_ID + " + PersonaDiarioIDSum + ")) AS ASISTENCIA_D" + Dia;
            if (TurnoDia)
                Campos += ", (SELECT TURNO " + CeC_BD.Concatenador + " ' / ' " + CeC_BD.Concatenador + " INCIDENCIA_ABR FROM EC_V_PERSONAS_DIARIO WHERE (PERSONA_DIARIO_ID = EC_V_ASISTENCIAS.PERSONA_DIARIO_ID + " + PersonaDiarioIDSum + ")) AS TURNO_D" + Dia;
            if (IncidenciaAbr)
                Campos += ", (SELECT INCIDENCIA_ABR FROM EC_V_PERSONAS_DIARIO WHERE (PERSONA_DIARIO_ID = EC_V_ASISTENCIAS.PERSONA_DIARIO_ID + " + PersonaDiarioIDSum + ")) AS DIA" + Dia;
            PersonaDiarioIDSum++;
            Dia++;
        }
        string Qry = "SELECT    PERSONA_DIARIO_ID,  " + Campos + " " +
                " FROM         EC_V_ASISTENCIAS " +
                " WHERE        (PERSONA_DIARIO_FECHA = @FECHA_INICIAL@) " +
                ValidaAgrupacion(Persona_ID, Sesion.USUARIO_ID, Agrupacion, false) +
                " \n ORDER BY " + OrdenarPor;

        Qry = CeC_BD.AsignaParametro(Qry, "USUARIO_ID", Sesion.USUARIO_ID);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal);
        Qry = CeC_BD.AsignaParametro(Qry, "AGRUPACION_NOMBRE", Agrupacion + "%");

        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }
    /// <summary>
    /// Funcion que se encarga de realizar la consulta para obtener la asistencia en forma horizontal
    /// </summary>
    /// param name="SesionSeguridad">Firma para comprobar que estes Logeado</param>
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
    /// <returns></returns>
    public static DataSet ObtenAsistenciaHorizontalN(bool EntradaSalida, bool TurnoDia, bool IncidenciaAbr, bool ColorTurno, bool ColorIncidencia,
        bool MuestraAgrupacion, bool MuestraEmpleado, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, string Lang, CeC_Sesion Sesion)
    {
        return ObtenAsistenciaHorizontalN(EntradaSalida, TurnoDia, IncidenciaAbr, ColorTurno, ColorIncidencia, MuestraAgrupacion, MuestraEmpleado, false, Persona_ID, Agrupacion, FechaInicial, FechaFinal, Lang, Sesion);
    }

    public static DataSet ObtenAsistenciaHorizontalN(bool EntradaSalida, bool TurnoDia, bool IncidenciaAbr, bool ColorTurno, bool ColorIncidencia,
    bool MuestraAgrupacion, bool MuestraEmpleado, bool MuestraTotales, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, string Lang, CeC_Sesion Sesion)
    {
        string Campos = "";
        string OrdenarPor = "";

        if (MuestraAgrupacion)
        {
            Campos += "EC_PERSONAS.AGRUPACION_NOMBRE AS AGR, ";
            OrdenarPor += "EC_PERSONAS.AGRUPACION_NOMBRE, ";
        }
        Campos += "EC_PERSONAS.PERSONA_LINK_ID AS LINK ";
        if (MuestraEmpleado)
        {
            Campos += ",EC_PERSONAS.PERSONA_NOMBRE AS NOMBRE ";
            OrdenarPor += "EC_PERSONAS.PERSONA_NOMBRE,";
        }
        OrdenarPor += " EC_PERSONAS.PERSONA_LINK_ID";
        Campos += ", TURNO_NOMBRE AS TURNO ";
        string TablasIJ = " ";

        int PersonaDiarioIDSum = 0;
        int Dia = 0;
        Campos += "\n";
        for (DateTime Fecha = FechaInicial; Fecha < FechaFinal; Fecha = Fecha.AddDays(1))
        {


            if (Fecha.Year != FechaInicial.Year && Fecha.Day == 1 && Fecha.Month == 1 && ((Fecha.Year - 1) % 4) != 0)
                PersonaDiarioIDSum++;
            Dia = PersonaDiarioIDSum;
            string TablaNombre = "ASISTENCIA_D" + Dia;
            TablasIJ = CeC.AgregaSeparador(TablasIJ, "EC_V_PERSONAS_DIARIO AS " + TablaNombre + " ON EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID + " + PersonaDiarioIDSum + " = " + TablaNombre + ".PERSONA_DIARIO_ID", "\n INNER JOIN ");
            if (EntradaSalida && !IncidenciaAbr)
                Campos = CeC.AgregaSeparador(Campos, TablaNombre + ".ENTRADASALIDA" + " AS IO" + Dia, ", ");
            if (EntradaSalida && IncidenciaAbr)
                Campos = CeC.AgregaSeparador(Campos, TablaNombre + ".ENTRADASALIDA " + CeC_BD.Concatenador + " '/' " + CeC_BD.Concatenador + TablaNombre + ".INCIDENCIA_ABR" + " AS IO" + Dia, ", ");
            if (TurnoDia && !IncidenciaAbr)
                Campos = CeC.AgregaSeparador(Campos, TablaNombre + ".TURNO" + " AS TD" + Dia, ", ");
            if (TurnoDia && IncidenciaAbr)
                Campos = CeC.AgregaSeparador(Campos, TablaNombre + ".TURNO " + CeC_BD.Concatenador + " '/' " + CeC_BD.Concatenador + TablaNombre + ".INCIDENCIA_ABR" + " AS TD" + Dia, ", ");
            if (!TurnoDia && !EntradaSalida)
                Campos = CeC.AgregaSeparador(Campos, TablaNombre + ".INCIDENCIA_ABR" + " AS ABR" + Dia, ", ");
            if (ColorTurno)
                Campos = CeC.AgregaSeparador(Campos, TablaNombre + ".TURNO_COLOR" + " AS TC" + Dia, ", ");
            if (ColorIncidencia)
                Campos = CeC.AgregaSeparador(Campos, TablaNombre + ".INCIDENCIA_COLOR" + " AS IC" + Dia, ", ");
            PersonaDiarioIDSum++;

        }
        string QryTotales = "";
        if (MuestraTotales)
        {
            QryTotales += ", (SELECT COUNT (*) FROM EC_V_PERSONAS_DIARIO_IG WHERE " +
                "EC_V_PERSONAS_DIARIO_IG.PERSONA_DIARIO_ID >= EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID AND EC_V_PERSONAS_DIARIO_IG.PERSONA_DIARIO_ID < EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID + " + PersonaDiarioIDSum +
                " AND TIPO_INC_SIS_ID IN (" + ObtenIDAsistencias(Sesion) + ") AND INCIDENCIA_ID <= 0) AS Asistencias";
            QryTotales += ", (SELECT COUNT (*) FROM EC_V_PERSONAS_DIARIO_IG WHERE " +
                "EC_V_PERSONAS_DIARIO_IG.PERSONA_DIARIO_ID >= EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID AND EC_V_PERSONAS_DIARIO_IG.PERSONA_DIARIO_ID < EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID + " + PersonaDiarioIDSum +
                " AND TIPO_INC_SIS_ID IN (" + ObtenIDFaltas(Sesion) + ") AND INCIDENCIA_ID <= 0) AS Faltas";
            QryTotales += ", (SELECT COUNT (*) FROM EC_V_PERSONAS_DIARIO_IG WHERE " +
                "EC_V_PERSONAS_DIARIO_IG.PERSONA_DIARIO_ID >= EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID AND EC_V_PERSONAS_DIARIO_IG.PERSONA_DIARIO_ID < EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID + " + PersonaDiarioIDSum +
                " AND TIPO_INC_SIS_ID IN (" + ObtenIDRetardos(Sesion) + ") AND INCIDENCIA_ID <= 0) AS Retardos";
            QryTotales += ", (SELECT COUNT (*) FROM EC_V_PERSONAS_DIARIO_IG WHERE " +
                "EC_V_PERSONAS_DIARIO_IG.PERSONA_DIARIO_ID >= EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID AND EC_V_PERSONAS_DIARIO_IG.PERSONA_DIARIO_ID < EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID + " + PersonaDiarioIDSum +
                " AND INCIDENCIA_ID > 0) AS Justificados";
            /*
            QryTotales += ", 0 AS Asistencias";
            QryTotales += ", 0 AS Faltas";
            QryTotales += ", 0 AS Retardos";
            QryTotales += ", 0 AS Justificados";*/

        }
        string Qry = "SELECT    EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID AS ID,  " + Campos + " " + QryTotales + " " +
                " FROM         EC_PERSONAS_DIARIO INNER JOIN EC_PERSONAS ON EC_PERSONAS_DIARIO.PERSONA_ID = EC_PERSONAS.PERSONA_ID  " +
                " INNER JOIN EC_TURNOS ON EC_PERSONAS.TURNO_ID = EC_TURNOS.TURNO_ID " + TablasIJ +
                "\n WHERE        (EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA = @FECHA_INICIAL@) AND EC_PERSONAS_DIARIO." +
                ValidaAgrupacion(Persona_ID, Sesion.USUARIO_ID, Agrupacion, true) +
                " \n ORDER BY " + OrdenarPor;

        Qry = CeC_BD.AsignaParametro(Qry, "USUARIO_ID", Sesion.USUARIO_ID);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal);
        Qry = CeC_BD.AsignaParametro(Qry, "AGRUPACION_NOMBRE", Agrupacion + "%");
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry);
        /*if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
        {
            foreach(DataRow DR in DS.Tables[0].Rows)

        }*/

        return DS;
    }

    public static DataSet ObtenAsistenciaHorizontalDias(bool EntradaSalida, bool TurnoDia, bool IncidenciaAbr, bool ColorTurno, bool ColorIncidencia,
        bool MuestraAgrupacion, bool MuestraEmpleado, int Persona_ID, string Agrupacion, List<DateTime> Dias, string Lang, CeC_Sesion Sesion, int UsuarioID)
    {
        string Campos = "";
        string OrdenarPor = "";
        if (Dias == null || Dias.Count <= 0)
            return null;
        if (MuestraAgrupacion)
        {
            Campos += "EC_PERSONAS.AGRUPACION_NOMBRE AS AGR, ";
            OrdenarPor += "EC_PERSONAS.AGRUPACION_NOMBRE, ";
        }
        Campos += "EC_PERSONAS.PERSONA_LINK_ID AS LINK ";
        if (MuestraEmpleado)
        {
            Campos += ",EC_PERSONAS.PERSONA_NOMBRE AS NOMBRE ";
            OrdenarPor += "EC_PERSONAS.PERSONA_NOMBRE,";
        }
        OrdenarPor += " EC_PERSONAS.PERSONA_LINK_ID";
        Campos += ", TURNO_NOMBRE AS TURNO ";
        string TablasIJ = " ";

        int Dia = 0;
        Campos += "\n";
        DateTime FechaInicial = Dias[0];
        string Wheres = "";
        foreach (DateTime Fecha in Dias)
        {


            string TablaNombre = "ASISTENCIA_D" + Dia;
            TablasIJ = CeC.AgregaSeparador(TablasIJ, "EC_V_PERSONAS_DIARIO AS " + TablaNombre + " ON EC_PERSONAS_DIARIO.PERSONA_ID = " + TablaNombre + ".PERSONA_ID", "\n INNER JOIN ");
            if (EntradaSalida && !IncidenciaAbr)
                Campos = CeC.AgregaSeparador(Campos, TablaNombre + ".ENTRADASALIDA" + " AS IO" + Dia, ", ");
            if (EntradaSalida && IncidenciaAbr)
                Campos = CeC.AgregaSeparador(Campos, TablaNombre + ".ENTRADASALIDA " + CeC_BD.Concatenador + " '/' " + CeC_BD.Concatenador + TablaNombre + ".INCIDENCIA_ABR" + " AS IO" + Dia, ", ");
            if (TurnoDia && !IncidenciaAbr)
                Campos = CeC.AgregaSeparador(Campos, TablaNombre + ".TURNO" + " AS TD" + Dia, ", ");
            if (TurnoDia && IncidenciaAbr)
                Campos = CeC.AgregaSeparador(Campos, TablaNombre + ".TURNO " + CeC_BD.Concatenador + " '/' " + CeC_BD.Concatenador + TablaNombre + ".INCIDENCIA_ABR" + " AS TD" + Dia, ", ");
            if (!TurnoDia && !EntradaSalida)
                Campos = CeC.AgregaSeparador(Campos, TablaNombre + ".INCIDENCIA_ABR" + " AS ABR" + Dia, ", ");
            if (ColorTurno)
                Campos = CeC.AgregaSeparador(Campos, TablaNombre + ".TURNO_COLOR" + " AS TC" + Dia, ", ");
            if (ColorIncidencia)
                Campos = CeC.AgregaSeparador(Campos, TablaNombre + ".INCIDENCIA_COLOR" + " AS IC" + Dia, ", ");
            Dia++;
            Wheres = CeC.AgregaSeparador(Wheres, TablaNombre + ".PERSONA_DIARIO_FECHA = " + CeC_BD.SqlFecha(Fecha), " AND ");
        }
        string Qry = "SELECT    EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID AS ID,  " + Campos + " " +
                " FROM         EC_PERSONAS_DIARIO INNER JOIN EC_PERSONAS ON EC_PERSONAS_DIARIO.PERSONA_ID = EC_PERSONAS.PERSONA_ID  " +
                " INNER JOIN EC_TURNOS ON EC_PERSONAS.TURNO_ID = EC_TURNOS.TURNO_ID " + TablasIJ +
                "\n WHERE        (EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA = @FECHA_INICIAL@) AND EC_PERSONAS_DIARIO." +
                ValidaAgrupacion(Persona_ID, UsuarioID, Agrupacion, true) +
                " AND " + Wheres +
                " \n ORDER BY " + OrdenarPor;

        Qry = CeC_BD.AsignaParametro(Qry, "USUARIO_ID", UsuarioID);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "AGRUPACION_NOMBRE", Agrupacion + "%");

        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }

    /// <summary>
    /// Obtiene la asistencia semanal de un empleado en especifico.
    /// </summary>
    /// <param name="EntradaSalida"></param>
    /// <param name="TurnoDia"></param>
    /// <param name="MuestraAgrupacion"></param>
    /// <param name="MuestraEmpleado"></param>
    /// <param name="Persona_ID"></param>
    /// <param name="Agrupacion"></param>
    /// <param name="FechaInicial"></param>
    /// <param name="Sesion"></param>
    /// <returns></returns>
    public static DataSet ObtenAsistenciaSemanal(bool EntradaSalida, bool TurnoDia, bool MuestraAgrupacion, bool MuestraEmpleado, int Persona_ID, string Agrupacion, DateTime FechaInicial, CeC_Sesion Sesion)
    {
        string Campos = "";
        string OrdenarPor = "";

        if (MuestraAgrupacion)
        {
            Campos += "AGRUPACION_NOMBRE, ";
            OrdenarPor += "AGRUPACION_NOMBRE, ";
        }
        Campos += "PERSONA_LINK_ID,";
        if (MuestraEmpleado)
        {
            Campos += " PERSONA_NOMBRE, ";
            OrdenarPor += "PERSONA_NOMBRE, ";
        }
        OrdenarPor += " PERSONA_LINK_ID";
        Campos += " TURNO_NOMBRE";
        for (int Cont = 0; Cont < 7; Cont++)
        {
            if (EntradaSalida)
                Campos += ", ASISTENCIA_D" + Cont.ToString();

            if (TurnoDia)
                Campos += ", TURNO_D" + Cont.ToString();
        }
        string Qry = "SELECT PERSONA_DIARIO_ID,  " + Campos + " " +
                    " FROM         EC_V_ASISTENCIAS_SEMANA " +
                    " WHERE        (PERSONA_DIARIO_FECHA = @FECHA_INICIAL@)  " +
        ValidaAgrupacion(Persona_ID, Sesion.USUARIO_ID, Agrupacion, false) +
                    " \n ORDER BY " + OrdenarPor;
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }
    /// <summary>
    /// Obtiene las asistencias generales de una persona en especifico.
    /// </summary>
    /// <param name="EntradaSalida"></param>
    /// <param name="Comida"></param>
    /// <param name="HorasExtras"></param>
    /// <param name="Totales"></param>
    /// <param name="Incidencia"></param>
    /// <param name="TurnoDia"></param>
    /// <param name="TiposIncidenciasSistemaIDs"></param>
    /// <param name="TiposIncidenciasIDs"></param>
    /// <param name="MuestraAgrupacion"></param>
    /// <param name="MuestraEmpleado"></param>
    /// <param name="Persona_ID"></param>
    /// <param name="Agrupacion"></param>
    /// <param name="FechaInicial"></param>
    /// <param name="FechaFinal"></param>
    /// <param name="Sesion"></param>
    /// <returns></returns>
    public static DataSet ObtenAsistencia(bool EntradaSalida, bool Comida, bool HorasExtras, bool Totales, bool Incidencia, bool TurnoDia, string TiposIncidenciasSistemaIDs, string TiposIncidenciasIDs, bool MuestraAgrupacion, bool MuestraEmpleado, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, CeC_Sesion Sesion)
    {
        string Campos = "";
        string OrdenarPor = "";

        //, ACCESO_E, ACCESO_S, INCIDENCIA_NOMBRE, TURNO, PERSONA_DIARIO_TT, PERSONA_DIARIO_TDE
        if (MuestraAgrupacion)
        {
            Campos += "AGRUPACION_NOMBRE, ";
            OrdenarPor += "AGRUPACION_NOMBRE, ";
        }

        /*if (Persona_ID > 0)
        {
            Campos += "PERSONA_DIARIO_FECHA";
            OrdenarPor += "PERSONA_DIARIO_FECHA";
        }
        else*/
        {
            Campos += "PERSONA_LINK_ID";
            if (MuestraEmpleado)
            {
                Campos += ", PERSONA_NOMBRE";
                OrdenarPor += "PERSONA_NOMBRE, ";
            }
            Campos += ", PERSONA_DIARIO_FECHA";
            OrdenarPor += "PERSONA_LINK_ID, PERSONA_DIARIO_FECHA";
        }
        if (Comida)
            if (EntradaSalida)
                Campos += ", ACCESO_E,  ACCESO_CS, ACCESO_CR, ACCESO_S";
            else
                Campos += ", ACCESO_CS, ACCESO_CR";
        else
            if (EntradaSalida)
                Campos += ", ACCESO_E, ACCESO_S";
        if (TurnoDia)
        {
            Campos += ", TURNO";
        }
        if (Incidencia)
        {
            Campos += ", INCIDENCIA_NOMBRE, INCIDENCIA_ABR";
            if (Comida)
                Campos += ",  TIPO_INC_C_SIS_NOMBRE";
        }

        if (Totales)
        {
            Campos += ", PERSONA_DIARIO_TT, PERSONA_DIARIO_TDE";
            ///Se mostrara Siempre el Tiempo de estancia del trabajador, falta validar que solo
            ///cuando enga habilitado la comida o la generación de entradas y salidas
            Campos += ", PERSONA_DIARIO_TE";
            if (Comida)
                Campos += ", PERSONA_DIARIO_TC";
            //Campos += ", PERSONA_DIARIO_TE, PERSONA_DIARIO_TC";
        }
        if (HorasExtras)
            Campos += ",  PERSONA_D_HE_SIS,  PERSONA_D_HE_CAL,  PERSONA_D_HE_APL";
        if (Incidencia)
        {
            Campos += ", INCIDENCIA_COMENTARIO";
        }
        return ObtenAsistenciaLineal(Persona_ID, Agrupacion, FechaInicial, FechaFinal, Campos, OrdenarPor, TiposIncidenciasSistemaIDs, TiposIncidenciasIDs, Sesion);

    }
    /// <summary>
    /// Agrega la grafica de asistencia.
    /// </summary>
    /// <param name="Cadena"></param>
    /// <param name="Campo"></param>
    /// <param name="Valor"></param>
    /// <returns></returns>
    private static string ObtenAsistenciaGraficaAgregaOr(string Cadena, string Campo, string Valor)
    {
        if (Cadena.Length > 0)
            Cadena += " OR ";
        Cadena += " " + Campo + " = " + Valor + " ";
        return Cadena;
    }
    /// <summary>
    /// Obtiene la cadena para la grafica de asistencias.
    /// </summary>
    /// <param name="Campo"></param>
    /// <param name="IDS"></param>
    /// <returns></returns>
    private static string ObtenAsistenciaGraficaObtenCadena(string Campo, string[] IDS)
    {
        string Ret = "";
        foreach (string ID in IDS)
        {
            if (ID.Trim().Length > 0)
                Ret = ObtenAsistenciaGraficaAgregaOr(Ret, Campo, ID);
        }
        return " SUM(CASE WHEN (" + Ret + ") AND INCIDENCIA_ID <= 0 THEN 1 ELSE 0 END) ";
    }
    /// <summary>
    /// Obtiene las asistencias totales de un historial previo.
    /// </summary>
    /// <param name="MostrarAgrupacion"></param>
    /// <param name="MostrarEmpleado"></param>
    /// <param name="Persona_ID"></param>
    /// <param name="Agrupacion"></param>
    /// <param name="FechaInicial"></param>
    /// <param name="FechaFinal"></param>
    /// <param name="Sesion"></param>
    /// <returns></returns>
    public static DataSet ObtenAsistenciaTotalesHistorial(bool MostrarAgrupacion, bool MostrarEmpleado, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, CeC_Sesion Sesion)
    {
        string Campos = "";
        string CamposTot = "";
        string OrdenarPor = "";
        if (MostrarAgrupacion)
        {
            Campos = "AGRUPACION_NOMBRE";
            OrdenarPor = "AGRUPACION_NOMBRE";
        }
        if (MostrarEmpleado)
        {
            if (Campos.Length > 0)
            {
                Campos += ", ";
                OrdenarPor += ", ";
            }
            Campos += "PERSONA_LINK_ID, PERSONA_NOMBRE";
            OrdenarPor += "PERSONA_NOMBRE, PERSONA_LINK_ID";
        }
        OrdenarPor = CeC.AgregaSeparador(OrdenarPor, "ALMACEN_INC_ID DESC", ","); ;
        //        Campos = CeC.AgregaSeparador(Campos, "ALMACEN_INC_FECHA,TIPO_INCIDENCIA_R_ID, ALMACEN_INC_NO, ALMACEN_INC_SALDO, ALMACEN_INC_ID", ", ");
        Campos = CeC.AgregaSeparador(Campos, "ALMACEN_INC_FECHA,TIPO_INCIDENCIA_NOMBRE, ALMACEN_INC_NO, ALMACEN_INC_SALDO,TIPO_ALMACEN_INC_NOMBRE,ALMACEN_INC_COMEN,ALMACEN_INC_EXTRAS,ALMACEN_INC_ID", ", ");
        string Qry = "SELECT   " + Campos +
                    " FROM         EC_PERSONAS, EC_ALMACEN_INC,EC_TIPO_INCIDENCIAS_R,EC_TIPO_INCIDENCIAS,EC_TIPO_ALMACEN_INC  " +
                    " WHERE    EC_PERSONAS.PERSONA_ID = EC_ALMACEN_INC.PERSONA_ID AND EC_ALMACEN_INC.TIPO_INCIDENCIA_R_ID= EC_TIPO_INCIDENCIAS_R.TIPO_INCIDENCIA_R_ID " +
                    " AND EC_TIPO_ALMACEN_INC.TIPO_ALMACEN_INC_ID =EC_ALMACEN_INC.TIPO_ALMACEN_INC_ID AND EC_TIPO_INCIDENCIAS_R.TIPO_INCIDENCIA_ID=EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_ID AND    (ALMACEN_INC_FECHA >= @FECHA_INICIAL@) AND  " +
                    " (ALMACEN_INC_FECHA < @FECHA_FINAL@) AND EC_PERSONAS." +
        ValidaAgrupacion(Persona_ID, Sesion.USUARIO_ID, Agrupacion, true);



        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal);

        if (Campos.Length > 0)
        {
            //OrdenarPor = CeC.AgregaSeparador(OrdenarPor, "ALMACEN_INC_ID", ",");
            Qry += " \n ORDER BY " + OrdenarPor;
        }

        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry);
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return null;
        return DS;
    }
    /// <summary>
    /// Obtiene las asistencias totales incluyendo saldos.
    /// </summary>
    /// <param name="MostrarAgrupacion"></param>
    /// <param name="MostrarEmpleado"></param>
    /// <param name="Persona_ID"></param>
    /// <param name="Agrupacion"></param>
    /// <param name="FechaInicial"></param>
    /// <param name="FechaInicial"></param>
    /// <param name="Valor"></param>
    /// <returns></returns>
    public static DataSet ObtenAsistenciaTotalesSaldos(bool MostrarAgrupacion, bool MostrarEmpleado, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, CeC_Sesion Sesion)
    {
        string Filtro = "SELECT     MAX(ALMACEN_INC_ID) AS ALMACEN_INC_ID FROM EC_ALMACEN_INC WHERE " +
            ValidaAgrupacion(Persona_ID, Sesion.USUARIO_ID, Agrupacion, true) + " GROUP BY PERSONA_ID";
        string Campos = "";
        string CamposTot = "";
        string OrdenarPor = "";
        if (MostrarAgrupacion)
        {
            Campos = "AGRUPACION_NOMBRE";
            OrdenarPor = "AGRUPACION_NOMBRE";
        }
        if (MostrarEmpleado)
        {
            if (Campos.Length > 0)
            {
                Campos += ", ";
                OrdenarPor += ", ";
            }
            Campos += "PERSONA_LINK_ID, PERSONA_NOMBRE";
            OrdenarPor += "PERSONA_NOMBRE, PERSONA_LINK_ID";
        }

        Campos = CeC.AgregaSeparador(Campos, "TIPO_INCIDENCIA_NOMBRE", ", ");
        Campos = CeC.AgregaSeparador(Campos, "sum(ALMACEN_INC_SALDO) as ALMACEN_INC_SALDO", ", ");
        string Qry = "SELECT   " + Campos +
            " FROM         EC_PERSONAS INNER JOIN " +
            "                      EC_ALMACEN_INC ON EC_PERSONAS.PERSONA_ID = EC_ALMACEN_INC.PERSONA_ID " +
            " WHERE        ALMACEN_INC_ID in (" + Filtro + ") ";

        Qry = "SELECT   " + Campos +
" FROM         EC_PERSONAS, EC_ALMACEN_INC, EC_TIPO_INCIDENCIAS,EC_TIPO_INCIDENCIAS_R WHERE EC_PERSONAS.PERSONA_ID = EC_ALMACEN_INC.PERSONA_ID \n" +
" AND EC_ALMACEN_INC.TIPO_INCIDENCIA_R_ID = EC_TIPO_INCIDENCIAS_R.TIPO_INCIDENCIA_R_ID AND EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_ID = EC_TIPO_INCIDENCIAS_R.TIPO_INCIDENCIA_ID \n" +
" AND  ALMACEN_INC_ID in (SELECT MAX(ALMACEN_INC_ID) AS ALMACEN_INC_ID FROM EC_ALMACEN_INC \n" +
" GROUP BY PERSONA_ID, TIPO_INCIDENCIA_R_ID) \n" +
" AND  EC_PERSONAS." + ValidaAgrupacion(Persona_ID, Sesion.USUARIO_ID, Agrupacion, true) +
" ";
        OrdenarPor = CeC.AgregaSeparador(OrdenarPor, "TIPO_INCIDENCIA_NOMBRE", ", ");
        if (OrdenarPor.Length > 0)
        {
            //OrdenarPor = CeC.AgregaSeparador(OrdenarPor, "ALMACEN_INC_ID", ",");
            Qry += "\n GROUP BY " + OrdenarPor + "\n ORDER BY " + OrdenarPor;

        }

        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry);
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return null;
        return DS;
    }
    /// <summary>
    /// Obtiene las asistencias totales para personas.
    /// </summary>
    /// <param name="MostrarAgrupacion"></param>
    /// <param name="MostrarEmpleado"></param>
    /// <param name="Totales"></param>
    /// <param name="Historial"></param>
    /// <param name="Saldos"></param>
    /// <param name="Faltas"></param>
    /// <param name="Persona_ID"></param>
    /// <param name="Agrupacion"></param>
    /// <param name="FechaInicial"></param>
    /// <param name="FechaFinal"></param>
    /// <param name="Sesion"></param>
    /// <returns></returns>
    public static DataSet ObtenAsistenciaTotales(bool MostrarAgrupacion, bool MostrarEmpleado, bool Totales, bool Historial, bool Saldos, bool Faltas, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, CeC_Sesion Sesion)
    {
        if (Faltas)
            return ObtenAsistenciaTotalFaltas(MostrarAgrupacion, MostrarEmpleado, Persona_ID, Agrupacion, FechaInicial, FechaFinal, Sesion);
        if (Historial)
            return ObtenAsistenciaTotalesHistorial(MostrarAgrupacion, MostrarEmpleado, Persona_ID, Agrupacion, FechaInicial, FechaFinal, Sesion);
        if (Saldos)
            return ObtenAsistenciaTotalesSaldos(MostrarAgrupacion, MostrarEmpleado, Persona_ID, Agrupacion, FechaInicial, FechaFinal, Sesion);
        return ObtenAsistenciaTotales(MostrarAgrupacion, MostrarEmpleado, Persona_ID, Agrupacion, FechaInicial, FechaFinal, Sesion);
    }

    public static DataSet ObtenAsistenciaTotales(bool MostrarAgrupacion, bool MostrarEmpleado, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, CeC_Sesion Sesion)
    {
        string Campos = "";
        string CamposTot = "";
        string OrdenarPor = "";
        if (MostrarAgrupacion)
        {
            Campos = "AGRUPACION_NOMBRE";
            OrdenarPor = "AGRUPACION_NOMBRE";
        }
        if (MostrarEmpleado)
        {
            if (Campos.Length > 0)
            {
                Campos += ", ";
                OrdenarPor += ", ";
            }
            Campos += "PERSONA_ID, PERSONA_LINK_ID, PERSONA_NOMBRE";
            OrdenarPor += "PERSONA_NOMBRE, PERSONA_LINK_ID";
        }
        Campos = CeC.AgregaSeparador(Campos, "INCIDENCIA_NOMBRE", ", ");
        CamposTot = CeC.AgregaSeparador(Campos, "COUNT(*) as No", ", ");
        CamposTot = CeC.AgregaSeparador(CamposTot, "MAX(TIPO_INCIDENCIA_ID) as Saldo", ", ");
        string Qry = "SELECT   " + CamposTot +
                    " FROM         EC_V_ASISTENCIAS " +
                    " WHERE        (PERSONA_DIARIO_FECHA >= @FECHA_INICIAL@) AND  " +
                    " (PERSONA_DIARIO_FECHA < @FECHA_FINAL@) " +
        ValidaAgrupacion(Persona_ID, Sesion.USUARIO_ID, Agrupacion, false);

        if (Campos.Length > 0)
        {
            Qry += " GROUP BY " + Campos;
        }

        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal);

        if (Campos.Length > 0)
        {
            OrdenarPor = CeC.AgregaSeparador(OrdenarPor, "INCIDENCIA_NOMBRE", ",");
            Qry += " \n ORDER BY " + OrdenarPor;
        }

        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry);
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return null;
        int SuscripcionID = CeC_Usuarios.ObtenSuscripcionID(Sesion.USUARIO_ID);
       /* DataSet DSInci = Cec_Incidencias.ObtenTiposIncidencias(SuscripcionID, false, true);
        DataTable DT = DS.Tables[0];
        if (DSInci != null && DSInci.Tables.Count > 0 && DSInci.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow DRInci in DSInci.Tables[0].Rows)
            {
                bool Agregar = true;
                string Incidencia = CeC.Convierte2String(DRInci["TIPO_INCIDENCIA_NOMBRE"]);

                foreach (DataRow DRT in DT.Rows)
                {
                    string IncNombre = CeC.Convierte2String(DRT["INCIDENCIA_NOMBRE"]);
                    if (IncNombre == Incidencia)
                    {
                        DRT["Saldo"] = -9999;
                        Agregar = false;
                        break;
                    }
                }
                if (Agregar)
                {
                    DataRow DR = DT.NewRow();
                    DR["INCIDENCIA_NOMBRE"] = Incidencia;
                    DR["Saldo"] = -9999;
                    DT.Rows.Add(DR);
                }

            }
        }*/

        foreach (DataRow DR in DS.Tables[0].Rows)
        {
            int Saldo = CeC.Convierte2Int(DR["Saldo"]);
            if (Saldo > 0)
            {
                int iPersonaID = CeC.Convierte2Int(DR["PERSONA_ID"]);
                DR["Saldo"] = CeC_IncidenciasInventario.ObtenSaldoParche(iPersonaID, Saldo);
            }
        }

        return DS;
    }
    /// <summary>
    /// Muestra una gráfica con la Asistencia del Periodo seleccionado
    /// </summary>
    /// <param name="MostrarAgrupacion">Inica si se muestra la agrupación</param>
    /// <param name="MostrarEmpleado">Indica si se muestra el empleado</param>
    /// <param name="Persona_ID">ID de la Persona</param>
    /// <param name="Agrupacion">Nombre de la Agrupación</param>
    /// <param name="FechaInicial">Fecha de Inicio del Periodo seleccionado</param>
    /// <param name="FechaFinal">Fecha de Fin del Periodo seleccionado</param>
    /// <param name="Usuario_ID">ID del Usuario logueado</param>
    /// <param name="Sesion">Variable de Sesion</param>
    /// <returns>DataSet con los datos de la Asistencia para mostras en la gráfica</returns>
    public static DataSet ObtenAsistenciaGrafica(bool MostrarAgrupacion, bool MostrarEmpleado, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, CeC_Sesion Sesion)
    {
        return ObtenAsistenciaGrafica(MostrarAgrupacion, MostrarEmpleado, false, Persona_ID, Agrupacion, FechaInicial, FechaFinal, Sesion);
    }
    /// <summary>
    /// Muestra una gráfica con la Asistencia del Periodo seleccionado
    /// </summary>
    /// <param name="MostrarAgrupacion">Inica si se muestra la agrupación</param>
    /// <param name="MostrarEmpleado">Indica si se muestra el empleado</param>
    /// <param name="MostrarFecha">Indica si se muestra la fecha</param>
    /// <param name="Persona_ID">ID de la Persona</param>
    /// <param name="Agrupacion">Nombre de la Agrupación</param>
    /// <param name="FechaInicial">Fecha de Inicio del Periodo seleccionado</param>
    /// <param name="FechaFinal">Fecha de Fin del Periodo seleccionado</param>
    /// <param name="Usuario_ID">ID del Usuario logueado</param>
    /// <param name="Sesion">Variable de Sesion</param>
    /// <returns>DataSet con los datos de la Asistencia para mostras en la gráfica</returns>
    public static DataSet ObtenAsistenciaGrafica(bool MostrarAgrupacion, bool MostrarEmpleado, bool MostrarFecha, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, CeC_Sesion Sesion)
    {
        // SUM(CASE WHEN field1 = 0 AND field2 = 0 THEN 1 ELSE 0 END) AS Expr1
        string[] Faltas = ObtenIDFaltas(null).Split(new char[] { ',' });
        string[] Asistencias = ObtenIDAsistencias(null).Split(new char[] { ',' });
        string[] Retardos = ObtenIDRetardos(null).Split(new char[] { ',' });
        string CaseAsistencias = ObtenAsistenciaGraficaObtenCadena("TIPO_INC_SIS_ID", Asistencias) + " AS TotalAsistencias";
        string CaseRetardos = ObtenAsistenciaGraficaObtenCadena("TIPO_INC_SIS_ID", Retardos) + " AS TotalRetardos";
        string CaseFaltas = ObtenAsistenciaGraficaObtenCadena("TIPO_INC_SIS_ID", Faltas) + " AS TotalFaltas";
        string CaseIncidencias = " SUM(CASE WHEN INCIDENCIA_ID > 0 THEN 1 ELSE 0 END)" + " AS TotalIncidencias";
        string TotalFestivos = "SUM(CASE WHEN TIPO_INC_SIS_ID = 11 THEN 1 ELSE 0 END) AS TotalDiasFestivos";
        string TotalDescansados = "SUM(CASE WHEN TIPO_INC_SIS_ID = 10 THEN 1 ELSE 0 END) AS TotalDiasDescanso";
        //int TotalFestivos = ObtenTotalDiasFestivos(Persona_ID, Agrupacion, FechaInicial, FechaFinal, Usuario_ID);
        //int TotalDescansados = ObtenTotalDiasDescansados(Persona_ID, Agrupacion, FechaInicial, FechaFinal, Usuario_ID);
        string Campos = "";
        string CamposMos = "";
        string OrdenarPor = "";
        if (MostrarAgrupacion)
        {
            Campos = "AGRUPACION_NOMBRE";
            OrdenarPor = "AGRUPACION_NOMBRE";
        }
        if (MostrarEmpleado)
        {
            Campos = CeC.AgregaSeparador(Campos, "PERSONA_LINK_ID, PERSONA_NOMBRE", ",");
            OrdenarPor = CeC.AgregaSeparador(OrdenarPor, "PERSONA_NOMBRE, PERSONA_LINK_ID", ",");
        }
        if (MostrarFecha)
        {
            Campos = CeC.AgregaSeparador(Campos, "PERSONA_DIARIO_FECHA", ",");
            OrdenarPor = CeC.AgregaSeparador(OrdenarPor, "PERSONA_DIARIO_FECHA", ",");
        }
        if (Campos.Length > 0)
            CamposMos = Campos + ", ";
        else
            CamposMos = Campos;
        string Qry = "SELECT   " + CamposMos + CaseAsistencias + ", " + CaseRetardos + ", " + CaseFaltas + ", " + CaseIncidencias + ", " + TotalFestivos + ", " + TotalDescansados +
                        " FROM         EC_V_ASISTENCIAS " +
                        " WHERE        (PERSONA_DIARIO_FECHA >= @FECHA_INICIAL@) AND  " +
                        " (PERSONA_DIARIO_FECHA < @FECHA_FINAL@) " +
        ValidaAgrupacion(Persona_ID, Sesion.USUARIO_ID, Agrupacion, false);

        if (Campos.Length > 0)
        {
            Qry += " GROUP BY " + Campos;
        }

        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal);
        string P = " * 100 /( CASE WHEN (TotalAsistencias + TotalRetardos + TotalFaltas + TotalIncidencias) > 0 THEN  (TotalAsistencias + TotalRetardos + TotalFaltas + TotalIncidencias) ELSE 1 END) ";
        string Q = "* 100 /( CASE WHEN (TotalAsistencias + TotalRetardos + TotalFaltas + TotalIncidencias + TotalDiasFestivos) > 0 THEN  (TotalAsistencias + TotalRetardos + TotalFaltas + TotalIncidencias + TotalDiasFestivos) ELSE 1 END)";
        Qry = " SELECT " +
                    CamposMos +
                    " TotalAsistencias" +
                    P + " as PAsistencias , " +
                    " TotalRetardos" + P + " as PRetardos, " +
                    " TotalFaltas" + P + " as PFaltas, " +
                    " TotalIncidencias" + P + " as PIncidencias, " +
                    " TotalDiasFestivos " + Q + " as PDiasFestivos, " +
                    " TotalDiasDescanso " + Q + " as PDiasDescanso, " +
                    " TotalAsistencias, " +
                    " TotalRetardos, " +
                    " TotalFaltas, " +
                    " TotalIncidencias " + //", " +
            //" TotalDiasFestivos, " +
            //" TotalDiasDescanso "+
                " FROM (" + Qry + ")t";

        if (Campos.Length > 0)
        {
            Qry += " \n ORDER BY " + OrdenarPor;
        }
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }


    /// <summary>
    /// Obtiene asistencias por medio de las entradas y las salidas.
    /// </summary>
    /// <param name="Persona_ID"></param>
    /// <param name="Agrupacion"></param>
    /// <param name="FechaInicial"></param>
    /// <param name="FechaFinal"></param>
    /// <param name="Sesion"></param>
    /// <returns></returns>
    public static DataSet ObtenAsistenciaEntradaSalida(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, CeC_Sesion Sesion)
    {
        string Campos = "";
        string OrdenarPor = "";
        if (Persona_ID > 0)
        {
            Campos = "PERSONA_DIARIO_FECHA, ACCESO_E, ACCESO_S, INCIDENCIA_NOMBRE, TURNO, PERSONA_DIARIO_TT, PERSONA_DIARIO_TDE";
            OrdenarPor = "PERSONA_DIARIO_FECHA";
        }
        else
        {
            Campos = "PERSONA_LINK_ID, PERSONA_NOMBRE, PERSONA_DIARIO_FECHA, ACCESO_E, ACCESO_S, INCIDENCIA_NOMBRE, TURNO, PERSONA_DIARIO_TT, PERSONA_DIARIO_TDE";
            OrdenarPor = "PERSONA_NOMBRE, PERSONA_LINK_ID, PERSONA_DIARIO_FECHA";
        }
        return ObtenAsistenciaLineal(Persona_ID, Agrupacion, FechaInicial, FechaFinal, Campos, OrdenarPor, false, Sesion);

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Persona_ID">dejar en blanco si no se conoce la persona</param>
    /// <param name="Agrupacion">Dejar en blanco si no se conoce la agrupacion</param>
    /// <param name="FechaInicial"></param>
    /// <param name="FechaFinal"></param>
    /// <param name="Campos"></param>
    /// <param name="OrdenarPor"></param>
    /// <param name="SoloInasistencias"></param>
    /// <param name="Sesion"></param>
    /// <returns></returns>
    public static DataSet ObtenAsistenciaLineal(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, string Campos, string OrdenarPor, bool SoloInasistencias, CeC_Sesion Sesion)
    {
        bool SoloFaltas = false;
        bool SoloRetardos = false;

        if (SoloInasistencias)
        {
            SoloFaltas = SoloRetardos = true;
        }
        return ObtenAsistenciaLineal(Persona_ID, Agrupacion, FechaInicial, FechaFinal, Campos, OrdenarPor, SoloFaltas, SoloRetardos, Sesion);
    }
    /// <summary>
    /// Obtiene asistencia lineal dependiendo a si solo faltas o solo retardos.
    /// </summary>
    /// <param name="Persona_ID"></param>
    /// <param name="Agrupacion"></param>
    /// <param name="FechaInicial"></param>
    /// <param name="FechaFinal"></param>
    /// <param name="Campos"></param>
    /// <param name="OrdenarPor"></param>
    /// <param name="SoloFaltas"></param>
    /// <param name="SoloRetardos"></param>
    /// <param name="Sesion"></param>
    /// <returns></returns>
    public static DataSet ObtenAsistenciaLineal(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, string Campos, string OrdenarPor, bool SoloFaltas, bool SoloRetardos, CeC_Sesion Sesion)
    {
        string QryTemp = "";
        if (SoloFaltas)
            QryTemp += ObtenIDFaltas(null);

        if (SoloRetardos)
        {
            if (QryTemp.Length > 0)
                QryTemp += ", ";
            QryTemp += ObtenIDRetardos(null);
        }
        return ObtenAsistenciaLineal(Persona_ID, Agrupacion, FechaInicial, FechaFinal, Campos, OrdenarPor, QryTemp, "", Sesion);
    }
    /// <summary>
    /// Obtiene la asistencia lineal por ID de persona 
    /// </summary>
    /// <param name="Persona_ID"></param>
    /// <param name="Agrupacion"></param>
    /// <param name="FechaInicial"></param>
    /// <param name="FechaFinal"></param>
    /// <param name="Campos"></param>
    /// <param name="OrdenarPor"></param>
    /// <param name="TiposIncidenciasSistemaIDs"></param>
    /// <param name="TiposIncidenciasIDs"></param>
    /// <param name="Sesion"></param>
    /// <returns></returns>
    public static DataSet ObtenAsistenciaLineal(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, string Campos, string OrdenarPor, string TiposIncidenciasSistemaIDs, string TiposIncidenciasIDs, CeC_Sesion Sesion)
    {
        string QryInasistencias = "";
        if (TiposIncidenciasSistemaIDs != "")
        {
            QryInasistencias = " AND TIPO_INC_SIS_ID IN ( " + TiposIncidenciasSistemaIDs + ") AND INCIDENCIA_ID <= 0 ";
        }
        if (TiposIncidenciasIDs != "")
        {
            if (TiposIncidenciasSistemaIDs != "")
                QryInasistencias = " AND (TIPO_INC_SIS_ID IN ( " + TiposIncidenciasSistemaIDs + ") AND INCIDENCIA_ID <= 0 ) OR (TIPO_INCIDENCIA_ID IN ( " + TiposIncidenciasIDs + ")) ";
            else
                QryInasistencias += " AND TIPO_INCIDENCIA_ID IN ( " + TiposIncidenciasIDs + ") ";
        }

        string Qry = "SELECT     PERSONA_DIARIO_ID,  " + Campos + " " +
                        " FROM         EC_V_ASISTENCIAS " +
                        " WHERE        (PERSONA_DIARIO_FECHA >= @FECHA_INICIAL@) AND  " +
                        " (PERSONA_DIARIO_FECHA < @FECHA_FINAL@) " +
                        ValidaAgrupacion(Persona_ID, Sesion.USUARIO_ID, Agrupacion, false) +
                        QryInasistencias;

        if (OrdenarPor != null && OrdenarPor != "")
            Qry += " \n ORDER BY " + OrdenarPor;
        Qry = CeC_BD.AsignaParametro(Qry, "USUARIO_ID", Sesion.USUARIO_ID);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal);
        Qry = CeC_BD.AsignaParametro(Qry, "AGRUPACION_NOMBRE", Agrupacion + "%");

        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }
    /// <summary>
    /// Se encarga de obtener la asistencia de los empleados en forma lineal
    /// </summary>
    /// <param name="PERSONA_DIARIO_ID_INICIO">Id</param>
    /// <param name="PERSONA_DIARIO_ID_FIN">Id</param>
    /// <param name="Lang">Lenguage, "es" indica español</param>
    /// <param name="Usuario_ID">Id del usuario</param>
    /// <returns>La consulta</returns>
    public static DataSet ObtenAsistenciaLinealV5(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, string OrdenarPor, string TiposIncidenciasSistemaIDs, string TiposIncidenciasIDs, string Lang, int Usuario_ID)
    {

        string Campos = "PERSONA_DIARIO_ID AS ID, PERSONA_ID AS PID, PERSONA_LINK_ID AS PLID, PERSONA_NOMBRE AS NOM, AGRUPACION_NOMBRE AS AGR, " +
            "PERSONA_DIARIO_FECHA AS FECHA, " +
            "ACCESO_E AS E,  ACCESO_CS AS CS, ACCESO_CR AS CR, ACCESO_S AS S ";
        Campos += ", TURNO";
        Campos += ", INCIDENCIA_NOMBRE as INC, INCIDENCIA_ABR AS ABR";
        Campos += ", TIPO_INC_C_SIS_NOMBRE as COMI";
        Campos += ", PERSONA_DIARIO_TT as TT, PERSONA_DIARIO_TDE AS TDE";
        ///Se mostrara Siempre el Tiempo de estancia del trabajador, falta validar que solo
        ///cuando enga habilitado la comida o la generación de entradas y salidas
        Campos += ", PERSONA_DIARIO_TE AS TE";
        Campos += ", PERSONA_DIARIO_TC AS TC";
        Campos += ", PERSONA_D_HE_SIS AS HES,  PERSONA_D_HE_CAL AS HEC,  PERSONA_D_HE_APL AS HEA";
        Campos += ", INCIDENCIA_COMENTARIO AS COME";
        Campos += ",TURNO_DIA_PHEX AS PHEX,TURNO_DIA_HAYCOMIDA AS PCOMI,INCIDENCIA_COLOR AS IC";
        string QryInasistencias = "";
        if (TiposIncidenciasSistemaIDs != "")
            QryInasistencias = CeC.AgregaSeparador(QryInasistencias, "(TIPO_INC_SIS_ID IN ( " + TiposIncidenciasSistemaIDs + ") AND INCIDENCIA_ID <= 0)", " OR ");

        if (TiposIncidenciasIDs != "")
            QryInasistencias = CeC.AgregaSeparador(QryInasistencias, "(TIPO_INCIDENCIA_ID IN ( " + TiposIncidenciasIDs + "))", " OR ");


        string Qry = "SELECT      " + Campos + " \n" +
            " FROM         EC_V_ASISTENCIAS_V5 " +
            " WHERE        (PERSONA_DIARIO_FECHA >= @FECHA_INICIAL@) AND  " +
            " (PERSONA_DIARIO_FECHA < @FECHA_FINAL@) \n" +
            ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, false);
        if (QryInasistencias != "")
            Qry += "\n AND (" + QryInasistencias + ")";

        if (OrdenarPor != null && OrdenarPor != "")
            Qry += "\n" + " \n ORDER BY " + OrdenarPor;

        Qry = CeC_BD.AsignaParametro(Qry, "USUARIO_ID", Usuario_ID);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal);
        Qry = CeC_BD.AsignaParametro(Qry, "AGRUPACION_NOMBRE", Agrupacion + "%");

        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }


    public static DataSet ObtenAsistenciaLinealN(int PERSONA_DIARIO_ID_INICIO, int PERSONA_DIARIO_ID_FIN, string Lang, int Usuario_ID)
    {
        string Campos = "ACCESO_E AS E,  ACCESO_CS AS CS, ACCESO_CR AS CR, ACCESO_S AS S";
        Campos += ", TURNO";
        Campos += ", INCIDENCIA_NOMBRE as INC, INCIDENCIA_ABR AS ABR";
        Campos += ", TIPO_INC_C_SIS_NOMBRE as COMI";
        Campos += ", PERSONA_DIARIO_TT as TT, PERSONA_DIARIO_TDE AS TDE";
        ///Se mostrara Siempre el Tiempo de estancia del trabajador, falta validar que solo
        ///cuando enga habilitado la comida o la generación de entradas y salidas
        Campos += ", PERSONA_DIARIO_TE AS TE";
        Campos += ", PERSONA_DIARIO_TC AS TC";
        Campos += ", PERSONA_D_HE_SIS AS HES,  PERSONA_D_HE_CAL AS HEC,  PERSONA_D_HE_APL AS HEA";
        Campos += ", INCIDENCIA_COMENTARIO AS COME";
        Campos += ",TURNO_DIA_PHEX AS PHEX,TURNO_DIA_HAYCOMIDA AS PCOMI,INCIDENCIA_COLOR AS IC";
        string Qry = "SELECT     PERSONA_DIARIO_ID AS ID, PERSONA_DIARIO_FECHA AS FECHA, " + Campos + " " +
" FROM         EC_V_ASISTENCIAS_V5 " +
" WHERE        (PERSONA_DIARIO_ID >= @PERSONA_DIARIO_ID_INICIO@) AND  " +
" (PERSONA_DIARIO_ID <= @PERSONA_DIARIO_ID_FIN@) " +
            //ValidaAgrupacion(-1, Usuario_ID, "", false) +
" \n ORDER BY PERSONA_DIARIO_ID";
        Qry = CeC_BD.AsignaParametro(Qry, "USUARIO_ID", Usuario_ID);
        Qry = CeC_BD.AsignaParametro(Qry, "PERSONA_DIARIO_ID_INICIO", PERSONA_DIARIO_ID_INICIO);
        Qry = CeC_BD.AsignaParametro(Qry, "PERSONA_DIARIO_ID_FIN", PERSONA_DIARIO_ID_FIN);

        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }
    public static DataSet ObtenAsistencias_V5(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, string Campos, string OrdenarPor, string TiposIncidenciasSistemaIDs, string TiposIncidenciasIDs, CeC_Sesion Sesion)
    {

        string QryInasistencias = "";
        if (TiposIncidenciasSistemaIDs!= null && TiposIncidenciasSistemaIDs != "")
        {
            QryInasistencias = " AND TIPO_INC_SIS_ID IN ( " + TiposIncidenciasSistemaIDs + ") AND INCIDENCIA_ID <= 0 ";
        }
        if (TiposIncidenciasIDs != null && TiposIncidenciasIDs != "")
        {
            if (TiposIncidenciasSistemaIDs != null && TiposIncidenciasSistemaIDs != "")
                QryInasistencias = " AND (TIPO_INC_SIS_ID IN ( " + TiposIncidenciasSistemaIDs + ") AND INCIDENCIA_ID <= 0 ) OR (TIPO_INCIDENCIA_ID IN ( " + TiposIncidenciasIDs + ")) ";
            else
                QryInasistencias += " AND TIPO_INCIDENCIA_ID IN ( " + TiposIncidenciasIDs + ") ";
        }

        string Qry = "SELECT     " + Campos + " " +
                        " FROM         EC_V_ASISTENCIAS_V5  " +
                        " WHERE        (PERSONA_DIARIO_FECHA >= @FECHA_INICIAL@) AND  " +
                        " (PERSONA_DIARIO_FECHA < @FECHA_FINAL@) " +
                        ValidaAgrupacion(Persona_ID, Sesion.USUARIO_ID, Agrupacion, false) +
                        QryInasistencias;
        if (OrdenarPor != null && OrdenarPor != "")
        {
            Qry += " \n ORDER BY " + OrdenarPor;
        }
        Qry = CeC_BD.AsignaParametro(Qry, "USUARIO_ID", Sesion.USUARIO_ID);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal);
        Qry = CeC_BD.AsignaParametro(Qry, "AGRUPACION_NOMBRE", Agrupacion + "%");

        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }




    public static string ObtenVistaAccesos(DateTime FechaInicial, DateTime FechaFinal, int TerminalID)
    {
        string Vista = "";

        Vista = "(SELECT ACCESO_ID " +
                          ",PERSONA_ID " +
                          ",SUSCRIPCION_ID " +
                          ",PERSONA_LINK_ID " +
                          ",PERSONA_NOMBRE " +
                          ",TERMINAL_NOMBRE " +
                          ",TIPO_ACCESO_NOMBRE " +
                          ",ACCESO_FECHAHORA " +
                          ",ACCESO_CALCULADO " +
                          ",AGRUPACION_NOMBRE " +
                          "FROM (" +
            "SELECT     ACCESOS.ACCESO_ID, EC_PERSONAS.PERSONA_ID, EC_PERSONAS.SUSCRIPCION_ID, EC_PERSONAS.PERSONA_LINK_ID,  " +
            "EC_PERSONAS.PERSONA_NOMBRE, EC_TERMINALES.TERMINAL_NOMBRE, EC_TIPO_ACCESOS.TIPO_ACCESO_NOMBRE,  " +
            "ACCESOS.ACCESO_FECHAHORA, ACCESOS.ACCESO_CALCULADO, EC_PERSONAS.AGRUPACION_NOMBRE " +
            "FROM         EC_PERSONAS INNER JOIN " +
            "(SELECT     ACCESO_ID, PERSONA_ID, TERMINAL_ID, ACCESO_FECHAHORA, TIPO_ACCESO_ID, ACCESO_CALCULADO " +
            "FROM          EC_ACCESOS " +
            " WHERE     (ACCESO_FECHAHORA >= @FECHA_INICIAL@) AND  " +
            " (ACCESO_FECHAHORA < @FECHA_FINAL@) ";
        if (TerminalID > 0)
            Vista += " AND TERMINAL_ID = " + TerminalID;

        if (CeC_BD.EsOracle)
            Vista += ") ACCESOS ON  ";
        else
            Vista += ") AS ACCESOS ON  ";

        Vista += "EC_PERSONAS.PERSONA_ID = ACCESOS.PERSONA_ID INNER JOIN " +
                    "EC_TIPO_ACCESOS ON ACCESOS.TIPO_ACCESO_ID = EC_TIPO_ACCESOS.TIPO_ACCESO_ID INNER JOIN " +
                    "EC_TERMINALES ON ACCESOS.TERMINAL_ID = EC_TERMINALES.TERMINAL_ID " +
                    ")";
        Vista = CeC_BD.AsignaParametro(Vista, "FECHA_INICIAL", FechaInicial);
        Vista = CeC_BD.AsignaParametro(Vista, "FECHA_FINAL", FechaFinal);
        if (!CeC_BD.EsOracle)
            Vista += "ta";
        Vista += ")";
        if (!CeC_BD.EsOracle)
            Vista += "V_Accesos";
        return Vista;
    }
    public static DataSet ObtenAccesosTerminal(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int Usuario_ID, int TerminalID, CeC_Sesion Sesion)
    {
        string Campos = "";
        string OrdenarPor = "";
        if (Persona_ID > 0)
        {
            Campos = "ACCESO_FECHAHORA, TIPO_ACCESO_NOMBRE, TERMINAL_NOMBRE, ACCESO_CALCULADO";
            OrdenarPor = "ACCESO_FECHAHORA";
        }
        else
        {
            Campos = "PERSONA_LINK_ID, PERSONA_NOMBRE, ACCESO_FECHAHORA, TIPO_ACCESO_NOMBRE, TERMINAL_NOMBRE, ACCESO_CALCULADO";
            OrdenarPor = "ACCESO_FECHAHORA";
        }

        string Qry = " SELECT     ACCESO_ID,  " + Campos + " " +
            //                        " FROM  EC_V_ACCESOS " +
            //        " WHERE     (ACCESO_FECHAHORA >= @FECHA_INICIAL@) AND  " +
            //        " (ACCESO_FECHAHORA < @FECHA_FINAL@) " +
                    " FROM " + ObtenVistaAccesos(FechaInicial, FechaFinal, TerminalID) +
                    " WHERE 1=1 " +
        ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, false) +
                    " \n ORDER BY " + OrdenarPor;
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal);
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }
    /// <summary>
    /// El campo llave para accesos es ACCESO_ID
    /// </summary>
    /// <param name="Persona_ID">dejar en blanco si no se conoce la persona</param>
    /// <param name="Agrupacion">Dejar en blanco si no se conoce la agrupacion</param>
    /// <param name="FechaInicial"></param>
    /// <param name="FechaFinal"></param>
    /// <param name="Usuario_ID"></param>
    /// <returns></returns>
    public static DataSet ObtenAccesos(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int Usuario_ID, CeC_Sesion Sesion)
    {
        string Campos = "";
        string OrdenarPor = "";
        if (Persona_ID > 0)
        {
            Campos = "ACCESO_FECHAHORA, TIPO_ACCESO_NOMBRE, TERMINAL_NOMBRE, ACCESO_CALCULADO";
            OrdenarPor = "ACCESO_FECHAHORA";
        }
        else
        {
            Campos = "PERSONA_LINK_ID, PERSONA_NOMBRE, ACCESO_FECHAHORA, TIPO_ACCESO_NOMBRE, TERMINAL_NOMBRE, ACCESO_CALCULADO";
            OrdenarPor = "ACCESO_FECHAHORA";
        }

        string Qry = " SELECT     ACCESO_ID,  " + Campos + " " +
            //                        " FROM  EC_V_ACCESOS " +
            //        " WHERE     (ACCESO_FECHAHORA >= @FECHA_INICIAL@) AND  " +
            //        " (ACCESO_FECHAHORA < @FECHA_FINAL@) " +
            // Pasamos -1 para obtener todo el listado de terminales y no solo la que indiquemos
                    " FROM " + ObtenVistaAccesos(FechaInicial, FechaFinal, -1) +
                    " WHERE 1=1 " +
        ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, false) +
                    " \n ORDER BY " + OrdenarPor;
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal);
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }

    public static DataSet ObtenAsistenciaES(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, CeC_Sesion Sesion)
    {
        string Campos = "";
        string OrdenarPor = "";
        if (Persona_ID > 0)
        {
            Campos = "PERSONA_DIARIO_FECHA, ACCESO_E_ES, ACCESO_S_ES, PERSONA_ES_TE";
            OrdenarPor = "PERSONA_ES_ORD";
        }
        else
        {
            Campos = "PERSONA_LINK_ID, PERSONA_NOMBRE, PERSONA_DIARIO_FECHA, ACCESO_E_ES, ACCESO_S_ES, PERSONA_ES_TE";
            OrdenarPor = "PERSONA_NOMBRE, PERSONA_LINK_ID, PERSONA_ES_ORD";
        }

        string Qry = " SELECT     PERSONA_ES_ID,  " + Campos + " " +
                        " FROM EC_V_ASISTENCIAS_ES " +
                        " WHERE        (PERSONA_DIARIO_FECHA >= @FECHA_INICIAL@) AND  " +
                        " (PERSONA_DIARIO_FECHA < @FECHA_FINAL@) " +
        ValidaAgrupacion(Persona_ID, Sesion.USUARIO_ID, Agrupacion, false) +
        " \n ORDER BY " + OrdenarPor;
        Qry = CeC_BD.AsignaParametro(Qry, "USUARIO_ID", Sesion.USUARIO_ID);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal);
        Qry = CeC_BD.AsignaParametro(Qry, "AGRUPACION_NOMBRE", Agrupacion + "%");
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }

    public static int ObtenPersonaDiarioID(int PersonaID, DateTime Persona_Fecha)
    {
        return CeC_BD.EjecutaEscalarInt("Select PERSONA_DIARIO_ID From EC_PERSONAS_DIARIO WHERE PERSONA_ID = " + PersonaID + " AND PERSONA_DIARIO_FECHA = " + CeC_BD.SqlFecha(Persona_Fecha));
    }

    public static DataSet ObtenDiasFestivos(int Usuario_ID, bool MostrarBorrados)
    {
        string Borrado = "";
        if (!MostrarBorrados)
            Borrado = " AND (DIA_FESTIVO_BORRADO = 0)";

        string ADD = " DIA_FESTIVO_ID IN (";
        ADD += " SELECT     EC_AUTONUM.AUTONUM_TABLA_ID AS DIA_FESTIVO_ID " +
                "FROM         EC_AUTONUM INNER JOIN " +
                " EC_PERMISOS_SUSCRIP ON EC_AUTONUM.SUSCRIPCION_ID = EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID" +
                " WHERE     (EC_AUTONUM.AUTONUM_TABLA = 'EC_DIAS_FESTIVOS') AND (EC_PERMISOS_SUSCRIP.USUARIO_ID = " + Usuario_ID + "))";

        return (DataSet)
            CeC_BD.EjecutaDataSet(
                @"SELECT DIA_FESTIVO_ID, DIA_FESTIVO_NOMBRE, DIA_FESTIVO_FECHA, DIA_FESTIVO_BORRADO FROM EC_DIAS_FESTIVOS WHERE " + ADD + Borrado);
    }
    public static System.Drawing.Color ObtenColor(string Abr)
    {
        if (Abr == "")
            return System.Drawing.Color.Empty;
        if (Abr == "D")
            return (Color.Gray);
        else
            if (Abr[0] != 'A' && Abr != "FD" && Abr != "D")
                if (Abr[0] == 'F')
                    return (System.Drawing.Color.Red);
                else
                    if (Abr[0] == 'R')
                        return (System.Drawing.Color.Yellow);
                    else
                        return (System.Drawing.Color.Green);
        return System.Drawing.Color.Empty;
    }
    public static List<DateTime> ObtenFechasList(string PersonasDiarioIds)
    {
        return eClockBase.CeC.PersonasDiarioIDs2Fechas(PersonasDiarioIds);
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet("SELECT PERSONA_DIARIO_FECHA FROM EC_PERSONAS_DIARIO,EC_PERSONAS WHERE EC_PERSONAS_DIARIO.PERSONA_ID = EC_PERSONAS.PERSONA_ID AND PERSONA_DIARIO_ID IN (" + PersonasDiarioIds + ")");
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return null;
        List<DateTime> Fechas = new List<DateTime>();
        foreach (DataRow DR in DS.Tables[0].Rows)
        {

            DateTime Fecha = CeC.Convierte2DateTime(DR["PERSONA_DIARIO_FECHA"]);
            Fechas.Add(Fecha);
        }
        return Fechas;
    }
    public static string ObtenFechas(string PersonasDiarioIds)
    {

        return eClockBase.CeC.ObtenDiasTexto(eClockBase.CeC.PersonasDiarioIDs2Fechas(PersonasDiarioIds));

        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet("SELECT PERSONA_LINK_ID, PERSONA_DIARIO_FECHA FROM EC_PERSONAS_DIARIO,EC_PERSONAS WHERE EC_PERSONAS_DIARIO.PERSONA_ID = EC_PERSONAS.PERSONA_ID AND PERSONA_DIARIO_ID IN (" + PersonasDiarioIds + ")");
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return "";
        string Ret = "";
        DateTime FechaDesde = CeC_BD.FechaNula;

        DateTime FechaHasta = CeC_BD.FechaNula;
        int PersonaLinkIDAnterior = 0;
        foreach (DataRow DR in DS.Tables[0].Rows)
        {
            int PersonaLinkID = CeC.Convierte2Int(DR["PERSONA_LINK_ID"]);
            DateTime Fecha = CeC.Convierte2DateTime(DR["PERSONA_DIARIO_FECHA"]);
            if (PersonaLinkID != PersonaLinkIDAnterior || FechaHasta.AddDays(1) != Fecha)
            {
                Ret = CeC.AgregaSeparador(Ret, FechaDesde.ToShortDateString() + "-" + FechaHasta.ToShortDateString(), ", ");
                FechaDesde = FechaHasta = Fecha;
                PersonaLinkIDAnterior = PersonaLinkID;
            }
            FechaHasta = Fecha;
        }
        Ret = CeC.AgregaSeparador(Ret, PersonaLinkIDAnterior.ToString() + " " + FechaDesde.ToShortDateString() + "-" + FechaHasta.ToShortDateString(), ", ");
        return Ret;

    }
    public static string ObtenTexto(string PersonasDiario)
    {
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet("SELECT EC_PERSONAS.PERSONA_LINK_ID,PERSONA_NOMBRE,FECHA_INGRESO, PERSONA_DIARIO_FECHA FROM EC_PERSONAS_DIARIO,EC_PERSONAS,EC_PERSONAS_DATOS " +
            "WHERE EC_PERSONAS_DATOS.PERSONA_ID = EC_PERSONAS.PERSONA_ID AND EC_PERSONAS_DIARIO.PERSONA_ID = EC_PERSONAS.PERSONA_ID AND PERSONA_DIARIO_ID IN (" + PersonasDiario + ")");
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return "";
        string Ret = "";
        DateTime FechaDesde = CeC_BD.FechaNula;

        DateTime FechaHasta = CeC_BD.FechaNula;
        int PersonaLinkIDAnterior = 0;
        string PersonaNombre = "";
        DateTime FechaIngreso = CeC_BD.FechaNula;
        foreach (DataRow DR in DS.Tables[0].Rows)
        {
            int PersonaLinkID = CeC.Convierte2Int(DR["PERSONA_LINK_ID"]);
            DateTime Fecha = CeC.Convierte2DateTime(DR["PERSONA_DIARIO_FECHA"]);
            if (PersonaLinkID != PersonaLinkIDAnterior || FechaHasta.AddDays(1) != Fecha)
            {
                if (PersonaLinkIDAnterior != 0)
                    Ret = CeC.AgregaSeparador(Ret, PersonaNombre + "(" + PersonaLinkIDAnterior.ToString() + ") F, Ingreso = " + FechaIngreso.ToShortDateString() + " Solicitud de " + FechaDesde.ToShortDateString() + "-" + FechaHasta.ToShortDateString(), "<br>");
                FechaDesde = FechaHasta = Fecha;
                PersonaLinkIDAnterior = PersonaLinkID;
                FechaIngreso = CeC.Convierte2DateTime(DR["FECHA_INGRESO"]);
                PersonaNombre = CeC.Convierte2String(DR["PERSONA_NOMBRE"]);

            }
            FechaHasta = Fecha;
        }
        Ret = CeC.AgregaSeparador(Ret, PersonaNombre + "(" + PersonaLinkIDAnterior.ToString() + ") F, Ingreso = " + FechaIngreso.ToShortDateString() + " Solicitud de " + FechaDesde.ToShortDateString() + "-" + FechaHasta.ToShortDateString(), "<br>");
        return Ret;
    }
    public static string ObtenPersonasIDs(string PersonasDiarioIDs)
    {
        return eClockBase.CeC.PersonasDiarioIDs2PersonaIDs(PersonasDiarioIDs);
    }

    public static bool AgregaFavorito(int UsuarioID, string Favorito)
    {
        if (UsuarioID < 0)
            return false;
        CeC_Config Cfg = new CeC_Config(UsuarioID);
        string AFav = Cfg.AsistenciaFavoritos + Favorito + "|";
        Cfg.AsistenciaFavoritos = AFav;
        return true;
    }

    /// <summary>
    /// Obtiene la ultima checada del dia validada como Hora de Entrada, Salida a Comer, Regreso de comer o Salida Final,
    /// si no tiene ninguna traerá la hora supuesta de entrada
    /// </summary>
    /// <param name="Persona_Diario_ID"></param>
    /// <returns></returns>
    public static DateTime ObtenUltimaChecada(int Persona_Diario_ID, CeC_Sesion Sesion)
    {
        CeC_Asistencias Asistenca = new CeC_Asistencias(Persona_Diario_ID, Sesion);
        int AccesoID = 0;
        if (Asistenca.ACCESO_E_ID > 0)
            AccesoID = Asistenca.ACCESO_E_ID;
        if (Asistenca.ACCESO_CS_ID > 0)
            AccesoID = Asistenca.ACCESO_CS_ID;
        if (Asistenca.ACCESO_CR_ID > 0)
            AccesoID = Asistenca.ACCESO_CR_ID;
        if (Asistenca.ACCESO_S_ID > 0)
            AccesoID = Asistenca.ACCESO_S_ID;
        DateTime UltimaChecada = CeC_BD.FechaNula;
        DateTime HoraEntrada = ObtenHoraEntrada(Persona_Diario_ID);
        if (AccesoID > 0)
        {
            UltimaChecada = CeC_Accesos.ObtenFechaHora(AccesoID);
            if (UltimaChecada < HoraEntrada)
                return HoraEntrada;
        }
        else
            UltimaChecada = HoraEntrada;
        return UltimaChecada;
    }
    /// <summary>
    /// Obtiene la ultima checada del dia validada como Hora de Entrada, Salida a Comer, Regreso de comer o Salida Final,
    /// si no tiene ninguna traerá la hora supuesta de entrada
    /// Si la primera checada es posterior al horario, entonces regresa la hora de salida
    /// </summary>
    /// <param name="Persona_Diario_ID"></param>
    /// <returns></returns>
    public static DateTime ObtenPrimeraChecada(int Persona_Diario_ID, CeC_Sesion Sesion)
    {
        CeC_Asistencias Asistenca = new CeC_Asistencias(Persona_Diario_ID, Sesion);
        int AccesoID = 0;
        if (Asistenca.ACCESO_S_ID > 0)
            AccesoID = Asistenca.ACCESO_S_ID;
        if (Asistenca.ACCESO_CR_ID > 0)
            AccesoID = Asistenca.ACCESO_CR_ID;
        if (Asistenca.ACCESO_CS_ID > 0)
            AccesoID = Asistenca.ACCESO_CS_ID;

        if (Asistenca.ACCESO_E_ID > 0)
            AccesoID = Asistenca.ACCESO_E_ID;
        DateTime PPrimeraChecada = CeC_BD.FechaNula;
        DateTime HoraSalida = ObtenHoraSalida(Persona_Diario_ID);
        if (AccesoID > 0)
        {
            PPrimeraChecada = CeC_Accesos.ObtenFechaHora(AccesoID);
            if (PPrimeraChecada > HoraSalida)
                return HoraSalida;

        }
        else
            PPrimeraChecada = HoraSalida;
        return PPrimeraChecada;
    }

    /// <summary>
    /// Hora en la que el Empleado entro a trabajar ese día
    /// </summary>
    /// <param name="Persona_Diario_ID"></param>
    /// <returns></returns>
    public static DateTime ObtenHoraEntradaDia(int Persona_Diario_ID)
    {
        DateTime Hora = CeC_BD.FechaNula;
        int ACCESO_ID = CeC_BD.EjecutaEscalarInt("SELECT ACCESO_E_ID FROM EC_PERSONAS_DIARIO INNER JOIN EC_TURNOS_DIA ON EC_PERSONAS_DIARIO.TURNO_DIA_ID = EC_TURNOS_DIA.TURNO_DIA_ID WHERE     (EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID = " + Persona_Diario_ID + ")");
        if (ACCESO_ID < 0)
            return CeC_BD.FechaNula;
        else
            return Hora = CeC_BD.EjecutaEscalarDateTime(" SELECT ACCESO_FECHAHORA FROM EC_ACCESOS WHERE ACCESO_ID = " + ACCESO_ID);
    }
    /// <summary>
    /// Hora en la que el empleado salio de trabajar ese día
    /// </summary>
    /// <param name="Persona_Diario_ID"></param>
    /// <returns></returns>
    public static DateTime ObtenHoraSalidaDia(int Persona_Diario_ID)
    {
        DateTime Hora = CeC_BD.FechaNula;
        int ACCESO_ID = CeC_BD.EjecutaEscalarInt("SELECT ACCESO_S_ID FROM EC_PERSONAS_DIARIO INNER JOIN EC_TURNOS_DIA ON EC_PERSONAS_DIARIO.TURNO_DIA_ID = EC_TURNOS_DIA.TURNO_DIA_ID WHERE     (EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID = " + Persona_Diario_ID + ")");
        if (ACCESO_ID < 0)
            return CeC_BD.FechaNula;
        else
            return Hora = CeC_BD.EjecutaEscalarDateTime(" SELECT ACCESO_FECHAHORA FROM EC_ACCESOS WHERE ACCESO_ID = " + ACCESO_ID);
    }
    /// <summary>
    /// Hora a la que deberá Entrar el empleado en dicho día
    /// </summary>
    /// <param name="Persona_Diario_ID"></param>
    /// <returns></returns>
    public static DateTime ObtenHoraEntrada(int Persona_Diario_ID)
    {
        DateTime PersonaDiarioFecha = CeC_BD.EjecutaEscalarDateTime("SELECT PERSONA_DIARIO_FECHA FROM EC_PERSONAS_DIARIO WHERE PERSONA_DIARIO_ID =" + Persona_Diario_ID);
        if (PersonaDiarioFecha == CeC_BD.FechaNula)
            return CeC_BD.FechaNula;
        string Qry = "SELECT EC_TURNOS_DIA.TURNO_DIA_HE " +
                        "FROM EC_PERSONAS_DIARIO INNER JOIN " +
                        "EC_TURNOS_DIA ON EC_PERSONAS_DIARIO.TURNO_DIA_ID = EC_TURNOS_DIA.TURNO_DIA_ID " +
                        "WHERE     (EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID = " + Persona_Diario_ID + ")";
        DateTime Hora = CeC_BD.EjecutaEscalarDateTime(Qry);
        return PersonaDiarioFecha + CeC_BD.DateTime2TimeSpan(Hora);
    }
    /// <summary>
    /// Hora a la que deberá salir el empleado en dicho día
    /// </summary>
    /// <param name="Persona_Diario_ID"></param>
    /// <returns></returns>
    public static DateTime ObtenHoraSalida(int Persona_Diario_ID)
    {
        DateTime PersonaDiarioFecha = CeC_BD.EjecutaEscalarDateTime("SELECT PERSONA_DIARIO_FECHA FROM EC_PERSONAS_DIARIO WHERE PERSONA_DIARIO_ID =" + Persona_Diario_ID);
        if (PersonaDiarioFecha == CeC_BD.FechaNula)
            return CeC_BD.FechaNula;
        string Qry = "SELECT EC_TURNOS_DIA.TURNO_DIA_HS " +
                        "FROM EC_PERSONAS_DIARIO INNER JOIN " +
                        "EC_TURNOS_DIA ON EC_PERSONAS_DIARIO.TURNO_DIA_ID = EC_TURNOS_DIA.TURNO_DIA_ID " +
                        "WHERE     (EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID = " + Persona_Diario_ID + ")";
        DateTime Hora = CeC_BD.EjecutaEscalarDateTime(Qry);
        return PersonaDiarioFecha + CeC_BD.DateTime2TimeSpan(Hora);
    }

    public static DataSet ObtenAsistenciaTotalFaltas(bool MostrarAgrupacion, bool MostrarEmpleado, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, CeC_Sesion Sesion)
    {
        string Campos = "";

        string OrdenarPor = "";
        if (MostrarAgrupacion)
        {
            Campos = "AGRUPACION_NOMBRE";
            OrdenarPor = "AGRUPACION_NOMBRE";
        }
        if (MostrarEmpleado)
        {
            if (Campos.Length > 0)
            {
                Campos += ", ";
                OrdenarPor += ", ";
            }
            Campos += "PERSONA_LINK_ID, PERSONA_NOMBRE";
            OrdenarPor += "PERSONA_NOMBRE, PERSONA_LINK_ID";
        }

        string Qry = System.IO.File.ReadAllText(HttpRuntime.AppDomainAppPath + "eC_ReporteFaltas.sql");
        if (OrdenarPor.Length > 0)
            Qry += " \n ORDER BY " + OrdenarPor;
        if (Campos != "")
            Campos += ",";
        Qry = Qry.Replace("@CAMPOS_EXTRAS@", Campos);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal);
        Qry = Qry.Replace("@FILTRO_PERSONAS@", ValidaAgrupacion(Persona_ID, Sesion.USUARIO_ID, Agrupacion, false));

        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }
    /// <summary>
    /// Limpia el Ultimo Reporte del Qry que se ejecuto para recargar horarios y justificaciones.
    /// </summary>
    /// <param name="Sesion"></param>
    public static void LimpiaUltimoReporteQryHash(CeC_Sesion Sesion)
    {
        Sesion.UltimoReporteQryHash = "";
    }

    public static eClockBase.Modelos.Asistencias.Model_Tiempos ObtenTiempos(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int Usuario_ID)
    {
        DataSet DS =
         CeC_BD.EjecutaDataSet("SELECT " +
            " SUM(" + QryCampoSegundos("PERSONA_DIARIO_TES") + ") AS SegundosEsperados \n" +
            ", SUM(" + QryCampoSegundos("PERSONA_DIARIO_TE") + ") AS SegundosEstancia \n" +
            ", SUM(" + QryCampoSegundos("PERSONA_DIARIO_TT") + ") AS SegundosTrabajados \n" +
            ", SUM(" + QryCampoSegundos("PERSONA_DIARIO_TC") + ") AS SegundosComida \n" +
            ", SUM(" + QryCampoSegundos("PERSONA_DIARIO_TDE") + ") AS SegundosDeuda \n" +
            ", SUM(" + QryCampoSegundos("PERSONA_D_HE_SIS") + ") AS SegundosHESis \n" +
            ", SUM(" + QryCampoSegundos("PERSONA_D_HE_CAL") + ") AS SegundosHECal \n" +
            ", SUM(" + QryCampoSegundos("PERSONA_D_HE_APL") + ") AS SegundosHeApl \n" +
            ", SUM(PERSONA_D_HE_SIMPLE) AS HorasSimples \n" +
            ", SUM(PERSONA_D_HE_DOBLE) AS HorasDobles \n" +
            ", SUM(PERSONA_D_HE_TRIPLE) AS HorasTriples \n" +
        " FROM EC_V_ASISTENCIAS_V5 WHERE " +
    " PERSONA_DIARIO_FECHA >= " + CeC_BD.SqlFecha(FechaInicial) + " AND PERSONA_DIARIO_FECHA <" +
    CeC_BD.SqlFecha(FechaFinal) + " " +
    " \n AND " + ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, true));
        if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
        {
            DataRow Fila = DS.Tables[0].Rows[0];
            eClockBase.Modelos.Asistencias.Model_Tiempos Tiempo = new eClockBase.Modelos.Asistencias.Model_Tiempos(
                eClockBase.CeC.Convierte2Int(Fila["SegundosEsperados"]), 
                eClockBase.CeC.Convierte2Int(Fila["SegundosEstancia"]),
                eClockBase.CeC.Convierte2Int(Fila["SegundosTrabajados"]),
                eClockBase.CeC.Convierte2Int(Fila["SegundosComida"]),
                eClockBase.CeC.Convierte2Int(Fila["SegundosDeuda"]),
                eClockBase.CeC.Convierte2Int(Fila["SegundosHESis"]),
                eClockBase.CeC.Convierte2Int(Fila["SegundosHECal"]),
                eClockBase.CeC.Convierte2Int(Fila["SegundosHeApl"]),
                eClockBase.CeC.Convierte2Decimal(Fila["HorasSimples"]),
                eClockBase.CeC.Convierte2Decimal(Fila["HorasDobles"]),
                eClockBase.CeC.Convierte2Decimal(Fila["HorasTriples"])
                );
            return Tiempo;
        }
        return null;
    }

    #region PersonasDiario
    public int PERSONA_DIARIO_ID = -1;
    public int PERSONA_D_HE_ID = 0;
    public int ACCESO_E_ID = 0;
    public int ACCESO_S_ID = 0;
    public int ACCESO_CS_ID = 0;
    public int ACCESO_CR_ID = 0;
    public int PERSONA_ID = 0;
    public DateTime PERSONA_DIARIO_FECHA = CeC_BD.FechaNula;
    public int TIPO_INC_SIS_ID = 0;
    public int TIPO_INC_C_SIS_ID = 0;
    public int INCIDENCIA_ID = 0;
    public int TURNO_DIA_ID = 0;
    public DateTime PERSONA_DIARIO_TT = CeC_BD.FechaNula;
    public DateTime PERSONA_DIARIO_TE = CeC_BD.FechaNula;
    public DateTime PERSONA_DIARIO_TC = CeC_BD.FechaNula;
    public DateTime PERSONA_DIARIO_TDE = CeC_BD.FechaNula;
    public DateTime PERSONA_DIARIO_TES = CeC_BD.FechaNula;
    public CeC_Asistencias()
    {
        //
        // TODO: agregar aquí la lógica del constructor
        //
    }
    public CeC_Asistencias(int PersonaDiarioID, CeC_Sesion Sesion)
    {
        string Qry = "SELECT " + Campos + " FROM EC_PERSONAS_DIARIO WHERE PERSONA_DIARIO_ID = " + PersonaDiarioID.ToString();
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry);
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return;
        Carga(DS.Tables[0].Rows[0]);
    }
    public static CeC_Asistencias PorHorasExtras(int PERSONA_D_HE_ID, CeC_Sesion Sesion)
    {
        string Qry = "SELECT " + Campos + " FROM EC_PERSONAS_DIARIO WHERE PERSONA_D_HE_ID = " + PERSONA_D_HE_ID.ToString();
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry);
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return null;
        CeC_Asistencias Asis = new CeC_Asistencias();
        if (!Asis.Carga(DS.Tables[0].Rows[0]))
            return null;
        return Asis;
    }
    public static string Campos
    {
        get { return " PERSONA_DIARIO_ID, PERSONA_D_HE_ID, ACCESO_E_ID, ACCESO_S_ID, ACCESO_CS_ID, ACCESO_CR_ID, PERSONA_ID, PERSONA_DIARIO_FECHA, TIPO_INC_SIS_ID, TIPO_INC_C_SIS_ID, INCIDENCIA_ID, TURNO_DIA_ID, PERSONA_DIARIO_TT, PERSONA_DIARIO_TE, PERSONA_DIARIO_TC, PERSONA_DIARIO_TDE, PERSONA_DIARIO_TES"; }
    }
    public bool Carga(DataRow Fila)
    {
        try
        {
            DataRow DR = Fila;
            PERSONA_DIARIO_ID = CeC.Convierte2Int(DR["PERSONA_DIARIO_ID"]);
            PERSONA_D_HE_ID = CeC.Convierte2Int(DR["PERSONA_D_HE_ID"]);
            ACCESO_E_ID = CeC.Convierte2Int(DR["ACCESO_E_ID"]);
            ACCESO_S_ID = CeC.Convierte2Int(DR["ACCESO_S_ID"]);
            ACCESO_CS_ID = CeC.Convierte2Int(DR["ACCESO_CS_ID"]);
            ACCESO_CR_ID = CeC.Convierte2Int(DR["ACCESO_CR_ID"]);
            PERSONA_ID = CeC.Convierte2Int(DR["PERSONA_ID"]);
            PERSONA_DIARIO_FECHA = CeC.Convierte2DateTime(DR["PERSONA_DIARIO_FECHA"]);
            TIPO_INC_SIS_ID = CeC.Convierte2Int(DR["TIPO_INC_SIS_ID"]);
            TIPO_INC_C_SIS_ID = CeC.Convierte2Int(DR["TIPO_INC_C_SIS_ID"]);
            INCIDENCIA_ID = CeC.Convierte2Int(DR["INCIDENCIA_ID"]);
            TURNO_DIA_ID = CeC.Convierte2Int(DR["TURNO_DIA_ID"]);
            PERSONA_DIARIO_TT = CeC.Convierte2DateTime(DR["PERSONA_DIARIO_TT"]);
            PERSONA_DIARIO_TE = CeC.Convierte2DateTime(DR["PERSONA_DIARIO_TE"]);
            PERSONA_DIARIO_TC = CeC.Convierte2DateTime(DR["PERSONA_DIARIO_TC"]);
            PERSONA_DIARIO_TDE = CeC.Convierte2DateTime(DR["PERSONA_DIARIO_TDE"]);
            PERSONA_DIARIO_TES = CeC.Convierte2DateTime(DR["PERSONA_DIARIO_TES"]);
            return true;
        }
        catch { }
        return false;
    }

    #endregion
}