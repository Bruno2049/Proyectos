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
    /// K_CORTE_PARCIAL data access lay
    /// </summary>
    public class K_CORTE_PARCIALDAL
    {
        private static readonly K_CORTE_PARCIALDAL _classinstance = new K_CORTE_PARCIALDAL();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static K_CORTE_PARCIALDAL ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Insert_K_CORTE_PARCIAL(K_CORTE_PARCIALModel instance)
        {
            try
            {
                string executesqlstr = "INSERT INTO K_CORTE_PARCIAL (ID_Centro_Disp,Codigo_Partial,Codigo_Producto,Cve_Tecnologia,Cve_Material,Dt_Fecha_Creacion,ID_Estatus,Fg_Aprobacion," +
                                              " Peso_Inicial,Fg_Tipo_Centro_Disp) VALUES(@ID_Centro_Disp,@Codigo_Partial,@Codigo_Producto,@Cve_Tecnologia,@Cve_Material,@Dt_Fecha_Creacion," +
                                              " @ID_Estatus,@Fg_Aprobacion,@Peso_Inicial,@Fg_Tipo_Centro_Disp)";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@ID_Centro_Disp",instance.ID_Centro_Disp),
                    new SqlParameter("@Codigo_Partial",instance.Codigo_Partial),
                    new SqlParameter("@Codigo_Producto",instance.Codigo_Producto),
                    new SqlParameter("@Cve_Tecnologia",instance.Cve_Tecnologia),
                    new SqlParameter("@Cve_Material",instance.Cve_Material),
                    new SqlParameter("@Dt_Fecha_Creacion",instance.Dt_Fecha_Creacion),
                    new SqlParameter("@ID_Estatus",instance.ID_Estatus),
                    new SqlParameter("@Fg_Aprobacion",instance.Fg_Aprobacion),
                    new SqlParameter("@Peso_Inicial",instance.Peso_Inicial),
                    new SqlParameter("@Fg_Tipo_Centro_Disp",instance.Fg_Tipo_Centro_Disp)
                };
                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Add K_CORTE_PARCIAL failed: Execute method Insert_K_CORTE_PARCIAL in K_CORTE_PARCIALDAL.", ex, true);
            }
        }
        /// <summary>
        /// get create date of partial cut
        /// </summary>
        /// <returns></returns>
        public DataTable GetCreateDate()
        {
            try
            {
                string SQL = "select distinct CONVERT(VARCHAR(10),Dt_Fecha_Creacion, 120) as Dt_Fecha_Creacion from K_CORTE_PARCIAL" +
                                  " where Codigo_Producto in (select distinct Codigo_Producto from K_RECUP_RESIDUOS where Fg_Incluido='Y')" +
                                  " and ISNULL(Fg_Aprobacion,'')<>'Y'" +
                                  " and ID not in (select ID_CORTE_PARCIAL from K_RELACION_ACTA_CORTES)";
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get create date failed: Execute method GetCreateDate in K_CORTE_PARCIALDAL.", ex, true);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Program"></param>
        /// <param name="Disposal"></param>
        /// <param name="DisposalType"></param>
        /// <param name="CreateDate"></param>
        /// <param name="Estatus"></param>
        /// <param name="sortName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable GetPartialCutsForApproval(int Program, int Disposal, string DisposalType, string CreateDate, int Estatus, string sortName, int pageIndex, int pageSize, out int pageCount)
        {
            DataTable dtResult = null;
            string strWhere = "";
            try
            {
                strWhere = " INNER JOIN(" +
                                " SELECT Id_Centro_Disp,Dx_Razon_Social+'(Main Center)' as Dx_Razon_Social,'M' as Fg_Tipo_Centro_Disp FROM CAT_CENTRO_DISP" +
                                " UNION" +
                                " SELECT Id_Centro_Disp_Sucursal as Id_Centro_Disp,Dx_Razon_Social+'(Branch)' as Dx_Razon_Social,'B' as Fg_Tipo_Centro_Disp FROM CAT_CENTRO_DISP_SUCURSAL) B" +
                                " ON A.ID_Centro_Disp=B.Id_Centro_Disp and A.Fg_Tipo_Centro_Disp=B.Fg_Tipo_Centro_Disp" +
                                " INNER JOIN" +
                                " (SELECT Codigo_Partial,COUNT(Codigo_Partial) as Num_Products from (SELECT DISTINCT Codigo_Partial,Codigo_Producto FROM K_CORTE_PARCIAL) K_CORTE_PARCIAL GROUP BY Codigo_Partial) C" +
                                " ON A.Codigo_Partial=C.Codigo_Partial" +
                                " INNER JOIN" +
                                " (SELECT DISTINCT Codigo_Partial FROM K_CORTE_PARCIAL WHERE ID NOT IN (SELECT ID_CORTE_PARCIAL FROM K_RELACION_ACTA_CORTES)) D" +
                                " ON A.Codigo_Partial=D.Codigo_Partial";
                strWhere += " WHERE 1=1";
                if (Program != 0)
                {
                    strWhere += " AND A.ID_Prog_Proy=" + Program;
                }
                if (Disposal != 0)
                {
                    strWhere += " AND A.ID_Centro_Disp=" + Disposal;
                }
                if (DisposalType != "")
                {
                    strWhere += " AND A.Fg_Tipo_Centro_Disp='" + DisposalType + "'";
                }
                if (CreateDate != "")
                {
                    strWhere += " AND CONVERT(VARCHAR(10),A.Dt_Fecha_Creacion, 120) as Dt_Fecha_Creacion='" + CreateDate + "'";
                }
                if (Estatus != 0)
                {
                    strWhere += " AND A.ID_Estatus=" + Estatus;
                }

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@PageCount", SqlDbType.Int),
                    new SqlParameter("@OrderBy", sortName),
                    new SqlParameter("@PageIndex", pageIndex),
                    new SqlParameter("@PageSize", pageSize),
                    new SqlParameter("@spWhere", strWhere)
                };
                paras[0].Direction = ParameterDirection.Output;

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "UP_Pager_GetView_PartialCuts_ForApproval", paras);
                int.TryParse(paras[0].Value.ToString(), out pageCount);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Get partial cuts failed:Execute method  GetPartialCutsForApproval in K_CORTE_PARCIALDAL.", ex, true);
            }
            return dtResult;
        }
        /// <summary>
        /// Get partial act create date
        /// </summary>
        /// <param name="DisposalID"></param>
        /// <param name="UserType"></param>
        /// <returns></returns>
        public DataTable Get_Partial_Act_CreateDate(int DisposalID, string UserType)
        {
            try
            {
                string SQL = "select distinct CONVERT(VARCHAR(10),Dt_Fecha_Creacion, 120) as Dt_Fecha_Creacion from K_CORTE_PARCIAL where ID_Centro_Disp =@ID_Centro_Disp and Fg_Tipo_Centro_Disp=@Fg_Tipo_Centro_Disp";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@ID_Centro_Disp", DisposalID),
                    new SqlParameter("@Fg_Tipo_Centro_Disp", UserType)                
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get create date failed: Execute method GetCreateDate in K_CORTE_PARCIALDAL.", ex, true);
            }
        }
        /// <summary>
        /// Get partial cuts
        /// </summary>
        /// <param name="Disposal"></param>
        /// <param name="DisposalType"></param>
        /// <param name="FromPeso"></param>
        /// <param name="ToPeso"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable GetPartialCutsForFinialAct(int Disposal, string DisposalType, string FromPeso,string ToPeso, int pageIndex, int pageSize, out int pageCount)
        {
            DataTable dtResult = null;
            string strWhere = "";
            try
            {
                strWhere += " WHERE 1=1";
                if (Disposal != 0)
                {
                    strWhere += " AND ID_Centro_Disp=" + Disposal;
                }
                if (DisposalType != "")
                {
                    strWhere += " AND Fg_Tipo_Centro_Disp='" + DisposalType + "'";
                }
                if (FromPeso != "")
                {
                    strWhere += " AND SUBSTRING(Ltrim(Rtrim(Codigo_Producto)),1,6) >='" + FromPeso + "'";
                }
                if (ToPeso != "")
                {
                    strWhere += " AND SUBSTRING(Ltrim(Rtrim(Codigo_Producto)),1,6) <='" + ToPeso + "'";
                }
                strWhere += "  AND ID_Estatus=" + DisposalStatus.COMPLETADO;
                strWhere += " and ID not in(select ID_CORTE_PARCIAL from K_RELACION_ACTA_CORTES)";

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@PageCount", SqlDbType.Int),                
                    new SqlParameter("@PageIndex", pageIndex),
                    new SqlParameter("@PageSize", pageSize),
                    new SqlParameter("@spWhere", strWhere)
                };
                paras[0].Direction = ParameterDirection.Output;

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "sp_get_FinialAct", paras);
                int.TryParse(paras[0].Value.ToString(), out pageCount);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Get partial cuts failed:Execute method  GetPartialCutsForFinialAct in K_CORTE_PARCIALDAL.", ex, true);
            }
            return dtResult;
        }
        /// <summary>
        /// get products with partial cut for approval
        /// </summary>
        /// <param name="PartialCode"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable GetPartialCutsProductsForApproval(string PartialCode, int pageIndex, int pageSize, out int pageCount)
        {
            DataTable dtResult = null;
            string strWhere = "";
            try
            {
                strWhere += " WHERE 1=1 AND Codigo_Partial='" + PartialCode + "' AND ISNULL(Fg_Aprobacion,'')<>'Y'";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@PageCount", SqlDbType.Int),                
                    new SqlParameter("@PageIndex", pageIndex),
                    new SqlParameter("@PageSize", pageSize),
                    new SqlParameter("@spWhere", strWhere)
                };
                paras[0].Direction = ParameterDirection.Output;

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "UP_Pager_GetView_PartialCuts_Products_ForApproval", paras);
                int.TryParse(paras[0].Value.ToString(), out pageCount);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get products of the partial cuts failed:Execute method  GetPartialCutsProductsForApproval in K_CORTE_PARCIALDAL.", ex, true);
            }
            return dtResult;
        }
        /// <summary>
        /// Get partial acts
        /// </summary>
        /// <param name="Disposal"></param>
        /// <param name="UserType"></param>
        /// <returns></returns>
        public DataTable Get_PartialAct_ForFinial(int Disposal, string UserType)
        {
            DataTable dtPartial = null;
            try
            {
                string Sql = "select  ID, Codigo_Producto   from K_CORTE_PARCIAL where ID_Centro_Disp=@ID_Centro_Disp and Fg_Tipo_Centro_Disp=@Fg_Tipo_Centro_Disp and ID not in(select ID_CORTE_PARCIAL from K_RELACION_ACTA_CORTES) and ID_Estatus =@ID_Estatus ";
                SqlParameter[] paras = new SqlParameter[]{
                    new SqlParameter("@ID_Centro_Disp",Disposal),
                    new SqlParameter("@Fg_Tipo_Centro_Disp",UserType),
                    new SqlParameter("@ID_Estatus",DisposalStatus.COMPLETADO)
                };
                dtPartial = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, Sql, paras);
                return dtPartial;
            }
            catch (Exception ex)
            {
                throw new LsDAException(this, "Get  partial Act failed:Execute method  Get_PartialAct_ForFinial in K_CORTE_PARCIALDAL.", ex, true);
            }
        }
        /// <summary>
        /// approval products
        /// </summary>
        /// <param name="PartialCode"></param>
        /// <param name="RowNum"></param>
        /// <returns></returns>
        public int ApprovalProducts(string PartialCode,string RowNum)
        {
            string strWhere = "";
            try
            {
                strWhere = " WHERE 1=1 AND Codigo_Partial='" + PartialCode + "' AND ISNULL(Fg_Aprobacion,'')<>'Y'";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@rowNum",RowNum),     
                    new SqlParameter("@spWhere",strWhere)
                };
                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "UP_Update_PartialCuts_Products_Approval", para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "approval products failed: Execute method ApprovalProducts in K_CORTE_PARCIALDAL.", ex, true);
            }
        }
        /// <summary>
        /// Approve products and update its status
        /// </summary>
        /// <param name="PartialCode"></param>
        /// <param name="Estatus"></param>
        /// <returns></returns>
        public int ApprovalProductsAndStatus(string PartialCode,int Estatus)
        {
            string sql = "";
            try
            {
                sql = " UPDATE K_CORTE_PARCIAL SET ID_Estatus=" + Estatus + ",Fg_Aprobacion='Y' WHERE Codigo_Partial='" + PartialCode + "'";
                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, sql);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "approval products failed: Execute method ApprovalProducts in K_CORTE_PARCIALDAL.", ex, true);
            }
        }
    }
}
