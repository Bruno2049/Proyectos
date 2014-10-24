using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOminio.Querys;

namespace Logica
{
    public partial class LogicaFactura :ICabeceraLogica
    {
        DOminio.Querys.Querys consultas = new Querys();
        public bool InsertarCabeceraLogica(DOminio.Dtos.CabeceraFacturaDTO cabeceraFacturaDto)
        {
           return  consultas.InsertarCabeceraConDetalles(cabeceraFacturaDto);
        }

        public List<DOminio.Dtos.CabeceraFacturaDTO> ListarCabeceraFacturaLogica()
        {
           return consultas.ListarCabeceras();
        }
    }
}
