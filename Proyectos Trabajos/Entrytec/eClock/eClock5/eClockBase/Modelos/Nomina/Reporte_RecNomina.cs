using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos.Nomina
{
    public class Reporte_RecNomina
    {
        public List<eClockBase.Modelos.Nomina.Model_Rec_Nominas_Ampliados> Recibo_Nomina = new List<Model_Rec_Nominas_Ampliados>();
        public List<eClockBase.Modelos.Model_REC_NOMI_MOV> Percepciones = new List<eClockBase.Modelos.Model_REC_NOMI_MOV>();
        public List<eClockBase.Modelos.Model_REC_NOMI_MOV> Deducciones = new List<eClockBase.Modelos.Model_REC_NOMI_MOV>();
    }
}
