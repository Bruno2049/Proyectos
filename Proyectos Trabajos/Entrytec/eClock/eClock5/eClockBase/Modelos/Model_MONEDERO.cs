using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;

namespace eClockBase.Modelos
{
    public class Model_MONEDERO
    {
        //Identificador unico de la transaccion
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int MONEDERO_ID { get; set; }
        //Forma en la que se realizo el cobro
        [Campo_Int(true, false, -1, -1, 0, Campo_IntAttribute.Tipo.TextBox)]
        public int TIPO_COBRO_ID { get; set; }
        //Identificador de la persona.
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.PersonaLinkID)]
        public int PERSONA_ID { get; set; }
        //Siempre que sea una carga manual, contendrá el id de sesión, de lo contrario tendrá 0
        [Campo_Int(true, false, -1, -1, 0, Campo_IntAttribute.Tipo.TextBox)]
        public int SESION_ID { get; set; }
        //Fecha en la que se realizo la transacción
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime MONEDERO_FECHA { get; set; }
        //Consumo realizado, si es un abono será negativo
        [Campo_Decimal(false, false, (float)-1, (float)-1, (float)-1, Campo_DecimalAttribute.Tipo.TextBox)]
        public decimal MONEDERO_CONSUMO { get; set; }
        //Saldo en monedero, valores negativos significan deuda
        [Campo_Decimal(false, false, (float)-1, (float)-1, (float)-1, Campo_DecimalAttribute.Tipo.TextBox)]
        public decimal MONEDERO_SALDO { get; set; }

    }
}
