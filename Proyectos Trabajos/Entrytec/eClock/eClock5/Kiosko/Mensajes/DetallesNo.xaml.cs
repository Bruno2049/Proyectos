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
    /// Lógica de interacción para Detalles.xaml
    /// </summary>
    public partial class DetallesNo : UserControl
    {
        eClockBase.CeC_SesionBase m_Sesion = null;

        public UserControl ParentControl { get; set; }

        public DetallesNo()
        {
            InitializeComponent();
        }

        Mensajes Msj;
        private void ToolBar_OnEventClickToolBar(eClock5.UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                //case "Btn_Regresar":
                //    eClock5.Clases.Parametros.ObtenPadre(this).Visibility = Visibility.Hidden;
                //    break;
                //case "Btn_Nuevo":
                //    Kiosko.Generales.Main.MuestraComoDialogo(Lst_Chats, new Usuarios(), this.Background);
                //    break;

            }
        }

        public int UsuarioID = 0;
        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            
            //Obtiene parametros especificados por la vista Anterior y almacenados en la propiedad Tag del objeto (control) actual
            //En este caso Solicitudes\Detalles.
            eClock5.Clases.Parametros Parametros = eClock5.Clases.Parametros.Tag2Parametros(this.Tag);
            //Creamos una variable "TipoTramite" del tipo "eClockBase.Modelos.Model_TIPO_TRAMITE",....
            ///..Despues del signo igual(=) Convertimos la variable "Parametros.Parametro" de tipo OBJECT a STRING porque asumimos
            ///que este es un Jsone, debido a que esta ventana (Detalles) fue llamada desde UCListado y este control al darle click a un 
            ///..elemento del listado crea un objeto json y lo manda de parametro en TAG
            ///..posteriormente este parametro TAG, se deserializa en tipo eClockBase.Modelos.Model_TIPO_TRAMITE,
            ///..para ser alamcenado dentro de la variable "TipoTramite" que es del mismo Tipo.
            eClockBase.Modelos.Model_USUARIOS Usuario = JsonConvert.DeserializeObject<eClockBase.Modelos.Model_USUARIOS>(Parametros.Parametro.ToString());
            UsuarioID = Usuario.USUARIO_ID;

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

        private void ToolBar_BotonesCreados(object sender)
        {

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

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (Timer != null)
            {
                Timer.Stop();
                Timer = null;
            }
        }
    }
}
