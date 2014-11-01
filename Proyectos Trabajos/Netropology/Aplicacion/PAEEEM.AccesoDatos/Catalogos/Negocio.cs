using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class beNegocio
    {

        public static CLI_Negocio Obtener(string NoCredito)
        {

            CLI_Negocio obj = null;
            int IdNegocio = 0;

            using (var r = new Repositorio<CRE_Credito>())
            {
                CRE_Credito cre = r.Extraer(m => m.No_Credito == NoCredito);
                IdNegocio = int.Parse(cre.IdNegocio.ToString());
            }

            using (var r = new Repositorio<CLI_Negocio>())
            {

                obj = r.Extraer(m => m.IdNegocio == IdNegocio);
            }

            return obj;
        }

        public static CLI_Negocio Insertar(CLI_Negocio negocio)
        {
            CLI_Negocio obj;

            using (var r = new Repositorio<CLI_Negocio>())
            {
                obj = r.Agregar(negocio);
            }

            return obj;
        }

        public static bool Actualizar(CLI_Negocio negocio)
        {
            bool obj = false;

            using (var r = new Repositorio<CLI_Negocio>())
            {
                obj = r.Actualizar(negocio);
            }

            return obj;
        }

    }
}
