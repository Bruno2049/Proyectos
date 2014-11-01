using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.Alta_Solicitud
{
    public class CapturaAuxiliar
    {
        public string Rpu { get; set; }
        public string Nombre { get; set; }
        public int CveTarifa { get; set; }
        public string Tarifa { get; set; }
        public string Cuenta { get; set; }
        public int CvePeriodoPago { get; set; }
        public string PeriodoPago { get; set; }
        public byte TotalPeriodos { get; set; }
        public byte Vigencia { get; set; }
        public byte CveEstatus { get; set; }
        public string Estatus { get; set; }
        public DateTime FechaAdicion { get; set; }
        public string AdicionadoPor { get; set; }
    }
}
