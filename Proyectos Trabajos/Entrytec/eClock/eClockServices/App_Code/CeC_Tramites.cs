using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Descripción breve de CeC_Tramites
/// </summary>
public class CeC_Tramites
{
    public CeC_Tramites()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public static int Nuevo(int TipoTramiteID, int PersonaID, int TipoPrioridadID, string Descripcion, CeC_Sesion Sesion)
    {
        eClockBase.Modelos.Model_TRAMITES Modelo = new eClockBase.Modelos.Model_TRAMITES();
        Modelo.TRAMITE_ID = -1;
        Modelo.TIPO_TRAMITE_ID = TipoTramiteID;
        Modelo.TIPO_PRIORIDAD_ID = TipoPrioridadID;
        Modelo.PERSONA_ID = PersonaID;
        Modelo.TRAMITE_FECHA = System.DateTime.Now;
        Modelo.TRAMITE_DESCRIPCION = Descripcion;
        int R = CeC_Tabla.GuardaDatos("EC_TRAMITES", "TRAMITE_ID", Modelo, true, Sesion, Sesion.SUSCRIPCION_ID);
        if (R > 0)
        {
            eClockBase.Modelos.Model_TIPO_TRAMITE TipoTramite = new eClockBase.Modelos.Model_TIPO_TRAMITE();
            TipoTramite.TIPO_TRAMITE_ID = TipoTramiteID;
            TipoTramite = Newtonsoft.Json.JsonConvert.DeserializeObject<eClockBase.Modelos.Model_TIPO_TRAMITE>(CeC_Tabla.ObtenDatos("EC_TIPO_TRAMITE", "TIPO_TRAMITE_ID", TipoTramite, Sesion));

            if (TipoTramite.TIPO_TRAMITE_MOMENTO != "")
            {
                string Detalle = "";
                eClockBase.Modelos.Personas.Model_Datos Datos = CeC_Personas.ObtenDatosPersonaModelo(PersonaID);
                Detalle = "Se ha iniciado un nuevo tramite por: " + Datos.PERSONA_NOMBRE + " (" + Datos.PERSONA_LINK_ID + ")" + CeC.SaltoLineaHtml;
                Detalle += "Con tramite: " + TipoTramite.TIPO_TRAMITE_NOMBRE + CeC.SaltoLineaHtml;
                Detalle += "Y los siguientes parámetros: " + Descripcion;
                CeC_Mails.EnviarMail(TipoTramite.TIPO_TRAMITE_MOMENTO, "Nueva tramite de " + TipoTramite.TIPO_TRAMITE_NOMBRE, Detalle);
            }
        }
        return R;
    }


    public static int Resumen()
    {
        int UltimoTramiteID = CeC_Config.TramiteResumenUltimoTramiteID;
        string Qry = "SELECT	EC_TIPO_TRAMITE.TIPO_TRAMITE_NOMBRE, EC_TIPO_TRAMITE.TIPO_TRAMITE_CAMPOS,  \n" +
" 	EC_TIPO_PRIORIDADES.TIPO_PRIORIDAD_NOMBRE, EC_PERSONAS.PERSONA_LINK_ID, EC_PERSONAS.PERSONA_NOMBRE,  \n" +
" 	EC_TRAMITES.TRAMITE_FECHA, EC_TRAMITES.TRAMITE_DESCRIPCION, EC_TIPO_TRAMITE.TIPO_TRAMITE_RESUMEN,  \n" +
" 	EC_TRAMITES.TRAMITE_ID, EC_TRAMITES.TIPO_TRAMITE_ID \n" +
" FROM	EC_TRAMITES INNER JOIN \n" +
" 	EC_PERSONAS ON EC_TRAMITES.PERSONA_ID = EC_PERSONAS.PERSONA_ID INNER JOIN \n" +
" 	EC_TIPO_TRAMITE ON EC_TRAMITES.TIPO_TRAMITE_ID = EC_TIPO_TRAMITE.TIPO_TRAMITE_ID INNER JOIN \n" +
" 	EC_TIPO_PRIORIDADES ON EC_TRAMITES.TIPO_PRIORIDAD_ID = EC_TIPO_PRIORIDADES.TIPO_PRIORIDAD_ID \n" +
" WHERE	(EC_TIPO_TRAMITE.TIPO_TRAMITE_RESUMEN IS NOT NULL AND EC_TIPO_TRAMITE.TIPO_TRAMITE_RESUMEN <> '') AND  \n" +
" 	(EC_TRAMITES.TRAMITE_ID > " + UltimoTramiteID + ") \n" +
"ORDER BY EC_TRAMITES.TIPO_TRAMITE_ID, EC_PERSONAS.PERSONA_NOMBRE";
        int Enviados = 0;
        DataSet DS = CeC_BD.EjecutaDataSet(Qry);
        if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
        {
            int TIPO_TRAMITE_ID_Anterior = -1;
            string Titulo = "";
            string Cuerpo = "";
            string eMails = "";
            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                Enviados++;
                string TIPO_TRAMITE_NOMBRE = CeC.Convierte2String(DR["TIPO_TRAMITE_NOMBRE"]);
                string TIPO_TRAMITE_CAMPOS = CeC.Convierte2String(DR["TIPO_TRAMITE_CAMPOS"]);
                string TIPO_PRIORIDAD_NOMBRE = CeC.Convierte2String(DR["TIPO_PRIORIDAD_NOMBRE"]);
                int PERSONA_LINK_ID = CeC.Convierte2Int(DR["PERSONA_LINK_ID"]);
                string PERSONA_NOMBRE = CeC.Convierte2String(DR["PERSONA_NOMBRE"]);
                DateTime TRAMITE_FECHA = CeC.Convierte2DateTime(DR["TRAMITE_FECHA"]);
                string TRAMITE_DESCRIPCION = CeC.Convierte2String(DR["TRAMITE_DESCRIPCION"]);
                string TIPO_TRAMITE_RESUMEN = CeC.Convierte2String(DR["TIPO_TRAMITE_RESUMEN"]);
                int TRAMITE_ID = CeC.Convierte2Int(DR["TRAMITE_ID"]);
                int TIPO_TRAMITE_ID = CeC.Convierte2Int(DR["TIPO_TRAMITE_ID"]);

                if (TRAMITE_ID > UltimoTramiteID)
                    UltimoTramiteID = TRAMITE_ID;

                if (TIPO_TRAMITE_ID != TIPO_TRAMITE_ID_Anterior)
                {
                    Cuerpo += "</table>";
                    if (TIPO_TRAMITE_ID_Anterior > 0)
                        CeC_Mails.EnviarMail(eMails, Titulo, Cuerpo);
                    TIPO_TRAMITE_ID_Anterior = TIPO_TRAMITE_ID;

                    Titulo = "Resumen de tramites de tipo " + TIPO_TRAMITE_NOMBRE;
                    eMails = TIPO_TRAMITE_RESUMEN;
                    Cuerpo = "Al momento (" + DateTime.Now.ToString() + ") " + CeC.SaltoLineaHtml;
                    Cuerpo += "Se las siguientes personas iniciaron el tramite " + TIPO_TRAMITE_NOMBRE + ":";
                    Cuerpo += "<table style=\"width:100%;\">";
                    Cuerpo += "<tr>";
                    Cuerpo += "<td>Empleado</td>";
                    Cuerpo += "<td>Datos</td>";
                    Cuerpo += "</tr>";
                }
                Cuerpo += "<tr>";
                Cuerpo += "<td>" + PERSONA_NOMBRE + " (" + CeC.Convierte2String(PERSONA_LINK_ID) + ")</td>";
                Cuerpo += "<td>" + TRAMITE_DESCRIPCION + "</td>";
                Cuerpo += "</tr>";
            }
            Cuerpo += "</table>";
            if (TIPO_TRAMITE_ID_Anterior > 0)
                CeC_Mails.EnviarMail(eMails, Titulo, Cuerpo);
            CeC_Config.TramiteResumenUltimoTramiteID = UltimoTramiteID;
        }

        return Enviados;
    }
    public static DataSet ObtenerNoTramitesDS(CeC_Sesion Sesion)
    {
        return ObtenerNoTramitesDS(new DateTime(), new DateTime(), Sesion);
    }
    public static DataSet ObtenerNoTramitesDS(DateTime FechaInicial, DateTime FechaFinal, CeC_Sesion Sesion)
    {
        string QryFechas = "";
        if (FechaInicial != new DateTime() && FechaFinal != new DateTime())
        {
            QryFechas = " AND TRAMITE_FECHA >= " + CeC_BD.SqlFechaHora(FechaInicial) + " AND TRAMITE_FECHA <= " + CeC_BD.SqlFechaHora(FechaFinal.AddDays(1)) + " \n";
        }
        string Qry = "SELECT EC_TIPO_TRAMITE.TIPO_TRAMITE_ID, EC_TIPO_TRAMITE.TIPO_TRAMITE_NOMBRE,  \n" +
                    " 	EC_TIPO_TRAMITE.TIPO_TRAMITE_AYUDA, COUNT(*) AS NO_TRAMITES \n" +
                    " FROM	EC_TRAMITES RIGHT OUTER JOIN \n" +
                    " 	EC_TIPO_TRAMITE ON EC_TRAMITES.TIPO_TRAMITE_ID = EC_TIPO_TRAMITE.TIPO_TRAMITE_ID \n" +
                    " WHERE	(EC_TIPO_TRAMITE.TIPO_TRAMITE_BORRADO = 0) \n" +
                    " AND EC_TIPO_TRAMITE." + CeC_Autonumerico.ValidaSuscripcion("EC_TIPO_TRAMITE", "TIPO_TRAMITE_ID", Sesion.SUSCRIPCION_ID) + " \n" +QryFechas+
                    " GROUP BY EC_TIPO_TRAMITE.TIPO_TRAMITE_ID, EC_TIPO_TRAMITE.TIPO_TRAMITE_NOMBRE, EC_TIPO_TRAMITE.TIPO_TRAMITE_AYUDA \n" +
                    " \n ORDER BY EC_TIPO_TRAMITE.TIPO_TRAMITE_NOMBRE";
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
            QryFechas = " AND TRAMITE_FECHA >= " + CeC_BD.SqlFechaHora(FechaInicial) + " AND TRAMITE_FECHA <= " + CeC_BD.SqlFechaHora(FechaFinal.AddDays(1)) + " \n";
        }
        string Qry = "SELECT EC_TRAMITES.TRAMITE_ID, EC_TRAMITES.TIPO_TRAMITE_ID, EC_TIPO_TRAMITE.TIPO_TRAMITE_NOMBRE,  \n" +
                " 	EC_TRAMITES.TRAMITE_FECHA, EC_TRAMITES.TRAMITE_DESCRIPCION, EC_PERSONAS.PERSONA_LINK_ID,  \n" +
                " 	EC_PERSONAS.PERSONA_NOMBRE, EC_PERSONAS.AGRUPACION_NOMBRE, EC_TIPO_PRIORIDADES.TIPO_PRIORIDAD_NOMBRE \n" +
                " FROM	EC_TIPO_TRAMITE INNER JOIN \n" +
                " 	EC_TRAMITES ON EC_TIPO_TRAMITE.TIPO_TRAMITE_ID = EC_TRAMITES.TIPO_TRAMITE_ID INNER JOIN \n" +
                " 	EC_PERSONAS ON EC_TRAMITES.PERSONA_ID = EC_PERSONAS.PERSONA_ID INNER JOIN \n" +
                " 	EC_TIPO_PRIORIDADES ON EC_TRAMITES.TIPO_PRIORIDAD_ID = EC_TIPO_PRIORIDADES.TIPO_PRIORIDAD_ID \n" +
                " WHERE	(EC_TIPO_TRAMITE.TIPO_TRAMITE_BORRADO = 0) \n" +
                " AND EC_TIPO_TRAMITE." + CeC_Autonumerico.ValidaSuscripcion("EC_TIPO_TRAMITE", "TIPO_TRAMITE_ID", Sesion.SUSCRIPCION_ID) + " \n" + QryFechas +
                " \n ORDER BY EC_TIPO_TRAMITE.TIPO_TRAMITE_NOMBRE, EC_PERSONAS.AGRUPACION_NOMBRE, EC_PERSONAS.PERSONA_NOMBRE";
        return CeC_BD.EjecutaDataSet(Qry);
    }
}