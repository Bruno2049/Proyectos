using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Logica;

namespace Servicio
{
    public partial class Cabecera : ICabeceraService
    {
        Logica.LogicaFactura logicaFactura = new LogicaFactura();
        public bool InsertarCabeceraService(DOminio.Dtos.CabeceraFacturaDTO cabeceraFacturaDto)
        {
           return logicaFactura.InsertarCabeceraLogica(cabeceraFacturaDto);
        }

        public List<DOminio.Dtos.CabeceraFacturaDTO> ListaCabeceraFacturasService()
        {
           return logicaFactura.ListarCabeceraFacturaLogica();
        }
    }
}