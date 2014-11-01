using PAEEEM.AccesoDatos;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.Entidades;

namespace PAEEEM.LogicaNegocios.Credito
{
    public class blNegocio
    {

        public static CLI_Negocio Obtener(string NoCredito)
        {
            CLI_Negocio negocio = beNegocio.Obtener(NoCredito);

            return negocio;
        }

        public static CLI_Negocio Insertar(CLI_Negocio negocio)
        {
            CLI_Negocio nuevo = null;

            var datos = beNegocio.Insertar(negocio);

            if (datos != null)
            {
                nuevo = datos;
            }

            return nuevo;
        }

        public static bool Actualizar(CLI_Negocio negocio)
        {
            bool obj;

            using (var r = new Repositorio<CLI_Negocio>())
            {
                obj = r.Actualizar(negocio);
            }

            return obj;
        }

    }
}
