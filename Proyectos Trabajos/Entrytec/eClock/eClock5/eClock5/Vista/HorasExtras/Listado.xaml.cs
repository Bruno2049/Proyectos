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

namespace eClock5.Vista.HorasExtras
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
            Sesion = CeC_Sesion.ObtenSesion(this);
        }

        void Listado_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.Equals(true))
            {
                
            }
        }

        void Listado_Loaded(object sender, RoutedEventArgs e)
        {

            eClockBase.Controladores.Sesion CoSesion = new eClockBase.Controladores.Sesion(Sesion);
            CoSesion.ObtenConfigEvent += CoSesion_ObtenConfigEvent;
            CoSesion.ObtenConfig("TiposIncidenciasHorasExtras", 1);
        }

        void CoSesion_ObtenConfigEvent(string Resultado)
        {
            Lst_TiposIncidencias.Filtro = "TIPO_INCIDENCIA_ID IN (" + Resultado + ")";
        }
        string Parametros = "-1";
        string Agrupacion = "";
        int PersonaID = -1;
        eClockBase.CeC_SesionBase Sesion = null;
        public void ActualizaDatos()
        {

            eClockBase.Controladores.Asistencias Asistencia;
            Lvw_Datos.ItemsSource = null;

            Asistencia = new eClockBase.Controladores.Asistencias(Sesion);
            Asistencia.EventoObtenAsistenciaHE += Asistencia_EventoObtenAsistenciaHE;

            Parametros = eClockBase.CeC.Convierte2String(Clases.Parametros.Tag2Parametros(Tag).Parametro);
            DateTime FechaFinP1 = RangoFechas.FechaFin.AddDays(1);
            if (Parametros == "")
                Agrupacion = "";
            else
                if (Parametros[0] == '|' || Parametros[0] == '@')
                    Agrupacion = Parametros;
                else
                    PersonaID = eClockBase.CeC.Convierte2Int(Parametros);

            Asistencia.ObtenAsistenciaHE(false, false, true, false, false, true, true, true, PersonaID, Agrupacion, RangoFechas.FechaInicio, FechaFinP1);
        }

        List<eClockBase.Modelos.Asistencias.Model_HorasExtra> Datos = null;
        void Asistencia_EventoObtenAsistenciaHE(List<eClockBase.Modelos.Asistencias.Model_HorasExtra> AsistenciaHE)
        {
            Datos = AsistenciaHE;
            bool PrimeraVez = Lvw_Datos.ItemsSource == null ? true : false;

            Lvw_Datos.ItemsSource = AsistenciaHE;
            if (!PrimeraVez)
                Lvw_Datos.Items.Refresh();

        }

        public string ObtenPersonasDiarioHEIDs()
        {
            List<eClockBase.Modelos.Asistencias.Model_HorasExtra> AsistenciaHE = (List<eClockBase.Modelos.Asistencias.Model_HorasExtra>)Lvw_Datos.ItemsSource;
            string PersonasDiarioIds = "";
            //Controles.UC_AsistenciaHorizontal.ObtenElementoAsistenciaHorizontal(Lst_Datos, 19);
            foreach (eClockBase.Modelos.Asistencias.Model_HorasExtra HoraExtra in AsistenciaHE)
            {
                if (HoraExtra.Seleccionado)
                    PersonasDiarioIds = eClockBase.CeC.AgregaSeparador(PersonasDiarioIds, eClockBase.CeC.Convierte2String(HoraExtra.PERSONA_D_HE_ID), ",");
            }
            return PersonasDiarioIds;
        }
        List<int> PERSONA_D_HE_ID_Enviadas = new List<int>();
        void AceptarHorasExtras(int TipoIncidenciaID)
        {
            List<eClockBase.Modelos.Asistencias.Model_AplicaHorasExtras> Extras = new List<eClockBase.Modelos.Asistencias.Model_AplicaHorasExtras>();
            List<eClockBase.Modelos.Asistencias.Model_AplicaHorasExtrasAv> ExtrasAv = new List<eClockBase.Modelos.Asistencias.Model_AplicaHorasExtrasAv>();
            foreach (eClockBase.Modelos.Asistencias.Model_HorasExtra HoraExtra in Datos)
            {
                if (HoraExtra.Seleccionado)
                {
                    PERSONA_D_HE_ID_Enviadas.Add(HoraExtra.PERSONA_D_HE_ID);
                    if (TipoIncidenciaID >= 0)
                    {
                        eClockBase.Modelos.Asistencias.Model_AplicaHorasExtrasAv ExtraAv = new eClockBase.Modelos.Asistencias.Model_AplicaHorasExtrasAv();
                        ExtraAv.TIPO_INCIDENCIA_ID = TipoIncidenciaID;
                        ExtraAv.PERSONA_D_HE_ID = HoraExtra.PERSONA_D_HE_ID;
                        ExtraAv.PERSONA_D_HE_APL = eClockBase.CeC.DateTime2TimeSpan(HoraExtra.PERSONA_D_HE_APL);
                        ExtraAv.PERSONA_D_HE_COMEN = HoraExtra.PERSONA_D_HE_COMEN;
                        ExtrasAv.Add(ExtraAv);
                    }
                    else
                    {
                        eClockBase.Modelos.Asistencias.Model_AplicaHorasExtras Extra = new eClockBase.Modelos.Asistencias.Model_AplicaHorasExtras();
                        Extra.PERSONA_D_HE_ID = HoraExtra.PERSONA_D_HE_ID;
                        Extra.PERSONA_D_HE_COMEN = HoraExtra.PERSONA_D_HE_COMEN;
                        Extras.Add(Extra);
                    }
                }
            }

            eClockBase.Controladores.Asistencias Asis = new eClockBase.Controladores.Asistencias(Sesion);
            Asis.AplicaHorasExtrasEvent += Asis_AplicaHorasExtrasEvent;
            if (TipoIncidenciaID >= 0)
                Asis.AplicaHorasExtras(ExtrasAv);
            else
                Asis.AplicaHorasExtras(Extras);

        }

        void QuitarHorasExtrasAplicadas()
        {
            List<eClockBase.Modelos.Asistencias.Model_AplicaHorasExtrasAv> ExtrasAv = new List<eClockBase.Modelos.Asistencias.Model_AplicaHorasExtrasAv>();
            foreach (eClockBase.Modelos.Asistencias.Model_HorasExtra HoraExtra in Datos)
            {
                if (HoraExtra.Seleccionado)
                {
                    PERSONA_D_HE_ID_Enviadas.Add(HoraExtra.PERSONA_D_HE_ID);
                    eClockBase.Modelos.Asistencias.Model_AplicaHorasExtrasAv ExtraAv = new eClockBase.Modelos.Asistencias.Model_AplicaHorasExtrasAv();
                    ExtraAv.TIPO_INCIDENCIA_ID = 0;
                    ExtraAv.PERSONA_D_HE_ID = HoraExtra.PERSONA_D_HE_ID;
                    ExtraAv.PERSONA_D_HE_APL = new TimeSpan();
                    ExtraAv.PERSONA_D_HE_COMEN = HoraExtra.PERSONA_D_HE_COMEN;
                    ExtrasAv.Add(ExtraAv);
                }
            }

            eClockBase.Controladores.Asistencias Asis = new eClockBase.Controladores.Asistencias(Sesion);
            Asis.AplicaHorasExtrasEvent += Asis_AplicaHorasExtrasEvent;
            Asis.AplicaHorasExtras(ExtrasAv);

        }

        void Asis_AplicaHorasExtrasEvent(List<int> PERSONA_D_HE_IDs)
        {
            foreach (int PERSONA_D_HE_ID in PERSONA_D_HE_IDs)
            {
                foreach (eClockBase.Modelos.Asistencias.Model_HorasExtra HoraExtra in Datos)
                {
                    if (HoraExtra.PERSONA_D_HE_ID == PERSONA_D_HE_ID)
                    {
                        HoraExtra.Seleccionado = false;
                        break;
                    }
                }
            }
            Lvw_Datos.Items.Refresh();
            if (CuentaSeleccionados() > 0)
            {
                Sesion.MuestraMensaje("Algunos registros no pudieron ser guardados", 10);
            }
        }
        private void Tbar_OnEventClickToolBar(UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Aceptar":
                    AceptarHorasExtras(-1);
                    break;
                case "Btn_Mias":
                    AceptarHorasExtras(0);
                    break;
                case "Btn_Como":
                    Ppp_TiposIncidencias.IsOpen = true;
                    break;
                case "Btn_Reportes":
                    {
                        Controles.UC_Reportes Reportes = new Controles.UC_Reportes();//",eClockBase.Modelos.HorasExtras.Reporte_Semanal_HET_DT,eClockBase.Modelos.HorasExtras.Reporte_Semanal_HET"
                        Reportes.ParametrosGenerales = new eClockBase.Modelos.Asistencias.Model_Parametros(PersonaID, Agrupacion, RangoFechas.FechaInicio, RangoFechas.FechaFin);
                        Reportes.Modelos = "eClockBase.Modelos.HorasExtras.Reporte_Semanal,eClockBase.Modelos.Asistencias.Model_HorasExtra,eClockBase.Modelos.HorasExtras.Reporte_Semanal_HET_DT,eClockBase.Modelos.HorasExtras.Reporte_Semanal_HET,eClockBase.Modelos.Asistencias.Model_HorasExtra";
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

                case "Btn_Importar":
                    {
                        /*        ImportacionDeIncidencias Importa = new Vista.Asistencias.ImportacionDeIncidencias();
                                Importa.IsVisibleChanged += delegate(object sender, DependencyPropertyChangedEventArgs e)
                                {
                                    if (Importa.Realizado)
                                        ActualizaDatos();
                                };
                                Clases.Parametros.MuestraControl(Grid, Importa, new Clases.Parametros(true, ObtenPersonasDiarioIDs()));*/
                    }
                    break;
                case "Btn_Quitar":
                    {
                        QuitarHorasExtrasAplicadas();
                        /*
                        eClockBase.Controladores.Asistencias Asis = new eClockBase.Controladores.Asistencias(Sesion);
                        Asis.QuitaHorasExtrasEvent += Asis_QuitaHorasExtrasEvent;
                        Asis.QuitaHorasExtras(ObtenPersonasDiarioHEIDs());*/
                    }
                    break;
            }
        }

        void Asis_QuitaHorasExtrasEvent(List<int> PERSONA_D_HE_IDs)
        {
            foreach (int PERSONA_D_HE_ID in PERSONA_D_HE_IDs)
            {
                foreach (eClockBase.Modelos.Asistencias.Model_HorasExtra HoraExtra in Datos)
                {
                    if (HoraExtra.PERSONA_D_HE_ID == PERSONA_D_HE_ID)
                    {
                        HoraExtra.Seleccionado = false;
                        break;
                    }
                }
            }
            Lvw_Datos.Items.Refresh();
            if (CuentaSeleccionados() > 0)
            {
                Sesion.MuestraMensaje("Algunos registros no pudieron ser guardados", 10);
            }
            else
                ActualizaDatos();
        }

        int CuentaSeleccionados()
        {
            var results = from c in Datos
                          where c.Seleccionado == true
                          select new { c };
            int R = Tbar.Seleccionados = results.Count();
            return R;
        }

        public void Deselecciona()
        {
            foreach (eClockBase.Modelos.Asistencias.Model_HorasExtra HoraExtra in Datos)
            {
                if (HoraExtra.Seleccionado)
                    HoraExtra.Seleccionado = false;
            }
            Lvw_Datos.Items.Refresh();
            Tbar.Seleccionados =  0;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CuentaSeleccionados();
        }

        private void Lst_TiposIncidencias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Ppp_TiposIncidencias.IsOpen = false;
            AceptarHorasExtras(eClockBase.CeC.Convierte2Int(Lst_TiposIncidencias.Seleccionado.Llave));

        }

        private void RangoFechas_CambioFechaEvent(bool Cargando)
        {
            if(!Cargando)
                ActualizaDatos();
        }




    }
}
