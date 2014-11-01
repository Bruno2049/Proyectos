using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;

namespace eClockBase.Modelos
{
    public class Model_AUTONUM
    {
        //Nombre de la tabla a la que se le generará el autonumérico
        [Campo_String(true, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string AUTONUM_TABLA { get; set; }
        //Nombre del campo llave que contiene el autonumérico
        [Campo_String(true, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string AUTONUM_CAMPO_ID { get; set; }
        //Valor del autonumérico
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int AUTONUM_TABLA_ID { get; set; }
        //
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int SESION_ID { get; set; }
        //Fecha y hora en la que se creo
        //[Campo_FechaHora(false,false,-1,-1,-1,Campo_FechaHoraAttribute.Tipo.DatePicker)]
        //public DateTime AUTONUM_FECHAHORA { get; set; }
        //Indica que suscripcion agrego el registro
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int SUSCRIPCION_ID { get; set; }

    }
}
