using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Atributos_Campos
{
    public class Campo_BoolAttribute : CampoAttribute
    {
        public enum Tipo
        {
            CheckBox,
            Switch,
            Toggle
        };
        public Tipo Control { get; set; }

        public Campo_BoolAttribute(bool bRequerido, bool bSoloLectura, bool bValorPredeterminado, Tipo cControl)
        {
            base.Requerido = bRequerido;
            SoloLectura = bSoloLectura;
            ValorPredeterminado = bValorPredeterminado;
            Control = cControl;
        }
    }
}
