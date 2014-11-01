
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using PAEEEM.Entities;
using PAEEEM.Helpers;
namespace PAEEEM.DataAccessLayer
{
    /// <summary>
    /// Credit amortizacion table
    /// </summary>
    public class K_CREDITO_AMORTIZACIONDal
    {
        /// <summary>
        /// Readonly variable for class instance
        /// </summary>
        private static readonly K_CREDITO_AMORTIZACIONDal _classInstance = new K_CREDITO_AMORTIZACIONDal();
        /// <summary>
        /// Property for class instance
        /// </summary>
        public static K_CREDITO_AMORTIZACIONDal ClassInstance { get { return _classInstance; } }
        /// <summary>
        /// Add credit amortizacion record
        /// </summary>
        /// <param name="creditNo"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int Insert_K_CREDITO_AMORTIZACION(string creditNo,DataTable dt)
        {
            int iCount = 0;
            try
            {
                //string SQL1 = "INSERT INTO K_CREDITO_AMORTIZACION (No_Credito,No_Pago,Dt_Fecha,No_Dias_Periodo,Mt_Capital,Mt_Interes, ";
                //SQL1 = SQL1 + "   Mt_IVA,Mt_Pago,Mt_Amortizacion,Mt_Saldo,Dt_Fecha_Credito_Amortización)";
                //SQL1 = SQL1 + " VALUES (@No_Credito,@No_Pago,cast(convert(varchar(10),@Dt_Fecha,120) as datetime),@No_Dias_Periodo,@Mt_Capital,@Mt_Interes";
                //SQL1 = SQL1 + ",@Mt_IVA,@Mt_Pago,@Mt_Amortizacion,@Mt_Saldo,cast(convert(varchar(10),@Dt_Fecha_Credito_Amortización,120) as datetime))";
                string SQL = "INSERT INTO K_CREDITO_AMORTIZACION (No_Credito,No_Pago,Dt_Fecha,No_Dias_Periodo,Mt_Capital,Mt_Interes, ";
                SQL = SQL + "   Mt_IVA,Mt_Pago,Mt_Amortizacion,Mt_Saldo,Dt_Fecha_Credito_Amortización)";
                SQL = SQL + " VALUES (@No_Credito,@No_Pago,@Dt_Fecha,@No_Dias_Periodo,@Mt_Capital,@Mt_Interes";
                SQL = SQL + ",@Mt_IVA,@Mt_Pago,@Mt_Amortizacion,@Mt_Saldo,@Dt_Fecha_Credito_Amortización)";
               
                foreach (DataRow row in dt.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[]{
                     new SqlParameter("@No_Credito", creditNo),
                    new SqlParameter("@No_Pago", row["No_Pago"].ToString()),
                    new SqlParameter("@Dt_Fecha",row["Dt_Fecha"]),
                    new SqlParameter("No_Dias_Periodo",row["No_Dias_Periodo"].ToString()),
                    new SqlParameter("@Mt_Capital", row["Mt_Capital"].ToString()),
                    new SqlParameter("@Mt_Interes", row["Mt_Interes"].ToString()),
                    new SqlParameter("@Mt_IVA", row["Mt_IVA"].ToString()),
                    new SqlParameter("Mt_Pago",row["Mt_Pago"].ToString()),
                     new SqlParameter("@Mt_Amortizacion", row["Mt_Amortizacion"].ToString()),
                    new SqlParameter("@Mt_Saldo", row["Mt_Saldo"].ToString()),
                    new SqlParameter("@Dt_Fecha_Credito_Amortización", row["Dt_Fecha_Credito_Amortización"])                 
                     };
                    iCount = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
                    iCount = iCount + 1;
                }               
            }
            catch (Exception ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
            return iCount;
        }
        /// <summary>
        /// Delete credit amortizacion record
        /// </summary>
        /// <param name="creditNo"></param>
        /// <returns></returns>
        public int Dalete_K_CREDITO_AMORTIZACION(string creditNo)
        {
            int iCount = 0;
            try
            {
                string Sql = "DELETE FROM K_CREDITO_AMORTIZACION WHERE No_Credito =@No_Credito";
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
    }
}
