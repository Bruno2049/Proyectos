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
    /// Lógica de interacción para FiltradoPor.xaml
    /// </summary>
    public partial class FiltradoPor : UserControl
    {
        public string TipoIncSisIDS = "";
        public string TipoIncIDS = "";
        public FiltradoPor()
        {
            InitializeComponent();
        }

        int CuentaSeleccionados()
        {
            var results = from c in Datos
                          where c.SELECCIONADO == true
                          select new { c };
            return TbarFP.Seleccionados = results.Count();
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CuentaSeleccionados();
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (eClockBase.CeC.Convierte2Bool(e.NewValue) == true)
            {
                
            }
        }

        int m_NoSeleccionados = 0;

        string Agrupacion = "";
        int PersonaID = -1;
        void ActualizaDatos()
        {
            TbarFP.Seleccionados = m_NoSeleccionados = 0;

            Lvw_Datos.ItemsSource = null;
            eClockBase.Controladores.Asistencias Asistencia;
            string Parametros = eClockBase.CeC.Convierte2String(Clases.Parametros.Tag2Parametros(Tag).Parametro);
            DateTime FechaFinP1 = RangoFechas.FechaFin.AddDays(1);
            if (Parametros == "")
                Agrupacion = "";
            else
                if (Parametros[0] == '|' || Parametros[0] == '@')
                    Agrupacion = Parametros;
                else
                    PersonaID = eClockBase.CeC.Convierte2Int(Parametros);

            
            Asistencia = new eClockBase.Controladores.Asistencias(CeC_Sesion.ObtenSesion(this));
            Asistencia.ObtenAsistenciaLinealV5Finalizado += Asistencia_ObtenAsistenciaLinealV5Finalizado;
            Asistencia.ObtenAsistenciaLinealV5(PersonaID, Agrupacion, RangoFechas.FechaInicio, FechaFinP1, "PERSONA_NOMBRE", TipoIncSisIDS, TipoIncIDS);
        }
        List<eClockBase.Modelos.Asistencias.Model_Asistencia_Lineal_V5> Datos = null;
        void Asistencia_ObtenAsistenciaLinealV5Finalizado(List<eClockBase.Modelos.Asistencias.Model_Asistencia_Lineal_V5> Asistencia)
        {
            Datos = Asistencia;
            bool PrimeraVez = Lvw_Datos.ItemsSource == null ? true : false;

            Lvw_Datos.ItemsSource = Asistencia;
            if (!PrimeraVez)
                Lvw_Datos.Items.Refresh();
            //Datos = (List<eClockBase.Modelos.Asistencias.Model_Asistencia_Lineal_V5>)Lvw_Datos.ItemsSource;
        }

        public void Deselecciona()
        {
            foreach (eClockBase.Modelos.Asistencias.Model_Asistencia_Lineal_V5 AsisDia in Datos)
            {
                if (AsisDia.SELECCIONADO)
                    AsisDia.SELECCIONADO = false;
            }
            Lvw_Datos.Items.Refresh();
            TbarFP.Seleccionados = m_NoSeleccionados = 0;
        }

        public string ObtenPersonasDiarioIDs()
        {
             string PersonasDiarioIds = "";
            //Controles.UC_AsistenciaHorizontal.ObtenElementoAsistenciaHorizontal(Lst_Datos, 19);
             foreach (eClockBase.Modelos.Asistencias.Model_Asistencia_Lineal_V5 AsisDia in Datos)
            {
                if (AsisDia.SELECCIONADO)
                    PersonasDiarioIds = eClockBase.CeC.AgregaSeparador(PersonasDiarioIds, eClockBase.CeC.Convierte2String(AsisDia.ID), ",");
            }
            return PersonasDiarioIds;
        }

        private void Tbar_OnEventClickToolBar(UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Regresar":
                    this.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case "Btn_Reportes":
                    {
                        Controles.UC_Reportes Reportes = new Controles.UC_Reportes();
                        Reportes.ParametrosGenerales = new eClockBase.Modelos.Asistencias.Model_Parametros(PersonaID, Agrupacion, RangoFechas.FechaInicio, RangoFechas.FechaFin, TipoIncSisIDS, TipoIncIDS);
                        Reportes.Modelos = "eClockBase.Modelos.Asistencias.Reporte_Asistencias";
                        UC_Listado.MuestraComoDialogo(this, Reportes, Colors.White);
                    }
                    break;
                case "Btn_Actualizar":
                    ActualizaDatos();
                    break;
                case "Btn_DeSeleccionar":
                    
                    Deselecciona();
                    //Lst_Datos.Items.Refresh();
                    break;
                case "Btn_Turno":
                    {
                        AsigncionTurnos AsignaTurno = new Vista.Asistencias.AsigncionTurnos();
                        AsignaTurno.IsVisibleChanged += delegate(object sender, DependencyPropertyChangedEventArgs e)
                        {
                            if (AsignaTurno.Asignado)
                                ActualizaDatos();
                        };

                        Clases.Parametros.MuestraControl(Grid, AsignaTurno, new Clases.Parametros(true, ObtenPersonasDiarioIDs()));
                    }
                    break;
                case "Btn_Justificar":
                    {
                        Justificaciones Justifica = new Vista.Asistencias.Justificaciones();
                        Justifica.IsVisibleChanged += delegate(object sender, DependencyPropertyChangedEventArgs e)
                        {
                            if (Justifica.Justificado)
                                ActualizaDatos();
                        };
                        Clases.Parametros.MuestraControl(Grid, Justifica, new Clases.Parametros(true, ObtenPersonasDiarioIDs()));
                    }
                    break;
                case "Btn_Importar":
                    {
                        ImportacionDeIncidencias Importa = new Vista.Asistencias.ImportacionDeIncidencias();
                        Importa.IsVisibleChanged += delegate(object sender, DependencyPropertyChangedEventArgs e)
                        {
                            if (Importa.Realizado)
                                ActualizaDatos();
                        };
                        Clases.Parametros.MuestraControl(Grid, Importa, new Clases.Parametros(true, ObtenPersonasDiarioIDs()));
                    }
                    break;
            }
        }

        private void RangoFechas_CambioFechaEvent(bool Cargando)
        {
            if(!Cargando)
                ActualizaDatos();
        }

    }
}
