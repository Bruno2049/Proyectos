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
/// Descripción breve de CeC_Incidencias_Ex
/// </summary>
public class CeC_Incidencias_Ex : CeC_Tabla
{
    int m_Incidencia_Ex_Id = 0;
    [Description("")]
    [DisplayNameAttribute("Incidencia_Ex_Id")]
    public int Incidencia_Ex_Id { get { return m_Incidencia_Ex_Id; } set { m_Incidencia_Ex_Id = value; } }
    int m_Persona_Id = 0;
    [Description("")]
    [DisplayNameAttribute("Persona_Id")]
    public int Persona_Id { get { return m_Persona_Id; } set { m_Persona_Id = value; } }
    int m_Tipo_Incidencias_Ex_Id = 0;
    [Description("")]
    [DisplayNameAttribute("Tipo_Incidencias_Ex_Id")]
    public int Tipo_Incidencias_Ex_Id { get { return m_Tipo_Incidencias_Ex_Id; } set { m_Tipo_Incidencias_Ex_Id = value; } }
    int m_Tipo_Origen_Ex_Id = 0;
    [Description("")]
    [DisplayNameAttribute("Tipo_Origen_Ex_Id")]
    public int Tipo_Origen_Ex_Id { get { return m_Tipo_Origen_Ex_Id; } set { m_Tipo_Origen_Ex_Id = value; } }
    int m_Incidencia_Ex_Origen_Id = 0;
    [Description("Contiene el Identificador del origen (persona_diario_id, persona_d_he_id), o cero en caso de no tener.")]
    [DisplayNameAttribute("Incidencia_Ex_Origen_Id")]
    public int Incidencia_Ex_Origen_Id { get { return m_Incidencia_Ex_Origen_Id; } set { m_Incidencia_Ex_Origen_Id = value; } }
    DateTime m_Incidencia_Ex_Fecha = CeC_BD.FechaNula;
    [Description("Fecha de la incidencia")]
    [DisplayNameAttribute("Incidencia_Ex_Fecha")]
    public DateTime Incidencia_Ex_Fecha { get { return m_Incidencia_Ex_Fecha; } set { m_Incidencia_Ex_Fecha = value; } }
    decimal m_Incidencia_Ex_Variable1 = 0.0M;
    [Description("")]
    [DisplayNameAttribute("Incidencia_Ex_Variable1")]
    public decimal Incidencia_Ex_Variable1 { get { return m_Incidencia_Ex_Variable1; } set { m_Incidencia_Ex_Variable1 = value; } }
    decimal m_Incidencia_Ex_Variable2 = 0.0M;
    [Description("")]
    [DisplayNameAttribute("Incidencia_Ex_Variable2")]
    public decimal Incidencia_Ex_Variable2 { get { return m_Incidencia_Ex_Variable2; } set { m_Incidencia_Ex_Variable2 = value; } }
    decimal m_Incidencia_Ex_Variable3 = 0.0M;
    [Description("")]
    [DisplayNameAttribute("Incidencia_Ex_Variable3")]
    public decimal Incidencia_Ex_Variable3 { get { return m_Incidencia_Ex_Variable3; } set { m_Incidencia_Ex_Variable3 = value; } }
    decimal m_Incidencia_Ex_Variable4 = 0.0M;
    [Description("")]
    [DisplayNameAttribute("Incidencia_Ex_Variable4")]
    public decimal Incidencia_Ex_Variable4 { get { return m_Incidencia_Ex_Variable4; } set { m_Incidencia_Ex_Variable4 = value; } }
    decimal m_Incidencia_Ex_Variable5 = 0.0M;
    [Description("")]
    [DisplayNameAttribute("Incidencia_Ex_Variable5")]
    public decimal Incidencia_Ex_Variable5 { get { return m_Incidencia_Ex_Variable5; } set { m_Incidencia_Ex_Variable5 = value; } }
    string m_Incidencia_Ex_Referencia1 = "";
    [Description("")]
    [DisplayNameAttribute("Incidencia_Ex_Referencia1")]
    public string Incidencia_Ex_Referencia1 { get { return m_Incidencia_Ex_Referencia1; } set { m_Incidencia_Ex_Referencia1 = value; } }
    string m_Incidencia_Ex_Referencia2 = "";
    [Description("")]
    [DisplayNameAttribute("Incidencia_Ex_Referencia2")]
    public string Incidencia_Ex_Referencia2 { get { return m_Incidencia_Ex_Referencia2; } set { m_Incidencia_Ex_Referencia2 = value; } }
    string m_Incidencia_Ex_Referencia3 = "";
    [Description("")]
    [DisplayNameAttribute("Incidencia_Ex_Referencia3")]
    public string Incidencia_Ex_Referencia3 { get { return m_Incidencia_Ex_Referencia3; } set { m_Incidencia_Ex_Referencia3 = value; } }
    string m_Incidencia_Ex_Referencia4 = "";
    [Description("")]
    [DisplayNameAttribute("Incidencia_Ex_Referencia4")]
    public string Incidencia_Ex_Referencia4 { get { return m_Incidencia_Ex_Referencia4; } set { m_Incidencia_Ex_Referencia4 = value; } }
    string m_Incidencia_Ex_Referencia5 = "";
    [Description("")]
    [DisplayNameAttribute("Incidencia_Ex_Referencia5")]
    public string Incidencia_Ex_Referencia5 { get { return m_Incidencia_Ex_Referencia5; } set { m_Incidencia_Ex_Referencia5 = value; } }
    int m_Incidencia_Ex_Estado = 0;
    [Description("-1 Cancelada, 0: sin procesar, 1: procesada. 2 Corregida")]
    [DisplayNameAttribute("Incidencia_Ex_Estado")]
    public int Incidencia_Ex_Estado { get { return m_Incidencia_Ex_Estado; } set { m_Incidencia_Ex_Estado = value; } }

    public CeC_Incidencias_Ex(CeC_Sesion Sesion)
        : base("EC_INCIDENCIAS_EX", "INCIDENCIA_EX_ID", Sesion)
    {

    }
    public CeC_Incidencias_Ex(int Incidencia_ExID, CeC_Sesion Sesion)
        : base("EC_INCIDENCIAS_EX", "INCIDENCIA_EX_ID", Sesion)
    {
        Carga(Incidencia_ExID.ToString(), Sesion);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="IncidenciaExId"></param>
    /// <param name="PersonaId"></param>
    /// <param name="TipoIncidenciasExId"></param>
    /// <param name="TipoOrigenExId"></param>
    /// <param name="IncidenciaExOrigenId">Contiene el Identificador del origen (persona_diario_id, persona_d_he_id), o cero en caso de no tener.</param>
    /// <param name="IncidenciaExFecha">Fecha de la incidencia</param>
    /// <param name="IncidenciaExVariable1"></param>
    /// <param name="IncidenciaExVariable2"></param>
    /// <param name="IncidenciaExVariable3"></param>
    /// <param name="IncidenciaExVariable4"></param>
    /// <param name="IncidenciaExVariable5"></param>
    /// <param name="IncidenciaExReferencia1"></param>
    /// <param name="IncidenciaExReferencia2"></param>
    /// <param name="IncidenciaExReferencia3"></param>
    /// <param name="IncidenciaExReferencia4"></param>
    /// <param name="IncidenciaExReferencia5"></param>
    /// <param name="IncidenciaExEstado">-1 Cancelada, 0: sin procesar, 1: procesada. 2 Corregida</param>
    /// <param name="Sesion"></param>
    /// <returns></returns>
    public bool Actualiza(int IncidenciaExId, int PersonaId, int TipoIncidenciasExId, int TipoOrigenExId, int IncidenciaExOrigenId, decimal IncidenciaExVariable1, decimal IncidenciaExVariable2, decimal IncidenciaExVariable3, decimal IncidenciaExVariable4, decimal IncidenciaExVariable5, string IncidenciaExReferencia1, string IncidenciaExReferencia2, string IncidenciaExReferencia3, string IncidenciaExReferencia4, string IncidenciaExReferencia5, int IncidenciaExEstado,
        CeC_Sesion Sesion)
    {
        try
        {
            bool Nuevo = false;
            if (!Carga(IncidenciaExId.ToString(), Sesion))
                Nuevo = true;
            m_EsNuevo = Nuevo;
            Incidencia_Ex_Id = IncidenciaExId;
            Persona_Id = PersonaId;
            Tipo_Incidencias_Ex_Id = TipoIncidenciasExId;
            Tipo_Origen_Ex_Id = TipoOrigenExId;
            Incidencia_Ex_Origen_Id = IncidenciaExOrigenId;
            Incidencia_Ex_Variable1 = IncidenciaExVariable1;
            Incidencia_Ex_Variable2 = IncidenciaExVariable2;
            Incidencia_Ex_Variable3 = IncidenciaExVariable3;
            Incidencia_Ex_Variable4 = IncidenciaExVariable4;
            Incidencia_Ex_Variable5 = IncidenciaExVariable5;
            Incidencia_Ex_Referencia1 = IncidenciaExReferencia1;
            Incidencia_Ex_Referencia2 = IncidenciaExReferencia2;
            Incidencia_Ex_Referencia3 = IncidenciaExReferencia3;
            Incidencia_Ex_Referencia4 = IncidenciaExReferencia4;
            Incidencia_Ex_Referencia5 = IncidenciaExReferencia5;
            Incidencia_Ex_Estado = IncidenciaExEstado;
            if (Guarda(Sesion))
            {
                return true;
            }
        }
        catch { }
        return false;
    }

    public static string ObtenQryIncidenciasExAsis(DateTime Desde, DateTime Hasta, string QryPersonasID)
    {
        string Qry = "SELECT   EC_PERSONAS.PERSONA_ID,  EC_PERSONAS.PERSONA_LINK_ID, EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA, \n" +
                      "EC_PERSONAS_DIARIO.PERSONA_DIARIO_TT, EC_PERSONAS_DIARIO.PERSONA_DIARIO_TE, EC_PERSONAS_DIARIO.PERSONA_DIARIO_TC,  \n" +
                      "EC_PERSONAS_DIARIO.PERSONA_DIARIO_TDE, EC_PERSONAS_DIARIO.PERSONA_DIARIO_TES,  \n" +
                      "EC_V_PERSONAS_DIARIO_EX.TIPO_INCIDENCIAS_EX_ID, EC_V_PERSONAS_DIARIO_EX.TIPO_INCIDENCIAS_EX_TXT,  \n" +
                      "EC_V_PERSONAS_DIARIO_EX.TIPO_FALTA_EX_ID, EC_V_PERSONAS_DIARIO_EX.TIPO_INCIDENCIAS_EX_PARAM,  \n" +
                      "EC_V_PERSONAS_DIARIO_EX.INCIDENCIA_COMENTARIO, EC_PERSONAS_DIARIO.INCIDENCIA_ID \n" +
                      "FROM         EC_PERSONAS INNER JOIN \n" +
                      "EC_PERSONAS_DIARIO ON EC_PERSONAS.PERSONA_ID = EC_PERSONAS_DIARIO.PERSONA_ID INNER JOIN \n" +
                      "EC_V_PERSONAS_DIARIO_EX ON EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID = EC_V_PERSONAS_DIARIO_EX.PERSONA_DIARIO_ID";
        Qry += " WHERE PERSONA_BORRADO = 0 AND EC_PERSONAS_DIARIO.PERSONA_ID IN (" + QryPersonasID +
            ") AND PERSONA_DIARIO_FECHA >= \n" + CeC_BD.SqlFecha(Desde) + " AND PERSONA_DIARIO_FECHA <= \n" +
            CeC_BD.SqlFecha(Hasta);
        return Qry;
    }

    public static string ObtenQryIncidenciasExAsis(DateTime Desde, DateTime Hasta, int SuscripcionID, int PeriodoID)
    {
        string Select = CeC_BD.EjecutaEscalarString("SELECT PERIODO_N_PERSONAS FROM EC_PERIODOS_N WHERE PERIODO_N_ID in (SELECT PERIODO_N_ID FROM EC_PERIODOS WHERE PERIODO_ID = " + PeriodoID + ")");
        if (Select != "")
            Select += " AND  SUSCRIPCION_ID = " + SuscripcionID;
        return ObtenQryIncidenciasExAsis(Desde, Hasta, Select);
    }

    public static DataSet ObtenIncidenciasExAsis(DateTime Desde, DateTime Hasta, int SuscripcionID, int PeriodoID)
    {
        return CeC_BD.EjecutaDataSet(ObtenQryIncidenciasExAsis(Desde, Hasta, SuscripcionID, PeriodoID));
    }

    public static bool EnviaANomina( int PeriodoID, int SuscripcionID)
    {
        DateTime Desde = CeC_BD.EjecutaEscalarDateTime("SELECT PERIODO_ASIS_INICIO FROM EC_PERIODOS WHERE PERIODO_ID = " + PeriodoID );
        DateTime Hasta = CeC_BD.EjecutaEscalarDateTime("SELECT PERIODO_ASIS_FIN FROM EC_PERIODOS WHERE PERIODO_ID = " + PeriodoID);
        string Select = CeC_BD.EjecutaEscalarString("SELECT PERIODO_N_PERSONAS FROM EC_PERIODOS_N WHERE PERIODO_N_ID in (SELECT PERIODO_N_ID FROM EC_PERIODOS WHERE PERIODO_ID = " + PeriodoID + ")");
        if (Select != "")
            Select = "SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_ID IN ("+Select+ ") AND SUSCRIPCION_ID = " + SuscripcionID;
        else
            Select = "SELECT PERSONA_ID FROM EC_PERSONAS WHERE SUSCRIPCION_ID = " + SuscripcionID;
        return CMd_Base.gEnviaIncidencias(Desde, Hasta, PeriodoID, Select);
    }
}