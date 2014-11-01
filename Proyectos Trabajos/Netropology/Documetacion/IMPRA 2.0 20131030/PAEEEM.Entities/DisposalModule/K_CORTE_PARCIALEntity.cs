/* ----------------------------------------------------------------------
 * File Name: K_CORTE_PARCIALModel.cs
 * 
 * Create Author: Tina
 * 
 * Create DateTime: 2011/10/13
 *
 * Description: K_CORTE_PARCIAL business entity
 *----------------------------------------------------------------------*/

using System;

namespace PAEEEM.Entities
{
    [Serializable()]
    public class K_CORTE_PARCIALModel
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

        ///<summary>
        ///
        ///</summary>
        private string _Codigo_Partial;
        public string Codigo_Partial
        {
            get { return _Codigo_Partial; }
            set { this._Codigo_Partial = value; }
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
        private object _Cve_Tecnologia;
        public object Cve_Tecnologia
        {
            get { return _Cve_Tecnologia; }
            set
            {
                if (null == value)
                {
                    this._Cve_Tecnologia = DBNull.Value;
                }
                else
                {
                    this._Cve_Tecnologia = value;
                }
            }
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

        /// <summary>
        /// 
        /// </summary>
        private string _Fg_Aprobacion;
        public string Fg_Aprobacion
        {
            get { return _Fg_Aprobacion; }
            set { this._Fg_Aprobacion = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private object _Peso_Inicial;
        public object Peso_Inicial
        {
            get { return _Peso_Inicial; }
            set
            {
                if (null == value)
                {
                    this._Peso_Inicial = DBNull.Value;
                }
                else
                {
                    this._Peso_Inicial = value;
                }
            }
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
    }
}
