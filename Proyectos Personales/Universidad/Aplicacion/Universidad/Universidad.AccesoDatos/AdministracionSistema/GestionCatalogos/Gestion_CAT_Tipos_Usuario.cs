using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universidad.Entidades;

namespace Universidad.AccesoDatos.AdministracionSistema.GestionCatalogos
{
    public class Gestion_CAT_Tipos_Usuario
    {
        #region Propiedades de la clase
        /// <summary>
        /// Instancia de la clace Gestion_CAT_Tipos_Usuario
        /// </summary>
        private static readonly Gestion_CAT_Tipos_Usuario _classInstance = new Gestion_CAT_Tipos_Usuario();

        public static Gestion_CAT_Tipos_Usuario ClassInstance
        {
            get { return _classInstance; }
        }

        /// <summary>
        ///  Instancia hacia el contexto de la DB
        /// </summary>
        private readonly UniversidadBDEntities _contexto = new UniversidadBDEntities();

        public Gestion_CAT_Tipos_Usuario()
        {
        }

        #endregion

        #region Motodos Extracion

        /// <summary>
        /// Este metodo se encraga de buscar todos los registro de la tabla US_CAT_TIPO_USUARIO
        /// </summary>
        /// <returns>regresa una lista de todos los registros de US_CAT_TIPO_USUARIO</returns>
        public List<US_CAT_TIPO_USUARIO> ObtenListaCatTiposUsuario()
        {
            List<US_CAT_TIPO_USUARIO> Lista = null;

            using (var r = new Repositorio<US_CAT_TIPO_USUARIO>())
            {
                Lista = r.TablaCompleta();
            }

            return Lista;
        }

        public US_CAT_TIPO_USUARIO ObtenCatTipoUsuario(int Id_Tipo_Usuario)
        {
            US_CAT_TIPO_USUARIO Lista = null;

            using (var r = new Repositorio<US_CAT_TIPO_USUARIO>())
            {
                Lista = r.Extraer(y => y.ID_TIPO_USUARIO == Id_Tipo_Usuario);
            }

            return Lista;
        }

        #endregion

        #region Metodos de Insercion
        /// <summary>
        /// En este metodo se añade un registro de US_CAT_TIPO_USUARIO en este se añadira ID_Log_registro
        /// </summary>
        /// <param name="RegistroUsuario">Se requiere Objeto US_CAT_TIPO_USUARIO con todos los valores 
        /// requeridos excepto por el ID_Log_registro</param>
        /// <param name="AdicionadoPor">Se requiere Usuario el cual añadio el registro </param>
        /// <returns></returns>
        public US_CAT_TIPO_USUARIO InsertaRegistro(US_CAT_TIPO_USUARIO RegistroUsuario)
        {
            using (var r = new Repositorio<US_CAT_TIPO_USUARIO>())
            {
                var Nueva = r.Agregar(RegistroUsuario);

                return Nueva;
            }
        }

        #endregion
    }
}
