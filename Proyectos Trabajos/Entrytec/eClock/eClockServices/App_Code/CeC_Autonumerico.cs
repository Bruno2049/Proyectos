using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Descripción breve de CeC_Autonumerico
/// </summary>
public class CeC_Autonumerico
{
    public CeC_Autonumerico()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    /// <summary>
    /// Genera un numero autonumerico. Si regresa menor o igual que 0 significa que no se pudo
    /// generar dicho autonumérico 
    /// </summary>
    /// <param name="NombreTabla">Nombre de la Tabla</param>
    /// <param name="NombreCampo">Nombre del Campo</param>
    /// <returns>Regresa el autonumérico mayor que cero si es correcto</returns>
    public static int GeneraAutonumerico(string NombreTabla, string NombreCampo)
    {
        return GeneraAutonumerico(NombreTabla, NombreCampo, 0, 0);
    }

    public static int InsertaAutonumerico(string NombreTabla, string NombreCampo, int Autonumerico, CeC_Sesion Sesion)
    {
        if (Sesion == null)
            return InsertaAutonumerico(NombreTabla, NombreCampo, Autonumerico, 0, 0);
        return InsertaAutonumerico(NombreTabla, NombreCampo, Autonumerico, Sesion.SESION_ID, Sesion.SUSCRIPCION_ID);
    }

    public static int InsertaAutonumerico(string NombreTabla, string NombreCampo, int Autonumerico, int Sesion_ID, int Suscripcion_ID)
    {
        string AComando = "INSERT INTO EC_AUTONUM (AUTONUM_TABLA, AUTONUM_CAMPO_ID, AUTONUM_TABLA_ID, SESION_ID, AUTONUM_FECHAHORA,SUSCRIPCION_ID) VALUES(" +
        "'" + NombreTabla + "', '" + NombreCampo + "',";
        string BComando;
        BComando = " , " + Sesion_ID + ", " + CeC_BD.QueryFechaHora + "," + Suscripcion_ID + ")";
        return CeC_BD.EjecutaComando(AComando + Autonumerico + BComando);

    }

    /// <summary>
    /// Genera un numero autonumerico. Si regresa menor o igual que 0 significa que no se pudo
    /// generar dicho autonumérico
    /// </summary>
    /// <param name="NombreTabla">Nombre de la tabla</param>
    /// <param name="NombreCampo">Nombre del campo</param>
    /// <param name="Sesion_ID">Identificador de la sesión</param>
    /// <param name="Suscripcion_ID">Identificador de la suscripción</param>
    /// <returns>Regresa el autonumérico mayor que cero si es correcto</returns>
    public static int GeneraAutonumerico(string NombreTabla, string NombreCampo, int Sesion_ID, int Suscripcion_ID)
    {
        for (int Cont = 0; Cont < 20; Cont++)
        {
            /*            if (CeC_BD.EsOracle)
                        {
              */
            int MaximoID = -1;

            MaximoID = CeC_BD.EjecutaEscalarInt("SELECT  MAX(AUTONUM_TABLA_ID) FROM EC_AUTONUM WHERE AUTONUM_TABLA = '"
                + NombreTabla + "' AND AUTONUM_CAMPO_ID = '" + NombreCampo + "'");
            //        if (MaximoID <= 0)
            {
                int NMax = 0;
                NMax = CeC_BD.EjecutaEscalarInt("SELECT MAX(" + NombreCampo + ") FROM " + NombreTabla);
                if (NMax < 0)
                    NMax = 0;
                if (MaximoID < NMax)
                    MaximoID = NMax;
            }
            int Autonumerico = MaximoID + 1;

            if (InsertaAutonumerico(NombreTabla, NombreCampo, Autonumerico, Sesion_ID, Suscripcion_ID) > 0)
            {
                return Autonumerico;
            }

        }
        return -1;
    }

    /// <summary>
    /// Genera un numero autonumerico. Si regresa menor o igual que 0 significa que no se pudo
    /// generar dicho autonumérico
    /// </summary>
    /// <param name="NombreTabla">Nombre de la tabla</param>
    /// <param name="NombreCampo">Nombre del campo</param>
    /// <returns>Mayor que cero si es correcto</returns>    
    public static int GeneraAutonumerico(string NombreTabla, string NombreCampo, CeC_Sesion Sesion)
    {
        if (Sesion == null)
            return GeneraAutonumerico(NombreTabla, NombreCampo);
        return GeneraAutonumerico(NombreTabla, NombreCampo, Sesion.SESION_ID, Sesion.SUSCRIPCION_ID);
    }

    /// <summary>
    /// Obtiene el identificador de el usuario de la sesión actual
    /// </summary>
    /// <param name="NombreTabla">Nombre de la tabla</param>
    /// <param name="NombreCampo">Nombre del campo</param>
    /// <param name="ID"></param>
    /// <returns></returns>
    public static int ObtenUsuarioID(string NombreTabla, string NombreCampo, int ID)
    {
        return CeC_BD.EjecutaEscalarInt("SELECT USUARIO_ID FROM EC_AUTONUM,EC_SESIONES WHERE EC_AUTONUM.SESION_ID = EC_SESIONES.SESION_ID AND AUTONUM_TABLA ='" + NombreTabla + "' AND AUTONUM_CAMPO_ID = '" + NombreCampo + "' AND AUTONUM_TABLA_ID = " + ID);
    }
    /// <summary>
    /// Obtiene un Qry para validar la suscripcion de un registro
    /// </summary>
    /// <param name="NombreTabla"></param>
    /// <param name="NombreCampo"></param>
    /// <param name="SuscripcionID"></param>
    /// <returns>Sentencia NombreCampo in </returns>
    public static string ValidaSuscripcion(string NombreTabla, string NombreCampo, int SuscripcionID)
    {
        string ADD = "";
        ADD = NombreCampo + " IN (";
        if (NombreCampo != "")
        {
            ADD += " SELECT     EC_AUTONUM.AUTONUM_TABLA_ID AS " + NombreCampo + " " +
                "FROM         EC_AUTONUM  " +
                " WHERE     (EC_AUTONUM.AUTONUM_TABLA = '" + NombreTabla + "') AND (SUSCRIPCION_ID = " + SuscripcionID + "))";
        }
        else
        {
            ADD += " SELECT     EC_AUTONUM.AUTONUM_TABLA_ID " +
                "FROM         EC_AUTONUM  " +
                " WHERE     (EC_AUTONUM.AUTONUM_TABLA = '" + NombreTabla + "') AND (SUSCRIPCION_ID = " + SuscripcionID + "))";
        }
        return ADD;
    }

    /// <summary>
    /// Valida el usuario, consultando si el ID de usuario se encuentra
    /// dentro de la base de datos.
    /// </summary>
    /// <param name="NombreTabla"></param>
    /// <param name="NombreCampo"></param>
    /// <param name="UsuarioID"></param>
    /// <returns>Regresa el usuario que se encuentra dentro de la BD</returns>
    public static string ValidaUsuarioID(string NombreTabla, string NombreCampo, int UsuarioID)
    {
        string ADD = "";
        ADD = NombreCampo + " IN (";
        ADD += " SELECT EC_AUTONUM.AUTONUM_TABLA_ID AS " + NombreCampo + " " +
            "FROM EC_AUTONUM INNER JOIN " +
                " EC_PERMISOS_SUSCRIP ON EC_AUTONUM.SUSCRIPCION_ID = EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID" +
                " WHERE EC_AUTONUM.AUTONUM_TABLA = '" + NombreTabla + "' AND EC_PERMISOS_SUSCRIP.USUARIO_ID = " + UsuarioID + ")";
        return ADD;
    }
    /// <summary>
    /// Obtiene la suscripción del usuario actual
    /// </summary>
    /// <param name="NombreTabla">Nombre de la tabla a la que se le generará el autonumérico</param>
    /// <param name="TablaID">Valor del autonumérico</param>
    /// <returns>ID de la suscripción del usuario actual</returns>
    public static int ObtenSuscripcionID(string NombreTabla, int TablaID)
    {
        return CeC_BD.EjecutaEscalarInt("SELECT SUSCRIPCION_ID FROM EC_AUTONUM WHERE AUTONUM_TABLA = '" + NombreTabla + "' AND AUTONUM_TABLA_ID = " + TablaID);
    }

    /// <summary>
    /// Asigna una suscripción a el elemento que se esta editando
    /// </summary>
    /// <param name="NombreTabla">Nombre de la tabla a la que se le generará el autonumérico</param>
    /// <param name="TablaID">Valor del autonumérico</param>
    /// <param name="SuscripcionID">Indica que suscripcion agrego el registro</param>
    /// <returns>Verdadero si se puedo agregar correctamente la suscripción. Falso si no se pudieron actualizar los datos</returns>
    public static bool AsignaSuscripcionID(string NombreTabla, int TablaID, int SuscripcionID)
    {

        if (CeC_BD.EjecutaComando("UPDATE EC_AUTONUM SET SUSCRIPCION_ID = " + SuscripcionID + " WHERE AUTONUM_TABLA = '" + NombreTabla + "' AND AUTONUM_TABLA_ID = " + TablaID) >= 1)
            return true;
        return false;
    }

    public static bool Actualiza(string NombreTabla, string NombreCampo, int TablaID, CeC_Sesion Sesion)
    {
        string QrySesion = "";
        if (Sesion != null)
            QrySesion = ", SESION_ID_M = " + Sesion.SESION_ID;
        string QryActualiza = "UPDATE EC_AUTONUM SET AUTONUM_FECHAHORAM = " + CeC_BD.QueryFechaHora + QrySesion + "  WHERE AUTONUM_TABLA = '" + NombreTabla + "' AND AUTONUM_TABLA_ID = " + TablaID;
        if (CeC_BD.EjecutaComando(QryActualiza) > 0)
            return true;
        if (InsertaAutonumerico(NombreTabla, NombreCampo, TablaID, Sesion) > 0)
            if (CeC_BD.EjecutaComando(QryActualiza) > 0)
                return true;
        return false;
    }

    public static bool Borra(string NombreTabla, string NombreCampo, int TablaID, CeC_Sesion Sesion)
    {
        string QrySesion = "";
        if (Sesion != null)
            QrySesion = ", SESION_ID_M = " + Sesion.SESION_ID;
        string Qry = "UPDATE EC_AUTONUM SET AUTONUM_FECHAHORAB = " + CeC_BD.QueryFechaHora + QrySesion + ", AUTONUM_BORRADO = 1  WHERE AUTONUM_TABLA = '" + NombreTabla + "' AND AUTONUM_TABLA_ID = " + TablaID;
        if (CeC_BD.EjecutaComando(Qry) >= 1)
            return true;
        if (InsertaAutonumerico(NombreTabla, NombreCampo, TablaID, Sesion) > 0)
            if (CeC_BD.EjecutaComando(Qry) > 0)
                return true;
        return false;
    }

    public static DateTime ObtenFechaModificacion(string NombreTabla, string NombreCampo, int TablaID, CeC_Sesion Sesion)
    {
        DateTime FechaHora = CeC_BD.EjecutaEscalarDateTime("SELECT AUTONUM_FECHAHORAM FROM EC_AUTONUM WHERE AUTONUM_TABLA = '" + NombreTabla + "' AND AUTONUM_TABLA_ID = " + TablaID);
        return FechaHora;
    }
    public static int ObtenNoCambios(string NombreTabla, bool ValidaSuscripcion, DateTime UltimaConsulta, CeC_Sesion Sesion)
    {
        return ObtenNoCambios(NombreTabla, "*", ValidaSuscripcion, UltimaConsulta, Sesion);
    }
    public static int ObtenNoCambios(string NombreTabla, string NombreCampo, bool ValidaSuscripcion, DateTime UltimaConsulta, CeC_Sesion Sesion)
    {
        string Suscripcion = "";
        if (ValidaSuscripcion)
            Suscripcion = " AND SUSCRIPCION_ID = " + Sesion.SUSCRIPCION_ID;
        string sUltimaConsulta = CeC_BD.SqlFechaHora(UltimaConsulta);
        return CeC_BD.EjecutaEscalarInt("SELECT COUNT(*) FROM EC_AUTONUM WHERE AUTONUM_TABLA = '" + NombreTabla + "' AND (" +
            " AUTONUM_FECHAHORA > " + sUltimaConsulta + " OR AUTONUM_FECHAHORAM > " + sUltimaConsulta + ")" +
            Suscripcion);
    }
}
