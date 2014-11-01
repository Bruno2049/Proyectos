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
using Newtonsoft.Json;
using System.ComponentModel;
using eClockBase.Controladores;
using System.Collections.ObjectModel;

namespace eClock5.Vista.Asistencias
{
    /// <summary>
    /// Lógica de interacción para Justificaciones.xaml
    /// </summary>
    public partial class Justificaciones : UserControl
    {

        public bool Justificado = false;

        public Justificaciones()
        {
            InitializeComponent();
        }

        public string ObtenPersonasDiariosIDS()
        {
            return eClock5.Clases.Parametros.Tag2Parametros(this.Tag).Parametro.ToString();
        }

        private void UC_ToolBar_OnEventClickToolBar(UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Regresar":
                    this.Visibility = Visibility.Hidden;
                    break;
                case "Btn_Borrar":
                    {
                        eClockBase.Controladores.Incidencias Incidencias = new eClockBase.Controladores.Incidencias(Sesion);
                        Incidencias.AsignaIncidenciaPersonasDiarioEvent += Incidencias_AsignaIncidenciaPersonasDiarioEvent;
                        this.IsEnabled = false;
                        Incidencias.AsignaIncidenciaPersonasDiario(0, ObtenPersonasDiariosIDS(), "");                        
                    }
                    break;
                case "Btn_Guardar":
                    if (Lbx_TipoIncidencia.SeleccionadoLlave == null)
                    {
                        Sesion.MuestraMensaje("No ha seleccionado el tipo de incidencia", 10);
                        return;
                    }
                    if (ValidaRegla() && ValidaHoras())
                    {
                        eClockBase.Modelos.Model_TIPO_INCIDENCIAS TipoIncidencia = (eClockBase.Modelos.Model_TIPO_INCIDENCIAS)Lbx_TipoIncidencia.Seleccionado.Imagen;

                        string Comentario = "";
                        if (TipoIncidencia.TIPO_INCIDENCIA_HORAS)
                        {
                            eClockBase.Modelos.Incidencias.Model_Horas Horas = new eClockBase.Modelos.Incidencias.Model_Horas();
                            Horas.Inicio = StatusHoras.Turno_Entrada.Date + Tbx_HoraInicio.ValueTimeSpan;
                            Horas.Fin = StatusHoras.Turno_Entrada.Date + Tbx_HoraFin.ValueTimeSpan;
                            Horas.Tiempo = Horas.Fin - Horas.Inicio;
                            Comentario = Campos.Vista2JSon(Horas);
                        }
                        else
                            Comentario = Campos.Vista2JSon(null);

                        eClockBase.Controladores.Incidencias Incidencias = new eClockBase.Controladores.Incidencias(Sesion);

                        Incidencias.AsignaIncidenciaPersonasDiarioEvent += Incidencias_AsignaIncidenciaPersonasDiarioEvent;
                        this.IsEnabled = false;
                        Lbl_ErrorPeriodo.Visibility = System.Windows.Visibility.Collapsed;
                        Incidencias.AsignaIncidenciaPersonasDiario(TipoIncidencia.TIPO_INCIDENCIA_ID, ObtenPersonasDiariosIDS(), Comentario);

                    }
                    else
                        Sesion.MuestraMensaje("Resuelva los errores antes de continuar", 5);
                    break;
            }
        }

        void Incidencias_AsignaIncidenciaPersonasDiarioEvent(int NoGuardados)
        {
            this.IsEnabled = true;
            if (NoGuardados > 0)
            {
                Justificado = true;
                this.Visibility = Visibility.Hidden;
            }
            else
            {
                Sesion.MuestraMensaje("No se pudo justificar, verifique que el periodo no este cerrado y tenga conexión", 10);
                Lbl_ErrorPeriodo.Visibility = System.Windows.Visibility.Visible;
            }
        }
        eClockBase.CeC_SesionBase Sesion;
        List<eClockBase.Modelos.Model_TIPO_INCIDENCIAS> TipoIncidencias;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Lbl_NoUsableVariosDias.Visibility = System.Windows.Visibility.Collapsed;
            Stp_PorHoras.Visibility = System.Windows.Visibility.Collapsed;
            Lbl_ErrorPeriodo.Visibility = System.Windows.Visibility.Collapsed;
            Sesion = CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Sesion cSesion = new Sesion(Sesion);
            eClockBase.Modelos.Model_TIPO_INCIDENCIAS TipoIncidencia = new eClockBase.Modelos.Model_TIPO_INCIDENCIAS();
            cSesion.ObtenDatosEvent += Ses_ObtenDatosEvent;
            cSesion.ObtenDatos("EC_TIPO_INCIDENCIAS", "", TipoIncidencia);
            Rbt_Salida_Checked(sender, e);
        }

        void Ses_ObtenDatosEvent(int Resultado, string Datos)
        {            
            try
            {
                if (Resultado > 0)
                {
                    Datos = eClockBase.CeC.Json2JsonList(Datos);
                    TipoIncidencias = eClockBase.Controladores.CeC_ZLib.Json2Object<List<eClockBase.Modelos.Model_TIPO_INCIDENCIAS>>(Datos);
                    List<ListadoJson> Listado = new List<ListadoJson>();
                    foreach (eClockBase.Modelos.Model_TIPO_INCIDENCIAS TipoIncidencia in TipoIncidencias)
                    {
                        Listado.Add(new ListadoJson(TipoIncidencia.TIPO_INCIDENCIA_ID, TipoIncidencia.TIPO_INCIDENCIA_NOMBRE, TipoIncidencia.TIPO_INCIDENCIA_ABR, TipoIncidencia.TIPO_INCIDENCIA_AGRUPADOR, TipoIncidencia));
                    }
                    Lbx_TipoIncidencia.CambiarListado(Listado);
                }
            }
            catch { }

        }

        private eClockBase.Modelos.Model_TIPO_INCIDENCIAS ObtenTipoIncidencia(int TipoIncidenciaID)
        {
            foreach (eClockBase.Modelos.Model_TIPO_INCIDENCIAS TipoIncidencia in TipoIncidencias)
            {
                if (TipoIncidencia.TIPO_INCIDENCIA_ID == TipoIncidenciaID)
                    return TipoIncidencia;
            }
            return null;
        }

        private bool MuestraCampos(eClockBase.Modelos.Model_TIPO_INCIDENCIAS TipoIncidencia)
        {
            string Desc = TipoIncidencia.TIPO_INCIDENCIA_CAMPOS;
            if (Desc != null && Desc != "" && Desc != "[]")
            {
                Campos.Visibility = System.Windows.Visibility.Visible;
                Campos.Asigna(Desc, "");
            }
            else
            {
                Campos.Visibility = System.Windows.Visibility.Collapsed;
            }
            return true;
        }
        eClockBase.Modelos.Model_TIPO_INCIDENCIAS TipoIncidenciaSeleccionada = null;
        bool Puede()
        {
            TipoIncidenciaSeleccionada = ObtenTipoIncidencia(eClockBase.CeC.Convierte2Int(Lbx_TipoIncidencia.Seleccionado.Llave));
            bool R = true;
            if (!PuedeHoras(TipoIncidenciaSeleccionada))
                R = false;
            if (!MuestraCampos(TipoIncidenciaSeleccionada))
                R = false;
            if (!PuedeIncidencia(TipoIncidenciaSeleccionada))
                R = false;
            return R;
        }


        private void Lbx_TipoIncidencia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Puede();
        }

        /// <summary>
        /// Region de incidencia por Horas
        /// </summary>
        #region Incidencia Por Horas
        string PersonasDiarioHoras = "";
        string[] sPersonasDiarioHoras = null;
        eClockBase.Modelos.Incidencias.Model_StatusHoras StatusHoras = null;
        bool ActualizandoHoras = false;
        private bool PuedeHoras(eClockBase.Modelos.Model_TIPO_INCIDENCIAS TipoIncidencia)
        {
            Lbl_NoUsableVariosDias.Visibility = System.Windows.Visibility.Collapsed;
            ActualizandoHoras = false;
            if (TipoIncidencia.TIPO_INCIDENCIA_HORAS)
            {

                if (PersonasDiarioHoras != ObtenPersonasDiariosIDS())
                {
                    PersonasDiarioHoras = ObtenPersonasDiariosIDS();
                    sPersonasDiarioHoras = eClockBase.CeC.ObtenArregoSeparador(PersonasDiarioHoras, ",");
                    StatusHoras = null;
                    ActualizaTurno();
                }
                if (sPersonasDiarioHoras.Length > 1)
                {
                    Lbl_NoUsableVariosDias.Visibility = System.Windows.Visibility.Visible;
                    return false;

                }
                else
                {
                    ChecaHoras();
                    Stp_PorHoras.Visibility = System.Windows.Visibility.Visible;
                    Rbt_Salida.IsChecked = true;
                    if (StatusHoras == null)
                    {
                        ActualizandoHoras = true;
                        eClockBase.Controladores.Incidencias Incidencias = new Incidencias(Sesion);
                        Incidencias.StatusHorasEvent += Incidencias_StatusHorasEvent;
                        Incidencias.StatusHoras(PersonasDiarioHoras);
                    }
                }
            }
            else
            {
                Stp_PorHoras.Visibility = System.Windows.Visibility.Collapsed;
            }
            return true;
        }

        decimal ObtenNoHoras()
        {
            decimal R = 0;
            TimeSpan TS = Tbx_HoraFin.ValueTimeSpan - Tbx_HoraInicio.ValueTimeSpan;
            if (StatusRegla != null && StatusRegla.Count > 0)
            {
                return eClockBase.CeC.ObtenValor(TS, StatusRegla[0].TIPO_UNIDAD_ID, StatusRegla[0].TIPO_REDONDEO_ID);
            }

            return R;
        }

        bool ChecaHoras()
        {
            bool Correcto = true;
            if (Tbx_HoraInicio.Value > Tbx_HoraFin.Value)
                Correcto = false;
            Lbl_EntradaMayor.Visibility = Correcto ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
            if (Correcto)
            {
                ActualizaAConsumir(ObtenNoHoras());
            }
            else
            {
                ActualizaAConsumir(0);
            }
            return Correcto;
        }
        void ActualizaTurno()
        {
            if (StatusHoras != null)
            {
                Tbx_TurnoEntrada.Visibility = Tbx_TurnoSalida.Visibility = System.Windows.Visibility.Visible;
                Tbx_TurnoEntrada.ValueTimeSpan = StatusHoras.Turno_Entrada - StatusHoras.Turno_Entrada.Date;
                Tbx_TurnoSalida.ValueTimeSpan = StatusHoras.Turno_Salida - StatusHoras.Turno_Entrada.Date;
            }
            else
            {
                Tbx_TurnoEntrada.Visibility = Tbx_TurnoSalida.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
        void ActualizaHoras()
        {
            if (StatusHoras != null)
            {
                if (eClockBase.CeC.Convierte2Bool(Rbt_Salida.IsChecked))
                {
                    if(StatusHoras.UltimaChecada == new DateTime())
                        Tbx_HoraInicio.ValueTimeSpan = StatusHoras.Turno_Entrada - StatusHoras.Turno_Entrada.Date;
                    else
                        Tbx_HoraInicio.ValueTimeSpan = StatusHoras.UltimaChecada - StatusHoras.Turno_Entrada.Date;
                    Tbx_HoraFin.ValueTimeSpan = StatusHoras.Turno_Salida - StatusHoras.Turno_Entrada.Date;
                }
                if (eClockBase.CeC.Convierte2Bool(Rbt_Entrada.IsChecked))
                {
                    Tbx_HoraInicio.ValueTimeSpan = StatusHoras.Turno_Entrada - StatusHoras.Turno_Entrada.Date;
                    if (StatusHoras.PrimerChecada == new DateTime())
                        Tbx_HoraFin.ValueTimeSpan = StatusHoras.Turno_Salida - StatusHoras.Turno_Entrada.Date;
                    else
                        Tbx_HoraFin.ValueTimeSpan = StatusHoras.PrimerChecada - StatusHoras.Turno_Entrada.Date;
                }
                ChecaHoras();
            }


        }

        void Incidencias_StatusHorasEvent(List<eClockBase.Modelos.Incidencias.Model_StatusHoras> Status)
        {
            if (Status != null && Status.Count == 1)
            {
                StatusHoras = Status[0];
                ActualizaHoras();
            }
            ActualizaTurno();
            ActualizandoHoras = false;
        }

        private void Rbt_Salida_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Tbx_HoraInicio.IsEnabled = false;
                Tbx_HoraFin.IsEnabled = true;
                ActualizaHoras();
            }
            catch { }
        }

        private void Rbt_Entrada_Checked(object sender, RoutedEventArgs e)
        {
            try
            {

                Tbx_HoraInicio.IsEnabled = true;
                Tbx_HoraFin.IsEnabled = false;
                ActualizaHoras();
            }
            catch { }
        }

        private void Rbt_Intervalo_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Tbx_HoraInicio.IsEnabled = true;
                Tbx_HoraFin.IsEnabled = true;
                ActualizaHoras();
            }
            catch { }
        }

        private void HoraValorCambio(object sender, RoutedEventArgs e)
        {
            ChecaHoras();
        }

        bool ValidaHoras()
        {
            if (TipoIncidenciaSeleccionada != null)
            {
                if (!TipoIncidenciaSeleccionada.TIPO_INCIDENCIA_HORAS)
                    return true;
                if (ActualizandoHoras)
                    return false;
                if (sPersonasDiarioHoras.Length > 1)
                    return false;
            }
            return true;
        }

        #endregion

        #region Incidencia Reglas
        List<eClockBase.Modelos.Incidencias.Model_StatusRegla> StatusRegla = null;
        string PersonasDiarioReglas = "";
        string[] sPersonasDiarioReglas = null;
        eClockBase.Modelos.Model_TIPO_INCIDENCIAS TipoIncidenciaReglas = null;
        bool ActualizandoRegla = false;
        bool PuedeIncidencia(eClockBase.Modelos.Model_TIPO_INCIDENCIAS TipoIncidencia)
        {
            ActualizandoRegla = false;
            if (!TipoIncidencia.TIPO_INCIDENCIA_REGLAS)
            {
                Stp_Saldos.Visibility = System.Windows.Visibility.Collapsed;
                return true;
            }
            Stp_Saldos.Visibility = System.Windows.Visibility.Visible;

            if (PersonasDiarioReglas != ObtenPersonasDiariosIDS())
            {
                PersonasDiarioReglas = ObtenPersonasDiariosIDS();
                sPersonasDiarioReglas = eClockBase.CeC.ObtenArregoSeparador(PersonasDiarioReglas, ",");
                StatusRegla = null;
                Stp_Saldos.Children.Clear();
                TipoIncidenciaReglas = null;
            }
            if (TipoIncidenciaReglas != TipoIncidencia)
            {
                TipoIncidenciaReglas = TipoIncidencia;
                eClockBase.Controladores.Incidencias cIncidencias = new Incidencias(Sesion);


                if (TipoIncidenciaReglas.TIPO_INCIDENCIA_HORAS)
                {
                    ActualizandoRegla = true;
                    cIncidencias.StatusReglaHorasEvent += cIncidencias_StatusReglaHorasEvent;
                    cIncidencias.StatusReglaHoras(TipoIncidenciaReglas.TIPO_INCIDENCIA_ID, eClockBase.CeC.Convierte2Int(sPersonasDiarioReglas[0]), 0);
                }
                else
                {
                    ActualizandoRegla = true;
                    cIncidencias.StatusReglaEvent += cIncidencias_StatusReglaEvent;
                    cIncidencias.StatusRegla(TipoIncidenciaReglas.TIPO_INCIDENCIA_ID, PersonasDiarioReglas);
                }
            }
            return true;
        }

        List<ReglaJustificacion> StatusReglasJustificacion = null;
        bool ActualizaSaldos()
        {
            Stp_Saldos.Children.Clear();
            StatusReglasJustificacion = new List<ReglaJustificacion>();
            foreach (eClockBase.Modelos.Incidencias.Model_StatusRegla Status in StatusRegla)
            {
                ReglaJustificacion RJ = new ReglaJustificacion();
                RJ.Carga(Status, PersonasDiarioReglas);
                StatusReglasJustificacion.Add(RJ);
                Stp_Saldos.Children.Add(RJ);
            }
            return true;
        }

        bool ActualizaAConsumir(decimal AConsumir)
        {
            if (StatusReglasJustificacion != null)
            {
                foreach (ReglaJustificacion RJ in StatusReglasJustificacion)
                    RJ.ActualizaSaldo(AConsumir);
            }
            return true;
        }

        bool TieneSaldo()
        {
            foreach (ReglaJustificacion RJ in StatusReglasJustificacion)
                if (!RJ.TieneSaldo())
                    return false;
            return true;
        }

        void cIncidencias_StatusReglaHorasEvent(eClockBase.Modelos.Incidencias.Model_StatusRegla Status)
        {
            StatusRegla = new List<eClockBase.Modelos.Incidencias.Model_StatusRegla>();
            if (Status != null)
                StatusRegla.Add(Status);
            ActualizaSaldos();
            ActualizandoRegla = false;
            ChecaHoras();
        }

        void cIncidencias_StatusReglaEvent(List<eClockBase.Modelos.Incidencias.Model_StatusRegla> Status)
        {
            if (Status != null)
                StatusRegla = Status;
            else
                StatusRegla = new List<eClockBase.Modelos.Incidencias.Model_StatusRegla>();
            ActualizaSaldos();
            ActualizandoRegla = false;
        }

        bool ValidaRegla()
        {
            if (TipoIncidenciaSeleccionada != null)
            {
                if (!TipoIncidenciaSeleccionada.TIPO_INCIDENCIA_REGLAS)
                    return true;
                if (ActualizandoRegla)
                    return false;
                if (!TieneSaldo())
                    return false;
            }
            return true;
        }

        #endregion
    }
}
