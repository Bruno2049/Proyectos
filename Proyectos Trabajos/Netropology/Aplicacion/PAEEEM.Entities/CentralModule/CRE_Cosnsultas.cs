using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entities.CentralModule
{
    [Serializable()]
    public class CRE_Cosnsultas
    {
        private object _CVE_Consulta;
        public object CVE_Consulta
        {
            get { return _CVE_Consulta; }
            set
            {
                if (null == value)
                {
                    this._CVE_Consulta = DBNull.Value;
                }
                else
                {
                    this._CVE_Consulta = value;
                }
            }
        }

        private string _Nombre_Consulta;
        public string Nombre_Consulta
        {
            get { return _Nombre_Consulta; }
            set { this._Nombre_Consulta = value; }
        }
      
        private string _Descripcion_Consulta;
        public string Descripcion_Consulta
        {
            get { return _Descripcion_Consulta; }
            set { this._Descripcion_Consulta = value; }
        }
       
        private int _Validado;
        public int Validado
        {
            get { return _Validado; }
            set { this._Validado = value; }
        }
        
        private int _Estatus;
        public int Estatus
        {
            get { return _Estatus; }
            set { this._Estatus = value; }
        }
        
        private string _Adicionado_Por;
        public string Adicionado_Por
        {
            get { return _Adicionado_Por; }
            set { this._Adicionado_Por = value; }
        }
        
        private DateTime _Fecha_Adicion;
        public DateTime Fecha_Adicion
        {
            get { return _Fecha_Adicion; }
            set { this._Fecha_Adicion = value; }
        }
    
    }
}
