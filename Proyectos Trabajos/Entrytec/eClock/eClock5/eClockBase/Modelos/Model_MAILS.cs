using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;
namespace eClockBase.Modelos
{
    public class Model_MAILS
    {
        //Identificador de Mail
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int MAIL_ID { get; set; }
        //Destinatarios separados con punto y coma
        [Campo_String(false, false, 1024, "", Campo_StringAttribute.Tipo.TextBox)]
        public string MAIL_DESTINOS { get; set; }
        //Destinatarios de copia separados por punto y coma
        [Campo_String(false, false, 1024, "", Campo_StringAttribute.Tipo.TextBox)]
        public string MAIL_COPIAS { get; set; }
        //titulo del MAIL
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string MAIL_TITULO { get; set; }
        //Contenido del Mail
        [Campo_String(false, false, 8000, "", Campo_StringAttribute.Tipo.TextBox)]
        public string MAIL_MENSAJE { get; set; }
        //archivo adjunto
        //Otro
        [Campo_Binario(false, false, Campo_BinarioAttribute.Tipo.Archivos)]
        public byte[] MAIL_ADJUNTO { get; set; }
        //Nombre del archivo adjunto
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string MAIL_ADJUNTO_NOMBRE { get; set; }
        //Fecha y hora en que se cargo
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime MAIL_FECHAHORA { get; set; }
        //Indica si se ha enviado el mensage
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool MAIL_ENVIADO { get; set; }
        //fecha y hora de envio
        [JsonConverter(typeof(BoolConverter))]
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime MAIL_FECHAHORAE { get; set; }

    }
}
