using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos.Nomina
{
    public class Model_RecNominas
    {

        //Identificador de numero de epleado
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int PERSONA_LINK_ID { get; set; }

        //
        [Campo_Decimal(false, false, (float)-1.0, (float)-1.0, (float)0.0, Campo_DecimalAttribute.Tipo.TextBox)]
        public decimal REC_NOMINA_DIASPAG { get; set; }

        //
        [Campo_Decimal(false, false, (float)-1.0, (float)-1.0, (float)0.0, Campo_DecimalAttribute.Tipo.TextBox)]
        public decimal REC_NOMINA_DESCTRAB { get; set; }

        //
        [Campo_Decimal(false, false, (float)-1.0, (float)-1.0, (float)0.0, Campo_DecimalAttribute.Tipo.TextBox)]
        public decimal REC_NOMINA_VALES { get; set; }

        //
        [Campo_Decimal(false, false, (float)-1.0, (float)-1.0, (float)0.0, Campo_DecimalAttribute.Tipo.TextBox)]
        public decimal REC_NOMINA_HEXTRAS { get; set; }
        //
        [Campo_Decimal(false, false, (float)-1.0, (float)-1.0, (float)0.0, Campo_DecimalAttribute.Tipo.TextBox)]
        public decimal REC_NOMINA_N_PAGAR { get; set; }

        [Campo_String(false, false, 4000, "", Campo_StringAttribute.Tipo.TextBoxMemo)]
        public string REC_NOMINA_COMENTARIOS { get; set; }

        public List<eClockBase.Modelos.Nomina.Model_RecNomiMov> Movimientos = new List<eClockBase.Modelos.Nomina.Model_RecNomiMov>();

    }
}
