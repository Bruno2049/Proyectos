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


namespace Kiosko.Vacaciones
{
    /// <summary>
    /// Lógica de interacción para Consultas.xaml
    /// </summary>
    public partial class Consultas : UserControl
    {
        CeC_SesionBase Sesion = null;
        public Consultas()
        {
            InitializeComponent();
        }


        private void ToolBar_OnEventClickToolBar(eClock5.UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Enviar":
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
            VerHistorial();
        }

        private void RangoFechas_CambioFechas(object sender, RoutedEventArgs e)
        {
            VerHistorial();
        }

        public void VerHistorial()
        {
            DateTime FI = new DateTime(2000,01,01);
            DateTime FF = DateTime.Now;
            if (Sesion == null)
                Sesion = CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Asistencias Asistencia = new eClockBase.Controladores.Asistencias(Sesion);
            Asistencia.ObtenAsistenciaLinealFinalizado += Asistencia_ObtenAsistenciaLinealFinalizado;
            Asistencia.ObtenAsistenciaLineal(Sesion.Mdl_Sesion.PERSONA_ID, "", FI, FF, "PERSONA_DIARIO_FECHA", "PERSONA_DIARIO_FECHA DESC", "",MainWindow.KioscoParametros.Parametros.TIPO_INCIDENCIA_ID_Vaca.ToString());
        }

        bool Cerrar = false;
        void Asistencia_ObtenAsistenciaLinealFinalizado(List<eClockBase.Modelos.Asistencias.Model_Asistencia> Asistencia)
        {
            if (Asistencia != null)
            {
                List<eClockBase.Controladores.ListadoJson> Listado = new List<eClockBase.Controladores.ListadoJson>();

                foreach (eClockBase.Modelos.Asistencias.Model_Asistencia Asis in Asistencia)
                {
                    Listado.Add(new eClockBase.Controladores.ListadoJson(Asis.PERSONA_DIARIO_ID, Asis.PERSONA_DIARIO_FECHA.ToShortDateString(), null, null, Asis));
                }
                Lst_Consulta.CambiarListado(Listado);
            }
        }
        void Msg_Cerrado()
        {
            if (Cerrar)
            {
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }
    }
}
