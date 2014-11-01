using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Turnos
{
    public class Model_Turno
    {
        public eClockBase.Modelos.Model_TURNOS Turno = new Model_TURNOS();
        public List<eClockBase.Modelos.Model_TURNOS_DIA> TurnoDias = new List<Model_TURNOS_DIA>();
        public List<eClockBase.Modelos.Model_TURNOS_SEMANAL_DIA> TurnoSemanalDias = new List<Model_TURNOS_SEMANAL_DIA>();
    }
}
