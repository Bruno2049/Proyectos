using System;

namespace PAEEEM.Entities
{
    [Serializable]
    public class K_RELACION_ACTA_CORTESEntity
    {
        /// <summary>
        /// 
        /// </summary>
        private int _ID_ACTA_CIRCUNSTANCIADA;
        public int ID_ACTA_CIRCUNSTANCIADA
        {
            get { return _ID_ACTA_CIRCUNSTANCIADA; }
            set { this._ID_ACTA_CIRCUNSTANCIADA = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private int _ID_CORTE_PARCIAL;
        public int ID_CORTE_PARCIAL
        {
            get { return _ID_CORTE_PARCIAL; }
            set { this._ID_CORTE_PARCIAL = value; }
        }
    }
}
