/* ----------------------------------------------------------------------
 * File Name:K_CENTRO_DISP_TECNOLOGIAEntity.cs
 * 
 * Create Author: Tina
 * 
 * Create DateTime: 2011/10/27
 *
 * Description: K_CENTRO_DISP_TECNOLOGIA business entity
 *----------------------------------------------------------------------*/

using System;

namespace PAEEEM.Entities
{
    [Serializable()]
    public class K_CENTRO_DISP_TECNOLOGIAModel
    {
        /// <summary>
        /// Id_Centro_Disp
        /// </summary>
        private int _Id_Centro_Disp;
        public int Id_Centro_Disp
        {
            get { return _Id_Centro_Disp; }
            set { this._Id_Centro_Disp = value; }
        }
        /// <summary>
        /// Cve_Tecnologia
        /// </summary>
        private int _Cve_Tecnologia;
        public int Cve_Tecnologia
        {
            get { return _Cve_Tecnologia; }
            set { this._Cve_Tecnologia = value; }
        }
        /// <summary>
        /// Cve_Estatus_CD_Tec
        /// </summary>
        private int _Cve_Estatus_CD_Tec;
        public int Cve_Estatus_CD_Tec
        {
            get { return _Cve_Estatus_CD_Tec; }
            set { this._Cve_Estatus_CD_Tec = value; }
        }
        /// <summary>
        /// Dt_Fecha_CD_Tec
        /// </summary>
        private DateTime _Dt_Fecha_CD_Tec;
        public DateTime Dt_Fecha_CD_Tec
        {
            get { return _Dt_Fecha_CD_Tec; }
            set { this._Dt_Fecha_CD_Tec = value; }
        }
        /// <summary>
        /// Fg_Tipo_Centro_Disp
        /// </summary>
        private string _Fg_Tipo_Centro_Disp;
        public string Fg_Tipo_Centro_Disp
        {
            get { return _Fg_Tipo_Centro_Disp; }
            set { this._Fg_Tipo_Centro_Disp = value; }
        }
    }
}
