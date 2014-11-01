/* ----------------------------------------------------------------------
 * File Name: CAT_TARIFADAL.cs
 * 
 * Create Author: Tina
 * 
 * Create DateTime: 2011/6/27
 *
 * Description:   CAT_TARIFA data access lay
 *----------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PAEEEM.Helpers;

namespace PAEEEM.DataAccessLayer
{
    /// <summary>
    /// CAT_TARIFA data access lay
    /// </summary>
    public class CAT_TARIFADAL
    {
        /// <summary>
        /// Class instance field
        /// </summary>
        private static readonly CAT_TARIFADAL _classinstance = new CAT_TARIFADAL();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static CAT_TARIFADAL ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// Get All the Tarifa
        /// </summary>
        /// <returns></returns>
        public DataTable Get_All_CAT_TARIFA()
        {
            try
            {
                string SQL = "SELECT Cve_Tarifa,Dx_Tarifa FROM CAT_TARIFA";
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get all Estado failed: Execute method Get_All_CAT_ESTADO in CAT_ESTADO.", ex, true);
            }
        }
        /// <summary>
        /// Get tarifa with date
        /// </summary>
        /// <param name="currentDate">Current Date</param>
        /// <param name="programID">Program ID</param>
        /// <returns>Table With Tarifa Cost</returns>
        public DataTable GetTarifaWithDate(DateTime currentDate, int programID)
        {
            DataTable dtTarifa = null;
            string SQL = "";

            try
            {
                SQL = "SELECT T.[Cve_Tarifa],T.[Dx_Tarifa],T.[No_Cargo_Fijo],T.[No_Basico_Hasta],T.[No_Intermedio_Hasta],T.[No_Excedente_Mayor_que],T.[Dt_Fecha_Tarifa] "+
                            "FROM [dbo].[CAT_TARIFA] AS T  INNER JOIN K_PROGRAMA_TARIFA AS PT ON T.Cve_Tarifa = PT.Cve_Tarifa  "+
                            //"WHERE CONVERT(VARCHAR(7),T.[Dt_Fecha_Tarifa],120) = CONVERT(VARCHAR(7),CAST(CONVERT(VARCHAR(10),@CurrentDate,120) AS datetime),120) AND PT.ID_Prog_Proy = 1";
                            // "WHERE CONVERT(VARCHAR(7),T.[Dt_Fecha_Tarifa],120) = CONVERT(VARCHAR(7),CAST(CONVERT(VARCHAR(10),@CurrentDate,120) AS datetime),120)
                            "where PT.ID_Prog_Proy = 1";

                           
                   SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@CurrentDate", currentDate)
                };

                dtTarifa = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get specified date tarifa failed: Execute method GetTarifaWithDate in CAT_ESTADO.", ex, true);
            }

            return dtTarifa;
        }
        /// <summary>
        /// Get tarifa without date
        /// </summary>
        /// <param name="programID">Program ID</param>
        /// <returns></returns>
        public DataTable GetTarifaWithoutDate(int programID)
        {
            DataTable dtTarifa = null;
            string SQL = "";

            try
            {
                SQL = "SELECT T.[Cve_Tarifa],T.[Dx_Tarifa],T.[No_Cargo_Fijo],T.[No_Basico_Hasta],T.[No_Intermedio_Hasta],T.[No_Excedente_Mayor_que],T.[Dt_Fecha_Tarifa] " +
                            "FROM [dbo].[CAT_TARIFA] AS T  INNER JOIN K_PROGRAMA_TARIFA AS PT ON T.Cve_Tarifa = PT.Cve_Tarifa  " +
                            "WHERE  PT.ID_Prog_Proy = 1";
                
                dtTarifa = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get tarifa failed: Execute method GetTarifaWithoutDate in CAT_ESTADO.", ex, true);
            }

            return dtTarifa;
        }
        /// <summary>
        /// Get rate with rate name
        /// </summary>
        /// <param name="ratename"></param>
        /// <returns></returns>
        public DataTable GetTarifaWithName(string ratename)
        {
            DataTable dtTarifa = null;
            string SQL = "";

            try
            {
                SQL = "SELECT T.[Cve_Tarifa],T.[Dx_Tarifa],T.[No_Cargo_Fijo],T.[No_Basico_Hasta],T.[No_Intermedio_Hasta],T.[No_Excedente_Mayor_que],T.[Dt_Fecha_Tarifa] " +
                            "FROM [dbo].[CAT_TARIFA] AS T  WHERE T.Dx_Tarifa = @RateName  ";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@RateName", ratename)
                };
                dtTarifa = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get tarifa failed: Execute method GetTarifaWithName in CAT_ESTADO.", ex, true);
            }

            return dtTarifa;
        }

        public DataTable GetTarifaCFEWithoutDate(string ratename)
        {
            DataTable dtTarifa = null;
            string SQL = "";

            try
            {
                SQL = "SELECT CFE.[Cve_Tarifa],T.[Dx_Tarifa],CFE.[Mt_Tarifa_Demanda],CFE.[Mt_Costo_Tarifa_Consumo] " +
                            "FROM [dbo].[K_TARIFA_PROMEDIO_CFE] AS CFE inner join [dbo].[CAT_TARIFA] as T on CFE.[Cve_Tarifa] = T.Cve_Tarifa "+
                            "WHERE   T.[Dx_Tarifa]= @ratename";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@ratename", ratename)
                };
                dtTarifa = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL,paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get tarifa failed: Execute method GetTarifacfeWithoutDate ", ex, true);
            }

            return dtTarifa;
        }




    }
}
