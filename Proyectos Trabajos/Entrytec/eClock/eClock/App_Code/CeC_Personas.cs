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
/// Descripción breve de CeC_Personas
/// </summary>
public class CeC_Personas
{
    public enum TipoImagen : int
    {
        No_definido = 0,
        Foto = 1,
        Firma = 2,
        Huella = 3,
        Documento = 4

    }

    public int Sesion_ID = 0;
    public static bool Borra(int Persona_ID)
    {
        return Borra(Persona_ID, -1);
    }
    public static bool Borra(int Persona_ID, int Suscripcion_ID)
    {
        string ValidaSuscripcion = " ";
        if (Suscripcion_ID > 0)
            ValidaSuscripcion = " AND SUSCRIPCION_ID = " + Suscripcion_ID;
        if (CeC_BD.EjecutaComando("UPDATE EC_PERSONAS SET PERSONA_BORRADO = 1 WHERE PERSONA_ID = " + Persona_ID + ValidaSuscripcion) > 0)
            return true;
        return false;
    }
    /// <summary>
    /// Da de baja a los empleados activos que tengan fecha de baja Menor o igual a la fecha de la base de datos
    /// </summary>
    /// <returns></returns>
    public static bool Borra()
    {
        if (CeC_BD.EjecutaComando("UPDATE EC_PERSONAS SET PERSONA_BORRADO = 1 WHERE PERSONA_ID IN (SELECT PERSONA_ID FROM EC_PERSONAS_DATOS WHERE FECHA_BAJA <=" + CeC_BD.SqlFecha(DateTime.Now) + " )") > 0)
            return true;
        return false;
    }
    public static int Agrega(int Persona_Link_ID, int Tipo_Persona, int Suscripcion_ID)
    {
        return Agrega(Persona_Link_ID, Tipo_Persona, Suscripcion_ID, "");
    }
    public static int Agrega(int Persona_Link_ID, int Tipo_Persona, int Suscripcion_ID, string Agrupacion)
    {
        return Agrega(Persona_Link_ID, Tipo_Persona, Suscripcion_ID, Agrupacion, 0);
    }
    public static int Agrega(int Persona_Link_ID, int Tipo_Persona, int Suscripcion_ID, string Agrupacion, int Sesion_ID)
    {
        int Persona_ID = CeC_Autonumerico.GeneraAutonumerico("EC_PERSONAS", "PERSONA_ID", Sesion_ID, Suscripcion_ID);
        return Agrega(Persona_ID, Persona_Link_ID, Tipo_Persona, Suscripcion_ID, Agrupacion);
    }
    public static int Agrega(int Persona_ID, int Persona_Link_ID, int Tipo_Persona, int Suscripcion_ID, string Agrupacion)
    {
        int ID = ObtenPersonaIDBySuscripcion(Persona_Link_ID, Suscripcion_ID);
        if (ID > 0)
            return ID;
        if (Agrupacion == "")
            Agrupacion = "|Sin Agrupacion|";
        int R = CeC_BD.EjecutaComando("INSERT INTO EC_PERSONAS (PERSONA_ID, PERSONA_LINK_ID, TIPO_PERSONA_ID, SUSCRIPCION_ID,AGRUPACION_NOMBRE) VALUES("
    + Persona_ID + ", " + Persona_Link_ID + ", " + Tipo_Persona + ", " + Suscripcion_ID + ",'" + Agrupacion + "')");
        if (R > 0)
        {
            CeC_Asistencias.GeneraPrevioPersonaDiario(Persona_ID);
            CeC_BD.EjecutaComando("INSERT INTO EC_PERSONAS_DATOS (PERSONA_ID, PERSONA_LINK_ID) VALUES(" + Persona_ID + ", " + Persona_Link_ID + ")");

            CeC_BD.AsignaTerminalAuto(Persona_ID);
            return Persona_ID;

        }
        return -1;
    }
    public static int Agrega(int Persona_Link_ID, int Tipo_Persona, CeC_Sesion Sesion)
    {
        int Persona_ID = CeC_Autonumerico.GeneraAutonumerico("EC_PERSONAS", "PERSONA_ID", Sesion);
        return Agrega(Persona_ID, Persona_Link_ID, Tipo_Persona, Sesion.SUSCRIPCION_ID, "|Sin Agrupacion|");
    }

    /*
    //Se recomienda usar la otra funcion
    public int Agrega(int Persona_Link_ID, int Tipo_Persona, int Suscripcion_ID)
    {
        int Persona_ID = CeC_Autonumerico.GeneraAutonumerico("EC_PERSONAS", "PERSONA_ID");
        int R = CeC_BD.EjecutaComando("INSERT INTO EC_PERSONAS (PERSONA_ID, PERSONA_LINK_ID, TIPO_PERSONA_ID, SUSCRIPCION_ID) VALUES("
            + Persona_ID + ", " + Persona_Link_ID + ", " + Tipo_Persona + ", " + Suscripcion_ID + ")");
        if (R > 0)
            return Persona_ID;
        return -1;
    }*/

    public static string ObtenValorCampo(int PersonaID, string Campo_Nombre)
    {
        return CeC_Campos.ObtenValorCampo(PersonaID, Campo_Nombre);
    }

    public static bool AsignaValorDato(int Persona_ID, string CAMPO_NOMBRE, int SESION_ID, string Dato, DateTime FechaHora)
    {
        int R = CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_DATO SET PERSONA_DATO = '" + ObtenSoloCampo(Dato) + "', SESION_ID = " + SESION_ID + ", PERSONA_DATO_FECHA = " + CeC_BD.SqlFechaHora(FechaHora) + " WHERE PERSONA_ID = " +
            Persona_ID + " AND CAMPO_NOMBRE = '" + ObtenSoloCampo(CAMPO_NOMBRE) + "'");
        if (R < 1)
        {
            int PERSONA_DATO_ID = CeC_Autonumerico.GeneraAutonumerico("EC_PERSONAS_DATO", "PERSONA_DATO_ID", SESION_ID, 0);
            R = CeC_BD.EjecutaComando("INSERT INTO EC_PERSONAS_DATO  (PERSONA_DATO_ID,PERSONA_ID,CAMPO_NOMBRE,SESION_ID, PERSONA_DATO,PERSONA_DATO_FECHA) VALUES("
                + PERSONA_DATO_ID + "," + Persona_ID + ",'" + ObtenSoloCampo(CAMPO_NOMBRE) + "'," + SESION_ID + ",'" + ObtenSoloCampo(Dato) + "', " + CeC_BD.SqlFechaHora(FechaHora) + ")");
            if (R <= 0)
                return false;
        }
        return true;
    }

    public static int AgregaHistorialValor(int PERSONA_ID, string CAMPO_NOMBRE, int SESION_ID, string PERSONA_HIST_VAL, DateTime PERSONA_HIST_FECHA, DateTime PERSONA_HIST_FECHA_D, DateTime PERSONA_HIST_FECHA_H, string PERSONA_HIST_COM)
    {
        AsignaValorDato(PERSONA_ID, CAMPO_NOMBRE, SESION_ID, PERSONA_HIST_VAL, PERSONA_HIST_FECHA);
        int PERSONA_HIST_ID = CeC_Autonumerico.GeneraAutonumerico("EC_PERSONAS_HIST", "PERSONA_HIST_ID", SESION_ID, 0);
        if (CeC_BD.EjecutaComando("INSERT INTO EC_PERSONAS_HIST (PERSONA_HIST_ID, PERSONA_ID, CAMPO_NOMBRE, " +
            "SESION_ID, PERSONA_HIST_VAL, PERSONA_HIST_FECHA, " +
            "PERSONA_HIST_FECHA_D, PERSONA_HIST_FECHA_H, PERSONA_HIST_COM) VALUES(" +
            PERSONA_HIST_ID + ", " + PERSONA_ID + ", '" + ObtenSoloCampo(CAMPO_NOMBRE) + "', " +
            SESION_ID + ", '" + PERSONA_HIST_VAL + "', " + CeC_BD.SqlFecha(PERSONA_HIST_FECHA) + ", " +
            CeC_BD.SqlFecha(PERSONA_HIST_FECHA_D) + ", " + CeC_BD.SqlFecha(PERSONA_HIST_FECHA_H) + ", '" + PERSONA_HIST_COM + "')") > 0)
            return PERSONA_HIST_ID;
        return -1;
    }
    public static int AgregaHistorialValor(int PERSONA_ID, string CAMPO_NOMBRE, int SESION_ID, string PERSONA_HIST_VAL, DateTime PERSONA_HIST_FECHA)
    {
        return AgregaHistorialValor(PERSONA_ID, CAMPO_NOMBRE, SESION_ID, PERSONA_HIST_VAL, PERSONA_HIST_FECHA, CeC_BD.FechaNula, CeC_BD.FechaNula, "");
    }
    public static int CambiaPersonaLinkID(int Persona_ID, int Persona_Link_IDNuevo)
    {
        return CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_DATOS SET PERSONA_LINK_ID = " + Persona_Link_IDNuevo + " WHERE PERSONA_ID = " + Persona_ID) +
            CeC_BD.EjecutaComando("UPDATE EC_PERSONAS SET PERSONA_LINK_ID = " + Persona_Link_IDNuevo + " WHERE PERSONA_ID = " + Persona_ID)
            ;
    }
    public static string ObtenSoloCampo(string Campo)
    {
        string SoloCampo = Campo;
        int PosPunto = Campo.IndexOf('.');
        if (PosPunto > 0)
            SoloCampo = Campo.Substring(PosPunto + 1);
        return SoloCampo;
    }
    public static bool GuardaValor(int Persona_ID, string Campo, object Valor, int SesionID)
    {
        string SoloCampo = ObtenSoloCampo(Campo);

        if (SoloCampo == "TURNO_ID")
        {
            Valor = CeC.Convierte2Int(Valor, 0);
        }

        string ValorAnterior = ObtenValorCampo(Persona_ID, SoloCampo);
        try
        {
            string ValorNuevo = Convert.ToString(Valor);
            if (ValorAnterior != ValorNuevo)
            {
                AgregaHistorialValor(Persona_ID, Campo, SesionID, ValorNuevo, DateTime.Now);
            }
            else
                return true;
        }
        catch { }
        if (SoloCampo == "PERSONA_LINK_ID")
        {
            if (CambiaPersonaLinkID(Persona_ID, Convert.ToInt32(Valor)) > 0)
                return true;
            return false;
        }
        if (SoloCampo == "NOMBRE_COMPLETO")
        {
            CeC_Campos.GuardaValor("PERSONA_NOMBRE", Persona_ID, Valor, SesionID);
        }
        if (SoloCampo == "FECHA_BAJA")
        {
            DateTime FechaBaja = CeC.Convierte2DateTime(Valor);
            if (FechaBaja != CeC_BD.FechaNula && FechaBaja.Date <= DateTime.Now.Date)
                Borra(Persona_ID);
        }
        return CeC_Campos.GuardaValor(Campo, Persona_ID, Valor, SesionID);
    }
    /// <summary>
    /// Obtiene el numero de personas sin turno asignado
    /// </summary>
    /// <param name="Suscripcion_ID"></param>
    /// <returns></returns>
    public static int ObtenSinTurnoNo(int Usuario_ID)
    {
        return CeC_Turnos.ObtenNoPersonasSinTurno(Usuario_ID);
    }
    /// <summary>
    /// Obtiene un qry que obtiene las personas IDS que pertenecen a un usuario y una suscripcion
    /// </summary>
    /// <param name="Usuario_ID"></param>
    /// <param name="Suscripcion_ID"></param>
    /// <returns></returns>
    public static string ObtenQryPersonasdeUsuario(int Usuario_ID, int Suscripcion_ID)
    {
        string Qry = "SELECT        EC_PERSONAS.PERSONA_ID " +
        "FROM            EC_PERSONAS INNER JOIN " +
        "EC_USUARIOS_PERMISOS ON EC_PERSONAS.SUSCRIPCION_ID = EC_USUARIOS_PERMISOS.SUSCRIPCION_ID " +
        "WHERE        (EC_PERSONAS.PERSONA_BORRADO = 0) AND ";
        if (!CeC_BD.EsOracle)
            Qry += "(EC_PERSONAS.AGRUPACION_NOMBRE LIKE EC_USUARIOS_PERMISOS.USUARIO_PERMISO + '%') ";
        else
            Qry += "(EC_PERSONAS.AGRUPACION_NOMBRE LIKE EC_USUARIOS_PERMISOS.USUARIO_PERMISO || '%') ";
        Qry += " AND (EC_USUARIOS_PERMISOS.USUARIO_ID = " + Usuario_ID + ") AND  ";
        Qry += "(EC_PERSONAS.SUSCRIPCION_ID = " + Suscripcion_ID + ") ";
        return Qry;
    }
    /// <summary>
    /// Obtiene un qry que obtiene las personas IDS que pertenecen a un usuario y una suscripcion
    /// </summary>
    /// <param name="Usuario_ID"></param>
    /// <param name="Suscripcion_ID"></param>
    /// <returns></returns>
    public static string ObtenQryPersonasdeUsuario(int Usuario_ID)
    {
        string Qry = "SELECT        EC_PERSONAS.PERSONA_ID " +
        "FROM            EC_PERSONAS INNER JOIN " +
        "EC_USUARIOS_PERMISOS ON EC_PERSONAS.SUSCRIPCION_ID = EC_USUARIOS_PERMISOS.SUSCRIPCION_ID " +
        "WHERE        (EC_PERSONAS.PERSONA_BORRADO = 0) AND ";
        if (!CeC_BD.EsOracle)
            Qry += "(EC_PERSONAS.AGRUPACION_NOMBRE LIKE EC_USUARIOS_PERMISOS.USUARIO_PERMISO + '%') ";
        else
            Qry += "(EC_PERSONAS.AGRUPACION_NOMBRE LIKE EC_USUARIOS_PERMISOS.USUARIO_PERMISO || '%') ";
        Qry += " AND (EC_USUARIOS_PERMISOS.USUARIO_ID = " + Usuario_ID + ") AND  ";
        Qry += "(EC_PERSONAS.SUSCRIPCION_ID = EC_USUARIOS_PERMISOS.SUSCRIPCION_ID) ";
        return Qry;
    }
    public static int ObtenPersonasNoSinRegistro(int Suscripcion_ID)
    {
        string Qry = "SELECT	COUNT(TERMINALES_DEXTRAS_TEXTO1) from	EC_TERMINALES_DEXTRAS, " +
            " EC_AUTONUM where AUTONUM_TABLA='EC_TERMINALES'  AND " +
            " EC_AUTONUM.SUSCRIPCION_ID = " + Suscripcion_ID + "  AND " +
            " EC_TERMINALES_DEXTRAS.TERMINAL_ID = AUTONUM_TABLA_ID GROUP BY TERMINALES_DEXTRAS_TEXTO1";
        return CeC_BD.EjecutaEscalarInt(Qry);
    }
    public static int ObtenPersonasNO(int Suscripcion_ID)
    {
        return CeC_BD.EjecutaEscalarInt("SELECT COUNT(PERSONA_ID) FROM EC_PERSONAS WHERE SUSCRIPCION_ID = " + Suscripcion_ID);
    }
    public static int AcutalizaPersonaNombredeNOMBRE_COMPLETO(int Suscripcion_ID)
    {
        return CeC_BD.EjecutaComando("UPDATE EC_PERSONAS SET PERSONA_NOMBRE = (select NOMBRE_COMPLETO AS PERSONA_NOMBRE FROM EC_PERSONAS_DATOS WHERE EC_PERSONAS.PERSONA_ID = EC_PERSONAS_DATOS.PERSONA_ID) WHERE SUSCRIPCION_ID = " + Suscripcion_ID);
    }

    public static bool BorraRegistroPersonaNoCreada(int TERMINALES_DEXTRAS_ID)
    {
        if (CeC_BD.EjecutaComando("DELETE EC_TERMINALES_DEXTRAS WHERE TERMINALES_DEXTRAS_ID = " + TERMINALES_DEXTRAS_ID) > 0)
            return true;
        return false;
    }

    public static DataSet ObtenPersonasNoCreadas(int Suscripcion_ID)
    {
        string Qry = "SELECT	 TERMINALES_DEXTRAS_ID, TERMINAL_ID, TIPO_TERM_DEXTRAS_ID, TERMINALES_DEXTRAS_TEXTO1, TERMINALES_DEXTRAS_TEXTO2 from	EC_TERMINALES_DEXTRAS, " +
    " EC_AUTONUM where AUTONUM_TABLA='EC_TERMINALES'  AND " +
    " EC_AUTONUM.SUSCRIPCION_ID = " + Suscripcion_ID + " AND TIPO_TERM_DEXTRAS_ID < 100 AND " +
    " EC_TERMINALES_DEXTRAS.TERMINAL_ID = AUTONUM_TABLA_ID ORDER BY TERMINALES_DEXTRAS_TEXTO1";
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }
    /*  public int Agrega(int Persona_Link_ID, int Suscripcion_ID)
      {
          return Agrega(Persona_Link_ID, 1, Suscripcion_ID);
      }*/

    public static int ObtenPersonaIDBySuscripcion(int Persona_Link_ID, int SuscripcionID)
    {
        string Qry = "SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_LINK_ID = " + Persona_Link_ID.ToString() + " AND SUSCRIPCION_ID = " + SuscripcionID;
        return CeC_BD.EjecutaEscalarInt(Qry);
    }

    /// <summary>
    /// Obtiene el Persona_ID nombre a partir del Numero de empleado ligado y un usuario propietario (Valida también la Suscripción)
    /// </summary>
    public static int ObtenPersonaID(int Persona_Link_ID, int UsuarioID)
    {
        string Qry = "SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_LINK_ID = " + Persona_Link_ID.ToString() + " AND SUSCRIPCION_ID IN (SELECT SUSCRIPCION_ID FROM EC_USUARIOS WHERE USUARIO_ID = " + UsuarioID + ")";
        return CeC_BD.EjecutaEscalarInt(Qry);
    }

    /// <summary>
    /// Obtiene el usuario Maestro de una personaID
    /// Actualmente el primer usuario encontrado es el maextro
    /// </summary>
    /// <param name="SuscripcionID"></param>
    /// <returns></returns>
    public static int ObtenUsuarioID(int PersonaID)
    {
        return CeC_Suscripcion.ObtenUsuarioID(ObtenSuscripcionID(PersonaID));
    }


    /// <summary>
    /// Obtiene el Persona_ID nombre a partir del Numero de empleado ligado
    /// </summary>
    public static int ObtenPersonaID(int Persona_Link_ID)
    {
        string Qry = "SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_LINK_ID = " + Persona_Link_ID.ToString() + "";
        return CeC_BD.EjecutaEscalarInt(Qry);
    }
    /// <summary>
    /// Obtiene la configuración de la suscripcion
    /// </summary>
    /// <param name="PersonaID"></param>
    /// <returns></returns>
    public static CeC_ConfigSuscripcion ObtenConfigSuscripcion(int PersonaID)
    {
        return new CeC_ConfigSuscripcion(ObtenSuscripcionID(PersonaID));
    }

    /// <summary>
    /// Obtiene el ID persona a partir del Nombre del Campo llave y el valor
    /// </summary>
    public static int ObtenPersonaID(int Terminal_ID, string NombreCampo, string Valor)
    {
        string Campo = NombreCampo;
        if (Campo.IndexOf('.') < 0)
            Campo = "EC_PERSONAS_DATOS." + NombreCampo.Trim();
        string Qry = "SELECT	EC_PERSONAS.PERSONA_ID  from	EC_PERSONAS_DATOS, " +
            " EC_PERSONAS,EC_AUTONUM where AUTONUM_TABLA='EC_TERMINALES' AND AUTONUM_TABLA_ID = " + Terminal_ID.ToString() + " AND " +
            " EC_AUTONUM.SUSCRIPCION_ID = EC_PERSONAS.SUSCRIPCION_ID  AND " +
            " EC_PERSONAS.PERSONA_ID = EC_PERSONAS_DATOS.PERSONA_ID " +
            " AND " + Campo + " = '" + Valor + "'";
        return CeC_BD.EjecutaEscalarInt(Qry);
    }



    /// <summary>
    /// Obtiene un persona ID, desde un persona_link_id, si no existe, lo crea y regresa el id del recien creado
    /// Si el personaLink_id es menor a 0 tambien lo genera
    /// </summary>
    /// <param name="PersonaLinkID">Identificador de empleado</param>
    /// <param name="Suscripcion"></param>
    /// <returns></returns>
    public static int ObtenPersonaIDAutogenera(int PersonaLinkID, int TipoPersona, int Sesion_ID, int SuscripcionID)
    {
        if (PersonaLinkID < 0)
            PersonaLinkID = GeneraPersonaLinkId(SuscripcionID);
        int PersonaID = ObtenPersonaIDBySuscripcion(PersonaLinkID, SuscripcionID);
        if (PersonaID < 0)
        {
            PersonaID = Agrega(PersonaLinkID, TipoPersona, SuscripcionID, "", Sesion_ID);

        }
        return PersonaID;
    }

    public static int GeneraPersonaLinkId(int SuscripcionID)
    {
        int ID = CeC_BD.EjecutaEscalarInt("SELECT  MAX(PERSONA_LINK_ID) FROM EC_PERSONAS WHERE SUSCRIPCION_ID = " + SuscripcionID);
        if (ID < 0)
            ID = 0;
        ID++;
        return ID;
    }


    /// <summary>
    /// Actualiza los datos o importa los datos
    /// </summary>
    /// <param name="PersonasDatos">Cataset con los registros a actualizar, 
    /// cada columna contiene el nombre del campo a actualizar</param>
    /// <param name="SuscripcionID"></param>
    /// <param name="SesionID"></param>
    /// <returns></returns>
    public static int ImportaRegistros(DataSet PersonasDatos, int TipoPersona, bool ImportarNulos, int SuscripcionID, int SesionID)
    {
        if (PersonasDatos == null || PersonasDatos.Tables.Count < 1 || PersonasDatos.Tables[0].Rows.Count < 1)
            return -1;
        return ImportaRegistros(PersonasDatos.Tables[0], TipoPersona, ImportarNulos, SuscripcionID, SesionID);
    }
    /// <summary>
    /// Valida si cambio el sha de una fila para importacion
    /// </summary>
    /// <param name="Persona_ID"></param>
    /// <param name="Sha"></param>
    /// <param name="SuscripcionID"></param>
    /// <param name="SesionID"></param>
    /// <returns></returns>
    public static bool CambioSha(int Persona_ID, string Sha, int SuscripcionID, int SesionID)
    {
        string ShaAnterior = CeC_BD.EjecutaEscalarString("SELECT PERSONA_SHA FROM EC_PERSONAS_SHA WHERE PERSONA_ID = " + Persona_ID);
        if (ShaAnterior == Sha)
            return false;

        if (ShaAnterior == "")
        {
            CeC_BD.EjecutaComando("INSERT INTO EC_PERSONAS_SHA (PERSONA_ID, PERSONA_SHA, PERSONA_SHA_FECHA) VALUES(" + Persona_ID + ", '" + Sha + "', " + CeC_BD.SqlFechaHora(DateTime.Now) + ")");
            return true;
        }
        CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_SHA SET PERSONA_SHA='" + Sha + "', PERSONA_SHA_FECHA=" + CeC_BD.SqlFechaHora(DateTime.Now) + " WHERE PERSONA_ID = " + Persona_ID);

        return true;
    }
    public static int ImportaRegistros(DataTable PersonasDatos, int TipoPersona, bool ImportarNulos, int SuscripcionID, int SesionID)
    {
        int R = 0;
        try
        {
            if (PersonasDatos == null || PersonasDatos.Rows.Count < 1)
                return -1;
            CIsLog2.AgregaLog("Iniciado Importación");
            DateTime MesesAtraz = DateTime.Now.AddMonths(2);
            foreach (DataRow DR in PersonasDatos.Rows)
            {
                try
                {
                    int PersonaLinkID = Convert.ToInt32(DR["PERSONA_LINK_ID"]);
                    int PersonaID = ObtenPersonaIDAutogenera(PersonaLinkID, TipoPersona, SesionID, SuscripcionID);


                    string Sha = CeC_BD.CalculaHash(DR.ItemArray);
                    if (!CambioSha(PersonaID, Sha, SuscripcionID, SesionID))
                        continue;
                    /*                    try
                                        {
                                            if (CeC.Convierte2DateTime(DR["FECHA_BAJA"]) < MesesAtraz)
                                            {
                                                //Si el registro fue dado de baja hace mas de 3 meses se presume que no se deberán cargar sus datos
                                                GuardaValor(PersonaID, "FECHA_BAJA", DR["FECHA_BAJA"], SesionID);
                                                continue;
                                            }
                                        }
                                        catch { }*/

                    foreach (DataColumn DC in PersonasDatos.Columns)
                    {
                        if (DC.ColumnName != "PERSONA_LINK_ID")
                        {
                            try
                            {
                                GuardaValor(PersonaID, DC.ColumnName, DR[DC], SesionID);
                            }
                            catch { }
                        }
                    }
                    R++;
                }
                catch (Exception ex) { CIsLog2.AgregaError(ex); }
            }

            //      
        }
        catch { }
        CIsLog2.AgregaLog("Importación Finalizada");
        return R;
    }



    int m_Persona_ID = 0;
    public CeC_Personas(int Persona_ID)
    {
        m_Persona_ID = Persona_ID;
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public CeC_Personas(int Persona_ID, int Sesion_id)
    {
        m_Persona_ID = Persona_ID;
        Sesion_ID = Sesion_id;
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public string PERSONA_EMAIL
    {
        get { return CeC_BD.ObtenPersonaEMail(m_Persona_ID); }
        set { GuardaValor(m_Persona_ID, "PERSONA_EMAIL", value, Sesion_ID); }
    }
    public string PERSONA_NOMBRE
    {
        get { return CeC_BD.ObtenPersonaNombre(m_Persona_ID); }
        set { GuardaValor(m_Persona_ID, "PERSONA_NOMBRE", value, Sesion_ID); }
    }
    public int SUSCRIPCION_ID
    {
        get { return ObtenSuscripcionID(m_Persona_ID); }
        set { GuardaValor(m_Persona_ID, "SUSCRIPCION_ID", value, Sesion_ID); }
    }
    public static int ObtenSuscripcionID(int PersonaID)
    {
        return CeC_BD.ObtenPersonaSUSCRIPCION_ID(PersonaID);
    }

    #region Fotos de personas
    /// <summary>
    /// Asigna la foto a una persona
    /// </summary>
    public static bool AsignaFoto(int PersonaID, byte[] Foto)
    {
        int ID = CeC_BD.EjecutaEscalarInt("SELECT PERSONA_IMA_ID FROM EC_PERSONAS_IMA WHERE PERSONA_ID = " + PersonaID + " AND TIPO_IMAGEN_ID = 1 AND PERSONA_IMA_ORD = 0");
        if (ID <= 0)
        {
            ID = CeC_Autonumerico.GeneraAutonumerico("EC_PERSONAS_IMA", "PERSONA_IMA_ID");
            if (CeC_BD.EjecutaComando("INSERT INTO EC_PERSONAS_IMA (PERSONA_IMA_ID, TIPO_IMAGEN_ID, PERSONA_ID, PERSONA_IMA_ORD) VALUES (" + ID.ToString() + ",1," + PersonaID.ToString() + ",0)") > 0)
            {
                CeC_BD.AsignaBinario("EC_PERSONAS_IMA", "PERSONA_IMA_ID", ID, "PERSONA_IMA", Foto);
                return true;
            }
        }
        if (ID > 0)
        {
            CeC_BD.AsignaBinario("EC_PERSONAS_IMA", "PERSONA_IMA_ID", ID, "PERSONA_IMA", Foto);
            return true;
        }
        return false;
    }
    /// <summary>
    /// Asigna una Firma a una persona
    /// </summary>
    /// <param name="PersonaID"></param>
    /// <param name="Firma"></param>
    /// <returns></returns>
    public static bool AsignaFirma(int PersonaID, byte[] Firma)
    {
        int ID = CeC_BD.EjecutaEscalarInt("SELECT PERSONA_IMA_ID FROM EC_PERSONAS_IMA WHERE PERSONA_ID = " + PersonaID + " AND TIPO_IMAGEN_ID = 2 AND PERSONA_IMA_ORD = 0");
        if (ID <= 0)
        {
            ID = CeC_Autonumerico.GeneraAutonumerico("EC_PERSONAS_IMA", "PERSONA_IMA_ID");
            if (CeC_BD.EjecutaComando("INSERT INTO EC_PERSONAS_IMA (PERSONA_IMA_ID, TIPO_IMAGEN_ID, PERSONA_ID, PERSONA_IMA_ORD) VALUES (" + ID.ToString() + ",2," + PersonaID.ToString() + ",0)") > 0)
            {
                CeC_BD.AsignaBinario("EC_PERSONAS_IMA", "PERSONA_IMA_ID", ID, "PERSONA_IMA", Firma);
                return true;
            }
        }
        if (ID > 0)
        {
            CeC_BD.AsignaBinario("EC_PERSONAS_IMA", "PERSONA_IMA_ID", ID, "PERSONA_IMA", Firma);
            return true;
        }
        return false;
    }
    /// <summary>
    /// Borra la foto de la Persona
    /// </summary>
    /// <param name="PersonaID"></param>
    /// <returns></returns>
    public static bool BorraFoto(int PersonaID)
    {
        int ID = CeC_BD.EjecutaEscalarInt("SELECT PERSONA_IMA_ID FROM EC_PERSONAS_IMA WHERE PERSONA_ID = " + PersonaID + " AND TIPO_IMAGEN_ID = 1 AND PERSONA_IMA_ORD = 0");

        if (CeC_BD.EjecutaComando("Delete from EC_PERSONAS_IMA where PERSONA_IMA_ID = " + ID.ToString()) > 0)
            return true;
        return false;
    }
    public static bool BorraFirma(int PersonaID)
    {
        int ID = CeC_BD.EjecutaEscalarInt("SELECT PERSONA_IMA_ID FROM EC_PERSONAS_IMA WHERE PERSONA_ID = " + PersonaID + " AND TIPO_IMAGEN_ID = 2 AND PERSONA_IMA_ORD = 0");

        if (CeC_BD.EjecutaComando("Delete from EC_PERSONAS_IMA where PERSONA_IMA_ID = " + ID.ToString()) > 0)
            return true;
        return false;
    }
    /// <summary>
    /// Obtiene la foto de una persona
    /// </summary>
    public static byte[] ObtenFoto(int Persona_ID)
    {
        int ID = CeC_BD.EjecutaEscalarInt("SELECT PERSONA_IMA_ID FROM EC_PERSONAS_IMA WHERE PERSONA_ID = " + Persona_ID + " AND TIPO_IMAGEN_ID = 1 AND PERSONA_IMA_ORD = 0");
        if (ID <= 0)
            return null;
        return CeC_BD.ObtenBinario("EC_PERSONAS_IMA", "PERSONA_IMA_ID", ID, "PERSONA_IMA");
    }
    public static byte[] ObtenFirma(int Persona_ID)
    {
        int ID = CeC_BD.EjecutaEscalarInt("SELECT PERSONA_IMA_ID FROM EC_PERSONAS_IMA WHERE PERSONA_ID = " + Persona_ID + " AND TIPO_IMAGEN_ID = 2 AND PERSONA_IMA_ORD = 0");
        if (ID <= 0)
            return null;
        return CeC_BD.ObtenBinario("EC_PERSONAS_IMA", "PERSONA_IMA_ID", ID, "PERSONA_IMA");
    }
    public static byte[] ObtenTipoImagen(int Persona_ID, TipoImagen Tipo_Imagen)
    {
        int ID = CeC_BD.EjecutaEscalarInt("SELECT PERSONA_IMA_ID FROM EC_PERSONAS_IMA WHERE PERSONA_ID = " + Persona_ID + " AND TIPO_IMAGEN_ID = " + (int)Tipo_Imagen + " AND PERSONA_IMA_ORD = 0");
        if (ID <= 0)
            return null;
        return CeC_BD.ObtenBinario("EC_PERSONAS_IMA", "PERSONA_IMA_ID", ID, "PERSONA_IMA");
    }

    #endregion
    public static DataSet ObtenPersonasIDS_DS(int Suscripcion_ID, CeC_Sesion Sesion)
    {
        if (Sesion == null)
            return null;
        return (DataSet)CeC_BD.EjecutaDataSet("SELECT PERSONA_ID FROM EC_PERSONAS WHERE SUSCRIPCION_ID = " + Suscripcion_ID + "AND PERSONA_BORRADO = 0");
    }
}
