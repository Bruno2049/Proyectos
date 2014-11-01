
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
using eClock5.Controles;


namespace eClock5
{
    /// <summary>
    /// Lógica de interacción para UC_Campos.xaml
    /// </summary>
    public partial class UC_Campos : UserControl
    {

        private string m_Tabla = "";

        public string Tabla
        {
            get { return m_Tabla; }
            set { m_Tabla = value; }
        }
        private string m_CamposLlave = "";

        public string CamposLlave
        {
            get { return m_CamposLlave; }
            set { m_CamposLlave = value; }
        }

        private bool m_EsNuevo = true;

        public bool EsNuevo
        {
            get { return m_EsNuevo; }
            set { m_EsNuevo = value; }
        }

        public string CampoLlave { get; set; }

        public string PermisoEditar { get; set; }


        private UserControl m_ControlRelacion = null;

        public UserControl ControlRelacion
        {
            get { return m_ControlRelacion; }
            set { m_ControlRelacion = value; }
        }
        /// <summary>
        /// Campos que no se mostrarán en la edicion si no es la suscripcion 1
        /// </summary>
        //public static string[] CamposOcultosNoSuscripcion1 = new string[] { "SUSCRIPCION_ID", "SITIO_CONSULTA", "PERSONA_ID" };
        public static string[] CamposOcultosNoSuscripcion1 = new string[] { "SUSCRIPCION_ID", "SITIO_CONSULTA" };
        Clases.Parametros Parametros = null;
        eClockBase.CeC_SesionBase Sesion = null;
        eClockBase.Controladores.Localizaciones Localizacion = null;
        private bool m_MostrarToolBar = true;
        public bool MostrarToolBar
        {
            get { return m_MostrarToolBar; }
            set { m_MostrarToolBar = value; }
        }

        private bool m_ControlRelacionAgregado = false;

        private string PreficoCampo = "Ctr_";

        public UC_Campos()
        {

            InitializeComponent();
            Loaded += UC_Campos_Loaded;
            Tb_General.OnEventClickToolBar += Tb_General_OnEventClickToolBar;
            this.IsVisibleChanged += UC_Campos_IsVisibleChanged;
            //SetBinding(MyDataContextProperty, new Binding());
        }
        void Cerrar()
        {
            Clases.Parametros.ObtenPadre(this).Visibility = Visibility.Hidden;
        }
        void Borrar()
        {
            /*            string[] Llaves = eClockBase.CeC.ObtenArregoSeparador(CamposLlave, ",");
                        string FinalJson = "";

                        foreach (string Llave in Llaves)
                        {

                            string ValorJson = Vista2JSon(Llave, null);
                            if (ValorJson != "")
                                FinalJson = eClockBase.CeC.AgregaSeparador(FinalJson, ValorJson, ",");
                            //pi.PropertyType.Name;

                            pi.SetValue(Contexto, Valor, null);
                        }
                        return "{" + FinalJson + "}";*/


            eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Sesion SE = new eClockBase.Controladores.Sesion(Sesion);
            SE.BorradosDatos += SE_BorradosDatos;
            SE.BorrarDatos(Tabla, CamposLlave, Parametros.Parametro.ToString());
        }

        void SE_BorradosDatos(int NoAfectados)
        {
            //throw new NotImplementedException();
        }


        void MuestraRelacion()
        {
            if (m_ControlRelacion == null)
            {
                //MessageBox.Show("Agrege de favor su control de edicion para este listado, no sea usted estupido");
                return;
            }
            m_ControlRelacion.Margin = new Thickness(0);
            m_ControlRelacion.Tag = Clases.Parametros.ObtenParametrosPadre(this);
            if (!m_ControlRelacionAgregado)
            {
                m_ControlRelacion.Width = Double.NaN;
                m_ControlRelacion.Height = Double.NaN;
                Grd_Main.Children.Add(m_ControlRelacion);
                m_ControlRelacionAgregado = true;
                eClock5.BaseModificada.Localizaciones.sLocaliza(m_ControlRelacion);
                m_ControlRelacion.IsVisibleChanged += m_ControlRelacion_IsVisibleChanged;
            }
            else
            {
                Grd_Main.Children.Add(m_ControlRelacion);
                m_ControlRelacion.Visibility = Visibility.Visible;

            }
        }

        void m_ControlRelacion_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!eClockBase.CeC.Convierte2Bool(e.NewValue))
            {
                Grd_Main.Children.Remove(m_ControlRelacion);
            }
        }


        void Tb_General_OnEventClickToolBar(UC_ToolBar_Control Control)
        {

            switch (Control.Nombre)
            {
                case "Btn_Regresar":
                    Cerrar();
                    break;
                case "Btn_Borrar":
                    Borrar();
                    break;
                case "Btn_Relacion":
                    MuestraRelacion();
                    break;
                case "Btn_Guardar":
                    {

                        string Json = Vista2JSon(this.DataContext);
                        eClockBase.Controladores.Sesion CSesion = new eClockBase.Controladores.Sesion(Sesion);
                        CSesion.GuardaDatosEvent += CSesion_GuardaDatosEvent;
                        CSesion.GuardaDatos(Tabla, CamposLlave, Json, EsNuevo);
                        //CSesion.gu(Tabla,CamposLlave,JsonConvert.SerializeObject(
                    }
                    break;

            }
        }

        void CSesion_GuardaDatosEvent(int Guardados)
        {
            if (Guardados > 0)
            {
                Sesion.MuestraMensaje("Guardado", 3);
                Cerrar();
                return;
            }
            else
            {
                Sesion.MuestraMensaje("No se pudo guardar", 5);
                return;
            }

        }

        void UC_Campos_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            if (e.NewValue.Equals(true) && Sesion != null)
            {
                CreaVista(this.DataContext);
            }

        }

        void UC_Campos_Loaded(object sender, RoutedEventArgs e)
        {
            if (!MostrarToolBar)
            {
                Tb_General.Visibility = System.Windows.Visibility.Hidden;
                Scr_Grid.Margin = new Thickness(0);
            }
            Sesion = CeC_Sesion.ObtenSesion(this);

            //        ucParent.Tag
            ///Solo cargará el dataContext en Vista de diseño, de lo contrario deberá cargarlo de otra forma
            //if(DesignerProperties.GetIsInDesignMode(this))

            CreaVista(this.DataContext);
        }

        Clases.Parametros m_ParametrosAnteriores = null;
        private bool CreaVista(object Contexto, bool PrimeraVez = true)
        {
            try
            {
                if (PrimeraVez)
                {

                    Tag = Parametros = Clases.Parametros.ObtenParametrosPadre(this);
                    if (m_ParametrosAnteriores == Parametros)
                        return false;
                    xGrid.Children.Clear();
                    m_ParametrosAnteriores = Parametros;
                    eClockBase.CeC_Log.AgregaLog("CreaVista" + this.Name);
                    EsNuevo = true;
                    if (Parametros != null)
                    {
                        if (Tabla == "")
                            Tabla = Parametros.Listado.Tabla;
                        if (CamposLlave == "")
                            CamposLlave = Parametros.Listado.CampoLlave;

                        if (Parametros.Parametro != null && Parametros.Parametro != "")
                        {
                            Contexto = Newtonsoft.Json.JsonConvert.DeserializeObject(Parametros.Parametro.ToString(), Contexto.GetType());
                            eClockBase.Controladores.Sesion CSesion = new eClockBase.Controladores.Sesion(Sesion);

                            CSesion.ObtenDatosEvent += delegate(int Resultado, string Datos)
                            {
                                try
                                {
                                    if (Resultado > 0)
                                    {
                                        Contexto = Newtonsoft.Json.JsonConvert.DeserializeObject(Datos, Contexto.GetType());
                                        EsNuevo = false;
                                        CreaVista(Contexto, false);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    eClockBase.CeC_Log.AgregaError(ex);
                                }
                            };
                            CSesion.ObtenDatos(Tabla, CamposLlave, Contexto);
                            return false;
                        }
                    }
                }
                PropertyInfo[] properties = Contexto.GetType().GetProperties();
                int NoFila = 0;

                foreach (PropertyInfo pi in properties)
                {
                    if (Sesion.SUSCRIPCION_ID != 1)
                    {
                        if (CamposOcultosNoSuscripcion1.Contains(pi.Name))
                        {
                            if (pi.Name == "SUSCRIPCION_ID")
                                pi.SetValue(Contexto, Sesion.SUSCRIPCION_ID_SELECCIONADA, null);
                            continue;
                        }
                    }
                    RowDefinition Fila = new RowDefinition();
                    //Fila.Height = new GridLength(36);

                    xGrid.RowDefinitions.Add(Fila);
                    object Valor = pi.GetValue(Contexto, null);
                    //try
                    //{
                    //    if (((Valor != null) && (Convert.ToInt32(Valor) != 0)) || Convert.ToDateTime(Valor) == Convert.ToDateTime("01/01/0001"))
                    //    {
                    //        DateTime Valor1 = Convert.ToDateTime(Valor);
                    //        if (Valor1 == Convert.ToDateTime("01/01/0001"))
                    //        {
                    //            Valor = System.DateTime.Now;
                    //        }
                    //    }
                    //}
                    //catch
                    //{
                    //}
                    FrameworkElement Fe = AgregaEtiqueta(Contexto, pi, Valor, NoFila, 0);

                    AgregaCampo(Contexto, pi, Valor, NoFila, 1);
                    NoFila++;
                    Fila = new RowDefinition();
                    xGrid.RowDefinitions.Add(Fila);
                    AgregaAyuda(Contexto, pi, Valor, NoFila, 1);

                    NoFila++;
                }
                return true;
            }
            catch (Exception ex)
            {

                eClockBase.CeC_Log.AgregaError(ex);
            }
            return false;
        }


        public string Json(string Campo, string Valor)
        {
            return eClockBase.CeC.AgregaSeparador("\"" + Campo + "\"", Valor, ":");
        }
        public string Vista2JSon(object Contexto)
        {
            string FinalJson = "";
            CamposNoGuardados = "";
            PropertyInfo[] properties = Contexto.GetType().GetProperties();
            foreach (PropertyInfo pi in properties)
            {
                object Valor = pi.GetValue(Contexto, null);

                string ValorJson = Vista2JSon(pi.Name, Valor);
                if (ValorJson != "")
                    FinalJson = eClockBase.CeC.AgregaSeparador(FinalJson, ValorJson, ",");
                //pi.PropertyType.Name;

                pi.SetValue(Contexto, Valor, null);
            }
            return "{" + FinalJson + "}";
        }
        string CamposNoGuardados = "";
        /// <summary>
        /// Obtiene el valor actual ctde una propiedad leyendola de la vista
        /// </summary>
        /// <param name="Propiedad"></param>
        /// <param name="ValorAnterior"></param>
        /// <returns></returns>
        public string Vista2JSon(string Propiedad, object ValorAnterior)
        {
            try
            {
                object Valor = ValorAnterior;
                foreach (FrameworkElement Control in xGrid.Children)
                {
                    if (Control.Name == PreficoCampo + Propiedad)
                    {
                        string ValorStr = ObtenValorString(Control);
                        return Json(Propiedad, ValorStr);
                    }
                }
                return Json(Propiedad, JsonConvert.SerializeObject(Valor));
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
            CamposNoGuardados = eClockBase.CeC.AgregaSeparador(CamposNoGuardados, Propiedad, ",");
            return "";
        }
        public static string ObtenValorString(object Objeto)
        {
            String TextoAGuardar = "";
            switch (Objeto.GetType().Name)
            {
                case "DatePicker":
                    {
                        DateTime? DP = ((DatePicker)Objeto).SelectedDate;
                        if (DP == null || DP == new DateTime())
                            return "";
                        TextoAGuardar = JsonConvert.SerializeObject(DP);
                    }
                    break;
                case "CheckBox":
                    TextoAGuardar = JsonConvert.SerializeObject(((CheckBox)Objeto).IsChecked);//.ToString();
                    break;
                case "PasswordBox":
                    TextoAGuardar = JsonConvert.SerializeObject(((PasswordBox)Objeto).Password);
                    break;
                case "UC_ColorPicker":
                    TextoAGuardar = JsonConvert.SerializeObject(((UC_ColorPicker)Objeto).iColorActual);
                    break;
                case "UC_Combo":
                    TextoAGuardar = JsonConvert.SerializeObject(((UC_Combo)Objeto).Seleccionado);
                    break;
                case "UC_Foto":
                    TextoAGuardar = JsonConvert.SerializeObject(((UC_Foto)Objeto).ImagenBytes);
                    break;
                case "UC_Horas":
                    TextoAGuardar = JsonConvert.SerializeObject(((UC_Horas)Objeto).Value);
                    break;
                case "UC_CamposTextoDiseno":
                    TextoAGuardar = JsonConvert.SerializeObject(((UC_CamposTextoDiseno)Objeto).Valor);
                    break;
                case "UC_Direccion":
                    TextoAGuardar = JsonConvert.SerializeObject(((UC_Direccion)Objeto).Valor);
                    break;
                case "UC_PersonaLinkID":
                    TextoAGuardar = JsonConvert.SerializeObject(((UC_PersonaLinkID)Objeto).PersonaID);
                    break;
                default:
                    TextoAGuardar = JsonConvert.SerializeObject(((TextBox)Objeto).Text);
                    break;
            }
            return TextoAGuardar;
        }
        public FrameworkElement AgregaEtiqueta(object Contexto, PropertyInfo Propiedad, object Valor, int Fila, int Columna)
        {
            try
            {
                FrameworkElement Etiqueta = NuevaEtiqueta("Lbl_" + Propiedad.Name, Sesion.IDIOMA, Propiedad.Name, false);
                Etiqueta.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                Etiqueta.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                Etiqueta.MouseRightButtonDown+= delegate(object sender, MouseButtonEventArgs e)
                {
                    Vista.Localizaciones.Traducciones.CargaTraduccion(Propiedad.Name);
                };

                xGrid.Children.Add(Etiqueta);
                Grid.SetColumn(Etiqueta, Columna);
                Grid.SetRow(Etiqueta, Fila);
                return Etiqueta;
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
            return null;
        }


        public FrameworkElement NuevoCampo(object Contexto, PropertyInfo Propiedad, object Valor)
        {
            FrameworkElement Campo = null;

            ///Lee los atributos, aplicados al campo, si alguno es para mostrar un control , entonces creara el control
            ///y saldra, de lo contrario creara una etiqueta

            MemberInfo Tipo = Propiedad as MemberInfo;

            foreach (object Atributo in Tipo.GetCustomAttributes(true))
            {
                switch (Atributo.GetType().ToString())
                {
                    case "eClockBase.Atributos_Campos.Campo_StringAttribute":
                        {
                            eClockBase.Atributos_Campos.Campo_StringAttribute CampoString = ((eClockBase.Atributos_Campos.Campo_StringAttribute)Atributo);
                            switch (CampoString.Control)
                            {
                                case eClockBase.Atributos_Campos.Campo_StringAttribute.Tipo.TextBox:
                                    Campo = NuevoTextBox(CampoString.ToString(Valor), CampoString.LongitudMaxima, CampoString.Requerido, CampoString.SoloLectura);
                                    break;
                                case eClockBase.Atributos_Campos.Campo_StringAttribute.Tipo.TextBoxPassword:
                                    Campo = NuevoTextBoxPassword(CampoString.ToString(Valor), CampoString.LongitudMaxima, CampoString.Requerido, CampoString.SoloLectura);
                                    break;
                                case eClockBase.Atributos_Campos.Campo_StringAttribute.Tipo.TextBoxMemo:
                                    Campo = NuevoTextBoxMemo(CampoString.ToString(Valor), CampoString.LongitudMaxima, CampoString.Requerido, CampoString.SoloLectura);
                                    break;
                                case eClockBase.Atributos_Campos.Campo_StringAttribute.Tipo.Color:
                                    Campo = NuevoColor(Valor, CampoString.Requerido, CampoString.SoloLectura);
                                    break;
                                case eClockBase.Atributos_Campos.Campo_StringAttribute.Tipo.CamposTextoDiseno:
                                    Campo = NuevoCamposTextoDiseno(Valor, CampoString.Requerido, CampoString.SoloLectura);
                                    break;
                                case eClockBase.Atributos_Campos.Campo_StringAttribute.Tipo.TerminalDir:
                                    Campo = NuevoTerminalDir(Valor, CampoString.Requerido, CampoString.SoloLectura);
                                    break;

                            }
                            break;
                        }
                    case "eClockBase.Atributos_Campos.Campo_IntAttribute":
                        {
                            eClockBase.Atributos_Campos.Campo_IntAttribute CampoInt = ((eClockBase.Atributos_Campos.Campo_IntAttribute)Atributo);
                            switch (CampoInt.Control)
                            {
                                case eClockBase.Atributos_Campos.Campo_IntAttribute.Tipo.TextBox:
                                    Campo = NuevoTextBox(CampoInt.ToString(Valor), -1, CampoInt.Requerido, CampoInt.SoloLectura);
                                    break;
                                case eClockBase.Atributos_Campos.Campo_IntAttribute.Tipo.ComboBox:
                                    Campo = NuevoComboBox(Valor, CampoInt.Requerido, CampoInt.SoloLectura);
                                    break;
                                case eClockBase.Atributos_Campos.Campo_IntAttribute.Tipo.Color:
                                    Campo = NuevoColor(Valor, CampoInt.Requerido, CampoInt.SoloLectura);
                                    break;
                                case eClockBase.Atributos_Campos.Campo_IntAttribute.Tipo.PersonaLinkID:
                                    Campo = NuevoPersonaLinkID(Valor, CampoInt.Requerido, CampoInt.SoloLectura);
                                    break;
                            }
                            break;
                        }
                    case "eClockBase.Atributos_Campos.Campo_DecimalAttribute":
                        {
                            eClockBase.Atributos_Campos.Campo_DecimalAttribute CampoDec = ((eClockBase.Atributos_Campos.Campo_DecimalAttribute)Atributo);
                            switch (CampoDec.Control)
                            {
                                case eClockBase.Atributos_Campos.Campo_DecimalAttribute.Tipo.TextBox:
                                    Campo = NuevoTextBox(CampoDec.ToString(Valor), -1, CampoDec.Requerido, CampoDec.SoloLectura);
                                    break;
                                case eClockBase.Atributos_Campos.Campo_DecimalAttribute.Tipo.ComboBox:
                                    Campo = NuevoComboBox(Valor, CampoDec.Requerido, CampoDec.SoloLectura);
                                    break;
                            }
                            break;
                        }
                    case "eClockBase.Atributos_Campos.Campo_BoolAttribute":
                        {
                            eClockBase.Atributos_Campos.Campo_BoolAttribute CampoBool = ((eClockBase.Atributos_Campos.Campo_BoolAttribute)Atributo);
                            switch (CampoBool.Control)
                            {
                                case eClockBase.Atributos_Campos.Campo_BoolAttribute.Tipo.CheckBox:
                                    Campo = NuevoCheckBox(CampoBool.ToBool(Valor), CampoBool.Requerido, CampoBool.SoloLectura);
                                    break;
                            }
                            break;
                        }
                    case "eClockBase.Atributos_Campos.Campo_FechaHoraAttribute":
                        {
                            eClockBase.Atributos_Campos.Campo_FechaHoraAttribute CampoFH = ((eClockBase.Atributos_Campos.Campo_FechaHoraAttribute)Atributo);
                            switch (CampoFH.Control)
                            {
                                case eClockBase.Atributos_Campos.Campo_FechaHoraAttribute.Tipo.TextBox:
                                    Campo = NuevoTextBox(CampoFH.ToString(Valor), -1, CampoFH.Requerido, CampoFH.SoloLectura);
                                    break;
                                case eClockBase.Atributos_Campos.Campo_FechaHoraAttribute.Tipo.DatePicker:
                                    if ((Convert.ToDateTime(Valor)) == Convert.ToDateTime("01/01/0001"))
                                    {
                                        Campo = NuevoDatePicker(CampoFH.ToDateTime(CampoFH.ValorPredeterminado), CampoFH.ValorMinimo, CampoFH.ValorMaximo, CampoFH.Requerido, CampoFH.SoloLectura);
                                    }
                                    else
                                    {
                                        Campo = NuevoDatePicker(CampoFH.ToDateTime(Valor), CampoFH.ValorMinimo, CampoFH.ValorMaximo, CampoFH.Requerido, CampoFH.SoloLectura);
                                    }

                                    break;
                                case eClockBase.Atributos_Campos.Campo_FechaHoraAttribute.Tipo.Horas:
                                    Campo = NuevoHoras(CampoFH.ToDateTime(Valor), CampoFH.Requerido, CampoFH.SoloLectura);
                                    break;

                            }
                            break;
                        }
                    case "eClockBase.Atributos_Campos.Campo_BinarioAttribute":
                        {
                            eClockBase.Atributos_Campos.Campo_BinarioAttribute CampoBin = ((eClockBase.Atributos_Campos.Campo_BinarioAttribute)Atributo);
                            switch (CampoBin.Control)
                            {
                                case eClockBase.Atributos_Campos.Campo_BinarioAttribute.Tipo.Imagen:
                                    Campo = NuevaFoto(Valor, CampoBin.Requerido, CampoBin.SoloLectura);
                                    //Campo = NuevoTextBox(CampoFH.ToString(Valor), -1, CampoFH.Requerido, CampoFH.SoloLectura);
                                    break;
                                /*
                            case eClockBase.Atributos_Campos.Campo_BinarioAttribute.Tipo.:
                                Campo = NuevoDatePicker(CampoFH.ToDateTime(Valor), CampoFH.ValorMinimo, CampoFH.ValorMaximo, CampoFH.Requerido, CampoFH.SoloLectura);
                                break;*/

                            }

                        }
                        break;

                }
            }
            if (Campo == null)
            {
                ///Si no se encuentra definido el atributo de tipo de campo usará un predeterminado
                switch (Propiedad.PropertyType.ToString())
                {
                    case "System.String":
                        Campo = NuevoTextBox(eClockBase.CeC.Convierte2String(Valor), -1, false, !Propiedad.CanWrite);
                        break;
                    case "System.Int32":
                        Campo = NuevoTextBox(eClockBase.CeC.Convierte2String(Valor), -1, false, !Propiedad.CanWrite);
                        break;
                    case "System.Boolean":
                        Campo = NuevoCheckBox(eClockBase.CeC.Convierte2Bool(Valor, false), false, !Propiedad.CanWrite);
                        break;
                    case "System.DateTime":
                        Campo = NuevoDatePicker(eClockBase.CeC.Convierte2DateTimeN(Valor, null), null, null, false, !Propiedad.CanWrite);
                        break;
                    case "System.Decimal":
                        Campo = NuevoTextBox(eClockBase.CeC.Convierte2String(Valor), -1, false, !Propiedad.CanWrite);
                        break;
                    case "System.Double":
                        Campo = NuevoTextBox(eClockBase.CeC.Convierte2String(Valor), -1, false, !Propiedad.CanWrite);
                        break;
                }

                if (Campo == null)
                {
                    Campo = new Label();
                    ((Label)Campo).Content = "No Soportado";
                }
            }

            Campo.Name = PreficoCampo + Propiedad.Name;
            return Campo;
        }



        public FrameworkElement AgregaCampo(object Contexto, PropertyInfo Propiedad, object Valor, int Fila, int Columna)
        {
            try
            {
                FrameworkElement Campo = null;
                Campo = NuevoCampo(Contexto, Propiedad, Valor);
                Campo.Margin = new Thickness(5);
                //Campo.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                xGrid.Children.Add(Campo);
                Grid.SetColumn(Campo, Columna);
                Grid.SetRow(Campo, Fila);
                return Campo;
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
            return null;
        }

        public bool AgregaAyuda(object Contexto, PropertyInfo Propiedad, object Valor, int Fila, int Columna)
        {
            try
            {
                FrameworkElement Etiqueta = NuevaEtiqueta("LblH_" + Propiedad.Name, Sesion.IDIOMA, Propiedad.Name, true);
                Etiqueta.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
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


        public FrameworkElement NuevaEtiqueta(string Nombre, string Idioma, string Campo, bool Ayuda)
        {
            FrameworkElement Etiqueta = new TextBlock();
            Etiqueta.Name = Nombre;
            ((TextBlock)Etiqueta).Text = Campo;
            /*            ((TextBlock)Etiqueta).Foreground = Recursos.Blanco_Brush;
                        ((TextBlock)Etiqueta).FontSize = Recursos.FontSizeParrafo;*/
            ((TextBlock)Etiqueta).TextWrapping = TextWrapping.Wrap;
            Localizacion = new eClockBase.Controladores.Localizaciones(Sesion);
            Localizacion.ObtenTextoEvent += Localizacion_ObtenTextoEvent;
            if (Ayuda)
                Localizacion.ObtenAyuda(Campo, Campo, Etiqueta);
            else
                Localizacion.ObtenEtiqueta(Campo, Campo, Etiqueta);
            return Etiqueta;
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


        public FrameworkElement NuevoColor(object Color, bool Requerido, bool SoloLectura)
        {
            Controles.UC_ColorPicker ColorPicker = new Controles.UC_ColorPicker();
            //Combo.Name = Nombre;

            ColorPicker.IsEnabled = !SoloLectura;
            ColorPicker.iColorActual = eClockBase.CeC.Convierte2Int(Color);
            return ColorPicker;
        }

        public FrameworkElement NuevoPersonaLinkID(object PersonaID, bool Requerido, bool SoloLectura)
        {
            Controles.UC_PersonaLinkID PersonaLinkID = new Controles.UC_PersonaLinkID();
            //Combo.Name = Nombre;

            PersonaLinkID.IsEnabled = !SoloLectura;
            PersonaLinkID.PersonaID = eClockBase.CeC.Convierte2Int(PersonaID);
            return PersonaLinkID;
        }

        public FrameworkElement NuevoCamposTextoDiseno(object Texto, bool Requerido, bool SoloLectura)
        {
            Controles.UC_CamposTextoDiseno Campo = new Controles.UC_CamposTextoDiseno();
            //Combo.Name = Nombre;

            Campo.IsEnabled = !SoloLectura;
            Campo.Valor = eClockBase.CeC.Convierte2String(Texto);
            return Campo;
        }

        public FrameworkElement NuevoTerminalDir(object Texto, bool Requerido, bool SoloLectura)
        {
            Controles.UC_Direccion Campo = new Controles.UC_Direccion();
            //Combo.Name = Nombre;

            Campo.IsEnabled = !SoloLectura;
            Campo.Valor = eClockBase.CeC.Convierte2String(Texto);
            return Campo;
        }

        public FrameworkElement NuevoHoras(object FechaHora, bool Requerido, bool SoloLectura)
        {
            Controles.UC_Horas Campo = new Controles.UC_Horas();
            //Combo.Name = Nombre;

            Campo.IsEnabled = !SoloLectura;
            Campo.Value = eClockBase.CeC.Convierte2DateTime(FechaHora);
            return Campo;
        }

        public FrameworkElement NuevaFoto(object Foto, bool Requerido, bool SoloLectura)
        {
            Controles.UC_Foto Imagen = new Controles.UC_Foto();
            //Combo.Name = Nombre;
            if (Foto != null)
                Imagen.ImagenBytes = (byte[])Foto;
            Imagen.IsEnabled = !SoloLectura;
            //            Imagen.ImagenBytes
            return Imagen;
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
        public FrameworkElement NuevoTextBoxMemo(string Texto, int LongitudMaxima, bool Requerido, bool SoloLectura)
        {
            TextBox R = (TextBox)NuevoTextBox(Texto, LongitudMaxima, Requerido, SoloLectura);
            R.AcceptsReturn = true;
            R.Height = 100;
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
            R.DisplayDateEnd = FechaMaxima;
            R.IsEnabled = !SoloLectura;
            return R;
        }

        private void Btn_Guardar_Click(object sender, RoutedEventArgs e)
        {

        }
        public bool Localiza()
        {
            return false;
        }

        private void Tb_General_ControlesCreados(UC_ToolBar ToolBar)
        {
            foreach (UC_ToolBar_Control Control in ToolBar.Controles)
            {
                if (Control.Nombre == "Btn_Relacion" && ControlRelacion == null)
                    ((Button)Control.ControlCreado).Visibility = System.Windows.Visibility.Collapsed;
            }
        }
    }
}
