using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class Zona
    {
        public static List<CAT_ZONA> CatZona()
        {
            List<CAT_ZONA> zon = null;
            using (var r = new Repositorio<CAT_ZONA>())
            {
                zon = r.Filtro(me => me.Dx_Nombre_Zona != null);
            }
            return zon;
        }

        public static List<CAT_ZONA> CatZonaid(int idZona)
        {
            List<CAT_ZONA> zon = null;
            using (var r = new Repositorio<CAT_ZONA>())
            {
                zon = r.Filtro(me => me.Cve_Zona == idZona);
            }
            return zon;
        }
        public static List<CAT_ZONA> CatZonaxRegion(int idRegion)
        {
            List<CAT_ZONA> zon = null;
            using (var r = new Repositorio<CAT_ZONA>())
            {
                zon = r.Filtro(me => me.Cve_Region == idRegion);
            }
            return zon;
        }

        
    }
}
