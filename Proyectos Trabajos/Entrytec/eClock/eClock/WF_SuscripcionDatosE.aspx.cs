using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

public partial class WF_SuscripcionDatosE : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    int m_SuscripcionID;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Permite guardar los datos de sesión para su uso
        Sesion = CeC_Sesion.Nuevo(this);

        //Titulo y Descripcion del Form
        Sesion.TituloPagina = "Suscripción-Edición";
        Sesion.DescripcionPagina = "Ingrese los datos de la suscripción";
        if (Sesion.Parametros.Length > 0)
        {
            m_SuscripcionID = CeC.Convierte2Int(CeC.ObtenColumnaSeparador(CeC.ObtenColumnaSeparador(Sesion.Parametros, "|", 0), "~", 0));
        }
        // Si la configuración es nueva se le asigna el valor -1 para que se genere un autonumerico para el nuevo ID
        if (m_SuscripcionID < 0)
        {
            m_SuscripcionID = -1;
        }
        // Inicializa combos
        CargaCombo(Wco_SUSCRIPCION_NOMBRE, "SUSCRIPCION_ID");
        CargaCombo(Wco_EDO_SUSCRIPCION_ID, "EDO_SUSCRIPCION_ID");
        CargaCombo(Wco_SUSCRIP_PRECIO_ID, "SUSCRIP_PRECIO_ID");
        
        if (!IsPostBack)
        {
            // Inicializa combos
            CargaCombo(Wco_SUSCRIPCION_NOMBRE, "SUSCRIPCION_ID");
            CargaCombo(Wco_EDO_SUSCRIPCION_ID, "EDO_SUSCRIPCION_ID");
            CargaCombo(Wco_SUSCRIP_PRECIO_ID, "SUSCRIP_PRECIO_ID");
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Edición Suscripción", Sesion.USUARIO_ID_SUSCRIPCION, Sesion.USUARIO_NOMBRE);
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

    /// Botón que deshace los cambios hechos y vuelve a cargar los datos sin modificaciones.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void WIBtn_Deshacer_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
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

        // Objeto local que contiene los datos de la suscripción seleccionada
        CeC_Suscripcion l_SuscripcionID = new CeC_Suscripcion(m_SuscripcionID, Sesion);
        // Variables que guardan las fechas de contratacion
        // Se inicializan con la fecha actual
        DateTime l_FechaContratacion = DateTime.Now;
        DateTime l_FechaFinalContrato = DateTime.Now;

        // Se inicializan los controles Text Box con una cadena vacia
        //if (Wdc_SUSCRIP_DATOS_CONTRATACION.Value == null)
        //Wdc_SUSCRIP_DATOS_CONTRATACION.Value = l_FechaContratacion.Date;
        ////if (Wdc_SUSCRIP_DATOS_FINAL.Text == null)
        //Wdc_SUSCRIP_DATOS_FINAL.Value = l_FechaFinalContrato.Date;
        if (Wtx_SUSCRIP_DATOS_RAZON.Value == null)
            Wtx_SUSCRIP_DATOS_RAZON.Value = "";
        if (Wtx_SUSCRIP_DATOS_RFC.Value == null)
            Wtx_SUSCRIP_DATOS_RFC.Value = "";
        if (Wtx_SUSCRIP_DATOS_DIRECCION1.Value == null)
            Wtx_SUSCRIP_DATOS_DIRECCION1.Value = "";
        if (Wtx_SUSCRIP_DATOS_DIRECCION2.Value == null)
            Wtx_SUSCRIP_DATOS_DIRECCION2.Value = "";
        if (Wtx_SUSCRIP_DATOS_CIUDAD.Value == null)
            Wtx_SUSCRIP_DATOS_CIUDAD.Value = "";
        if (Wtx_SUSCRIP_DATOS_ESTADO.Value == null)
            Wtx_SUSCRIP_DATOS_ESTADO.Value = "";
        if (Wtx_SUSCRIP_DATOS_PAIS.Value == null)
            Wtx_SUSCRIP_DATOS_PAIS.Value = "";
        if (Wne_SUSCRIP_DATOS_EMPLEADOS.Value == null)
            Wne_SUSCRIP_DATOS_EMPLEADOS.Value = 0;
        if (Wne_SUSCRIP_DATOS_TERMINALES.Value == null)
            Wne_SUSCRIP_DATOS_TERMINALES.Value = 0;
        if (Wne_SUSCRIP_DATOS_USUARIOS.Value == null)
            Wne_SUSCRIP_DATOS_USUARIOS.Value = 0;
        if (Wne_SUSCRIP_DATOS_ALUMNOS.Value == null)
            Wne_SUSCRIP_DATOS_ALUMNOS.Value = 0;
        if (Wne_SUSCRIP_DATOS_VISITANTES.Value == null)
            Wne_SUSCRIP_DATOS_VISITANTES.Value = 0;
        if (Wtx_SUSCRIP_DATOS_OTROS.Value == null)
            Wtx_SUSCRIP_DATOS_OTROS.Value = "";
        if (Wne_SUSCRIP_DATOS_MENSUAL.Value == null)
            Wne_SUSCRIP_DATOS_MENSUAL.Value = 0;
        try
        {
            // Si es una carga de datos
            if (Cargar)
            {
                // Asigna al control Combo Box de Infragistics el elemento guardado en la Base de Datos para el elemento seleccionado
                CeC_Grid.SeleccionaID(Wco_SUSCRIPCION_NOMBRE, m_SuscripcionID);
                CeC_Grid.SeleccionaID(Wco_SUSCRIP_PRECIO_ID, l_SuscripcionID.Suscrip_Precio_Id);
                CeC_Grid.SeleccionaID(Wco_EDO_SUSCRIPCION_ID, l_SuscripcionID.Edo_Suscripcion_Id);

                // Asigna al control Text Box el elemento guardado en la Base de Datos para el elemento seleccionado
                Wne_SUSCRIPCION_ID.Value = m_SuscripcionID;
                // Se inicializan los controles Text Box con una cadena vacia
                Wtx_SUSCRIP_DATOS_RAZON.Value = l_SuscripcionID.Suscripcion_Razon;
                Wtx_SUSCRIP_DATOS_RFC.Value = l_SuscripcionID.Suscripcion_Rfc;
                Wtx_SUSCRIP_DATOS_DIRECCION1.Value = l_SuscripcionID.Suscripcion_Direccion1;
                Wtx_SUSCRIP_DATOS_DIRECCION2.Value = l_SuscripcionID.Suscripcion_Direccion2;
                Wtx_SUSCRIP_DATOS_CIUDAD.Value = l_SuscripcionID.Suscripcion_Ciudad;
                Wtx_SUSCRIP_DATOS_ESTADO.Value = l_SuscripcionID.Suscripcion_Estado;
                Wtx_SUSCRIP_DATOS_PAIS.Value = l_SuscripcionID.Suscripcion_Pais;
                Chb_SUSCRIP_DATOS_FACTURAR.Checked = l_SuscripcionID.Suscripcion_Facturar;
                Wne_SUSCRIP_DATOS_EMPLEADOS.Value = l_SuscripcionID.Suscripcion_Empleados;
                Wne_SUSCRIP_DATOS_TERMINALES.Value = l_SuscripcionID.Suscripcion_Terminales;
                Wne_SUSCRIP_DATOS_USUARIOS.Value = l_SuscripcionID.Suscripcion_Usuarios;
                Wne_SUSCRIP_DATOS_ALUMNOS.Value = l_SuscripcionID.Suscripcion_Alumnos;
                Wne_SUSCRIP_DATOS_VISITANTES.Value = l_SuscripcionID.Suscripcion_Visitantes;
                Chb_SUSCRIP_DATOS_ADICIONALES.Checked = l_SuscripcionID.Suscripcion_Adicionales;
                Wtx_SUSCRIP_DATOS_OTROS.Value = l_SuscripcionID.Suscripcion_Otros;
                Wne_SUSCRIP_DATOS_MENSUAL.Value = l_SuscripcionID.Suscripcion_Mensual;
                Wdc_SUSCRIP_DATOS_CONTRATACION.Value = l_SuscripcionID.Suscripcion_Contratacion.Date;
                Wdc_SUSCRIP_DATOS_FINAL.Value = l_SuscripcionID.Suscripcion_Final.Date;
            }
            // Si no es una carga de datos y se va a modificar o guardar nuevos datos.
            else
            {
                l_FechaContratacion = CeC.Convierte2DateTime(Wdc_SUSCRIP_DATOS_CONTRATACION.Value).Date;
                l_FechaFinalContrato = CeC.Convierte2DateTime(Wdc_SUSCRIP_DATOS_FINAL.Value).Date;
                //if (Wdc_SUSCRIP_DATOS_CONTRATACION.Text != "")
                //    l_FechaContratacion = CeC.Convierte2DateTime(Wdc_SUSCRIP_DATOS_CONTRATACION.Value).Date;
                //if (Wdc_SUSCRIP_DATOS_FINAL.Text != "")
                //    l_FechaFinalContrato = CeC.Convierte2DateTime(Wdc_SUSCRIP_DATOS_FINAL.Value).Date;
                if (l_SuscripcionID.Actualiza(CeC.Convierte2Int(Wne_SUSCRIPCION_ID.Value),
                    CeC.Convierte2Int(Wco_EDO_SUSCRIPCION_ID.DataValue),
                        CeC.Convierte2Int(Wco_SUSCRIP_PRECIO_ID.DataValue),
                        Wtx_SUSCRIP_DATOS_RAZON.Value.ToString(),
                        Chb_SUSCRIPCION_BORRADO.Checked,
                        Wtx_SUSCRIP_DATOS_RAZON.Value.ToString(),
                        Wtx_SUSCRIP_DATOS_RFC.Value.ToString(),
                        Wtx_SUSCRIP_DATOS_DIRECCION1.Value.ToString(),
                        Wtx_SUSCRIP_DATOS_DIRECCION2.Value.ToString(),
                        Wtx_SUSCRIP_DATOS_CIUDAD.Value.ToString(),
                        Wtx_SUSCRIP_DATOS_ESTADO.Value.ToString(),
                        Wtx_SUSCRIP_DATOS_PAIS.Value.ToString(),
                        Chb_SUSCRIP_DATOS_FACTURAR.Checked,
                        l_FechaContratacion.Date,
                        CeC.Convierte2Int(Wne_SUSCRIP_DATOS_EMPLEADOS.Value),
                        CeC.Convierte2Int(Wne_SUSCRIP_DATOS_TERMINALES.Value),
                        CeC.Convierte2Int(Wne_SUSCRIP_DATOS_USUARIOS.Value),
                        CeC.Convierte2Int(Wne_SUSCRIP_DATOS_ALUMNOS.Value),
                        CeC.Convierte2Int(Wne_SUSCRIP_DATOS_VISITANTES.Value),
                        Chb_SUSCRIP_DATOS_ADICIONALES.Checked,
                        Wtx_SUSCRIP_DATOS_OTROS.Value.ToString(),
                        CeC.Convierte2Int(Wne_SUSCRIP_DATOS_MENSUAL.Value),
                        l_FechaFinalContrato.Date,
                        Sesion))
                {
                    Lbl_Correcto.Text = "Datos guardados";
                    Lbl_Correcto.Text = "";
                }
                else
                {
                    Lbl_Error.Text = "Error al guardar los datos";
                    Lbl_Correcto.Text = "";
                }
            }
        }
        catch { }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void WIBtn_Guardar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        try
        {
            CargarDatos(false);
            Sesion.Redirige("WF_SuscripcionE.aspx?Parametros=" + Wne_SUSCRIPCION_ID.Text);
        }
        catch (Exception ex)
        {
            Lbl_Error.Text = "Error :" + ex.Message + "\nNo se han llenado los datos correctamente.";
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void  WIBtn_Salir_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.Redirige("WF_SuscripcionE.aspx?Parametros=" + Wne_SUSCRIPCION_ID.Text);
    }
}