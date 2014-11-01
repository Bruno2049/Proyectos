using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entities
{
    public class RegionalEntity
    {
        private int _regional;
        public int RegionalID
        {
            get { return _regional; }
            set { this._regional = value; }
        }

        private string _regional_name;
        public string RegionalNombre
        {
            get { return _regional_name; }
            set { this._regional_name = value; }
        }

        private string _responsible_name;
        public string ResponsibleNombre
        {
            get { return _responsible_name; }
            set { this._responsible_name = value; }
        }

        private string _regional_puesto;
        public string RegionalPuesto
        {
            get { return _regional_puesto; }
            set { this._regional_puesto = value; }
        }

        private DateTime _regional_fetch;
        public DateTime RegionalFetch
        {
            get { return _regional_fetch; }
            set { this._regional_fetch = value; }
        }
    }
}
