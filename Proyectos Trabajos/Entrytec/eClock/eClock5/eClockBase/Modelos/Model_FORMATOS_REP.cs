using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;
namespace eClockBase.Modelos
{
    public class Model_FORMATOS_REP
    {
        //Identificador del formato de reporte
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int FORMATO_REP_ID { get; set; }
        //Nombre del formato de reporte
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string FORMATO_REP_NOMBRE { get; set; }
        //Indica si esta borrado este formato
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool FORMATO_REP_BORRADO { get; set; }

    }
}
