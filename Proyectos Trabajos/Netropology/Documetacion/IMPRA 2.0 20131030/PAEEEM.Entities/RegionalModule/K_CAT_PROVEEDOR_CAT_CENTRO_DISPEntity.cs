using System;

namespace PAEEEM.Entities
{
    [Serializable()]
    public class K_CAT_PROVEEDOR_CAT_CENTRO_DISPModel
    {
        /// <summary>
        /// Id_Proveedor_Centro_Disp
        /// </summary>
        private int _Id_Proveedor_Centro_Disp;
        public int Id_Proveedor_Centro_Disp
        {
            get { return _Id_Proveedor_Centro_Disp; }
            set { _Id_Proveedor_Centro_Disp = value; }
        }
        /// <summary>
        /// Id_Proveedor
        /// </summary>
        private int _Id_Proveedor;
        public int Id_Proveedor
        {
            get { return _Id_Proveedor; }
            set { _Id_Proveedor = value; }
        }
        /// <summary>
        /// Id_Centro_Disp
        /// </summary>
        private int _Id_Centro_Disp;
        public int Id_Centro_Disp
        {
            get { return _Id_Centro_Disp; }
            set { _Id_Centro_Disp = value; }
        }
        /// <summary>
        /// Fg_Tipo_Proveedor
        /// </summary>
        private string _Fg_Tipo_Proveedor;
        public string Fg_Tipo_Proveedor
        {
            get { return _Fg_Tipo_Proveedor; }
            set { _Fg_Tipo_Proveedor = value; }
        }
        /// <summary>
        /// Fg_Tipo_Centro_Disp
        /// </summary>
        private string _Fg_Tipo_Centro_Disp;
        public string Fg_Tipo_Centro_Disp
        {
            get { return _Fg_Tipo_Centro_Disp; }
            set { _Fg_Tipo_Centro_Disp = value; }
        }
    }
}
