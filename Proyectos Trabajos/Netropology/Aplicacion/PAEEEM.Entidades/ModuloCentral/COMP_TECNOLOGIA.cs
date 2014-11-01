using System;
using System.Collections.Generic;

namespace PAEEEM.Entidades.ModuloCentral
{
    [Serializable]
    public class COMP_TECNOLOGIA
    {
        public COMP_TECNOLOGIA()
        {
            Tarifa02 = "NO";
            Tarifa03 = "NO";
            TarifaOM = "NO";
            TarifaHM = "NO";
        }

        public int? Rownum { get; set; }
        public int CveTecnologia {get; set;}
        public string DxNombreGeneral {get; set;}
        public DateTime DtFechaTecnologoia {get; set;}
        public int CveTipoTecnologia {get; set;}
        public string DxCveCC {get; set;}
        public int? CveEsquema {get; set;}
        public int? CveGasto {get; set;}
        public string CveTipoMovimiento {get; set;}
        public string DxTipoMovimiento { get; set; }
        public int? CveEquiposBaja {get; set;}
        public string DxEquiposBaja { get; set; }
        public int? CveEquiposAlta {get; set;}
        public string DxEquiposAlta { get; set; }
        public int? CveChatarrizacion {get; set;}
        public string DxChatarrizacion { get; set; }
        public decimal? Monto_Chatarrizacion {get;set;}
        public int? CveFactorSustitucion {get; set;}
        public string DxFactorSustitucion { get; set; }
        public int? CveIncentivo {get; set;}
        public string DxIncentivo { get; set; }
        public decimal? MontoIncentivo {get; set;}
        public int? CombinaTecnologias {get; set;}
        public string DxTecnologias { get; set; }
        public int? CvePlantilla{get; set; }
        public string DxPlantilla { get; set; }
        public int? Estatus {get; set;}
        public string DxEstatus { get; set; }
        public string AdicionadoPor { get; set; }
        public string Tarifa02 { get; set; }
        public string Tarifa03 { get; set; }
        public string TarifaOM { get; set; }
        public string TarifaHM { get; set; }
        public int? CveLeyenda { get; set; }

        public List<Combinacion_Tecnologia> tecnologiasCombinadas { get; set; }
        public List<Tarifa_Tecnologia> tarifasTecnologia { get; set; }
    }
}
