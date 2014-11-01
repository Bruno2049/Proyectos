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

namespace eClock5.Controles
{
    /// <summary>
    /// Lógica de interacción para UC_Multiseleccion.xaml
    /// </summary>
    public partial class UC_Multiseleccion : UserControl
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

        private bool m_MostrarBorrados = false;
        [Description("Indica si mostrará borrados."), Category("Datos")]
        public bool MostrarBorrados { get { return m_MostrarBorrados; } set { m_MostrarBorrados = value; } }

        public UC_Multiseleccion()
        {
            InitializeComponent();
            Loaded += UC_Multiseleccion_Loaded;
            ToolBar.OnEventClickToolBar += ToolBar_OnEventClickToolBar;
            Lst_Datos.SelectionChanged += Lst_Datos_SelectionChanged;
        }

        void ToolBar_OnEventClickToolBar(UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Actualizar":
                    ActualizaDatos();
                    break;
                case "Btn_Seleccionar":
                    //
                    foreach (var list in Datos)
                    {
                        list.Seleccionado = true;
                    }
                    ToolBar.Seleccionados = Datos.Count;
                    Lst_Datos.Items.Refresh();
                    //
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
            }
        }

        void Lst_Datos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Lst_Datos.SelectedItems != null)
            {
                
            }
        }

        void UC_Multiseleccion_Loaded(object sender, RoutedEventArgs e)
        {
            if (Lst_Datos.ItemsSource == null)
                ActualizaDatos();
        }
        private List<Listado> Datos = null;
        public class Listado : eClockBase.Controladores.ListadoJson
        {
            public bool Seleccionado { get; set; }
        }
        void ToolBar_OnEventClick(UC_ToolBar_Control Control)
        {
            
        }
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
                    CSesion.ObtenListado(Tabla, CampoLlave, CampoNombre, CampoAdicional, CampoDescripcion, "", MostrarBorrados, Filtro);
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
            try
            {
                Datos = eClockBase.Controladores.CeC_ZLib.Json2Object<List<Listado>>(Listado);
                // Datos[0].Seleccionado = true;
                bool PrimeraVez = Lst_Datos.ItemsSource == null ? true : false;
                Lst_Datos.ItemsSource = Datos;
                if (!PrimeraVez)
                    Lst_Datos.Items.Refresh();
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }   
        }
        private void CheckBox_Click_1(object sender, RoutedEventArgs e)
        {

            var results = from c in Datos
                          where c.Seleccionado == true
                          select new { c };
            ToolBar.Seleccionados = results.Count();
        }

        public string ObtenIDLlave(Listado Elemento)
        {
            if (CampoLlave == null || CampoLlave.Length < 1)
                return "";
            return "{\"" + CampoLlave + "\":\"" + Elemento.Llave.ToString() + "\"}";
        }
    }
}
