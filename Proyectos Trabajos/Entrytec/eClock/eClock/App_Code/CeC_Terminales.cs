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
/// Descripción breve de CeC_Terminales
/// </summary>
public class CeC_Terminales : CeC_Tabla
{
    int m_Terminal_Id = 0;
    [Description("Identificador unico de terminales dadas de alta en el sistema")]
    [DisplayNameAttribute("Terminal_Id")]
    public int Terminal_Id { get { return m_Terminal_Id; } set { m_Terminal_Id = value; } }
    int m_Tipo_Terminal_Acceso_Id = 0;
    [Description("Indica el tipo de acceso que genera la terminal, entrada, salida o desconocido")]
    [DisplayNameAttribute("Tipo_Terminal_Acceso_Id")]
    public int Tipo_Terminal_Acceso_Id { get { return m_Tipo_Terminal_Acceso_Id; } set { m_Tipo_Terminal_Acceso_Id = value; } }
    string m_Terminal_Nombre = "";
    [Description("Nombre de la teminal (sirve para ubicar el equipo en las instalaciones)")]
    [DisplayNameAttribute("Terminal_Nombre")]
    public string Terminal_Nombre { get { return m_Terminal_Nombre; } set { m_Terminal_Nombre = value; } }
    int m_Almacen_Vec_Id = 0;
    [Description("")]
    [DisplayNameAttribute("Almacen_Vec_Id")]
    public int Almacen_Vec_Id { get { return m_Almacen_Vec_Id; } set { m_Almacen_Vec_Id = value; } }
    int m_Sitio_Id = 0;
    [Description("")]
    [DisplayNameAttribute("Sitio_Id")]
    public int Sitio_Id { get { return m_Sitio_Id; } set { m_Sitio_Id = value; } }
    int m_Modelo_Terminal_Id = 0;
    [Description("Indica el modelo de terminal usada ej. EtherTrax")]
    [DisplayNameAttribute("Modelo_Terminal_Id")]
    public int Modelo_Terminal_Id { get { return m_Modelo_Terminal_Id; } set { m_Modelo_Terminal_Id = value; } }
    int m_Tipo_Tecnologia_Id = 0;
    [Description("Indica la tecnología principal ej Huella")]
    [DisplayNameAttribute("Tipo_Tecnologia_Id")]
    public int Tipo_Tecnologia_Id { get { return m_Tipo_Tecnologia_Id; } set { m_Tipo_Tecnologia_Id = value; } }
    int m_Tipo_Tecnologia_Add_Id = 0;
    [Description("Indica si hay una tecnología adicional ej. Tarjeta y Huella")]
    [DisplayNameAttribute("Tipo_Tecnologia_Add_Id")]
    public int Tipo_Tecnologia_Add_Id { get { return m_Tipo_Tecnologia_Add_Id; } set { m_Tipo_Tecnologia_Add_Id = value; } }
    int m_Terminal_Sincronizacion = 0;
    [Description("RFU Cantidad en segundos que indica cada cuando se realizara la sincronizacion, si se coloca cero no sera automática dicha sincronización (en la versión 1.X siempre deberá tener un valor mayor a 5), por el momento se usara una Variable del sistema llamada SyncTimeOut")]
    [DisplayNameAttribute("Terminal_Sincronizacion")]
    public int Terminal_Sincronizacion { get { return m_Terminal_Sincronizacion; } set { m_Terminal_Sincronizacion = value; } }
    string m_Terminal_Campo_Llave = "";
    [Description("Contiene el nombre de la TABLA.CAMPO que contiene los datos del identificador único, ej. TABLA.TRACVE")]
    [DisplayNameAttribute("Terminal_Campo_Llave")]
    public string Terminal_Campo_Llave { get { return m_Terminal_Campo_Llave; } set { m_Terminal_Campo_Llave = value; } }
    string m_Terminal_Campo_Adicional = "";
    [Description("Contiene el nombre de la TABLA.CAMPO que contiene los datos adicionales ej. TABLA.NS (el no. de tarjeta mifare)")]
    [DisplayNameAttribute("Terminal_Campo_Adicional")]
    public string Terminal_Campo_Adicional { get { return m_Terminal_Campo_Adicional; } set { m_Terminal_Campo_Adicional = value; } }
    bool m_Terminal_Enrola = true;
    [Description("Indica si es terminal de enrolamiente")]
    [DisplayNameAttribute("Terminal_Enrola")]
    public bool Terminal_Enrola { get { return m_Terminal_Enrola; } set { m_Terminal_Enrola = value; } }
    string m_Terminal_Dir = "";
    [Description("Dirección IP de la terminal o URL")]
    [DisplayNameAttribute("Terminal_Dir")]
    public string Terminal_Dir { get { return m_Terminal_Dir; } set { m_Terminal_Dir = value; } }
    bool m_Terminal_Asistencia = true;
    [Description("Indica si las checadas en esta terminal serviran para generar asistencia")]
    [DisplayNameAttribute("Terminal_Asistencia")]
    public bool Terminal_Asistencia { get { return m_Terminal_Asistencia; } set { m_Terminal_Asistencia = value; } }
    bool m_Terminal_Comida = false;
    [Description("Indica si las checadas en esta terminal serviran para el reguistro y/o cobro de comidas")]
    [DisplayNameAttribute("Terminal_Comida")]
    public bool Terminal_Comida { get { return m_Terminal_Comida; } set { m_Terminal_Comida = value; } }
    int m_Terminal_Minutos_Dif = 0;
    [Description("Cuantos minutos hay de diferencia entre la hora del servidor y la hora de la terminal")]
    [DisplayNameAttribute("Terminal_Minutos_Dif")]
    public int Terminal_Minutos_Dif { get { return m_Terminal_Minutos_Dif; } set { m_Terminal_Minutos_Dif = value; } }
    bool m_Terminal_Validahorarioe = false;
    [Description("Indica si la terminal no permitirá entrar si esta fuera de horario de entrada  ")]
    [DisplayNameAttribute("Terminal_Validahorarioe")]
    public bool Terminal_Validahorarioe { get { return m_Terminal_Validahorarioe; } set { m_Terminal_Validahorarioe = value; } }
    bool m_Terminal_Validahorarios = false;
    [Description("Indica si la terminal no permitirá salir si su salida es menor o mayor a la permitida")]
    [DisplayNameAttribute("Terminal_Validahorarios")]
    public bool Terminal_Validahorarios { get { return m_Terminal_Validahorarios; } set { m_Terminal_Validahorarios = value; } }
    bool m_Terminal_Borrado = false;
    [Description("Indica si ha sido borrado el registro")]
    [DisplayNameAttribute("Terminal_Borrado")]
    public bool Terminal_Borrado { get { return m_Terminal_Borrado; } set { m_Terminal_Borrado = value; } }
    string m_Terminal_Descripcion = "";
    [Description("Descripcion de la terminal")]
    [DisplayNameAttribute("Terminal_Descripcion")]
    public string Terminal_Descripcion { get { return m_Terminal_Descripcion; } set { m_Terminal_Descripcion = value; } }
    byte[] m_Terminal_Bin = { 0 };
    [Description("Imagen donde se encuentra localizada la terminal")]
    [DisplayNameAttribute("Terminal_Bin")]
    public byte[] Terminal_Bin { get { return m_Terminal_Bin; } set { m_Terminal_Bin = value; } }
    string m_Terminal_Modelo = "";
    [Description("Nombre del modelo que le asigna el fabircante")]
    [DisplayNameAttribute("Terminal_Modelo")]
    public string Terminal_Modelo { get { return m_Terminal_Modelo; } set { m_Terminal_Modelo = value; } }
    string m_Terminal_No_Serie = "";
    [Description("Número de serie de la Terminal")]
    [DisplayNameAttribute("Terminal_No_Serie")]
    public string Terminal_No_Serie { get { return m_Terminal_No_Serie; } set { m_Terminal_No_Serie = value; } }
    string m_Terminal_Firmware_Ver = "";
    [Description("Versión del Firmware instalado en la Terminal")]
    [DisplayNameAttribute("Terminal_Firmware_Ver")]
    public string Terminal_Firmware_Ver { get { return m_Terminal_Firmware_Ver; } set { m_Terminal_Firmware_Ver = value; } }
    int m_Terminal_No_Huellas = 0;
    [Description("Cantidad de registros de huellas que la Terminal es capaz de almacenar.")]
    [DisplayNameAttribute("Terminal_No_Huellas")]
    public int Terminal_No_Huellas { get { return m_Terminal_No_Huellas; } set { m_Terminal_No_Huellas = value; } }
    int m_Terminal_No_Empleados = 0;
    [Description("Cantidad de registros de empleados que la Terminal es capaz de almacenar.")]
    [DisplayNameAttribute("Terminal_No_Empleados")]
    public int Terminal_No_Empleados { get { return m_Terminal_No_Empleados; } set { m_Terminal_No_Empleados = value; } }
    int m_Terminal_No_Tarjetas = 0;
    [Description("Cantidad de registros de tarjetas que la Terminal es capaz de almacenar.")]
    [DisplayNameAttribute("Terminal_No_Tarjetas")]
    public int Terminal_No_Tarjetas { get { return m_Terminal_No_Tarjetas; } set { m_Terminal_No_Tarjetas = value; } }
    int m_Terminal_No_Rostros = 0;
    [Description("Cantidad de registros de rostros que la Terminal es capaz de almacenar.")]
    [DisplayNameAttribute("Terminal_No_Rostros")]
    public int Terminal_No_Rostros { get { return m_Terminal_No_Rostros; } set { m_Terminal_No_Rostros = value; } }
    int m_Terminal_No_Checadas = 0;
    [Description("Cantidad de registros de checadas que la Terminal es capaz de almacenar.")]
    [DisplayNameAttribute("Terminal_No_Checadas")]
    public int Terminal_No_Checadas { get { return m_Terminal_No_Checadas; } set { m_Terminal_No_Checadas = value; } }
    int m_Terminal_No_Palmas = 0;
    [Description("Cantidad de registros de palmas que la Terminal es capaz de almacenar.")]
    [DisplayNameAttribute("Terminal_No_Palmas")]
    public int Terminal_No_Palmas { get { return m_Terminal_No_Palmas; } set { m_Terminal_No_Palmas = value; } }
    int m_Terminal_No_Iris = 0;
    [Description("Cantidad de registros de iris que la Terminal es capaz de almacenar.")]
    [DisplayNameAttribute("Terminal_No_Iris")]
    public int Terminal_No_Iris { get { return m_Terminal_No_Iris; } set { m_Terminal_No_Iris = value; } }
    DateTime m_Terminal_Garantia_Inicio = CeC_BD.FechaNula;
    [Description("Fecha de Inicio de la Garantia de la Terminal.")]
    [DisplayNameAttribute("Terminal_Garantia_Inicio")]
    public DateTime Terminal_Garantia_Inicio { get { return m_Terminal_Garantia_Inicio; } set { m_Terminal_Garantia_Inicio = value; } }
    DateTime m_Terminal_Garantia_Fin = CeC_BD.FechaNula;
    [Description("Fecha de Expiración de la Garantia de la Terminal.")]
    [DisplayNameAttribute("Terminal_Garantia_Fin")]
    public DateTime Terminal_Garantia_Fin { get { return m_Terminal_Garantia_Fin; } set { m_Terminal_Garantia_Fin = value; } }
    string m_Terminal_Agrupacion = "";
    [Description("Agrupación a la que pertenece la Terminal")]
    [DisplayNameAttribute("Terminal_Agrupacion")]
    public string Terminal_Agrupacion { get { return m_Terminal_Agrupacion; } set { m_Terminal_Agrupacion = value; } }

    public CeC_Terminales(CeC_Sesion Sesion)
        : base("EC_TERMINALES", "TERMINAL_ID", Sesion)
    {

    }
    public CeC_Terminales(int TerminalID, CeC_Sesion Sesion)
        : base("EC_TERMINALES", "TERMINAL_ID", Sesion)
    {
        Carga(TerminalID.ToString(), Sesion);
    }

    /// <summary>
    /// Agrega o actualiza los datos de la terminal
    /// </summary>
    /// <param name="TerminalId">Identificador unico de terminales dadas de alta en el sistema</param>
    /// <param name="TipoTerminalAccesoId">Indica el tipo de acceso que genera la terminal, entrada, salida o desconocido</param>
    /// <param name="TerminalNombre">Nombre de la teminal (sirve para ubicar el equipo en las instalaciones)</param>
    /// <param name="AlmacenVecId"></param>
    /// <param name="SitioId"></param>
    /// <param name="ModeloTerminalId">Indica el modelo de terminal usada ej. EtherTrax</param>
    /// <param name="TipoTecnologiaId">Indica la tecnología principal ej Huella</param>
    /// <param name="TipoTecnologiaAddId">Indica si hay una tecnología adicional ej. Tarjeta y Huella</param>
    /// <param name="TerminalSincronizacion">RFU Cantidad en segundos que indica cada cuando se realizara la sincronizacion, si se coloca cero no sera automática dicha sincronización (en la versión 1.X siempre deberá tener un valor mayor a 5), por el momento se usara una Variable del sistema llamada SyncTimeOut</param>
    /// <param name="TerminalCampoLlave">Contiene el nombre de la TABLA.CAMPO que contiene los datos del identificador único, ej. TABLA.TRACVE</param>
    /// <param name="TerminalCampoAdicional">Contiene el nombre de la TABLA.CAMPO que contiene los datos adicionales ej. TABLA.NS (el no. de tarjeta mifare)</param>
    /// <param name="TerminalEnrola">Indica si es terminal de enrolamiente</param>
    /// <param name="TerminalDir">Dirección IP de la terminal o URL</param>
    /// <param name="TerminalAsistencia">Indica si las checadas en esta terminal serviran para generar asistencia</param>
    /// <param name="TerminalComida">Indica si las checadas en esta terminal serviran para el reguistro y/o cobro de comidas</param>
    /// <param name="TerminalMinutosDif">Cuantos minutos hay de diferencia entre la hora del servidor y la hora de la terminal</param>
    /// <param name="TerminalValidahorarioe">Indica si la terminal no permitirá entrar si esta fuera de horario de entrada  </param>
    /// <param name="TerminalValidahorarios">Indica si la terminal no permitirá salir si su salida es menor o mayor a la permitida</param>
    /// <param name="TerminalBorrado">Indica si ha sido borrado el registro</param>
    /// <param name="TerminalDescripcion">Descripcion de la terminal</param>
    /// <param name="TerminalBin">Imagen donde se encuentra localizada la terminal</param>
    /// <param name="TerminalModelo">Nombre del modelo que le asigna el fabircante</param>
    /// <param name="TerminalNoSerie">Número de serie de la Terminal</param>
    /// <param name="TerminalFirmwareVer">Versión del Firmware instalado en la Terminal</param>
    /// <param name="TerminalNoHuellas">Cantidad de registros de huellas que la Terminal es capaz de almacenar.</param>
    /// <param name="TerminalNoEmpleados">Cantidad de registros de empleados que la Terminal es capaz de almacenar.</param>
    /// <param name="TerminalNoTarjetas">Cantidad de registros de tarjetas que la Terminal es capaz de almacenar.</param>
    /// <param name="TerminalNoRostros">Cantidad de registros de rostros que la Terminal es capaz de almacenar.</param>
    /// <param name="TerminalNoChecadas">Cantidad de registros de checadas que la Terminal es capaz de almacenar.</param>
    /// <param name="TerminalNoPalmas">Cantidad de registros de palmas que la Terminal es capaz de almacenar.</param>
    /// <param name="TerminalNoIris">Cantidad de registros de iris que la Terminal es capaz de almacenar.</param>
    /// <param name="TerminalGarantiaInicio">Fecha de Inicio de la Garantia de la Terminal.</param>
    /// <param name="TerminalGarantiaFin">Fecha de Expiración de la Garantia de la Terminal.</param>
    /// <param name="TerminalAgrupacion">Agrupación a la que pertenece la Terminal</param>
    /// <param name="Sesion">Sesión actual en el sistema</param>
    /// <returns>Vedadero si se pudo guardar correctamente los datos. Falso en caso de que ocurra algún problema o error al guardar los datos</returns>
    public bool Actualiza(int TerminalId, int TipoTerminalAccesoId, string TerminalNombre, int AlmacenVecId, int SitioId, int ModeloTerminalId, int TipoTecnologiaId, int TipoTecnologiaAddId, int TerminalSincronizacion, string TerminalCampoLlave, string TerminalCampoAdicional, bool TerminalEnrola, string TerminalDir, bool TerminalAsistencia, bool TerminalComida, int TerminalMinutosDif, bool TerminalValidahorarioe, bool TerminalValidahorarios, bool TerminalBorrado, string TerminalDescripcion, byte[] TerminalBin, string TerminalModelo, string TerminalNoSerie, string TerminalFirmwareVer, int TerminalNoHuellas, int TerminalNoEmpleados, int TerminalNoTarjetas, int TerminalNoRostros, int TerminalNoChecadas, int TerminalNoPalmas, int TerminalNoIris, DateTime TerminalGarantiaInicio, DateTime TerminalGarantiaFin, string TerminalAgrupacion,
CeC_Sesion Sesion)
    {
        try
        {
            bool Nuevo = false;
            if (!Carga(TerminalId.ToString(), Sesion))
                Nuevo = true;
            m_EsNuevo = Nuevo;
            Terminal_Id = TerminalId;
            Tipo_Terminal_Acceso_Id = TipoTerminalAccesoId;
            Terminal_Nombre = TerminalNombre;
            Almacen_Vec_Id = AlmacenVecId;
            Sitio_Id = SitioId;
            Modelo_Terminal_Id = ModeloTerminalId;
            Tipo_Tecnologia_Id = TipoTecnologiaId;
            Tipo_Tecnologia_Add_Id = TipoTecnologiaAddId;
            Terminal_Sincronizacion = TerminalSincronizacion;
            Terminal_Campo_Llave = TerminalCampoLlave;
            Terminal_Campo_Adicional = TerminalCampoAdicional;
            Terminal_Enrola = TerminalEnrola;
            Terminal_Dir = TerminalDir;
            Terminal_Asistencia = TerminalAsistencia;
            Terminal_Comida = TerminalComida;
            Terminal_Minutos_Dif = TerminalMinutosDif;
            Terminal_Validahorarios = TerminalValidahorarios;
            Terminal_Borrado = TerminalBorrado;
            Terminal_Descripcion = TerminalDescripcion;
            Terminal_Bin = TerminalBin;
            Terminal_Modelo = TerminalModelo;
            Terminal_No_Serie = TerminalNoSerie;
            Terminal_Firmware_Ver = TerminalFirmwareVer;
            Terminal_No_Huellas = TerminalNoHuellas;
            Terminal_No_Empleados = TerminalNoEmpleados;
            Terminal_No_Tarjetas = TerminalNoTarjetas;
            Terminal_No_Rostros = TerminalNoRostros;
            Terminal_No_Checadas = TerminalNoChecadas;
            Terminal_No_Palmas = TerminalNoPalmas;
            Terminal_No_Iris = TerminalNoIris;
            Terminal_Garantia_Inicio = TerminalGarantiaInicio;
            Terminal_Garantia_Fin = TerminalGarantiaFin;
            Terminal_Agrupacion = TerminalAgrupacion;
            if (Guarda(Sesion))
            {
                return true;
            }
        }
        catch { }
        return false;
    }

    /// <summary>
    /// Obtiene el usuario que es controlador de una suscripcion
    /// </summary>
    /// <param name="TerminalID">ID de la Terminal</param>
    /// <returns>ID del Usuario que es controlador de la suscripción</returns>
    public static int ObtenUsuarioSuscripcionID(int TerminalID)
    {
        return CeC_BD.EjecutaEscalarInt("SELECT MIN(USUARIO_ID)AS USUARIO_ID FROM EC_USUARIOS WHERE SUSCRIPCION_ID IN (SUSCRIPCION_ID FROM EC_AUTONUM WHERE AUTONUM_TABLA = 'EC_TERMINALES' AND AUTONUM_TABLA_ID = " + TerminalID + " )");
    }

    public static bool ActualizaCom(int TerminalID, CeC_Terminales_DExtras.Tipo_Term_DEXTRAS Tipo)
    {
        switch (Tipo)
        {
            case CeC_Terminales_DExtras.Tipo_Term_DEXTRAS.Conexion_Correcta:
                return ActualizaCom(TerminalID, "TERMINAL_COM_CONEXIONOK");
            case CeC_Terminales_DExtras.Tipo_Term_DEXTRAS.Error_Conexion:
                return ActualizaCom(TerminalID, "TERMINAL_COM_CONEXIONNOK");
            case CeC_Terminales_DExtras.Tipo_Term_DEXTRAS.ComunicacionCorrecta:
                return ActualizaCom(TerminalID, "TERMINAL_COM_COMUNICAOK");
            case CeC_Terminales_DExtras.Tipo_Term_DEXTRAS.Error_Comunicacion:
                return ActualizaCom(TerminalID, "TERMINAL_COM_COMUNICANOK");
            case CeC_Terminales_DExtras.Tipo_Term_DEXTRAS.FechaHora_Enviada:
                return ActualizaCom(TerminalID, "TERMINAL_COM_FECHAHORAOK");
            case CeC_Terminales_DExtras.Tipo_Term_DEXTRAS.FechaHora_Error:
                return ActualizaCom(TerminalID, "TERMINAL_COM_FECHAHORANOK");
            case CeC_Terminales_DExtras.Tipo_Term_DEXTRAS.Checadas_Descargadas:
                return ActualizaCom(TerminalID, "TERMINAL_COM_CHECADASOK");
            case CeC_Terminales_DExtras.Tipo_Term_DEXTRAS.Checadas_Error:
                return ActualizaCom(TerminalID, "TERMINAL_COM_CHECADASNOK");
            case CeC_Terminales_DExtras.Tipo_Term_DEXTRAS.Vectores_Descargados:
                return ActualizaCom(TerminalID, "TERMINAL_COM_NVECTORESOK");
            case CeC_Terminales_DExtras.Tipo_Term_DEXTRAS.Vectores_Error_Desc:
                return ActualizaCom(TerminalID, "TERMINAL_COM_NVECTORESNOK");
            case CeC_Terminales_DExtras.Tipo_Term_DEXTRAS.Vectores_Enviados:
                return ActualizaCom(TerminalID, "TERMINAL_COM_VECTORESOK");
            case CeC_Terminales_DExtras.Tipo_Term_DEXTRAS.Vectores_Error_Env:
                return ActualizaCom(TerminalID, "TERMINAL_COM_VECTORESNOK");

        }
        return false;
    }
    public static bool ActualizaCom(int TerminalID, string Campo)
    {
        if (CeC_BD.EjecutaEscalarInt("SELECT TERMINAL_ID FROM EC_TERMINALES_COM WHERE TERMINAL_ID = " + TerminalID) <= 0)
            CeC_BD.EjecutaComando("INSERT INTO EC_TERMINALES_COM (TERMINAL_ID) VALUES (" + TerminalID + ")");
        if (CeC_BD.EjecutaComando("UPDATE EC_TERMINALES_COM SET " + Campo + "=" + CeC_BD.SqlFechaHora(DateTime.Now) + " WHERE TERMINAL_ID=" + TerminalID) > 0)
            return true;
        return false;
    }

    public static int PermisoDaTodos(int Terminal_ID, int Suscripcion_ID)
    {
        CeC_BD.EjecutaComando("INSERT INTO EC_PERSONAS_TERMINALES (PERSONA_ID, TERMINAL_ID) SELECT PERSONA_ID," + Terminal_ID + " FROM EC_PERSONAS WHERE SUSCRIPCION_ID = " + Suscripcion_ID + " AND PERSONA_ID NOT IN (SELECT PERSONA_ID FROM EC_PERSONAS_TERMINALES WHERE TERMINAL_ID=" + Terminal_ID + ")");
        return CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_TERMINALES SET PERSONA_TERMINAL_BORRADO = 0, PERSONA_TERMINAL_B_FH = " + CeC_BD.SqlFechaHora(DateTime.Now) + "  WHERE TERMINAL_ID=" + Terminal_ID + " AND PERSONA_ID IN (SELECT PERSONA_ID FROM EC_PERSONAS WHERE SUSCRIPCION_ID = " + Suscripcion_ID + " AND PERSONA_BORRADO = 0)");

    }
    /// <summary>
    /// Quita el permiso de Acceso en la Terminal a todas las Personas de la Suscripcion
    /// </summary>
    /// <param name="Terminal_ID">ID de la Terminal</param>
    /// <param name="Suscripcion_ID">ID de la Suscripción</param>
    /// <returns>Verdadero si se quitaron correctamente los permisos. Falso en otro caso</returns>
    public static int PermisoQuitaTodos(int Terminal_ID, int Suscripcion_ID)
    {
        // Antes se usaba
        //return CeC_BD.EjecutaComando("DELETE EC_PERSONAS_TERMINALES WHERE TERMINAL_ID=" + Terminal_ID + " AND PERSONA_ID IN (SELECT PERSONA_ID FROM EC_PERSONAS WHERE SUSCRIPCION_ID = " + Suscripcion_ID + ")");
        return CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_TERMINALES SET PERSONA_TERMINAL_BORRADO = 1, PERSONA_TERMINAL_B_FH = " + CeC_BD.SqlFechaHora(DateTime.Now) + "  WHERE TERMINAL_ID=" + Terminal_ID + " AND PERSONA_ID IN (SELECT PERSONA_ID FROM EC_PERSONAS WHERE SUSCRIPCION_ID = " + Suscripcion_ID + ")");
    }
    /// <summary>
    /// Asigna a la Persona el permiso de Acceso a la Terminal
    /// </summary>
    /// <param name="Persona_ID">ID de la Persona</param>
    /// <param name="Terminal_ID">ID de la Terminal</param>
    /// <returns>Verdadero si se asigno el permiso. Falso en otro caso</returns>
    public static bool PermisoDa(int Persona_ID, int Terminal_ID)
    {
        CeC_BD.EjecutaEscalarInt("UPDATE EC_PERSONAS_TERMINALES SET PERSONA_TERMINAL_BORRADO = 0, PERSONA_TERMINAL_B_FH = " + CeC_BD.SqlFechaHora(DateTime.Now) + " WHERE TERMINAL_ID=" + Terminal_ID + " AND PERSONA_ID =" + Persona_ID);

        if (CeC_BD.EjecutaEscalarInt("SELECT COUNT(TERMINAL_ID)FROM EC_PERSONAS_TERMINALES WHERE TERMINAL_ID=" + Terminal_ID + " AND PERSONA_ID =" + Persona_ID) > 0)
            return true;
        if (CeC_BD.EjecutaComando(" INSERT INTO EC_PERSONAS_TERMINALES (PERSONA_ID, TERMINAL_ID, PERSONA_TERMINAL_UPDATE, PERSONA_TERMINAL_BORRADO) " +
                                    " VALUES(" + Persona_ID + "," + Terminal_ID + "," + CeC_BD.SqlFechaHora(System.DateTime.Now) + ", 0" + ")") < 1)
            return false;
        return true;
    }
    /// <summary>
    /// Quita a la Persona el permiso de Acceso a la Terminal
    /// </summary>
    /// <param name="Persona_ID">ID de la Persona</param>
    /// <param name="Terminal_ID">ID de la Terminal</param>
    /// <returns>Verdadero si se borro el permiso. Falso en otro caso</returns>
    public static bool PermisoQuita(int Persona_ID, int Terminal_ID)
    {

        if (CeC_BD.EjecutaEscalarInt("UPDATE EC_PERSONAS_TERMINALES SET PERSONA_TERMINAL_BORRADO = 1, PERSONA_TERMINAL_B_FH = " + CeC_BD.SqlFechaHora(DateTime.Now) + " WHERE TERMINAL_ID=" + Terminal_ID + " AND PERSONA_ID =" + Persona_ID) > 0)
            return true;
        return false;
    }
    public static DataSet ObtenTerminalesCatalogo(CeC_Sesion Sesion)
    {
        return ObtenTerminalesCatalogo(Sesion.SUSCRIPCION_ID);
    }

    public static int ObtenNoPersonasXTransmitir(int SUSCRIPCION_ID, bool MostrarBorrados)
    {
        string ValidaSuscripcion = "";
        string Borrados = "";
        if (!MostrarBorrados)
            Borrados = " AND TERMINAL_BORRADO = 0 ";
        if (SUSCRIPCION_ID > 1)
        {
            ValidaSuscripcion = " AND EC_TERMINALES." + CeC_Autonumerico.ValidaSuscripcion("EC_TERMINALES", "TERMINAL_ID", SUSCRIPCION_ID);
        }
        string Qry = "SELECT     COUNT(*) " +
                    "FROM         EC_TERMINALES,EC_PERSONAS_TERMINALES,EC_V_TERMINAL_CID_PERSONA WHERE  " +
                    "EC_PERSONAS_TERMINALES.PERSONA_ID = EC_V_TERMINAL_CID_PERSONA.PERSONA_ID   " +
                    "AND  " +
                    "EC_PERSONAS_TERMINALES.TERMINAL_ID = EC_TERMINALES.TERMINAL_ID AND  " +
                    "EC_TERMINALES.TERMINAL_CAMPO_LLAVE = EC_V_TERMINAL_CID_PERSONA.TERMINAL_CAMPO_LLAVE AND  " +
                    "EC_PERSONAS_TERMINALES.PERSONA_ID  NOT IN (SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_BORRADO = 1) " +
                    "AND  " +
                    "EC_PERSONAS_TERMINALES.PERSONA_TERMINAL_UPDATE IS NULL AND TERMINAL_DIR <> 'USB::0'  " +
                    Borrados + ValidaSuscripcion;
        /*
        string Qry = "SELECT COUNT(*) " +
                "FROM EC_TERMINALES INNER JOIN " +
                "EC_SITIOS ON EC_TERMINALES.SITIO_ID = EC_SITIOS.SITIO_ID INNER JOIN " +
                "EC_PERSONAS_TERMINALES ON EC_TERMINALES.TERMINAL_ID = EC_PERSONAS_TERMINALES.TERMINAL_ID " +
                "WHERE (EC_PERSONAS_TERMINALES.PERSONA_TERMINAL_UPDATE IS NULL) AND PERSONA_ID NOT IN (SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_BORRADO = 1) " +
                 Borrados + ValidaSuscripcion ;*/

        return CeC_BD.EjecutaEscalarInt(Qry);
    }
    public static string ObtenCampoLlave(int TerminalID)
    {
        string CampoLlave = CeC_BD.EjecutaEscalarString("SELECT TERMINAL_CAMPO_LLAVE FROM EC_TERMINALES WHERE TERMINAL_ID = " + TerminalID.ToString() + "");
        return CampoLlave;
    }
    public static string ObtenCampoLlaveAdicional(int TerminalID)
    {
        string CampoLlaveAdicional = CeC_BD.EjecutaEscalarString("SELECT TERMINAL_CAMPO_ADICIONAL FROM EC_TERMINALES WHERE TERMINAL_ID = " + TerminalID.ToString() + "");
        return CampoLlaveAdicional;
    }
    public static bool AsignaCampoLlaveAdicional(int TerminalID, int PersonaID, string ValorCampoAdicional, int SesionID)
    {
        string CampoLlaveAdicional = ObtenCampoLlaveAdicional(TerminalID);
        if (CampoLlaveAdicional == "")
            return false;
        return CeC_Personas.GuardaValor(PersonaID, CampoLlaveAdicional, ValorCampoAdicional, SesionID);
    }
    public static bool AsignaCampoLlave(int TerminalID, int PersonaID, string ValorCampo, int SesionID)
    {
        string CampoLlave = ObtenCampoLlave(TerminalID);
        if (CampoLlave == "")
            return false;
        return CeC_Personas.GuardaValor(PersonaID, CampoLlave, ValorCampo, SesionID);
    }
    public static DataSet ObtenTerminales(int SUSCRIPCION_ID, bool MostrarBorrados)
    {
        string ValidaSuscripcion = "";
        string Borrados = "";
        if (!MostrarBorrados)
            Borrados = " AND TERMINAL_BORRADO = 0 ";
        if (SUSCRIPCION_ID > 1)
        {
            ValidaSuscripcion = " AND " + CeC_Autonumerico.ValidaSuscripcion("EC_TERMINALES", "TERMINAL_ID", SUSCRIPCION_ID);
        }
        string Qry = "SELECT  TERMINAL_ID, TERMINAL_NOMBRE, SITIO_NOMBRE, NO_PERSONAS, NO_PERSONAS_POS, NO_PERSONAS_ENV, NO_PERSONAS_HUELLA, Conexion_Correcta, Error_Conexion, ComunicacionCorrecta, Error_Comunicacion, " +
                      "Log_Comunicacion, FechaHora_Enviada, FechaHora_Error, Checadas_Descargadas, Checadas_Error, Vectores_Descargados, Vectores_Error_Desc, " +
                      "Vectores_Enviados, Vectores_Error_Env, TIPO_TECNOLOGIA_NOMBRE, TERMINAL_CAMPO_LLAVE, TIPO_TECNOLOGIA_NOMBRE_ADD, " +
                      "TERMINAL_CAMPO_ADICIONAL, TERMINAL_DIR " +
"FROM  EC_V_TERMINALES_EDO " +
"WHERE   1=1  " + Borrados + ValidaSuscripcion +
"ORDER BY TERMINAL_NOMBRE";

        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }

    public static DataSet ObtenTerminalesCatalogo(int SUSCRIPCION_ID)
    {
        return (DataSet)CeC_BD.EjecutaDataSet("SELECT        TERMINAL_ID, TERMINAL_NOMBRE FROM            EC_TERMINALES INNER JOIN " +
                         "EC_AUTONUM ON EC_TERMINALES.TERMINAL_ID = EC_AUTONUM.AUTONUM_TABLA_ID " +
"WHERE        EC_TERMINALES.TERMINAL_BORRADO = 0  AND (EC_AUTONUM.AUTONUM_TABLA = 'EC_TERMINALES') AND (EC_AUTONUM.SUSCRIPCION_ID = " + SUSCRIPCION_ID + ")" +
" ORDER BY TERMINAL_NOMBRE");
    }

    /// <summary>
    /// Obtiene un catalogo de las Agrupaciones de las Terminales
    /// </summary>
    /// <param name="SUSCRIPCION_ID">Identificador de la Suscripción</param>
    /// <returns></returns>
    public static DataSet ObtenTerminalesAgrupacion(int SUSCRIPCION_ID)
    {
        return (DataSet)CeC_BD.EjecutaDataSet(" SELECT DISTINCT(TERMINAL_AGRUPACION)  " +
                                              " FROM EC_TERMINALES INNER JOIN " +
                                                   " EC_AUTONUM ON EC_TERMINALES.TERMINAL_ID = EC_AUTONUM.AUTONUM_TABLA_ID " +
                                              " WHERE EC_TERMINALES.TERMINAL_BORRADO = 0  " +
                                              " AND (EC_AUTONUM.AUTONUM_TABLA = 'EC_TERMINALES') " +
                                              " AND (EC_AUTONUM.SUSCRIPCION_ID = " + SUSCRIPCION_ID + ")" +
                                              " ORDER BY TERMINAL_AGRUPACION ");
    }

    public static DataSet ObtenPersonasDS(int Terminal_ID, int SUSCRIPCION_ID)
    {
        string Qry = " SELECT     EC_PERSONAS.PERSONA_ID, EC_PERSONAS.PERSONA_LINK_ID, EC_PERSONAS.PERSONA_NOMBRE, AGRUPACION_NOMBRE,  " +
                            " (SELECT PERSONA_TERMINAL_UPDATE " +
                            " FROM EC_PERSONAS_TERMINALES  " +
                            " WHERE TERMINAL_ID = " + Terminal_ID +
                            " AND PERSONA_ID = EC_PERSONAS.PERSONA_ID) AS PERSONA_TERMINAL_UPDATE, " +
                            " (SELECT PERSONA_ID FROM EC_PERSONAS_TERMINALES  " +
                            " WHERE TERMINAL_ID = " + Terminal_ID +
                            " AND PERSONA_ID = EC_PERSONAS.PERSONA_ID AND EC_PERSONAS_TERMINALES.PERSONA_TERMINAL_BORRADO = 0) AS TIENE_ACCESO " +
                        " FROM  EC_PERSONAS " +
                        " WHERE PERSONA_BORRADO = 0 AND SUSCRIPCION_ID = " + SUSCRIPCION_ID +
                        " ORDER BY PERSONA_NOMBRE";
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }
    /// <summary>
    /// Obtiene un listado de las Terminales asignadas a la Persona
    /// </summary>
    /// <param name="Persona_ID">ID de la Persona</param>
    /// <param name="Suscripcion_ID">ID de la Suscripción</param>
    /// <returns>DataSet con los datos de las Terminales asignadas a la Persona</returns>
    public static DataSet ObtenPersonaTerminalDS(int Persona_ID, int Suscripcion_ID)
    {
        string Qry = "";
        Qry = " SELECT TERMINAL_ID, TERMINAL_NOMBRE, SITIO_NOMBRE, " +
                    " (SELECT PERSONA_TERMINAL_UPDATE " +
                    " FROM EC_PERSONAS_TERMINALES " +
                    " WHERE TERMINAL_ID = EC_TERMINALES.TERMINAL_ID AND PERSONA_ID = " + Persona_ID + ") AS PERSONA_TERMINAL_UPDATE, " +
                    " (SELECT (CASE WHEN PERSONA_TERMINAL_BORRADO IS NULL OR PERSONA_TERMINAL_BORRADO = 0 THEN 1 ELSE 0 END) " +
                    " FROM EC_PERSONAS_TERMINALES " +
                    " WHERE TERMINAL_ID = EC_TERMINALES.TERMINAL_ID AND PERSONA_ID = " + Persona_ID + ") AS ACTIVO " +
                " FROM EC_TERMINALES, EC_SITIOS ";
        Qry += " WHERE TERMINAL_BORRADO = 0  AND EC_TERMINALES.SITIO_ID = EC_SITIOS.SITIO_ID " +
                " AND " + CeC_Autonumerico.ValidaSuscripcion("EC_TERMINALES", "TERMINAL_ID", Suscripcion_ID);
        Qry += " ORDER BY TERMINAL_NOMBRE";
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }

    /// <summary>
    /// Obtiene la ID de la Terminal almacenada en la base de datos
    /// </summary>
    /// <param name="Terminal_ID">Id del Terminal</param>
    /// <returns></returns>
    public static string ObtenTerminalNombre(int Terminal_ID)
    {
        string Qry = "SELECT TERMINAL_NOMBRE FROM EC_TERMINALES WHERE   TERMINAL_ID = " + Terminal_ID.ToString() + "";
        return CeC_BD.EjecutaEscalarString(Qry);
    }
    /// <summary>
    /// Obtiene la ID de la Terminal almacenada en la base de datos con cierto Nombre
    /// </summary>
    /// <param name="Terminal_Nombre">Nombre de la Terminal</param>
    /// <returns></returns>
    public static int ObtenTerminalID(string Terminal_Nombre)
    {
        string Qry = "SELECT TERMINAL_ID FROM EC_TERMINALES WHERE   TERMINAL_NOMBRE LIKE '" + Terminal_Nombre + "'";
        return CeC_BD.EjecutaEscalarInt(Qry);
    }


    public enum tipo
    {
        USB,
        Serial,
        RS485,
        Modem,
        Red
    }

    public tipo TipoConexion;
    public int Puerto = 0;
    public string Direccion = "";
    public int NoTerminal = 0;
    public int Velocidad = 0;
    /// <summary>
    /// Recibe los datos de la direccion y para cada tipo regresa una cadena de direccion diferente con los datos
    /// Tipo de Direccion, Nombre, Puerto, Velocidad, ID, Telefono separados por dos puntos
    /// En Tipo de Direccion se usara: 1:USB 2:Serial 3:485 4:modem 5:Red
    /// Nombre, Telefono y Direccion se gardan en la variable direccion
    /// <returns>Cadena Concatenada</returns>

    public string ObtenCadenaConexion()
    {
        string cadenafinal = "";
        switch (TipoConexion)
        {
            case tipo.USB: cadenafinal = "USB" + ":" + Direccion + ":" + NoTerminal.ToString();
                break;
            case tipo.Serial: cadenafinal = "Serial" + ":" + Puerto.ToString() + ":" + Velocidad.ToString() + ":" + NoTerminal.ToString();
                break;
            case tipo.RS485: cadenafinal = "RS485" + ":" + Puerto.ToString() + ":" + Velocidad.ToString() + ":" + NoTerminal.ToString();
                break;
            case tipo.Modem: cadenafinal = "Modem" + ":" + Direccion + ":" + Puerto.ToString() + ":" + Velocidad.ToString() + ":" + NoTerminal.ToString();
                break;
            case tipo.Red: cadenafinal = "Red" + ":" + Direccion + ":" + Puerto.ToString() + ":" + NoTerminal.ToString();
                break;
        }
        return cadenafinal;
    }
    /// <summary>
    /// Recibe la cadena concatenada y guarda los valores segun sea USB, RS485,MODEM,RED
    /// </summary>
    /// <param name="concatenada"></param>
    /// <returns></returns>
    public bool CargarCadenaConexion(string concatenada)
    {
        concatenada = concatenada.Trim();
        string[] ConjuntoDatos = concatenada.Split(new char[] { ':' });
        //        TipoConexion = Convert.ToInt32(ConjuntoDatos[0]);
        try
        {
            switch (ConjuntoDatos[0])
            {

                case "USB":
                    {
                        TipoConexion = tipo.USB;
                        Direccion = ConjuntoDatos[1];
                        Puerto = -1;
                        NoTerminal = 0;
                        Velocidad = 0;
                        if (ConjuntoDatos.Length > 2)
                            NoTerminal = Convert.ToInt32(ConjuntoDatos[2]);
                    }
                    break;
                case "Serial":
                    {
                        TipoConexion = tipo.Serial;
                        Puerto = Convert.ToInt32(ConjuntoDatos[1]);
                        Velocidad = Convert.ToInt32(ConjuntoDatos[2]);
                        Direccion = "";
                        NoTerminal = 0;
                        if (ConjuntoDatos.Length > 3)
                            NoTerminal = Convert.ToInt32(ConjuntoDatos[3]);

                    }
                    break;
                case "RS485":
                    {
                        TipoConexion = tipo.RS485;
                        Puerto = Convert.ToInt32(ConjuntoDatos[1]);
                        Velocidad = Convert.ToInt32(ConjuntoDatos[2]);
                        NoTerminal = Convert.ToInt32(ConjuntoDatos[3]);
                        Direccion = "";
                    }
                    break;
                case "Modem":
                    {
                        TipoConexion = tipo.Modem;
                        Direccion = ConjuntoDatos[1];

                        Puerto = Convert.ToInt32(ConjuntoDatos[2]);
                        Velocidad = Convert.ToInt32(ConjuntoDatos[3]);
                        NoTerminal = 0;
                        if (ConjuntoDatos.Length > 4)
                            NoTerminal = Convert.ToInt32(ConjuntoDatos[4]);

                    }
                    break;
                case "Red":
                    {
                        TipoConexion = tipo.Red;
                        Direccion = ConjuntoDatos[1];
#if eClockSYNC
                        try
                        {
                            IPAddress.Parse(Direccion);
                        }
                        catch{
                            try
                            {
                                IPHostEntry ipEntry = Dns.GetHostByName(Direccion);
                                IPAddress[] addr = ipEntry.AddressList;

                                for (int i = 0; i < addr.Length; i++)
                                {
                                    Direccion = addr[i].ToString();
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                                CIsLog2.AgregaError(ex);
                            }
                        }
                        
#endif
                        Puerto = Convert.ToInt32(ConjuntoDatos[2]);
                        NoTerminal = 0;
                        Velocidad = -1;
                        if (ConjuntoDatos.Length > 3)
                            NoTerminal = Convert.ToInt32(ConjuntoDatos[3]);

                    }
                    break;
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    //
    // TODO: Agregar aquí la lógica del constructor
    //
#if !eClockSYNC
    public static bool GeneraControladora(int TerminalID, string Nombre, int borrado)
    {
        CeC_Terminales_Param TP = new CeC_Terminales_Param(TerminalID);
        string sentencia;
        if (TP.SITIO_HIJO_ID > 0)
        {
            sentencia = "UPDATE eC_SITIOS SET SITIO_NOMBRE='" + Nombre + "', SITIO_BORRADO= " + borrado + " WHERE SITIO_ID = " + TP.SITIO_HIJO_ID;
        }
        else
        {

            int SitioID = CeC_Autonumerico.GeneraAutonumerico("EC_SITIOS", "SITIO_ID");
            sentencia = "INSERT INTO eC_SITIOS (SITIO_ID, SITIO_NOMBRE, SITIO_CONSULTA, SITIO_SEGUNDOS_SYNC, SITIO_BORRADO) VALUES (" + SitioID.ToString() + ", '" + Nombre + "','SELECT PERSONA_ID FROM EC_PERSONAS_TERMINALES WHERE TERMINAL_ID = " + TerminalID.ToString() + "', 1200, 0)";
            TP.SITIO_HIJO_ID = SitioID;

        }
        return Convert.ToBoolean(CeC_BD.EjecutaComando(sentencia));

    }
    public static bool EsControladora(int TerminalID)
    {
        CeC_Terminales_Param TP = new CeC_Terminales_Param(TerminalID);
        return Convert.ToBoolean(TP.SITIO_HIJO_ID);
    }
#endif

    public static int ObtenSuscripcionID(int Terminal_ID)
    {
        return CeC_Autonumerico.ObtenSuscripcionID("EC_TERMINALES", Terminal_ID);
    }
}






