using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.SolicitudCredito
{
    public class SECTOR_ECONOMICO
    {
        public SECTOR_ECONOMICO()
        { }

        public List<CAT_SECTOR_ECONOMICO> ObtenerSectorEconomico()
        {
            List<CAT_SECTOR_ECONOMICO> resultado = null;

            using (var r = new Repositorio<CAT_SECTOR_ECONOMICO>())
            {
                resultado = r.Filtro(c => c.Cve_Sector > 0);
            }

            return resultado;
        }
    }
}
