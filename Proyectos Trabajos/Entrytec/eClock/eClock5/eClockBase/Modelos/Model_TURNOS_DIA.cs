using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_TURNOS_DIA
    {
        //Identificador del turno semanal
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TURNO_DIA_ID { get; set; }
        //Hora mínima permitida de entrada, en Horario abierto se guardara la hora de inicio de dia
        [Campo_FechaHora(true, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime TURNO_DIA_HEMIN { get; set; }
        //Hora normal de entrada
        [Campo_FechaHora(true, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime TURNO_DIA_HE { get; set; }        
        //Hora máxima permitida de entrada
        [Campo_FechaHora(true, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime TURNO_DIA_HEMAX { get; set; }
        //Hora desde (>=) que será contemplado como retardo
        [Campo_FechaHora(true, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime TURNO_DIA_HERETARDO { get; set; }
        //Hora mínima permitida de salida
        [Campo_FechaHora(true, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime TURNO_DIA_HSMIN { get; set; }
        //Hora normal de salida
        [Campo_FechaHora(true, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime TURNO_DIA_HS { get; set; }
        //Hora máxima de salida
        [Campo_FechaHora(true, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime TURNO_DIA_HSMAX { get; set; }
        //Indica el boque de acceso (ej. entradas cada media hora)
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime TURNO_DIA_HBLOQUE { get; set; }
        //Indica la tolerancia que existira para cada bloque
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime TURNO_DIA_HBLOQUET { get; set; }
        //Tiempo que tiene que laborar el empleado para cumplir su jornada de trabajo si es cero se no se tomara en cuenta esta opción, en caso contrario si no cuenta con este tiempo tendra una alerta por salida antes de tiempo
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime TURNO_DIA_HTIEMPO { get; set; }
        //Indica si este horario diario tiene comida
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, true, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TURNO_DIA_HAYCOMIDA { get; set; }
        //Hora de salida a comer
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime TURNO_DIA_HCS { get; set; }
        //Hora de regreso de comer
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime TURNO_DIA_HCR { get; set; }
        //Tendra 0 si se trata de que tiene un horario fijo de comida, y cualquier valor en caso de que tenga un tiempo de comida ej 60 minutos
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime TURNO_DIA_HCTIEMPO { get; set; }
        //Minutos de tolerancia para comer
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime TURNO_DIA_HCTOLERA { get; set; }
        //Indica que se permitirá el cálculo automático de horas extras este día
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, true, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TURNO_DIA_PHEX { get; set; }
        //Indica que este turno día no generará asistencia
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TURNO_DIA_NO_ASIS { get; set; }
        //Hora desde (>=) que será contemplado como retardo minimo
        [Campo_FechaHora(true, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime TURNO_DIA_HERETARDO_B { get; set; }
        //Hora desde (>=) que será contemplado como retardo maximo
        [Campo_FechaHora(true, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime TURNO_DIA_HERETARDO_C { get; set; }
        //Hora desde (>=) que será contemplado como retardo superior
        [Campo_FechaHora(true, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime TURNO_DIA_HERETARDO_D { get; set; }

    }
}
