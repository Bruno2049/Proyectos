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
    /// Lógica de interacción para DetalleEdicion.xaml
    /// </summary>
    public partial class DetalleEdicion : UserControl
    {
        public eClockBase.Modelos.Actividades.Model_ACTIVIDAD Actividad = null;

        public DetalleEdicion()
        {
            InitializeComponent();
        }

        private void ToolBar_OnEventClickToolBar(eClock5.UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Regresar":
                    this.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case "Btn_Siguiente":
                    eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);
                    eClockBase.Controladores.Actividades Ads = new eClockBase.Controladores.Actividades(Sesion);
                    Ads.IncribirseEvent += Ads_IncribirseEvent;
                    Ads.Incribirse(Actividad.ACTIVIDAD_ID, Sesion.Mdl_Sesion.PERSONA_ID, 1, Campos.Vista2JSon());
                    //eClockBase.Modelos.Model_ACTIVIDAD_INS Modelo = new eClockBase.Modelos.Model_ACTIVIDAD_INS();
                    //Modelo.ACTIVIDAD_INS_ID = -1;
                    //Modelo.ACTIVIDAD_ID = Actividad.ACTIVIDAD_ID;
                    //Modelo.PERSONA_ID = Sesion.Mdl_Sesion.PERSONA_ID;
                    //Modelo.TIPO_INSCRIPCION_ID = 1;
                    //Modelo.ACTIVIDAD_INS_FECHA = System.DateTime.Now;
                    //Modelo.ACTIVIDAD_INS_DESCRIPCION = Campos.Vista2JSon();
                    //Se.GuardaDatosEvent += Se_GuardaDatosEvent;
                    //string DatosModelo = JsonConvert.SerializeObject(Modelo);
                    //Se.GuardaDatos("EC_ACTIVIDAD_INS", "ACTIVIDAD_INS_ID", DatosModelo, true);
                    break;
            }
        }
        public delegate void RegistradoArgs();
        public event RegistradoArgs Registrado;


        bool Cerrar = false;
        void Ads_IncribirseEvent(int Resultado)
        {
            Controles.UC_MessageBoxFullScreen Msg = new Controles.UC_MessageBoxFullScreen();
            if (Resultado > 0)
            {
                Msg.Mensaje = "Inscripción satisfactoria";

                Cerrar = true;
            }
            else
            {
                Msg.Mensaje = "Error en la inscripción";
                Msg.Imagen = null;
                Cerrar = false;
            }
            Msg.Cerrado += Msg_Cerrado;
            Msg.Mostrar(this);
        }


        void Msg_Cerrado()
        {
            if (Cerrar)
            {
                this.Visibility = System.Windows.Visibility.Hidden;
                if (Registrado != null)
                    Registrado();
            }
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            Lbl_Titulo.Text = Actividad.ACTIVIDAD_NOMBRE;
            Campos.Asigna(Actividad.ACTIVIDAD_CAMPOS, "");
            if (Campos.NoCampos > 0)
                Teclado.Visibility = System.Windows.Visibility.Visible;
            else
                Teclado.Visibility = System.Windows.Visibility.Collapsed;
            eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);            

            eClockBase.Controladores.Actividades Actividades = new eClockBase.Controladores.Actividades(Sesion);
            Actividades.ObtenImagenFinalizado += delegate(byte[] Imagen)
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


    }
}
