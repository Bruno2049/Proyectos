using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PAEEEM.Helpers;

namespace PAEEEM.DataAccessLayer
{
    public class K_CREDITO_SUSTITUCION_EXTENSIONDal
    {
        private static readonly K_CREDITO_SUSTITUCION_EXTENSIONDal _classinstance = new K_CREDITO_SUSTITUCION_EXTENSIONDal();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static K_CREDITO_SUSTITUCION_EXTENSIONDal ClassInstance { get { return _classinstance; } }

        public int Insert_K_CREDITO_SUSTITUCION_EXTENSION(int sustitutionID)
        {
            try
            {
                string executesqlstr = "INSERT INTO K_CREDITO_SUSTITUCION_EXTENSION (Id_Credito_Sustitucion,Dt_Fecha_Imagen_Recepcion) VALUES(@Id_Credito_Sustitucion,GETDATE())";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Id_Credito_Sustitucion",sustitutionID)
                };
                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Add K_CREDITO_SUSTITUCION_EXTENSION failed: Execute method Insert_K_CREDITO_SUSTITUCION_EXTENSION in K_CREDITO_SUSTITUCION_EXTENSIONDal.", ex, true);
            }
        }
    }
}
