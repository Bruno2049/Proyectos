using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;


namespace eClockBase.Modelos
{
    public class Model_PUBLICIDAD
    {
        //Identificador unico de registro
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int PUBLICIDAD_ID { get; set; }
        //Indica de que persona se refiere el acceso
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int SUSCRIPCION_ID { get; set; }
        //Terminal en la que ocurrio dicho acceso, si es 0 significa que fue una justificacion
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int TIPO_PUBLICIDAD_ID { get; set; }
        //Indica la imagen que se obtiene.
        [Campo_BinarioAttribute(false, false, Campo_BinarioAttribute.Tipo.Imagen)]
        public byte[] PUBLICIDAD { get; set; }
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string PUBLICIDAD_NOMBRE { get; set; }
        [Campo_String(false, false, 4000, "", Campo_StringAttribute.Tipo.TextBoxMemo)]
        public string PUBLICIDAD_COMENTARIO { get; set; }
        [Campo_String(false, false, 4000, "", Campo_StringAttribute.Tipo.TextBoxMemo)]
        public string PUBLICIDAD_LINK { get; set; }
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int PUBLICIDAD_ORDEN { get; set; }
        [Campo_FechaHora(true, false, "2006-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime PUBLICIDAD_DESDE { get; set; }
        [Campo_FechaHora(true, false, "2006-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime PUBLICIDAD_HASTA { get; set; }
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int PUBLICIDAD_SEGUNDOS { get; set; }
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(true, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool PUBLICIDAD_BORRADO { get; set; }
    }
}
