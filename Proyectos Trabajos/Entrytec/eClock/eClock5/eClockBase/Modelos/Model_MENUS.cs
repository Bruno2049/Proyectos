using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_MENUS
    {
        //Identificador de Menu
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int MENU_ID { get; set; }
        //Nombre de la mascara
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int TIPO_MENU_ID { get; set; }
        //Orden en el que se mostrara el menu
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int MENU_ORDEN { get; set; }
        //Identificador del Menu Padre
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int MENU_PADRE_ID { get; set; }
        //Identificador del Menu Contextual
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int MENU_CONTEXTUAL_ID { get; set; }
        //Identificador del Menu 3D
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int MENU_3D_ID { get; set; }
        //Ruta a donde apunta el menu
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string MENU_LINK { get; set; }
        //Codigo para crear hijos
        [Campo_String(false, false, 512, "", Campo_StringAttribute.Tipo.TextBox)]
        public string MENU_HIJOS { get; set; }
        //Codigo para crear hermanos
        [Campo_String(false, false, 512, "", Campo_StringAttribute.Tipo.TextBox)]
        public string MENU_HERMANOS { get; set; }
        //Etiqueta
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string MENU_ETIQUETA { get; set; }
        //Descripcion
        [Campo_String(false, false, 512, "", Campo_StringAttribute.Tipo.TextBox)]
        public string MENU_DESCRIPCION { get; set; }
        //Ayuda del Menu
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string MENU_AYUDA { get; set; }
        //URL Con la imagen
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string MENU_IMAGEN { get; set; }
        //Tecla de acceso directo
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string MENU_ALT { get; set; }
        //Indica si el menu se ha borrado
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, true, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool MENU_BORRADO { get; set; }

    }
}
