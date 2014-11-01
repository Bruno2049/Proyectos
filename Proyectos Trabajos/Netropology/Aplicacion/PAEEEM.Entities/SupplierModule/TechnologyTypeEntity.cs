using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entities
{
    public class TechnologyTypeEntity
    {
        private int _Cve_Tipo_Tecnologia;
        public int Cve_Tipo_Tecnologia
        {
            get { return this._Cve_Tipo_Tecnologia; }
            set { this._Cve_Tipo_Tecnologia = value; }
        }

        private string _Dx_Nombre;
        public string Dx_Nombre
        {
            get { return this._Dx_Nombre; }
            set { this._Dx_Nombre = value; }
        }

        private string _Atributo1;
        public string Atributo1
        {
            get { return this._Atributo1; }
            set { this._Atributo1 = value; }
        }

        private string _Atributo2;
        public string Atributo2
        {
            get { return this._Atributo2; }
            set { this._Atributo2 = value; }
        }

        private string _Atributo3;
        public string Atributo3
        {
            get { return this._Atributo3; }
            set { this._Atributo3 = value; }
        }

        private string _Atributo4;
        public string Atributo4
        {
            get { return this._Atributo4; }
            set { this._Atributo4 = value; }
        }

        private string _Atributo5;
        public string Atributo5
        {
            get { return this._Atributo5; }
            set { this._Atributo5 = value; }
        }

        private DateTime _Dt_Fecha_Tipo_Tecnologia;
        public DateTime Dt_Fecha_Tipo_Tecnologia
        {
            get { return this._Dt_Fecha_Tipo_Tecnologia; }
            set { this._Dt_Fecha_Tipo_Tecnologia = value; }
        }
    }
}
