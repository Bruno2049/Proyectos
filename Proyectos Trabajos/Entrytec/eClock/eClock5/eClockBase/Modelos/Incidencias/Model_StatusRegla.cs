using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Incidencias
{
    public class Model_StatusRegla
    {
        public int PersonaID { get; set; }
        public int PersonaLinkID { get; set; }
        public string PersonaNombre { get; set; }
        public string Agrupacion { get; set; }
        public bool Permitido { get; set; }
        public bool Inventario { get; set; }
        public DateTime FechaDesde { get; set; }
        public decimal Saldo { get; set; }
        public decimal AConsumir { get; set; }
        public decimal ValorMinimo { get; set; }
        public int TIMESPAN_ID { get; set; }
        public int TIPO_UNIDAD_ID { get; set; }
        public int TIPO_REDONDEO_ID { get; set; }
        public bool Sumar { get; set; }
    }
}
