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

using Newtonsoft.Json;
using System.ComponentModel;
using eClockBase.Controladores;

namespace eClock5.Controles
{
    /// <summary>
    /// Lógica de interacción para UC_ListBox.xaml
    /// </summary>
    public partial class UC_ListBox  : UserControl
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



        private bool m_ControlEdicionAgregado = false;
        public UC_ListBox()
        {
            InitializeComponent();
            Loaded += UC_Listado_Loaded;
            Lst_Datos.SelectionChanged += Lst_Datos_SelectionChanged;
            //            SizeChanged += UC_Listado_SizeChanged;            
        }
        [Category("Behavior")]
        public event SelectionChangedEventHandler SelectionChanged;

        public object SeleccionadoLlave = null;
        void Lst_Datos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (Lst_Datos.SelectedItem != null)
            {
                eClockBase.Controladores.ListadoJson Seleccionado = Lst_Datos.SelectedItem as eClockBase.Controladores.ListadoJson;
                SeleccionadoLlave = Seleccionado.Llave;
            }
            else
                SeleccionadoLlave = null;
            if (SelectionChanged != null)
                SelectionChanged(this, e);
        }

        public ListadoJson Seleccionado
        {
            get { return (ListadoJson)Lst_Datos.SelectedItem; }
            set { Lst_Datos.SelectedItem = value; }
        }


        private List<eClockBase.Controladores.ListadoJson> Datos = null;

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
        public delegate void CambioListadoArgs(UC_ListBox Listado);
        public event CambioListadoArgs CambioListadoEvent;

        public void CambiarListado(string Listado)
        {
            CambiarListado(eClockBase.Controladores.CeC_ZLib.Json2Object<List<eClockBase.Controladores.ListadoJson>>(Listado));
        }
        public void CambiarListado(List<eClockBase.Controladores.ListadoJson> Listado)
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

        public string ObtenIDLlave(eClockBase.Controladores.ListadoJson Elemento)
        {
            if (CampoLlave == null || CampoLlave.Length < 1)
                return "";
            return "{\"" + CampoLlave + "\":\"" + Elemento.Llave.ToString() + "\"}";
        }
    }
}
