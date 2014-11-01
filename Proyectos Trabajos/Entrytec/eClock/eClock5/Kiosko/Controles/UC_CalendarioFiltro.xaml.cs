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
    /// Lógica de interacción para UC_CalendarioFiltro.xaml
    /// </summary>
    public partial class UC_CalendarioFiltro : UserControl
    {
        private DateTime m_MesDesde;

        public DateTime MesDesde
        {
            get { return m_MesDesde; }
            set
            {
                if (m_MesDesde == value)
                    return;
                m_MesDesde = value;

                Mes1.Mes = m_MesDesde;
                Mes2.Mes = m_MesDesde.AddMonths(1);
            }
        }

        public DateTime FechaDesde;
        public DateTime FechaHasta;
        public UC_CalendarioFiltro()
        {
            InitializeComponent();
        }

        private void UC_Mes_MesChanged(UC_Mes ControlMes)
        {
            if (ControlMes == Mes1)
                MesDesde = ControlMes.Mes;
            if (ControlMes == Mes2)
                MesDesde = ControlMes.Mes.AddMonths(-1);

        }
        public void AsignaFechasDH(DateTime Desde, DateTime Hasta)
        {
            FechaDesde = Desde.Date;
            FechaHasta = Hasta.Date;

            AsignaFechas(Desde, Hasta);
        }
        private void AsignaFechas(DateTime Desde, DateTime Hasta)
        {
            List<UC_Mes.DiasColorClass> DiasColor = new List<UC_Mes.DiasColorClass>();
            if (Hasta == null || Hasta == new DateTime())
                Hasta = Desde;
            if (Hasta < Desde)
            {
                DateTime FTemp = Hasta;
                Hasta = Desde;
                Desde = FTemp;
            }

            while (Desde <= Hasta)
            {
                UC_Mes.DiasColorClass DC = new UC_Mes.DiasColorClass();
                DC.Dia = Desde;
                DC.Color = Colors.BlueViolet;
                DiasColor.Add(DC);
                Desde = Desde.AddDays(1);
            }
            Mes1.ActualizaDiasColor(DiasColor);
            Mes2.ActualizaDiasColor(DiasColor);
        }

        private void UC_Mes_SeleccionadoChanged(UC_Mes ControlMes)
        {
            
            if (FechaDesde == null || FechaDesde == new DateTime())
            {
                FechaDesde = ControlMes.DiaSeleccionado;
            }
            else
                if (FechaHasta == null || FechaHasta == new DateTime())
                {
                    FechaHasta = ControlMes.DiaSeleccionado;                    
                }
                else
                {
                    FechaDesde = ControlMes.DiaSeleccionado;
                    FechaHasta = new DateTime();
                }
            AsignaFechas(FechaDesde, FechaHasta);
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (m_MesDesde == null)
            {
                m_MesDesde = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).AddMonths(-1);

            }
        }
    }
}
