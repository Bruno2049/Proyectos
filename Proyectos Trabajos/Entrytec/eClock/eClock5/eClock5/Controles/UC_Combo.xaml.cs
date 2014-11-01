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
using System.ComponentModel;
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
using System.Collections;

namespace eClock5.Controles
{

    /// <summary>
    /// Lógica de interacción para UC_Combo.xaml
    /// </summary>

    public partial class UC_Combo : UserControl
    {

        [Description("Nombre de la tabla que almacena los datos."), Category("Datos")]
        public string Tabla { get; set; }
        [Description("Campo con el id que se usará para identificar al elemento."), Category("Datos")]
        public string CampoLlave { get; set; }
        [Description("Campo con el Nombre o texto que mostrará de manera principal."), Category("Datos")]
        public string CampoNombre { get; set; }
        [Description("Campo Adicional, esquina superior derecha."), Category("Datos")]
        public string CampoAdicional { get; set; }
        [Description("Campo con el Nombre o texto que mostrará debajo del nombre."), Category("Datos")]
        public string CampoDescripcion { get; set; }
        [Description("Filtro que aplicará."), Category("Datos")]
        public string Filtro { get; set; }
        [Description("Or en Filtro."), Category("Datos")]
        public string Or { get; set; }
        private bool m_MostrarBorrados = false;
        [Description("Indica si mostrará borrados."), Category("Datos")]
        public bool MostrarBorrados { get { return m_MostrarBorrados; } set { m_MostrarBorrados = value; } }


        /*   public object Seleccionado
           {
               get { return m_Seleccionado; }
               set
               {
                   m_Seleccionado = value;
                   if (m_Listado != null)
                   {
                       foreach (ListadoJson Elemento in m_Listado)
                       {
                           if (Elemento.Llave == value)
                           {
                               Combo.SelectedValue = Elemento;
                           }
                       }
                   }
               }
           }*/



        public ListadoJson SelectedItem
        {
            get { return (ListadoJson)Combo.SelectedItem; }
            set { Combo.SelectedItem = value; }
        }



        public int SeleccionadoInt
        {
            get
            {
                return eClockBase.CeC.Convierte2Int(Seleccionado);
            }
            set
            {
                Seleccionado = value;
            }
        }

        public string SeleccionadoString
        {
            get
            {
                return eClockBase.CeC.Convierte2String(Seleccionado);
            }
            set
            {
                Seleccionado = value;
            }
        }

        public bool AsignaSeleccionado(object oSeleccionado)
        {
            try
            {

                if (m_Listado != null && m_Listado.Count > 0)
                {
                    //object Selec = Convert.ChangeType(oSeleccionado, m_Listado[0].Llave.GetType());
                    foreach (ListadoJson Elemento in m_Listado)
                    {
                        if (eClockBase.CeC.Compara(Elemento.Llave, oSeleccionado))
                        {
                            Combo.SelectedItem = Elemento;
                            break;
                        }
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
            return false;
        }
        private static void OnSeleccionadoIntChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // OnSeleccionadoChanged(sender, e);
        }


        public List<ListadoJson> m_Listado = null;

        public UC_Combo()
        {
            InitializeComponent();
            Loaded += UC_Combo_Loaded;
        }

        public delegate void DatosActualizadosArgs();
        public event DatosActualizadosArgs DatosActualizados;


        bool ActualizaDatos()
        {
            try
            {

                eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);


                eClockBase.Controladores.Sesion CSesion = new eClockBase.Controladores.Sesion(Sesion);
                CSesion.CambioListadoEvent += CSesion_CambioListadoEvent;
                if (Tabla != null && Tabla.Length > 0
                    && CampoLlave != null && CampoLlave.Length > 0
                    && CampoNombre != null && CampoNombre.Length > 0)
                {

                    CSesion.ObtenListado(Tabla, CampoLlave, CampoNombre, CampoAdicional, CampoDescripcion, "", MostrarBorrados, Filtro);
                }
                else
                {
                    string Campo = this.Name;
                    if (Campo != null)
                    {
                        if (Campo.Length > 4)
                        {
                            switch (Campo.Substring(0, 4))
                            {
                                case "Cbx_":
                                case "Ctr_":
                                    Campo = Campo.Substring(4);
                                    break;
                            }
                        }
                        //CeC_Sesion.
                        CSesion.ObtenListadoCatalogo(Campo);
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
            return false;
        }

        void CSesion_CambioListadoEvent(string Listado)
        {

            m_Listado = eClockBase.Controladores.CeC_ZLib.Json2Object<List<ListadoJson>>(Listado);
            Combo.ItemsSource = m_Listado;

            if (Seleccionado != null)
            {
                AsignaSeleccionado(Seleccionado);
            }
            if (DatosActualizados != null)
                DatosActualizados();
        }

        void UC_Combo_Loaded(object sender, RoutedEventArgs e)
        {
            if (Combo.ItemsSource == null)
                ActualizaDatos();
        }
        [Category("Behavior")]
        public event SelectionChangedEventHandler SelectionChanged;
        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListadoJson ElementoSeleccionado = (ListadoJson)Combo.SelectedItem;
            if (ElementoSeleccionado == null || !eClockBase.CeC.Compara(Seleccionado, ElementoSeleccionado.Llave))
            {
                ListadoJson LJ = Combo.SelectedItem as ListadoJson;

                //Seleccionado = LJ.Llave;
                if (Seleccionado == null)
                    Seleccionado = LJ.Llave;
                else
                    Seleccionado = Convert.ChangeType(LJ.Llave, Seleccionado.GetType());
                // DataContext = Seleccionado;
                //NotifyPropertyChanged("Seleccionado");
            }
            if (SelectionChanged != null)
                SelectionChanged(this, e);
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }


        public object Seleccionado
        {
            get { return GetValue(SeleccionadoProperty); }
            set
            {
                SetValue(SeleccionadoProperty, value);

            }
        }

        // Using a DependencyProperty as the backing store for Apps.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SeleccionadoProperty =
            DependencyProperty.Register("Seleccionado", typeof(object), typeof(UC_Combo),
            new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnSeleccionadoChanged
    )));
        private static void OnSeleccionadoChanged(DependencyObject obj,
    DependencyPropertyChangedEventArgs args)
        {
            // When the color changes, set the icon color
            UC_Combo control = (UC_Combo)obj;

            if (control.m_Listado != null)
            {
                foreach (ListadoJson Elemento in control.m_Listado)
                {
                    if (eClockBase.CeC.Compara(Elemento.Llave, control.Seleccionado))
                    {
                        control.Combo.SelectedValue = Elemento;
                        break;
                    }
                }
                /*
                if (control.Seleccionado.GetType().Name == "Int32")
                {
                    int Valor = eClockBase.CeC.Convierte2Int(control.Seleccionado);
                    foreach (ListadoJson Elemento in control.m_Listado)
                    {
                        if (eClockBase.CeC.Convierte2Int(Elemento.Llave) == Valor)
                        {
                            control.Combo.SelectedValue = Elemento;
                        }
                    }
                }
                else
                {
                    foreach (ListadoJson Elemento in control.m_Listado)
                    {
                        if (Elemento.Llave == control.Seleccionado)
                        {
                            control.Combo.SelectedValue = Elemento;
                        }
                    }
                }*/
            }
        }
    }
}
