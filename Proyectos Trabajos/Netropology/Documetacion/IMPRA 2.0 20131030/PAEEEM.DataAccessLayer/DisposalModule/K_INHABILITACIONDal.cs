using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PAEEEM.Helpers;
using PAEEEM.Entities;

namespace PAEEEM.DataAccessLayer
{
    /// <summary>
    /// 
    /// </summary>
    public class K_INHABILITACIONDal
    {
        private static readonly K_INHABILITACIONDal _classinstance = new K_INHABILITACIONDal();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static K_INHABILITACIONDal ClassInstance { get { return _classinstance; } }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="instance"></param>
       /// <param name="Id_Inhabilitacion"></param>
       /// <returns></returns>
        public int Insert_K_INHABILITACION(K_INHABILITACIONModel instance, out int Id_Inhabilitacion)
        {
            int result = 0;
            try
            {
                string executesqlstr = "INSERT INTO K_INHABILITACION (Dt_Fecha_Inhabilitacion,Id_Centro_Disp,Fg_Tipo_Centro_Disp) VALUES(@Dt_Fecha_Inhabilitacion,@Id_Centro_Disp,@Fg_Tipo_Centro_Disp) " +
                                              " SELECT @Id_Inhabilitacion=@@IDENTITY";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Dt_Fecha_Inhabilitacion",instance.Dt_Fecha_Inhabilitacion),
                    new SqlParameter("@Id_Centro_Disp",instance.Id_Centro_Disp),
                    new SqlParameter("@Fg_Tipo_Centro_Disp",instance.Fg_Tipo_Centro_Disp),
                    new SqlParameter("@Id_Inhabilitacion",SqlDbType.Int)
                };
                para[3].Direction = ParameterDirection.Output;

                result = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);

                int.TryParse(para[3].Value.ToString(), out Id_Inhabilitacion);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Add K_INHABILITACION failed: Execute method Insert_K_INHABILITACION in K_INHABILITACIONDal.", ex, true);
            }
            return result;
        }

        // added by Tina 2012-02-20
        /// <summary>
        /// Update K_INHABILITACION Number of Final Act
        /// </summary>
        /// <param name="Id_Recuperacion"></param>
        /// <param name="finalActID"></param>
        /// <returns></returns>
        public int UpdateFinalActID(string Id_Inhabilitacion, string finalActID)
        {
            int result = 0;
            try
            {
                string executesqlstr = "UPDATE K_INHABILITACION SET Id_Acta_Inhabilitacion=@Id_Acta_Inhabilitacion WHERE Id_Inhabilitacion=@Id_Inhabilitacion";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Id_Acta_Inhabilitacion",finalActID),
                    new SqlParameter("@Id_Inhabilitacion",Id_Inhabilitacion)
                };

                result = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Update K_INHABILITACION Number of Final Act failed: Execute method UpdateFinalActID in K_INHABILITACIONDal.", ex, true);
            }
            return result;
        }
    }
}
