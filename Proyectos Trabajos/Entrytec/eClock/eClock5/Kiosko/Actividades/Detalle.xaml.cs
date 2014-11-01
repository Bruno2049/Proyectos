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


using Newtonsoft.Json;
using eClock5.BaseModificada;
using eClock5;
using eClockBase;

namespace Kiosko.Actividades
{
    /// <summary>
    /// Lógica de interacción para Detalle.xaml
    /// </summary>
    public partial class Detalle : UserControl
    {
        int ActividadID = 0;
        public Detalle()
        {
            InitializeComponent();
        }

        private void UC_ToolBar_OnEventClickToolBar(UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Regresar":
                    this.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case "Btn_Siguiente":
                    Kiosko.Actividades.DetalleEdicion De = new Kiosko.Actividades.DetalleEdicion();
                    De.Actividad = ActividadActual;
                    De.Registrado += De_Registrado;
                    Kiosko.Generales.Main.MuestraComoDialogo(this, De, this.Background);
                    
                    break;

            }
        }
        public delegate void RegistradoArgs();
        public event RegistradoArgs Registrado;

        void De_Registrado()
        {
            this.Visibility = System.Windows.Visibility.Hidden;
            if (Registrado != null)
                Registrado();
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Sesion Se = new eClockBase.Controladores.Sesion(Sesion);
            Se.ObtenDatosEvent += Se_ObtenDatosEvent;
            eClockBase.Modelos.Actividades.Model_ACTIVIDAD Actividad = new eClockBase.Modelos.Actividades.Model_ACTIVIDAD();
            Actividad.ACTIVIDAD_ID = CeC.Convierte2Int(Tag);
            ActividadID = Actividad.ACTIVIDAD_ID;
            Se.ObtenDatos("EC_ACTIVIDADES", "ACTIVIDAD_ID", Actividad, "");


            eClockBase.Controladores.Actividades Actividades= new eClockBase.Controladores.Actividades(Sesion);
            Actividades.ObtenImagenFinalizado += delegate (byte[] Imagen)
                {
                    try
                    {
                        System.IO.MemoryStream MS = new System.IO.MemoryStream(Imagen);
                        BitmapImage bi = new BitmapImage();
                        bi.BeginInit();
                        bi.StreamSource = MS;
                        bi.EndInit();
                        Img_Foto.Source = null;
                        Img_Foto.Source = bi;
                    }
                    catch 
                    { }
                };
            Actividades.ObtenImagen(eClockBase.CeC.Convierte2Int(Actividad.ACTIVIDAD_ID));
        }
        eClockBase.Modelos.Actividades.Model_ACTIVIDAD ActividadActual = null;
        void Se_ObtenDatosEvent(int Resultado, string Datos)
        {
            
            try
            {
                if (Resultado > 0)
                {
                    ActividadActual = JsonConvert.DeserializeObject<eClockBase.Modelos.Actividades.Model_ACTIVIDAD>(Datos);
                    Lbl_Titulo.Text = ActividadActual.ACTIVIDAD_NOMBRE.ToString();
                    Lbl_FechaDesde.Text = ActividadActual.ACTIVIDAD_DESDE.ToShortDateString();
                    Lbl_FechaHasta.Text = ActividadActual.ACTIVIDAD_INSCRIPHASTA.ToShortDateString();
                    Lbl_Descripcion.Text = ActividadActual.ACTIVIDAD_DESCRIPCION.ToString();
                }
            }
            catch
            {
            }
        }




    }
}
