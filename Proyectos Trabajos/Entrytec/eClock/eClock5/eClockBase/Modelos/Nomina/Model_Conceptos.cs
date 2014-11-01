using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos.Nomina
{
    public class Model_Conceptos
    {
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string REC_NOMI_MOV_CVE { get; set; }
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string REC_NOMI_MOV_CONCEPTO { get; set; }
        public Model_Conceptos()
        {
        }
        public Model_Conceptos(string sREC_NOMI_MOV_CVE, string sREC_NOMI_MOV_CONCEPTO)
        {
            REC_NOMI_MOV_CVE = sREC_NOMI_MOV_CVE;
            REC_NOMI_MOV_CONCEPTO = sREC_NOMI_MOV_CONCEPTO;
        }
    }
}
