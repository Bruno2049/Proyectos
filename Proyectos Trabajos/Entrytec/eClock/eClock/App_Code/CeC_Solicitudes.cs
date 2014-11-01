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
/// Descripción breve de CeC_Solicitudes
/// </summary>
public class CeC_Solicitudes
{
    public enum EstadoSolicitud
    {
        Pendiente = 0,
        Revisado,
        Autorizado,
        Denegado,
        Cerrado
    }

    public enum TipoPrioridad
    {
        Muy_Baja = -2,
        Baja,
        Normal,
        Alta,
        Muy_Alta
    }

    public int SOLICITUD_ID = -1;
    public int EDO_SOLICITUD_ID;
    public int TIPO_PRIORIDAD_ID;
    public string SOLICITUD_TITULO;
    public string SOLICITUD_DESC;
    public string SOLICITUD_EXTRAS;
    public DateTime SOLICITUD_FECHAHORA;
    public DateTime SOLICITUD_ANTESDE;

    public CeC_Solicitudes()
    {
    }
    public CeC_Solicitudes(int SolicitudID)
    {
        string Qry = "SELECT " + Campos + " FROM EC_SOLICITUDES WHERE SOLICITUD_ID = " + SolicitudID.ToString();
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry);
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return;
        Carga(DS.Tables[0].Rows[0]);

    }
    public static string Campos
    {
        get { return " SOLICITUD_ID, EDO_SOLICITUD_ID, TIPO_PRIORIDAD_ID, SOLICITUD_TITULO, SOLICITUD_DESC, SOLICITUD_EXTRAS, SOLICITUD_FECHAHORA, SOLICITUD_ANTESDE"; }
    }
    public bool Carga(DataRow Fila)
    {
        try
        {
            DataRow DR = Fila;
            SOLICITUD_ID = CeC.Convierte2Int(DR["SOLICITUD_ID"]);
            EDO_SOLICITUD_ID = CeC.Convierte2Int(DR["EDO_SOLICITUD_ID"]);
            TIPO_PRIORIDAD_ID = CeC.Convierte2Int(DR["TIPO_PRIORIDAD_ID"]);
            SOLICITUD_TITULO = CeC.Convierte2String(DR["SOLICITUD_TITULO"]);
            SOLICITUD_DESC = CeC.Convierte2String(DR["SOLICITUD_DESC"]);
            SOLICITUD_EXTRAS = CeC.Convierte2String(DR["SOLICITUD_EXTRAS"]);
            SOLICITUD_FECHAHORA = CeC.Convierte2DateTime(DR["SOLICITUD_FECHAHORA"]);
            SOLICITUD_ANTESDE = CeC.Convierte2DateTime(DR["SOLICITUD_ANTESDE"]);
            return true;
        }
        catch { }
        return false;
    }
    /// <summary>
    /// Agrega una Solicitud
    /// </summary>
    /// <param name="EstadoSolicitud"></param>
    /// <param name="TipoPrioridad"></param>
    /// <param name="Titulo"></param>
    /// <param name="Descripcion"></param>
    /// <param name="Extras"></param>
    /// <param name="AntesDe"></param>
    /// <param name="SesionID"></param>
    /// <returns></returns>
    public static int Agrega(EstadoSolicitud EstadoSolicitud, TipoPrioridad TipoPrioridad, string Titulo, string Descripcion, string Extras, DateTime AntesDe, int SesionID)
    {
        int SOLICITUD_ID = CeC_Autonumerico.GeneraAutonumerico("EC_SOLICITUDES", "SOLICITUD_ID", SesionID, 0);
        if (CeC_BD.EjecutaComando("INSERT INTO EC_SOLICITUDES (SOLICITUD_ID, EDO_SOLICITUD_ID, TIPO_PRIORIDAD_ID, SOLICITUD_TITULO, SOLICITUD_DESC, SOLICITUD_EXTRAS, SOLICITUD_FECHAHORA, SOLICITUD_ANTESDE) VALUES(" +
            SOLICITUD_ID + ", " + Convert.ToInt32(EstadoSolicitud) + ", " + Convert.ToInt32(TipoPrioridad) + ", '" + CeC_BD.ObtenParametroCadena(Titulo) + "', '"
            + CeC_BD.ObtenParametroCadena(Descripcion) + "', '" + CeC_BD.ObtenParametroCadena(Extras) + "', " + CeC_BD.QueryFechaHora + ", " + CeC_BD.SqlFechaHora(AntesDe) + ")") > 0)
            return SOLICITUD_ID;
        return -1;
    }
    /// <summary>
    /// Agrega una Solicitud y la liga a un usuario
    /// </summary>
    /// <param name="UsuarioID"></param>
    /// <param name="EstadoSolicitud"></param>
    /// <param name="TipoPrioridad"></param>
    /// <param name="Titulo"></param>
    /// <param name="Descripcion"></param>
    /// <param name="Extras"></param>
    /// <param name="AntesDe"></param>
    /// <param name="SesionID"></param>
    /// <returns></returns>
    public static int Agrega(int UsuarioID, EstadoSolicitud EstadoSolicitud, TipoPrioridad TipoPrioridad, string Titulo, string Descripcion, string Extras, DateTime AntesDe, int SesionID)
    {
        int SolicitudID = Agrega(EstadoSolicitud, TipoPrioridad, Titulo, Descripcion, Extras, AntesDe, SesionID);
        if (SolicitudID > 0)
        {
            Liga(SolicitudID, UsuarioID, EstadoSolicitud, TipoPrioridad, Titulo, Descripcion, Extras, AntesDe);
        }
        return SolicitudID;
    }

    public static bool Liga(int SolicitudID, int UsuarioID, EstadoSolicitud EstadoSolicitud, TipoPrioridad TipoPrioridad, string Titulo, string Descripcion, string Extras, DateTime AntesDe)
    {
        if (CeC_BD.EjecutaComando("INSERT INTO EC_SOLICITUDES_USUARIOS (SOLICITUD_ID, USUARIO_ID) VALUES(" + SolicitudID + ", " + UsuarioID + ")") > 0)
        {
            CeC_Config Config = new CeC_Config(UsuarioID);
            if (Config.EnviarMailSolicitudIncidencia)
            {
//                string Link = CeC_Config.LinkeClock + "WF_Solicitudes.aspx";
//                Descripcion += "<br><a href=\"" + Link + "?Parametros=" + SolicitudID + "\">" + Link + "</a>";
                string Link = CeC_Config.LinkeClock;
                Descripcion += "<br><a href=\"" + Link + "\">" + Link + "</a>";
                CeC_BD.EnviarMail(CeC_Usuarios.ObtenUsuarioeMail(UsuarioID), Titulo, Descripcion);
            }
            return true;
        }
        return false;
    }
    /// <summary>
    ///  Agrega una Solicitud y la liga varios usuarios
    /// </summary>
    /// <param name="UsuariosIDs">listado de usuarioID separados por coma</param>
    /// <param name="EstadoSolicitud"></param>
    /// <param name="TipoPrioridad"></param>
    /// <param name="Titulo"></param>
    /// <param name="Descripcion"></param>
    /// <param name="Extras"></param>
    /// <param name="AntesDe"></param>
    /// <param name="SesionID"></param>
    /// <returns></returns>
    public static int Agrega(string UsuariosIDs, EstadoSolicitud EstadoSolicitud, TipoPrioridad TipoPrioridad, string Titulo, string Descripcion, string Extras, DateTime AntesDe, int SesionID)
    {
        int SolicitudID = Agrega(EstadoSolicitud, TipoPrioridad, Titulo, Descripcion, Extras, AntesDe, SesionID);
        if (SolicitudID > 0)
        {
            string[] sUsuariosIDS = CeC.ObtenArregoSeparador(UsuariosIDs, ",");
            foreach (string sUsuarioID in sUsuariosIDS)
            {
                int UsuarioID = CeC.Convierte2Int(sUsuarioID);
                if (UsuarioID > 0)
                    Liga(SolicitudID, UsuarioID, EstadoSolicitud, TipoPrioridad, Titulo, Descripcion, Extras, AntesDe);
            }
        }
        return SolicitudID;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="TipoIncidenciaID"></param>
    /// <param name="PersonasDiarioIds">Deberán pertenecer a un solo personaID</param>
    /// <param name="Comentario"></param>
    /// <param name="Sesion"></param>
    /// <returns></returns>
    public static bool AgregaJustificacion(int TipoIncidenciaID, string PersonasDiarioIds,string Comentario, CeC_Sesion Sesion)
    {
        string PersonasIDs = CeC_Asistencias.ObtenPersonasIDs(PersonasDiarioIds);
        DataSet DS = (DataSet) CeC_BD.EjecutaDataSet("SELECT USUARIO_ID FROM EC_USUARIOS_PERMISOS,EC_PERSONAS WHERE " +
            "EC_PERSONAS.AGRUPACION_NOMBRE LIKE EC_USUARIOS_PERMISOS.USUARIO_PERMISO + '%' " +
            " AND EC_PERSONAS.PERSONA_ID in (" + PersonasIDs + ")");
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return false;
        string sUsuarios = "";
        foreach (DataRow DR in DS.Tables[0].Rows)
        {
            sUsuarios = CeC.AgregaSeparador(sUsuarios,CeC.Convierte2String(DR["USUARIO_ID"]),",");
        }
        string TipoIncidencia = Cec_Incidencias.ObtenTipoIncidenciaNombre(TipoIncidenciaID);
        int PersonaID = CeC.Convierte2Int(PersonasIDs);
        string Nombre = CeC_BD.ObtenPersonaNombre(PersonaID);
        int PersonaLinkID = CeC_BD.ObtenPersonaLinkID(PersonaID);
        string Titulo = "Solicitud de " + TipoIncidencia + " para " + PersonaLinkID;
        string Descripcion = "Justificacion: " + TipoIncidencia + " <br>Para: " + PersonaLinkID + "-" + Nombre +
            "<br> los días " + CeC_Asistencias.ObtenFechas(PersonasDiarioIds);
        string Extras ="JUSTIFICACION|"+TipoIncidenciaID+"|"+PersonasDiarioIds+"|"+Comentario;

        Agrega(sUsuarios, EstadoSolicitud.Pendiente, TipoPrioridad.Normal, Titulo, Descripcion, Extras, DateTime.Now, Sesion.SESION_ID);
        return true;
    }
    public static int ObtenNoPorAutorizar(int UsuarioID)
    {
        string Qry = "SELECT COUNT(EC_SOLICITUDES.SOLICITUD_ID) FROM EC_SOLICITUDES,EC_SOLICITUDES_USUARIOS WHERE EC_SOLICITUDES.SOLICITUD_ID=EC_SOLICITUDES_USUARIOS.SOLICITUD_ID AND EC_SOLICITUDES_USUARIOS.USUARIO_ID = " + UsuarioID +
            " AND EDO_SOLICITUD_ID < 2 ";
        return CeC_BD.EjecutaEscalarInt(Qry);
    }
    public static string ObtenEstadoSolicitudNombre(int EstadoSolicitudID)
    {
        return CeC_BD.EjecutaEscalarString("SELECT EDO_SOLICITUD_NOMBRE FROM EC_EDO_SOLICITUD WHERE EDO_SOLICITUD_ID = " + EstadoSolicitudID);
    }
    public static bool Autoriza(CeC_Sesion Sesion, string Extra)
    {
        string []Parametros = CeC.ObtenArregoSeparador(Extra, "|");
        if (Parametros[0] == "JUSTIFICACION")
        {
            if (Cec_Incidencias.CreaIncidencia(CeC.Convierte2Int(Parametros[1]), Parametros[2], Parametros[3], Sesion) > 0)
                return true;
        }
        return false;
    }
    public static bool CambiaEstado(int SolicitudID, EstadoSolicitud Estado, CeC_Sesion Sesion)
    {
        CeC_Solicitudes Sol = new CeC_Solicitudes(SolicitudID);
        int UsuarioID = CeC_Autonumerico.ObtenUsuarioID("EC_SOLICITUDES", "SOLICITUD_ID", SolicitudID);
        if (UsuarioID > 0)
        {            
            string Titulo= "Cambio de estado de solicitud";
            string Descripcion = "Se ha cambiado el estado de " + Sol.SOLICITUD_DESC+"<br>" + "Nuevo estado = " + ObtenEstadoSolicitudNombre(Convert.ToInt32(Estado));
            CeC_BD.EnviarMail(CeC_Usuarios.ObtenUsuarioeMail(UsuarioID), Titulo, Descripcion);
        }
        if (Estado == EstadoSolicitud.Autorizado)
        {
            Autoriza(Sesion, Sol.SOLICITUD_EXTRAS);
        }
        string Qry = "UPDATE EC_SOLICITUDES SET EDO_SOLICITUD_ID = "+ Convert.ToInt32(Estado) + " WHERE SOLICITUD_ID = " +SolicitudID;
        if (CeC_BD.EjecutaComando(Qry) > 0)
            return true;
        return false;
    }
    public static DataSet ObtenDetalles(int UsuarioID)
    {
        return (DataSet)CeC_BD.EjecutaDataSet("SELECT EC_SOLICITUDES." + Campos + " FROM EC_SOLICITUDES,EC_SOLICITUDES_USUARIOS WHERE EC_SOLICITUDES.SOLICITUD_ID=EC_SOLICITUDES_USUARIOS.SOLICITUD_ID AND USUARIO_ID = " + UsuarioID +
            " AND EDO_SOLICITUD_ID < 2 "); 
    }
}
