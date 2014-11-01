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
/// Descripción breve de CeC_Empleados
/// </summary>
public class CeC_Empleados : CeC_Personas
{
    /// <summary>
    /// Obtiene el persona Link ID a partir del ID persona
    /// </summary>
    public static int ObtenPersona_Link_ID(int Persona_ID)
    {
        return CeC_BD.ObtenPersonaLinkID(Persona_ID);
    }
    //sustituida por CeCAgrupaciones
    /*
    public static int RegeneraAgrupacion(int Suscripcion_ID, string Campo)
    {
        return CeC_BD.EjecutaComando("UPDATE EC_PERSONAS SET AGRUPACION_NOMBRE = ((SELECT " + Campo + " FROM EC_PERSONAS_DATOS WHERE EC_PERSONAS.PERSONA_ID = EC_PERSONAS_DATOS.PERSONA_ID)) WHERE " +
            " SUSCRIPCION_ID = " + Suscripcion_ID);
    }
    */
    public static int GeneraID(int Suscripcion_ID)
    {
        /*    int MaximoID = -1;
            string Tabla = "EC_PERSONAS_DATOS";
            string TablaVirtual = Tabla + Suscripcion_ID;
            string Campo = "PERSONA_LINK_ID";
            MaximoID = EjecutaEscalarInt("SELECT  MAX(AUTONUM_TABLA_ID) FROM EC_AUTONUM " +
                "WHERE AUTONUM_TABLA = '" + TablaVirtual + "' AND AUTONUM_CAMPO_ID = '" + Campo + "'");
            int NMax = 0;
            NMax = EjecutaEscalarInt("SELECT MAX(" + Campo + ") FROM " + Tabla +
                " WHERE PERSONA_ID IN (SELECT PERSONA_ID FROM EC_PERSONAS WHERE SUSCRIPCION_ID = " + Suscripcion_ID + ")");
            if (NMax < 0)
                NMax = 0;
            if (MaximoID < NMax)
                MaximoID = NMax;*/
        return -1;

    }
    //Agrega un Empleado y regresa el persona_ID
    public static int Agrega(int Persona_Link_ID, CeC_Sesion Sesion)
    {
        return CeC_Personas.Agrega(Persona_Link_ID, 1, Sesion);
    }


    public CeC_Empleados(int Persona_ID)
        : base(Persona_ID)
    {

        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public static bool AcutalizaPersonaNOMBRE_COMPLETO(int Persona_ID, int SuscripcionID, int Sesion_ID, bool Forza)
    {

        string Nombre = "";
        if (Forza)
            Nombre = CeC_BD.ObtenPersonaNombre(Persona_ID);
        if (Nombre == "" || Nombre.ToUpper() == "NULL")
        {
            CeC_ConfigSuscripcion Config = new CeC_ConfigSuscripcion(SuscripcionID);
            /*            string[] Campos = CeC.ObtenArregoSeparador(Config.NombrePersona.Replace("'", ""), "+");
                        string NombreCompleto = "";
                        foreach (string Campo in Campos)
                        {
                            NombreCompleto
                        }
                        string NombreCompleto = ((string)CeC_Campos.ObtenValorCampo(Persona_ID, "APATERNO") + ", " + CeC_Campos.ObtenValorCampo(Persona_ID, "NOMBRE"));
             */
            string NombreCompleto = CeC_BD.EjecutaEscalarString("SELECT " + Config.NombrePersona_QRY + " FROM EC_PERSONAS_DATOS WHERE PERSONA_ID = " + Persona_ID);

            //CeC_Campos.GuardaValor("PERSONA_NOMBRE", Persona_ID, NombreCompleto,SesionID);
            return GuardaValor(Persona_ID, "NOMBRE_COMPLETO", NombreCompleto, Sesion_ID);
        }
        return false;
    }
    /*/// <summary>
    /// Actualiza el nombre de empleados de acuerdo a la configuración
    /// </summary>
    public static void ActualizaNombresEmpleados()
    {
        //Actualiza Nombres empleados
        string[] arreglo = (CeC_Campos.ObtenListaCamposTE().Split(new char[] { ',' }));
        foreach (string Campo in arreglo)
        {
            EjecutaComando("UPDATE EC_PERSONAS_DATOS SET " + Campo.Trim() + " = '' WHERE " + Campo.Trim() + " IS NULL");
        }
        EjecutaComando("UPDATE EC_PERSONAS SET PERSONA_NOMBRE = (SELECT " + CeC_Config.NombrePersona_QRY + " FROM EC_PERSONAS_DATOS WHERE EC_PERSONAS_DATOS.PERSONA_ID = EC_PERSONAS.PERSONA_ID) WHERE PERSONA_ID > 0 AND TIPO_PERSONA_ID = 1");

    }
    */
    // PEndiente por Programar
    public static bool AcutalizaPersonasNOMBRE_COMPLETO(int SuscripcionID)
    {
        return false;
    }
    // PEndiente por Programar
    public static bool AcutalizaPersonasNOMBRE_COMPLETO()
    {
        return false;
    }
    public static bool AsignaAgrupacion(int Persona_ID, string Agrupacion, int SesionID)
    {
        return GuardaValor(Persona_ID, "AGRUPACION_NOMBRE", Agrupacion, SesionID);
    }

    public static bool AsignaAgrupacion(int Persona_Link_ID, string Agrupacion, int UsuarioID, int SesionID)
    {
        int PersonaID = ObtenPersonaID(Persona_Link_ID, UsuarioID);
        if (PersonaID <= 0)
            return false;
        return GuardaValor(PersonaID, "AGRUPACION_NOMBRE", Agrupacion, SesionID);
    }

    public static string ObtenAgrupacion(int Persona_Link_ID, int UsuarioID)
    {
        int PersonaID = ObtenPersonaID(Persona_Link_ID, UsuarioID);
        if (PersonaID <= 0)
            return "";
        return CeC_Campos.ObtenValorCampo(PersonaID, "AGRUPACION_NOMBRE");
    }
    public static bool AsignaNombre(int Persona_ID, string Nombre, int SesionID)
    {
        return GuardaValor(Persona_ID, "NOMBRE_COMPLETO", Nombre, SesionID);
    }

    /// <summary>
    /// Trae de la base de datos la clave de empleado en referente al ID del mismo
    /// </summary>
    /// <param name="ClaveEmpl"></param>
    /// <param name="UsuarioID"></param>
    /// <returns></returns>
    public static int ClaveEmpl2PersonaID(string ClaveEmpl, int UsuarioID)
    {
        string Qry = "SELECT PERSONA_ID FROM EC_PERSONAS_DATOS WHERE CLAVE_EMPL = '" + ClaveEmpl + "' AND PERSONA_ID IN (SELECT PERSONA_ID FROM EC_PERSONAS WHERE SUSCRIPCION_ID IN (SELECT SUSCRIPCION_ID FROM EC_USUARIOS WHERE USUARIO_ID = " + UsuarioID + "))";
        return CeC_BD.EjecutaEscalarInt(Qry);
    }

    /// <summary>
    /// Actualiza los datos o importa los datos de los empleados
    /// </summary>
    /// <param name="PersonasDatos">Cataset con los registros a actualizar, 
    /// cada columna contiene el nombre del campo a actualizar</param>
    /// <param name="SuscripcionID"></param>
    /// <param name="SesionID"></param>
    /// <returns></returns>
    public static int ImportaRegistros(DataSet PersonasDatos, bool ImportarNulos, int SuscripcionID, int SesionID)
    {
        return ImportaRegistros(PersonasDatos, 1, ImportarNulos, SuscripcionID, SesionID);
    }

    /// <summary>
    /// Actualiza los datos o importa los datos de los empleados
    /// </summary>
    /// <param name="PersonasDatos">DataTable con los registros a actualizar, 
    /// cada columna contiene el nombre del campo a actualizar</param>
    /// <param name="ImportarNulos"></param>
    /// <param name="SuscripcionID"></param>
    /// <param name="SesionID"></param>
    /// <returns></returns>
    public static int ImportaRegistros(DataTable PersonasDatos, bool ImportarNulos, int SuscripcionID, int SesionID)
    {
        return ImportaRegistros(PersonasDatos, 1, ImportarNulos, SuscripcionID, SesionID);
    }
}
