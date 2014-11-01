using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.AltaBajaEquipos
{
    public class DetalleEquiposBajaEfic
    {
        public string No_credito { get; set; }
        public string Dx_Grupo { get; set; }
        public string Dx_Tecnologia { get; set; }
        public string Dx_Tipo_Producto { get; set; }
        public string CapSis { get; set; }
        public string Dx_Unidad { get; set; }
        public int? Cantidad { get; set; }

        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }
        public string Antiguedad { get; set; }
        public string PreFolio { get; set; }
        public string Folio { get; set; }
        public DateTime? FechaIngr { get; set; }
        public string CAyD { get; set; }
        public string RazonSocial { get; set; }
        public string Zona { get; set; }
        public string Region { get; set; }

        public int? IdCreditoSustitucion { get; set; }
        public int IdConsecutivo { get; set; }
        public int idTipoFoto { get; set; }
        public string URL { get; set; }

        public byte[] Foto { get; set; }
    }
}
