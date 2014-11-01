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
namespace eClock5.Vista.Empleados
{
    /// <summary>
    /// Lógica de interacción para ListadoPersonas.xaml
    /// </summary>
    public partial class ListadoPersonas : UserControl
    {
        public ListadoPersonas()
        {
            InitializeComponent();
        }
        string Agrupacion = "";
        private void UC_Listado_CambioListadoEvent(UC_Listado Listado)
        {


        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            string Parametros = eClockBase.CeC.Convierte2String(Clases.Parametros.Tag2Parametros(Tag).Parametro);
            if (Parametros == "")
                Agrupacion = "";
            else
                if (Parametros[0] == '|' || Parametros[0] == '@')
                    Agrupacion = Parametros;
            string NuevoFiltro = "AGRUPACION_NOMBRE LIKE '" + Agrupacion + "%'";
            bool Actualizar = false;
            if (Lst_Main.Filtro != null && Lst_Main.Filtro != "" && Lst_Main.Filtro != NuevoFiltro)
                Actualizar = true;
            Lst_Main.Filtro = NuevoFiltro;
            if (Actualizar)
                Lst_Main.ActualizaDatos();
        }
    }
}
