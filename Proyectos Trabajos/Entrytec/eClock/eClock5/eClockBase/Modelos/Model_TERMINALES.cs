using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;
namespace eClockBase.Modelos
{
    public class Model_TERMINALES
    {
        //Identificador unico de terminales dadas de alta en el sistema
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TERMINAL_ID { get; set; }
        //Indica el tipo de acceso que genera la terminal, entrada, salida o desconocido
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int TIPO_TERMINAL_ACCESO_ID { get; set; }
        //Nombre de la teminal (sirve para ubicar el equipo en las instalaciones)
        [Campo_String(true, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TERMINAL_NOMBRE { get; set; }
        //Indica el almacen de vectores donde se guardaran las huellas
        [Campo_Int(false, false, -1, -1, 1, Campo_IntAttribute.Tipo.ComboBox)]
        public int ALMACEN_VEC_ID { get; set; }
        //Indica el sitio al que pertenece la terminal
        [Campo_Int(false, false, -1, -1, 0, Campo_IntAttribute.Tipo.ComboBox)]
        public int SITIO_ID { get; set; }
        //Indica el modelo de terminal usada ej. EtherTrax
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int MODELO_TERMINAL_ID { get; set; }
        //Indica la tecnología principal ej Huella
        [Campo_Int(false, false, -1, -1, 0, Campo_IntAttribute.Tipo.ComboBox)]
        public int TIPO_TECNOLOGIA_ID { get; set; }
        //Indica si hay una tecnología adicional ej. Tarjeta y Huella
        [Campo_Int(false, false, -1, -1, 0, Campo_IntAttribute.Tipo.ComboBox)]
        public int TIPO_TECNOLOGIA_ADD_ID { get; set; }
        //RFU Cantidad en segundos que indica cada cuando se realizara la sincronizacion, si se coloca cero no sera automática dicha sincronización (en la versión 1.X siempre deberá tener un valor mayor a 5), por el momento se usara una Variable del sistema llamada SyncTimeOut
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TERMINAL_SINCRONIZACION { get; set; }
        //Contiene el nombre de la TABLA.CAMPO que contiene los datos del identificador único, ej. TABLA.TRACVE
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TERMINAL_CAMPO_LLAVE { get; set; }
        //Contiene el nombre de la TABLA.CAMPO que contiene los datos adicionales ej. TABLA.NS (el no. de tarjeta mifare)
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TERMINAL_CAMPO_ADICIONAL { get; set; }
        //Inidica si la Terminal esta configurada para poder enrolar.
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, true, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TERMINAL_ENROLA { get; set; }
        //Dirección IP de la terminal o URL
        [Campo_String(false, false, 255, "Red:127.0.0.1", Campo_StringAttribute.Tipo.TerminalDir)]
        public string TERMINAL_DIR { get; set; }
        //Indica si las checadas en esta terminal serviran para generar asistencia
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, true, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TERMINAL_ASISTENCIA { get; set; }
        //Indica si las checadas en esta terminal serviran para el reguistro y/o cobro de comidas
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TERMINAL_COMIDA { get; set; }
        //Cuantos minutos hay de diferencia entre la hora del servidor y la hora de la terminal
        [Campo_Int(false, false, -1, -1, 0, Campo_IntAttribute.Tipo.TextBox)]
        public int TERMINAL_MINUTOS_DIF { get; set; }
        //Indica si la terminal no permitirá entrar si esta fuera de horario de entrada  
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TERMINAL_VALIDAHORARIOE { get; set; }
        //Indica si la terminal no permitirá salir si su salida es menor o mayor a la permitida
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TERMINAL_VALIDAHORARIOS { get; set; }

        //Descripcion de la terminar (lugar, caracteristicas, etc)
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TERMINAL_DESCRIPCION { get; set; }
        //Fotografía que muestra donde esta colocada la terminal
        //Otro
        public Byte[] TERMINAL_BIN { get; set; }
        //Nombre del modelo que le asigna el fabircante
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TERMINAL_MODELO { get; set; }
        //Número de serie de la Terminal
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TERMINAL_NO_SERIE { get; set; }
        //Versión del Firmware instalado en la Terminal
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TERMINAL_FIRMWARE_VER { get; set; }
        //Cantidad de registros de huellas que la Terminal es capaz de almacenar.
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TERMINAL_NO_HUELLAS { get; set; }
        //Cantidad de registros de empleados que la Terminal es capaz de almacenar.
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TERMINAL_NO_EMPLEADOS { get; set; }
        //Cantidad de registros de tarjetas que la Terminal es capaz de almacenar.
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TERMINAL_NO_TARJETAS { get; set; }
        //Cantidad de registros de rostros que la Terminal es capaz de almacenar.
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TERMINAL_NO_ROSTROS { get; set; }
        //Cantidad de registros de checadas que la Terminal es capaz de almacenar.
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TERMINAL_NO_CHECADAS { get; set; }
        //Cantidad de registros de palmas que la Terminal es capaz de almacenar.
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TERMINAL_NO_PALMAS { get; set; }
        //Cantidad de registros de iris que la Terminal es capaz de almacenar.
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TERMINAL_NO_IRIS { get; set; }
        //Fecha de Inicio de la Garantia de la Terminal.
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime TERMINAL_GARANTIA_INICIO { get; set; }
        //Fecha de Expiración de la Garantia de la Terminal.
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime TERMINAL_GARANTIA_FIN { get; set; }
        //Agrupación a la que pertenece la Terminal
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TERMINAL_AGRUPACION { get; set; }

        //Indica el sitio al que pertenece la terminal
        [Campo_Int(false, false, -1, -1, 0, Campo_IntAttribute.Tipo.ComboBox)]
        public int SITIO_HIJO_ID { get; set; }

        //Indica que la terminal será usada como entrada
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TERMINAL_ESENTRADA { get; set; }
        //Indica que la terminal será usada como salida
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TERMINAL_ESSALIDA { get; set; }
        //Indica que acepta los tipos de acceso
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TERMINAL_ACEPTA_TA { get; set; }

        //Indica si ha sido borrado el registro
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TERMINAL_BORRADO { get; set; }
    }
}
