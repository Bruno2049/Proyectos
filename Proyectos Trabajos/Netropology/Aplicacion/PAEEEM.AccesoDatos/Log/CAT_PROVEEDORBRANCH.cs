using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Log
{
    public class CatProveedorbranch
    {
        public static CAT_PROVEEDORBRANCH ObtienePorId(int idBranch)
        {
            CAT_PROVEEDORBRANCH trInfoGeneral;

            using (var r = new Repositorio<CAT_PROVEEDORBRANCH>())
            {
                trInfoGeneral = r.Extraer(tr => tr.Id_Branch == idBranch);
            }
            return trInfoGeneral;
        }
    }
}
