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

using eClockBase;
using eClock5;
using eClock5.BaseModificada;

namespace Kiosko.Asistencias
{
    /// <summary>
    /// Lógica de interacción para SaldosIncidencias.xaml
    /// </summary>
    public partial class SaldosIncidencias : UserControl
    {
        CeC_SesionBase Sesion = null;
        public SaldosIncidencias()
        {
            InitializeComponent();
        }
        private void ToolBar_OnEventClickToolBar(eClock5.UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Siguiente":
                    break;
                case "Btn_Regresar":
                    Close();
                    break;

            }
        }
        private void Close()
        {
            this.Visibility = System.Windows.Visibility.Hidden;
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            SaldoIncidencias();
        }

        public void SaldoIncidencias()
        {
            
            if (Sesion == null)
                Sesion = CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Incidencias Incidencias = new eClockBase.Controladores.Incidencias(Sesion);
            Incidencias.ObtenerSaldosFinalizado += Incidencias_ObtenerSaldosFinalizado;
            Incidencias.ObtenerSaldos(Sesion.Mdl_Sesion.PERSONA_ID, "", "");
       
        }

        void Incidencias_ObtenerSaldosFinalizado(List<eClockBase.Modelos.Incidencias.Model_SaldosTiposIncidenciasR> Saldos)
        {
            if (Saldos != null)
            {
                List<eClockBase.Controladores.ListadoJson> Listado = new List<eClockBase.Controladores.ListadoJson>();

                foreach (eClockBase.Modelos.Incidencias.Model_SaldosTiposIncidenciasR Sald in Saldos)
                {
                    Listado.Add(new eClockBase.Controladores.ListadoJson(Sald.PERSONA_ID, Sald.TIPO_INCIDENCIA_NOMBRE, Sald.ALMACEN_INC_SALDO, Sald.TIPO_INCIDENCIA_R_DESC, Sald));
                }
                Lst_Consulta.CambiarListado(Listado);
            }
        }
    }
}
