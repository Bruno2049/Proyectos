using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{

    public class Model_DISENOS
    {
        //Identificador unico del diseño
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int DISENO_ID { get; set; }
        //Nombre del Diseño
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string DISENO_NOMBRE { get; set; }
        //Sentencia SQL que debuelve todas las personas que al no tener diseño asignado se usará el actual
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string DISENO_SQL { get; set; }
        //Contiene el diseño
        //Otro
        //public blob DISENO_DISENO { get; set; }
        //Imagen del Diseño
        //Otro
        //public blob DISENO_DISENO_IMA { get; set; }
        //Imagen del Diseño posterior
        //Otro
        //public blob DISENO_DISENO_IMA_REV { get; set; }
        //Contiene una imagen del diseño en miniatura
        //Otro
        //public blob DISENO_DISENO_THUMB { get; set; }
        //Fecha de Creación del Diseño
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime DISENO_F_CREADO { get; set; }
        //Fecha de Ultima Modificación
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime DISENO_F_EDITADO { get; set; }
        //
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, true, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool DISENO_BORRADO { get; set; }

    }
}
