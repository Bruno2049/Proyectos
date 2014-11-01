using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;
using PAEEEM.AccesoDatos;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class EQUIPOS_BAJA_ALTA
    {
        private static readonly EQUIPOS_BAJA_ALTA _classInstance = new EQUIPOS_BAJA_ALTA();
        public static EQUIPOS_BAJA_ALTA ClassInstance { get { return _classInstance; } }

        public EQUIPOS_BAJA_ALTA()
        { }

        public List<CAT_EQUIPOS_BAJA_ALTA> ObtieneTodos()
        {
            List<CAT_EQUIPOS_BAJA_ALTA> resultado = null;

            using (var r = new Repositorio<CAT_EQUIPOS_BAJA_ALTA>())
            {
                resultado = r.Filtro(c => c.Estatus == 1);
            }

            return resultado;
        }
    }
}
