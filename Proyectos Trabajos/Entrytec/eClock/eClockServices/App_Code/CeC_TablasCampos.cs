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
/// Descripción breve de CeC_TablasCampos
/// </summary>
public class CeC_TablasCampos : CeC_Tabla
{
    string m_Campo_Nombre = "";
    [Description("Nombre del campo")]
    [DisplayNameAttribute("Campo_Nombre")]
    public string Campo_Nombre { get { return m_Campo_Nombre; } set { m_Campo_Nombre = value; } }
    string m_Tabla_Nombre = "";
    [Description("Nombre de la tabla")]
    [DisplayNameAttribute("Tabla_Nombre")]
    public string Tabla_Nombre { get { return m_Tabla_Nombre; } set { m_Tabla_Nombre = value; } }
    int m_Tipo_Autonumerico_Id = 0;
    [Description("Indica si es autonumérico y el tipo")]
    [DisplayNameAttribute("Tipo_Autonumerico_Id")]
    public int Tipo_Autonumerico_Id { get { return m_Tipo_Autonumerico_Id; } set { m_Tipo_Autonumerico_Id = value; } }
    int m_Tabla_Campo_Orden = 0;
    [Description("Orden que se tendrá para poder ordenar los campos")]
    [DisplayNameAttribute("Tabla_Campo_Orden")]
    public int Tabla_Campo_Orden { get { return m_Tabla_Campo_Orden; } set { m_Tabla_Campo_Orden = value; } }
    string m_Tabla_Campo_Default = "";
    [Description("Valor predeterminado que se tendra en un campo al crear un nuevo registro,")]
    [DisplayNameAttribute("Tabla_Campo_Default")]
    public string Tabla_Campo_Default { get { return m_Tabla_Campo_Default; } set { m_Tabla_Campo_Default = value; } }
    bool m_Tabla_Campo_No_Nulo = false;
    [Description("Verdadero indica que no se permitiran nulos")]
    [DisplayNameAttribute("Tabla_Campo_No_Nulo")]
    public bool Tabla_Campo_No_Nulo { get { return m_Tabla_Campo_No_Nulo; } set { m_Tabla_Campo_No_Nulo = value; } }
    string m_Tabla_Campo_Duplicado = "";
    [Description("Qry para validar la diplicidad del campo ingresado")]
    [DisplayNameAttribute("Tabla_Campo_Duplicado")]
    public string Tabla_Campo_Duplicado { get { return m_Tabla_Campo_Duplicado; } set { m_Tabla_Campo_Duplicado = value; } }
    bool m_Tabla_Campo_Visible = true;
    [Description("Indica si el campo se mostrará en tiempo de edicion")]
    [DisplayNameAttribute("Tabla_Campo_Visible")]
    public bool Tabla_Campo_Visible { get { return m_Tabla_Campo_Visible; } set { m_Tabla_Campo_Visible = value; } }
    bool m_Tabla_Campo_Nuevo = true;
    [Description("Indica si el campo se podrá editar en la creación el registro")]
    [DisplayNameAttribute("Tabla_Campo_Nuevo")]
    public bool Tabla_Campo_Nuevo { get { return m_Tabla_Campo_Nuevo; } set { m_Tabla_Campo_Nuevo = value; } }
    bool m_Tabla_Campo_Editable = true;
    [Description("Indica si el campo seá editable")]
    [DisplayNameAttribute("Tabla_Campo_Editable")]
    public bool Tabla_Campo_Editable { get { return m_Tabla_Campo_Editable; } set { m_Tabla_Campo_Editable = value; } }
    bool m_Tabla_Campo_Importable = true;
    [Description("Indica si el campo se podrá importar")]
    [DisplayNameAttribute("Tabla_Campo_Importable")]
    public bool Tabla_Campo_Importable { get { return m_Tabla_Campo_Importable; } set { m_Tabla_Campo_Importable = value; } }
    string m_Tabla_Campo_Validacion = "";
    [Description("Validación para poder guardar el campo")]
    [DisplayNameAttribute("Tabla_Campo_Validacion")]
    public string Tabla_Campo_Validacion { get { return m_Tabla_Campo_Validacion; } set { m_Tabla_Campo_Validacion = value; } }
    string m_Tabla_Campo_Ayuda = "";
    [Description("Ayuda para la edicion del campo")]
    [DisplayNameAttribute("Tabla_Campo_Ayuda")]
    public string Tabla_Campo_Ayuda { get { return m_Tabla_Campo_Ayuda; } set { m_Tabla_Campo_Ayuda = value; } }
    int m_Tabla_Campo_Columna = -1;
    [Description("Columna en la que se mostrará el elemento de edicion")]
    [DisplayNameAttribute("Tabla_Campo_Columna")]
    public int Tabla_Campo_Columna { get { return m_Tabla_Campo_Columna; } set { m_Tabla_Campo_Columna = value; } }
    int m_Tabla_Campo_Fila = -1;
    [Description("Fila en la que se mostrará el campo de edicion")]
    [DisplayNameAttribute("Tabla_Campo_Fila")]
    public int Tabla_Campo_Fila { get { return m_Tabla_Campo_Fila; } set { m_Tabla_Campo_Fila = value; } }
    int m_Tabla_Campo_Columnas = 1;
    [Description("Columnas que ocupará el objeto")]
    [DisplayNameAttribute("Tabla_Campo_Columnas")]
    public int Tabla_Campo_Columnas { get { return m_Tabla_Campo_Columnas; } set { m_Tabla_Campo_Columnas = value; } }
    int m_Tabla_Campo_Filas = 1;
    [Description("Filas que ocupará el objeto")]
    [DisplayNameAttribute("Tabla_Campo_Filas")]
    public int Tabla_Campo_Filas { get { return m_Tabla_Campo_Filas; } set { m_Tabla_Campo_Filas = value; } }


    public CeC_TablasCampos(CeC_Sesion Sesion)
        : base("EC_TABLAS_CAMPOS", "CAMPO_NOMBRE,TABLA_NOMBRE", Sesion)
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public CeC_TablasCampos(string TablaNombre, string CampoNombre, CeC_Sesion Sesion)
        : base("EC_TABLAS_CAMPOS", "CAMPO_NOMBRE,TABLA_NOMBRE", Sesion)
    {
        Carga(new string[] { CampoNombre, TablaNombre }, Sesion);
    }
    public static string ObtenCamposTabla(string Tabla)
    {
        string Qry = "SELECT CAMPO_NOMBRE FROM EC_TABLAS_CAMPOS WHERE TABLA_NOMBRE='" + Tabla + "' ORDER BY TABLA_CAMPO_ORDEN";
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
    /// Crea la definicion de campos, solo se deberá ejecutar despues de actualizar la base de datos(en caso de una actualización)
    /// </summary>
    /// <param name="Sesion">Puede ser nulo si no se conoce</param>
    /// <returns></returns>
    public static bool CreaDefinicionCampos(CeC_Sesion Sesion)
    {
        string QryDataSetCampos = "";
        if (!CeC_BD.EsOracle)
            QryDataSetCampos = "SELECT SO.NAME as Tabla, SC.NAME as Campo FROM sys.objects SO INNER JOIN sys.columns SC " +
                "ON SO.OBJECT_ID = SC.OBJECT_ID WHERE SO.TYPE = 'U' AND SO.NAME LIKE 'EC_%' ORDER BY SO.NAME, SC.NAME";
        else
            QryDataSetCampos = "SELECT TABLE_NAME as Tabla, COLUMN_NAME as Campo from all_tab_columns where TABLE_NAME like 'EC_%' order by owner, table_name, COLUMN_NAME";
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(QryDataSetCampos);
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return false;
        string TablaNombreAnterior = "";

        foreach (DataRow DR in DS.Tables[0].Rows)
        {
            try
            {
                string TablaNombre = DR["Tabla"].ToString();
                if (TablaNombre != TablaNombreAnterior)
                {
                    TablaNombreAnterior = TablaNombre;
                    CeC_Tablas.CreaDefinicionTabla(TablaNombre, Sesion);
                }
                CreaDefinicionCampo(TablaNombre, DR["Campo"].ToString(), Sesion);

            }
            catch { }
        }
        return true;
    }

    public static bool CreaDefinicionCampo(string TablaNombre, string CampoNombre, CeC_Sesion Sesion)
    {
        CeC_TablasCampos TC = new CeC_TablasCampos(TablaNombre, CampoNombre, Sesion);
        if (TablaNombre == "EC_PERSONAS_DATOS")
            CeC_Campos.CreaDefinicionCampo(CampoNombre, true);
        else
            CeC_Campos.CreaDefinicionCampo(CampoNombre, false);
        if (TC.m_EsNuevo)
        {
            TC.m_EsNuevo = true;
            TC.Campo_Nombre = CampoNombre;
            TC.Tabla_Nombre = TablaNombre;
            return TC.Guarda(Sesion);
        }
        return false;

    }

    /// <summary>
    /// Crea y devueve un objeto que representa al campo ej. editbox
    /// </summary>
    /// <param name="CampoNombre">Nombre del campo</param>
    /// <returns></returns>
    public object CreaObjetoCampo(string CampoNombre)
    {
        return CeC_Campos.CreaCampo(CampoNombre, m_CamposLlave);
    }


    public bool Actualiza(string CampoNombre, string TablaNombre, int TipoAutonumericoId, int TablaCampoOrden, string TablaCampoDefault, bool TablaCampoNoNulo, string TablaCampoDuplicado, bool TablaCampoVisible, bool TablaCampoNuevo, bool TablaCampoEditable, bool TablaCampoImportable, string TablaCampoValidacion, string TablaCampoAyuda, int TablaCampoColumna, int TablaCampoFila, int TablaCampoColumnas, int TablaCampoFilas, CeC_Sesion Sesion)
    {
        try
        {
            bool Nuevo = false;
            if (!Carga(new string[] { CampoNombre, TablaNombre }, Sesion))
                Nuevo = true;
            m_EsNuevo = Nuevo;
            Campo_Nombre = CampoNombre; Tabla_Nombre = TablaNombre; Tipo_Autonumerico_Id = TipoAutonumericoId; Tabla_Campo_Orden = TablaCampoOrden; Tabla_Campo_Default = TablaCampoDefault; Tabla_Campo_No_Nulo = TablaCampoNoNulo; Tabla_Campo_Duplicado = TablaCampoDuplicado; Tabla_Campo_Visible = TablaCampoVisible; Tabla_Campo_Nuevo = TablaCampoNuevo; Tabla_Campo_Editable = TablaCampoEditable; Tabla_Campo_Importable = TablaCampoImportable; Tabla_Campo_Validacion = TablaCampoValidacion; Tabla_Campo_Ayuda = TablaCampoAyuda; Tabla_Campo_Columna = TablaCampoColumna; Tabla_Campo_Fila = TablaCampoFila; Tabla_Campo_Columnas = TablaCampoColumnas; Tabla_Campo_Filas = TablaCampoFilas;
            return Guarda(Sesion);

        }
        catch { }
        return false;
    }

    public static string ObtenCampoNombre(object ControlCampo)
    {

        if (ControlCampo == null)
            return "No Existe el objeto";
        System.Web.UI.WebControls.WebControl WControl = ((System.Web.UI.WebControls.WebControl)ControlCampo);
        if (WControl.ID == null || WControl.ID.Length < 1)
            return "No se encuentra el nombre del campo";
        DS_Campos.EC_CAMPOSRow Campo = CeC_Campos.Obten_Campo(WControl.ID);
        if (Campo == null)
            return "No existe el campo " + WControl.ID;
        return WControl.ID;
    }

    public static string ObtenCampoAyuda(string Tabla, string Campo)
    {
        if (Tabla == "")
            return "";
        return CeC_BD.EjecutaEscalarString("SELECT TABLA_NOMBRE, CAMPO_NOMBRE FROM EC_TABLAS_CAMPOS WHERE TABLA_NOMBRE = '" + Tabla + "' AND CAMPO_NOMBRE = '" + Campo + "'");
    }
}
