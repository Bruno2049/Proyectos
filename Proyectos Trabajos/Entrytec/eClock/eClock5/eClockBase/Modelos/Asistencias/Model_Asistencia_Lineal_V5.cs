using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;


namespace eClockBase.Modelos.Asistencias
{
    public class Model_Asistencia_Lineal_V5
    {
        public int ID { get; set; }
        public int PID { get; set; }
        public int PLID { get; set; }
        public string NOM { get; set; }
        public string AGR { get; set; }
        public DateTime FECHA { get; set; }
        public DateTime E { get; set; }
        public DateTime CS { get; set; }
        public DateTime CR { get; set; }
        public DateTime S { get; set; }
        public string TURNO { get; set; }
        public string INC { get; set; }
        public string ABR { get; set; }
        public string COMI { get; set; }
        public DateTime TT { get; set; }
        public DateTime TDE { get; set; }
        public DateTime TE { get; set; }
        public DateTime TC { get; set; }
        public DateTime HES { get; set; }
        public DateTime HEC { get; set; }
        public DateTime HEA { get; set; }
        public DateTime HED { get; set; }
        public DateTime HET { get; set; }
        [JsonConverter(typeof(BoolConverter))]
        public bool PHEX { get; set; }
        [JsonConverter(typeof(BoolConverter))]
        public bool PCOMI { get; set; }
        [JsonConverter(typeof(BoolConverter))]
        public bool SELECCIONADO { get; set; }
        public int IC { get; set; }
        public object TAG { get; set; }

        public override string ToString()
        {
            return ID.ToString();/* +FECHA + E.ToString("dd/MM/yyyy") + CS.ToString("dd/MM/yyyy") +
                CS.ToString("dd/MM/yyyy") + S.ToString("dd/MM/yyyy") + TURNO + INC + ABR + COMI +
                TT + TDE + TE + TC + HES.ToString("dd/MM/yyyy") + HEC.ToString("dd/MM/yyyy")
                + PHEX + PCOMI;*/
        }
    }
}
