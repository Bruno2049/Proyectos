using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;
namespace eClockBase.Modelos
{
    public class Model_CAMPOS_TE
    {
        //Nombre real del campo
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int CAMPO_NOMBRE { get; set; }
        //Orden en el que se mostrara el campo
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int CAMPO_TE_ORDEN { get; set; }
        //Indica si es llave
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool CAMPO_TE_ES_LLAVE { get; set; }
        //Indica si se incrementará automaticamente el valor del campo cuando sea un nuevo registro
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool CAMPO_TE_ES_AUTONUM { get; set; }
        //Indica en que columna se mostrará el campo en edicion
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int CAMPO_TE_COLUMNA { get; set; }
        //Indica si se permitira hacer filtros usando este campo 
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool CAMPO_TE_FILTRO { get; set; }
        //Indica si el campo sera invisible (estará oculto)
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool CAMPO_TE_INVISIBLE { get; set; }
        //Indica si el campo sera invisible en los grids (estará oculto)
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool CAMPO_TE_INVISIBLEG { get; set; }

    }
}
