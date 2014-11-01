using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Log
{
    public class CatProveedor
    {
        public static CAT_PROVEEDOR ObtienePorId(int idProveedor)
        {
            CAT_PROVEEDOR trInfoGeneral;

            using (var r = new Repositorio<CAT_PROVEEDOR>())
            {
                trInfoGeneral = r.Extraer(tr => tr.Id_Proveedor == idProveedor);
            }
            return trInfoGeneral;
        }
    }
}
