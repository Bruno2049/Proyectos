/* ----------------------------------------------------------------------
 * File Name: K_RECUP_RESIDUOSModel.cs
 * 
 * Create Author: Tina
 * 
 * Create DateTime: 2011/10/12
 *
 * Description: K_RECUP_RESIDUOS business entity
 *----------------------------------------------------------------------*/

using System;

namespace PAEEEM.Entities
{
    [Serializable()]
    public class K_RECUP_RESIDUOSModel
    {
        ///<summary>
        ///
        ///</summary>
        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { this._ID = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private string _Codigo;
        public string Codigo
        {
            get { return _Codigo; }
            set { this._Codigo = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private string _No_Credito;
        public string No_Credito
        {
            get { return _No_Credito; }
            set { this._No_Credito = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private object _ID_Centro_Disp;
        public object ID_Centro_Disp
        {
            get { return _ID_Centro_Disp; }
            set
            {
                if (null == value)
                {
                    this._ID_Centro_Disp = DBNull.Value;
                }
                else
                {
                    this._ID_Centro_Disp = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private string _Codigo_Producto;
        public string Codigo_Producto
        {
            get { return _Codigo_Producto; }
            set { this._Codigo_Producto = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private object _Cve_Material;
        public object Cve_Material
        {
            get { return _Cve_Material; }
            set
            {
                if (null == value)
                {
                    this._Cve_Material = DBNull.Value;
                }
                else
                {
                    this._Cve_Material = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private object _Valor_Material;
        public object Valor_Material
        {
            get { return _Valor_Material; }
            set
            {
                if (null == value)
                {
                    this._Valor_Material = DBNull.Value;
                }
                else
                {
                    this._Valor_Material = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private string _Fg_Incluido;
        public string Fg_Incluido
        {
            get { return _Fg_Incluido; }
            set { this._Fg_Incluido = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private string _Fg_Tipo_Centro_Disp;
        public string Fg_Tipo_Centro_Disp
        {
            get { return _Fg_Tipo_Centro_Disp; }
            set { this._Fg_Tipo_Centro_Disp = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private object _Dt_Fecha_Creacion;
        public object Dt_Fecha_Creacion
        {
            get { return _Dt_Fecha_Creacion; }
            set
            {
                if (null == value)
                {
                    this._Dt_Fecha_Creacion = DBNull.Value;
                }
                else
                {
                    this._Dt_Fecha_Creacion = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private object _ID_Estatus;
        public object ID_Estatus
        {
            get { return _ID_Estatus; }
            set
            {
                if (null == value)
                {
                    this._ID_Estatus = DBNull.Value;
                }
                else
                {
                    this._ID_Estatus = value;
                }
            }
        }
    }
}
