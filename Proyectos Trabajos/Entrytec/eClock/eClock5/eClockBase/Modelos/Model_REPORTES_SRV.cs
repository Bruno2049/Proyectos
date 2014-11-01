using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_REPORTES_SRV
    {
        //Identificador de Servidor de Reportes
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int REPORTE_SRV_ID { get; set; }

        //Nombre del Servidor de Reportes
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string REPORTE_SRV_NOMBRE { get; set; }

        //Direccion del WebService de los Reportes
        [Campo_String(false, false, 1024, "", Campo_StringAttribute.Tipo.TextBox)]
        public string REPORTE_SRV_URL { get; set; }

        //Empresa propietaria del Servidor de Reportes
        [Campo_String(false, false, 1024, "", Campo_StringAttribute.Tipo.TextBox)]
        public string REPORTE_SRV_EMPR { get; set; }

        //Datos Adicionales
        [Campo_String(false, false, 1024, "", Campo_StringAttribute.Tipo.TextBox)]
        public string REPORTE_SRV_DESC { get; set; }

        //Indica que se ha borrado el servidor de reportes
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(true, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool REPORTE_SRV_BORRADO { get; set; }
    }
}
