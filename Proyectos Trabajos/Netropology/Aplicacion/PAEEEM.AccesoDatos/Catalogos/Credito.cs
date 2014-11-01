using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class beCredito
    {

        public static CRE_Credito Obtener(string NoCredito)
        {
            CRE_Credito obj = null;

            using (var r = new Repositorio<CRE_Credito>())
            {
                obj = r.Extraer(m => m.No_Credito == NoCredito);
            }

            return obj;
        }

        public static CRE_Credito Insertar(CRE_Credito credito)
        {
            CRE_Credito obj;

            using (var r = new Repositorio<CRE_Credito>())
            {
                obj = r.Agregar(credito);
            }

            return obj;
        }

        public static bool Actualizar(CRE_Credito credito)
        {
            bool obj = false;

            using (var r = new Repositorio<CRE_Credito>())
            {
                obj = r.Actualizar(credito);
            }

            return obj;
        }

    }
}
