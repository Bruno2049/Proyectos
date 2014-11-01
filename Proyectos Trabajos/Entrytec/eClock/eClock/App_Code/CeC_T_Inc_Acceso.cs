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
/// Descripción breve de CeC_T_Inc_Acceso
/// </summary>
public class CeC_T_Inc_Acceso : CeC_Tabla
{
    int m_T_Inc_Acceso_Id = 0;
    [Description("Identificador unico de registro")]
    [DisplayNameAttribute("T_Inc_Acceso_Id")]
    public int T_Inc_Acceso_Id { get { return m_T_Inc_Acceso_Id; } set { m_T_Inc_Acceso_Id = value; } }
    string m_T_Inc_Acceso_Nombre = "";
    [Description("Contiene el nombre de el tipo de Incidencia en el acceso (solo para accesos justificados)")]
    [DisplayNameAttribute("T_Inc_Acceso_Nombre")]
    public string T_Inc_Acceso_Nombre { get { return m_T_Inc_Acceso_Nombre; } set { m_T_Inc_Acceso_Nombre = value; } }
    string m_T_Inc_Acceso_Abr = "";
    [Description("Abreviatura del tipo de incidencia de acceso justificado")]
    [DisplayNameAttribute("T_Inc_Acceso_Abr")]
    public string T_Inc_Acceso_Abr { get { return m_T_Inc_Acceso_Abr; } set { m_T_Inc_Acceso_Abr = value; } }
    int m_T_Inc_Acceso_Color = -1;
    [Description("Contiene el color en entero, -1 es no definido")]
    [DisplayNameAttribute("T_Inc_Acceso_Color")]
    public int T_Inc_Acceso_Color { get { return m_T_Inc_Acceso_Color; } set { m_T_Inc_Acceso_Color = value; } }
    int m_Tipo_Incidencia_Id = 0;
    [Description("-1: Significa que solicitará el tipo de incidencia dicho día; 0: no asignará ninguna incidencia en el día, se recalculará como si el acceso fuera satisfactorio; >0 Indica la incidencia que se asignará en el día")]
    [DisplayNameAttribute("Tipo_Incidencia_Id")]
    public int Tipo_Incidencia_Id { get { return m_Tipo_Incidencia_Id; } set { m_Tipo_Incidencia_Id = value; } }
    int m_Tipo_Incidencia_Ex_Id = 0;
    [Description("0: Indica que no se exportará; > 0 contiene el identificador de la incidencia que se usará para exportar la incidencia")]
    [DisplayNameAttribute("Tipo_Incidencia_Ex_Id")]
    public int Tipo_Incidencia_Ex_Id { get { return m_Tipo_Incidencia_Ex_Id; } set { m_Tipo_Incidencia_Ex_Id = value; } }
    string m_Tipo_Incidencia_Ex_Regla = "";
    [Description("Contiene la regla para exportar el elemento")]
    [DisplayNameAttribute("Tipo_Incidencia_Ex_Regla")]
    public string Tipo_Incidencia_Ex_Regla { get { return m_Tipo_Incidencia_Ex_Regla; } set { m_Tipo_Incidencia_Ex_Regla = value; } }
    string m_T_Inc_Acceso_Comen = "";
    [Description("Regla para la captura del comentario")]
    [DisplayNameAttribute("T_Inc_Acceso_Comen")]
    public string T_Inc_Acceso_Comen { get { return m_T_Inc_Acceso_Comen; } set { m_T_Inc_Acceso_Comen = value; } }
    bool m_T_Inc_Acceso_Entrada = true;
    [Description("Indica si se  permitira la captura en entrada")]
    [DisplayNameAttribute("T_Inc_Acceso_Entrada")]
    public bool T_Inc_Acceso_Entrada { get { return m_T_Inc_Acceso_Entrada; } set { m_T_Inc_Acceso_Entrada = value; } }
    bool m_T_Inc_Acceso_Salida = true;
    [Description("Indica si se permitirá la captura en salida")]
    [DisplayNameAttribute("T_Inc_Acceso_Salida")]
    public bool T_Inc_Acceso_Salida { get { return m_T_Inc_Acceso_Salida; } set { m_T_Inc_Acceso_Salida = value; } }
    bool m_T_Inc_Acceso_Intervalo = true;
    [Description("Indica si se permitirá la captura de intervalo")]
    [DisplayNameAttribute("T_Inc_Acceso_Intervalo")]
    public bool T_Inc_Acceso_Intervalo { get { return m_T_Inc_Acceso_Intervalo; } set { m_T_Inc_Acceso_Intervalo = value; } }
    int m_T_Inc_Acceso_Asignar = -1;
    [Description("0: indica que no se asignará la incidencia en todo el dia o persona diario; 1:Indica que asignará la incidencia todo el día en persona Diario ; -1:Indica que preguntará que hacer")]
    [DisplayNameAttribute("T_Inc_Acceso_Asignar")]
    public int T_Inc_Acceso_Asignar { get { return m_T_Inc_Acceso_Asignar; } set { m_T_Inc_Acceso_Asignar = value; } }
    int m_T_Inc_Acceso_Medicion = 0;
    [Description("forma en la que se medirá el tiempo de permiso; 0: Permite Introducir la hora desde la que se dará permiso hasta la hora de salida supuesta de salida o el tiempo de permiso; 1: tomar ")]
    [DisplayNameAttribute("T_Inc_Acceso_Medicion")]
    public int T_Inc_Acceso_Medicion { get { return m_T_Inc_Acceso_Medicion; } set { m_T_Inc_Acceso_Medicion = value; } }
    bool m_T_Inc_Acceso_Borrado = false;
    [Description("Indica que el elemento se ha borrado")]
    [DisplayNameAttribute("T_Inc_Acceso_Borrado")]
    public bool T_Inc_Acceso_Borrado { get { return m_T_Inc_Acceso_Borrado; } set { m_T_Inc_Acceso_Borrado = value; } }


    public CeC_T_Inc_Acceso(CeC_Sesion Sesion)
        : base("EC_T_INC_ACCESO", "T_INC_ACCESO_ID", Sesion)
    {

    }


    public CeC_T_Inc_Acceso(int TIncAccesoId, CeC_Sesion Sesion)
        : base("EC_T_INC_ACCESO", "T_INC_ACCESO_ID", Sesion)
    {
        Carga(TIncAccesoId.ToString(), Sesion);
    }


    public static CeC_T_Inc_Acceso ObtenByTipo_Acceso_ID(int TipoAccesoID, CeC_Sesion Sesion)
    {
        CeC_T_Inc_Acceso Ret = new CeC_T_Inc_Acceso(Sesion);
        if (Ret.Carga("TIPO_INCIDENCIA_ID", TipoAccesoID.ToString(), Sesion))
            return Ret;
        return null;
    }
}