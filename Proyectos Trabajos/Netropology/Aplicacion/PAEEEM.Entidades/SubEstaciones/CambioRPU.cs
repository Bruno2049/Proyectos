using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.SubEstaciones
{
    public class CambioRPU
    {
        private string _No_Credito;

        public string No_Credito
        {
            get { return _No_Credito; }
            set { _No_Credito = value; }
        }

        private string _Razon_Social;

        public string Razon_Social
        {
            get { return _Razon_Social; }
            set { _Razon_Social = value; }
        }

        private string _Antiguo_RPU;

        public string Antiguo_RPU
        {
            get { return _Antiguo_RPU; }
            set { _Antiguo_RPU = value; }
        }

        private string _Nuevo_RPU;

        public string Nuevo_RPU
        {
            get { return _Nuevo_RPU; }
            set { _Nuevo_RPU = value; }
        }

        private string _Nombre;

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
        
    }
}
