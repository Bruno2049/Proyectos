using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Descripción breve de CeC_Actividades
/// </summary>
public class CeC_Actividades
{
    public CeC_Actividades()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public static byte[] ObtenImagen(int ActividadID, DateTime FechaHoraMinima, CeC_Sesion Sesion)
    {
        return CeC_BD.ObtenBinario("EC_ACTIVIDADES", "ACTIVIDAD_ID", ActividadID, "ACTIVIDAD_IMAGEN", FechaHoraMinima, Sesion);

        DateTime Modificacion = CeC_Autonumerico.ObtenFechaModificacion("EC_ACTIVIDADES", "ACTIVIDAD_ID", ActividadID, Sesion);
        if (Modificacion < FechaHoraMinima)
            return null;
        return CeC_BD.ObtenBinario("EC_ACTIVIDADES", "ACTIVIDAD_ID", ActividadID, "ACTIVIDAD_IMAGEN");
    }

    public static string ObtenACTIVIDAD_MOMENTO(int ActividadID)
    {
        return CeC_BD.EjecutaEscalarString("SELECT ACTIVIDAD_MOMENTO FROM EC_ACTIVIDADES WHERE ACTIVIDAD_ID = " + ActividadID);
    }


    public static int Incribirse(int ActividadID, int PersonaID, int TipoIncripcionID, string Descripcion, CeC_Sesion Sesion)
    {
        eClockBase.Modelos.Model_ACTIVIDAD_INS Modelo = new eClockBase.Modelos.Model_ACTIVIDAD_INS();

        Modelo.ACTIVIDAD_INS_ID = -1;
        Modelo.ACTIVIDAD_ID = ActividadID;
        Modelo.PERSONA_ID = PersonaID;
        Modelo.TIPO_INSCRIPCION_ID = TipoIncripcionID;
        Modelo.ACTIVIDAD_INS_FECHA = System.DateTime.Now;
        Modelo.ACTIVIDAD_INS_DESCRIPCION = Descripcion;
        int R = CeC_Tabla.GuardaDatos("EC_ACTIVIDAD_INS", "ACTIVIDAD_INS_ID", Modelo, true, Sesion, Sesion.SUSCRIPCION_ID);
        if (R > 0)
        {
            eClockBase.Modelos.Model_ACTIVIDADES Actividad = new eClockBase.Modelos.Model_ACTIVIDADES();
            Actividad.ACTIVIDAD_ID = ActividadID;
            Actividad = Newtonsoft.Json.JsonConvert.DeserializeObject<eClockBase.Modelos.Model_ACTIVIDADES>(CeC_Tabla.ObtenDatos("EC_ACTIVIDADES", "ACTIVIDAD_ID", Actividad, Sesion));

            if (Actividad.ACTIVIDAD_MOMENTO != "")
            {
                string Detalle = "";
                eClockBase.Modelos.Personas.Model_Datos Datos = CeC_Personas.ObtenDatosPersonaModelo(PersonaID);
                Detalle = "Se ha inscrito: " + Datos.PERSONA_NOMBRE + " (" + Datos.PERSONA_LINK_ID + ")" + CeC.SaltoLineaHtml;
                Detalle += "A la actividad: " + Actividad.ACTIVIDAD_NOMBRE + CeC.SaltoLineaHtml;
                Detalle += "Con los siguientes parámetros: " + Descripcion;
                CeC_Mails.EnviarMail(Actividad.ACTIVIDAD_MOMENTO, "Nueva incripcion a " + Actividad.ACTIVIDAD_NOMBRE, Detalle);
            }
        }
        return R;

    }

    public static int Resumen()
    {
        int UltimoActividadInscID = CeC_Config.ActividadResumenUltimoIncripActID;
        string Qry = "SELECT EC_ACTIVIDADES.ACTIVIDAD_NOMBRE, EC_ACTIVIDADES.ACTIVIDAD_CAMPOS,  \n" +
" 	EC_TIPO_INSCRIPCION.TIPO_INSCRIPCION_NOMBRE, EC_PERSONAS.PERSONA_LINK_ID, EC_PERSONAS.PERSONA_NOMBRE,  \n" +
" 	EC_ACTIVIDAD_INS.ACTIVIDAD_INS_FECHA, EC_ACTIVIDAD_INS.ACTIVIDAD_INS_DESCRIPCION, EC_ACTIVIDADES.ACTIVIDAD_RESUMEN,  \n" +
" 	EC_ACTIVIDADES.ACTIVIDAD_ID, EC_ACTIVIDAD_INS.ACTIVIDAD_INS_ID \n" +
" FROM	EC_ACTIVIDAD_INS INNER JOIN \n" +
" 	EC_ACTIVIDADES ON EC_ACTIVIDAD_INS.ACTIVIDAD_ID = EC_ACTIVIDADES.ACTIVIDAD_ID INNER JOIN \n" +
" 	EC_PERSONAS ON EC_ACTIVIDAD_INS.PERSONA_ID = EC_PERSONAS.PERSONA_ID INNER JOIN \n" +
" 	EC_TIPO_INSCRIPCION ON EC_ACTIVIDAD_INS.TIPO_INSCRIPCION_ID = EC_TIPO_INSCRIPCION.TIPO_INSCRIPCION_ID \n" +
" WHERE	(EC_ACTIVIDADES.ACTIVIDAD_RESUMEN IS NOT NULL AND EC_ACTIVIDADES.ACTIVIDAD_RESUMEN <> '') AND  \n" +
" 	(EC_ACTIVIDAD_INS.ACTIVIDAD_INS_ID > " + UltimoActividadInscID + ") \n" +
" \n ORDER BY EC_ACTIVIDADES.ACTIVIDAD_ID, EC_PERSONAS.PERSONA_NOMBRE";
        int Enviados = 0;
        DataSet DS = CeC_BD.EjecutaDataSet(Qry);
        if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
        {
            int ACTIVIDAD_ID_Anterior = -1;
            string Titulo = "";
            string Cuerpo = "";
            string eMails = "";
            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                Enviados++;
                string ACTIVIDAD_NOMBRE = CeC.Convierte2String(DR["ACTIVIDAD_NOMBRE"]);
                string ACTIVIDAD_CAMPOS = CeC.Convierte2String(DR["ACTIVIDAD_CAMPOS"]);
                string TIPO_INSCRIPCION_NOMBRE = CeC.Convierte2String(DR["TIPO_INSCRIPCION_NOMBRE"]);
                int PERSONA_LINK_ID = CeC.Convierte2Int(DR["PERSONA_LINK_ID"]);
                string PERSONA_NOMBRE = CeC.Convierte2String(DR["PERSONA_NOMBRE"]);
                DateTime ACTIVIDAD_INS_FECHA = CeC.Convierte2DateTime(DR["ACTIVIDAD_INS_FECHA"]);
                string ACTIVIDAD_INS_DESCRIPCION = CeC.Convierte2String(DR["ACTIVIDAD_INS_DESCRIPCION"]);
                string ACTIVIDAD_RESUMEN = CeC.Convierte2String(DR["ACTIVIDAD_RESUMEN"]);
                int ACTIVIDAD_ID = CeC.Convierte2Int(DR["ACTIVIDAD_ID"]);
                int ACTIVIDAD_INS_ID = CeC.Convierte2Int(DR["ACTIVIDAD_INS_ID"]);

                if (ACTIVIDAD_INS_ID > UltimoActividadInscID)
                    UltimoActividadInscID = ACTIVIDAD_INS_ID;

                if (ACTIVIDAD_ID != ACTIVIDAD_ID_Anterior)
                {
                    Cuerpo += "</table>";
                    if (ACTIVIDAD_ID_Anterior > 0)
                        CeC_Mails.EnviarMail(eMails, Titulo, Cuerpo);
                    ACTIVIDAD_ID_Anterior = ACTIVIDAD_ID;

                    Titulo = "Resumen de incripciones a la actividad " + ACTIVIDAD_NOMBRE;
                    eMails = ACTIVIDAD_RESUMEN;
                    Cuerpo = "Al momento (" + DateTime.Now.ToString() + ") " + CeC.SaltoLineaHtml;
                    Cuerpo += "Se las siguientes personas se inscribieron a la actividad " + ACTIVIDAD_NOMBRE + ":";
                    Cuerpo += "<table style=\"width:100%;\">";
                    Cuerpo += "<tr>";
                    Cuerpo += "<td>Empleado</td>";
                    Cuerpo += "<td>Datos</td>";
                    Cuerpo += "</tr>";
                }
                Cuerpo += "<tr>";
                Cuerpo += "<td>" + PERSONA_NOMBRE + " (" + CeC.Convierte2String(PERSONA_LINK_ID) + ")</td>";
                Cuerpo += "<td>" + ACTIVIDAD_INS_DESCRIPCION + "</td>";
                Cuerpo += "</tr>";
            }
            Cuerpo += "</table>";
            if (ACTIVIDAD_ID_Anterior > 0)
                CeC_Mails.EnviarMail(eMails, Titulo, Cuerpo);
            CeC_Config.ActividadResumenUltimoIncripActID = UltimoActividadInscID;
        }

        return Enviados;
    }
    public static DataSet ObtenerNoInscripcionesDS(CeC_Sesion Sesion)
    {
        return ObtenerNoInscripcionesDS(new DateTime(), new DateTime(), Sesion);
    }
    public static DataSet ObtenerNoInscripcionesDS(DateTime FechaInicial, DateTime FechaFinal, CeC_Sesion Sesion)
    {
        string QryFechas = "";
        if (FechaInicial != new DateTime() && FechaFinal != new DateTime())
        {
            QryFechas = " AND ACTIVIDAD_INS_FECHA >= " + CeC_BD.SqlFechaHora(FechaInicial) + " AND ACTIVIDAD_INS_FECHA <= " + CeC_BD.SqlFechaHora(FechaFinal.AddDays(1)) + " \n";
        }
        string Qry = "SELECT	EC_ACTIVIDADES.ACTIVIDAD_ID, EC_ACTIVIDADES.ACTIVIDAD_NOMBRE, EC_ACTIVIDADES.ACTIVIDAD_DESCRIPCION, COUNT(*)  \n" +
                " 	AS NO_INSCRIPCIONES \n" +
                " FROM	EC_ACTIVIDADES LEFT OUTER JOIN \n" +
                " 	EC_ACTIVIDAD_INS ON EC_ACTIVIDADES.ACTIVIDAD_ID = EC_ACTIVIDAD_INS.ACTIVIDAD_ID \n" +
                " WHERE	(EC_ACTIVIDADES.ACTIVIDAD_BORRADO = 0) \n" +
                " AND EC_ACTIVIDADES." + CeC_Autonumerico.ValidaSuscripcion("EC_ACTIVIDADES", "ACTIVIDAD_ID", Sesion.SUSCRIPCION_ID) + " \n" + QryFechas +
                " GROUP BY EC_ACTIVIDADES.ACTIVIDAD_ID, EC_ACTIVIDADES.ACTIVIDAD_NOMBRE, EC_ACTIVIDADES.ACTIVIDAD_DESCRIPCION";
        return CeC_BD.EjecutaDataSet(Qry);
    }

    public static DataSet ObtenerDetallesDS(CeC_Sesion Sesion)
    {
        return ObtenerDetallesDS(new DateTime(), new DateTime(), Sesion);
    }
    public static DataSet ObtenerDetallesDS(DateTime FechaInicial, DateTime FechaFinal, CeC_Sesion Sesion)
    {
        string QryFechas = "";
        if (FechaInicial != new DateTime() && FechaFinal != new DateTime())
        {
            QryFechas = " AND ACTIVIDAD_INS_FECHA >= " + CeC_BD.SqlFechaHora(FechaInicial) + " AND ACTIVIDAD_INS_FECHA <= " + CeC_BD.SqlFechaHora(FechaFinal.AddDays(1)) + " \n";
        }
        string Qry = "SELECT EC_ACTIVIDAD_INS.ACTIVIDAD_INS_ID, EC_ACTIVIDAD_INS.ACTIVIDAD_ID, EC_ACTIVIDAD_INS.ACTIVIDAD_INS_FECHA,  \n" +
                " 	EC_ACTIVIDAD_INS.ACTIVIDAD_INS_DESCRIPCION, EC_ACTIVIDADES.ACTIVIDAD_NOMBRE, EC_EDO_ACTIVIDAD.EDO_ACTIVIDAD_NOMBRE,  \n" +
                " 	EC_PERSONAS.PERSONA_LINK_ID, EC_PERSONAS.PERSONA_NOMBRE, EC_PERSONAS.AGRUPACION_NOMBRE \n" +
                " FROM	EC_ACTIVIDADES INNER JOIN \n" +
                " 	EC_ACTIVIDAD_INS ON EC_ACTIVIDADES.ACTIVIDAD_ID = EC_ACTIVIDAD_INS.ACTIVIDAD_ID INNER JOIN \n" +
                " 	EC_PERSONAS ON EC_ACTIVIDAD_INS.PERSONA_ID = EC_PERSONAS.PERSONA_ID INNER JOIN \n" +
                " 	EC_TIPO_INSCRIPCION ON EC_ACTIVIDAD_INS.TIPO_INSCRIPCION_ID = EC_TIPO_INSCRIPCION.TIPO_INSCRIPCION_ID INNER JOIN \n" +
                " 	EC_EDO_ACTIVIDAD ON EC_ACTIVIDADES.EDO_ACTIVIDAD_ID = EC_EDO_ACTIVIDAD.EDO_ACTIVIDAD_ID \n" +
                " WHERE	(EC_ACTIVIDADES.ACTIVIDAD_BORRADO = 0) \n" +
                " AND EC_ACTIVIDADES." + CeC_Autonumerico.ValidaSuscripcion("EC_ACTIVIDADES", "ACTIVIDAD_ID", Sesion.SUSCRIPCION_ID) + " \n" + QryFechas +
                " \n ORDER BY EC_ACTIVIDADES.ACTIVIDAD_NOMBRE, EC_PERSONAS.AGRUPACION_NOMBRE, EC_PERSONAS.PERSONA_NOMBRE";
        return CeC_BD.EjecutaDataSet(Qry);
    }
}