using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;

namespace eClockBase.Modelos
{
    public class Model_INCIDENCIAS_EX
    {
        //
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int INCIDENCIA_EX_ID { get; set; }
        //
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.PersonaLinkID)]
        public int PERSONA_ID { get; set; }
        //
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TIPO_INCIDENCIAS_EX_ID { get; set; }
        //
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TIPO_ORIGEN_INC_EX_ID { get; set; }
        //Contiene el Identificador del origen (persona_diario_id, persona_d_he_id), o cero en caso de no tener.
        [Campo_Int(false, false, -1, -1, 0, Campo_IntAttribute.Tipo.TextBox)]
        public int INCIDENCIA_EX_ORIGEN_ID { get; set; }
        //Fecha de la incidencia
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime INCIDENCIA_EX_FECHA { get; set; }
        //Variable para ser usada dependiendo configuración
        [Campo_Decimal(false, false, (float)-1, (float)-1, (float)-1, Campo_DecimalAttribute.Tipo.TextBox)]
        public decimal INCIDENCIA_EX_VARIABLE1 { get; set; }
        //Variable para ser usada dependiendo configuración
        [Campo_Decimal(false, false, (float)-1, (float)-1, (float)-1, Campo_DecimalAttribute.Tipo.TextBox)]
        public decimal INCIDENCIA_EX_VARIABLE2 { get; set; }
        //Variable para ser usada dependiendo configuración
        [Campo_Decimal(false, false, (float)-1, (float)-1, (float)-1, Campo_DecimalAttribute.Tipo.TextBox)]
        public decimal INCIDENCIA_EX_VARIABLE3 { get; set; }
        //Variable para ser usada dependiendo configuración
        [Campo_Decimal(false, false, (float)-1, (float)-1, (float)-1, Campo_DecimalAttribute.Tipo.TextBox)]
        public decimal INCIDENCIA_EX_VARIABLE4 { get; set; }
        //Variable para ser usada dependiendo configuración
        [Campo_Decimal(false, false, (float)-1, (float)-1, (float)-1, Campo_DecimalAttribute.Tipo.TextBox)]
        public decimal INCIDENCIA_EX_VARIABLE5 { get; set; }
        //-1 Cancelada, 0: sin procesar, 1: procesada. 2 Corregida
        [Campo_Int(false, false, -1, -1, 0, Campo_IntAttribute.Tipo.TextBox)]
        public int INCIDENCIA_EX_ESTADO { get; set; }

    }
}
