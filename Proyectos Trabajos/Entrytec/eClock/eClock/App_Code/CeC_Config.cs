using System;
using System.Data;
using System.Configuration;

/// <summary>
/// Esta clase esta diseñada para contener todas las variables de configuracion por 
/// usuario del eClock Web, por default el usuario es 0 (sistema)
/// </summary>
public class CeC_Config
{
    protected int m_Usuario_ID = 0;

    public CeC_Config()
    { }

    /// <summary>
    /// Constructor de la clase que inicializa el identificador del usuario (UsuarioId)
    /// </summary>
    /// <param name="UsuarioId">Identificador del usuario</param>
    public CeC_Config(int UsuarioId)
    {
        m_Usuario_ID = UsuarioId;
    }

    /// <summary>
    /// Obtiene una lista de UsuarioID de todos los usuarios que tengan un parametro con el valor asignado
    /// </summary>
    /// <param name="Variable">Variable de configuración del usuario</param>
    /// <param name="Valor">Cadena con el valor de la variable</param>
    /// <returns>DataSet con una lista de Identificadores de Usuarios</returns>
    public static DataSet ObtenUsuariosConfig(string Variable, string Valor)
    {
        return (DataSet)CeC_BD.EjecutaDataSet("SELECT EC_CONFIG_USUARIO.USUARIO_ID FROM EC_CONFIG_USUARIO, EC_USUARIOS WHERE EC_USUARIOS.USUARIO_ID = EC_CONFIG_USUARIO.USUARIO_ID AND EC_USUARIOS.USUARIO_BORRADO = 0 AND CONFIG_USUARIO_VARIABLE = '" + Variable + "' AND CONFIG_USUARIO_VALOR = '" + Valor + "'");
    }

    /// <summary>
    /// Obtiene una lista de UsuarioID de todos los usuarios que tengan un parametro con el valor asignado
    /// </summary>
    /// <param name="Variable">Variable de configuración del usuario</param>
    /// <param name="Valor">Cadena con el valor de la variable</param>
    /// <returns>DataSet con una lista de Identificadores de Usuarios</returns>
    public static DataSet ObtenUsuariosConfig(string Variable, bool Valor)
    {
        return ObtenUsuariosConfig(Variable, Valor.ToString());
    }

    /// <summary>
    /// Obtiene una lista de UsuarioID de todos los usuarios que tengan un parametro con el valor asignado
    /// </summary>
    /// <param name="Variable">Variable de configuración del usuario</param>
    /// <param name="Valor">Cadena con el valor de la variable</param>
    /// <returns>DataSet con una lista de Identificadores de Usuarios</returns>
    public static DataSet ObtenUsuariosConfig(string Variable, DateTime Valor)
    {
        return ObtenUsuariosConfig(Variable, Valor.ToString("dd/MM/yyyy HH:mm:ss"));
    }

    /// <summary>
    /// Obtiene una lista de UsuarioID de todos los usuarios que tengan un parametro con el valor asignado
    /// </summary>
    /// <param name="Variable">Variable de configuración del usuario</param>
    /// <param name="Valor">Cadena con el valor de la variable</param>
    /// <returns>DataSet con una lista de Identificadores de Usuarios</returns>
    public static DataSet ObtenUsuariosConfig(string Variable, int Valor)
    {
        return ObtenUsuariosConfig(Variable, Valor.ToString());
    }

    /// <summary>
    /// Obtiene o establece el identificador de usuario que se usará para cargar o guardar
    /// la configuración
    /// </summary>
    public int USUARIO_ID
    {
        set { m_Usuario_ID = value; }
        get { return m_Usuario_ID; }
    }

    /// <summary>
    /// Obtiene o establece el tiempo en minitos en el que el sistema estará fuera entre cada sincronización 
    /// el tiempo predeterminado sera cada 5 minutos
    /// </summary>
    public int SyncTimeOut
    {
        get { return ObtenConfig(USUARIO_ID, "SyncTimeOut", 5); }
        set { GuardaConfig(USUARIO_ID, "SyncTimeOut", value); }
    }

    /// <summary>
    /// Obtiene o establece el Hash calculado del ultimo script de creación de tablas como eCMSSQL.Sql 
    /// para verificar si este a cambiado y se debe ejecutar nuevamente todo el archivo de script
    /// </summary>
    public static string HashQuiery_IniciaBase
    {
        get { return ObtenConfig(0, "HashQuiery_IniciaBase", ""); }
        set { GuardaConfig(0, "HashQuiery_IniciaBase", value); }
    }

    /// <summary>
    /// Obtiene o Establece la configuracion de RecalculaFInicial
    /// </summary>
    public static DateTime RecalculaFInicial
    {
        get { return ObtenConfig(0, "RecalculaFInicial", DateTime.Now); }
        set { GuardaConfig(0, "RecalculaFInicial", value); }
    }

    /// <summary>
    /// Obtiene o establece la configuracion de RecalculaFFinal
    /// </summary>
    public static DateTime RecalculaFFinal
    {
        get { return ObtenConfig(0, "RecalculaFFinal", DateTime.Now); }
        set { GuardaConfig(0, "RecalculaFFinal", value); }
    }

    /// <summary>
    /// Obtiene o establece la configuracion de Recalcula
    /// </summary>
    public static bool Recalcula
    {
        get { return ObtenConfig(0, "Recalcula", false); }
        set
        {
            GuardaConfig(0, "Recalcula", value);
            if (value == true)
                CeC_Asistencias.CambioRecalculaAccesos();
        }
    }

    /// <summary>
    /// Obtiene o establce la configuracion de CampoMail
    /// </summary>
    public static string CampoMail
    {
        get { return ObtenConfig(0, "CampoMail", "USUARIO_DESCRIPCION"); }
        set { GuardaConfig(0, "CampoMail", value); }
    }

    /// <summary>
    /// Obtiene o establece la configuracion de CampoGrupoSeleccionado
    /// </summary>
    public static int CampoGrupoSeleccionado
    {
        get { return ObtenConfig(0, "CampoGrupoSeleccionado", 2); }
        set { GuardaConfig(0, "CampoGrupoSeleccionado", value); }
    }

    /// <summary>
    /// Obtiene o establece la configuracion de NombreGrupo1 y actualiza el campo_etiqueta
    /// </summary>
    public static string NombreGrupo1
    {
        get { return ObtenConfig(0, "NombreGrupo1", "Centro de Costos"); }
        set
        {
            string Valor = value;
            if (Valor == "")
                Valor = "-";
            GuardaConfig(0, "NombreGrupo1", Valor);
            CeC_BD.EjecutaComando("UPDATE EC_CAMPOS SET CAMPO_ETIQUETA = '" + Valor + "' WHERE CAMPO_NOMBRE LIKE 'SUSCRIPCION_ID' OR CAMPO_NOMBRE LIKE 'SUSCRIPCION_NOMBRE'");
        }
    }

    /// <summary>
    /// Obtiene o establece la configuracion de NombreGrupo2 y actualiza el campo_etiqueta
    /// </summary>
    public static string NombreGrupo2
    {
        get { return ObtenConfig(0, "NombreGrupo2", "Grupo"); }
        set
        {
            string Valor = value;
            if (Valor == "")
                Valor = "-";
            GuardaConfig(0, "NombreGrupo2", Valor);
            CeC_BD.EjecutaComando("UPDATE EC_CAMPOS SET CAMPO_ETIQUETA = '" + Valor + "' WHERE CAMPO_NOMBRE LIKE 'GRUPO_2_ID' OR CAMPO_NOMBRE LIKE 'GRUPO_2_NOMBRE'");
        }
    }

    /// <summary>
    /// Obtiene o establece la configuracion de NombreGrupo3 y actualiza el campo_etiqueta
    /// </summary>
    public static string NombreGrupo3
    {
        get { return ObtenConfig(0, "NombreGrupo3", "Departamento"); }
        set
        {
            string Valor = value;
            if (Valor == "")
                Valor = "-";
            GuardaConfig(0, "NombreGrupo3", Valor);
            CeC_BD.EjecutaComando("UPDATE EC_CAMPOS SET CAMPO_ETIQUETA = '" + Valor + "' WHERE CAMPO_NOMBRE LIKE 'GRUPO_3_ID' OR CAMPO_NOMBRE LIKE 'GRUPO_3_NOMBRE'");
        }
    }


    public static bool AsignaGrupoACampo(string Campo, int Grupo)
    {
        if (Grupo < 1 || Grupo > 3)
            return false;

        int Punto = Campo.IndexOf(".");
        if (Punto > 0)
            Campo = Campo.Substring(Punto + 1);
        int Catalogo = Grupo + 1;
        CeC_BD.EjecutaComando("UPDATE EC_CAMPOS SET CATALOGO_ID = 0 WHERE CATALOGO_ID = " + Catalogo);
        CeC_BD.EjecutaComando("UPDATE EC_CAMPOS SET CATALOGO_ID = " + Catalogo + " WHERE CAMPO_NOMBRE = '" + Campo.ToUpper() + "'");
        return true;
    }

    /// <summary>
    /// Obtiene o establece el valor de configuracion de CampoGrupo1
    /// </summary>
    public static string CampoGrupo1
    {
        get { return ObtenConfig(0, "CampoGrupo1", "EC_PERSONAS_DATOS.CENTRO_DE_COSTOS"); }
        set
        {
            GuardaConfig(0, "CampoGrupo1", value);
            GuardaConfig(0, "CampoGrupo1Combo1", value);
            AsignaGrupoACampo(value, 1);
        }
    }
    /// <summary>
    /// Obtiene o establce el valor de configuracion de CampoGrupo2
    /// </summary>
    public static string CampoGrupo2
    {
        get { return ObtenConfig(0, "CampoGrupo2", "EC_PERSONAS_DATOS.GRUPO"); }
        set
        {
            GuardaConfig(0, "CampoGrupo2", value);
            GuardaConfig(0, "CampoGrupo2Combo2", value);
            AsignaGrupoACampo(value, 2);
        }
    }
    /// <summary>
    /// Obtiene o establece la configuracion de CampoGrupo2
    /// </summary>
    public static string CampoGrupo3
    {
        get { return ObtenConfig(0, "CampoGrupo3", "EC_PERSONAS_DATOS.DEPARTAMENTO"); }
        set
        {
            GuardaConfig(0, "CampoGrupo3", value);
            GuardaConfig(0, "CampoGrupo3Combo3", value);
            AsignaGrupoACampo(value, 3);
        }
    }
    /// <summary>
    /// Obtiene o establece el valor de configuracion de CampoGrupo1_Back 
    /// </summary>
    public static string CampoGrupo1_Back
    {
        get { return ObtenConfig(0, "CampoGrupo1_Back", ""); }
        set
        {
            GuardaConfig(0, "CampoGrupo1_Back", value);
        }
    }
    /// <summary>
    /// Obtiene o establce el valor de configuracion de CampoGrupo2_Back
    /// </summary>
    public static string CampoGrupo2_Back
    {
        get { return ObtenConfig(0, "CampoGrupo2_Back", ""); }
        set { GuardaConfig(0, "CampoGrupo2_Back", value); }
    }
    /// <summary>
    /// Obtiene o establece el valor de configuracion de CampoGrupo3_Back
    /// </summary>
    public static string CampoGrupo3_Back
    {
        get { return ObtenConfig(0, "CampoGrupo3_Back", ""); }
        set { GuardaConfig(0, "CampoGrupo3_Back", value); }
    }
    /// <summary>
    /// Obtiene o establece el valor de configuracion de UsaSicoss
    /// </summary>
    public bool UsaSicoss
    {
        get { return ObtenConfig(USUARIO_ID, "UsaSicoss", false); }
        set { GuardaConfig(USUARIO_ID, "UsaSicoss", value); }
    }
    /// <summary>
    /// Obtiene o establece el valor de configuracion de UsaSicoss
    /// </summary>
    public bool UsaNomipac
    {
        get { return ObtenConfig(USUARIO_ID, "UsaNomipac", false); }
        set { GuardaConfig(USUARIO_ID, "UsaNomipac", value); }
    }
    /// <summary>
    /// Obtiene o establece el valor de configuracion de UsaSicoss
    /// </summary>
    public bool UsaNOI
    {
        get { return ObtenConfig(USUARIO_ID, "UsaNOI", false); }
        set { GuardaConfig(USUARIO_ID, "UsaNOI", value); }
    }
    /// <summary>
    /// Obtiene o establece el valor de configuracion de UsaSicoss
    /// </summary>
    public bool UsaAdam
    {
        get { return ObtenConfig(USUARIO_ID, "UsaAdam", false); }
        set { GuardaConfig(USUARIO_ID, "UsaAdam", value); }
    }
    /// <summary>
    /// Obtiene o establece el valor de configuracion de UsaSicoss
    /// </summary>
    public bool UsaTerminales
    {
        get
        {
            if (USUARIO_ID == 1)
                return true;
            return ObtenConfig(USUARIO_ID, "UsaTerminales", false);
        }
        set { GuardaConfig(USUARIO_ID, "UsaTerminales", value); }
    }
    /// <summary>
    /// Obtiene o establece el valor de configuracion de UsaSicoss
    /// </summary>
    public bool UsaUsuarios
    {
        get
        {
            if (USUARIO_ID == 1)
                return true;
            return ObtenConfig(USUARIO_ID, "UsaUsuarios", false);
        }
        set { GuardaConfig(USUARIO_ID, "UsaUsuarios", value); }
    }
    /// <summary>
    /// Obtiene o establece el valor de Configuracion de CampoGrupo4_Back
    /// </summary>
    public static int CampoGrupo4Combo4
    {
        get { return ObtenConfig(0, "VACACIONES_ID", 0); }
        set { GuardaConfig(0, "VACACIONES_ID", value); }
    }
    /// <summary>
    /// Obtiene o establece el valor de configuracion de CampoGrupo4Combo4_Texto
    /// </summary>
    public static string CampoGrupo4Combo4_Texto
    {
        get { return ObtenConfig(0, "CampoGrupo4Combo4_Texto", ""); }
        set { GuardaConfig(0, "CampoGrupo4Combo4_Texto", value); }
    }
    /// <summary>
    /// Obtiene o establece el valor de configuracion de SyncTimeOutG
    /// </summary>
    public static int SyncTimeOutG
    {
        get { return ObtenConfig(0, "SyncTimeOut", 5); }
        set { GuardaConfig(0, "SyncTimeOut", value); }
    }
    //OBTIENE O ESTABLECE LAS VARIABLES DE CONFIGURACION
    /*Carpeta de Reportes PDF y XLS, Mensaje de Restriccion JSCript, 
     */
    /// <summary>
    /// Obtiene o establece el valor de configuracion de la rutareportesPDF
    /// </summary>
    public static string RutaReportesPDF
    {
        get { return ObtenConfig(0, "RutaReportesPDF", "PDF/"); }
        set { GuardaConfig(0, "RutaReportesPDF", value); }
    }
    /// <summary>
    /// Obtiene o establece el valor de configuracion de la rutareportesXLS
    /// </summary>
    public static string RutaReportesXLS
    {
        get { return ObtenConfig(0, "RutaReportesXLS", "XLS/"); }
        set { GuardaConfig(0, "RutaReportesXLS", value); }
    }
    /// <summary>
    /// Obtiene o establece el valor de configuracion de MensajeJScript
    /// </summary>
    public static string MensajeJScript
    {
        get { return ObtenConfig(0, "MensajeJScript", "No cuentas con los permisos necesarios para ver el contenido de esta pagina"); }
        set { GuardaConfig(0, "MensajeJScript", value); }
    }

    //OBTIENE O ESTABLECE LAS VARIABLES DE CONFIGURACION DE SMTP
    /*Servidor de Correo, Servidor SMTP, Servidor SMTPPuerto, ServidorSMTPNombreUsuario
     * Servidor SMTPPass
     */
    public static string SevidorCorreo
    {
        get { return ObtenConfig(0, "SevidorCorreo", "sstsst@prodigy.net.mx"); }
        set { GuardaConfig(0, "SevidorCorreo", value); }
    }
    /// <summary>
    /// Obtiene o establece el valor de configuracion del SevidorSMTP
    /// </summary>
    public static string SevidorSMTP
    {
        get { return ObtenConfig(0, "SevidorSMTP", "smtp.prodigy.net.mx"); }
        set { GuardaConfig(0, "SevidorSMTP", value); }
    }
    /// <summary>
    /// Obtiene o establece el valor de congifuracion de SevidorSMTPPuerto
    /// </summary>
    public static int SevidorSMTPPuerto
    {
        get { return ObtenConfig(0, "SevidorSMTPPuerto", 25); }
        set { GuardaConfig(0, "SevidorSMTPPuerto", value); }
    }
    /// <summary>
    /// Obtiene o establece el valor de configuracion del ServidorSMTPNombreUsuario
    /// </summary>
    public static string ServidorSMTPNombreUsuario
    {
        get { return ObtenConfig(0, "ServidorSMTPNombreUsuario", ""); }
        set { GuardaConfig(0, "ServidorSMTPNombreUsuario", value); }
    }
    /// <summary>
    /// Obtiene o establece el valor de configuracion de ServidorSMTPPass
    /// </summary>
    public static string ServidorSMTPPass
    {
        get { return ObtenConfig(0, "ServidorSMTPPass", ""); }
        set { GuardaConfig(0, "ServidorSMTPPass", value); }
    }
   
    /// <summary>
    /// Obtiene o establece el tiempo extra que se tendrá de tolerancia
    /// esto es adicional a la configurada en el turno 
    /// </summary>
    public static TimeSpan TiempoToleranciaExtraTS
    {
        get
        {
            return new TimeSpan(0, 0, ObtenConfig(0, "TiempoToleranciaExtra", 0));
        }
        set
        {
            GuardaConfig(0, "TiempoToleranciaExtra", Convert.ToInt32(value.TotalSeconds));
        }
    }

    /// <summary>
    /// Obtiene o establece el tiempo extra en segundos que se tendrá de tolerancia
    /// esto es adicional a la configurada en el turno 
    /// </summary>
    public static int TiempoToleranciaExtra
    {
        get
        {
            return ObtenConfig(0, "TiempoToleranciaExtra", 0);
        }
        set
        {
            GuardaConfig(0, "TiempoToleranciaExtra", value);
        }
    }
    /// <summary>
    /// Obtiene o establce el valor de configuracion de TERMINAL_NOMBRE_ENROLA
    /// </summary>
    public static int TERMINAL_NOMBRE_ENROLA
    {
        get { return ObtenConfig(0, "TERMINAL_NOMBRE_ENROLA", 0); }
        set { GuardaConfig(0, "TERMINAL_NOMBRE_ENROLA", value); }
    }


    /// <summary>
    /// Obtiene o establce el valor de configuracion de TERMINAL_NOMBRE_ENROLA
    /// </summary>
    public static int TERMINAL_ID_ENROLA
    {
        get { return ObtenConfig(0, "TERMINAL_ID_ENROLA", 0); }
        set { GuardaConfig(0, "TERMINAL_ID_ENROLA", value); }
    }
    /// <summary>
    /// Obtiene o establce el valor de configuracion de TERMINAL_PERSONA_NUEVA
    /// </summary>
    public static bool TERMINAL_PERSONA_NUEVA
    {
        get { return ObtenConfig(0, "TERMINAL_PERSONA_NUEVA", false); }
        set { GuardaConfig(0, "TERMINAL_PERSONA_NUEVA", value); }
    }
    /// <summary>
    /// Obtiene o establece el valor de configuracion de TERMINAL_NUEVA_ASIGNAPERSONA
    /// </summary>
    public static bool TERMINAL_NUEVA_ASIGNAPERSONA
    {
        get { return ObtenConfig(0, "TERMINAL_NUEVA_ASIGNAPERSONA", false); }
        set { GuardaConfig(0, "TERMINAL_NUEVA_ASIGNAPERSONA", value); }
    }
    ///<summary>
    ///Gurada los valores de Inicio del grid de empleados[Listado Completo]
    ///</summary>
    public static int PAGINA_GRID_EMPLEADOS
    {
        get { return ObtenConfig(0, "EMPLEADOS_GRID_PAGINA", 0); }
        set { GuardaConfig(0, "EMPLEADOS_GRID_PAGINA", value); }
    }
    /// <summary>
    /// Obtiene o establece los valores de configuracion de PAGINA_GRID_INCIDENCIAS_RASTREO_COMBOINDEX
    /// </summary>
    public static int PAGINA_GRID_INCIDENCIAS_RASTREO_COMBOINDEX
    {
        get { return ObtenConfig(0, "PAGINA_GRID_INCIDENCIAS_RASTREO_COMBOINDEX", 0); }
        set { GuardaConfig(0, "PAGINA_GRID_INCIDENCIAS_RASTREO_COMBOINDEX", value); }
    }
    /// <summary>
    /// Obtiene o establece los valores de configuracion de PAGINA_GRID_INCIDENCIAS_RASTREO_PAGEINDEX
    /// </summary>
    public static int PAGINA_GRID_INCIDENCIAS_RASTREO_PAGEINDEX
    {
        get { return ObtenConfig(0, "PAGINA_GRID_INCIDENCIAS_RASTREO_PAGEINDEX", 0); }
        set { GuardaConfig(0, "PAGINA_GRID_INCIDENCIAS_RASTREO_PAGEINDEX", value); }
    }
    /// <summary>
    /// Obtiene o establece los valores de configuracion de CAMPO_GRID_EMPLEADOS
    /// </summary>
    public static int CAMPO_GRID_EMPLEADOS
    {
        get { return ObtenConfig(0, "EMPLEADOS_GRID_CAMPO", 0); }
        set { GuardaConfig(0, "EMPLEADOS_GRID_CAMPO", value); }
    }
    /// <summary>
    /// Obtiene o establece los valores de configuracion de INCIDENCIAS_PERSONALIZADAS_VACACIONES_ID
    /// </summary>
    public static int INCIDENCIAS_PERSONALIZADAS_VACACIONES_ID
    {
        get { return ObtenConfig(0, "VACACIONES_ID", 0); }
        set { GuardaConfig(0, "VACACIONES_ID", value); }
    }

    /// <summary>
    /// Regresa true si se usa la fotografia en la aplicacion
    /// </summary>
    public static bool FotografiaActiva
    {
        get { return ObtenConfig(0, "FotografiaActiva", true); }
        set { GuardaConfig(0, "FotografiaActiva", value); }
    }
    /// <summary>
    /// Regresa true si se usa la firma en la aplicacion
    /// </summary>
    public static bool FirmaActiva
    {
        get { return ObtenConfig(0, "FirmaActiva", false); }
        set { GuardaConfig(0, "FirmaActiva", value); }
    }
    /// <summary>
    /// Regresa true si se usa la huella en la aplicacion
    /// </summary>
    public static bool HuellaActiva
    {
        get { return ObtenConfig(0, "HuellaActiva", false); }
        set { GuardaConfig(0, "HuellaActiva", value); }
    }
    /// <summary>
    /// Obtiene o establece el valor de TolerancioaTurnosExpress
    /// </summary>
    public int ToleranciaTurnosExpress
    {
        get { return ObtenConfig(0, "ToleranciaTurnosExpress", 15); }
        set { GuardaConfig(0, "ToleranciaTurnosExpress", value); }
    }
    /// <summary>
    /// Obtiene o establece el valor de ComidaTurnosExpress
    /// </summary>
    public int ComidaTurnosExpress
    {
        get { return ObtenConfig(0, "ComidaTurnosExpress", 60); }
        set { GuardaConfig(0, "ComidaTurnosExpress", value); }
    }
    /// <summary>
    /// Obtiene o establece el valor de PermiteCreditoComida
    /// </summary>
    public static bool PermiteCreditoComida
    {
        get { return ObtenConfig(0, "PermiteCreditoComida", false); }
        set { GuardaConfig(0, "PermiteCreditoComida", value); }
    }
    /// <summary>
    /// Obtiene o establece el valor de PermiteCreditoDesayuno
    /// </summary>
    public static bool PermiteCreditoDesayuno
    {
        get { return ObtenConfig(0, "PermiteCreditoDesayuno", false); }
        set { GuardaConfig(0, "PermiteCreditoDesayuno", value); }
    }
    /// <summary>
    /// Obtiene o establece el valor de PermiteComerDespues
    /// </summary>
    public static bool PermiteComerDespues
    {
        get { return ObtenConfig(0, "PermiteComerDespues", false); }
        set { GuardaConfig(0, "PermiteComerDespues", value); }
    }
    /// <summary>
    /// Obtiene o establece el valor de PermiteDesayunarDespues
    /// </summary>
    public static bool PermiteDesayunarDespues
    {
        get { return ObtenConfig(0, "PermiteDesayunarDespues", false); }
        set { GuardaConfig(0, "PermiteDesayunarDespues", value); }
    }
    /// <summary>
    /// Reservado para uso futuro, por el momento en el sincronizador
    /// se configurara este parametro (app.config)
    /// </summary>
    public static string RutaBD_Veritrax
    {
        get { return ObtenConfig(0, "RutaBD_Veritrax", ""); }
        set { GuardaConfig(0, "RutaBD_Veritrax", value); }
    }
    public static bool EsDemo
    {
        get { return ObtenConfig(0, "EsDemo", false); }
        set { GuardaConfig(0, "EsDemo", value); }
    }

    /// <summary>
    /// Obtiene o establece el nombre de la impresora que se usará para generar los reportes
    /// </summary>
    public static string ImpresoraNombre
    {
        get { return ObtenConfig(0, "ImpresoraNombre", ""); }
        set { GuardaConfig(0, "ImpresoraNombre", value); }
    }


    /// <summary>
    /// Se Guarda La imagen del Login    
    /// </summary>
    public static byte[] imglogin
    {
        get
        {
            try { return CeC_BD.ObtenerJPG("imglogin"); }
            catch { return ObtenConfigBin(0, "imglogin"); }
        }
        set
        {
            GuardaConfigBin(0, "imglogin", value);
            CeC_BD.GuardarJPG("imglogin", value);
        }
    }
    /// <summary>
    /// Se Guarda La imagen del Login    
    /// </summary>
    public static byte[] imgencabezado
    {
        get
        {
            try { return CeC_BD.ObtenerJPG("imgencabezado"); }
            catch { return ObtenConfigBin(0, "imgencabezado"); }
        }
        set
        {
            GuardaConfigBin(0, "imgencabezado", value);
            CeC_BD.GuardarJPG("imgencabezado", value);
        }
    }
    /// <summary>
    /// Se Guarda La imagen del Login    
    /// </summary>
    public static byte[] imgreporte
    {
        get
        {
            try { return CeC_BD.ObtenerJPG("imgreporte"); }
            catch { return ObtenConfigBin(0, "imgreporte"); }
        }
        set
        {
            GuardaConfigBin(0, "imgreporte", value);
            CeC_BD.GuardarJPG("imgreporte", value);
        }
    }
    /// <summary>
    /// El Campo Con el que se va a usar como identificador en noi
    /// </summary>
    public static string CampoLlaveNOI
    {
        get { return ObtenConfig(0, "PERSONA_LINK_ID", ""); }
        set { GuardaConfig(0, "PERSONA_LINK_ID", value); }
    }

    /// <summary>
    /// El campo indica si se usara el modulo de envio atumatico de Mails
    /// </summary>
    public static bool MODULO_ENVIO_AUTOMATICO_MAILS
    {
        get { return ObtenConfig(0, "CMd_Mails.Habilitado", false); }
        set { GuardaConfig(0, "CMd_Mails.Habilitado", value); }
    }

    public static string FechaExpira_ePrint
    {

        get { return ObtenConfig(0, "FechaExpira_ePrint", "2012-02-01"); }
        set { GuardaConfig(0, "FechaExpira_ePrint", value); }

    }

    public static string LinkURL
    {

        get { return ObtenConfig(0, "LinkURL", "http://web.eclock.com.mx/"); }
        set { GuardaConfig(0, "LinkURL", value); }

    }

    public static string LinkeClock
    {

        get { return ObtenConfig(0, "LinkURL", "http://www.eclock.com.mx/"); }
        set { GuardaConfig(0, "LinkURL", value); }

    }
    public static string LinkeClockComprar
    {

        get { return ObtenConfig(0, "LinkURL", "http://www.eclock.com.mx/"); }
        set { GuardaConfig(0, "LinkURL", value); }

    }
    public static string LinkeClockComentarios
    {

        get { return ObtenConfig(0, "LinkURL", "http://www.eclock.com.mx/"); }
        set { GuardaConfig(0, "LinkURL", value); }

    }
    public static string LinkVerComprarPuntos
    {

        get { return ObtenConfig(0, "LinkVerComprarPuntos", LinkURL + "WF_LogInBuy.aspx"); }
        set { GuardaConfig(0, "LinkVerComprarPuntos", value); }

    }

    /// <summary>
    /// Se indica a partir de cuando se van a contar los dias guardados en ENV_AUT_MAILS_DIAS
    /// </summary>
    /// 
    public static string LinkVerHistorial
    {

        get { return ObtenConfig(0, "LinkVerHistorial", LinkURL); }
        set { GuardaConfig(0, "LinkVerHistorial", value); }

    }

    public static string LinkAltaUsuario
    {

        get { return ObtenConfig(0, "LinkAltaUsuario", LinkURL); }
        set { GuardaConfig(0, "LinkAltaUsuario", value); }

    }

    public static string LinkVerSplash
    {

        get { return ObtenConfig(0, "LinkVerSplash", "Conozca las alternativas que EntryTec le ofrece para impresión local en www.entrytec.com.mx"); }
        set { GuardaConfig(0, "LinkVerSplash", value); }

    }
    public static string LinkPropaganda
    {

        get { return ObtenConfig(0, "LinkPropaganda", ""); }
        set { GuardaConfig(0, "LinkPropaganda", value); }

    }
    public static string LinkDescargaArchivos
    {

        get { return ObtenConfig(0, "LinkDescargaArchivos", LinkURL + "Disenos/"); }
        set { GuardaConfig(0, "LinkDescargaArchivos", value); }

    }
    public static string LinkPaginAyuda
    {

        get { return ObtenConfig(0, "LinkPaginAyuda", LinkURL); }
        set { GuardaConfig(0, "LinkPaginAyuda", value); }

    }
    public static string VersionePrint
    {

        get { return ObtenConfig(0, "VersionePrint", "2.5.1.0"); }
        set { GuardaConfig(0, "VersionePrint", value); }

    }
    /// <summary>
    /// Se indica a partir de cuando se van a contar los dias guardados en ENV_AUT_MAILS_DIAS
    /// </summary>
    public static DateTime ENV_AUT_MAILS_DESDE
    {
        get { return ObtenConfig(0, "ENV_AUT_MAILS_DESDE", DateTime.Now); }
        set { GuardaConfig(0, "ENV_AUT_MAILS_DESDE", value); }
    }
    /// <summary>
    /// Define cada cuantos dias se va a mandar el mail
    /// </summary>
    public static int ENV_AUT_MAILS_DIAS
    {
        get { return ObtenConfig(0, "ENV_AUT_MAILS_DIAS", 1); }
        set { GuardaConfig(0, "ENV_AUT_MAILS_DIAS", value); }
    }
    public static string ENV_AUT_MAILS_RUTA_TEMP
    {
        get { return ObtenConfig(0, "ENV_AUT_MAILS_RUTA_TEMP", ""); }
        set { GuardaConfig(0, "ENV_AUT_MAILS_RUTA_TEMP", value); }
    }



    public static int IncidenciaExIDHorasExtras
    {
        get { return ObtenConfig(0, "IncidenciaExIDHorasExtras", 0); }
        set { GuardaConfig(0, "IncidenciaExIDHorasExtras", value); }
    }

    //Indica si se mostrará el Wizard o Asistente por primera vez
    public static bool MostrarWizardInicio
    {
        get { return ObtenConfig(0, "MostrarWizardInicio", true); }
        set { GuardaConfig(0, "MostrarWizardInicio", value); }
    }
    public static string IncidenciasEmpleadosSQL
    {
        get { return ObtenConfig(0, "IncidenciasEmpleadosSQL", "SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_BORRADO = 0 AND TURNO_ID IN (SELECT TURNO_ID FROM EC_TURNOS where TURNO_ASISTENCIA = 1)"); }
        set { GuardaConfig(0, "IncidenciasEmpleadosSQL", value); }
    }

    private static object m_AsistenciaNoLaborable = null;
    /// <summary>
    /// Indica si se genera la asistencia en dias no laborables
    /// </summary>
    public static bool AsistenciaNoLaborable
    {
        get
        {
            if (m_AsistenciaNoLaborable == null)
                m_AsistenciaNoLaborable = ObtenConfig(0, "AsistenciaNoLaborable", false);
            return Convert.ToBoolean(m_AsistenciaNoLaborable);
        }
        set
        {
            GuardaConfig(0, "AsistenciaNoLaborable", value);
            m_AsistenciaNoLaborable = value;
        }
    }
    private static object m_AsistenciaMinutosLibres = null;
    public static int AsistenciaMinutosLibres
    {
        get
        {
            if (m_AsistenciaMinutosLibres == null)
            {
                int MinutosLibres = ObtenConfig(0, "AsistenciaMinutosLibres", 0);
                m_AsistenciaMinutosLibres = MinutosLibres;
                if (MinutosLibres <= 0)
                {
                    m_AsistenciaMinutosLibres = MinutosLibres;
                    GuardaConfig(0, "AsistenciaMinutosLibres", MinutosLibres);
                }
            }
            return Convert.ToInt32(m_AsistenciaMinutosLibres);
        }
        set
        {
            GuardaConfig(0, "AsistenciaMinutosLibres", value);
            m_AsistenciaMinutosLibres = value;
        }
    }

    private static object m_FestivoTrabajado = null;
    /// <summary>
    /// 0 indica que los festivos no serán nunca trabajados
    /// 1 indica que los festivos seran trabajados solo cuando contengan entrada y salida
    /// 2 indica que los festivos seran trabajados siempre que se tenga una solo checada
    /// </summary>
    public static int FestivoTrabajado
    {
        get
        {
            if (m_FestivoTrabajado == null)
            {
                int FT = ObtenConfig(0, "FestivoTrabajado", -1);
                m_FestivoTrabajado = FT;
                if (FT < 0)
                {
                    CMd_BahiaPrinc BP = new CMd_BahiaPrinc();
                    if (BP.Habilitado)
                        FT = 2;
                    else
                        FT = 1;
                    m_FestivoTrabajado = FT;
                    GuardaConfig(0, "FestivoTrabajado", FT);
                }
            }
            return Convert.ToInt32(m_FestivoTrabajado);
        }
        set
        {
            GuardaConfig(0, "FestivoTrabajado", value);
            m_FestivoTrabajado = value;
        }
    }

    public static string LinkNuevoUsuario
    {
        get
        {
            return ObtenConfig(0, "LinkNuevoUsuario", "http://www.eClock.com.mx");
        }
        set
        {
            GuardaConfig(0, "LinkNuevoUsuario", value);
        }
    }

    public static string LinkOlvidoClave
    {
        get
        {
            return ObtenConfig(0, "LinkOlvidoClave", "WF_NuevaClave.aspx");
        }
        set
        {
            GuardaConfig(0, "LinkOlvidoClave", value);
        }
    }

    private static object m_AlertaRetardosSupervisor = null;
    public static bool AlertaRetardosSupervisor
    {
        get
        {
            if (m_AlertaRetardosSupervisor == null)
                m_AlertaRetardosSupervisor = ObtenConfig(0, "AlertaRetardosSupervisor", false);
            return Convert.ToBoolean(m_AlertaRetardosSupervisor);
        }
        set
        {
            GuardaConfig(0, "AlertaRetardosSupervisor", value);
            m_AlertaRetardosSupervisor = value;
        }
    }

    private static object m_AlertaRetardosEmpleado = null;
    public static bool AlertaRetardosEmpleado
    {
        get
        {
            if (m_AlertaRetardosEmpleado == null)
                m_AlertaRetardosEmpleado = ObtenConfig(0, "AlertaRetardosEmpleado", false);
            return Convert.ToBoolean(m_AlertaRetardosEmpleado);
        }
        set
        {
            GuardaConfig(0, "AlertaRetardosEmpleado", value);
            m_AlertaRetardosEmpleado = value;
        }
    }

    private static object m_AlertaFaltasUDia = null;
    public static DateTime AlertaFaltasUDia
    {
        get
        {
            if (m_AlertaFaltasUDia == null)
                m_AlertaFaltasUDia = ObtenConfig(0, "AlertaFaltasUDia", new DateTime(2002, 09, 24));
            return Convert.ToDateTime(m_AlertaFaltasUDia);
        }
        set
        {
            GuardaConfig(0, "AlertaFaltasUDia", value);
            m_AlertaFaltasUDia = value;
        }
    }
    private static object m_AlertaFaltasSupervisor = null;
    public static bool AlertaFaltasSupervisor
    {
        get
        {
            if (m_AlertaFaltasSupervisor == null)
                m_AlertaFaltasSupervisor = ObtenConfig(0, "AlertaFaltasSupervisor", false);
            return Convert.ToBoolean(m_AlertaFaltasSupervisor);
        }
        set
        {
            GuardaConfig(0, "AlertaFaltasSupervisor", value);
            m_AlertaFaltasSupervisor = value;
        }
    }
    private static object m_AlertaFaltasEmpleado = null;
    public static bool AlertaFaltasEmpleado
    {
        get
        {
            if (m_AlertaFaltasEmpleado == null)
                m_AlertaFaltasEmpleado = ObtenConfig(0, "AlertaFaltasEmpleado", false);
            return Convert.ToBoolean(m_AlertaFaltasEmpleado);
        }
        set
        {
            GuardaConfig(0, "AlertaFaltasEmpleado", value);
            m_AlertaFaltasEmpleado = value;
        }
    }
    private static object m_AlertaTerminalFuera = null;
    public static bool AlertaTerminalFuera
    {
        get
        {
            if (m_AlertaTerminalFuera == null)
                m_AlertaTerminalFuera = ObtenConfig(0, "AlertaTerminalFuera", false);
            return Convert.ToBoolean(m_AlertaTerminalFuera);
        }
        set
        {
            GuardaConfig(0, "AlertaTerminalFuera", value);
            m_AlertaTerminalFuera = value;
        }
    }

    public bool EnviarMailFaltas
    {
        get { return ObtenConfig(USUARIO_ID, "EnviarMailFaltas", false); }
        set { GuardaConfig(USUARIO_ID, "EnviarMailFaltas", value); }
    }

    public bool EnviarMailRetardos
    {
        get { return ObtenConfig(USUARIO_ID, "EnviarMailRetardos", false); }
        set { GuardaConfig(USUARIO_ID, "EnviarMailRetardos", value); }
    }

    /// <summary>
    /// Envia un email cuando la terminal o checador no responde
    /// </summary>
    public bool EnviarMailTerminalNoResponde
    {
        get { return ObtenConfig(USUARIO_ID, "EnviarMailTerminalNoResponde", false); }
        set { GuardaConfig(USUARIO_ID, "EnviarMailTerminalNoResponde", value); }
    }

    /// <summary>
    /// Permite recibir mails de solicitudes de justificaciones o incidencias
    /// Se envia al momento
    /// </summary>
    public bool EnviarMailSolicitudIncidencia
    {
        get { return ObtenConfig(USUARIO_ID, "EnviarMailSolicitudIncidencia", false); }
        set { GuardaConfig(USUARIO_ID, "EnviarMailSolicitudIncidencia", value); }
    }

    public bool EnviaMailDiario
    {
        get { return ObtenConfig(USUARIO_ID, "EnviaMailDiario", false); }
        set { GuardaConfig(USUARIO_ID, "EnviaMailDiario", value); }
    }

    /// <summary>
    /// Muestra los días de la semana que se enviarán mail si esta vacio ("") significa que no se ejecutara, 
    /// Usar 1 para Domingo y 7 para sabado
    /// Separarlos por coma ",".
    /// Ej. 2,5 o 2 
    /// </summary>
    public string EnviaMailDiasSemana
    {
        get { return ObtenConfig(USUARIO_ID, "EnviaMailDiasSemana", ""); }
        set { GuardaConfig(USUARIO_ID, "EnviaMailDiasSemana", value); }
    }
    /// <summary>
    /// Muestra los días del mes que se enviara el mail de aviso si esta vacio ("") significa que no se ejecutara, 
    /// Usar el numero del día
    /// Ej. 1,16
    /// </summary>
    public string EnviaMailDiasMes
    {
        get { return ObtenConfig(USUARIO_ID, "EnviaMailDiasSemana", ""); }
        set { GuardaConfig(USUARIO_ID, "EnviaMailDiasSemana", value); }
    }

    /// <summary>
    /// solo guarda la ultima "fecha" del envio de Retardos
    /// </summary>
    public DateTime EnviarMailFaltasUltimaFecha
    {
        get { return ObtenConfig(USUARIO_ID, "EnviarMailFaltasUltimaFecha", DateTime.Today.AddMonths(-1)); }
        set { GuardaConfig(USUARIO_ID, "EnviarMailFaltasUltimaFecha", value.Date); }
    }
    /// <summary>
    /// solo guarda la ultima "fecha" del envio de Retardos
    /// </summary>
    public DateTime EnviarMailRetardosUltimaFecha
    {
        get { return ObtenConfig(USUARIO_ID, "EnviarMailRetardosUltimaFecha", DateTime.Today.AddMonths(-1)); }
        set { GuardaConfig(USUARIO_ID, "EnviarMailRetardosUltimaFecha", value.Date); }
    }
    public string AsistenciaFavoritos
    {
        get { return ObtenConfig(USUARIO_ID, "AsistenciaFavoritos", ""); }
        set { GuardaConfig(USUARIO_ID, "AsistenciaFavoritos", value); }
    }



    public string AsistenciaSoloRetardos
    {
        get { return ObtenConfig(USUARIO_ID, "AsistenciaSoloRetardos", "1100100111010100001"); }
        set { GuardaConfig(USUARIO_ID, "AsistenciaSoloRetardos", value); }
    }
    public string AsistenciaSoloFaltas
    {
        get { return ObtenConfig(USUARIO_ID, "AsistenciaSoloFaltas", "1100100011100100001"); }
        set { GuardaConfig(USUARIO_ID, "AsistenciaSoloFaltas", value); }
    }
    public string AsistenciaHEFavoritos
    {
        get { return ObtenConfig(USUARIO_ID, "AsistenciaHEFavoritos", ""); }
        set { GuardaConfig(USUARIO_ID, "AsistenciaHEFavoritos", value); }
    }
    public string AsistenciaHESimple
    {
        get { return ObtenConfig(USUARIO_ID, "AsistenciaHESimple", "0000000"); }
        set { GuardaConfig(USUARIO_ID, "AsistenciaHESimple", value); }
    }
    public string AsistenciaHESimpleGrupo
    {
        get { return ObtenConfig(USUARIO_ID, "AsistenciaHESimple", "0000001"); }
        set { GuardaConfig(USUARIO_ID, "AsistenciaHESimple", value); }
    }

    public string AsistenciaHEDetalle
    {
        get { return ObtenConfig(USUARIO_ID, "AsistenciaHESimple", "1111111"); }
        set { GuardaConfig(USUARIO_ID, "AsistenciaHESimple", value); }
    }
    public string AsistenciaHEDetalleGrupo
    {
        get { return ObtenConfig(USUARIO_ID, "AsistenciaHESimple", "1111111"); }
        set { GuardaConfig(USUARIO_ID, "AsistenciaHESimple", value); }
    }

    /// <summary>
    /// Indica cuantos empleados se mostraran por hoja cuando se requiere firma en el formato Predeterminado
    /// </summary>
    public int EmpleadosXHoja
    {
        get { return ObtenConfig(USUARIO_ID, "EmpleadosXHoja", 2); }
        set { GuardaConfig(USUARIO_ID, "EmpleadosXHoja", value); }
    }


    #region Configuración del sistema

    /// <summary>
    /// Obtiene la ID del Usuario, en caso de que no lo encuentre generara un autonumerico
    /// e insertara en la tabla esté
    /// </summary>
    /// <param name="USUARIO_ID">ID del Usuario</param>
    /// <param name="Variable"></param>
    /// <returns></returns>
    public static byte[] ObtenConfigBin(int USUARIO_ID, string Variable)
    {
        int CONFIG_USUARIO_ID = CeC_BD.EjecutaEscalarInt("SELECT CONFIG_USUARIO_ID FROM EC_CONFIG_USUARIO WHERE USUARIO_ID = " +
            USUARIO_ID.ToString() + " AND CONFIG_USUARIO_VARIABLE = '" + Variable + "'");
        if (CONFIG_USUARIO_ID < 0)
        {
            CONFIG_USUARIO_ID = CeC_Autonumerico.GeneraAutonumerico("EC_CONFIG_USUARIO", "CONFIG_USUARIO_ID");
            CeC_BD.EjecutaComando("INSERT INTO EC_CONFIG_USUARIO (CONFIG_USUARIO_ID, USUARIO_ID, CONFIG_USUARIO_VARIABLE) VALUES(" + CONFIG_USUARIO_ID.ToString() + "," + USUARIO_ID.ToString() + ",'" + Variable + "')");
        }
        return CeC_BD.ObtenBinario("EC_CONFIG_USUARIO", "CONFIG_USUARIO_ID", CONFIG_USUARIO_ID, "CONFIG_USUARIO_VALOR_BIN");
    }
    /// <summary>
    /// Obtiene una variable de configuración almacenada en la base de datos 
    /// </summary>
    public static string ObtenConfig(int USUARIO_ID, string Variable, string ValorDefecto)
    {
        try
        {
            object Valor = CeC_BD.EjecutaEscalar("SELECT CONFIG_USUARIO_VALOR FROM EC_CONFIG_USUARIO WHERE CONFIG_USUARIO_VARIABLE = '" + Variable + "' AND USUARIO_ID = " + USUARIO_ID.ToString());
            if (Valor == null)
                return ValorDefecto;
            return Convert.ToString(Valor);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
            return ValorDefecto;
        }
    }


    /// <summary>
    /// Guarda una variable de configuración en la base de datos del sistema de tipo entero
    /// </summary>
    public static bool GuardaConfigCrypt(int USUARIO_ID, string Variable, int Valor)
    {
        byte[] Datos = BitConverter.GetBytes(Valor);
        return GuardaConfig(USUARIO_ID, Variable, CeC.Bcd2String(Datos, 0, 4));
    }

    public static int ObtenConfigCrypt(int USUARIO_ID, string Variable, int ValorDefecto)
    {
        string Valor = ObtenConfig(USUARIO_ID, Variable, "");
        if (Valor.Length > 0)
        {
            try
            {
                int iValor = 0;
                byte[] Datos = CeC.String2Bcd(Valor);
                iValor = BitConverter.ToInt32(Datos, 0);
                return iValor;
            }
            catch { }
        }
        return ValorDefecto;
    }
    /// <summary>
    /// Obtiene una variable de configuracion del usuario almacenada en la base de datos, si no existe se regresa el valor por defecto
    /// </summary>
    /// <param name="USUARIO_ID">Id del Usuario</param>
    /// <param name="Variable"></param>
    /// <param name="ValorDefecto">Intervalo de Tiempo por defecto</param>
    /// <returns></returns>
    public static DateTime ObtenConfig(int USUARIO_ID, string Variable, DateTime ValorDefecto)
    {
        try
        {
            object Valor = CeC_BD.EjecutaEscalar("SELECT CONFIG_USUARIO_VALOR FROM EC_CONFIG_USUARIO WHERE CONFIG_USUARIO_VARIABLE = '" + Variable + "' AND USUARIO_ID = " + USUARIO_ID.ToString());
            if (Valor == null)
                return ValorDefecto;
            System.Globalization.CultureInfo CI = new System.Globalization.CultureInfo("es-MX");
            return Convert.ToDateTime(Valor, CI);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
            return ValorDefecto;
        }
    }
    /// <summary>
    /// Obtiene una variable de configuracion almacenada en la base de datos, si no existe se regresa el valor por defecto
    /// </summary>
    /// <param name="USUARIO_ID">Id del Usuario</param>
    /// <param name="Variable"></param>
    /// <param name="ValorDefecto"></param>
    /// <returns></returns>
    public static bool ObtenConfig(int USUARIO_ID, string Variable, bool ValorDefecto)
    {
        try
        {
            object Valor = CeC_BD.EjecutaEscalar("SELECT CONFIG_USUARIO_VALOR FROM EC_CONFIG_USUARIO WHERE CONFIG_USUARIO_VARIABLE = '" + Variable + "' AND USUARIO_ID = " + USUARIO_ID.ToString());
            if (Valor == null)
                return ValorDefecto;
            return Convert.ToBoolean(Valor);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
            return ValorDefecto;
        }
    }
    /// <summary>
    /// Obtiene una variable de configuración almacenada en la base de datos 
    /// </summary>
    public static int ObtenConfig(int USUARIO_ID, string Variable, int ValorDefecto)
    {
        return CeC.Convierte2Int(ObtenConfig(USUARIO_ID, Variable, ValorDefecto.ToString()), ValorDefecto);
    }
    /// <summary>
    /// Obtiene una variable de configuración (del sistema) almacenada en la base de datos 
    /// </summary>
    public static string ObtenConfigSistema(string Variable, string ValorDefecto)
    {
        return ObtenConfig(0, Variable, ValorDefecto);
    }

    /// <summary>
    /// Obtiene una variable de configuración (del sistema) almacenada en la base de datos de tipo entero
    /// </summary>
    public static int ObtenConfigSistema(string Variable, int ValorDefecto)
    {
        return Convert.ToInt32(ObtenConfigSistema(Variable, ValorDefecto.ToString()));
    }
    /// <summary>
    /// Actualiza en EC_CAMPOS el la ID del Catalogo y Nombre del Campo,
    /// sino se encuentra dicho campo agregara un nuevo campo a EC_CAMPOS,
    /// en otro caso generara un error
    /// </summary>
    /// <param name="Campo"></param>
    /// <param name="CatalogoID">ID del Catalog</param>
    /// <returns></returns>
    public static bool GuardaConfigGrupos(string Campo, int CatalogoID)
    {
        try
        {
            int Registros = CeC_BD.EjecutaComando("UPDATE EC_CAMPOS SET CATALOGO_ID = " + CatalogoID.ToString() + " WHERE CAMPO_NOMBRE = '" + Campo + "'");
            if (Registros <= 0)
            {
                Registros = CeC_BD.EjecutaComando("INSERT INTO EC_CAMPOS(CAMPO_NOMBRE,CAMPO_ETIQUETA,CATALOGO_ID,MASCARA_ID,TIPO_DATO_ID,CAMPO_LONGITUD,CAMPO_ANCHO_GRID,CAMPO_ALTO_GRID,CAMPO_ANCHO,CAMPO_ALTO,CAMPO_ES_TEMPLEADOS) VALUES(" + Campo + "," + Campo + "," + CatalogoID.ToString() + ",0,1,0,0,0,0,0,0)");
                if (Registros > 0)
                    return true;
            }
            else
                return true;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
            return false;
        }
        return false;
    }
    /// <summary>
    /// Guarda una variable de configuración en la base de datos del sistema
    /// </summary>
    public static bool GuardaConfig(int USUARIO_ID, string Variable, string Valor)
    {
        try
        {
            //Se reemplaza comilla simple por comilla doble para no tener problemas con el insert y update
            Valor = Valor.Replace("'", "''");
            int Registros = CeC_BD.EjecutaComando("UPDATE EC_CONFIG_USUARIO SET CONFIG_USUARIO_VALOR = '" + Valor + "' WHERE USUARIO_ID = " + USUARIO_ID.ToString() + " AND CONFIG_USUARIO_VARIABLE = '" + Variable + "'");
            if (Registros <= 0)
                Registros = CeC_BD.EjecutaComando("INSERT INTO EC_CONFIG_USUARIO (CONFIG_USUARIO_ID, USUARIO_ID, CONFIG_USUARIO_VARIABLE, CONFIG_USUARIO_VALOR) VALUES( " +
                    CeC_Autonumerico.GeneraAutonumerico("EC_CONFIG_USUARIO", "CONFIG_USUARIO_ID").ToString() + ", " + USUARIO_ID.ToString() + " , '" + Variable + "', '" + Valor + "')");
            if (Registros > 0)
                return true;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return false;
    }

    /*public static bool GuardaConfigBin(int USUARIO_ID, string Variable, string Valor)
    {
        try
        {
            int Registros = CeC_BD.EjecutaComando("UPDATE EC_CONFIG_USUARIO SET CONFIG_USUARIO_VALOR_BIN = '" + Valor + "' WHERE USUARIO_ID = " + USUARIO_ID.ToString() + " AND CONFIG_USUARIO_VARIABLE = '" + Variable + "'");
            if (Registros <= 0)
                Registros = CeC_BD.EjecutaComando("INSERT INTO EC_CONFIG_USUARIO (CONFIG_USUARIO_ID, USUARIO_ID, CONFIG_USUARIO_VARIABLE, CONFIG_USUARIO_VALOR_BIN) VALUES( " +
                        CeC_Autonumerico.GeneraAutonumerico("EC_CONFIG_USUARIO", "CONFIG_USUARIO_ID").ToString() + ", " + USUARIO_ID.ToString() + " , '" + Variable + "', '" + Valor + "')");
            if (Registros > 0)
                return true;
        }
        catch
        {

        }
        return false;
    }*/
    /// <summary>
    /// Guarda una variable de configuración en la base de datos del sistema de tipo entero
    /// </summary>
    public static bool GuardaConfig(int USUARIO_ID, string Variable, int Valor)
    {
        return GuardaConfig(USUARIO_ID, Variable, Valor.ToString());
    }
    /// <summary>
    /// Guarda una variable de configuracion en la base de datos del sistema de tipo booleano
    /// </summary>
    /// <param name="USUARIO_ID">Id del Usuario</param>
    /// <param name="Variable"></param>
    /// <param name="Valor"></param>
    /// <returns></returns>
    public static bool GuardaConfig(int USUARIO_ID, string Variable, bool Valor)
    {
        return GuardaConfig(USUARIO_ID, Variable, Convert.ToString(Valor));
    }
    /// <summary>
    ///  Guarda una variable de configuracion en la base de datos del sistema de tipo DateTime
    /// </summary>
    /// <param name="USUARIO_ID">Id del Usuario</param>
    /// <param name="Variable"></param>
    /// <param name="Valor"></param>
    /// <returns></returns>
    public static bool GuardaConfig(int USUARIO_ID, string Variable, DateTime Valor)
    {
        return GuardaConfig(USUARIO_ID, Variable, Valor.ToString("dd/MM/yyyy HH:mm:ss"));
    }

    /// <summary>
    /// Se obtiene el ID de Configuracion, si no existe se asigna uno y se guarda el arreglo de Bytes en la tabla
    /// </summary>
    /// <param name="USUARIO_ID">Id del Usuario</param>
    /// <param name="Variable"></param>
    /// <param name="Valor"></param>
    /// <returns></returns>
    public static bool GuardaConfigBin(int USUARIO_ID, string Variable, byte[] Valor)
    {
        int CONFIG_USUARIO_ID = CeC_BD.EjecutaEscalarInt("SELECT CONFIG_USUARIO_ID FROM EC_CONFIG_USUARIO WHERE USUARIO_ID = " +
            USUARIO_ID.ToString() + " AND CONFIG_USUARIO_VARIABLE = '" + Variable + "'");
        if (CONFIG_USUARIO_ID < 0)
        {
            CONFIG_USUARIO_ID = CeC_Autonumerico.GeneraAutonumerico("EC_CONFIG_USUARIO", "CONFIG_USUARIO_ID");
            CeC_BD.EjecutaComando("INSERT INTO EC_CONFIG_USUARIO (CONFIG_USUARIO_ID, USUARIO_ID, CONFIG_USUARIO_VARIABLE) VALUES(" + CONFIG_USUARIO_ID.ToString() + "," + USUARIO_ID.ToString() + ",'" + Variable + "')");
        }
        return CeC_BD.AsignaBinario("EC_CONFIG_USUARIO", "CONFIG_USUARIO_ID", CONFIG_USUARIO_ID, "CONFIG_USUARIO_VALOR_BIN", Valor);
    }
    #endregion


    /// <summary>
    /// Contiene el listado de turnos a mostrar en asignación de turnos
    /// </summary>
    public string TurnosFavoritos
    {
        get { return ObtenConfig(USUARIO_ID, "TurnosFavoritos", ""); }
        set { GuardaConfig(USUARIO_ID, "TurnosFavoritos", value); }
    }

    /// <summary>
    /// Indica si usará como tiempo adicional el tiempo anterior a la hora de entrada
    /// </summary>
    public static bool HorasExtras_Antes
    {
        get { return ObtenConfig(0, "HorasExtras_Antes", false); }
        set { GuardaConfig(0, "HorasExtras_Antes", value); }
    }

    /// <summary>
    /// Contiene el listado de turnos a mostrar en asignación de turnos
    /// </summary>
    public string Localizacion
    {
        get { return ObtenConfig(USUARIO_ID, "Localizacion", "es-MX"); }
        set { GuardaConfig(USUARIO_ID, "Localizacion", value); }
    }
    /// <summary>
    /// Determina si es TIP para el cálculo de horas extras (después de los 61 minutos)
    /// </summary>
    public static bool EsTIP
    {
        get { return ObtenConfig(0, "EsTIP", false); }
        set { GuardaConfig(0, "EsTIP", value); }
    }
    /// <summary>
    /// Obtiene los minutos que son necesarios para que una hora sea considerada como hora extra
    /// </summary>
    public static int MinutosParaHoraExtra
    {
        get { return ObtenConfig(0, "MinutosParaHoraExtra", 31); }
        set { GuardaConfig(0, "MinutosParaHoraExtra", value); }
    }
    /// <summary>
    /// Obtiene la terminal predeterminada para el calculo de accesos en la carga masiva de incidencias
    /// </summary>
    public static int TerminalID_CargaMasivaIncidencias
    {
        get { return ObtenConfig(0, "TerminalID_CargaMasivaIncidencias", 0); }
        set { GuardaConfig(0, "TerminalID_CargaMasivaIncidencias", value); }
    }
    /// <summary>
    /// Determina si es Miro para actualizar los turnos al sistema de nómina(Adam)
    /// </summary>
    public static bool EsMIRO
    {
        get { return ObtenConfig(0, "EsMIRO", false); }
        set { GuardaConfig(0, "EsMIRO", value); }
    }
    /// <summary>
    /// Tiempo en minutos que el sistema almacena los DataSet para su rápida consulta
    /// </summary>
    public static int MinutosGuardaDataSet
    {
        get { return ObtenConfig(0, "MinutosGuardaDataSet", 0); }
        set { GuardaConfig(0, "MinutosGuardaDataSet", value); }
    }
    /// <summary>
    /// Variable que configura si se muestran las terminales de asistencia
    /// </summary>
    public static string MostrarESTerminalesAsistencia
    {
        get { return ObtenConfig(0, "MostrarESTerminalesAsistencia", ""); }
        set { GuardaConfig(0, "MostrarESTerminalesAsistencia", value); }
    }
    /// <summary>
    /// Variable que indica si se generara un previo de los registros de las personas. (Solo en el caso que no se muestres en el reporte de Asistencia)
    /// </summary>
    public static bool GeneraPrevioPersonasDiario
    {
        get { return ObtenConfig(0, "GeneraPrevioPersonasDiario", false); }
        set { GuardaConfig(0, "GeneraPrevioPersonasDiario", value); }
    }
}

