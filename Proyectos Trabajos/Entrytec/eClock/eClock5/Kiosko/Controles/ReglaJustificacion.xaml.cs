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

namespace eClock5.Vista.Asistencias
{
    /// <summary>
    /// Lógica de interacción para ReglaJustificacion.xaml
    /// </summary>
    public partial class ReglaJustificacion : UserControl
    {
        public ReglaJustificacion()
        {
            InitializeComponent();
        }
        public bool ActualizaSaldo(decimal AConsumir)
        {
            if (Status != null)
            {
                Status.AConsumir = AConsumir;
                Lbl_AConsumir.Text = AConsumir.ToString();
                decimal SaldoFinal = Status.Saldo - AConsumir;
                Lbl_SaldoFinal.Text = eClockBase.CeC.Convierte2String(SaldoFinal);
                if (SaldoFinal < Status.ValorMinimo)
                    Lbl_SinSaldo.Visibility = System.Windows.Visibility.Visible;
                else
                {
                    Lbl_SinSaldo.Visibility = System.Windows.Visibility.Collapsed;
                    return true;
                }
            }
            return false;
        }

        public bool TieneSaldo()
        {
            decimal SaldoFinal = Status.Saldo - Status.AConsumir;
            if (SaldoFinal < Status.ValorMinimo)
                return false;
            return true;
        }

        public eClockBase.Modelos.Incidencias.Model_StatusRegla Status = null;
        public bool Carga(eClockBase.Modelos.Incidencias.Model_StatusRegla sStatus, string PersonasDiariosIDs)
        {
            Status = sStatus;
            Lbl_PersonaLink.Text = Status.PersonaLinkID.ToString();
            Lbl_PersonaNombre.Text = Status.PersonaNombre;
            Lbl_Fechas.Text = eClockBase.CeC.ObtenDiasTexto(eClockBase.CeC.PersonasDiarioIDs2Fechas(PersonasDiariosIDs, Status.PersonaID));
            if (Status.Inventario)
            {
                Stp_Saldo.Visibility = System.Windows.Visibility.Visible;
                Lbl_FechaDesde.Text = Status.FechaDesde.ToShortDateString();
                Lbl_Saldo.Text = Status.Saldo.ToString();
                ActualizaSaldo(Status.AConsumir);
            }
            else
            {
                Stp_Saldo.Visibility = System.Windows.Visibility.Collapsed;
            }
            return true;
        }
    }
}
