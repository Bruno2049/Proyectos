
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if !NETFX_CORE
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
#endif
using System.Reflection;
using System.ComponentModel;
using Newtonsoft.Json;

namespace eClock5.Vista.Personas
{
    /// <summary>
    /// Lógica de interacción para UC_EdicionDePersonas.xaml
    /// </summary>
    public partial class Edicion : UserControl
    {

        /// <summary>
        /// Variable que se encargara de traernos el Persona_ID para 
        /// posteriormente traer los datos de la BD al
        /// diseño del formulario con respecto al Tipo de Persona.
        /// </summary>
        /// <param name="m_Tipo_Persona_ID"></param>
        /// <returns></returns>
        private int m_Tipo_Persona_ID = 0;
        public int Tipo_Persona_ID
        {
            get { return m_Tipo_Persona_ID; }
            set { m_Tipo_Persona_ID = value; }
        }

        /// <summary>
        /// Variable que se encarga identificar si ha pasado
        /// por algun procedimiento en especifico.
        /// </summary>
        /// <param name="m_EsNuevo"></param>
        /// <returns></returns>
        private bool m_EsNuevo = true;
        public bool EsNuevo
        {
            get { return m_EsNuevo; }
            set { m_EsNuevo = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m_Tipo_Persona_ID"></param>
        /// <returns></returns>
        public string PermisoEditar { get; set; }
        Clases.Parametros Parametros = null;
        eClockBase.CeC_SesionBase Sesion = null;
        eClockBase.Controladores.Localizaciones Localizacion = null;

        /// <summary>
        /// Variable que se encarga de identificar si va a mostrar
        /// la toolbar.
        /// </summary>
        /// <param name="m_MostrarToolBar"></param>
        /// <returns></returns>
        private bool m_MostrarToolBar = true;
        public bool MostrarToolBar
        {
            get { return m_MostrarToolBar; }
            set { m_MostrarToolBar = value; }
        }

        private string PreficoCampo = "Ctr_";

        /// <summary>
        /// Inicializador del Usuario de Control.
        /// </summary>
        /// <param name="m_MostrarToolBar"></param>
        /// <returns></returns>
        public Edicion()
        {
            InitializeComponent();
            Loaded += UC_EdicionDePersonas_Loaded;
            IsVisibleChanged += UC_EdicionDePersonas_IsVisibleChanged;
            Tb_General.OnEventClickToolBar += Tb_General_OnEventClickToolBar;
        }
        /// <summary>
        /// Oculta el Control de Usuario mostrando el Padre.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public void Regresar()
        {
            if (!Cerrar)
                return;
            Clases.Parametros.ObtenPadre(this).Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// Evento de click sobre la ToolBar pasa el control y realiza un
        /// procedimiento ya sea ejecuta el metodo REGRESAR o Guardar Datos.
        /// </summary>
        /// <param name="Control"></param>
        /// <returns></returns>
        void Tb_General_OnEventClickToolBar(UC_ToolBar_Control Control)
        {

            switch (Control.Nombre)
            {
                case "Btn_Regresar":
                    Regresar();
                    break;
                case "Btn_Accesos":
                    {
                        Accesos Dlg = new Accesos();
                        Clases.Parametros Param = Clases.Parametros.ObtenParametrosPadre(this);
                        Param.MostrarRegresar = true;
                        Dlg.Tag = Param;
                        UC_Listado.MuestraComoDialogo(this, Dlg, Colors.White);
                    }
                    break;
                case "Btn_Guardar":
                    eClockBase.Controladores.Sesion CSesion = new eClockBase.Controladores.Sesion(Sesion);

                    Vista2Diseno(false);

                    //CSesion.gu(Tabla,CamposLlave,JsonConvert.SerializeObject(
                    break;

            }
        }
        bool GuardaFoto(int PersonaID)
        {
            if (Img_Persona.Cambio)
            {
                //Codigo Guardar
                //Img_Persona.ImagenBytes;
                eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);
                eClockBase.Controladores.Personas Ps = new eClockBase.Controladores.Personas(Sesion);
                Ps.GuardaFotoEvent += Ps_GuardaFotoEvent;
                Ps.GuardaFoto(PersonaID, Img_Persona.ImagenBytes);
                Regresar();
            }
            return true;
        }
        /// <summary>
        /// Función que despues de Cambiar la visibilidad del control y evaluar si hay una Sesión
        /// hace una llamada ala funcón de CreaVista pasandole como parametro el DataContext.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        void UC_EdicionDePersonas_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.Equals(true) && Sesion != null)
            {
                CreaVista(this.DataContext);
            }
        }
        /// <summary>
        /// Función de Cargado el Control de UC_Edición de Personas, se encarga de evaluar si la
        /// variable MostrarToolBar viene con verdadero para cambiarle la propiedad de visibilidad
        /// a verdadero al control UC_ToolBar y dandole un margen de espaciado.
        /// Justamente despues de Visibilizar la UC_ToolBar, manda a llamar la función CreaVista.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        void UC_EdicionDePersonas_Loaded(object sender, RoutedEventArgs e)
        {
            if (!MostrarToolBar)
            {
                Tb_General.Visibility = System.Windows.Visibility.Hidden;
                //Scr_Grid.Margin = new Thickness(0);
            }
            Sesion = CeC_Sesion.ObtenSesion(this);

            //        ucParent.Tag
            ///Solo cargará el dataContext en Vista de diseño, de lo contrario deberá cargarlo de otra forma
            //if(DesignerProperties.GetIsInDesignMode(this))

            CreaVista(this.DataContext);
        }
        bool Cerrar = true;
        int PersonaID = -1;
        /// <summary>
        /// Esta función se encarga de recibir como parametros una variable de tipo object llamada Contexto; esta variable
        /// se encarga de almacenar un modelo de datos (estructura), despues dentro de una variable Tag, obtiene los parametros
        /// del padre contenedor de Control de Usuario UC_EdicionDePersonas.
        /// Y SE ENCARGA DE SUBSTRAER LOS DATOS POR MEDIO DE UN SERVICIO PARA SER MOSTRADOS EN LA VISTA 
        /// ALA CUAL EL CONTROLADOR HACE REFERENCIA.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        Clases.Parametros m_ParametrosAnteriores = null;
        List<eClockBase.Modelos.Model_PERSONAS_DATO> PersonaDatos = null;
        private bool CreaVista(object Contexto, bool PrimeraVez = true)
        {
            try
            {
                Img_Persona.Source = null;
                Tag = Parametros = Clases.Parametros.ObtenParametrosPadre(this);
                if (m_ParametrosAnteriores == Parametros)
                    return false;
                //Limpia todos el contenido de los campos.
                xGrid.Children.Clear();
                //Limpia todos el contenido de los campos.
                CamposVista.Clear();
                m_ParametrosAnteriores = Parametros;
                //Crea un log de la vista.
                eClockBase.CeC_Log.AgregaLog("CreaVista" + this.Name);

                try
                {
                    eClockBase.Modelos.Model_PERSONAS_DATO Dato = eClockBase.Controladores.CeC_ZLib.Json2Object<eClockBase.Modelos.Model_PERSONAS_DATO>(Parametros.Parametro.ToString());
                    PersonaID = Dato.PERSONA_ID;
                    Cerrar = true;
                }
                catch
                {
                    PersonaID = eClockBase.CeC.Convierte2Int(Parametros.Parametro);
                    if (PersonaID > 0)
                        Cerrar = false;
                }

                PersonaDatos = null;

                //Crea el objeto del controlador de Campos mandando como variable de inicio a Sesion.
                eClockBase.Controladores.Campos Campos = new eClockBase.Controladores.Campos(Sesion);
                //Evalua si el evento con respecto a la función lista campos ha terminado de ejecutarse.
                Campos.ListaCamposFinalizado += Campos_ListaCamposFinalizado;
                //Devuelve el resultado en un listado de datos; esto es el resultado de llamar dos
                //subfunciones que devuelven la consulta de los datos.
                Campos.ListaCampos(Tipo_Persona_ID);

                if (PersonaID > 0)
                {
                    eClockBase.Controladores.Sesion cSesion = new eClockBase.Controladores.Sesion(Sesion);
                    eClockBase.Modelos.Model_PERSONAS_DATO Dato = new eClockBase.Modelos.Model_PERSONAS_DATO();
                    Dato.PERSONA_ID = PersonaID;
                    cSesion.ObtenDatosEvent += delegate(int Resultado, string Datos)
                    {
                        try
                        {
                            if (Resultado > 0)
                            {
                                Sesion.MuestraMensaje("Listo", 3);
                                PersonaDatos = eClockBase.Controladores.CeC_ZLib.Json2Object<List<eClockBase.Modelos.Model_PERSONAS_DATO>>(Datos);
                                ActualizaVistaDatos();
                            }
                        }
                        catch { }
                    };
                    cSesion.ObtenDatos("EC_PERSONAS_DATO", "PERSONA_ID", Dato);
                }

                eClockBase.Controladores.Personas Persona = new eClockBase.Controladores.Personas(Sesion);
                Persona.ObtenFotoEvent += Persona_ObtenFotoEvent;

                if (PersonaID > 0)
                    Persona.ObtenFoto(PersonaID);
                return true;
            }
            catch (Exception ex)
            {

                eClockBase.CeC_Log.AgregaError(ex);
            }
            return false;
        }

        void ActualizaVistaDatos()
        {
            if (CamposVista == null || CamposVista.Count < 1)
                return;
            if (PersonaDatos == null || PersonaDatos.Count < 1)
                return;
            foreach (FrameworkElement CampoVista in CamposVista)
            {
                eClockBase.Modelos.Model_PERSONAS_DATO Dato = (eClockBase.Modelos.Model_PERSONAS_DATO)CampoVista.Tag;
                foreach (eClockBase.Modelos.Model_PERSONAS_DATO PersonaDato in PersonaDatos)
                {
                    if (PersonaDato.CAMPO_NOMBRE.ToUpper() == Dato.CAMPO_NOMBRE.ToUpper())
                    {
                        CampoVista.Tag = PersonaDato;

                        try
                        {
                            switch (CampoVista.GetType().Name)
                            {
                                case "DatePicker":
                                    ((DatePicker)CampoVista).SelectedDate = eClockBase.CeC.Convierte2DateTime(PersonaDato.PERSONA_DATO);
                                    break;
                                case "CheckBox":
                                    ((CheckBox)CampoVista).IsChecked = eClockBase.CeC.Convierte2Bool(PersonaDato.PERSONA_DATO, false);
                                    break;
                                case "UC_Combo":
                                    ((eClock5.Controles.UC_Combo)CampoVista).Seleccionado = PersonaDato.PERSONA_DATO;
                                    break;

                                default:
                                    ((TextBox)CampoVista).Text = PersonaDato.PERSONA_DATO;
                                    break;
                            }
                        }
                        catch { }
                        break;
                    }
                }
            }
        }

        void Persona_ObtenFotoEvent(byte[] Foto)
        {
            Img_Persona.ImagenBytes = Foto;

        }

        void Ps_GuardaFotoEvent(bool FotoGuardada)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Funcion privada que se encarga de pasar como parametros a Model_CAMPOS_DATOS(estructura)
        /// como una lista de datos, que con referencia a esta estructura de datos asignara
        /// tanto como una Etiqueta(label), Campo(textbox), Ayuda(Label) ala vista principal.
        /// </summary>
        /// <param name="CamposDatos"></param>
        /// <returns></returns>
        void Campos_ListaCamposFinalizado(List<eClockBase.Modelos.Model_CAMPOS_DATOS> CamposDatos)
        {

            int NoFila = 0;
            xGrid.Background = Recursos.AzulClaro_Brush;
            foreach (eClockBase.Modelos.Model_CAMPOS_DATOS Campo in CamposDatos)
            {
                RowDefinition Fila = new RowDefinition();
                Fila.Height = GridLength.Auto;
                xGrid.RowDefinitions.Add(Fila);
                // Se encarga de crear las etiquetas con el nombre que le conrresponde.
                AgregaEtiqueta(Campo.CAMPO_DATO_NOMBRE, Campo.CAMPO_DATO_ETIQUETA, NoFila, 0);
                // Se encarga de crear los campos nombre que le conrresponde.
                AgregaCampo(Campo.CAMPO_DATO_NOMBRE, Campo.TIPO_DATO_ID, Campo.CAMPO_DATO_USABILIDAD, NoFila, 1);



                NoFila++;
                Fila = new RowDefinition();
                xGrid.RowDefinitions.Add(Fila);
                // Se encarga de crear las etiquetas de ayuda con el nombre que le conrresponde.
                AgregaAyuda(Campo.CAMPO_DATO_NOMBRE, Campo.CAMPO_DATO_AYUDA, NoFila, 1);
                NoFila++;
            }
            ActualizaVistaDatos();
        }
        /// <summary>
        /// Esta función se encarga de guardar los datos que estan contenidos dentro de  los campos
        /// en la vista principal de modificación.
        /// </summary>
        /// <param name="NoSeGuardaron"></param>
        /// <param name="PorGuardar"></param>
        /// <param name="Contexto"></param>
        /// <returns></returns>
        string NoSeGuardaron = "";
        int PorGuardar = 0;
        int PersonaIDNuevo = -1;
        public void Vista2Diseno(bool Nuevo)
        {
            int PersonaIDFoto = -1;
            //Variable que se encargara de controlar si se no se han guardado datos sobre la base de datos.
            NoSeGuardaron = "";
            //Muestra mensaje de guardando dantos.
            Sesion.MuestraMensaje("Guardando...");
            //Inicializa un ciclo para obtener el valor que esta asignado en cada campo de la vista frontal.
            foreach (FrameworkElement CampoVista in CamposVista)
            {

                try
                {
                    String TextoAGuardar = UC_Campos.ObtenValorString(CampoVista);
                    if (TextoAGuardar.Length > 1 && TextoAGuardar[0] == '\"' && TextoAGuardar[TextoAGuardar.Length - 1] == '\"')
                    {
                        TextoAGuardar = TextoAGuardar.Substring(1, TextoAGuardar.Length - 2);
                    }
                    // Creamos un objeto "Modelo" de tipo "Model_PERSONAS_DATO", y traemos los parametros(VALORES) desde R.Tag(campos de la vista frontal y los convertimos en
                    // la estructura  de la clase tipo "Model_PERSONAS_DATO"
                    eClockBase.Modelos.Model_PERSONAS_DATO Modelo = (eClockBase.Modelos.Model_PERSONAS_DATO)CampoVista.Tag;

                    // Creamos el objeto de controlador de Personas mandando como variable de inicio a Sesion.
                    eClockBase.Controladores.Personas Persona = new eClockBase.Controladores.Personas(Sesion);

                    // Toma el ID para comprobar si es nuevo o no el registro que se quiere modificar.
                    if (Modelo.PERSONA_ID < 1 && !Nuevo)
                    {

                        Persona.AgregaFinalizadoEvent += delegate(int PersonaID)
                        {
                            try
                            {
                                if (PersonaID > 0)
                                {
                                    PersonaIDNuevo = PersonaID;
                                    Vista2Diseno(true);
                                    return;
                                }
                            }
                            catch { }
                            Sesion.MuestraMensaje("No se pudo generar un nuevo personaID");
                        };
                        //Obtiene un Nuevo PersonaID
                        Persona.Agrega(eClockBase.CeC.Convierte2Int(TextoAGuardar), m_Tipo_Persona_ID);
                        return;
                    }
                    if (Nuevo)
                    {
                        Modelo.PERSONA_ID = PersonaIDNuevo;
                    }
                    PersonaIDFoto = Modelo.PERSONA_ID;

                    // Con el objeto de controlados Persona llama a uno de sus metodos llamado "GuardaValor"
                    // Pero antes que nada esto solo lo mantiene en memoria hasta que se ejecuta el "if (Modelo.PERSONA_DATO != R.Text)"
                    Persona.GuardaValorFinalizadoEvent += delegate(bool Guardado)
                        {
                            PorGuardar--;
                            // Si guardado es diferente de verdader
                            if (!Guardado)
                            {
                                // En esta variable se asignan los datos que no se pudieron guardar.
                                NoSeGuardaron = eClockBase.CeC.AgregaSeparador(NoSeGuardaron, Modelo.CAMPO_NOMBRE, ",");
                            }
                            // Si por guardar es menor igual a 0 ejecutara las condiciones
                            if (PorGuardar <= 0)
                            {
                                //No se guardaron X campos
                                if (NoSeGuardaron != "")
                                    Sesion.MuestraMensaje("No se pudieron guardar los siguientes campos " + NoSeGuardaron);
                                else
                                {
                                    // Se guardaron con exito todos los campos.
                                    Sesion.MuestraMensaje("Datos Guardados", 5);
                                    Regresar();
                                }
                            }

                        };

                    // EN ESTA LINEA DE CODIGO VA VERIFICANDO CAMPO-BASE DE DATOS CONTRA CAMPO-VISTA FRONTAL Y SI SE LE HIZO ALGUN CAMBIO
                    // INSERTA EL NUEVO DATO DEL CAMPO MODIFICADO.
                    if ((Modelo.PERSONA_DATO != null && Modelo.PERSONA_DATO != TextoAGuardar) || (Modelo.PERSONA_DATO == null && TextoAGuardar != ""))
                    {
                        // Ejecuta un servicio para procesar el guardado de los datos en la base de datos.
                        PorGuardar++;
                        Persona.GuardaValor(Modelo.PERSONA_ID, Modelo.CAMPO_NOMBRE, TextoAGuardar);
                    }
                    /*else  (Modelo.PERSONA_DATO != )
                    {

                    }*/
                }
                catch { }

            }
            GuardaFoto(PersonaIDFoto);

        }



        /// <summary>
        /// Se encarga de agregar una etiqueta con el nombre, el campo que le corresponde, y 
        /// los posiciona sobre la fila y sobre la columna.
        /// </summary>
        /// <param name="CampoNombre"></param>
        /// <param name="CampoEtiqueta"></param>
        /// <param name="Fila"></param>
        /// <param name="Columna"></param>
        /// <returns></returns>
        public bool AgregaEtiqueta(string CampoNombre, string CampoEtiqueta, int Fila, int Columna)
        {
            try
            {
                FrameworkElement Etiqueta = NuevaEtiqueta("Lbl_" + CampoNombre, Sesion.IDIOMA, CampoEtiqueta, false);

                xGrid.Children.Add(Etiqueta);
                Grid.SetColumn(Etiqueta, Columna);
                Grid.SetRow(Etiqueta, Fila);
                return true;
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
            return false;
        }

        List<FrameworkElement> CamposVista = new List<FrameworkElement>();
        public FrameworkElement NuevoCampo(string CampoNombre, int TipoDatoID, int Usabilidad)
        {
            FrameworkElement Campo = null;
            switch (TipoDatoID)
            {
                /*
                 * 
                 0	Desconocido		1
                1	Texto	System.String	0
                2	Entero	System.Int32	0
                3	Decimal	System.Decimal	0
                4	Imagen	System.Byte[]	0
                5	Fecha	System.DateTime	1
                6	Fecha y Hora	System.DateTime	0
                7	Hora	System.DateTime	1
                8	Boleano	System.Boolean	0
                9	Horas	System.DateTime	0
                 */
                case 0:
                    break;
                case 1:
                    Campo = NuevoTextBox("", 4000, Usabilidad == 1 ? true : false, false);
                    break;
                case 2:
                    Campo = NuevoTextBox("", 4000, Usabilidad == 1 ? true : false, false);
                    break;
                case 3:
                    Campo = NuevoTextBox("", 4000, Usabilidad == 1 ? true : false, false);
                    break;
                case 5:
                    Campo = NuevoDatePicker(null, null, null, Usabilidad == 1 ? true : false, false);
                    break;
                case 6:
                    Campo = NuevoTextBox("", 100, Usabilidad == 1 ? true : false, false);
                    break;
                case 7:
                    Campo = NuevoTextBox("", 20, Usabilidad == 1 ? true : false, false);
                    break;
                case 8:
                    Campo = NuevoCheckBox(false, Usabilidad == 1 ? true : false, false);
                    break;
                case 9:
                    Campo = NuevoTextBox("", 20, Usabilidad == 1 ? true : false, false);
                    break;
                case 10:
                    Campo = NuevoComboBox(null, Usabilidad == 1 ? true : false, false);
                    break;
            }


            if (Campo == null)
            {
                Campo = new Label();
                ((Label)Campo).Content = "No Soportado";
            }
            else
            {
                CamposVista.Add(Campo);
                if (PersonaID > 0)
                {
                    //eClockBase.Controladores.Sesion cSesion = new eClockBase.Controladores.Sesion(Sesion);
                    eClockBase.Modelos.Model_PERSONAS_DATO Dato = new eClockBase.Modelos.Model_PERSONAS_DATO();
                    Dato.PERSONA_ID = PersonaID;
                    /*
                    cSesion.ObtenDatosEvent += delegate(int Resultado, string Datos)
                    {
                        if (Resultado> 0)
                        {
                            Campo.Tag = Dato = eClockBase.Controladores.CeC_ZLib.Json2Object<eClockBase.Modelos.Model_PERSONAS_DATO>(Datos);

                            try
                            {
                                switch (TipoDatoID)
                                {
                                    case 5:
                                        ((DatePicker)Campo).SelectedDate = eClockBase.CeC.Convierte2DateTime(Dato.PERSONA_DATO);
                                        break;
                                    case 8:
                                        ((CheckBox)Campo).IsChecked = eClockBase.CeC.Convierte2Bool(Dato.PERSONA_DATO, false);
                                        break;
                                    case 10:
                                        ((UC_Combo)Campo).Seleccionado = Dato.PERSONA_DATO;
                                        break;
                                        
                                    default:
                                        ((TextBox)Campo).Text = Dato.PERSONA_DATO;
                                        break;
                                }
                            }
                            catch { }
                        }
                    };*/
                    //Dato.PERSONA_ID = 1408;
                    Dato.CAMPO_NOMBRE = CampoNombre;
                    Campo.Tag = Dato;
                    //cSesion.ObtenDatos("EC_PERSONAS_DATO", "PERSONA_ID,CAMPO_NOMBRE", Dato);
                }
                else
                {
                    eClockBase.Modelos.Model_PERSONAS_DATO Dato = new eClockBase.Modelos.Model_PERSONAS_DATO();
                    Dato.CAMPO_NOMBRE = CampoNombre;
                    Dato.PERSONA_ID = -1;
                    Campo.Tag = Dato;
                }
            }
            Campo.Name = PreficoCampo + CampoNombre;
            return Campo;
        }


        public bool AgregaCampo(string CampoNombre, int TipoDatoID, int Usabilidad, int Fila, int Columna)
        {
            try
            {
                FrameworkElement Campo = null;
                Campo = NuevoCampo(CampoNombre, TipoDatoID, Usabilidad);
                Campo.Margin = new Thickness(5);
                xGrid.Children.Add(Campo);
                Grid.SetColumn(Campo, Columna);
                Grid.SetRow(Campo, Fila);
                return true;
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
            return false;
        }

        public FrameworkElement NuevaEtiqueta(string Nombre, string Idioma, string Campo, bool Ayuda)
        {
            //FrameworkElement Etiqueta = new TextBlock();
            //Etiqueta.Name = Nombre;
            //((TextBlock)Etiqueta).Text = Campo;
            //((TextBlock)Etiqueta).Foreground = Recursos.Blanco_Brush;
            //((TextBlock)Etiqueta).FontSize = Recursos.FontSizeSubTitulo;
            //return Etiqueta;

            FrameworkElement Etiqueta = new TextBlock();
            Etiqueta.Name = Nombre;
            ((TextBlock)Etiqueta).Text = Campo;
            ((TextBlock)Etiqueta).Foreground = Recursos.Blanco_Brush;
            ((TextBlock)Etiqueta).FontSize = Recursos.FontSizeParrafo;
            Etiqueta.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            Etiqueta.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            /*            Localizacion = new eClockBase.Controladores.Localizaciones(Sesion);
                        Localizacion.ObtenTextoEvent += Localizacion_ObtenTextoEvent;
            /*            if (Ayuda)
                            Localizacion.ObtenAyuda(Idioma, Campo, Etiqueta);
                        else
                            Localizacion.ObtenEtiqueta(Nombre, Campo, Etiqueta);*/
            return Etiqueta;
        }


        public bool AgregaAyuda(string CampoNombre, string CampoEtiquetaAyuda, int Fila, int Columna)
        {
            try
            {
                FrameworkElement Etiqueta = NuevaEtiqueta("LblH_" + CampoNombre, Sesion.IDIOMA, CampoEtiquetaAyuda, true);
                xGrid.Children.Add(Etiqueta);
                Grid.SetColumn(Etiqueta, Columna);
                Grid.SetRow(Etiqueta, Fila);
                Grid.SetColumnSpan(Etiqueta, 2);
                return true;
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
            return false;
        }

        void Localizacion_ObtenTextoEvent(object Destino, string Texto)
        {
            //if (((TextBlock)Destino).Text != Texto && Texto.Length > 0)
            ((TextBlock)Destino).Text = Texto;
        }
        public FrameworkElement NuevoComboBox(object Seleccionado, bool Requerido, bool SoloLectura)
        {
            Controles.UC_Combo Combo = new Controles.UC_Combo();
            //Combo.Name = Nombre;
            Combo.Seleccionado = Seleccionado;
            Combo.IsEnabled = !SoloLectura;
            return Combo;
        }

        public FrameworkElement NuevoTextBox(string Texto, int LongitudMaxima, bool Requerido, bool SoloLectura)
        {
            TextBox R = new TextBox();
            R.Text = Texto;
            if (LongitudMaxima > 0)
                R.MaxLength = LongitudMaxima;
            R.IsEnabled = !SoloLectura;
            return R;
        }

        public FrameworkElement NuevoTextBoxPassword(string Texto, int LongitudMaxima, bool Requerido, bool SoloLectura)
        {
            PasswordBox R = new PasswordBox();
            R.Password = Texto;
            if (LongitudMaxima > 0)
                R.MaxLength = LongitudMaxima;
            R.IsEnabled = !SoloLectura;
            return R;
        }

        public FrameworkElement NuevoCheckBox(bool Checado, bool Requerido, bool SoloLectura)
        {

            CheckBox R = new CheckBox();
            R.IsChecked = Checado;
            R.IsEnabled = !SoloLectura;
            return R;
        }

        public FrameworkElement NuevoDatePicker(DateTime? Fecha, DateTime? FechaMinima, DateTime? FechaMaxima, bool Requerido, bool SoloLectura)
        {
            DatePicker R = new DatePicker();
            if (Fecha != null)
            {
                R.DisplayDate = eClockBase.CeC.Convierte2DateTime(Fecha);
                R.Text = R.DisplayDate.ToString();
            }
            R.DisplayDateStart = FechaMinima;
            R.DisplayDateEnd = FechaMinima;
            R.IsEnabled = !SoloLectura;
            return R;
        }

        private void Btn_Guardar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
