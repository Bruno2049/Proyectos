using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;
namespace eClockBase.Modelos.Sesion
{
    public class Model_Sesion
    {
        public string SESION_SEGURIDAD { get; set; }

        //Identificador de la tabla de usuarios
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int USUARIO_ID { get; set; }
        //
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int PERFIL_ID { get; set; }
        //Usuario (login)
        [Campo_String(true, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string USUARIO_USUARIO { get; set; }
        //Nombre propio de la persona
        [Campo_String(true, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string USUARIO_NOMBRE { get; set; }
        //Cualquier comentario sobre el usuario
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string USUARIO_DESCRIPCION { get; set; }
        //Contiene el e-mail del usuario
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string USUARIO_EMAIL { get; set; }
        //Indica que a este usuario se le enviaran mails de asistencia con todos sus empleados seleccionados
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, true, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool USUARIO_ENVMAILA { get; set; }
        //Suscripcion Predeterminada
        [Campo_Int(false, false, -1, -1, 0, Campo_IntAttribute.Tipo.TextBox)]
        public int SUSCRIPCION_ID { get; set; }
        //Contiene la agrupación a la cual tiene control, por default esta en blanco y significa a todos los empleados de la suscripcion
        [Campo_String(false, false, 2000, "", Campo_StringAttribute.Tipo.TextBox)]
        public string USUARIO_AGRUPACION { get; set; }
        //Indica que este usuario es supervisor, reporteador o administrador
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, true, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool USUARIO_ESSUP { get; set; }
        //Indica que este usuario es empleado
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool USUARIO_ESEMP { get; set; }
        //Identificador del persona_ID en caso de ser empleado
        [Campo_Int(false, false, -1, -1, 0, Campo_IntAttribute.Tipo.TextBox)]
        public int PERSONA_ID { get; set; }

    }
}
