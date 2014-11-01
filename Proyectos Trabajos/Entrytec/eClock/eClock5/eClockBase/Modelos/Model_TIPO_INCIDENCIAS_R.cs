using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_TIPO_INCIDENCIAS_R
    {
        //Identificador de tipo de incidencias Regla del tipo de incidencia
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TIPO_INCIDENCIA_R_ID { get; set; }
        //Descripcion de la Regla
        [Campo_String(false, false, 4000, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TIPO_INCIDENCIA_R_DESC { get; set; }
        //        
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int TIPO_INCIDENCIA_ID { get; set; }
        //Indica si la incidencia requiere confirmación
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TIPO_INCIDENCIA_R_CNF { get; set; }
        //Contiene el query del select de personas ID a las que se les aplicará esta regla
        [Campo_String(false, false, 255, "SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_BORRADO = 0", Campo_StringAttribute.Tipo.TextBox)]
        public string TIPO_INCIDENCIA_R_PER { get; set; }
        //Permitir corrimiento en caso de que ya tenga una incidencia previa
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TIPO_INCIDENCIA_R_CORR { get; set; }
        //Informar por mail sobre la justificacion
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TIPO_INCIDENCIA_R_EMAIL { get; set; }
        //indica que manejara control de inventario
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TIPO_INCIDENCIA_R_INV { get; set; }
        //
        [Campo_Int(true, false, -1, -1, 0, Campo_IntAttribute.Tipo.TextBox)]
        public int TIPO_INCIDENCIA_R_ID_INV { get; set; }

        //
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int TIMESPAN_ID { get; set; }
        //Indica que no incrementará el inventario, sino lo perderá
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TIPO_INCIDENCIA_R_LIMPIAR { get; set; }
                //
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int TIPO_UNIDAD_ID { get; set; }
                    //
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int TIPO_REDONDEO_ID { get; set; } 
        //¿Se manejaran fracciones?
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TIPO_INCIDENCIA_R_FRAC { get; set; }

        //Indica que esta incidencia Será de suma o de resta, por default restará el valor será de resta, y se incrementará con las tablas de ciclos
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TIPO_INCIDENCIA_R_SUMAR { get; set; }

        //Contiene el qry con la fecha desde cuando se va a contar como fecha de inicio
        [Campo_String(false, false, 255, "SELECT FECHA_INGRESO FROM EC_PERSONAS_DATOS WHERE PERSONA_ID = @PERSONA_ID'", Campo_StringAttribute.Tipo.TextBox)]
        public string TIPO_INCIDENCIA_R_FIQRY { get; set; }
        //Manejara Credito (contiene el valor minimo que deberá quedar para permitir la incidencia)
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TIPO_INCIDENCIA_R_CRED { get; set; }
        
        //Indica que se ha borrado el registro
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TIPO_INCIDENCIA_R_BORRADO { get; set; }

    }
}
