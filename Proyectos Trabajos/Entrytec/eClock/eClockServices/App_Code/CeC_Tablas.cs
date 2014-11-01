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
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
/// <summary>
/// Contiene la definicion y propiedades de una tabla
/// </summary>
public class CeC_Tablas : CeC_Tabla
{
    string m_Tabla_Nombre = "";
    [Description("")]
    [DisplayNameAttribute("Tabla_Nombre")]
    public string Tabla_Nombre { get { return m_Tabla_Nombre; } set { m_Tabla_Nombre = value; } }
    string m_Tabla_Etiqueta = "";
    [Description("")]
    [DisplayNameAttribute("Tabla_Etiqueta")]
    public string Tabla_Etiqueta { get { return m_Tabla_Etiqueta; } set { m_Tabla_Etiqueta = value; } }
    string m_Tabla_Plural = "";
    [Description("")]
    [DisplayNameAttribute("Tabla_Plural")]
    public string Tabla_Plural { get { return m_Tabla_Plural; } set { m_Tabla_Plural = value; } }
    string m_Tabla_Singular = "";
    [Description("")]
    [DisplayNameAttribute("Tabla_Singular")]
    public string Tabla_Singular { get { return m_Tabla_Singular; } set { m_Tabla_Singular = value; } }
    string m_Tabla_Descripcion = "";
    [Description("")]
    [DisplayNameAttribute("Tabla_Descripcion")]
    public string Tabla_Descripcion { get { return m_Tabla_Descripcion; } set { m_Tabla_Descripcion = value; } }
    string m_Tabla_Llave = "";
    [Description("")]
    [DisplayNameAttribute("Tabla_Llave")]
    public string Tabla_Llave { get { return m_Tabla_Llave; } set { m_Tabla_Llave = value; } }
    string m_Tabla_Grid_Qry = "";
    [Description("")]
    [DisplayNameAttribute("Tabla_Grid_Qry")]
    public string Tabla_Grid_Qry { get { return m_Tabla_Grid_Qry; } set { m_Tabla_Grid_Qry = value; } }
    string m_Tabla_Icono16 = "";
    [Description("")]
    [DisplayNameAttribute("Tabla_Icono16")]
    public string Tabla_Icono16 { get { return m_Tabla_Icono16; } set { m_Tabla_Icono16 = value; } }
    string m_Tabla_Icono24 = "";
    [Description("")]
    [DisplayNameAttribute("Tabla_Icono24")]
    public string Tabla_Icono24 { get { return m_Tabla_Icono24; } set { m_Tabla_Icono24 = value; } }
    string m_Tabla_Icono32 = "";
    [Description("")]
    [DisplayNameAttribute("Tabla_Icono32")]
    public string Tabla_Icono32 { get { return m_Tabla_Icono32; } set { m_Tabla_Icono32 = value; } }
    string m_Tabla_Icono64 = "";
    [Description("")]
    [DisplayNameAttribute("Tabla_Icono64")]
    public string Tabla_Icono64 { get { return m_Tabla_Icono64; } set { m_Tabla_Icono64 = value; } }
    string m_Tabla_C_Borrado = "";
    [Description("")]
    [DisplayNameAttribute("Tabla_C_Borrado")]
    public string Tabla_C_Borrado { get { return m_Tabla_C_Borrado; } set { m_Tabla_C_Borrado = value; } }
    bool m_Tabla_Historico = false;
    [Description("")]
    [DisplayNameAttribute("Tabla_Historico")]
    public bool Tabla_Historico { get { return m_Tabla_Historico; } set { m_Tabla_Historico = value; } }
    bool m_Tabla_Nuevo = true;
    [Description("")]
    [DisplayNameAttribute("Tabla_Nuevo")]
    public bool Tabla_Nuevo { get { return m_Tabla_Nuevo; } set { m_Tabla_Nuevo = value; } }
    bool m_Tabla_Edicion = true;
    [Description("")]
    [DisplayNameAttribute("Tabla_Edicion")]
    public bool Tabla_Edicion { get { return m_Tabla_Edicion; } set { m_Tabla_Edicion = value; } }
    bool m_Tabla_Borrar = true;
    [Description("")]
    [DisplayNameAttribute("Tabla_Borrar")]
    public bool Tabla_Borrar { get { return m_Tabla_Borrar; } set { m_Tabla_Borrar = value; } }
    string m_Tabla_Hijos = "";
    [Description("")]
    [DisplayNameAttribute("Tabla_Hijos")]
    public string Tabla_Hijos { get { return m_Tabla_Hijos; } set { m_Tabla_Hijos = value; } }
    bool m_Tabla_Val_Suscripcion = false;
    [Description("")]
    [DisplayNameAttribute("Tabla_Val_Suscripcion")]
    public bool Tabla_Val_Suscripcion { get { return m_Tabla_Val_Suscripcion; } set { m_Tabla_Val_Suscripcion = value; } }
    bool m_Tabla_Borrado = false;
    [Description("")]
    [DisplayNameAttribute("Tabla_Borrado")]
    public bool Tabla_Borrado { get { return m_Tabla_Borrado; } set { m_Tabla_Borrado = value; } }



    public CeC_Tablas(CeC_Sesion Sesion)
        : base("EC_TABLAS", "TABLA_NOMBRE", Sesion)
    {
        //Carga("EC_TABLAS", Sesion);
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public CeC_Tablas(string Tabla, string CampoLlave, CeC_Sesion Sesion)
        : base(Tabla, CampoLlave, Sesion)
    {

        Carga(Tabla_Nombre, Sesion);
    }


    /// <summary>
    /// Crea un objeto y carga atumaticamente los parametros del registro
    /// </summary>
    /// <param name="Tabla_Nombre"></param>
    /// <param name="Sesion"></param>
    public CeC_Tablas(string Tabla_Nombre, CeC_Sesion Sesion)
        : base("EC_TABLAS", "TABLA_NOMBRE", Sesion)
    {

        Carga(Tabla_Nombre, Sesion);
    }

    public override string ObtenGridQry(CeC_Sesion Sesion, bool MuestraBorrados)
    {
        string QryBorrado = "";
        if (!MuestraBorrados)
            QryBorrado = "WHERE TABLA_BORRADO = 0 ";
        string Qry = "SELECT TABLA_NOMBRE, TABLA_ETIQUETA FROM " + m_Tabla + " " + QryBorrado + " \n ORDER BY " + m_CamposLlave;
        return Qry;
    }
    /// <summary>
    /// Carga una tabla usando el parametro "Parametro" para obtener 
    /// el nombre de la tabla, si se desea editar un registro, 
    /// se usará pipe (|) seguido de los ids separados por coma para abrir el registro o
    /// -1 para crear uno nuevo
    /// 
    /// </summary>
    /// <param name="Sesion"></param>
    /// <returns></returns>
    public bool Carga(CeC_Sesion Sesion)
    {
        if (Sesion.Parametros.Length < 1)
            return false;
        string[] sParametros = CeC.ObtenArregoSeparador(Sesion.Parametros, "|");

        return base.Carga(sParametros[0], Sesion);
    }

    /// <summary>
    /// Carga una tabla
    /// </summary>
    /// <param name="Tabla_Nombre">Nombre de la Tabla</param>
    /// <param name="Sesion"></param>
    /// <returns></returns>
    public bool Carga(string Tabla_Nombre, CeC_Sesion Sesion)
    {

        return base.Carga(Tabla_Nombre, Sesion);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="TablaNombre">Se refiere a la configuracion de la tabla que se desea editar</param>
    /// <param name="TablaEtiqueta"></param>
    /// <param name="TablaPlural"></param>
    /// <param name="TablaSingular"></param>
    /// <param name="TablaDescripcion"></param>
    /// <param name="TablaLlave"></param>
    /// <param name="TablaGridQry"></param>
    /// <param name="TablaCBorrado"></param>
    /// <param name="TablaNuevo"></param>
    /// <param name="TablaEdicion"></param>
    /// <param name="TablaBorrar"></param>
    /// <param name="TablaHijos"></param>
    /// <param name="Sesion"></param>
    /// <returns></returns>
    public bool Actualiza(string TablaNombre, string TablaEtiqueta, string TablaPlural, string TablaSingular, string TablaDescripcion, string TablaLlave, string TablaGridQry, string TablaCBorrado, bool TablaNuevo, bool TablaEdicion, bool TablaBorrar, string TablaHijos, bool TablaValSuscripcion,
 CeC_Sesion Sesion)
    {
        try
        {
            bool Nuevo = false;
            if (!Carga(TablaNombre, Sesion))
            {
                Nuevo = true;
            }
            m_EsNuevo = Nuevo;
            Tabla_Nombre = TablaNombre; Tabla_Etiqueta = TablaEtiqueta; Tabla_Plural = TablaPlural; Tabla_Singular = TablaSingular; Tabla_Descripcion = TablaDescripcion; Tabla_Llave = TablaLlave; Tabla_Grid_Qry = TablaGridQry; Tabla_C_Borrado = TablaCBorrado; Tabla_Nuevo = TablaNuevo; Tabla_Edicion = TablaEdicion; Tabla_Borrar = TablaBorrar; Tabla_Hijos = TablaHijos; Tabla_Val_Suscripcion = TablaValSuscripcion;
            return Guarda(Sesion);

        }
        catch { }
        return false;
    }

    public static bool CreaDefinicionTabla(string TablaNombre, CeC_Sesion Sesion)
    {
        try
        {
            CeC_Tablas Tabla = new CeC_Tablas(TablaNombre, Sesion);
            if (Tabla.m_EsNuevo)
            {
                Tabla.Tabla_Nombre = TablaNombre;
                Tabla.Tabla_Val_Suscripcion = true;
                return Tabla.Guarda(Sesion);
            }
        }
        catch { }
        return false;
    }

    public static bool IniciaTablas(CeC_Sesion Sesion)
    {
        CeC_TablasCampos.CreaDefinicionCampos(null);
        CeC_Tablas Tablas = new CeC_Tablas(Sesion);
        Tablas.Actualiza("EC_TIPO_NOMINA", "Tipos de nomina", "Tipos de nominas", "Tipo de nomina", "", "TIPO_NOMINA_ID",
            "SELECT TIPO_NOMINA_ID, PERIODO_N_NOM, TIPO_NOMINA_NOMBRE, TIPO_NOMINA_IDEX, TIPO_NOMINA_BORRADO FROM EC_TIPO_NOMINA, EC_PERIODOS_N " +
            " WHERE EC_TIPO_NOMINA.PERIODO_N_ID = EC_PERIODOS_N.PERIODO_N_ID ",
            "TIPO_NOMINA_BORRADO", true, true, true, "", true, Sesion);
        Tablas.Actualiza("EC_PERSONAS", "Empleados", "Empleados", "Empleado", "Catalogo de empleados", "PERSONA_ID",
            "SELECT EC_PERSONAS_DATOS.PERSONA_ID, EC_PERSONAS_DATOS.PERSONA_LINK_ID, EC_PERSONAS_DATOS.NOMBRE_COMPLETO,  " +
            "EC_PERSONAS_DATOS.NOMBRE, EC_PERSONAS_DATOS.APATERNO, EC_PERSONAS_DATOS.AMATERNO, EC_PERSONAS_DATOS.RFC,  " +
            "EC_PERSONAS_DATOS.CURP, EC_PERSONAS_DATOS.IMSS, EC_PERSONAS_DATOS.ESTUDIOS, EC_PERSONAS_DATOS.SEXO,  " +
            "EC_PERSONAS_DATOS.NACIONALIDAD, EC_PERSONAS_DATOS.FECHA_INGRESO, EC_PERSONAS_DATOS.FECHA_BAJA,  " +
            "EC_PERSONAS_DATOS.CENTRO_DE_COSTOS, EC_PERSONAS_DATOS.AREA, EC_PERSONAS_DATOS.DEPARTAMENTO,  " +
            "EC_PERSONAS_DATOS.PUESTO, EC_PERSONAS_DATOS.GRUPO, EC_PERSONAS_DATOS.NO_CREDENCIAL,  " +
            "EC_PERSONAS_DATOS.LINEA_PRODUCCION, EC_PERSONAS_DATOS.COMPANIA, EC_PERSONAS_DATOS.DIVISION,  " +
            "EC_PERSONAS_DATOS.TIPO_NOMINA, PERSONA_BORRADO " +
            "FROM EC_PERSONAS INNER JOIN " +
            "EC_PERSONAS_DATOS ON EC_PERSONAS.PERSONA_ID = EC_PERSONAS_DATOS.PERSONA_ID INNER JOIN " +
            "EC_TURNOS ON EC_PERSONAS.TURNO_ID = EC_TURNOS.TURNO_ID", "PERSONA_BORRADO", true, true, true, "EC_TURNOS", true, Sesion);
        return true;
    }

    public static string QryValidaciones(string NombreTabla, CeC_Sesion Sesion, bool MostrarBorrados = false)
    {
        string Qry = "";
        try
        {
            CeC_Tablas Tabla = new CeC_Tablas(NombreTabla, Sesion);
            if (Tabla.Tabla_Val_Suscripcion)
            {
                Qry = CeC.AgregaSeparador(Qry, " " + NombreTabla + "." + CeC_Autonumerico.ValidaSuscripcion(NombreTabla, Tabla.Tabla_Llave, Sesion.SUSCRIPCION_ID), " AND ");
            }
            if (Tabla.Tabla_C_Borrado != "" && !MostrarBorrados)
            {
                Qry = CeC.AgregaSeparador(Qry, " " + Tabla.Tabla_C_Borrado + " = 0 ", " AND ");
            }
            return Qry;
        }
        catch { }
        return "";
    }

    public static bool ExisteRegistro(string NombreTabla, string CampoLlave, string ValorCampoLlave, CeC_Sesion Sesion, int SuscipcionSeleccionada)
    {
        bool ValidaSuscripcion = ValidarSuscripcion(NombreTabla);
        string Qry = CampoLlave + " = '" + ValorCampoLlave + "' ";
        if (ValidaSuscripcion)
        {
            Qry = CeC.AgregaSeparador(Qry, " " + NombreTabla + "." + CeC_Autonumerico.ValidaSuscripcion(NombreTabla, CampoLlave, SuscipcionSeleccionada), " AND ");
        }
        if (CeC_BD.EjecutaEscalar("SELECT " + CampoLlave + " FROM " + NombreTabla + " WHERE " + Qry) == null)
            return false;
        return true;
    }

    public static bool ValidarSuscripcion(string NombreTabla)
    {
        return CeC_BD.EjecutaEscalarBool("SELECT TABLA_VAL_SUSCRIPCION FROM EC_TABLAS WHERE TABLA_NOMBRE = '" + NombreTabla + "'", false);
    }
    public static string Obten_TablaCBorrado(string NombreTabla)
    {
        return CeC_BD.EjecutaEscalarString("SELECT TABLA_C_BORRADO FROM EC_TABLAS WHERE TABLA_NOMBRE = '" + NombreTabla + "'");
    }
    public static DataSet ObtenListado(int SuscripcionID, string NombreTabla, string CampoLlave, string CampoNombre, string CampoAdicional, string CampoDescripcion, string CampoImagen, bool MostrarBorrados, string Filtro, string Or = "")
    {
        string Qry = "SELECT " + CampoLlave;
        if (CampoNombre != null && CampoNombre.Length > 0)
            Qry += ", " + CampoNombre;
        if (CampoAdicional != null && CampoAdicional.Length > 0)
            Qry += ", " + CampoAdicional;
        if (CampoDescripcion != null && CampoDescripcion.Length > 0)
            Qry += ", " + CampoDescripcion;
        if (CampoImagen != null && CampoImagen.Length > 0)
            Qry += ", " + CampoImagen;
        Qry += " FROM " + NombreTabla;
        bool Validar = ValidarSuscripcion(NombreTabla);
        string Where = "";
        if (Validar)
            Where = CeC.AgregaSeparador(Where, CeC_Autonumerico.ValidaSuscripcion(NombreTabla, CampoLlave, SuscripcionID), " AND ");
        if (!MostrarBorrados)
        {
            string CampoBorrado = Obten_TablaCBorrado(NombreTabla);
            if (CampoBorrado != "")
            {
                Where = CeC.AgregaSeparador(Where, CampoBorrado + " = 0", " AND ");
            }
        }
        if (Filtro != null && Filtro != "")
            Where = CeC.AgregaSeparador(Where, Filtro, " AND ");
        if (Where.Length > 0)
        {
            Qry += " WHERE (" + Where + ")";
            if (Or != null && Or != "")
                Qry += " OR (" + Or + ")";
        }
        return CeC_BD.EjecutaDataSet(Qry);
    }

    class ListadoJson
    {
        public object Llave { get; set; }
        public object Nombre { get; set; }
        public object Adicional { get; set; }
        public object Descripcion { get; set; }
        public object Imagen { get; set; }
        public ListadoJson()
        { }
        public ListadoJson(object oCampoLlave, object oCampoNombre, object oCampoAdicional, object oCampoDescripcion, object oCampoImagen)
        {
            Llave = oCampoLlave;
            Nombre = oCampoNombre;
            Adicional = oCampoAdicional;
            Descripcion = oCampoDescripcion;
            Imagen = oCampoImagen;
        }

    }
    /// <summary>
    /// Obtiene un listado contenido en las tablas de la suscripcion
    /// </summary>
    /// <param name="SuscripcionID"></param>
    /// <param name="CampoLlave"></param>
    /// <returns></returns>
    public static string ObtenListadoJsonSuscripcion(int SuscripcionID, string CampoLlave)
    {
        string Qry = "SELECT	EC_TABLAS_SUS_DATO.TABLA_SUS_DATO_LLAVE, EC_TABLAS_SUS_DATO.TABLA_SUS_DATO \n" +
                    " FROM	EC_TABLAS_SUS_DATO INNER JOIN \n" +
                    " 	EC_TABLAS_SUS ON EC_TABLAS_SUS_DATO.TABLA_SUS_ID = EC_TABLAS_SUS.TABLA_SUS_ID \n" +
                    " WHERE	(EC_TABLAS_SUS.SUSCRIPCION_ID = " + SuscripcionID + ") AND (EC_TABLAS_SUS.TABLA_SUS_NOMBRE = '" + CeC_BD.ObtenParametroCadena(CampoLlave) + "')";
        try
        {
            DataSet DS = CeC_BD.EjecutaDataSet(Qry);
            if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
                return null;
            List<ListadoJson> Tabla = new List<ListadoJson>();
            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                Tabla.Add(new ListadoJson(
                    DR[0],
                    DR[1],
                    null,
                    null,
                     null
                    ));
            }
            string json = JsonConvert.SerializeObject(Tabla);

            return json;
        }
        catch (Exception ex) { CIsLog2.AgregaError(ex); }
        return null;

    }

    public static string ObtenListadoJson(int SuscripcionID, string CampoLlave)
    {
        try
        {
            string ListadoSuscripcion = ObtenListadoJsonSuscripcion(SuscripcionID, CampoLlave);
            if (ListadoSuscripcion != null && ListadoSuscripcion != "")
                return ListadoSuscripcion;
            //Temporalmente no validará si el catalogo esta borrado CATALOGO_BORRADO = 0 AND 
            DataSet DS = CeC_BD.EjecutaDataSet("SELECT CATALOGO_ID,CATALOGO_NOMBRE,CATALOGO_TABLA,CATALOGO_C_LLAVE,CATALOGO_C_DESC," +
                "CATALOGO_C_EXTRA,CATALOGO_QA_SQL,CATALOGO_M_NSEL,CATALOGO_BORRADO FROM EC_CATALOGOS WHERE CATALOGO_C_LLAVE ='" + CeC_BD.ObtenParametroCadena(CampoLlave) + "' OR CATALOGO_ALIAS ='" + CeC_BD.ObtenParametroCadena(CampoLlave) + "'");
            if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
                return null;
            DataRow Fila = DS.Tables[0].Rows[0];
            bool CATALOGO_M_NSEL = CeC.Convierte2Bool(Fila["CATALOGO_M_NSEL"]);
            string Or = "";
            string CampoLLave = CeC.Convierte2String(Fila["CATALOGO_C_LLAVE"]);
            if (!CATALOGO_M_NSEL)
                Or = CampoLLave + " = '0'";
            DS = ObtenListado(SuscripcionID, CeC.Convierte2String(Fila["CATALOGO_TABLA"]),
                CampoLLave, CeC.Convierte2String(Fila["CATALOGO_C_DESC"]), "", "", "", false, "", Or);
            if (DS == null || DS.Tables.Count < 1)
                return null;
            List<ListadoJson> Tabla = new List<ListadoJson>();
            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                Tabla.Add(new ListadoJson(
                    DR[0],
                    DR[1],
                    null,
                    null,
                     null
                    ));
            }

            //        string Otro = JsonConvert.SerializeObject(DS.Tables[0], Formatting.Indented);
            string json = JsonConvert.SerializeObject(Tabla);

            return json;
        }
        catch (Exception ex) { CIsLog2.AgregaError(ex); }
        return null;
    }
    /// <summary>
    /// Obtiene el listado en formato json
    /// </summary>
    /// <param name="SuscripcionID">Id de la suscripcion</param>
    /// <param name="NombreTabla">Nombre de la tabla</param>
    /// <param name="CampoLlave">Campo(s) llave(s) de la tabla</param>
    /// <param name="CampoNombre">Nombre del campo consultado</param>
    /// <param name="CampoAdicional">Si se requiere, nombre del campo adicional</param>
    /// <param name="CampoDescripcion">Campo que contiene la descripcion</param>
    /// <param name="CampoImagen">Campo de la imagen</param>
    /// <param name="MostrarBorrados">Indica si se mostraran o no los datos borrados</param>
    /// <param name="Filtro">Filtro de la tabla</param>
    /// <returns></returns>
    public static string ObtenListadoJson(int SuscripcionID, string NombreTabla, string CampoLlave, string CampoNombre, string CampoAdicional, string CampoDescripcion, string CampoImagen, bool MostrarBorrados, string Filtro, string Or = "")
    {
        DataSet DS = ObtenListado(SuscripcionID, NombreTabla, CampoLlave, CampoNombre, CampoAdicional, CampoDescripcion, CampoImagen, MostrarBorrados, Filtro, Or);
        return DataSet2JSonListModel(DS, CampoLlave, CampoNombre, CampoAdicional, CampoDescripcion, CampoImagen);
    }

    public static string DataSet2JSonListModel(DataSet DS, string CampoLlave, string CampoNombre, string CampoAdicional, string CampoDescripcion, string CampoImagen)
    {
        if (DS == null || DS.Tables.Count < 1)
            return null;
        List<ListadoJson> Tabla = new List<ListadoJson>();
        foreach (DataRow DR in DS.Tables[0].Rows)
        {
            Tabla.Add(new ListadoJson(
                CampoLlave != null && CampoLlave != "" ? DR[CampoLlave] : null,
                CampoNombre != null && CampoNombre != "" ? DR[CampoNombre] : null,
                CampoAdicional != null && CampoAdicional != "" ? DR[CampoAdicional] : null,
                CampoDescripcion != null && CampoDescripcion != "" ? DR[CampoDescripcion] : null,
                CampoImagen != null && CampoImagen != "" ? DR[CampoImagen] : null
                ));
        }

        //        string Otro = JsonConvert.SerializeObject(DS.Tables[0], Formatting.Indented);
        string json = JsonConvert.SerializeObject(Tabla);

        return json;
    }

    public static bool GuardaConsulta(string NombreTabla, string Add, CeC_Sesion Sesion)
    {
        return CeC_Config.GuardaConfig(Sesion.USUARIO_ID, "UltimaConsulta" + Add + NombreTabla, DateTime.Now);
    }
    public static int ObtenNoCambiosTabla(string NombreTabla, string Add, CeC_Sesion Sesion)
    {
        DateTime UltimaConsulta = CeC_Config.ObtenConfig(Sesion.USUARIO_ID, "UltimaConsulta" + Add + NombreTabla, CeC_BD.FechaNula);
        switch (NombreTabla)
        {
            case "ASISTENCIAS":
                return CeC_Asistencias.ObtenTotalRetardos(Sesion.PERSONA_ID, "", UltimaConsulta, DateTime.Now, Sesion.USUARIO_ID) +
                    CeC_Asistencias.ObtenTotalFaltas(Sesion.PERSONA_ID, "", UltimaConsulta, DateTime.Now, Sesion.USUARIO_ID) +
                    CeC_Asistencias.ObtenTotalJustificaciones(Sesion.PERSONA_ID, "", UltimaConsulta, DateTime.Now, Sesion.USUARIO_ID)
                    ;
                break;
            case "MENSAJES":
                return CeC_Mails.ObtenNoMensajes(UltimaConsulta, -1, Sesion);
                break;

        }
        return CeC_Autonumerico.ObtenNoCambios(NombreTabla, ValidarSuscripcion(NombreTabla), UltimaConsulta, Sesion);
    }

    public static string ObtenNoCambiosTablas(string Tablas, string Add, CeC_Sesion Sesion)
    {
        try
        {
            string R = "";
            string[] sTablas = CeC.ObtenArregoSeparador(Tablas, ",");
            foreach (string sTabla in sTablas)
            {
                R = CeC.AgregaSeparador(R, ObtenNoCambiosTabla(sTabla, Add, Sesion).ToString(), ",");
            }
            return R;
        }
        catch { }
        return "";
    }
}
