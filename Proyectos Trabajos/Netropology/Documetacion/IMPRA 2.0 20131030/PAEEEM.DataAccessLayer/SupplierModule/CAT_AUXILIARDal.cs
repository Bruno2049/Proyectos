/*	
	$Author:     Tina
	$Date:       2011-07-29	
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PAEEEM.Entities;
using PAEEEM.Helpers;

namespace PAEEEM.DataAccessLayer
{
    /// <summary>
    /// Data Access Layer for auxiliar
    /// </summary>
    public class CAT_AUXILIARDal
    {
        /// <summary>
        /// Class instance field
        /// </summary>
        private static readonly CAT_AUXILIARDal _classinstance = new CAT_AUXILIARDal();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static CAT_AUXILIARDal ClassInstance { get { return _classinstance; } }
        /// <summary>
        /// Update auxiliar with primary key
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update_CAT_AUXILIARByCredito(CAT_AUXILIAREntity model)
        {
            int iResult = 0;
            string SQL = "update CAT_AUXILIAR set No_MOP=@No_MOP,Ft_Folio=@Ft_Folio, Dt_Fecha_Consulta = @Dt_Fecha_Consulta where No_Credito=@No_Credito";
            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter("@No_MOP", model.No_MOP.ToString()  == "mop" ? null : model.No_MOP),
                    new SqlParameter("@Ft_Folio",model.Ft_Folio),
                    new SqlParameter("@No_Credito", model.No_Credito),
                    new SqlParameter("@Dt_Fecha_Consulta", DateTime.Now)
                };
                iResult = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Update credit failed: Execute method UpdateCredit in CreditDal.", ex, true);
            }
            return iResult;
        }
        /// <summary>
        /// Insert auxiliar record
        /// </summary>
        /// <param name="Instance"></param>
        /// <returns></returns>
        public int Insert_CAT_AUXILIAR(CAT_AUXILIAREntity Instance)
        {
            int iCount = 0;
            try
            {
                string SQL = "INSERT INTO CAT_AUXILIAR ([No_Credito],[Dx_Nombres],[Dx_Apellido_Paterno],[Dx_Apellido_Materno],[Dt_Nacimiento_Fecha],[Dx_Ciudad])  VALUES(@No_Credito,@Dx_Nombres,@Dx_Apellido_Paterno,@Dx_Apellido_Materno,@Dt_Nacimiento_Fecha,@Dx_Ciudad)";
                SqlParameter[] paras = new SqlParameter[]{
                     new SqlParameter("@No_Credito", Instance.No_Credito),
                    new SqlParameter("@Dx_Nombres", Instance.Dx_Nombres),
                    new SqlParameter("@Dx_Apellido_Paterno", Instance.Dx_Apellido_Paterno),
                     new SqlParameter("@Dx_Apellido_Materno", Instance.Dx_Apellido_Materno),
                    new SqlParameter("@Dt_Nacimiento_Fecha", Instance.Dt_Nacimiento_Fecha),
                    new SqlParameter("@Dx_Ciudad",Instance.Dx_Ciudad)
                };
                iCount = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
            return iCount;
        }
        /// <summary>
        /// Get auxiliar with credit number
        /// </summary>
        /// <param name="strCreditNo"></param>
        /// <returns></returns>
        public DataTable Get_CAT_AUXILIARByCreditNo(string strCreditNo)
        {
            try
            {
                string SQL = "select * from CAT_AUXILIAR   where No_Credito=@No_Credito";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@No_Credito", strCreditNo)  
                 };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        //add by coco 2011-08-02
        /// <summary>
        /// Credit Review Update Cat_Auxiliar
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update_CAT_AUXILIARByCreditoReview(CAT_AUXILIAREntity model)
        {
            int iResult = 0;
            string SQL = "update CAT_AUXILIAR set Dx_Nombres=@Dx_Nombres,Dx_Apellido_Paterno=@Dx_Apellido_Paterno,Dx_Apellido_Materno=@Dx_Apellido_Materno,";
            SQL = SQL + " Dt_Nacimiento_Fecha=@Dt_Nacimiento_Fecha,Dx_Ciudad=@Dx_Ciudad  where No_Credito=@No_Credito";
            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter("@Dx_Nombres", model.Dx_Nombres),
                    new SqlParameter("@Dx_Apellido_Paterno",model.Dx_Apellido_Paterno),
                     new SqlParameter("@Dx_Apellido_Materno", model.Dx_Apellido_Materno),
                    new SqlParameter("@Dt_Nacimiento_Fecha",model.Dt_Nacimiento_Fecha),
                    new SqlParameter("Dx_Ciudad",model.Dx_Ciudad),
                    new SqlParameter("No_Credito",model.No_Credito)
                };
                iResult = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Update credit failed: Execute method UpdateCredit in CreditDal.", ex, true);
            }
            return iResult;
        }
        /// <summary>
        /// Delete auxiliar with credit number
        /// </summary>
        /// <param name="creditNo"></param>
        /// <returns></returns>
        public int Delete_CatAuxilira(string creditNo)
        {
            int iResult = 0;
            string SQL = "DELETE FROM CAT_AUXILIAR  where No_Credito=@No_Credito";         
            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {                  
                    new SqlParameter("No_Credito",creditNo)
                };
                iResult = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "delete credit failed:", ex, true);
            }
            return iResult;
        }
        //end add
    }
}
