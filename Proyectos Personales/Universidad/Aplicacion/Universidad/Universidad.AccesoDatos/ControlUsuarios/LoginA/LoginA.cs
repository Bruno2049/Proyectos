using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Universidad.Entidades;

namespace Universidad.AccesoDatos.ControlUsuarios.LoginA
{
    public class LoginA
    {
        #region Propiedades de la clase
        
        private static readonly LoginA _classInstance = new LoginA();

        public static LoginA ClassInstance
        {
            get { return _classInstance; }
        }

        /// <summary>
        ///  Instancia hacia el contexto de la DB
        /// </summary>
        private readonly UniversidadBDEntities _contexto = new UniversidadBDEntities();

        public LoginA()
        {
        }

        #endregion

        #region Metodos de Insercion

        #endregion

        #region Metodos de Extraccion

        public US_USUARIOS LoginAdministrador(string Nombre, string Contrasena)
        {
            US_USUARIOS Usuario = null; 

            using (var Aux = new Repositorio<US_USUARIOS> ())
            {
                Usuario = Aux.Extraer(r => r.USUARIO == Nombre && r.CONTRASENA == Contrasena);
            }

            return Usuario;
        }

        public US_USUARIOS LoginAdministradoresTSQL(string Nombre, string Contrasena)
        {
            const string executesqlstr = "SELECT TOP 1 * FROM US_USUARIOS WHERE USUARIO = @Usuario AND CONTRASENA = @Contrasena";
            US_USUARIOS a = null;
            try
            {
                var para = new SqlParameter[] { 
                    new SqlParameter("@Usuario",Nombre),
                    new SqlParameter("@Contrasena",Contrasena)                    
                };
                var obj = ControladorSQL.ExecuteScalar(ParametrosSQL.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
                if (obj != null)
                {
                    a = ((US_USUARIOS) obj);
                }
            }
            catch (SqlException ex)
            {
                
            }
            return a;
        }

        #endregion
    }
}
