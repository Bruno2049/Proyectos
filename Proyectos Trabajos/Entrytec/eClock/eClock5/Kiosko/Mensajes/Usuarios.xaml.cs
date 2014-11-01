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
using System.ComponentModel;
using eClockBase.Controladores;
using System.Collections.ObjectModel;
using eClock5;
using eClockBase;

namespace Kiosko.Mensajes
{
    /// <summary>
    /// Lógica de interacción para Usuarios.xaml
    /// </summary>
    public partial class Usuarios : UserControl
    {

        public UserControl ParentControl { get; set; }

        public Usuarios()
        {
            InitializeComponent();
        }

        Detalles Dtl;
        Mensajes Msj;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            eClockBase.CeC_SesionBase Sesion = eClock5.CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Mails email = new eClockBase.Controladores.Mails(Sesion);
            email.ObtenContactosEvent += email_ObtenContactosEvent;
            email.ObtenContactos();
        }

        void email_ObtenContactosEvent(string Resultados)
        {
            Lst_Usuarios.CambiarListado(Resultados);
        }
        int UsuarioID = -1;
        private void Lst_Usuarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UsuarioID = CeC.Convierte2Int(Lst_Usuarios.Seleccionado.Llave);
            
            Dtl = new Detalles();
            Dtl.UsuarioID = UsuarioID;
            Dtl.Nombre = CeC.Convierte2String(Lst_Usuarios.Seleccionado.Nombre);
            Kiosko.Generales.Main.MuestraComoDialogo(this, Dtl, this.Background);
        }

    }
}
