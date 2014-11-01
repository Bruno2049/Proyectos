using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class Programa
    {
        public static CAT_PROGRAMA Obtener()
        {
            CAT_PROGRAMA obj = null;

            using (var r = new Repositorio<CAT_PROGRAMA>())
            {
                obj = r.Extraer(p => p.ID_Prog_Proy == 1);
            }

            return obj;
        }

        public static bool Actualizar(CAT_PROGRAMA pro)
        {
            bool obj = false;

            using (var r = new Repositorio<CAT_PROGRAMA>())
            {
                obj = r.Actualizar(pro);
            }

            return obj;
        }
    }
}
