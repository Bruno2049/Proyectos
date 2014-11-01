using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_TIPO_INCIDENCIAS_EX
    {
        //Identificador de tipos de incidencias externas (Autonumerico o ADAM)
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TIPO_INCIDENCIAS_EX_ID { get; set; }
        //Identificador de tipo de incidencia en Texto (sicoss)
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TIPO_INCIDENCIAS_EX_TXT { get; set; }
        //Descripccion del nombre de la incidencia en el sistema externo
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TIPO_INCIDENCIAS_EX_NOMBRE { get; set; }
        //Descripcion
        [Campo_String(false, false, 4000, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TIPO_INCIDENCIAS_EX_DESC { get; set; }
        //VEASE CLASE CEC_IncidenciasEx Contiene las reglas para solicitar la captura de la incidencia se usará #### para la cantidad de digitos numericos ####.## para numericos, aaaa para alfanumericos AAAA alfanumericos en mayusculas
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TIPO_INCIDENCIAS_EX_PARAM { get; set; }
        //0: Indica que la incidencia no es reemplazable; 1: Indica que la incidencia es falta y será reemplazable por una incidencia 2; 2:Indica que la incidencia actual puede reemplazar a cualquier incidencia tipo 1
        [Campo_Int(true, false, -1, -1, 0, Campo_IntAttribute.Tipo.TextBox)]
        public int TIPO_FALTA_EX_ID { get; set; }
        //Indica si la incidencia se encuentra borrada
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TIPO_INCIDENCIAS_EX_BORRADO { get; set; }

    }
}