using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos.Wizard
{
    public class Model_Suscripcion
    {
        //Identificador de la suscripcion
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int SUSCRIPCION_ID { get; set; }
        //Contiene la razon social del propietario de la suscripcion
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string SUSCRIPCION_RAZON { get; set; }
        //RFC
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string SUSCRIPCION_RFC { get; set; }
        //Campo uno de Dirección
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string SUSCRIPCION_DIRECCION1 { get; set; }
        //Campo dos de Dirección
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string SUSCRIPCION_DIRECCION2 { get; set; }
        //Campo dos de Dirección
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string SUSCRIPCION_CIUDAD { get; set; }
        //
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string SUSCRIPCION_ESTADO { get; set; }
        //
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string SUSCRIPCION_PAIS { get; set; }
        //Contiene la cantidad de Empleados que se permitiran en esta suscripcion
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int SUSCRIPCION_EMPLEADOS { get; set; }
        //Contiene la cantidad de Terminales que se permitiran en esta suscripcion
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int SUSCRIPCION_TERMINALES { get; set; }
    }
}
