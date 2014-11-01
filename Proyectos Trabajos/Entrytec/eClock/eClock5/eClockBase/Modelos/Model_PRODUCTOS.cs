using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_PRODUCTOS
    {
        //Identificador unico del producto
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int PRODUCTO_ID { get; set; }
        //Indica la sesion en la que se creo el producto
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int SESION_ID { get; set; }
        //Número de 2 digitos máximo que teclearan en comedor para referirce a este procuto
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int PRODUCTO_NO { get; set; }
        //Nombre del Procuto
        [Campo_String(true, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string PRODUCTO { get; set; }
        //Costo que tiene dicho producto
        [Campo_Decimal(true, false, (float)-1.0, (float)-1.0, (float)-1.0, Campo_DecimalAttribute.Tipo.TextBox)]
        public decimal PRODUCTO_COSTO { get; set; }
        //Indica si el producto se ha borrado y no podrá ser seleccionado nuevamente
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(true, true, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool PRODUCTO_BORRADO { get; set; }

    }
}
