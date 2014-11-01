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
namespace eClock5.Vista.Accesos
{
    /// <summary>
    /// Lógica de interacción para Listado.xaml
    /// </summary>
    public partial class Listado : UserControl
    {


        public Listado()
        {
            InitializeComponent();
            Loaded += Listado_Loaded;
            IsVisibleChanged += Listado_IsVisibleChanged;
        }

        void Listado_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.Equals(true))
            {
                
            }
        }


        void Listado_Loaded(object sender, RoutedEventArgs e)
        {


        }

        string Agrupacion = "";
        int PersonaID = -1;

        public void ActualizaDatos()
        {


            eClockBase.Controladores.Accesos Acceso;
            Acceso = new eClockBase.Controladores.Accesos(CeC_Sesion.ObtenSesion(this));

            Acceso.EventoObtenAccesosFinalizado += Acceso_EventoObtenAccesosFinalizado;


            string Parametros = eClockBase.CeC.Convierte2String(Clases.Parametros.Tag2Parametros(Tag).Parametro);
            DateTime FechaFinP1 = RangoFechas.FechaFin.AddDays(1);
            if (Parametros == "")
                Agrupacion = "";
            else
                if (Parametros[0] == '|' || Parametros[0] == '@')
                    Agrupacion = Parametros;
                else
                    PersonaID = eClockBase.CeC.Convierte2Int(Parametros);

            Acceso.ObtenAccesos(true, PersonaID, Agrupacion, RangoFechas.FechaInicio, FechaFinP1, "", "");
        }



        void Acceso_EventoObtenAccesosFinalizado(List<eClockBase.Modelos.Accesos.Model_Accesos> Accesos)
        {
            try
            {
                bool primeravez = Lst_Datos.ItemsSource == null ? true : false;
                Lst_Datos.ItemsSource = Accesos;
                if (primeravez)
                    Lst_Datos.Items.Refresh();
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
        }

        private void RangoFechas_CambioFechaEvent(bool Cargando)
        {
            if (!Cargando)
                ActualizaDatos();
        }

    }
}
