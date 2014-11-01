using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;

public partial class WF_PeriodosE : System.Web.UI.Page
{
    // Variable donde se almacenan los datos de la sesión actual
    CeC_Sesion Sesion;
    // Veriable miembro de la clase que guarda el ID del Periodo que se está editando
    protected int m_Periodo_ID;

    /// <summary>
    /// Carga de la página de Edición de Perdiodos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Permite guardar los datos de sesión para su uso. Guarda los datos de sesión actual
        Sesion = CeC_Sesion.Nuevo(this);

        //Titulo y Descripcion del Form
        Sesion.TituloPagina = "Periodos-Edición";
        Sesion.DescripcionPagina = "Ingrese los datos de el Período";
        // Obtiene el ID del periodo desde el parametro de la sesion.
        if (Sesion.Parametros.Length > 0)
        {
            m_Periodo_ID = CeC.Convierte2Int(CeC.ObtenColumnaSeparador(CeC.ObtenColumnaSeparador(Sesion.Parametros, "|", 0), "~", 0));
        }
        // Si el periodo es nuevo se le asigna el valor -1 para que se genere un autonumerico para el nuevo ID
        if (m_Periodo_ID < 0)
        {
            m_Periodo_ID = -1;
        }
        // Checamos si es la primera vez que se carga la página y no contiene valores introducidos
        // por el cliente
        if (!IsPostBack)
        {
            //string[] lista = CeC_Campos.ObtenListaCamposTE().Split(',');
            //LlenarCombo(lista);

            // Cargamos el ID del Nombre del Periodo para que se llene el control Combo Box de Ingrgistics
            CargaCombo(Cbx_PERIODO_N_ID, "PERIODO_N_ID");
            // Cargamos el ID del Estado del Periodo para que se llene el control Combo Box de Ingrgistics
            CargaCombo(Cbx_EDO_PERIODO_ID, "EDO_PERIODO_ID");
            // Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Edición de Períodos", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
            // Cargamos los datos del formulario
            CargarDatos(true);
        }
    }

    /// <summary>
    /// Carga los datos para el control Combo Box de Infragistics.
    /// </summary>
    /// <param name="Combo">Nombre del control Combo Box de Infragistics.</param>
    /// <param name="Campo">Nombre del campo de donde se tomaran los datos para llenar el combo.</param>
    /// <returns>Catálogo asignado al control Combo Box de Infragistics</returns>
    protected bool CargaCombo(Infragistics.WebUI.WebCombo.WebCombo Combo, string Campo)
    {
        return CeC_Grid.AsignaCatalogo(Combo, Campo);
    }

    /// <summary>
    /// Botón que deshace los cambios hechos y vuelve a cargar los datos sin modificaciones.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Btn_DeshacerCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        // Pasamos TRUE a cargar datos para que lea los datos que estaban guardados y deshaga los cambios no guardados
        CargarDatos(true);
    }

    /// <summary>
    /// Carga o guarda los elementos que se muestran en el formulario.
    /// </summary>
    /// <param name="Cargar">Verdadero indica que leera los datos para mostrarlos en el formulario. 
    /// Falso indica que va a guardar los datos que se muestran en el formulario.</param>
    private void CargarDatos(bool Cargar)
    {
        // Objeto local que contiene los datos del Periodo seleccionado
        CeC_Periodos l_Periodo = new CeC_Periodos(m_Periodo_ID, Sesion);
        // Variables locales que guardaran las fechas de los periodos de nomina y de asistencia
        // Se inicializan con la fecha actual
        DateTime l_PeriodoNominaInic = DateTime.Now;
        DateTime l_PeriodoNominaFin = DateTime.Now;
        DateTime l_PeriodoAsistenciaInic = DateTime.Now;
        DateTime l_PeriodoAsistenciaFin = DateTime.Now;
        //Guardamos el valor de Periodo_N_ID para su uso posterior
        int l_Periodo_N_ID = l_Periodo.Periodo_N_Id;
        // Se inicializan los controles Combo Box de Infragistics con una cadena vacia
        if (Cbx_PERIODO_N_ID.DataValue == null)
            Cbx_PERIODO_N_ID.DataValue = "";
        if (Cbx_EDO_PERIODO_ID.DataValue == null)
            Cbx_EDO_PERIODO_ID.DataValue = "";
        // Se inicializan los controles Text Box con una cadena vacia
        if (Tbx_PERIODO_NO.Value == null)
            Tbx_PERIODO_NO.Value = 0;
        if (Tbx_PERIODO_ANO.Value == null)
            Tbx_PERIODO_ANO.Value = 0;
        try
        {
            // Si es una carga de datos
            if (Cargar)
            {
                
                // Asigna al control Combo Box de Infragistics el elemento guardado en la Base de Datos para el Periodo seleccionado
                CeC_Grid.SeleccionaID(Cbx_PERIODO_N_ID, l_Periodo.Periodo_N_Id);
                // Asigna al control Combo Box de Infragistics el elemento guardado en la Base de Datos para el Periodo seleccionado
                CeC_Grid.SeleccionaID(Cbx_EDO_PERIODO_ID, l_Periodo.Edo_Periodo_Id);
                // Asigna al control Web Date Picker de Infragistics el elemento guardado en la Base de Datos para el Periodo seleccionado
                Wdc_PERIODO_NOM_INICIO.Value = l_Periodo.Periodo_Nom_Inicio.Date;
                Wdc_PERIODO_NOM_FIN.Value = l_Periodo.Periodo_Nom_Fin.Date;
                Wdc_PERIODO_ASIS_INICIO.Value = l_Periodo.Periodo_Asis_Inicio.Date;
                Wdc_PERIODO_ASIS_FIN.Value = l_Periodo.Periodo_Asis_Fin.Date;
                // Asigna al control Text Box el elemento guardado en la Base de Datos para el Periodo seleccionado
                Tbx_PERIODO_NO.Text = l_Periodo.Periodo_No.ToString();
                Tbx_PERIODO_ANO.Text = l_Periodo.Periodo_Ano.ToString();
            }
            // Si no es una carga de datos y se va a modificar o guardar nuevos datos.
            else
            {
                // Guardamos las fechas introducidas en el control Web Date Picker de Infragistics
                l_PeriodoNominaInic = CeC.Convierte2DateTime(Wdc_PERIODO_NOM_INICIO.Value).Date;
                l_PeriodoNominaFin = CeC.Convierte2DateTime(Wdc_PERIODO_NOM_FIN.Value).Date;
                l_PeriodoAsistenciaInic = CeC.Convierte2DateTime(Wdc_PERIODO_ASIS_INICIO.Value).Date;
                l_PeriodoAsistenciaFin = CeC.Convierte2DateTime(Wdc_PERIODO_ASIS_FIN.Value).Date;
                // Actualizamos los datos introducidos en el formulario.
                // Si m_Periodo_ID es -1 significa que se esta creando un nuevo registro y se generara un autonumérico para m_Periodo_ID
                l_Periodo.Actualiza(m_Periodo_ID,
                    l_Periodo_N_ID,
                    CeC.Convierte2Int(Cbx_EDO_PERIODO_ID.DataValue),
                    l_PeriodoNominaInic,
                    l_PeriodoNominaFin,
                    l_PeriodoAsistenciaInic,
                    l_PeriodoAsistenciaFin,
                    CeC.Convierte2Int(Tbx_PERIODO_ANO.Value),
                    CeC.Convierte2Int(Tbx_PERIODO_NO.Value),
                    Sesion);
            }
        }
        catch { }

    }

    /// <summary>
    /// Código para el boton de Guardar los cambios
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Btn_GuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        try
        {
            // Objeto local que contiene los datos del Periodo seleccionado
            CeC_Periodos l_Periodo = new CeC_Periodos(m_Periodo_ID, Sesion);
            // Iniciamos las etiquetas de error
            Lbl_Error.Text = "";
            Lbl_Correcto.Text = "";


            // Validamos que no se deje ningún campo vacio
            // Coídgo comentado para Cbx_PERIODO_N_ID, ya que no se desea que se modifique este dato
            //if (Cbx_PERIODO_N_ID.DataValue == null)
            //{
            //    Lbl_Error.Text = "No se ha elegido el Período";
            //    return;
            //}
            if (Cbx_EDO_PERIODO_ID.DataValue == null)
            {
                Lbl_Error.Text = "No se ha elegido el Estado del Período";
                return;
            }
            if (Tbx_PERIODO_ANO.Value == null)
            {
                Lbl_Error.Text = "No se ha elegido el Estado del Período";
                return;
            }
            if (Tbx_PERIODO_NO.Value == null)
            {
                Lbl_Error.Text = "No se ha elegido el Estado del Período";
                return;
            }
        }
        catch { }
        try
        {
            // Se envia FALSE ya que se van a guardar los datos
            CargarDatos(false);
            // Mensaje de éxito al guardar los datos
            Lbl_Correcto.Text = "Los datos se han guardado satisfactoriamente";
        }
        catch (Exception ex)
        {
            // Mensaje de error al guardar los datos
            Lbl_Error.Text = "Error :" + ex.Message + "\nNo se han llenado los datos correctamente.";
        }
    }
}