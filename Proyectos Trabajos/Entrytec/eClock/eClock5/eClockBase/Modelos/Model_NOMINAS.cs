using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_NOMINAS
    {
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int NOMINA_ID {get;set;}

        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int TIPO_NOMINA_ID {get;set;}

        [Campo_String(true, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string NOMINA_NOMBRE {get;set;}

        [Campo_String(false, false, 2000, "", Campo_StringAttribute.Tipo.TextBoxMemo)]
        public string NOMINA_DESCRIPCION {get;set;}

        [Campo_FechaHora(false, true, "2006-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime NOMINA_FECHA_IMP {get;set;}

        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int NOMINA_PERIODO_ANO {get;set;}

        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int NOMINA_PERIODO_NO {get;set;}

        [Campo_Bool(false, false, true, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool NOMINA_BORRADO {get;set;}

    }
}
