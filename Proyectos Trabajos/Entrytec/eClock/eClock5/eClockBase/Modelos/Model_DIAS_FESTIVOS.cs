using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_DIAS_FESTIVOS
    {
        //Identificador unico del registro de dia festivo
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int DIA_FESTIVO_ID { get; set; }
        //
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int CALENDARIO_DF_ID { get; set; }
        //Fecha en la que se aplicara dicho dia festivo
        [Campo_FechaHora(true, false, "2008-01-01", "2029-12-31", "2010-02-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime DIA_FESTIVO_FECHA { get; set; }
        //Nombre o nombre del dia festivo
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string DIA_FESTIVO_NOMBRE { get; set; }
        //Indica el color del día festivo
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.Color)]
        public int DIA_FESTIVO_COLOR { get; set; }
        //Indica si se ha borrado el registro
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool DIA_FESTIVO_BORRADO { get; set; }

    }
}
