using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.SolicitudCredito
{
    public class ESTRATIFICACION_MIPYMES
    {
        private static readonly ESTRATIFICACION_MIPYMES _classInstance = new ESTRATIFICACION_MIPYMES();
        public static ESTRATIFICACION_MIPYMES ClassInstance { get { return _classInstance; } }

        public ESTRATIFICACION_MIPYMES()
        { }

        public List<CAT_ESTRATIFICACION_MIPYMES> RegresaListaEstratificacion()
        {
            List<CAT_ESTRATIFICACION_MIPYMES> resultado = null;

            using (var r = new Repositorio<CAT_ESTRATIFICACION_MIPYMES>())
            {
                resultado = r.Filtro(me => me.Cve_Estratificacion > 0);
            }

            return resultado;
        }
    }
}
