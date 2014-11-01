using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_TIPO_INCIDENCIAS
    {
        //Identificador del Tipo de incidencias del sistema
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TIPO_INCIDENCIA_ID { get; set; }
        //Nombre de la incidencia
        [Campo_String(true, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TIPO_INCIDENCIA_NOMBRE { get; set; }
        //Abreviatura que se usara para mostrar esta incidencia
        [Campo_String(false, false, 2, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TIPO_INCIDENCIA_ABR { get; set; }
        //Indica el color de la incidencia
        [Campo_Int(false, false, -1, -1, 1, Campo_IntAttribute.Tipo.Color)]
        public int TIPO_INCIDENCIA_COLOR { get; set; }
        //Contiene un texto para agrupar, ej. todas los tipos de permisos deberian llevar la palabra 'Permiso' y así se podrán agrupar por permiso
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TIPO_INCIDENCIA_AGRUPADOR { get; set; }
        ///Campos a capturar para realizar la incidencia
        [Campo_String(false, false, 4000, "", Campo_StringAttribute.Tipo.CamposTextoDiseno)]
        public string TIPO_INCIDENCIA_CAMPOS { get; set; }
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TIPO_INCIDENCIA_KIOSCO { get; set; }

        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TIPO_INCIDENCIA_HORAS { get; set; }

        [Campo_Int(false, false, -1, -1, 0, Campo_IntAttribute.Tipo.ComboBox)]
        public int T_INC_ACCESO_ID { get; set; }

        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TIPO_INCIDENCIA_REGLAS { get; set; }

        ///Campos a capturar para realizar la incidencia
        [Campo_String(false, false, 4000, "", Campo_StringAttribute.Tipo.TextBoxMemo)]
        public string TIPO_INCIDENCIA_RESUMEN { get; set; }

        ///Campos a capturar para realizar la incidencia
        [Campo_String(false, false, 4000, "", Campo_StringAttribute.Tipo.TextBoxMemo)]
        public string TIPO_INCIDENCIA_MOMENTO { get; set; }

        //Indica si esta en estado baja (borrado = 1)
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TIPO_INCIDENCIA_BORRADO { get; set; }

    }
}
