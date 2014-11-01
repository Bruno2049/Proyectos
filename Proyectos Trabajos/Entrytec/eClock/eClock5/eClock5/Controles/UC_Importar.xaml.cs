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
using eClock5.BaseModificada;

namespace eClock5.Controles
{
    /// <summary>
    /// Lógica de interacción para UC_Importar.xaml
    /// </summary>
    public partial class UC_Importar : UserControl
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


#if !NETFX_CORE
        [Description("Indica si mostrará borrados."), Category("Datos")]
#endif
        public bool MostrarBorrados { get; set; }


        public string JsonValores { get; set; }

        public UC_Importar()
        {
            InitializeComponent();
        }

        private void Btn_Archivo_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".json";
            dlg.Filter = "Json files (.json)|*.json|All files (*.*)|*.*";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                Tbx_Archivo.Text = dlg.FileName;
            }
        }

        private void Tb_General_OnEventClickToolBar(UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Regresar":
                    this.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case "Btn_Guardar":
                    Importar();
                    break;
            }
        }

        public delegate void ImportarArgs(string Datos);
        public event ImportarArgs ImportarEvent;

        eClockBase.CeC_SesionBase Sesion = null;
        private void Importar()
        {
            try
            {
                Sesion = CeC_Sesion.ObtenSesion(this);
                JsonValores = System.IO.File.ReadAllText(Tbx_Archivo.Text);
                if (Tabla != null && Tabla != "")
                {
                    eClockBase.Controladores.Sesion CSesion = new eClockBase.Controladores.Sesion(Sesion);
                    CSesion.GuardaDatosEvent += CSesion_GuardaDatosEvent;
                    if (JsonValores[0] != '[')
                        JsonValores = "[" + JsonValores + "]";
                    CSesion.GuardaDatos(Tabla, CampoLlave, JsonValores, false);
                }
                else
                    if (ImportarEvent != null)
                        ImportarEvent(JsonValores);
                return;
            }
            catch { }
            Sesion.MuestraMensaje("No se pudo Importar", 5);
        }

        void CSesion_GuardaDatosEvent(int Guardados)
        {
            if (Guardados > 0)
                Sesion.MuestraMensaje("Se importaron " + Guardados + " Registros", 5);
            else
                Sesion.MuestraMensaje("No se importaron registros", 5);
        }

        private void Tbx_Archivo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Tbx_Archivo.Text != "")
                Tb_General.Seleccionados = 1;
            else
                Tb_General.Seleccionados = 0;
        }
    }
}
