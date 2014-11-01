using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_CAMPOS_DATOS
    {
        //Identificador unico del registro
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int CAMPO_DATO_ID { get; set; }
        //
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int SUSCRIPCION_ID { get; set; }
        //
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TIPO_PERSONA_ID { get; set; }
        //
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int TIPO_DATO_ID { get; set; }
        //Nombre del campo
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string CAMPO_DATO_NOMBRE { get; set; }
        //Etiqueta del Campo
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string CAMPO_DATO_ETIQUETA { get; set; }
        //ToolTip con la ayuda en caso de no saber que capturar en este campo
        [Campo_String(false, false, 555, "", Campo_StringAttribute.Tipo.TextBox)]
        public string CAMPO_DATO_AYUDA { get; set; }
        //Indica si este campo se podrá almacenar mas de una vez por registro
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool CAMPO_DATO_REPETIDOS { get; set; }
        //Agrupador del campo Ej. Dirección, todos los campos con agrupador Dirección podran aparecer todos a la vez
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string CAMPO_DATO_AGRUPADOR { get; set; }
        //Contiene una lista de etiquetas multiples separadas por pipe | ej. para el campo email podrá decir 'Trabajo|Casa|Otro|Personalizado', en caso de estar agrupado, tomará la configuración del primer campo
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string CAMPO_DATO_MULTIPLE { get; set; }
        //Indica si permite en la etiqueta multiple agregar un campo personalizado de este tipo
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool CAMPO_DATO_MULTIPLEP { get; set; }
        //Indica si su uso será 0=opcional, 2=Opcional y oculto de manera predeterminada, 1=Requerido, -1=Oculto y no se mostrará, -2=Oculto, no se mostrará y no se usará en busquedas
        [Campo_Int(false, false, -1, -1, 0, Campo_IntAttribute.Tipo.TextBox)]
        public int CAMPO_DATO_USABILIDAD { get; set; }
        //Mascara para la captura del campo
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string CAMPO_DATO_MASCARA { get; set; }
        //Validación para poder guardar el campo
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string CAMPO_DATO_VALIDACION { get; set; }
        //Orden en el que aparecerá el campo
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int CAMPO_DATO_ORDEN { get; set; }
        //Valor predeterminado del campo
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string CAMPO_DATO_DEFAULT { get; set; }
        //Qry para validar si existen campos duplicados
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string CAMPO_DATO_DUPLICADO { get; set; }
        //Indica si el campo se podrá editar en la creación del registro
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, true, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool CAMPO_DATO_NUEVO { get; set; }

        public Model_CAMPOS_DATOS()
        {
        }

        public Model_CAMPOS_DATOS(int _CAMPO_DATO_ID, int _SUSCRIPCION_ID, int _TIPO_PERSONA_ID, int _TIPO_DATO_ID,
            string _CAMPO_DATO_NOMBRE, string _CAMPO_DATO_ETIQUETA, string _CAMPO_DATO_AYUDA, bool _CAMPO_DATO_REPETIDOS,
            string _CAMPO_DATO_AGRUPADOR, string _CAMPO_DATO_MULTIPLE, bool _CAMPO_DATO_MULTIPLEP, int _CAMPO_DATO_USABILIDAD,
            string _CAMPO_DATO_MASCARA, string _CAMPO_DATO_VALIDACION, int _CAMPO_DATO_ORDEN, string _CAMPO_DATO_DEFAULT,
        string _CAMPO_DATO_DUPLICADO, bool _CAMPO_DATO_NUEVO)
        {
            CAMPO_DATO_ID = _CAMPO_DATO_ID;
            SUSCRIPCION_ID = _SUSCRIPCION_ID;
            TIPO_PERSONA_ID = _TIPO_PERSONA_ID;
            TIPO_DATO_ID = _TIPO_DATO_ID;
            CAMPO_DATO_NOMBRE = _CAMPO_DATO_NOMBRE;
            CAMPO_DATO_ETIQUETA = _CAMPO_DATO_ETIQUETA;
            CAMPO_DATO_AYUDA = _CAMPO_DATO_AYUDA;
            CAMPO_DATO_REPETIDOS = _CAMPO_DATO_REPETIDOS;
            CAMPO_DATO_AGRUPADOR = _CAMPO_DATO_AGRUPADOR;
            CAMPO_DATO_MULTIPLE = _CAMPO_DATO_MULTIPLE;
            CAMPO_DATO_MULTIPLEP = _CAMPO_DATO_MULTIPLEP;
            CAMPO_DATO_USABILIDAD = _CAMPO_DATO_USABILIDAD;
            CAMPO_DATO_MASCARA = _CAMPO_DATO_MASCARA;
            CAMPO_DATO_VALIDACION = _CAMPO_DATO_VALIDACION;
            CAMPO_DATO_ORDEN = _CAMPO_DATO_ORDEN;
            CAMPO_DATO_DEFAULT = _CAMPO_DATO_DEFAULT;
            CAMPO_DATO_DUPLICADO = _CAMPO_DATO_DUPLICADO;
            CAMPO_DATO_NUEVO = _CAMPO_DATO_NUEVO;
        }

    }
}
