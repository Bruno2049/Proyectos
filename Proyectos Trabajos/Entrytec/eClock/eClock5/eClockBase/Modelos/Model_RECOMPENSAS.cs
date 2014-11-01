using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_RECOMPENSAS
    {
        //
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int RECOMPENSA_ID { get; set; }
        //Nombre de la recompenza
        [Campo_String(true, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string RECOMPENSA_NOMBRE { get; set; }
        //Porcentage de la recompenza
        [Campo_Decimal(false, false, (float)-1.0, (float)-1.0, (float)0.0, Campo_DecimalAttribute.Tipo.TextBox)]
        public decimal RECOMPENSA_POR { get; set; }
        //Puntos que otorga la recompenza
        [Campo_Decimal(false, false, (float)-1.0, (float)-1.0, (float)0.0, Campo_DecimalAttribute.Tipo.TextBox)]
        public decimal RECOMPENSA_PT { get; set; }
        //Comentario sobre la recompensa
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string RECOMPENSA_COMEN { get; set; }
        //Indica el precio predeterminado de esta recompenza
        [Campo_Decimal(false, false, (float)-1.0, (float)-1.0, (float)0.0, Campo_DecimalAttribute.Tipo.TextBox)]
        public decimal RECOMPENSA_PRECIO { get; set; }
        //Precio maximo que podrá tener la recompensa
        [Campo_Decimal(false, false, (float)-1.0, (float)-1.0, (float)-1.0, Campo_DecimalAttribute.Tipo.TextBox)]
        public decimal RECOMPENSA_PRECIO_MAX { get; set; }
        //Indica la cantidad Predeterminada de la recompensa
        [Campo_Decimal(false, false, (float)-1.0, (float)-1.0, (float)0.0, Campo_DecimalAttribute.Tipo.TextBox)]
        public decimal RECOMPENSA_CANTIDAD { get; set; }
        //Cantidad maxima que podra tener la recompensa
        [Campo_Decimal(false, false, (float)-1.0, (float)-1.0, (float)-1.0, Campo_DecimalAttribute.Tipo.TextBox)]
        public decimal RECOMPENSA_CANTIDAD_MAX { get; set; }
        //Indica si se podra modificar el precio
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, true, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool RECOMPENSA_M_PRECIO { get; set; }
        //Indica si se podra modificar la cantidad predeterminada
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, true, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool RECOMPENSA_M_CANTIDAD { get; set; }
        //Indica la fecha y hora desde cuando será valida la recompensa
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime RECOMPENSA_FECHAHORA_I { get; set; }
        //Indica la fecha y hora hasta cuando será valida la recompensa
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime RECOMPENSA_FECHAHORA_F { get; set; }
        //Indica la hora inicio desde cuando será valida la recompensa predeterminado será 00:00
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime RECOMPENSA_HORAI { get; set; }
        //Indica la hora fin hasta cuando será valida la recompensa 23:59:59
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime RECOMPENSA_HORAF { get; set; }
        //Combinación de Teclas de acceso rapido
        [Campo_String(false, false, 10, "", Campo_StringAttribute.Tipo.TextBox)]
        public string RECOMPENSA_TECLA { get; set; }
        //indica si esta borrado el registro
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool RECOMPENSA_BORRADO { get; set; }

    }
}
