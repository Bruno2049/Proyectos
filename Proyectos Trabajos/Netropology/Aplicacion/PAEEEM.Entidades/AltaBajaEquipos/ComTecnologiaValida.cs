using System;

namespace PAEEEM.Entidades.AltaBajaEquipos
{
    [Serializable]
    public class CompDetalleTecnologia
    {
        public int CveTecnologia { get; set; }
        public string DxNombreGeneral { get; set; }        
        public int CveTipoTecnologia { get; set; }
        public string TipoTecnologia  { get; set; }
        public string DxCveCc { get; set; }
        public string CveTipoMovimiento { get; set; }
        public string DxTipoMovimiento { get; set; }
        public int? CveEquiposBaja { get; set; }
        public string DxEquiposBaja { get; set; }
        public int? CveEqupoAlta { get; set; }
        public string DxEquipoAlta { get; set; }
        public int CveFactorSusticion { get; set; }
        public string DxFactorSusticion { get; set; }
        public bool CveChatarrizacion { get; set; }
        public decimal? MontoChatarrizacion { get; set; }
        public bool CveIncentivo { get; set; }
        public bool CveGasto { get; set; }
        public decimal MontoIncentivo { get; set; }
        public int CveEsquema { get; set; }
        public bool CombinaTecnologias { get; set; }
        public int NumeroGrupos { get; set; }
    }
}
