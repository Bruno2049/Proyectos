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
using System.IO;
using System.Globalization;

public partial class WF_TerminalesEd : System.Web.UI.Page
{
    // 
    string m_TConexion = "";
    // Variable global que permite guardar los datos de la sesión
    CeC_Sesion Sesion;
    NumberFormatInfo numInfo = new NumberFormatInfo();

    protected void Page_Load(object sender, EventArgs e)
    {
        // Quitamos las comas como separador para los numeros.
        numInfo.NumberGroupSeparator = "";
        this.Wne_TERMINAL_DIR_485_ID.NumberFormat = numInfo;
        this.Wne_TERMINAL_DIR_485_PUERTO.NumberFormat = numInfo;
        this.Wne_TERMINAL_DIR_485_VELOCIDAD.NumberFormat = numInfo;
        this.Wne_TERMINAL_DIR_MODEM_TEL.NumberFormat = numInfo;
        this.Wne_TERMINAL_DIR_MODEM_VELOCIDAD.NumberFormat = numInfo;
        this.Wne_TERMINAL_DIR_RED_ID.NumberFormat = numInfo;
        this.Wne_TERMINAL_DIR_RED_PUERTO.NumberFormat = numInfo;
        this.Wne_TERMINAL_DIR_SERIAL_PUERTO.NumberFormat = numInfo;
        this.Wne_TERMINAL_DIR_SERIAL_VELOCIDAD.NumberFormat = numInfo;
        this.Wne_TERMINAL_DIR_USB_ID.NumberFormat = numInfo;
        this.Wne_TERMINAL_ID.NumberFormat = numInfo;
        this.Wne_TERMINAL_MINUTOS_DIF.NumberFormat = numInfo;
        this.Wne_TERMINAL_SINCRONIZACION.NumberFormat = numInfo;
        // Inicializamos la variable de la actual sesión para su uso
        Sesion = CeC_Sesion.Nuevo(this);

        //Titulo y Descripcion del Form
        Sesion.TituloPagina = "Terminales-Edición";
        Sesion.DescripcionPagina = "Ingrese los datos de la terminal";

        // Permisos****************************************
        string[] Permiso = new string[10];
        Permiso[0] = eClock.CEC_RESTRICCIONES.S0Terminales0Nuevo;
        Permiso[1] = eClock.CEC_RESTRICCIONES.S0Terminales0Editar;
        Permiso[2] = eClock.CEC_RESTRICCIONES.S0Terminales0Borrar;

        if (!Sesion.Acceso(Permiso, CIT_Perfiles.Acceso(Sesion.PERFIL_ID, this)))
        {
            CIT_Perfiles.CrearVentana(this, Sesion.MensajeVentanaJScript(), Sesion.TituloPagina, "Aceptar", "WF_Main.aspx", "", "");
            Habilitarcontroles(false, Sesion.Restriccion.ToString());
            return;
        }

        Habilitarcontroles(false, Sesion.Restriccion.ToString());
        //**************************************************

        // Determina si la página se está cargando como respuesta a un valor devuelto por el cliente, o si es la primera vez que se carga y se obtiene acceso a la misma.
        if (!IsPostBack)
        {
            //Inicializa Combos    
            CeC_Grid.AsignaCatalogo(Wco_TIPO_TERMINAL_ACCESO_ID, "TIPO_TERMINAL_ACCESO_ID");
            CeC_Grid.AsignaCatalogo(Wco_ALMACEN_VEC_ID, "ALMACEN_VEC_ID");
            CeC_Sitios.AsignaCatalogo(Wco_SITIO_ID, Sesion.SuscripcionParametro);
            CeC_Grid.AsignaCatalogo(Wco_MODELO_TERMINAL_ID, "MODELO_TERMINAL_ID");
            CeC_Grid.AsignaCatalogo(Wco_TIPO_TECNOLOGIA_ID, "TIPO_TECNOLOGIA_ID");
            CeC_Grid.AsignaCatalogo(Wco_TIPO_TECNOLOGIA_ADD_ID, "TIPO_TECNOLOGIA_ID");
            CeC_Grid.AsignaCatalogo(Wco_TERMINAL_CAMPO_LLAVE, "TERMINAL_CAMPO_LLAVE");
            CeC_Grid.AsignaCatalogo(Wco_TERMINAL_CAMPO_ADICIONAL, "TERMINAL_CAMPO_ADICIONAL");

            string[] lista = CeC_Campos.ObtenListaCamposTE().Split(',');
            LlenarCombo(lista);
            CeC_Grid.AplicaFormato(Wco_TIPO_TERMINAL_ACCESO_ID);
            CeC_Grid.AplicaFormato(Wco_ALMACEN_VEC_ID);
            CeC_Grid.AplicaFormato(Wco_SITIO_ID);
            CeC_Grid.AplicaFormato(Wco_MODELO_TERMINAL_ID);
            CeC_Grid.AplicaFormato(Wco_TIPO_TECNOLOGIA_ID);
            CeC_Grid.AplicaFormato(Wco_TIPO_TECNOLOGIA_ADD_ID);
            CeC_Grid.AplicaFormato(Wco_TERMINAL_CAMPO_LLAVE);
            CeC_Grid.AplicaFormato(Wco_TERMINAL_CAMPO_ADICIONAL);

            //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Edición de Terminales", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);

            CargarDatos(true);
        }
        else
        {
            /*Oculta el WPRED si es terminal maestra*/
            try
            {
                bool EsLectora = Convert.ToBoolean(CeC_BD.EjecutaEscalarInt("SELECT COUNT (*) FROM EC_TERMINALES_PARAM WHERE TERMINALES_PARAM_NOMBRE ='SITIO_HIJO_ID' AND TERMINALES_PARAM_VALOR = " + Wco_SITIO_ID.DataValue));
                if (EsLectora == true)
                    Wpn_RED_.Visible = false;
                else
                    Wpn_RED_.Visible = true;
            }
            catch (Exception ex)
            {
                CIsLog2.AgregaError("WF_TerminalesEd.aspx", ex);
            }
        }
    }

    /// <summary>
    /// Botón que guarda los cambios o una nueva terminal
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void WIBtn_Guardar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        try
        {
            Lbl_Error.Text = "";
            Lbl_Correcto.Text = "";
            bool EsLectora = Convert.ToBoolean(CeC_BD.EjecutaEscalarInt("SELECT COUNT (*) FROM EC_TERMINALES_PARAM WHERE TERMINALES_PARAM_NOMBRE ='SITIO_HIJO_ID' AND TERMINALES_PARAM_VALOR = " + Wco_SITIO_ID.DataValue));
            CeC_Terminales l_TerminalConexion = new CeC_Terminales(Sesion);
            if (!EsLectora)
            {
                string l_CampoLlave = Wco_TERMINAL_CAMPO_LLAVE.DataValue.ToString();
                // Validamos que introduzcan los campos necesarios
                // Validamos solo valores positivos
                if (l_CampoLlave.IndexOf("-") > 0)
                {
                    Lbl_Error.Text = "No se admiten valores negativos";
                    return;
                }
                if (Wco_TERMINAL_CAMPO_LLAVE.DataValue == null)
                {
                    Lbl_Error.Text = "No se ha elegido el campo llave de la terminal";
                    return;
                }
                if (Wco_MODELO_TERMINAL_ID.SelectedIndex == -1)
                {
                    Lbl_Error.Text = "Debe de seleccionar un modelo de Terminal";
                    return;
                }
                if (Wco_TIPO_TECNOLOGIA_ID.SelectedIndex == -1)
                {
                    Lbl_Error.Text = "Debe de seleccionar un Tipo de Tecnologia";
                    return;
                }
                if (Wco_TIPO_TECNOLOGIA_ADD_ID.SelectedIndex == -1 && Chb_TIPO_TECNOLOGIA_ADD_ID.Checked)
                {
                    Lbl_Error.Text = "Debe de seleccionar un Tipo de Tecnologia en la Tecnologia Adicional";
                    return;
                }
                //if (Chb_TERMINALES_DEXTRAS_BIN.Checked && Img_TERMINALES_DEXTRAS_BIN.ImageUrl == "")
                //{
                //    Lbl_Error.Text = "No se ha elegido una imagen de la ubicación de la terminal";
                //    return;
                //}
                switch (Rbn_TERMINAL_DIR.SelectedIndex)
                {
                    case 0:
                        l_TerminalConexion.TipoConexion = CeC_Terminales.tipo.USB;
                        l_TerminalConexion.Direccion = Wtx_TERMINAL_DIR_USB_NOMBRE.Text;
                        l_TerminalConexion.NoTerminal = Convert.ToInt32(Wne_TERMINAL_DIR_USB_ID.Value);
                        Wpn_USB_.Visible = true;
                        Wpn_SERIAL_.Visible = false;
                        Wpn_MODEM_.Visible = false;
                        Wpn_485_.Visible = false;
                        Wpn_RED_.Visible = false;
                        break;
                    case 1:
                        if (Wne_TERMINAL_DIR_SERIAL_PUERTO.Text == "" || Wne_TERMINAL_DIR_SERIAL_VELOCIDAD.Text == "")
                        {
                            Lbl_Error.Text = "Debe especificar todos los datos de la conexion en la terminal";
                            return;
                        }
                        l_TerminalConexion.TipoConexion = CeC_Terminales.tipo.Serial;
                        l_TerminalConexion.Puerto = Convert.ToInt32(Wne_TERMINAL_DIR_SERIAL_PUERTO.Value);
                        l_TerminalConexion.Velocidad = Convert.ToInt32(Wne_TERMINAL_DIR_SERIAL_VELOCIDAD.Value);
                        Wpn_USB_.Visible = false;
                        Wpn_SERIAL_.Visible = true;
                        Wpn_MODEM_.Visible = false;
                        Wpn_485_.Visible = false;
                        Wpn_RED_.Visible = false;
                        break;
                    case 2:
                        if (Wne_TERMINAL_DIR_485_PUERTO.Text == "" || Wne_TERMINAL_DIR_485_VELOCIDAD.Text == "" || Wne_TERMINAL_DIR_485_ID.Text == "")
                        {
                            Lbl_Error.Text = "Debe especificar todos los datos de la conexion en la terminal";
                            return;
                        }
                        l_TerminalConexion.TipoConexion = CeC_Terminales.tipo.RS485;
                        l_TerminalConexion.Puerto = Convert.ToInt32(Wne_TERMINAL_DIR_485_PUERTO.Value);
                        l_TerminalConexion.Velocidad = Convert.ToInt32(Wne_TERMINAL_DIR_485_VELOCIDAD.Value);
                        l_TerminalConexion.NoTerminal = Convert.ToInt32(Wne_TERMINAL_DIR_485_ID.Value);
                        Wpn_USB_.Visible = false;
                        Wpn_SERIAL_.Visible = false;
                        Wpn_MODEM_.Visible = false;
                        Wpn_485_.Visible = true;
                        Wpn_RED_.Visible = false;
                        break;
                    case 3:
                        if (Wne_TERMINAL_DIR_MODEM_TEL.Text == "" || Wne_TERMINAL_DIR_MODEM_PUERTO.Text == "" || Wne_TERMINAL_DIR_MODEM_VELOCIDAD.Text == "")
                        {
                            Lbl_Error.Text = "Debe especificar todos los datos de la conexion en la terminal";
                            return;
                        }
                        l_TerminalConexion.TipoConexion = CeC_Terminales.tipo.Modem;
                        l_TerminalConexion.Direccion = Wne_TERMINAL_DIR_MODEM_TEL.Text;
                        l_TerminalConexion.Puerto = Convert.ToInt32(Wne_TERMINAL_DIR_MODEM_PUERTO.Value);
                        l_TerminalConexion.Velocidad = Convert.ToInt32(Wne_TERMINAL_DIR_MODEM_VELOCIDAD.Value);
                        Wpn_USB_.Visible = false;
                        Wpn_SERIAL_.Visible = false;
                        Wpn_MODEM_.Visible = true;
                        Wpn_485_.Visible = false;
                        Wpn_RED_.Visible = false;
                        break;
                    case 4:
                        if (Wne_TERMINAL_DIR_RED_PUERTO.Text == "" || Wtx_TERMINAL_DIR_RED_DIR.Text == "")
                        {
                            Lbl_Error.Text = "Debe especificar todos los datos de la conexion en la terminal";
                            return;
                        }
                        l_TerminalConexion.Direccion = Wtx_TERMINAL_DIR_RED_DIR.Text;
                        l_TerminalConexion.Puerto = Convert.ToInt32(Wne_TERMINAL_DIR_RED_PUERTO.Value);
                        l_TerminalConexion.TipoConexion = CeC_Terminales.tipo.Red;
                        l_TerminalConexion.NoTerminal = Convert.ToInt32(Wne_TERMINAL_DIR_RED_ID.Value);
                        Wpn_USB_.Visible = false;
                        Wpn_SERIAL_.Visible = false;
                        Wpn_MODEM_.Visible = false;
                        Wpn_485_.Visible = false;
                        Wpn_RED_.Visible = true;
                        break;
                }
            }
            else
            {
                if (Wne_TERMINAL_DIR_485_PUERTO.Text == "" || Wne_TERMINAL_DIR_485_VELOCIDAD.Text == "" || Wne_TERMINAL_DIR_485_ID.Text == "")
                {
                    Lbl_Error.Text = "Debe especificar todos los datos de la conexion en la terminal";
                    return;
                }

                l_TerminalConexion.TipoConexion = CeC_Terminales.tipo.RS485;
                l_TerminalConexion.Puerto = Convert.ToInt32(Wne_TERMINAL_DIR_485_PUERTO.Value);
                l_TerminalConexion.Velocidad = Convert.ToInt32(Wne_TERMINAL_DIR_485_VELOCIDAD.Value);
                l_TerminalConexion.NoTerminal = Convert.ToInt32(Wne_TERMINAL_DIR_485_ID.Value);
            }
            m_TConexion = l_TerminalConexion.ObtenCadenaConexion();
            try
            {
                CargarDatos(false);
                Sesion.Redirige("WF_Terminales.aspx");
            }
            catch (Exception ex)
            {
                Lbl_Error.Text = "Error :" + ex.Message + "\nNo se han llenado los datos correctamente.";
            }
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("WF_TerminalesEd.Guardar",ex);
        }
    }

    /// <summary>
    /// Carga o guarda los elementos para mostrarlos en el formulario
    /// </summary>
    /// <param name="Cargar">Verdadero indica que lee los datos, y falso que agrega un nuevo registro o guarda modificaciones</param>
    private void CargarDatos(bool Cargar)
    {
        try
        {
            CeC_Terminales l_Terminal = null;
            CeC_Terminales l_Terminal_Direccion = null;
            int l_SuscripcionID = 0;
            int l_Terminal_ID = Sesion.WF_Terminales_TERMINALES_ID;
            bool l_EsLectora = CeC_BD.EjecutaEscalarInt("SELECT COUNT (*) FROM EC_TERMINALES_PARAM WHERE TERMINALES_PARAM_NOMBRE ='SITIO_HIJO_ID' AND TERMINALES_PARAM_VALOR = " + Wco_SITIO_ID.DataValue) > 0;
            CeC_Terminales_Param l_Terminal_Param = null;
            #region Cargar Datos Almacenados
            if (Cargar)
            {
                l_Terminal = new CeC_Terminales(l_Terminal_ID, Sesion);
                l_SuscripcionID = CeC_Terminales.ObtenSuscripcionID(l_Terminal_ID);
                l_Terminal_Direccion = new CeC_Terminales(Sesion);
                l_Terminal_Param = new CeC_Terminales_Param(l_Terminal_ID);
                // Si el parametro suscripcion de la sesion actual es diferente del ID de la suscripcion
                if (l_SuscripcionID != Sesion.SuscripcionParametro)
                {
                    // Se llena el control Combo Box de Infragistics con los datos de los Sitiod de la Suscripción
                    CeC_Sitios.AsignaCatalogo(Wco_SITIO_ID, l_SuscripcionID);
                }
                try
                {
                    // Llenamos los datos en el formulario.
                    Wne_TERMINAL_ID.Text = l_Terminal.Terminal_Id.ToString();
                    Wtx_TERMINAL_NOMBRE.Text = l_Terminal.Terminal_Nombre;
                    Wtx_TERMINAL_AGRUPACION.Text = l_Terminal.Terminal_Agrupacion;
                    Tbx_TERMINAL_DESCRIPCION.Text = l_Terminal_Param.TerminalDescripcion;
                    CeC_Grid.SeleccionaID(Wco_SITIO_ID, l_Terminal.Sitio_Id);
                    CeC_Grid.SeleccionaID(Wco_TIPO_TERMINAL_ACCESO_ID, l_Terminal.Tipo_Terminal_Acceso_Id);
                }
                catch (Exception ex)
                {
                    CIsLog2.AgregaError("WF_TerminalesEd.CargarDatos", ex);
                    
                }
                // Validamos que no sea lectora
                if (!l_EsLectora || Convert.ToBoolean(CeC_Terminales.EsControladora(l_Terminal.Terminal_Id)))
                {
                    try
                    {
                        // Se llenan los datos en el formulario
                        Chb_Controladora.Checked = Convert.ToBoolean(CeC_Terminales.EsControladora(Convert.ToInt32(l_Terminal.Terminal_Id)));
                        CeC_Grid.SeleccionaID(Wco_ALMACEN_VEC_ID, l_Terminal.Almacen_Vec_Id);
                        CeC_Grid.SeleccionaID(Wco_MODELO_TERMINAL_ID, l_Terminal.Modelo_Terminal_Id);
                        CeC_Grid.SeleccionaID(Wco_TIPO_TECNOLOGIA_ID, l_Terminal.Tipo_Tecnologia_Id);
                        CeC_Grid.SeleccionaID(Wco_TERMINAL_CAMPO_LLAVE, l_Terminal.Terminal_Campo_Llave);
                        if (l_Terminal.Tipo_Tecnologia_Add_Id >= 0)
                        {
                            CeC_Grid.SeleccionaID(Wco_TIPO_TECNOLOGIA_ADD_ID, l_Terminal.Tipo_Tecnologia_Add_Id);
                        }
                    }
                    catch (Exception ex)
                    {
                        CIsLog2.AgregaError("WF_TerminalesEd.CargaDatos", ex);
                    }
                    try
                    {
                        // Si hay campos adicionales se muestran
                        if (l_Terminal.Terminal_Campo_Adicional != null)
                        {
                            CeC_Grid.SeleccionaID(Wco_TERMINAL_CAMPO_ADICIONAL, l_Terminal.Terminal_Campo_Adicional);
                        }
                    }
                    catch (Exception ex)
                    {
                        CIsLog2.AgregaError("WF_TerminalesEd.CargaDatos", ex);
                    }
                    try
                    {
                        Wne_TERMINAL_SINCRONIZACION.Text = l_Terminal.Terminal_Sincronizacion.ToString();
                        // Si el identificador único no esta vacio se muestra
                        if (l_Terminal.Terminal_Campo_Llave != null)
                        {
                            Wco_TERMINAL_CAMPO_LLAVE.FindByValue(l_Terminal.Terminal_Campo_Llave).Selected = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        CIsLog2.AgregaError("WF_TerminalesEd.CargaDatos", ex);
                    }
                    try
                    {
                        // Si hay campos adicionales se muestran
                        if (l_Terminal.Terminal_Campo_Adicional != null)
                        {
                            CeC_Grid.SeleccionaID(Wco_TERMINAL_CAMPO_ADICIONAL, l_Terminal.Terminal_Campo_Adicional);
                        }
                    }
                    catch (Exception ex)
                    {
                        CIsLog2.AgregaError("WF_TerminalesEd.CargaDatos", ex);
                    }
                    try
                    {
                        // Si hay minutos de diferencia mayores a cero, se muestran
                        if (l_Terminal.Terminal_Minutos_Dif >= 0)
                        {
                            Wne_TERMINAL_MINUTOS_DIF.Text = l_Terminal.Terminal_Minutos_Dif.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        CIsLog2.AgregaError("WF_TerminalesEd.CargaDatos", ex);
                    }
                    try
                    {
                        // Si la direccion de la teminal esta vacio se usa la cadena de conexion
                        if (l_Terminal.Terminal_Dir == "")
                        {
                            m_TConexion = l_Terminal.Terminal_Dir;
                        }
                    }
                    catch (Exception ex)
                    {
                        CIsLog2.AgregaError("WF_TerminalesEd.CargaDatos", ex);
                    }
                    try
                    {
                        if (Wco_TIPO_TECNOLOGIA_ADD_ID.DataValue != null)
                        {
                            Wco_TIPO_TECNOLOGIA_ADD_ID.Visible = Chb_TIPO_TECNOLOGIA_ADD_ID.Checked = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        CIsLog2.AgregaError("WF_TerminalesEd.CargaDatos", ex);
                    }
                    try
                    {
                        if (Wco_TERMINAL_CAMPO_ADICIONAL.DataValue != null)
                        {
                            Wco_TERMINAL_CAMPO_ADICIONAL.Visible = Chb_TERMINAL_CAMPO_ADICIONAL.Checked = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        CIsLog2.AgregaError("WF_TerminalesEd.CargaDatos", ex);
                    }
                    try
                    {
                        if (l_Terminal_Param.TerminalDescripcion != "")
                        {
                            Tbx_TERMINAL_DESCRIPCION.Text = l_Terminal_Param.TerminalDescripcion;
                        }
                    }
                    catch (Exception ex)
                    {
                        CIsLog2.AgregaError("WF_TerminalesEd.CargaDatos", ex);
                    }
                    try
                    {
                        if (CeC_Terminales_DExtras.ObtenBin(l_Terminal.Terminal_Id, 114) != null)
                        {
                            Img_TERMINALES_DEXTRAS_BIN.ImageUrl = "WF_Terminal_Imagen.aspx?Parametros=" + l_Terminal.Terminal_Id;
                        }
                    }
                    catch (Exception ex)
                    {
                        CIsLog2.AgregaError("WF_TerminalesEd.CargaDatos", ex);
                    }
                    try
                    {
                        if (Wtx_TERMINAL_MODELO.Text != "")
                            Wtx_TERMINAL_MODELO.Text = l_Terminal.Terminal_Modelo;
                    }
                    catch (Exception ex)
                    {
                        CIsLog2.AgregaError("WF_TerminalesEd.CargaDatos", ex);
                    }
                    try
                    {
                        if (Wtx_TERMINAL_NO_SERIE.Text != "")
                            Wtx_TERMINAL_NO_SERIE.Text = l_Terminal.Terminal_No_Serie;
                    }
                    catch (Exception ex)
                    {
                        CIsLog2.AgregaError("WF_TerminalesEd.CargaDatos", ex);
                    }
                    try
                    {
                        if (Wtx_TERMINAL_FIRMWARE_VER.Text != "")
                            Wtx_TERMINAL_FIRMWARE_VER.Text = l_Terminal.Terminal_Firmware_Ver;
                    }
                    catch (Exception ex)
                    {
                        CIsLog2.AgregaError("WF_TerminalesEd.CargaDatos", ex);
                    }
                    try
                    {
                        if (Wne_TERMINAL_NO_HUELLAS.Text != null)
                            Wne_TERMINAL_NO_HUELLAS.Text = l_Terminal.Terminal_No_Huellas.ToString();
                    }
                    catch (Exception ex)
                    {
                        CIsLog2.AgregaError("WF_TerminalesEd.CargaDatos", ex);
                    }
                    try
                    {
                        if (Wne_TERMINAL_NO_EMPLEADOS.Text != "")
                            Wne_TERMINAL_NO_EMPLEADOS.Text = l_Terminal.Terminal_No_Empleados.ToString();
                    }
                    catch (Exception ex)
                    {
                        CIsLog2.AgregaError("WF_TerminalesEd.CargaDatos", ex);
                    }
                    try
                    {
                        if (Wne_TERMINAL_NO_TARJETAS.Text != "")
                            Wtx_TERMINAL_MODELO.Text = l_Terminal.Terminal_No_Tarjetas.ToString();
                    }
                    catch (Exception ex)
                    {
                        CIsLog2.AgregaError("WF_TerminalesEd.CargaDatos", ex);
                    }
                    try
                    {
                        if (Wne_TERMINAL_NO_ROSTROS.Text != "")
                            Wne_TERMINAL_NO_ROSTROS.Text = l_Terminal.Terminal_No_Rostros.ToString();
                    }
                    catch (Exception ex)
                    {
                        CIsLog2.AgregaError("WF_TerminalesEd.CargaDatos", ex);
                    }
                    try
                    {
                        if (Wne_TERMINAL_NO_CHECADAS.Text != "")
                            Wne_TERMINAL_NO_CHECADAS.Text = l_Terminal.Terminal_No_Checadas.ToString();
                    }
                    catch (Exception ex)
                    {
                        CIsLog2.AgregaError("WF_TerminalesEd.CargaDatos", ex);
                    }
                    try
                    {
                        if (Wne_TERMINAL_NO_PALMAS.Text != "")
                            Wne_TERMINAL_NO_PALMAS.Text = l_Terminal.Terminal_No_Palmas.ToString();
                    }
                    catch (Exception ex)
                    {
                        CIsLog2.AgregaError("WF_TerminalesEd.CargaDatos", ex);
                    }
                    try
                    {
                        if (Wne_TERMINAL_NO_IRIS.Text != "")
                            Wne_TERMINAL_NO_IRIS.Text = l_Terminal.Terminal_No_Iris.ToString();
                    }
                    catch (Exception ex)
                    {
                        CIsLog2.AgregaError("WF_TerminalesEd.CargaDatos", ex);
                    }
                    try
                    {
                        if (Wdc_TERMINAL_GARANTIA_INICIO.Text != "")
                            Wdc_TERMINAL_GARANTIA_INICIO.Value = l_Terminal.Terminal_Garantia_Inicio;
                    }
                    catch (Exception ex)
                    {
                        CIsLog2.AgregaError("WF_TerminalesEd.CargaDatos", ex);
                    }
                    try
                    {
                        if (Wdc_TERMINAL_GARANTIA_FIN.Text != "")
                            Wdc_TERMINAL_GARANTIA_FIN.Value = l_Terminal.Terminal_Garantia_Fin;
                    }
                    catch (Exception ex)
                    {
                        CIsLog2.AgregaError("WF_TerminalesEd.CargaDatos", ex);
                    }
                    try
                    {
                        // Si la direccion de la terminal no esta vacia se llenan los datos
                        if (l_Terminal.Terminal_Dir != null)
                        {
                            l_Terminal_Direccion.CargarCadenaConexion(l_Terminal.Terminal_Dir);
                            Rbn_TERMINAL_DIR.Items.FindByText(l_Terminal_Direccion.TipoConexion.ToString()).Selected = true;
                            switch (Rbn_TERMINAL_DIR.SelectedIndex)
                            {
                                case 0:
                                    Wpn_USB_.Visible = true;
                                    Wtx_TERMINAL_DIR_USB_NOMBRE.Text = l_Terminal_Direccion.Direccion;
                                    Wne_TERMINAL_DIR_USB_ID.Value = l_Terminal_Direccion.NoTerminal;
                                    break;
                                case 1:
                                    Wpn_SERIAL_.Visible = true;
                                    Wne_TERMINAL_DIR_SERIAL_PUERTO.Value = l_Terminal_Direccion.Puerto;
                                    Wne_TERMINAL_DIR_SERIAL_VELOCIDAD.Value = l_Terminal_Direccion.Velocidad;
                                    break;
                                case 2:
                                    Wpn_485_.Visible = true;
                                    Wne_TERMINAL_DIR_485_ID.Value = l_Terminal_Direccion.NoTerminal;
                                    Wne_TERMINAL_DIR_485_PUERTO.Value = l_Terminal_Direccion.Puerto;
                                    Wne_TERMINAL_DIR_485_VELOCIDAD.Value = l_Terminal_Direccion.Velocidad;
                                    break;
                                case 3:
                                    Wpn_MODEM_.Visible = true;
                                    Wne_TERMINAL_DIR_MODEM_PUERTO.Value = l_Terminal_Direccion.Puerto;
                                    Wne_TERMINAL_DIR_MODEM_TEL.Value = l_Terminal_Direccion.Direccion;
                                    Wne_TERMINAL_DIR_MODEM_VELOCIDAD.Value = l_Terminal_Direccion.Velocidad;
                                    break;
                                case 4:
                                    Wpn_RED_.Visible = true;
                                    Wtx_TERMINAL_DIR_RED_DIR.Value = l_Terminal_Direccion.Direccion;
                                    Wne_TERMINAL_DIR_RED_PUERTO.Value = l_Terminal_Direccion.Puerto;
                                    Wne_TERMINAL_DIR_RED_ID.Value = l_Terminal_Direccion.NoTerminal;
                                    break;
                            }

                        }
                        Chb_TERMINAL_ASISTENCIA.Checked = l_Terminal.Terminal_Asistencia;
                        Chb_TERMINAL_COMIDA.Checked = l_Terminal.Terminal_Comida;
                        Chb_TERMINAL_VALIDAHORARIOE.Checked = l_Terminal.Terminal_Validahorarioe;
                        Chb_TERMINAL_VALIDAHORARIOS.Checked = l_Terminal.Terminal_Validahorarios;
                        Chb_TERMINAL_ENROLA.Checked = l_Terminal.Terminal_Enrola;
                        Chb_TERMINAL_BORRADO.Checked = l_Terminal.Terminal_Borrado;
                        Rbn_TERMINAL_DIR.Enabled = !Chb_Controladora.Checked;
                    }
                    catch (Exception ex)
                    {
                        CIsLog2.AgregaError("WF_TerminalesEd.CargaDatos", ex);
                    }
                }
                else
                {
                    if (l_Terminal.Terminal_Dir == "")
                    {
                        m_TConexion = l_Terminal.Terminal_Dir;
                    }
                    l_Terminal_Direccion.CargarCadenaConexion(m_TConexion);
                    Wpn_485_.Visible = true;
                    Wne_TERMINAL_DIR_485_ID.Value = l_Terminal_Direccion.NoTerminal;
                    Wne_TERMINAL_DIR_485_PUERTO.Value = l_Terminal_Direccion.Puerto;
                    Wne_TERMINAL_DIR_485_VELOCIDAD.Value = l_Terminal_Direccion.Velocidad;
                }
                Lectora();
                try
                {
                    Chb_TERMINAL_ESENTRADA.Checked = l_Terminal_Param.TERMINAL_ESENTRADA;
                }
                catch (Exception ex)
                { CIsLog2.AgregaError("WF_TerminalesEd.CargaDatos", ex); }
                try
                {
                    Chb_TERMINAL_ESSALIDA.Checked = l_Terminal_Param.TERMINAL_ESSALIDA;
                }
                catch (Exception ex)
                { CIsLog2.AgregaError("WF_TerminalesEd.CargaDatos", ex); }
                try
                {
                    Chb_TERMINAL_ACEPTA_TA.Checked = l_Terminal_Param.TERMINAL_ACEPTA_TA;
                }
                catch (Exception ex)
                { CIsLog2.AgregaError("WF_TerminalesEd.CargaDatos", ex); }
                try
                { Tbx_TERMINAL_DESCRIPCION.Text = l_Terminal_Param.TerminalDescripcion; }
                catch (Exception ex)
                { CIsLog2.AgregaError("WF_TerminalesEd.CargaDatos", ex); }
                //try
                //{ Wtx_TERMINAL_MODELO.Text = l_Terminal
            }
            #endregion
            #region Guardar Datos Introducidos
            else
            {
                // Se inicializan los campos
                string CampoAdicional = "";
                int TecnologiaAdicional = 0;
                l_Terminal = new CeC_Terminales(Sesion);
                bool Nuevo = false;
                //int l_Terminal_ID = CeC_Tabla.GeneraAutonumerico;
                l_Terminal_Param = new CeC_Terminales_Param(l_Terminal.Terminal_Id);
                Wne_TERMINAL_DIR_SERIAL_PUERTO.Value = 1;
                Wne_TERMINAL_DIR_RED_PUERTO.Value = 8499;
                Wne_TERMINAL_DIR_MODEM_PUERTO.Value = 1;
                Wne_TERMINAL_DIR_485_PUERTO.Value = 8499;
                Wne_TERMINAL_DIR_MODEM_VELOCIDAD.Value = 9600;
                Wne_TERMINAL_DIR_485_VELOCIDAD.Value = 9600;
                Wne_TERMINAL_DIR_SERIAL_VELOCIDAD.Value = 9600;
                Wtx_TERMINAL_DIR_RED_DIR.Value = 0;
                Wne_TERMINAL_DIR_485_ID.Value = 0;
                Wne_TERMINAL_DIR_RED_ID.Value = 0;
                Wne_TERMINAL_DIR_USB_ID.Value = 0;

                if (Chb_TERMINAL_CAMPO_ADICIONAL.Checked)
                {
                    CampoAdicional = Wco_TIPO_TECNOLOGIA_ADD_ID.DataValue.ToString().Trim();
                }
                if (Wco_TERMINAL_CAMPO_LLAVE.DataValue == null)
                {
                    Wco_TERMINAL_CAMPO_LLAVE.DataValue = "";
                }
                if (Chb_TERMINAL_CAMPO_ADICIONAL.Checked)
                {
                    TecnologiaAdicional = CeC.Convierte2Int(Wco_TIPO_TECNOLOGIA_ADD_ID.DataValue);
                }
                if (Wne_TERMINAL_ID.Text != "")
                {
                    l_Terminal_ID = CeC.Convierte2Int(Wne_TERMINAL_ID.Text);
                }
                if (l_Terminal_ID == 0)
                    l_Terminal_ID = -99999;
                if (l_Terminal.Actualiza(l_Terminal_ID,
                    CeC.Convierte2Int(Wco_TIPO_TERMINAL_ACCESO_ID.DataValue),
                    Wtx_TERMINAL_NOMBRE.Text,
                    CeC.Convierte2Int(Wco_ALMACEN_VEC_ID.DataValue),
                    CeC.Convierte2Int(Wco_SITIO_ID.DataValue),
                    CeC.Convierte2Int(Wco_MODELO_TERMINAL_ID.DataValue),
                    CeC.Convierte2Int(Wco_TIPO_TECNOLOGIA_ID.DataValue),
                    TecnologiaAdicional,
                    CeC.Convierte2Int(Wne_TERMINAL_SINCRONIZACION.Text),
                    Wco_TERMINAL_CAMPO_LLAVE.DataValue.ToString().Trim(),
                    CampoAdicional,
                    CeC.Convierte2Bool(Chb_TERMINAL_ENROLA.Checked),
                    m_TConexion,
                    CeC.Convierte2Bool(Chb_TERMINAL_ASISTENCIA.Checked),
                    CeC.Convierte2Bool(Chb_TERMINAL_COMIDA.Checked),
                    Wne_TERMINAL_MINUTOS_DIF.ValueInt,
                    CeC.Convierte2Bool(Chb_TERMINAL_VALIDAHORARIOE.Checked),
                    CeC.Convierte2Bool(Chb_TERMINAL_VALIDAHORARIOS.Checked),
                    CeC.Convierte2Bool(Chb_TERMINAL_BORRADO.Checked),
                    Tbx_TERMINAL_DESCRIPCION.Text,
                    null,
                    Wtx_TERMINAL_MODELO.Text,
                    Wtx_TERMINAL_NO_SERIE.Text,
                    Wtx_TERMINAL_FIRMWARE_VER.Text,
                    CeC.Convierte2Int(Wne_TERMINAL_NO_HUELLAS.Value),
                    CeC.Convierte2Int(Wne_TERMINAL_NO_EMPLEADOS.Value),
                    CeC.Convierte2Int(Wne_TERMINAL_NO_TARJETAS.Value),
                    CeC.Convierte2Int(Wne_TERMINAL_NO_ROSTROS.Value),
                    CeC.Convierte2Int(Wne_TERMINAL_NO_CHECADAS.Value),
                    CeC.Convierte2Int(Wne_TERMINAL_NO_PALMAS.Value),
                    CeC.Convierte2Int(Wne_TERMINAL_NO_IRIS.Value),
                    CeC.Convierte2DateTime(Wdc_TERMINAL_GARANTIA_INICIO.Value),
                    CeC.Convierte2DateTime(Wdc_TERMINAL_GARANTIA_FIN.Value),
                    Wtx_TERMINAL_AGRUPACION.Text,
                    Sesion))
                    Nuevo = true;

                if (Nuevo)
                {
                    CeC_BD.AsignaPersonaATerminalAuto(l_Terminal.Terminal_Id);
                }
                l_Terminal_Param.TERMINAL_ESENTRADA = Chb_TERMINAL_ESENTRADA.Checked;
                l_Terminal_Param.TERMINAL_ESSALIDA = Chb_TERMINAL_ESSALIDA.Checked;
                l_Terminal_Param.TERMINAL_ACEPTA_TA = Chb_TERMINAL_ACEPTA_TA.Checked;
                l_Terminal_Param.TERMINAL_ID = l_Terminal_ID;
                l_Terminal_Param.TerminalDescripcion = Tbx_TERMINAL_DESCRIPCION.Text.ToString();
                if (Chb_Controladora.Checked)
                {
                    CeC_Terminales.GeneraControladora(l_Terminal.Terminal_Id, l_Terminal.Terminal_Nombre.Trim(), CeC.Convierte2Int(l_Terminal.Terminal_Borrado));
                }
                if (Chb_TERMINALES_DEXTRAS_BIN.Checked)
                {
                    Img_TERMINALES_DEXTRAS_BIN.ImageUrl = "WF_Terminal_Imagen.aspx?Parametros=" + l_Terminal.Terminal_Id;
                }
                else
                {
                    Lbl_Error.Text = "";
                }
            }
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("WF_TerminalesEd.Cargar", ex);
        }
        #endregion
    }
    protected void WIBtn_Deshacer_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        CargarDatos(true);
    }

    /// <summary>
    /// Habilita los controles de edición de terminales si el usuario tiene permisos
    /// </summary>
    /// <param name="Caso">Caso</param>
    /// <param name="Restriccion">Restricción</param>
    private void Habilitarcontroles(bool Caso, string Restriccion)
    {
        if (!Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Terminales0Nuevo) && !Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Terminales0Editar))
        {
            Wco_TERMINAL_CAMPO_ADICIONAL.Visible = Caso;
            Wco_TERMINAL_CAMPO_LLAVE.Visible = Caso;
            Wne_TERMINAL_ID.Visible = Caso;
            Wtx_TERMINAL_NOMBRE.Visible = Caso;
            Wtx_TERMINAL_AGRUPACION.Visible = Caso;
            Tbx_TERMINAL_DESCRIPCION.Visible = Caso;
            Wco_TIPO_TECNOLOGIA_ADD_ID.Visible = Caso;
            Wco_TIPO_TECNOLOGIA_ID.Visible = Caso;
            Wco_MODELO_TERMINAL_ID.Visible = Caso;
            Wco_TIPO_TERMINAL_ACCESO_ID.Visible = Caso;
            Lbl_TERMINAL_ID.Visible = Caso;
            Lbl_TERMINAL_NOMBRE.Visible = Caso;
            Lbl_MODELO_TERMINAL_ID.Visible = Caso;
            Lbl_TIPO_TECNOLOGIA_ID.Visible = Caso;
            Lbl_TIPO_TECNOLOGIA_ADD_ID.Visible = Caso;
            Lbl_TERMINAL_CAMPO_LLAVE.Visible = Caso;
            Lbl_TERMINAL_CAMPO_ADICIONAL.Visible = Caso;
            Lbl_TERMINAL_DIR.Visible = Caso;
        }
        if (!Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Terminales0Borrar))
        {
            Chb_TERMINAL_BORRADO.Visible = Caso;
            Lbl_TERMINAL_BORRADO.Visible = Caso;
        }

    }

    private void LlenarCombo(string[] Tablas)
    {
        CeC_Grid.AsignaCatalogo(Wco_TERMINAL_CAMPO_LLAVE, 28);
        CeC_Grid.AsignaCatalogo(Wco_TERMINAL_CAMPO_ADICIONAL, 28);
    }

    protected void Lectora()
    {
        try
        {
            bool EsLectora = Convert.ToBoolean(CeC_BD.EjecutaEscalarInt("SELECT COUNT (*) FROM EC_TERMINALES_PARAM WHERE TERMINALES_PARAM_NOMBRE ='SITIO_HIJO_ID' AND TERMINALES_PARAM_VALOR = " + Wco_SITIO_ID.DataValue));
            if (EsLectora)
            {
                Rbn_TERMINAL_DIR.SelectedValue = "RS485";
                Wpn_485_.Visible = true;
                Wpn_MODEM_.Visible = false;
                Wpn_SERIAL_.Visible = false;
                Wpn_USB_.Visible = false;
                Wpn_RED_.Visible = false;

                CeC_Grid.SeleccionaID(Wco_TIPO_TECNOLOGIA_ID, Convert.ToInt32(CeC_BD.EjecutaEscalarInt(" SELECT TIPO_TECNOLOGIA_ID FROM EC_TERMINALES INNER JOIN EC_TERMINALES_PARAM ON EC_TERMINALES.TERMINAL_ID = EC_TERMINALES_PARAM.TERMINAL_ID WHERE EC_TERMINALES_PARAM.TERMINALES_PARAM_NOMBRE ='SITIO_HIJO_ID' AND EC_TERMINALES_PARAM.TERMINALES_PARAM_VALOR = " + Wco_SITIO_ID.DataValue)));
                CeC_Grid.SeleccionaID(Wco_TIPO_TECNOLOGIA_ADD_ID, Convert.ToInt32(CeC_BD.EjecutaEscalarInt(" SELECT TIPO_TECNOLOGIA_ADD_ID FROM EC_TERMINALES INNER JOIN EC_TERMINALES_PARAM ON EC_TERMINALES.TERMINAL_ID = EC_TERMINALES_PARAM.TERMINAL_ID WHERE EC_TERMINALES_PARAM.TERMINALES_PARAM_NOMBRE ='SITIO_HIJO_ID' AND EC_TERMINALES_PARAM.TERMINALES_PARAM_VALOR = " + Wco_SITIO_ID.DataValue)));
                CeC_Grid.SeleccionaID(Wco_TIPO_TERMINAL_ACCESO_ID, Convert.ToInt32(CeC_BD.EjecutaEscalarInt(" SELECT TIPO_TERMINAL_ACCESO_ID FROM EC_TERMINALES INNER JOIN EC_TERMINALES_PARAM ON EC_TERMINALES.TERMINAL_ID = EC_TERMINALES_PARAM.TERMINAL_ID WHERE EC_TERMINALES_PARAM.TERMINALES_PARAM_NOMBRE ='SITIO_HIJO_ID' AND EC_TERMINALES_PARAM.TERMINALES_PARAM_VALOR = " + Wco_SITIO_ID.DataValue)));
                //Lbx_TERMINAL_CAMPO_LLAVE.Enabled = false;
                //Lbx_TIPO_TECNOLOGIA_ADD_ID.Enabled = false;
                Wco_TERMINAL_CAMPO_LLAVE.Enabled = true;
                Wco_TIPO_TECNOLOGIA_ADD_ID.Enabled = true;
                Wco_TIPO_TERMINAL_ACCESO_ID.Enabled = true;
                CeC_Grid.SeleccionaID(Wco_ALMACEN_VEC_ID, Convert.ToInt32(CeC_BD.EjecutaEscalarInt("  SELECT ALMACEN_VEC_ID FROM EC_TERMINALES INNER JOIN EC_TERMINALES_PARAM ON EC_TERMINALES.TERMINAL_ID = EC_TERMINALES_PARAM.TERMINAL_ID WHERE EC_TERMINALES_PARAM.TERMINALES_PARAM_NOMBRE ='SITIO_HIJO_ID' AND EC_TERMINALES_PARAM.TERMINALES_PARAM_VALOR = " + Wco_SITIO_ID.DataValue)));
                CeC_Grid.SeleccionaID(Wco_TERMINAL_CAMPO_LLAVE, CeC_BD.EjecutaEscalarString("  SELECT TERMINAL_CAMPO_LLAVE FROM EC_TERMINALES INNER JOIN EC_TERMINALES_PARAM ON EC_TERMINALES.TERMINAL_ID = EC_TERMINALES_PARAM.TERMINAL_ID WHERE EC_TERMINALES_PARAM.TERMINALES_PARAM_NOMBRE ='SITIO_HIJO_ID' AND EC_TERMINALES_PARAM.TERMINALES_PARAM_VALOR = " + Wco_SITIO_ID.DataValue));
                CeC_Grid.SeleccionaID(Wco_TERMINAL_CAMPO_ADICIONAL, CeC_BD.EjecutaEscalarString("  SELECT TERMINAL_CAMPO_ADICIONAL FROM EC_TERMINALES INNER JOIN EC_TERMINALES_PARAM ON EC_TERMINALES.TERMINAL_ID = EC_TERMINALES_PARAM.TERMINAL_ID WHERE EC_TERMINALES_PARAM.TERMINALES_PARAM_NOMBRE ='SITIO_HIJO_ID' AND EC_TERMINALES_PARAM.TERMINALES_PARAM_VALOR = " + Wco_SITIO_ID.DataValue));
                if (Wco_TIPO_TECNOLOGIA_ADD_ID.DataValue != null)
                { 
                    Wco_TIPO_TECNOLOGIA_ADD_ID.Visible = Chb_TIPO_TECNOLOGIA_ADD_ID.Checked = true; 
                }
                if (Wco_TERMINAL_CAMPO_ADICIONAL.DataValue != null)
                { 
                    Wco_TERMINAL_CAMPO_ADICIONAL.Visible = Chb_TERMINAL_CAMPO_ADICIONAL.Checked = true; 
                }
            }
            else
            {
                Chb_TERMINAL_ASISTENCIA.Enabled = !EsLectora;
                if (EsLectora || !Chb_Controladora.Checked)
                    Rbn_TERMINAL_DIR.Enabled = false;
            
                Wco_ALMACEN_VEC_ID.Enabled = !EsLectora;
                //ModeloTerminalId.Enabled = !EsLectora;
                Wco_TIPO_TECNOLOGIA_ID.Enabled = !EsLectora;
                Chb_TIPO_TECNOLOGIA_ADD_ID.Enabled = !EsLectora;
                Wne_TERMINAL_SINCRONIZACION.Enabled = !EsLectora;
                Wco_TERMINAL_CAMPO_LLAVE.Enabled = !EsLectora;
                Chb_TERMINAL_CAMPO_ADICIONAL.Enabled = !EsLectora;
                Chb_TERMINAL_ASISTENCIA.Enabled = !EsLectora;
                Chb_TERMINAL_COMIDA.Enabled = !EsLectora;
                Wne_TERMINAL_MINUTOS_DIF.Enabled = !EsLectora;
                Chb_TERMINAL_ESENTRADA.Enabled = !EsLectora;
                Chb_TERMINAL_ESSALIDA.Enabled = !EsLectora;
                Chb_Controladora.Enabled = !EsLectora;
                Chb_TERMINAL_ENROLA.Enabled = !EsLectora;
                Wco_TERMINAL_CAMPO_LLAVE.Enabled = !EsLectora;
                Wco_TIPO_TECNOLOGIA_ADD_ID.Enabled = !EsLectora;
                Wco_TIPO_TERMINAL_ACCESO_ID.Enabled = !EsLectora;
                Wco_TERMINAL_CAMPO_ADICIONAL.Enabled = !EsLectora;
                Tbx_TERMINAL_DESCRIPCION.Enabled = !EsLectora;
                Rbn_TERMINAL_DIR.Enabled = !EsLectora;
            }
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
    }
    protected void Rbn_TERMINAL_DIR_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Wpn_485_.Visible = false;
            Wpn_MODEM_.Visible = false;
            Wpn_RED_.Visible = false;
            Wpn_SERIAL_.Visible = false;
            Wpn_USB_.Visible = false;

            switch (Rbn_TERMINAL_DIR.SelectedIndex)
            {
                case 0:
                    Wpn_USB_.Visible = true;
                    break;
                case 1:
                    Wpn_SERIAL_.Visible = true;
                    break;
                case 2:
                    Wpn_485_.Visible = true;
                    break;
                case 3:
                    Wpn_MODEM_.Visible = true;
                    break;
                case 4:
                    Wpn_RED_.Visible = true;
                    break;
            }
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("WF_TerminalesEd.Rbn_TERMINAL_DIR_SelectedIndexChanged",ex);
        }
    }
    protected void Chb_TIPO_TECNOLOGIA_ADD_ID_CheckedChanged(object sender, EventArgs e)
    {
        Wco_TIPO_TECNOLOGIA_ADD_ID.Visible = Chb_TIPO_TECNOLOGIA_ADD_ID.Checked;
    }
    protected void Chb_TERMINAL_CAMPO_ADICIONAL_CheckedChanged(object sender, EventArgs e)
    {
        Wco_TERMINAL_CAMPO_ADICIONAL.Visible = Chb_TERMINAL_CAMPO_ADICIONAL.Checked;
    }
    protected void Chb_TERMINALES_DEXTRAS_BIN_CheckedChanged(object sender, EventArgs e)
    {
        Img_TERMINALES_DEXTRAS_BIN.Visible = Chb_TERMINALES_DEXTRAS_BIN.Checked;
        Fup_TERMINALES_DEXTRAS_BIN.Visible = Chb_TERMINALES_DEXTRAS_BIN.Checked;
        if (!Chb_TERMINALES_DEXTRAS_BIN.Checked)
        {
            Lbl_Error.Text = "";
        }
    }

    protected void WIBtn_TERMINALES_DEXTRAS_BIN_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        try
        {
            int l_TerminalID = Sesion.WF_Terminales_TERMINALES_ID;
            string StrFileName = Fup_TERMINALES_DEXTRAS_BIN.PostedFile.FileName.Substring(Fup_TERMINALES_DEXTRAS_BIN.PostedFile.FileName.LastIndexOf("\\") + 1);
            string StrFileType = Fup_TERMINALES_DEXTRAS_BIN.PostedFile.ContentType;
            int IntFileSize = Fup_TERMINALES_DEXTRAS_BIN.PostedFile.ContentLength;
            if (Fup_TERMINALES_DEXTRAS_BIN.Value == "")
            {
                Lbl_Error.Text = "Haga click en Examinar para subir una foto nueva";
                Lbl_Correcto.Text = "";
            }
            else
            {
                try
                {
                    CeC_Terminales_DExtras.BorrarDExtras(l_TerminalID, 114);

                    if (IntFileSize <= 0)
                    {
                        Response.Write(" <font color='Red' size='2'>Subida del archivo " + StrFileName + " fallo. </font>");
                        Lbl_Error.Text = "";
                        Lbl_Correcto.Text = "";
                    }
                    else
                    {
                        byte[] Datos = null;
                        int Len = Fup_TERMINALES_DEXTRAS_BIN.PostedFile.ContentLength;
                        if (Len > 100000)
                        {
                            Lbl_Error.Text = "La imagen es demasiado grande, comprimala (jpg) para poder subirla o baje la resolución de la imagen.";
                            Lbl_Correcto.Text = "";
                            return;
                        }
                        Lbl_Error.Text = "";
                        Datos = new byte[Len];
                        Fup_TERMINALES_DEXTRAS_BIN.PostedFile.InputStream.Read(Datos, 0, Len);
                        if (CeC_Terminales_DExtras.AgregaDExtra(l_TerminalID, 114, Sesion.SESION_ID, Sesion.SUSCRIPCION_ID, "", "", Datos))
                        {
                            Img_TERMINALES_DEXTRAS_BIN.ImageUrl = "WF_Terminal_Imagen.aspx?Parametros=" + l_TerminalID;
                            Lbl_Correcto.Text = "Se subio apropiadamente la foto, no olvide guardar";
                            Lbl_Error.Text = "";
                        }
                        else
                        {
                            Lbl_Error.Text = "Hubo un error al subir la foto, no se guardará.";
                            Lbl_Correcto.Text = "";
                        }
                    }
                }
                catch { }
            }
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("WF_TerminalesEd.WIBtn_TERMINALES_DEXTRAS_BIN_Click", ex);
        }
    }

    /// <summary>
    /// Botón que manda a la pantalla pata asignar personas a la terminal
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void WIBtn_AccesoPersonas_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        CeC_Terminales TerminalConexion = new CeC_Terminales(Sesion);
        WIBtn_Guardar_Click(sender, e);
        if (Lbl_Error.Text == "")
        {
            Sesion.Redirige_WF_TerminalesPersonas(Sesion.WF_Terminales_TERMINALES_ID);
        }
    }

    /// <summary>
    /// Elimina la imagen de la Terminal almacenada en la tabla EC_TERMINALES_DEXTRAS
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void WIBtn_TERMINALES_DEXTRAS_BIN_ELIMINAR_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        // Se usa 114 en el parámetro TipoTermDExtras para asegurarnos de que solo se borren los registros
        // que contienen datos de imagen en TERMINALES_DEXTRAS_BIN de la tabla EC_TERMINALES_DEXTRAS
        if (CeC_Terminales_DExtras.BorrarDExtras(Sesion.WF_Terminales_TERMINALES_ID, 114))
        {
            Lbl_Correcto.Text = "Se elimino correctamente la imagen.";
            Lbl_Error.Text = "";
            CargarDatos(true);
        }
        else
        {
            Lbl_Error.Text = "No hay foto para esta terminal.";
            Lbl_Correcto.Text = "";
        }
    }
}