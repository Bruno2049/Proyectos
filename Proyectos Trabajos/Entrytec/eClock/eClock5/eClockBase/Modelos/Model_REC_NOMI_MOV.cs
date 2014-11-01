using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_REC_NOMI_MOV
    {
        //Identificador unico de registro
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int REC_NOMI_MOV_ID { get; set; }

        //Identificador unico de registro
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int CLASIFIC_MOV_ID { get; set; }

        //Identificador unico de registro
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int REC_NOMINA_ID { get; set; }

        //
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string REC_NOMI_MOV_CVE { get; set; }

        //
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string REC_NOMI_MOV_CONCEPTO { get; set; }

        //
        [Campo_Decimal(false, false, (float)-1.0, (float)-1.0, (float)0.0, Campo_DecimalAttribute.Tipo.TextBox)]
        public decimal REC_NOMI_MOV_VALOR { get; set; }

        //
        [Campo_Decimal(false, false, (float)-1.0, (float)-1.0, (float)0.0, Campo_DecimalAttribute.Tipo.TextBox)]
        public decimal REC_NOMI_MOV_UNIDAD { get; set; }

        //
        [Campo_Decimal(false, false, (float)-1.0, (float)-1.0, (float)0.0, Campo_DecimalAttribute.Tipo.TextBox)]
        public decimal REC_NOMI_MOV_IMPORTE { get; set; }

        //
        [Campo_Decimal(false, false, (float)-1.0, (float)-1.0, (float)0.0, Campo_DecimalAttribute.Tipo.TextBox)]
        public decimal REC_NOMI_MOV_SALDO { get; set; }
    }
}