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
using eClockBase;
using eClockBase.Modelos;
using Newtonsoft.Json;

namespace eClock5.Vista.Turnos
{
    /// <summary>
    /// Lógica de interacción para Edicion.xaml
    /// </summary>
    public partial class Edicion : UserControl
    {
        /// <summary>
        /// Numero de datos en EC_DIAS_SEMANA
        /// </summary>
        private static uint EC_DIAS_SEMANA = 9;
        /// <summary>
        /// Numero de dias de la semana (7 dias de Domingo a Sabado)
        /// </summary>
        private static uint NUMERO_DIAS_SEMANA = 7;
        #region Modelos
        /// <summary>
        /// Modelo con los datos generales del turno
        /// </summary>
        private static eClockBase.Modelos.Turnos.Model_Turno EC_TURNOS = new eClockBase.Modelos.Turnos.Model_Turno();
        /// <summary>
        /// Modelo con los detalles del turno
        /// </summary>
        private static Model_TURNOS_DIA EC_TURNOS_DIA = new Model_TURNOS_DIA();
        /// <summary>
        /// Modelo con la relacion del EC_TURNOS y EC_TURNOS_DIA
        /// </summary>
        private static Model_TURNOS_SEMANAL_DIA EC_TURNOS_SEMANAL_DIA = new Model_TURNOS_SEMANAL_DIA();


        #endregion
        /// <summary>
        /// Indica si el Turno es Nuevo o si se esta editando un Turno existente. Para crear un nuevo turno se asigna el valor: -1
        /// </summary>
        /// 
        private bool m_EsNuevo = true;
        public bool EsNuevo
        {
            get { return m_EsNuevo; }
            set { m_EsNuevo = value; }
        }
        /// <summary>
        /// Inidca el tipo de Turno según el catalogo de Turnos siguientes: 
        /// 0	No Asignado
        /// 1	Semanal
        /// 2	Flexible
        /// 3	Diario
        /// 4	Abierto
        /// 5	Simple
        /// </summary>
        private int m_TipoTurno;
        public int TipoTurno
        {
            get
            {
                if (CeC.Convierte2Bool(Rbn_Horario.IsChecked))
                {
                    //
                    if (CeC.Convierte2Bool(Chb_RestringeAcceso.IsChecked) && CeC.Convierte2Bool(Rbn_SinComida.IsChecked) && CeC.Convierte2Bool(Chb_MismoHorario.IsChecked))
                    {
                        m_TipoTurno = 5;
                    }
                    else
                    {
                        m_TipoTurno = 1;
                    }
                }
                else
                {
                    m_TipoTurno = 2;
                }
                return m_TipoTurno;
            }
            set
            {
                m_TipoTurno = value;
            }
        }
        #region ID de Tablas de Turnos
        /// <summary>
        /// Propiedad que guarda o estable el Identificador del Turno.
        /// Se usa el valor -1 para Turnos Nuevos
        /// </summary>
        private int m_TurnoID = -1;

        public int TurnoID
        {
            get { return m_TurnoID; }
            set { m_TurnoID = value; }
        }
        /// <summary>
        /// Propiedad que guarda o estable el Identificador del Turno Dia
        /// Se usa el valor -1 para Turnos Nuevos
        /// </summary>
        private int m_TurnoDiaID = -1;

        public int TurnoDiaID
        {
            get { return m_TurnoDiaID; }
            set { m_TurnoDiaID = value; }
        }
        /// <summary>
        /// Propiedad que guarda o estable el Identificador del Turno Semanal Dia
        /// Se usa el valor -1 para Turnos Nuevos. Siempre se crea un nuevo TurnoSemanalDiaID para cada creación o modificación del horario
        /// </summary>
        private int m_TurnoSemanalDiaID = -1;

        public int TurnosSemanalDiaID
        {
            get { return m_TurnoSemanalDiaID; }
            set { m_TurnoSemanalDiaID = value; }
        }
        #endregion
        /// <summary>
        /// Guarda los dias de la semana que tiene horario
        /// 0	Ninguno
        /// 1	Domingo
        /// 2	Lunes
        /// 3	Martes
        /// 4	Miercoles
        /// 5	Jueves
        /// 6	Viernes
        /// 7	Sabado
        /// 8	Todos los dias
        /// </summary>


        /// <summary>
        /// Propiedad que guarda o establece si hay horario de comida en el Turno
        /// </summary>
        private bool m_HayComida;

        public bool HayComida
        {
            get
            {
                if (CeC.Convierte2Bool(Rbn_SinComida.IsChecked))
                    return false;
                return true;
            }
            set { m_HayComida = value; }
        }

        /// <summary>
        /// Propidad que guarda o establece si el Turno es individual.
        /// </summary>
        private bool m_TURNO_INDIVIDUAL = false;

        public bool TURNO_INDIVIDUAL
        {
            get { return m_TURNO_INDIVIDUAL; }
            set { m_TURNO_INDIVIDUAL = value; }
        }


        public Edicion()
        {
            InitializeComponent();
            Loaded += Edicion_Loaded;
            IsVisibleChanged += Edicion_IsVisibleChanged;
        }

        void Edicion_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.Equals(true) && Sesion != null)
            {
                CargaTurno();
            }
        }
        void Edicion_Loaded(object sender, RoutedEventArgs e)
        {
            
            CargaTurno();
        }

        void LimpiaVista()
        {
            Tbx_TURNO_NOMBRE.Text ="";
            Tbx_TURNO_GRUPOS.Text = "";
            Chb_TURNO_ASISTENCIA.IsChecked = true;
            Chb_TURNO_PHEXTRAS.IsChecked = false;
            CP_TURNO_COLOR.iColorActual = 0;
            Chb_TURNO_BORRADO.IsChecked = false;

            Rbn_SinComida.IsChecked = true;
            TD_Lunes.LimpiaVista();
            TD_Martes.LimpiaVista();
            TD_Miercoles.LimpiaVista();
            TD_Jueves.LimpiaVista();
            TD_Viernes.LimpiaVista();
            TD_Sabado.LimpiaVista();
            TD_Domingo.LimpiaVista();

            TD_Dias.ValoresPredeterminados();

            DT_TURNO_DIA_HBLOQUE.ValueTimeSpan = new TimeSpan(0, 30, 0);
            DT_TOLERANCIABLOQUE.ValueTimeSpan = new TimeSpan(0, 6, 0);
            DT_TURNO_DIA_HERETARDO.ValueTimeSpan = new TimeSpan(0, 10, 0);
            DT_TURNO_DIA_HERETARDO_B.ValueTimeSpan = new TimeSpan(0, 30, 0);
            DT_TURNO_DIA_HERETARDO_C.ValueTimeSpan = new TimeSpan(1, 00, 0);
            DT_TURNO_DIA_HERETARDO_D.ValueTimeSpan = new TimeSpan(2, 00, 0);
            DT_TURNO_DIA_HCTOLERA.ValueTimeSpan = new TimeSpan(0, 10, 0);


            Chb_HorarioLunes.IsChecked =
                Chb_HorarioMartes.IsChecked =
                Chb_HorarioMiercoles.IsChecked =
                Chb_HorarioJueves.IsChecked =
                Chb_HorarioViernes.IsChecked =
                Chb_HorarioSabado.IsChecked =
                Chb_HorarioDomingo.IsChecked = false;
            Chb_MismoHorario.IsChecked = true;
            ActualizaVista();

        }
        void Turno_ObtenDatosFinalizado(eClockBase.Modelos.Turnos.Model_Turno DatosTurno)
        {
            try
            {
                Tbx_TURNO_NOMBRE.Text = DatosTurno.Turno.TURNO_NOMBRE;
                Tbx_TURNO_GRUPOS.Text = DatosTurno.Turno.TURNO_GRUPOS;
                Chb_TURNO_ASISTENCIA.IsChecked = DatosTurno.Turno.TURNO_ASISTENCIA;
                Chb_TURNO_PHEXTRAS.IsChecked = DatosTurno.Turno.TURNO_PHEXTRAS;
                CP_TURNO_COLOR.iColorActual = DatosTurno.Turno.TURNO_COLOR;
                EC_TURNOS.Turno.TURNO_BORRADO = CeC.Convierte2Bool(Chb_TURNO_BORRADO.IsChecked);
                DT_TURNO_DIA_HBLOQUE.Value = CeC.Convierte2DateTime(DatosTurno.TurnoDias.ElementAt<Model_TURNOS_DIA>(0).TURNO_DIA_HBLOQUE);


                if (DatosTurno.TurnoDias.Count > 0)
                {
                    eClockBase.Modelos.Model_TURNOS_DIA TurnoDiaPrimero = DatosTurno.TurnoDias.ElementAt<Model_TURNOS_DIA>(0);
                    DT_TURNO_DIA_HBLOQUE.Value = TurnoDiaPrimero.TURNO_DIA_HBLOQUE;
                    DT_TURNO_DIA_HERETARDO.ValueTimeSpan = TurnoDiaPrimero.TURNO_DIA_HERETARDO - TurnoDiaPrimero.TURNO_DIA_HE;
                    DT_TURNO_DIA_HERETARDO_B.ValueTimeSpan = TurnoDiaPrimero.TURNO_DIA_HERETARDO_B - TurnoDiaPrimero.TURNO_DIA_HE;
                    DT_TURNO_DIA_HERETARDO_C.ValueTimeSpan = TurnoDiaPrimero.TURNO_DIA_HERETARDO_C - TurnoDiaPrimero.TURNO_DIA_HE;
                    DT_TURNO_DIA_HERETARDO_D.ValueTimeSpan = TurnoDiaPrimero.TURNO_DIA_HERETARDO_D - TurnoDiaPrimero.TURNO_DIA_HE;
                    DT_TOLERANCIABLOQUE.Value = TurnoDiaPrimero.TURNO_DIA_HBLOQUET;
                    DT_TURNO_DIA_HCTOLERA.ValueTimeSpan = TurnoDiaPrimero.TURNO_DIA_HCTOLERA - CeC.FechaNula;
                    TD_Dias.Modelo2Vista(TurnoDiaPrimero);
                    if (TurnoDiaPrimero.TURNO_DIA_HAYCOMIDA)
                    {
                        if (TurnoDiaPrimero.TURNO_DIA_HCS != TurnoDiaPrimero.TURNO_DIA_HCR && CeC.DateTime2TimeSpan(TurnoDiaPrimero.TURNO_DIA_HCTIEMPO) != (TurnoDiaPrimero.TURNO_DIA_HCR - TurnoDiaPrimero.TURNO_DIA_HCS))
                        {
                            Rbn_ComidaAvanzado.IsChecked = true;
                        }
                        else
                            if (CeC.DateTime2TimeSpan(TurnoDiaPrimero.TURNO_DIA_HCTIEMPO) != (TurnoDiaPrimero.TURNO_DIA_HCR - TurnoDiaPrimero.TURNO_DIA_HCS))
                            {
                                Rbn_ComidaPorTiempo.IsChecked = true;
                            }
                            else
                            {
                                Rbn_ComidaHorarioFijo.IsChecked = true;
                            }

                    }
                    else
                        Rbn_SinComida.IsChecked = true;
                    
                    if (DatosTurno.TurnoDias.Count == 1)
                        Chb_MismoHorario.IsChecked = true;
                    else
                        Chb_MismoHorario.IsChecked = false;
                    foreach (eClockBase.Modelos.Model_TURNOS_SEMANAL_DIA TurnoSemanalDia in DatosTurno.TurnoSemanalDias)
                    {
                        foreach (eClockBase.Modelos.Model_TURNOS_DIA TurnoDia in DatosTurno.TurnoDias)
                        {
                            if (TurnoDia.TURNO_DIA_ID == TurnoSemanalDia.TURNO_DIA_ID)
                            {
                                switch (TurnoSemanalDia.DIA_SEMANA_ID)
                                {
                                    case 1:
                                        TD_Domingo.Modelo2Vista(TurnoDia);
                                        Chb_HorarioDomingo.IsChecked = true;
                                        break;
                                    case 2:
                                        TD_Lunes.Modelo2Vista(TurnoDia);
                                        Chb_HorarioLunes.IsChecked = true;
                                        break;
                                    case 3:
                                        TD_Martes.Modelo2Vista(TurnoDia);
                                        Chb_HorarioMartes.IsChecked = true;
                                        break;
                                    case 4:
                                        TD_Miercoles.Modelo2Vista(TurnoDia);
                                        Chb_HorarioMiercoles.IsChecked = true;
                                        break;
                                    case 5:
                                        TD_Jueves.Modelo2Vista(TurnoDia);
                                        Chb_HorarioJueves.IsChecked = true;
                                        break;
                                    case 6:
                                        TD_Viernes.Modelo2Vista(TurnoDia);
                                        Chb_HorarioViernes.IsChecked = true;
                                        break;
                                    case 7:
                                        TD_Sabado.Modelo2Vista(TurnoDia);
                                        Chb_HorarioSabado.IsChecked = true;
                                        break;
                                }
                            }
                        }

                    }
                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }
            ActualizaVista();
        }


        private Visibility m_Oculto = Visibility.Collapsed;
        private Visibility m_Visible = Visibility.Visible;

        public void ActualizaVista()
        {


            ///Valida si se debe restringir el acceso
            bool Asistencia = eClockBase.CeC.Convierte2Bool(Chb_TURNO_ASISTENCIA.IsChecked, false);
            if (!Asistencia)
            {
                Rbn_Horario.IsChecked =
                    Rbn_SinComida.IsChecked = true;

                Chb_RestringeAcceso.IsChecked = false;
            }
            bool PorHorario = eClockBase.CeC.Convierte2Bool(Rbn_Horario.IsChecked, false);
            if (!PorHorario)
                Chb_RestringeAcceso.IsChecked = false;
            bool RestringirHorario = eClockBase.CeC.Convierte2Bool(Chb_RestringeAcceso.IsChecked, false);

            TD_Lunes.CalcularAsistencia =
                TD_Martes.CalcularAsistencia =
                TD_Miercoles.CalcularAsistencia =
                TD_Jueves.CalcularAsistencia =
                TD_Viernes.CalcularAsistencia =
                TD_Sabado.CalcularAsistencia =
                TD_Domingo.CalcularAsistencia =
                TD_Dias.CalcularAsistencia = Asistencia;


            UC_TurnoDia.TipoComidaEnum TipoComida = UC_TurnoDia.TipoComidaEnum.SinComida;
            if (eClockBase.CeC.Convierte2Bool(Rbn_ComidaAvanzado.IsChecked, false))
                TipoComida = UC_TurnoDia.TipoComidaEnum.Avanzada;
            if (eClockBase.CeC.Convierte2Bool(Rbn_ComidaHorarioFijo.IsChecked, false))
                TipoComida = UC_TurnoDia.TipoComidaEnum.PorHorario;
            if (eClockBase.CeC.Convierte2Bool(Rbn_ComidaPorTiempo.IsChecked, false))
                TipoComida = UC_TurnoDia.TipoComidaEnum.PorTiempo;

            bool MismoHorario = eClockBase.CeC.Convierte2Bool(Chb_MismoHorario.IsChecked, false);

            TD_Lunes.RestringirHorario =
                TD_Martes.RestringirHorario =
                TD_Miercoles.RestringirHorario =
                TD_Jueves.RestringirHorario =
                TD_Viernes.RestringirHorario =
                TD_Sabado.RestringirHorario =
                TD_Domingo.RestringirHorario =
                TD_Dias.RestringirHorario = RestringirHorario;



            TD_Dias.PorHorario =
            TD_Lunes.PorHorario =
                TD_Martes.PorHorario =
            TD_Miercoles.PorHorario =
            TD_Jueves.PorHorario =
            TD_Viernes.PorHorario =
            TD_Sabado.PorHorario =
            TD_Domingo.PorHorario = PorHorario;

            Lbl_TOLERANCIABLOQUE.Visibility =
                DT_TOLERANCIABLOQUE.Visibility =
                Lbl_TURNO_DIA_HBLOQUE.Visibility =
                DT_TURNO_DIA_HBLOQUE.Visibility = PorHorario ? m_Oculto : m_Visible;

            Lbl_TURNO_DIA_HERETARDO.Visibility =
                DT_TURNO_DIA_HERETARDO.Visibility =
                Chb_RestringeAcceso.Visibility = PorHorario ? m_Visible : m_Oculto;
            //Temporalmente no se muestran los retardos avanzados
            Lbl_TURNO_DIA_HERETARDO_B.Visibility =
                DT_TURNO_DIA_HERETARDO_B.Visibility =
                Lbl_TURNO_DIA_HERETARDO_C.Visibility =
                DT_TURNO_DIA_HERETARDO_C.Visibility =
                Lbl_TURNO_DIA_HERETARDO_D.Visibility =
                DT_TURNO_DIA_HERETARDO_D.Visibility =  m_Oculto;

            TD_Dias.Comida =
            TD_Lunes.Comida =
            TD_Martes.Comida =
            TD_Miercoles.Comida =
            TD_Jueves.Comida =
            TD_Viernes.Comida =
            TD_Sabado.Comida =
            TD_Domingo.Comida = TipoComida;
            Lbl_TURNO_DIA_HCTOLERA.Visibility =
                DT_TURNO_DIA_HCTOLERA.Visibility =
                TipoComida == UC_TurnoDia.TipoComidaEnum.SinComida ? m_Oculto : m_Visible;


            Grd_Dia.Visibility = MismoHorario ? m_Visible : m_Oculto;
            Stp_Dias.Visibility = MismoHorario ? m_Oculto : m_Visible;
        }


        public void Regresar()
        {
            this.Visibility = System.Windows.Visibility.Hidden;
        }

        private void ActualizaVista(object sender, RoutedEventArgs e)
        {
            ActualizaVista();
        }

        private void UC_ToolBar_OnEventClickToolBar(UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Regresar":
                    Regresar();
                    break;
                case "Btn_Guardar":
                    GuardaTurno();
                    break;

            }
        }

        CeC_SesionBase Sesion;
        /// <summary>
        /// Carga los datos del Turno
        /// </summary>
        private void CargaTurno()
        {
            try
            {
                LimpiaVista();
                Sesion = CeC_Sesion.ObtenSesion(this);
                eClockBase.Controladores.Turnos Turno = new eClockBase.Controladores.Turnos(Sesion);
                Turno.ObtenDatosFinalizado += Turno_ObtenDatosFinalizado;
                Clases.Parametros Param = Clases.Parametros.Tag2Parametros(this.Tag);
                // Se llenar los campos con valores predeterminados
                eClockBase.Modelos.Model_TURNOS TurnoSeleccionado = eClockBase.Controladores.CeC_ZLib.Json2Object<eClockBase.Modelos.Model_TURNOS>(Param.Parametro.ToString());
                //int TurnoID = CeC.Convierte2Int(Param.Parametro);
                if (TurnoSeleccionado == null)
                {
                    this.EsNuevo = true;
                    //return;
                }
                if (TurnoSeleccionado.TURNO_ID > 0)
                {
                    this.EsNuevo = false;
                    this.TurnoID = TurnoSeleccionado.TURNO_ID;
                    Turno.ObtenDatos(TurnoSeleccionado.TURNO_ID);
                }
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }

        }

        bool AgregaDia(Model_TURNOS_DIA TurnoDia, int TurnoSemanaDiaID)
        {
            if (TurnoDia == null)
                return false;
            EC_TURNOS.TurnoSemanalDias.Add(new Model_TURNOS_SEMANAL_DIA(EC_TURNOS.Turno.TURNO_ID, -1, TurnoSemanaDiaID));
            EC_TURNOS.TurnoDias.Add(TurnoDia);
            return true;
        }
        /// <summary>
        /// Guarda los datos del Turno. Si es una edición de datos los actualiza.
        /// </summary>
        private bool GuardaTurno()
        {
            EC_TURNOS = new eClockBase.Modelos.Turnos.Model_Turno();

            // EC_TURNOS
            if (this.EsNuevo)
            {
                EC_TURNOS.Turno.TURNO_ID = -1;
            }
            else
            {
                EC_TURNOS.Turno.TURNO_ID = this.TurnoID;
            }

            EC_TURNOS.Turno.TIPO_TURNO_ID = this.TipoTurno;
            EC_TURNOS.Turno.TURNO_NOMBRE = Tbx_TURNO_NOMBRE.Text;
            EC_TURNOS.Turno.TURNO_ASISTENCIA = CeC.Convierte2Bool(Chb_TURNO_ASISTENCIA.IsChecked);
            EC_TURNOS.Turno.TURNO_PHEXTRAS = CeC.Convierte2Bool(Chb_TURNO_PHEXTRAS.IsChecked);
            EC_TURNOS.Turno.TURNO_INDIVIDUAL = this.TURNO_INDIVIDUAL;
            EC_TURNOS.Turno.TURNO_GRUPOS = Tbx_TURNO_GRUPOS.Text;
            EC_TURNOS.Turno.TURNO_COLOR = CP_TURNO_COLOR.iColorActual;
            EC_TURNOS.Turno.TURNO_BORRADO = CeC.Convierte2Bool(Chb_TURNO_BORRADO.IsChecked);

            TimeSpan HERETARDO = DT_TURNO_DIA_HERETARDO.ValueTimeSpan;
            TimeSpan HERETARDO_B = DT_TURNO_DIA_HERETARDO_B.ValueTimeSpan;
            TimeSpan HERETARDO_C = DT_TURNO_DIA_HERETARDO_C.ValueTimeSpan;
            TimeSpan HERETARDO_D = DT_TURNO_DIA_HERETARDO_D.ValueTimeSpan;
            TimeSpan HBLOQUE = DT_TURNO_DIA_HBLOQUE.ValueTimeSpan;
            TimeSpan HCTOLERA = DT_TURNO_DIA_HCTOLERA.ValueTimeSpan;
            TimeSpan TOLERANCIABLOQUE = DT_TOLERANCIABLOQUE.ValueTimeSpan;

            if (CeC.Convierte2Bool(Chb_MismoHorario.IsChecked))
            {
                if (!TD_Dias.ValidaControles())
                    return NoSeValidoSatisfactoriamente();
                EC_TURNOS.TurnoDias.Add(
                TD_Dias.Vista2Modelo(HERETARDO, HERETARDO_B, HERETARDO_C, HERETARDO_D, HBLOQUE, HCTOLERA, TOLERANCIABLOQUE)
                );
                if (CeC.Convierte2Bool(Chb_HorarioDomingo.IsChecked))
                    EC_TURNOS.TurnoSemanalDias.Add(new Model_TURNOS_SEMANAL_DIA(EC_TURNOS.Turno.TURNO_ID, -1, 1));
                if (CeC.Convierte2Bool(Chb_HorarioLunes.IsChecked))
                    EC_TURNOS.TurnoSemanalDias.Add(new Model_TURNOS_SEMANAL_DIA(EC_TURNOS.Turno.TURNO_ID, -1, 2));
                if (CeC.Convierte2Bool(Chb_HorarioMartes.IsChecked))
                    EC_TURNOS.TurnoSemanalDias.Add(new Model_TURNOS_SEMANAL_DIA(EC_TURNOS.Turno.TURNO_ID, -1, 3));
                if (CeC.Convierte2Bool(Chb_HorarioMiercoles.IsChecked))
                    EC_TURNOS.TurnoSemanalDias.Add(new Model_TURNOS_SEMANAL_DIA(EC_TURNOS.Turno.TURNO_ID, -1, 4));
                if (CeC.Convierte2Bool(Chb_HorarioJueves.IsChecked))
                    EC_TURNOS.TurnoSemanalDias.Add(new Model_TURNOS_SEMANAL_DIA(EC_TURNOS.Turno.TURNO_ID, -1, 5));
                if (CeC.Convierte2Bool(Chb_HorarioViernes.IsChecked))
                    EC_TURNOS.TurnoSemanalDias.Add(new Model_TURNOS_SEMANAL_DIA(EC_TURNOS.Turno.TURNO_ID, -1, 6));
                if (CeC.Convierte2Bool(Chb_HorarioSabado.IsChecked))
                    EC_TURNOS.TurnoSemanalDias.Add(new Model_TURNOS_SEMANAL_DIA(EC_TURNOS.Turno.TURNO_ID, -1, 7));
            }
            else
            {
                if (!TD_Domingo.ValidaControles())
                    return NoSeValidoSatisfactoriamente();
                if (!TD_Lunes.ValidaControles())
                    return NoSeValidoSatisfactoriamente();
                if (!TD_Martes.ValidaControles())
                    return NoSeValidoSatisfactoriamente();
                if (!TD_Miercoles.ValidaControles())
                    return NoSeValidoSatisfactoriamente();
                if (!TD_Jueves.ValidaControles())
                    return NoSeValidoSatisfactoriamente();
                if (!TD_Viernes.ValidaControles())
                    return NoSeValidoSatisfactoriamente();
                if (!TD_Sabado.ValidaControles())
                    return NoSeValidoSatisfactoriamente();

                AgregaDia(TD_Domingo.Vista2Modelo(HERETARDO, HERETARDO_B, HERETARDO_C, HERETARDO_D, HBLOQUE, HCTOLERA, TOLERANCIABLOQUE), 1);
                AgregaDia(TD_Lunes.Vista2Modelo(HERETARDO, HERETARDO_B, HERETARDO_C, HERETARDO_D, HBLOQUE, HCTOLERA, TOLERANCIABLOQUE), 2);
                AgregaDia(TD_Martes.Vista2Modelo(HERETARDO, HERETARDO_B, HERETARDO_C, HERETARDO_D, HBLOQUE, HCTOLERA, TOLERANCIABLOQUE), 3);
                AgregaDia(TD_Miercoles.Vista2Modelo(HERETARDO, HERETARDO_B, HERETARDO_C, HERETARDO_D, HBLOQUE, HCTOLERA, TOLERANCIABLOQUE), 4);
                AgregaDia(TD_Jueves.Vista2Modelo(HERETARDO, HERETARDO_B, HERETARDO_C, HERETARDO_D, HBLOQUE, HCTOLERA, TOLERANCIABLOQUE), 5);
                AgregaDia(TD_Viernes.Vista2Modelo(HERETARDO, HERETARDO_B, HERETARDO_C, HERETARDO_D, HBLOQUE, HCTOLERA, TOLERANCIABLOQUE), 6);
                AgregaDia(TD_Sabado.Vista2Modelo(HERETARDO, HERETARDO_B, HERETARDO_C, HERETARDO_D, HBLOQUE, HCTOLERA, TOLERANCIABLOQUE), 7);
            }



            eClockBase.Controladores.Turnos CntTurno = new eClockBase.Controladores.Turnos(Sesion);
            CntTurno.GuardadoEvent += CntTurno_GuardadoEvent;
            CntTurno.Guardar(EC_TURNOS);
            return true;
        }

        bool NoSeValidoSatisfactoriamente()
        {
            Sesion.MuestraMensaje("No se puede guardar");
            return true;
        }

        void CntTurno_GuardadoEvent(bool Guardado)
        {
            Regresar();
        }



    }
}
