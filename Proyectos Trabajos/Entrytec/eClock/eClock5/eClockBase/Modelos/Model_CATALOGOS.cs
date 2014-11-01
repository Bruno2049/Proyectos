using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_CATALOGOS
    {
        //Identificador unico del registro de catalogo
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int CATALOGO_ID { get; set; }
        //Nombre del catalogo
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string CATALOGO_NOMBRE { get; set; }
        //Tabla que contiene el catalogo
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string CATALOGO_TABLA { get; set; }
        //Campo identificador de registro en la tabla que contiene el catalogo
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string CATALOGO_C_LLAVE { get; set; }
        //campo descripción de la tabla que contiene el catalogo
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string CATALOGO_C_DESC { get; set; }
        //campo adicional de la tabla que contiene el catalogo
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string CATALOGO_C_EXTRA { get; set; }
        //Contiene un Qry adicional al ""SELECT CATALOGO_C_LLAVE, CATALOGO_C_DESC FROM CATALOGO_NOMBRE""
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string CATALOGO_QA_SQL { get; set; }
        //Indica si se mostrara un registro adicional con campo llave 0 o '' con etiqueta 'No Seleccionado'
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool CATALOGO_M_NSEL { get; set; }
        //Indica si el registro se podrá visualizar por el usuario
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool CATALOGO_BORRADO { get; set; }

    }
}
