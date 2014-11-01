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

namespace eClock5.Vista.Turnos
{
    /// <summary>
    /// Lógica de interacción para AsignacionPorFechas.xaml
    /// </summary>
    public partial class AsignacionPorFechas : UserControl
    {
        public AsignacionPorFechas()
        {
            InitializeComponent();
        }
        private void UC_ToolBar_OnEventClickToolBar(UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Regresar":
                    /*this.Visibility = Visibility.Hidden;
                    break;*/
                case "Btn_Guardar":
                    AsignarTurnos();                    
                    break;
            }
        }
        private void AsignarTurnos()
        {
            try
            {
                eClockBase.Modelos.Turnos.Model_TurnoImportacion TurnoImportacion = new eClockBase.Modelos.Turnos.Model_TurnoImportacion();
                List<eClockBase.Modelos.Turnos.Model_TurnoImportacion> TurnosImportacion = new List<eClockBase.Modelos.Turnos.Model_TurnoImportacion>();

                DateTime FechaInicial = eClockBase.CeC.Convierte2DateTime(Cal_Desde.SelectedDate.Value);
                DateTime FechaFinal = eClockBase.CeC.Convierte2DateTime(Cal_Hasta.SelectedDate.Value);
                string[] PersonasLinkIDs = eClockBase.CeC.ObtenArregoSeparador(Tbx_Empleados.Text, ",");
                //string[] Persona_Link_IDS = CeC.ObtenArregoSeparador(Tbx_PERSONA_LINK_ID.Text, ",");
                int TurnoID = eClockBase.CeC.Convierte2Int(Lstb_EC_TURNOS.SeleccionadoLlave);
                foreach(string PersonaLinkID in PersonasLinkIDs)
                {
                    TurnoImportacion.PERSONA_LINK_ID = eClockBase.CeC.Convierte2Int(PersonaLinkID);
                    TurnoImportacion.FECHA_INICIAL = FechaInicial;
                    TurnoImportacion.FECHA_FINAL = FechaFinal;
                    TurnoImportacion.TURNO_ID = TurnoID;
                    TurnosImportacion.Add(TurnoImportacion);
                    TurnoImportacion = new eClockBase.Modelos.Turnos.Model_TurnoImportacion();
                }
                eClockBase.Controladores.Turnos ControladorTurnos = new eClockBase.Controladores.Turnos(CeC_Sesion.ObtenSesion(this));
                ControladorTurnos.AsignadoTurnoEvent += ControladorTurnos_AsignadoTurnoEvent;
                ControladorTurnos.AsignarTurno(TurnosImportacion);
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
        }

        void ControladorTurnos_AsignadoTurnoEvent(bool Guardado)
        {
            //throw new NotImplementedException();
        }
        private void Btn_Asignar_Click(object sender, RoutedEventArgs e)
        {
            AsignarTurnos();
        }

    }
}
