using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;


namespace eClockBase.Modelos
{
    public class Model_TABLAS
    {
        //Nombre de la tabla
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TABLA_NOMBRE { get; set; }

        //Etiqueta que se usará para nombrar la tabla o el módulo, ej. catalogo de Turnos
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TABLA_ETIQUETA { get; set; }

        //Etiqueta que se usará para nombrar a los campos en plural, ejemplo Turnos
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TABLA_PLURAL { get; set; }

        //Etiqueta que se usará para nombrar a los campos en singular, ejemplo Turno
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TABLA_SINGULAR { get; set; }

        //Descripcion de la tabla, uso, etc.
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TABLA_DESCRIPCION { get; set; }

        //Contiene el campo o los campos llave que identificarán a cada registro, ejemplo Turno_ID
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TABLA_LLAVE { get; set; }

        //Qry que obtiene la lista de registros, se deberá validar la suscripción si se requiere
        [Campo_String(false, false, 2048, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TABLA_GRID_QRY { get; set; }

        //Contiene la URL relativa al Icono de la tabla 16x16
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TABLA_ICONO16 { get; set; }

        //Contiene la URL relativa al Icono de la tabla 24x24
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TABLA_ICONO24 { get; set; }

        //Contiene la URL relativa al Icono de la tabla 32x32
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TABLA_ICONO32 { get; set; }

        //Contiene la URL relativa al Icono de la tabla 64x64
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TABLA_ICONO64 { get; set; }

        //Contiene el nombre del campo que identifica si el registro se ha borrado, si este campo esta vacio significa que se borrará el registro
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TABLA_C_BORRADO { get; set; }

        //Indica si se guardará un historico con los valores de los cambios realizados
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(true, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TABLA_HISTORICO { get; set; }

        //Indica si se podrán crear nuevos registros
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(true, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TABLA_NUEVO { get; set; }

        //Indica si se podrán editar los registros
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(true, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TABLA_EDICION { get; set; }

        //Indica si se pueden borrar los registros
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(true, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TABLA_BORRAR { get; set; }

        //Contiene el listado de tablas, separadas por coma con las tablas hijo, es decir con las tablas que dependen de esta
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TABLA_HIJOS { get; set; }

        //Indica si se pueden borrar los registros
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(true, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TABLA_VAL_SUSCRIPCION { get; set; }

        //Indica que esta tabla no estará mas para ser usada
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(true, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TABLA_BORRADO { get; set; }
    }
}
