using PAEEEM.AccesoDatos.RPUZona;
using PAEEEM.Entidades.RPUZona;


namespace PAEEEM.LogicaNegocios.RPUZona
{
    public class ValidacionZonaL
    {
         private static readonly ValidacionZonaL _classInstance = new ValidacionZonaL();

        public static ValidacionZonaL ClassInstance
        {
            get { return _classInstance; }
        }

        public bool ObtieneZonaCfe(string rpu, out Region_Zona traeZonaFide, out ZonaRPURD traeZonaCfe)
        {
            traeZonaCfe = ValidacionZonaA.ClassInstance.ObtenZonaCfe(rpu);
            traeZonaFide = ValidacionZonaA.ClassInstance.ObtenZonaRpur(traeZonaCfe.Zone);
            var existeZona = traeZonaFide != null;

            return existeZona;

        }

        public bool CoincideZona(string rpu, string nombreUsuario,int zonaRpu)
        {
            var usuario = ValidacionZonaA.ClassInstance.Trae_Usuario(nombreUsuario);

            return usuario.Zona == zonaRpu;
        }
    }
}
