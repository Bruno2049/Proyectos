using System.Text;
using System.Data;
using System.Data.SqlClient;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using System;

namespace PAEEEM.DataAccessLayer
{
    /// <summary>
    /// Data Access Layer for credit related discount
    /// </summary>
    public class K_CREDITO_DESCUENTODal
    {
        /// <summary>
        /// Readonly variable for class instance
        /// </summary>
        private static readonly K_CREDITO_DESCUENTODal _classInstance = new K_CREDITO_DESCUENTODal();
        /// <summary>
        /// Property for class instance
        /// </summary>
        public static K_CREDITO_DESCUENTODal ClassInstance { get { return _classInstance; } }
        /// <summary>
        /// Insert data
        /// </summary>
        /// <param name="Instance"></param>
        /// <returns></returns>
        public int Insert_K_CREDITO_DESCUENTO(K_CREDITO_DESCUENTOEntity Instance)
        {
            int iCount = 0;
            try
            {
                string SQL = "INSERT INTO K_CREDITO_DESCUENTO ([No_Credito] ,[Mt_Descuento],[Dt_Credito_Descuento])  VALUES(@No_Credito,@Mt_Descuento,@Dt_Credito_Descuento)";
                SqlParameter[] paras = new SqlParameter[]{
                     new SqlParameter("@No_Credito", Instance.No_Credito),
                    new SqlParameter("@Mt_Descuento", Instance.Mt_Descuento),
                    new SqlParameter("@Dt_Credito_Descuento", Instance.Dt_Credito_Descuento)
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
        /// Update credit discount
        /// </summary>
        /// <param name="Instance"></param>
        /// <returns></returns>
        public int Update_K_CREDITO_DESCUENTO(K_CREDITO_DESCUENTOEntity Instance)
        {
            int iCount = 0;
            try
            {
                string SQL = "UPDATE K_CREDITO_DESCUENTO SET Mt_Descuento =@Mt_Descuento ,Dt_Credito_Descuento =@Dt_Credito_Descuento WHERE  No_Credito=@No_Credito";
                SqlParameter[] paras = new SqlParameter[]{
                       new SqlParameter("@No_Credito", Instance.No_Credito),
                    new SqlParameter("@Mt_Descuento", Instance.Mt_Descuento),
                    new SqlParameter("@Dt_Credito_Descuento", Instance.Dt_Credito_Descuento)
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
        /// delete K_CREDITO_DESCUENTO
        /// </summary>
        /// <param name="creditNo"></param>
        /// <returns></returns>
        public int Delete_K_CREDITO_DESCUENTO(string creditNo)
        {
            int iCount = 0;
            try
            {
                string Sql = "DELETE FROM K_CREDITO_DESCUENTO WHERE No_Credito =@No_Credito";
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
        /// Get total discount
        /// </summary>
        /// <param name="credit"></param>
        /// <returns></returns>
        public string GetTotalDescount(string credit)
        {
            string Result = "";
            string SQL = "";
            try
            {
                SQL = "SELECT [Mt_Descuento] FROM [dbo].[K_CREDITO_DESCUENTO] WHERE [No_Credito] = @CreditNum";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@CreditNum", credit)
                };

                object TotalDescount = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
                if (TotalDescount != null)
                {
                    Result = TotalDescount.ToString();
                }
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get total discount failed.", ex, true);
            }

            return Result;
        }
    }
}
