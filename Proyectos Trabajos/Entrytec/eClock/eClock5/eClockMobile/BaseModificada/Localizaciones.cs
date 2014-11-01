using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eClockMobile.BaseModificada
{
    public class Localizaciones : eClockBase.Controladores.Localizaciones
    {
        public static string Dinamico = "_DIN";
        public Localizaciones(eClockBase.CeC_SesionBase SesionBase)
            : base(SesionBase)
        {
        }      
    }
}
