using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace eClock5.Vista.Configuracion
{
    /// <summary>
    /// Lógica de interacción para Nomina.xaml
    /// </summary>
    public partial class Nomina : UserControl
    {
        public Nomina()
        {
            InitializeComponent();
        }

        private void UC_ToolBar_OnEventClickToolBar(UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Guardar":
                    if (Interfaz.Parametros == null)
                        Interfaz.Parametros = new eClockBase.Modelos.Nomina.Model_Interfaz();
                    Interfaz.Parametros.SISTEMA_NOMINA_ID = Cmb_SistemaNominaID.SeleccionadoInt;
                    Interfaz.Parametros.CadenaConexion = Tbx_CadenaConexion.Text;
                    Interfaz.Parametros.ActualizarEmpleados = eClockBase.CeC.Convierte2Bool(Chb_ActualizarEmpleados.IsChecked);
                    Interfaz.Parametros.ImportarIncidencias = eClockBase.CeC.Convierte2Bool(Chb_ImportarIncidencias.IsChecked);
                    Interfaz.Parametros.ImportarNomina = eClockBase.CeC.Convierte2Bool(Chb_ImportarNomina.IsChecked);
                    Interfaz.Parametros.ImportarTiposIncidencias = eClockBase.CeC.Convierte2Bool(Chb_ImportarTiposIncidencias.IsChecked);
                    Interfaz.Parametros.ExportarIncidencias = eClockBase.CeC.Convierte2Bool(Chb_ExportarIncidencias.IsChecked);
                    Interfaz.GuardaParametros();
                    break;
            }
        }

        Clases.Interfaces.Interfaz Interfaz = null;


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Interfaz = new Clases.Interfaces.Interfaz();
            Interfaz.CargaParametrosEvent += Interfaz_CargaParametrosEvent;
            Interfaz.CargaParametros(this);
        }

        void Interfaz_CargaParametrosEvent(bool Cargados)
        {

            Cmb_SistemaNominaID.SeleccionadoInt = Interfaz.Parametros.SISTEMA_NOMINA_ID;
            Tbx_CadenaConexion.Text = Interfaz.Parametros.CadenaConexion;
            Chb_ActualizarEmpleados.IsChecked = Interfaz.Parametros.ActualizarEmpleados;
            Chb_ImportarIncidencias.IsChecked = Interfaz.Parametros.ImportarIncidencias;
            Chb_ImportarNomina.IsChecked = Interfaz.Parametros.ImportarNomina;
            Chb_ImportarTiposIncidencias.IsChecked = Interfaz.Parametros.ImportarTiposIncidencias;
            Chb_ExportarIncidencias.IsChecked = Interfaz.Parametros.ExportarIncidencias;
        }
    }
}
