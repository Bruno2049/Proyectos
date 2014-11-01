using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;
namespace eClockBase.Modelos
{
    public class Model_TABLAS_SUS
    {
        //Identificador unico de la tabla de suscripcion
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TABLA_SUS_ID { get; set; }

        //Suscripcion
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int SUSCRIPCION_ID { get; set; }

        //Indica una variable de las actividades por persona.
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TABLA_SUS_NOMBRE { get; set; }

        //Etiqueta que se mostra en el titulo del módulo
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TABLA_SUS_ETIQUETA { get; set; }

        //Campos de la tabla
        [Campo_String(false, false, 4000, "", Campo_StringAttribute.Tipo.CamposTextoDiseno)]
        public string TABLA_SUS_CAMPOS { get; set; }
        
        //Nombre del campo llave, debe estar definido en campos (TABLA_SUS_CAMPOS)
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TABLA_SUS_LLAVE { get; set; }

        //Descripcion de la tabla
        [Campo_String(false, false, 4000, "", Campo_StringAttribute.Tipo.TextBoxMemo)]
        public string TABLA_SUS_DESCRIPCION { get; set; }

        //Indica que almacenará un historico de cambios
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(true, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TABLA_SUS_HISTORICO { get; set; }

        //Indica que la tabla que la tabla fue borrada
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(true, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TABLA_SUS_BORRADO { get; set; }      
    
    }
}
