using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.ComponentModel;
using System.IO;

//using System.Drawing;
/// <summary>
/// Esta clase esta diseñada para contener todas las variables globales por usuario del eClock Web
/// </summary>
public class CeC_Sesion
{

    /// <summary>
    /// Constructor
    /// </summary>
    public CeC_Sesion()
    {
        Configura = new CeC_Config();

    }

    // Attributes

    /// <summary>
    /// Contiene un objeto de configuración ligado a la sesión actual
    /// </summary>
    public CeC_Config Configura;

    /// <summary>
    /// Configuraciones de la suscripcion
    /// </summary>
    private CeC_ConfigSuscripcion m_ConfiguraSuscripcion = null;
    public CeC_ConfigSuscripcion ConfiguraSuscripcion
    {
        get
        {
            if (m_ConfiguraSuscripcion == null)
                m_ConfiguraSuscripcion = new CeC_ConfigSuscripcion(SUSCRIPCION_ID);
            return m_ConfiguraSuscripcion;
        }
        set { m_ConfiguraSuscripcion = value; }
    }

    public System.Web.UI.Page m_PaginaWeb
    {
        get { return (System.Web.UI.Page)m_ObjetoWeb; }
        set { m_ObjetoWeb = value; }
    }

    /// <summary>
    /// Obtiene el valor Random de la url
    /// </summary>
    public string RND
    {
        get
        {
            try
            {
                string Valor = m_PaginaWeb.Request.Params["RND"].ToString();
                return Valor;
            }
            catch
            {
            }
            return "";
        }
    }

    /// <summary>
    /// Obtiene el contenido de la variable Parametros la cual es parametro de la pagina
    /// </summary>
    public string Parametros
    {
        get
        {
            try
            {
                string Valor = m_PaginaWeb.Request.Params["Parametros"].ToString();
                return Valor;
            }
            catch
            {
            }
            return "";
        }
    }
    /// <summary>
    /// Revisa la longitud de la cadena
    /// </summary>
    /// <param name="Cadena"></param>
    /// <returns></returns>
    public static string RevisaCadena(string Cadena)
    {
        string Caracter = "";
        string Retorno = "";
        for (int Cont = 0; Cont < Cadena.Length; Cont++)
        {
            Caracter = Cadena.Substring(Cont, 1);
            if (Caracter == "\\")
            {
                Caracter += "\\";
            }
            Retorno += Caracter;
        }
        return Retorno;
    }
    private int m_SESION_ID = -1;
    /// <summary>
    /// Obtiene el identificador de sesion Sesion_ID
    /// </summary>
    public int SESION_ID
    {
        get
        {
            if (m_ObjetoWeb == null)
                return m_SESION_ID;
            return LeeIntSesion(m_ObjetoWeb, "SESION_ID", 0);
        }
        set
        {
            if (m_ObjetoWeb == null)
                m_SESION_ID = value;
            else
                GuardaIntSesion(m_ObjetoWeb, "SESION_ID", value);
        }
    }


    private int m_USUARIO_ID = -1;
    /// <summary>
    /// Obtiene o establece el identificador al usuario que esta actualmente logeado en el sistema
    /// </summary>
    public int USUARIO_ID
    {
        get
        {
            if (m_USUARIO_ID > 0) return m_USUARIO_ID;
            return LeeIntSesion(m_ObjetoWeb, "USUARIO_ID", 0);
        }
        set
        {
            if (m_ObjetoWeb == null)
                m_USUARIO_ID = value;
            else
                if (USUARIO_ID != value)
                    IniciaSesion(value);
        }

    }
    private int m_PERFIL_ID = -1;
    /// <summary>
    /// Obtiene el identificador al perfil que tiene asignado el usuario
    /// </summary>
    /// <remarks></remarks>
    public int PERFIL_ID
    {
        get
        {
            if (m_ObjetoWeb == null)
                return m_PERFIL_ID;
            return LeeIntSesion(m_ObjetoWeb, "PERFIL_ID", 0);
        }
        set
        {
            if (m_ObjetoWeb == null)
                m_PERFIL_ID = value;
            else
                GuardaIntSesion(m_ObjetoWeb, "PERFIL_ID", value);
        }
    }


    private int m_SUSCRIPCION_ID = -1;
    /// <summary>
    /// Obtiene el identificador a la suscripcion predeterminada que tiene asignado el usuario
    /// </summary>
    /// <remarks></remarks>
    public int SUSCRIPCION_ID
    {
        get
        {
            if (m_ObjetoWeb == null)
                return m_SUSCRIPCION_ID;
            return LeeIntSesion(m_ObjetoWeb, "SUSCRIPCION_ID", 0);
        }
        set
        {
            if (m_ObjetoWeb == null)
                m_SUSCRIPCION_ID = value;
            else
                GuardaIntSesion(m_ObjetoWeb, "SUSCRIPCION_ID", value);
        }
    }

    private int m_PERSONA_ID = -1;
    /// <summary>
    /// Obtiene o establece el identificador de la persona_id que esta ligado el usuario que esta actualmente logeado en el sistema
    /// </summary>
    public int PERSONA_ID
    {
        get
        {
            if (m_ObjetoWeb == null)
                return m_PERSONA_ID;
            return LeeIntSesion(m_ObjetoWeb, "PERSONA_ID", 0);
        }
        set
        {
            if (m_ObjetoWeb == null)
                m_PERSONA_ID = value;
            else
                GuardaIntSesion(m_ObjetoWeb, "PERSONA_ID", value);
        }

    }
    /// <summary>
    /// ID del usuario que es poseedor de la suscripción
    /// </summary>
    public int USUARIO_ID_SUSCRIPCION
    {
        get
        {
            int R = LeeIntSesion(m_ObjetoWeb, "USUARIO_ID_SUSCRIPCION", -1);
            if (R > 0)
                return R;
            USUARIO_ID_SUSCRIPCION = R = CeC_Suscripcion.ObtenUsuarioID(SUSCRIPCION_ID);
            return R;
        }
        set
        {
            GuardaIntSesion(m_ObjetoWeb, "USUARIO_ID_SUSCRIPCION", value);
        }

    }
    /// <summary>
    /// Obtiene el Nombre del Usuario
    /// </summary>
    public string USUARIO_NOMBRE
    {
        get { return LeeStringSesion(m_ObjetoWeb, "USUARIO_NOMBRE"); }
    }
    /// <summary>
    /// Obtiene el Usuario (login)
    /// </summary>
    public string USUARIO_USUARIO
    {
        get { return LeeStringSesion(m_ObjetoWeb, "USUARIO_USUARIO"); }
    }

    /// <summary>
    /// Contiene el objeto Pagina pasado como argumento en el método Nuevo
    /// </summary>
    protected object m_ObjetoWeb = null;

    private int GeneraSuscripcion(string Suscripcion)
    {
        int SUSCRIPCION_ID = CeC_Autonumerico.GeneraAutonumerico("EC_SUSCRIPCION", "SUSCRIPCION_ID");
        CeC_BD.EjecutaComando("INSERT INTO EC_SUSCRIPCION (SUSCRIPCION_ID, SUSCRIPCION_NOMBRE) VALUES(" + SUSCRIPCION_ID + ", '" + Suscripcion + "')");
        return SUSCRIPCION_ID;
    }

    // Associations

    public static int ObtenSesionID(object EstaPagina)
    {
        return LeeIntSesion(EstaPagina, "SESION_ID", 0);
    }


    public static int CreaSesionID(int UsuarioID)
    {
        return CreaSesionID(UsuarioID, 0);
    }
    /// <summary>
    /// Carga una nueva instancia de la clase cec_sesion con los siguientes datos
    ///     SESION_ID, USUARIO_ID, PERFIL_ID, SUSCRIPCION_ID, PERSONA_ID, Configura
    ///     haciendo como referencia una consulta ala base de datos.
    /// </summary>
    /// <param name="SESION_ID, USUARIO_ID, PERFIL_ID, SUSCRIPCION_ID, PERSONA_ID, Configura"></param>
    /// <returns></returns>
    public static CeC_Sesion Carga(string Sesion_Seguridad)
    {
        if (!ValidaSeguridad(Sesion_Seguridad))
            return null;

        int SesionID = Sesion_Seguridad2SesionID(Sesion_Seguridad);
        if (SesionID < 1)
            return null;
        CeC_Sesion Sesion = new CeC_Sesion();
        string Qry = "SELECT     EC_USUARIOS.USUARIO_ID, EC_USUARIOS.PERFIL_ID, EC_USUARIOS.SUSCRIPCION_ID, EC_USUARIOS.PERSONA_ID " +
        "FROM EC_SESIONES INNER JOIN EC_USUARIOS ON EC_SESIONES.USUARIO_ID = EC_USUARIOS.USUARIO_ID WHERE SESION_SEGURIDAD = '" + Sesion_Seguridad + "' AND SESION_ID = " + SesionID;
        DataSet Datos = CeC_BD.EjecutaDataSet(Qry);
        if (Datos == null || Datos.Tables.Count == 0 || Datos.Tables[0].Rows.Count == 0)
            return null;
        DataRow DRUsuario = Datos.Tables[0].Rows[0];
        Sesion.SESION_ID = SesionID;
        Sesion.USUARIO_ID = CeC.Convierte2Int(DRUsuario["USUARIO_ID"]);
        Sesion.PERFIL_ID = CeC.Convierte2Int(DRUsuario["PERFIL_ID"]);
        Sesion.SUSCRIPCION_ID = CeC.Convierte2Int(DRUsuario["SUSCRIPCION_ID"]);
        Sesion.PERSONA_ID = CeC.Convierte2Int(DRUsuario["PERSONA_ID"]);
        Sesion.Configura = new CeC_Config(Sesion.USUARIO_ID);
        return Sesion;
    }

    /// <summary>
    /// Convierte una cadena de tipo string es sus tipos binarios
    /// para posteriormente comparar la longitud de esa misma cadena en formato bytes con
    /// una variable SesionId igualmente de tipo byte y guardar en BUFFER cierta cantidad de bytes
    /// de cada varible
    /// </summary>
    /// <param name="Sesion_Seguridad, SesionID"></param>
    /// <returns></returns>
    public static int Sesion_Seguridad2SesionID(string Sesion_Seguridad)
    {
        try
        {
            byte[] binaryData;
            binaryData = System.Convert.FromBase64String(Sesion_Seguridad);
            int SesionID = 0;
            byte[] baSesionID = BitConverter.GetBytes(SesionID);
            if (binaryData.Length > baSesionID.Length)
            {
                System.Buffer.BlockCopy(binaryData, 0, baSesionID, 0, baSesionID.Length);
                SesionID = BitConverter.ToInt32(baSesionID, 0);
                return SesionID;
            }
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return -1;
    }
    /// <summary>
    /// Convierte el ID de sesion para posteriormente obtener los bytes
    /// y codificar el ID de sesion para ser almacenado dentro de un BUFFER
    /// y obtener la Seguridad de ID de Sesion
    /// </summary>
    /// <param name="SesionID"></param>
    /// <returns></returns>
    public static string ObtenSeguridadSesionID(int SesionID)
    {
        byte[] baSesionID = BitConverter.GetBytes(SesionID);
        int Chk = 0;
        foreach (byte Dato in baSesionID)
        {
            Chk += (Dato % 8) * 3;
        }
        byte[] baChk = BitConverter.GetBytes(Chk);
        byte[] rv = new byte[baSesionID.Length + baChk.Length];

        System.Buffer.BlockCopy(baSesionID, 0, rv, 0, baSesionID.Length);
        System.Buffer.BlockCopy(baChk, 0, rv, baSesionID.Length, baChk.Length);

        return System.Convert.ToBase64String(rv);
    }

    public static int CreaSesionID(int UsuarioID, int Terminal_ID)
    {
        int SesionID = CeC_Autonumerico.GeneraAutonumerico("EC_SESIONES", "SESION_ID");
        string Qry = "INSERT INTO EC_SESIONES (SESION_ID, USUARIO_ID, SESION_INICIO_FECHAHORA, SESION_TERMINAL_ID, SESION_SEGURIDAD) VALUES( " +
            SesionID.ToString() + "," + UsuarioID.ToString() + "," + CeC_BD.QueryFechaHora + "," + Terminal_ID + ",'" + ObtenSeguridadSesionID(SesionID) + "' )";

        if (CeC_BD.EjecutaComando(Qry) <= 0)
            return 0;

        return SesionID;
    }

    /// <summary>
    /// Carga la sesion con un codigo de seguridad enviado através de la variable SesionSeguridad
    /// La variable de SesionSeguridad lleva el codigo de seguridad que autentificara la sesion
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <returns>TRUE o FALSE dependiendo de la verificacion de la variable</returns>
    public static bool ValidaSeguridad(string Sesion_Seguridad)
    {
        return ValidaSeguridad(Sesion_Seguridad, true);
    }
    /// <summary>
    /// Valida si la sesion sigue activa y actualiza la fecha de ultima ejecución 
    /// se validará futuramente dicha fecha para que no exceda el timeout
    /// </summary>
    /// <param name="Sesion_Seguridad"></param>
    /// <returns></returns>
    public static bool ValidaSeguridad(string Sesion_Seguridad, bool Actualizar)
    {
        int SesionID = Sesion_Seguridad2SesionID(Sesion_Seguridad);
        string Str = ObtenSeguridadSesionID(SesionID);
        if (Str != Sesion_Seguridad)
            return false;
        if (!Actualizar)
            return true;
        //A resolver, en multiejecucion falla
        int R = CeC_BD.EjecutaComando("UPDATE EC_SESIONES SET SESION_ACT_FECHAHORA = " + CeC_BD.QueryFechaHora + " WHERE SESION_SEGURIDAD = '" + Sesion_Seguridad + "' AND SESION_FIN_FECHAHORA IS NULL ");
        if (R <= 0)
        {
            if (CeC_BD.EjecutaEscalarInt("SELECT SESION_ID FROM EC_SESIONES  WHERE SESION_SEGURIDAD = '" + Sesion_Seguridad + "' AND SESION_FIN_FECHAHORA IS NULL ") <= 0)
                return false;
        }
        return true;
    }

    public static int CreaSesion(int UsuarioID, int Terminal_ID)
    {
        int SesionID = CeC_Autonumerico.GeneraAutonumerico("EC_SESIONES", "SESION_ID");
        string qry = "INSERT INTO EC_SESIONES (SESION_ID, USUARIO_ID, SESION_INICIO_FECHAHORA, SESION_TERMINAL_ID) VALUES( " + SesionID.ToString() + "," + UsuarioID.ToString() + "," + CeC_BD.SqlFechaHora(DateTime.Now) + ", " + Terminal_ID + " )";
        int ret = CeC_BD.EjecutaComando(qry);
        return SesionID;
    }

    // Operations
    /// <summary>
    /// Inicia sesión y crea un registro en la tabla de sesiones
    /// </summary>
    public bool IniciaSesion(int Usuario_ID)
    {
        if (Usuario_ID <= 0)
        {
            return false;
        }
        m_USUARIO_ID = Usuario_ID;
        try
        {
            int SesionID = CreaSesionID(Usuario_ID);
            string Qry;
            /*int SesionID = CeC_Autonumerico.GeneraAutonumerico("EC_SESIONES", "SESION_ID");
            string Qry = "INSERT INTO EC_SESIONES (SESION_ID, USUARIO_ID, SESION_INICIO_FECHAHORA) VALUES( " +
                SesionID.ToString() + "," + Usuario_ID.ToString() + "," + CeC_BD.QueryFechaHora + " )";
            */
            if (SesionID <= 0)
            {
                CeC_BD.IniciaBase(true);
                return false;
            }

            Qry = "SELECT USUARIO_USUARIO, USUARIO_NOMBRE, EC_PERFILES.PERFIL_ID, EC_PERFILES.PERFIL_NOMBRE , SUSCRIPCION_ID " +
                "FROM EC_USUARIOS, EC_PERFILES WHERE EC_PERFILES.PERFIL_ID = EC_USUARIOS.PERFIL_ID AND " +
                " USUARIO_ID =" + Usuario_ID.ToString();
            DataSet Datos = CeC_BD.EjecutaDataSet(Qry);
            if (Datos == null || Datos.Tables.Count == 0 || Datos.Tables[0].Rows.Count == 0)
                return false;
            DataRow DRUsuario = Datos.Tables[0].Rows[0];

            Configura.USUARIO_ID = Usuario_ID;
            GuardaIntSesion(m_ObjetoWeb, "USUARIO_ID", Usuario_ID);
            GuardaIntSesion(m_ObjetoWeb, "PERFIL_ID", CeC.Convierte2Int(DRUsuario["PERFIL_ID"]));

            int SUSCRIPCION_ID = CeC.Convierte2Int(DRUsuario["SUSCRIPCION_ID"]);
            if (SUSCRIPCION_ID == 0)
            {
                SUSCRIPCION_ID = GeneraSuscripcion(USUARIO_USUARIO);
                CeC_BD.EjecutaComando("UPDATE EC_USUARIOS SET SUSCRIPCION_ID = " + SUSCRIPCION_ID + "  WHERE USUARIO_ID = " + Usuario_ID);
            }
            GuardaIntSesion(m_ObjetoWeb, "SUSCRIPCION_ID", SUSCRIPCION_ID);

            ConfiguraSuscripcion = new CeC_ConfigSuscripcion(SUSCRIPCION_ID);
            GuardaIntSesion(m_ObjetoWeb, "SESION_ID", SesionID);
            int PersonaID = CeC_Usuarios.ObtenPersonaID(Usuario_ID);
            if (PersonaID > 0)
                PERSONA_ID = PersonaID;
            return true;
        }
        catch (System.Exception e)
        {
            //     if (Conexion != null)
            //        Conexion.Dispose();
        }
        return false;


    }

    /// <summary>
    /// Es indispensable llamar a este método para que se liguen los datos con la sesión actual, de lo contrario todos los datos devueltos por esta clase seran erroneos.
    /// </summary>
    static public CeC_Sesion Nuevo(Object Pagina)
    {

        return Nuevo(Pagina, -1);
    }


    /// <summary>
    /// Es indispensable llamar a este método para que se liguen los datos con la sesión actual, de lo contrario todos los datos devueltos por esta clase seran erroneos.
    /// </summary>
    static public CeC_Sesion Nuevo(Object Pagina, int Usuario_ID)
    {
        CeC_Sesion Sesion = null;
        if (Pagina == null)
        {
            return null;
        }
        try
        {
            Sesion = new CeC_Sesion();
            Sesion.m_ObjetoWeb = Pagina;
            if (Usuario_ID > 0)
                Sesion.USUARIO_ID = Usuario_ID;
            if (Usuario_ID <= 0)
                if (Sesion.SESION_ID > 0)
                    if (Sesion.SESION_ID <= 0)
                    {

                    }
                    else
                    {
                        Sesion.Configura = new CeC_Config(Sesion.USUARIO_ID);
                        //Ahorra sentencias SQL
                        Sesion.ConfiguraSuscripcion = CeC_ConfigSuscripcion.Nuevo(Sesion.USUARIO_ID_SUSCRIPCION);
                    }

            return Sesion;
        }
        catch (System.Exception e)
        {

        }
        return Sesion;
    }

    /// <summary>
    /// Agrega un log siempre que se consulte(el modulo), borre, edite, o agregue un registro.
    /// </summary>
    /// 

    public int Obten_ID_Log_Auditoria(string Contenido)
    {
        int ret = CeC_BD.EjecutaEscalarInt("SELECT TIPO_AUDITORIA_ID FROM EC_TIPO_AUDITORIA WHERE  TIPO_AUDITORIA_NOMBRE = '" + Contenido + "'");

        if (ret > 0)
            return ret;
        else
        {
            int Tipo_Auditoria_ID = CeC_Autonumerico.GeneraAutonumerico("EC_TIPO_AUDITORIA", "TIPO_AUDITORIA_ID");

            int ret2 = CeC_BD.EjecutaComando("INSERT INTO EC_TIPO_AUDITORIA(TIPO_AUDITORIA_ID,TIPO_AUDITORIA_NOMBRE) VALUES (" + Tipo_Auditoria_ID + ",'" + Contenido.Trim() + "')");

            if (ret2 > 0)
                return Tipo_Auditoria_ID;
            else
                return -1;

        }
    }

    public bool AgregaLogModulo(LOG_TABLA_TIPO Tipo, String NombreModulo, int Llave, string Descripcion)
    {
        return AgregaLogModulo(Tipo, NombreModulo, Llave, Descripcion, this.SESION_ID);
    }

    /// <summary>
    /// Agrega un LOG a un Modulo
    /// </summary>
    /// <param name="Tipo"></param>
    /// <param name="NombreModulo"></param>
    /// <param name="Llave"></param>
    /// <param name="Descripcion"></param>
    /// <param name="sesion_id"></param>
    /// <returns></returns>
    public bool AgregaLogModulo(LOG_TABLA_TIPO Tipo, String NombreModulo, int Llave, string Descripcion, int sesion_id)
    {
        string Respuesta = "";
        switch (Tipo)
        {
            case LOG_TABLA_TIPO.BORRADO:
                Respuesta = "Borrado";
                break;
            case LOG_TABLA_TIPO.CONSULTA:
                Respuesta = "Consulta";
                break;
            case LOG_TABLA_TIPO.EDICION:
                Respuesta = "Edicion";
                break;
            case LOG_TABLA_TIPO.NUEVO:
                Respuesta = "Nuevo";
                break;
            case LOG_TABLA_TIPO.OTRO:
                Respuesta = "Otro";
                break;
        }
        string TipoLogAud = NombreModulo + "->" + Respuesta;
        string mensaje = "ID =" + Llave + "\\" + Descripcion;
        mensaje = mensaje.Replace("'", "''");
        int Log_Id = CeC_Autonumerico.GeneraAutonumerico("EC_LOG_AUDITORIA", "LOG_AUDITORIA_ID");
        CeC_BD.EjecutaComando("INSERT INTO eC_LOG_AUDITORIA(LOG_AUDITORIA_ID, SESION_ID, LOG_AUDITORIA_FECHAHORA, TIPO_AUDITORIA_ID, LOG_AUDITORIA_DESCRIPCION) VALUES (" + Log_Id + "," + sesion_id + "," + CeC_BD.SqlFechaHora(DateTime.Now) + "," + Obten_ID_Log_Auditoria(TipoLogAud) + ",'" + mensaje + "')");
        return true;
    }
    /// <summary>
    /// Agrega un modulo al log de transacciones.
    /// </summary>
    /// <param name="Tipo"></param>
    /// <param name="NombreModulo"></param>
    /// <param name="Llave"></param>
    /// <param name="Descripcion"></param>
    /// <param name="sesion_id"></param>
    /// <returns></returns>
    public static bool SAgregaLogModulo(LOG_TABLA_TIPO Tipo, String NombreModulo, int Llave, string Descripcion, int sesion_id)
    {
        CeC_Sesion Sesion = new CeC_Sesion();
        return Sesion.AgregaLogModulo(Tipo, NombreModulo, Llave, Descripcion, sesion_id);
    }
    public static bool SAgregaLog(string Descripcion, int sesion_id)
    {
        Console.WriteLine("SAgregaLog = {0:G}", Descripcion);
        int Log_Id = CeC_Autonumerico.GeneraAutonumerico("EC_LOG_AUDITORIA", "LOG_AUDITORIA_ID");
        CeC_BD.EjecutaComando("INSERT INTO eC_LOG_AUDITORIA(LOG_AUDITORIA_ID, SESION_ID, LOG_AUDITORIA_FECHAHORA, TIPO_AUDITORIA_ID, LOG_AUDITORIA_DESCRIPCION) VALUES (" + Log_Id + "," + sesion_id + "," + CeC_BD.SqlFechaHora(DateTime.Now) + ",0,'" + Descripcion + "')");

        return true;
    }
    /// <summary>
    /// Agrega un Log y lo muestra en la consola
    /// </summary>
    /// <param name="Descripcion"></param>
    /// <returns></returns>
    public static bool SAgregaLog(string Descripcion)
    {
        return SAgregaLog(Descripcion, 0);
    }


    /// <summary>
    /// Agrega un log siempre que ocurra un error.
    /// </summary>
    public bool AgregaLogEror(String Descripcion)
    {
        Console.WriteLine("AgregaLogEror = {0:G}", Descripcion);
        return true;
    }
    /// <summary>
    /// Agrega un log cuando ocurre un error
    /// </summary>
    /// <param name="Descripcion"></param>
    /// <returns></returns>
    public static bool SAgregaLogEror(String Descripcion)
    {
        Console.WriteLine("SAgregaLogEror = {0:G}", Descripcion);
        return true;
    }
    /// <summary>
    /// Cambia los espacios por un guion bajo
    /// </summary>
    /// <param name="Cadena"></param>
    /// <returns></returns>
    public static string NormalizaCadena(string Cadena)
    {
        return Cadena.Replace(" ", "_");
    }

    /// <summary>
    /// Obtiene el nombre del pdf
    /// </summary>
    public string ObtenNombreReportesPDF(string Reporte)
    {
        string Cadena = CeC_Config.RutaReportesPDF + this.SESION_ID.ToString() + "_" + Reporte.Trim() + ".pdf";
        this.NOMBRE_REPORTE_ACTUAL = Cadena;
        return NormalizaCadena(Cadena);
    }
    /// <summary>
    /// Obtiene el nombre del pdf en una variable de sesion
    /// </summary>
    public string NOMBRE_REPORTE_ACTUAL
    {
        get { return LeeStringSesion(m_ObjetoWeb, "NOMBRE_REPORTE_ACTUAL"); }
        set { GuardaStringSesion(m_ObjetoWeb, "NOMBRE_REPORTE_ACTUAL", value); }
    }


    public string ObtenRutaReportesPDF(string Reporte, bool EsAutomatico)
    {
        if (EsAutomatico)
            return CeC_Config.ENV_AUT_MAILS_RUTA_TEMP + CeC_Config.RutaReportesPDF + "AsistenciaPorMail.pdf";
        System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
        string Cadena = CeC_Config.RutaReportesPDF;
        //			string Cadena = Globales.CadenaConnOLE();
        if (Cadena[1] != ':')
        {
            Cadena = HttpRuntime.AppDomainAppPath + Cadena;
        }
        return Cadena + NormalizaCadena(this.SESION_ID.ToString() + "_" + Reporte.Trim() + ".pdf");


    }

    public static string ObtenRutaArchivoTemp(string Nombre)
    {
        string Cadena = HttpRuntime.AppDomainAppPath + CeC_Config.RutaReportesPDF + Nombre.Trim();
        return Cadena;
    }


    /// <summary>
    /// Regresa la ruta de las imagenes
    /// </summary>
    /// <returns></returns>
    public string ObtenDirImagenes()
    {

        System.Configuration.AppSettingsReader LectorConfiguracion = new System.Configuration.AppSettingsReader();

        string Ruta = ((string)LectorConfiguracion.GetValue("RutaReportesImagen", typeof(string)));
        return Ruta;
    }
    /// <summary>
    /// Convierte la imagen a bytes
    /// </summary>
    /// <param name="NombreImagen">Nombre de la Imagen</param>
    /// <returns></returns>
    public byte[] ConvertirImagen(string NombreImagen)
    {

        FileStream ffs = new FileStream(NombreImagen, FileMode.Open);
        BinaryReader br = new BinaryReader(ffs);
        byte[] datos = new byte[(int)ffs.Length];
        br.Read(datos, 0, (int)ffs.Length);
        br.Close();
        ffs.Close();

        return datos;
    }
    /// <summary>
    /// Regresa el nombre del archivo
    /// </summary>
    /// <param name="Direccion">Direccion o ruta</param>
    /// <returns></returns>
    public string ObtenerNombreArchivoImagen(string Direccion)
    {
        string letra = "";
        string NArchivo = "";


        for (int i = Direccion.Length - 1; i > -1; i--)
        {
            letra = Direccion.Substring(i, 1);
            if (letra == "\\")
            {
                break;
            }
            else
            {
                NArchivo = letra + NArchivo;
            }

        }

        return NArchivo;
    }
    /// <summary>
    /// Reemplaza en la consulta los parametros
    /// </summary>
    /// <param name="Query"></param>
    /// <returns></returns>
    public string ExaminarQuery(string Query)
    {
        string totalqry = "";
        Query = Query.Replace("and (EC_PERSONAS.SUSCRIPCION_ID in (Select EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID from EC_PERMISOS_SUSCRIP where EC_PERMISOS_SUSCRIP.usuario_id = " + USUARIO_ID + "))", " Asignados a Usuario " + USUARIO_NOMBRE);
        Query = Query.Replace("SELECT", " Selecciona ");
        //Query = Query.Replace("SUSCRIPCION_ID", CeC_BD.CeC_Config.CampoGrupo1);
        Query = Query.Replace("SELECT EC_PERSONAS.PERSONA_ID FROM EC_PERSONAS, EC_PERSONAS_DATOS WHERE EC_PERSONAS.PERSONA_LINK_ID = EC_PERSONAS_DATOS.PERSONA_LINK_ID AND ", "");
        Query = Query.Replace("SELECT EC_PERSONAS.PERSONA_ID FROM EC_PERSONAS, EC_PERSONAS_DATOS WHERE EC_PERSONAS.PERSONA_LINK_ID = EC_PERSONAS_DATOS.PERSONA_LINK_ID ", "");
        Query = Query.Replace("FROM", " de la Tabla  ");
        Query = Query.Replace("WHERE", " donde ");
        Query = Query.Replace("LIKE", " como ");
        Query = Query.Replace("AND", " y ");
        Query = Query.Replace("EC_PERSONAS.PERSONA_LINK_ID = EC_PERSONAS_DATOS." + CeC_Campos.CampoTE_Llave, "");
        Query = Query.Replace("EC_PERSONAS.PERSONA_LINK_ID = EC_PERSONAS_DATOS." + CeC_Campos.CampoTE_Llave + "  y", "");
        Query = Query.Replace("PERSONAS.PERSONA_ID", "");
        Query = Query.Replace("de la Tabla   EC_PERSONAS,", "");
        Query = Query.Replace("SUSCRIPCION_ID", CeC_Config.NombreGrupo1);
        Query = Query.Replace("GRUPO_2_ID", CeC_Config.NombreGrupo2);
        Query = Query.Replace("GRUPO_3_ID", CeC_Config.NombreGrupo3);
        /*11-07-08*/
        Query = Query.Replace("UPPER(", "");
        Query = Query.Replace(")", "");
        Query = Query.Replace("like", "como");
        Query = Query.Replace("EC_PERSONAS.turno_id", "Turno ");
        Query = Query.Replace("convert(datetime,", "");
        Query = Query.Replace(",103", "");
        //        Query = Query.Replace("PERSONA_", "");
        Query = Query.Replace("%", "");
        Query = Query.Replace("EC_", "");
        Query = Query.Replace("   ", " ");
        Query = Query.Replace("  ", " ");
        Query = Query.Replace("EMPLEADOS.", "");
        Query = Query.Replace("donde Desde", "Desde");
        Query = Query.Replace("donde y", "donde");
        Query = Query.Replace("EC_terminales.terminal_id", "Terminal");
        if (Query.EndsWith("donde ") == true)
            Query = Query.Replace("donde ", "");

        /* if(coQuery.Substring(X)>6)
             Query.Replace("donde", "");*/
        bool Continuar = true;
        for (int i = 0; i < CeC_Campos.ds_Campos.EC_CAMPOS.Rows.Count; i++)
        {
            Query = Query.Replace(CeC_Campos.ds_Campos.EC_CAMPOS.Rows[i]["CAMPO_NOMBRE"].ToString(), CeC_Campos.ds_Campos.EC_CAMPOS.Rows[i]["CAMPO_ETIQUETA"].ToString());
        }
        Query = Query.Replace("   ", " ");
        Query = Query.Replace("  ", " ");
        Query = Query.Replace("donde PERSONAS.Inactivo = 0 y   Desde:", "Desde:");
        Query = Query.Replace("donde PERSONAS.Inactivo = 0 y  ", "");
        Query = Query.Replace("PERSONAS.Inactivo = 0 y y", "");
        /*   try
           {
          //     String[] Campos = CeC_Campos.ObtenListaCamposTE().Split(new char[] { ',' });
        //       foreach (string Campo in Campos)
               {
         //          Query = Query.Replace(Campo, CeC_Campos.ObtenEtiqueta(Campo));
               }
           }
           catch
           {

           }*/

        if (Query.Length > 1)
        {

            totalqry = Query;
            return totalqry;
        }
        return "";
    }


    /// <summary>
    /// Envía a otra pagina al cliente
    /// </summary>
    public bool Redirige(string PaginaDestino, bool GeneraRandom)
    {
        if (GeneraRandom)
        {
            if (PaginaDestino.IndexOf('&') > 0 || PaginaDestino.IndexOf('?') > 0)
                PaginaDestino += "&";
            else
                PaginaDestino += "?";
            Random Rnd = new Random();
            PaginaDestino += "R=" + Convert.ToString(Rnd.Next(0, 209));
            CIsLog2.AgregaLog(PaginaDestino);
        }
        return Redirige(PaginaDestino);
    }

    public bool Redirige(string PaginaDestino)
    {
        return Redirige(PaginaDestino, "");
    }
    /// <summary>
    /// Envía a otra pagina al cliente
    /// </summary>
    public bool Redirige(string PaginaDestino, string Parametros)
    {
        try
        {
            if (Parametros.Length > 0)
                PaginaDestino += "?Parametros=" + Parametros;
            m_PaginaWeb.Response.Redirect(PaginaDestino, false);
            //			Random Rnd = new Random();
            //			Pagina.RegisterStartupScript("ST" + Rnd.Next().ToString(),"<script language=JavaScript> top.frames['main'].location='"+ Parametro + "';  </script>");
            return true;
        }
        catch (Exception e)
        {
            CIsLog2.AgregaError(e);
            return true;
        }
    }

    /// <summary>
    /// Envía a otra pagina al cliente
    /// </summary>
    public static bool Redirige(System.Web.UI.Page PaginaOrigen, string PaginaDestino)
    {

        PaginaOrigen.Response.Redirect(PaginaDestino);
        //			Random Rnd = new Random();
        //			Pagina.RegisterStartupScript("ST" + Rnd.Next().ToString(),"<script language=JavaScript> top.frames['main'].location='"+ Parametro + "';  </script>");
        return true;
    }

    public enum LOG_TABLA_TIPO
    {
        CONSULTA,
        NUEVO,
        EDICION,
        BORRADO,
        OTRO
    }

    /// <summary>
    /// Guarda la Pagina Sesion
    /// </summary>
    /// <param name="Pagina"></param>
    /// <param name="VariableSesion"></param>
    /// <param name="Contenido"></param>
    /// <returns></returns>
    public static bool GuardaStringSesion(object Pagina, string VariableSesion, string Contenido)
    {
        try
        {
            return GuardaObjectSesion(Pagina, VariableSesion, Contenido);
        }
        catch (Exception Ex)
        {
            return false;
        }
    }
    /// <summary>
    /// Guarda el Objeto Sesion
    /// </summary>
    /// <param name="Pagina"></param>
    /// <param name="VariableSesion"></param>
    /// <param name="Contenido"></param>
    /// <returns></returns>
    public static bool GuardaObjectSesion(object Pagina, string VariableSesion, object Contenido)
    {
        try
        {
            if (Pagina.GetType().ToString() == "System.Web.HttpContext")
            {
                ((System.Web.HttpContext)Pagina).Session[VariableSesion] = Contenido;
                return true;
            }
            if (Pagina.GetType().BaseType.ToString() == "System.Web.Services.WebService")
                ((System.Web.Services.WebService)Pagina).Session[VariableSesion] = Contenido;
            else
                ((System.Web.UI.Page)Pagina).Session[VariableSesion] = Contenido;
            return true;
        }
        catch (Exception Ex)
        {
            return false;
        }
    }

    /// <summary>
    /// Lee el objeto sesion
    /// </summary>
    /// <param name="Pagina"></param>
    /// <param name="VariableSesion"></param>
    /// <returns></returns>
    public static object LeeObjectSesion(object Pagina, string VariableSesion)
    {
        try
        {
            object Temp;
            if (Pagina.GetType().ToString() == "System.Web.HttpContext")
            {
                Temp = ((System.Web.HttpContext)Pagina).Session[VariableSesion];
                return Temp;
            }
            if (Pagina.GetType().BaseType.ToString() == "System.Web.Services.WebService")
                Temp = ((System.Web.Services.WebService)Pagina).Session[VariableSesion];
            else
                Temp = ((System.Web.UI.Page)Pagina).Session[VariableSesion];
            return Temp;
        }
        catch (Exception Ex)
        {
            //		Redirige(Pagina,"Default.htm");
            //				Pagina.Server.Transfer("Cerrar.htm");
            return "";
        }
    }
    /// <summary>
    /// Intenta obtener la Sesion 
    /// </summary>
    /// <param name="Pagina"></param>
    /// <param name="VariableSesion"></param>
    /// <returns></returns>
    public static string LeeStringSesion(object Pagina, string VariableSesion)
    {
        return LeeStringSesion(Pagina, VariableSesion, "");
    }
    public static string LeeStringSesion(object Pagina, string VariableSesion, string Predeterminado)
    {
        try
        {
            string Valor = (string)LeeObjectSesion(Pagina, VariableSesion);
            if (Valor == null)
                return Predeterminado;
            return (string)Valor;
        }
        catch (Exception Ex)
        {
            return Predeterminado;
        }
    }
    /// <summary>
    /// Obtene una variable string valida para la sesion
    /// </summary>
    /// <param name="VariableSesion">Variable a obtener, se recomienda usar NombreModulo + Variable</param>
    /// <param name="ValorPredeterminado">Valor que se usará de manera predeterminada</param>
    /// <returns>Regresa el valor obtenido o predeterminado</returns>
    public string Lee(string VariableSesion, string ValorPredeterminado)
    {
        return CeC.Convierte2String(LeeObjectSesion(m_ObjetoWeb, VariableSesion), ValorPredeterminado);
    }
    /// <summary>
    /// Obtene una variable int valida para la sesion
    /// </summary>
    /// <param name="VariableSesion">Variable a obtener, se recomienda usar NombreModulo + Variable</param>
    /// <param name="ValorPredeterminado">Valor que se usará de manera predeterminada</param>
    /// <returns>Regresa el valor obtenido o predeterminado</returns>
    public int Lee(string VariableSesion, int ValorPredeterminado)
    {
        return CeC.Convierte2Int(LeeObjectSesion(m_ObjetoWeb, VariableSesion), ValorPredeterminado);
    }
    /// <summary>
    /// Obtene una variable bool valida para la sesion
    /// </summary>
    /// <param name="VariableSesion">Variable a obtener, se recomienda usar NombreModulo + Variable</param>
    /// <param name="ValorPredeterminado">Valor que se usará de manera predeterminada</param>
    /// <returns>Regresa el valor obtenido o predeterminado</returns>
    public bool Lee(string VariableSesion, bool ValorPredeterminado)
    {
        return CeC.Convierte2Bool(LeeObjectSesion(m_ObjetoWeb, VariableSesion), ValorPredeterminado);
    }
    /// <summary>
    /// Obtene una variable datetime valida para la sesion
    /// </summary>
    /// <param name="VariableSesion">Variable a obtener, se recomienda usar NombreModulo + Variable</param>
    /// <param name="ValorPredeterminado">Valor que se usará de manera predeterminada</param>
    /// <returns>Regresa el valor obtenido o predeterminado</returns>
    public DateTime Lee(string VariableSesion, DateTime ValorPredeterminado)
    {
        return CeC.Convierte2DateTime(LeeObjectSesion(m_ObjetoWeb, VariableSesion), ValorPredeterminado);
    }

    /// <summary>
    /// Guarda el valor de una variable de sesión tipo datetime
    /// </summary>
    /// <param name="VariableSesion">Variable a guardar, se recomienda usar NombreModulo + Variable</param>
    /// <param name="Valor">Valor que se usará </param>
    /// <returns>Regresa verdadero si se pudo guardar</returns>
    public bool Guarda(string VariableSesion, DateTime Valor)
    {
        return GuardaObjectSesion(m_ObjetoWeb, VariableSesion, Valor);
    }

    /// <summary>
    /// Guarda el valor de una variable de sesión tipo int
    /// </summary>
    /// <param name="VariableSesion">Variable a guardar, se recomienda usar NombreModulo + Variable</param>
    /// <param name="Valor">Valor que se usará </param>
    /// <returns>Regresa verdadero si se pudo guardar</returns>
    public bool Guarda(string VariableSesion, int Valor)
    {
        return GuardaObjectSesion(m_ObjetoWeb, VariableSesion, Valor);
    }

    /// <summary>
    /// Guarda el valor de una variable de sesión tipo bool
    /// </summary>
    /// <param name="VariableSesion">Variable a guardar, se recomienda usar NombreModulo + Variable</param>
    /// <param name="Valor">Valor que se usará </param>
    /// <returns>Regresa verdadero si se pudo guardar</returns>
    public bool Guarda(string VariableSesion, bool Valor)
    {
        return GuardaObjectSesion(m_ObjetoWeb, VariableSesion, Valor);
    }

    /// <summary>
    /// Guarda el valor de una variable de sesión tipo string
    /// </summary>
    /// <param name="VariableSesion">Variable a guardar, se recomienda usar NombreModulo + Variable</param>
    /// <param name="Valor">Valor que se usará </param>
    /// <returns>Regresa verdadero si se pudo guardar</returns>
    public bool Guarda(string VariableSesion, string Valor)
    {
        return GuardaObjectSesion(m_ObjetoWeb, VariableSesion, Valor);
    }

    /// <summary>
    /// Lee el instante de tiempo de la sesion
    /// </summary>
    /// <param name="Pagina"></param>
    /// <param name="VariableSesion"></param>
    /// <returns></returns>
    public static DateTime LeeDateTimeSesion(object Pagina, string VariableSesion, DateTime FechaPredeterminada)
    {
        try
        {
            DateTime Temp = Convert.ToDateTime(LeeObjectSesion(Pagina, VariableSesion));
            if (Temp.Year <= 1)
                return FechaPredeterminada;
            return Temp;
        }
        catch (Exception Ex)
        {
            return FechaPredeterminada;
        }
    }

    public static DateTime LeeDateTimeSesion(object Pagina, string VariableSesion)
    {
        return LeeDateTimeSesion(Pagina, VariableSesion, CeC_BD.FechaNula);
    }
    /// <summary>
    /// Guarda el instante de tiempo de la sesion
    /// </summary>
    /// <param name="Pagina"></param>
    /// <param name="VariableSesion"></param>
    /// <param name="Contenido"></param>
    /// <returns></returns>
    public static bool GuardaDateTimeSesion(object Pagina, string VariableSesion, DateTime Contenido)
    {
        try
        {
            return GuardaObjectSesion(Pagina, VariableSesion, Contenido);
        }
        catch (Exception Ex)
        {
            return false;
        }
    }
    /// <summary>
    /// Intenta guardar la sesion
    /// </summary>
    /// <param name="Pagina"></param>
    /// <param name="VariableSesion"></param>
    /// <param name="Contenido"></param>
    /// <returns></returns>
    public static bool GuardaIntSesion(object Pagina, string VariableSesion, int Contenido)
    {
        try
        {

            return GuardaObjectSesion(Pagina, VariableSesion, Contenido);
        }
        catch (Exception Ex)
        {
            //	Redirige(Pagina,"Default.htm");
            return false;
        }
    }

    /// <summary>
    /// Regresa la sesion 
    /// </summary>
    /// <param name="Pagina"></param>
    /// <param name="VariableSesion"></param>
    /// <param name="Predeterminado"></param>
    /// <returns></returns>
    public static int LeeIntSesion(object Pagina, string VariableSesion, int Predeterminado)
    {
        try
        {
            return (int)LeeObjectSesion(Pagina, VariableSesion);
        }
        catch (Exception Ex)
        {
            return Predeterminado;
        }
    }
    public static bool GuardaBoolSesion(object Pagina, string VariableSesion, bool Valor)
    {
        return GuardaObjectSesion(Pagina, VariableSesion, Valor);
    }

    public static bool LeeBoolSesion(object Pagina, string VariableSesion, bool Predeterminado)
    {
        try
        {
            return (bool)LeeObjectSesion(Pagina, VariableSesion);
        }
        catch { }
        return Predeterminado;
    }

    /// <summary>
    /// Regresa el valor de de la webConfig
    /// </summary>
    /// <param name="Variable"></param>
    /// <returns></returns>
    public static string ObtenerValorWebconfig(string Variable)
    {
        System.Configuration.AppSettingsReader Configuracion = new System.Configuration.AppSettingsReader();
        string Cadena = ((string)(Configuracion.GetValue(Variable, typeof(string))));
        return Cadena;
    }
    /// <summary>
    /// Regresa el valor de la webConfig
    /// </summary>
    /// <param name="Variable"></param>
    /// <returns></returns>
    public string ObtenerValorWebconfigP(string Variable)
    {
        System.Configuration.AppSettingsReader Configuracion = new System.Configuration.AppSettingsReader();
        string Cadena = ((string)(Configuracion.GetValue(Variable, typeof(string))));
        return Cadena;
    }
    /// <summary>
    /// Regresa el tipo de Permiso del usuario
    /// </summary>
    /// <param name="Perfil_ID"></param>
    /// <param name="Permiso"></param>
    /// <returns></returns>
    public string Tipo_Usuario_Pemisos(int Perfil_ID, string Permiso)
    {
        string Perfiltipo = CeC_BD.EjecutaEscalarString("Select Perfil_nombre from EC_PERFILES where perfil_id = " + Perfil_ID);

        if (Perfiltipo == "Administrador")
            return "S";
        else
            return Permiso;
    }
    public string UltimoReporteQryHash
    {
        get { return LeeStringSesion(m_ObjetoWeb, "UltimoReporteQryHash", ""); }
        set { GuardaStringSesion(m_ObjetoWeb, "UltimoReporteQryHash", value); }
    }
    public DateTime UltimoReporteQryFechaHora
    {
        get { return LeeDateTimeSesion(m_ObjetoWeb, "UltimoReporteQryFechaHora", CeC_BD.FechaNula); }
        set { GuardaDateTimeSesion(m_ObjetoWeb, "UltimoReporteQryFechaHora", value); }
    }
    /// <summary>
    /// Regresa la ruta de los Directorios PDF
    /// </summary>
    /// <returns></returns>
    public string ObtenerSoloDirectorioPDF()
    {
        System.Configuration.AppSettingsReader Configuracion = new System.Configuration.AppSettingsReader();
        string Cadena = CeC_Config.RutaReportesPDF;

        return Cadena;
    }
    /// <summary>
    /// Verifica que exista un permiso para accesar
    /// </summary>
    /// <param name="PermisoparaAccessar"></param>
    /// <param name="TodosPermisosUsu"></param>
    /// <returns></returns>
    public bool ExistePermiso(string PermisoparaAccessar, string TodosPermisosUsu)
    {

        char[] Caracter = new char[1];
        Caracter[0] = Convert.ToChar("|");
        char[] Caracter1 = new char[1];
        Caracter1[0] = Convert.ToChar(".");
        string[] PermisoTempArray = TodosPermisosUsu.Split(Caracter);
        string[] Permiso_A = PermisoparaAccessar.Split(Caracter1);
        string checarPermiso = "";

        /*for (int i = 0 ; i<PermisoTempArray.Length;i++)
        {
            checarPermiso = "";
	
            for (int j = 0 ;j<Permiso_A.Length;j++)
            {
                checarPermiso = ArrayUnir(Permiso_A,j);
				
                if (checarPermiso==PermisoTempArray[i])
                {
                    return true;
                }
            }


        }*/

        for (int i = 0; i < PermisoTempArray.Length; i++)
        {

            if (PermisoparaAccessar == PermisoTempArray[i])
            {
                return true;
            }
        }


        return false;
    }

#if !eClockSync
    /// <summary>
    /// Obtiene o establece la sesion de Restriccion
    /// </summary>
    public string Restriccion
    {
        get { return LeeStringSesion(m_ObjetoWeb, "Restriccion"); }
        set { GuardaStringSesion(m_ObjetoWeb, "Restriccion", value); }
    }
    /// <summary>
    /// Regresa un mensaje en la ventana JScript
    /// </summary>
    /// <returns></returns>
    public string MensajeVentanaJScript()
    {
        return CeC_Config.MensajeJScript;
    }

    /// <summary>
    /// Cierra la sesion 
    /// </summary>
    /// <returns></returns>
    public bool CierraSesion()
    {
        bool Res = CierraSesion(SESION_ID);
        GuardaIntSesion(m_ObjetoWeb, "SESION_ID", -1);
        m_PaginaWeb.Session.Abandon();
        return Res;
    }

#endif
    /// <summary>
    /// Cierra la sesion
    /// </summary>
    /// <param name="Sesion"></param>
    /// <returns></returns>
    public static bool CierraSesion(System.Web.SessionState.HttpSessionState Sesion)
    {
        try
        {
            int SesionID = LeeIntSesion(Sesion, "SESION_ID", -1);
            return CierraSesion(SesionID);
        }
        catch
        { }
        return false;
    }
    public static bool CierraSesion(string Sesion_Seguridad)
    {
        if (ValidaSeguridad(Sesion_Seguridad))
            return CierraSesion(Sesion_Seguridad2SesionID(Sesion_Seguridad));
        return false;

    }
    /// <summary>
    /// Cierra la sesion y actualiza la fecha de finalizacion de la sesion
    /// </summary>
    /// <param name="SesionID"></param>
    /// <returns></returns>
    public static bool CierraSesion(int SesionID)
    {
        if (SesionID <= 0)
            return false;
        CeC_BD.EjecutaComando("UPDATE EC_SESIONES SET SESION_FIN_FECHAHORA = " + CeC_BD.SqlFechaHora(DateTime.Now) + " WHERE SESION_ID = " + SesionID);
        SAgregaLog("Cerrando sesion, SesionID = " + SesionID);
        return true;
    }
    /// <summary>
    /// Verifica si la sesión se encuentra activa
    /// </summary>
    /// <param name="SesionID"></param>
    /// <returns></returns>
    public static bool SesionActiva(int SesionID)
    {
        int NSesionID = CeC_BD.EjecutaEscalarInt("SELECT SESION_ID FROM EC_SESIONES WHERE SESION_ID = " + SesionID.ToString() + " AND SESION_FIN_FECHAHORA IS NULL");
        if (NSesionID == SesionID)
            return true;
        return false;
    }

    /// <summary>
    /// Valida el usuario a travez de su nombre de usuario y password
    /// </summary>
    /// <param name="Usuario">Nombre de usuario</param>
    /// <param name="Password">Contraseña de usuario</param>
    /// <returns>identificador de usuario si se encuentra el usuario de lo contrario regresa -9999</returns>
    public static int ValidarUsuarioCrypt(string Usuario, string PasswordEncriptado)
    {
        string UsuarioR = Usuario;
        //if (Usuario.Length > 20)
        //  UsuarioR = Usuario.Substring(0, 20);
        string Clave = CeC_BD.EjecutaEscalarString("SELECT USUARIO_CLAVE FROM EC_USUARIOS WHERE USUARIO_USUARIO = '" + UsuarioR + "'");
        if (Clave.Length < 1)
            return -9998;
        string ClaveOriginal = PasswordEncriptado;
        string ClaveHash = CeC_BD.CalculaHash(Clave);

        ClaveHash = ClaveHash.Replace("-", "").ToUpper();
        PasswordEncriptado = PasswordEncriptado.Replace("-", "").ToUpper();

        if (ClaveHash != PasswordEncriptado && Clave != ClaveOriginal)
            return -9997;

        return CeC_BD.EjecutaEscalarInt("SELECT USUARIO_ID FROM EC_USUARIOS WHERE USUARIO_USUARIO = '" + UsuarioR + "'");
    }

    /// <summary>
    /// Valida el usuario a travez de su nombre de usuario y password
    /// </summary>
    /// <param name="Usuario">Nombre de usuario</param>
    /// <param name="Password">Contraseña de usuario</param>
    /// <returns>identificador de usuario si se encuentra el usuario de lo contrario regresa -9999</returns>
    public static int ValidarUsuario(string Usuario, string Password, string Suscripcion = "")
    {
        try
        {
            CeC_Sesion Sesion = new CeC_Sesion();

            string UsuarioR = Usuario;
            // if (Usuario.Length > 20)
            //     UsuarioR = Usuario.Substring(0, 20);
            int UsuarioId = ValidarUsuarioeC(UsuarioR, Password);
            if (UsuarioId < 0 && Suscripcion != "")
                UsuarioId = ValidarUsuarioeC(Suscripcion + "_" + UsuarioR, Password);
            if (UsuarioId < 0)
                UsuarioId = ValidarUsuarioeMail(UsuarioR, Password);
            if (UsuarioId < 0)
            {
                if (CeC_Config.EsDemo)
                {
                    //UsuarioId = ValidarUsuarioDNN(Usuario, Password);
                    return UsuarioId;
                }
                CeC_Sesion.SAgregaLog("ATENCION: Validacion de usuario INCORRECTA " + Usuario);
            }
            CeC_Sesion.SAgregaLog("Validacion de usuario correcta " + Usuario);
            return UsuarioId;
        }
        catch (Exception ex)
        {
            return -9999;
        }
    }

    /// <summary>
    /// Valida el usuario si se esta trabajando con eClock Web
    /// </summary>
    /// <param name="Usuario">Nombre de usuario</param>
    /// <param name="Password">Contraseña de usuario</param>
    /// <returns>Indentificador de usuario si es que se encuentra de lo contrario regresara -9999</returns>
    public static int ValidarUsuarioeC(string Usuario, string Password)
    {
        return CeC_BD.EjecutaEscalarInt("SELECT USUARIO_ID FROM EC_USUARIOS where USUARIO_USUARIO = '" + CeC_BD.ObtenParametroCadena(Usuario) + "' AND USUARIO_CLAVE = '" + CeC_BD.ObtenParametroCadena(Password) + "' AND USUARIO_BORRADO = 0");
    }

    public static int ValidarUsuarioeMail(string UsuarioeMail, string Password)
    {
        return CeC_BD.EjecutaEscalarInt("SELECT USUARIO_ID FROM EC_USUARIOS where USUARIO_EMAIL = '" + CeC_BD.ObtenParametroCadena(UsuarioeMail) + "' AND USUARIO_CLAVE = '" + CeC_BD.ObtenParametroCadena(Password) + "' AND USUARIO_BORRADO = 0");
    }
    /// <summary>
    /// Guarda la localización Actual Código ISO 639-1 del lenguaje a mostrar
    /// </summary>
    public string Localizacion
    {
        get
        {
            string Local = LeeStringSesion(m_ObjetoWeb, "Localizacion", "-");
            if (Local == "-")
                Localizacion = Local = this.Configura.Localizacion;
            return Local;
        }
        set { GuardaStringSesion(m_ObjetoWeb, "Localizacion", value); }
    }

    public int SuscripcionParametro
    {
        get
        {

            try
            {
                if (SUSCRIPCION_ID <= 1)
                {
                    string Valor = m_PaginaWeb.Request.Params["SuscripcionID"].ToString();
                    return Convert.ToInt32(Valor);
                }
            }
            catch
            {
            }
            return SUSCRIPCION_ID;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool EsNavegadorMovil
    {
        get
        {
            //GETS THE CURRENT USER CONTEXT
            HttpRequest Request = m_PaginaWeb.Request;

            //FIRST TRY BUILT IN ASP.NT CHECK
            if (Request.Browser.IsMobileDevice)
            {
                return true;
            }
            //THEN TRY CHECKING FOR THE HTTP_X_WAP_PROFILE HEADER
            if (Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
            {
                return true;
            }
            //THEN TRY CHECKING THAT HTTP_ACCEPT EXISTS AND CONTAINS WAP
            if (Request.ServerVariables["HTTP_ACCEPT"] != null &&
                Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
            {
                return true;
            }
            //AND FINALLY CHECK THE HTTP_USER_AGENT
            //HEADER VARIABLE FOR ANY ONE OF THE FOLLOWING
            if (Request.ServerVariables["HTTP_USER_AGENT"] != null)
            {
                //Create a list of all mobile types
                string[] mobiles =
                    new string[]
              {
                  "midp", "j2me", "avant", "docomo",
                  "novarra", "palmos", "palmsource",
                  "240x320", "opwv", "chtml",
                  "pda", "windows ce", "mmp/",
                  "blackberry", "mib/", "symbian",
                  "wireless", "nokia", "hand", "mobi",
                  "phone", "cdm", "up.b", "audio",
                  "SIE-", "SEC-", "samsung", "HTC",
                  "mot-", "mitsu", "sagem", "sony"
                  , "alcatel", "lg", "eric", "vx",
                  "NEC", "philips", "mmm", "xx",
                  "panasonic", "sharp", "wap", "sch",
                  "rover", "pocket", "benq", "java",
                  "pt", "pg", "vox", "amoi",
                  "bird", "compal", "kg", "voda",
                  "sany", "kdd", "dbt", "sendo",
                  "sgh", "gradi", "jb", "dddi",
                  "moto", "iphone"
              };

                //Loop through each item in the list created above
                //and check if the header contains that text
                foreach (string s in mobiles)
                {
                    if (Request.ServerVariables["HTTP_USER_AGENT"].
                                                        ToLower().Contains(s.ToLower()))
                    {
                        return true;
                    }
                }


                return false;
            }
            return false;
        }
    }

    public DateTime ObtenUltimaSesion()
    {
        string Qry = "SELECT SESION_INICIO_FECHAHORA, SESION_ID " +
            "FROM EC_SESIONES " +
            "WHERE (USUARIO_ID = " + this.USUARIO_ID + ") AND (SESION_ID < " + this.SESION_ID + ") " +
            "ORDER BY SESION_INICIO_FECHAHORA DESC ";
        return CeC_BD.EjecutaEscalarDateTime(Qry, new DateTime());
    }
}