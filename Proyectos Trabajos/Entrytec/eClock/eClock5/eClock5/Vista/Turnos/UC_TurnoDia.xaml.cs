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
using eClockBase.Modelos;

namespace eClock5.Vista.Turnos
{
    /// <summary>
    /// Lógica de interacción para UC_TurnoDia.xaml
    /// </summary>
    public partial class UC_TurnoDia : UserControl
    {
        public enum TipoComidaEnum
        {
            SinComida,
            PorTiempo,
            PorHorario,
            Avanzada
        }

        public UC_TurnoDia()
        {
            InitializeComponent();
            OcultaValidadores();
        }

        public bool MostrarCheckBox
        {
            get
            {
                if (Chk_Main_DIN.Visibility != m_Visible)
                    return false;
                return true;
            }
            set
            {
                if (value)
                {
                    Chk_Main_DIN.Visibility = m_Visible;
                    Lbl_Main_DIN.Visibility = m_Oculto;
                }
                else
                {
                    Chk_Main_DIN.Visibility = m_Oculto;
                    Lbl_Main_DIN.Visibility = m_Visible;
                }
            }
        }

        public string Texto
        {
            get
            {
                return Lbl_Main_DIN.Text;
            }
            set
            {
                Lbl_Main_DIN.Text = value;
                Chk_Main_DIN.Content = value;
            }
        }

        public bool MostrarTitulos
        {
            get
            {
                if (Grd_Main.RowDefinitions[0].Height == GridLength.Auto)
                    return true;
                return false;
            }
            set
            {
                if (value)
                    Grd_Main.RowDefinitions[0].Height = GridLength.Auto;
                else
                    Grd_Main.RowDefinitions[0].Height = new GridLength(0);
            }
        }

        private bool m_HorasExtras = false;
        public bool HorasExtras
        {
            get
            {
                return m_HorasExtras;
            }
            set
            {
                m_HorasExtras = value;
            }
        }

        private bool m_CalcularAsistencia = true;
        public bool CalcularAsistencia
        {
            get { return m_CalcularAsistencia; }
            set { m_CalcularAsistencia = value; }
        }

        private Visibility m_Oculto = Visibility.Collapsed;
        private Visibility m_Visible = Visibility.Visible;

        private TipoComidaEnum m_Comida = TipoComidaEnum.SinComida;

        public TipoComidaEnum Comida
        {
            get { return m_Comida; }
            set
            {
                m_Comida = value;
                ActualizaVista();
            }
        }



        private bool m_RestringirHorario = false;
        public bool RestringirHorario
        {
            get { return m_RestringirHorario; }
            set
            {
                m_RestringirHorario = value;
                ActualizaVista();
            }
        }

        private bool m_PorHorario = true;
        //Indica si será por horario la validacíon o por Jornada de trabajo
        public bool PorHorario
        {
            get { return m_PorHorario; }
            set
            {
                m_PorHorario = value;
                ActualizaVista();
            }
        }
        public void ActualizaVista()
        {
            if (CalcularAsistencia)
            {
                EntradaMinimaMostrar = false | !m_PorHorario | RestringirHorario;
                EntradaMostrar = true;
                EntradaMaximaMostrar = false | !m_PorHorario | RestringirHorario;

                SalidaComidaMostrar = false | Comida == TipoComidaEnum.Avanzada | Comida == TipoComidaEnum.PorHorario;
                RegresoComidaMostrar = false | Comida == TipoComidaEnum.Avanzada | Comida == TipoComidaEnum.PorHorario;
                TiempoComidaMostrar = false | Comida == TipoComidaEnum.Avanzada | Comida == TipoComidaEnum.PorTiempo;

                SalidaMinimaMostrar = false | (m_PorHorario & RestringirHorario);
                SalidaMostrar = false | m_PorHorario;
                SalidaMaximaMostrar = false | (m_PorHorario & RestringirHorario);

                JornadaMostrar = false | !m_PorHorario;
            }
            else
            {
                EntradaMinimaMostrar = false;
                EntradaMostrar = true;
                EntradaMaximaMostrar = false;

                SalidaComidaMostrar = false | Comida == TipoComidaEnum.PorHorario;
                RegresoComidaMostrar = false | Comida == TipoComidaEnum.PorHorario;
                TiempoComidaMostrar = false;

                SalidaMinimaMostrar = false;
                SalidaMostrar = true;
                SalidaMaximaMostrar = false;

                JornadaMostrar = false;
            }

            DT_EntradaMinima.IsEnabled =
                DT_Entrada.IsEnabled =
                DT_EntradaMaxima.IsEnabled =
                DT_SalidaComida.IsEnabled =
                DT_RegresoComida.IsEnabled =
                DT_TiempoComida.IsEnabled =
                DT_SalidaMinima.IsEnabled =
                DT_Salida.IsEnabled =
                DT_SalidaMaxima.IsEnabled =
                DT_Jornada.IsEnabled = EstaActivo();

            ValidaControles();
        }


        public bool EntradaMinimaMostrar
        {
            get
            {
                if (DT_EntradaMinima.Visibility != System.Windows.Visibility.Visible)
                    return false;
                return true;
            }
            set
            {
                if (value)
                {
                    DT_EntradaMinima.Visibility = m_Visible;
                    Lbl_EntradaMinima.Visibility = m_Visible;
                }
                else
                {
                    DT_EntradaMinima.Visibility = m_Oculto;
                    Lbl_EntradaMinima.Visibility = m_Oculto;
                }
            }
        }

        public bool EntradaMostrar
        {
            get
            {
                if (DT_Entrada.Visibility != System.Windows.Visibility.Visible)
                    return false;
                return true;
            }
            set
            {
                if (value)
                {
                    DT_Entrada.Visibility = m_Visible;
                    Lbl_Entrada.Visibility = m_Visible;
                }
                else
                {
                    DT_Entrada.Visibility = m_Oculto;
                    Lbl_Entrada.Visibility = m_Oculto;
                }
            }
        }

        public bool EntradaMaximaMostrar
        {
            get
            {
                if (DT_EntradaMaxima.Visibility != System.Windows.Visibility.Visible)
                    return false;
                return true;

            }
            set
            {
                if (value)
                {
                    DT_EntradaMaxima.Visibility = m_Visible;
                    Lbl_EntradaMaxima.Visibility = m_Visible;
                }
                else
                {
                    DT_EntradaMaxima.Visibility = m_Oculto;
                    Lbl_EntradaMaxima.Visibility = m_Oculto;
                }
            }
        }

        public bool SalidaComidaMostrar
        {
            get
            {
                if (DT_SalidaComida.Visibility != System.Windows.Visibility.Visible)
                    return false;
                return true;

            }
            set
            {
                if (value)
                {
                    DT_SalidaComida.Visibility = m_Visible;
                    Lbl_SalidaComida.Visibility = m_Visible;
                }
                else
                {
                    DT_SalidaComida.Visibility = m_Oculto;
                    Lbl_SalidaComida.Visibility = m_Oculto;
                }
            }
        }

        public bool RegresoComidaMostrar
        {
            get
            {
                if (DT_RegresoComida.Visibility != System.Windows.Visibility.Visible)
                    return false;
                return true;

            }
            set
            {
                if (value)
                {
                    DT_RegresoComida.Visibility = m_Visible;
                    Lbl_RegresoComida.Visibility = m_Visible;
                }
                else
                {
                    DT_RegresoComida.Visibility = m_Oculto;
                    Lbl_RegresoComida.Visibility = m_Oculto;
                }
            }
        }

        public bool TiempoComidaMostrar
        {
            get
            {
                if (DT_TiempoComida.Visibility != System.Windows.Visibility.Visible)
                    return false;
                return true;

            }
            set
            {
                if (value)
                {
                    DT_TiempoComida.Visibility = m_Visible;
                    Lbl_TiempoComida.Visibility = m_Visible;
                }
                else
                {
                    DT_TiempoComida.Visibility = m_Oculto;
                    Lbl_TiempoComida.Visibility = m_Oculto;
                }
            }
        }


        public bool SalidaMinimaMostrar
        {
            get
            {
                if (DT_SalidaMinima.Visibility != System.Windows.Visibility.Visible)
                    return false;
                return true;

            }
            set
            {
                if (value)
                {
                    DT_SalidaMinima.Visibility = m_Visible;
                    Lbl_SalidaMinima.Visibility = m_Visible;
                }
                else
                {
                    DT_SalidaMinima.Visibility = m_Oculto;
                    Lbl_SalidaMinima.Visibility = m_Oculto;
                }
            }
        }

        public bool SalidaMostrar
        {
            get
            {
                if (DT_Salida.Visibility != System.Windows.Visibility.Visible)
                    return false;
                return true;

            }
            set
            {
                if (value)
                {
                    DT_Salida.Visibility = m_Visible;
                    Lbl_Salida.Visibility = m_Visible;
                }
                else
                {
                    DT_Salida.Visibility = m_Oculto;
                    Lbl_Salida.Visibility = m_Oculto;
                }
            }
        }


        public bool SalidaMaximaMostrar
        {
            get
            {
                if (DT_SalidaMaxima.Visibility != System.Windows.Visibility.Visible)
                    return false;
                return true;

            }
            set
            {
                if (value)
                {
                    DT_SalidaMaxima.Visibility = m_Visible;
                    Lbl_SalidaMaxima.Visibility = m_Visible;
                }
                else
                {
                    DT_SalidaMaxima.Visibility = m_Oculto;
                    Lbl_SalidaMaxima.Visibility = m_Oculto;
                }
            }
        }

        public bool JornadaMostrar
        {
            get
            {
                if (DT_Jornada.Visibility != System.Windows.Visibility.Visible)
                    return false;
                return true;

            }
            set
            {
                if (value)
                {
                    DT_Jornada.Visibility = m_Visible;
                    Lbl_Jornada.Visibility = m_Visible;
                }
                else
                {
                    DT_Jornada.Visibility = m_Oculto;
                    Lbl_Jornada.Visibility = m_Oculto;
                }
            }
        }





        eClockBase.Modelos.Model_TURNOS_DIA m_TurnoDia = null;
        public bool LimpiaVista()
        {
            Chk_Main_DIN.IsChecked = false;
            m_PorHorario = true;
            m_RestringirHorario = false;
            m_Comida = TipoComidaEnum.SinComida;

            DT_EntradaMinima.Value =
            DT_Entrada.Value =
            DT_EntradaMaxima.Value =
            DT_SalidaComida.Value =
            DT_RegresoComida.Value =
            DT_TiempoComida.Value =
            DT_SalidaMinima.Value =
            DT_Salida.Value =
            DT_SalidaMaxima.Value =
            DT_Jornada.Value = CeC.FechaNula;
            ActualizaVista();
            return true;
        }
        public bool ValoresPredeterminados()
        {
            LimpiaVista();
            DT_Entrada.ValueTimeSpan = new TimeSpan(9, 0, 0);
            DT_Salida.ValueTimeSpan = new TimeSpan(19, 0, 0);
            return true;
        }

        public bool Modelo2Vista(eClockBase.Modelos.Model_TURNOS_DIA TurnoDia)
        {
            m_TurnoDia = TurnoDia;
            LimpiaVista();
            Chk_Main_DIN.IsChecked = true;

            DT_EntradaMinima.Value = TurnoDia.TURNO_DIA_HEMIN;
            DT_Entrada.Value = TurnoDia.TURNO_DIA_HE;
            DT_EntradaMaxima.Value = TurnoDia.TURNO_DIA_HEMAX;

            DT_SalidaComida.Value = TurnoDia.TURNO_DIA_HCS;
            DT_RegresoComida.Value = TurnoDia.TURNO_DIA_HCR;
            DT_TiempoComida.Value = TurnoDia.TURNO_DIA_HCTIEMPO;

            DT_SalidaMinima.Value = TurnoDia.TURNO_DIA_HSMIN;
            DT_Salida.Value = TurnoDia.TURNO_DIA_HS;
            DT_SalidaMaxima.Value = TurnoDia.TURNO_DIA_HSMAX;

            DT_Jornada.Value = TurnoDia.TURNO_DIA_HTIEMPO;
            ValidaControles();
            return true;
        }

        public Model_TURNOS_DIA Vista2Modelo(TimeSpan HERetardo, TimeSpan HERetardoB, TimeSpan HERetardoC, TimeSpan HERetardoD, TimeSpan HBloque, TimeSpan HCTolerancia, TimeSpan ToleranciaBloque)
        {
            if (!EstaActivo())
                return null;
            eClockBase.Modelos.Model_TURNOS_DIA TD_Horario = new Model_TURNOS_DIA();

            // Entrada
            TD_Horario.TURNO_DIA_HE = DT_Entrada.Value;

            if (EntradaMinimaMostrar)
                TD_Horario.TURNO_DIA_HEMIN = DT_EntradaMinima.Value;
            else
                TD_Horario.TURNO_DIA_HEMIN = ObtenEntradaMinima(DT_Entrada.Value);

            if (EntradaMaximaMostrar)
                TD_Horario.TURNO_DIA_HEMAX = DT_EntradaMaxima.Value;
            else
                TD_Horario.TURNO_DIA_HEMAX = DT_Salida.Value.AddHours(-1);

            // Retardos
            if (PorHorario)
            {
                TD_Horario.TURNO_DIA_HERETARDO = DT_Entrada.Value.Add(HERetardo);
                TD_Horario.TURNO_DIA_HERETARDO_B = DT_Entrada.Value.Add(HERetardoB);
                TD_Horario.TURNO_DIA_HERETARDO_C = DT_Entrada.Value.Add(HERetardoC);
                TD_Horario.TURNO_DIA_HERETARDO_D = DT_Entrada.Value.Add(HERetardoD); 
            }
            else
            {
                TD_Horario.TURNO_DIA_HERETARDO = TD_Horario.TURNO_DIA_HEMAX;
                TD_Horario.TURNO_DIA_HERETARDO_B = TD_Horario.TURNO_DIA_HEMAX;
                TD_Horario.TURNO_DIA_HERETARDO_C = TD_Horario.TURNO_DIA_HEMAX;
                TD_Horario.TURNO_DIA_HERETARDO_D = TD_Horario.TURNO_DIA_HEMAX;
            }
            // Salida
            if (PorHorario)
                TD_Horario.TURNO_DIA_HS = DT_Salida.Value;
            else
                TD_Horario.TURNO_DIA_HS = DT_Entrada.Value.Add(DT_Jornada.ValueTimeSpan);

            if (SalidaMinimaMostrar)
                TD_Horario.TURNO_DIA_HSMIN = DT_SalidaMinima.Value;
            else
                TD_Horario.TURNO_DIA_HSMIN = TD_Horario.TURNO_DIA_HS.AddHours(-1);


            if (SalidaMaximaMostrar)
                TD_Horario.TURNO_DIA_HSMAX = DT_SalidaMaxima.Value;
            else
                TD_Horario.TURNO_DIA_HSMAX = ObtenSalidaMaxima(DT_Entrada.Value, DT_Salida.Value);
            // Bloque
            if (PorHorario)
                TD_Horario.TURNO_DIA_HBLOQUE = CeC.FechaNula;
            else
                TD_Horario.TURNO_DIA_HBLOQUE = CeC.TimeSpan2DateTime(HBloque);

            //Tolerancia al bloque
            if (PorHorario)
                TD_Horario.TURNO_DIA_HBLOQUET = CeC.FechaNula;
            else
                TD_Horario.TURNO_DIA_HBLOQUET = CeC.TimeSpan2DateTime(ToleranciaBloque);

            if (PorHorario)
                TD_Horario.TURNO_DIA_HTIEMPO = CeC.TimeSpan2DateTime(DT_Salida.ValueTimeSpan - DT_Entrada.ValueTimeSpan);
            else
                TD_Horario.TURNO_DIA_HTIEMPO = DT_Jornada.Value;

            // Comida
            TD_Horario.TURNO_DIA_HAYCOMIDA = Comida == TipoComidaEnum.SinComida ? false : true;

            if(SalidaComidaMostrar)
                TD_Horario.TURNO_DIA_HCS = DT_SalidaComida.Value;
            else
                TD_Horario.TURNO_DIA_HCS = CeC.FechaNula;

            if (RegresoComidaMostrar)
                TD_Horario.TURNO_DIA_HCR =DT_RegresoComida.Value;
            else
                TD_Horario.TURNO_DIA_HCR = CeC.FechaNula;

            if (TiempoComidaMostrar)
                TD_Horario.TURNO_DIA_HCTIEMPO = DT_TiempoComida.Value;
            else
                TD_Horario.TURNO_DIA_HCTIEMPO = CeC.TimeSpan2DateTime(TD_Horario.TURNO_DIA_HCR - TD_Horario.TURNO_DIA_HCS);
            
            if(TD_Horario.TURNO_DIA_HAYCOMIDA)
                TD_Horario.TURNO_DIA_HCTOLERA = CeC.TimeSpan2DateTime(HCTolerancia);
            else
                TD_Horario.TURNO_DIA_HCTOLERA = CeC.FechaNula;

            // Permite Horas Extras
            TD_Horario.TURNO_DIA_PHEX = HorasExtras;
            // Calcula asistencia
            TD_Horario.TURNO_DIA_NO_ASIS = !CalcularAsistencia;



            return TD_Horario;
        }


        #region Valida Entradas Minimas y Salidas Maximas
        /// <summary>
        /// Obtiene la Saida Maxima para el Dia
        /// </summary>
        /// <param name="HoraEntradaDia">Hora del dia seleccionado</param>
        /// <param name="Chb_HorarioSiguienteDia">CheckBox que indica si al dia siguiente hay horario</param>
        /// <returns></returns>
        private DateTime ObtenSalidaMaxima(DateTime HoraEntradaDia, DateTime HoraSalidaDia)
        {
            try
            {
                if (CeC.Convierte2Bool(EsDiaSiguiente(HoraEntradaDia, HoraSalidaDia)))
                    return ObtenEntradaMinima(HoraEntradaDia);
                return ObtenEntradaMinima(HoraEntradaDia).AddDays(1);
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
                return CeC.FechaNula;
            }
        }
        bool EsDiaSiguiente(DateTime HoraEntradaDia, DateTime HoraSalidaDia)
        {
            bool DiaSiguiente = false;
            TimeSpan HorasATrabajar = HoraEntradaDia - HoraSalidaDia;
            if (HorasATrabajar.Hours > 24)
                DiaSiguiente = true;
            return DiaSiguiente;
        }
        /// <summary>
        /// Obtiene la Salida Maxima para el dia
        /// </summary>
        /// <param name="HoraEntradaDia">Hora del dia seleccionado</param>
        /// <param name="HoraEntradaDiaSiguiente">Hora del dia siguiente</param>
        /// <param name="Chb_Dia">CheckBox que indica si ese dia hay horario</param>
        /// <param name="Chb_DiaSiguiente">CheckBox que indica si al dia siguiente hay horario</param>
        /// <returns></returns>
        private DateTime ObtenSalidaMaxima(DateTime HoraEntradaDia, DateTime HoraSalidaDia, DateTime HoraEntradaDiaSiguiente, CheckBox Chb_Dia)
        {
            try
            {
                if (CeC.Convierte2Bool(EsDiaSiguiente(HoraEntradaDia, HoraSalidaDia)))
                    return ObtenEntradaMinima(HoraEntradaDiaSiguiente);
                return ObtenEntradaMinima(HoraEntradaDia).AddDays(1);
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
                return CeC.FechaNula;
            }
        }
        /// <summary>
        /// Obtiene la Entrada Minima para el dia
        /// </summary>
        /// <param name="HoraEntradaDia">Hora de Entrada del dia seleccionado</param>
        /// <returns></returns>
        private DateTime ObtenEntradaMinima(DateTime HoraEntradaDia)
        {
            try
            {
                return HoraEntradaDia.AddHours(-3);
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
                return CeC.FechaNula;
            }
        }
        #endregion

        public bool EstaActivo()
        {
            return !Chk_Main_DIN.IsVisible | CeC.Convierte2Bool(Chk_Main_DIN.IsChecked) ? true : false;
        }
        private void ActualizaChecado(object sender, RoutedEventArgs e)
        {
            ActualizaVista();

        }
        bool m_Validando = false;
        public void OcultaValidadores()
        {
            Lbl_SalidaMenorEntrada.Visibility = m_Oculto;
            Lbl_EntradaMinimaMayorEntrada.Visibility = m_Oculto;
            Lbl_EntradaMaximaMenorEntrada.Visibility = m_Oculto;
            Lbl_SalidaMinimaMenorEntradaMaxima.Visibility = m_Oculto;
            Lbl_SalidaMinimaMayorSalida.Visibility = m_Oculto;
            Lbl_SalidaMaximaManorSalida.Visibility = m_Oculto;
            Lbl_SalidaComidaMenorEntrada.Visibility = m_Oculto;
            Lbl_RegresoComidaMenorSalidaComida.Visibility = m_Oculto;
        }
        public bool ValidaControles()
        {

            OcultaValidadores();

            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                return true;

            bool Correcto = true;

            if (SalidaMostrar && DT_Salida.Value < DT_Entrada.Value)
            {
                Lbl_SalidaMenorEntrada.Visibility = m_Visible;
                Correcto = false;
            }

            if (EntradaMinimaMostrar && DT_EntradaMinima.Value > DT_Entrada.Value)
            {
                Lbl_EntradaMinimaMayorEntrada.Visibility = m_Visible;
                Correcto = false;
            }

            if (EntradaMaximaMostrar && DT_EntradaMaxima.Value < DT_Entrada.Value)
            {
                Lbl_EntradaMaximaMenorEntrada.Visibility = m_Visible;
                Correcto = false;
            }

            if (SalidaMinimaMostrar && DT_SalidaMinima.Value < DT_EntradaMaxima.Value)
            {
                Lbl_SalidaMinimaMenorEntradaMaxima.Visibility = m_Visible;
                Correcto = false;
            }

            if (SalidaMinimaMostrar && DT_SalidaMinima.Value > DT_Salida.Value)
            {
                Lbl_SalidaMinimaMayorSalida.Visibility = m_Visible;
                Correcto = false;
            }

            if (SalidaMaximaMostrar && DT_SalidaMaxima.Value < DT_Salida.Value)
            {
                Lbl_SalidaMaximaManorSalida.Visibility = m_Visible;
                Correcto = false;
            }

            if (SalidaComidaMostrar && DT_SalidaComida.Value < DT_Entrada.Value)
            {
                Lbl_SalidaComidaMenorEntrada.Visibility = m_Visible;
                Correcto = false;
            }

            if (RegresoComidaMostrar && DT_RegresoComida.Value < DT_SalidaComida.Value)
            {
                Lbl_RegresoComidaMenorSalidaComida.Visibility = m_Visible;
                Correcto = false;
            }



            return Correcto;
        }

        public void Localiza()
        {

            BaseModificada.Localizaciones.sLocaliza(this);
        }
        private void ValorCambio(object sender, RoutedEventArgs e)
        {
            ValidaControles();
        }

    }
}

