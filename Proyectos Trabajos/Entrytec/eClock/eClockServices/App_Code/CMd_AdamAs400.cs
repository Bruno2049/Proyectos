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
using System.Data.Odbc;

/// <summary>
/// Descripción breve de CMd_AdamAs400
/// </summary>
public class CMd_AdamAs400_Vac : CMd_Base
{
    /// <summary>
    /// Obtiene el nombre del módulo
    /// </summary>
    /// <returns></returns>
    public override string LeeNombre()
    {
        return "Adam Vacaciónes (AS400)";
    }
    string m_CadenaConexion = "";
    [Description("Contiene la cadena de conexión hacia la base de datos de adam AS400 (usar odbc)")]
    [DisplayNameAttribute("Cadena de Conexión")]
    ///Obtiene o establece la cadena de conexion
    public string CadenaConexion
    {
        get { return m_CadenaConexion; }
        set { m_CadenaConexion = value; }
    }

    string m_NombreDeCampos = "";
    [Description("Contiene los nombres de los campos en siguiente orden separados por coma TRACVE, FECHA_DESDE, FECHA_HASTA")]
    [DisplayNameAttribute("Campos")]
    ///Obtiene o establece el nombre de campos
    public string NombreDeCampos
    {
        get { return m_NombreDeCampos; }
        set { m_NombreDeCampos = value; }
    }
    string m_NombreDeTabla = "";
    [Description("Nombre de la tabla de vacaciones")]
    [DisplayNameAttribute("Tabla")]
    ///Obtiene o establece el nombre de la Tabla
    public string NombreDeTabla
    {
        get { return m_NombreDeTabla; }
        set { m_NombreDeTabla = value; }
    }

    string m_CampoFecha = "";
    [Description("Nombre del campo de fecha inicial")]
    [DisplayNameAttribute("Campo Fecha")]
    ///Obtiene o establece el CampoFecha
    public string CampoFecha
    {
        get { return m_CampoFecha; }
        set { m_CampoFecha = value; }
    }

    string m_INCIDENCIA_ID = "";
    [Description("Nombre de la incidencia de vacaciónes")]
    [DisplayNameAttribute("Incidencia")]
    ///Obtiene o establece la INCIDENCIA_ID
    public string INCIDENCIA_ID
    {
        get { return m_INCIDENCIA_ID; }
        set { m_INCIDENCIA_ID = value; }
    }

    /// <summary>
    /// Ingresa una nueva sesion, asignando Sesion_ID,Usuario_ID,Sesion_Inicio_FechaHora
    /// </summary>
    /// <returns></returns>
    private int InsertarNuevaSesion()
    {
        int sesion_id = CeC_Autonumerico.GeneraAutonumerico("EC_SESIONES", "SESION_ID");
        int ret = CeC_BD.EjecutaComando("INSERT INTO EC_SESIONES(SESION_ID,USUARIO_ID,SESION_INICIO_FECHAHORA) VALUES (" + sesion_id + ",0," + CeC_BD.SqlFechaHora(DateTime.Now) + ")");

        if (ret > 0)
            return sesion_id;
        else
            return 0;

    }
    /// <summary>
    /// Ingresa una nueva incidencia
    /// </summary>
    /// <param name="Tipo_incidencia_id">Tipo de Incidencia</param>
    /// <param name="Comentario"></param>
    /// <param name="Sesion_id"></param>
    /// <returns></returns>
    private int InsertarNuevaIncidencia(int Tipo_incidencia_id, string Comentario, int Sesion_id)
    {

        int inciedncia_id = CeC_Autonumerico.GeneraAutonumerico("EC_INCIDENCIAS", "INCIDENCIA_ID");
        string QRY = "INSERT INTO EC_INCIDENCIAS(INCIDENCIA_ID,TIPO_INCIDENCIA_ID,INCIDENCIA_COMENTARIO,INCIDENCIA_FECHAHORA,SESION_ID) VALUES (" + inciedncia_id + "," + Tipo_incidencia_id + ",'" + Comentario + "'," + CeC_BD.SqlFechaHora(DateTime.Now) + "," + Sesion_id + ")";
        int ret = CeC_BD.EjecutaComando(QRY);

        if (ret > 0)
            return inciedncia_id;
        else
            return 0;

    }
    /// <summary>
    /// Verifica las vaciones, evaluando las sentencias de las incidencias
    /// y las compara, una vez comparadas las actualiza en EC_PERSONAS_DIARIO
    /// y agrega un log
    /// </summary>
    public void VacacionesAS400toeClock()
    {
        CIsLog2.AgregaLog("iniciando VacacionesAS400toeClock");
        System.Data.DataSet Vac = ObtenerDataSetAS_400(DateTime.Today.Year, DateTime.Today.Year + 1);

        int Incidencias_vacaciones_id = CeC_Config.INCIDENCIAS_PERSONALIZADAS_VACACIONES_ID;

        if (Incidencias_vacaciones_id <= 0)
            return;

        int Sesion_id_var = InsertarNuevaSesion();

        for (int i = 0; i < Vac.Tables[0].Rows.Count; i++)
        {
            Array Valores = Vac.Tables[0].Rows[i].ItemArray;

            int Persona_Link_ID = Convert.ToInt32(Valores.GetValue(0));
            string F1 = Convert.ToInt32(Valores.GetValue(1)).ToString();
            string F2 = Convert.ToInt32(Valores.GetValue(2)).ToString();

            string Comentario = "|@" + Persona_Link_ID.ToString() + "|" + F1 + "|" + F2;
            int ret = CeC_BD.EjecutaEscalarInt("SELECT INCIDENCIA_ID FROM EC_INCIDENCIAS WHERE INCIDENCIA_COMENTARIO = '" + Comentario + "' ");
            if (ret <= 0)
            {

                int Incidencia_ID_ParaPersona_Diario = InsertarNuevaIncidencia(Incidencias_vacaciones_id, Comentario, Sesion_id_var);
                int PERSONA_ID = CeC_BD.EjecutaEscalarInt("SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_LINK_ID = " + Persona_Link_ID);


                //DateTime FechaInicial = Convert.ToDateTime(F1.ToString("YYYY/MM/DD"));
                //DateTime FechaFinal = Convert.ToDateTime(F2.ToString("YYYY/MM/DD"));

                DateTime FechaInicial = new DateTime(Convert.ToInt32(F1.Substring(0, 4)), Convert.ToInt32(F1.Substring(4, 2)), Convert.ToInt32(F1.Substring(6, 2)));
                DateTime FechaFinal = new DateTime(Convert.ToInt32(F2.Substring(0, 4)), Convert.ToInt32(F2.Substring(4, 2)), Convert.ToInt32(F2.Substring(6, 2)));

                for (DateTime j = FechaInicial; j < FechaFinal.AddDays(1); j = j.AddDays(1))
                {
                    try
                    {
                        string Update = "UPDATE EC_PERSONAS_DIARIO SET INCIDENCIA_ID = " + Incidencia_ID_ParaPersona_Diario + " WHERE PERSONA_ID = " + PERSONA_ID + " AND PERSONA_DIARIO_FECHA = " + CeC_BD.SqlFecha(j);
                        CIsLog2.AgregaLog("VacacionesAS400toeClock TRACVE = " + Persona_Link_ID + "Upd = " + Update);
                        CeC_BD.EjecutaComando(Update);
                    }
                    catch (Exception ex)
                    {
                        CIsLog2.AgregaError(ex);
                    }

                }
            }


        }
        CIsLog2.AgregaLog("Fin VacacionesAS400toeClock");
    }
    /// <summary>
    /// Regresa un valor verdadero si tiene una incidencia o vacaciones
    /// </summary>
    /// <param name="Persona_Link_ID"></param>
    /// <param name="Fecha1"></param>
    /// <param name="incidenciaComentario"></param>
    /// <returns></returns>
    private bool TieneVacaciones(int Persona_Link_ID, DateTime Fecha1, string incidenciaComentario)
    {
        int PERSONA_ID = 0;
        PERSONA_ID = CeC_BD.EjecutaEscalarInt("SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_LINK_ID = " + Persona_Link_ID);
        int INCIDENCIA_ID = CeC_BD.EjecutaEscalarInt("Select INCIDENCIA_ID from EC_PERSONAS_DIARIO WHERE PERSONA_DIARIO_FECHA = " + CeC_BD.SqlFecha(Fecha1) + " AND PERSONA_ID = " + PERSONA_ID);

        int ret = CeC_BD.EjecutaEscalarInt("SELECT TIPO_INCIDENCIA_ID FROM EC_INCIDENCIAS WHERE INCIDENCIA_COMENTARIO = '" + incidenciaComentario + "' AND INCIDENCIA_ID =" + INCIDENCIA_ID);

        if (ret > 0)
            return true;
        else
            return false;

    }
    /// <summary>
    /// Establece una conexion y obtiene un dataset de as400 en 
    /// caso contrario generara un error
    /// </summary>
    /// <param name="Ano1"></param>
    /// <param name="Ano2"></param>
    /// <returns></returns>
    private System.Data.DataSet ObtenerDataSetAS_400(int Ano1, int Ano2)
    {

        try
        {
            /*
            string str_Conexion = CeC_Sesion.ObtenerValorWebconfig("gBDatosAS400.ConnectionString");
            string camposASeleccionar = CeC_Sesion.ObtenerValorWebconfig("CamposQuerySelect");
            string NombreTabla = CeC_Sesion.ObtenerValorWebconfig("NombreTabla");
            string CampoComparacion = CeC_Sesion.ObtenerValorWebconfig("CamposWhere");*/

            string str_Conexion = CadenaConexion;
            string camposASeleccionar = NombreDeCampos;
            string NombreTabla = NombreDeTabla;
            string CampoComparacion = CampoFecha;

            string Qry = "Select " + camposASeleccionar + " from " + NombreTabla + " where " + CampoComparacion + " >= " + Ano1 + "0101 And " + CampoComparacion + " < " + Ano2 + "0101";

            OdbcConnection as400Conn = new OdbcConnection(str_Conexion);
            OdbcCommand commando = new OdbcCommand(Qry, as400Conn);
            OdbcDataReader lector;


            if (as400Conn.State != System.Data.ConnectionState.Open)
                as400Conn.Open();


            System.Data.DataTable DT = new System.Data.DataTable("Tabla1");
            System.Data.DataSet DS = new System.Data.DataSet("DSTabla1");
            System.Data.DataRow DR;

            DT.Columns.Add("Columna1", System.Type.GetType("System.String"));
            DT.Columns.Add("Columna2", System.Type.GetType("System.Decimal"));
            DT.Columns.Add("Columna3", System.Type.GetType("System.Decimal"));


            lector = commando.ExecuteReader();

            while (lector.Read())
            {
                DR = DT.NewRow();

                try
                {
                    DR[0] = lector.GetString(0);
                    DR[1] = lector.GetDecimal(1);
                    DR[2] = lector.GetDecimal(2);
                }
                catch (Exception ex)
                {
                    CIsLog2.AgregaError(ex);
                    Console.Write("Conexion a as400" + ex.Message);
                }

                DT.Rows.Add(DR);
            }

            DS.Tables.Add(DT);

            lector.Close();
            as400Conn.Close();
            return DS;

        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
            Console.Write("Conexion a as400" + ex.Message);
        }

        return null;

    }

    /// <summary>
    /// Esta función será ejecutada en la clase de asistencias una instante
    /// despues de generar las faltas, y una vez al día
    /// </summary>
    /// <returns></returns>
    public override bool EjecutarUnaVezAlDia()
    {
        try
        {
            VacacionesAS400toeClock();
            return true;
        }
        catch 
        {
        }
        return false;
    }
}
