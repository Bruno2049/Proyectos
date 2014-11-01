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
/// Descripción breve de CeC_Usuarios
/// </summary>
public class CeC_Usuarios : CeC_Tabla
{
    int m_Usuario_Id = 0;
    [Description("Identificador de la tabla de usuarios")]
    [DisplayNameAttribute("Usuario_Id")]
    public int Usuario_Id { get { return m_Usuario_Id; } set { m_Usuario_Id = value; } }
    int m_Perfil_Id = 0;
    [Description("Identificar del Perfil")]
    [DisplayNameAttribute("Perfil_Id")]
    public int Perfil_Id { get { return m_Perfil_Id; } set { m_Perfil_Id = value; } }
    string m_Usuario_Usuario = "";
    [Description("Usuario (login)")]
    [DisplayNameAttribute("Usuario_Usuario")]
    public string Usuario_Usuario { get { return m_Usuario_Usuario; } set { m_Usuario_Usuario = value; } }
    string m_Usuario_Nombre = "";
    [Description("Nombre propio de la persona")]
    [DisplayNameAttribute("Usuario_Nombre")]
    public string Usuario_Nombre { get { return m_Usuario_Nombre; } set { m_Usuario_Nombre = value; } }
    string m_Usuario_Descripcion = "";
    [Description("Cualquier comentario sobre el usuario")]
    [DisplayNameAttribute("Usuario_Descripcion")]
    public string Usuario_Descripcion { get { return m_Usuario_Descripcion; } set { m_Usuario_Descripcion = value; } }
    string m_Usuario_Clave = "";
    [Description("Contraseña del usuario")]
    [DisplayNameAttribute("Usuario_Clave")]
    public string Usuario_Clave { get { return m_Usuario_Clave; } set { m_Usuario_Clave = value; } }
    string m_Usuario_Email = "";
    [Description("Contiene el e-mail del usuario")]
    [DisplayNameAttribute("Usuario_Email")]
    public string Usuario_Email { get { return m_Usuario_Email; } set { m_Usuario_Email = value; } }
    bool m_Usuario_Envmaila = true;
    [Description("Indica que a este usuario se le enviaran mails de asistencia con todos sus empleados seleccionados")]
    [DisplayNameAttribute("Usuario_Envmaila")]
    public bool Usuario_Envmaila { get { return m_Usuario_Envmaila; } set { m_Usuario_Envmaila = value; } }
    int m_Suscripcion_Id = 0;
    [Description("Suscripcion Predeterminada")]
    [DisplayNameAttribute("Suscripcion_Id")]
    public int Suscripcion_Id { get { return m_Suscripcion_Id; } set { m_Suscripcion_Id = value; } }
    string m_Usuario_Agrupacion = "";
    [Description("Contiene la agrupación a la cual tiene control, por default esta en blanco y significa a todos los empleados de la suscripcion")]
    [DisplayNameAttribute("Usuario_Agrupacion")]
    public string Usuario_Agrupacion { get { return m_Usuario_Agrupacion; } set { m_Usuario_Agrupacion = value; } }
    bool m_Usuario_Essup = true;
    [Description("Indica que este usuario es supervisor, reporteador o administrador")]
    [DisplayNameAttribute("Usuario_Essup")]
    public bool Usuario_Essup { get { return m_Usuario_Essup; } set { m_Usuario_Essup = value; } }
    bool m_Usuario_Esemp = false;
    [Description("Indica que este usuario es empleado")]
    [DisplayNameAttribute("Usuario_Esemp")]
    public bool Usuario_Esemp { get { return m_Usuario_Esemp; } set { m_Usuario_Esemp = value; } }
    int m_Persona_Id = 0;
    [Description("Identificador del persona_ID en caso de ser empleado")]
    [DisplayNameAttribute("Persona_Id")]
    public int Persona_Id { get { return m_Persona_Id; } set { m_Persona_Id = value; } }
    bool m_Usuario_Borrado = false;
    [Description("Si es positivo indica que este usuario esta dado de baja (borrado)")]
    [DisplayNameAttribute("Usuario_Borrado")]
    public bool Usuario_Borrado { get { return m_Usuario_Borrado; } set { m_Usuario_Borrado = value; } }

    public CeC_Usuarios(CeC_Sesion Sesion)
        : base("EC_USUARIOS", "USUARIO_ID", Sesion)
    {

    }
    public CeC_Usuarios(int UsuarioID, CeC_Sesion Sesion)
        : base("EC_USUARIOS", "USUARIO_ID", Sesion)
    {
        Carga(UsuarioID.ToString(), Sesion);
    }


    public bool Actualiza(int UsuarioId, int PerfilId, string UsuarioUsuario, string UsuarioNombre, string UsuarioDescripcion, string UsuarioClave, string UsuarioEmail, bool UsuarioEnvmaila, int SuscripcionId, string UsuarioAgrupacion, bool UsuarioEssup, bool UsuarioEsemp, int PersonaId, bool UsuarioBorrado,
    CeC_Sesion Sesion)
    {
        try
        {
            bool Nuevo = false;
            if (!Carga(UsuarioId.ToString(), Sesion))
                Nuevo = true;
            m_EsNuevo = Nuevo;
            Usuario_Id = UsuarioId; 
            Perfil_Id = PerfilId; 
            Usuario_Usuario = UsuarioUsuario; 
            Usuario_Nombre = UsuarioNombre; 
            Usuario_Descripcion = UsuarioDescripcion; 
            Usuario_Clave = UsuarioClave; 
            Usuario_Email = UsuarioEmail; 
            Usuario_Envmaila = UsuarioEnvmaila; 
            Suscripcion_Id = SuscripcionId; 
            Usuario_Agrupacion = UsuarioAgrupacion; 
            Usuario_Essup = UsuarioEssup; 
            Usuario_Esemp = UsuarioEsemp; 
            Persona_Id = PersonaId; 
            Usuario_Borrado = UsuarioBorrado;
            if (Guarda(Sesion))
            {
                return true;
            }
        }
        catch { }
        return false;
    }

    public static bool CambiaUsuarioeMail(int UsuarioID, string eMail)
    {
        int R = CeC_BD.EjecutaComando("UPDATE EC_USUARIOS SET USUARIO_EMAIL = '" + CeC_BD.ObtenParametroCadena(eMail) + "' WHERE USUARIO_ID = " + UsuarioID);
        if (R > 0)
            return true;
        return false;
    }
    public static bool CreaPassword(int UsuarioID)
    {
        return CreaPassword(UsuarioID, "");
    }
    public static bool CreaPassword(int UsuarioID, string Usuario)
    {
        CS_GeneraPasswords GP = new CS_GeneraPasswords();
        GP.ExcludeSymbols = true;
        

        string PasswordNuevo = GP.Generate();
        int R = CeC_BD.EjecutaComando("UPDATE EC_USUARIOS SET USUARIO_CLAVE = '" + CeC_BD.ObtenParametroCadena(PasswordNuevo) + "' WHERE USUARIO_ID = " + UsuarioID);
        if (R > 0)
        {
            string UsuarioTexto = " ";
            if(Usuario.Length > 0)
                UsuarioTexto = "Usuario: " + Usuario + "<BR>";
            string PasswordTexto = "Clave: " +PasswordNuevo +"<BR>";
            string URL = CeC_Config.LinkURL;
            CeC_Asistencias.EnviaMail(ObtenUsuarioeMail(UsuarioID), "Regeneración de contraseña", "Se le ha regenerado su clave de acceso:<BR> " +UsuarioTexto+ PasswordTexto
                + " <BR>Esta clave usted puede cambiarla desde el sistema para incrementar la seguridad <BR> <BR><a href=\"" + URL + "\">" + URL + "</a>");
            return true;
        }
        return false;
    }
    public static bool EnviaPassword(int UsuarioID)
    {
        string Password = CeC_BD.EjecutaEscalarString("SELECT USUARIO_CLAVE FROM EC_USUARIOS WHERE USUARIO_ID = " + UsuarioID);

        string URL = CeC_Config.LinkURL;
            CeC_Asistencias.EnviaMail(ObtenUsuarioeMail(UsuarioID), "recuperación de contraseña", "Se ha recuperado su clave de acceso la cual es: " + Password
                + " <BR>Esta clave usted puede cambiarla desde el sistema para incrementar la seguridad <BR><BR><a href=\"" + URL + "\">" + URL + "</a>");
            return true;

    }

    public static bool CambiaPassword(int UsuarioID, string PasswordAnterior, string PasswordNuevo)
    {
        string Anterior = CeC_BD.EjecutaEscalarString("SELECT USUARIO_CLAVE FROM EC_USUARIOS WHERE USUARIO_ID = " + UsuarioID);
        if (Anterior != PasswordAnterior)
            return false;
        int R = CeC_BD.EjecutaComando("UPDATE EC_USUARIOS SET USUARIO_CLAVE = '" + CeC_BD.ObtenParametroCadena(PasswordNuevo) + "' WHERE USUARIO_ID = " + UsuarioID);
        if (R > 0)
            return true;
        return false;
    }
    public static bool ObtenUSUARIO_ESEMP(int UsuarioID)
    {
        return CeC_BD.EjecutaEscalarBool("SELECT USUARIO_ESEMP FROM EC_USUARIOS WHERE USUARIO_ID = " + UsuarioID, false);
    }
    /// <summary>
    /// Obtiene el persona ID en caso de ser un usuario de empleado
    /// </summary>
    /// <param name="UsuarioID"></param>
    /// <returns></returns>
    public static int ObtenPersonaID(int UsuarioID)
    {
        return CeC_BD.EjecutaEscalarInt("SELECT PERSONA_ID FROM EC_USUARIOS WHERE USUARIO_ID = " + UsuarioID);
    }
    public static int ObtenSuscripcionID(int UsuarioID)
    {
        return CeC_BD.EjecutaEscalarInt("SELECT SUSCRIPCION_ID FROM EC_USUARIOS WHERE USUARIO_ID = " + UsuarioID);
    }
    public static string ObtenUsuarioNombre(int UsuarioID)
    {
        return CeC_BD.EjecutaEscalarString("SELECT USUARIO_NOMBRE FROM EC_USUARIOS WHERE USUARIO_ID = " + UsuarioID);
    }
    public static bool ObtenEsEmpleado(int UsuarioID)
    {
        return CeC_BD.EjecutaEscalarBool("SELECT USUARIO_ESEMP FROM EC_USUARIOS WHERE USUARIO_ID = " + UsuarioID,false);
    }

    public static string ObtenUsuarioeMail(int UsuarioID)
    {
        return CeC_BD.EjecutaEscalarString("SELECT USUARIO_EMAIL FROM EC_USUARIOS WHERE USUARIO_ID = " + UsuarioID);
    }
    public static DataSet ObtenUsuariosDS(int Suscripcion_ID, int UsuarioBorrado)
    {
        return (DataSet)CeC_BD.EjecutaDataSet(  " SELECT USUARIO_ID, USUARIO_NOMBRE, USUARIO_DESCRIPCION, " +
                                                " USUARIO_EMAIL, PERFIL_ID, USUARIO_USUARIO,'' as USUARIO_CLAVE, USUARIO_ENVMAILA, USUARIO_BORRADO " +
                                                " FROM EC_USUARIOS " +
                                                " WHERE        (USUARIO_BORRADO = 0 OR " + 
                                                " USUARIO_BORRADO = " + UsuarioBorrado +") AND (USUARIO_ID > 0) AND (SUSCRIPCION_ID = " + Suscripcion_ID + ")");

            //" SELECT USUARIO_ID, PERFIL_ID, USUARIO_USUARIO, USUARIO_NOMBRE, USUARIO_DESCRIPCION, USUARIO_EMAIL, " +
            //                                        " (SELECT PERSONA_EMAIL " +
            //                                        "  FROM EC_PERSONAS " +
            //                                        "  WHERE (PERSONA_LINK_ID) = EC_USUARIOS.USUARIO_USUARIO) AS PERSONA_EMAIL " +
            //                                      " FROM EC_USUARIOS " +
            //                                      " WHERE USUARIO_BORRADO = 0 AND SUSCRIPCION_ID = " + Suscripcion_ID.ToString());
    }
    public static int ObtenUsuarioID(string UsuarioOeMail)
    {
        UsuarioOeMail = UsuarioOeMail.ToLower();
        return CeC_BD.EjecutaEscalarInt("SELECT USUARIO_ID FROM EC_USUARIOS WHERE LOWER(USUARIO_USUARIO) = '" +CeC_BD.ObtenParametroCadena(UsuarioOeMail) 
            + "' OR LOWER(USUARIO_EMAIL) = '" +CeC_BD.ObtenParametroCadena(UsuarioOeMail) +"'");
    }
    public static int AgregaUsuario(string Usuario, int Perfil_ID, string Nombre, string Descripcion, string eMail, int SuscripcionID, int Sesion_ID)
    {
        return AgregaUsuario(Usuario, Perfil_ID, Nombre, Descripcion, eMail, SuscripcionID, Sesion_ID, true, 0);
    }
    public static int AgregaUsuario(string Usuario,int Perfil_ID, string Nombre, string Descripcion, string eMail,  int SuscripcionID, int Sesion_ID, bool EsSupervisor, int Persona_ID)
    {
        if (ObtenUsuarioID(Usuario) > 0)
            return 0;
        int UsuarioID = CeC_Autonumerico.GeneraAutonumerico("EC_USUARIOS","USUARIO_ID",Sesion_ID,SuscripcionID);
        if(UsuarioID < 0)
            return -1;
        int EsEmpleado = 0;
        if (Persona_ID > 0)
            EsEmpleado = 1;
        int EsSup = 0;
        if (EsSupervisor)
            EsSup = 1;
        string Qry = "INSERT INTO EC_USUARIOS (USUARIO_ID, PERFIL_ID, USUARIO_USUARIO, USUARIO_NOMBRE, USUARIO_DESCRIPCION, " +
            " USUARIO_EMAIL,  SUSCRIPCION_ID, USUARIO_CLAVE,USUARIO_ESSUP, USUARIO_ESEMP, PERSONA_ID, USUARIO_BORRADO) VALUES(" +
            UsuarioID + ", " + Perfil_ID + ", '" + CeC_BD.ObtenParametroCadena(Usuario) + "', '" + CeC_BD.ObtenParametroCadena(Nombre) +
            "', '" + CeC_BD.ObtenParametroCadena(Descripcion) + "', '" + CeC_BD.ObtenParametroCadena(eMail) + "', " + SuscripcionID +
            ", 'CLAVE',"+ EsSup + "," + EsEmpleado + "," + Persona_ID + ",0)";
        int R = CeC_BD.EjecutaComando(Qry);
        if (R < 0)
            return -2;
        CreaPassword(UsuarioID, Usuario);
        if (Perfil_ID == 1 || Perfil_ID == 4)
        {
            ///Se dará control total sobre la suscripcion
            CeC_Suscripcion.PermisoDarControlTotal(Sesion_ID, UsuarioID, SuscripcionID);
        }
        return R;
    }
    public static string ObtenUsuario(int Persona_ID)
    {
        return CeC_BD.EjecutaEscalarString("SELECT USUARIO_USUARIO FROM EC_USUARIOS WHERE PERSONA_ID = " + Persona_ID);
    }
    public static int CreaUsuarioDesdeEmpeado(int PersonaID, string eMail)
    {
        CeC_Personas Persona = new CeC_Personas(PersonaID);
        Persona.PERSONA_EMAIL = eMail;
        return AgregaUsuario(eMail, 7, Persona.PERSONA_NOMBRE, "Empleado", eMail, Persona.SUSCRIPCION_ID, 0,false,PersonaID);
    }
}
