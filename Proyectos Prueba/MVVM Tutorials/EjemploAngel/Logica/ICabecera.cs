using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOminio.Dtos;

namespace Logica
{
    public interface ICabeceraLogica
   {
       bool InsertarCabeceraLogica(CabeceraFacturaDTO cabeceraFacturaDto);
       List<CabeceraFacturaDTO> ListarCabeceraFacturaLogica();
   }
}
