using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos.Solicitudes
{
    public class Model_ReporteSolicitudVacaciones
    {
        //Identificador unico por persona
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.PersonaLinkID)]
        public int PERSONA_ID { get; set; }

        //Identificador unico por persona
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int SOLICITUD_ID { get; set; }

        //Nombre de la persona
        [Campo_String(false, false, 4000, "", Campo_StringAttribute.Tipo.TextBox)]
        public string SOLICITUD_TITULO { get; set; }

        //Nombre de la persona
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string SOLICITUD_DESC { get; set; }



        //****************************************************************
        //Fecha y Hora de instalación
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime SOLICITUD_FECHAHORA { get; set; }

        //Nombre del estado de la solicitud
        [Campo_String(true, true, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string EDO_SOLICITUD_NOMBRE { get; set; }


        //Indica la agrupación de la persona
        [Campo_String(false, false, 2000, "", Campo_StringAttribute.Tipo.TextBox)]
        public string AGRUPACION_NOMBRE { get; set; }

        //Liga al numero de empleado, en el caso de merck es TRACVE
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int PERSONA_LINK_ID { get; set; }

        //Nombre de la persona
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string PERSONA_NOMBRE { get; set; }

        //Nombre de la incidencia
        [Campo_String(true, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TIPO_INCIDENCIA_NOMBRE { get; set; }
        
    }
}
