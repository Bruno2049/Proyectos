using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.ComponentModel;

using System.IO;
using System.Web.Mail;

//using System.Drawing;
/// <summary>
/// Esta clase esta diseñada para contener todas las variables globales por usuario del eClock Web
/// </summary>
public class CeC_Sesion
{
    //Arreglo para localizar los dias que fueron modificados

    public int[] DiArreglo = new int[7];

    static long ReporteAut = 0;

    private int m_USUARIO_ID = -1;
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
    public CeC_ConfigSuscripcion ConfiguraSuscripcion;



    /// <summary>
    /// Obtiene o establece el Nombre de la Pagina Actual
    /// </summary>
    public string Pagina_Actual
    {
        get { return LeeStringSesion(m_ObjetoWeb, "Pagina_Actual"); }
        set
        {
            string Pagina = value;
            if (Pagina.Substring(0, 4) == "ASP.")
            {
                Pagina = Pagina.Substring(4, Pagina.Length - 9) + ".aspx";
            }
            Pagina_Anterior = Pagina_Actual;
            if (Pagina == Pagina_Actual)
                return;

            GuardaStringSesion(m_ObjetoWeb, "Pagina_Actual", Pagina);
        }
    }
    public System.Web.UI.Page Pagina_Actual_Page
    {
        get { return (System.Web.UI.Page)LeeObjectSesion(m_ObjetoWeb, "Pagina_Actual_Page"); }
        set { GuardaObjectSesion(m_ObjetoWeb, "Pagina_Actual_Page", value); }
    }

    public System.Web.UI.Page m_PaginaWeb
    {
        get { return (System.Web.UI.Page)m_ObjetoWeb; }
        set { m_ObjetoWeb = value; }
    }

    /// <summary>
    /// Obtiene verdadero en el caso de que la pagina sea solo reporte
    /// </summary>
    public int EsSoloReporte
    {
        get
        {
            try
            {
                string Valor = m_PaginaWeb.Request.Params["Reporte"].ToString();
                int Valo = Convert.ToInt32(Valor);
                if (Convert.ToInt32(Valor) < 0)
                    return 0;
                else
                    return Convert.ToInt32(Valor);
            }
            catch (Exception ex)
            {
                string f = ex.Message;
            }
            return 0;
        }
    }
    /// <summary>
    /// Obtiene el valor de los parametros
    /// </summary>
    /// <param name="NombreParametro"></param>
    /// <returns></returns>
    public int obtenerValor_Prametros(string NombreParametro)
    {
        try
        {
            string Valor = m_PaginaWeb.Request.Params[NombreParametro].ToString();
            int Valo = Convert.ToInt32(Valor);
            if (Convert.ToInt32(Valor) < 0)
                return 0;
            else
                return Convert.ToInt32(Valor);
        }
        catch (Exception ex)
        {
            string f = ex.Message;
        }
        return 0;
    }
    /// <summary>
    /// Obtiene la URL destino
    /// </summary>
    public string UrlDestino
    {
        get
        {
            try
            {
                string Valor = m_PaginaWeb.Request.Params["UrlDestino"].ToString();
                return Valor;
            }
            catch
            {
            }
            return "";
        }
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


    /// <summary>
    /// Ejecuta un script
    /// </summary>	
    public void EjecutaScript(string Script)
    {
        EjecutaScript(m_PaginaWeb, Script);
        return;
    }
    public static void EjecutaScript(System.Web.UI.Page Pagina, string Script)
    {
        Random rdm1 = new Random(unchecked(1000000));
        Pagina.RegisterClientScriptBlock("MensageScript" + rdm1.Next().ToString(), "<script language=JavaScript> " + RevisaCadena(Script) + "  </script>");
        return;
    }

    /// <summary>
    /// Obtiene o establece el Nombre de la Pagina Anterior
    /// </summary>
    public string Pagina_Anterior
    {
        get { return LeeStringSesion(m_ObjetoWeb, "Pagina_Anterior"); }
        set { GuardaStringSesion(m_ObjetoWeb, "Pagina_Anterior", value); }
    }
    /// <summary>
    /// Obtiene o establece el mensaje de un evento realizado correcto
    /// </summary>
    public string MensajeCorrecto
    {
        get { return LeeStringSesion(m_ObjetoWeb, "MensajeCorrecto"); }
        set { GuardaStringSesion(m_ObjetoWeb, "MensajeCorrecto", value); }
    }
    /// <summary>
    /// Obtiene o establece el mensaje de error de un evento realizado 
    /// </summary>
    public string MensajeError
    {
        get { return LeeStringSesion(m_ObjetoWeb, "MensajeError"); }
        set { GuardaStringSesion(m_ObjetoWeb, "MensajeError", value); }
    }
    /// <summary>
    /// Obtiene la Sesion de la Pagina Anterior
    /// </summary>
    public string Array_Sesion
    {
        get { return LeeStringSesion(m_ObjetoWeb, "Pagina_Anterior"); }
        set { GuardaStringSesion(m_ObjetoWeb, "Pagina_Anterior", value); }
    }

    /// <summary>
    /// Obtiene o establece el Nombre del archivo del reporte PDF
    /// </summary>
    public string ArchivoReporte
    {
        get { return LeeStringSesion(m_ObjetoWeb, "ArchivoReporte"); }
        set { GuardaStringSesion(m_ObjetoWeb, "ArchivoReporte", value); }
    }
    /// <summary>
    /// Obtiene o establece la sesion 
    /// </summary>
    public int EsWizard
    {
        get { return LeeIntSesion(m_ObjetoWeb, "EsWizard", 0); }
        set { GuardaIntSesion(m_ObjetoWeb, "ESWizard", value); }
    }
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
            if (USUARIO_ID != value)
                IniciaSesion(value);
        }

    }
    public int USUARIO_ID_SUSCRIPCION
    {
        get
        {
            return LeeIntSesion(m_ObjetoWeb, "USUARIO_ID_SUSCRIPCION", USUARIO_ID);
        }
        set
        {
            GuardaIntSesion(m_ObjetoWeb, "USUARIO_ID_SUSCRIPCION", value);
        }

    }
    /// <summary>
    /// Obtiene el Usuario (login)
    /// </summary>
    public string USUARIO_USUARIO
    {
        get { return LeeStringSesion(m_ObjetoWeb, "USUARIO_USUARIO"); }
    }

    /// <summary>
    /// Obtiene el Nombre del Usuario
    /// </summary>
    public string USUARIO_NOMBRE
    {
        get { return LeeStringSesion(m_ObjetoWeb, "USUARIO_NOMBRE"); }
    }
    /// <summary>
    /// Obtiene el identificador de sesion Sesion_ID
    /// </summary>
    public int SESION_ID
    {
        get { return LeeIntSesion(m_ObjetoWeb, "SESION_ID", 0); }
    }

    /// <summary>
    /// Obtiene el identificador a la suscripcion predeterminada que tiene asignado el usuario
    /// </summary>
    /// <remarks></remarks>
    public int SUSCRIPCION_ID
    {
        get { return LeeIntSesion(m_ObjetoWeb, "SUSCRIPCION_ID", 0); }
    }

    /// <summary>
    /// Obtiene el identificador al perfil que tiene asignado el usuario
    /// </summary>
    /// <remarks></remarks>
    public int PERFIL_ID
    {
        get { return LeeIntSesion(m_ObjetoWeb, "PERFIL_ID", 0); }
    }

    /// <summary>
    /// Obtiene el nombre del perfil que tiene asignado el usuario
    /// </summary>
    /// <remarks></remarks>
    public string PERFIL_NOMBRE
    {
        get { return LeeStringSesion(m_ObjetoWeb, "PERFIL_NOMBRE"); }
    }


    /// <summary>
    /// Contiene el objeto Pagina pasado como argumento en el método Nuevo
    /// </summary>
    protected object m_ObjetoWeb;

    public static int CreaSesion(int UsuarioID, int Terminal_ID)
    {
        int SesionID = CeC_Autonumerico.GeneraAutonumerico("EC_SESIONES", "SESION_ID");
        string qry = "INSERT INTO EC_SESIONES (SESION_ID, USUARIO_ID, SESION_INICIO_FECHAHORA, SESION_TERMINAL_ID) VALUES( " + SesionID.ToString() + "," + UsuarioID.ToString() + "," + CeC_BD.SqlFechaHora(DateTime.Now) + ", " + Terminal_ID + " )";
        int ret = CeC_BD.EjecutaComando(qry);
        return SesionID;
    }

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
        int SesionID = CeC_Autonumerico.GeneraAutonumerico("EC_SESIONES", "SESION_ID");
        string Qry = "INSERT INTO EC_SESIONES (SESION_ID, USUARIO_ID, SESION_INICIO_FECHAHORA) VALUES( " +
            SesionID.ToString() + "," + UsuarioID.ToString() + "," + CeC_BD.QueryFechaHora + " )";

        if (CeC_BD.EjecutaComando(Qry) <= 0)
        {

            return 0;
        }
        return SesionID;
    }
    // Operations
    /// <summary>
    /// Inicia sesión y crea un registro en la tabla de sesiones
    /// </summary>
    public bool IniciaSesion(int Usuario_ID)
    {
        OleDbConnection Conexion;
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
            AgregaLogModulo(LOG_TABLA_TIPO.NUEVO, "Sesion", SesionID, " UsuarioID = " + Usuario_ID);
            Qry = "SELECT Usuario_Usuario, Usuario_Nombre, EC_PERFILES.Perfil_ID, EC_PERFILES.PERFIL_NOMBRE , SUSCRIPCION_ID " +
                "FROM EC_USUARIOS, EC_PERFILES WHERE EC_PERFILES.PERFIL_ID = EC_USUARIOS.PERFIL_ID AND " +
                " USUARIO_ID =" + Usuario_ID.ToString();

            object Obj = CeC_BD.EjecutaReader(Qry, out Conexion);
            if (Obj == null)
            {
                if (Conexion != null)
                    Conexion.Dispose();
                return false;
            }
            OleDbDataReader DR = (OleDbDataReader)Obj;
            if (!DR.Read())
            {
                DR.Close();
                Conexion.Dispose();
                return false;
            }
            Configura.USUARIO_ID = Usuario_ID;
            GuardaIntSesion(m_ObjetoWeb, "USUARIO_ID", Usuario_ID);

            GuardaStringSesion(m_ObjetoWeb, "USUARIO_USUARIO", DR.GetValue(0).ToString());
            GuardaStringSesion(m_ObjetoWeb, "USUARIO_NOMBRE", DR.GetValue(1).ToString());
            GuardaIntSesion(m_ObjetoWeb, "PERFIL_ID", Convert.ToInt32(DR.GetValue(2)));
            GuardaStringSesion(m_ObjetoWeb, "PERFIL_NOMBRE", DR.GetValue(3).ToString());
            int SUSCRIPCION_ID = Convert.ToInt32(DR.GetValue(4).ToString());
            if (SUSCRIPCION_ID == 0)
            {
                SUSCRIPCION_ID = GeneraSuscripcion(USUARIO_USUARIO);
                CeC_BD.EjecutaComando("UPDATE EC_USUARIOS SET SUSCRIPCION_ID = " + SUSCRIPCION_ID + "  WHERE USUARIO_ID = " + Usuario_ID);
            }
            GuardaIntSesion(m_ObjetoWeb, "SUSCRIPCION_ID", SUSCRIPCION_ID);
            USUARIO_ID_SUSCRIPCION = CeC_Suscripcion.ObtenUsuarioID(SUSCRIPCION_ID);
            ConfiguraSuscripcion = new CeC_ConfigSuscripcion(SUSCRIPCION_ID);

            DR.Close();
            Conexion.Dispose();
            GuardaIntSesion(m_ObjetoWeb, "SESION_ID", SesionID);
            PermisosEfectivos = "";
            int PersonaID = CeC_Usuarios.ObtenPersonaID(Usuario_ID);
            if (PersonaID > 0)
                eClock_Persona_ID = PersonaID;
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
    /// Asignar este valor despues de llavar a Sesion.Nuevo
    /// </summary>
    public string LinkLogIn
    {
        get
        {
            string Val = LeeStringSesion(m_ObjetoWeb, "LinkLogIn");

            if (Val == "")
                if (PERFIL_ID == 7)
                    Val = "eClockEmpleado.aspx";
                else
                    Val = "eClock.aspx";
            //else
            //LinkLogIn = "";
            return Val;
        }
        set { GuardaStringSesion(m_ObjetoWeb, "LinkLogIn", value); }
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
            Sesion.m_ObjetoWeb = (System.Web.UI.Page)Pagina;


            Sesion.Pagina_Actual = Sesion.m_ObjetoWeb.ToString();
            if (Sesion.Pagina_Actual != Sesion.Pagina_Anterior && Sesion.Pagina_Actual != "eclockact.aspx")
            {
                Sesion.AgregaLogModulo(LOG_TABLA_TIPO.CONSULTA, Sesion.Pagina_Actual, 0, "Consultando pagina...");
            }
            Sesion.Pagina_Actual_Page = (System.Web.UI.Page)Pagina;
            if (Usuario_ID <= 0)
                if (Sesion.SESION_ID <= 0)
                {
                    Sesion.Redirige("WF_LogIn.aspx");
                }
                else
                {
                    Sesion.Configura = new CeC_Config(Sesion.USUARIO_ID);
                    //Ahorra sentencias SQL
                    Sesion.ConfiguraSuscripcion = CeC_ConfigSuscripcion.Nuevo(Sesion.USUARIO_ID_SUSCRIPCION);
                    Sesion.LinkLogIn = "";
                }
            //Sesion.IniciaSesion(Usuario_ID);
            if (Pagina.GetType().BaseType.ToString() != "System.Web.Services.WebService")
            {
                if (!Sesion.Pagina_Actual_Page.IsPostBack)
                    CeC_Localizaciones.AplicaLocalizacion(Sesion);
                Sesion.InicializaScripts();
            }
            return Sesion;
        }
        catch (System.Exception e)
        {

        }
        return Sesion;
    }
    public void InicializaScripts()
    {
        EjecutaScript("function BotonIS_MouseDown(oButton, oEvent){oButton.getElementAt(0).click();oButton.setEnabled(false);}");
    }

    public bool ControlaBoton(ref Infragistics.WebUI.WebDataInput.WebImageButton Bonton)
    {
        if (Bonton.ClientSideEvents.MouseDown == null || Bonton.ClientSideEvents.MouseDown.Length < 1)
        {
            Bonton.ClientSideEvents.MouseDown = "BotonIS_MouseDown";
            return true;
        }
        return false;
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
        string Cadena = CeC_Config.RutaReportesPDF + this.SESION_ID.ToString() + "_" + (CeC_Sesion.ReporteAut).ToString() + "_" + Reporte.Trim() + ".pdf";
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
        return Cadena + NormalizaCadena(this.SESION_ID.ToString() + "_" + (CeC_Sesion.ReporteAut).ToString() + "_" + Reporte.Trim() + ".pdf");


    }

    public static string ObtenRutaArchivoTemp(string Nombre)
    {
        string Cadena = HttpRuntime.AppDomainAppPath + CeC_Config.RutaReportesPDF + (CeC_Sesion.ReporteAut).ToString() + "_" + Nombre.Trim();
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

    public static bool sRedirige(Page PaginaActual, string PaginaDestino)
    {
        try
        {
            PaginaActual.Response.Redirect(PaginaDestino, false);
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
    /// Redirecciona a la página destino
    /// </summary>
    /// <param name="PaginaDestino"></param>
    /// <param name="PersonaLinkID"></param>
    /// <returns></returns>
    public bool Redirige(string PaginaDestino, int PersonaLinkID)
    {
        try
        {
            if (PaginaDestino == "WF_Personas_Diario.aspx" || PaginaDestino == "WF_HorarioPersona.aspx")
                this.WF_EmpleadosBus_Query = "Para que pase la consulta";
            this.WF_Empleados_PERSONA_LINK_ID = PersonaLinkID;
            this.WF_Empleados_PERSONA_ID = CeC_BD.EjecutaEscalarInt("SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_LINK_ID = " + PersonaLinkID);
            this.WF_Empleados_PERSONA_NOMBRE = CeC_BD.EjecutaEscalarString("SELECT PERSONA_NOMBRE FROM EC_PERSONAS WHERE PERSONA_LINK_ID = " + PersonaLinkID);
            m_PaginaWeb.Response.Redirect(PaginaDestino, false);
            return true;

        }
        catch (Exception ex)
        {
            m_PaginaWeb.Response.Redirect("WF_EmpleadosBus2.aspx");
            return false;
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

    public bool Redirige_WF_TerminalesPersonas(int Terminal_ID)
    {
        WF_Terminales_TERMINALES_ID = WF_TerminalesE_TerminalID = Terminal_ID;
        return Redirige("WF_TerminalesPersonas.aspx",Terminal_ID.ToString());
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
    /// Intenta leer la sesion 
    /// </summary>
    /// <param name="Sesion"></param>
    /// <param name="VariableSesion"></param>
    /// <param name="Predeterminado"></param>
    /// <returns></returns>
    public static int LeeIntSesion(System.Web.SessionState.HttpSessionState Sesion, string VariableSesion, int Predeterminado)
    {
        try
        {
            return (int)Sesion[VariableSesion];
        }
        catch (Exception Ex)
        {
            return Predeterminado;
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
    /// Obtiene o establece el Usuario Seleccionado en el
    /// </summary>
    public int WF_UsuariosE_UsuarioId
    {
        get
        {
            return LeeIntSesion(m_ObjetoWeb, "WF_UsuariosE_UsuarioId", -1);
        }
        set
        {
            GuardaIntSesion(m_ObjetoWeb, "WF_UsuariosE_UsuarioId", value);
        }
    }



    /// <summary>
    /// Contiene el parámetro del Usuario seleccionado 
    /// </summary>



    public int WF_Empleados_PERSONA_LINK_ID
    {
        get { return LeeIntSesion(m_ObjetoWeb, "WF_Empleados_PERSONA_LINK_ID", 0); }
        set { GuardaIntSesion(m_ObjetoWeb, "WF_Empleados_PERSONA_LINK_ID", value); }
    }
    /// <summary>
    /// Obtiene el parametro Persona_ID
    /// </summary>
    public int WF_Empleados_PERSONA_ID
    {
        get { return LeeIntSesion(m_ObjetoWeb, "WF_Empleados_PERSONA_ID", 0); }
        set { GuardaIntSesion(m_ObjetoWeb, "WF_Empleados_PERSONA_ID", value); }
    }
    /// <summary>
    /// OBTIENE EL VALOR DE PERSONA_NOMRE
    /// </summary>
    public string WF_Empleados_PERSONA_NOMBRE
    {
        get { return CeC_BD.EjecutaEscalarString(" SELECT PERSONA_NOMBRE FROM EC_PERSONAS WHERE PERSONA_ID = " + WF_Empleados_PERSONA_ID.ToString()); }
        set { GuardaStringSesion(m_ObjetoWeb, "WF_Empleados_PERSONA_NOMBRE", value); }

    }
    /// <summary>
    /// OBTIENE EL VALOR DE USUARIO_ID
    /// </summary>
    public int WF_Usuarios_USUARIO_ID
    {
        get { return LeeIntSesion(m_ObjetoWeb, "WF_Usuarios_USUARIO_ID", 0); }
        set { GuardaIntSesion(m_ObjetoWeb, "WF_Usuarios_USUARIO_ID", value); }
    }
    /// <summary>
    /// OBTIENE EL VALOR DE TERMINALES_ID
    /// </summary>
    public int WF_Terminales_TERMINALES_ID
    {
        get { return LeeIntSesion(m_ObjetoWeb, "WF_Terminales_TERMINALES_ID", 0); }
        set { GuardaIntSesion(m_ObjetoWeb, "WF_Terminales_TERMINALES_ID", value); }
    }

    /// <summary>
    /// OBTIENE EL VALOR DE ENV_REPORTE_ID
    /// </summary>
    public int WF_ENV_REPORTE_ID
    {
        get { return LeeIntSesion(m_ObjetoWeb, "WF_ENV_REPORTE_ID", 0); }
        set { GuardaIntSesion(m_ObjetoWeb, "WF_ENV_REPORTE_ID", value); }
    }

    /// <summary>
    /// OBTIENE EL VALOR DE USUARIO_ID DEL ENVIO DE REPORTES
    /// </summary>
    public int WF_ENV_REPORTE_USUARIO_ID
    {
        get { return LeeIntSesion(m_ObjetoWeb, "WF_ENV_REPORTE_USUARIO_ID", 0); }
        set { GuardaIntSesion(m_ObjetoWeb, "WF_ENV_REPORTE_USUARIO_ID", value); }
    }

    /// <summary>
    /// OBTIENE EL VALOR DE REPORTE_ID DEL ENVIO DE REPORTES
    /// </summary>
    public int WF_ENV_REPORTE_REPORTE_ID
    {
        get { return LeeIntSesion(m_ObjetoWeb, "WF_ENV_REPORTE_REPORTE_ID", 0); }
        set { GuardaIntSesion(m_ObjetoWeb, "WF_ENV_REPORTE_REPORTE_ID", value); }
    }

    /// <summary>
    /// OBTIENE EL VALOR DE TIPO_INCIDENCIA_ID
    /// </summary>
    public int WF_Tipo_Incidencias_TIPO_INCIDENCIA_ID
    {
        get { return LeeIntSesion(m_ObjetoWeb, "WF_Dias_Festivos_DIAFESTIVO_ID", 0); }
        set { GuardaIntSesion(m_ObjetoWeb, "WF_Dias_Festivos_DIAFESTIVO_ID", value); }
    }
    /// <summary>
    /// OBTIENE EL VALOR DE DIAFESTIVO_ID
    /// </summary>
    public int WF_Dias_Festivos_DIAFESTIVO_ID
    {
        get { return LeeIntSesion(m_ObjetoWeb, "WF_Dias_Festivos_DIAFESTIVO_ID", 0); }
        set { GuardaIntSesion(m_ObjetoWeb, "WF_Dias_Festivos_DIAFESTIVO_ID", value); }
    }
    /// <summary>
    /// OBTIENE EL VALOR DE EmpleadoTerminal_Persona_id
    /// </summary>
    public int WF_EmpleadoTerminal_Persona_id
    {
        get { return LeeIntSesion(m_ObjetoWeb, "WF_EmpleadoTerminal_Persona_id", 0); }
        set { GuardaIntSesion(m_ObjetoWeb, "WF_EmpleadoTerminal_Persona_id", value); }
    }
    /// <summary>
    /// OBTIENE EL VALOR DE Turno_ID
    /// </summary>
    public int WF_Turnos_TURNO_ID
    {
        get { return LeeIntSesion(m_ObjetoWeb, "WF_Turnos_TURNO_ID", 0); }
        set { GuardaIntSesion(m_ObjetoWeb, "WF_Turnos_TURNO_ID", value); }
    }
    /// <summary>
    /// OBTIENE EL VALOR DE Diario_Fecha
    /// </summary>
    public DateTime WF_Personas_Diario_Fecha
    {
        get { return LeeDateTimeSesion(m_ObjetoWeb, "WF_Personas_Diario_Fecha"); }
        set { GuardaDateTimeSesion(m_ObjetoWeb, "WF_Personas_Diario_Fecha", value); }
    }

    /// <summary>
    /// Parametro que contiene el personaID usado para las ediciones en personaDiario
    /// </summary>
    public int WF_Personas_Diario_Persona_ID
    {
        get
        {
            return LeeIntSesion(m_ObjetoWeb, "WF_Personas_Diario_Persona_ID", -1);
        }
        set
        {
            GuardaIntSesion(m_ObjetoWeb, "WF_Personas_Diario_Persona_ID", value);
        }
    }

    /// <summary>
    /// OBTIENE EL VALOR DE EmpleadosFil_HayFecha
    /// 0 para no hay fecha
    /// 1 para solo desde
    /// 2 para desta -> hasta
    /// </summary>
    public int WF_EmpleadosFil_HayFecha
    {
        get
        {
            return LeeIntSesion(m_ObjetoWeb, "WF_EmpleadosFil_HayFecha", 0);
        }
        set { GuardaIntSesion(m_ObjetoWeb, "WF_EmpleadosFil_HayFecha", value); }
    }
    /// <summary>
    /// OBTIENE EL VALOR DE EmpleadosFil_HayTerminales
    /// </summary>
    public bool WF_EmpleadosFil_HayTerminales
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "WF_EmpleadosFil_HayTerminales", true); }
        set { GuardaBoolSesion(m_ObjetoWeb, "WF_EmpleadosFil_HayTerminales", value); }
    }
    /// <summary>
    /// Obtiene el valor bool de EmpleadosFil_HayExportacion
    /// </summary>
    public bool WF_EmpleadosFil_HayExportacion
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "WF_EmpleadosFil_HayExportacion", true); }
        set { GuardaBoolSesion(m_ObjetoWeb, "WF_EmpleadosFil_HayExportacion", value); }
    }
    /// <summary>
    /// Obtiene el valor bool de EmpleadosFil_HayEmpleados
    /// </summary>
    public bool WF_EmpleadosFil_HayEmpleados
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "WF_EmpleadosFil_HayEmpleados", true); }
        set { GuardaBoolSesion(m_ObjetoWeb, "WF_EmpleadosFil_HayEmpleados", value); }
    }
    /// <summary>
    /// Obtiene el valor bool de EmpleadosFil_HayHora
    /// </summary>
    public bool WF_EmpleadosFil_HayHora
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "WF_EmpleadosFil_HayHora", false); }
        set { GuardaBoolSesion(m_ObjetoWeb, "WF_EmpleadosFil_HayHora", value); }
    }
    /// <summary>
    /// Obtiene el valor bool de EmpleadosFil_HayDiasLaborales
    /// </summary>
    public bool WF_EmpleadosFil_HayDiasLaborables
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "WF_EmpleadosFil_HayDiasLaborables", true); }
        set { GuardaBoolSesion(m_ObjetoWeb, "WF_EmpleadosFil_HayDiasLaborables", value); }
    }
    /// <summary>
    /// Obtiene o estable ce el valor de guardar cambios a turnos
    /// </summary>
    public bool TurnosGuardarCambios
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "TurnosGuardarCambios", true); }
        set { GuardaBoolSesion(m_ObjetoWeb, "TurnosGuardarCambios", value); }
    }

    /// <summary>
    /// Obtiene o establce el valor de AsignarGrupo_Usuario_ID
    /// </summary>
    public int WF_AsignarGrupoUsuarios_Usuario_ID
    {
        get { return LeeIntSesion(m_ObjetoWeb, "WF_AsignarGrupoUsuarios_Usuario_ID", 0); }
        set { GuardaIntSesion(m_ObjetoWeb, "WF_AsignarGrupoUsuarios_Usuario_ID", value); }
    }
    /// <summary>
    /// Limpia EmpleadosFil y todas sus caracteristicas(Titutlo,qry,qryinformacion,link,formatoreporte,
    /// fechai,fechaf,mensage,filtros,boton mensaje,dnl e instancias)
    /// </summary>
    /// <returns></returns>
    public bool WF_EmpleadosFil_Limpia()
    {
        try
        {
            WF_EmpleadosFil_Titulo = "";
            WF_EmpleadosFil_Qry = "";
            WF_EmpleadosFil_QryInformacion = "";
            WF_EmpleadosFil_LINK = "";
            WF_EmpleadosFil_FormatoReporte = 0;
            WF_EmpleadosFil_FechaI = DateTime.Today;
            WF_EmpleadosFil_FechaF = DateTime.Today.AddDays(1);
            WF_EmpleadosFil_Mensage = "";
            WF_EmpleadosFil_Filtros = "";
            WF_EmpleadosFil_BotonMensaje = "";
            WF_EmpleadosFil_DNL = 0;
            Inasistencias = 0;
            return true;
        }
        catch
        {

        }
        return false;
    }
    /// <summary>
    /// Obtiene el valor de EmpleadosFil_Titulo
    /// </summary>
    public string WF_EmpleadosFil_Titulo
    {
        get { return LeeStringSesion(m_ObjetoWeb, "WF_EmpleadosFil_Titulo"); }
        set { GuardaStringSesion(m_ObjetoWeb, "WF_EmpleadosFil_Titulo", value); }
    }
    /// <summary>
    /// Obtiene el valor de EmpleadosFil_Qry
    /// </summary>
    public string WF_EmpleadosFil_Qry
    {
        get { return LeeStringSesion(m_ObjetoWeb, "WF_EmpleadosFil_Qry"); }
        set { GuardaStringSesion(m_ObjetoWeb, "WF_EmpleadosFil_Qry", value); }
    }

    /// <summary>
    /// Obtiene el valor de WF_EmpleadosFil_CamposFiltro
    /// </summary>
    public string WF_EmpleadosFil_CamposFiltro
    {
        get { return LeeStringSesion(m_ObjetoWeb, "WF_EmpleadosFil_CamposFiltro"); }
        set { GuardaStringSesion(m_ObjetoWeb, "WF_EmpleadosFil_CamposFiltro", value); }
    }

    /// <summary>
    /// Obtiene el valor de EmpleadosFil_QryInformation
    /// </summary>
    public string WF_EmpleadosFil_QryInformacion
    {
        get { return LeeStringSesion(m_ObjetoWeb, "WF_EmpleadosFil_QryInformacion"); }
        set { GuardaStringSesion(m_ObjetoWeb, "WF_EmpleadosFil_QryInformacion", value); }
    }
    /// <summary>
    /// Obtiene el valor de EmpleadosFil_Link
    /// </summary>
    public string WF_EmpleadosFil_LINK
    {
        get { return LeeStringSesion(m_ObjetoWeb, "WF_EmpleadosFil_LINK"); }
        set { GuardaStringSesion(m_ObjetoWeb, "WF_EmpleadosFil_LINK", value); }
    }
    /// <summary>
    /// Obtiene o establece el formato en el que los reportes se mostraran
    /// </summary>
    public int WF_EmpleadosFil_FormatoReporte
    {
        get { return LeeIntSesion(m_ObjetoWeb, "WF_EmpleadosFil_FormatoReporte", 0); }
        set { GuardaIntSesion(m_ObjetoWeb, "WF_EmpleadosFil_FormatoReporte", value); }
    }
    /// <summary>
    /// Obtiene o establece la fecha de la sesion
    /// </summary>
    public DateTime WF_EmpleadosFil_FechaI
    {
        get
        {
            return LeeDateTimeSesion(m_ObjetoWeb, "WF_EmpleadosFil_FechaI", DateTime.Today);
        }
        set
        {
            GuardaDateTimeSesion(m_ObjetoWeb, "WF_EmpleadosFil_FechaI", value);
        }
    }
    /// <summary>
    /// Obtiene o establece la FechaF de la sesion
    /// </summary>
    public DateTime WF_EmpleadosFil_FechaF
    {
        get
        {
            return LeeDateTimeSesion(m_ObjetoWeb, "WF_EmpleadosFil_FechaF", DateTime.Today);
        }
        set { GuardaDateTimeSesion(m_ObjetoWeb, "WF_EmpleadosFil_FechaF", value); }
    }
    /// <summary>
    /// Obtiene o establece Mensaje 
    /// </summary>
    public string WF_EmpleadosFil_Mensage
    {
        get { return LeeStringSesion(m_ObjetoWeb, "WF_EmpleadosFil_Mensage"); }
        set { GuardaStringSesion(m_ObjetoWeb, "WF_EmpleadosFil_Mensage", value); }
    }
    /// <summary>
    /// Obtiene los Filtros de los Empleados
    /// </summary>
    public string WF_EmpleadosFil_Filtros
    {
        get { return LeeStringSesion(m_ObjetoWeb, "WF_EmpleadosFil_Filtros"); }
        set { GuardaStringSesion(m_ObjetoWeb, "WF_EmpleadosFil_Filtros", value); }

    }
    /// <summary>
    /// Obtiene el Boton Mensaje
    /// </summary>
    public string WF_EmpleadosFil_BotonMensaje
    {
        get { return LeeStringSesion(m_ObjetoWeb, "WF_EmpleadosFil_BotonMensaje"); }
        set { GuardaStringSesion(m_ObjetoWeb, "WF_EmpleadosFil_BotonMensaje", value); }
    }
    /// <summary>
    /// Indica si no se mostraran los dias no laborables
    /// </summary>
    public int WF_EmpleadosFil_DNL
    {
        get { return LeeIntSesion(m_ObjetoWeb, "WF_EmpleadosFil_DNL", 0); }
        set { GuardaIntSesion(m_ObjetoWeb, "WF_EmpleadosFil_DNL", value); }
    }

    /// <summary>
    /// Obtiene el filtro predeterminado validando el nivel de usuario
    /// </summary>
    public string WF_EmpleadosFil_ObtenFiltroDefault
    {
        get
        {
            string Filtro = "";

            if (TienePermiso(eClock.CEC_RESTRICCIONES.S0Empleados0Listado))
            {
                //Filtro = "SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_BORRADO = 0";
                Filtro = "SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_BORRADO = 0 and (EC_PERSONAS.SUSCRIPCION_ID in (Select EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID from EC_PERMISOS_SUSCRIP where EC_PERMISOS_SUSCRIP.usuario_id = " + USUARIO_ID + "))";
            }
            else
            {

                if (TienePermiso(eClock.CEC_RESTRICCIONES.S0Empleados0Listado0Grupo) || TienePermiso(eClock.CEC_RESTRICCIONES.S0Reportes0Reportes_Empleados0Grupo))
                {
                    Filtro = "SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_BORRADO = 0 and (EC_PERSONAS.SUSCRIPCION_ID in (Select EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID from EC_PERMISOS_SUSCRIP where EC_PERMISOS_SUSCRIP.usuario_id = " + USUARIO_ID + "))";
                }
                else
                    Filtro = "SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_BORRADO = -1";
            }
            return Filtro;
        }

    }
    /// <summary>
    /// Obtiene o establece el texto que contiene el catalogo de opciones del filtro
    /// </summary>
    public string WF_EmpleadosFil_Opciones
    {
        get { return LeeStringSesion(m_ObjetoWeb, "WF_EmpleadosFil_Opciones"); }
        set { GuardaStringSesion(m_ObjetoWeb, "WF_EmpleadosFil_Opciones", value); }
    }


    public bool WF_EmpleadosFil_OpcionAgrega(string Imagen, string Link, string Titulo, string ToolTip)
    {
        WF_EmpleadosFil_Opciones += Imagen + "|" + Link + "|" + Titulo + "|" + ToolTip + "|" + "@";
        return true;
    }
    public bool WF_EmpleadosFil_OpcionLimpia()
    {
        WF_EmpleadosFil_Opciones = "";
        return true;
    }
    public bool WF_EmpleadosFil_LimpiaParametros()
    {
        WF_EmpleadosFil_FormatoReporte = 0;
        WF_EmpleadosFil_OpcionLimpia();
        return true;
    }
    /// <summary>
    /// Redirecciona a la pag WF_EmpleadosFil.aspx
    /// </summary>
    /// <returns></returns>
    public bool WF_EmpleadosFil()
    {
        return Redirige("WF_EmpleadosFil.aspx");
    }



    /// <summary>
    /// Asigna la fecha, terminales, titulo de la apagina y los botones de mensaje y nos redirecciona a la pag WF_EmpleadosFil.aspx
    /// </summary>
    /// <param name="HayExportacion"></param>
    /// <param name="HayFecha"></param>
    /// <param name="HayTerminales"></param>
    /// <param name="BotonMensaje"></param>
    /// <param name="TituloPagina"></param>
    /// <param name="Link"></param>
    /// <param name="TituloFil"></param>
    /// <param name="HayHora"></param>
    /// <param name="HayEmpleados"></param>
    /// <param name="HayDiasLaborales"></param>
    /// <returns></returns>
    public bool WF_EmpleadosFil(bool HayExportacion, bool HayFecha, bool HayTerminales, string BotonMensaje, string TituloPagina, string Link, string TituloFil, bool HayHora, bool HayEmpleados, bool HayDiasLaborales)
    {
        int iHayFecha = 0;
        if (HayFecha)
            iHayFecha = 2;
        return WF_EmpleadosFil(HayExportacion, iHayFecha, HayTerminales, BotonMensaje, TituloPagina, Link, TituloFil, HayHora, HayEmpleados, HayDiasLaborales);
    }

    public bool WF_EmpleadosFil(bool HayExportacion, int HayFecha, bool HayTerminales, string BotonMensaje, string TituloPagina, string Link, string TituloFil, bool HayHora, bool HayEmpleados, bool HayDiasLaborales)
    {
        this.WF_EmpleadosFil_HayExportacion = HayExportacion;
        this.WF_EmpleadosFil_HayEmpleados = HayEmpleados;
        this.WF_EmpleadosFil_HayHora = HayHora;
        this.WF_EmpleadosFil_HayFecha = HayFecha;
        this.WF_EmpleadosFil_HayTerminales = HayTerminales;
        GuardaStringSesion(m_ObjetoWeb, "WF_EmpleadosFil_BotonMensaje", BotonMensaje);
        this.TituloPagina = TituloPagina;
        GuardaStringSesion(m_ObjetoWeb, "WF_EmpleadosFil_LINK", Link);
        GuardaStringSesion(m_ObjetoWeb, "WF_EmpleadosFil_Titulo", TituloFil);
        this.WF_EmpleadosFil_HayDiasLaborables = HayDiasLaborales;
        //WF_EmpleadosFil_FormatoReporte = 0;
        return Redirige("WF_EmpleadosFil.aspx");
    }

    /// <summary>
    /// Obtiene o establece los dias de la Semana de WF_HorarioPersona_Semana
    /// </summary>
    public DateTime WF_HorarioPersona_Semana
    {
        get
        {
            try { return (DateTime)m_PaginaWeb.Session["WF_HorarioPersona_Semana"]; }
            catch { }
            return DateTime.Today.AddDays(-(DateTime.Today.DayOfWeek - DayOfWeek.Sunday));
        }
        set { m_PaginaWeb.Session["WF_HorarioPersona_Semana"] = value; }
    }
    /// <summary>
    /// Obtiene o establece los Datos de WF_HorarioPersona_Datos
    /// </summary>
    public object WF_HorarioPersona_Datos
    {
        get
        {
            try { return m_PaginaWeb.Session["WF_HorarioPersona_Datos"]; }
            catch { }
            return null;
        }
        set { m_PaginaWeb.Session["WF_HorarioPersona_Datos"] = value; }
    }
    /// <summary>
    /// Redirecciona a la pagina WF_EmpleadosBus2.aspx
    /// </summary>
    /// <returns></returns>
    public bool WF_EmpleadosBus()
    {
        return Redirige("WF_EmpleadosBus2.aspx");
    }
    /// <summary>
    /// Obtiene o establece la sesion de la pagina WF_EmpleadosBus_Titulo
    /// </summary>
    public string WF_EmpleadosBus_Titulo
    {
        get
        {
            return LeeStringSesion(m_ObjetoWeb, "WF_EmpleadosBus_Titulo");
        }
        set
        {
            GuardaStringSesion(m_ObjetoWeb, "WF_EmpleadosBus_Titulo", value);
        }
    }
    /// <summary>
    /// Obtiene o establece la sesion de la pagina WF_EmpleadosBus_Link
    /// </summary>
    public string WF_EmpleadosBus_Link
    {
        get
        {
            return LeeStringSesion(m_ObjetoWeb, "WF_EmpleadosBus_Link");
        }
        set
        {
            GuardaStringSesion(m_ObjetoWeb, "WF_EmpleadosBus_Link", value);
        }
    }

    /// <summary>
    /// Obtiene o establece la sesion de WF_EmpleadosBus_Query
    /// </summary>
    public string WF_EmpleadosBus_Query
    {
        get
        {
            return LeeStringSesion(m_ObjetoWeb, "WF_EmpleadosBus_Query");
        }
        set
        {
            GuardaStringSesion(m_ObjetoWeb, "WF_EmpleadosBus_Query", value);
        }
    }
    /// <summary>
    /// Obtiene o establece la sesion de  WF_EmpleadosBus_Query_Temp
    /// </summary>
    public string WF_EmpleadosBus_Query_Temp
    {
        get
        {
            return LeeStringSesion(m_ObjetoWeb, "WF_EmpleadosBus_Query_Temp");
        }
        set
        {
            GuardaStringSesion(m_ObjetoWeb, "WF_EmpleadosBus_Query_Temp", value);
        }
    }
    /// <summary>
    /// Obtiene o establece la sesion de WF_PersonasAltasBajas
    /// </summary>

    public string WF_PersonasAltasBajas_Query
    {
        get
        {
            return LeeStringSesion(m_ObjetoWeb, "WF_PersonasAltasBajas");
        }
        set
        {
            GuardaStringSesion(m_ObjetoWeb, "WF_PersonasAltasBajas", value);
        }
    }
    /// <summary>
    /// Obtiene o establece la sesion de WF_PersonasAltasBajas_Link
    /// </summary>
    public string WF_PersonasAltasBajas_Link
    {
        get
        {
            return LeeStringSesion(m_ObjetoWeb, "WF_PersonasAltasBajas_Link");
        }
        set
        {
            GuardaStringSesion(m_ObjetoWeb, "WF_PersonasAltasBajas_Link", value);
        }
    }
    /// <summary>
    /// Obtiene o establece la sesion de WF_PersonasAltasBajas_Titulo
    /// </summary>
    public string WF_PersonasAltasBajas_Titulo
    {
        get
        {
            return LeeStringSesion(m_ObjetoWeb, "WF_PersonasAltasBajas_Titulo");
        }
        set
        {
            GuardaStringSesion(m_ObjetoWeb, "WF_PersonasAltasBajas_Titulo", value);
        }
    }
    /// <summary>
    /// Redirecciona a la pagina WF_EmpleadosFil.aspx
    /// </summary>
    /// <returns></returns>
    public bool WF_PersonasAltasBajas()
    {
        return Redirige("WF_EmpleadosFil.aspx");
    }
    /// <summary>
    /// Obtiene o establece la sesion de Dia0
    /// </summary>
    public string Dia0
    {
        get
        {
            return LeeStringSesion(m_ObjetoWeb, "Dia0");
        }
        set
        {
            GuardaStringSesion(m_ObjetoWeb, "Dia0", value);
        }
    }
    /// <summary>
    /// Obtiene o establece la sesion de WF_Productos_PRODUCTO_ID
    /// </summary>
    public int WF_Productos_PRODUCTO_ID
    {
        get
        {
            return LeeIntSesion(m_ObjetoWeb, "WF_Productos_PRODUCTO_ID", 0);

        }
        set
        {
            GuardaIntSesion(m_ObjetoWeb, "WF_Productos_PRODUCTO_ID", value);
        }
    }
    /// <summary>
    /// Obtiene o establece la sesion de Dia1
    /// </summary>
    public string Dia1
    {
        get { return LeeStringSesion(m_ObjetoWeb, "Dia1"); }
        set { GuardaStringSesion(m_ObjetoWeb, "Dia1", value); }
    }
    /// <summary>
    /// Obtiene o establece la sesion de Dia2
    /// </summary>
    public string Dia2
    {
        get { return LeeStringSesion(m_ObjetoWeb, "Dia2"); }
        set { GuardaStringSesion(m_ObjetoWeb, "Dia2", value); }
    }
    /// <summary>
    /// Obtiene o establece la sesion de Dia3
    /// </summary>
    public string Dia3
    {
        get { return LeeStringSesion(m_ObjetoWeb, "Dia3"); }
        set { GuardaStringSesion(m_ObjetoWeb, "Dia3", value); }
    }
    /// <summary>
    /// Obtiene o establece la sesion de Dia4
    /// </summary>
    public string Dia4
    {
        get { return LeeStringSesion(m_ObjetoWeb, "Dia4"); }
        set { GuardaStringSesion(m_ObjetoWeb, "Dia4", value); }
    }
    /// <summary>
    /// Obtiene o establece la sesion de Dia5
    /// </summary>
    public string Dia5
    {
        get { return LeeStringSesion(m_ObjetoWeb, "Dia5"); }
        set { GuardaStringSesion(m_ObjetoWeb, "Dia5", value); }
    }
    /// <summary>
    /// Obtiene o establece la sesion de Dia6
    /// </summary>
    public string Dia6
    {
        get { return LeeStringSesion(m_ObjetoWeb, "Dia6"); }
        set { GuardaStringSesion(m_ObjetoWeb, "Dia6", value); }
    }
    /// <summary>
    /// Si el combobox es seleccionado cambiara o modificara la consulta
    /// </summary>
    /// <param name="Adaptador"></param>
    /// <param name="Query"></param>
    /// <param name="Cbox"></param>
    public void DA_ModQuery(OleDbDataAdapter Adaptador, string Query, System.Web.UI.WebControls.CheckBox Cbox)
    {
        if (Cbox.Checked)
            Adaptador.SelectCommand.CommandText = Adaptador.SelectCommand.CommandText.Replace(Query, " ");
    }
    /// <summary>
    /// Si el combobox es seleccionado cambiara o modificara la consulta en otro caso reemplazara
    /// por valores predeterminados
    /// </summary>
    /// <param name="Adaptador"></param>
    /// <param name="Query"></param>
    /// <param name="Cbox"></param>
    public void DA_ModQueryAddColumnaUltraGrid(OleDbDataAdapter Adaptador, string Query, System.Web.UI.WebControls.CheckBox Cbox)
    {
        if (Cbox.Checked)
            Adaptador.SelectCommand.CommandText = Adaptador.SelectCommand.CommandText.Replace(Query, " ");

        Adaptador.SelectCommand.CommandText = Adaptador.SelectCommand.CommandText.Replace("123456789", " (case when EC_PERSONAS.PERSONA_BORRADO = 0 then 'Activado' else 'Desactivado' End ) ");
    }
    /// <summary>
    /// Si el combobox es seleccionado cambiara o modificara la consulta en otro caso reemplazara
    /// el valor por uno default cuando el campo sea igual a 0
    /// </summary>
    /// <param name="Adaptador"></param>
    /// <param name="SenteciaQueryBorrado"></param>
    /// <param name="Cbox"></param>
    /// <param name="Campo"></param>
    public void DA_ModQueryAddColumnaUltraGridPerzonalizada(OleDbDataAdapter Adaptador, string SenteciaQueryBorrado, System.Web.UI.WebControls.CheckBox Cbox, string Campo)
    {
        if (Cbox.Checked)
            Adaptador.SelectCommand.CommandText = Adaptador.SelectCommand.CommandText.Replace(SenteciaQueryBorrado, "  ");

        Adaptador.SelectCommand.CommandText = Adaptador.SelectCommand.CommandText.Replace("123456789", " (case when " + Campo + " = 0 then 'Activado' else 'Desactivado' End ) ");
    }
    /// <summary>
    /// Convierte la imagen a bytes
    /// </summary>
    /// <param name="Direccion"></param>
    /// <returns></returns>
    public byte[] convertImagen(string Direccion)
    {
        FileStream ff = new FileStream(Direccion, FileMode.Open);
        BinaryReader binario = new BinaryReader(ff);

        byte[] Imagen = new byte[(int)ff.Length];
        binario.Read(Imagen, 0, (int)ff.Length);
        binario.Close();
        ff.Close();
        return Imagen;
    }
    /// <summary>
    /// Obtiene o establece la sesion de Error_Mail
    /// </summary>
    public string Error_Mail
    {
        get { return LeeStringSesion(m_ObjetoWeb, "Error_Mail"); }
        set { GuardaStringSesion(m_ObjetoWeb, "Error_Mail", value); }
    }
    /// <summary>
    /// Obtiene o establece la sesion de WF_Grupos1ControlCAdenaControl
    /// </summary>
    public string WF_Grupos1ControlCAdenaControl
    {
        get { return LeeStringSesion(m_ObjetoWeb, "WF_Grupos1ControlCAdenaControl"); }
        set { GuardaStringSesion(m_ObjetoWeb, "WF_Grupos1ControlCAdenaControl", value); }
    }
    /// <summary>
    /// Obtiene o establece la sesion de  WF_EmpleadosFil_Qry_Temp
    /// </summary>
    public string WF_EmpleadosFil_Qry_Temp
    {
        get { return LeeStringSesion(m_ObjetoWeb, "WF_EmpleadosFil_Qry_Temp"); }
        set { GuardaStringSesion(m_ObjetoWeb, "WF_EmpleadosFil_Qry_Temp", value); }
    }
    /// <summary>
    /// Obtiene o establece la sesion de WF_EmpleadosFilCadenaControlUsuarios
    /// </summary>
    public string WF_EmpleadosFilCadenaControlUsuarios
    {
        get { return LeeStringSesion(m_ObjetoWeb, "WF_EmpleadosFilCadenaControlUsuarios"); }
        set { GuardaStringSesion(m_ObjetoWeb, "WF_EmpleadosFilCadenaControlUsuarios", value); }
    }
    /// <summary>
    /// Obtiene o establece la sesion de WF_PerfilesNodosSeleccion
    /// </summary>
    public string WF_PerfilesNodosSeleccion
    {
        get { return LeeStringSesion(m_ObjetoWeb, "WF_PerfilesNodosSeleccion"); }
        set { GuardaStringSesion(m_ObjetoWeb, "WF_PerfilesNodosSeleccion", value); }
    }
    /// <summary>
    /// Obtiene o establece la sesion de ObjectArray_Sesion
    /// </summary>
    public object ObjectArray_Sesion
    {
        get { return LeeObjectSesion(m_ObjetoWeb, "ObjectArray_Sesion"); }
        set { GuardaObjectSesion(m_ObjetoWeb, "ObjectArray_Sesion", value); }
    }

    /// <summary>
    /// Obtiene o establece la sesion de WF_EmpleadosFilEnviarReporteUsuarios
    /// </summary>
    public bool WF_EmpleadosFilEnviarReporteUsuarios
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "WF_EmpleadosFilEnviarReporteUsuarios", true); }
        set { GuardaBoolSesion(m_ObjetoWeb, "WF_EmpleadosFilEnviarReporteUsuarios", value); }
    }
    public bool WF_EnviarReporteUsuariosGuardado
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "WF_EnviarReporteUsuariosGuardado", false); }
        set { GuardaBoolSesion(m_ObjetoWeb, "WF_EnviarReporteUsuariosGuardado", value); }
    }
    /// <summary>
    /// Regresa el mesaje final del usuario agregando la direccion el ancho y la alineacion
    /// asi como la fecha
    /// </summary>
    /// <param name="MensajeDeusuario"></param>
    /// <returns></returns>
    private string ObtenerEnviaMailMensaje(string MensajeDeusuario)
    {
        string Espacios = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

        /*MensajeDeusuario = MensajeDeusuario.Replace("<","!");
        MensajeDeusuario = MensajeDeusuario.Replace(">","!");*/

        string Mensaje_Final = "Sistema de Asistencia y Control de Acceso " + Espacios + DateTime.Today.ToString("dd/MM/yyyy");
        Mensaje_Final += "<br>";
        Mensaje_Final += MensajeDeusuario;

        Mensaje_Final += "<br>";
        Mensaje_Final += "<hr width =50% align =left>";
        Mensaje_Final += "http://www.EntryTec.com.mx";


        return Mensaje_Final;

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
    /// <summary>
    /// Envia un mail en caso de no ser enviado nos avisara
    /// </summary>
    /// <param name="MailDestino"></param>
    /// <param name="Mensaje"></param>
    /// <param name="DireccionArchivo"></param>
    /// <param name="Asunto"></param>
    /// <param name="TipoReporte"></param>
    /// <param name="UsuarioSender"></param>
    /// <returns></returns>
    public string EnviaMail(string MailDestino, string Mensaje, string DireccionArchivo, string Asunto, string TipoReporte, string UsuarioSender)
    {
        try
        {
            MailMessage NuevoMail = new MailMessage();
            NuevoMail.To = MailDestino;
            NuevoMail.Attachments.Add(new MailAttachment(DireccionArchivo));
            NuevoMail.From = CeC_Config.SevidorCorreo;


            if (Asunto.Length > 0)
            {
                NuevoMail.Subject = Asunto;
            }
            else
            {
                NuevoMail.Subject = "Sistema de Asistencia";
            }

            NuevoMail.Body = ObtenerEnviaMailMensaje("Reporte Eviado por : " + UsuarioSender + " <br>  Tipo de Reporte : " + TipoReporte + " <br> Mensaje : " + Mensaje);
            NuevoMail.BodyFormat = MailFormat.Html;

            /*
            if (CeC_Sesion.ObtenerValorWebconfig("ServidorSMTPNombreUsuario").Length >1)
            {
                NuevoMail.Fields["http://schemas.microsoft.com/cdo/configuration/smtsperver"]= CeC_Sesion.ObtenerValorWebconfig("SevidorSMTP");
                NuevoMail.Fields["http://schemas.microsoft.com/cdo/configuration/smtpserverport"] = Convert.ToInt32(CeC_Sesion.ObtenerValorWebconfig("SevidorSMTPPuerto"));
                NuevoMail.Fields["http://schemas.microsoft.com/cdo/configuration/sendusing"]  = 2;
                NuevoMail.Fields["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"] = 1;
                NuevoMail.Fields["http://schemas.microsoft.com/cdo/configuration/sendusername"] = CeC_Sesion.ObtenerValorWebconfig("ServidorSMTPNombreUsuario");
                NuevoMail.Fields["http://schemas.microsoft.com/cdo/configuration/sendpassword"] = CeC_Sesion.ObtenerValorWebconfig("ServidorSMTPPass");

            }
					
			
*/


            NuevoMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", "2");
            NuevoMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtsperver", CeC_Config.SevidorSMTP);
            NuevoMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", CeC_Config.SevidorSMTPPuerto);

            NuevoMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
            NuevoMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", CeC_Config.ServidorSMTPNombreUsuario);
            NuevoMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", CeC_Config.ServidorSMTPPass);

            SmtpMail.SmtpServer = CeC_Config.SevidorSMTP;
            SmtpMail.Send(NuevoMail);

            //Error_Mail += "\n " + "E-mail Enviado Exitosamente a "+ MailDestino;
            return "E-mail Enviado Exitosamente a " + MailDestino;

        }
        catch (Exception ex)
        {
            Error_Mail += "\n " + "E-mail Enviado Exitosamente a " + MailDestino;
            return "No se a podido enviar el mail al siguiente destinatario" + MailDestino;
        }

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


    private string ObtenBDPermisosEfectivos()
    {
        try
        {
            DataSet DS = (DataSet)CeC_BD.EjecutaDataSet("SELECT EC_RESTRICCIONES.RESTRICCION FROM EC_RESTRICCIONES INNER JOIN EC_RESTRICCIONES_PERFILES ON EC_RESTRICCIONES.RESTRICCION_ID = EC_RESTRICCIONES_PERFILES.RESTRICCION_ID WHERE     (EC_RESTRICCIONES_PERFILES.PERFIL_ID = " + PERFIL_ID + ") AND (EC_RESTRICCIONES.RESTRICCION_BORRADO = 0) ORDER BY EC_RESTRICCIONES.RESTRICCION_ID");
            string Permisos = "";

            for (int Cont = 0; Cont < DS.Tables[0].Rows.Count; Cont++)
            {
                Permisos += DS.Tables[0].Rows[Cont][0].ToString() + "|";
            }
            PermisosEfectivos = Permisos;
            return Permisos;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return "";
    }

    public string PermisosEfectivos
    {
        get
        {
            string Permisos = LeeStringSesion(m_ObjetoWeb, "PermisosEfectivos");
            if (Permisos.Length < 1)
                return ObtenBDPermisosEfectivos();
            return Permisos;
        }
        set { GuardaStringSesion(m_ObjetoWeb, "PermisosEfectivos", value); }
    }
    /// <summary>
    /// Verifica si el usuario logeado tiene permiso para desarrollar determinada restriccion
    /// o sobre una con mayor jerarquia
    /// </summary>
    /// <param name="Permiso"></param>
    /// <returns></returns>
    public bool TienePermiso(string Permiso)
    {
        string Permisos = PermisosEfectivos;
        if (Permisos.Length < 1)
            return false;
        return TienePermiso(Permiso, Permisos);
    }

    private bool TienePermiso(string Permiso, string Permisos)
    {
        if (Permiso.Length < 1)
            return false;
        if (Permisos.Length < 1)
            return false;
        string[] Perms = Permisos.Split(new char[] { '|' });
        foreach (string Perm in Perms)
        {
            if (Perm == Permiso)
                return true;
        }
        int Pos = Permiso.LastIndexOf('.');
        if (Pos <= 0)
            return false;
        return TienePermiso(Permiso.Substring(0, Pos), Permisos);
    }
    public bool TienePermisoOHijos(string Permiso)
    {
        string Permisos = PermisosEfectivos;
        if (Permisos.Length < 1)
            return false;
        string[] Perms = Permisos.Split(new char[] { '|' });
        foreach (string Perm in Perms)
        {
            if (Perm.IndexOf(Permiso) >= 0)
                return true;
        }

        return TienePermiso(Permiso, Permisos);
    }
    public bool TienePermisoOHijos(string Permiso, bool MostrarError)
    {
        bool Res = TienePermisoOHijos(Permiso);
        if (!Res && MostrarError)
            CIT_Perfiles.CrearVentana((Page)m_ObjetoWeb, MensajeVentanaJScript(), TituloPagina, "Aceptar", "parent.location.reload()", "", "");
        return Res;
    }

    /// <summary>
    /// Arreglo que verifica que sea igual al nivel y nos regresara el valor
    /// </summary>
    /// <param name="ArrayCadena"></param>
    /// <param name="Nivel"></param>
    /// <returns></returns>
    public static string ArrayUnir(string[] ArrayCadena, int Nivel)
    {
        string valor = "";
        for (int i = 0; i < ArrayCadena.Length; i++)
        {
            if (i == Nivel)
            {
                valor += ArrayCadena[i];
                break;
            }
            else
            {
                valor += ArrayCadena[i] + ".";
            }
        }

        return valor;
    }
    /// <summary>
    /// Intenta validar el acceso
    /// </summary>
    /// <param name="Permiso"></param>
    /// <returns></returns>
    public bool ValidaAcceso(string Permiso)
    {
        try
        {
            int Pos = Permiso.LastIndexOf('.');
            if (Pos > 0)
            {
                bool Res = ValidaAcceso(Permiso.Substring(0, Pos));
                if (Res)
                {
                    return Res;
                }
            }
            String Qry = "SELECT RESTRICCION_ID FROM EC_RESTRICCIONES WHERE RESTRICCION_ID IN (" +
                "SELECT RESTRICCION_ID FROM EC_RESTRICCIONES_PERFILES WHERE PERFIL_ID = " + PERFIL_ID.ToString() +
            ") AND (RESTRICCION = '" + Permiso + "' OR RESTRICCION = '''" + Permiso + "''')";
            if (CeC_BD.EjecutaEscalarInt(Qry) > 0)
                return true;
        }
        catch
        {

        }
        return false;

    }
    /*	public bool ExisteRestriccion(string Restriccion)
        {
            String Qry = "SELECT RESTRICCION_ID FROM EC_RESTRICCIONES WHERE RESTRICCION = '" + Restriccion + "'";
            int ExisteRestric = CeC_BD.EjecutaEscalarInt(Qry);
            if(ExisteRestric > 0)
            {
                return true;
            }
            Qry = "INSERT INTO EC_RESTRICCIONES "
        }*/
    /// <summary>
    /// Establece un permiso de acceso 
    /// </summary>
    /// <param name="Permiso"></param>
    /// <param name="PermisosDelUsuario"></param>
    /// <returns></returns>
    public bool Acceso(string[] Permiso, string[] PermisosDelUsuario)
    {

        char[] Caracter = new char[1];
        Caracter[0] = Convert.ToChar(".");
        string Tempermiso = "";

        for (int i = 0; i < PermisosDelUsuario.Length; i++)
        {
            try
            {
                for (int j = 0; j < Permiso.Length; j++)
                {
                    if (PermisosDelUsuario[i] != null && Permiso[j] != null)
                        if (Permiso[j].Length > 0 && PermisosDelUsuario[i].Length > 0)
                        {

                            string[] Permiso_Array = Permiso[j].Split(Caracter);

                            for (int z = 0; z < Permiso_Array.Length; z++)
                            {

                                Tempermiso = ArrayUnir(Permiso_Array, z);

                                if (Tempermiso == PermisosDelUsuario[i])
                                    return true;

                            }

                        }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        return false;
    }
    /// <summary>
    /// Obtiene o establece la sesion de AplicarAEmpleadosSel
    /// </summary>
    public bool AplicarAEmpleadosSel
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "AplicarAEmpleadosSel", true); }
        set { GuardaBoolSesion(m_ObjetoWeb, "AplicarAEmpleadosSel", value); }
    }
    /// <summary>
    /// Agrega espacios al texto
    /// </summary>
    /// <param name="texto"></param>
    /// <param name="numeroSpacios"></param>
    /// <returns></returns>
    public string AddSpacios(string texto, int numeroSpacios)
    {

        for (int i = texto.Length; i < numeroSpacios; i++)
        {

            texto += " ";

        }

        return texto;

    }

    /// <summary>
    /// Establece un permiso de acceso 
    /// </summary>
    /// <param name="Permiso"></param>
    /// <param name="PermisosDelUsuario"></param>
    /// <returns></returns>
    public bool Acceso(string Permiso, string[] PermisosDelUsuario)
    {

        char[] Caracter = new char[1];
        Caracter[0] = Convert.ToChar(".");
        string[] PermisoTempArray = Permiso.Split(Caracter);
        string PermisoTempstr = "";

        for (int i = 0; i < PermisosDelUsuario.Length; i++)
        {
            try
            {
                string[] Array2 = PermisosDelUsuario[i].Split(Caracter);

                for (int j = 0; j < Array2.Length; j++)
                {
                    PermisoTempstr = ArrayUnir(Array2, j);
                    if (PermisoTempstr == PermisosDelUsuario[i])
                    {
                        return true;
                    }
                }
            }
            catch
            {

                return false;
            }

        }
        return false;
    }
#if !eClockSync
    /// <summary>
    /// Llana un combolist
    /// </summary>
    /// <param name="Tabla"></param>
    /// <param name="Campo"></param>
    /// <param name="Combolist"></param>
    /// <param name="Encabezado"></param>
    public void LlenaCombo(string Tabla, string Campo, global::Infragistics.WebUI.WebCombo.WebCombo Combolist, string Encabezado)
    {

        string Query = "Select " + Campo + " From " + Tabla + " ORDER BY " + Campo;


        OleDbConnection Conexion = new OleDbConnection(CeC_Sesion.ObtenerValorWebconfig("gBDAtos.ConnectionString"));

        DataSet DS = new DataSet("DS_Datos");
        DataTable DT = new DataTable("DT_Datos");
        DataRow DR;

        Combolist.Columns.Clear();

        if (Conexion.State != System.Data.ConnectionState.Open)
            Conexion.Open();

        OleDbCommand commando = new OleDbCommand(Query, Conexion);
        OleDbDataReader lector;

        lector = commando.ExecuteReader();

        DT.Columns.Add("Dato", System.Type.GetType("System.String"));

        try
        {
            while (lector.Read())
            {
                DR = DT.NewRow();
                try
                {
                    DR["Dato"] = lector.GetString(0);
                }
                catch
                {
                    DR["Dato"] = "";
                }

                DT.Rows.Add(DR);
            }
        }
        catch
        {
            lector.Close();
            Conexion.Close();
        }

        DS.Tables.Add(DT);
        lector.Close();
        Conexion.Close();


        Combolist.DataSource = DS;
        Combolist.DataMember = DT.ToString();
        Combolist.DataTextField = DT.Columns["Dato"].ToString();
        Combolist.DataValueField = DT.Columns["Dato"].ToString();
        Combolist.DataBind();

        Combolist.Columns[0].Header.Caption = Encabezado;


    }
    /// <summary>
    /// Nos regresa un grupo de configuracion segun sea el caso del Num_Grupo
    /// </summary>
    /// <param name="Num_Grupo"></param>
    /// <returns></returns>
    public string OBTEN_GRUPO_CONFIG_STR(int Num_Grupo)
    {
        switch (Num_Grupo)
        {
            case 1:
                return CeC_Config.CampoGrupo1;
            case 2:
                return CeC_Config.CampoGrupo2;
            case 3:
                return CeC_Config.CampoGrupo3;
            default:
                return "";
        }

    }
    /// <summary>
    /// Llena un combolist
    /// </summary>
    /// <param name="Campo"></param>
    /// <param name="Combolist"></param>
    /// <param name="Encabezado"></param>
    public void WF_Empleados_LlenadoCombo(string Campo, Infragistics.WebUI.WebCombo.WebCombo Combolist, string Encabezado)
    {

        string Query = "Select " + Campo + " From  EC_PERSONAS_DATOS Group By " + Campo;


        OleDbConnection Conexion = new OleDbConnection(CeC_Sesion.ObtenerValorWebconfig("gBDAtos.ConnectionString"));

        DataSet DS = new DataSet("DS_Datos");
        DataTable DT = new DataTable("DT_Datos");
        DataRow DR;

        Combolist.Columns.Clear();

        if (Conexion.State != System.Data.ConnectionState.Open)
            Conexion.Open();

        OleDbCommand commando = new OleDbCommand(Query, Conexion);
        OleDbDataReader lector;

        lector = commando.ExecuteReader();

        DT.Columns.Add("Dato", System.Type.GetType("System.String"));

        try
        {
            while (lector.Read())
            {
                DR = DT.NewRow();
                try
                {
                    DR["Dato"] = lector.GetString(0);
                }
                catch
                {
                    DR["Dato"] = "";
                }

                DT.Rows.Add(DR);
            }
        }
        catch
        {
            lector.Close();
            Conexion.Close();
        }

        DS.Tables.Add(DT);
        lector.Close();
        Conexion.Close();

        Combolist.DataSource = DS;
        Combolist.DataMember = DT.ToString();
        Combolist.DataTextField = DT.Columns["Dato"].ToString();
        Combolist.DataValueField = DT.Columns["Dato"].ToString();
        Combolist.DataBind();

        Combolist.Columns[0].Header.Caption = Encabezado;

    }

#endif
    /// <summary>
    /// Obtiene o establece la sesion de WF_EmpleadosFil_DescripcionTemp
    /// </summary>
    public string WF_EmpleadosFil_DescripcionTemp
    {
        get { return LeeStringSesion(m_ObjetoWeb, "WF_EmpleadosFil_DescripcionTemp"); }
        set { GuardaStringSesion(m_ObjetoWeb, "WF_EmpleadosFil_DescripcionTemp", value); }
    }

#if !eClockSync
    /*   public eClock.WC_Menu ObtenMenu()
	{
		Object Objeto = CeC_Sesion_m_Pagina.FindControl("WC_Menu1");
		try
		{
			return (eClock.WC_Menu  )Objeto;
		}
		catch{ 	}
		return null;
	}
    */
    /// <summary>
    /// Obtiene o establece el Titulo de la Pagina
    /// </summary>
    public string TituloPagina
    {
        get { return LeeStringSesion(m_PaginaWeb, "TituloPagina"); }
        set
        {
            try
            {
                //ObtenMenu().Tutulo = value;	
                m_PaginaWeb.Session["TituloPagina"] = value;
            }
            catch
            { }
        }
    }
    /// <summary>
    /// Obtiene o establece la descripcion de la pagina
    /// </summary>
    public string DescripcionPagina
    {
        get { return LeeStringSesion(m_PaginaWeb, "DescripcionPagina"); }
        set
        {
            try
            {
                m_PaginaWeb.Session["DescripcionPagina"] = value;
                //  ObtenMenu().Descripcion = value;
            }
            catch { }
        }
    }
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
    /// Obtiene o establece la sesion de WF_ControlTermnales_Terminales_Selecionadas
    /// </summary>
    public string WF_ControlTermnales_Terminales_Selecionadas
    {
        get { return LeeStringSesion(m_ObjetoWeb, "WF_ControlTermnales_Terminales_Selecionadas"); }
        set { GuardaStringSesion(m_ObjetoWeb, "WF_ControlTermnales_Terminales_Selecionadas", value); }
    }
    /// <summary>
    /// Intenta convertir a int32
    /// </summary>
    /// <param name="Valor"></param>
    /// <returns></returns>
    public int ConvertforzadoInt32(string Valor)
    {

        try
        {
            int ret = Convert.ToInt32(Valor);
            return ret;
        }
        catch
        {
            return -1;
        }

    }

    /// <summary>
    /// Obtiene o establece la sesion de WF_QueryString_Valor
    /// </summary>
    public int WF_QueryString_Valor
    {
        get { return LeeIntSesion(m_ObjetoWeb, "WF_QueryString_Valor", 0); }
        set { GuardaIntSesion(m_ObjetoWeb, "WF_QueryString_Valor", value); }
    }
    /// <summary>
    /// Obtiene o establece la sesion de Inasistencias
    /// </summary>
    public int Inasistencias
    {
        get { return LeeIntSesion(m_ObjetoWeb, "Inasistencias", 0); }
        set { GuardaIntSesion(m_ObjetoWeb, "Inasistencias", value); }
    }
    /// <summary>
    /// Obtiene o establece la sesin de Agrupaciones
    /// </summary>
    public int Caso_Agrupaciones
    {
        get { return LeeIntSesion(m_ObjetoWeb, "Caso_Agrupaciones", 0); }
        set { GuardaIntSesion(m_ObjetoWeb, "Caso_Agrupaciones", value); }
    }
    /// <summary>
    /// Obtiene o establece la sesion de Caso_Agrupaciones_Editar
    /// </summary>
    public int Caso_Agrupaciones_Editar
    {
        get { return LeeIntSesion(m_ObjetoWeb, "Caso_Agrupaciones_Editar", 0); }
        set { GuardaIntSesion(m_ObjetoWeb, "Caso_Agrupaciones_Editar", value); }
    }
    //se usa para saber que terminal esta seleccionada
    /// <summary>
    /// Obtiene o establece la sesion de WF_TerminalesE_TerminalID
    /// </summary>
    public int WF_TerminalesE_TerminalID
    {
        get { return LeeIntSesion(m_ObjetoWeb, "WF_TerminalesE_TerminalID", -1); }
        set { GuardaIntSesion(m_ObjetoWeb, "WF_TerminalesE_TerminalID", value); }
    }
    /// <summary>
    /// Obtiene o establece la sesion de WF_UsuariosE_GrupoNo
    /// </summary>
    public int WF_UsuariosE_GrupoNo
    {
        get { return LeeIntSesion(m_ObjetoWeb, "WF_UsuariosE_GrupoNo", 0); }
        set { GuardaIntSesion(m_ObjetoWeb, "WF_UsuariosE_GrupoNo", value); }
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
    /// Obtiene o establece la sesion de WF_Turnos_TipoTurno
    /// </summary>
    public int WF_Turnos_TipoTurno
    {
        get { return LeeIntSesion(m_ObjetoWeb, "WF_Turnos_TipoTurno", 0); }
        set { GuardaIntSesion(m_ObjetoWeb, "WF_Turnos_TipoTurno", value); }
    }
    /// <summary>
    /// Obtiene o establce la sesion de WF_Personas_DiarioE_Guardado
    /// </summary>
    public int WF_Personas_DiarioE_Guardado
    {
        get { return LeeIntSesion(m_ObjetoWeb, "WF_Personas_DiarioE_Guardado", 0); }
        set { GuardaIntSesion(m_ObjetoWeb, "WF_Personas_DiarioE_Guardado", value); }
    }
    /// <summary>
    /// Obtiene o establece la sesion de WF_TurnosEmpleadosE_Guardado
    /// </summary>
    public int WF_TurnosEmpleadosE_Guardado
    {
        get { return LeeIntSesion(m_ObjetoWeb, "WF_TurnosEmpleadosE_Guardado", 0); }
        set { GuardaIntSesion(m_ObjetoWeb, "WF_TurnosEmpleadosE_Guardado", value); }
    }
    /// <summary>
    /// Obtiene o establece la sesion de WF_TurnosE_HorasExtras
    /// </summary>
    public int WF_TurnosE_HorasExtras
    {
        get { return LeeIntSesion(m_ObjetoWeb, "WF_TurnosE_HorasExtras", 0); }
        set { GuardaIntSesion(m_ObjetoWeb, "WF_TurnosE_HorasExtras", value); }
    }
    /// <summary>
    /// Obtiene o establece la sesion de WF_TurnosE_TurnoAsistencia
    /// </summary>
    public int WF_TurnosE_TurnoAsistencia
    {
        get { return LeeIntSesion(m_ObjetoWeb, "WF_TurnosE_TurnoAsistencia", 0); }
        set { GuardaIntSesion(m_ObjetoWeb, "WF_TurnosE_TurnoAsistencia", value); }
    }
    /// <summary>
    /// Obtiene o establce la seseion de WF_Promociones_PROMOCION_ID
    /// </summary>
    public int WF_Promociones_PROMOCION_ID
    {
        get { return LeeIntSesion(m_ObjetoWeb, "WF_Promociones_PROMOCION_ID", 0); }
        set { GuardaIntSesion(m_ObjetoWeb, "WF_Promociones_PROMOCION_ID", value); }
    }

    /// <summary>
    /// obtiene o establece el Usuario Seleccionado en el
    /// </summary>
    public int WF_Recompensas_RECOMPENSA_LINK_ID
    {
        get
        {
            return LeeIntSesion(m_ObjetoWeb, "WF_Recompensas_RECOMPENSA_LINK_ID", 0);
        }
        set
        {
            GuardaIntSesion(m_ObjetoWeb, "WF_Recompensas_RECOMPENSA_LINK_ID", value);
        }
    }
    /// <summary>
    /// Obtiene o establece la sesion de WF_Recompensas_RECOMPENSA_ID
    /// </summary>
    public int WF_Recompensas_RECOMPENSA_ID
    {
        get
        {
            return LeeIntSesion(m_ObjetoWeb, "WF_Recompensas_RECOMPENSA_ID", 0);

        }
        set
        {
            GuardaIntSesion(m_ObjetoWeb, "WF_Recompensas_RECOMPENSA_ID", value);
        }
    }
    /// <summary>
    /// Obtiene o establece la sesion de WF_Recompensas_RECOMPENSA_ID
    /// </summary>
    public string WF_Recompensas_NOMBRE
    {
        get
        {
            return CeC_BD.EjecutaEscalarString(" SELECT RECOMPENSA_NOMBRE FROM eC_RECOMPENSAS WHERE RECOMPENSA_ID = " + WF_Recompensas_RECOMPENSA_ID.ToString());
        }
        set
        {
            GuardaStringSesion(m_ObjetoWeb, "WF_RECOMPENSAS_NOMBRE", value);
        }

    }
    /// <summary>
    /// Obtiene o establce la sesion de WF_UsuarioEmpleado_QRY
    /// </summary>
    public string WF_UsuarioEmpleado_QRY
    {
        get { return LeeStringSesion(m_ObjetoWeb, "WF_UsuarioEmpleado_QRY"); }
        set { GuardaStringSesion(m_ObjetoWeb, "WF_UsuarioEmpleado_QRY", value); }
    }
    /// <summary>
    /// Obtiene o establece la sesion de la fecha de WF_Personas_Diario_AnoTemp
    /// </summary>
    public DateTime WF_Personas_Diario_AnoTemp
    {
        get { return LeeDateTimeSesion(m_ObjetoWeb, "WF_Personas_Diario_AnoTemp", DateTime.Now); }
        set { GuardaDateTimeSesion(m_ObjetoWeb, "WF_Personas_Diario_AnoTemp", value); }
    }
    /// <summary>
    /// Obtiene o establece la sesion de WF_PERSONA_S_HUELLA_FECHA
    /// </summary>
    public string WF_PERSONA_S_HUELLA_FECHA
    {
        get { return LeeStringSesion(m_ObjetoWeb, "WF_PERSONA_S_HUELLA_FECHA"); }
        set { GuardaStringSesion(m_ObjetoWeb, "WF_PERSONA_S_HUELLA_FECHA", value); }
    }
    /// <summary>
    /// Obtiene o establece la sesion de WF_PerfilesE_Perfil_ID
    /// </summary>
    public int WF_PerfilesE_Perfil_ID
    {
        get { return LeeIntSesion(m_ObjetoWeb, "WF_PerfilesE_Perfil_ID", -1); }
        set { GuardaIntSesion(m_ObjetoWeb, "WF_PerfilesE_Perfil_ID", value); }

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
    public static int ValidarUsuario(string Usuario, string Password)
    {
        try
        {
            CeC_Sesion Sesion = new CeC_Sesion();

            string UsuarioR = Usuario;
            // if (Usuario.Length > 20)
            //     UsuarioR = Usuario.Substring(0, 20);
            int UsuarioId = ValidarUsuarioeC(UsuarioR, Password);
            if (UsuarioId < 0)
            {
                if (CeC_Config.EsDemo)
                {
                    UsuarioId = ValidarUsuarioDNN(Usuario, Password);
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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Usuario"></param>
    /// <param name="Password"></param>
    /// <returns></returns>
    public static int ValidarUsuarioDNN(string Usuario, string Password)
    {
        try
        {
            string UsuarioR = Usuario;
            //if (Usuario.Length > 20)
            //  UsuarioR = Usuario.Substring(0, 20);
            DotNetNuke.UserInfo UserInfo = null;
            int UsuarioId = 0;
            try
            {
                DotNetNuke.WebService WS = new DotNetNuke.WebService();
                DotNetNuke.IWebAuthendicationHeader IWA = new DotNetNuke.IWebAuthendicationHeader();
                IWA.Username = Usuario;
                IWA.Password = Password;
                IWA.PortalID = 0;
                IWA.Encrypted = "False";

                WS.IWebAuthendicationHeaderValue = IWA;
                UserInfo = WS.GetUser(Usuario);
            }
            catch { return -9998; }


            //UsuarioId = ValidarUsuarioDNN(Usuario, Password);
            if (UserInfo.UserID > 0)
            {
                UsuarioId = CeC_BD.EjecutaEscalarInt("SELECT USUARIO_ID FROM EC_USUARIOS WHERE USUARIO_USUARIO = '" + UsuarioR.ToLower() + "'");
                if (UsuarioId <= 0)
                {
                    int SuscripcionID = CeC_Autonumerico.GeneraAutonumerico("EC_SUSCRIPCION", "SUSCRIPCION_ID");
                    int ret = CeC_BD.EjecutaComando("INSERT INTO EC_SUSCRIPCION (SUSCRIPCION_ID, SUSCRIPCION_NOMBRE) VALUES(" + SuscripcionID + ",'" + UsuarioR + "')");

                    UsuarioId = CeC_Autonumerico.GeneraAutonumerico("EC_USUARIOS", "USUARIO_ID");
                    ret = CeC_BD.EjecutaComando("INSERT INTO EC_USUARIOS(USUARIO_ID,PERFIL_ID,USUARIO_USUARIO,USUARIO_NOMBRE,USUARIO_CLAVE,SUSCRIPCION_ID,USUARIO_BORRADO) VALUES("
                        + UsuarioId.ToString() + ",4,'" + UsuarioR + "','" + UsuarioR + "','" + Password + "'," + SuscripcionID + ",0)");
                    CeC_Suscripcion.PermisoDarControlTotal(0, UsuarioId, SuscripcionID);
                    Cec_Incidencias.CrearTiposIncidenciasPredeterminados(SuscripcionID);
                    CeC_Turnos.CreaTurnosPredeterminados(UsuarioId, SuscripcionID);
                }
                CeC_BD.EjecutaComando("UPDATE EC_USUARIOS SET USUARIO_NOMBRE = '" + UserInfo.DisplayName + "', USUARIO_EMAIL = '" + UserInfo.Email + "' WHERE USUARIO_ID = " + UsuarioId);
            }

            return UsuarioId;
        }
        catch (Exception ex)
        {
            return -9999;
        }
    }
    /// <summary>
    /// es el link al que redirigira cuando se de guardar y continuar
    /// </summary>
    public string OrigenConfigCampos
    {
        get { return LeeStringSesion(m_ObjetoWeb, "OrigenConfigCampos"); }
        set { GuardaStringSesion(m_ObjetoWeb, "OrigenConfigCampos", value); }
    }
    public int WF_Sitios_Sitio_ID
    {
        get
        {
            return LeeIntSesion(m_ObjetoWeb, "WF_Sitios_Sitio_ID", 0);

        }
        set
        {
            GuardaIntSesion(m_ObjetoWeb, "WF_Sitios_Sitio_ID", value);
        }
    }
    public string WF_TurnosAsignacionExpressQry
    {

        get { return LeeStringSesion(m_ObjetoWeb, "WF_TurnosAsignacionExpressQry"); }
        set { GuardaStringSesion(m_ObjetoWeb, "WF_TurnosAsignacionExpressQry", value); }
    }
    public string WF_TerminalesPersonasQry
    {
        get { return LeeStringSesion(m_ObjetoWeb, "WF_TerminalesPersonasQry"); }
        set { GuardaStringSesion(m_ObjetoWeb, "WF_TerminalesPersonasQry", value); }
    }
    public string WF_PersonasSemanaQry
    {
        get { return LeeStringSesion(m_ObjetoWeb, "WF_PersonasSemanaQry"); }
        set { GuardaStringSesion(m_ObjetoWeb, "WF_PersonasSemanaQry", value); }
    }
    public DS_Personas_Semana.CambiosHorarioDataTable WF_PersonasSemanaCambiosHorarioDataTable
    {
        get
        {
            try
            {
                object Obj = LeeObjectSesion(m_ObjetoWeb, "WF_PersonasSemanaCambiosHorarioDataTable");
                return (DS_Personas_Semana.CambiosHorarioDataTable)Obj;
            }
            catch { }
            return null;
        }
        set { GuardaObjectSesion(m_ObjetoWeb, "WF_PersonasSemanaCambiosHorarioDataTable", value); }
    }
    public string WF_TiempoExtraQry
    {

        get { return LeeStringSesion(m_ObjetoWeb, "WF_TiempoExtraQry"); }
        set { GuardaStringSesion(m_ObjetoWeb, "WF_TiempoExtraQry", value); }
    }

    public string WF_JustificaDiasParametros
    {
        get { return LeeStringSesion(m_ObjetoWeb, "WF_JustificaDiasParametros"); }
        set { GuardaStringSesion(m_ObjetoWeb, "WF_JustificaDiasParametros", value); }
    }

    public string WF_PerSemParametros
    {
        get { return LeeStringSesion(m_ObjetoWeb, "WF_PerSemParametros"); }
        set { GuardaStringSesion(m_ObjetoWeb, "WF_PerSemParametros", value); }
    }

    public string WF_ModuloOpcional
    {
        get { return LeeStringSesion(m_ObjetoWeb, "WF_ModulOpcional"); }
        set { GuardaStringSesion(m_ObjetoWeb, "WF_ModulOpcional", value); }
    }

    public int WF_ReglaVacaciones
    {
        get { return LeeIntSesion(m_ObjetoWeb, "WF_ReglaVacaciones", -1); }
        set { GuardaIntSesion(m_ObjetoWeb, "WF_ReglaVacaciones", value); }

    }

    public string WF_TurnosEdicion
    {
        get { return LeeStringSesion(m_ObjetoWeb, "WF_TurnosEdicion"); }
        set { GuardaStringSesion(m_ObjetoWeb, "WF_TurnosEdicion", value); }
    }

    public int WF_Personas_ImaS_Persona_ID
    {
        get { return LeeIntSesion(m_ObjetoWeb, "WF_Personas_ImaS_Persona_ID", -1); }
        set { GuardaIntSesion(m_ObjetoWeb, "WF_Personas_ImaS_Persona_ID", value); }
    }

    public int eClock_Persona_ID
    {
        get { return LeeIntSesion(m_ObjetoWeb, "eClock_Persona_ID", -1); }
        set
        {
            GuardaIntSesion(m_ObjetoWeb, "eClock_Persona_ID", value);
            WF_Personas_ImaS_Persona_ID = value;
            WF_Empleados_PERSONA_ID = value;
        }
    }
    public int eClock_Turno_ID
    {
        get { return LeeIntSesion(m_ObjetoWeb, "eClock_Turno_ID", -1); }
        set { GuardaIntSesion(m_ObjetoWeb, "eClock_Turno_ID", value); }
    }
    public string eClock_Agrupacion
    {
        get { return LeeStringSesion(m_ObjetoWeb, "eClock_Agrupacion"); }
        set { GuardaStringSesion(m_ObjetoWeb, "eClock_Agrupacion", value); }
    }

    public int AccesoTerminalFiltro
    {
        get { return LeeIntSesion(m_ObjetoWeb, "AccesoTerminalFiltro", -1); }
        set { GuardaIntSesion(m_ObjetoWeb, "AccesoTerminalFiltro", value); }
    }
    public DateTime AsistenciaFechaInicio
    {
        get { return LeeDateTimeSesion(m_ObjetoWeb, "AsistenciaFechaInicio", DateTime.Now.AddDays(-8)); }
        set { GuardaDateTimeSesion(m_ObjetoWeb, "AsistenciaFechaInicio", value); }
    }
    public DateTime AsistenciaFechaFin
    {
        get { return LeeDateTimeSesion(m_ObjetoWeb, "AsistenciaFechaFin", DateTime.Now.AddDays(-1)); }
        set { GuardaDateTimeSesion(m_ObjetoWeb, "AsistenciaFechaFin", value); }
    }
    public bool AsistenciaMostrar
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "AsistenciaMostrar", true); }
        set { GuardaBoolSesion(m_ObjetoWeb, "AsistenciaMostrar", value); }
    }
    public bool AS_PREDETERMINADO
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "AS_PREDETERMINADO", true); }
        set { GuardaBoolSesion(m_ObjetoWeb, "AS_PREDETERMINADO", value); }
    }
    public bool AS_7_DIAS
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "AS_7_DIAS", false); }
        set { GuardaBoolSesion(m_ObjetoWeb, "AS_7_DIAS", value); }
    }
    public bool AS_31_DIAS
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "AS_31_DIAS", false); }
        set { GuardaBoolSesion(m_ObjetoWeb, "AS_31_DIAS", value); }
    }
    public bool AS_ENTRADA_SALIDA
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "AS_ENTRADA_SALIDA", true); }
        set { GuardaBoolSesion(m_ObjetoWeb, "AS_ENTRADA_SALIDA", value); }
    }
    public bool AS_COMIDA
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "AS_COMIDA", false); }
        set { GuardaBoolSesion(m_ObjetoWeb, "AS_COMIDA", value); }
    }
    public bool AS_HORAS_EXTRAS
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "AS_HORAS_EXTRAS", false); }
        set { GuardaBoolSesion(m_ObjetoWeb, "AS_HORAS_EXTRAS", value); }
    }

    public bool AS_TOTALES
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "AS_TOTALES", true); }
        set { GuardaBoolSesion(m_ObjetoWeb, "AS_TOTALES", value); }
    }
    public bool AS_INCIDENCIA
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "AS_INCIDENCIA", true); }
        set { GuardaBoolSesion(m_ObjetoWeb, "AS_INCIDENCIA", value); }
    }
    public bool AS_TURNO
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "AS_TURNO", true); }
        set { GuardaBoolSesion(m_ObjetoWeb, "AS_TURNO", value); }
    }
    /*
    public bool AS_INASISTENCIAS
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "AS_INASISTENCIAS", false); }
        set { GuardaBoolSesion(m_ObjetoWeb, "AS_INASISTENCIAS", value); }
    }*/

    public bool AS_SOLO_FALTAS
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "AS_SOLO_FALTAS", false); }
        set { GuardaBoolSesion(m_ObjetoWeb, "AS_SOLO_FALTAS", value); }
    }

    public bool AS_SOLO_RETARDOS
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "AS_SOLO_RETARDOS", false); }
        set { GuardaBoolSesion(m_ObjetoWeb, "AS_SOLO_RETARDOS", value); }
    }
    public bool AS_EMPLEADO
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "AS_EMPLEADO", true); }
        set { GuardaBoolSesion(m_ObjetoWeb, "AS_EMPLEADO", value); }
    }
    public bool AS_AGRUPACION
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "AS_AGRUPACION", false); }
        set { GuardaBoolSesion(m_ObjetoWeb, "AS_AGRUPACION", value); }
    }
    public bool AS_FIRMA
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "AS_FIRMA", false); }
        set { GuardaBoolSesion(m_ObjetoWeb, "AS_FIRMA", value); }
    }

    public bool AS_G_AGRUPACION
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "AS_G_AGRUPACION", true); }
        set { GuardaBoolSesion(m_ObjetoWeb, "AS_G_AGRUPACION", value); }
    }
    public bool AS_G_EMPLEADO
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "AS_G_EMPLEADO", false); }
        set { GuardaBoolSesion(m_ObjetoWeb, "AS_G_EMPLEADO", value); }
    }

    public bool AS_T_AGRUPACION
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "AS_T_AGRUPACION", false); }
        set { GuardaBoolSesion(m_ObjetoWeb, "AS_T_AGRUPACION", value); }
    }
    public bool AS_T_EMPLEADO
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "AS_T_EMPLEADO", false); }
        set { GuardaBoolSesion(m_ObjetoWeb, "AS_T_EMPLEADO", value); }
    }
    public bool AS_T_TOTALES
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "AS_T_TOTALES", true); }
        set { GuardaBoolSesion(m_ObjetoWeb, "AS_T_TOTALES", value); }
    }
    public bool AS_T_HISTORIAL
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "AS_T_HISTORIAL", false); }
        set { GuardaBoolSesion(m_ObjetoWeb, "AS_T_HISTORIAL", value); }
    }
    public bool AS_T_SALDO
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "AS_T_SALDO", false); }
        set { GuardaBoolSesion(m_ObjetoWeb, "AS_T_SALDO", value); }
    }
    public bool AS_T_FALTAS
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "AS_T_FALTAS", false); }
        set { GuardaBoolSesion(m_ObjetoWeb, "AS_T_FALTAS", value); }
    }
    public string AS_CAMPO_ORDEN
    {
        get { return LeeStringSesion(m_ObjetoWeb, "AS_CAMPO_ORDEN"); }
        set { GuardaStringSesion(m_ObjetoWeb, "AS_CAMPO_ORDEN", value); }
    }

    public bool AsistenciaGraficar
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "AsistenciaGraficar", false); }
        set { GuardaBoolSesion(m_ObjetoWeb, "AsistenciaGraficar", value); }
    }
    public bool AsistenciaMostrarES
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "AsistenciaMostrarES", false); }
        set { GuardaBoolSesion(m_ObjetoWeb, "AsistenciaMostrarES", value); }
    }
    public bool AsistenciaTotales
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "AsistenciaTotales", false); }
        set { GuardaBoolSesion(m_ObjetoWeb, "AsistenciaTotales", value); }
    }
    public bool AsistenciaMostrarAccesos
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "AsistenciaMostrarAccesos", false); }
        set { GuardaBoolSesion(m_ObjetoWeb, "AsistenciaMostrarAccesos", value); }
    }
    public string Terminal_ES
    {
        get { return LeeStringSesion(m_ObjetoWeb, "Terminal_ES"); }
        set { GuardaStringSesion(m_ObjetoWeb, "Terminal_ES", value); }
    }
    public string AsistenciaEspecifica
    {
        get { return LeeStringSesion(m_ObjetoWeb, "AsistenciaEspecifica"); }
        set { GuardaStringSesion(m_ObjetoWeb, "AsistenciaEspecifica", value); }
    }
    public bool ASHE_ENTRADA_SALIDA
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "ASHE_ENTRADA_SALIDA", false); }
        set { GuardaBoolSesion(m_ObjetoWeb, "ASHE_ENTRADA_SALIDA", value); }
    }

    public bool ASHE_COMIDA
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "ASHE_COMIDA", false); }
        set { GuardaBoolSesion(m_ObjetoWeb, "ASHE_COMIDA", value); }
    }

    public bool ASHE_TOTALES
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "ASHE_TOTALES", false); }
        set { GuardaBoolSesion(m_ObjetoWeb, "ASHE_TOTALES", value); }
    }

    public bool ASHE_INCIDENCIA
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "ASHE_INCIDENCIA", false); }
        set { GuardaBoolSesion(m_ObjetoWeb, "ASHE_INCIDENCIA", value); }
    }

    public bool ASHE_TURNO
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "ASHE_TURNO", false); }
        set { GuardaBoolSesion(m_ObjetoWeb, "ASHE_TURNO", value); }
    }

    public bool ASHE_AGRUPACION
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "ASHE_AGRUPACION", true); }
        set { GuardaBoolSesion(m_ObjetoWeb, "ASHE_AGRUPACION", value); }
    }

    public bool ASHE_EMPLEADO
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "ASHE_EMPLEADO", true); }
        set { GuardaBoolSesion(m_ObjetoWeb, "ASHE_EMPLEADO", value); }
    }
    public bool ASHE_CEROS
    {
        get { return LeeBoolSesion(m_ObjetoWeb, "ASHE_CEROS", false); }
        set { GuardaBoolSesion(m_ObjetoWeb, "ASHE_CEROS", value); }
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

    

    // Reportes Comida
    //public bool 

    // Reportes Monedero


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
    public void MsgNoTienePermiso()
    {
        CIT_Perfiles.CrearVentana(m_PaginaWeb, MensajeVentanaJScript(), "No tiene Permiso", "Aceptar", "WF_Main.aspx", "", "");
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
}