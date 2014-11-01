using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_SUSCRIPCION
    {
        //Identificador de la suscripcion
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int SUSCRIPCION_ID { get; set; }
        //
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int EDO_SUSCRIPCION_ID { get; set; }
        //Identificador único del precio
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int SUSCRIP_PRECIO_ID { get; set; }
        //Nombre de la suscripcion regularmente no contrato
        [Campo_String(true, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string SUSCRIPCION_NOMBRE { get; set; }

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
        //Campo de Codigo postal
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string SUSCRIPCION_CP { get; set; }
        //Campo de Ciudad
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string SUSCRIPCION_CIUDAD { get; set; }
        //
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string SUSCRIPCION_ESTADO { get; set; }
        //
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string SUSCRIPCION_PAIS { get; set; }
        //Indica si el cliente requiere Factura
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool SUSCRIPCION_FACTURAR { get; set; }
        //Contiene la fecha de contratación
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime SUSCRIPCION_CONTRATACION { get; set; }
        //Contiene la cantidad de Empleados que se permitiran en esta suscripcion
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int SUSCRIPCION_EMPLEADOS { get; set; }
        //Contiene la cantidad de Terminales que se permitiran en esta suscripcion
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int SUSCRIPCION_TERMINALES { get; set; }
        //Contiene la cantidad de Usuarios que se permitiran en esta suscripcion
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int SUSCRIPCION_USUARIOS { get; set; }
        //Contiene la cantidad de Alumnos que se permitiran en esta suscripcion
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int SUSCRIPCION_ALUMNOS { get; set; }
        //Contiene la cantidad de Visitantes que se permitiran en esta suscripcion
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int SUSCRIPCION_VISITANTES { get; set; }
        //Indica si permitirá empleados, terminales, etc adicionales a los autorizados
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool SUSCRIPCION_ADICIONALES { get; set; }
        //Contiene datos adicionales a validar
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string SUSCRIPCION_OTROS { get; set; }
        //Mensualidad de pago por la suscripcion
        [Campo_Decimal(false, false, (float)-1.0, (float)-1.0, (float)-1.0, Campo_DecimalAttribute.Tipo.TextBox)]
        public decimal SUSCRIPCION_MENSUAL { get; set; }
        //Fecha en la que finalizará el contrato
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime SUSCRIPCION_FINAL { get; set; }

        //Contiene la url del servicio que se usará para esta suscripcion
        [Campo_String(false, false, 512, "", Campo_StringAttribute.Tipo.TextBox)]
        public string SUSCRIPCION_URL { get; set; }

        //Indica que la suscripción se ha borrado, por lo tanto nadie de dicha suscripción deberá tener acceso al sistema
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool SUSCRIPCION_BORRADO { get; set; }

    }
}
