using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.AccesoDatos;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class FACTOR_SUSTITUCION
    {
        private static readonly FACTOR_SUSTITUCION _classInstance = new FACTOR_SUSTITUCION();
        public static FACTOR_SUSTITUCION ClassInstance { get { return _classInstance; } }

        public FACTOR_SUSTITUCION()
        { }

        public List<CAT_FACTOR_SUSTITUCION> ObtieneTodos()
        {
            List<CAT_FACTOR_SUSTITUCION> resultado = null;

            using (var r = new Repositorio<CAT_FACTOR_SUSTITUCION>())
            {
                resultado = r.Filtro(c => c.Estatus == 1);
            }

            return resultado;
        }
    }
}
