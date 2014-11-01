using System;

namespace PAEEEM.Entidades.AltaBajaEquipos
{
    [Serializable]
    public class RefrigeracionComercial
    {
        public decimal FDeg { get; set; }
        public decimal FBio { get; set; }
        public int NoEb { get; set; }
        public decimal AEb { get; set; }
        public decimal CapEb { get; set; }
        public decimal FMes { get; set; }
        public int NoEa { get; set; }
        public decimal AEa { get; set; }
        public decimal CapEa { get; set; }
        public decimal Horas { get; set; }
        public decimal V1 { get; set; }
        public decimal V2 { get; set; }
    }


    public class AireAcondicionado
    {
        public decimal FBio { get; set; }
        public decimal FDegAA { get; set; }
        public decimal FCAA { get; set; }
        public int NoEb { get; set; }
        public int NoEa { get; set; }
        public decimal kWhEB { get; set; }
        public decimal kWEB { get; set; }
        public decimal kWEA { get; set; }
        public decimal OdiaAA { get; set; }
        public decimal FmesAA { get; set; }
    }


    public class IluminacionLID
    {
        public decimal FBio { get; set; }
        public decimal FCIlu { get; set; }
        public decimal FDegIlu { get; set; }
        public int NoEb { get; set; }
        public int NoEa { get; set; }
        public decimal PEB { get; set; }
        public decimal PEA { get; set; }
        public decimal OanioIlu { get; set; }
        public decimal FmesIlu { get; set; }
    }


    public class MotoresElectricos
    {
        public decimal FBio { get; set; }
        public decimal FDegME { get; set; }
        public int NoEb { get; set; }
        public int NoEa { get; set; }
        public decimal EficienciaEB { get; set; }
        public decimal EficienciaEA { get; set; }
        public decimal FcargaME { get; set; }
        public decimal HPnominal { get; set; }
        public decimal OanioME { get; set; }
        public decimal FmesME { get; set; }
        public decimal kwAhorro { get; set; }
    }


}
