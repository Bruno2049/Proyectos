using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.OleDb;
using System.Data;

/// <summary>
/// Descripción breve de WS_Reportes
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.None)]


public class WS_Reportes : System.Web.Services.WebService {

    DS_WS_Reportes ds_WS_Reportes = new DS_WS_Reportes();
    DS_WS_ReportesTableAdapters.EmpleadosIncongruentesTableAdapter TAReportesEmInc = new DS_WS_ReportesTableAdapters.EmpleadosIncongruentesTableAdapter();
    DS_WS_ReportesTableAdapters.EmpleadosDentroTableAdapter TAEmpleadosDentro = new DS_WS_ReportesTableAdapters.EmpleadosDentroTableAdapter();
    DS_WS_ReportesTableAdapters.RetardosporPersonaTableAdapter TARetardosPersona = new DS_WS_ReportesTableAdapters.RetardosporPersonaTableAdapter();
    DS_WS_ReportesTableAdapters.EmpleadosFaltasTableAdapter TAFaltasPersona = new DS_WS_ReportesTableAdapters.EmpleadosFaltasTableAdapter();
    DS_WS_ReportesTableAdapters.EmpleadosAsistenciaTableAdapter TAAsistenciaPersona = new DS_WS_ReportesTableAdapters.EmpleadosAsistenciaTableAdapter();
    DS_WS_ReportesTableAdapters.EC_SUSCRIPCIONTableAdapter TAGrupo1 = new DS_WS_ReportesTableAdapters.EC_SUSCRIPCIONTableAdapter();
    DS_WS_ReportesTableAdapters.EC_GRUPOS_2TableAdapter TAGrupo2 = new DS_WS_ReportesTableAdapters.EC_GRUPOS_2TableAdapter();
  
    public WS_Reportes () {

        //Eliminar la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    /// <summary>
    /// Envia un datatable de los Empleados con Incongruencia en el status
    /// </summary>
    /// <param name="IDTipo"></param>
    /// <param name="IDArea"></param>
    /// <param name="FechaInicio"></param>
    /// <param name="FechaFinal"></param>
    /// <returns></returns>

    [WebMethod(Description = "Envia DataTable de Incongruencia", EnableSession = true)]
    public DS_WS_Reportes.EmpleadosIncongruentesDataTable EnviaDTIncongruencia(DateTime FechaInicio,DateTime FechaFinal, string Usuario, string Password)
    {
        string[] Qry = new string[3];
        if (CeC_Sesion.ValidarUsuarioeC(Usuario, Password) <= 0)
            return null;
      
        return TAReportesEmInc.GetDataByFI_FF(FechaInicio, FechaFinal);
    }
    [WebMethod(Description = "Envia DataTable de Empleados dentro de Instalaciones por una hora Especifica", EnableSession = true)]
    public DS_WS_Reportes.EmpleadosDentroDataTable EnviaDTEmpleadosDentroporDiaHoraEspefica(int IDTipo, DateTime FechaInicio, DateTime HoraBusqueda, string Usuario, string Password)
    {
        TAEmpleadosDentro.ActualizaIn("(EC_ACCESOS.ACCESO_ID IN (SELECT MAX(ACCESO_ID) AS ACCESO_ID FROM EC_ACCESOS AS EC_ACCESOS_1"+
                            " WHERE (ACCESO_FECHAHORA >= CONVERT(DATETIME, '"+FechaInicio.ToString("yyyy-MM-dd")+" ' + '00:00:00', 102)) AND (ACCESO_FECHAHORA < CONVERT(DATETIME, " +
                            " '" + FechaInicio.ToString("yyyy-MM-dd") + " ' + '" + HoraBusqueda.ToString("HH:mm:ss") + "', 102)) GROUP BY PERSONA_ID))");
        if (CeC_Sesion.ValidarUsuarioeC(Usuario, Password) <= 0)
            return null;
        if (IDTipo > 0)
            return TAEmpleadosDentro.GetDataByTipoFecha(IDTipo);
        else
            return TAEmpleadosDentro.GetDatabyFechaHora(FechaInicio, HoraBusqueda);

    }
    [WebMethod(Description = "Envia DataTable de Empleados que se encuentran dentro de Instalaciones hasta la hora actual", EnableSession = true)]
    public DS_WS_Reportes.EmpleadosDentroDataTable EnviaDTEmpleadosDentroporHoraActual(int IDTipo,string Usuario, string Password)
    {
        if (CeC_Sesion.ValidarUsuarioeC(Usuario, Password) <= 0)
            return null;
        if (IDTipo > 0)
            return TAEmpleadosDentro.GetDataByDiaHoraActual();
        else
            return TAEmpleadosDentro.GetDataByTipoHoraActual(IDTipo);


    }
    [WebMethod(Description = "Envia DataTable de Empleados que tienen retardos", EnableSession = true)]
    public DS_WS_Reportes.RetardosporPersonaDataTable EnviaDTRetardosporPersonaporTipo(int IDTipo,DateTime FechaInicio,DateTime FechaFinal, string Usuario, string Password)
    {
        if (CeC_Sesion.ValidarUsuarioeC(Usuario, Password) <= 0)
            return null;
        if (IDTipo > 0)
            return TARetardosPersona.GetDataByTIpo__Fechas(FechaInicio, FechaFinal, IDTipo);
        else
            return TARetardosPersona.GetDataRangoFechas(FechaInicio, FechaFinal);


    }

    [WebMethod(Description = "Envia DataTable de Empleados que tienen retardos", EnableSession = true)]
    public DS_WS_Reportes.RetardosporPersonaDataTable EnviaDTRetardosporPersonaporRango(DateTime FechaInicio, DateTime FechaFinal, string Usuario, string Password)
    {
        if (CeC_Sesion.ValidarUsuarioeC(Usuario, Password) <= 0)
            return null;
      return TARetardosPersona.GetDataRangoFechas(FechaInicio, FechaFinal);


    }
    [WebMethod(Description = "Envia DataTable de Empleados que tienen retardos", EnableSession = true)]
    public DS_WS_Reportes.RetardosporPersonaDataTable EnviaDTRetardosporPersonaTipoArea(int IDTipo, string IDArea,DateTime FechaInicio, DateTime FechaFinal, string Usuario, string Password)
    {
        if (CeC_Sesion.ValidarUsuarioeC(Usuario, Password) <= 0)
            return null;
        if (IDTipo <= 0)
            return TARetardosPersona.GetDataByAreaFecha(FechaInicio, FechaFinal, IDArea);
        else
            return TARetardosPersona.GetDataByTipoAreaFechas(FechaInicio, FechaFinal,IDTipo,IDArea);
     

    }
    [WebMethod(Description = "Envia DataTable de Empleados que tienen faltas", EnableSession = true)]
    public DS_WS_Reportes.EmpleadosFaltasDataTable EnviaDTFaltasPersona(int IDTipo, string Nombre, decimal PersonaID, DateTime FechaInicio, DateTime FechaFinal, string Usuario, string Password)
    {

        if (CeC_Sesion.ValidarUsuarioeC(Usuario, Password) <= 0)
            return null;
        try
        {
            if (IDTipo <= 0)
            {
                if (PersonaID <= 0 && Nombre == "" )
                    return TAFaltasPersona.GetDatabyFechas(FechaInicio, FechaFinal);
                else if (PersonaID <= 0)
                    return TAFaltasPersona.GetDataByFechasNombre(FechaInicio, FechaFinal,Nombre);
                else if ( Nombre == "" )
                    return TAFaltasPersona.GetDataByFechasPersonaID(FechaInicio, FechaFinal, PersonaID);
                else
                    return TAFaltasPersona.GetDataByFechasPersonaIDNombre(FechaInicio, FechaFinal,PersonaID, Nombre);
            }
            else
            {
                if (PersonaID <= 0 && Nombre == "")
                    return TAFaltasPersona.GetDataByFechasTipo(FechaInicio, FechaFinal,IDTipo);
                else if (PersonaID <= 0)
                    return TAFaltasPersona.GetDataByFechasTipoNombre(FechaInicio, FechaFinal,IDTipo,Nombre);
                else if ( Nombre == "" )
                    return TAFaltasPersona.GetDataByFechasTipoPersonaID(FechaInicio, FechaFinal, PersonaID,IDTipo);
                else
                    return TAFaltasPersona.GetDataByFechasTipoPersonaIDNombre(FechaInicio, FechaFinal,PersonaID, IDTipo,Nombre);

            
            }
        }
        catch (Exception ex)
        {

            CIsLog2.AgregaError(ex);
        }
        return null;


    }
    [WebMethod(Description = "Envia DataTable Asistencia de Empleados ", EnableSession = true)]
    public DS_WS_Reportes.EmpleadosAsistenciaDataTable EnviaDTAsistenciasPersona(int IDTipo, string NombreArea, DateTime FechaInicio, DateTime FechaFinal, string Usuario, string Password)
    {
  
        if (CeC_Sesion.ValidarUsuarioeC(Usuario, Password) <= 0)
            return null;
        if (IDTipo < 1 && NombreArea=="")
            return TAAsistenciaPersona.GetDataByFechas(FechaInicio, FechaFinal);
        else if (NombreArea=="")
            return TAAsistenciaPersona.GetDataByTipoFechas(FechaInicio, FechaFinal, IDTipo);
        else
            return TAAsistenciaPersona.GetDataByTipoAreaFechas(FechaInicio, FechaFinal, IDTipo, NombreArea);
      

    }
    [WebMethod(Description = "Envia Grupo1 equivalente al Tipo Usuario", EnableSession = true)]
    public DS_WS_Reportes.EC_SUSCRIPCIONDataTable EnviaDTTipoUsuario(string Usuario, string Password)
    {
        if (CeC_Sesion.ValidarUsuarioeC(Usuario, Password) <= 0)
            return null;
        return TAGrupo1.GetData();
    }
    [WebMethod(Description = "Envia Grupo2 equivalente al Area", EnableSession = true)]
    public DS_WS_Reportes.EC_GRUPOS_2DataTable EnviaDTTipoArea(string Usuario, string Password)
    {
        if (CeC_Sesion.ValidarUsuarioeC(Usuario, Password) <= 0)
            return null;
        return TAGrupo2.GetData();
    }
    [WebMethod(Description = "Envia ID de Tipo Usuario", EnableSession = true)]
    public int EnviaIDTipoUsuario(string NombreTipo, string Usuario, string Password)
    {
        if (CeC_Sesion.ValidarUsuarioeC(Usuario, Password) <= 0)
            return -9999;
        return (int)TAGrupo1.EscalarIDGrupo1(NombreTipo);

    }
    [WebMethod(Description = "Envia ID del Area", EnableSession = true)]
    public int EnviaIDTipoArea(string NombreArea, string Usuario, string Password)
    {
        if (CeC_Sesion.ValidarUsuarioeC(Usuario, Password) <= 0)
            return -9999;
            return (int)TAGrupo2.EscalarIDGrupo2(NombreArea);

    }

  
}

