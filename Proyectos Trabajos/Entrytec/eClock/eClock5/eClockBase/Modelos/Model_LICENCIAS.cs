using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_LICENCIAS
    {
        //Identificador único para la Licencia
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int LICENCIA_ID { get; set; }
        //
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int LICENCIA_ORIGEN_ID { get; set; }
        //
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int LICENCIA_DISTRIBUIDOR_ID { get; set; }
        //Usuario generado para esta licencia
        [Campo_Int(true, false, -1, -1, 0, Campo_IntAttribute.Tipo.ComboBox)]
        public int USUARIO_ID { get; set; }
        //
        [Campo_Int(true, false, -1, -1, 0, Campo_IntAttribute.Tipo.ComboBox)]
        public int SUSCRIPCION_ID { get; set; }
        //Número de la Licencia incluyendo giones
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string LICENCIA { get; set; }
        //Fecha y Hora de creación
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime LICENCIA_CREACIONFH { get; set; }
        //Fecha y Hora de vigencia de la instalación
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime LICENCIA_VIGENCIAFH { get; set; }
        //Fecha y Hora de instalación
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime LICENCIA_INSTALACIONFH { get; set; }
        //Fecha y Hora de la última compra o pago a la licencia o suscripción
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime LICENCIA_COMPRAFH { get; set; }
        //Cantidad de instalaciones realizadas
        [Campo_Int(false, false, -1, -1, 0, Campo_IntAttribute.Tipo.TextBox)]
        public int LICENCIA_INSTALACIONES { get; set; }
        //Fecha y hora de su último uso.
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime LICENCIA_USOFH { get; set; }
        //Version de software y datos del mismo
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string LICENCIA_SOFTWARE { get; set; }
        //Indica si se deberá actualizar el software en su siguiente validación de licencia.
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool LICENCIA_SOFTWARE_ACT { get; set; }
        //Datos de la máquina PC
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string LICENCIA_MAQUINA { get; set; }
        //Indica si esta borrado
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool LICENCIA_BORRADO { get; set; }

    }
}
