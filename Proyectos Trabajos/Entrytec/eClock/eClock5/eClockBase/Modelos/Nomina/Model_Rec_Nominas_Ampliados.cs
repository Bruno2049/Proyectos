using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos.Nomina
{
    public class Model_Rec_Nominas_Ampliados : Model_REC_NOMINAS
    {
        //Identificador unico de registro
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int PERSONA_LINK_ID { get; set; }

        //
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string PERSONA_NOMBRE { get; set; }

        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string AGRUPACION_NOMBRE { get; set; }
        
        //
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string AREA { get; set; }

        //
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TURNO { get; set; }


        //
        [Campo_String(false, false, 20, "", Campo_StringAttribute.Tipo.TextBox)]
        public string CURP { get; set; }

        //
        [Campo_String(false, false, 50, "", Campo_StringAttribute.Tipo.TextBox)]
        public string RFC { get; set; }

        //
        [Campo_String(false, false, 20, "", Campo_StringAttribute.Tipo.TextBox)]
        public string IMSS { get; set; }

        //Empresa a la que pertenecio en el momento de generar el recibo de nomina
        [Campo_String(false, false, 4000, "", Campo_StringAttribute.Tipo.TextBox)]
        public string COMPANIA { get; set; }

        //Puesto en el momento de generar el recibo de nomina
        [Campo_String(false, false, 4000, "", Campo_StringAttribute.Tipo.TextBox)]
        public string PUESTO { get; set; }

    }
}

