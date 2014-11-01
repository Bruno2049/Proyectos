/* ----------------------------------------------------------------------
 * File Name:K_CREDITO_PRODUCTOEntity.cs
 * 
 * Create Author: coco
 * 
 * Create DateTime: 2011/7/11
 *
 * Description: K_CREDITO_PRODUCTO business entity
 *----------------------------------------------------------------------*/

using System;

namespace PAEEEM.Entities
{
    [Serializable()]
    public class K_CREDITO_PRODUCTOEntity
    {
        /// <summary>
        /// No_Credito
        /// </summary>
        private string _No_Credito;
        public string No_Credito
        {
            get { return _No_Credito; }
            set { this._No_Credito = value; }
        }
        /// <summary>
        /// Cve_Producto
        /// </summary>
        private int _Cve_Producto;
        public int Cve_Producto
        {
            get { return _Cve_Producto; }
            set { this._Cve_Producto = value; }
        }
        /// <summary>
        /// No_Cantidad
        /// </summary>
        private int _No_Cantidad;
        public int No_Cantidad
        {
            get { return _No_Cantidad; }
            set { this._No_Cantidad = value; }
        }
        /// <summary>
        /// Mt_Precio_Unitario
        /// </summary>
        private decimal _Mt_Precio_Unitario;
        public decimal Mt_Precio_Unitario
        {
            get { return _Mt_Precio_Unitario; }
            set { this._Mt_Precio_Unitario = value; }
        }
        /// <summary>
        /// Mt_Precio_Unitario_Sin_IVA
        /// </summary>
        private decimal _Mt_Precio_Unitario_Sin_IVA;
        public decimal Mt_Precio_Unitario_Sin_IVA
        {
            get { return _Mt_Precio_Unitario_Sin_IVA; }
            set { this._Mt_Precio_Unitario_Sin_IVA = value; }
        }
        /// <summary>
        /// Mt_Total
        /// </summary>
        private decimal _Mt_Total;
        public decimal Mt_Total
        {
            get { return _Mt_Total; }
            set { this._Mt_Total = value; }
        }
        /// <summary>
        /// Dt_Fecha_Credito_Producto
        /// </summary>
        private DateTime _Dt_Fecha_Credito_Producto;
        public DateTime Dt_Fecha_Credito_Producto
        {
            get { return _Dt_Fecha_Credito_Producto; }
            set { this._Dt_Fecha_Credito_Producto = value; }
        }
        /// <summary>
        /// Cve_Producto_Capacidad
        /// </summary>
        private int _Cve_Producto_Capacidad;
        public int Cve_Producto_Capacidad
        {
            get { return _Cve_Producto_Capacidad; }
            set { this._Cve_Producto_Capacidad = value; }
        }
        /// <summary>
        /// Mt_Precio_Unitario
        /// </summary>
        private decimal _Mt_Gastos_Instalacion_Mano_Obra;
        public decimal Mt_Gastos_Instalacion_Mano_Obra
        {
            get { return _Mt_Gastos_Instalacion_Mano_Obra; }
            set { this._Mt_Gastos_Instalacion_Mano_Obra = value; }
        }
    }
}
