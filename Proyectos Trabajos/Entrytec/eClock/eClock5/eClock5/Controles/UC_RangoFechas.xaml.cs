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

namespace eClock5.Controles
{
    /// <summary>
    /// Lógica de interacción para UC_RangoFechas.xaml
    /// </summary>
    public partial class UC_RangoFechas : UserControl
    {
        static double Dias = 6;
        public static DateTime s_FechaFin = DateTime.Now.Date.AddDays(-1);
        public static DateTime s_FechaInicio = s_FechaFin.AddDays(-Dias);

        DateTime m_FechaFin;
        public DateTime FechaFin
        {
            get { return eClockBase.CeC.Convierte2DateTime(Wdp_Fin.SelectedDate); }
            set { Wdp_Fin.SelectedDate = m_FechaFin = s_FechaFin = value; }
        }
        DateTime m_FechaInicio;
        public DateTime FechaInicio
        {
            get { return eClockBase.CeC.Convierte2DateTime(Wdp_Inicio.SelectedDate); }
            set { Wdp_Inicio.SelectedDate = m_FechaInicio = s_FechaInicio = value; }
        }

        public UC_RangoFechas()
        {
            InitializeComponent();
            CambioFechaEvent += UC_RangoFechas_CambioFechaEvent;
        }

        void UC_RangoFechas_CambioFechaEvent(bool Cargando)
        {
            Dias = (m_FechaFin - m_FechaInicio).Days;
        }
        string Parametros = "-1";
        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (eClockBase.CeC.Convierte2Bool(e.NewValue) == true)
            {
                bool Cambio = false;


                if (m_FechaInicio != s_FechaInicio)
                {
                    FechaInicio = s_FechaInicio;
                    Cambio = true;
                }

                if (m_FechaFin != s_FechaFin)
                {
                    FechaFin = s_FechaFin;
                    Cambio = true;
                }
                try
                {
                    string NParametro = eClockBase.CeC.Convierte2String(Clases.Parametros.ObtenParametrosPadre(this).Parametro);
                    if (Parametros != NParametro)
                    {
                        Parametros = NParametro;
                        CambioFechaEvent(false);
                        return;
                    }
                }
                catch { 
                    CambioFechaEvent(false); 
                }

                if (Cambio && CambioFechaEvent != null)
                    CambioFechaEvent(true);

            }
        }

        public delegate void CambioFechaArgs(bool Cargando);
        public event CambioFechaArgs CambioFechaEvent;

        private void Wdp_Inicio_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (m_FechaInicio == FechaInicio)
                return;
            FechaInicio = FechaInicio;
            FechaFin = FechaInicio.AddDays(Dias);
            if (CambioFechaEvent != null)
                CambioFechaEvent(false);
        }

        private void Wdp_Fin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (m_FechaFin == FechaFin)
                return;

            FechaFin = FechaFin;
            if (CambioFechaEvent != null)
                CambioFechaEvent(false);

        }
    }
}
