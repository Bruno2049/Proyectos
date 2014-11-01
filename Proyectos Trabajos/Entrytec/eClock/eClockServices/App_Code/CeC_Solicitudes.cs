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
using eClockBase;
using System.Collections.Generic;

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
    public int PERSONA_ID;
    public string SOLICITUD_EXTRA_ID;
    public string SOLICITUD_PDIDS;
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
        get
        {
            return " SOLICITUD_ID, EDO_SOLICITUD_ID, TIPO_PRIORIDAD_ID, SOLICITUD_TITULO, SOLICITUD_DESC, SOLICITUD_EXTRAS, SOLICITUD_FECHAHORA," +
                " SOLICITUD_ANTESDE, PERSONA_ID,SOLICITUD_EXTRA_ID,SOLICITUD_PDIDS";
        }
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
            PERSONA_ID = CeC.Convierte2Int(DR["PERSONA_ID"]);
            SOLICITUD_EXTRA_ID = CeC.Convierte2String(DR["SOLICITUD_EXTRA_ID"]);
            SOLICITUD_PDIDS = CeC.Convierte2String(DR["SOLICITUD_PDIDS"]);
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
    public static int Agrega(EstadoSolicitud EstadoSolicitud, TipoPrioridad TipoPrioridad, string Titulo, string Descripcion, string Extras, DateTime AntesDe, int PersonaID, string ExtraID, string PersonasDiarioIds, CeC_Sesion Sesion)
    {
        int SOLICITUD_ID = CeC_Autonumerico.GeneraAutonumerico("EC_SOLICITUDES", "SOLICITUD_ID", Sesion);
        if (CeC_BD.EjecutaComando("INSERT INTO EC_SOLICITUDES (SOLICITUD_ID, EDO_SOLICITUD_ID, TIPO_PRIORIDAD_ID, SOLICITUD_TITULO," +
            " SOLICITUD_DESC, SOLICITUD_EXTRAS, SOLICITUD_FECHAHORA, SOLICITUD_ANTESDE, PERSONA_ID,SOLICITUD_EXTRA_ID,SOLICITUD_PDIDS) VALUES(" +
            SOLICITUD_ID + ", " + Convert.ToInt32(EstadoSolicitud) + ", " + Convert.ToInt32(TipoPrioridad) + ", '" + CeC_BD.ObtenParametroCadena(Titulo) + "', '"
            + CeC_BD.ObtenParametroCadena(Descripcion) + "', '" + CeC_BD.ObtenParametroCadena(Extras) + "', " + CeC_BD.QueryFechaHora + ", " +
                CeC_BD.SqlFechaHora(AntesDe) + "," + PersonaID + ",'" + CeC_BD.ObtenParametroCadena(ExtraID) + "','" + CeC_BD.ObtenParametroCadena(PersonasDiarioIds) + "')") > 0)
        {
            CeC_BD.EjecutaComando("INSERT INTO EC_SOLICITUDES_P_DIARIO (SOLICITUD_ID, PERSONA_DIARIO_ID) SELECT " + SOLICITUD_ID + " AS SOLICITUD_ID, PERSONA_DIARIO_ID FROM EC_PERSONAS_DIARIO WHERE PERSONA_DIARIO_ID IN (" + CeC_BD.ObtenParametroCadena(PersonasDiarioIds) + ")");
            return SOLICITUD_ID;
        }
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
    public static int Agrega(int UsuarioID, EstadoSolicitud EstadoSolicitud, TipoPrioridad TipoPrioridad, string Titulo, string Descripcion, string Extras,
        DateTime AntesDe, int PersonaID, string ExtraID, string PersonasDiarioIds, CeC_Sesion Sesion)
    {
        int SolicitudID = Agrega(EstadoSolicitud, TipoPrioridad, Titulo, Descripcion, Extras, AntesDe, PersonaID, ExtraID, PersonasDiarioIds, Sesion);
        if (SolicitudID > 0)
        {
            Liga(SolicitudID, UsuarioID, EstadoSolicitud, TipoPrioridad, Titulo, Descripcion, Extras, AntesDe, Sesion);
        }
        return SolicitudID;
    }

    public static bool Liga(int SolicitudID, int UsuarioID, EstadoSolicitud EstadoSolicitud, TipoPrioridad TipoPrioridad, string Titulo, string Descripcion, string Extras, DateTime AntesDe, CeC_Sesion Sesion)
    {
        string SolicitudUsuario = ObtenSolicitudUsuario(SolicitudID, UsuarioID);
        if (CeC_BD.EjecutaComando("INSERT INTO EC_SOLICITUDES_USUARIOS (SOLICITUD_ID, USUARIO_ID,SOLICITUD_USUARIO) VALUES(" + SolicitudID + ", " + UsuarioID + ",'" + SolicitudUsuario + "')") > 0)
        {
            CeC_Config Config = new CeC_Config(UsuarioID);
            if (Config.EnviarMailSolicitudIncidencia)
            {
                //CeC_Mails.EnviarMail(CeC_Usuarios.ObtenUsuarioeMail(UsuarioID), Titulo, Descripcion);
                Descripcion = Descripcion.Replace("@SolicitudUsuario", SolicitudUsuario);
                CeC_Mails.EnviaMensajeUsuario(UsuarioID, false, Titulo, Descripcion, null, Sesion, 0);

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
    public static int Agrega(string UsuariosIDs, EstadoSolicitud EstadoSolicitud, TipoPrioridad TipoPrioridad, string Titulo,
        string Descripcion, string Extras, DateTime AntesDe, int PersonaID, string ExtraID, string PersonasDiarioIds, CeC_Sesion Sesion)
    {
        int SolicitudID = Agrega(EstadoSolicitud, TipoPrioridad, Titulo, Descripcion, Extras, AntesDe, PersonaID, ExtraID, PersonasDiarioIds, Sesion);
        if (SolicitudID > 0)
        {
            string[] sUsuariosIDS = CeC.ObtenArregoSeparador(UsuariosIDs, ",");
            foreach (string sUsuarioID in sUsuariosIDS)
            {
                int UsuarioID = CeC.Convierte2Int(sUsuarioID);
                if (UsuarioID > 0)
                    Liga(SolicitudID, UsuarioID, EstadoSolicitud, TipoPrioridad, Titulo, Descripcion, Extras, AntesDe, Sesion);
            }
        }
        return SolicitudID;
    }

    public static string ObtenSolicitudUsuario(int SolicitudID, int UsuarioID)
    {
        string Resultado = SolicitudID.ToString() + "|" + UsuarioID.ToString() + "|";
        Resultado += MD5Core.GetHashString(Resultado);
        return Resultado;

    }

    public static string SaltoLineaHtml
    {
        get { return eClockBase.CeC.SaltoLineaHtml; }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="TipoIncidenciaID"></param>
    /// <param name="PersonasDiarioIds">Deberán pertenecer a un solo personaID</param>
    /// <param name="Comentario"></param>
    /// <param name="Sesion"></param>
    /// <returns></returns>
    public static bool SolicitaJustificacion(int TipoIncidenciaID, string PersonasDiarioIds, string Comentario, CeC_Sesion Sesion)
    {
        string PersonasIDs = CeC_Asistencias.ObtenPersonasIDs(PersonasDiarioIds);
        int PersonaID = CeC.Convierte2Int(PersonasIDs);
        if (PersonaID < 1)
            return false;
        string sUsuarios = CeC_Usuarios.ObtenJefes(PersonaID);

        string TipoIncidencia = Cec_Incidencias.ObtenTipoIncidenciaNombre(TipoIncidenciaID);
        string Nombre = CeC_BD.ObtenPersonaNombre(PersonaID);
        int PersonaLinkID = CeC_BD.ObtenPersonaLinkID(PersonaID);
        string Titulo = "Solicitud de " + TipoIncidencia + " por " + Nombre;
        string DescripcionConstante = "Solicitud de: " + TipoIncidencia + " " + SaltoLineaHtml + "Para: " + PersonaLinkID + "-" + Nombre +
            SaltoLineaHtml + " los días " + CeC_Asistencias.ObtenFechas(PersonasDiarioIds);
        DescripcionConstante += SaltoLineaHtml + "Comentario:" + Comentario;
        List<DateTime> Fechas = CeC_Asistencias.ObtenFechasList(PersonasDiarioIds);
        string[] saUsuarios = CeC.ObtenArregoSeparador(sUsuarios, ",");

        foreach (string sUsuario in saUsuarios)
        //for (int a=0; a <= 1; a++)
        {
            int iUsuario = CeC.Convierte2Int(sUsuario);
            DataSet DS = CeC_Asistencias.ObtenAsistenciaHorizontalDias(false, false, true, false, true, false, true, -1, CeC_Personas.ObtenAgrupacionNombre(PersonaID),
                Fechas, Sesion.Localizacion, Sesion, iUsuario);
            if (DS == null || DS.Tables.Count < 1)
                return false;
            string Descripcion = "<table style=\"width:100%;\">";
            Descripcion += "<tr>";

            Descripcion += "<td>Empleado</td>";
            int Dia = 0;
            foreach (DateTime Fecha in Fechas)
            {
                Descripcion += "<td>";
                Descripcion += Fecha.ToShortDateString();
                Descripcion += "</td>";
                Dia++;
            }
            Descripcion += "</tr>";

            //foreach (DataRow Filas in DS.Tables[0].Rows)
            //{
            //    Descripcion += "<tr>";
            //    string Persona = CeC.Convierte2String(Filas["NOMBRE"]) + "(" + CeC.Convierte2String(Filas["LINK"]) + ")";
            //    Descripcion += "<td>" + Persona + "</td>";
            //    Dia = 0;
            //    foreach (DateTime Fecha in Fechas)
            //    {
            //        Descripcion += "<td bgcolor=\"#" + CeC.Convierte2Int(Filas["IC" + Dia]).ToString("X").PadLeft(6, '0') + "\" >";
            //        Descripcion += CeC.Convierte2String(Filas["ABR" + Dia]);
            //        Descripcion += "</td>";
            //        Dia++;
            //    }
            //    Descripcion += "</tr>";
            //}
            Descripcion += "</table>";

            Descripcion += SaltoLineaHtml;
            Descripcion += "<table style=\"width:100%;\">";
            Descripcion += "<tr>";
            Descripcion += "<td>" + "<A HREF=\"" + CeC_Config.LinkURL + "Solicitudes.aspx?D=@SolicitudUsuario\">Denegar</A>" + "</td>";
            Descripcion += "<td>" + "<A HREF=\"" + CeC_Config.LinkURL + "Solicitudes.aspx?A=@SolicitudUsuario\">Autorizar</A>" + "</td>";
            Descripcion += "</tr>";
            Descripcion += "</table>";
            string Extras = "INCIDENCIA|" + TipoIncidenciaID + "|" + PersonasDiarioIds + "|" + Comentario;

            //Descripcion = Descripcion.Substring(0, 120);
            if (Agrega(sUsuario, EstadoSolicitud.Pendiente, TipoPrioridad.Normal, Titulo, DescripcionConstante + Descripcion, Extras, DateTime.Now,
                PersonaID, TipoIncidenciaID.ToString(), PersonasDiarioIds, Sesion) < 1)
                return false;
        }
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
    public static bool Autoriza(CeC_Sesion Sesion, string Extras)
    {
        string[] Parametros = CeC.ObtenArregoSeparador(Extras, "|");
        if (Parametros[0] == "INCIDENCIA")
        {
            string Comentario = "";
            if (Parametros.Length > 3)
                Comentario = Parametros[3];

            if (Cec_Incidencias.AsignaIncidencia(CeC.Convierte2Int(Parametros[1]), Parametros[2], Comentario, Sesion) > 0)
            {
                return true;
            }
        }
        return false;
    }
    public static int Existe(string SolicutudUsuario)
    {
        return CeC_BD.EjecutaEscalarInt("SELECT SOLICITUD_ID FROM EC_SOLICITUDES_USUARIOS WHERE SOLICITUD_USUARIO = '" + CeC_BD.ObtenParametroCadena(SolicutudUsuario) + "'");
    }

    public static DataSet ExisteTitulo(int SolicitudID)
    {
        return CeC_BD.EjecutaDataSet("SELECT SOLICITUD_DESC, PERSONA_ID FROM EC_SOLICITUDES WHERE SOLICITUD_ID = " + SolicitudID + "");
    }
    public static int Autoriza(Object Pagina, string SolicutudUsuario, bool Autorizar)
    {
        try
        {
            int SolicitudID = Existe(SolicutudUsuario);
            
            if (SolicitudID < 0)
                return -1;
            string[] Valores = CeC.ObtenArregoSeparador(SolicutudUsuario, "|");
            int UsuarioID = CeC.Convierte2Int(Valores[1]);
            if (UsuarioID <= 0)
                return -2;
            bool Res = false;
            int Estado = ObtenEstado(SolicitudID);
            if (Estado == 3 || Estado == 2)
                return -99;
            CeC_Sesion Sesion = CeC_Sesion.Nuevo(Pagina, UsuarioID);
            if (Autorizar)
            {

                CambiaEstado(SolicitudID, EstadoSolicitud.Autorizado, Sesion);
            }
            else
                CambiaEstado(SolicitudID, EstadoSolicitud.Denegado, Sesion);
            Sesion.CierraSesion();
            return 1;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return 0;
    }
    public static int Deniega(Object Pagina, string SolicutudUsuario)
    {
        return Autoriza(Pagina, SolicutudUsuario, false);
    }
    public static int ObtenEstado(int SolicitudID)
    {
        return CeC_BD.EjecutaEscalarInt("SELECT EDO_SOLICITUD_ID FROM EC_SOLICITUDES WHERE SOLICITUD_ID = " + SolicitudID);
    }

    public static string ObtenIDsSolicitudes(string SOLICITUD_DESC)
    {
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet("SELECT SOLICITUD_ID FROM EC_SOLICITUDES WHERE SOLICITUD_DESC = '" + SOLICITUD_DESC + "'");
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return "";
        string IDsSolicitudes = "";
        foreach (DataRow DR in DS.Tables[0].Rows)
        {
            IDsSolicitudes = CeC.AgregaSeparador(IDsSolicitudes, CeC.Convierte2String(DR["SOLICITUD_ID"]), ",");
        }

        return IDsSolicitudes;


    }
    public static bool CambiaEstado(int SolicitudID, EstadoSolicitud Estado, CeC_Sesion Sesion)
    {
        CeC_Solicitudes Sol = new CeC_Solicitudes(SolicitudID);

        string Solicitudes = ObtenIDsSolicitudes(Sol.SOLICITUD_DESC);

        if (Estado == EstadoSolicitud.Autorizado)
        {
            if (!Autoriza(Sesion, Sol.SOLICITUD_EXTRAS))
                return false;
        }

        int UsuarioID = CeC_Autonumerico.ObtenUsuarioID("EC_SOLICITUDES", "SOLICITUD_ID", SolicitudID);
        if (UsuarioID > 0)
        {
            string[] Parametros = CeC.ObtenArregoSeparador(Sol.SOLICITUD_EXTRAS, "|");

            try
            {
                string Titulo = "Cambio de estado de solicitud";
                if (Estado == EstadoSolicitud.Autorizado)
                    Titulo = "Se ha autorizado la " + Sol.SOLICITUD_TITULO;
                if (Estado == EstadoSolicitud.Denegado)
                    Titulo = "Se ha denegado la " + Sol.SOLICITUD_TITULO;

                string Dias = eClockBase.CeC.ObtenDiasTexto(eClockBase.CeC.PersonasDiarioIDs2Fechas(Parametros[2]));
                string Descripcion = "Se ha cambiado el estado de " + Sol.SOLICITUD_TITULO + SaltoLineaHtml + "Nuevo estado = "
                    + ObtenEstadoSolicitudNombre(Convert.ToInt32(Estado)) + SaltoLineaHtml + "Para los días: " + Dias;
                CeC_Mails.EnviaMensajeUsuario(UsuarioID, false, Titulo, Descripcion, null, Sesion);
            }
            catch (Exception ex)
            {
                CIsLog2.AgregaError(ex);
            }
            //CeC_Mails.EnviarMail(CeC_Usuarios.ObtenUsuarioeMail(UsuarioID), Titulo, Descripcion);
        }

        string Qry = "UPDATE EC_SOLICITUDES SET EDO_SOLICITUD_ID = " + Convert.ToInt32(Estado) + " WHERE SOLICITUD_ID in (" + Solicitudes + ")";
        if (CeC_BD.EjecutaComando(Qry) > 0)
            return true;
        return false;
    }
    public static DataSet ObtenDetalles(int UsuarioID)
    {
        return (DataSet)CeC_BD.EjecutaDataSet("SELECT EC_SOLICITUDES." + Campos + " FROM EC_SOLICITUDES,EC_SOLICITUDES_USUARIOS WHERE EC_SOLICITUDES.SOLICITUD_ID=EC_SOLICITUDES_USUARIOS.SOLICITUD_ID AND USUARIO_ID = " + UsuarioID +
            " AND EDO_SOLICITUD_ID < 2 ");
    }

    public static void Obten(int PersonaID, DateTime FechaIncial, DateTime FechaFinal)
    {

    }

    public static DataSet SolicitudVacaciones()
    {
        //return (DataSet)CeC_BD.EjecutaDataSet("SELECT DISTINCT (EC_SOLICITUDES.SOLICITUD_DESC), SOLICITUD_TITULO FROM EC_SOLICITUDES ");
        string Qry = "SELECT " +
        " EC_SOLICITUDES.SOLICITUD_FECHAHORA, EC_SOLICITUDES.SOLICITUD_TITULO, EC_SOLICITUDES.SOLICITUD_DESC, \n"+
        "EC_EDO_SOLICITUD.EDO_SOLICITUD_NOMBRE, EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_NOMBRE, EC_PERSONAS.PERSONA_NOMBRE, \n"+
        " EC_PERSONAS.PERSONA_LINK_ID, EC_PERSONAS.AGRUPACION_NOMBRE \n"+
        "FROM EC_SOLICITUDES INNER JOIN  EC_SOLICITUDES_P_DIARIO ON EC_SOLICITUDES.SOLICITUD_ID = EC_SOLICITUDES_P_DIARIO.SOLICITUD_ID \n"+
        "INNER JOIN EC_PERSONAS ON EC_SOLICITUDES.PERSONA_ID = EC_PERSONAS.PERSONA_ID INNER JOIN EC_PERSONAS_DIARIO ON \n"+
        "EC_PERSONAS.PERSONA_ID = EC_PERSONAS_DIARIO.PERSONA_ID AND EC_SOLICITUDES_P_DIARIO.PERSONA_DIARIO_ID = EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID\n"+
        " INNER JOIN EC_TIPO_PRIORIDADES ON EC_SOLICITUDES.TIPO_PRIORIDAD_ID = EC_TIPO_PRIORIDADES.TIPO_PRIORIDAD_ID INNER JOIN \n"+
        "EC_EDO_SOLICITUD ON EC_SOLICITUDES.EDO_SOLICITUD_ID = EC_EDO_SOLICITUD.EDO_SOLICITUD_ID INNER JOIN EC_TIPO_INCIDENCIAS\n"+
        " ON EC_SOLICITUDES.SOLICITUD_EXTRA_ID = EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_ID ORDER BY EC_SOLICITUDES.SOLICITUD_FECHAHORA DESC";
        
            return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }
}
