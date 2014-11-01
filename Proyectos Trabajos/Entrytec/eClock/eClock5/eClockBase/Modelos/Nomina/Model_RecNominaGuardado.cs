using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Nomina
{
    public class Model_RecNominaGuardado
    {
        public Model_Rec_Nominas_Ampliados RecNomina { get; set; }
        public List<eClockBase.Modelos.Model_REC_NOMI_MOV> Percepciones = new List<eClockBase.Modelos.Model_REC_NOMI_MOV>();
        public List<eClockBase.Modelos.Model_REC_NOMI_MOV> Deducciones = new List<eClockBase.Modelos.Model_REC_NOMI_MOV>();
    }
}
