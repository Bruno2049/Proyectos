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
/// Descripción breve de CeC_Tablas_Comandos
/// </summary>
public class CeC_TablasComandos : CeC_Tabla
{
    int m_Tabla_Comando_Id = 0;
    [Description("Identificador de comando")]
    [DisplayNameAttribute("Tabla_Comando_Id")]
    public int Tabla_Comando_Id { get { return m_Tabla_Comando_Id; } set { m_Tabla_Comando_Id = value; } }
    string m_Tabla_Nombre = "";
    [Description("Nombre de la tabla")]
    [DisplayNameAttribute("Tabla_Nombre")]
    public string Tabla_Nombre { get { return m_Tabla_Nombre; } set { m_Tabla_Nombre = value; } }
    int m_Catalogo_Id = 0;
    [Description("Identificador de catalogo")]
    [DisplayNameAttribute("Catalogo_Id")]
    public int Catalogo_Id { get { return m_Catalogo_Id; } set { m_Catalogo_Id = value; } }
    string m_Tabla_Comando_Nombre = "";
    [Description("Nombre del comando")]
    [DisplayNameAttribute("Tabla_Comando_Nombre")]
    public string Tabla_Comando_Nombre { get { return m_Tabla_Comando_Nombre; } set { m_Tabla_Comando_Nombre = value; } }
    string m_Tabla_Comando_Codigo = "";
    [Description("Contiene el codigo de la Clase.Funcion(Parametro1,Parametro2,Parametro3) para variables de sesión usar <IDS></IDS> para lista de identificadores separados por coma,  <SESION>Variable</SESION>, para parametros html <PARAMETRO>Variable</PARAMETRO>, para valor seleccionado <CAMPO>Nombre Campo</CAMPO>, para incognita <PREGUNTA>Campo</PREGUNTA>. el Campo CATALOGO_ID se refiere a la selección del menu")]
    [DisplayNameAttribute("Tabla_Comando_Codigo")]
    public string Tabla_Comando_Codigo { get { return m_Tabla_Comando_Codigo; } set { m_Tabla_Comando_Codigo = value; } }
    string m_Tabla_Comando_Etiqueta = "";
    [Description("Etiqueta del comando")]
    [DisplayNameAttribute("Tabla_Comando_Etiqueta")]
    public string Tabla_Comando_Etiqueta { get { return m_Tabla_Comando_Etiqueta; } set { m_Tabla_Comando_Etiqueta = value; } }
    string m_Tabla_Comando_Tooltip = "";
    [Description("Ayuda rapida")]
    [DisplayNameAttribute("Tabla_Comando_Tooltip")]
    public string Tabla_Comando_Tooltip { get { return m_Tabla_Comando_Tooltip; } set { m_Tabla_Comando_Tooltip = value; } }
    bool m_Tabla_Comando_Multiple = true;
    [Description("Indica si permitirá seleccion multiple de filas.")]
    [DisplayNameAttribute("Tabla_Comando_Multiple")]
    public bool Tabla_Comando_Multiple { get { return m_Tabla_Comando_Multiple; } set { m_Tabla_Comando_Multiple = value; } }
    bool m_Tabla_Comando_Grupal = true;
    [Description("Indica que se ejecutará una sola vez el comando ")]
    [DisplayNameAttribute("Tabla_Comando_Grupal")]
    public bool Tabla_Comando_Grupal { get { return m_Tabla_Comando_Grupal; } set { m_Tabla_Comando_Grupal = value; } }
    bool m_Tabla_Comando_Grid = true;
    [Description("Indica que se mostrará en el grid el comando")]
    [DisplayNameAttribute("Tabla_Comando_Grid")]
    public bool Tabla_Comando_Grid { get { return m_Tabla_Comando_Grid; } set { m_Tabla_Comando_Grid = value; } }
    bool m_Tabla_Comando_Edicion = true;
    [Description("Indica que se mostrará en la ventana de edición")]
    [DisplayNameAttribute("Tabla_Comando_Edicion")]
    public bool Tabla_Comando_Edicion { get { return m_Tabla_Comando_Edicion; } set { m_Tabla_Comando_Edicion = value; } }
    bool m_Tabla_Comando_Borrado = false;
    [Description("Indica que esta borrado el comando")]
    [DisplayNameAttribute("Tabla_Comando_Borrado")]
    public bool Tabla_Comando_Borrado { get { return m_Tabla_Comando_Borrado; } set { m_Tabla_Comando_Borrado = value; } }


    public CeC_TablasComandos(CeC_Sesion Sesion)
        : base("EC_TABLAS_COMANDOS", "TABLA_COMANDO_ID", Sesion)
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public CeC_TablasComandos(string TablaNombre, string TablaComandoNombre, CeC_Sesion Sesion)
        : base("EC_TABLAS_COMANDOS", "TABLA_COMANDO_ID", Sesion)
    {
        Carga("TABLA_NOMBRE,TABLA_COMANDO_NOMBRE", new string[] { TablaNombre, TablaComandoNombre }, Sesion);
    }
    public enum ComandoNombre
    {
        Editar,
        Nuevo,
        Borrar
    }

    public bool Actualiza(string TablaNombre, int CatalogoId, ComandoNombre TablaComandoNombre, string TablaComandoCodigo, string TablaComandoEtiqueta, string TablaComandoTooltip, bool TablaComandoMultiple, bool TablaComandoGrupal, bool TablaComandoGrid, bool TablaComandoEdicion, bool TablaComandoBorrado, CeC_Sesion Sesion)
    {
        return Actualiza(-1, TablaNombre, CatalogoId, TablaComandoNombre.ToString(), TablaComandoCodigo, TablaComandoEtiqueta, TablaComandoTooltip, TablaComandoMultiple, TablaComandoGrupal, TablaComandoGrid, TablaComandoEdicion, TablaComandoBorrado, Sesion);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="TablaComandoId">Identificador de comando</param>
    /// <param name="TablaNombre">Nombre de la tabla</param>
    /// <param name="CatalogoId">Identificador de catalogo</param>
    /// <param name="TablaComandoNombre">Nombre del comando</param>
    /// <param name="TablaComandoCodigo">Contiene el codigo de la Clase.Funcion(Parametro1,Parametro2,Parametro3) para variables de sesión usar <IDS></IDS> para lista de identificadores separados por coma,  <SESION>Variable</SESION>, para parametros html <PARAMETRO>Variable</PARAMETRO>, para valor seleccionado <CAMPO>Nombre Campo</CAMPO>, para incognita <PREGUNTA>Campo</PREGUNTA>. el Campo CATALOGO_ID se refiere a la selección del menu</param>
    /// <param name="TablaComandoEtiqueta">Etiqueta del comando</param>
    /// <param name="TablaComandoTooltip">Ayuda rapida</param>
    /// <param name="TablaComandoMultiple">Indica si permitirá seleccion multiple de filas.</param>
    /// <param name="TablaComandoGrupal">Indica que se ejecutará una sola vez el comando </param>
    /// <param name="TablaComandoGrid">Indica que se mostrará en el grid el comando</param>
    /// <param name="TablaComandoEdicion">Indica que se mostrará en la ventana de edición</param>
    /// <param name="TablaComandoBorrado">Indica que esta borrado el comando</param>
    /// <param name="Sesion"></param>
    /// <returns></returns>
    public bool Actualiza(int TablaComandoId, string TablaNombre, int CatalogoId, string TablaComandoNombre, string TablaComandoCodigo, string TablaComandoEtiqueta, string TablaComandoTooltip, bool TablaComandoMultiple, bool TablaComandoGrupal, bool TablaComandoGrid, bool TablaComandoEdicion, bool TablaComandoBorrado, CeC_Sesion Sesion)
    {
        try
        {
            bool Nuevo = false;
            if (TablaComandoId < 0)
            {
                if (!Carga("TABLA_NOMBRE,TABLA_COMANDO_NOMBRE", new string[] { TablaNombre, TablaComandoNombre }, Sesion))
                    Nuevo = true;
            }
            else
            {
                if (!Carga(TablaComandoId, Sesion))
                    Nuevo = true;
            }
            m_EsNuevo = Nuevo;
            if (m_EsNuevo)
            {
                TablaComandoId = CeC_Autonumerico.GeneraAutonumerico("EC_TABLAS_COMANDOS", "TABLA_COMANDO_ID", Sesion);
            }
            Tabla_Comando_Id = TablaComandoId; Tabla_Nombre = TablaNombre; Catalogo_Id = CatalogoId; Tabla_Comando_Nombre = TablaComandoNombre; Tabla_Comando_Codigo = TablaComandoCodigo; Tabla_Comando_Etiqueta = TablaComandoEtiqueta; Tabla_Comando_Tooltip = TablaComandoTooltip; Tabla_Comando_Multiple = TablaComandoMultiple; Tabla_Comando_Grupal = TablaComandoGrupal; Tabla_Comando_Grid = TablaComandoGrid; Tabla_Comando_Edicion = TablaComandoEdicion; Tabla_Comando_Borrado = TablaComandoBorrado;

            return Guarda(Sesion);

        }
        catch { }
        return false;
    }
    /// <summary>
    /// Crea la definicion de campos, solo se deberá ejecutar despues de actualizar la base de datos(en caso de una actualización)
    /// </summary>
    /// <param name="Sesion">Puede ser nulo si no se conoce</param>
    /// <returns></returns>
    public static bool CreaDefinicionCampos(CeC_Sesion Sesion)
    {
        CeC_TablasComandos Comando = new CeC_TablasComandos(Sesion);
        Comando.Actualiza("EC_PERSONAS", 0, ComandoNombre.Nuevo, "URL(WF_EmpleadosEd.aspx?Parametros=<IDS></IDS>)", "", "", false, false, true, false, false, Sesion);
        Comando.Actualiza("EC_PERSONAS", 0, ComandoNombre.Editar, "URL(WF_EmpleadosEd.aspx?Parametros=<IDS></IDS>)", "", "", false, false, true, false, false, Sesion);

        // Comandos para crear un nuevo periodo y editar un periodo
        Comando.Actualiza("EC_PERIODOS", 0, ComandoNombre.Nuevo, "URL(WF_PeriodosE.aspx?Parametros=<IDS></IDS>)", "", "", false, false, true, false, false, Sesion);
        Comando.Actualiza("EC_PERIODOS", 0, ComandoNombre.Editar, "URL(WF_PeriodosE.aspx?Parametros=<IDS></IDS>)", "", "", false, false, true, false, false, Sesion);

        return true;
    }

    public DataSet ObtenDS(CeC_Sesion Sesion, string TablaNombre)
    {
        return ObtenDS(Sesion, TablaNombre, "TABLA_COMANDO_NOMBRE");
    }
}
