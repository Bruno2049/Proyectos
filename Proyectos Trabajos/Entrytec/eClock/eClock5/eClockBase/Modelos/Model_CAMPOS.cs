using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_CAMPOS
    {
        //Nombre real del campo
        [Campo_String(true, true, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string CAMPO_NOMBRE { get; set; }
        //Nombre del campo (etiqueta)
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string CAMPO_ETIQUETA { get; set; }
        //
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int CATALOGO_ID { get; set; }
        //
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int MASCARA_ID { get; set; }
        //
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int TIPO_DATO_ID { get; set; }
        //Numero de caracteres permitidos en el campo
        [Campo_Int(false, false, -1, -1, 45, Campo_IntAttribute.Tipo.TextBox)]
        public int CAMPO_LONGITUD { get; set; }
        //Ancho del campo en pixeles en el grid
        [Campo_Int(false, false, -1, -1, 0, Campo_IntAttribute.Tipo.TextBox)]
        public int CAMPO_ANCHO_GRID { get; set; }
        //Alto del campo en pixeles en el grid
        [Campo_Int(false, false, -1, -1, 0, Campo_IntAttribute.Tipo.TextBox)]
        public int CAMPO_ALTO_GRID { get; set; }
        //Ancho del campo en pixeles en edicion
        [Campo_Int(false, false, -1, -1, 0, Campo_IntAttribute.Tipo.TextBox)]
        public int CAMPO_ANCHO { get; set; }
        //Alto del campo en pixeles
        [Campo_Int(false, false, -1, -1, 0, Campo_IntAttribute.Tipo.TextBox)]
        public int CAMPO_ALTO { get; set; }
        //Indica si el campo pertenece a la tabla de empleados
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool CAMPO_ES_TEMPLEADOS { get; set; }

    }
}
