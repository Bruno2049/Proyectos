using System;

namespace PAEEEM.Entidades
{
    public class DatosCredito
    {
        public int? IdProgProy { get; set; }
        public string RFC { get; set; }
        public int CveEstatusCredito { get; set; }
        public int? CveTipoSociedad { get; set; }
        public int? NoPlazoPago { get; set; }
        public string PeriodoPago { get; set; }
        public string RazonSocial { get; set; }
        public decimal? MontoSolicitado { get; set; }
        public DateTime? FechaCalificacionMopNoValida { get; set; }
        public int? NoMop { get; set; }
        public string CveCc { get; set; }
        public string Estado { get; set; }
        public string DelegMunicipio { get; set; }
        public string CvePm { get; set; }
        public string Calle { get; set; }
        public string Colonia { get; set; }
        public string NumExt { get; set; }
        public string CodigoPostal { get; set; }
        public int? TipoDomicilio { get; set; }
        public string Nombre { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public DateTime? FecNacimiento { get; set; }
    }
}
