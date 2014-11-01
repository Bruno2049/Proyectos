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

/// <summary>
/// Descripción breve de CMd_Sicoss
/// </summary>
public class CMd_Sicoss : CMd_Base
{
    public enum TipoAccesos
    {
        No_definido = 0,
        Correcto,
        Entrada,
        Salida,
        Salida_a_Comer,
        Regreso_de_comer,
        Incorrecto
    }
    public enum TipoAccesosSICOSS
    {
        No_definido = 0,
        Entrada,
        Salida,
        Salida_a_Comer,
        Regreso_de_comer,
        Incorrecto
    }
	public CMd_Sicoss()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
    /// <summary>
    /// Obtiene el nombre del módulo
    /// </summary>
    /// <returns></returns>
    public override string LeeNombre()
    {
        return "Integración Sicoss";
    }

    /// <summary>
    /// esta función será ejecutada en la clase de asistencias una instante
    /// despues de generar las faltas, y una vez cada hora
    /// </summary>
    /// <returns></returns>
    public override bool EjecutarUnaVezCadaHora()
    {
        if (EjecutaCadaHora)
            EjecutarUnaVezAlDia();
        return true;
    }

    /// <summary>
    /// esta función será ejecutada en la clase de asistencias una instante
    /// despues de generar las faltas, y una vez al día
    /// </summary>
    /// <returns></returns>
    public override bool EjecutarUnaVezAlDia()
    {
        try
        {            
            ListaEmpledos();
            EnviarChecadas();
            return true;
        }
        catch
        {
        }
        return false;
    }

    public override bool ActualizaEmpleados(int SuscripcionID, bool Manual)
    {
        try
        {
            ListaEmpledos();
            return true;
        }
        catch
        {
        }
        return false;
    }

    int m_CompaniaID = 1;
    [Description("Identificador de la Compañia")]
    [DisplayNameAttribute("IDComapañia")]
    ///Obtiene o establece la CompaniaID
    public int CompaniaID
    {
        get { return m_CompaniaID; }
        set { m_CompaniaID = value; }
    }
    int m_UltimoAccesoID = 0;
    [Description("Identificador del ultimo Acceso")]
    [DisplayNameAttribute("UltimoAccesoID")]
    ///Obtiene o establece el UltimoAcessoID
    public int UltimoAccesoID
    {
        get { return m_UltimoAccesoID; }
        set { m_UltimoAccesoID = value; }
    }

    bool m_EjecutaCadaHora = false;
    [Description("Indica si se ejecutara la sincronización con sicoss cada hora")]
    [DisplayNameAttribute("EjecutaCadaHora")]
    ///Obtiene o establece el si se ejecutara una vez cada hora
    public bool EjecutaCadaHora
    {
        get { return m_EjecutaCadaHora; }
        set { m_EjecutaCadaHora = value; }
    }

    string m_UrlWebServiceSicoss = "http://localhost:1375/SwSyncSicoss/SwSyncSicoss.asmx";
    [Description("Dirección del WebService instalado en SICOSS")]
    [DisplayNameAttribute("Url de WebService Sicoss")]
    ///Obtiene o establece la URLWebServiceSicoss
    public string UrlWebServiceSicoss
    {
        get { return m_UrlWebServiceSicoss; }
        set { m_UrlWebServiceSicoss = value; }
    }
    /// <summary>
    /// Actualiza la Tabla Empleados
    /// </summary>
    /// <param name="TablaEmpleados"></param>
    /// <returns></returns>
    public bool ActualizarPersonas(WS_Sicoss.DS_ListaPersonas.EmpleadosDataTable TablaEmpleados)
    {
        try
        {
            if (TablaEmpleados != null)
            {
                for (int i = 0; i < TablaEmpleados.Rows.Count; i++)
                {
                    int _borrado = TablaEmpleados[i][TablaEmpleados.EstatusColumn.Caption].ToString() != "B" ? 0 : 1;
                    int _registros = CeC_BD.EjecutaComando("UPDATE EC_PERSONAS SET PERSONA_BORRADO = " + _borrado.ToString() + " WHERE PERSONA_LINK_ID = " + Convert.ToInt32(TablaEmpleados[i][TablaEmpleados.Trab_IDColumn.Caption]));
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    /// <summary>
    /// Inserta y actualiza a los empleados
    /// </summary>
    public void ListaEmpledos()
    {
        try
        {
            int ret;
            DS_ValidacionesSicoss DS_Sicoss = new DS_ValidacionesSicoss();
            DS_ValidacionesSicossTableAdapters.EC_PERSONAS_DATOSTableAdapter DA_Sicoss = new DS_ValidacionesSicossTableAdapters.EC_PERSONAS_DATOSTableAdapter();
            WS_Sicoss.SwSyncSicoss WSSicoss = new WS_Sicoss.SwSyncSicoss();
            WSSicoss.Url = UrlWebServiceSicoss;
            WS_Sicoss.DS_ListaPersonas.EmpleadosDataTable DT_Empleados_Sicoss = WSSicoss.ListaEmpleados(CompaniaID);
            if (DT_Empleados_Sicoss != null)
            {
                for (int i = 0; i < DT_Empleados_Sicoss.Rows.Count; i++)
                {
                    DA_Sicoss.FillbyNoEmpleado(DS_Sicoss.EC_PERSONAS_DATOS, Convert.ToDecimal(DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.Trab_IDColumn.Caption]));
                    if (DS_Sicoss.EC_PERSONAS_DATOS.Rows.Count == 0)
                    {
                        //Inserta un empleado
                        ret = DA_Sicoss.InsertEmpleado(Convert.ToDecimal(DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.Trab_IDColumn.Caption]), DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.CURPColumn.Caption].ToString(), DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.PaternoColumn.Caption].ToString().Trim(), DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.MaternoColumn.Caption].ToString().Trim(), DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.NombreColumn.Caption].ToString().Trim(), DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.IMSSColumn.Caption].ToString()
                            , DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.RFCColumn.Caption].ToString(), Convert.ToDateTime(DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.FechaNacimientoColumn.Caption]), Convert.ToDateTime(DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.FechaIngresoColumn.Caption]), DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.CalleColumn.Caption].ToString(), DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.ColoniaColumn.Caption].ToString(), DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.CPColumn.Caption].ToString().Trim(), DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.EstadoColumn.Caption].ToString()
                        , DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.CiudadColumn.Caption].ToString(), DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.TelefonoColumn.Caption].ToString(), DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.PuestoColumn.Caption].ToString().Trim(), DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.DepartamentoColumn.Caption].ToString().Trim(), DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.CentroCostoColumn.Caption] == DBNull.Value ? "" : DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.CentroCostoColumn.Caption].ToString());
                    }
                    else
                    {
                        //Actualiza un empleado
                        ret = DA_Sicoss.UpdateEmpleado(Convert.ToDecimal(DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.Trab_IDColumn.Caption]), DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.CURPColumn.Caption].ToString(), DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.PaternoColumn.Caption].ToString().Trim(), DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.MaternoColumn.Caption].ToString().Trim(), DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.NombreColumn.Caption].ToString().Trim(), DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.IMSSColumn.Caption].ToString()
                            , DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.RFCColumn.Caption].ToString(), Convert.ToDateTime(DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.FechaNacimientoColumn.Caption]), Convert.ToDateTime(DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.FechaIngresoColumn.Caption]), DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.CalleColumn.Caption].ToString(), DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.ColoniaColumn.Caption].ToString(), DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.CPColumn.Caption].ToString(), DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.EstadoColumn.Caption].ToString()
                        , DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.CiudadColumn.Caption].ToString(), DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.TelefonoColumn.Caption].ToString(), DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.PuestoColumn.Caption].ToString().Trim(), DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.DepartamentoColumn.Caption].ToString().Trim(), DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.CentroCostoColumn.Caption] == DBNull.Value ? "" : DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.CentroCostoColumn.Caption].ToString().Trim(), Convert.ToDecimal(DT_Empleados_Sicoss[i][DT_Empleados_Sicoss.Trab_IDColumn.Caption]));
                    }
                }
                CeC_BD.CreaRelacionesEmpleados();
                ActualizarPersonas(DT_Empleados_Sicoss);
            }
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
    }
    /// <summary>
    /// Hace la conversion de los tipos de acceso
    /// </summary>
    /// <param name="TipoAcceso"></param>
    /// <returns></returns>
    private int ConversionTipoAccesos(object TipoAcceso)
    {
        switch ((TipoAccesos)(Convert.ToInt32(TipoAcceso)))
        {
            case TipoAccesos.Entrada: return (int)TipoAccesosSICOSS.Entrada; break;
            case TipoAccesos.Salida: return (int)TipoAccesosSICOSS.Salida; break;
            case TipoAccesos.Regreso_de_comer: return (int)TipoAccesosSICOSS.Regreso_de_comer; break;
            case TipoAccesos.Salida_a_Comer: return (int)TipoAccesosSICOSS.Salida_a_Comer; break;
            default: return (int)TipoAccesosSICOSS.No_definido; break;
        }
    }
    /// <summary>
    /// Envia los accesos checados por las terminales
    /// </summary>
    public void EnviarChecadas()
    {
        try
        {
            WS_Sicoss.SwSyncSicoss WSSicoss = new WS_Sicoss.SwSyncSicoss();
            WSSicoss.Url = UrlWebServiceSicoss;
            DS_ValidacionesSicoss DS_Sicoss = new DS_ValidacionesSicoss();
            DS_ValidacionesSicossTableAdapters.EC_ACCESOSTableAdapter DA_Accesos = new DS_ValidacionesSicossTableAdapters.EC_ACCESOSTableAdapter();
            WS_Sicoss.DS_ListaPersonas.EmpleadoIncidenciaRelojDataTable Tabla_Sicoss = new WS_Sicoss.DS_ListaPersonas.EmpleadoIncidenciaRelojDataTable();
            DA_Accesos.Fill(DS_Sicoss.EC_ACCESOS, (decimal)UltimoAccesoID);
            string Formato = WSSicoss.FormatoCompania(CompaniaID);
            if (DS_Sicoss.EC_ACCESOS != null && DS_Sicoss.EC_ACCESOS.Rows.Count != 0)
            {
                for (int i = 0; i < DS_Sicoss.EC_ACCESOS.Rows.Count; i++)
                {
                    WS_Sicoss.DS_ListaPersonas.EmpleadoIncidenciaRelojRow FilaNueva = Tabla_Sicoss.NewEmpleadoIncidenciaRelojRow();
                    FilaNueva[Tabla_Sicoss.Trab_IDColumn.Caption] = Convert.ToInt32(DS_Sicoss.EC_ACCESOS[i][DS_Sicoss.EC_ACCESOS.PERSONA_LINK_IDColumn.Caption]).ToString(Formato);
                    FilaNueva[Tabla_Sicoss.FechaHoraColumn.Caption] = DS_Sicoss.EC_ACCESOS[i][DS_Sicoss.EC_ACCESOS.ACCESO_FECHAHORAColumn.Caption];
                    FilaNueva[Tabla_Sicoss.Incidencia_IDColumn.Caption] = ConversionTipoAccesos(DS_Sicoss.EC_ACCESOS[i][DS_Sicoss.EC_ACCESOS.TIPO_ACCESO_IDColumn.Caption]);
                    Tabla_Sicoss.AddEmpleadoIncidenciaRelojRow(FilaNueva);
                }
                UltimoAccesoID = Convert.ToInt32(DS_Sicoss.EC_ACCESOS[DS_Sicoss.EC_ACCESOS.Rows.Count - 1][DS_Sicoss.EC_ACCESOS.ACCESO_IDColumn.Caption]);
                WSSicoss.RecibirChecadas(CompaniaID, Tabla_Sicoss);
            }
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
    }
}
