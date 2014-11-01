using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos.Actividades
{
    public class Reporte_Detalle
    {
        //Identificador unico de registro de incripcion a una actividad
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int ACTIVIDAD_INS_ID { get; set; }

        //Identificador unico de registro de actividad
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int ACTIVIDAD_ID { get; set; }

        //Indica la agrupación de la persona
        [Campo_String(true, true, 2000, "", Campo_StringAttribute.Tipo.TextBox)]
        public string AGRUPACION_NOMBRE { get; set; }

        //Terminal en la que ocurrio dicho acceso, si es 0 significa que fue una justificacion
        [Campo_String(true, true, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string ACTIVIDAD_NOMBRE { get; set; }

        //Liga al numero de empleado, en el caso de merck es TRACVE
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int PERSONA_LINK_ID { get; set; }

        //Nombre de la persona
        [Campo_String(true, true, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string PERSONA_NOMBRE { get; set; }
        
        //Nombre de la inscripción
        [Campo_String(true, true, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TIPO_INSCRIPCION_NOMBRE { get; set; }

        //
        [Campo_FechaHora(true, true, "2006-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime ACTIVIDAD_INS_FECHA { get; set; }

        //
        [Campo_String(true, true, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string ACTIVIDAD_INS_DESCRIPCION { get; set; }

    }
}
