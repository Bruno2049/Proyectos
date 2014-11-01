using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;

/// <summary>
/// Descripción breve de CeC_Turnos
/// </summary>
public class CeC_Turnos : CeC_Tabla
{
    int m_Turno_Id = 0;
    [Description("Identificador unico de registro de turno")]
    [DisplayNameAttribute("Turno_Id")]
    public int Turno_Id { get { return m_Turno_Id; } set { m_Turno_Id = value; } }
    int m_Tipo_Turno_Id = 0;
    [Description("Tipo de Turno (Semanal, Flexible, etc.)")]
    [DisplayNameAttribute("Tipo_Turno_Id")]
    public int Tipo_Turno_Id { get { return m_Tipo_Turno_Id; } set { m_Tipo_Turno_Id = value; } }
    string m_Turno_Nombre = "";
    [Description("Nombre del Turno")]
    [DisplayNameAttribute("Turno_Nombre")]
    public string Turno_Nombre { get { return m_Turno_Nombre; } set { m_Turno_Nombre = value; } }
    bool m_Turno_Asistencia = false;
    [Description("Indica si las personas que esten asignadas a este turno se les generara el cálculo de prenomina (asistencia)")]
    [DisplayNameAttribute("Turno_Asistencia")]
    public bool Turno_Asistencia { get { return m_Turno_Asistencia; } set { m_Turno_Asistencia = value; } }
    bool m_Turno_Phextras = false;
    [Description("Indica si se permitirán horas extras")]
    [DisplayNameAttribute("Turno_Phextras")]
    public bool Turno_Phextras { get { return m_Turno_Phextras; } set { m_Turno_Phextras = value; } }
    bool m_Turno_Individual = false;
    [Description("Indica si el turno actual solo aplica a una persona. el nombre del turno deberá ser el persona_Link_ID")]
    [DisplayNameAttribute("Turno_Individual")]
    public bool Turno_Individual { get { return m_Turno_Individual; } set { m_Turno_Individual = value; } }
    string m_Turno_Grupos = "";
    [Description("Si este campo contiene datos indicará los grupos (grupo_1_ID separado por comas) que podrán ver el turno actual , el administrador")]
    [DisplayNameAttribute("Turno_Grupos")]
    public string Turno_Grupos { get { return m_Turno_Grupos; } set { m_Turno_Grupos = value; } }
    int m_Turno_Color = 0;
    [Description("Color que se usará para dibujar el turno")]
    [DisplayNameAttribute("Turno_Color")]
    public int Turno_Color { get { return m_Turno_Color; } set { m_Turno_Color = value; } }
    bool m_Turno_Borrado = false;
    [Description("Indica el status de este registro")]
    [DisplayNameAttribute("Turno_Borrado")]
    public bool Turno_Borrado { get { return m_Turno_Borrado; } set { m_Turno_Borrado = value; } }

    public CeC_Turnos(CeC_Sesion Sesion)
        : base("EC_TURNOS", "TURNO_ID", Sesion)
    { }

    public CeC_Turnos(int TurnoId, CeC_Sesion Sesion)
        : base("EC_TURNOS", "TURNO_ID", Sesion)
    {
        Carga(TurnoId.ToString(), Sesion);
    }
    /// <summary>
    /// Carga, edita o crea Turnos
    /// </summary>
    /// <param name="TurnoId">Identificador unico de registro de turno</param>
    /// <param name="TipoTurnoId">Tipo de Turno (Semanal, Flexible, etc.)</param>
    /// <param name="TurnoNombre">Nombre del Turno</param>
    /// <param name="TurnoAsistencia">Indica si las personas que esten asignadas a este turno se les generara el cálculo de prenomina (asistencia)</param>
    /// <param name="TurnoPhextras">Indica si se permitirán horas extras</param>
    /// <param name="TurnoIndividual">Indica si el turno actual solo aplica a una persona. el nombre del turno deberá ser el persona_Link_ID</param>
    /// <param name="TurnoGrupos">Si este campo contiene datos indicará los grupos (grupo_1_ID separado por comas) que podrán ver el turno actual , el administrador</param>
    /// <param name="TurnoColor">Color que se usará para dibujar el turno</param>
    /// <param name="TurnoBorrado">Indica el status de este registro</param>
    /// <param name="Sesion">Variable de Sesion</param>
    /// <returns>True si se realizaron los cambios correctamente. Falso en otro caso</returns>
    public bool Actualiza(int TurnoId, int TipoTurnoId, string TurnoNombre, bool TurnoAsistencia, bool TurnoPhextras, bool TurnoIndividual, string TurnoGrupos, int TurnoColor, bool TurnoBorrado,
        CeC_Sesion Sesion)
    {
        try
        {
            bool Nuevo = false;
            if (!Carga(TurnoId.ToString(), Sesion))
                Nuevo = true;
            m_EsNuevo = Nuevo;
            Turno_Id = TurnoId; 
            Tipo_Turno_Id = TipoTurnoId;
            Turno_Nombre = TurnoNombre; 
            Turno_Asistencia = TurnoAsistencia; 
            Turno_Phextras = TurnoPhextras; 
            Turno_Individual = TurnoIndividual; 
            Turno_Grupos = TurnoGrupos; 
            Turno_Color = TurnoColor; 
            Turno_Borrado = TurnoBorrado;
            if (Guarda(Sesion))
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("CeC_Turnos_Dia.Actualiza", ex);
        }
        return false;
    }

    string m_Errores = "";

    public static bool BorrarTurno(int TurnoID)
    {
        return BorrarTurno(TurnoID, false);
    }
    public static bool BorrarTurno(int TurnoID, bool Forzar)
    {
        int ret = CeC_BD.EjecutaEscalarInt("SELECT * FROM EC_PERSONAS WHERE EC_PERSONAS.TURNO_ID = " + TurnoID);
        if (ret > 0)
            return false;

        int R = CeC_BD.EjecutaComando("UPDATE EC_TURNOS SET TURNO_BORRADO = 1 WHERE TURNO_ID = " + TurnoID);
        if (R > 0)
        {
            return true;
        }
        return false;
    }
    public static DataSet ObtenTurnosDSMenuAgregado(int Suscripcion_ID)
    {
        try
        {
            DataSet DS = ObtenTurnosDSMenu(Suscripcion_ID);
            DataRow DR = DS.Tables[0].NewRow();
            DR["TURNO_ID"] = -1;
            DR[1] = "Descanso";
            DS.Tables[0].Rows.Add(DR);
            DR = DS.Tables[0].NewRow();
            DR["TURNO_ID"] = 0;
            DR[1] = "Predeterminado";
            DS.Tables[0].Rows.Add(DR);
            return DS;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return null;
    }
    public static int AgregaTurnoSemanalDia(int turnoDiaID, int turnoID, int Dia, CeC_Sesion Sesion)
    {
        return AgregaTurnoSemanalDia(turnoDiaID, turnoID, Dia, Sesion.SESION_ID, Sesion.SUSCRIPCION_ID);
    }
    public static int AgregaTurnoSemanalDia(int turnoDiaID, int turnoID, int Dia, int SesionID, int SuscripcionID)
    {
        int turnoSemID = CeC_Autonumerico.GeneraAutonumerico("EC_TURNOS_SEMANAL_DIA", "TURNO_SEMANAL_DIA_ID", SesionID, SuscripcionID);
        CeC_BD.EjecutaComando("INSERT INTO EC_TURNOS_SEMANAL_DIA(TURNO_SEMANAL_DIA_ID,TURNO_DIA_ID," +
            "TURNO_ID,DIA_SEMANA_ID)" +
            "VALUES(" + turnoSemID + "," + turnoDiaID + "," + turnoID + "," + Dia + ")");
        return turnoSemID;
    }
    public static int AgregaTurno(int TipoTurno, string NombreTurno, int TiempoExtra, int TurnoAsistencia, int TurnoIndividual, int Borrado, CeC_Sesion Sesion)
    {
        return AgregaTurno(TipoTurno, NombreTurno, TiempoExtra, TurnoAsistencia, TurnoIndividual, Borrado, Sesion.SESION_ID, Sesion.SUSCRIPCION_ID);
    }
    public static int AgregaTurno(int TipoTurno, string NombreTurno, int TiempoExtra, int TurnoAsistencia, int TurnoIndividual, int Borrado, int SesionID, int SuscripcionID)
    {

        int turnoID = CeC_Autonumerico.GeneraAutonumerico("EC_TURNOS", "TURNO_ID", SesionID, SuscripcionID);
        int Agregado = CeC_BD.EjecutaComando("INSERT INTO EC_TURNOS(TURNO_ID,TIPO_TURNO_ID,TURNO_NOMBRE,TURNO_ASISTENCIA,TURNO_PHEXTRAS," +
            "TURNO_INDIVIDUAL,TURNO_BORRADO)" +
            "VALUES(" + turnoID + "," + TipoTurno + ",'" + NombreTurno + "'," + TurnoAsistencia + "," + TiempoExtra + "," + TurnoIndividual + "," + Borrado + ")");
        if (SesionID > 0 && Agregado > 0)
        {
            CeC_Sesion.SAgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.NUEVO, "Turno", turnoID, "Nombre = " + NombreTurno, SesionID);
        }
        return turnoID;
    }
    public static int AgregaTurnoDia(DateTime[] horas, int tiempoEx, int tipo_turno, int NoAsistencia, int hayComida, CeC_Sesion Sesion)
    {
        return AgregaTurnoDia(horas, tiempoEx, tipo_turno, NoAsistencia, hayComida, Sesion.SESION_ID, Sesion.SUSCRIPCION_ID);
    }
    public static int AgregaTurnoDia(DateTime[] horas, int tiempoEx, int tipo_turno, int NoAsistencia, int hayComida, int SesionID, int SuscripcionID)
    {
        try
        {
            int turnoDiaID = CeC_Autonumerico.GeneraAutonumerico("EC_TURNOS_DIA", "TURNO_DIA_ID", SesionID, SuscripcionID);
            TimeSpan TiempoXTrabajar;
            string Comando = "";
            switch (tipo_turno)
            {
                case 1://Semanal
                    //Tiempo por Trabajar = Hora de salida - Hora de entrada
                    TiempoXTrabajar = horas[5] - horas[1];

                    Comando = "INSERT INTO EC_TURNOS_DIA ( " +
                                    "TURNO_DIA_ID,TURNO_DIA_HEMIN," +
                                    "TURNO_DIA_HE,TURNO_DIA_HEMAX," +
                                    "TURNO_DIA_HERETARDO,TURNO_DIA_HSMIN," +
                                    "TURNO_DIA_HS,TURNO_DIA_HSMAX,TURNO_DIA_HAYCOMIDA," +
                                    "TURNO_DIA_HCS,TURNO_DIA_HCR," +
                                    "TURNO_DIA_HCTIEMPO,TURNO_DIA_HCTOLERA,TURNO_DIA_HTIEMPO," +
                                    "TURNO_DIA_PHEX,TURNO_DIA_NO_ASIS," +
                                    "TURNO_DIA_HERETARDO_B, TURNO_DIA_HERETARDO_C," +
                                    "TURNO_DIA_HERETARDO_D)" +
                                    "VALUES(" + turnoDiaID + "," + CeC_BD.SqlFechaHora(horas[0]) + "," +
                                    CeC_BD.SqlFechaHora(horas[1]) + "," + CeC_BD.SqlFechaHora(horas[2]) + "," +
                                    CeC_BD.SqlFechaHora(horas[3]) + "," + CeC_BD.SqlFechaHora(horas[4]) + "," +
                                    CeC_BD.SqlFechaHora(horas[5]) + "," + CeC_BD.SqlFechaHora(horas[6]) + "," + hayComida + "," +
                                    CeC_BD.SqlFechaHora(horas[7]) + "," + CeC_BD.SqlFechaHora(horas[8]) + "," +
                                    CeC_BD.SqlFechaHora(horas[9]) + "," + CeC_BD.SqlFechaHora(horas[10]) + "," + CeC_BD.SqlFechaHora(CeC_BD.TimeSpan2DateTime(TiempoXTrabajar)) + "," +
                                    tiempoEx + "," + NoAsistencia + "," +
                                    CeC_BD.SqlFechaHora(horas[3]) + "," + CeC_BD.SqlFechaHora(horas[3]) + "," +
                                    CeC_BD.SqlFechaHora(horas[3]) +
                                    ")";
                    break;
                case 2://Flexible

                    Comando = "INSERT INTO EC_TURNOS_DIA ( " +
                                    "TURNO_DIA_ID,TURNO_DIA_HEMIN," +
                                    "TURNO_DIA_HE,TURNO_DIA_HEMAX,TURNO_DIA_HERETARDO,TURNO_DIA_HSMIN,TURNO_DIA_HS," +
                                    "TURNO_DIA_HSMAX,TURNO_DIA_HAYCOMIDA," +
                                    "TURNO_DIA_HCTIEMPO,TURNO_DIA_HCTOLERA,TURNO_DIA_PHEX,TURNO_DIA_NO_ASIS," +
                                    "TURNO_DIA_HBLOQUE,TURNO_DIA_HBLOQUET,TURNO_DIA_HTIEMPO," +
                                    "TURNO_DIA_HERETARDO_B, TURNO_DIA_HERETARDO_C," +
                                    "TURNO_DIA_HERETARDO_D)" +
                                    ")" +
                                    "VALUES(" + turnoDiaID + "," + CeC_BD.SqlFechaHora(horas[0]) + "," +
                                    CeC_BD.SqlFechaHora(horas[1]) + "," + CeC_BD.SqlFechaHora(horas[2]) + "," +
                                    CeC_BD.SqlFechaHora(horas[3]) + "," + CeC_BD.SqlFechaHora(horas[4]) + "," +
                                    CeC_BD.SqlFechaHora(horas[5]) + "," + CeC_BD.SqlFechaHora(horas[6]) + "," + hayComida + "," +
                                    CeC_BD.SqlFechaHora(horas[7]) + "," + CeC_BD.SqlFechaHora(horas[8]) + "," +
                                    tiempoEx + "," + NoAsistencia + "," + CeC_BD.SqlFechaHora(horas[9]) + "," +
                                    CeC_BD.SqlFechaHora(horas[10]) + "," + CeC_BD.SqlFechaHora(horas[11]) + "," +
                                    CeC_BD.SqlFechaHora(horas[3]) + "," + CeC_BD.SqlFechaHora(horas[3]) + "," +
                                    CeC_BD.SqlFechaHora(horas[3]) +
                                    ")";
                    break;
                case 5://Simple
                    //Tiempo por Trabajar = Hora de salida - Hora de entrada
                    TiempoXTrabajar = horas[5] - horas[1];

                    Comando = "INSERT INTO EC_TURNOS_DIA (" +
                                    "TURNO_DIA_ID,TURNO_DIA_HEMIN," +
                                    "TURNO_DIA_HE,TURNO_DIA_HEMAX," +
                                    "TURNO_DIA_HERETARDO,TURNO_DIA_HSMIN," +
                                    "TURNO_DIA_HS, TURNO_DIA_HSMAX,TURNO_DIA_HAYCOMIDA," +
                                    "TURNO_DIA_HCTIEMPO,TURNO_DIA_HCTOLERA,TURNO_DIA_HTIEMPO," +
                                    "TURNO_DIA_PHEX,TURNO_DIA_NO_ASIS," +
                                    "TURNO_DIA_HERETARDO_B, TURNO_DIA_HERETARDO_C," +
                                    "TURNO_DIA_HERETARDO_D)" +
                                    "VALUES(" + turnoDiaID + "," + CeC_BD.SqlFechaHora(horas[0]) + "," +
                                    CeC_BD.SqlFechaHora(horas[1]) + "," + CeC_BD.SqlFechaHora(horas[2]) + "," +
                                    CeC_BD.SqlFechaHora(horas[3]) + "," + CeC_BD.SqlFechaHora(horas[4]) + "," +
                                    CeC_BD.SqlFechaHora(horas[5]) + "," + CeC_BD.SqlFechaHora(horas[6]) + "," + hayComida + "," +
                                    CeC_BD.SqlFechaHora(horas[7]) + "," + CeC_BD.SqlFechaHora(horas[8]) + "," + CeC_BD.SqlFechaHora(CeC_BD.TimeSpan2DateTime(TiempoXTrabajar)) + "," +
                                    tiempoEx + "," + NoAsistencia + "," +
                                    CeC_BD.SqlFechaHora(horas[3]) + "," + CeC_BD.SqlFechaHora(horas[3]) + "," +
                                    CeC_BD.SqlFechaHora(horas[3]) +
                                    ")";
                    break;
            }
            CeC_BD.EjecutaComando(Comando);
            return turnoDiaID;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("CeC_Turnos.AgregaTurnoDia", ex);
            return 0;
        }
    }
    public static int ActualizaTurno(int turnoID, int tipoTurno, string NombreTurno, int TiempoExtra, int TurnoAsistencia, int Sesion_ID)
    {
        try
        {
            if (CeC_BD.EjecutaComando("UPDATE EC_TURNOS SET TIPO_TURNO_ID = " + tipoTurno + ",TURNO_NOMBRE = '" + NombreTurno +
                 "',TURNO_ASISTENCIA = " + TurnoAsistencia + ",TURNO_PHEXTRAS = " + TiempoExtra +
                 " WHERE TURNO_ID =" + turnoID) > 0)
            {
                CeC_BD.EjecutaComando("UPDATE EC_TURNOS_SEMANAL_DIA SET TURNO_ID = 0 WHERE TURNO_ID = " + turnoID);
                {
                    if (Sesion_ID > 0)
                    {
                        CeC_Sesion.SAgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.EDICION, "Turno", turnoID, "Nombre = " + NombreTurno, Sesion_ID);
                        return 1;
                    }
                }
            }
            return 0;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("CeC_Turnos.ActualizaTurno", ex);
            return 0;
        }
    }


    public static string ObtenTurnoNombre(int Turno_ID)
    {
        return CeC_BD.EjecutaEscalarString("SELECT EC_TURNOS.TURNO_NOMBRE FROM EC_TURNOS WHERE  TURNO_ID = " + Turno_ID);
    }
    public static DataSet ObtenTurnosDSMenu(int Suscripcion_ID)
    {

        string ADD = " AND EC_TURNOS.TURNO_ID IN (";
        ADD += " SELECT     EC_AUTONUM.AUTONUM_TABLA_ID AS TURNO_ID " +
                "FROM         EC_AUTONUM" +
                " WHERE    (AUTONUM_TABLA = 'EC_TURNOS') AND (SUSCRIPCION_ID = " + Suscripcion_ID + "))";
        ADD += " AND (EC_TURNOS.TURNO_BORRADO = 0)";
        if (CeC_BD.EsOracle)
            return (DataSet)CeC_BD.EjecutaDataSet(@"SELECT EC_TURNOS.TURNO_ID, EC_TURNOS.TURNO_NOMBRE || ' (' || TURNOS.TURNO_DIA_HE || '-' ||  TURNOS.TURNO_DIA_HS || ')' AS TURNO_NOMBRE " +
                " FROM EC_TURNOS, EC_TIPO_TURNOS, (SELECT TURNO_ID, TO_CHAR(MIN(TURNO_DIA_HE), 'HH24:mi') AS TURNO_DIA_HE, TO_CHAR(MAX(TURNO_DIA_HS), 'HH24:mi') AS TURNO_DIA_HS  " +
                " FROM EC_TURNOS_DIA, EC_TURNOS_SEMANAL_DIA WHERE EC_TURNOS_DIA.TURNO_DIA_ID = EC_TURNOS_SEMANAL_DIA.TURNO_DIA_ID GROUP BY TURNO_ID) TURNOS WHERE  " +
                " EC_TURNOS.TIPO_TURNO_ID = EC_TIPO_TURNOS.TIPO_TURNO_ID AND EC_TURNOS.TURNO_ID = TURNOS.TURNO_ID" + ADD + " ORDER BY EC_TURNOS.TURNO_NOMBRE");

        return (DataSet)CeC_BD.EjecutaDataSet(@"SELECT EC_TURNOS.TURNO_ID, EC_TURNOS.TURNO_NOMBRE + ' (' " +
            "+ TURNOS.TURNO_DIA_HE + '-' +  TURNOS.TURNO_DIA_HS + ')' AS TURNO_NOMBRE FROM EC_TURNOS, EC_TIPO_TURNOS," +
            " (SELECT TURNO_ID, CONVERT(VARCHAR(5),MIN(TURNO_DIA_HE), 114) AS TURNO_DIA_HE, CONVERT(VARCHAR(5),MAX(TURNO_DIA_HS), 114) AS TURNO_DIA_HS " +
            "FROM EC_TURNOS_DIA, EC_TURNOS_SEMANAL_DIA WHERE EC_TURNOS_DIA.TURNO_DIA_ID = EC_TURNOS_SEMANAL_DIA.TURNO_DIA_ID GROUP BY TURNO_ID) TURNOS " +
            "WHERE EC_TURNOS.TIPO_TURNO_ID = EC_TIPO_TURNOS.TIPO_TURNO_ID AND EC_TURNOS.TURNO_ID = TURNOS.TURNO_ID" + ADD + " ORDER BY EC_TURNOS.TURNO_NOMBRE");
        string Qry = "" +
                    " SELECT     EC_TURNOS.TURNO_ID, EC_TURNOS.TURNO_NOMBRE + ' (' + TURNOS.TURNO_DIA_HE + '-' +  TURNOS.TURNO_DIA_HS + ')'" +
                    " FROM         EC_TURNOS INNER JOIN" +
                    " EC_TIPO_TURNOS ON EC_TURNOS.TIPO_TURNO_ID = EC_TIPO_TURNOS.TIPO_TURNO_ID LEFT OUTER JOIN" +
                    " (SELECT     EC_TURNOS_SEMANAL_DIA.TURNO_ID, CONVERT(VARCHAR(5), MIN(EC_TURNOS_DIA.TURNO_DIA_HE), 114) AS TURNO_DIA_HE, " +
                    " CONVERT(VARCHAR(5), MAX(EC_TURNOS_DIA.TURNO_DIA_HS), 114) AS TURNO_DIA_HS" +
                    " FROM          EC_TURNOS_DIA INNER JOIN" +
                    " EC_TURNOS_SEMANAL_DIA ON EC_TURNOS_DIA.TURNO_DIA_ID = EC_TURNOS_SEMANAL_DIA.TURNO_DIA_ID" +
                    " GROUP BY EC_TURNOS_SEMANAL_DIA.TURNO_ID) AS TURNOS ON EC_TURNOS.TURNO_ID = TURNOS.TURNO_ID" +
                    " WHERE     ";

        ADD = " EC_TURNOS.TURNO_ID IN (";
        ADD += " SELECT     EC_AUTONUM.AUTONUM_TABLA_ID AS TURNO_ID " +
                "FROM         EC_TURNOS,EC_AUTONUM" +
                " WHERE    TURNO_ID = AUTONUM_TABLA_ID AND (AUTONUM_TABLA = 'EC_TURNOS') AND (SUSCRIPCION_ID = " + Suscripcion_ID + "))";

        return (DataSet)CeC_BD.EjecutaDataSet(Qry + ADD + " AND (EC_TURNOS.TURNO_BORRADO = 0)" + " ORDER BY EC_TURNOS.TURNO_NOMBRE");
    }
    public static int ObtenTurnoID(string TurnoNombre, int Suscripcion_ID)
    {
        string SQL = "";
        if (!CeC_BD.EsOracle)
            SQL = " COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI ";
        string Qry = "SELECT TURNO_ID FROM EC_TURNOS WHERE TURNO_BORRADO = 0 AND "
            + CeC_Autonumerico.ValidaSuscripcion("EC_TURNOS", "TURNO_ID", Suscripcion_ID) + " AND " +
        " TURNO_NOMBRE LIKE '" + TurnoNombre.Trim() + "'" + SQL;
        return CeC_BD.EjecutaEscalarInt(Qry);
    }
    public static DataSet ObtenTurnosDS(int Suscripcion_ID)
    {
        return ObtenTurnosDS(Suscripcion_ID, false);
    }
    public static DataSet ObtenTurnosDS(int Suscripcion_ID, bool MostrarBorrados)
    {
        string Borrado = "";
        if (!MostrarBorrados)
            Borrado = " AND (EC_TURNOS.TURNO_BORRADO = 0)";

        string ADD;

        if (CeC_BD.EsOracle)
        {
            ADD = " AND EC_TURNOS.TURNO_ID IN (";
            ADD += " SELECT     EC_AUTONUM.AUTONUM_TABLA_ID AS TURNO_ID " +
                "FROM         EC_AUTONUM  " +
                " WHERE     (EC_AUTONUM.AUTONUM_TABLA = 'EC_TURNOS') AND (SUSCRIPCION_ID = " + Suscripcion_ID + "))";

            return (DataSet)CeC_BD.EjecutaDataSet(@"SELECT EC_TURNOS.TURNO_ID, EC_TURNOS.TURNO_NOMBRE, EC_TURNOS.TURNO_ASISTENCIA, EC_TIPO_TURNOS.TIPO_TURNO_NOMBRE, TURNOS.TURNO_DIA_HE," +
                " TURNOS.TURNO_DIA_HS FROM EC_TURNOS, EC_TIPO_TURNOS, (SELECT TURNO_ID, TO_CHAR(MIN(TURNO_DIA_HE), 'HH24:mi') AS TURNO_DIA_HE, TO_CHAR(MAX(TURNO_DIA_HS), 'HH24:mi') AS TURNO_DIA_HS FROM EC_TURNOS_DIA, " +
                "EC_TURNOS_SEMANAL_DIA WHERE EC_TURNOS_DIA.TURNO_DIA_ID = EC_TURNOS_SEMANAL_DIA.TURNO_DIA_ID GROUP BY TURNO_ID) TURNOS WHERE EC_TURNOS.TIPO_TURNO_ID = EC_TIPO_TURNOS.TIPO_TURNO_ID AND EC_TURNOS.TURNO_ID = TURNOS.TURNO_ID" + Borrado + ADD + " ORDER BY EC_TURNOS.TURNO_NOMBRE");
        }

        string Qry = "" +
                    " SELECT     EC_TURNOS.TURNO_ID, EC_TURNOS.TURNO_NOMBRE, EC_TURNOS.TURNO_ASISTENCIA, EC_TIPO_TURNOS.TIPO_TURNO_NOMBRE, " +
                    " TURNOS.TURNO_DIA_HE, TURNOS.TURNO_DIA_HS" +
                    " FROM         EC_TURNOS INNER JOIN" +
                    " EC_TIPO_TURNOS ON EC_TURNOS.TIPO_TURNO_ID = EC_TIPO_TURNOS.TIPO_TURNO_ID LEFT OUTER JOIN" +
                    " (SELECT     EC_TURNOS_SEMANAL_DIA.TURNO_ID, CONVERT(VARCHAR(5), MIN(EC_TURNOS_DIA.TURNO_DIA_HE), 114) AS TURNO_DIA_HE, " +
                    " CONVERT(VARCHAR(5), MAX(EC_TURNOS_DIA.TURNO_DIA_HS), 114) AS TURNO_DIA_HS" +
                    " FROM          EC_TURNOS_DIA INNER JOIN" +
                    " EC_TURNOS_SEMANAL_DIA ON EC_TURNOS_DIA.TURNO_DIA_ID = EC_TURNOS_SEMANAL_DIA.TURNO_DIA_ID" +
                    " GROUP BY EC_TURNOS_SEMANAL_DIA.TURNO_ID) AS TURNOS ON EC_TURNOS.TURNO_ID = TURNOS.TURNO_ID" +
                    " WHERE     ";

        ADD = " EC_TURNOS.TURNO_ID IN (";
        ADD += " SELECT     EC_AUTONUM.AUTONUM_TABLA_ID AS TURNO_ID " +
            "FROM         EC_AUTONUM " +
            " WHERE     (EC_AUTONUM.AUTONUM_TABLA = 'EC_TURNOS') AND (SUSCRIPCION_ID = " + Suscripcion_ID + "))";

        return (DataSet)CeC_BD.EjecutaDataSet(Qry + ADD + Borrado);
    }
    /// <summary>
    /// Obtiene el dataset de turnos y le agrega el turno predeterminado y el Descanso
    /// </summary>
    /// <param name="Usuario_ID"></param>
    /// <returns></returns>
    public static DataSet ObtenTurnosDSAgregado(int SuscripcionID)
    {
        try
        {
            DataSet DS = ObtenTurnosDS(SuscripcionID);
            DataRow DR = DS.Tables[0].NewRow();
            DR["TURNO_ID"] = -1;
            DR["TURNO_NOMBRE"] = "Descanso";
            DR["TURNO_ASISTENCIA"] = 1;
            DS.Tables[0].Rows.Add(DR);
            DR = DS.Tables[0].NewRow();
            DR["TURNO_ID"] = 0;
            DR["TURNO_NOMBRE"] = "Predeterminado";
            DR["TURNO_ASISTENCIA"] = 1;
            DS.Tables[0].Rows.Add(DR);
            return DS;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return null;
    }
    public static int ObtenNoPersonasSinTurno(int Usuario_ID)
    {
        return ObtenNoPersonasTurno(0, Usuario_ID);
    }
    public static int ObtenNoPersonasTurno(int Turno_ID, int Usuario_ID)
    {
        return CeC_BD.EjecutaEscalarInt("SELECT count(EC_PERSONAS.PERSONA_LINK_ID) " +
                " FROM EC_PERSONAS INNER JOIN " +
                " EC_PERMISOS_SUSCRIP ON EC_PERSONAS.SUSCRIPCION_ID = EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID " +
                " WHERE (EC_PERSONAS.PERSONA_BORRADO = 0) AND (EC_PERMISOS_SUSCRIP.USUARIO_ID = " +
                +Usuario_ID + ") AND EC_PERSONAS.Turno_ID = " + Turno_ID);
    }

    public static DataSet ObtenPersonasTurno(int Turno_ID, int Usuario_ID)
    {
        return (DataSet)CeC_BD.EjecutaDataSet("SELECT EC_PERSONAS.PERSONA_LINK_ID, EC_PERSONAS.PERSONA_NOMBRE, EC_PERSONAS.PERSONA_EMAIL, EC_PERSONAS.PERSONA_BORRADO, " +
                " EC_PERMISOS_SUSCRIP.USUARIO_ID " +
                " FROM EC_PERSONAS INNER JOIN " +
                " EC_PERMISOS_SUSCRIP ON EC_PERSONAS.SUSCRIPCION_ID = EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID " +
                " WHERE (EC_PERSONAS.PERSONA_BORRADO = 0) AND (EC_PERMISOS_SUSCRIP.USUARIO_ID = " +
                +Usuario_ID + ") AND EC_PERSONAS.Turno_ID = " + Turno_ID);
    }
    public static int ObtenCantidadTurnos(int SuscripcionID, bool MostrarBorrados)
    {
        DataSet DS = ObtenTurnosDS(SuscripcionID, MostrarBorrados);
        return DS.Tables[0].Rows.Count;
    }
    public static void CreaTurnosPredeterminados(int Usuario_ID, int SuscripcionID)
    {
        int TurnoID = -1;
        int turnodia = -1;
        if (ObtenCantidadTurnos(SuscripcionID, false) < 1)
        {
            // Lunes a Viernes de 9 a 6 
            TurnoID = AgregaTurno(5, "Lun-Vie 9 a 6", 0, 1, 0, 0, 0, SuscripcionID);
            DateTime[] Horas = new DateTime[9];
            Horas[0] = new DateTime(2006, 1, 1, 4, 00, 00);//Entrada Minima
            Horas[1] = new DateTime(2006, 1, 1, 9, 00, 00);//Entrada 
            Horas[2] = new DateTime(2006, 1, 1, 15, 00, 00); ;//Entrada Maxima
            Horas[3] = new DateTime(2006, 1, 1, 9, 01, 00);//Retardo
            Horas[4] = new DateTime(2006, 1, 1, 16, 00, 00);//Salida Minima
            Horas[5] = new DateTime(2006, 1, 1, 18, 00, 00);//Salida 
            Horas[6] = new DateTime(2006, 1, 2, 4, 00, 00);//Salida Maxima
            Horas[7] = new DateTime(2006, 1, 1, 1, 00, 00);//Tiene Comida
            Horas[8] = new DateTime(2006, 1, 1, 1, 01, 00);//Total Comida
            turnodia = AgregaTurnoDia(Horas, 0, 5, 0, 1, 0, SuscripcionID);
            for (int j = 2; j < 7; j++)
                CeC_Turnos.AgregaTurnoSemanalDia(turnodia, TurnoID, j, 0, SuscripcionID);
            // Lunes a Viernes de 9 a 6 y Sabado 9 a 2 
            TurnoID = AgregaTurno(1, "Lun-Vie 9 a 6, Sab 9 a 2", 0, 1, 0, 0, 0, SuscripcionID);
            for (int j = 2; j < 7; j++)
            {
                turnodia = AgregaTurnoDia(Horas, 0, 5, 0, 1, 0, SuscripcionID);
                CeC_Turnos.AgregaTurnoSemanalDia(turnodia, TurnoID, j, 0, SuscripcionID);
            }
            DateTime[] Horas1 = new DateTime[11];
            Horas1[0] = new DateTime(2006, 1, 1, 4, 00, 00);//Entrada Minima
            Horas1[1] = new DateTime(2006, 1, 1, 9, 00, 00);//Entrada 
            Horas1[2] = new DateTime(2006, 1, 1, 15, 00, 00); ;//Entrada Maxima
            Horas1[3] = new DateTime(2006, 1, 1, 9, 01, 00);//Retardo
            Horas1[4] = new DateTime(2006, 1, 1, 12, 00, 00);//Salida Minima
            Horas1[5] = new DateTime(2006, 1, 1, 14, 00, 00);//Salida 
            Horas1[6] = new DateTime(2006, 1, 2, 4, 00, 00);//Salida Maxima
            Horas1[7] = new DateTime(2006, 1, 1, 1, 00, 00);//Tiene Comida
            Horas1[8] = new DateTime(2006, 1, 1, 1, 01, 00);//Total Comida
            turnodia = AgregaTurnoDia(Horas1, 0, 5, 0, 1, 0, SuscripcionID);
            CeC_Turnos.AgregaTurnoSemanalDia(turnodia, TurnoID, 7, 0, SuscripcionID);
            // Lunes a Viernes de 9 a 7 
            TurnoID = AgregaTurno(5, "Lun-Vie 9 a 7", 0, 1, 0, 0, 0, SuscripcionID);
            DateTime[] Horas2 = new DateTime[11];
            Horas2[0] = new DateTime(2006, 1, 1, 4, 00, 00);//Entrada Minima
            Horas2[1] = new DateTime(2006, 1, 1, 9, 00, 00);//Entrada 
            Horas2[2] = new DateTime(2006, 1, 1, 15, 00, 00); ;//Entrada Maxima
            Horas2[3] = new DateTime(2006, 1, 1, 9, 01, 00);//Retardo
            Horas2[4] = new DateTime(2006, 1, 1, 17, 00, 00);//Salida Minima
            Horas2[5] = new DateTime(2006, 1, 1, 19, 00, 00);//Salida 
            Horas2[6] = new DateTime(2006, 1, 2, 4, 00, 00);//Salida Maxima
            Horas2[7] = new DateTime(2006, 1, 1, 1, 00, 00);//Tiene Comida
            Horas2[8] = new DateTime(2006, 1, 1, 1, 01, 00);//Total Comida
            turnodia = AgregaTurnoDia(Horas2, 0, 5, 0, 1, 0, SuscripcionID);
            for (int j = 2; j < 7; j++)
                CeC_Turnos.AgregaTurnoSemanalDia(turnodia, TurnoID, j, 0, SuscripcionID);

            //Flexible de 8 horas 
            TurnoID = AgregaTurno(2, "Flexible 8h dia", 0, 1, 0, 0, 0, SuscripcionID);
            DateTime[] Horas3 = new DateTime[12];
            Horas3[0] = new DateTime(2005, 1, 1, 5, 00, 00);//TURNO_DIA_HEMIN
            Horas3[1] = new DateTime(2006, 1, 1, 6, 00, 00);//TURNO_DIA_HE
            Horas3[2] = new DateTime(2006, 1, 1, 20, 00, 00);//TURNO_DIA_HEMAX
            Horas3[3] = new DateTime(2006, 1, 1, 11, 00, 00);//TURNO_DIA_HERETARDO
            Horas3[4] = new DateTime(2006, 1, 1, 14, 00, 00);//TURNO_DIA_HSMIN
            Horas3[5] = new DateTime(2006, 1, 1, 14, 00, 00);//TURNO_DIA_HS
            Horas3[6] = new DateTime(2006, 1, 2, 04, 45, 00);//TURNO_DIA_HSMAX
            Horas3[7] = new DateTime(2006, 1, 1, 1, 00, 00);//TURNO_DIA_HCTIEMPO
            Horas3[8] = new DateTime(2006, 1, 1, 0, 15, 00);//TURNO_DIA_HCTOLERA
            Horas3[9] = new DateTime(2006, 1, 1, 0, 30, 00);//TURNO_DIA_HBLOQUE
            Horas3[10] = new DateTime(2006, 1, 1, 0, 06, 00);//TURNO_DIA_HBLOQUET
            Horas3[11] = new DateTime(2006, 1, 1, 8, 00, 00);//TURNO_DIA_HTIEMPO
            turnodia = AgregaTurnoDia(Horas3, 0, 2, 0, 1, 0, SuscripcionID);
            for (int j = 2; j < 7; j++)
                CeC_Turnos.AgregaTurnoSemanalDia(turnodia, TurnoID, j, 0, SuscripcionID);
        }
    }
    /// <summary>
    /// Obtiene un dataset con el formato para importar la asignacion de turnos
    /// </summary>
    /// <returns></returns>
    public static DataSet ObtenDataSetImportaAsignacionTurnos()
    {
        return (DataSet)CeC_BD.EjecutaDataSet_Schema("SELECT 1 AS PERSONA_LINK_ID, TURNO_NOMBRE, " +
            CeC_BD.SqlFechaNula() + " AS FECHA_INICIAL, " + CeC_BD.SqlFechaNula() + " AS FECHA_FINAL FROM EC_TURNOS WHERE TURNO_ID = 0");
    }
    private void AgregaError(string Error, int Linea)
    {
        m_Errores = "Error en el Registro:" + Linea + " " + CeC.AgregaSeparador(m_Errores, Error, "<br>");
    }
    /// <summary>
    /// Importa y asigna los turnos por archivo.
    /// </summary>
    /// <param name="DS">DataSet con los valores del archivo.</param>
    /// <param name="Sesion">Sesion</param>
    /// <returns></returns>
    public int ImportaAsignacionTurnos(DataSet DS, CeC_Sesion Sesion)
    {
        m_Errores = "";
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return 0;
        int Importados = 0;
        int Linea = -1;
        foreach (DataRow DR in DS.Tables[0].Rows)
        {
            Linea++;
            try
            {
                int TurnoID = CeC_Turnos.ObtenTurnoID(CeC.Convierte2String(DR["TURNO_NOMBRE"]), Sesion.SUSCRIPCION_ID);
                // Obtenemos el PersonaID con el Numero de Empleado (Persona_Link_ID) y el Usuario que esta usando la Sesion actual (Valida tambien la Suscripciópn)
                int PersonaID = CeC_Personas.ObtenPersonaID(CeC.Convierte2Int(DR["PERSONA_LINK_ID"]), Sesion.USUARIO_ID);
                if (TurnoID < 0)
                {
                    AgregaError("Turno no Valido", Linea);
                    continue;
                }
                if (PersonaID < 0)
                {
                    AgregaError("Persona no Valida", Linea);
                    continue;
                }
                DateTime FechaInicio = CeC.Convierte2DateTime(CeC.Convierte2String(DR["FECHA_INICIAL"]));
                DateTime FechaFin = CeC.Convierte2DateTime(CeC.Convierte2String(DR["FECHA_FINAL"]));
                if (FechaInicio <= CeC_BD.FechaNula || FechaFin <= CeC_BD.FechaNula)
                {
                    AgregaError("Fecha Invalida", Linea);
                    continue;
                }
                if (FechaFin < FechaInicio)
                {
                    AgregaError("La fecha inicial no puede ser mayor a la final", Linea);
                    continue;
                }
                AsignaHorario(PersonaID, TurnoID, FechaInicio, FechaFin, Sesion);
                Importados++;
            }
            catch (Exception ex)
            {
                CIsLog2.AgregaError(ex);
            }
        }
        return Importados;
    }


    public static int AsignaHorarioPred(int Persona_ID, int Turno_ID, int SesionID)
    {
        if (Persona_ID < 0)
            return -1;
        if (SesionID > 0)
        {
            int Persona_Link_ID = CeC_Empleados.ObtenPersona_Link_ID(Persona_ID);
            string Descripcion = "";

            Descripcion += "NoEmpleado = " + Persona_Link_ID + ", TurnoID =" + Turno_ID + ", Turno = " + CeC_Turnos.ObtenTurnoNombre(Turno_ID);
            CeC_Sesion.SAgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.EDICION, "Turno Predeterminado", Persona_ID, Descripcion, SesionID);
        }
        return CeC_BD.EjecutaComando("UPDATE EC_PERSONAS SET TURNO_ID = " + Turno_ID + " where Persona_ID = " +
            Persona_ID);
    }


    public static int AsignaHorarioPred(int Persona_ID, int Turno_ID)
    {
        return AsignaHorarioPred(Persona_ID, Turno_ID, 0);
    }

    public static int AsignaHorarioPred(int Turno_ID, string PersonasIDs, string Agrupacion, int Usuario_ID, int SesionID)
    {
        string Validacion = CeC_Asistencias.ValidaAgrupacion(PersonasIDs, Usuario_ID, Agrupacion, true);
        if (SesionID > 0)
        {
            string Descripcion = "";
            Descripcion += "Turno = " + CeC_Turnos.ObtenTurnoNombre(Turno_ID) + " asignado a = " + Validacion;
            CeC_Sesion.SAgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.EDICION, "Turno Predeterminado V", Turno_ID, Descripcion, SesionID);
        }
        return CeC_BD.EjecutaComando("UPDATE EC_PERSONAS SET TURNO_ID = " + Turno_ID + " where " +
        Validacion);
    }
    public static int AsignaHorarioPred(int Turno_ID, int Persona_ID, string Agrupacion, int Usuario_ID, int SesionID)
    {
        return AsignaHorarioPred(Turno_ID, Persona_ID.ToString(), Agrupacion, Usuario_ID, SesionID);
    }


    public static int AsignaHorarioPred(int Turno_ID, int Persona_ID, string Agrupacion, int Usuario_ID)
    {
        return AsignaHorarioPred(Turno_ID, Persona_ID, Agrupacion, Usuario_ID, 0);
    }
    public static int AsignaHorario(string Persona_Diario_IDs, int Turno_ID, CeC_Sesion Sesion)
    {
        int Dias = 0;
        bool Validar = false;

        if (Sesion != null)
            if (!CeC_Periodos.PuedeModificarBloqueados(Sesion))
                Validar = true;


        string[] sPersonaDiarioIDs = CeC.ObtenArregoSeparador(Persona_Diario_IDs, ",");
        string Persona_Diario_IDsProcesados = "";
        string PersonasIDs = "";
        foreach (string sPersonaDiario in sPersonaDiarioIDs)
        {

            Dias++;
            int Persona_Diario_ID = CeC.Convierte2Int(sPersonaDiario);
            if (Validar)
                if (!CeC_Periodos.PuedeModificar(Persona_Diario_ID))
                {
                    Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.EDICION, "Turno Dia", Persona_Diario_ID, "No puede modificar días bloqueados");
                    continue;
                }
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.EDICION, "Turno Dia", Persona_Diario_ID, "Modificado correctamente");
            Persona_Diario_IDsProcesados = CeC.AgregaSeparador(Persona_Diario_IDsProcesados, Persona_Diario_ID.ToString(), ",");
            int PersonaID = CeC_Asistencias.ObtenPersonaID(Persona_Diario_ID);
            PersonasIDs = CeC.AgregaSeparador(PersonasIDs, PersonaID.ToString(), ",");
            DateTime Fecha = CeC_Asistencias.ObtenFecha(Persona_Diario_ID);
            int TurnoDia = ObtenTurnoDiaID(Turno_ID, Fecha);
            AsignaTurnoDia(Persona_Diario_ID, TurnoDia);
        }
        CeC_Asistencias.RecalculaAsistencia(Persona_Diario_IDs, PersonasIDs);
/*        CeC_Asistencias.LimpiaPersonasDiario(Persona_Diario_IDs);
        CeC_Asistencias.RecalculaAccesos(Persona_Diario_IDs);
        CeC_Asistencias.ProcesaFaltas(PersonasIDs, DateTime.Today);*/
        return Dias;
    }

    public static int AsignaTurnoDia(int Persona_Diario_ID, int Turno_Dia_ID)
    {
        CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_DIARIO SET PERSONA_DIARIO_TES = " +
        													" (SELECT TURNO_DIA_HTIEMPO " +
													        " FROM EC_TURNOS_DIA " +
													        " WHERE TURNO_DIA_ID = " + Turno_Dia_ID + ") " +
                               "WHERE PERSONA_DIARIO_ID = " + Persona_Diario_ID);
        return CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_diario set turno_dia_id = " + Turno_Dia_ID + " where persona_diario_ID = " + Persona_Diario_ID);
        
        //return CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_diario set PERSONA_DIARIO_TES = " + + TURNO_DIA_HTIEMPO
    }

    public static int AsignaHorario(int Persona_Diario_ID, int Turno_ID, CeC_Sesion Sesion)
    {
        return AsignaHorario(Persona_Diario_ID.ToString(), Turno_ID, Sesion);
    }


    public static int AsignaHorario(int Persona_ID, int Turno_ID, DateTime FechaHora, CeC_Sesion Sesion)
    {
        //int TurnoDia = ObtenTurnoDiaID(Turno_ID, FechaHora);
        int PersonaDiarioID = CeC_Asistencias.ObtenPersonaDiarioID(Persona_ID, FechaHora);
        return AsignaHorario(PersonaDiarioID, Turno_ID, Sesion);
        //return CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_diario set turno_dia_id = " + TurnoDia + " where persona_diario_fecha = " + CeC_BD.SqlFecha(FechaHora) + "  AND persona_id = " + Persona_ID);

    }
    /*    public static int AsignaHorario(int Persona_ID, int Turno_ID, DateTime FechaInicio, DateTime FechaFin)
        {
            return AsignaHorario(Persona_ID, Turno_ID, FechaInicio, FechaFin, null);
        }*/
    /// <summary>
    /// Valida que tenga permisos de asignación de horario y a continuación lo asigna
    /// </summary>
    /// <param name="Persona_ID"></param>
    /// <param name="Turno_ID"></param>
    /// <param name="FechaInicio"></param>
    /// <param name="FechaFin"></param>
    /// <param name="Sesion"></param>
    /// <returns></returns>
    public static int AsignaHorario(int Persona_ID, int Turno_ID, DateTime FechaInicio, DateTime FechaFin, CeC_Sesion Sesion)
    {
        return AsignaHorario(Persona_ID, Turno_ID, FechaInicio, FechaFin, Sesion, true);
    }

    public static int AsignaHorario(int Persona_ID, int Turno_ID, DateTime FechaInicio, DateTime FechaFin, CeC_Sesion Sesion, bool ValidaPermisos)
    {
        int Dias = 0;
        string PersonasDiarioIDS = "";
        DateTime Fecha = FechaInicio;
        while (Fecha <= FechaFin)
        {
            PersonasDiarioIDS = CeC.AgregaSeparador(PersonasDiarioIDS, CeC_Asistencias.ObtenPersonaDiarioID(Persona_ID, Fecha).ToString(), ",");
            Fecha = Fecha.AddDays(1);
        }
        return AsignaHorario(PersonasDiarioIDS, Turno_ID, Sesion);
    }
    public static int ObtenTurnoDiaID(int Turno_ID, DateTime FechaHora)
    {
        int DiaSemana = Convert.ToInt32(FechaHora.DayOfWeek);
        DiaSemana++;
        int Turno_Dia = 0;
        if (Turno_ID > 0)
        {
            //        int Turno_Dia = CeC_BD.EjecutaEscalarInt("SELECT  turno_dia_id FROM EC_PERSONAS_diario  where persona_diario_fecha = " + CeC_BD.SqlFecha(FechaHora) + " AND persona_id = " + Persona_ID);
            Turno_Dia = CeC_BD.EjecutaEscalarInt("select TURNO_DIA_ID from EC_TURNOs_semanal_Dia where EC_TURNOs_semanal_Dia.turno_id  = " + Turno_ID + " and dia_semana_id = " + DiaSemana);
            if (Turno_Dia <= 0)
            {
                /*			Turno_Dia = CeC_BD.EjecutaEscalarInt("SELECT     EC_TURNOS_SEMANAL_DIA.TURNO_DIA_ID, EC_TURNOS_SEMANAL_DIA.DIA_SEMANA_ID, EC_TURNOS_DIARIOS.TURNO_ID,  " +
                                " EC_TURNOS_DIARIOS.TURNO_DIARIO_FECHA, EC_PERSONAS.PERSONA_ID " +
                                " FROM         EC_TURNOS, EC_TURNOS_DIARIOS, EC_TURNOS EC_TURNOS_1, EC_TURNOS_SEMANAL_DIA, EC_PERSONAS " +
                                " WHERE     EC_TURNOS.TURNO_ID = EC_TURNOS_DIARIOS.TURNO_ID AND EC_TURNOS_DIARIOS.TURNO_HIJO_ID = EC_TURNOS_1.TURNO_ID AND  " + 
                                " EC_TURNOS_1.TURNO_ID = EC_TURNOS_SEMANAL_DIA.TURNO_ID AND EC_TURNOS.TURNO_ID = EC_PERSONAS.TURNO_ID " +
                                " AND EC_TURNOS_SEMANAL_DIA.DIA_SEMANA_ID = " + DiaSemana + " AND EC_PERSONAS.PERSONA_ID = " + Persona_ID + " AND EC_TURNOS_SEMANAL_DIA.DIA_SEMANA_ID = " 
                                + DiaSemana + " AND EC_TURNOS_DIARIOS.TURNO_DIARIO_FECHA = " + CeC_BD.SqlFecha(FechaHora));
                            if (Turno_Dia <= 0)
                            {
                                Turno_Dia = -1;
                            }*/

                Turno_Dia = -1;
            }
        }
        else
            Turno_Dia = Turno_ID;
        return Turno_Dia;
    }
    /// <summary>
    /// Asigna un Horario a un día, tomando el horario predeterminado
    /// </summary>
    /// <param name="Persona_ID"></param>
    /// <param name="FechaHora"></param>
    /// <returns></returns>
    public static int AsignaHorario(int Persona_ID, DateTime FechaHora)
    {
        int DiaSemana = Convert.ToInt32(FechaHora.DayOfWeek);
        DiaSemana++;
        int Turno_Dia = 0;
        //        int Turno_Dia = CeC_BD.EjecutaEscalarInt("SELECT  turno_dia_id FROM EC_PERSONAS_diario  where persona_diario_fecha = " + CeC_BD.SqlFecha(FechaHora) + " AND persona_id = " + Persona_ID);
        Turno_Dia = CeC_BD.EjecutaEscalarInt("select TURNO_DIA_ID from EC_PERSONAS, EC_TURNOs_semanal_Dia where EC_PERSONAS.turno_id = EC_TURNOs_semanal_Dia.turno_id and persona_id = " + Persona_ID + " and dia_semana_id = " + DiaSemana);
        if (Turno_Dia <= 0)
        {
            Turno_Dia = CeC_BD.EjecutaEscalarInt("SELECT     EC_TURNOS_SEMANAL_DIA.TURNO_DIA_ID, EC_TURNOS_SEMANAL_DIA.DIA_SEMANA_ID, EC_TURNOS_DIARIOS.TURNO_ID,  " +
                " EC_TURNOS_DIARIOS.TURNO_DIARIO_FECHA, EC_PERSONAS.PERSONA_ID " +
                " FROM         EC_TURNOS, EC_TURNOS_DIARIOS, EC_TURNOS EC_TURNOS_1, EC_TURNOS_SEMANAL_DIA, EC_PERSONAS " +
                " WHERE     EC_TURNOS.TURNO_ID = EC_TURNOS_DIARIOS.TURNO_ID AND EC_TURNOS_DIARIOS.TURNO_HIJO_ID = EC_TURNOS_1.TURNO_ID AND  " +
                " EC_TURNOS_1.TURNO_ID = EC_TURNOS_SEMANAL_DIA.TURNO_ID AND EC_TURNOS.TURNO_ID = EC_PERSONAS.TURNO_ID " +
                " AND EC_TURNOS_SEMANAL_DIA.DIA_SEMANA_ID = " + DiaSemana + " AND EC_PERSONAS.PERSONA_ID = " + Persona_ID + " AND EC_TURNOS_SEMANAL_DIA.DIA_SEMANA_ID = "
                + DiaSemana + " AND EC_TURNOS_DIARIOS.TURNO_DIARIO_FECHA = " + CeC_BD.SqlFecha(FechaHora));
            if (Turno_Dia <= 0)
            {
                Turno_Dia = -1;
            }
        }
        return CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_diario set turno_dia_id = " + Turno_Dia + " where persona_diario_fecha = " + CeC_BD.SqlFecha(FechaHora) + " AND turno_dia_id = 0 AND persona_id = " + Persona_ID);

    }


}
