using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos.Tramites
{
    public class Reporte_Detalle
    {
        //Identificador unico de registro
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TRAMITE_ID { get; set; }

        //Indica el nombre del tipo del tramite
        [Campo_String(true, true, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TIPO_TRAMITE_NOMBRE { get; set; }

        //Liga al numero de empleado, en el caso de merck es TRACVE
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int PERSONA_LINK_ID { get; set; }

        //Nombre de la persona
        [Campo_String(true, true, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string PERSONA_NOMBRE { get; set; }

        //Indica la agrupación de la persona
        [Campo_String(true, true, 2000, "", Campo_StringAttribute.Tipo.TextBox)]
        public string AGRUPACION_NOMBRE { get; set; }

        //Indica el nombre del tipo del tramite
        [Campo_String(true, true, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TIPO_PRIORIDAD_NOMBRE { get; set; }


        //
        [Campo_FechaHora(true, true, "2006-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime TRAMITE_FECHA { get; set; }

        //Contenido del tramite
        [Campo_String(true, true, 4000, "", Campo_StringAttribute.Tipo.TextBoxMemo)]
        public string TRAMITE_DESCRIPCION { get; set; }

    }
}
