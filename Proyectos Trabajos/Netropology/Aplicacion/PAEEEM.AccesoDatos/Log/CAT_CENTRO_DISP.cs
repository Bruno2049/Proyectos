using System.Linq;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Log
{
    public class CatCentroDisp
    {
        private static readonly PAEEEM_DESAEntidades Contexto = new PAEEEM_DESAEntidades();

        public static CAT_CENTRO_DISP ObtienePorId(int idCentroDisp)
        {
            CAT_CENTRO_DISP trInfoGeneral;

            using (var r = new Repositorio<CAT_CENTRO_DISP>())
            {
                trInfoGeneral = r.Extraer(tr => tr.Id_Centro_Disp == idCentroDisp);
            }
            return trInfoGeneral;
        }

        public static int GetIdCentroDisp()
        {
            var infoCentroDisp =
                (from p in Contexto.CAT_CENTRO_DISP
                 select p.Id_Centro_Disp).Max();
            return infoCentroDisp;
        }
    }
}
