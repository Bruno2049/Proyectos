using System;

namespace PAEEEM.Entities
{
    [Serializable]
    public class K_RECUPERACIONModel
    {
        private int _Id_Recuperacion;
        public int Id_Recuperacion
        {
            get { return _Id_Recuperacion; }
            set { this._Id_Recuperacion = value; }
        }
        private double _No_Material;
        public double No_Material
        {
            get { return _No_Material; }
            set { this._No_Material = value; }
        }
        private DateTime _Dt_Fecha_Recuperacion;
        public DateTime Dt_Fecha_Recuperacion
        {
            get { return _Dt_Fecha_Recuperacion; }
            set { this._Dt_Fecha_Recuperacion = value; }
        }
        private int _Cve_Tecnologia;
        public int Cve_Tecnologia
        {
            get { return _Cve_Tecnologia; }
            set { this._Cve_Tecnologia = value; }
        }
        private int _Cve_Residuo_Material;
        public int Cve_Residuo_Material
        {
            get { return _Cve_Residuo_Material; }
            set { this._Cve_Residuo_Material = value; }
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
        private int _Id_Acta_Recuperacion;
        public int Id_Acta_Recuperacion
        {
            get { return _Id_Acta_Recuperacion; }
            set { this._Id_Acta_Recuperacion = value; }
        }
    }
}
