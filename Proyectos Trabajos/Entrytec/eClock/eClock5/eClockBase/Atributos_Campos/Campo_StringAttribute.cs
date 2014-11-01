using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Atributos_Campos
{
    public class Campo_StringAttribute : CampoAttribute
    {
        public  enum Tipo
        {
            TextBox,
            TextBoxPassword,
            TextBoxMemo,
            ComboBox,
            Color,
            CamposTextoDiseno,
            CamposTextoEdicion,
            TerminalDir
        };
        public Tipo Control {get;set;}
        public int LongitudMaxima { get; set; }

        public Campo_StringAttribute(bool bRequerido, bool bSoloLectura, int iLongitudMaxima, string sValorPredeterminado, Tipo cControl)
        {
            base.Requerido = bRequerido;
            SoloLectura = bSoloLectura;
            LongitudMaxima = iLongitudMaxima;
            ValorPredeterminado = sValorPredeterminado;
            Control = cControl;
        }
    }
}
