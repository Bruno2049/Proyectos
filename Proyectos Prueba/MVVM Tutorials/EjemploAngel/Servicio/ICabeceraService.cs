using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using DOminio.Dtos;

namespace Servicio
{
    [ServiceContract]
    public interface ICabeceraService
    {
         [OperationContract]
        bool InsertarCabeceraService(CabeceraFacturaDTO cabeceraFacturaDto);
         [OperationContract]
        List<CabeceraFacturaDTO> ListaCabeceraFacturasService();


    }
}
