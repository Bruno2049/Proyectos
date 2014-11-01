using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_SUSCRIP_PRECIOS
    {
        //Identificador del cargo realizado a la suscripcion
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int SUSCRIP_PRECIO_ID { get; set; }
        //Descripcion del precio del tipo de suscripción
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string SUSCRIP_PRECIO_DESC { get; set; }
        //Precio por empleado adicional
        [Campo_Decimal(false, false, (float)-1.0, (float)-1.0, (float)-1.0, Campo_DecimalAttribute.Tipo.TextBox)]
        public decimal SUSCRIP_PRECIO_EMPLEADOS { get; set; }
        //precio por terminal adicional
        [Campo_Decimal(false, false, (float)-1.0, (float)-1.0, (float)-1.0, Campo_DecimalAttribute.Tipo.TextBox)]
        public decimal SUSCRIP_PRECIO_TERMINALES { get; set; }
        //Precio por usuario adicional
        [Campo_Decimal(false, false, (float)-1.0, (float)-1.0, (float)-1.0, Campo_DecimalAttribute.Tipo.TextBox)]
        public decimal SUSCRIP_PRECIO_USUARIOS { get; set; }
        //precio por alumno adicional
        [Campo_Decimal(false, false, (float)-1.0, (float)-1.0, (float)-1.0, Campo_DecimalAttribute.Tipo.TextBox)]
        public decimal SUSCRIP_PRECIO_ALUMNOS { get; set; }
        //precio por visitante adicional
        [Campo_Decimal(false, false, (float)-1.0, (float)-1.0, (float)-1.0, Campo_DecimalAttribute.Tipo.TextBox)]
        public decimal SUSCRIP_PRECIO_VISITANTES { get; set; }
        //Indica si se contarán los elementos en status borrado
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool SUSCRIP_PRECIO_BORRADOS { get; set; }

    }
}
