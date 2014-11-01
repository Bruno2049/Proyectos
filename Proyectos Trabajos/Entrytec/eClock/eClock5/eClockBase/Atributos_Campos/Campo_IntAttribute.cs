using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Atributos_Campos
{
    public class Campo_IntAttribute : CampoAttribute
    {
        public enum Tipo
        {
            TextBox,
            Spin,
            ComboBox,
            Option,
            Toggle,
            Color,
            PersonaLinkID
        };
        public Tipo Control { get; set; }
        public int ValorMinimo { get; set; }
        public int ValorMaximo { get; set; }


        public Campo_IntAttribute(bool bRequerido, bool bSoloLectura, int iValorMinimo, int iValorMaximo, int iValorPredeterminado, Tipo cControl)
        {
            base.Requerido = bRequerido;
            SoloLectura = bSoloLectura;
            ValorMinimo = iValorMinimo;
            ValorMaximo = iValorMaximo;
            ValorPredeterminado = iValorPredeterminado;
            Control = cControl;
        }


    }
}
