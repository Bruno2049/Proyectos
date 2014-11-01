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
using System.Windows.Threading;

namespace Kiosko.Mensajes
{


    /// <summary>
    /// Lógica de interacción para Prueba.xaml
    /// </summary>
    public partial class Detalles : UserControl
    {
        eClockBase.CeC_SesionBase m_Sesion = null;

        public UserControl ParentControl { get; set; }
        public Detalles()
        {
            InitializeComponent();
        }
        public string Nombre = "";
        Detalles Dtl;
        Mensajes Msj;
        public void ToolBar_OnEventClickToolBar(eClock5.UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Regresar":
                    //Se puede usar esta linea de codigo...
                    //this.Visibility = System.Windows.Visibility.Hidden;
                    //o esta otra linea de codigo
                    (this.Parent as Grid).Children.Remove(this);
                    
                    break;
                case "Btn_Nuevo":

                    break;
                case "Btn_Borrar":
                    if (UsuarioID > 0)
                    {
                        eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);
                        eClockBase.Controladores.Mails Mails = new eClockBase.Controladores.Mails(Sesion);
                        Mails.BorraMensajesConEvent += Mails_BorraMensajesConEvent;
                        Mails.BorraMensajesCon(UsuarioID);
                        //(this.Parent as Grid).Children.Remove(this);
                    }
                    else
                    {
                        Controles.UC_MessageBoxFullScreen Msg = new Controles.UC_MessageBoxFullScreen();
                        Msg.Mensaje = "Debe Seleccionar los mensajes a borrar";
                        Msg.Mostrar(this);

                    }
                    break;

            }
        }
        bool Cerrar = false;
        void Mails_BorraMensajesConEvent(string Resultados)
        {
            Controles.UC_MessageBoxFullScreen Msg = new Controles.UC_MessageBoxFullScreen();
            if (Resultados != "")
            {
                Msg.Mensaje = "Mensajes Borrados";

                Cerrar = true;
            }
            else
            {
                Msg.Mensaje = "Error al Borrar Mensajes";
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
            }
        }



        public int UsuarioID = 0;
        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            ToolBar.Titulo =  Nombre;
            int Seleccionado = eClockBase.CeC.Convierte2Int(UsuarioID);
            m_Sesion = eClock5.CeC_Sesion.ObtenSesion(this);

            ///Asigna el control que tendra el foco cuando cargue la pantalla
            Teclado.FocoPrincipal = Tbx_Mensaje;
            ///Liga los controles que tendr'an tecla enter en vez de tabulador
            Teclado.ControlesEnter.Add(Tbx_Mensaje);
            ///Elige el boton que se ejecutar'a cuando click en enter
            Teclado.BotonEnter = Btn_Enviar;

            Actualiza();
        }

        public void Actualiza()
        {
            eClockBase.Controladores.Mails email = new eClockBase.Controladores.Mails(m_Sesion);
            email.ObtenChatsConEvent += email_ObtenChatsConEvent;
            email.ObtenChatsCon(UsuarioID);
        }
        DispatcherTimer Timer = new DispatcherTimer();
        void email_ObtenChatsConEvent(string Resultados)
        {
            Lst_Chats.CambiarListado(Resultados);
            if (Lst_Chats.Datos.Count > 0)
                Lst_Chats.Lst_Datos.ScrollIntoView(Lst_Chats.Datos.Last());
            Timer.Stop();
            Timer.Tick -= Timer_Tick;
            Timer.Tick += Timer_Tick;
            Timer.Interval = new TimeSpan(0, 0, 5);
            Timer.Start();
        }
        bool Actualizando = false;
        void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Actualizando)
                    return;


                Actualizando = true;
                Timer.Stop();
                int Valor = 0;
                if (Lst_Chats.Datos.Count > 0)
                    Valor = CeC.Convierte2Int(Lst_Chats.Datos.Last().Llave);
                eClockBase.Controladores.Mails email = new eClockBase.Controladores.Mails(m_Sesion);
                email.ObtenChatsConDesdeEvent += email_ObtenChatsConDesdeEvent;
                email.ObtenChatsConDesde(UsuarioID, Valor);
            }
            catch { }
        }

        void email_ObtenChatsConDesdeEvent(string Resultados)
        {
            try
            {
                List<UC_Listado.Listado> NuevosMensajes = JsonConvert.DeserializeObject<List<UC_Listado.Listado>>(Resultados);
                Lst_Chats.Datos.AddRange(NuevosMensajes);
                Lst_Chats.Lst_Datos.ItemsSource = Lst_Chats.Datos;
                Lst_Chats.Lst_Datos.Items.Refresh();
                Lst_Chats.Lst_Datos.ScrollIntoView(Lst_Chats.Datos.Last());
            }
            catch { }
            Actualizando = false;
            Timer.Start();
        }

        private void Btn_Enviar_Click(object sender, RoutedEventArgs e)
        {
            if (Tbx_Mensaje.Text == "")
                return;
            eClockBase.Controladores.Mails emails = new eClockBase.Controladores.Mails(m_Sesion);
            emails.EnviaMensajeEvent += emails_EnviaMensajeEvent;
            emails.EnviaMensaje(UsuarioID, Tbx_Mensaje.Text);
            Tbx_Mensaje.Text = "";
        }

        void emails_EnviaMensajeEvent(bool Resultados)
        {
            if (Resultados)
            {
                m_Sesion.MuestraMensaje("Mensaje Enviado", 3);

                Timer_Tick(null, null);
            }
        }

        private void UserControl_Unloaded_1(object sender, RoutedEventArgs e)
        {
            if (Timer != null)
            {
                Timer.Stop();
                Timer = null;
            }
        }

    }
}
