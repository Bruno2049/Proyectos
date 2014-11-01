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
using Newtonsoft.Json;
using System.ComponentModel;
using eClockBase.Controladores;
using System.Collections.ObjectModel;
namespace eClock5
{
    /// <summary>
    /// Lógica de interacción para UC_Listado.xaml
    /// </summary>
    /// <param name="Tabla"></param>
    public partial class UC_Listado : UserControl
    {
#if !NETFX_CORE
        [Description("Nombre de la tabla que almacena los datos."), Category("Datos")]
#endif
        public string Tabla { get; set; }
#if !NETFX_CORE
        [Description("Campo con el id que se usará para identificar al elemento."), Category("Datos")]
#endif
        public string CampoLlave { get; set; }
#if !NETFX_CORE
        [Description("Campo con el Nombre o texto que mostrará de manera principal."), Category("Datos")]
#endif
        public string CampoNombre { get; set; }
#if !NETFX_CORE
        [Description("Campo Adicional, esquina superior derecha."), Category("Datos")]
#endif
        public string CampoAdicional { get; set; }
#if !NETFX_CORE
        [Description("Campo con el Nombre o texto que mostrará debajo del nombre."), Category("Datos")]
#endif
        public string CampoDescripcion { get; set; }
#if !NETFX_CORE
        [Description("Filtro que aplicará."), Category("Datos")]
#endif
        public string Filtro { get; set; }

        [Description("Or en Filtro."), Category("Datos")]
        public string Or { get; set; }

        private bool m_MostrarBorrados = false;
#if !NETFX_CORE
        [Description("Indica si mostrará borrados."), Category("Datos")]
#endif
        public bool MostrarBorrados { get { return m_MostrarBorrados; } set { m_MostrarBorrados = value; } }

        private UserControl m_ControlEdicion = null;

        public UserControl ControlEdicion
        {
            get { return m_ControlEdicion; }
            set { m_ControlEdicion = value; }
        }

        private UserControl m_ControlImportar = null;

        public UserControl ControlImportar
        {
            get { return m_ControlImportar; }
            set { m_ControlImportar = value; }
        }

        private UserControl m_ControlExportar = null;

        public UserControl ControlExportar
        {
            get { return m_ControlExportar; }
            set { m_ControlExportar = value; }
        }

        private bool m_MostrarToolBar = true;
        public bool MostrarToolBar
        {
            get
            {
                return m_MostrarToolBar;
            }
            set
            {
                m_MostrarToolBar = value;
                if (MostrarToolBar)
                {
                    ToolBar.Visibility = System.Windows.Visibility.Visible;
                    Lst_Datos.Margin = new Thickness(0, ToolBar.Height, 0, 0);
                }
                else
                {
                    ToolBar.Visibility = System.Windows.Visibility.Hidden;
                    Lst_Datos.Margin = new Thickness(0, 0, 0, 0);
                }
            }
        }

        private string m_Modelos = "";

        public string Modelos
        {
            get { return m_Modelos; }
            set { m_Modelos = value; }
        }

        public string PermisoConsultar { get; set; }
        public string PermisoBorrar { get; set; }
        public string PermisoAlta { get; set; }
        public string PermisoMostrarBorrados { get; set; }
        private bool m_ControlEdicionAgregado = false;

        public bool IsVisible { get; set; }


        public UC_Listado()
        {
            InitializeComponent();
            Loaded += UC_Listado_Loaded;
            ToolBar.OnEventClickToolBar += ToolBar_OnEventClickToolBar;
            Lst_Datos.SelectionChanged += Lst_Datos_SelectionChanged;
            //            SizeChanged += UC_Listado_SizeChanged;            
        }

        void MuestraEdicion(string Llave)
        {
            if (m_ControlEdicion == null)
            {
                MessageBox.Show("Agrege de favor su control de edicion para este listado, valla alas propiedades de UC_Listado y en control edición agregue la vista que usará para edición");
                return;
            }
            m_ControlEdicion.Margin = new Thickness(0);
            m_ControlEdicion.Tag = new Clases.Parametros(true, Llave, this);
            if (!m_ControlEdicionAgregado)
            {
                m_ControlEdicion.Width = Double.NaN;
                m_ControlEdicion.Height = Double.NaN;

                this.Grid.Height = 680;

                Grid.Children.Add(m_ControlEdicion);
                m_ControlEdicionAgregado = true;
                eClock5.BaseModificada.Localizaciones.sLocaliza(m_ControlEdicion);
            }
            else
            {
                m_ControlEdicion.Visibility = Visibility.Visible;
            }
        }

        public Listado Seleccionado
        {
            get
            {
                if (Lst_Datos.SelectedItem == null)
                    return null;
                return Lst_Datos.SelectedItem as Listado;
            }
        }

        [Category("Behavior")]
        public event SelectionChangedEventHandler SelectionChanged;
        DateTime FechaSeleccitionChanged = new DateTime();
        void Lst_Datos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FechaSeleccitionChanged = DateTime.Now;
            if (SelectionChanged != null)
                SelectionChanged(this, e);

            if (m_ControlEdicion != null && Lst_Datos.SelectedItem != null)
            {


                Listado Seleccionado = Lst_Datos.SelectedItem as Listado;
                MuestraEdicion(ObtenIDLlave(Seleccionado));

                Lst_Datos.SelectedItem = null;
            }

        }
        public List<Listado> Datos = null;
        void ToolBar_OnEventClickToolBar(UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Actualizar":
                    ActualizaDatos();
                    break;
                case "Btn_Nuevo":
                    MuestraEdicion("");
                    break;
                case "Btn_MostrarBorrados":
                    m_MostrarBorrados = true;
                    ActualizaDatos();
                    break;
                case "Btn_DeSeleccionar":

                    var results = from c in Datos
                                  where c.Seleccionado == true
                                  select c;

                    foreach (var List in results)
                    {
                        List.Seleccionado = false;
                    }
                    ToolBar.Seleccionados = 0;
                    Lst_Datos.Items.Refresh();
                    break;
                case "Btn_SeleccionarTodos":
                    foreach (var Dato in Datos)
                    {
                        Dato.Seleccionado = true;
                    }
                    ToolBar.Seleccionados = Datos.Count();
                    Lst_Datos.Items.Refresh();
                    break;
                case "Btn_Borrar":
                    Borrar();
                    break;
                case "Btn_Filtrar":
                    //Filtrar(((eClock5.Controles.UC_TextBoxButton)Control.ControlCreado).Texto);
                    break;

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Texto"></param>
        public void Filtrar(string Texto)
        {
            Texto = Texto.ToLower();
            var results = from c in Datos
                          where (c.Nombre != null && c.Nombre.ToString().ToLower().Contains(Texto)) || (c.Descripcion != null && c.Descripcion.ToString().ToLower().Contains(Texto))
                          select c;
            Lst_Datos.ItemsSource = results;
        }
        public void QuitarElemento(Listado Elemento)
        {
            ToolBar.Seleccionados--;
            Datos.Remove(Elemento);
            Lst_Datos.Items.Refresh();

        }
        public void Borrar()
        {
            var results = from c in Datos
                          where c.Seleccionado == true
                          select c;
            int ABorrar = 0;
            string Json = "";
            eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);
            foreach (var List in results)
            {

                eClockBase.Controladores.Sesion SE = new eClockBase.Controladores.Sesion(Sesion);
                SE.BorradosDatos += delegate(int NoAfectados)
                {
                    if (NoAfectados > 0)
                    {
                        QuitarElemento(List);
                    }
                };
                SE.BorrarDatos(Tabla, CampoLlave, ObtenIDLlave(List));
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Padre"></param>
        /// <param name="Hijo"></param>
        /// <param name="ColorFondo"></param>
        public static void MuestraComoDialogo(ContentControl Padre, UserControl Hijo, Color ColorFondo)
        {
            MuestraComoDialogo(Padre, Hijo, new SolidColorBrush(ColorFondo));
        }
        public static void MuestraComoDialogo(ContentControl Padre, UserControl Hijo, Brush ColorFondo)
        {
            Hijo.Width = double.NaN;
            Hijo.Height = double.NaN;
            Hijo.Margin = new Thickness(0);
            Hijo.Background = ColorFondo;
            ((Panel)Padre.Content).Children.Add(Hijo);
            Hijo.IsVisibleChanged += Hijo_IsVisibleChanged;

        }
        static void Hijo_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            bool Visible = ((bool)e.NewValue);
            if (!Visible)
            {
                ((Panel)((UserControl)sender).Parent).Children.Remove((UserControl)sender);
            }
        }


        public bool ActualizaDatos()
        {
            try
            {

                eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);


                eClockBase.Controladores.Sesion CSesion = new eClockBase.Controladores.Sesion(Sesion);
                CSesion.CambioListadoEvent += CambiarListado;
                if (Tabla != null && Tabla.Length > 0
                    && CampoLlave != null && CampoLlave.Length > 0
                    && CampoNombre != null && CampoNombre.Length > 0)
                    CSesion.ObtenListado(Tabla, CampoLlave, CampoNombre, CampoAdicional, CampoDescripcion, "", MostrarBorrados, Filtro, Or);
                return true;
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
            return false;
        }
        public delegate void CambioListadoArgs(UC_Listado Listado);
        public event CambioListadoArgs CambioListadoEvent;

        public void CambiarListado(string Listado)
        {
            CambiarListado(JsonConvert.DeserializeObject<List<Listado>>(Listado));
        }

        public void CambiarListado(List<eClockBase.Controladores.ListadoJson> Listado)
        {
            List<Listado> ListadoNuevo = new List<UC_Listado.Listado>();
            foreach (eClockBase.Controladores.ListadoJson Elemento in Listado)
            {
                ListadoNuevo.Add(new Listado(Elemento));
            }
            CambiarListado(ListadoNuevo);
        }

        public void CambiarListado(List<Listado> Listado)
        {
            try
            {
                Datos = Listado;
                // Datos[0].Seleccionado = true;
                bool PrimeraVez = Lst_Datos.ItemsSource == null ? true : false;
                Lst_Datos.ItemsSource = Datos;
                if (!PrimeraVez)
                    Lst_Datos.Items.Refresh();
                if (CambioListadoEvent != null)
                    CambioListadoEvent(this);

            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
        }

        void UC_Listado_Loaded(object sender, RoutedEventArgs e)
        {
            if (Lst_Datos.ItemsSource == null)
                ActualizaDatos();
        }

        public class Listado : eClockBase.Controladores.ListadoJson
        {
            public bool Seleccionado { get; set; }
            public Listado()
            {

            }
            public Listado(eClockBase.Controladores.ListadoJson ListadoBase)
            {
                this.Llave = ListadoBase.Llave;
                this.Nombre = ListadoBase.Nombre;
                this.Descripcion = ListadoBase.Descripcion;
                this.Adicional = ListadoBase.Adicional;
                this.Imagen = ListadoBase.Imagen;
            }
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {


        }

        private void CheckBox_Click_1(object sender, RoutedEventArgs e)
        {

            var results = from c in Datos
                          where c.Seleccionado == true
                          select new { c };
            ToolBar.Seleccionados = results.Count();
        }
        /// <summary>
        /// Regresa un pseudo modelo donde solo se contiene el o los campos llave con su valor
        /// </summary>
        /// <param name="Elemento"></param>
        /// <returns></returns>
        public string ObtenIDLlave(Listado Elemento)
        {
            if (CampoLlave == null || CampoLlave.Length < 1)
                return "";
            return "{\"" + CampoLlave + "\":\"" + Elemento.Llave.ToString() + "\"}";
        }

        private void ToolBar_ControlesCreados(UC_ToolBar ToolBar)
        {
            ChecaControles(ToolBar.Controles);
        }
        private void Oculta(UC_ToolBar_Control Control)
        {

        }
        private void ChecaControles(List<UC_ToolBar_Control> Controles)
        {
            if (Controles == null)
                return;

            foreach (UC_ToolBar_Control Control in Controles)
            {
                ChecaControles(Control.Controles);
                if (Control.Nombre == "Btn_Reportes" && (Modelos == null || Modelos == ""))
                    Oculta(Control);
                if (Control.Nombre == "Btn_ImportarAdv" && ControlImportar == null)
                    Oculta(Control);
                if (Control.Nombre == "Btn_ExportarAdv" && ControlExportar == null)
                    Oculta(Control);
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (m_ControlEdicionAgregado)
            {
                Grid.Children.Remove(m_ControlEdicion);
            }
        }

        private void Lst_Datos_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TimeSpan Ts = DateTime.Now - FechaSeleccitionChanged;
            if (Ts.TotalSeconds > 1)
                if (SelectionChanged != null)
                    SelectionChanged(this, null);
        }
        bool Abajo = true;
        private void Lst_Datos_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Abajo = true;
        }

    }
}
