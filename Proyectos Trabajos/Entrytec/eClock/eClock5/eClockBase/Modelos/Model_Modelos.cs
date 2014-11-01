using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_MODELOS
    {
        //Identificador unico del modelo de terminal
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int MODELO_ID { get; set; }
        
        //Nombre del Marca de terminal
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string MODELO_MARCA { get; set; }

        //Nombre del modelo de terminal
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string MODELO_MODELO { get; set; }

        //Comentario acerca de terminal
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string MODELO_COMENTARIO { get; set; }

        //Campo llave de terminal
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string MODELO_CAMPO_LLAVE { get; set; }

        // Campo adicional a modelo de terminal
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string MODELO_CAMPO_ADICIONAL { get; set; }

        //Nombre del modelo de terminal
        [Campo_Int(true, false, -1, -1, 0, Campo_IntAttribute.Tipo.ComboBox)]
        public int TIPO_TECNOLOGIA_ID { get; set; }

        //Campo ID ADD de terminal
        [Campo_Int(true, false, -1, -1, 0, Campo_IntAttribute.Tipo.ComboBox)]
        public int TIPO_TECNOLOGIA_ADD_ID { get; set; }


        [Campo_Int(true, false, -1, -1, 0, Campo_IntAttribute.Tipo.ComboBox)]
        public int ALMACEN_VEC_ID { get; set; }

        //No de Huellas en terminal
        [Campo_Int(false, false, -1, -1, 0, Campo_IntAttribute.Tipo.TextBox)]
        public int MODELO_NO_HUELLAS { get; set; }

        //No de Targetas en terminal
        [Campo_Int(false, false, -1, -1, 0, Campo_IntAttribute.Tipo.TextBox)]
        public int MODELO_NO_TARJETAS { get; set; }

        //No de ROSTROS en terminal
        [Campo_Int(false, false, -1, -1, 0, Campo_IntAttribute.Tipo.TextBox)]
        public int MODELO_NO_ROSTROS { get; set; }

        //No de Palmas en terminal
        [Campo_Int(false, false, -1, -1, 0, Campo_IntAttribute.Tipo.TextBox)]
        public int MODELO_NO_PALMAS { get; set; }

        //No de Iris en terminal
        [Campo_Int(false, false, -1, -1, 0, Campo_IntAttribute.Tipo.TextBox)]
        public int MODELO_NO_IRIS { get; set; }

        //No de Tarjetas en terminal
        [Campo_Int(false, false, -1, -1, 0, Campo_IntAttribute.Tipo.ComboBox)]
        public int MODELO_TERMINAL_ID { get; set; }

        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string MODELO_PUERTO { get; set; }
        
        //Indica si este modelo esta disponible
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool MODELO_BORRADO { get; set; }
    }
}
