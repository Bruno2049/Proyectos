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
/// Descripción breve de CeC_Campos_Inc_R
/// </summary>
public class CeC_Campos_Inc_R : CeC_Tabla
{
    string m_Campo_Nombre = "";
    [Description("Nombre que se usará para controlar los campos. @RI+ id de regla de incidencia + _ + Campo")]
    [DisplayNameAttribute("Campo_Nombre")]
    public string Campo_Nombre { get { return m_Campo_Nombre; } set { m_Campo_Nombre = value; } }
    int m_Tipo_Incidencia_R_Id = 0;
    [Description("Identificador de la Regla de Tipo de Incidencia")]
    [DisplayNameAttribute("Tipo_Incidencia_R_Id")]
    public int Tipo_Incidencia_R_Id { get { return m_Tipo_Incidencia_R_Id; } set { m_Tipo_Incidencia_R_Id = value; } }
    bool m_Campo_Inc_R_Obl = false;
    [Description("Indica que el campo es obligatorio")]
    [DisplayNameAttribute("Campo_Inc_R_Obl")]
    public bool Campo_Inc_R_Obl { get { return m_Campo_Inc_R_Obl; } set { m_Campo_Inc_R_Obl = value; } }
    string m_Campo_Inc_R_Dest = "";
    [Description("Contiene el nombre del campo donde se guardará en caso de exportar la informacion")]
    [DisplayNameAttribute("Campo_Inc_R_Dest")]
    public string Campo_Inc_R_Dest { get { return m_Campo_Inc_R_Dest; } set { m_Campo_Inc_R_Dest = value; } }
    string m_Campo_Inc_R_Exp = "";
    [Description("Contiene datos extras para la exportación")]
    [DisplayNameAttribute("Campo_Inc_R_Exp")]
    public string Campo_Inc_R_Exp { get { return m_Campo_Inc_R_Exp; } set { m_Campo_Inc_R_Exp = value; } }
    int m_Campo_Inc_R_Ord = 0;
    [Description("Orden en el que se mostraran los campos")]
    [DisplayNameAttribute("Campo_Inc_R_Ord")]
    public int Campo_Inc_R_Ord { get { return m_Campo_Inc_R_Ord; } set { m_Campo_Inc_R_Ord = value; } }

    public CeC_Campos_Inc_R(CeC_Sesion Sesion)
        : base("EC_CAMPOS_INC_R", "CAMPO_NOMBRE,TIPO_INCIDENCIA_R_ID", Sesion)
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public CeC_Campos_Inc_R(string CampoNombre, int Tipo_Incidencia_R_ID, CeC_Sesion Sesion)
        : base("EC_CAMPOS_INC_R", "CAMPO_NOMBRE,TIPO_INCIDENCIA_R_ID", Sesion)
    {
        Carga(new string[] { CampoNombre, Tipo_Incidencia_R_ID.ToString() }, Sesion);
    }

    public CeC_Campos_Inc_R(string CampoNombre, CeC_Sesion Sesion)
        : base("EC_CAMPOS_INC_R", "CAMPO_NOMBRE", Sesion)
    {
        Carga(new string[] { CampoNombre }, Sesion);
    }

    public static string ObtenCampos(int Tipo_Incidencia_R_ID, CeC_Sesion Sesion)
    {
        string Qry = "SELECT CAMPO_NOMBRE FROM EC_CAMPOS_INC_R WHERE TIPO_INCIDENCIA_R_ID = " + Tipo_Incidencia_R_ID + " ORDER BY CAMPO_INC_R_ORD";
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry);
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return "";
        string Campos = "";
        foreach (DataRow DR in DS.Tables[0].Rows)
        {
            Campos = CeC.AgregaSeparador(Campos, CeC.Convierte2String(DR["CAMPO_NOMBRE"]), ",");
        }
        return Campos;
    }

    /// <summary>
    /// Indica si este comentario contiene valores de campos de reglas de incidencias, almacenados en un comentario de incidencia
    /// </summary>
    /// <param name="Comentario"></param>
    /// <returns></returns>
    public static bool TieneValores(string Comentario)
    {
        if (Comentario.IndexOf("@RI") >= 0)
            return true;
        return false;
    }

    /// <summary>
    /// Obtiene los campos que fueron almacenados en un comentario de incidencia
    /// </summary>
    /// <param name="Comentario"></param>
    /// <returns></returns>
    public static string[] ObtenCampos(string Comentario)
    {
        try
        {
            string[] Separadores = CeC.ObtenArregoSeparador(Comentario, "&");
            string[] Campos = new string[Separadores.Length];
            for (int i = 0; i < Separadores.Length; i++)
            {
                string sSeperador = Separadores[i];
                string[] Valores = CeC.ObtenArregoSeparador(sSeperador, "=");
                Campos[i] = Valores[0];
            }
            return Campos;
        }
        catch { }
        return null;
    }

    /// <summary>
    /// Obtiene el valor de determinado campo que fue almacenados en un comentario de incidencia
    /// </summary>
    /// <param name="Comentario"></param>
    /// <param name="Campo"></param>
    /// <returns></returns>
    public static string ObtenValor(string Comentario, string Campo)
    {
        try
        {
            string[] Separadores = CeC.ObtenArregoSeparador(Comentario, "&");
            for (int i = 0; i < Separadores.Length; i++)
            {
                string sSeperador = Separadores[i];
                string[] Valores = CeC.ObtenArregoSeparador(sSeperador, "=");
                if (Campo == Valores[0])
                    return Valores[1];
            }
        }
        catch { }
        return "";
    }

}