/* ----------------------------------------------------------------------
 * File Name:K_CREDITO_DESCUENTOEntity.cs
 * 
 * Create Author: coco
 * 
 * Create DateTime: 2011/7/11
 *
 * Description: K_CREDITO_DESCUENTO business entity
 *----------------------------------------------------------------------*/

using System;


namespace PAEEEM.Entities
{
    [Serializable()]
   public class K_CREDITO_DESCUENTOEntity
    {
        /// <summary>
        /// Id_Credito_Descuento
        /// </summary>
        private int _Id_Credito_Descuento;
        public int Id_Credito_Descuento
        {
            get { return _Id_Credito_Descuento; }
            set { this._Id_Credito_Descuento = value; }
        }
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
        /// Mt_Descuento
        /// </summary>
        private decimal _Mt_Descuento;
        public decimal Mt_Descuento
        {
            get { return _Mt_Descuento; }
            set { this._Mt_Descuento = value; }
        }
        /// <summary>
        /// Dt_Credito_Descuento
        /// </summary>
        private DateTime _Dt_Credito_Descuento;
        public DateTime Dt_Credito_Descuento
        {
            get { return _Dt_Credito_Descuento; }
            set { this._Dt_Credito_Descuento = value; }
        }
    }
}
