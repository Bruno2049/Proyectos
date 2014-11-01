using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_TIPO_TRAMITE
    {
        //Identificador unico de registro
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TIPO_TRAMITE_ID { get; set; }
        //Indica una variable para el nombre.
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TIPO_TRAMITE_NOMBRE { get; set; }
        //Indica una variable para la ayuda.
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TIPO_TRAMITE_AYUDA { get; set; }
        //Indica una variable para la descripcion.
        [Campo_String(false, false, 4000, "", Campo_StringAttribute.Tipo.TextBoxMemo)]
        public string TIPO_TRAMITE_DESCRIPCION { get; set; }
        //emails a los que se les enviará un resumen diario de las personas que iniciarion el tramite
        [Campo_String(false, false, 4000, "", Campo_StringAttribute.Tipo.TextBoxMemo)]
        public string TIPO_TRAMITE_RESUMEN { get; set; }
        //emails a los que se les enviará un mail inmediatamente solicitan un tramite
        [Campo_String(false, false, 4000, "", Campo_StringAttribute.Tipo.TextBoxMemo)]
        public string TIPO_TRAMITE_MOMENTO { get; set; }
        ///Campos a capturar para realizar el tramite
        [Campo_String(false, false, 4000, "", Campo_StringAttribute.Tipo.CamposTextoDiseno)]
        public string TIPO_TRAMITE_CAMPOS { get; set; }
        //Indica una variable de las actividades para algun borrado.
         [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(true, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TIPO_TRAMITE_BORRADO { get; set; }
    }
}
