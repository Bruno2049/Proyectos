using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Descripción breve de CeC_Agrupaciones
/// </summary>
public class CeC_Agrupaciones
{
    public static string AgregaPipe(string Agrupacion)
    {
        if(Agrupacion.Length <1)
            return "|";
        if (Agrupacion[Agrupacion.Length - 1] != '|')
            return Agrupacion += "|";
        return Agrupacion;
    }
    public static bool BorrarAgrupacion(int Suscripcion_ID, string Agrupacion, bool Forza)
    {
        bool Borrar = true;
        Agrupacion = AgregaPipe(Agrupacion);
        if (!Forza)
        {
            int ID = CeC_BD.EjecutaEscalarInt("SELECT SUSCRIPCION_ID FROM EC_AGRUPACIONES WHERE SUSCRIPCION_ID=" + Suscripcion_ID + " AND AGRUPACION_NOMBRE = '" + Agrupacion + "'");
            if (ID > 0)
                Borrar = true;
        }
        if(Borrar && CeC_BD.EjecutaComando("DELETE EC_AGRUPACIONES WHERE SUSCRIPCION_ID=" + Suscripcion_ID + " AND AGRUPACION_NOMBRE = '" + Agrupacion + "'")> 0)
            return true; ;
        return false;
    }
    public static bool AgregaAgrupacion(int Suscripcion_ID, string Agrupacion)
    {
        try
        {
            int Res = CeC_BD.EjecutaEscalarInt("SELECT SUSCRIPCION_ID FROM EC_AGRUPACIONES  WHERE SUSCRIPCION_ID = "
                + Suscripcion_ID + " AND AGRUPACION_NOMBRE ='" + Agrupacion + "'");
            if (Res > 0)
                return true;
            if (CeC_BD.EjecutaComando("INSERT INTO EC_AGRUPACIONES (SUSCRIPCION_ID, AGRUPACION_NOMBRE) VALUES(" + Suscripcion_ID + ", '" + Agrupacion + "')") > 0)
                return true;

        }
        catch { }
        return false;
    }
    /// <summary>
    /// Regenera agrupaciones apartir de una serie de campos separados por | pipe
    /// </summary>
    /// <param name="Campos"></param>
    /// <returns></returns>
    public static bool RegeneraAgrupaciones(int SUSCRIPCION_ID, string ListadoCampos, bool BorraAgrupaciones)
    {
        string[] Campos = ListadoCampos.Split(new char[] { '|' });
        string QryCampos = "''";

        foreach (string Campo in Campos)
        {
            if (QryCampos.Length > 0)
                QryCampos += " + '|' +";
            QryCampos += Campo;
            CeC_BD.EjecutaComando(" UPDATE EC_PERSONAS_DATOS SET " + Campo + " = '' WHERE " + Campo + " is null AND PERSONA_ID IN (SELECT PERSONA_ID FROM EC_PERSONAS WHERE SUSCRIPCION_ID = " + SUSCRIPCION_ID + ")");
        }
        QryCampos += "+ '|' ";
        if (CeC_BD.EsOracle)
            QryCampos = QryCampos.Replace("+", "||");

        string Sql = "UPDATE EC_PERSONAS SET AGRUPACION_NOMBRE = (SELECT " + QryCampos + " FROM EC_PERSONAS_DATOS " +
            " WHERE EC_PERSONAS.PERSONA_ID = EC_PERSONAS_DATOS.PERSONA_ID " +
            ") WHERE SUSCRIPCION_ID = " + SUSCRIPCION_ID;

        CeC_BD.EjecutaComando(Sql);
        CeC_BD.EjecutaComando("UPDATE EC_PERSONAS SET AGRUPACION_NOMBRE = '|Sin Agrupacion|' WHERE (AGRUPACION_NOMBRE is null or AGRUPACION_NOMBRE = '|' or AGRUPACION_NOMBRE = '||' or AGRUPACION_NOMBRE = '|||')"
            + " AND SUSCRIPCION_ID = " + SUSCRIPCION_ID);
        AutogeneraAgrupaciones(SUSCRIPCION_ID, BorraAgrupaciones);
        return true;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Usuario_ID"></param>
    /// <param name="BorraAgrupaciones"></param>
    /// <returns></returns>
    public static bool AutogeneraAgrupaciones(int SUSCRIPCION_ID, bool BorraAgrupaciones)
    {
        if (BorraAgrupaciones)
        {
            CeC_BD.EjecutaComando("DELETE EC_AGRUPACIONES WHERE SUSCRIPCION_ID = " + SUSCRIPCION_ID);
        }
        //CeC_Autonumerico.GeneraAutonumerico("EC_AGRUPACIONES", "");
        CeC_BD.EjecutaComando("INSERT INTO EC_AGRUPACIONES (SUSCRIPCION_ID, AGRUPACION_NOMBRE) " +
    "SELECT SUSCRIPCION_ID, AGRUPACION_NOMBRE  FROM EC_PERSONAS WHERE SUSCRIPCION_ID =" + SUSCRIPCION_ID + " AND AGRUPACION_NOMBRE NOT IN (" +
    " SELECT AGRUPACION_NOMBRE FROM EC_AGRUPACIONES WHERE SUSCRIPCION_ID =" + SUSCRIPCION_ID + ") GROUP BY SUSCRIPCION_ID, AGRUPACION_NOMBRE");

        return true;
    }
    public static int CambiaNombreAgrupacion(int Usuario_ID, string AgrupacionOrigen, string AgrupacionDestino)
    {
        int Modificados = 0;
        AgrupacionOrigen = AgregaPipe(AgrupacionOrigen);
        AgrupacionDestino = AgregaPipe(AgrupacionDestino);

        Modificados += CeC_BD.EjecutaComando("UPDATE EC_AGRUPACIONES SET AGRUPACION_NOMBRE = '" + CeC_BD.ObtenParametroCadena(AgrupacionDestino) + "' " +
            " WHERE SUSCRIPCION_ID IN (SELECT SUSCRIPCION_ID FROM EC_PERMISOS_SUSCRIP WHERE USUARIO_ID = " + Usuario_ID + ") " +
            " AND AGRUPACION_NOMBRE = '" + CeC_BD.ObtenParametroCadena(AgrupacionOrigen) + "' ");

        Modificados += CeC_BD.EjecutaComando("UPDATE EC_PERSONAS SET AGRUPACION_NOMBRE = '" + CeC_BD.ObtenParametroCadena(AgrupacionDestino) + "' " +
            " WHERE SUSCRIPCION_ID IN (SELECT SUSCRIPCION_ID FROM EC_PERMISOS_SUSCRIP WHERE USUARIO_ID = " + Usuario_ID + ") " +
            " AND AGRUPACION_NOMBRE = '" + CeC_BD.ObtenParametroCadena(AgrupacionOrigen) + "' ");
        int Len = AgrupacionOrigen.Length + 1;
        Modificados += CeC_BD.EjecutaComando("UPDATE EC_AGRUPACIONES SET AGRUPACION_NOMBRE = '" + CeC_BD.ObtenParametroCadena(AgrupacionDestino) + "' " +
            " + SUBSTRING(AGRUPACION_NOMBRE," + Len + ",10000) " +
            " WHERE SUSCRIPCION_ID IN (SELECT SUSCRIPCION_ID FROM EC_PERMISOS_SUSCRIP WHERE USUARIO_ID = " + Usuario_ID + ") " +
            " AND AGRUPACION_NOMBRE like '" + CeC_BD.ObtenParametroCadena(AgrupacionOrigen) + "%' ");
        Modificados += CeC_BD.EjecutaComando("UPDATE EC_PERSONAS SET AGRUPACION_NOMBRE = '" + CeC_BD.ObtenParametroCadena(AgrupacionDestino) + "' " +
            " + SUBSTRING(AGRUPACION_NOMBRE," + Len + ",10000) " +
            " WHERE SUSCRIPCION_ID IN (SELECT SUSCRIPCION_ID FROM EC_PERMISOS_SUSCRIP WHERE USUARIO_ID = " + Usuario_ID + ") " +
            " AND AGRUPACION_NOMBRE like '" + CeC_BD.ObtenParametroCadena(AgrupacionOrigen) + "%' ");
        return Modificados;
        // "UPDATE  AGRUPACION = " +CeC_BD.ObtenParametroCadena(AgrupacionOrigen) + " WHERE
    }

    public static DataSet ObtenPermisosUsuarios(int Suscripcion_ID, string Agrupacion)
    {
        string QryAgrupacion = "";
        string[] Valores = Agrupacion.Split(new char[] { '|' });
        string Agr = "|";
        for (int Cont = 1; Cont < Valores.Length - 1; Cont++)
        {
            Agr += Valores[Cont] + "|";
            if (QryAgrupacion.Length > 0)
                QryAgrupacion += " OR ";
            QryAgrupacion += " EC_USUARIOS_PERMISOS.USUARIO_PERMISO = '" + CeC_BD.ObtenParametroCadena(Agr) + "' ";
        }
        string Qry = "SELECT USUARIO_PERMISO_ID, EC_USUARIOS.USUARIO_USUARIO, EC_USUARIOS.USUARIO_NOMBRE,  " +
                      "EC_USUARIOS_PERMISOS.TIPO_PERMISO_ID, EC_USUARIOS_PERMISOS.USUARIO_PERMISO " +
                      "FROM EC_USUARIOS, EC_USUARIOS_PERMISOS WHERE " +
                      "EC_USUARIOS.USUARIO_ID = EC_USUARIOS_PERMISOS.USUARIO_ID AND EC_USUARIOS_PERMISOS.SUSCRIPCION_ID = " +
                      Suscripcion_ID + " AND (" + QryAgrupacion + ") " +
                      " ORDER BY EC_USUARIOS_PERMISOS.USUARIO_PERMISO,EC_USUARIOS.USUARIO_USUARIO";
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }
    public static bool QuitaPermisoUsuario(int Suscripcion_ID, int Usuario_ID, string Agrupacion)
    {
        int ID = CeC_BD.EjecutaEscalarInt("SELECT USUARIO_PERMISO_ID FROM EC_USUARIOS_PERMISOS WHERE USUARIO_ID = " + Usuario_ID +
    " AND SUSCRIPCION_ID = " + Suscripcion_ID + " AND USUARIO_PERMISO = '" + Agrupacion + "'");
        return QuitaPermisoUsuario(ID);
    }

    public static bool QuitaPermisoUsuario(int UsuarioPermisoID)
    {
        if (CeC_BD.EjecutaComando("DELETE EC_USUARIOS_PERMISOS WHERE USUARIO_PERMISO_ID = " + UsuarioPermisoID) > 0)
            return true;
        return false;
    }

    public static bool AsignaPermiso(int Sesion_ID, int Suscripcion_ID, int Usuario_ID, string Agrupacion, int TipoPermisoID)
    {
        int ID = CeC_BD.EjecutaEscalarInt("SELECT USUARIO_PERMISO_ID FROM EC_USUARIOS_PERMISOS WHERE USUARIO_ID = " + Usuario_ID +
            " AND SUSCRIPCION_ID = " + Suscripcion_ID + " AND USUARIO_PERMISO = '" + Agrupacion + "'");
        if (ID > 0)
        {
            CeC_BD.EjecutaComando("UPDATE EC_USUARIOS_PERMISOS SET TIPO_PERMISO_ID = " + TipoPermisoID + " WHERE USUARIO_PERMISO_ID = " + ID);
            return true;
        }

        ID = CeC_Autonumerico.GeneraAutonumerico("EC_USUARIOS_PERMISOS", "USUARIO_PERMISO_ID", Sesion_ID, Suscripcion_ID);

        string Qry = "INSERT INTO EC_USUARIOS_PERMISOS (USUARIO_PERMISO_ID,USUARIO_ID, SUSCRIPCION_ID, TIPO_PERMISO_ID, USUARIO_PERMISO) VALUES("
             + ID + ", " + Usuario_ID + ", " + Suscripcion_ID + ", " + TipoPermisoID + ",'" + Agrupacion + "')";
        if (CeC_BD.EjecutaComando(Qry) > 0)
            return true;
        return false;
    }
    public CeC_Agrupaciones()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    /// <summary>
    /// Obtiene las agrupaciones que sole el usuario tiene derecho a visualizar
    /// </summary>
    /// <param name="UsuarioID"></param>
    /// <returns></returns>
    public static DataSet ObtenAgrupaciones(int UsuarioID)
    {
        string Qry1 = "SELECT     EC_AGRUPACIONES.AGRUPACION_NOMBRE " +
                        "FROM         EC_USUARIOS_PERMISOS INNER JOIN " +
                        "EC_AGRUPACIONES ON EC_USUARIOS_PERMISOS.SUSCRIPCION_ID = EC_AGRUPACIONES.SUSCRIPCION_ID " +
                        "WHERE     (EC_USUARIOS_PERMISOS.USUARIO_ID = "+UsuarioID+") AND  " +
                        "(EC_AGRUPACIONES.AGRUPACION_NOMBRE LIKE EC_USUARIOS_PERMISOS.USUARIO_PERMISO + '%') ";
        if(CeC_BD.EsOracle)
            Qry1 = "SELECT     EC_AGRUPACIONES.AGRUPACION_NOMBRE " +
                        "FROM         EC_USUARIOS_PERMISOS INNER JOIN " +
                        "EC_AGRUPACIONES ON EC_USUARIOS_PERMISOS.SUSCRIPCION_ID = EC_AGRUPACIONES.SUSCRIPCION_ID " +
                        "WHERE     (EC_USUARIOS_PERMISOS.USUARIO_ID = " + UsuarioID + ") AND  " +
                        "(EC_AGRUPACIONES.AGRUPACION_NOMBRE LIKE EC_USUARIOS_PERMISOS.USUARIO_PERMISO || '%') ";
        string Qry0 = "SELECT     AGRUPACION_NOMBRE " +
"FROM         EC_AGRUPACIONES " +
"WHERE     SUSCRIPCION_ID IN " +
                          "(SELECT SUSCRIPCION_ID FROM EC_USUARIOS WHERE USUARIO_ID = " + UsuarioID + 
                          ") AND  AGRUPACION_NOMBRE in ( " + Qry1 + ") " +
                            "GROUP BY AGRUPACION_NOMBRE ORDER BY AGRUPACION_NOMBRE";
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry0);
//        DS.Tables[0].row
        return DS;

    }
    public static string ObtenSQLPersonaIDsPermisos(int UsuarioID)
    {
        return ObtenSQLPersonaIDsPermisos(UsuarioID, false);
    }
    public static string ObtenSQLPersonaIDsPermisos(int UsuarioID, bool MostrarBorrados)
    {
        string NoMostrarBorrados = " AND PERSONA_BORRADO = 0 ";
        if (MostrarBorrados)
            NoMostrarBorrados = ""; 
        string Qry = "SELECT PERSONA_ID FROM EC_PERSONAS, EC_USUARIOS_PERMISOS WHERE USUARIO_ID = " + UsuarioID +
            " AND EC_PERSONAS.SUSCRIPCION_ID = EC_USUARIOS_PERMISOS.SUSCRIPCION_ID AND " +
            " AGRUPACION_NOMBRE LIKE USUARIO_PERMISO+'%' " + NoMostrarBorrados;
        if (CeC_BD.EsOracle)
            Qry = Qry.Replace("+", "||");
        return Qry;
    }
}
