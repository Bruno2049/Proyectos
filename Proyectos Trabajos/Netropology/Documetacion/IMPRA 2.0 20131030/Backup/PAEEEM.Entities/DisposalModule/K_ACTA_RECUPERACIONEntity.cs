using System;

namespace PAEEEM.Entities
{
    [Serializable()]
    public class K_ACTA_RECUPERACIONModel
    {
        private string _Id_Acta_Recuperacion;
        public string Id_Acta_Recuperacion
        {
            get { return _Id_Acta_Recuperacion; }
            set { this._Id_Acta_Recuperacion = value; }
        }
        private DateTime _Dt_Fe_Inicio_Recup;
        public DateTime Dt_Fe_Inicio_Recup
        {
            get { return _Dt_Fe_Inicio_Recup; }
            set { this._Dt_Fe_Inicio_Recup = value; }
        }
        private DateTime _Dt_Fe_Fin_Recup;
        public DateTime Dt_Fe_Fin_Recup
        {
            get { return _Dt_Fe_Fin_Recup; }
            set { this._Dt_Fe_Fin_Recup = value; }
        }
        private DateTime _Dt_Fe_Creacion;
        public DateTime Dt_Fe_Creacion
        {
            get { return _Dt_Fe_Creacion; }
            set { this._Dt_Fe_Creacion = value; }
        }
    }
}
