using System;

namespace PAEEEM.Entities
{
    [Serializable()]
    public class K_ACTA_INHABILITACIONModel
    {
        private string _Id_Acta_Inhabilitacion;
        public string Id_Acta_Inhabilitacion
        {
            get { return _Id_Acta_Inhabilitacion; }
            set { this._Id_Acta_Inhabilitacion = value; }
        }
        private DateTime _Dt_Fe_Inicio_Inhabilita;
        public DateTime Dt_Fe_Inicio_Inhabilita
        {
            get { return _Dt_Fe_Inicio_Inhabilita; }
            set { this._Dt_Fe_Inicio_Inhabilita = value; }
        }
        private DateTime _Dt_Fe_Fin_Inhabilita;
        public DateTime Dt_Fe_Fin_Inhabilita
        {
            get { return _Dt_Fe_Fin_Inhabilita; }
            set { this._Dt_Fe_Fin_Inhabilita = value; }
        }
        private DateTime _Dt_Fe_Creacion;
        public DateTime Dt_Fe_Creacion
        {
            get { return _Dt_Fe_Creacion; }
            set { this._Dt_Fe_Creacion = value; }
        }
    }
}
