/* ----------------------------------------------------------------------
 * File Name:K_CREDITO_COSTOEntity.cs
 * 
 * Create Author: coco
 * 
 * Create DateTime: 2011/7/12
 *
 * Description: K_CREDITO_COSTO business entity
 *----------------------------------------------------------------------*/

using System;


namespace PAEEEM.Entities
{
    [Serializable()]
    public class K_CREDITO_COSTOEntity
    {
        /// <summary>
        /// Id_Credito_Costo
        /// </summary>
        private int _Id_Credito_Costo;
        public int Id_Credito_Costo
        {
            get { return _Id_Credito_Costo; }
            set { this._Id_Credito_Costo = value; }
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
        /// Mt_Costo
        /// </summary>
        private decimal _Mt_Costo;
        public decimal Mt_Costo
        {
            get { return _Mt_Costo; }
            set { this._Mt_Costo = value; }
        }
        /// <summary>
        /// Dt_Credito_Costo
        /// </summary>
        private DateTime _Dt_Credito_Costo;
        public DateTime Dt_Credito_Costo
        {
            get { return _Dt_Credito_Costo; }
            set { this._Dt_Credito_Costo = value; }
        }
    }
}
