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
    /// Data Access Layer for credit cost
    /// </summary>
    public class K_CREDITO_COSTODal
    {
        /// <summary>
        /// Readonly variable for class instance
        /// </summary>
        private static readonly K_CREDITO_COSTODal _classInstance = new K_CREDITO_COSTODal();
        /// <summary>
        /// Property for class instance
        /// </summary>
        public static K_CREDITO_COSTODal ClassInstance { get { return _classInstance; } }
        /// <summary>
        /// insert data
        /// </summary>
        /// <param name="Instance"></param>
        /// <returns></returns>
        public int Insert_K_Credito_Costo(K_CREDITO_COSTOEntity Instance)
        {
            int iCount = 0;
            try
            {
                string SQL = "INSERT INTO K_CREDITO_COSTO ([No_Credito] ,[Mt_Costo],[Dt_Credito_Costo])  VALUES(@No_Credito,@Mt_Costo,@Dt_Credito_Costo)";
                SqlParameter[] paras = new SqlParameter[]{
                     new SqlParameter("@No_Credito", Instance.No_Credito),
                    new SqlParameter("@Mt_Costo", Instance.Mt_Costo),
                    new SqlParameter("@Dt_Credito_Costo", Instance.Dt_Credito_Costo)
                };
                iCount = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
            return iCount;
        }
        //add by coco 2011-08-08
        /// <summary>
        /// update K_Credito_Costo
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Update_K_Credito_Costo(K_CREDITO_COSTOEntity instance)
        {
            int iCount = 0;
            try
            {
                string SQL = "UPDATE K_CREDITO_COSTO SET Mt_Costo =@Mt_Costo ,Dt_Credito_Costo =@Dt_Credito_Costo WHERE  No_Credito=@No_Credito";
                SqlParameter[] paras = new SqlParameter[]{
                     new SqlParameter("@No_Credito", instance.No_Credito),
                    new SqlParameter("@Mt_Costo", instance.Mt_Costo),
                    new SqlParameter("@Dt_Credito_Costo", instance.Dt_Credito_Costo)
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
        /// delete k_credito_costo
        /// </summary>
        /// <param name="creditNo"></param>
        /// <returns></returns>
        public int Delete_K_Credito_Costo(string creditNo)
        {
            int iCount = 0;
            try
            {
                string Sql = "DELETE FROM K_CREDITO_COSTO WHERE No_Credito =@No_Credito";
                SqlParameter[] para = new SqlParameter[] { 
                 new SqlParameter("@No_Credito",creditNo)
                };
                iCount = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, Sql, para);
            }
            catch (Exception ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
            return iCount;
        }
        //end add
        /// <summary>
        /// Get total cost by credit number
        /// </summary>
        /// <param name="credit"></param>
        /// <returns></returns>
        public string GetTotalCost(string credit)
        {
            string Result = "";
            string SQL = "";
            try
            {
                SQL = "SELECT [Mt_Costo] FROM [dbo].[K_CREDITO_COSTO] WHERE [No_Credito] = @CreditNum";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@CreditNum", credit)
                };

                object TotalCost = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
                if (TotalCost != null)
                {
                    Result = TotalCost.ToString();
                }
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get total cost failed.", ex, true);
            }

            return Result;
        }
    }
}
