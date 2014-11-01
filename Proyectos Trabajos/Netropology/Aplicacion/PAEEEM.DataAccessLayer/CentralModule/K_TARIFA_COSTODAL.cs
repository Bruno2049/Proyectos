/* ----------------------------------------------------------------------
 * File Name: K_TARIFA_COSTODAL.cs
 * 
 * Create Author: Tina
 * 
 * Create DateTime: 2011/6/27
 *
 * Description:   K_TARIFA_COSTO data access lay
 *----------------------------------------------------------------------*/

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
    /// K_TARIFA_COSTO data access lay
    /// </summary>
    public class K_TARIFA_COSTODAL
    {
        private static readonly K_TARIFA_COSTODAL _classinstance = new K_TARIFA_COSTODAL();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static K_TARIFA_COSTODAL ClassInstance { get { return _classinstance; } }

        /// /// <summary>
        /// Get Record by Estado
        /// </summary>
        /// <param name="sorder"></param>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="Cve_Estado"></param>
        /// <param name="Periodo"></param>
        /// <param name="Cve_Tarifa"></param>
        /// <param name="pagecount"></param>
        /// <returns></returns>
        public DataTable Get_K_TARIFA_COSTOListByPagerAndEstadoID(String sorder, Int32 currentPageIndex, Int32 pageSize, int Cve_Estado, String Periodo, int Cve_Tarifa, out Int32 pagecount)
        {
            try
            {
                if (string.IsNullOrEmpty(sorder))
                {
                    sorder = "Cve_Estado ASC,Dt_Periodo_Tarifa_Costo DESC";
                }

                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@Count",SqlDbType.Int),			
                new SqlParameter("@OrderBy",sorder),
                new SqlParameter("@CurrentPageIndex",currentPageIndex),
				new SqlParameter("@PageSize",pageSize),
                new SqlParameter("@Cve_Estado",Cve_Estado),
                new SqlParameter("@Periodo",Periodo),
                new SqlParameter("@Cve_Tarifa",Cve_Tarifa)
            };
                para[0].Direction = ParameterDirection.Output;
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "dbo.UP_Pager_GetView_TARIFA_COSTOList", para);
                int.TryParse(para[0].Value.ToString(), out pagecount);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get tarifa cost failed: Execute method Get_K_TARIFA_COSTOListByPagerAndEstadoID in K_TARIFA_COSTODAL.", ex, true);
            }
        }
        /// <summary>
        /// Add K_TARIFA_COSTO
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Insert_K_TARIFA_COSTO(K_TARIFA_COSTOModel instance)
        {
            try
            {
                string executesqlstr = "INSERT INTO K_TARIFA_COSTO (Cve_Tarifa,Mt_Costo_Kw_h_Fijo,Mt_Costo_Kw_h_Basico,Mt_Costo_Kw_h_Intermedio,Mt_Costo_Kw_h_Excedente,Dt_Periodo_Tarifa_Costo,Cve_Estado,Dt_Fecha_UltMod,MT_Tarifa_Demanda,MT_Costo_Tarifa_Consumo)" +
                                                "VALUES (@Cve_Tarifa,@Mt_Costo_Kw_h_Fijo,@Mt_Costo_Kw_h_Basico,@Mt_Costo_Kw_h_Intermedio,@Mt_Costo_Kw_h_Excedente,@Dt_Periodo_Tarifa_Costo,@Cve_Estado,@Dt_Fecha_UltMod,@Mt_Tarifa_Demanda,@Mt_Costo_Tarifa_Consumo)";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Cve_Tarifa",instance.Cve_Tarifa),
                    new SqlParameter("@Mt_Costo_Kw_h_Fijo",instance.Mt_Costo_Kw_h_Fijo),
                    new SqlParameter("@Mt_Costo_Kw_h_Basico",instance.Mt_Costo_Kw_h_Basico),
                    new SqlParameter("@Mt_Costo_Kw_h_Intermedio",instance.Mt_Costo_Kw_h_Intermedio),
                    new SqlParameter("@Mt_Costo_Kw_h_Excedente",instance.Mt_Costo_Kw_h_Excedente),
                    new SqlParameter("@Dt_Periodo_Tarifa_Costo",instance.Dt_Periodo_Tarifa_Costo),
                    new SqlParameter("@Cve_Estado",instance.Cve_Estado),
                    new SqlParameter("@Dt_Fecha_UltMod",instance.Dt_Fecha_UltMod),
                    new SqlParameter("@Mt_Tarifa_Demanda", instance.MT_Tarifa_Demanda),
                    new SqlParameter("@Mt_Costo_Tarifa_Consumo", instance.MT_Costo_Tarifa_Consumo)

                };

                return SqlHelper.ExecuteNonQueryGetID(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Add K_TARIFA_COSTO failed: Execute method Insert_K_TARIFA_COSTO in K_TARIFA_COSTODAL.", ex, true);
            }
        }
        /// <summary>
        /// Get tarifa and cost with tarifa and estado
        /// </summary>
        /// <param name="tarifa">Tarifa ID</param>
        /// <param name="estado">Estado ID</param>
        /// <param name="currentDate">Current Date</param>
        /// <returns></returns>
        public DataTable GetTarifaAndCostWithDateEstado(string tarifa, int estado, DateTime currentDate)
        {
            DataTable dtTarifaCost = null;
            string SQL = "";
            try
            {
                SQL = "SELECT TC.[Cve_Tarifa],TC.[Mt_Costo_Kw_h_Fijo],TC.[Mt_Costo_Kw_h_Basico],TC.[Mt_Costo_Kw_h_Intermedio],TC.[Mt_Costo_Kw_h_Excedente],TC.[Dt_Periodo_Tarifa_Costo]" +
                            ",TC.[Cve_Estado],TC.[Dt_Fecha_UltMod],TC.[Fl_Tarifa_Costo],TC.Mt_Tarifa_Demanda,TC.Mt_Costo_Tarifa_Consumo FROM [dbo].[K_TARIFA_COSTO] AS TC  INNER JOIN [dbo].[CAT_TARIFA] AS T ON TC.Cve_Tarifa = T.Cve_Tarifa WHERE  T.Dx_Tarifa = @Tarifa AND TC.Cve_Estado = @Estado " + //+
                           // "AND CONVERT(VARCHAR(7),TC.[Dt_Periodo_Tarifa_Costo],120) = CONVERT(VARCHAR(7),CAST(CONVERT(VARCHAR(10),@CurrentDate,120) AS datetime),120)";
                             "AND CONVERT(VARCHAR(7),TC.[Dt_Periodo_Tarifa_Costo],120) = CONVERT(VARCHAR(7),CAST(CONVERT(VARCHAR(10),@CurrentDate,120) AS datetime),120)";
                         

                    SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Tarifa", tarifa),
                    new SqlParameter("@Estado", estado),
                    new SqlParameter("@CurrentDate", currentDate)
                };

                dtTarifaCost = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get tarifa and cost failed: Execute method GetTarifaAndCostWithDateEstado in K_TARIFA_COSTODAL.", ex, true);
            }

            return dtTarifaCost;
        }
        // Update by Tina 2011/08/02
        /// <summary>
        /// Get tarifa and cost by tarifa and period
        /// </summary>
        /// <param name="estado">estado ID</param>
        /// <param name="period">period</param>
        /// <returns></returns>
        public DataTable GetTarifaAndCostByEstadoAndPeriod(int estado, DateTime period)
        {
            DataTable dtTarifaCost = null;
            string SQL = "";
            try
            {
                SQL = " SELECT Fl_Tarifa_Costo,Cve_Tarifa,Mt_Costo_Kw_h_Fijo,Mt_Costo_Kw_h_Basico,Mt_Costo_Kw_h_Intermedio,Mt_Costo_Kw_h_Excedente,Dt_Periodo_Tarifa_Costo,Cve_Estado, Mt_Tarifa_Demanda, Mt_Costo_Tarifa_Consumo "+
                            "FROM K_TARIFA_COSTO  WHERE ISNULL(Cve_Estado,0) = @estado " +
                            "AND CONVERT(VARCHAR(7),Dt_Periodo_Tarifa_Costo,120) = CONVERT(VARCHAR(7),CAST(CONVERT(VARCHAR(10),@period,120) AS datetime),120)";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@estado", estado),
                    new SqlParameter("@period", period)
                };

                dtTarifaCost = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get tarifa and cost failed: Execute method GetTarifaAndCostWithDateEstado in K_TARIFA_COSTODAL.", ex, true);
            }

            return dtTarifaCost;
        }
        // End
        /// <summary>
        /// Get tarifa and cost by pk
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public DataTable GetTarifaAndCostByPK(int id)
        {
            DataTable dtTarifaCost = null;
            string SQL = "";
            try
            {
                SQL = " SELECT Fl_Tarifa_Costo,Cve_Tarifa,Mt_Costo_Kw_h_Fijo,Mt_Costo_Kw_h_Basico,Mt_Costo_Kw_h_Intermedio,Mt_Costo_Kw_h_Excedente,Dt_Periodo_Tarifa_Costo,Cve_Estado, Mt_Tarifa_Demanda, Mt_Costo_Tarifa_Consumo " +
                            "FROM K_TARIFA_COSTO  WHERE Fl_Tarifa_Costo = @id ";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@id", id),
                };

                dtTarifaCost = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get tarifa and cost failed: Execute method GetTarifaAndCostWithDateEstado in K_TARIFA_COSTODAL.", ex, true);
            }

            return dtTarifaCost;
        }
        // Update by Tina 2011/08/02
        /// <summary>
        /// edit tarifa and cost
        /// </summary>
        /// <param name="model">K_TARIFA_COSTOModel</param>
        /// <returns></returns>
        public int UpdateTarifaAndCost(K_TARIFA_COSTOModel model)
        {
            int iResult = 0;
            string SQL = "";
            try
            {
                SQL = "UPDATE K_TARIFA_COSTO SET Cve_Tarifa=@Cve_Tarifa, Mt_Costo_Kw_h_Fijo=@Mt_Costo_Kw_h_Fijo,Mt_Costo_Kw_h_Basico=@Mt_Costo_Kw_h_Basico,Mt_Costo_Kw_h_Intermedio=@Mt_Costo_Kw_h_Intermedio, " +
                        " Mt_Costo_Kw_h_Excedente=@Mt_Costo_Kw_h_Excedente,Dt_Fecha_UltMod=@Dt_Fecha_UltMod, Mt_Tarifa_Demanda=@Mt_Tarifa_Demanda, Mt_Costo_Tarifa_Consumo=@Mt_Costo_Tarifa_Consumo WHERE Fl_Tarifa_Costo=@id";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Cve_Tarifa", model.Cve_Tarifa),
                    new SqlParameter("@Mt_Costo_Kw_h_Fijo", model.Mt_Costo_Kw_h_Fijo),
                    new SqlParameter("@Mt_Costo_Kw_h_Basico", model.Mt_Costo_Kw_h_Basico),
                    new SqlParameter("@Mt_Costo_Kw_h_Intermedio", model.Mt_Costo_Kw_h_Intermedio),
                    new SqlParameter("@Mt_Costo_Kw_h_Excedente", model.Mt_Costo_Kw_h_Excedente),
                    new SqlParameter("@Dt_Fecha_UltMod", model.Dt_Fecha_UltMod),
                    new SqlParameter("@id", model.Fl_Tarifa_Costo),
                    new SqlParameter("@Mt_Tarifa_Demanda", model.MT_Tarifa_Demanda),
                    new SqlParameter("@Mt_Costo_Tarifa_Consumo", model.MT_Costo_Tarifa_Consumo)

                };

                iResult = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Rechazar credit failed: Execute method RechazarCredit in CreditDal.", ex, true);
            }

            return iResult;
        }
        // End
    }
}
