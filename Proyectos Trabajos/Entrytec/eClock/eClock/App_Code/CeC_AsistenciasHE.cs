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

/// <summary>
/// Descripción breve de CeC_AsistenciaHE
/// </summary>
public class CeC_AsistenciasHE
{
    public CeC_AsistenciasHE()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public static DataSet ObtenHorasExtras(bool EntradaSalida, bool Comida, bool Totales, bool Incidencia, bool TurnoDia, bool MuestraAgrupacion, bool MuestraEmpleado, bool MuestraCeros, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int Usuario_ID)
    {
        string Campos = "";
        string OrdenarPor = "";
        if (MuestraAgrupacion)
        {
            Campos += "AGRUPACION_NOMBRE, ";
            OrdenarPor += "AGRUPACION_NOMBRE, ";
        }
        if (Persona_ID > 0)
        {

            OrdenarPor += "PERSONA_DIARIO_FECHA";
        }
        else
        {

            OrdenarPor += "PERSONA_NOMBRE, PERSONA_LINK_ID, PERSONA_DIARIO_FECHA";
        }
        if (!MuestraEmpleado)
            Campos += "PERSONA_DIARIO_FECHA";
        else
            Campos += "PERSONA_LINK_ID, PERSONA_NOMBRE, PERSONA_DIARIO_FECHA";

        //        Campos += ",  PERSONA_D_HE_SIS,  PERSONA_D_HE_CAL, convert(int,datepart(hh,PERSONA_D_HE_APL)) as PERSONA_D_HE_APL ";
        string QryPERSONA_D_HE_APL = CeC_Asistencias.QryCampoSegundos("PERSONA_D_HE_APL") + "/3600.0";
        Campos += ",  PERSONA_D_HE_SIS,  PERSONA_D_HE_CAL, " + QryPERSONA_D_HE_APL + " as PERSONA_D_HE_APL ";

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
            if (Comida)
                Campos += ", PERSONA_DIARIO_TE, PERSONA_DIARIO_TC";
        }

        Campos += ", PERSONA_D_HE_SIMPLE, PERSONA_D_HE_DOBLE, PERSONA_D_HE_TRIPLE, PERSONA_D_HE_COMEN";

        return ObtenHorasExtras(Persona_ID, Agrupacion, FechaInicial, FechaFinal, Usuario_ID, Campos, OrdenarPor, MuestraCeros);
    }
    public static DataSet ObtenHorasExtras(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int Usuario_ID, string Campos, string OrdenarPor, bool MuestraCeros)
    {
        string QryCeros = " AND (PERSONA_D_HE_CAL IS NOT NULL AND PERSONA_D_HE_CAL <> " + CeC_BD.SqlFecha(CeC_BD.FechaNula) + " OR PERSONA_D_HE_APL > " + CeC_BD.SqlFecha(CeC_BD.FechaNula) + ")";
        if (MuestraCeros)
            QryCeros = "";
        string Qry = "SELECT     PERSONA_D_HE_ID,  " + Campos + " " +
        " FROM         EC_V_ASISTENCIAS " +
        " WHERE        (PERSONA_DIARIO_FECHA >= @FECHA_INICIAL@) AND  " +
        " (PERSONA_DIARIO_FECHA < @FECHA_FINAL@) " +
         QryCeros +
        CeC_Asistencias.ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, false) +
        " AND PERSONA_D_HE_ID > 0 " +
        " ORDER BY " + OrdenarPor;
        Qry = CeC_BD.AsignaParametro(Qry, "USUARIO_ID", Usuario_ID);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal);
        Qry = CeC_BD.AsignaParametro(Qry, "AGRUPACION_NOMBRE", Agrupacion + "%");

        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }


    public static int QuitaHoraExtra(DateTime FechaInicio, DateTime FechaFin)
    {
        return QuitaHoraExtra(FechaInicio, FechaFin, false);
    }
    public static int QuitaHoraExtra(DateTime FechaInicio, DateTime FechaFin, bool Forzar)
    {
        if (Forzar)
        {
            CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_DIARIO SET PERSONA_D_HE_ID = 0 " +
                                " WHERE PERSONA_DIARIO_FECHA >= " + CeC_BD.SqlFecha(FechaInicio) + " AND PERSONA_DIARIO_FECHA <= " + CeC_BD.SqlFecha(FechaFin)
            );
        }
        else
        {
            CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_D_HE SET PERSONA_D_HE_SIS = " + CeC_BD.SqlFechaNula()
                + " , PERSONA_D_HE_SIS_A = " + CeC_BD.SqlFechaNula()
                + " , PERSONA_D_HE_SIS_D = " + CeC_BD.SqlFechaNula()
                + " , PERSONA_D_HE_CAL = " + CeC_BD.SqlFechaNula()
                + "  WHERE PERSONA_D_HE_ID in (SELECT PERSONA_D_HE_ID FROM EC_PERSONAS_DIARIO "
                + " WHERE PERSONA_DIARIO_FECHA >= " + CeC_BD.SqlFecha(FechaInicio) + " AND PERSONA_DIARIO_FECHA <= " + CeC_BD.SqlFecha(FechaFin)
                + " AND PERSONA_D_HE_ID > 0 "
                + " )");

        }
        return 0;
    }
    public static int QuitaHoraExtra(int PERSONA_D_HE_ID, bool Forzar)
    {
        if (PERSONA_D_HE_ID <= 0)
            return 0;
        int Sesion_ID = CeC_BD.EjecutaEscalarInt("SELECT SESION_ID FROM EC_PERSONAS_D_HE WHERE PERSONA_D_HE_ID = " + PERSONA_D_HE_ID);

        if (Sesion_ID <= 0 || Forzar)
        {
            CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_DIARIO SET PERSONA_D_HE_ID = 0 WHERE PERSONA_D_HE_ID = " + PERSONA_D_HE_ID);
        }
        else
        {
            CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_D_HE SET PERSONA_D_HE_SIS = " + CeC_BD.SqlFechaNula()
                + " , PERSONA_D_HE_SIS_A = " + CeC_BD.SqlFechaNula()
                + " , PERSONA_D_HE_SIS_D = " + CeC_BD.SqlFechaNula()
                + " , PERSONA_D_HE_CAL = " + CeC_BD.SqlFechaNula()
                + "  WHERE PERSONA_D_HE_ID = " + PERSONA_D_HE_ID);
        }
        return 0;
    }

    public static int QuitaHoraExtraPersona_Diario(int PERSONA_Diario_ID, bool Forzar)
    {
        int PERSONA_D_HE_ID = CeC_BD.EjecutaEscalarInt("SELECT PERSONA_D_HE_ID FROM EC_PERSONAS_DIARIO WHERE PERSONA_DIARIO_ID = " + PERSONA_Diario_ID);
        if (PERSONA_D_HE_ID > 0)
            return QuitaHoraExtra(PERSONA_D_HE_ID, Forzar);
        return 0;
    }

    public static int QuitaHoraExtra(int PERSONA_ID, DateTime Fecha, bool Forzar)
    {
        int PERSONA_D_HE_ID = CeC_BD.EjecutaEscalarInt("SELECT PERSONA_D_HE_ID FROM EC_PERSONAS_DIARIO WHERE PERSONA_ID = " + PERSONA_ID + " AND PERSONA_DIARIO_FECHA = " + CeC_BD.SqlFecha(Fecha));
        if (PERSONA_D_HE_ID > 0)
            return QuitaHoraExtra(PERSONA_D_HE_ID, Forzar);
        return 0;

    }

    /// <summary>
    /// Agrega unas horas extras a una persona para un dia especifico, esta funcion se podra llamar
    /// desde el modulo de alta de horas extras
    /// </summary>
    /// <param name="PERSONA_ID"></param>
    /// <param name="FechaHoraExtra"></param>
    /// <param name="Sesion_ID"></param>
    /// <param name="TiempoHoraExtra"></param>
    /// <returns>Regresa menor o igual a cero si existió un error de lo contrario regresa el PERSONA_D_HE_ID</returns>
    public static int AgregaHorasExtrasApl(int PERSONA_ID, DateTime FechaHoraExtra, int Sesion_ID, TimeSpan TiempoHoraExtra)
    {
        try
        {
            int PERSONA_D_HE_ID = CeC_BD.EjecutaEscalarInt("SELECT PERSONA_D_HE_ID FROM EC_PERSONAS_DIARIO WHERE PERSONA_ID = " +
        PERSONA_ID + " AND PERSONA_DIARIO_FECHA = " + CeC_BD.SqlFecha(FechaHoraExtra));
            DS_CEC_AsistenciasTableAdapters.EC_PERSONAS_D_HETableAdapter TAHE = new DS_CEC_AsistenciasTableAdapters.EC_PERSONAS_D_HETableAdapter();
            DS_CEC_Asistencias.EC_PERSONAS_D_HEDataTable DTHE = new DS_CEC_Asistencias.EC_PERSONAS_D_HEDataTable();
            DS_CEC_Asistencias.EC_PERSONAS_D_HERow FilaHE = null;
            if (PERSONA_D_HE_ID > 0)
            {
                TAHE.FillBy(DTHE, PERSONA_D_HE_ID);
                FilaHE = DTHE[0];
            }
            else
            {
                FilaHE = (DS_CEC_Asistencias.EC_PERSONAS_D_HERow)DTHE.NewRow();
                FilaHE.PERSONA_D_HE_ID = CeC_Autonumerico.GeneraAutonumerico("EC_PERSONAS_D_HE", "PERSONA_D_HE_ID");
                FilaHE.SESION_ID = 0;
            }

            FilaHE.SESION_ID = Sesion_ID;
            FilaHE.PERSONA_D_HE_APL = CeC_BD.TimeSpan2DateTime(TiempoHoraExtra);
            FilaHE.PERSONA_D_HE_FH = DateTime.Now;

            if (PERSONA_D_HE_ID <= 0)
            {
                DTHE.AddEC_PERSONAS_D_HERow(FilaHE);
            }
            TAHE.Update(DTHE);
            CalculaHorasExtrasDT(Convert.ToInt32(FilaHE.PERSONA_D_HE_ID));

            return Convert.ToInt32(FilaHE.PERSONA_D_HE_ID);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return 0;
    }

    /// <summary>
    /// Aplica las horas extras calculadas
    /// </summary>
    /// <param name="PERSONA_ID"></param>
    /// <param name="FechaHoraExtra"></param>
    /// <param name="?"></param>
    /// <returns></returns>
    public static int AsignaHorasExtrasApl(CeC_Sesion Sesion, int PERSONA_D_HE_ID, string Comentarios)
    {
        if (Sesion == null)
            return -1;
        int No = CeC_BD.EjecutaEscalarInt("UPDATE EC_PERSONAS_D_HE SET PERSONA_D_HE_APL = PERSONA_D_HE_CAL, SESION_ID = " + Sesion.SESION_ID + ", PERSONA_D_HE_FH = "
            + CeC_BD.SqlFechaHora(DateTime.Now) + ", TIPO_INCIDENCIA_ID = 0, PERSONA_D_HE_COMEN = '" + CeC_BD.ObtenParametroCadena(Comentarios) + "' WHERE PERSONA_D_HE_ID = " + PERSONA_D_HE_ID);
        CalculaHorasExtrasDT(PERSONA_D_HE_ID);
        return No;
    }
    public static int AsignaHorasExtrasApl(CeC_Sesion Sesion, int PERSONA_D_HE_ID, double HorasExtrasAAplicar, int TipoIncidenciaID, string Comentarios)
    {
        return AsignaHorasExtrasApl(Sesion, PERSONA_D_HE_ID, TimeSpan.FromHours(HorasExtrasAAplicar), TipoIncidenciaID, Comentarios);
    }

    public static int AsignaHorasExtrasApl(CeC_Sesion Sesion, int PERSONA_D_HE_ID, TimeSpan PERSONA_D_HE_APL, int TipoIncidenciaID, string Comentarios)
    {
        if (Sesion == null)
            return -1;

        int No = CeC_BD.EjecutaEscalarInt("UPDATE EC_PERSONAS_D_HE SET PERSONA_D_HE_APL = "
            + CeC_BD.SqlFechaHora(CeC_BD.TimeSpan2DateTime(PERSONA_D_HE_APL)) +
            ", PERSONA_D_HE_SIMPLE = " + PERSONA_D_HE_APL.TotalHours +
            ", SESION_ID = " + Sesion.SESION_ID + ", PERSONA_D_HE_FH = "
            + CeC_BD.SqlFechaHora(DateTime.Now) + ", TIPO_INCIDENCIA_ID = " + TipoIncidenciaID + ", PERSONA_D_HE_COMEN = '" + CeC_BD.ObtenParametroCadena(Comentarios) + "' WHERE PERSONA_D_HE_ID = " + PERSONA_D_HE_ID);
        CalculaHorasExtrasDT(PERSONA_D_HE_ID);
        if (CeC_TiempoXTiempos.EsHorasExtras(TipoIncidenciaID))
        {
            CeC_TiempoXTiempos.Agrega(Sesion, PERSONA_D_HE_ID, PERSONA_D_HE_APL, TipoIncidenciaID, Comentarios);
        }
        return No;
    }

    public static int ObtenSuscripcionID(int PERSONA_D_HE_ID)
    {
        return CeC_BD.EjecutaEscalarInt("SELECT SUSCRIPCION_ID FROM EC_PERSONAS,EC_PERSONAS_DIARIO,EC_PERSONAS_D_HE WHERE EC_PERSONAS.PERSONA_ID = EC_PERSONAS_DIARIO.PERSONA_ID  AND EC_PERSONAS_DIARIO.PERSONA_D_HE_ID = EC_PERSONAS_D_HE.PERSONA_D_HE_ID AND EC_PERSONAS_D_HE.PERSONA_D_HE_ID = " + PERSONA_D_HE_ID);
    }
    public static int ObtenPersonaID(int PERSONA_D_HE_ID)
    {
        return CeC_BD.EjecutaEscalarInt("SELECT PERSONA_ID FROM EC_PERSONAS_DIARIO,EC_PERSONAS_D_HE WHERE EC_PERSONAS_DIARIO.PERSONA_D_HE_ID = EC_PERSONAS_D_HE.PERSONA_D_HE_ID AND EC_PERSONAS_D_HE.PERSONA_D_HE_ID = " + PERSONA_D_HE_ID);
    }

    public static int ObtenPeriodoHE(int PERSONA_D_HE_ID, int PeriodoNID)
    {

        string Qry = "SELECT     EC_PERIODOS.PERIODO_ID " +
    " FROM         EC_PERSONAS_DIARIO INNER JOIN " +
    "                       EC_PERSONAS_D_HE ON EC_PERSONAS_DIARIO.PERSONA_D_HE_ID = EC_PERSONAS_D_HE.PERSONA_D_HE_ID INNER JOIN " +
    "                       EC_PERIODOS ON EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA >= EC_PERIODOS.PERIODO_ASIS_INICIO AND  " +
    "                       EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA <= EC_PERIODOS.PERIODO_ASIS_FIN " +
    " WHERE     (EC_PERSONAS_D_HE.PERSONA_D_HE_ID = " + PERSONA_D_HE_ID + ") AND (EC_PERIODOS.PERIODO_N_ID = " + PeriodoNID + ")";

        return CeC_BD.EjecutaEscalarInt(Qry);
    }

    public static bool AsignaHoras(int PERSONA_D_HE_ID, double Simples, double Dobles, double Triples, int TipoIncID)
    {
        if (CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_D_HE SET PERSONA_D_HE_SIMPLE = " + Simples +
            ", PERSONA_D_HE_DOBLE = " + Dobles + ", PERSONA_D_HE_TRIPLE = " + Triples +
            ", TIPO_INCIDENCIA_ID = " + TipoIncID + " WHERE PERSONA_D_HE_ID = " + PERSONA_D_HE_ID) > 0)
            return true;
        return false;
    }
    /// <summary>
    /// Calculas las horas extras dobles y triples
    /// </summary>
    /// <param name="PERSONA_D_HE_ID"></param>
    /// <returns></returns>
    public static bool CalculaHorasExtrasDT(int PERSONA_D_HE_ID)
    {
        if (PERSONA_D_HE_ID <= 0)
            return false;
        int PersonaID = ObtenPersonaID(PERSONA_D_HE_ID);
        if (PersonaID <= 0)
            return false;
        //        CeC_ConfigSuscripcion  Cfg = new CeC_ConfigSuscripcion(ObtenSuscripcionID(PERSONA_D_HE_ID));
        CeC_ConfigSuscripcion Cfg = CeC_Personas.ObtenConfigSuscripcion(PersonaID);
        int PeriodoHEID = ObtenPeriodoHE(PERSONA_D_HE_ID, Cfg.PeriodoNID_HorasExtras);
        //        int HED = Cfg.TipoIncidenciaHoraExtraDoble;
        //        int HET = Cfg.TipoIncidenciaHoraExtraTriple;
        string FiltroTipoIncidenca = "TIPO_INCIDENCIA_ID = 0";
        int HoraExtraTxT_TipoIncidenciaID = Cfg.HoraExtraTxT_TipoIncidenciaID;
        int TipoIncidenciaReglaID = 0;
        if (HoraExtraTxT_TipoIncidenciaID > 0)
        {
            TipoIncidenciaReglaID = CeC_IncidenciasRegla.ObtenTipo_Incidencia_R_ID(PersonaID, HoraExtraTxT_TipoIncidenciaID);
            if (TipoIncidenciaReglaID > 0)
                FiltroTipoIncidenca = "(TIPO_INCIDENCIA_ID = 0 OR TIPO_INCIDENCIA_ID = " + HoraExtraTxT_TipoIncidenciaID + ")";
        }


        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet("SELECT EC_PERSONAS_D_HE.PERSONA_D_HE_ID, EC_PERSONAS_D_HE.PERSONA_D_HE_APL, EC_PERSONAS_D_HE.TIPO_INCIDENCIA_ID, EC_PERSONAS_D_HE.PERSONA_D_HE_SIMPLE " +
    " FROM         EC_PERSONAS_DIARIO INNER JOIN " +
    "                       EC_PERSONAS_D_HE ON EC_PERSONAS_DIARIO.PERSONA_D_HE_ID = EC_PERSONAS_D_HE.PERSONA_D_HE_ID INNER JOIN " +
    "                       EC_PERIODOS ON EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA >= EC_PERIODOS.PERIODO_ASIS_INICIO AND  " +
    "                       EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA <= EC_PERIODOS.PERIODO_ASIS_FIN " +
    " WHERE     (EC_PERSONAS_DIARIO.PERSONA_ID = " + PersonaID + ") AND (EC_PERIODOS.PERIODO_ID = " + PeriodoHEID + ") AND  " +
    "                       (EC_PERSONAS_D_HE.PERSONA_D_HE_APL IS NOT NULL) AND " + FiltroTipoIncidenca +
    " ORDER BY EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA");
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return false;
        bool EsTriple = false;
        double NoHoras = 0;
        double NoHorasNo = 0;
        double Saldo = 0;
        double TxTSemanal = 0;
        int TxTMaximoXDia = 0;
        int TxTMaximoXSemana = 0;
        if (TipoIncidenciaReglaID > 0)
        {
            TxTMaximoXDia = Cfg.HoraExtraTxT_MaximoXDia;
            TxTMaximoXSemana = Cfg.HoraExtraTxT_MaxXSemana;
        }

        if (TipoIncidenciaReglaID > 0)
            Saldo = CeC.Convierte2Double(CeC_IncidenciasInventario.ObtenSaldo(PersonaID, TipoIncidenciaReglaID));
        CIsLog2.AgregaLog("Persona " + PersonaID + " TipoIncidenciaReglaID = " + TipoIncidenciaReglaID + " Con saldo " + Saldo + " HoraExtraTxT_TipoIncidenciaID=" + HoraExtraTxT_TipoIncidenciaID + " PeriodoHEID= " + PeriodoHEID);

        int NoDiasHE = 0;

        foreach (DataRow DR in DS.Tables[0].Rows)
        {
            int Persona_D_HE_ID = Convert.ToInt32(DR["PERSONA_D_HE_ID"]);
            DateTime Persona_D_HE_APL = Convert.ToDateTime(DR["PERSONA_D_HE_APL"]);
            TimeSpan TS = CeC_BD.DateTime2TimeSpan(Persona_D_HE_APL);
            int Tipo_IncidenciaID = CeC.Convierte2Int(DR["TIPO_INCIDENCIA_ID"]);
            double Simples = CeC.Convierte2Double(DR["PERSONA_D_HE_SIMPLE"]);
            double TotalHoras = TS.TotalHours;


            if (Tipo_IncidenciaID > 0)
                TotalHoras = TotalHoras - Simples;
            if (Tipo_IncidenciaID == 0)
            {
                Simples = 0;
                CeC_TiempoXTiempos.Quita(Persona_D_HE_ID, TipoIncidenciaReglaID, 0, 0);
                
            }

            if (Saldo < 0 && Tipo_IncidenciaID == 0)
            {

                //Contiene el decremento de horas extras pero incremento en el saldo de Tiempo Por Tiempo
                double Decremento = 0;
                if (-Saldo > TxTMaximoXDia)
                    Decremento = TxTMaximoXDia;
                else
                    Decremento = -Saldo;
                if (Decremento > TotalHoras)
                    Decremento = TotalHoras;

                if (TxTSemanal + Decremento > TxTMaximoXSemana)
                    if (TxTSemanal >= TxTMaximoXSemana)
                        Decremento = 0;
                    else
                        Decremento = TxTMaximoXSemana - TxTSemanal;

                CIsLog2.AgregaLog("Persona " + PersonaID + " Con saldo " + Saldo + " Con Decremento " + Decremento);
                if (Decremento > 0)
                {
                    TotalHoras -= Decremento;
                    TxTSemanal += Decremento;
                    Simples = Decremento;
                    Tipo_IncidenciaID = HoraExtraTxT_TipoIncidenciaID;
                    CeC_TiempoXTiempos.Agrega(Persona_D_HE_ID, HoraExtraTxT_TipoIncidenciaID, TipoIncidenciaReglaID, CeC.Convierte2Decimal(Decremento)
                        , "", null);
                    Saldo += Decremento;
                }
            }
            double Dobles = 0;
            double Triples = 0;
            NoHorasNo = NoHoras + TotalHoras;

            if (Cfg.HorasExtrasRegla == "9")
            {
                if (NoHorasNo >= 10)
                {
                    if (NoHoras < 10)
                    {
                        Dobles = 9 - NoHoras;
                        Triples = NoHorasNo - 9;
                    }
                    else
                        Triples = TotalHoras;
                }
                else
                    Dobles = TotalHoras;
                NoHoras = NoHorasNo;
            }
            if (Cfg.HorasExtrasRegla == "333")
            {
                if (NoDiasHE >= 3)
                {
                    Triples = TotalHoras;
                }
                else
                {
                    if (TotalHoras > 3)
                    {
                        Dobles = 3;
                        Triples = TotalHoras - 3;
                    }
                    else
                        Dobles = TotalHoras;
                }
                if (TotalHoras > 0)
                    NoDiasHE++;
            }
            AsignaHoras(Persona_D_HE_ID, Simples, Dobles, Triples, Tipo_IncidenciaID);
        }
        return false;
    }

    public static int AsignaHorasExtras(int PERSONA_ID, DateTime FechaHoraExtra,
        TimeSpan PERSONA_D_HE_SIS, TimeSpan PERSONA_D_HE_SIS_A, TimeSpan PERSONA_D_HE_SIS_D,
        TimeSpan PERSONA_D_HE_CAL)
    {
        try
        {
            int PERSONA_D_HE_ID = CeC_BD.EjecutaEscalarInt("SELECT PERSONA_D_HE_ID FROM EC_PERSONAS_DIARIO WHERE PERSONA_ID = " +
        PERSONA_ID + " AND PERSONA_DIARIO_FECHA = " + CeC_BD.SqlFecha(FechaHoraExtra));
            DS_CEC_AsistenciasTableAdapters.EC_PERSONAS_D_HETableAdapter TAHE = new DS_CEC_AsistenciasTableAdapters.EC_PERSONAS_D_HETableAdapter();
            DS_CEC_Asistencias.EC_PERSONAS_D_HEDataTable DTHE = new DS_CEC_Asistencias.EC_PERSONAS_D_HEDataTable();
            DS_CEC_Asistencias.EC_PERSONAS_D_HERow FilaHE = null;
            if (PERSONA_D_HE_ID > 0)
            {
                TAHE.FillBy(DTHE, PERSONA_D_HE_ID);
                FilaHE = DTHE[0];
            }
            else
            {
                FilaHE = (DS_CEC_Asistencias.EC_PERSONAS_D_HERow)DTHE.NewRow();
                FilaHE.PERSONA_D_HE_ID = CeC_Autonumerico.GeneraAutonumerico("EC_PERSONAS_D_HE", "PERSONA_D_HE_ID");
                FilaHE.SESION_ID = 0;

            }


            FilaHE.PERSONA_D_HE_SIS = CeC_BD.TimeSpan2DateTime(PERSONA_D_HE_SIS);
            FilaHE.PERSONA_D_HE_SIS_A = CeC_BD.TimeSpan2DateTime(PERSONA_D_HE_SIS_A);
            FilaHE.PERSONA_D_HE_SIS_D = CeC_BD.TimeSpan2DateTime(PERSONA_D_HE_SIS_D);
            FilaHE.PERSONA_D_HE_CAL = CeC_BD.TimeSpan2DateTime(PERSONA_D_HE_CAL);

            if (PERSONA_D_HE_ID <= 0)
            {
                DTHE.AddEC_PERSONAS_D_HERow(FilaHE);
            }
            TAHE.Update(DTHE);
            CalculaHorasExtrasDT(PERSONA_D_HE_ID);
            return Convert.ToInt32(FilaHE.PERSONA_D_HE_ID);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("CeC_AsistenciasHE.AsignaHorasExtras", ex);
        }
        return 0;
    }
    public static int ObtenNoHorasExtrasXAplicar(int SuscripcionID)
    {
        return ObtenNoHorasExtrasXAplicar(SuscripcionID, -1);
    }
    public static int ObtenNoHorasExtrasXAplicar(int SuscripcionID, int Usuario_ID)
    {
        string Qry = "SELECT PERSONA_ID FROM EC_PERSONAS INNER JOIN " +
                " EC_PERMISOS_SUSCRIP ON EC_PERSONAS.SUSCRIPCION_ID = EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID " +
                " WHERE (EC_PERSONAS.PERSONA_BORRADO = 0) ";
        if (Usuario_ID > 0)
            Qry += " AND (EC_PERMISOS_SUSCRIP.USUARIO_ID = " + Usuario_ID + ")";
        if (SuscripcionID > 0)
            Qry += " AND (EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID = " + SuscripcionID + ")";


        return CeC_BD.EjecutaEscalarInt("SELECT COUNT(EC_PERSONAS_D_HE.PERSONA_D_HE_ID) FROM EC_PERSONAS_D_HE, EC_PERSONAS_DIARIO WHERE " +
                "EC_PERSONAS_D_HE.PERSONA_D_HE_ID = EC_PERSONAS_DIARIO.PERSONA_D_HE_ID AND EC_PERSONAS_DIARIO.PERSONA_ID IN (" +
                Qry + ") AND  " +
                " PERSONA_D_HE_APL is null  AND PERSONA_D_HE_SIS <> " + CeC_BD.SqlFechaHora(CeC_BD.FechaNula));
    }


    public static int ObtenTotalSegundosHE(string CampoPersonaDiario, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int Usuario_ID)
    {
        return CeC_BD.EjecutaEscalarInt("SELECT SUM(" + CeC_Asistencias.QryCampoSegundos(CampoPersonaDiario) + ") AS TotalSegundos " +
                " FROM EC_PERSONAS_DIARIO, EC_PERSONAS_D_HE WHERE " +
                " EC_PERSONAS_DIARIO.PERSONA_D_HE_ID = EC_PERSONAS_D_HE.PERSONA_D_HE_ID AND " +
            " PERSONA_DIARIO_FECHA >= " + CeC_BD.SqlFecha(FechaInicial) + " AND PERSONA_DIARIO_FECHA <" +
            CeC_BD.SqlFecha(FechaFinal) + " " +
            " AND " + CeC_Asistencias.ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, true));
    }

    public static string ObtenTotalHorasHE(string CampoPersonaDiario, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int Usuario_ID)
    {
        if (CampoPersonaDiario == "PERSONA_D_HE_SIMPLE" || CampoPersonaDiario == "PERSONA_D_HE_DOBLE" || CampoPersonaDiario == "PERSONA_D_HE_TRIPLE")
        {
            decimal Des = CeC_BD.EjecutaEscalarDecimal("SELECT SUM(" + CampoPersonaDiario + ") AS TotalHoras " +
                " FROM EC_PERSONAS_DIARIO, EC_PERSONAS_D_HE WHERE " +
                " EC_PERSONAS_DIARIO.PERSONA_D_HE_ID = EC_PERSONAS_D_HE.PERSONA_D_HE_ID AND " +
            " PERSONA_DIARIO_FECHA >= " + CeC_BD.SqlFecha(FechaInicial) + " AND PERSONA_DIARIO_FECHA <" +

            CeC_BD.SqlFecha(FechaFinal) + " " +
            " AND " + CeC_Asistencias.ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, true));
            if (Des <= -9999)
                return "0.00";
            return Des.ToString("#,##0.00");
        }
        int Total = ObtenTotalSegundosHE(CampoPersonaDiario, Persona_ID, Agrupacion, FechaInicial, FechaFinal, Usuario_ID);
        if (Total <= -9999)
            return "00:00:00";
        TimeSpan TS = new TimeSpan(Total / (60 * 60), (Total / 60) % 60, Total % (60));
        int TotHoras = (TS.Days * 24 + TS.Hours);
        return TotHoras.ToString("#,##0") + ":" + TS.Minutes.ToString("00") + ":" + TS.Seconds.ToString("00");
    }

    public static DateTime ObtenHorasExtrasCalculadas(int PersonaDHE_ID)
    {
        DateTime Ret = CeC_BD.EjecutaEscalarDateTime("SELECT PERSONA_D_HE_CAL FROM EC_PERSONAS_D_HE WHERE PERSONA_D_HE_ID = " + PersonaDHE_ID);
        return Ret;
    }
}
