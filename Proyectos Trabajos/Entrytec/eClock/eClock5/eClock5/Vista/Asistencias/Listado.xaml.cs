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
namespace eClock5.Vista.Asistencias
{
    /// <summary>
    /// Lógica de interacción para Listado.xaml
    /// </summary>
    public partial class Listado : UserControl
    {
        public Listado()
        {
            InitializeComponent();
            Loaded += Listado_Loaded;
            IsVisibleChanged += Listado_IsVisibleChanged;
        }

        void Listado_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.Equals(true))
            {

            }
        }

        void Listado_Loaded(object sender, RoutedEventArgs e)
        {

        }


        string Agrupacion = "";
        int PersonaID = -1;

        public void ActualizaDatos()
        {
            Tbar.Seleccionados = m_NoSeleccionados = 0;
            m_PersonasDiario = new List<int>();

            eClockBase.Controladores.Asistencias Asistencia;
            Lst_Datos.ItemsSource = null;
            Asistencia = new eClockBase.Controladores.Asistencias(CeC_Sesion.ObtenSesion(this));
            Asistencia.EventObtenAsistenciaFinalizado += Asistencia_EventObtenAsistenciaFinalizado;
            Asistencia.EventoObtenAsistenciaHorizontalFinalizado += Asistencia_EventoObtenAsistenciaHorizontalFinalizado;
            string Parametros = eClockBase.CeC.Convierte2String(Clases.Parametros.Tag2Parametros(Tag).Parametro);
            DateTime FechaFinP1 = RangoFechas.FechaFin.AddDays(1);
            if (Parametros == "")
                Agrupacion = "";
            else
                if (Parametros[0] == '|' || Parametros[0] == '@')
                    Agrupacion = Parametros;
                else
                    PersonaID = eClockBase.CeC.Convierte2Int(Parametros);
            
            Asistencia.ObtenAsistenciaHorizontalN(true, false, true, false, true, true, true, PersonaID, Agrupacion, RangoFechas.FechaInicio, FechaFinP1);
        }


        void Asistencia_EventoObtenAsistenciaHorizontalFinalizado(List<object> AsistenciaHorizontal)
        {
            try
            {

                // Datos[0].Seleccionado = true;

                bool PrimeraVez = Lst_Datos.ItemsSource == null ? true : false;
                if (AsistenciaHorizontal.Count > 0)
                {

                    Asis_Titulo.DataContext = AsistenciaHorizontal[0];
                    Asis_Titulo.Actualiza();
                }

                Lst_Datos.ItemsSource = AsistenciaHorizontal;
                if (!PrimeraVez)
                    Lst_Datos.Items.Refresh();

            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
        }


        void Asistencia_EventObtenAsistenciaFinalizado(List<eClockBase.Modelos.Asistencias.Model_Asistencia> Asistencias)
        {
            try
            {

                // Datos[0].Seleccionado = true;
                bool PrimeraVez = Lst_Datos.ItemsSource == null ? true : false;
                Lst_Datos.ItemsSource = Asistencias;
                if (!PrimeraVez)
                    Lst_Datos.Items.Refresh();




            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
        }

        private void Lst_Datos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Object Obj = e.AddedItems[0];
            Obj = Obj;
        }
        int m_NoSeleccionados = 0;
        private void UC_AsistenciaHorizontal_NoSeleccionadosCambio_1(int NoSeleccionados, int NoSeleccionadosAnterior)
        {
            m_NoSeleccionados += NoSeleccionados - NoSeleccionadosAnterior;
            Tbar.Seleccionados = m_NoSeleccionados;
        }

        public string ObtenPersonasDiarioIDs()
        {
            string PersonasDiarioIds = "";
            //Controles.UC_AsistenciaHorizontal.ObtenElementoAsistenciaHorizontal(Lst_Datos, 19);
            for (int Cont = 0; Cont < Lst_Datos.Items.Count; Cont++)
            {
                Controles.UC_AsistenciaHorizontal AH = Controles.UC_AsistenciaHorizontal.ObtenElementoAsistenciaHorizontal(Lst_Datos, Cont);
                if (AH != null)
                {
                    string PersonasDiariosIDS = AH.ObtenPersonasDiarioIdsSeleccionadas();
                    if (PersonasDiariosIDS != null && PersonasDiariosIDS != "")
                        PersonasDiarioIds = eClockBase.CeC.AgregaSeparador(PersonasDiarioIds, PersonasDiariosIDS, ",");
                }
            }
            return PersonasDiarioIds;
        }

        private void Tbar_OnEventClickToolBar(UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Reportes":
                    {
                        Controles.UC_Reportes Reportes = new Controles.UC_Reportes();
                        Reportes.ParametrosGenerales = new eClockBase.Modelos.Asistencias.Model_Parametros(PersonaID, Agrupacion, RangoFechas.FechaInicio, RangoFechas.FechaFin);
                        Reportes.Modelos = "eClockBase.Modelos.Asistencias.Reporte_Asistencias,eClockBase.Modelos.Asistencias.Reporte_Asistencia31,eClockBase.Modelos.Asistencias.Reporte_AsistenciaAbr31,eClockBase.Modelos.HorasExtras.Reporte_Semanal_HET_DT,eClockBase.Modelos.HorasExtras.Reporte_Semanal_HET,eClockBase.Modelos.PreNomina.Reporte_PreNomina,eClockBase.Modelos.HorasExtras.Reporte_Semanal,eClockBase.Modelos.Asistencias.Model_HorasExtra,eClockBase.Modelos.HorasExtras.Reporte_Semanal_HET_DT,eClockBase.Modelos.HorasExtras.Reporte_Semanal_HET,eClockBase.Modelos.Asistencias.Model_HorasExtra";
                        UC_Listado.MuestraComoDialogo(this, Reportes, Colors.White);
                    }
                    break;
                case "Btn_Actualizar":
                    ActualizaDatos();
                    break;
                case "Btn_DeSeleccionar":
                    Tbar.Seleccionados = m_NoSeleccionados = 0;
                    Lst_Datos.Items.Refresh();
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
                case "Btn_Filtrar":
                    Clases.Parametros.MuestraControl(Grid, new SeleccionTiposIncidencias(), new Clases.Parametros(true, Clases.Parametros.Tag2Parametros(Tag).Parametro));
                    break;
                case "Btn_Saldos":
                    Clases.Parametros.MuestraControl(Grid, new Saldos(), new Clases.Parametros(true, Clases.Parametros.Tag2Parametros(Tag).Parametro));
                    break;
                case "Btn_Totales":
                    Clases.Parametros.MuestraControl(Grid, new Totales(), new Clases.Parametros(true, Clases.Parametros.Tag2Parametros(Tag).Parametro));
                    break;
                case "Btn_Historial":
                    /* En esta parte se muestra el nuevo Control, Al entrear en el tercer parametro se pide un parametro true el cual enviara que se requiere el boton regresar*/
                    Clases.Parametros.MuestraControl(Grid, new Historial(), new Clases.Parametros(true, Clases.Parametros.Tag2Parametros(Tag).Parametro));
                    break;
                case "Btn_Horas":
                    {
                        //Pop_Horas.IsOpen = true;
                        eClockBase.Controladores.Asistencias Asistencia;
                        Asistencia = new eClockBase.Controladores.Asistencias(CeC_Sesion.ObtenSesion(this));
                        Asistencia.ObtenTiemposFinalizado += Asistencia_ObtenTiemposFinalizado;
                        Asistencia.ObtenTiempos(PersonaID, Agrupacion, RangoFechas.FechaInicio, RangoFechas.FechaFin.AddDays(1));
                    }
                    break;

                case "Btn_Solicitudes":
                    {
                        Controles.UC_Reportes Reportes = new Controles.UC_Reportes();
                        Reportes.ParametrosGenerales = new eClockBase.Modelos.Asistencias.Model_Parametros(PersonaID, Agrupacion, RangoFechas.FechaInicio, RangoFechas.FechaFin);
                        Reportes.Modelos = "eClockBase.Modelos.Solicitudes.Model_ReporteSolicitudVacaciones";
                        UC_Listado.MuestraComoDialogo(this, Reportes, Colors.White);
                    }
                    break;
            }
        }

        string ATexto(int Segundos)
        {
            TimeSpan TS = new TimeSpan(0, 0, Segundos);

            return TS.ToString();
        }
        string ATexto(decimal Horas)
        {
            return Horas.ToString();
        }
        void Asistencia_ObtenTiemposFinalizado(eClockBase.Modelos.Asistencias.Model_Tiempos Tiempos)
        {
            if (Tiempos != null)
            {
                Pop_Horas.IsOpen = true;
                Lbld_HorasEsperadas.Text = ATexto(Tiempos.SegundosEsperados);
                Lbld_HorasTrabajadas.Text = ATexto(Tiempos.SegundosTrabajados);
                Lbld_HorasEstancia.Text = ATexto(Tiempos.SegundosEstancia);
                Lbld_HorasComida.Text = ATexto(Tiempos.SegundosComida);
                Lbld_HorasRetardo.Text = ATexto(Tiempos.SegundosDeuda);
                Lbld_HorasExtraTrabajadas.Text = ATexto(Tiempos.SegundosHESis);
                Lbld_HorasExtraCalculadas.Text = ATexto(Tiempos.SegundosHECal);
                Lbld_HorasExtraAplicadas.Text = ATexto(Tiempos.SegundosHeApl);
                Lbld_HorasExtraSimples.Text = ATexto(Tiempos.HorasSimples);
                Lbld_HorasExtraDobles.Text = ATexto(Tiempos.HorasDobles);
                Lbld_HorasExtraTriples.Text = ATexto(Tiempos.HorasTriples);

            }
        }



        /// <summary>
        /// 
        /// </summary>

        List<int> m_PersonasDiario;
        private void UC_AsistenciaHorizontal_CambioSeleccionado_1(int PersonaDiarioID, bool Seleccionado)
        {
            if (Seleccionado)
                m_PersonasDiario.Add(PersonaDiarioID);
            else
                m_PersonasDiario.Remove(PersonaDiarioID);
        }



        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            Console.WriteLine("ScrollViewer_ScrollChanged");
            Asis_Titulo.Width = e.ExtentWidth;
            Asis_Titulo.Margin = new Thickness(-e.HorizontalOffset + 6, Asis_Titulo.Margin.Top, Asis_Titulo.Margin.Bottom, Asis_Titulo.Margin.Right);

            //            e.HorizontalOffset
        }


        private void RangoFechas_CambioFechaEvent(bool Cargando)
        {
            if (!Cargando)
                ActualizaDatos();
        }
    }
}
