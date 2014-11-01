using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Atributos_Campos
{
    public class Campo_BinarioAttribute : CampoAttribute
    {
        public enum Tipo
        {
            Archivo,
            Imagen,
            Archivos,
            Imagenes
        };
        public Tipo Control { get; set; }
        public Campo_BinarioAttribute(bool bRequerido, bool bSoloLectura, Tipo cControl)
        {
            Requerido = bRequerido;
            SoloLectura = bSoloLectura;
            Control = cControl;
        }

    }
}
