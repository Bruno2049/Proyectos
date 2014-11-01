using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

using System.Web.Mail;
using System.Net.Mail;

using System.Threading;

using System.Data.OleDb;
using System.Data;

using System.Drawing;



/// <summary>
/// Descripción breve de WS_Asistencia
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WS_Asistencia : System.Web.Services.WebService
{

    public WS_Asistencia()
    {

        //Eliminar la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hola a todos";
    }

    /// <summary>
    /// Obtiene el Sesion_ID Actual en caso de haberse logeado
    /// </summary>
    /// <returns></returns>
    [WebMethod(Description = "Obtiene la sesion actual", EnableSession = true)]
    public int ObtenSESION_ID()
    {
        try { return Convert.ToInt32(Session["SESION_ID"]); }
        catch (Exception ex) { }
        return 0;
    }

    [WebMethod(Description = "Obtiene el usuarioID", EnableSession = true)]
    public int ObtenUsuario_ID()
    {
        try { return Convert.ToInt32(Session["USUARIO_ID"]); }
        catch (Exception ex) { }
        return 0;
    }

    [WebMethod(Description = "Obtiene la asistencia de empleados", MessageName = "ObtenAsistencia", EnableSession = true)]
    public DataSet ObtenAsistencia(bool EntradaSalida, bool Comida, bool HorasExtras, bool Totales, bool Incidencia, bool TurnoDia, string TiposIncidenciasSistemaIDs, string TiposIncidenciasIDs, bool MuestraAgrupacion, bool MuestraEmpleado, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal)
    {
        if (ObtenSESION_ID() <= 0)
            return null;
        return CeC_Asistencias.ObtenAsistencia(EntradaSalida, Comida, HorasExtras, Totales, Incidencia, TurnoDia, TiposIncidenciasSistemaIDs, TiposIncidenciasIDs, MuestraAgrupacion, MuestraEmpleado, Persona_ID, Agrupacion, FechaInicial, FechaFinal, CeC_Sesion.Nuevo(this));
    }

    [WebMethod(Description = "Obtiene la asistencia de empleados", MessageName = "ObtenAsistenciaLineal", EnableSession = true)]
    public DataSet ObtenAsistenciaLineal(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, string Campos, string OrdenarPor, string TiposIncidenciasSistemaIDs, string TiposIncidenciasIDs)
    {
        if (ObtenSESION_ID() <= 0)
            return null;
        return CeC_Asistencias.ObtenAsistenciaLineal(Persona_ID, Agrupacion, FechaFinal, FechaFinal, Campos, OrdenarPor, TiposIncidenciasSistemaIDs, TiposIncidenciasIDs, CeC_Sesion.Nuevo(this));
    }

    [WebMethod(Description = "Obtiene las horas extras de los empleados", MessageName = "ObtenHorasExtras", EnableSession = true)]
    public DataSet ObtenHorasExtras(bool EntradaSalida, bool Comida, bool Totales, bool Incidencia, bool TurnoDia, bool MuestraAgrupacion, bool MuestraEmpleado, bool MuestraCeros, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal)
    {
        if (ObtenSESION_ID() <= 0)
            return null;
        return CeC_AsistenciasHE.ObtenHorasExtras(EntradaSalida, Comida, Totales, Incidencia, TurnoDia, MuestraAgrupacion, MuestraEmpleado, MuestraCeros, Persona_ID, Agrupacion, FechaInicial, FechaFinal, ObtenUsuario_ID());

    }
}
