using System;

namespace PAEEEM.Entities
{
    [Serializable]
    public class K_INHABILITACIONModel
    {
        private int _Id_Inhabilitacion;
        public int Id_Inhabilitacion
        {
            get { return _Id_Inhabilitacion; }
            set { this._Id_Inhabilitacion = value; }
        }
        private DateTime _Dt_Fecha_Inhabilitacion;
        public DateTime Dt_Fecha_Inhabilitacion
        {
            get { return _Dt_Fecha_Inhabilitacion; }
            set { this._Dt_Fecha_Inhabilitacion = value; }
        }
        private int _Id_Centro_Disp;
        public int Id_Centro_Disp
        {
            get { return _Id_Centro_Disp; }
            set { this._Id_Centro_Disp = value; }
        }
        private string _Fg_Tipo_Centro_Disp;
        public string Fg_Tipo_Centro_Disp
        {
            get { return _Fg_Tipo_Centro_Disp; }
            set { this._Fg_Tipo_Centro_Disp = value; }
        }
        private int _Id_Acta_Inhabilitacion;
        public int Id_Acta_Inhabilitacion
        {
            get { return _Id_Acta_Inhabilitacion; }
            set { this._Id_Acta_Inhabilitacion = value; }
        }
    }
}
