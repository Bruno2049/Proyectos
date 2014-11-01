using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;

namespace eClockBase.Modelos
{
    public class Model_ALMACEN_INC
    {
        //Identificador unico de vacaciones disfrutadas
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int ALMACEN_INC_ID { get; set; }
        //
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TIPO_INCIDENCIA_R_ID { get; set; }
        //Identificador del Acceso creado manualmente (Justificado)
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TIPO_INCIDENCIA_INC_ID { get; set; }
        //Persona que disfruto las vacaciones
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.PersonaLinkID)]
        public int PERSONA_ID { get; set; }
        //Fecha de vacaciones
        //[Campo_FechaHora(false,false,-1,-1,-1,Campo_FechaHoraAttribute.Tipo.DatePicker)]
        //public DateTime ALMACEN_INC_FECHA { get; set; }
        //Numero de incidencias a favor o en contra
        //[Campo_Decimal(false,false,-1,-1,-1,Campo_DecimalAttribute.Tipo.TextBox)]
        //public decimal ALMACEN_INC_NO { get; set; }
        //Nuevo Saldo
        //[Campo_Decimal(false,false,-1,-1,-1,Campo_DecimalAttribute.Tipo.TextBox)]
        //public decimal ALMACEN_INC_SALDO { get; set; }
        //Comentario del movimiento
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string ALMACEN_INC_COMEN { get; set; }
        //Contiene datos extras
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string ALMACEN_INC_EXTRAS { get; set; }

    }
}
