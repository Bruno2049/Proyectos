using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOminio.Querys
{
    public partial class Querys : ICabecera

    {
        public bool InsertarCabeceraConDetalles(Dtos.CabeceraFacturaDTO cabeceraDto)
        {
            using (var modelo = new Persistencia.EjemploAngelFacturaEntities())
            {
                var cabeceraConvertidad = Convertidores.CabeceraFacturaAssembler.ToEntity(cabeceraDto);

                modelo.CabeceraFactura.Add(cabeceraConvertidad);
                modelo.SaveChanges();

                return true;
            }
        }

        public List<Dtos.CabeceraFacturaDTO> ListarCabeceras()
        {

            using (var modelo = new Persistencia.EjemploAngelFacturaEntities())
            {
                var listaCabecerasENTIDIDAD = modelo.CabeceraFactura.ToList();
                var listaConvertida = Convertidores.CabeceraFacturaAssembler.ToDTOs(listaCabecerasENTIDIDAD);

                return listaConvertida;
            }

        }
    }
}
