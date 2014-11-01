using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class WF_SitiosE : System.Web.UI.Page
{
    DS_Sitios DS;
    CeC_Sesion Sesion;
    DS_SitiosTableAdapters.EC_SITIOSTableAdapter TA;
    DS_Sitios.EC_SITIOSRow Fila;
    protected int m_SitioID;

    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        //Titulo y Descripcion del Form
        Sesion.TituloPagina = "Edición de Sitios";
        Sesion.DescripcionPagina = "Ingrese los datos de el sitio";
        // Checamos si es la primera vez que se carga la página y no contiene valores introducidos
        // por el cliente
        if (Sesion.Parametros.Length > 0)
        {
            m_SitioID = CeC.Convierte2Int(CeC.ObtenColumnaSeparador(CeC.ObtenColumnaSeparador(Sesion.Parametros, "|", 0), "~", 0));
        }
        // Si la configuración es nueva se le asigna el valor -1 para que se genere un autonumerico para el nuevo ID
        if (m_SitioID < 0)
        {
            m_SitioID = -1;
        }
        // Checamos si es la primera vez que se carga la página y no contiene valores introducidos
        // por el cliente
        if (!IsPostBack)
        {
            // Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Edición de Sitios", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
            // Cargamos los datos del formulario
            CargarDatos(true);
        }
    }

    public int SuscripcionID
    {
        get
        {
            if (Sesion.Parametros.Length > 0)
            {
                try
                {
                    return Convert.ToInt32(Sesion.Parametros);
                }
                catch { }
            }
            return Sesion.SUSCRIPCION_ID;
        }
    }

    private void CargarDatos(bool Cargar)
    {
        // Variable local que guardara los datos del sitio
        CeC_Sitios l_Sitio = new CeC_Sitios(m_SitioID, Sesion);
        // Variables locales que guardaran las fechas de los periodos de nomina y de asistencia
        // Se inicializan con la fecha actual
        DateTime l_SincronizaDesde = DateTime.Now;
        DateTime l_SincronizaHasta = DateTime.Now;
        
        // Se inicializan los controles Text Box con una cadena vacia
        // El Sitio ID debe de ser -1 para que se pueda crear un nuevo Sitio
        if (Wne_SITIO_ID.Value == null)
            Wne_SITIO_ID.Value = -99999;
        if (Wtx_SITIO_NOMBRE.Value == null)
            Wtx_SITIO_NOMBRE.Value = "";
        if (Wtx_SITIO_CONSULTA.Value == null)
            Wtx_SITIO_CONSULTA.Value = "";
        if (Wne_SITIO_SEGUNDOS_SYNC.Value == null)
            Wne_SITIO_SEGUNDOS_SYNC.Value = 0;
        try
        {
            // Si es una carga de datos
            if (Cargar)
            {
                // Asigna al control Text Box el elemento guardado en la Base de Datos para el elemento seleccionado
                try
                {
                    //Wne_SITIO_ID.Text = l_Sitio.Sitio_Id.ToString();
                    Wne_SITIO_ID.Text = m_SitioID.ToString();
                }
                catch (Exception ex)
                { CIsLog2.AgregaError("WF_SitiosE.CargarDatos", ex); }
                try
                {
                    Wtx_SITIO_NOMBRE.Text = l_Sitio.Sitio_Nombre.ToString();
                }
                catch (Exception ex)
                { CIsLog2.AgregaError("WF_SitiosE.CargarDatos", ex); }
                try
                {
                    Wtx_SITIO_CONSULTA.Text = l_Sitio.Sitio_Consulta.ToString();
                }
                catch (Exception ex)
                { CIsLog2.AgregaError("WF_SitiosE.CargarDatos", ex); }
                try
                {
                    Wde_SITIO_HDESDE_SVEC.Value = l_Sitio.Sitio_Hdesde_Svec.TimeOfDay;
                }
                catch (Exception ex)
                { CIsLog2.AgregaError("WF_SitiosE.CargarDatos", ex); }
                try
                {
                    Wde_SITIO_HHASTA_SVEC.Value = l_Sitio.Sitio_Hhasta_Svec.TimeOfDay;
                }
                catch (Exception ex)
                { CIsLog2.AgregaError("WF_SitiosE.CargarDatos", ex); }
                try
                {
                    Wne_SITIO_SEGUNDOS_SYNC.Text = l_Sitio.Sitio_Segundos_Sync.ToString();
                }
                catch (Exception ex)
                { CIsLog2.AgregaError("WF_SitiosE.CargarDatos", ex); }
                try
                {
                    Wtx_SITIO_RESPONSABLES.Text = l_Sitio.Sitio_Responsables.ToString();
                }
                catch (Exception ex)
                { CIsLog2.AgregaError("WF_SitiosE.CargarDatos", ex); }
                try
                {
                    Wtx_SITIO_TELEFONOS.Text = l_Sitio.Sitio_Telefonos.ToString();
                }
                catch (Exception ex)
                { CIsLog2.AgregaError("WF_SitiosE.CargarDatos", ex); }
                try
                {
                    Wtx_SITIO_DIRECCION_1.Text = l_Sitio.Sitio_Direccion_1.ToString();
                }
                catch (Exception ex)
                { CIsLog2.AgregaError("WF_SitiosE.CargarDatos", ex); }
                try
                { 
                    Wtx_SITIO_DIRECCION_2.Text = l_Sitio.Sitio_Direccion_2.ToString();
                }
                catch (Exception ex)
                { CIsLog2.AgregaError("WF_SitiosE.CargarDatos", ex); }
                try
                {
                    Wtx_SITIO_REFERENCIAS.Text = l_Sitio.Sitio_Referencias.ToString();
                }
                catch (Exception ex)
                { CIsLog2.AgregaError("WF_SitiosE.CargarDatos", ex); }
                try
                {
                    Wtx_SITIO_COMENTARIOS.Text = l_Sitio.Sitio_Comentarios.ToString();
                }
                catch (Exception ex)
                { CIsLog2.AgregaError("WF_SitiosE.CargarDatos", ex); }
            }
            // Si no es una carga de datos y se va a modificar o guardar nuevos datos.
            else
            {
                if(Wde_SITIO_HDESDE_SVEC.Text != "")
                    l_SincronizaDesde = CeC.Convierte2DateTime(Wde_SITIO_HDESDE_SVEC.Value);
                if (Wde_SITIO_HHASTA_SVEC.Text != "")
                    l_SincronizaHasta = CeC.Convierte2DateTime(Wde_SITIO_HHASTA_SVEC.Value);
                if (l_Sitio.Actualiza(  CeC.Convierte2Int(Wne_SITIO_ID.Value),
                                        CeC.Convierte2String(Wtx_SITIO_NOMBRE.Value),
                                        CeC.Convierte2String(Wtx_SITIO_CONSULTA.Value),
                                        l_SincronizaDesde,
                                        l_SincronizaHasta,
                                        CeC.Convierte2Int(Wne_SITIO_SEGUNDOS_SYNC.Value),
                                        Chb_SITIO_BORRADO.Checked,
                                        CeC.Convierte2String(Wtx_SITIO_RESPONSABLES.Value),
                                        CeC.Convierte2String(Wtx_SITIO_TELEFONOS.Value),
                                        CeC.Convierte2String(Wtx_SITIO_DIRECCION_1.Value),
                                        CeC.Convierte2String(Wtx_SITIO_DIRECCION_2.Value),
                                        CeC.Convierte2String(Wtx_SITIO_REFERENCIAS.Value),
                                        CeC.Convierte2String(Wtx_SITIO_COMENTARIOS.Value),
                                        Sesion))
                {
                    Lbl_Correcto.Text = "Datos guardados";
                    Lbl_Error.Text = "";
                }
                else
                {
                    Lbl_Error.Text = "Error al guardar los datos";
                    Lbl_Correcto.Text = "";
                    return;
                }
            }
        }
        catch { }

    }

    protected void Btn_Guardar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        try
        {
            CargarDatos(false);
        }
        catch (Exception ex)
        {
            Lbl_Error.Text = "Error :" + ex.Message + "\nNo se han llenado los datos correctamente.";
        }
    }
    protected void Btn_Deshacer_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        CargarDatos(true);
    }
}
