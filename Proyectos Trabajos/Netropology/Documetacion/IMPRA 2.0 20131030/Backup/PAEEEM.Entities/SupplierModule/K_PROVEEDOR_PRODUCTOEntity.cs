/* ----------------------------------------------------------------------
 * File Name: K_PROVEEDOR_PRODUCTOEntity.cs
 * 
 * Create Author: coco
 * 
 * Create DateTime: 2011/10/08
 *
 * Description: K_PROVEEDOR_PRODUCTOEntity business entity
 *----------------------------------------------------------------------*/

using System;

namespace PAEEEM.Entities
{
    [Serializable()]
    public class K_PROVEEDOR_PRODUCTOEntity
    {
        ///<summary>
        ///
        ///</summary>
        private int _Id_Proveedor;
        public int Id_Proveedor
        {
            get { return _Id_Proveedor; }
            set { this._Id_Proveedor = value; }
        }
        ///<summary>
        ///
        ///</summary>
        private int _Cve_Producto;
        public int Cve_Producto
        {
            get { return _Cve_Producto; }
            set { this._Cve_Producto = value; }
        }
        ///<summary>
        ///
        ///</summary>
        private int _Cve_Estatus_Prov_Prod;
        public int Cve_Estatus_Prov_Prod
        {
            get { return _Cve_Estatus_Prov_Prod; }
            set { this._Cve_Estatus_Prov_Prod = value; }
        }
        ///<summary>
        ///
        ///</summary>
        private decimal _Mt_Precio_Unitario;
        public decimal Mt_Precio_Unitario
        {
            get { return _Mt_Precio_Unitario; }
            set { this._Mt_Precio_Unitario = value; }
        }
               ///<summary>
        ///
        ///</summary>
        private DateTime _Dt_Fecha_Prov_Prod;
        public DateTime Dt_Fecha_Prov_Prod
        {
            get { return _Dt_Fecha_Prov_Prod; }
            set { this._Dt_Fecha_Prov_Prod = value; }
        }
    }
}
