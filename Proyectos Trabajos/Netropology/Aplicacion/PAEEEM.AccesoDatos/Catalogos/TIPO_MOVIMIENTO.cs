using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;
using PAEEEM.AccesoDatos;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class TIPO_MOVIMIENTO
    {
        private static readonly TIPO_MOVIMIENTO _classInstance = new TIPO_MOVIMIENTO();
        public static TIPO_MOVIMIENTO ClassInstance { get { return _classInstance; } }

        public TIPO_MOVIMIENTO()
        { }

        public List<CAT_TIPO_MOVIMIENTO> ObtieneTodos()
        {
            List<CAT_TIPO_MOVIMIENTO> resultado = null;

            using (var r = new Repositorio<CAT_TIPO_MOVIMIENTO>())
            {
                resultado = r.Filtro(c => c.Estatus == 1);
            }

            return resultado;
        }
    }
}
