using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Universidad.Entidades.ControlUsuario;

namespace Universidad.AccesoDatos.AdministracionSistema.Menu_Sistema
{
    public class Menu_Sistema_Administrativo
    {
        #region Propiedades de la clase
        /// <summary>
        /// Instancia de la clace Gestion_CAT_Tipos_Usuario
        /// </summary>
        private static readonly Menu_Sistema_Administrativo _classInstance = new Menu_Sistema_Administrativo();

        public static Menu_Sistema_Administrativo ClassInstance
        {
            get { return _classInstance; }
        }

        /// <summary>
        ///  Instancia hacia el contexto de la DB
        /// </summary>
        private readonly UniversidadBDEntities _contexto = new UniversidadBDEntities();

        public Menu_Sistema_Administrativo()
        {
        }

        #endregion

        #region Metodos de Extraccion

        public List<MenuSistemaE> TrarLista()
        {
            //var Menu = (from Padre in _contexto.SIS_AADM_MENUS join Hijo in _contexto.SIS_AADM_MENUS on new {} equals EXPR2 
            //)
            return null;
        }

        #endregion
    }
}
