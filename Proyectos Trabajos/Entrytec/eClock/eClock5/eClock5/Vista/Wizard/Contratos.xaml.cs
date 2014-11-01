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
namespace eClock5.Vista.Wizard
{
    /// <summary>
    /// Lógica de interacción para Contratos.xaml
    /// </summary>
    public partial class Contratos : UserControl
    {
        public Contratos()
        {
            InitializeComponent();
            IsVisibleChanged += Contratos_IsVisibleChanged;
            Chb_Acepto.Checked += Chb_Acepto_Checked;
            Chb_Acepto.Unchecked += Chb_Acepto_Unchecked;
        }

        void Chb_Acepto_Unchecked(object sender, RoutedEventArgs e)
        {
            ActualizaBotones();
        }

        void Chb_Acepto_Checked(object sender, RoutedEventArgs e)
        {
            ActualizaBotones();
        }
        void ActualizaBotones()
        {
            Controles.UC_Wizard.sMostrarBotones(true, Chb_Acepto.IsChecked == true ? true : false, true);
        }
            
        void Contratos_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ActualizaBotones();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            System.IO.StreamReader SrAcuerdo =  CeC_StreamFile.sLeerTexto("..\\Resources\\Acuerdo.rtf");
            Rtb_Acuerdo.Selection.Load(SrAcuerdo.BaseStream, System.Windows.DataFormats.Rtf);
            System.IO.StreamReader SrLicencia = CeC_StreamFile.sLeerTexto("..\\Resources\\Licencia.rtf");
            Rtb_Licencia.Selection.Load(SrLicencia.BaseStream, System.Windows.DataFormats.Rtf);

        }
    }
}
