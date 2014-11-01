using System.Linq;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Log
{
   public class CatProducto
    {
       private static readonly PAEEEM_DESAEntidades Contexto = new PAEEEM_DESAEntidades();

        public static CAT_PRODUCTO ObtienePorId(int cveProducto)
        {
            CAT_PRODUCTO trInfoGeneral;

            using (var r = new Repositorio<CAT_PRODUCTO>())
            {
                trInfoGeneral = r.Extraer(tr => tr.Cve_Producto== cveProducto);
            }
            return trInfoGeneral;
        }

        public static int GetIdProducto()
        {
            var infoProd =
                (from p in Contexto.CAT_PRODUCTO
                 select p.Cve_Producto).Max();
            return infoProd;
        }
    }
}
