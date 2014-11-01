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

public partial class WF_ConfigUsuarioE : System.Web.UI.Page
{

    // Variable donde se almacenan los datos de la sesión actual
    CeC_Sesion Sesion; 
    protected int m_ConfigUsuarioID;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Permite guardar los datos de sesión para su uso. Guarda los datos de sesión actual
        Sesion = CeC_Sesion.Nuevo(this);

        //Titulo y Descripcion del Form
        Sesion.TituloPagina = "Edición-Configuración";
        Sesion.DescripcionPagina = "Ingrese los datos de Configuración";
        // Obtiene el ID de la configuración desde el parametro de la sesion.
        if (Sesion.Parametros.Length > 0)
        {
            m_ConfigUsuarioID = CeC.Convierte2Int(CeC.ObtenColumnaSeparador(CeC.ObtenColumnaSeparador(Sesion.Parametros, "|", 0), "~", 0));
        }
        // Si la configuración es nueva se le asigna el valor -1 para que se genere un autonumerico para el nuevo ID
        if (m_ConfigUsuarioID < 0)
        {
            m_ConfigUsuarioID = -1;
        }
        CargaCombo(Cbx_USUARIO_ID, "USUARIO_ID");
        // Checamos si es la primera vez que se carga la página y no contiene valores introducidos
        // por el cliente
        if (!IsPostBack)
        {
            CargaCombo(Cbx_USUARIO_ID, "USUARIO_ID");
            // Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Edición de Configuración", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
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
        CeC_ConfigUsuario l_ConfiguracionUsuario = new CeC_ConfigUsuario(m_ConfigUsuarioID, Sesion);
        // Variables locales que guardaran las fechas de los periodos de nomina y de asistencia
        int l_ConfigUsuarioID = l_ConfiguracionUsuario.Config_Usuario_Id;
        Byte[] l_CONFIG_USUARIO_VALOR_BIN = {0 };
        // Se inicializan los controles Combo Box de Infragistics con una cadena vacia
        if (Cbx_USUARIO_ID.DataValue == null)
            Cbx_USUARIO_ID.DataValue = "";
        // Se inicializan los controles Text Box con una cadena vacia
        if (Tbx_CONFIG_USUARIO_VALOR.Value == null)
            Tbx_CONFIG_USUARIO_VALOR.Value = "";
        if (Tbx_CONFIG_USUARIO_VARIABLE.Value == null)
            Tbx_CONFIG_USUARIO_VARIABLE.Value = 0;
        
        try
        {
            // Si es una carga de datos
            if (Cargar)
            {
                // Asigna al control Combo Box de Infragistics el elemento guardado en la Base de Datos para el elemento seleccionado
                CeC_Grid.SeleccionaID(Cbx_USUARIO_ID, l_ConfiguracionUsuario.Usuario_Id);
                // Asigna al control Text Box el elemento guardado en la Base de Datos para el elemento seleccionado
                Tbx_CONFIG_USUARIO_VARIABLE.Text = l_ConfiguracionUsuario.Config_Usuario_Variable.ToString();
                Tbx_CONFIG_USUARIO_VALOR.Text = l_ConfiguracionUsuario.Config_Usuario_Valor.ToString();
                //Fup_CONFIG_USUARIO_VALOR_BIN..FileBytes = l_ConfiguracionUsuario.Config_Usuario_Valor_Bin;
            }
            // Si no es una carga de datos y se va a modificar o guardar nuevos datos.
            else
            {

                l_ConfiguracionUsuario.Actualiza(m_ConfigUsuarioID,
                    CeC.Convierte2Int(Cbx_USUARIO_ID.DataValue),
                    Tbx_CONFIG_USUARIO_VARIABLE.Text,
                    Tbx_CONFIG_USUARIO_VALOR.Text,
                    Fup_CONFIG_USUARIO_VALOR_BIN.FileBytes,
                    Sesion);
            }
        }
        catch { }

    }

    protected void Btn_GuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        byte[] l_ValorBinDefault = {0};
        int l_LongArchivo = 1;
        string l_UbicacionArchivo = "";

        if (Chb_CONFIG_USUARIO_VALOR_BIN.Checked)
        {
            // Get the length of the file.
            l_LongArchivo = Fup_CONFIG_USUARIO_VALOR_BIN.PostedFile.ContentLength;
        }
        else
        {
            l_LongArchivo = 0;
        }
        try
        {
            // Objeto local que contine los datos de la configuración seleccionada
            CeC_ConfigUsuario l_ConfiguracionUsuario = new CeC_ConfigUsuario(m_ConfigUsuarioID, Sesion);
            // Inicializamos las etiquetas de error
            Lbl_Error.Text = "";
            Lbl_Correcto.Text = "";

            // Validamos que no se deje ningún campo vacio
            if(Cbx_USUARIO_ID.DataValue == null)
            {
                Lbl_Error.Text = "No se ha seleccionado nungún usuario";
                return;
            }
            if (Tbx_CONFIG_USUARIO_VARIABLE.Value.ToString() == "")
            {
                Lbl_Error.Text = "No se ha introducido ninguna variable";
                return;
            }
            if (Tbx_CONFIG_USUARIO_VALOR.Value.ToString() == "")
            {
                Lbl_Error.Text = "No se ha introducido ningún valor para la variable";
                return;
            }
            if (l_LongArchivo == 0 && Chb_CONFIG_USUARIO_VALOR_BIN.Checked == true)
            {
                Lbl_Error.Text = "No se ha elegido un archivo para subir";
                l_LongArchivo = 0;
                return;
            }
            else
            {
                // Create a byte array to hold the contents of the file.
                if (l_LongArchivo == 0)
                    l_LongArchivo = 2;
                byte[] input = new byte[l_LongArchivo - 1];
                input = Fup_CONFIG_USUARIO_VALOR_BIN.FileBytes;
                // Copy the byte array to a string.
                for (int loop1 = 0; loop1 < l_LongArchivo; loop1++)
                {
                    l_UbicacionArchivo = l_UbicacionArchivo + input[loop1].ToString();
                }

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
    protected void Chb_CONFIG_USUARIO_VALOR_BIN_CheckedChanged(object sender, EventArgs e)
    {
        Fup_CONFIG_USUARIO_VALOR_BIN.Visible = Chb_CONFIG_USUARIO_VALOR_BIN.Checked;
        if (!Chb_CONFIG_USUARIO_VALOR_BIN.Checked)
        {
            Lbl_Error.Text = "";
        }
    }
}