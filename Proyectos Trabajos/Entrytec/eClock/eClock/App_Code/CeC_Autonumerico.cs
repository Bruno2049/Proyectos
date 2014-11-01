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
        /// <summary>
        /// Obtiene el identificador único de la tabla seleccionada (índice)
        /// </summary>
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

        try
        {
            /*            IsProtectServer.CeT_PS PS = new IsProtectServer.CeT_PS();
                        PS.Directorio = HttpRuntime.AppDomainAppPath;
                        int version = Convert.ToInt32(PS.LlaveProducto.Substring(3, 1));
                        switch (NombreTabla)
                        {
                            ///el segundo grupo de 4 digitos en el No. del Producto es para los limites
                            ///el primero de empleados, el segundo de terminales y el tercero de usuarios
                            ///si el numero es 0 significa que no hay limite en personas
                            ///si el numero de usuarios o terminales es 0, depende si es version profesional o
                            ///empresarial, en profesional significa 1 y en empresarial significa ilimitado
                            case "EC_PERSONAS":
                                if (version == 3 && MaximoID >= 25)
                                    return -1;
                                int NoPersonas = Convert.ToInt32(PS.LlaveProducto.Substring(5, 1));
                                if (0 < (NoPersonas * 25) && (NoPersonas * 25) < MaximoID)
                                    return -1;
                                break;
                            case "EC_USUARIOS":
                                int NoUsuarios = Convert.ToInt32(PS.LlaveProducto.Substring(7, 1));
                                if (NoUsuarios == 0)
                                {
                                    if (version >= 2)
                                        return -1;
                                }
                                else if (NoUsuarios < MaximoID)
                                    return -1;
                                break;
                            case "EC_ACCESOS":
                                break;
                            case "EC_TERMINALES":
                                if (version == 2 || version == 3)
                                    return -1;
                                int NoTerminales = Convert.ToInt32(PS.LlaveProducto.Substring(6, 1));
                                if (NoTerminales == 0)
                                {
                                    if (version >= 2)
                                        return -1;
                                }
                                else if (NoTerminales < MaximoID)
                                    return -1;
                                break;
                        }
                        */
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        int Autonumerico = MaximoID + 1;
        string AComando = "INSERT INTO EC_AUTONUM (AUTONUM_TABLA, AUTONUM_CAMPO_ID, AUTONUM_TABLA_ID, SESION_ID, AUTONUM_FECHAHORA,SUSCRIPCION_ID) VALUES(" +
    "'" + NombreTabla + "', '" + NombreCampo + "',";
        string BComando;

        BComando = " , " + Sesion_ID + ", " + CeC_BD.QueryFechaHora + "," + Suscripcion_ID + ")";
        for (int Cont = 0; Cont < 20; Cont++)
            if (CeC_BD.EjecutaComando(AComando + Autonumerico + BComando) > 0)
            {
                break;
            }
            else
                Autonumerico++;

        return Autonumerico;

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
        ADD += " SELECT     EC_AUTONUM.AUTONUM_TABLA_ID AS " + NombreCampo + " " +
            "FROM         EC_AUTONUM  " +
            " WHERE     (EC_AUTONUM.AUTONUM_TABLA = '" + NombreTabla + "') AND (SUSCRIPCION_ID = " + SuscripcionID + "))";
        return ADD;
    }


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

        if(CeC_BD.EjecutaComando("UPDATE EC_AUTONUM SET SUSCRIPCION_ID = " + SuscripcionID + " WHERE AUTONUM_TABLA = '" + NombreTabla + "' AND AUTONUM_TABLA_ID = " + TablaID) >= 1)
            return true;
        return false;
    }
}
