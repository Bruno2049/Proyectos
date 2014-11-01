using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_TRAMITES
    {
        //Identificador unico de registro
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TRAMITE_ID { get; set; }
        //Identificador unico del tipo de tramite.
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int TIPO_TRAMITE_ID { get; set; }
        //Identificador unico del tipo prioridad.
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int TIPO_PRIORIDAD_ID { get; set; }
        //Identificador unico para la persona.
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.PersonaLinkID)]
        public int PERSONA_ID { get; set; }
        //
        [Campo_FechaHora(false, false, "2006-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime TRAMITE_FECHA { get; set; }
        //Contenido del tramite
        [Campo_String(false, false, 4000, "", Campo_StringAttribute.Tipo.TextBoxMemo)]
        public string TRAMITE_DESCRIPCION { get; set; }

    }
}
