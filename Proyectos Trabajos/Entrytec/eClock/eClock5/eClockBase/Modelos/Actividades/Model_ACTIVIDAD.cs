using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;
namespace eClockBase.Modelos.Actividades
{
    public class Model_ACTIVIDAD
    {
        //Identificador unico de registro
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int ACTIVIDAD_ID { get; set; }
        //Indica de que persona se refiere el acceso
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int EDO_ACTIVIDAD_ID { get; set; }
        //Terminal en la que ocurrio dicho acceso, si es 0 significa que fue una justificacion
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string ACTIVIDAD_NOMBRE { get; set; }
        //Indica la descripcion de la actividad.
        [Campo_String(false, false, 4000, "", Campo_StringAttribute.Tipo.TextBoxMemo)]
        public string ACTIVIDAD_DESCRIPCION { get; set; }
        //Indica una variable de las actividades a invitados.
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string ACTIVIDAD_INVITADOS { get; set; }



        //
        [Campo_FechaHora(true, false, "2006-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime ACTIVIDAD_DESDE { get; set; }
        //
        [Campo_FechaHora(true, false, "2006-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime ACTIVIDAD_HASTA { get; set; }
        //
        [Campo_FechaHora(true, false, "2006-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime ACTIVIDAD_INSCRIPDESDE { get; set; }
        //
        [Campo_FechaHora(true, false, "2006-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime ACTIVIDAD_INSCRIPHASTA { get; set; }
        //Indica una variable de las actividades por persona.
        [Campo_String(false, false, 4000, "", Campo_StringAttribute.Tipo.TextBoxMemo)]
        public string ACTIVIDAD_PERSONAS { get; set; }
        //Identificador unico de CUPO
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int ACTIVIDAD_CUPO { get; set; }
        //eMails con las personas que recibiran un resumen díario de las personas inscritas a la actividad
        [Campo_String(false, false, 4000, "", Campo_StringAttribute.Tipo.TextBoxMemo)]
        public string ACTIVIDAD_RESUMEN { get; set; }
        //eMails con las personas que recibirán un email cada que alguien se inscriba a la actividad
        [Campo_String(false, false, 4000, "", Campo_StringAttribute.Tipo.TextBoxMemo)]
        public string ACTIVIDAD_MOMENTO { get; set; }
        [Campo_String(false, false, 4000, "", Campo_StringAttribute.Tipo.CamposTextoDiseno)]
        public string ACTIVIDAD_CAMPOS { get; set; }


    }
}
