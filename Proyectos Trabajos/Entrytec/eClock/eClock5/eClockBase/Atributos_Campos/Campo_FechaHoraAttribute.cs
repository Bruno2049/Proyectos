using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Atributos_Campos
{
    public class Campo_FechaHoraAttribute : CampoAttribute
    {
        
        public DateTime? ValorMinimo { get; set; }
        public DateTime? ValorMaximo { get; set; }
        
        public enum Tipo
        {
            TextBox,
            DatePicker,
            Horas
        };
        public Tipo Control { get; set; }

        //public Campo_FechaHoraAttribute(bool bRequerido, bool bSoloLectura, DateTime? dtValorMinimo, DateTime? dtValorMaximo, DateTime? dtValorPredeterminado, Tipo cControl)
        public Campo_FechaHoraAttribute(bool bRequerido, bool bSoloLectura, string dtValorMinimo, string dtValorMaximo, string dtValorPredeterminado, Tipo cControl)
        {
            base.Requerido = bRequerido;
            SoloLectura = bSoloLectura;
            //ValorMinimo = dtValorMinimo;
            ValorMinimo = DateTime.Parse(dtValorMinimo);
            ValorMaximo = DateTime.Parse(dtValorMaximo);
            ValorPredeterminado = DateTime.Parse(dtValorPredeterminado);
            Control = cControl;
        }
    }
}
