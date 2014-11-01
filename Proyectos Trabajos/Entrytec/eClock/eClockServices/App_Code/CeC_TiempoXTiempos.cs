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
using System.Reflection;

/// <summary>
/// Descripción breve de CeC_TiempoXTiempos
/// </summary>
public class CeC_TiempoXTiempos : CeC_Tabla
{
    int m_Tipo_Incidencia_Id = 0;
    [Description("Identificador del tipo de Incidencia")]
    [DisplayNameAttribute("Tipo_Incidencia_Id")]
    public int Tipo_Incidencia_Id { get { return m_Tipo_Incidencia_Id; } set { m_Tipo_Incidencia_Id = value; } }
    int m_Tiempoxtiempo_Unidades = 0;
    [Description("Unidades de Tiempo X Tiempo 0: Segundos; 1: Minutos; 2: Horas; 3: Dias laborables")]
    [DisplayNameAttribute("Tiempoxtiempo_Unidades")]
    public int Tiempoxtiempo_Unidades { get { return m_Tiempoxtiempo_Unidades; } set { m_Tiempoxtiempo_Unidades = value; } }
    int m_Tiempoxtiempo_Fracciones = 0;
    [Description("0: Trunca el valor ej. 9.9 = 9; 1: Fracciones ej. 9.9 = 9.9; -1: Redondea ej. 9.9 = 10;2: trunca el valor al entero siguiente ej. 9.2 = 10")]
    [DisplayNameAttribute("Tiempoxtiempo_Fracciones")]
    public int Tiempoxtiempo_Fracciones { get { return m_Tiempoxtiempo_Fracciones; } set { m_Tiempoxtiempo_Fracciones = value; } }
    bool m_Tiempoxtiempo_Sumar = true;
    [Description("1: Indica que esta incidencia sumará el tiempo; 0: que lo restara")]
    [DisplayNameAttribute("Tiempoxtiempo_Sumar")]
    public bool Tiempoxtiempo_Sumar { get { return m_Tiempoxtiempo_Sumar; } set { m_Tiempoxtiempo_Sumar = value; } }
    int m_Tipo_Incidencia_Id_Restar = 0;
    [Description("Indica el TIPO_INCIDENCIA_ID del elemento al que se le restará el tiempo, esto solo para los elementos de resta")]
    [DisplayNameAttribute("Tipo_Incidencia_Id_Restar")]
    public int Tipo_Incidencia_Id_Restar { get { return m_Tipo_Incidencia_Id_Restar; } set { m_Tipo_Incidencia_Id_Restar = value; } }
    string m_Tiempoxtiempo_Qryval = "";
    [Description("Qry para validar si se puede aplicar el tXt")]
    [DisplayNameAttribute("Tiempoxtiempo_Qryval")]
    public string Tiempoxtiempo_Qryval { get { return m_Tiempoxtiempo_Qryval; } set { m_Tiempoxtiempo_Qryval = value; } }

    public CeC_TiempoXTiempos(CeC_Sesion Sesion)
        : base("EC_TIEMPOXTIEMPOS", "TIPO_INCIDENCIA_ID", Sesion)
    {

    }
    public CeC_TiempoXTiempos(int TipoIncidenciaId, CeC_Sesion Sesion)
        : base("EC_TIEMPOXTIEMPOS", "TIPO_INCIDENCIA_ID", Sesion)
    {
        Carga(TipoIncidenciaId.ToString(), Sesion);
    }

    /// <summary>
    /// Pendiente Optimizar deberá traer solo a las de la suscripcion
    /// Obtiene las incidencias que se usarán como tiempos X tiempos que se sumaran, 
    /// </summary>
    /// <param name="Sesion"></param>
    /// <returns></returns>
    public static string ObtenTiemposXTiempos(CeC_Sesion Sesion, bool HorasExtras, bool HorasMenos)
    {
        string Filtro = "";
        if (HorasExtras && HorasMenos)
        { }
        else
            if (HorasExtras)
                Filtro = "WHERE TIEMPOXTIEMPO_SUMAR = 1";
            else
                Filtro = "WHERE TIEMPOXTIEMPO_SUMAR = 0";
        string Qry = "SELECT TIPO_INCIDENCIA_ID FROM EC_TIEMPOXTIEMPOS " + Filtro + " \n ORDER BY TIPO_INCIDENCIA_ID";
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry);
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return "";
        string Campos = "";
        foreach (DataRow DR in DS.Tables[0].Rows)
        {
            Campos = CeC.AgregaSeparador(Campos, CeC.Convierte2String(DR["TIPO_INCIDENCIA_ID"]), ",");
        }
        return Campos;
    }

    /// <summary>
    /// Obtiene el valor del tipo de incidencia sobre si es un tiempo x tiempo a sumar
    /// </summary>
    /// <param name="TipoIncidenciaId"></param>
    /// <returns></returns>
    public static bool EsHorasExtras(int TipoIncidenciaId)
    {
        string Qry = "SELECT TIEMPOXTIEMPO_SUMAR FROM EC_TIEMPOXTIEMPOS WHERE TIPO_INCIDENCIA_ID = " + TipoIncidenciaId;
        return CeC_BD.EjecutaEscalarBool(Qry, false);
    }

    /// <summary>
    /// Indica si la incidencia es tiempo por tiempo
    /// </summary>
    /// <param name="TipoIncidenciaId"></param>
    /// <returns></returns>
    public static bool EsTiempoXTiempo(int TipoIncidenciaId)
    {
        string Qry = "SELECT TIPO_INCIDENCIA_ID FROM EC_TIEMPOXTIEMPOS WHERE TIPO_INCIDENCIA_ID = " + TipoIncidenciaId;
        if (CeC_BD.EjecutaEscalarInt(Qry) > 0)
            return true;
        return false;

    }

    public decimal ObtenValor(decimal Tiempo)
    {
        decimal Valor = Tiempo;

        switch (Tiempoxtiempo_Fracciones)
        {
            case 0://trunca
                Valor = Math.Floor(Valor);
                break;
            case 1://fracciones
                Valor = Valor;
                break;
            case 2://fracciones
                Valor = Math.Ceiling(Valor);
                break;
            case -1://redondea
                Valor = Math.Round(Valor, 0);
                break;
        }
        return Valor;
    }
    public decimal ObtenValor(TimeSpan Tiempo)
    {
        decimal Valor = 0;
        switch (Tiempoxtiempo_Unidades)
        {
            case 0://segundos
                Valor = Convert.ToDecimal(Tiempo.TotalSeconds);
                break;
            case 1://minutos
                Valor = Convert.ToDecimal(Tiempo.TotalMinutes);
                break;
            case 2://horas
                Valor = Convert.ToDecimal(Tiempo.TotalHours);
                break;
            case 3://dias laborables
                Valor = Convert.ToDecimal(Tiempo.TotalDays);
                break;
        }
        switch (Tiempoxtiempo_Fracciones)
        {
            case 0://trunca
                Valor = Math.Floor(Valor);
                break;
            case 1://fracciones
                Valor = Valor;
                break;
            case 2://fracciones
                Valor = Math.Ceiling(Valor);
                break;
            case -1://redondea
                Valor = Math.Round(Valor, 0);
                break;
        }
        return Valor;
    }



    /// <summary>
    /// Se usa para agregar saldo a un tiempo por tiempo
    /// </summary>
    /// <param name="SesionID"></param>
    /// <param name="PERSONA_D_HE_ID"></param>
    /// <param name="PERSONA_D_HE_APL"></param>
    /// <param name="TipoIncidenciaID"></param>
    /// <param name="Comentarios"></param>
    /// <returns></returns>
    public static int Agrega(CeC_Sesion Sesion, int PERSONA_D_HE_ID, TimeSpan PERSONA_D_HE_APL, int TipoIncidenciaID, string Comentarios)
    {
        try
        {
            CeC_Asistencias PersonaDiario = CeC_Asistencias.PorHorasExtras(PERSONA_D_HE_ID, Sesion);
            if (PersonaDiario.PERSONA_ID <= 0)
                return -9999;

            int TipoIncidencia_R_ID = CeC_IncidenciasRegla.ObtenTipo_Incidencia_R_ID(PersonaDiario.PERSONA_ID, TipoIncidenciaID);
            if (TipoIncidencia_R_ID <= 0)
                return -9998;

            CeC_TiempoXTiempos TxT = new CeC_TiempoXTiempos(TipoIncidenciaID, Sesion);
            return CeC_IncidenciasInventario.AgregaMovimiento(PersonaDiario.PERSONA_ID, TipoIncidencia_R_ID, CeC_IncidenciasInventario.TipoAlmacenIncidencias.Normal,
                PersonaDiario.PERSONA_DIARIO_FECHA, CeC.Convierte2Decimal(TxT.ObtenValor(PERSONA_D_HE_APL)), CeC_BD.FechaNula, false, Comentarios, "PERSONA_D_HE_ID=" + PERSONA_D_HE_ID,
                Sesion.SESION_ID, Sesion.SUSCRIPCION_ID);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return -9997;
    }

    public static int Quita(int PERSONA_D_HE_ID, int TipoIncidenciaReglaID, int SesionID, int SuscripcionID)
    {
        return CeC_IncidenciasInventario.CorrigeMovimiento("PERSONA_D_HE_ID=" + PERSONA_D_HE_ID.ToString(),TipoIncidenciaReglaID, SesionID, SuscripcionID);
    }
    public static int Agrega(int PERSONA_D_HE_ID, int TipoIncidenciaID, int TipoIncidenciaReglaID, decimal Tiempo, string Comentarios, CeC_Sesion Sesion)
    {
        try
        {

            CeC_Asistencias PersonaDiario = CeC_Asistencias.PorHorasExtras(PERSONA_D_HE_ID, Sesion);
            if (PersonaDiario.PERSONA_ID <= 0)
                return -9999;

            int SesionID = 0;
            int SuscripcionID = 0;
            if (Sesion != null)
            {
                SesionID = Sesion.SESION_ID;
                SuscripcionID =  Sesion.SUSCRIPCION_ID;
            }
            Quita(PERSONA_D_HE_ID,TipoIncidenciaReglaID, SesionID, SuscripcionID);

            CeC_TiempoXTiempos TxT = new CeC_TiempoXTiempos(TipoIncidenciaID, Sesion);
            return CeC_IncidenciasInventario.AgregaMovimiento(PersonaDiario.PERSONA_ID, TipoIncidenciaReglaID, CeC_IncidenciasInventario.TipoAlmacenIncidencias.Normal,
                PersonaDiario.PERSONA_DIARIO_FECHA, TxT.ObtenValor(Tiempo), CeC_BD.FechaNula, false, Comentarios, "PERSONA_D_HE_ID=" + PERSONA_D_HE_ID,
                SesionID, SuscripcionID);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return -9997;
    }


    /// <summary>
    /// obtiene el Tipo de incidencia Regla ID a restar desde un tipo de incidencia regla id
    /// si no es de TxT o es positivo regresará lo mismo
    /// </summary>
    /// <param name="TIPO_INCIDENCIA_R_ID"></param>
    /// <param name="Persona_Diario_ID"></param>
    /// <param name="Sesion"></param>
    /// <returns></returns>
    public static int IncidenciaRegla2IncidenciaReglaResta(int TIPO_INCIDENCIA_R_ID, int Persona_Diario_ID, CeC_Sesion Sesion)
    {
        CeC_IncidenciasRegla Regla = new CeC_IncidenciasRegla(TIPO_INCIDENCIA_R_ID);
        
        CeC_TiempoXTiempos TxT = new CeC_TiempoXTiempos(Regla.TIPO_INCIDENCIA_ID, Sesion);
        if (TxT.Tipo_Incidencia_Id != Regla.TIPO_INCIDENCIA_ID || TxT.Tiempoxtiempo_Sumar)
            return TIPO_INCIDENCIA_R_ID;
        ///Sustituye la regla inicial por la regla de descuento
        return CeC_IncidenciasRegla.ObtenTipo_Incidencia_R_ID_DS(TxT.Tipo_Incidencia_Id_Restar, Persona_Diario_ID);

    }

    public static string ObtenHTML(int TIPO_INCIDENCIA_R_ID, int Persona_Diario_ID, CeC_Sesion Sesion, TimeSpan Tiempo)
    {
        try
        {
            string HTML = "";
            CeC_IncidenciasRegla Regla = new CeC_IncidenciasRegla(TIPO_INCIDENCIA_R_ID);
            CeC_TiempoXTiempos TxT = new CeC_TiempoXTiempos(Regla.TIPO_INCIDENCIA_ID, Sesion);

            ///Sustituye la regla inicial por la regla de descuento
            Regla = new CeC_IncidenciasRegla(CeC_IncidenciasRegla.ObtenTipo_Incidencia_R_ID_DS(TxT.Tipo_Incidencia_Id_Restar, Persona_Diario_ID));
            bool Cargar = true;
            DataSet DS = (DataSet)CeC_BD.EjecutaDataSet("SELECT PERSONA_ID,COUNT(PERSONA_DIARIO_ID) AS NOINC FROM EC_PERSONAS_DIARIO WHERE PERSONA_DIARIO_ID =" + Persona_Diario_ID + " GROUP BY PERSONA_ID");
            if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
                Cargar = false;
            if (Cargar)
                foreach (DataRow DR in DS.Tables[0].Rows)
                {

                    int PersonaID = CeC.Convierte2Int(DR["PERSONA_ID"]);
                    int NOInc = CeC.Convierte2Int(DR["NOINC"]);
                    int PersonaLinkID = CeC_Empleados.ObtenPersona_Link_ID(PersonaID);
                    decimal Descuento = TxT.ObtenValor(Tiempo);
                    decimal Saldo = CeC_IncidenciasInventario.ObtenSaldo(PersonaID, Regla.TIPO_INCIDENCIA_R_ID);
                    decimal SaldoPosterior = Saldo - Descuento;
                    string Adicional = "";
                    if (SaldoPosterior < Regla.TIPO_INCIDENCIA_R_CRED)
                        Adicional = "<font color=\"red\"> Saldo Insuficiente </font>";
                    HTML += "El saldo de " + PersonaLinkID.ToString() + " es " + Saldo.ToString("#,##0.00") + "-" + Descuento.ToString("#,##0.00") + "=" + SaldoPosterior.ToString("#,##0.00") + Adicional + " <BR>";


                }
            return HTML;
        }
        catch (Exception ex)
        {
            return ex.Message;

        }

    }
}