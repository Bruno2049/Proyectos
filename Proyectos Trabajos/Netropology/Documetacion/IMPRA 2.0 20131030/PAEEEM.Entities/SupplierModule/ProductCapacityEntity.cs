using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entities
{
    public class ProductCapacityEntity
    {
        private int _Cve_Producto_Capacidad;
        public int Cve_Producto_Capacidad
        {
            get
            {
                return this._Cve_Producto_Capacidad;
            }
            set
            {
                this._Cve_Producto_Capacidad = value;
            }
        }

        private int _Cve_Tecnologia;
        public int Cve_Tecnologia
        {
            get
            {
                return this._Cve_Tecnologia;
            }
            set
            {
                this._Cve_Tecnologia = value;
            }
        }

        private float _Ft_Capacidad;
        public float Ft_Capacidad
        {
            get
            {
                return this._Ft_Capacidad;
            }
            set
            {
                this._Ft_Capacidad = value;
            }
        }

        private DateTime _Dt_Fecha_Producto_Capacidad;
        public DateTime Dt_Fecha_Producto_Capacidad
        {
            get
            {
                return _Dt_Fecha_Producto_Capacidad;
            }
            set
            {
                this._Dt_Fecha_Producto_Capacidad = value;
            }
        }
    }
}
