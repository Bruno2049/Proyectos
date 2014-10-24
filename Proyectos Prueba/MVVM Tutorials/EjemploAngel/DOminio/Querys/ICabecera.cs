using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOminio.Dtos;

namespace DOminio.Querys
{
    public interface ICabecera
    {
        bool InsertarCabeceraConDetalles(CabeceraFacturaDTO cabeceraDto);
        List<CabeceraFacturaDTO> ListarCabeceras();
    }
}
