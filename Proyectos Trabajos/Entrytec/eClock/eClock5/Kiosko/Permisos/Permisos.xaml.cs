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

namespace Kiosko.Permisos
{
    /// <summary>
    /// Lógica de interacción para Permisos.xaml
    /// </summary>
    public partial class Permisos : UserControl
    {
        CeC_SesionBase m_SesionBase = null;
        public Permisos()
        {
            InitializeComponent();
        }

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

                case "Btn_Siguiente":
                    {
                        foreach (DateTime Dia in Calendario.DiasSeleccionados)
                        {
                            foreach (Kiosko.Controles.UC_Mes.DiasColorClass DC in Calendario.DiasColor)
                            {
                                if (DC.Dia == Dia)
                                {
                                    if (DC.Color == Solicitado)
                                    {
                                        Controles.UC_MessageBoxFullScreen Msg = new Controles.UC_MessageBoxFullScreen();
                                        Msg.Mensaje = "Tiene días seleccionados que estan pendientes para autorizar, deseleccionelos para continuar";
                                        Msg.TimeOutSegundos = 5;
                                        Msg.Mostrar(this);
                                        return;
                                    }
                                    if (DC.Color == Minimo)
                                    {
                                        Controles.UC_MessageBoxFullScreen Msg = new Controles.UC_MessageBoxFullScreen();
                                        Msg.Mensaje = "Tiene días seleccionados que estan negados, deseleccionelos para continuar";
                                        Msg.TimeOutSegundos = 5;
                                        Msg.Mostrar(this);
                                        return;
                                    }

                                }
                            }
                        }
                        SolicitudIncidencia SIncidencia = new SolicitudIncidencia();
                        SIncidencia.DiasSeleccionados = Calendario.DiasSeleccionados;
                        SIncidencia.VistaAnterior = this;
                        Kiosko.Generales.Main.MuestraComoDialogo(this, SIncidencia, this.Background);
                    }
                    break;
                /*
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
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }

                                SolicitudIncidencia SIncidencia = new SolicitudIncidencia();
                                SIncidencia.DiasSeleccionados = Calendario.DiasSeleccionados;
                                SIncidencia.VistaAnterior = this;
                                Kiosko.Generales.Main.MuestraComoDialogo(this, SIncidencia, this.Background);
                                break;

                */

            }
        }

        void Msg_Click_OK(object sender, RoutedEventArgs e)
        {

        }

        private void UC_Mes_SeleccionadoChanged(Controles.UC_Mes ControlMes)
        {
            ToolBar.Seleccionados = ControlMes.DiasSeleccionados.Count;
        }

        private void Calendario_MesChanged(Controles.UC_Mes ControlMes)
        {
            // return;
            eClockBase.Controladores.Incidencias Incidencias = new eClockBase.Controladores.Incidencias(m_SesionBase);
            Incidencias.StatusDiasEvent += Incidencias_StatusDiasEvent;
            Incidencias.StatusDias(ControlMes.Mes, ControlMes.Mes.AddMonths(1).AddDays(-1), "SOLICITUD_EXTRAS not like 'INCIDENCIA|" + MainWindow.KioscoParametros.Parametros.TIPO_INCIDENCIA_ID_Vaca.ToString() + "|%'");
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

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            // return;
            m_SesionBase = CeC_Sesion.ObtenSesion(this);
            Calendario_MesChanged(Calendario);
        }

    }
}
