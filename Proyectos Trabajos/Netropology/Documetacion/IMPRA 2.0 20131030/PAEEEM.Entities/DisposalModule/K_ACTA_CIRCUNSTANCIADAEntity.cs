/* ----------------------------------------------------------------------
 * File Name: K_CORTE_PARCIALModel.cs
 * 
 * Create Author: coco
 * 
 * Create DateTime: 2011/10/17
 *
 * Description: K_ACTA_CIRCUNSTANCIADAEntity business entity
 *----------------------------------------------------------------------*/

using System;

namespace PAEEEM.Entities
{
    [Serializable()]
    public class K_ACTA_CIRCUNSTANCIADAEntity
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
        private string _Codigo;
        public string Codigo
        {
            get { return _Codigo; }
            set { this._Codigo = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private DateTime _Dt_Periodo_Inicial;
        public DateTime Dt_Periodo_Inicial
        {
            get { return _Dt_Periodo_Inicial; }
            set { this._Dt_Periodo_Inicial = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private DateTime _Dt_Periodo_Final;
        public DateTime Dt_Periodo_Final
        {
            get { return _Dt_Periodo_Final; }
            set { this._Dt_Periodo_Final = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _Num_Productos;
        public string Num_Productos
        {
            get { return _Num_Productos; }
            set { this._Num_Productos = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private DateTime _Dt_Fecha_Creacion;
        public DateTime Dt_Fecha_Creacion
        {
            get { return _Dt_Fecha_Creacion; }
            set { this._Dt_Fecha_Creacion = value; }
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
