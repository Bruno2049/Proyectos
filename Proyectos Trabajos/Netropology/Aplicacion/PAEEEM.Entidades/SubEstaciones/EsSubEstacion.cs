using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.SubEstaciones
{
    public class EsSubEstacion
    {
        private string _No_credito;

        public string No_Credit
        {
            get { return _No_credito; }
            set { _No_credito = value; }
        }

        private string _Nombre_Producto;

        public string Nombre_producto
        {
            get { return _Nombre_Producto; }
            set { _Nombre_Producto = value; }
        }

        private string _Nombre_General;

        public string Nombre_General
        {
            get { return _Nombre_General; }
            set { _Nombre_General = value; }
        }
        
        
        
    }
}
