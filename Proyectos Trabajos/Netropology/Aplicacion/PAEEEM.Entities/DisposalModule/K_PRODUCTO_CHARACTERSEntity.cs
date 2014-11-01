using System;

namespace PAEEEM.Entities
{
    [Serializable]
    public class K_PRODUCTO_CHARACTERSEntity
    {
        /// <summary>
        /// Id_Credito_Sustitucion
        /// </summary>
        private int _Id_Credito_Sustitucion;
        public int Id_Credito_Sustitucion
        {
            get { return _Id_Credito_Sustitucion; }
            set { this._Id_Credito_Sustitucion = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _Dx_Marca;
        public string Dx_Marca
        {
            get { return _Dx_Marca; }
            set { this._Dx_Marca = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _No_Serial;
        public string No_Serial
        {
            get { return _No_Serial; }
            set { this._No_Serial = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _Dx_Color;
        public string Dx_Color
        {
            get { return _Dx_Color; }
            set { this._Dx_Color = value; }
        }
        ///<summary>
        ///
        ///</summary>
        private object _No_Peso;
        public object No_Peso
        {
            get { return _No_Peso; }
            set
            {
                if (null == value)
                {
                    this._No_Peso = DBNull.Value;
                }
                else
                {
                    this._No_Peso = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private object _Cve_Capacidad_Sust;
        public object Cve_Capacidad_Sust
        {
            get { return _Cve_Capacidad_Sust; }
            set
            {
                if (null == value)
                {
                    this._Cve_Capacidad_Sust = DBNull.Value;
                }
                else
                {
                    this._Cve_Capacidad_Sust = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _Dx_Antiguedad;
        public string Dx_Antiguedad
        {
            get { return _Dx_Antiguedad; }
            set { this._Dx_Antiguedad = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _Id_Pre_Folio;
        public string Id_Pre_Folio
        {
            get { return _Id_Pre_Folio; }
            set { this._Id_Pre_Folio = value; }
        }
        /// <summary>
        /// coco add 2012-01-12
        /// </summary>
        private string _Dx_Modelo_Producto;
        public string Dx_Modelo_Producto
        {
            get { return _Dx_Modelo_Producto; }
            set { this._Dx_Modelo_Producto = value; }
        }
    }
}
