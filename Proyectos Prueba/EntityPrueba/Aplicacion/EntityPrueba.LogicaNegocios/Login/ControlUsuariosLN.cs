using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityPrueba.AcesoDatos.Login;
using EntityPrueba.Entidad;

namespace EntityPrueba.LogicaNegocios.Login
{
    public class ControlUsuariosLN
    {
        private static readonly ControlUsuariosLN _classInstance = new ControlUsuariosLN();

        public static ControlUsuariosLN ClassInstance
        {
            get { return _classInstance; }
        }

        public ControlUsuariosLN()
        {
        }

        public US_USUARIOS InsertaUsuario(US_USUARIOS Usuario)
        {
            return ControlUsuariosAD.ClassInstance.Inserta_Usuario_Nuevo(Usuario);
        }
    }
}
