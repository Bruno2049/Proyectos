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
    /// Lógica de interacción para UC_Exportar.xaml
    /// </summary>
    public partial class UC_Exportar : UserControl
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

        public UC_Exportar()
        {
            InitializeComponent();
        }

        public void Exportar()
        {
            try
            {
                //Lst_Datos.Items.Clear();
                eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);
                if (Tabla != null && Tabla != "")
                {
                    eClockBase.Controladores.Sesion CSesion = new eClockBase.Controladores.Sesion(Sesion);

                    if (DataContext != null)
                    {
                        CSesion.ObtenDatosEvent += CSesion_ObtenDatosEvent;
                        CSesion.ObtenDatos(Tabla, "", DataContext);
                    }
                    else
                    {
                        CSesion.CambioListadoEvent += CSesion_CambioListadoEvent;
                        if (Tabla != null && Tabla.Length > 0
                            && CampoLlave != null && CampoLlave.Length > 0
                            && CampoNombre != null && CampoNombre.Length > 0)
                            CSesion.ObtenListado(Tabla, CampoLlave, CampoNombre, CampoAdicional, CampoDescripcion, "", MostrarBorrados, Filtro);
                    }
                }
                else
                {
                    Guarda(JsonValores);
                }

            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }

        }

        void CSesion_ObtenDatosEvent(int Resultado, string Datos)
        {
            if(Resultado > 0)
                Guarda(Datos);
        }

        public bool Guarda(string JSon)
        {
            string ArchivoNombre = "";
            if (CeC.Convierte2Bool( Rbt_Json.IsChecked))
            {
                ArchivoNombre = Tabla + "_Exp.json";
                CeC_StreamFile.sNuevoTexto(ArchivoNombre, JSon);
            }
            if (CeC.Convierte2Bool(Rbt_Txt.IsChecked))
            {
                ArchivoNombre = Tabla + "_Exp.txt";
                CeC_StreamFile.sNuevoTexto(ArchivoNombre, CeC.Json2txt(JSon));
            }
            if (CeC.Convierte2Bool(Rbt_CSV.IsChecked))
            {
                ArchivoNombre = Tabla + "_Exp.csv";
                CeC_StreamFile.sNuevoTexto(ArchivoNombre, CeC.Json2Csv(JSon));
            }
            eClockBase.CeC_Stream.sEjecuta(ArchivoNombre);

            return true;
        }


        void CSesion_CambioListadoEvent(string Listado)
        {
            Guarda(Listado);

            /*Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            /*string Archivo
            if (eClock5.BaseModificada.CeC.Convierte2Bool( Rbt_Json.IsChecked))
            {
                dlg.DefaultExt = ".json";
                dlg.Filter = "Json Files (*.json)|*.json|All Files (*.*)|*.*";
            }
            if (eClock5.BaseModificada.CeC.Convierte2Bool(Rbt_Txt.IsChecked))
            {
                dlg.DefaultExt = ".Txt";
                dlg.Filter = "Txt Files (*.Txt)|*.Txt|All Files (*.*)|*.*";
            }
            if (eClock5.BaseModificada.CeC.Convierte2Bool(Rbt_CSV.IsChecked))
            {
                dlg.DefaultExt = ".CSV";
                dlg.Filter = "CSV Files (*.CSV)|*.CSV|All Files (*.*)|*.*";
            }*/

        }
        private void Tb_General_OnEventClickToolBar(UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Regresar":
                    this.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case "Btn_Guardar":
                    Exportar();
                    break;
            }
        }
    }
}
