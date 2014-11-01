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


/// <summary>
/// Descripción breve de WF_TerminalesE.
/// </summary>
public partial class WF_TerminalesE : System.Web.UI.Page
{
    // protected System.Data.OleDb.OleDbConnection oleDbConnection1;
    protected DS_TerminalesTableAdapters.EC_TERMINALESTableAdapter DA_Terminales = new DS_TerminalesTableAdapters.EC_TERMINALESTableAdapter();
    protected System.Web.UI.WebControls.RequiredFieldValidator RVModeloTerminalId;
    protected DS_Terminales dS_Terminales1;
    DS_Terminales.EC_TERMINALESRow Fila;
    string TConexion;
    CeC_Sesion Sesion;

    private void Habilitarcontroles(bool Caso, string Restriccion)
    {
        dS_Terminales1 = new DS_Terminales();
        if (!Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Terminales0Nuevo) && !Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Terminales0Editar))
        {
            TerminalCampoAdicional.Visible = Caso;
            TerminalCampoLlave.Visible = Caso;
            TerminalId.Visible = Caso;
            TerminalNombre.Visible = Caso;
            //TIP.Visible = Caso;
            TipoTecnologiaAddId.Visible = Caso;
            TipoTecnologiaId.Visible = Caso;
            ModeloTerminalId.Visible = Caso;

            Label1.Visible = Caso;
            Label2.Visible = Caso;
            Label3.Visible = Caso;
            Label4.Visible = Caso;
            Label5.Visible = Caso;
            Label6.Visible = Caso;
            Label7.Visible = Caso;
            Label8.Visible = Caso;
        }
        if (!Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Terminales0Borrar))
        {
            CBBorrar.Visible = Caso;
            LBorrar.Visible = Caso;
        }

    }
    public void AsignaCatalogoSitios()
    {
        DataSet DS = CeC_Sitios.ObtenSitiosDSMenuSuscripcion(Sesion.SuscripcionParametro);

        Sitio.DataSource = DS;
        //   Combo.DataMember = DS.Tables[0].TableName;

        Sitio.DataTextField = "SITIO_NOMBRE";
        Sitio.DataValueField = "SITIO_ID";
        Sitio.DataBind();

    }
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // Introducir aquí el código de usuario para inicializar la página
       // CeC_Campos.ReiniciaCampos();
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

        if (!IsPostBack)
        {
            //Inicializa Combos    
            CeC_Grid.AsignaCatalogo(AlmacenVectores, "ALMACEN_VEC_ID");
            AsignaCatalogoSitios();
            CeC_Grid.AsignaCatalogo(ModeloTerminalId, "MODELO_TERMINAL_ID");
            CeC_Grid.AsignaCatalogo(TipoTecnologiaId, "TIPO_TECNOLOGIA_ID");
            CeC_Grid.AsignaCatalogo(TipoTecnologiaAddId, "TIPO_TECNOLOGIA_ID");
            //    CeC_Grid.AsignaCatalogo(TerminalCampoLlave, "CAMPO_NOMBRE");
            //    CeC_Grid.AsignaCatalogo(TerminalCampoAdicional, "CAMPO_NOMBRE");

            string[] lista = CeC_Campos.ObtenListaCamposTE().Split(',');
            LlenarCombo(lista);
            CeC_Grid.AplicaFormato(TerminalCampoLlave);
            CeC_Grid.AplicaFormato(TerminalCampoAdicional);
            CeC_Grid.AplicaFormato(AlmacenVectores);
            CeC_Grid.AplicaFormato(Sitio);
            CeC_Grid.AplicaFormato(ModeloTerminalId);
            CeC_Grid.AplicaFormato(TipoTecnologiaId);
            CeC_Grid.AplicaFormato(TipoTecnologiaAddId);
            CeC_Grid.AplicaFormato(AlmacenVectores);

            //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Edición de Terminales", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
        }
        DA_Terminales.Fill(dS_Terminales1.EC_TERMINALES, Sesion.WF_Terminales_TERMINALES_ID);

        if (dS_Terminales1.EC_TERMINALES.Count > 0)
        {
            Fila = (DS_Terminales.EC_TERMINALESRow)
            dS_Terminales1.EC_TERMINALES.Rows[0];
            if (!IsPostBack)
            {
                CargarDatos(true);
            }
        }
        else
        {
            Fila = dS_Terminales1.EC_TERMINALES.NewEC_TERMINALESRow();
            /*Oculta el WPRED si es terminal maestra*/
            try
            {
                bool EsLectora = Convert.ToBoolean(CeC_BD.EjecutaEscalarInt("SELECT COUNT (*) FROM EC_TERMINALES_PARAM WHERE TERMINALES_PARAM_NOMBRE ='SITIO_HIJO_ID' AND TERMINALES_PARAM_VALOR = " + Sitio.DataValue));
                if (EsLectora == true)
                    WPRed.Visible = false;
                else
                    WPRed.Visible = true;
            }
            catch (Exception ex)
            {
                CIsLog2.AgregaError(ex);
            }

            if (!IsPostBack)
            {
                RBTipoDireccion.ClearSelection();
                RBTipoDireccion.Items.FindByText("Red").Selected = true;
                WPUsb.Visible = false;
                WPRed.Visible = true;

                txtSerialPuerto.Value = 1;
                txtRedPuerto.Value = 8499;
                txtModemPuerto.Value = 1;
                txt485Puerto.Value = 8499;
                txtModemVelocidad.Value = 9600;
                txt485Velocidad.Value = 9600;
                txtSerialVelocidad.Value = 9600;
                txtRedDireccion.Value = 0;
                txt485ID.Value = 0;
                txtRedID.Value = 0;
                txtUsbID.Value = 0;
            }

        }

    }

    /// <summary>
    /// Carga o guarda los elementos (controles) en la variable Fila
    /// </summary>
    /// <param name="Cargar">Verdadero indica que lee los datos, y falso que guarda</param>
    private void CargarDatos(bool Cargar)
    {

        bool EsLectora = CeC_BD.EjecutaEscalarInt("SELECT COUNT (*) FROM EC_TERMINALES_PARAM WHERE TERMINALES_PARAM_NOMBRE ='SITIO_HIJO_ID' AND TERMINALES_PARAM_VALOR = " + Sitio.DataValue) > 0;

        if (Cargar)
        {
            TerminalId.Text = Convert.ToString(Fila.TERMINAL_ID);
            TerminalNombre.Text = CeC_Terminales.ObtenTerminalNombre(Convert.ToInt32(Fila.TERMINAL_ID));
            CeC_Grid.SeleccionaID(Sitio, Fila.SITIO_ID);
            EsLectora = CeC_BD.EjecutaEscalarInt("SELECT COUNT (*) FROM EC_TERMINALES_PARAM WHERE TERMINALES_PARAM_NOMBRE ='SITIO_HIJO_ID' AND TERMINALES_PARAM_VALOR = " + Sitio.DataValue) > 0;
            if (!EsLectora || Convert.ToBoolean(CeC_Terminales.EsControladora(Convert.ToInt32(Fila.TERMINAL_ID))))
            {
                Controladora.Checked = Convert.ToBoolean(CeC_Terminales.EsControladora(Convert.ToInt32(Fila.TERMINAL_ID)));
                CeC_Grid.SeleccionaID(AlmacenVectores, Fila.ALMACEN_VEC_ID);

                CeC_Grid.SeleccionaID(ModeloTerminalId, Fila.MODELO_TERMINAL_ID);
                CeC_Grid.SeleccionaID(TipoTecnologiaId, Fila.TIPO_TECNOLOGIA_ID);
                if (!Fila.IsTIPO_TECNOLOGIA_ADD_IDNull())
                    CeC_Grid.SeleccionaID(TipoTecnologiaAddId, Fila.TIPO_TECNOLOGIA_ADD_ID);

                CeC_Grid.SeleccionaID(TerminalCampoLlave, Fila.TERMINAL_CAMPO_LLAVE.Trim());
                if (!Fila.IsTERMINAL_CAMPO_ADICIONALNull())
                    CeC_Grid.SeleccionaID(TerminalCampoAdicional, Fila.TERMINAL_CAMPO_ADICIONAL.Trim());

                Sincronizacion.Text = Fila.TERMINAL_SINCRONIZACION.ToString();
                try
                {
                    if (!Fila.IsTERMINAL_CAMPO_LLAVENull())

                        TerminalCampoLlave.FindByValue(Fila.TERMINAL_CAMPO_LLAVE.ToString()).Selected = true;

                    if (!Fila.IsTERMINAL_CAMPO_ADICIONALNull())
                        TerminalCampoAdicional.FindByText(Fila.TERMINAL_CAMPO_ADICIONAL.ToString()).Selected = true;
                }
                catch (Exception ex)
                {
                }
                if (!Fila.IsTERMINAL_MINUTOS_DIFNull())
                    MinutosDiferencia.Text = Fila.TERMINAL_MINUTOS_DIF.ToString();
                if (!Fila.IsTERMINAL_DIRNull())
                    TConexion = Fila.TERMINAL_DIR;
                CeC_Terminales TDir = new CeC_Terminales(Sesion);
                if (TipoTecnologiaAddId.DataValue != null)
                {
                    TipoTecnologiaAddId.Visible = CBTecAd.Checked = true;

                }
                if (TerminalCampoAdicional.DataValue != null)
                {
                    TerminalCampoAdicional.Visible = CBDatAd.Checked = true;
                }
                if (!Fila.IsTERMINAL_DIRNull())
                {
                    TDir.CargarCadenaConexion(TConexion);

                    RBTipoDireccion.Items.FindByText(TDir.TipoConexion.ToString()).Selected = true;
                    txtSerialPuerto.Value = 1;
                    txtRedPuerto.Value = 8499;
                    txtModemPuerto.Value = 1;
                    txt485Puerto.Value = 8499;
                    txtModemVelocidad.Value = 9600;
                    txt485Velocidad.Value = 9600;
                    txtSerialVelocidad.Value = 9600;
                    txtRedDireccion.Value = 0;
                    txt485ID.Value = 0;
                    txtRedID.Value = 0;
                    txtUsbID.Value = 0;
                    switch (RBTipoDireccion.SelectedIndex)
                    {
                        case 0:
                            WPUsb.Visible = true;
                            txtUsbNombre.Text = TDir.Direccion;
                            txtUsbID.Value = TDir.NoTerminal;
                            break;
                        case 1:
                            WPSerial.Visible = true;
                            txtSerialPuerto.Value = TDir.Puerto;
                            txtSerialVelocidad.Value = TDir.Velocidad;
                            break;
                        case 2:
                            WP485.Visible = true;
                            txt485ID.Value = TDir.NoTerminal;
                            txt485Puerto.Value = TDir.Puerto;
                            txt485Velocidad.Value = TDir.Velocidad;
                            break;
                        case 3:
                            WPModem.Visible = true;
                            txtModemPuerto.Value = TDir.Puerto;
                            txtModemTelefono.Value = TDir.Direccion;
                            txtModemVelocidad.Value = TDir.Velocidad;
                            break;
                        case 4:
                            WPRed.Visible = true;
                            txtRedDireccion.Value = TDir.Direccion;
                            txtRedPuerto.Value = TDir.Puerto;
                            txtRedID.Value = TDir.NoTerminal;
                            break;
                    }

                }
                GeneraAsistencia.Checked = Convert.ToBoolean(Fila.TERMINAL_ASISTENCIA);
                CobraComidas.Checked = Convert.ToBoolean(Fila.TERMINAL_COMIDA);
                ValidaHorarioEntrada.Checked = Convert.ToBoolean(Fila.TERMINAL_VALIDAHORARIOE);
                ValidaHorarioSalida.Checked = Convert.ToBoolean(Fila.TERMINAL_VALIDAHORARIOS);
                Enrolar.Checked = Convert.ToBoolean(Fila.TERMINAL_ENROLA);
                CBBorrar.Checked = Convert.ToBoolean(Fila.TERMINAL_BORRADO);
                RBTipoDireccion.Enabled = !Controladora.Checked;

            }
            else
            {
                CeC_Terminales TDir = new CeC_Terminales(Sesion);
                if (!Fila.IsTERMINAL_DIRNull())
                    TConexion = Fila.TERMINAL_DIR;
                TDir.CargarCadenaConexion(TConexion);
                WP485.Visible = true;
                txt485ID.Value = TDir.NoTerminal;
                txt485Puerto.Value = TDir.Puerto;
                txt485Velocidad.Value = TDir.Velocidad;

            }
            Lectora();
            CeC_Terminales_Param TP = new CeC_Terminales_Param(Convert.ToInt32(Fila.TERMINAL_ID));
            CBEntrada.Checked = TP.TERMINAL_ESENTRADA;
            CBSalida.Checked = TP.TERMINAL_ESSALIDA;
            CBAceptarTA.Checked = TP.TERMINAL_ACEPTA_TA;

        }
        else
        {
            CeC_Terminales Terminal = new CeC_Terminales(Sesion);
            //Terminal.Actualiza(CeC.Convierte2Int(TerminalId.Value),
            //    CeC.Convierte2Int(ModeloTerminalId.DataValue),
            //    TerminalNombre.Text,
            //    CeC.Convierte2Int(AlmacenVectores.DataValue),
            //    CeC.Convierte2Int(Sitio.DataValue),
            //    CeC.Convierte2Int(ModeloTerminalId.DataValue),
            //    CeC.Convierte2Int(TipoTecnologiaId.DataValue),
            //    CeC.Convierte2Int(TipoTecnologiaAddId.DataValue),
            //    CeC.Convierte2Int(Sincronizacion.Text),
            //    TerminalCampoLlave.DataValue.ToString().Trim(),
            //    TerminalCampoAdicional.DataValue.ToString().Trim(),
            //    CeC.Convierte2Bool(Enrolar.Checked),
            //    TConexion,
            //    CeC.Convierte2Bool(GeneraAsistencia.Checked),
            //    CeC.Convierte2Bool(CobraComidas.Checked),
            //    MinutosDiferencia.ValueInt,
            //    CeC.Convierte2Bool(ValidaHorarioEntrada.Checked),
            //    CeC.Convierte2Bool(ValidaHorarioSalida.Checked),
            //    CeC.Convierte2Bool(CBBorrar.Checked),
            //    "", 
            //    null,
            //    "",
            //    Sesion);

            Fila.TERMINAL_SINCRONIZACION = 0;
            Fila.TERMINAL_NOMBRE = TerminalNombre.Text;
            if (EsLectora)
                Fila.ALMACEN_VEC_ID = 1;
            else
                Fila.ALMACEN_VEC_ID = Convert.ToInt32(AlmacenVectores.DataValue);
            Fila.SITIO_ID = Convert.ToInt32(Sitio.DataValue);
            Fila.MODELO_TERMINAL_ID = Convert.ToInt32(ModeloTerminalId.DataValue);
            Fila.TIPO_TECNOLOGIA_ID = Convert.ToInt32(TipoTecnologiaId.DataValue);
            if (CBTecAd.Checked)
                Fila.TIPO_TECNOLOGIA_ADD_ID = Convert.ToInt32(TipoTecnologiaAddId.DataValue);
            else
                Fila.TIPO_TECNOLOGIA_ADD_ID = 0;
            if (TerminalCampoLlave.DataValue != null)
                Fila.TERMINAL_CAMPO_LLAVE = TerminalCampoLlave.DataValue.ToString().Trim();
            if (CBDatAd.Checked)
                Fila.TERMINAL_CAMPO_ADICIONAL = TerminalCampoAdicional.DataValue.ToString().Trim();
            else
                Fila.TERMINAL_CAMPO_ADICIONAL = "";
            Fila.TERMINAL_DIR = TConexion;
            Fila.TERMINAL_ASISTENCIA = Convert.ToInt32(GeneraAsistencia.Checked);
            Fila.TERMINAL_COMIDA = Convert.ToInt32(CobraComidas.Checked);
            Fila.TERMINAL_VALIDAHORARIOE = Convert.ToInt32(ValidaHorarioEntrada.Checked);
            Fila.TERMINAL_VALIDAHORARIOS = Convert.ToInt32(ValidaHorarioSalida.Checked);
            Fila.TERMINAL_ENROLA = Convert.ToInt32(Enrolar.Checked);
            Fila.TERMINAL_BORRADO = Convert.ToInt32(CBBorrar.Checked);

            int nuevo = 0;
            if (dS_Terminales1.EC_TERMINALES.Count <= 0)
            {
                Fila.TERMINAL_ID = CeC_Autonumerico.GeneraAutonumerico("EC_TERMINALES", "TERMINAL_ID", Sesion.SESION_ID, Sesion.SuscripcionParametro);
                if (Fila.TERMINAL_ID < 1)
                {
                    LError.Text = "Ha Llegado al Limite de Terminales Permitidas por esta Version";
                    return;
                }
                Sesion.WF_TerminalesE_TerminalID = Convert.ToInt32(Fila.TERMINAL_ID);
                dS_Terminales1.EC_TERMINALES.AddEC_TERMINALESRow(Fila);
                nuevo = 1;
            }
            DA_Terminales.Update(dS_Terminales1);
            if (nuevo == 1)
                CeC_BD.AsignaPersonaATerminalAuto(Convert.ToInt32(Fila.TERMINAL_ID));
            CeC_Terminales_Param TP = new CeC_Terminales_Param(Convert.ToInt32(Fila.TERMINAL_ID));
            TP.TERMINAL_ESENTRADA = CBEntrada.Checked;
            TP.TERMINAL_ESSALIDA = CBSalida.Checked;
            TP.TERMINAL_ACEPTA_TA = CBAceptarTA.Checked;
             
            Fila.TERMINAL_MINUTOS_DIF = MinutosDiferencia.ValueInt;
            if (Controladora.Checked)
            {
                CeC_Terminales.GeneraControladora(Convert.ToInt32(Fila.TERMINAL_ID), Fila.TERMINAL_NOMBRE.Trim(), Convert.ToInt32(Fila.TERMINAL_BORRADO));
            }
        }

    }

    #region Código generado por el Diseñador de Web Forms
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: llamada requerida por el Diseñador de Web Forms ASP.NET.
        //
        InitializeComponent();
        base.OnInit(e);
    }
    /// <summary>
    /// Método necesario para admitir el Diseñador. No se puede modificar
    /// el contenido del método con el editor de código.
    /// </summary>
    private void InitializeComponent()
    {
        System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
        this.dS_Terminales1 = new DS_Terminales();
        ((System.ComponentModel.ISupportInitialize)(this.dS_Terminales1)).BeginInit();
        this.BDeshacerCambios.Click += new

        Infragistics.WebUI.WebDataInput.ClickHandler(this.BDeshacerCambios_Click);
        this.BGuardarCambios.Click += new Infragistics.WebUI.WebDataInput.ClickHandler

        (this.BGuardarCambios_Click);
        this.dS_Terminales1.DataSetName = "DS_Terminales";
        this.dS_Terminales1.Locale = new System.Globalization.CultureInfo("es-MX");
    }

    #endregion

    protected void BDeshacerCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        CargarDatos(true);
    }

    protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        LError.Text = "";
        LCorrecto.Text = "";
        bool EsLectora = Convert.ToBoolean(CeC_BD.EjecutaEscalarInt("SELECT COUNT (*) FROM EC_TERMINALES_PARAM WHERE TERMINALES_PARAM_NOMBRE ='SITIO_HIJO_ID' AND TERMINALES_PARAM_VALOR = " + Sitio.DataValue));
        CeC_Terminales TerminalConexion = new CeC_Terminales(Sesion);
        if (!EsLectora)
        {
            if (TerminalCampoLlave.DataValue == null)
            {
                LError.Text = "No se ha elegido el campo llave de la terminal";
                return;
            }
            string Cllave = TerminalCampoLlave.DataValue.ToString();


            if (Cllave.IndexOf("-") > 0)
            {
                LError.Text = "No se admiten valores negativos";
                return;
            }
            if (ModeloTerminalId.SelectedIndex == -1)
            {
                LError.Text = "Debe de seleccionar un modelo de Terminal";
                return;
            }
            if (TipoTecnologiaId.SelectedIndex == -1)
            {
                LError.Text = "Debe de seleccionar un Tipo de Tecnologia";
                return;
            }
            if (TipoTecnologiaAddId.SelectedIndex == -1 && CBTecAd.Checked)
            {
                LError.Text = "Debe de seleccionar un Tipo de Tecnologia en la Tecnologia Adicional";
                return;
            }

            switch (RBTipoDireccion.SelectedIndex)
            {

                case 0:
                    TerminalConexion.TipoConexion = CeC_Terminales.tipo.USB;
                    TerminalConexion.Direccion = txtUsbNombre.Text;
                    TerminalConexion.NoTerminal = Convert.ToInt32(txtUsbID.Value); 
                    //  WPUsb.Visible = true;
                    break;
                case 1:
                    if (txtSerialPuerto.Text == "" || txtSerialVelocidad.Text == "")
                    {
                        LError.Text = "Debe especificar todos los datos de la conexion en la terminal";
                        return;
                    }
                    TerminalConexion.TipoConexion = CeC_Terminales.tipo.Serial;
                    TerminalConexion.Puerto = Convert.ToInt32(txtSerialPuerto.Value);
                    TerminalConexion.Velocidad = Convert.ToInt32(txtSerialVelocidad.Value);
                    //  WPSerial.Visible = true;
                    break;
                case 2:
                    if (txt485Puerto.Text == "" || txt485Velocidad.Text == "" || txt485ID.Text == "")
                    {
                        LError.Text = "Debe especificar todos los datos de la conexion en la terminal";
                        return;
                    }
                    TerminalConexion.TipoConexion = CeC_Terminales.tipo.RS485;
                    TerminalConexion.Puerto = Convert.ToInt32(txt485Puerto.Value);
                    TerminalConexion.Velocidad = Convert.ToInt32(txt485Velocidad.Value);
                    TerminalConexion.NoTerminal = Convert.ToInt32(txt485ID.Value);
                    //  WP485.Visible = true;
                    break;
                case 3:
                    if (txtModemTelefono.Text == "" || txtModemPuerto.Text == "" || txtModemVelocidad.Text == "")
                    {
                        LError.Text = "Debe especificar todos los datos de la conexion en la terminal";
                        return;
                    }
                    TerminalConexion.TipoConexion = CeC_Terminales.tipo.Modem;
                    TerminalConexion.Direccion = txtModemTelefono.Text;
                    TerminalConexion.Puerto = Convert.ToInt32(txtModemPuerto.Value);
                    TerminalConexion.Velocidad = Convert.ToInt32(txtModemVelocidad.Value);
                    //  WPModem.Visible = true;
                    break;
                case 4:
                    if (txtRedPuerto.Text == "" || txtRedDireccion.Text == "")
                    {
                        LError.Text = "Debe especificar todos los datos de la conexion en la terminal";
                        return;
                    }
                    TerminalConexion.Direccion = txtRedDireccion.Text;
                    TerminalConexion.Puerto = Convert.ToInt32(txtRedPuerto.Value);
                    TerminalConexion.TipoConexion = CeC_Terminales.tipo.Red;
                    TerminalConexion.NoTerminal = Convert.ToInt32(txtRedID.Value);
                    //  WPRed.Visible = true;
                    break;
            }
        }
        else
        {
            if (txt485Puerto.Text == "" || txt485Velocidad.Text == "" || txt485ID.Text == "")
            {
                LError.Text = "Debe especificar todos los datos de la conexion en la terminal";
                return;
            }

            TerminalConexion.TipoConexion = CeC_Terminales.tipo.RS485;
            TerminalConexion.Puerto = Convert.ToInt32(txt485Puerto.Value);
            TerminalConexion.Velocidad = Convert.ToInt32(txt485Velocidad.Value);
            TerminalConexion.NoTerminal = Convert.ToInt32(txt485ID.Value);
        }
        TConexion = TerminalConexion.ObtenCadenaConexion();
        try
        {
            CargarDatos(false);

            Sesion.Redirige("WF_Terminales.aspx");
        }
        catch (Exception ex)
        {
            LError.Text = "Error :" + ex.Message;
        }
    }

    protected void btnacceso_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        BGuardarCambios_Click(sender, e);
        if (LError.Text == "")
        {
            Sesion.Redirige_WF_TerminalesPersonas(Convert.ToInt32(Fila.TERMINAL_ID));

            //          WF_TerminalesPersonas.Redirige(Convert.ToInt32(Fila.TERMINAL_ID));
            //          Sesion.WF_Terminales_TERMINALES_ID = Sesion.WF_TerminalesE_TerminalID = Convert.ToInt32(Fila.TERMINAL_ID);

            //          if (CeC_BD.NumEmpleados() > 100)
            //              Sesion.WF_EmpleadosFil(false, false, false, "Siguiente", "Asignacion de Personas a Terminales", "WF_TerminalesPersonas.aspx", "Filtro de Personas", false, true, false);
            //          else
            //          Sesion.Redirige("WF_TerminalesPersonas.aspx");
        }
    }

    protected void RBTipoDireccion_SelectedIndexChanged(object sender, EventArgs e)
    {
        WP485.Visible = false;
        WPModem.Visible = false;
        WPRed.Visible = false;
        WPSerial.Visible = false;
        WPUsb.Visible = false;

        switch (RBTipoDireccion.SelectedIndex)
        {
            case 0:
                WPUsb.Visible = true;
                break;
            case 1:
                WPSerial.Visible = true;
                break;
            case 2:
                WP485.Visible = true;
                break;
            case 3:
                WPModem.Visible = true;
                break;
            case 4:
                WPRed.Visible = true;
                break;
        }
    }

    protected void CBTecAd_CheckedChanged(object sender, EventArgs e)
    {
        TipoTecnologiaAddId.Visible = CBTecAd.Checked;
    }

    protected void CBDatAd_CheckedChanged(object sender, EventArgs e)
    {
        TerminalCampoAdicional.Visible = CBDatAd.Checked;
    }

    protected void TipoTecnologiaId_SelectedRowChanged(object sender, Infragistics.WebUI.WebCombo.SelectedRowChangedEventArgs e)
    {

    }

    protected void Controladora_CheckedChanged(object sender, EventArgs e)
    {
        /* RBTipoDireccion.Enabled = !Controladora.Checked;
     if (Controladora.Checked)
     {
         RBTipoDireccion.SelectedValue = "RS485";
         WP485.Visible = true;
         WPModem.Visible = false;
         WPSerial.Visible = false;
         WPUsb.Visible = false;
         WPRed.Visible = false;
     }
     else WPRed.Visible = false;*/
    }

    protected void Sitio_SelectedRowChanged(object sender, Infragistics.WebUI.WebCombo.SelectedRowChangedEventArgs e)
    {
        Lectora();
    }

    protected void Lectora()
    {
        try
        {
            bool EsLectora = Convert.ToBoolean(CeC_BD.EjecutaEscalarInt("SELECT COUNT (*) FROM EC_TERMINALES_PARAM WHERE TERMINALES_PARAM_NOMBRE ='SITIO_HIJO_ID' AND TERMINALES_PARAM_VALOR = " + Sitio.DataValue));
            if (EsLectora)
            {
                RBTipoDireccion.SelectedValue = "RS485";
                WP485.Visible = true;
                WPModem.Visible = false;
                WPSerial.Visible = false;
                WPUsb.Visible = false;
                WPRed.Visible = false;

                CeC_Grid.SeleccionaID(TipoTecnologiaId, Convert.ToInt32(CeC_BD.EjecutaEscalarInt("  SELECT TIPO_TECNOLOGIA_ID FROM EC_TERMINALES INNER JOIN EC_TERMINALES_PARAM ON EC_TERMINALES.TERMINAL_ID = EC_TERMINALES_PARAM.TERMINAL_ID WHERE EC_TERMINALES_PARAM.TERMINALES_PARAM_NOMBRE ='SITIO_HIJO_ID' AND EC_TERMINALES_PARAM.TERMINALES_PARAM_VALOR = " + Sitio.DataValue)));
                CeC_Grid.SeleccionaID(TipoTecnologiaAddId, Convert.ToInt32(CeC_BD.EjecutaEscalarInt("  SELECT TIPO_TECNOLOGIA_ADD_ID FROM EC_TERMINALES INNER JOIN EC_TERMINALES_PARAM ON EC_TERMINALES.TERMINAL_ID = EC_TERMINALES_PARAM.TERMINAL_ID WHERE EC_TERMINALES_PARAM.TERMINALES_PARAM_NOMBRE ='SITIO_HIJO_ID' AND EC_TERMINALES_PARAM.TERMINALES_PARAM_VALOR = " + Sitio.DataValue)));
                TerminalCampoLlave.Enabled = false;
                TipoTecnologiaAddId.Enabled = false;
                CeC_Grid.SeleccionaID(AlmacenVectores, Convert.ToInt32(CeC_BD.EjecutaEscalarInt("  SELECT ALMACEN_VEC_ID FROM EC_TERMINALES INNER JOIN EC_TERMINALES_PARAM ON EC_TERMINALES.TERMINAL_ID = EC_TERMINALES_PARAM.TERMINAL_ID WHERE EC_TERMINALES_PARAM.TERMINALES_PARAM_NOMBRE ='SITIO_HIJO_ID' AND EC_TERMINALES_PARAM.TERMINALES_PARAM_VALOR = " + Sitio.DataValue)));
                CeC_Grid.SeleccionaID(TerminalCampoLlave, CeC_BD.EjecutaEscalarString("  SELECT TERMINAL_CAMPO_LLAVE FROM EC_TERMINALES INNER JOIN EC_TERMINALES_PARAM ON EC_TERMINALES.TERMINAL_ID = EC_TERMINALES_PARAM.TERMINAL_ID WHERE EC_TERMINALES_PARAM.TERMINALES_PARAM_NOMBRE ='SITIO_HIJO_ID' AND EC_TERMINALES_PARAM.TERMINALES_PARAM_VALOR = " + Sitio.DataValue));
                CeC_Grid.SeleccionaID(TerminalCampoAdicional, CeC_BD.EjecutaEscalarString("  SELECT TERMINAL_CAMPO_ADICIONAL FROM EC_TERMINALES INNER JOIN EC_TERMINALES_PARAM ON EC_TERMINALES.TERMINAL_ID = EC_TERMINALES_PARAM.TERMINAL_ID WHERE EC_TERMINALES_PARAM.TERMINALES_PARAM_NOMBRE ='SITIO_HIJO_ID' AND EC_TERMINALES_PARAM.TERMINALES_PARAM_VALOR = " + Sitio.DataValue));
                if (TipoTecnologiaAddId.DataValue != null)
                { TipoTecnologiaAddId.Visible = CBTecAd.Checked = true; }
                if (TerminalCampoAdicional.DataValue != null)
                { TerminalCampoAdicional.Visible = CBDatAd.Checked = true; }
            }
            GeneraAsistencia.Enabled = !EsLectora;
            if (EsLectora || !Controladora.Checked)
                RBTipoDireccion.Enabled = false;

            AlmacenVectores.Enabled = !EsLectora;
            //ModeloTerminalId.Enabled = !EsLectora;
            TipoTecnologiaId.Enabled = !EsLectora;
            CBTecAd.Enabled = !EsLectora;
            Sincronizacion.Enabled = !EsLectora;
            TerminalCampoLlave.Enabled = !EsLectora;
            CBDatAd.Enabled = !EsLectora;
            GeneraAsistencia.Enabled = !EsLectora;
            CobraComidas.Enabled = !EsLectora;
            MinutosDiferencia.Enabled = !EsLectora;
            ValidaHorarioEntrada.Enabled = !EsLectora;
            ValidaHorarioSalida.Enabled = !EsLectora;
            Controladora.Enabled = !EsLectora;
            Enrolar.Enabled = !EsLectora;
            TerminalCampoLlave.Enabled = !EsLectora;
            TipoTecnologiaAddId.Enabled = !EsLectora;
            TerminalCampoAdicional.Enabled = !EsLectora;
            RBTipoDireccion.Enabled = !EsLectora;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
    }


    protected void ModeloTerminalId_SelectedRowChanged(object sender, Infragistics.WebUI.WebCombo.SelectedRowChangedEventArgs e)
    {
        //si selecciona palma, cambia la direccion para red porque es hand punch
        /*  if (Convert.ToInt32(ModeloTerminalId.DataValue) == 2)
          {
              txtRedPuerto.Value = 3001;
              WP485.Visible = false;
              WPModem.Visible = false;
              WPSerial.Visible = false;
              WPUsb.Visible = false;
              WPRed.Visible = true;
              //     RBTipoDireccion.ClearSelection();
              RBTipoDireccion.Items.FindByText("Red").Selected = true;
          }*/
        if (Convert.ToInt32(ModeloTerminalId.DataValue) == 1 || Convert.ToInt32(ModeloTerminalId.DataValue) == 11 || Convert.ToInt32(ModeloTerminalId.DataValue) == 12 || Convert.ToInt32(ModeloTerminalId.DataValue) == 13 || Convert.ToInt32(ModeloTerminalId.DataValue) == 14 || Convert.ToInt32(ModeloTerminalId.DataValue) == 15)
            Controladora.Enabled = true;
        else
            Controladora.Enabled = false;
    }
    private void LlenarCombo(string[] Tablas)
    {
        /*
        //ListaCampo
        DataSet DS = new DataSet("TodosCampos");
        DataTable DT = new DataTable("Tabla1");
        DataRow DR;

        TerminalCampoLlave.Columns.Clear();
        TerminalCampoLlave.Rows.Clear();

        TerminalCampoAdicional.Columns.Clear();
        TerminalCampoAdicional.Rows.Clear();

        DT.Columns.Add("Nombrecampo", System.Type.GetType("System.String"));
        
        TerminalCampoLlave.DataSource = DS;
        TerminalCampoLlave.DataMember = DT.TableName.ToString();
        TerminalCampoLlave.DataTextField = "Nombrecampo";//DT.Columns[0].ColumnName.ToString();
        TerminalCampoLlave.DataValue = "Nombrecampo";
        TerminalCampoLlave.DataValueField = "Nombrecampo";

        TerminalCampoAdicional.DataSource = DS;
        TerminalCampoAdicional.DataMember = DT.TableName.ToString();
        TerminalCampoAdicional.DataTextField = "Nombrecampo";//DT.Columns[0].ColumnName.ToString();
        TerminalCampoAdicional.DataValue = "Nombrecampo";
        TerminalCampoAdicional.DataValueField = "Nombrecampo";

        for (int i = 0; i < Tablas.Length; i++)
        {
            DR = DT.NewRow();

            DR[0] = Tablas[i].ToString().Trim();

            DT.Rows.Add(DR);
        }
        DS.Tables.Add(DT);
        TerminalCampoLlave.DataBind();
        TerminalCampoAdicional.DataBind();*/
        CeC_Grid.AsignaCatalogo(TerminalCampoLlave, 28);
        CeC_Grid.AsignaCatalogo(TerminalCampoAdicional, 28);
    }

    protected void TipoTecnologiaId_SelectedRowChanged1(object sender, Infragistics.WebUI.WebCombo.SelectedRowChangedEventArgs e)
    {
        // int num = Convert.ToInt32(TipoTecnologiaId.DataValue);
        // int sel = TipoTecnologiaId.SelectedIndex;
    }
}
