using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;

namespace eClockBase.Modelos
{
    public class Model_CONFIG_USUARIO
    {
        //Identificador de configuración para usuario
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int CONFIG_USUARIO_ID { get; set; }
        //Identificador de usuario se puede usar 0 para todos los usuarios
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int USUARIO_ID { get; set; }
        //Variable que se desea guardar
        [Campo_String(true, false, 1024, "", Campo_StringAttribute.Tipo.TextBox)]
        public string CONFIG_USUARIO_VARIABLE { get; set; }
        //Valor de la variable
        [Campo_String(false, false, 4000, "", Campo_StringAttribute.Tipo.TextBoxMemo)]
        public string CONFIG_USUARIO_VALOR { get; set; }
        //Valor de variable en Binario, solo que se requiera
        [Campo_BinarioAttribute(false, false, Campo_BinarioAttribute.Tipo.Archivo)]
        public byte[] CONFIG_USUARIO_VALOR_BIN { get; set; }

    }
}
