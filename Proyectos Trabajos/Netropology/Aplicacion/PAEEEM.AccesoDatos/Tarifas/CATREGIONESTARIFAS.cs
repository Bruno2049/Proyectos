using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Tarifas
{
    public class Catregionestarifas
    {
        public static CAT_REGIONES_TARIFAS GetRegion(int id)
        {
            CAT_REGIONES_TARIFAS trInfoGeneral;

            using (var r = new Repositorio<CAT_REGIONES_TARIFAS>())
            {
                trInfoGeneral = r.Extraer(tr => tr.id_region == id);
            }
            return trInfoGeneral;
        }
    }
}
