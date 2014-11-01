using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;

/// <summary>
/// Descripción breve de WS_Interfaz
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WS_Interfaz : System.Web.Services.WebService
{

    public WS_Interfaz()
    {

        //Eliminar la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    /// <summary>
    /// Importa empleados
    /// </summary>
    /// <returns>regresa el numero de registros que se importaron</returns>
    [WebMethod(Description = "Importación de Empleados", EnableSession = true)]
    public int ImportaEmpleados(DS_WS_Interfaz.EC_PERSONAS_DATOSDataTable DT_Empleados)
    {
        CeC_Sesion Sesion = CeC_Sesion.Nuevo(this);
        if (Sesion.USUARIO_ID < 0)
            return -100;
        if (Sesion.SESION_ID < 0)
            return -101;

        return CeC_Empleados.ImportaRegistros(DT_Empleados, true, Sesion.SUSCRIPCION_ID, Sesion.SESION_ID);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="PERIODO_N_ID">Identificador de nombre del periodo</param>
    /// <param name="TIPO_NOMINA_NOMBRE">Nombre del tipo de nomina</param>
    /// <param name="TIPO_NOMINA_IDEX">Identificador en el sistema externo, debe ser unico por suscripcion</param>
    /// <returns></returns>
    [WebMethod(Description = "Agrega un tipo de nomina", EnableSession = true)]
    public int AgragaTipoDeNomina(int PERIODO_N_ID, string TIPO_NOMINA_NOMBRE, string TIPO_NOMINA_IDEX)
    {
        CeC_Sesion Sesion = CeC_Sesion.Nuevo(this);
        if (Sesion.USUARIO_ID < 0)
            return -100;
        if (Sesion.SESION_ID < 0)
            return -101;
        return CeC_TiposNomina.Agraga(PERIODO_N_ID, TIPO_NOMINA_NOMBRE, TIPO_NOMINA_IDEX, Sesion.SESION_ID, Sesion.SUSCRIPCION_ID);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="PERIODO_N_NOM">Nombre del periodo</param>
    /// <param name="PERIODO_N_DESC">Descripcion o comentario </param>
    /// <param name="TIMESPAN_ID">0-Ninguno,1-Anual, 2-Mensual, 3-Quincenal, 4-Semanal, 5 diario,6-Catorcenal.7-semestral</param>
    /// <param name="PERIODO_N_FECHA">Fecha desde donde se iniciará el conteo de intervalo de tiempo</param>
    /// <param name="PERIODO_N_DASIS">Dias a restar para el periodo de asistencia</param>
    /// <returns></returns>
    [WebMethod(Description = "Agrega un Nombre de periodo", EnableSession = true)]
    public int AgregaPeriodoN(string PERIODO_N_NOM, string PERIODO_N_DESC, int TIMESPAN_ID,
    DateTime PERIODO_N_FECHA, int PERIODO_N_DASIS)
    {
        CeC_Sesion Sesion = CeC_Sesion.Nuevo(this);
        if (Sesion.USUARIO_ID < 0)
            return -100;
        if (Sesion.SESION_ID < 0)
            return -101;
        return CeC_Periodos_N.AgregaPeriodoN(PERIODO_N_NOM, PERIODO_N_DESC, TIMESPAN_ID, PERIODO_N_FECHA, "", PERIODO_N_DASIS, Sesion.SESION_ID, Sesion.SUSCRIPCION_ID);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="PERIODO_N_ID">Identifificador de periodo nombre</param>
    /// <param name="EDO_PERIODO_ID">Estado del periodo</param>
    /// <param name="PERIODO_NOM_INICIO"></param>
    /// <param name="PERIODO_NOM_FIN"></param>
    /// <param name="PERIODO_ANO"></param>
    /// <param name="PERIODO_NO"></param>
    /// <returns></returns>
    [WebMethod(Description = "Agrega un periodo", EnableSession = true)]
    public int AgregaPeriodo(int PERIODO_N_ID, int EDO_PERIODO_ID, DateTime PERIODO_NOM_INICIO,
        DateTime PERIODO_NOM_FIN,
        int PERIODO_ANO, int PERIODO_NO)
    {
        CeC_Sesion Sesion = CeC_Sesion.Nuevo(this);
        if (Sesion.USUARIO_ID < 0)
            return -100;
        if (Sesion.SESION_ID < 0)
            return -101;
        CeC_Periodos_N PeriodoNombre = new CeC_Periodos_N(PERIODO_N_ID);
        if (PeriodoNombre.PERIODO_N_ID < 0)
            return -102;
        return CeC_Periodos.AgregaPeriodo(PERIODO_N_ID, EDO_PERIODO_ID, PERIODO_NOM_INICIO, PERIODO_NOM_FIN,
            PERIODO_NOM_INICIO.AddDays(-PeriodoNombre.PERIODO_N_DASIS), PERIODO_NOM_FIN.AddDays(-PeriodoNombre.PERIODO_N_DASIS), PERIODO_ANO, PERIODO_NO, Sesion.SESION_ID, Sesion.SUSCRIPCION_ID);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="IDExtTXT"></param>
    /// <param name="Nombre"></param>
    /// <param name="Descripcion"></param>
    /// <param name="Parametros"></param>
    /// <param name="TIPO_FALTA_EX_ID"></param>
    /// <returns></returns>
    [WebMethod(Description = "Agrega un tipo de incidencia externo", EnableSession = true)]
    public int AgregaTipoIncidenciaEx(string IDExtTXT, string Nombre, string Descripcion, string Parametros, int TIPO_FALTA_EX_ID)
    {
        CeC_Sesion Sesion = CeC_Sesion.Nuevo(this);
        if (Sesion.USUARIO_ID < 0)
            return -100;
        if (Sesion.SESION_ID < 0)
            return -101;
        return Cec_Incidencias.TipoIncidenciaExAgrega(Sesion.SESION_ID, Sesion.SUSCRIPCION_ID, IDExtTXT, Nombre, Descripcion, Parametros, TIPO_FALTA_EX_ID);
    }

}
