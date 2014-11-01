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

using eClock5.BaseModificada;
using eClock5;
using eClockBase;

using System.Windows.Media.Animation;

//using System.Windows.Controls.ProgressBar;
using System.Threading;

namespace Kiosko.Vacaciones
{
    /// <summary>
    /// Lógica de interacción para Solicitud.xaml
    /// </summary>
    public partial class Solicitud : UserControl
    {

        CeC_SesionBase m_SesionBase = null;
        public Solicitud()
        {
            InitializeComponent();
        }

        Kiosko.Controles.UC_Cargando LoadDialog = new Controles.UC_Cargando();

        private void ToolBar_OnEventClickToolBar(eClock5.UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Regresar":
                    this.Visibility = System.Windows.Visibility.Hidden;
                    break;

                case "Btn_DeSeleccionar":
                    Calendario.DeSeleccionar();
                    break;
                case "Btn_Historial":
                    Kiosko.Vacaciones.Consultas Dlg = new Consultas();

                    Generales.Main.MuestraComoDialogo(this, Dlg, this.Background);
                    break;

                case "Btn_Siguiente":

                    foreach (System.DateTime Dia in Calendario.DiasSeleccionados)
                    {
                        Kiosko.Controles.UC_Mes.DiasColorClass DiaSeleccionadoColor = Calendario.BuscaDia(Dia);
                        if (DiaSeleccionadoColor != null)
                        {
                            if (DiaSeleccionadoColor.Color.ToString() == "#FFE43D34")
                            {
                                Calendario.DeSeleccionar();
                                Kiosko.Controles.UC_MessageBoxFullScreen Msg = new Controles.UC_MessageBoxFullScreen();
                                Msg.Mensaje = "No puede pedir dias DENEGADOS";
                                Msg.TimeOutSegundos = 3;
                                Msg.Mostrar(this);
                                return;
                            }
                            if (DiaSeleccionadoColor.Color.ToString() == "#FFEA6B00")
                            {
                                Calendario.DeSeleccionar();
                                Kiosko.Controles.UC_MessageBoxFullScreen Msg = new Controles.UC_MessageBoxFullScreen();
                                Msg.Mensaje = "No puede pedir días PREVIAMENTE SOLICITADOS";
                                Msg.TimeOutSegundos = 3;
                                Msg.Mostrar(this);
                                return;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }

                    if (Calendario.DiasSeleccionados.Count < 1)
                    {
                        Kiosko.Controles.UC_MessageBoxFullScreen Msg = new Controles.UC_MessageBoxFullScreen();
                        Msg.Mensaje = "No ha seleccionado ningun día";
                        Msg.TimeOutSegundos = 3;
                        Msg.Mostrar(this);
                        return;
                    }
                    else
                    {
                        //if(Calendario.DiaSeleccionado
                        Kiosko.Controles.UC_MessageBox Msg = new Controles.UC_MessageBox();
                        string Dias = eClockBase.CeC.ObtenDiasTexto(Calendario.DiasSeleccionados);
                        Msg.Mensaje = "¿Está seguro de que desea solicitar el (los) día(s) " + Dias + " a cuenta de vacaciones";
                        Msg.Texto_OK = "Si";
                        Msg.Texto_Cancel = "No";
                        Msg.Click_OK += Msg_Click_OK;
                        Msg.ShowDialog(Grd_Main, this.Background);
                    }

                    break;
            }
        }

        void Msg_Click_OK(object sender, RoutedEventArgs e)
        {

            
            eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Incidencias Idc = new eClockBase.Controladores.Incidencias(Sesion);
            Idc.SolicitaVacacionesEvent += Idc_SolicitaVacacionesEvent;

            Idc.SolicitaVacaciones(Calendario.DiasSeleccionados, "");

            Duration duration = new Duration(TimeSpan.FromSeconds(20));
            DoubleAnimation doubleanimation = new DoubleAnimation(70.0, duration);
            Bar_Solicitudes.BeginAnimation(ProgressBar.ValueProperty, doubleanimation);
            
            
            Kiosko.Generales.Main.MuestraComoDialogo(this, LoadDialog, System.Windows.Media.Color.FromArgb(55, 55, 55, 55));

            //for (int i = 0; i <= 100; i++)
            //{
            //    Bar_Solicitudes.Value = i;
            //    //System.Threading.Thread.Sleep(2000);
            //}
        }

        bool Cerrar = false;
        void Idc_SolicitaVacacionesEvent(string Solicitados)
        {
            Controles.UC_MessageBoxFullScreen Msg = new Controles.UC_MessageBoxFullScreen();
            if (Solicitados == "OK")
            {
                Msg.Mensaje = "Solicitud de Vacaciones enviada satisfactoriamente";
                Cerrar = true;
            }
            else
            {
                Msg.Mensaje = "Solicitud de Vacaciones No enviada";
                Msg.Imagen = null;
                Cerrar = false;
            }
            Msg.Cerrado += Msg_Cerrado;
            Msg.Mostrar(this);
        }
        void Msg_Cerrado()
        {
            if (Cerrar)
            {
                LoadDialog.Visibility = System.Windows.Visibility.Hidden;
                    this.Visibility = System.Windows.Visibility.Hidden;
                
            }
        }


        private void Calendario_SeleccionadoChanged(Controles.UC_Mes ControlMes)
        {
            ToolBar.Seleccionados = ControlMes.DiasSeleccionados.Count;
            ActualizaSaldoActual();
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            m_SesionBase = CeC_Sesion.ObtenSesion(this);
            AsignaColor();

            eClockBase.Controladores.Incidencias Idc = new eClockBase.Controladores.Incidencias(m_SesionBase);
            Idc.StatusVacacionesEvent += Idc_StatusVacacionesEvent;
            Idc.StatusVacaciones();
            Calendario_MesChanged(Calendario);
        }
        eClockBase.Modelos.Incidencias.Model_Vacaciones StatusActual = null;
        public void ActualizaSaldoActual()
        {
            if (StatusActual == null)
                return;
            Lbl_SaldoActualVacaciones.Text = StatusActual.SaldoVacaciones.ToString();
            if (ToolBar.Seleccionados > 0)
                Lbl_SaldoActualVacaciones.Text += " -" + ToolBar.Seleccionados.ToString() + " = " + Convert.ToString(StatusActual.SaldoVacaciones - ToolBar.Seleccionados);
        }

        void Idc_StatusVacacionesEvent(eClockBase.Modelos.Incidencias.Model_Vacaciones Status)
        {
            StatusActual = Status;
            Lbl_SiguienteCorte.Text = Status.SiguienteCorte.ToShortDateString();
            Lbl_SiguienteSaldo.Text = Status.PierdeVacaciones ? Status.SiguienteIncremento.ToString() : CeC.Convierte2String(Status.SiguienteIncremento + Status.SaldoVacaciones);
            ActualizaSaldoActual();
        }

        private void Calendario_MesChanged(Controles.UC_Mes ControlMes)
        {

            eClockBase.Controladores.Incidencias Incidencias = new eClockBase.Controladores.Incidencias(m_SesionBase);
            Incidencias.StatusDiasEvent += Incidencias_StatusDiasEvent;
            Incidencias.StatusDias(ControlMes.Mes, ControlMes.Mes.AddMonths(1).AddDays(-1), "SOLICITUD_EXTRAS like 'INCIDENCIA|" + MainWindow.KioscoParametros.Parametros.TIPO_INCIDENCIA_ID_Vaca.ToString() + "|%'");
        }
        public Color Solicitado = Color.FromRgb(234, 107, 0);
        public Color Aceptado = Color.FromRgb(0, 143, 46);
        public Color Alerta = Color.FromRgb(250, 198, 0);
        public Color Minimo = Color.FromRgb(228, 61, 52);
        public Color Festivo = Color.FromRgb(40, 115, 182);

        public void AsignaColor()
        {
            D_Solicitado.ColorDia = Solicitado;
            D_Aceptable.ColorDia = Aceptado;
            D_Alerta.ColorDia = Alerta;
            D_Minimo.ColorDia = Minimo;
            D_Festivo.ColorDia = Festivo;
        }

        void Incidencias_StatusDiasEvent(List<eClockBase.Modelos.Incidencias.Model_StatusDia> StatusDias)
        {
            if (StatusDias != null && StatusDias.Count > 0)
            {
                foreach (eClockBase.Modelos.Incidencias.Model_StatusDia SDia in StatusDias)
                {
                    switch (SDia.Tipo)
                    {
                        case -1:
                            Calendario.DiasColor.Add(new Controles.UC_Mes.DiasColorClass(SDia.Dia, Festivo));
                            break;
                        case 0:
                            Calendario.DiasColor.Add(new Controles.UC_Mes.DiasColorClass(SDia.Dia, Solicitado));
                            break;
                        case 1:
                            Calendario.DiasColor.Add(new Controles.UC_Mes.DiasColorClass(SDia.Dia, Alerta));
                            break;
                        case 2:
                            Calendario.DiasColor.Add(new Controles.UC_Mes.DiasColorClass(SDia.Dia, Minimo));
                            break;
                        case 3:
                            Calendario.DiasColor.Add(new Controles.UC_Mes.DiasColorClass(SDia.Dia, Minimo));
                            break;
                        case 12:
                            Calendario.DiasColor.Add(new Controles.UC_Mes.DiasColorClass(SDia.Dia, Aceptado));
                            break;
                        case 13:
                            Calendario.DiasColor.Add(new Controles.UC_Mes.DiasColorClass(SDia.Dia, Minimo));
                            break;
                        case 14:
                            Calendario.DiasColor.Add(new Controles.UC_Mes.DiasColorClass(SDia.Dia, Minimo));
                            break;

                    }
                }
                Calendario.ActualizaCalendario();
            }
        }
    }
}
