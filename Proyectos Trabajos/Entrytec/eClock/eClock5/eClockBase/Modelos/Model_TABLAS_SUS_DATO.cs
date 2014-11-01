using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_TABLAS_SUS_DATO
    {
        
        //Identificador unico de la tabla
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TABLA_SUS_DATO_ID { get; set; }
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int TABLA_SUS_ID { get; set; }
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TABLA_SUS_DATO_LLAVE { get; set; }
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TABLA_SUS_DATO_CAMPO { get; set; }
        [Campo_String(false, false, 4000, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TABLA_SUS_DATO { get; set; }
        [Campo_BinarioAttribute(false, false, Campo_BinarioAttribute.Tipo.Imagen)]
        public byte[] TABLA_SUS_DATO_B { get; set; }
        [Campo_FechaHora(true, false, "2006-01-01", "2029-12-31", "2013-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime TABLA_SUS_DATO_FECHA { get; set; }
    }
}
