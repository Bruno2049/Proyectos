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
    /// Lógica de interacción para AsigncionTurnos.xaml
    /// </summary>
    public partial class AsigncionTurnos : UserControl
    {
        public bool Asignado = false;
        public AsigncionTurnos()
        {
            InitializeComponent();
        }

        private void UC_ToolBar_OnEventClickToolBar(UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Regresar":
                    this.Visibility = Visibility.Hidden;
                    break;
                case "Btn_Guardar":
                    eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);

                    if (Rbn_AsigDiasSeleccionados.IsChecked == true)
                    {
                        if (Lst_Turnos.SeleccionadoLlave == null)
                        {
                            Sesion.MuestraMensaje("No ha seleccionado el turno", 10);
                            return;
                        }
                        Clases.Parametros Param = Clases.Parametros.Tag2Parametros(this.Tag);
                        eClockBase.Controladores.Turnos Turno = new eClockBase.Controladores.Turnos(Sesion);
                        Turno.AsignadoHorarioAPersonaDiarioIDsEvent += Turno_AsignadoHorarioAPersonaDiarioIDsEvent;
                        this.IsEnabled = false;
                        Turno.AsignaHorarioAPersonaDiarioIDs(Param.Parametro.ToString(), eClockBase.CeC.Convierte2Int(Lst_Turnos.SeleccionadoLlave));

                    }
                    else if (Rbn_Predeterminados.IsChecked == true)
                    {
                        if (Lst_Turnos.SeleccionadoLlave == null)
                        {
                            Sesion.MuestraMensaje("No ha seleccionado el turno", 10);
                            return;
                        }
                        Clases.Parametros Param = Clases.Parametros.Tag2Parametros(this.Tag);
                        eClockBase.Controladores.Turnos Turno = new eClockBase.Controladores.Turnos(Sesion);
                        //AsignaHorarioPredeterminadoAPersonaDiarioIDs
                        Turno.AsignaHorarioPredeterminadoAPersonaDiarioIDsEvent += Turno_AsignadoHorarioAPersonaDiarioIDsEvent;
                        this.IsEnabled = false;
                        Turno.AsignaHorarioPredeterminadoAPersonaDiarioIDs(Param.Parametro.ToString(), eClockBase.CeC.Convierte2Int(Lst_Turnos.SeleccionadoLlave));
                    }
                    else
                    {
                        Sesion.MuestraMensaje("Debe Seleccionar un tipo de turno ya sea por DIA o PREDETERMINADO", 10);
                    }
                    break;

            }
        }

        void Turno_AsignadoHorarioAPersonaDiarioIDsEvent(int NoGuardados)
        {
            this.IsEnabled = true;
            if (NoGuardados > 0)
            {
                Asignado = true;
                this.Visibility = Visibility.Hidden;
            }
        }
    }
}
