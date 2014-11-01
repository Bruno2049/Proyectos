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
using eClock5;
using eClockBase;

namespace Kiosko.Mensajes
{
    /// <summary>
    /// Lógica de interacción para Mensajes.xaml
    /// </summary>
    public partial class Mensajes : UserControl
    {
        public Mensajes()
        {
            InitializeComponent();
        }


        Usuarios MesgNuevo;
        Detalles Dtl;
        Mensajes Msj;
        public void ToolBar_OnEventClickToolBar(eClock5.UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Regresar":
                    Kiosko.Mensajes.Usuarios Usr = new Usuarios();

                    if (Lst_Usuarios.IsVisible == true)
                    {
                        Lst_Usuarios.Visibility = System.Windows.Visibility.Hidden;
                        Lst_Usuarios.IsVisible = false;
                        Lst_Chats.IsVisible = true;
                        Lst_Chats.IsEnabled = true;
                    }
                    else
                        if (Lst_Chats.IsVisible == true)
                        {
                            this.Visibility = System.Windows.Visibility.Hidden;
                        }

                    //Estas lineas las borre el 06/12/2013 ya que agregue detalles en un dialogo
                    //if (Lst_Chats.ControlEdicion.IsVisible == true)
                    //{
                    //    Lst_Chats.ControlEdicion.Visibility = System.Windows.Visibility.Hidden;
                    //}06/12/2013

                    //else if (Lst_Chats.FindName("Grid").ToString() == "System.Windows.Controls.Grid")
                    //{
                    //    ((Panel)((UserControl)Usr.Lst_Usuarios).Parent).Children.Remove((UserControl)Usr.Lst_Usuarios);
                    //}

                    //Estas lineas las borre el 06/12/2013 ya que agregue detalles en un dialogo
                    //else
                    //{06/12/2013
                        //((Panel)((UserControl)Lst_Chats).Parent).Children.Remove((UserControl)Usr.Lst_Usuarios);
                        if (MesgNuevo != null)
                        {
                            if (MesgNuevo.IsVisible == true)
                            {
                                MesgNuevo.Visibility = System.Windows.Visibility.Hidden;
                                MesgNuevo = null;
                            } 
                        }
                        //else
                        //    this.Visibility = System.Windows.Visibility.Hidden;
                    //}

                    break;
                case "Btn_Nuevo":
                    Lst_Chats.IsEnabled = false;
                    if (Lst_Chats.IsEnabled == true)
                    {
                        Cht();
                    }
                    else
                    {
                        Cts();
                    }
                    Lst_Usuarios.Visibility = System.Windows.Visibility.Visible;
                    Lst_Usuarios.IsVisible = true;
                    //MesgNuevo = new Usuarios();
                    //Kiosko.Generales.Main.MuestraComoDialogo(Lst_Chats, MesgNuevo, this.Background);
                    break;
                case "Btn_Borrar":
                    if (UsuarioID > 0)
                    {
                        eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);
                        eClockBase.Controladores.Mails Mails = new eClockBase.Controladores.Mails(Sesion);
                        Mails.BorraMensajesConEvent += Mails_BorraMensajesConEvent;
                        Mails.BorraMensajesCon(UsuarioID);
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

        public void Cht()
        {
            eClockBase.CeC_SesionBase Sesion = eClock5.CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Mails email = new eClockBase.Controladores.Mails(Sesion);
            email.ObtenChatsEvent += email_ObtenChatsEvent;
            email.ObtenChats();
        }

        public void Cts()
        {
            eClockBase.CeC_SesionBase Sesion = eClock5.CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Mails email = new eClockBase.Controladores.Mails(Sesion);
            email.ObtenContactosEvent += email_ObtenContactosEvent;
            email.ObtenContactos();
        }


        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            Lst_Usuarios.Visibility = System.Windows.Visibility.Hidden;
            Lst_Chats.IsVisible = true;
            if (Lst_Chats.IsEnabled == true)
            {
                Cht();
            }
            else
            {
                Cts();
            }
        }

        void email_ObtenContactosEvent(string Resultados)
        {
            Lst_Usuarios.CambiarListado(Resultados);
        }

        void email_ObtenChatsEvent(string Resultados)
        {
            Lst_Chats.CambiarListado(Resultados);
        }

        int UsuarioID = -1;
        private void Lst_Chats_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Lst_Chats.Seleccionado != null)
            {
                UsuarioID = CeC.Convierte2Int(Lst_Chats.Seleccionado.Llave);
                Dtl = new Detalles();
                Dtl.UsuarioID = UsuarioID;
                Dtl.Nombre = CeC.Convierte2String(Lst_Chats.Seleccionado.Descripcion);
                Dtl.IsVisibleChanged += Dtl_IsVisibleChanged;
                Kiosko.Generales.Main.MuestraComoDialogo(this, Dtl, this.Background);
            }
        }

        void Dtl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (CeC.Convierte2Bool(e.OldValue) == true)
            {
                Cht();
            }
        }

        private void Lst_Usuarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Lst_Usuarios.Seleccionado != null)
            {
                UsuarioID = CeC.Convierte2Int(Lst_Usuarios.Seleccionado.Llave);
                Dtl = new Detalles();
                Dtl.UsuarioID = UsuarioID;
                Dtl.Nombre = CeC.Convierte2String(Lst_Usuarios.Seleccionado.Nombre);
                Kiosko.Generales.Main.MuestraComoDialogo(this, Dtl, this.Background);
            }
        }
    }
}
