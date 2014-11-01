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
/// Descripción breve de CeC_Suscripcion
/// </summary>
public class CeC_Suscripcion : CeC_Tabla
{
    int m_Suscripcion_Id = 0;
    [Description("Identificador único de la Suscripcion")]
    [DisplayNameAttribute("Suscripcion_Id")]
    public int Suscripcion_Id { get { return m_Suscripcion_Id; } set { m_Suscripcion_Id = value; } }
    int m_Edo_Suscripcion_Id = 0;
    [Description("Identificador del Estado de la Suscripción")]
    [DisplayNameAttribute("Edo_Suscripcion_Id")]
    public int Edo_Suscripcion_Id { get { return m_Edo_Suscripcion_Id; } set { m_Edo_Suscripcion_Id = value; } }
    int m_Suscrip_Precio_Id = 0;
    [Description("Identificador único del precio")]
    [DisplayNameAttribute("Suscrip_Precio_Id")]
    public int Suscrip_Precio_Id { get { return m_Suscrip_Precio_Id; } set { m_Suscrip_Precio_Id = value; } }
    string m_Suscripcion_Nombre = "";
    [Description("Contiene la razon social del propietario de la suscripcion")]
    [DisplayNameAttribute("Suscripcion_Nombre")]
    public string Suscripcion_Nombre { get { return m_Suscripcion_Nombre; } set { m_Suscripcion_Nombre = value; } }
    bool m_Suscripcion_Borrado = false;
    [Description("Indica si el cliente requiere Factura")]
    [DisplayNameAttribute("Suscripcion_Borrado")]
    public bool Suscripcion_Borrado { get { return m_Suscripcion_Borrado; } set { m_Suscripcion_Borrado = value; } }
    string m_Suscripcion_Razon = "";
    [Description("Contiene la razon social del propietario de la suscripcion")]
    [DisplayNameAttribute("Suscripcion_Razon")]
    public string Suscripcion_Razon { get { return m_Suscripcion_Razon; } set { m_Suscripcion_Razon = value; } }
    string m_Suscripcion_Rfc = "";
    [Description("RFC")]
    [DisplayNameAttribute("Suscripcion_Rfc")]
    public string Suscripcion_Rfc { get { return m_Suscripcion_Rfc; } set { m_Suscripcion_Rfc = value; } }
    string m_Suscripcion_Direccion1 = "";
    [Description("Campo uno de Dirección")]
    [DisplayNameAttribute("Suscripcion_Direccion1")]
    public string Suscripcion_Direccion1 { get { return m_Suscripcion_Direccion1; } set { m_Suscripcion_Direccion1 = value; } }
    string m_Suscripcion_Direccion2 = "";
    [Description("Campo dos de Dirección")]
    [DisplayNameAttribute("Suscripcion_Direccion2")]
    public string Suscripcion_Direccion2 { get { return m_Suscripcion_Direccion2; } set { m_Suscripcion_Direccion2 = value; } }
    string m_Suscripcion_Ciudad = "";
    [Description("Campo para dato de Ciudad")]
    [DisplayNameAttribute("Suscripcion_Ciudad")]
    public string Suscripcion_Ciudad { get { return m_Suscripcion_Ciudad; } set { m_Suscripcion_Ciudad = value; } }
    string m_Suscripcion_Estado = "";
    [Description("Campo para dato de Estado")]
    [DisplayNameAttribute("Suscripcion_Estado")]
    public string Suscripcion_Estado { get { return m_Suscripcion_Estado; } set { m_Suscripcion_Estado = value; } }
    string m_Suscripcion_Pais = "";
    [Description("Campo para dato de Pais")]
    [DisplayNameAttribute("Suscripcion_Pais")]
    public string Suscripcion_Pais { get { return m_Suscripcion_Pais; } set { m_Suscripcion_Pais = value; } }
    bool m_Suscripcion_Facturar = true;
    [Description("Indica si el cliente requiere Factura")]
    [DisplayNameAttribute("Suscripcion_Facturar")]
    public bool Suscripcion_Facturar { get { return m_Suscripcion_Facturar; } set { m_Suscripcion_Facturar = value; } }
    DateTime m_Suscripcion_Contratacion = CeC_BD.FechaNula;
    [Description("Contiene la fecha de contratación")]
    [DisplayNameAttribute("Suscripcion_Contratacion")]
    public DateTime Suscripcion_Contratacion { get { return m_Suscripcion_Contratacion; } set { m_Suscripcion_Contratacion = value; } }
    int m_Suscripcion_Empleados = 0;
    [Description("Contiene la cantidad de Empleados que se permitiran en esta suscripcion")]
    [DisplayNameAttribute("Suscripcion_Empleados")]
    public int Suscripcion_Empleados { get { return m_Suscripcion_Empleados; } set { m_Suscripcion_Empleados = value; } }
    int m_Suscripcion_Terminales = 0;
    [Description("Contiene la cantidad de Terminales que se permitiran en esta suscripcion")]
    [DisplayNameAttribute("Suscripcion_Terminales")]
    public int Suscripcion_Terminales { get { return m_Suscripcion_Terminales; } set { m_Suscripcion_Terminales = value; } }
    int m_Suscripcion_Usuarios = 0;
    [Description("Contiene la cantidad de Usuarios que se permitiran en esta suscripcion")]
    [DisplayNameAttribute("Suscripcion_Usuarios")]
    public int Suscripcion_Usuarios { get { return m_Suscripcion_Usuarios; } set { m_Suscripcion_Usuarios = value; } }
    int m_Suscripcion_Alumnos = 0;
    [Description("Contiene la cantidad de Alumnos que se permitiran en esta suscripcion")]
    [DisplayNameAttribute("Suscripcion_Alumnos")]
    public int Suscripcion_Alumnos { get { return m_Suscripcion_Alumnos; } set { m_Suscripcion_Alumnos = value; } }
    int m_Suscripcion_Visitantes = 0;
    [Description("Contiene la cantidad de Visitantes que se permitiran en esta suscripcion")]
    [DisplayNameAttribute("Suscripcion_Visitantes")]
    public int Suscripcion_Visitantes { get { return m_Suscripcion_Visitantes; } set { m_Suscripcion_Visitantes = value; } }
    bool m_Suscripcion_Adicionales = false;
    [Description("Indica si permitirá empleados, terminales, etc adicionales a los autorizados")]
    [DisplayNameAttribute("Suscripcion_Adicionales")]
    public bool Suscripcion_Adicionales { get { return m_Suscripcion_Adicionales; } set { m_Suscripcion_Adicionales = value; } }
    string m_Suscripcion_Otros = "";
    [Description("Contiene datos adicionales a validar")]
    [DisplayNameAttribute("Suscripcion_Otros")]
    public string Suscripcion_Otros { get { return m_Suscripcion_Otros; } set { m_Suscripcion_Otros = value; } }
    int m_Suscripcion_Mensual = 0;
    [Description("Mensualidad de pago por la suscripcion")]
    [DisplayNameAttribute("Suscripcion_Mensual")]
    public int Suscripcion_Mensual { get { return m_Suscripcion_Mensual; } set { m_Suscripcion_Mensual = value; } }
    DateTime m_Suscripcion_Final = CeC_BD.FechaNula;
    [Description("Fecha en la que finalizará el contrato")]
    [DisplayNameAttribute("Suscripcion_Final")]
    public DateTime Suscripcion_Final { get { return m_Suscripcion_Final; } set { m_Suscripcion_Final = value; } }

    //public int Suscripcion_ID = 0;
    private int Usuario_ID = 0;
    private CeC_Config Configuracion = null;
    public static string CampoLlave
    {
        get { return "SUSCRIPCION_ID"; }
    }

    public CeC_Suscripcion(CeC_Sesion Sesion)
        : base("EC_SUSCRIPCION", "SUSCRIPCION_ID", Sesion)
    {

    }

    public CeC_Suscripcion(int Suscripcion_ID, CeC_Sesion Sesion)
        : base("EC_SUSCRIPCION", "SUSCRIPCION_ID", Sesion)
    {
        Carga(Suscripcion_ID.ToString(), Sesion);
    }

    /// <summary>
    /// 
    /// </summary>
    //// <param name="SuscripcionId">Identificador único de la Suscripcion</param>
    /// <param name="EdoSuscripcionId">Identificador del Estado de la Suscripción</param>
    /// <param name="SuscripPrecioId">Identificador único del precio</param>
    /// <param name="SuscripcionNombre">Contiene la razon social del propietario de la suscripcion</param>
    /// <param name="SuscripcionBorrado">Indica si el cliente requiere Factura</param>
    /// <param name="SuscripcionRazon">Contiene la razon social del propietario de la suscripcion</param>
    /// <param name="SuscripcionRfc">RFC</param>
    /// <param name="SuscripcionDireccion1">Campo uno de Dirección</param>
    /// <param name="SuscripcionDireccion2">Campo dos de Dirección</param>
    /// <param name="SuscripcionCiudad">Campo para dato de Ciudad</param>
    /// <param name="SuscripcionEstado">Campo para dato de Estado</param>
    /// <param name="SuscripcionPais">Campo para dato de Pais</param>
    /// <param name="SuscripcionFacturar">Indica si el cliente requiere Factura</param>
    /// <param name="SuscripcionContratacion">Contiene la fecha de contratación</param>
    /// <param name="SuscripcionEmpleados">Contiene la cantidad de Empleados que se permitiran en esta suscripcion</param>
    /// <param name="SuscripcionTerminales">Contiene la cantidad de Terminales que se permitiran en esta suscripcion</param>
    /// <param name="SuscripcionUsuarios">Contiene la cantidad de Usuarios que se permitiran en esta suscripcion</param>
    /// <param name="SuscripcionAlumnos">Contiene la cantidad de Alumnos que se permitiran en esta suscripcion</param>
    /// <param name="SuscripcionVisitantes">Contiene la cantidad de Visitantes que se permitiran en esta suscripcion</param>
    /// <param name="SuscripcionAdicionales">Indica si permitirá empleados, terminales, etc adicionales a los autorizados</param>
    /// <param name="SuscripcionOtros">Contiene datos adicionales a validar</param>
    /// <param name="SuscripcionMensual">Mensualidad de pago por la suscripcion</param>
    /// <param name="SuscripcionFinal">Fecha en la que finalizará el contrato</param>
    /// <param name="Sesion"></param>
    /// <returns></returns>
    public bool Actualiza(int SuscripcionId, int EdoSuscripcionId, int SuscripPrecioId, string SuscripcionNombre, bool SuscripcionBorrado, string SuscripcionRazon, string SuscripcionRfc, string SuscripcionDireccion1, string SuscripcionDireccion2, string SuscripcionCiudad, string SuscripcionEstado, string SuscripcionPais, bool SuscripcionFacturar, DateTime SuscripcionContratacion, int SuscripcionEmpleados, int SuscripcionTerminales, int SuscripcionUsuarios, int SuscripcionAlumnos, int SuscripcionVisitantes, bool SuscripcionAdicionales, string SuscripcionOtros, int SuscripcionMensual, DateTime SuscripcionFinal,
CeC_Sesion Sesion)
    {
        try
        {
            bool Nuevo = false;
            if (!Carga(SuscripcionId.ToString(), Sesion))
                Nuevo = true;
            m_EsNuevo = Nuevo;
            Suscripcion_Id = SuscripcionId;
            Edo_Suscripcion_Id = EdoSuscripcionId;
            Suscrip_Precio_Id = SuscripPrecioId;
            Suscripcion_Nombre = SuscripcionNombre;
            Suscripcion_Borrado = SuscripcionBorrado;
            Suscripcion_Razon = SuscripcionRazon;
            Suscripcion_Rfc = SuscripcionRfc;
            Suscripcion_Direccion1 = SuscripcionDireccion1;
            Suscripcion_Direccion2 = SuscripcionDireccion2;
            Suscripcion_Ciudad = SuscripcionCiudad;
            Suscripcion_Estado = SuscripcionEstado;
            Suscripcion_Pais = SuscripcionPais;
            Suscripcion_Facturar = SuscripcionFacturar;
            Suscripcion_Contratacion = SuscripcionContratacion;
            Suscripcion_Empleados = SuscripcionEmpleados;
            Suscripcion_Terminales = SuscripcionTerminales;
            Suscripcion_Usuarios = SuscripcionUsuarios;
            Suscripcion_Alumnos = SuscripcionAlumnos;
            Suscripcion_Visitantes = SuscripcionVisitantes;
            Suscripcion_Adicionales = SuscripcionAdicionales;
            Suscripcion_Otros = SuscripcionOtros;
            Suscripcion_Mensual = SuscripcionMensual;
            Suscripcion_Final = SuscripcionFinal;
            if (Guarda(Sesion))
            {
                return true;
            }
        }
        catch { }
        return false;
    }
    /// <summary>
    /// Obtiene el identificador de la Suscripción
    /// </summary>
    /// <param name="SuscripcionNombreORazonORFC">Nombre de la Suscripcion o la Razon Social o el RFC</param>
    /// <returns>SuscripcionID. Devuelve 0 si no se encontro la Suscripción.</returns>
    public static int ObtenSuscripcionID(string SuscripcionNombre)
    {
        int SuscripcionID = 0;
        try
        {
            SuscripcionNombre = SuscripcionNombre.ToLower();
            SuscripcionID = CeC_BD.EjecutaEscalarInt(
                                "SELECT SUSCRIPCION_ID FROM EC_SUSCRIPCION WHERE LOWER(SUSCRIPCION_NOMBRE) = '" + CeC_BD.ObtenParametroCadena(SuscripcionNombre) + "'");
            return SuscripcionID;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return SuscripcionID;
    }

    public static string ObtenSuscripcionURL(string SuscripcionNombre)
    {
        string SuscripcionURL = "";
        try
        {
            SuscripcionNombre = SuscripcionNombre.ToLower();
            SuscripcionURL = CeC_BD.EjecutaEscalarString(
                                "SELECT SUSCRIPCION_URL FROM EC_SUSCRIPCION WHERE LOWER(SUSCRIPCION_NOMBRE) = '" + CeC_BD.ObtenParametroCadena(SuscripcionNombre) + "'");
            return SuscripcionURL;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return SuscripcionURL;
    }

    public static DataSet Listar(bool Borrados)
    {
        try
        {
            int l_Borrado = CeC.Convierte2Int(Borrados);
            string MostrarBorrados = "";
            if (l_Borrado == 0)
                MostrarBorrados = " WHERE SUSCRIPCION_BORRADO = '0'";
            string Campos =
                        " EC_SUSCRIPCION.SUSCRIPCION_NOMBRE, EC_SUSCRIPCION.SUSCRIPCION_RAZON, EC_SUSCRIPCION.SUSCRIPCION_RFC, " +
                        " EC_SUSCRIPCION.SUSCRIPCION_DIRECCION1, EC_SUSCRIPCION.SUSCRIPCION_DIRECCION2, " +
                        " EC_SUSCRIPCION.SUSCRIPCION_CIUDAD, EC_SUSCRIPCION.SUSCRIPCION_ESTADO, EC_SUSCRIPCION.SUSCRIPCION_PAIS, " +
                        " EC_SUSCRIPCION.EDO_SUSCRIPCION_ID, EC_SUSCRIPCION.SUSCRIPCION_FACTURAR, " +
                        " EC_SUSCRIPCION.SUSCRIPCION_CONTRATACION, EC_SUSCRIPCION.SUSCRIPCION_EMPLEADOS, " +
                        " EC_SUSCRIPCION.SUSCRIPCION_TERMINALES, EC_SUSCRIPCION.SUSCRIPCION_USUARIOS, " +
                        " EC_SUSCRIPCION.SUSCRIPCION_ALUMNOS, EC_SUSCRIPCION.SUSCRIPCION_VISITANTES, " +
                        " EC_SUSCRIPCION.SUSCRIPCION_ADICIONALES, EC_SUSCRIPCION.SUSCRIPCION_OTROS, " +
                        " EC_SUSCRIPCION.SUSCRIPCION_MENSUAL, EC_SUSCRIPCION.SUSCRIPCION_FINAL, " +
                        " EC_SUSCRIP_PRECIOS.SUSCRIP_PRECIO_EMPLEADOS, EC_SUSCRIP_PRECIOS.SUSCRIP_PRECIO_TERMINALES, " +
                        " EC_SUSCRIP_PRECIOS.SUSCRIP_PRECIO_USUARIOS, EC_SUSCRIP_PRECIOS.SUSCRIP_PRECIO_ALUMNOS, " +
                        " EC_SUSCRIP_PRECIOS.SUSCRIP_PRECIO_VISITANTES ";
            string Tabla = " EC_SUSCRIPCION INNER JOIN " +
                            " EC_SUSCRIP_PRECIOS ON EC_SUSCRIPCION.SUSCRIP_PRECIO_ID = EC_SUSCRIP_PRECIOS.SUSCRIP_PRECIO_ID ";
            string Condiciones = MostrarBorrados;
            string Qry = " SELECT " + Campos + " FROM " + Tabla + Condiciones;
            return (DataSet)CeC_BD.EjecutaDataSet(Qry);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return null;
    }
    public static string ObtenFiltro(int UsuarioID, bool Escritura)
    {
        if (Escritura)
            return "SUSCRIPCION_ID IN (SELECT SUSCRIPCION_ID FROM EC_PERMISOS_SUSCRIP WHERE USUARIO_ID = " + UsuarioID +
            " AND PER_SUS_CONT_TOT = 1) ";
        return "SUSCRIPCION_ID IN (SELECT SUSCRIPCION_ID FROM EC_PERMISOS_SUSCRIP WHERE USUARIO_ID = " + UsuarioID +
            ") ";
    }
    public static bool GuardaSuscripcion(int SuscripcionID, string SuscripcionNombre, string Contacto, string ContactoeMail, bool Borrado)
    {
        if (SuscripcionID < 0)
            return false;
        SuscripcionNombre = CeC_BD.ObtenParametroCadena(SuscripcionNombre);
        Contacto = CeC_BD.ObtenParametroCadena(Contacto);
        ContactoeMail = CeC_BD.ObtenParametroCadena(ContactoeMail);


        string sBorrar = "1";
        if (!Borrado)
            sBorrar = "0";

        if (CeC_BD.EjecutaComando("UPDATE EC_SUSCRIPCION SET SUSCRIPCION_NOMBRE ='" + SuscripcionNombre
            + "', SUSCRIPCION_BORRADO = " + sBorrar + " WHERE SUSCRIPCION_ID = " + SuscripcionID.ToString()) > 0)
        {
            if (CeC_BD.EjecutaComando("UPDATE EC_USUARIOS SET USUARIO_NOMBRE='" + Contacto +
                "', USUARIO_EMAIL = '" + ContactoeMail + "' ,USUARIO_BORRADO = " + sBorrar +
                " WHERE USUARIO_USUARIO IN( SELECT SUSCRIPCION_NOMBRE FROM EC_SUSCRIPCION WHERE SUSCRIPCION_ID = " + SuscripcionID.ToString() + ")") > 0)
                return true;
        }
        return false;
    }
    public static int CreaSuscripcion(CeC_Sesion Sesion, string SuscripcionNombre, string Contacto, string ContactoeMail)
    {
        //int Insertados = -1;
        SuscripcionNombre = CeC_BD.ObtenParametroCadena(SuscripcionNombre);
        if (CeC_BD.EjecutaEscalarInt("SELECT SUSCRIPCION_ID FROM EC_SUSCRIPCION WHERE SUSCRIPCION_NOMBRE = '" + SuscripcionNombre + "'") > 0)
            return -1;
        //int SuscripcionID = CeC_Autonumerico.GeneraAutonumerico("EC_SUSCRIPCION", "SUSCRIPCION_ID", Sesion);

        CeC_Suscripcion Suscripcion = new CeC_Suscripcion(Sesion);
        if (!Suscripcion.Actualiza(-1, 0, 0, SuscripcionNombre, false, "", "", "", "", "", "", "", false, DateTime.Now, 0, 0, 0, 0, 0, false, "", 0, DateTime.Now, Sesion))
            //{
            //    Insertados = CeC_BD.EjecutaComando("INSERT INTO EC_SUSCRIPCION (SUSCRIPCION_ID, SUSCRIPCION_NOMBRE,SUSCRIPCION_BORRADO) VALUES(" + Suscripcion.Suscripcion_Id + ",'" + SuscripcionNombre + "',0)");
            //}
            //if (Insertados < 0)
            return -2;
        Contacto = CeC_BD.ObtenParametroCadena(Contacto);

        ContactoeMail = CeC_BD.ObtenParametroCadena(ContactoeMail);
        int Sesion_ID = 0;
        if (Sesion != null)
            Sesion_ID = Sesion.SESION_ID;
        int UsuarioId = CeC_Usuarios.AgregaUsuario(SuscripcionNombre, 4, Contacto, "Controlador de suscripcion", ContactoeMail, Suscripcion.Suscripcion_Id, Sesion_ID);

        CeC_Sitios.InsertaPredeterminado(Sesion_ID, UsuarioId, Suscripcion.Suscripcion_Id);
        Cec_Incidencias.CrearTiposIncidenciasPredeterminados(Suscripcion.Suscripcion_Id);
        CeC_Turnos.CreaTurnosPredeterminados(UsuarioId, Suscripcion.Suscripcion_Id);

        return Suscripcion.Suscripcion_Id;
    }

    public static DataRow ObtenSuscripcionDatos(int SuscripcionID)
    {
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet("SELECT EC_SUSCRIPCION.SUSCRIPCION_ID, SUSCRIPCION_NOMBRE, USUARIO_NOMBRE, USUARIO_EMAIL, SUSCRIPCION_BORRADO FROM EC_SUSCRIPCION, EC_USUARIOS WHERE " +
            " EC_SUSCRIPCION.SUSCRIPCION_NOMBRE = EC_USUARIOS.USUARIO_USUARIO AND EC_SUSCRIPCION.SUSCRIPCION_ID = " + SuscripcionID);
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return null;
        return DS.Tables[0].Rows[0];
    }

    public static DataSet ObtenSuscripcionesDSGrid(bool Borrado)
    {
        string Filtro = "";
        if (!Borrado)
            Filtro = " AND SUSCRIPCION_BORRADO = 0 ";
        string Qry = "SELECT EC_SUSCRIPCION.SUSCRIPCION_ID, SUSCRIPCION_NOMBRE, USUARIO_NOMBRE, USUARIO_EMAIL, SUSCRIPCION_BORRADO FROM EC_SUSCRIPCION, EC_USUARIOS WHERE " +
            " EC_SUSCRIPCION.SUSCRIPCION_NOMBRE = EC_USUARIOS.USUARIO_USUARIO " + Filtro + " \n ORDER BY SUSCRIPCION_NOMBRE DESC";
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }
    /// <summary>
    /// Borra la Suscripción
    /// </summary>
    /// <param name="SuscripcionID">Identificador de la Suscripcion</param>
    /// <returns>Verdadero en caso de que se halla borrado la Suscripcion</returns>
    public static bool BorraSuscripcion(int SuscripcionID)
    {
        return BorraSuscripcion(SuscripcionID, true);
    }
    /// <summary>
    /// Borra la Suscripción
    /// </summary>
    /// <param name="SuscripcionID">Identificador de la Suscripcion</param>
    /// <param name="Borrar"></param>
    /// <returns>Verdadero en caso de que se halla borrado la Suscripcion</returns>
    public static bool BorraSuscripcion(int SuscripcionID, bool Borrar)
    {
        string sBorrar = "1";
        if (!Borrar)
            sBorrar = "0";
        if (CeC_BD.EjecutaComando("UPDATE EC_SUSCRIPCION SET SUSCRIPCION_BORRADO = " + sBorrar + " WHERE SUSCRIPCION_ID = " + SuscripcionID.ToString()) > 0)
        {
            if (CeC_BD.EjecutaComando("UPDATE EC_USUARIOS SET USUARIO_BORRADO = " + sBorrar + " WHERE USUARIO_USUARIO IN( SELECT SUSCRIPCION_NOMBRE WHERE SUSCRIPCION_ID = " + SuscripcionID.ToString() + ")") > 0)

                return true;
        }
        return false;
    }
    public static int PermisoObten(int Usuario_ID, int Suscripcion_ID)
    {
        return CeC_BD.EjecutaEscalarInt("SELECT PER_SUS_CONT_TOT FROM EC_PERMISOS_SUSCRIP WHERE SUSCRIPCION_ID = "
        + Suscripcion_ID + " AND USUARIO_ID = " + Usuario_ID + "");
    }
    public static bool PermisoDar(int Sesion_ID, int Usuario_ID, int Suscripcion_ID, int Permiso)
    {
        int NPermiso = 3;
        if (Permiso != 1)
            NPermiso = 1;
        CeC_Agrupaciones.AsignaPermiso(0, Suscripcion_ID, Usuario_ID, "", NPermiso);
        int ExistePermiso = PermisoObten(Usuario_ID, Suscripcion_ID);
        if (ExistePermiso < 0)
            CeC_BD.EjecutaComando("INSERT INTO EC_PERMISOS_SUSCRIP (USUARIO_ID, SUSCRIPCION_ID, PER_SUS_CONT_TOT, PER_SUS_CONTROL) VALUES("
                + Usuario_ID + "," + Suscripcion_ID + ",1,'')");
        else
            CeC_BD.EjecutaComando("UPDATE PER_SUS_CONT_TOT SET PER_SUS_CONT_TOT = " + Permiso + " WHERE USUARIO_ID = " + Usuario_ID + " AND SUSCRIPCION_ID = " + Suscripcion_ID);
        return true;
    }
    public static bool PermisoDarControlTotal(int Sesion_ID, int Usuario_ID, int Suscripcion_ID)
    {
        return PermisoDar(Sesion_ID, Usuario_ID, Suscripcion_ID, 1);
    }
    public static bool PermisoQuitar(int Usuario_ID, int Suscripcion_ID)
    {
        CeC_Agrupaciones.QuitaPermisoUsuario(Suscripcion_ID, Usuario_ID, "");
        CeC_BD.EjecutaComando("DELETE PER_SUS_CONT_TOT WHERE USUARIO_ID = " + Usuario_ID + " AND SUSCRIPCION_ID = " + Suscripcion_ID);
        return true;
    }

    /// <summary>
    /// Obtiene el usuario Maestro de una suscriocion
    /// Actualmente el primer usuario encontrado es el maextro
    /// </summary>
    /// <param name="SuscripcionID"></param>
    /// <returns></returns>
    public static int ObtenUsuarioID(int SuscripcionID)
    {
        return CeC_BD.EjecutaEscalarInt("SELECT MIN(USUARIO_ID) FROM EC_USUARIOS WHERE SUSCRIPCION_ID = " + SuscripcionID);
    }
}
