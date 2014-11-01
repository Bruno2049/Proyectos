using System.Linq;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Log
{
    public class CatCentroDispSucursal
    {
        private static readonly PAEEEM_DESAEntidades Contexto = new PAEEEM_DESAEntidades();

        public static CAT_CENTRO_DISP_SUCURSAL ObtienePorId(int idCentroDispSucursal)
        {
            CAT_CENTRO_DISP_SUCURSAL trInfoGeneral;

            using (var r = new Repositorio<CAT_CENTRO_DISP_SUCURSAL>())
            {
                trInfoGeneral = r.Extraer(tr => tr.Id_Centro_Disp_Sucursal == idCentroDispSucursal);
            }
            return trInfoGeneral;
        }

        public static int GetIdCentroDispSucursal()
        {
            var infoCentroDispSucursal =
                (from p in Contexto.CAT_CENTRO_DISP_SUCURSAL
                 select p.Id_Centro_Disp_Sucursal).Max();
            return infoCentroDispSucursal;
        }
    }
}
