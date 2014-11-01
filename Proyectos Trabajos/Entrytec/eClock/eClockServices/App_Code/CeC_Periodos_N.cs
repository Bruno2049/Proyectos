using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

/// <summary>
/// Descripción breve de CeC_Periodos_N
/// </summary>
public class CeC_Periodos_N
{

    public int PERIODO_N_ID;
    public string PERIODO_N_NOM = "";
    public string PERIODO_N_DESC = "";
    public int TIMESPAN_ID;
    public DateTime PERIODO_N_FECHA;
    public string PERIODO_N_PERSONAS = "";
    public int PERIODO_N_DASIS = 0;
    public bool PERIODO_N_BORRADO = false;

    public CeC_Periodos_N(int Periodo_N_ID)
    {

        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet("SELECT PERIODO_N_ID, PERIODO_N_NOM, PERIODO_N_DESC, TIMESPAN_ID, PERIODO_N_FECHA, PERIODO_N_PERSONAS, PERIODO_N_DASIS, PERIODO_N_BORRADO FROM EC_PERIODOS_N WHERE PERIODO_N_ID = " + Periodo_N_ID);
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return;
        DataRow DR = DS.Tables[0].Rows[0];
        PERIODO_N_ID = CeC.Convierte2Int(DR["PERIODO_N_ID"]);
        PERIODO_N_NOM = CeC.Convierte2String(DR["PERIODO_N_NOM"]);
        PERIODO_N_DESC = CeC.Convierte2String(DR["PERIODO_N_DESC"]);
        TIMESPAN_ID = CeC.Convierte2Int(DR["TIMESPAN_ID"]);
        PERIODO_N_FECHA = CeC.Convierte2DateTime(DR["PERIODO_N_FECHA"]);
        try
        {
            PERIODO_N_PERSONAS = CeC.Convierte2String(DR["PERIODO_N_PERSONAS"]);

            PERIODO_N_DASIS = CeC.Convierte2Int(DR["PERIODO_N_DASIS"]);

            PERIODO_N_BORRADO = Convert.ToBoolean(DR["PERIODO_N_BORRADO"]);
        }
        catch { }
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public static int ObtenPeriodoN(string PERIODO_N_NOM, int SuscripcionID)
    {
        return CeC_BD.EjecutaEscalarInt("SELECT PERIODO_N_ID FROM EC_PERIODOS_N WHERE PERIODO_N_NOM = '" + PERIODO_N_NOM + "' AND " +
        CeC_Autonumerico.ValidaSuscripcion("EC_PERIODOS_N", "PERIODO_N_ID", SuscripcionID));
    }
    public static int AgregaPeriodoN(string PERIODO_N_NOM, string PERIODO_N_DESC, int TIMESPAN_ID,
    DateTime PERIODO_N_FECHA, string PERIODO_N_PERSONAS, int PERIODO_N_DASIS, int SesionID, int SuscripcionID)
    {
        int PERIODO_N_ID = ObtenPeriodoN(PERIODO_N_NOM, SuscripcionID);
        if (PERIODO_N_ID > 0)
            return PERIODO_N_ID;

        PERIODO_N_ID = CeC_Autonumerico.GeneraAutonumerico("EC_PERIODOS_N", "PERIODO_N_ID", SesionID, SuscripcionID);

        string Qry = "INSERT INTO EC_PERIODOS_N (" +
            "PERIODO_N_ID, PERIODO_N_NOM, PERIODO_N_DESC, TIMESPAN_ID," +
            " PERIODO_N_FECHA, PERIODO_N_PERSONAS, PERIODO_N_DASIS, PERIODO_N_BORRADO) VALUES(" +
            PERIODO_N_ID + ",'" + PERIODO_N_NOM + "','" + PERIODO_N_DESC + "'," + TIMESPAN_ID +
            "," + CeC_BD.SqlFecha(PERIODO_N_FECHA) + ",'" + CeC_BD.ObtenParametroCadena(PERIODO_N_PERSONAS) + "'," + PERIODO_N_DASIS + ",0)";

        if (CeC_BD.EjecutaComando(Qry) > 0)
            return PERIODO_N_ID;
        return -999;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="EstadoPeriodoID">Si es -1 significa que traera a todos</param>
    /// <param name="SuscripcionID"></param>
    /// <returns></returns>
    public static DataSet ObtenPeriodosDetalle(DateTime Desde, DateTime Hasta, int EstadoPeriodo, int SuscripcionID)
    {

        string Qry = " SELECT PERIODO_ID, PERIODO_NO, TIPO_NOMINA_NOMBRE, PERIODO_NOM_INICIO, PERIODO_NOM_FIN, PERIODO_ASIS_INICIO, PERIODO_ASIS_FIN, EDO_PERIODO_ID, EC_TIPO_NOMINA.PERIODO_N_ID " +
            " FROM EC_TIPO_NOMINA, EC_PERIODOS_N, EC_PERIODOS WHERE EC_TIPO_NOMINA.PERIODO_N_ID = EC_PERIODOS_N.PERIODO_N_ID AND EC_PERIODOS_N.PERIODO_N_ID = EC_PERIODOS.PERIODO_N_ID " +
            " AND PERIODO_ASIS_FIN <= " + CeC_BD.SqlFecha(Hasta) + " AND PERIODO_ASIS_INICIO >= " + CeC_BD.SqlFecha(Desde) + " AND EDO_PERIODO_ID = " + EstadoPeriodo +
            " AND " + CeC_BD.ObtenQryValidaSuscripcion("EC_TIPO_NOMINA", "TIPO_NOMINA_ID", SuscripcionID) +
            " \n ORDER BY PERIODO_NOM_INICIO ";
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }

    /// <summary>
    /// periodo nombre id para horas extras
    /// </summary>
    public static int PeriodoNID_HorasExtras()
    {
        return 1;
    }
}
