/*	
	$Author:     coco,wang
	$Date:       2011-07-06	
*/

using System.Data;
using System.Data.SqlClient;
using PAEEEM.Helpers;

namespace PAEEEM.DataAccessLayer
{
    /// <summary>
    /// Data Access Layer for program
    /// </summary>
    public class CAT_PROGRAMADal
    {
        /// <summary>
        /// Property field
        /// </summary>
        private static readonly CAT_PROGRAMADal _classinstance = new CAT_PROGRAMADal();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static CAT_PROGRAMADal ClassInstance { get { return _classinstance; } }
        /// <summary>
        /// Get program with program id
        /// </summary>
        /// <param name="strPk"></param>
        /// <returns></returns>
        public DataTable Get_All_CAT_PROGRAMAByPK(string strPk)
        {
            try
            {
                string SQL = "select * from CAT_PROGRAMA where ID_Prog_Proy =@ID_Prog_Proy";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@ID_Prog_Proy", strPk)                   
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        /// <summary>
        /// Validate rate
        /// </summary>
        /// <param name="ProgramID">Program</param>
        /// <param name="Rate">Rate</param>
        /// <returns></returns>
        public DataTable get_Cat_Tarifa(string ProgramID, string Rate)
        {
            try
            {
                string SQL = "select * from CAT_TARIFA A inner join K_PROGRAMA_TARIFA B on A.Cve_Tarifa=B.Cve_Tarifa";
                SQL = SQL + "  and B.ID_Prog_Proy=@ID_Prog_Proy where A.Dx_Tarifa =@Dx_Tarifa";
                SqlParameter[] para = new SqlParameter[] { 
                        new SqlParameter("@ID_Prog_Proy", ProgramID),
                        new SqlParameter("@Dx_Tarifa",Rate)
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);
                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        // Add by Tina 2011/08/12
        /// <summary>
        /// update the current amount of the program 
        /// </summary>
        /// <param name="ProgramID"></param>
        /// <param name="RequestAmount"></param>
        /// <returns></returns>
        public int SubtractCurrentAmount(int ProgramID, decimal RequestAmount)
        {
            int iCount = 0;
            try
            {
                string SQL = "update CAT_PROGRAMA set Mt_Fondo_Disponible=ISNULL(Mt_Fondo_Disponible,0)-@RequestAmount where ID_Prog_Proy=@ProgramID";
                SqlParameter[] paras = new SqlParameter[]{
                     new SqlParameter("@RequestAmount", RequestAmount),
                    new SqlParameter("@ProgramID", ProgramID)
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
        /// update the current amount of the program 
        /// </summary>
        /// <param name="ProgramID"></param>
        /// <param name="RequestAmount"></param>
        /// <returns></returns>
        public int IncreaseCurrentAmount(int ProgramID, decimal RequestAmount)
        {
            int iCount = 0;
            try
            {
                string SQL = "update CAT_PROGRAMA set Mt_Fondo_Disponible=ISNULL(Mt_Fondo_Disponible,0)+@RequestAmount where ID_Prog_Proy=@ProgramID";
                SqlParameter[] paras = new SqlParameter[]{
                     new SqlParameter("@RequestAmount", RequestAmount),
                    new SqlParameter("@ProgramID", ProgramID)
                };
                iCount = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
            return iCount;
        }
        // End
        /// <summary>
        /// get program with credit
        /// </summary>
        /// <param name="CreditNo"></param>
        /// <returns></returns>
        public DataTable Get_PROGRAMAByCreditNo(string CreditNo)
        {
            try
            {
                string SQL = "select * from CAT_PROGRAMA where ID_Prog_Proy = (select ID_Prog_Proy from CRE_CREDITO where No_Credito=@CreditNo)";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@CreditNo", CreditNo)                   
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        /// <summary>
        /// Get all defined programs in this system
        /// </summary>
        /// <returns>Programs</returns>
        public DataTable GetPrograms()
        {
            DataTable dt = null;
            try
            {
                string SQL = @"SELECT [ID_Prog_Proy]
                                                          ,[Dt_Fecha_Alta_Programa]
                                                          ,[Cve_Tipo_Industria]
                                                          ,[Dx_Nombre_Programa]
                                                          ,[Fg_Aplica_Aval]
                                                          ,[Mt_Fondo_Total_Programa]
                                                          ,[Mt_Fondo_Disponible]
                                                          ,[Mt_Total_Autorizado]
                                                          ,[Mt_Monto_Minimo]
                                                          ,[Mt_Monto_Maximo]
                                                          ,[Pct_Tasa_IVA_Intereses]
                                                          ,[No_Plazo]
                                                          ,[Pct_Tasa_Interes]
                                                          ,[Pct_Tasa_Fija]
                                                          ,[Pct_CAT_Factura_Mensual]
                                                          ,[Pct_CAT_Factura_Bimestral]
                                                          ,[No_Calif_MOP]
                                                          ,[No_Estatus_CFE]
                                                          ,[Dx_Folio_Programa]
                                                          ,[Dx_Categoria_Apoyo_Programa]
                                                          ,[Dx_Subcategoria_Apoyo_Programa]
                                                          ,[Dx_Concepto_Apoyo_Programa]
                                                          ,[Dx_Concepto_Chatarrizacion]
                                                          ,[Dt_Fecha_UltMod]
                                                      FROM [dbo].[CAT_PROGRAMA]";
                dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);                
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }

            return dt;
        }
    }
}
