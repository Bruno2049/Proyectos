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
/// Descripción breve de CeC_Periodos
/// </summary>
public class CeC_Periodos : CeC_Tabla
{
    int m_Periodo_Id = 0;
    [Description("Identificador del periodo")]
    [DisplayNameAttribute("Periodo_Id")]
    public int Periodo_Id
    { get { return m_Periodo_Id; } set { m_Periodo_Id = value; } }
    int m_Periodo_N_Id = 0;
    [Description("Indica el tipo de acceso que genera la terminal, entrada, salida o desconocido")]
    [DisplayNameAttribute("Periodo_N_Id")]
    public int Periodo_N_Id { get { return m_Periodo_N_Id; } set { m_Periodo_N_Id = value; } }
    int m_Edo_Periodo_Id = 0;
    [Description("Nombre de la teminal (sirve para ubicar el equipo en las instalaciones)")]
    [DisplayNameAttribute("Edo_Periodo_Id")]
    public int Edo_Periodo_Id { get { return m_Edo_Periodo_Id; } set { m_Edo_Periodo_Id = value; } }
    DateTime m_Periodo_Nom_Inicio = CeC_BD.FechaNula;
    [Description("Fecha de inicio de periodo de nomina")]
    [DisplayNameAttribute("Periodo_Nom_Inicio")]
    public DateTime Periodo_Nom_Inicio { get { return m_Periodo_Nom_Inicio; } set { m_Periodo_Nom_Inicio = value; } }
    DateTime m_Periodo_Nom_Fin = CeC_BD.FechaNula;
    [Description("Fecha de Fin de periodo de nomina")]
    [DisplayNameAttribute("Periodo_Nom_Fin")]
    public DateTime Periodo_Nom_Fin { get { return m_Periodo_Nom_Fin; } set { m_Periodo_Nom_Fin = value; } }
    DateTime m_Periodo_Asis_Inicio = CeC_BD.FechaNula;
    [Description("Fecha de inicio de Asistencia en el periodo")]
    [DisplayNameAttribute("Periodo_Asis_Inicio")]
    public DateTime Periodo_Asis_Inicio { get { return m_Periodo_Asis_Inicio; } set { m_Periodo_Asis_Inicio = value; } }
    DateTime m_Periodo_Asis_Fin = CeC_BD.FechaNula;
    [Description("Fecha de fin de asistencia en el periodo")]
    [DisplayNameAttribute("Periodo_Asis_Fin")]
    public DateTime Periodo_Asis_Fin { get { return m_Periodo_Asis_Fin; } set { m_Periodo_Asis_Fin = value; } }
    int m_Periodo_Ano = 0;
    [Description("Año del periodo ej 2011")]
    [DisplayNameAttribute("Periodo_Ano")]
    public int Periodo_Ano { get { return m_Periodo_Ano; } set { m_Periodo_Ano = value; } }
    int m_Periodo_No = 0;
    [Description("Numero de periodo")]
    [DisplayNameAttribute("Periodo_No")]
    public int Periodo_No { get { return m_Periodo_No; } set { m_Periodo_No = value; } }

    public CeC_Periodos(CeC_Sesion Sesion)
        : base("EC_PERIODOS", "PERIODO_ID", Sesion)
    { }

    public CeC_Periodos(int PeriodoID, CeC_Sesion Sesion)
        : base("EC_PERIODOS", "PERIODO_ID", Sesion)
    {
        Carga(PeriodoID.ToString(), Sesion);
    }

    public enum EDO_PERIODO
    {
        No_procesado = 0,
        Actual,
        Cerrado
    }

    /// <summary>
    /// Agrega o actualiza los datos de un periodo
    /// </summary>
    /// <param name="PeriodoId">Identificador único para el período. Es autonumérico.</param>
    /// <param name="PeriodoNId">Identificador único para el nombre del período</param>
    /// <param name="EdoPeriodoId">Identificador único para el estado de un período</param>
    /// <param name="PeriodoNomInicio">Fecha de inicio del período de nómina</param>
    /// <param name="PeriodoNomFin">Fecha de fin del paríodo de nómina</param>
    /// <param name="PeriodoAsisInicio">Fecha de inicio del período de asistencias</param>
    /// <param name="PeriodoAsisFin">Fecha de fin del período de asistencias</param>
    /// <param name="PeriodoAno">Año del periodo</param>
    /// <param name="PeriodoNo">Número del periodo en el año</param>
    /// <param name="Sesion">Sesión actual en el sistema</param>
    /// <returns>Vedadero si se pudo guardar correctamente los datos. Falso en caso de que ocurra algún problema o error al guardar los datos</returns>
    public bool Actualiza(int PeriodoId, int PeriodoNId, int EdoPeriodoId, DateTime PeriodoNomInicio, DateTime PeriodoNomFin, DateTime PeriodoAsisInicio, DateTime PeriodoAsisFin, int PeriodoAno, int PeriodoNo,
        CeC_Sesion Sesion)
    {
        try
        {
            bool Nuevo = false;
            if (!Carga(PeriodoId.ToString(), Sesion))
                Nuevo = true;
            m_EsNuevo = Nuevo;
            Periodo_Id = PeriodoId;
            Periodo_N_Id = PeriodoNId;
            Edo_Periodo_Id = EdoPeriodoId;
            Periodo_Nom_Inicio = PeriodoNomInicio;
            Periodo_Nom_Fin = PeriodoNomFin;
            Periodo_Asis_Inicio = PeriodoAsisInicio;
            Periodo_Asis_Fin = PeriodoAsisFin;
            Periodo_Ano = PeriodoAno;
            Periodo_No = PeriodoNo;
            if (Guarda(Sesion))
            {
                return true;
            }
        }
        catch { }
        return false;
    }

    public static int AgregaPeriodo(int PERIODO_N_ID, EDO_PERIODO EDO_PERIODO_ID, DateTime PERIODO_NOM_INICIO,
        DateTime PERIODO_NOM_FIN, DateTime PERIODO_ASIS_INICIO, DateTime PERIODO_ASIS_FIN,
        int PERIODO_ANO, int PERIODO_NO, int SesionID, int SuscripcionID)
    {
        return AgregaPeriodo(PERIODO_N_ID, Convert.ToInt32(EDO_PERIODO_ID), PERIODO_NOM_INICIO, PERIODO_NOM_FIN,
            PERIODO_ASIS_INICIO, PERIODO_ASIS_FIN, PERIODO_ANO, PERIODO_NO, SesionID, SuscripcionID);
    }
    public static int AgregaPeriodo(int PERIODO_N_ID, int EDO_PERIODO_ID, DateTime PERIODO_NOM_INICIO,
        DateTime PERIODO_NOM_FIN, DateTime PERIODO_ASIS_INICIO, DateTime PERIODO_ASIS_FIN,
        int PERIODO_ANO, int PERIODO_NO, int SesionID, int SuscripcionID)
    {

        int ID = CeC_BD.EjecutaEscalarInt("SELECT PERIODO_ID FROM EC_PERIODOS WHERE PERIODO_N_ID = " + PERIODO_N_ID + " AND PERIODO_ANO = " + PERIODO_ANO + " AND PERIODO_NO = " + PERIODO_NO);
        if (ID > 0)
            return ID;
        ID = CeC_Autonumerico.GeneraAutonumerico("EC_PERIODOS", "PERIODO_ID", SesionID, SuscripcionID);
        string Qry = "INSERT INTO EC_PERIODOS (PERIODO_ID, PERIODO_N_ID, EDO_PERIODO_ID," +
            " PERIODO_NOM_INICIO, PERIODO_NOM_FIN, PERIODO_ASIS_INICIO, PERIODO_ASIS_FIN, PERIODO_ANO, PERIODO_NO) VALUES("
            + ID + "," + PERIODO_N_ID + "," + EDO_PERIODO_ID + "," +
            CeC_BD.SqlFecha(PERIODO_NOM_INICIO) + "," + CeC_BD.SqlFecha(PERIODO_NOM_FIN) + "," +
            CeC_BD.SqlFecha(PERIODO_ASIS_INICIO) + "," + CeC_BD.SqlFecha(PERIODO_ASIS_FIN) + "," +
            PERIODO_ANO + "," + PERIODO_NO + ")";
        if (CeC_BD.EjecutaComando(Qry) > 0)
            return ID;
        return -999;



    }

    public static int AgregaPeriodoBloqueado(DateTime FechaInicio, DateTime FechaFinal, int SesionID, int SuscripcionID)
    {
        int ID = CeC_Autonumerico.GeneraAutonumerico("EC_PERIODOS", "PERIODO_ID", SesionID, SuscripcionID);
        if (CeC_BD.EjecutaComando("INSERT INTO EC_PERIODOS (PERIODO_ID, EDO_PERIODO_ID, PERIODO_ASIS_INICIO, PERIODO_ASIS_FIN) VALUES(" +
            ID + ", " + Convert.ToInt32(EDO_PERIODO.Cerrado) + ", " + CeC_BD.SqlFecha(FechaInicio) + ", " + CeC_BD.SqlFecha(FechaFinal) + ")") > 0)
            return ID;
        return -9999;
    }

    public static bool CambiaEstado(int PERIODO_ID, EDO_PERIODO Estado)
    {
        if (CeC_BD.EjecutaComando(" UPDATE EC_PERIODOS SET EDO_PERIODO_ID = " + Convert.ToInt32(Estado) + " WHERE PERIODO_ID = " + PERIODO_ID) > 0)
            return true;
        return false;
    }
    public static bool PuedeModificar(int PersonaDiarioID)
    {
        return PuedeModificar(PersonaDiarioID.ToString());
    }
    public static bool PuedeModificarAlmacenInc(string AlmacenIncIds)
    {
        if (AlmacenIncIds == "")
            return true;
        int NoRegistros =
            CeC_BD.EjecutaEscalarInt(" SELECT     EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID " +
                                        " FROM         EC_PERSONAS_DIARIO INNER JOIN " +
                                        " EC_PERIODOS ON EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA >= EC_PERIODOS.PERIODO_ASIS_INICIO AND  " +
                                        " EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA <= EC_PERIODOS.PERIODO_ASIS_FIN INNER JOIN " +
                                        " EC_ALMACEN_INC ON EC_PERSONAS_DIARIO.PERSONA_ID = EC_ALMACEN_INC.PERSONA_ID AND  " +
                                        " EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA = EC_ALMACEN_INC.ALMACEN_INC_FECHA " +
                                        " WHERE     (EC_PERIODOS.EDO_PERIODO_ID = 2) AND (EC_ALMACEN_INC.ALMACEN_INC_ID IN (" + AlmacenIncIds + ")) ");
        if (NoRegistros > 0)
            return false;
        return true;

    }
    public static bool PuedeModificar(string PersonaDiariosIDs)
    {
        if (PersonaDiariosIDs == "")
            return true;
        string Qry = " SELECT EC_PERIODOS_N.PERIODO_N_PERSONAS " +
                        " FROM  EC_PERSONAS_DIARIO INNER JOIN " +
                        " EC_PERIODOS ON EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA >= EC_PERIODOS.PERIODO_ASIS_INICIO AND  " +
                        " EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA <= EC_PERIODOS.PERIODO_ASIS_FIN INNER JOIN " +
                        " EC_PERIODOS_N ON EC_PERIODOS.PERIODO_N_ID = EC_PERIODOS_N.PERIODO_N_ID " +
                        " WHERE EC_PERIODOS.EDO_PERIODO_ID = 2 AND EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID IN (" + PersonaDiariosIDs + ")";
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry);
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return true;
        foreach (DataRow DR in DS.Tables[0].Rows)
        {

            int NoRegistros = CeC_BD.EjecutaEscalarInt("SELECT     COUNT(PERSONA_DIARIO_ID) AS NoRegistros " +
             "FROM         EC_PERSONAS_DIARIO WHERE PERSONA_DIARIO_ID in (" + PersonaDiariosIDs + ") " +
             "AND PERSONA_ID IN (" + CeC.Convierte2String(DR["PERIODO_N_PERSONAS"]) + ")");
            if (NoRegistros > 0)
                return false;

        }
        return true;
    }
    public static EDO_PERIODO ObtenEstado(int PeriodoID)
    {
        try
        {
            return (EDO_PERIODO)CeC_BD.EjecutaEscalarInt("SELECT EDO_PERIODO_ID FROM EC_PERIODOS WHERE PERIODO_ID = " + PeriodoID);
        }
        catch { }
        return EDO_PERIODO.No_procesado;
    }

    public static DateTime ObtenPeriodoNomInicio(int PeriodoID)
    {
        return CeC_BD.EjecutaEscalarDateTime("SELECT MIN(PERIODO_NOM_INICIO) FROM EC_PERIODOS WHERE PERIODO_ID = " + PeriodoID);
    }


    public static bool GeneraPeriodo(int Periodo_N_ID, DateTime Desde, DateTime Hasta, int CalendariosDFID, int SesionID, int SuscripcionID)
    {
        try
        {
            Desde = Desde.Date;
            Hasta = Hasta.Date;
            CeC_Periodos_N PeriodosN = new CeC_Periodos_N(Periodo_N_ID);
            if (Periodo_N_ID <= 0)
                return false;
            CeC_TimeSpan TS = new CeC_TimeSpan(PeriodosN.TIMESPAN_ID);
            DateTime Ultimo = CeC_BD.EjecutaEscalarDateTime("SELECT MAX(PERIODO_NOM_FIN) FROM EC_PERIODOS WHERE PERIODO_N_ID = " + Periodo_N_ID);
            if (Ultimo >= Hasta)
                return false;
            if (Ultimo < Desde)
                Ultimo = Desde;
            int PeriodoNO = 0;
            int Ano = 0;
            while (Ultimo <= Hasta)
            {

                DateTime Inicio = Ultimo;
                Ultimo = TS.ObtenNuevaFecha(Ultimo, CalendariosDFID, -1);
                DateTime Fin = Ultimo.AddDays(-1);
                DateTime InicioAsis = Inicio.AddDays(-PeriodosN.PERIODO_N_DASIS);
                DateTime FinAsis = Fin.AddDays(-PeriodosN.PERIODO_N_DASIS);
                if (Ano != InicioAsis.Year)
                {
                    Ano = InicioAsis.Year;
                    PeriodoNO = 1;
                }
                AgregaPeriodo(Periodo_N_ID, EDO_PERIODO.No_procesado, Inicio, Fin, InicioAsis, FinAsis, InicioAsis.Year, PeriodoNO++, SesionID, SuscripcionID);
            }
            return true;
        }
        catch { }
        return false;
    }

    public static bool GeneraPredeterminados()
    {
        return GeneraPeriodo(1, new DateTime(2009, 12, 28), new DateTime(2015, 01, 01), -9999, 0, 0);

    }
    public static bool PuedeModificarBloqueados(CeC_Sesion Sesion)
    {
        if (CeC.ExisteEnSeparador(Sesion.ConfiguraSuscripcion.UsuariosIdsModificanFechasBloqueadas, Sesion.USUARIO_ID.ToString(), ","))
            return true;
        return false;
    }

    public static DataSet ObtenPeriodoMenu(int SuscripcionID)
    {
        string ADD = "";

        if (SuscripcionID > 1)
            ADD = " AND EC_PERIODOS." + CeC_Autonumerico.ValidaSuscripcion("EC_PERIODOS", "PERIODO_ID", SuscripcionID);
        return (DataSet)CeC_BD.EjecutaDataSet(
            "SELECT EC_PERIODOS.PERIODO_ID,  EC_PERIODOS.PERIODO_N_ID FROM EC_PERIODOS" + ADD);
    }
}
