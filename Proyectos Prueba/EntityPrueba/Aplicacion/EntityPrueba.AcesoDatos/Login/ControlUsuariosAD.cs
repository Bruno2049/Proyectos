using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityPrueba.Entidad;
using EntityPrueba.Entidad.Login;
using EntityPrueba.AcesoDatos;

namespace EntityPrueba.AcesoDatos.Login
{
    public class ControlUsuariosAD
    {
        private static readonly ControlUsuariosAD _classInstance = new ControlUsuariosAD();

        public static ControlUsuariosAD ClassInstance
        {
            get { return _classInstance; }
        }

        private readonly EntityPruebaContexto _contexto = new EntityPruebaContexto();

        public ControlUsuariosAD()
        { }

        #region Metodos de Almacenamiento

        public US_USUARIOS Inserta_Usuario_Nuevo(US_USUARIOS Usuario)
        {
            using (var context = new EntityPruebaContexto())
            {
                US_USUARIOS Registro = null;
                using (var r = new Repositorio<US_USUARIOS>())
                {
                    Registro = r.Agregar(Usuario);
                }

                return Registro;
            }
        }
        #endregion
    }
}
