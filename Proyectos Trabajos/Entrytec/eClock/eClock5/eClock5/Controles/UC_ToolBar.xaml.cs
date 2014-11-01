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
using System.Windows.Controls.Primitives;
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
using Windows.UI.Xaml.Controls.Primitives;
#endif
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace eClock5
{
    public enum UC_ToolBar_Alineacion
    {
        Izquierda,
        Derecha
    }
    public enum UC_ToolBar_Mostrar
    {
        AmbosCasos,
        SoloSeleccionados,
        NoSeleccionados

    }
    public class UC_ToolBar_Control
    {
        public UC_ToolBar_Mostrar Mostrar { get; set; }
        public string Nombre { get; set; }
        public string Etiqueta { get; set; }
        public bool Clickeado { get; set; }

        public ImageSource Imagen { get; set; }
        public UC_ToolBar_Alineacion Alineacion { get; set; }

        private List<UC_ToolBar_Control> m_Controles = new List<UC_ToolBar_Control>();
        public List<UC_ToolBar_Control> Controles { get { return m_Controles; } }
        public override string ToString()
        {
            return Etiqueta + "(" + Nombre + ")";
        }
        public bool AsignaImagen(string NombreImagen)
        {
            try
            {
                BitmapImage bi = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/" + NombreImagen, UriKind.Absolute));
                Imagen = bi;
                return true;
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
            return false;
        }
        public bool ContieneTexto { get; set; }
       
        public object ControlCreado { get; set; }
    }

    /// <summary>
    /// Lógica de interacción para UC_ToolBar.xaml
    /// </summary>
    public partial class UC_ToolBar : UserControl
    {
        private List<ContentControl> m_SeleccionadosActualizar = new List<ContentControl>();
        private int m_Seleccionados = 0;
        [Description("Asigna el numero de elementos seleccionados."), Category("EntryTec")]
        public int Seleccionados
        {
            get { return m_Seleccionados; }
            set
            {

                bool bSel = HaySeleccionados();
                m_Seleccionados = value;
                foreach (ContentControl Boton in m_SeleccionadosActualizar)
                {
                    Boton.Content = string.Format(Boton.Tag.ToString(), Seleccionados);
                }
                if (HaySeleccionados() != bSel)
                    CreaControles();
            }
        }
        [Description("Listado de controles a utilizar."), Category("EntryTec")]
        private List<UC_ToolBar_Control> m_Controles = new List<UC_ToolBar_Control>();
        public List<UC_ToolBar_Control> Controles { get { return m_Controles; } }
        public delegate void EventClickToolBar(UC_ToolBar_Control Control);
        public event EventClickToolBar OnEventClickToolBar;

        public delegate void EventControlesCreados(UC_ToolBar ToolBar);
        public event EventControlesCreados ControlesCreados;

        private Clases.Parametros m_Parametros = null;
        public UC_ToolBar()
        {

            InitializeComponent();
            Loaded += UC_ToolBar_Loaded;
            //LayoutUpdated += UC_ToolBar_LayoutUpdated;
        }

        void UC_ToolBar_LayoutUpdated(object sender, EventArgs e)
        {
            CreaControles();
            
        }
        public bool HaySeleccionados()
        {
            return Seleccionados > 0 ? true : false;
        }
        public bool AgregarControl(int Pos, UC_ToolBar_Control ControlNuevo)
        {
            foreach (UC_ToolBar_Control Control in Controles)
            {
                if (Control.Nombre == ControlNuevo.Nombre)
                    return false;
            }
            Controles.Insert(0, ControlNuevo);

            return true;
        }
        public void CreaControles()
        {
            try
            {
                Derecha.Children.Clear();
                Izquierda.Children.Clear();
                if (m_Parametros != null && m_Parametros.MostrarRegresar)
                {
                    UC_ToolBar_Control Regresar = new UC_ToolBar_Control();
                    Regresar.Nombre = "Btn_Regresar";
                    Regresar.Etiqueta = "Regresar";
                    Regresar.Alineacion = UC_ToolBar_Alineacion.Izquierda;

                    //eClock5.Properties.Resources.Regresar_64;
                    Regresar.AsignaImagen("Regresar_64.png");
                    AgregarControl(0, Regresar);
                }
                foreach (UC_ToolBar_Control Control in Controles)
                {
                    if (HaySeleccionados() && Control.Mostrar == UC_ToolBar_Mostrar.NoSeleccionados)
                        continue;
                    if (!HaySeleccionados() && Control.Mostrar == UC_ToolBar_Mostrar.SoloSeleccionados)
                        continue;

                    if (Control.ContieneTexto)
                    {
                        eClock5.Controles.UC_TextBoxButton Boton = new eClock5.Controles.UC_TextBoxButton();
                        Boton.Name = Control.Nombre;
                        Boton.Height = this.Height;
                        if (Control.Alineacion == UC_ToolBar_Alineacion.Izquierda)
                            Izquierda.Children.Add(Boton);
                        else
                            Derecha.Children.Add(Boton);
                        try
                        {
                            Boton.Source = Control.Imagen;
                        }
                        catch { }
                        Boton.Click += Bot_Click;
                        Control.ControlCreado = Boton;
                    }
                    else
                    {
                        ContentControl Boton = null;
                        if (Control.Controles != null && Control.Controles.Count > 0)
                            Boton = new DropDownButton();
                        else
                            Boton = new Button();

                        Boton.Name = Control.Nombre;
                        if (Control.Imagen != null)
                        {
                            Image Ima = new Image();

                            try
                            {
                                Ima.Source = Control.Imagen;
                            }
                            catch { }
                            Boton.Content = Ima;
                            Boton.Width = this.Height;

                        }
                        else
                        {
                            if (Control.Etiqueta.IndexOf("{0}") >= 0)
                            {
                                Boton.Tag = Control.Etiqueta;
                                Boton.Content = string.Format(Boton.Tag.ToString(), Seleccionados);
                                m_SeleccionadosActualizar.Add(Boton);
                            }
                            else
                            {
                                Boton.Content = Control.Etiqueta;
                            }
                        }
                        Boton.Height = this.Height;
                        //Menu Menu = new Menu();

                        Boton.BorderBrush = Boton.Background = Brushes.Transparent;
                        if (Control.Alineacion == UC_ToolBar_Alineacion.Izquierda)
                            Izquierda.Children.Add(Boton);
                        else
                            Derecha.Children.Add(Boton);

                        if (Control.Controles != null && Control.Controles.Count > 0)
                        {
                            ContextMenu Menu = new System.Windows.Controls.ContextMenu();

                            foreach (UC_ToolBar_Control ControlChd in Control.Controles)
                            {
                                if (HaySeleccionados() && Control.Mostrar == UC_ToolBar_Mostrar.SoloSeleccionados)
                                    continue;
                                if (!HaySeleccionados() && Control.Mostrar == UC_ToolBar_Mostrar.NoSeleccionados)
                                    continue;
                                MenuItem MItem = new MenuItem();
                                MItem.Name = ControlChd.Nombre;
                                MItem.Header = ControlChd.Etiqueta;
                                MItem.Click += Bot_Click;
                                Menu.Items.Add(MItem);
                                ControlChd.ControlCreado = MItem;
                            }
                            Boton.ContextMenu = Menu;
                            //((DropDownButton)Boton).DropDown.ContextMenu = Menu;
                        }
                        else
                            ((Button)Boton).Click += Bot_Click;
                        Control.ControlCreado = Boton;
                    }

                }
                if (ControlesCreados != null)
                    ControlesCreados(this);
            }
            catch { }
        }

        void Bot_Click(object sender, RoutedEventArgs e)
        {
            if (sender != null && OnEventClickToolBar != null)
                foreach (UC_ToolBar_Control Control in Controles)
                {
                    if (Control.Nombre == ((FrameworkElement)sender).Name)
                        OnEventClickToolBar(Control);
                    foreach (UC_ToolBar_Control ControlI in Control.Controles)
                    {
                        if (ControlI.Nombre == ((FrameworkElement)sender).Name)
                            OnEventClickToolBar(ControlI);
                    }
                }
        }
        void UC_ToolBar_Loaded(object sender, RoutedEventArgs e)
        {

            Tag = m_Parametros = Clases.Parametros.ObtenParametrosPadre(this);             
            CreaControles();
        }
    }

    public class DropDownButton : ToggleButton
    {
        // *** Dependency Properties ***

        public static readonly DependencyProperty DropDownProperty =
          DependencyProperty.Register("DropDown",
                                      typeof(ContextMenu),
                                      typeof(DropDownButton),
                                      new UIPropertyMetadata(null));

        // *** Constructors *** 

        public DropDownButton()
        {
            // Bind the ToogleButton.IsChecked property to the drop-down's IsOpen property 

            Binding binding = new Binding("DropDown.IsOpen");
            binding.Source = this;
            this.SetBinding(IsCheckedProperty, binding);
        }

        // *** Properties *** 

        public ContextMenu DropDown
        {
            get { return (ContextMenu)this.GetValue(DropDownProperty); }
            set { this.SetValue(DropDownProperty, value); }
        }

        // *** Overridden Methods *** 

        protected override void OnClick()
        {

            if (this.DropDown != null)
            {
                // If there is a drop-down assigned to this button, then position and display it 

                this.DropDown.PlacementTarget = this;
                this.DropDown.Placement = PlacementMode.Bottom;

                this.DropDown.IsOpen = true;
            }
            else
            {
                ContextMenu.PlacementTarget = this;
                ContextMenu.IsOpen = true;
                //this.ContextMenu.Visibility = Visibility.Visible;
            }
        }
    }
}
