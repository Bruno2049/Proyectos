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
using System.Collections.Generic;

/// <summary>
/// Descripción breve de CeC_IncidenciasRegla
/// </summary>
public class CeC_IncidenciasRegla
{
    public int TIPO_INCIDENCIA_R_ID = -1;
    public int TIMESPAN_ID = 0;
    public CeC_TimeSpan TS;
    public int TIPO_INCIDENCIA_ID = 0;
    public bool TIPO_INCIDENCIA_R_CNF = false;
    public string TIPO_INCIDENCIA_R_PER = "";
    public bool TIPO_INCIDENCIA_R_CORR = false;
    public bool TIPO_INCIDENCIA_R_EMAIL = false;
    public bool TIPO_INCIDENCIA_R_INV = false;
    public int TIPO_INCIDENCIA_R_ID_INV = 0;
    public bool TIPO_INCIDENCIA_R_LIMPIAR = false;
    public int TIPO_REDONDEO_ID = 0;
    public string TIPO_INCIDENCIA_R_FIQRY = "";
    public int TIPO_INCIDENCIA_R_CRED = 0;
    public bool TIPO_INCIDENCIA_R_BORRADO = false;
    public int TIPO_UNIDAD_ID = 0;
    public bool TIPO_INCIDENCIA_R_SUMAR = false;
    public CeC_IncidenciasRegla()
    { }

    public static string ObtenFechaInicialQryFiltrado(string TIPO_INCIDENCIA_R_FIQRY, int PersonaID)
    {
        return TIPO_INCIDENCIA_R_FIQRY.Replace("@PERSONA_ID", PersonaID.ToString());
    }

    public static DateTime sObtenFechaInicia(string TIPO_INCIDENCIA_R_FIQRY, int PersonaID)
    {
        return CeC_BD.EjecutaEscalarDateTime(ObtenFechaInicialQryFiltrado(TIPO_INCIDENCIA_R_FIQRY, PersonaID));
    }

    public DateTime ObtenFechaInicial(int PersonaID)
    {
        return sObtenFechaInicia(TIPO_INCIDENCIA_R_FIQRY, PersonaID);
    }
    public CeC_IncidenciasRegla(int TipoIncidenciaRID)
    {
        string Qry = "SELECT " + Campos + " FROM EC_TIPO_INCIDENCIAS_R WHERE TIPO_INCIDENCIA_R_ID = " + TipoIncidenciaRID.ToString();
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry);
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return;
        Carga(DS.Tables[0].Rows[0]);
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public bool Carga(DataRow Fila)
    {
        try
        {
            DataRow DR = Fila;
            TIPO_INCIDENCIA_R_ID = CeC.Convierte2Int(DR["TIPO_INCIDENCIA_R_ID"]);
            TIMESPAN_ID = CeC.Convierte2Int(DR["TIMESPAN_ID"]);
            TIPO_INCIDENCIA_ID = CeC.Convierte2Int(DR["TIPO_INCIDENCIA_ID"]);
            TIPO_INCIDENCIA_R_CNF = CeC.Convierte2Bool(DR["TIPO_INCIDENCIA_R_CNF"]);
            TIPO_INCIDENCIA_R_PER = CeC.Convierte2String(DR["TIPO_INCIDENCIA_R_PER"]);
            TIPO_INCIDENCIA_R_CORR = CeC.Convierte2Bool(DR["TIPO_INCIDENCIA_R_CORR"]);
            TIPO_INCIDENCIA_R_EMAIL = CeC.Convierte2Bool(DR["TIPO_INCIDENCIA_R_EMAIL"]);
            TIPO_INCIDENCIA_R_INV = CeC.Convierte2Bool(DR["TIPO_INCIDENCIA_R_INV"]);
            TIPO_INCIDENCIA_R_ID_INV = CeC.Convierte2Int(DR["TIPO_INCIDENCIA_R_ID_INV"]);

            TIPO_INCIDENCIA_R_LIMPIAR = CeC.Convierte2Bool(DR["TIPO_INCIDENCIA_R_LIMPIAR"]);
            TIPO_REDONDEO_ID = CeC.Convierte2Int(DR["TIPO_REDONDEO_ID"]);
            TIPO_INCIDENCIA_R_FIQRY = CeC.Convierte2String(DR["TIPO_INCIDENCIA_R_FIQRY"]);
            TIPO_INCIDENCIA_R_CRED = CeC.Convierte2Int(DR["TIPO_INCIDENCIA_R_CRED"]);
            TIPO_INCIDENCIA_R_BORRADO = CeC.Convierte2Bool(DR["TIPO_INCIDENCIA_R_BORRADO"]);
            TIPO_UNIDAD_ID = CeC.Convierte2Int(DR["TIPO_UNIDAD_ID"]);
            TIPO_INCIDENCIA_R_SUMAR = CeC.Convierte2Bool(DR["TIPO_INCIDENCIA_R_SUMAR"]);
            TS = new CeC_TimeSpan(TIMESPAN_ID);
            return true;
        }
        catch { }
        return false;
    }

    /// <summary>
    /// Obtiene una cadena con la regla + | + Personas_Diario_ID, etc +| + la otra regla + Personas_Diario_ID +| 
    /// separando los personas_diario_ids dependiendo la configuración de la regla
    /// Si no tiene regla algún persona_diario_id lo traerá como regla 0
    /// </summary>
    /// <param name="Personas_Diario_Ids">Contiene los persona_diario_id de los días a justificar</param>
    /// <returns></returns>
    public static string ObtenIncidenciaReglas(string Personas_Diario_Ids, int Tipo_Incidencia_ID)
    {

        return "";
    }
    public static string Campos
    {
        get { return " TIPO_INCIDENCIA_R_ID, TIPO_INCIDENCIA_ID, TIPO_INCIDENCIA_R_DESC, TIPO_INCIDENCIA_R_CNF, TIPO_INCIDENCIA_R_PER, TIPO_INCIDENCIA_R_CORR, TIPO_INCIDENCIA_R_EMAIL, TIPO_INCIDENCIA_R_INV, TIPO_INCIDENCIA_R_ID_INV, TIMESPAN_ID, TIPO_INCIDENCIA_R_LIMPIAR, TIPO_UNIDAD_ID, TIPO_REDONDEO_ID, TIPO_INCIDENCIA_R_SUMAR, TIPO_INCIDENCIA_R_FIQRY, TIPO_INCIDENCIA_R_CRED, TIPO_INCIDENCIA_R_BORRADO"; }
    }

    public static DataSet ObtenEC_TIPO_INCIDENCIAS_R(bool SoloActivos, bool ConCiclos = false)
    {
        string Filtro = "";
        if (SoloActivos)
            Filtro = "TIPO_INCIDENCIA_ID IN (SELECT TIPO_INCIDENCIA_ID FROM EC_TIPO_INCIDENCIAS WHERE TIPO_INCIDENCIA_BORRADO = 0)";
        if (ConCiclos)
            Filtro = CeC.AgregaSeparador(Filtro, "TIPO_INCIDENCIA_R_ID IN (SELECT TIPO_INCIDENCIA_R_ID FROM EC_INC_REGLA_CICLOS)", " AND ");
        if (Filtro != "")
            Filtro = " WHERE " + Filtro;

        return (DataSet)CeC_BD.EjecutaDataSet("SELECT " + Campos + " FROM EC_TIPO_INCIDENCIAS_R " + Filtro);
    }

    public static DataSet ObtenEC_TIPO_INCIDENCIAS_R(int Tipo_Incidencia_ID)
    {
        return (DataSet)CeC_BD.EjecutaDataSet("SELECT " + Campos + " FROM EC_TIPO_INCIDENCIAS_R WHERE TIPO_INCIDENCIA_ID = " + Tipo_Incidencia_ID);
    }

    public static List<eClockBase.Modelos.Model_TIPO_INCIDENCIAS_R> ObtenTiposIncidenciasR(int Tipo_Incidencia_ID, CeC_Sesion Sesion)
    {
        try
        {
            eClockBase.Modelos.Model_TIPO_INCIDENCIAS_R TipoIncidenciaR = new eClockBase.Modelos.Model_TIPO_INCIDENCIAS_R();
            string Listado = eClockBase.CeC.Json2JsonList(CeC_Tablas.ObtenDatos("EC_TIPO_INCIDENCIAS_R", "TIPO_INCIDENCIA_R_ID", TipoIncidenciaR, Sesion, "", "TIPO_INCIDENCIA_ID = " + Tipo_Incidencia_ID));
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<eClockBase.Modelos.Model_TIPO_INCIDENCIAS_R>>(Listado);
        }
        catch { }
        return new List<eClockBase.Modelos.Model_TIPO_INCIDENCIAS_R>();
    }

    public static bool TieneIncidenciaRegla(int Tipo_Incidencia_ID)
    {
        return CeC_BD.EjecutaEscalarBool("SELECT count(TIPO_INCIDENCIA_R_ID) FROM EC_TIPO_INCIDENCIAS_R WHERE TIPO_INCIDENCIA_R_BORRADO=0 AND TIPO_INCIDENCIA_ID = " + Tipo_Incidencia_ID, false);
    }


    //Obtiene el query del select de personas ID a las que se les aplicará esta regla
    /// <summary>
    /// 
    /// </summary>
    /// <param name="EC_TIPO_INCIDENCIAS_R">Dataset Filtrado por Tipo de incidencia ID</param>
    /// <returns></returns>
    public static int ObtenTipo_Incidencia_R_ID(DataSet EC_TIPO_INCIDENCIAS_R, int Persona_Diario_ID)
    {
        if (EC_TIPO_INCIDENCIAS_R == null || EC_TIPO_INCIDENCIAS_R.Tables.Count < 1 || EC_TIPO_INCIDENCIAS_R.Tables[0].Rows.Count < 1)
            return -2;
        foreach (DataRow DR in EC_TIPO_INCIDENCIAS_R.Tables[0].Rows)
        {
            if (CeC_BD.EjecutaEscalarBool("SELECT PERSONA_DIARIO_ID FROM EC_PERSONAS_DIARIO WHERE PERSONA_DIARIO_ID = " + Persona_Diario_ID + " AND PERSONA_ID IN (" + DR["TIPO_INCIDENCIA_R_PER"].ToString() + ")", false))
                return Convert.ToInt32(DR["TIPO_INCIDENCIA_R_ID"]);
        }
        return -1;
    }

    public static int ObtenTipo_Incidencia_R_ID_DesdePersonaID(DataSet EC_TIPO_INCIDENCIAS_R, int Persona_ID)
    {
        if (EC_TIPO_INCIDENCIAS_R == null || EC_TIPO_INCIDENCIAS_R.Tables.Count < 1 || EC_TIPO_INCIDENCIAS_R.Tables[0].Rows.Count < 1)
            return -2;
        foreach (DataRow DR in EC_TIPO_INCIDENCIAS_R.Tables[0].Rows)
        {
            if (CeC_BD.EjecutaEscalarBool("SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_ID IN (" + DR["TIPO_INCIDENCIA_R_PER"].ToString() + ") AND PERSONA_ID = " + Persona_ID, false))
                return CeC.Convierte2Int(DR["TIPO_INCIDENCIA_R_ID"]);
        }
        return -1;
    }

    public static CeC_IncidenciasRegla ObtenTipo_Incidencia_R_DesdePersonaID(DataSet EC_TIPO_INCIDENCIAS_R, int Persona_ID)
    {
        if (EC_TIPO_INCIDENCIAS_R == null || EC_TIPO_INCIDENCIAS_R.Tables.Count < 1 || EC_TIPO_INCIDENCIAS_R.Tables[0].Rows.Count < 1)
            return null;
        foreach (DataRow DR in EC_TIPO_INCIDENCIAS_R.Tables[0].Rows)
        {
            if (CeC_BD.EjecutaEscalarBool("SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_ID IN (" + DR["TIPO_INCIDENCIA_R_PER"].ToString() + ") AND PERSONA_ID = " + Persona_ID, false))
            {
                CeC_IncidenciasRegla IR = new CeC_IncidenciasRegla();
                IR.Carga(DR);
                return IR;
            }
        }
        return null;
    }

    public static int ObtenTipo_Incidencia_R_ID_DS(int TipoIncidenciaID, int Persona_Diario_ID)
    {
        DataSet EC_TIPO_INCIDENCIAS_R = null;
        EC_TIPO_INCIDENCIAS_R = CeC_IncidenciasRegla.ObtenEC_TIPO_INCIDENCIAS_R(TipoIncidenciaID);

        return ObtenTipo_Incidencia_R_ID(EC_TIPO_INCIDENCIAS_R, Persona_Diario_ID); ;
    }

    /// <summary>
    /// Obtiene el identificador del tipo de regla de incidencia que se encuentra asignada a un dia de un empleado
    /// primero obtiene la incidencia de un día ...
    /// </summary>
    /// <param name="Persona_Diario_ID"></param>
    /// <returns></returns>
    public static int ObtenTipo_Incidencia_R_ID(int Persona_Diario_ID)
    {
        string Qry = "SELECT TIPO_INCIDENCIA_ID, PERSONA_ID " +
        "FROM  EC_PERSONAS_DIARIO INNER JOIN " +
        "EC_INCIDENCIAS ON EC_PERSONAS_DIARIO.INCIDENCIA_ID = EC_INCIDENCIAS.INCIDENCIA_ID " +
        "WHERE EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID = " + Persona_Diario_ID;
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry);
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return -1;

        DataRow DR = DS.Tables[0].Rows[0];
        return ObtenTipo_Incidencia_R_ID(CeC.Convierte2Int(DR["PERSONA_ID"]), CeC.Convierte2Int(DR["TIPO_INCIDENCIA_ID"]));
    }

    public static int Persona_Diario_ID2Tipo_Incidencia_R_ID_INV(int Persona_Diario_ID)
    {
        return ObtenTipo_Incidencia_R_ID_INV(ObtenTipo_Incidencia_R_ID(Persona_Diario_ID));
    }

    /// <summary>
    /// Obtiene el ID de la regla del Tipo de incidencia que corresponde a una persona
    /// </summary>
    /// <param name="PersonaID"></param>
    /// <param name="TipoIncidenciaID"></param>
    /// <returns></returns>
    public static int ObtenTipo_Incidencia_R_ID(int PersonaID, int TipoIncidenciaID)
    {
        DataSet EC_TIPO_INCIDENCIAS_R = null;
        EC_TIPO_INCIDENCIAS_R = CeC_IncidenciasRegla.ObtenEC_TIPO_INCIDENCIAS_R(TipoIncidenciaID);
        if (EC_TIPO_INCIDENCIAS_R == null || EC_TIPO_INCIDENCIAS_R.Tables.Count < 1 || EC_TIPO_INCIDENCIAS_R.Tables[0].Rows.Count < 1)
            return -2;
        foreach (DataRow DR in EC_TIPO_INCIDENCIAS_R.Tables[0].Rows)
        {
            CeC_IncidenciasRegla Regla = new CeC_IncidenciasRegla();
            Regla.Carga(DR);
            if (CeC_BD.EjecutaEscalarBool("SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_ID = " + PersonaID + " AND PERSONA_ID IN (" + Regla.TIPO_INCIDENCIA_R_PER + ")", false))
                return Regla.TIPO_INCIDENCIA_R_ID;
        }
        return -1;
    }
    public static int ObtenTipo_Incidencia_R_ID_INV(int TipoIncidenciaRID)
    {
        int R = CeC_BD.EjecutaEscalarInt("SELECT TIPO_INCIDENCIA_R_ID_INV FROM EC_TIPO_INCIDENCIAS_R WHERE TIPO_INCIDENCIA_R_ID = " + TipoIncidenciaRID);
        if (R > 0)
            return R;
        return TipoIncidenciaRID;
    }

    public static bool TieneIncidenciaRegla(int Persona_Diario_ID, string TIPO_INCIDENCIA_R_PER)
    {
        return CeC_BD.EjecutaEscalarBool("SELECT PERSONA_DIARIO_ID FROM EC_PERSONAS_DIARIO WHERE PERSONA_DIARIO_ID = " + Persona_Diario_ID + " AND PERSONA_ID IN (" + TIPO_INCIDENCIA_R_PER + ")", false);
    }
    /// <summary>
    /// Obtiene una lista separada por comas de los IDs de incidencia regla
    /// </summary>
    /// <param name="PendientesPorValidar">Contiene el tipo de incidencia R  y el persona diarioId separados por coma
    /// ej. TIPO_INCIDENCIA_R_ID1,PERSONA_DIARIO_ID1,TIPO_INCIDENCIA_R_ID2,PERSONA_DIARIO_ID2</param>
    /// <returns></returns>
    public static string ObtenIncidenciaReglasIDs(string PendientesPorValidar)
    {
        string Ret = "";
        try
        {
            string[] Valores = PendientesPorValidar.Split(new char[] { ',' });

            for (int Cont = 0; Cont < Valores.Length; Cont += 2)
            {
                if (!CeC.ExisteEnSeparador(Ret, Valores[Cont], ","))
                    Ret = CeC.AgregaSeparador(Ret, Valores[Cont], ",");
            }
        }
        catch { }
        return Ret;
    }

    public static string ObtenTipoIncidenciaNombre(int TIPO_INCIDENCIA_R_ID)
    {
        return CeC_BD.EjecutaEscalarString("SELECT TIPO_INCIDENCIA_NOMBRE FROM EC_TIPO_INCIDENCIAS_R,EC_TIPO_INCIDENCIAS WHERE EC_TIPO_INCIDENCIAS_R.TIPO_INCIDENCIA_ID=EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_ID AND TIPO_INCIDENCIA_R_ID = " + TIPO_INCIDENCIA_R_ID.ToString());
    }
    public static int ObtenTipoIncidenciaID(int TIPO_INCIDENCIA_R_ID)
    {
        return CeC_BD.EjecutaEscalarInt("SELECT EC_TIPO_INCIDENCIAS_R.TIPO_INCIDENCIA_ID FROM EC_TIPO_INCIDENCIAS_R,EC_TIPO_INCIDENCIAS WHERE EC_TIPO_INCIDENCIAS_R.TIPO_INCIDENCIA_ID=EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_ID AND TIPO_INCIDENCIA_R_ID = " + TIPO_INCIDENCIA_R_ID.ToString());
    }

    public static string ObtenHTML(int TIPO_INCIDENCIA_R_ID, string PersonasDiariosIDS)
    {
        string HTML = "";
        CeC_IncidenciasRegla Regla = new CeC_IncidenciasRegla(TIPO_INCIDENCIA_R_ID);
        if (Regla.TIPO_INCIDENCIA_R_INV)
        {
            bool Cargar = true;
            DataSet DS = (DataSet)CeC_BD.EjecutaDataSet("SELECT PERSONA_ID,COUNT(PERSONA_DIARIO_ID) AS NOINC FROM EC_PERSONAS_DIARIO WHERE PERSONA_DIARIO_ID IN (" + PersonasDiariosIDS + ") GROUP BY PERSONA_ID");
            if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
                Cargar = false;
            if (Cargar)
                foreach (DataRow DR in DS.Tables[0].Rows)
                {
                    int PersonaID = CeC.Convierte2Int(DR["PERSONA_ID"]);
                    int NOInc = CeC.Convierte2Int(DR["NOINC"]);
                    int PersonaLinkID = CeC_Empleados.ObtenPersona_Link_ID(PersonaID);
                    decimal Descuento = NOInc;
                    decimal Saldo = CeC_IncidenciasInventario.ObtenSaldo(PersonaID, TIPO_INCIDENCIA_R_ID);
                    decimal SaldoPosterior = Saldo - Descuento;
                    string Adicional = "";
                    if (SaldoPosterior < Regla.TIPO_INCIDENCIA_R_CRED)
                        Adicional = "<font color=\"red\"> Saldo Insuficiente </font>";
                    HTML += "El saldo de " + PersonaLinkID.ToString() + " es " + Saldo.ToString("#,##0.00") + "-" + Descuento.ToString("#,##0.00") + "=" + SaldoPosterior.ToString("#,##0.00") + Adicional + " <BR>";


                }
        }
        return HTML;
    }

    /// <summary>
    /// Agina una Incidencia a unos días, pendiente validar que hacer en caso de error
    /// </summary>
    /// <param name="TIPO_INCIDENCIA_R_ID"></param>
    /// <param name="PersonasDiariosIDS"></param>
    /// <param name="IncidenciaComentario"></param>
    /// <param name="SesionID"></param>
    /// <returns>IncidenciaID asignada</returns>
    public static int AsignaIncidencia(int TIPO_INCIDENCIA_R_ID, string PersonasDiariosIDS, string IncidenciaComentario, CeC_Sesion Sesion)
    {
        CeC_IncidenciasRegla Regla = new CeC_IncidenciasRegla(TIPO_INCIDENCIA_R_ID);

        int IncidenciaID = Cec_Incidencias.CreaIncidencia(Regla.TIPO_INCIDENCIA_ID, IncidenciaComentario, Sesion);
        if (IncidenciaID > 0)
        {
            string[] sPersonasDiariosIDS = CeC.ObtenArregoSeparador(PersonasDiariosIDS, ",");
            foreach (string sPersonaDiarioID in sPersonasDiariosIDS)
            {
                int PersonaDiarioID = CeC.Convierte2Int(sPersonaDiarioID);
                if (Regla.TIPO_INCIDENCIA_R_INV)
                {
                    if (Cec_Incidencias.AsignaIncidencia(PersonaDiarioID, IncidenciaID, Sesion) > 0)
                        CeC_IncidenciasInventario.AgregaMovimiento(PersonaDiarioID, TIPO_INCIDENCIA_R_ID, CeC_IncidenciasInventario.TipoAlmacenIncidencias.Normal, IncidenciaComentario, "", Sesion, 0);

                }
                else
                    Cec_Incidencias.AsignaIncidencia(PersonaDiarioID, IncidenciaID, Sesion);
            }

        }
        return IncidenciaID;
    }

    public static int AsignaIncidencia(eClockBase.Modelos.Model_TIPO_INCIDENCIAS TipoIncidencia, CeC_IncidenciasRegla Regla, string PersonasDiariosIDS, string IncidenciaComentario, CeC_Sesion Sesion)
    {


        int IncidenciaID = Cec_Incidencias.CreaIncidencia(Regla.TIPO_INCIDENCIA_ID, IncidenciaComentario, Sesion);
        if (IncidenciaID > 0)
        {

            string[] sPersonasDiariosIDS = CeC.ObtenArregoSeparador(PersonasDiariosIDS, ",");
            foreach (string sPersonaDiarioID in sPersonasDiariosIDS)
            {
                int PersonaDiarioID = CeC.Convierte2Int(sPersonaDiarioID);
                if (Regla.TIPO_INCIDENCIA_R_INV)
                {
                    string Extra = "";
                    Extra = CeC.AgregaSeparador(Extra, "INCIDENCIA_ID=" + IncidenciaID, "&");
                    int TIPO_INCIDENCIA_R_ID_INV = Regla.TIPO_INCIDENCIA_R_ID;
                    if (Regla.TIPO_INCIDENCIA_R_ID_INV > 0)
                        TIPO_INCIDENCIA_R_ID_INV = Regla.TIPO_INCIDENCIA_R_ID_INV;

                    decimal Movimiento = -1;
                    if (Regla.TIPO_INCIDENCIA_R_SUMAR)
                        Movimiento = 1;
                    if (TipoIncidencia.TIPO_INCIDENCIA_HORAS)
                    {
                        eClockBase.Modelos.Incidencias.Model_Horas Horas = Newtonsoft.Json.JsonConvert.DeserializeObject<eClockBase.Modelos.Incidencias.Model_Horas>(IncidenciaComentario);
                        TimeSpan TS = Horas.Fin - Horas.Inicio;
                        Movimiento = Movimiento * eClockBase.CeC.ObtenValor(TS, Regla.TIPO_UNIDAD_ID, Regla.TIPO_REDONDEO_ID);
                        if (TipoIncidencia.T_INC_ACCESO_ID > 0)
                        {
                            string NJ = CeC_Accesos_Jus.NuevaJustificacion(TipoIncidencia.T_INC_ACCESO_ID, CeC_BD.TimeSpan2DateTime(TS), IncidenciaComentario, true, PersonaDiarioID, Horas.Inicio, Horas.Fin, Sesion);
                            if (NJ != "")
                                Extra = CeC.AgregaSeparador(Extra, NJ, "&");
                        }
                    }

                    if (Cec_Incidencias.AsignaIncidencia(PersonaDiarioID, IncidenciaID, Sesion) > 0)
                        CeC_IncidenciasInventario.AgregaMovimiento(PersonaDiarioID, TIPO_INCIDENCIA_R_ID_INV, CeC_IncidenciasInventario.TipoAlmacenIncidencias.Normal, IncidenciaComentario, Extra, Sesion, 0, Movimiento);

                }
                else
                    Cec_Incidencias.AsignaIncidencia(PersonaDiarioID, IncidenciaID, Sesion);
            }

        }
        return IncidenciaID;
    }

    public bool TieneSaldoDisponible(int PersonaID, int NoMovimientos)
    {
        if (!TIPO_INCIDENCIA_R_INV)
            return true;
        decimal Saldo = CeC_IncidenciasInventario.ObtenSaldo(PersonaID, TIPO_INCIDENCIA_R_ID);
        if (Saldo - NoMovimientos >= TIPO_INCIDENCIA_R_CRED)
            return true;
        return false;
    }
    public static bool sTieneSaldoDisponible(int PersonaID, int TipoIncidenciaID, int NoMovimientos)
    {
        int TipoIncidencia_RID = ObtenTipo_Incidencia_R_ID(PersonaID, TipoIncidenciaID);
        if (TipoIncidencia_RID <= 0)
            return true;
        CeC_IncidenciasRegla Regla = new CeC_IncidenciasRegla(TipoIncidencia_RID);
        return Regla.TieneSaldoDisponible(PersonaID, NoMovimientos);
    }
}
