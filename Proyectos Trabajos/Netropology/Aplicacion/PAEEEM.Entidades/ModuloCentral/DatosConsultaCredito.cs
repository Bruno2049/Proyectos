using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.ModuloCentral
{
    [Serializable]
    public class DatosConsultaCredito
    {
        //DATOS DEL CREDITO
        public string No_Credito { get; set; }
        public int Id_Branch { get; set; }
        public int id_proveedor { get; set; }
        public string RPU { get; set; }
            //-- tarifa
        public decimal? Monto_Total_Pagar { get; set; }
        public DateTime? Fecha_Ultmod { get; set; }
        public string Dx_Estatus_Credito { get; set; }

        public DateTime? Afectacion_SICOM_fecha { get; set; }
        public DateTime? Afectacion_SIRCA_Fecha { get; set; }
        public DateTime? Fecha_Pendiente { get; set; }
        public DateTime? Fecha_Por_entregar { get; set; }
        public DateTime? Fecha_En_revision { get; set; }
        public DateTime? Fecha_Autorizado { get; set; }
        public DateTime? Fecha_Rechazado { get; set; }
        public DateTime? Fecha_Calificación_MOP_no_válida { get; set; }
        public DateTime? Fecha_Cancelado { get; set; }
        public decimal? Consumo_Promedio { get; set; }
        public string Usr_Ultmod { get; set; }
        public byte? No_Consultas_Crediticias { get; set; }
        public byte? No_MOP { get; set; }
        public string Folio_Consulta { get; set; }
        public DateTime? Fecha_Consulta { get; set; }
        public string ID_Intelisis { get; set; }
        public DateTime? Fecha_Pago_Intelisis { get; set; }
        public decimal? Mt_Monto_Pagado { get; set; }


        //DATOS DEL DISTRIBUDOR
        public string Dx_Razon_Social { get; set; }
        public string Dx_Nombre_Comercial { get; set; }
        public string Dx_Nombre_Repre { get; set; }
        public string Dx_Email_Repre { get; set; }
        public string Dx_Telefono_Repre { get; set; }
        public string Dx_Domicilio_Part_CP { get; set; }
        public int? Cve_Estado_Part { get; set; }
        public int? Cve_Deleg_Municipio_Part { get; set; }
            //--colonia
        public string Dx_Domicilio_Part_Calle { get; set; }
        public string Dx_Domicilio_Part_Num { get; set; }
        public string Dx_Domicilio_Fiscal_CP { get; set; }
        public int? Cve_Estado_Fisc { get; set; }
        public int? Cve_Deleg_Municipio_Fisc { get; set; }
            //--colonia
        public string Dx_Domicilio_Fiscal_Calle { get; set; }
        public string Dx_Domicilio_Fiscal_Num { get; set; }
        public string Dx_Nombre_Region { get; set; }
        public string Dx_Nombre_Zona { get; set; }

        //DATOS DEL CLIENTE Y NEGOCIO
        public int IdCliente { get; set; }
        public int IdNegocio { get; set; }
        public string NombreRazonSocial { get; set; }
        public int Cve_Tipo_Sociedad { get; set; }
        public string Nombre_Comercial { get; set; }
        public string RFC { get; set; }
        public string CURP { get; set; }
        public DateTime? Fec_Nacimiento { get; set; }
        public string Dx_Tipo_Industria { get; set; }
        public string Dx_Sector { get; set; }
        public string Dx_Estado_Civil { get; set; }
        public string RegimenConyugal { get; set; }
        public string email { get; set; }
        public string TipoIdentificacion { get; set; }
        public string Numero_Identificacion { get; set; }
        public string Nombre_Notario { get; set; }


        public int? Rownum { get; set; }
    }
}
