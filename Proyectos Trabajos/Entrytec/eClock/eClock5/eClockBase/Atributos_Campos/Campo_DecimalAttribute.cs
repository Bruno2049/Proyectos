using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Atributos_Campos
{
    public class Campo_DecimalAttribute : CampoAttribute
    {
        public enum Tipo
        {
            TextBox,
            Spin,
            ComboBox
        };
        public Tipo Control { get; set; }
        public decimal ValorMinimo { get; set; }
        public decimal ValorMaximo { get; set; }

        //public Campo_DecimalAttribute(bool bRequerido, bool bSoloLectura, decimal dValorMinimo, decimal dValorMaximo, decimal dValorPredeterminado, Tipo cControl)
        public Campo_DecimalAttribute(bool bRequerido, bool bSoloLectura, float dValorMinimo, float dValorMaximo, float dValorPredeterminado, Tipo cControl)
        {
            base.Requerido = bRequerido;
            SoloLectura = bSoloLectura;
            ValorMinimo =(decimal) dValorMinimo;
            ValorMaximo =(decimal) dValorMaximo;
            ValorPredeterminado =(decimal) dValorPredeterminado;
            Control = cControl;
        }
    }
}
