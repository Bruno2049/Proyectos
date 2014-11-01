using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_LOCALIZACIONES
    {
        //Identificador unico de la tabla
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int LOCALIZACION_ID { get; set; }
        //Código ISO 639-1 del lenguaje a mostrar
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string LOCALIZACION_IDIOMA { get; set; }
        //Texto en el idioma adecuado que se mostrara en el comando, botón o elemento
        [Campo_String(true, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string LOCALIZACION_LLAVE { get; set; }
        //Texto en el idioma adecuado que se mostrara en la etiqueta
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string LOCALIZACION_ETIQUETA { get; set; }
        //Descripción del elemento mostrado
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string LOCALIZACION_DESCRIPCION { get; set; }
        //Ayuda de como usar el comando, campo o elemento
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string LOCALIZACION_AYUDA { get; set; }
        //Ruta relativa de la imagen que se mostrara en pantalla
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string LOCALIZACION_IMAGEN { get; set; }
        //Acceso directo para los menus y funciones usando la tecla ALT
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string LOCALIZACION_ALTMENU { get; set; }
        //Ruta al archivo HTML que contiene el texto, solo valido para ayudas o textos grandes.
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string LOCALIZACION_HTML { get; set; }

    }
}
