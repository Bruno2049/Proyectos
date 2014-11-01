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
    /// Lógica de interacción para UC_RangoFechas.xaml
    /// </summary>
    public partial class UC_RangoFechas : UserControl
    {
        private DateTime m_FechaInicial;

        public DateTime FechaInicial
        {
            get { return m_FechaInicial; }
            set
            {
                m_FechaInicial = value.Date;
                Lbl_Desde.Text = m_FechaInicial.ToShortDateString();
               
            }
        }
        private DateTime m_FechaFinal;

        public DateTime FechaFinal
        {
            get { return m_FechaFinal; }
            set
            {
                m_FechaFinal = value.Date;
                Lbl_Hasta.Text = m_FechaFinal.ToShortDateString();
                
            }
        }

        public UC_RangoFechas()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler Click;

        public event RoutedEventHandler CambioFechas;

        private void Btn_Main_Click(object sender, RoutedEventArgs e)
        {
            Calendario.MesDesde = FechaFinal;
            Calendario.AsignaFechas(FechaInicial, FechaFinal);
            if (Click != null)
                Click(sender, e);

        }

        private void Btn_Anterior_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan Diferencia = FechaFinal - FechaInicial;
            FechaFinal = FechaInicial.AddDays(-1);
            FechaInicial = FechaFinal - Diferencia;
            ActualizaFechas();
        }
        private void ActualizaFechas()
        {

            if (CambioFechas != null)
                CambioFechas(this, null);
        }
        private void Btn_Siguiente_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan Diferencia = FechaFinal - FechaInicial;
            FechaInicial = FechaFinal.AddDays(1);
            FechaFinal = FechaInicial + Diferencia;
            ActualizaFechas();

        }

        private void Calendario_ListoClick(object sender, RoutedEventArgs e)
        {
            FechaInicial = Calendario.FechaDesde;
            FechaFinal = Calendario.FechaHasta;
            ActualizaFechas();
            Btn_Main.IsChecked = false;
        }

        private void Calendario_CancelarClick(object sender, RoutedEventArgs e)
        {
            Btn_Main.IsChecked = false;
        }

        


    }
}
