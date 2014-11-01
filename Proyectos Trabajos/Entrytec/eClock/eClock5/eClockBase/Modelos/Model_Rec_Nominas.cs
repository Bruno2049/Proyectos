using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_REC_NOMINAS
    {

        //Identificador unico de registro
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int REC_NOMINA_ID { get; set; }

        //Identificador unico de Nomina
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int NOMINA_ID { get; set; }

        //Identificador unico de tipo de Nomina
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int TIPO_NOMINA_ID { get; set; }

        //Identificador unico de persona
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int PERSONA_ID { get; set; }

        //Identificador unico de Nomina
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int REC_NOMINA_ANO { get; set; }

        //Numero de Periodo, -1 no se mostrará
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int REC_NOMINA_NO { get; set; }

        //Fecha de Generación del recibo 
        [Campo_FechaHora(true, false, "2006-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime REC_NOMINA_FECHA_GEN { get; set; }

        //Fecha de Generación del recibo 
        [Campo_FechaHora(true, false, "2006-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime REC_NOMINA_FINICIO { get; set; }

        //Fecha de Generación del recibo 
        [Campo_FechaHora(true, false, "2006-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime REC_NOMINA_FFIN { get; set; }

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

        //No esta en el recibo pero es un dato de cuantas veces se ha consultado
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int REC_NOMINA_CONSULTADO { get; set; }

        //Fecha en la que se consulto el recibo
        [Campo_FechaHora(true, false, "2006-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime REC_NOMINA_FCONSULTA { get; set; }

        //Indica Si se imprimio el recibo
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int REC_NOMINA_IMPRESO { get; set; }

        [Campo_String(false, false, 4000, "", Campo_StringAttribute.Tipo.TextBoxMemo)]
        public string REC_NOMINA_DATOS { get; set; }

        [Campo_Binario(false, false, Campo_BinarioAttribute.Tipo.Archivo)]
        public byte[] REC_NOMINA_PDF { get; set; }

        [Campo_Binario(false, false, Campo_BinarioAttribute.Tipo.Archivo)]
        public byte[] REC_NOMINA_XML { get; set; }

    }
}
