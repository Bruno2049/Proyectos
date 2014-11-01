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

namespace Kiosko.Controles
{
    /// <summary>
    /// Lógica de interacción para UC_CalendarioFiltroDlg.xaml
    /// </summary>
    public partial class UC_CalendarioFiltroDlg : UserControl
    {

        public string Titulo
        {
            get
            {
                return Lbl_Titulo.Text;
            }
            set
            {
                Lbl_Titulo.Text = value;
            }
        }
        public DateTime MesDesde
        {
            get { return Calendario.MesDesde; }
            set { Calendario.MesDesde = new DateTime(value.Year,value.Month,1); }
        }
        public DateTime FechaDesde
        {
            get { return Calendario.FechaDesde; }
            set { Calendario.FechaDesde = value.Date; }
        }
        public DateTime FechaHasta
        {
            get { return Calendario.FechaHasta; }
            set { Calendario.FechaHasta = value.Date; }
        }
        public UC_CalendarioFiltroDlg()
        {
            InitializeComponent();
        }

        public void AsignaFechas(DateTime Desde, DateTime Hasta)
        {
            Calendario.AsignaFechasDH(Desde.Date, Hasta.Date);
        }
        public event RoutedEventHandler ListoClick;
        private void Btn_Listo_Click(object sender, RoutedEventArgs e)
        {
            if (ListoClick != null)
                ListoClick(sender, e);
        }
        public event RoutedEventHandler CancelarClick;
        private void Btn_Cancelar_Click(object sender, RoutedEventArgs e)
        {
            if (CancelarClick != null)
                CancelarClick(sender, e);
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {

            Btn_Listo.ColorFondo = Colors.White;
            Btn_Listo.ColorFont = Colors.Black;
            Btn_Cancelar.ColorFondo = Colors.White;
            Btn_Cancelar.ColorFont = Colors.Black;
        }

              
        
    }
}
