using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

/// <summary>
/// Descripción breve de CeC_CamposDatos
/// </summary>
public class CeC_CamposDatos
{
    public CeC_CamposDatos()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    /* /// <summary>
     /// Función que se encarga de llenar los campos de empleados en Anviz
     /// </summary>
     /// <param name="Sesion">Sesion Actual</param>
     /// <param name="SuscripcionID">Identificador de la suscripcion</param>
     /// <param name="ModeloJson">Modelo en formato Json</param>
     /// <returns></returns>
     /// <summary>
     /// Evalua un listado en una variable denominada contenedores deserializando y contabilizando
     /// los registros de una lista para posteriormente avaluar cada uno de los registros; si este es
     /// Usirid; un campo con el nombre que identifica agregando el valor ala tabla de historia de personas
     /// con la fecha correspondiente. Si no es Userid lo almacena en la misma talba con un
     /// nombre diferente.
     /// </summary>
     /// <param name="Sesion"></param>
     /// <param name="SuscripcionID"></param>
     /// <param name="ModeloJson"></param>
     /// <returns></returns>
     public static int LlenarCamposEmpleados_Anviz(CeC_Sesion Sesion, int SuscripcionID, string ModeloJson)
     {
         int Campo_Dato_ID = -1;
         int PersonaID = -1;
         try
         {
             List<Newtonsoft.Json.Linq.JContainer> Contenedores = JsonConvert.DeserializeObject<List<Newtonsoft.Json.Linq.JContainer>>(ModeloJson);
             if (Contenedores.Count < 1)
                 return -1;
             foreach (Newtonsoft.Json.Linq.JContainer Contenedor in Contenedores)
             {
                 foreach (Newtonsoft.Json.Linq.JProperty Propiedad in Contenedor)
                 {
                     object Dato = Propiedad.Value.ToString();
                     string ColumnaNombre = Propiedad.Name;
                     //PersonaID = CeC_Personas.ObtenPersonaIDAutogenera(CeC.Convierte2Int(Dato), 1, Sesion.SESION_ID, SuscripcionID);
                     if (ColumnaNombre == "Userid")
                     {
                         //PersonaID = CeC_Personas.ObtenPersonaIDAutogenera(CeC.Convierte2Int(Dato), 1, Sesion.SESION_ID, SuscripcionID);
                         Campo_Dato_ID = CrearCampoDato(ColumnaNombre, 2, SuscripcionID);
                         PersonaID = CeC_Personas.Agrega(CeC.Convierte2Int(Dato), 1, Sesion);
                         CeC_Personas.AgregaHistorialValor(PersonaID, ColumnaNombre, Sesion.SESION_ID, Dato.ToString(), DateTime.Now);
                     }
                     else
                     {
                         Campo_Dato_ID = CrearCampoDato(ColumnaNombre, 1, SuscripcionID);
                         CeC_Personas.AgregaHistorialValor(PersonaID, ColumnaNombre, Sesion.SESION_ID, Dato.ToString(), DateTime.Now);
                     }
                 }
             }
             return Campo_Dato_ID;
         }
         catch (Exception ex)
         {
             CIsLog2.AgregaError(ex);
             return -1;
         }
     }
     */


    public static bool CreaCampo(string CampoNombre, int TipoDato)
    {
        string Campo_Nombre = ExisteCampo(CampoNombre);
        if (Campo_Nombre != CampoNombre)
            if (CeC_BD.EjecutaComando(" INSERT INTO EC_CAMPOS(CAMPO_NOMBRE, CAMPO_ETIQUETA, CATALOGO_ID, MASCARA_ID, TIPO_DATO_ID) " +
                                                        " VALUES ('" + CampoNombre + "', '" + CampoNombre + "', " + 0 + ", " + 0 + ", " + TipoDato + ")") > 0)
                return true;
        return false;
    }
    /// <summary>
    /// Crea el campo e la tabla EC_CAMPOS_DATO
    /// </summary>
    /// <param name="CampoNombre">Nombre del campoi</param>
    /// <param name="TipoDato">Tipo de dato del Campo</param>
    /// <param name="SuscripcionID">ID de la Suscripción</param>
    /// <returns>Campo_Dato_ID. -9999 en error</returns>
    private static int CrearCampoDato(string CampoNombre, int TipoPersonaID, int TipoDato, int SuscripcionID)
    {
        int Campo_Dato_ID = ExisteCampoDato(CampoNombre, SuscripcionID);
        CreaCampo(CampoNombre, TipoDato);
        if (Campo_Dato_ID < 0)
        {
            try
            {
                Campo_Dato_ID = CeC_Autonumerico.GeneraAutonumerico("EC_CAMPOS_DATOS", "CAMPO_DATO_ID");
                if (CeC_BD.EjecutaEscalarInt(" INSERT INTO EC_CAMPOS_DATOS(CAMPO_DATO_ID,SUSCRIPCION_ID,TIPO_PERSONA_ID,TIPO_DATO_ID,CAMPO_DATO_NOMBRE,CAMPO_DATO_ETIQUETA) " +
                                        " VALUES(" + Campo_Dato_ID + ", " + SuscripcionID + "," + TipoPersonaID + ", '" + TipoDato + "', '" + CampoNombre + "', '" + CampoNombre + "')") > 0)
                    return Campo_Dato_ID;
                return -9998;
            }
            catch (Exception ex)
            {
                CIsLog2.AgregaError(ex);
                return -9999;
            }
        }
        return Campo_Dato_ID;
    }

    public static int CrearCampoDato(eClockBase.Modelos.Model_CAMPOS_DATOS CampoDato, CeC_Sesion Sesion)
    {

        CreaCampo(CampoDato.CAMPO_DATO_NOMBRE, CampoDato.TIPO_DATO_ID);

        return CeC_Tabla.GuardaDatos("EC_CAMPOS_DATOS", "CAMPO_DATO_ID", CampoDato, CampoDato.CAMPO_DATO_ID <= 0 ? true : false, Sesion, CampoDato.SUSCRIPCION_ID);

    }

    /// <summary>
    /// Comprueba la existencia de un campo para la suscripcion seleccionada, si no existe regresara -1
    /// </summary>
    /// <param name="CampoNombre">Nombre del campo</param>
    /// <param name="SuscripcionID">Identificador de la suscripcion</param>
    /// <returns></returns>
    private static int ExisteCampoDato(string CampoNombre, int SuscripcionID)
    {
        try
        {
            return CeC_BD.EjecutaEscalarInt(" SELECT CAMPO_DATO_ID " +
                                            " FROM EC_CAMPOS_DATOS " +
                                            " WHERE CAMPO_DATO_NOMBRE = '" + CampoNombre + "' AND SUSCRIPCION_ID = " + SuscripcionID +
                                            " \n ORDER BY TIPO_PERSONA_ID");
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
            return -1;
        }
    }
    /// <summary>
    /// Verifica si existe el Campo para la Suscripcion seleccionada
    /// </summary>
    /// <param name="CampoNombre">Nombre del Campo</param>
    /// <param name="SuscripcionID">ID de la Suscripción</param>
    /// <returns>Campo_Dato_ID. -1 en caso de que el Campo no exista</returns>
    private static string ExisteCampo(string CampoNombre)
    {
        try
        {
            return CeC_BD.EjecutaEscalarString(" SELECT CAMPO_NOMBRE " +
                                            " FROM EC_CAMPOS " +
                                            " WHERE CAMPO_NOMBRE = '" + CampoNombre + "'");
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
            return "";
        }
    }
    /// <summary>
    /// Obtiene el PersonaID
    /// </summary>
    /// <param name="CampoNombre">Nombre del Campo</param>
    /// <param name="PersonaDato">Dato del Campo</param>
    /// <returns>PersonaID</returns>
    public static int ObtenPersonaIDXCampo(string CampoNombre, string PersonaDato)
    {
        string Qry = " SELECT PERSONA_ID " +
                        " FROM EC_PERSONAS_DATO " +
                        " WHERE CAMPO_NOMBRE = '" + CampoNombre + "'" +
                        " AND PERSONA_DATO = '" + PersonaDato + "'";
        return CeC_BD.EjecutaEscalarInt(Qry);
    }

    public static string ObtenCampoDatoNombre(int CampoDatoID)
    {
        return CeC_BD.EjecutaEscalarString(" SELECT CAMPO_DATO_NOMBRE FROM EC_CAMPOS_DATOS " +
                                " WHERE CAMPO_DATO_ID = " + CampoDatoID + "");
    }

    public static bool CreaCamposPredeterminados(int SuscripcionID, int TipoPersonaID, string Lang, CeC_Sesion Sesion)
    {
        /*
         * TIPO_DATO_ID	TIPO_DATO_NOMBRE	TIPO_DATO_DS	TIPO_DATO_BORRADO
0	Desconocido		1
1	Texto	System.String	0
2	Entero	System.Int32	0
3	Decimal	System.Decimal	0
4	Imagen	System.Byte[]	0
5	Fecha	System.DateTime	1
6	Fecha y Hora	System.DateTime	0
7	Hora	System.DateTime	1
8	Boleano	System.Boolean	0
9	Horas	System.DateTime	0
10	Foto	System.	0
         * 
            * 
         * */
        List<eClockBase.Modelos.Model_CAMPOS_DATOS> CamposDatos = new List<eClockBase.Modelos.Model_CAMPOS_DATOS>();
        CamposDatos.Add(new eClockBase.Modelos.Model_CAMPOS_DATOS(-1, SuscripcionID, TipoPersonaID, 2, "PERSONA_LINK_ID", "No. Empleado", "", false, "", "", false, 1, "", "", 1, "", "1", true));
        CamposDatos.Add(new eClockBase.Modelos.Model_CAMPOS_DATOS(-1, SuscripcionID, TipoPersonaID, 1, "PERSONA_NOMBRE", "Nombre Completo", "", false, "", "", false, 1, "", "", 1, "", "1", true));
        CamposDatos.Add(new eClockBase.Modelos.Model_CAMPOS_DATOS(-1, SuscripcionID, TipoPersonaID, 1, "PERSONA_EMAIL", "Correo eléctronico", "", false, "", "", false, 1, "", "", 1, "", "1", true));
        CamposDatos.Add(new eClockBase.Modelos.Model_CAMPOS_DATOS(-1, SuscripcionID, TipoPersonaID, 1, "NO_CREDENCIAL", "No Credencial", "", false, "", "", false, 1, "", "", 1, "", "1", true));


        CamposDatos.Add(new eClockBase.Modelos.Model_CAMPOS_DATOS(-1, SuscripcionID, TipoPersonaID, 5, "FECHA_NAC", "Fecha de Nacimiento", "", false, "", "", false, 1, "", "", 1, "", "1", true));
        CamposDatos.Add(new eClockBase.Modelos.Model_CAMPOS_DATOS(-1, SuscripcionID, TipoPersonaID, 5, "FECHA_INGRESO", "Fecha de Ingreso", "", false, "", "", false, 1, "", "", 1, "", "1", true));
        CamposDatos.Add(new eClockBase.Modelos.Model_CAMPOS_DATOS(-1, SuscripcionID, TipoPersonaID, 5, "FECHA_BAJA", "Fecha de baja", "", false, "", "", false, 1, "", "", 1, "", "1", true));

        CamposDatos.Add(new eClockBase.Modelos.Model_CAMPOS_DATOS(-1, SuscripcionID, TipoPersonaID, 1, "TIPO_NOMINA", "Tipo de Nomina", "", false, "", "", false, 1, "", "", 1, "", "1", true));
        CamposDatos.Add(new eClockBase.Modelos.Model_CAMPOS_DATOS(-1, SuscripcionID, TipoPersonaID, 1, "COMPANIA", "Empresa", "", false, "", "", false, 1, "", "", 1, "", "1", true));
        CamposDatos.Add(new eClockBase.Modelos.Model_CAMPOS_DATOS(-1, SuscripcionID, TipoPersonaID, 1, "CENTRO_DE_COSTOS", "Centro de costos", "", false, "", "", false, 1, "", "", 1, "", "1", true));
        CamposDatos.Add(new eClockBase.Modelos.Model_CAMPOS_DATOS(-1, SuscripcionID, TipoPersonaID, 1, "AREA", "Area", "", false, "", "", false, 1, "", "", 1, "", "1", true));
        CamposDatos.Add(new eClockBase.Modelos.Model_CAMPOS_DATOS(-1, SuscripcionID, TipoPersonaID, 1, "DEPARTAMENTO", "Departamento", "", false, "", "", false, 1, "", "", 1, "", "1", true));
        CamposDatos.Add(new eClockBase.Modelos.Model_CAMPOS_DATOS(-1, SuscripcionID, TipoPersonaID, 1, "PUESTO", "Puesto", "", false, "", "", false, 1, "", "", 1, "", "1", true));

        CamposDatos.Add(new eClockBase.Modelos.Model_CAMPOS_DATOS(-1, SuscripcionID, TipoPersonaID, 1, "SEXO", "Sexo", "", false, "", "Masculino|Femenino", true, 1, "", "", 1, "", "1", true));

        CamposDatos.Add(new eClockBase.Modelos.Model_CAMPOS_DATOS(-1, SuscripcionID, TipoPersonaID, 2, "TURNO_ID", "Turno", "", false, "", "", false, 1, "", "", 1, "", "1", true));
        CamposDatos.Add(new eClockBase.Modelos.Model_CAMPOS_DATOS(-1, SuscripcionID, TipoPersonaID, 2, "CALENDARIO_DF_ID", "Calendario Dias Festivos", "", false, "", "", false, 1, "", "", 1, "", "1", true));
        CamposDatos.Add(new eClockBase.Modelos.Model_CAMPOS_DATOS(-1, SuscripcionID, TipoPersonaID, 8, "PERSONA_BORRADO", "Inactivo?", "", false, "", "", false, 1, "", "", 1, "", "1", true));
        foreach (eClockBase.Modelos.Model_CAMPOS_DATOS CampoDato in CamposDatos)
        {
            CrearCampoDato(CampoDato, Sesion);
        }
        return true;
    }



    public static System.Data.DataSet ObtenCampos(int PersonaID, string Campos, CeC_Sesion Sesion)
    {
        string[] sCampos = CeC.ObtenArregoSeparador(Campos, ",");
        string QryCampos = "";
        foreach (string Campo in sCampos)
        {
            QryCampos = CeC.AgregaSeparador(QryCampos, "(SELECT PERSONA_DATO FROM EC_PERSONAS_DATO WHERE CAMPO_NOMBRE= '" + Campo + "' AND EC_PERSONAS_DATO.PERSONA_ID = EC_PERSONAS.PERSONA_ID) AS " + Campo, ",");
        }
        string Qry = "SELECT " + QryCampos + " FROM EC_PERSONAS WHERE PERSONA_ID = " + PersonaID;
        return CeC_BD.EjecutaDataSet(Qry);
    }

    public static int BorraCampos(int SuscripcionID, int TipoPersonaID, string NoBorrar)
    {
        string Qry = "DELETE EC_CAMPOS_DATOS WHERE SUSCRIPCION_ID = " + SuscripcionID + " AND TIPO_PERSONA_ID = " + TipoPersonaID;
        if (NoBorrar != null && NoBorrar != "")
            Qry += " AND CAMPO_DATO_ID NOT IN (" + NoBorrar + ")";
        return CeC_BD.EjecutaComando(Qry);
    }

    public static int GuardaCampos(List<eClockBase.Modelos.Model_CAMPOS_DATOS> CamposDatos, CeC_Sesion Sesion)
    {
        int Actualizados = 0;
        if (CamposDatos.Count > 0)
        {
            string IDS = "";
            foreach (eClockBase.Modelos.Model_CAMPOS_DATOS CampoDato in CamposDatos)
                if (CampoDato.CAMPO_DATO_ID > 0)
                    IDS = CeC.AgregaSeparador(IDS, CampoDato.CAMPO_DATO_ID.ToString(), ",");
            BorraCampos(CamposDatos[0].SUSCRIPCION_ID, CamposDatos[0].TIPO_PERSONA_ID, IDS);
        }
        foreach (eClockBase.Modelos.Model_CAMPOS_DATOS CampoDato in CamposDatos)
        {
            if (CeC_CamposDatos.CrearCampoDato(CampoDato, Sesion) > 0)
                Actualizados++;
        }
        return Actualizados;
    }
}