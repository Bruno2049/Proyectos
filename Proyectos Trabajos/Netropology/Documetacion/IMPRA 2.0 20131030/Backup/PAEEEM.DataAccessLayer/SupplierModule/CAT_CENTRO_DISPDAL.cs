/* ----------------------------------------------------------------------
 * File Name: CAT_CENTRO_DISPDAL.cs
 * 
 * Create Author: Tina
 * 
 * Create DateTime: 2011/7/4
 *
 * Description:   CAT_CENTRO_DISP data access lay
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
    /// CAT_CENTRO_DISP data access lay
    /// </summary>
    public class CAT_CENTRO_DISPDAL
    {
        private static readonly CAT_CENTRO_DISPDAL _classinstance = new CAT_CENTRO_DISPDAL();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static CAT_CENTRO_DISPDAL ClassInstance { get { return _classinstance; } }
        /// <summary>
        /// Get All the TECNOLOGIA
        /// </summary>
        /// <returns></returns>
        public DataTable Get_All_CAT_CENTRO_DISPDAL(int Cve_Estatus_Centro_Disp)
        {
            try
            {
                string SQL = "SELECT Id_Centro_Disp,Cve_Estatus_Centro_Disp,Cve_Region,Dx_Razon_Social,Dx_Nombre_Comercial,Dx_RFC,Dx_Domicilio_Part_Calle,Dx_Domicilio_Part_Num," +
                                "Dx_Domicilio_Part_CP,Cve_Deleg_Municipio_Part,Cve_Estado_Part,Fg_Mismo_Domicilio,Dx_Domicilio_Fiscal_Calle,Dx_Domicilio_Fiscal_Num,Dx_Domicilio_Fiscal_CP," +
                                "Cve_Deleg_Municipio_Fisc,Cve_Estado_Fisc,Dx_Nombre_Repre,Dx_Email_Repre,Dx_Telefono_Repre,Dx_Nombre_Repre_Legal,Dx_Nombre_Banco,Dx_Cuenta_Banco," +
                                "Binary_Acta_Constitutiva,Binary_Poder_Notarial,Dt_Fecha_Centro_Disp FROM CAT_CENTRO_DISP WHERE 1=1 ";
                if (Cve_Estatus_Centro_Disp != 0)
                {
                    SQL = SQL + " AND Cve_Estatus_Centro_Disp=@Cve_Estatus_Centro_Disp";
                }
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Cve_Estatus_Centro_Disp", Cve_Estatus_Centro_Disp)
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get all Estado failed: Execute method Get_All_CAT_CENTRO_DISPDAL in CAT_CENTRO_DISP.", ex, true);
            }
        }
        // Update by Tina 2011/08/24
        // Add by Tina 2011/08/03
        /// <summary>
        /// Get All the TECNOLOGIA
        /// </summary>
        /// <returns></returns>
        public DataTable Get_CAT_CENTRO_DISPByTECHNOLOGY(int Cve_Estatus_Centro_Disp, string Cve_Tecnologia, int ID_usuario)
        {
            try
            {
                string SQL = "SELECT DISTINCT A.Id_Centro_Disp,A.Cve_Estatus_Centro_Disp,A.Cve_Region,A.Dx_Razon_Social,A.Dx_Nombre_Comercial,A.Dx_RFC,Dx_Domicilio_Part_Calle,A.Dx_Domicilio_Part_Num," +
                                "A.Dx_Domicilio_Part_CP,A.Cve_Deleg_Municipio_Part,A.Cve_Estado_Part,A.Fg_Mismo_Domicilio,A.Dx_Domicilio_Fiscal_Calle,A.Dx_Domicilio_Fiscal_Num,A.Dx_Domicilio_Fiscal_CP," +
                                "A.Cve_Deleg_Municipio_Fisc,A.Cve_Estado_Fisc,A.Dx_Nombre_Repre,A.Dx_Email_Repre,A.Dx_Telefono_Repre,A.Dx_Nombre_Repre_Legal,A.Dx_Nombre_Banco,A.Dx_Cuenta_Banco," +
                                "A.Binary_Acta_Constitutiva,A.Binary_Poder_Notarial,A.Dt_Fecha_Centro_Disp,A.Cve_Zona,A.Fg_Tipo_Centro_Disp FROM" +
                                " (" +
                                "    select (CONVERT(VARCHAR(10),Id_Centro_Disp)+'-'+'(Matriz)') as Id_Centro_Disp,Cve_Estatus_Centro_Disp,Cve_Region,Dx_Razon_Social+'(Matriz)' as Dx_Razon_Social,Dx_Nombre_Comercial,Dx_RFC,Dx_Domicilio_Part_Calle,Dx_Domicilio_Part_Num," +
                                "    Dx_Domicilio_Part_CP,Cve_Deleg_Municipio_Part,Cve_Estado_Part,Fg_Mismo_Domicilio,Dx_Domicilio_Fiscal_Calle,Dx_Domicilio_Fiscal_Num,Dx_Domicilio_Fiscal_CP," +
                                "    Cve_Deleg_Municipio_Fisc,Cve_Estado_Fisc,Dx_Nombre_Repre,Dx_Email_Repre,Dx_Telefono_Repre,Dx_Nombre_Repre_Legal,Dx_Nombre_Banco,Dx_Cuenta_Banco," +
                                "    Binary_Acta_Constitutiva,Binary_Poder_Notarial,Dt_Fecha_Centro_Disp,Cve_Zona,'M' as Fg_Tipo_Centro_Disp FROM CAT_CENTRO_DISP" +
                                " UNION" +
                                 "    select (CONVERT(VARCHAR(10),Id_Centro_Disp_Sucursal)+'-'+'(Sucursal)') as Id_Centro_Disp,Cve_Estatus_Centro_Disp,Cve_Region,Dx_Razon_Social+'(Sucursal)' as Dx_Razon_Social,Dx_Nombre_Comercial,Dx_RFC,Dx_Domicilio_Part_Calle,Dx_Domicilio_Part_Num," +
                                "    Dx_Domicilio_Part_CP,Cve_Deleg_Municipio_Part,Cve_Estado_Part,Fg_Mismo_Domicilio,Dx_Domicilio_Fiscal_Calle,Dx_Domicilio_Fiscal_Num,Dx_Domicilio_Fiscal_CP," +
                                "    Cve_Deleg_Municipio_Fisc,Cve_Estado_Fisc,Dx_Nombre_Repre,Dx_Email_Repre,Dx_Telefono_Repre,Dx_Nombre_Repre_Legal,Dx_Nombre_Banco,Dx_Cuenta_Banco," +
                                "    Binary_Acta_Constitutiva,Binary_Poder_Notarial,Dt_Fecha_Centro_Disp_Sucursal as Dt_Fecha_Centro_Disp,Cve_Zona,'B' as Fg_Tipo_Centro_Disp FROM CAT_CENTRO_DISP_SUCURSAL" +
                                " ) A" +

                                " inner join K_CENTRO_DISP_TECNOLOGIA B" +
                                " on (A.Id_Centro_Disp= (CONVERT(VARCHAR(10),B.Id_Centro_Disp)+'-'+'(Matriz)') or  A.Id_Centro_Disp= (CONVERT(VARCHAR(10),B.Id_Centro_Disp)+'-'+'(Sucursal)'))" +
                                " and A.Fg_Tipo_Centro_Disp=B.Fg_Tipo_Centro_Disp " +

                                " INNER JOIN" +
                                " (" +
                                "    SELECT US_USUARIO.Id_Usuario,K_CAT_PROVEEDOR_CAT_CENTRO_DISP.Id_Centro_Disp,/*'M' as*/ Fg_Tipo_Centro_Disp FROM US_USUARIO" +
                                "    INNER JOIN CAT_PROVEEDOR ON US_USUARIO.Id_Departamento = CAT_PROVEEDOR.Id_Proveedor AND US_USUARIO.Tipo_Usuario = 'S' " +
                                "    INNER JOIN K_CAT_PROVEEDOR_CAT_CENTRO_DISP ON CAT_PROVEEDOR.Id_Proveedor=K_CAT_PROVEEDOR_CAT_CENTRO_DISP.Id_Proveedor" +
                                "            AND K_CAT_PROVEEDOR_CAT_CENTRO_DISP.Fg_Tipo_Proveedor='M' " +
                                "    UNION" +
                                "    SELECT US_USUARIO.Id_Usuario,K_CAT_PROVEEDOR_CAT_CENTRO_DISP.Id_Centro_Disp,/*'B' as*/ Fg_Tipo_Centro_Disp FROM US_USUARIO" +
                                "    INNER JOIN CAT_PROVEEDORBRANCH ON US_USUARIO.Id_Departamento = CAT_PROVEEDORBRANCH.Id_Branch AND US_USUARIO.Tipo_Usuario = 'S_B' " +
                                "    INNER JOIN K_CAT_PROVEEDOR_CAT_CENTRO_DISP ON CAT_PROVEEDORBRANCH.Id_Branch=K_CAT_PROVEEDOR_CAT_CENTRO_DISP.Id_Proveedor" +
                                "            AND K_CAT_PROVEEDOR_CAT_CENTRO_DISP.Fg_Tipo_Proveedor='B' " +
                                " ) C" +
                                " ON C.Id_Centro_Disp= B.Id_Centro_Disp and C.Fg_Tipo_Centro_Disp=B.Fg_Tipo_Centro_Disp " +
                                " AND B.Cve_Tecnologia =" + Cve_Tecnologia +
                                " AND C.Id_Centro_Disp= substring(A.Id_Centro_Disp, 1, charindex('-', A.Id_Centro_Disp)-1) and C.Fg_Tipo_Centro_Disp=A.Fg_Tipo_Centro_Disp" +
                                " WHERE C.ID_USUARIO =@ID_Usuario"; // updated by tina 2012-02-29 // RSA 2012-10-23 user filter removed, active state filter added

                if (Cve_Estatus_Centro_Disp != 0)
                {
                    SQL = SQL + " AND Cve_Estatus_Centro_Disp=@Cve_Estatus_Centro_Disp ";
                }
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Cve_Tecnologia", Cve_Tecnologia),
                    new SqlParameter("@Cve_Estatus_Centro_Disp", Cve_Estatus_Centro_Disp),
                    new SqlParameter("@ID_Usuario", ID_usuario)

                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get all Estado failed: Execute method Get_CAT_CENTRO_DISPByTECHNOLOGY in CAT_CENTRO_DISP.", ex, true);
            }
        }
        // End
        /// <summary>
        /// Get disposal centers without any parameters
        /// </summary>
        /// <returns></returns>
        public DataTable GetDisposals()
        {
            DataTable dtResult = null;
            string SQL = "";
            try
            {
                SQL = "SELECT [Id_Centro_Disp],[Dx_Razon_Social],[Dx_Nombre_Comercial],[Cve_Zona] " +
                            "FROM [dbo].[CAT_CENTRO_DISP] WHERE Cve_Estatus_Centro_Disp = 2 ORDER BY Dx_Razon_Social asc";

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get disposals failed: Execute method GetDisposals in CAT_CENTRO_DISPDAL.", ex, true);
            }

            return dtResult;
        }
        /// <summary>
        /// Get active disposal centers without any parameters
        /// </summary>
        /// <returns></returns>
        public DataTable GetDisposalsActive()
        {
            DataTable dtResult = null;
            string SQL = "";
            try
            {
                SQL = "SELECT [Id_Centro_Disp],[Dx_Razon_Social],[Dx_Nombre_Comercial],[Cve_Zona] " +
                            "FROM [dbo].[CAT_CENTRO_DISP] WHERE Cve_Estatus_Centro_Disp = 2 ORDER BY Dx_Razon_Social asc";

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get disposals failed: Execute method GetDisposals in CAT_CENTRO_DISPDAL.", ex, true);
            }

            return dtResult;
        }
        /// <summary>
        /// Get disposal centers with zone
        /// </summary>
        /// <returns></returns>
        public DataTable GetDisposals(int zone)
        {
            DataTable dtResult = null;
            string SQL = "";
            try
            {
                SQL = "SELECT [Id_Centro_Disp],[Dx_Razon_Social],[Dx_Nombre_Comercial],[Cve_Zona] " +
                            "FROM [dbo].[CAT_CENTRO_DISP] WHERE [Cve_Zona]=" + zone + " AND [Cve_Estatus_CENTRO_DISP] = 2 ORDER BY Dx_Razon_Social asc";

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get disposals failed: Execute method GetDisposals in CAT_CENTRO_DISPDAL.", ex, true);
            }

            return dtResult;
        }
        /// <summary>
        /// Get disposal centers with regional
        /// </summary>
        /// <returns></returns>
        public DataTable GetDisposalsByRegional(int regional)
        {
            DataTable dtResult = null;
            string SQL = "";
            try
            {
                SQL = "SELECT A.Id_Centro_Disp,A.Dx_Razon_Social,A.Dx_Nombre_Comercial,A.Cve_Zona " +
                         " FROM CAT_CENTRO_DISP A WHERE A.Cve_Region = " + regional + " AND [Cve_Estatus_CENTRO_DISP] = 2 ORDER BY A.Dx_Razon_Social asc";


                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get disposals failed: Execute method GetDisposalsByRegional in CAT_CENTRO_DISPDAL.", ex, true);
            }

            return dtResult;
        }
        /// <summary>
        /// get disposal center and branch
        /// </summary>
        /// <returns></returns>
        public DataTable GetDisposalCenterAndBranch() // updated by tina 2012-02-27
        {
            DataTable dtResult = null;
            string SQL = "";
            try
            {
                SQL = "SELECT DISTINCT A.Id_Centro_Disp,A.Cve_Estatus_Centro_Disp,A.Cve_Region,A.Dx_Razon_Social,A.Dx_Nombre_Comercial,A.Dx_RFC,Dx_Domicilio_Part_Calle,A.Dx_Domicilio_Part_Num," +
                                "A.Dx_Domicilio_Part_CP,A.Cve_Deleg_Municipio_Part,A.Cve_Estado_Part,A.Fg_Mismo_Domicilio,A.Dx_Domicilio_Fiscal_Calle,A.Dx_Domicilio_Fiscal_Num,A.Dx_Domicilio_Fiscal_CP," +
                                "A.Cve_Deleg_Municipio_Fisc,A.Cve_Estado_Fisc,A.Dx_Nombre_Repre,A.Dx_Email_Repre,A.Dx_Telefono_Repre,A.Dx_Nombre_Repre_Legal,A.Dx_Nombre_Banco,A.Dx_Cuenta_Banco," +
                                "A.Binary_Acta_Constitutiva,A.Binary_Poder_Notarial,A.Dt_Fecha_Centro_Disp,A.Cve_Zona,A.Fg_Tipo_Centro_Disp FROM" +
                                " (" +
                                "    select (CONVERT(VARCHAR(10),Id_Centro_Disp)+'-'+'(Matriz)') as Id_Centro_Disp,Cve_Estatus_Centro_Disp,Cve_Region,Dx_Razon_Social+'(Matriz)' as Dx_Razon_Social,Dx_Nombre_Comercial,Dx_RFC,Dx_Domicilio_Part_Calle,Dx_Domicilio_Part_Num," +
                                "    Dx_Domicilio_Part_CP,Cve_Deleg_Municipio_Part,Cve_Estado_Part,Fg_Mismo_Domicilio,Dx_Domicilio_Fiscal_Calle,Dx_Domicilio_Fiscal_Num,Dx_Domicilio_Fiscal_CP," +
                                "    Cve_Deleg_Municipio_Fisc,Cve_Estado_Fisc,Dx_Nombre_Repre,Dx_Email_Repre,Dx_Telefono_Repre,Dx_Nombre_Repre_Legal,Dx_Nombre_Banco,Dx_Cuenta_Banco," +
                                "    Binary_Acta_Constitutiva,Binary_Poder_Notarial,Dt_Fecha_Centro_Disp,Cve_Zona,'M' as Fg_Tipo_Centro_Disp FROM CAT_CENTRO_DISP" +
                                " UNION" +
                                 "    select (CONVERT(VARCHAR(10),Id_Centro_Disp_Sucursal)+'-'+'(Sucursal)') as Id_Centro_Disp,Cve_Estatus_Centro_Disp,Cve_Region,Dx_Razon_Social+'(Sucursal)' as Dx_Razon_Social,Dx_Nombre_Comercial,Dx_RFC,Dx_Domicilio_Part_Calle,Dx_Domicilio_Part_Num," +
                                "    Dx_Domicilio_Part_CP,Cve_Deleg_Municipio_Part,Cve_Estado_Part,Fg_Mismo_Domicilio,Dx_Domicilio_Fiscal_Calle,Dx_Domicilio_Fiscal_Num,Dx_Domicilio_Fiscal_CP," +
                                "    Cve_Deleg_Municipio_Fisc,Cve_Estado_Fisc,Dx_Nombre_Repre,Dx_Email_Repre,Dx_Telefono_Repre,Dx_Nombre_Repre_Legal,Dx_Nombre_Banco,Dx_Cuenta_Banco," +
                                "    Binary_Acta_Constitutiva,Binary_Poder_Notarial,Dt_Fecha_Centro_Disp_Sucursal as Dt_Fecha_Centro_Disp,Cve_Zona,'B' as Fg_Tipo_Centro_Disp FROM CAT_CENTRO_DISP_SUCURSAL" +
                                " ) A ORDER BY A.Fg_Tipo_Centro_Disp DESC"; // updated by tina 2012-02-29

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get disposals failed: Execute method GetDisposals in CAT_CENTRO_DISPDAL.", ex, true);
            }

            return dtResult;
        }
        // added by tina 2012-02-27
        /// <summary>
        /// get disposal center by user
        /// </summary>
        /// <param name="userType"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataTable GetDisposalCenterAndBranchByUser(string userType, int userID) 
        {
            DataTable dtResult = null;
            string SQL = "";
            try
            {
                SQL = "SELECT DISTINCT A.Id_Centro_Disp,A.Cve_Estatus_Centro_Disp,A.Cve_Region,A.Dx_Razon_Social,A.Dx_Nombre_Comercial,A.Dx_RFC,Dx_Domicilio_Part_Calle,A.Dx_Domicilio_Part_Num," +
                                "A.Dx_Domicilio_Part_CP,A.Cve_Deleg_Municipio_Part,A.Cve_Estado_Part,A.Fg_Mismo_Domicilio,A.Dx_Domicilio_Fiscal_Calle,A.Dx_Domicilio_Fiscal_Num,A.Dx_Domicilio_Fiscal_CP," +
                                "A.Cve_Deleg_Municipio_Fisc,A.Cve_Estado_Fisc,A.Dx_Nombre_Repre,A.Dx_Email_Repre,A.Dx_Telefono_Repre,A.Dx_Nombre_Repre_Legal,A.Dx_Nombre_Banco,A.Dx_Cuenta_Banco," +
                                "A.Binary_Acta_Constitutiva,A.Binary_Poder_Notarial,A.Dt_Fecha_Centro_Disp,A.Cve_Zona,A.Fg_Tipo_Centro_Disp FROM" +
                                " (" +
                                "    select (CONVERT(VARCHAR(10),Id_Centro_Disp)+'-'+'(Matriz)') as Id_Centro_Disp,Cve_Estatus_Centro_Disp,Cve_Region,Dx_Razon_Social+'(Matriz)' as Dx_Razon_Social,Dx_Nombre_Comercial,Dx_RFC,Dx_Domicilio_Part_Calle,Dx_Domicilio_Part_Num," +
                                "    Dx_Domicilio_Part_CP,Cve_Deleg_Municipio_Part,Cve_Estado_Part,Fg_Mismo_Domicilio,Dx_Domicilio_Fiscal_Calle,Dx_Domicilio_Fiscal_Num,Dx_Domicilio_Fiscal_CP," +
                                "    Cve_Deleg_Municipio_Fisc,Cve_Estado_Fisc,Dx_Nombre_Repre,Dx_Email_Repre,Dx_Telefono_Repre,Dx_Nombre_Repre_Legal,Dx_Nombre_Banco,Dx_Cuenta_Banco," +
                                "    Binary_Acta_Constitutiva,Binary_Poder_Notarial,Dt_Fecha_Centro_Disp,Cve_Zona,'M' as Fg_Tipo_Centro_Disp FROM CAT_CENTRO_DISP" +
                                " UNION" +
                                 "    select (CONVERT(VARCHAR(10),Id_Centro_Disp_Sucursal)+'-'+'(Sucursal)') as Id_Centro_Disp,Cve_Estatus_Centro_Disp,Cve_Region,Dx_Razon_Social+'(Sucursal)' as Dx_Razon_Social,Dx_Nombre_Comercial,Dx_RFC,Dx_Domicilio_Part_Calle,Dx_Domicilio_Part_Num," +
                                "    Dx_Domicilio_Part_CP,Cve_Deleg_Municipio_Part,Cve_Estado_Part,Fg_Mismo_Domicilio,Dx_Domicilio_Fiscal_Calle,Dx_Domicilio_Fiscal_Num,Dx_Domicilio_Fiscal_CP," +
                                "    Cve_Deleg_Municipio_Fisc,Cve_Estado_Fisc,Dx_Nombre_Repre,Dx_Email_Repre,Dx_Telefono_Repre,Dx_Nombre_Repre_Legal,Dx_Nombre_Banco,Dx_Cuenta_Banco," +
                                "    Binary_Acta_Constitutiva,Binary_Poder_Notarial,Dt_Fecha_Centro_Disp_Sucursal as Dt_Fecha_Centro_Disp,Cve_Zona,'B' as Fg_Tipo_Centro_Disp FROM CAT_CENTRO_DISP_SUCURSAL" +
                                " ) A where 1=1"; // updated by tina 2012-02-29

                if (userType == "R" || userType == "Z")
                {
                    if (userType == "R")
                    {
                        SQL += " AND A.Cve_Region=" + userID;
                    }
                    else if (userType == "Z")
                    {
                        SQL += " AND A.Cve_Zona=" + userID;
                    }
                }

                SQL += " ORDER BY A.Fg_Tipo_Centro_Disp DESC";

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get disposals failed: Execute method GetDisposals in CAT_CENTRO_DISPDAL.", ex, true);
            }

            return dtResult;
        }
        /// <summary>
        /// Get Disposal Center name
        /// </summary>
        /// <param name="DisposalID"></param>
        /// <returns></returns>
        public string GetDisposalCenterNameByDisposalID(int DisposalID)
        {
            string DisposalName = "";
            try
            {
                string Sql = "select Dx_Razon_Social from CAT_CENTRO_DISP where Id_Centro_Disp='" + DisposalID + "'";
                object obj = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, Sql);
                if (obj != null)
                {
                    DisposalName = obj.ToString();
                }
                return DisposalName;
            }
            catch (Exception ex)
            {
                throw new LsDAException(this, "Get disposalsName failed: Execute method GetDisposalCenterNameByDisposalID in CAT_CENTRO_DISPDAL.", ex, true);
            }
        }
        /// <summary>
        /// Get main center and branches with region, zone, type, and status, by sort name, by pager
        /// </summary>
        /// <param name="regional"></param>
        /// <param name="zone"></param>
        /// <param name="type"></param>
        /// <param name="status"></param>
        /// <param name="sortName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable GetDisposalCenterAndBranchWithZoneAndStatus(int regional, string zone, string type, int status, string sortName, int pageIndex, int pageSize, out int pageCount)
        {
            DataTable dtResult = null;
            string strWhere = "";
            try
            {
                strWhere = " WHERE 1=1";

                if (regional != 0)
                {
                    strWhere += " AND Cve_Region=" + regional + "";
                }
                if (zone != "")
                {
                    strWhere += " AND Cve_Zona=" + zone + "";
                }
                if (status != 0)
                {
                    strWhere += " AND Cve_Estatus_Centro_Disp=" + status + "";
                }

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@PageCount", SqlDbType.Int),
                    new SqlParameter("@OrderBy", sortName),
                    new SqlParameter("@PageIndex", pageIndex),
                    new SqlParameter("@PageSize", pageSize),
                    new SqlParameter("@spWhere", strWhere),
                    new SqlParameter("@type",type)
               };
                paras[0].Direction = ParameterDirection.Output;

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "UP_Pager_GetView_DisposalCenters", paras);
                int.TryParse(paras[0].Value.ToString(), out pageCount);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get disposals failed: Execute method GetDisposalCenterAndBranchWithZoneAndStatus in CAT_CENTRO_DISPDAL.", ex, true);
            }

            return dtResult;
        }
        /// <summary>
        /// Get disposal centers with regional
        /// </summary>
        /// <returns></returns>
        public DataTable GetDisposalsByPk(int disposal)
        {
            DataTable dtResult = null;
            string SQL = "";
            try
            {
                SQL = "SELECT * FROM [dbo].[CAT_CENTRO_DISP] WHERE [Id_Centro_Disp]=" + disposal;

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get disposals failed: Execute method GetDisposalsByRegional in CAT_CENTRO_DISPDAL.", ex, true);
            }

            return dtResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Insert_MainDisposalCenter(CAT_CENTRO_DISPModel instance)
        {
            try
            {
                string executesqlstr = "INSERT INTO CAT_CENTRO_DISP (Cve_Estatus_Centro_Disp,Cve_Region,Dx_Razon_Social,Dx_Nombre_Comercial,Dx_RFC,Dx_Domicilio_Part_Calle,Dx_Domicilio_Part_Num," +
                                             " Dx_Domicilio_Part_CP,Cve_Deleg_Municipio_Part,Cve_Estado_Part,Fg_Mismo_Domicilio,Dx_Domicilio_Fiscal_Calle,Dx_Domicilio_Fiscal_Num,Dx_Domicilio_Fiscal_CP," +
                                             " Cve_Deleg_Municipio_Fisc,Cve_Estado_Fisc,Dx_Nombre_Repre,Dx_Email_Repre,Dx_Telefono_Repre,Dx_Nombre_Repre_Legal,Dx_Nombre_Banco,Dx_Cuenta_Banco," +
                                             " No_Empleados, Marca_Analizador_Gas, Modelo_Analizador_Gas, Serie_Analizador_Gas, Horario_Desde," +
                                             " Horario_Hasta, Dias_Semana, No_Registro_Ambiental, Tipo," +
                                             " Estatus_Registro, Telefono_Atn1, Telefono_Atn2, Dx_Ap_Paterno_Rep_Leg, Dx_Ap_Materno_Rep_Leg, Dx_Email_Repre_Legal," +
                                             " Dx_Telefono_Repre_Leg, Dx_Celular_Repre_Leg, Dx_Ap_Paterno_Repre, Dx_Ap_Materno_Repre, Dx_Celular_Repre," +
                                             " Binary_Acta_Constitutiva,Binary_Poder_Notarial,Dt_Fecha_Centro_Disp,Cve_Zona,Codigo_Centro_Disp) VALUES (@Cve_Estatus_Centro_Disp,@Cve_Region,@Dx_Razon_Social," +
                                             " @Dx_Nombre_Comercial,@Dx_RFC,@Dx_Domicilio_Part_Calle,@Dx_Domicilio_Part_Num,@Dx_Domicilio_Part_CP,@Cve_Deleg_Municipio_Part,@Cve_Estado_Part," +
                                             " @Fg_Mismo_Domicilio,@Dx_Domicilio_Fiscal_Calle,@Dx_Domicilio_Fiscal_Num,@Dx_Domicilio_Fiscal_CP,@Cve_Deleg_Municipio_Fisc,@Cve_Estado_Fisc," +
                                             " @Dx_Nombre_Repre,@Dx_Email_Repre,@Dx_Telefono_Repre,@Dx_Nombre_Repre_Legal,@Dx_Nombre_Banco,@Dx_Cuenta_Banco," +
                                             " @No_Empleados, @Marca_Analizador_Gas, @Modelo_Analizador_Gas, @Serie_Analizador_Gas, @Horario_Desde," +
                                             " @Horario_Hasta, @Dias_Semana, @No_Registro_Ambiental, @Tipo," +
                                             " @Estatus_Registro, @Telefono_Atn1, @Telefono_Atn2, @Dx_Ap_Paterno_Rep_Leg, @Dx_Ap_Materno_Rep_Leg, @Dx_Email_Repre_Legal," +
                                             " @Dx_Telefono_Repre_Leg, @Dx_Celular_Repre_Leg, @Dx_Ap_Paterno_Repre, @Dx_Ap_Materno_Repre, @Dx_Celular_Repre," +
                                             " @Binary_Acta_Constitutiva,@Binary_Poder_Notarial,@Dt_Fecha_Centro_Disp,@Cve_Zona,@Codigo_Centro_Disp)";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Cve_Estatus_Centro_Disp",instance.Cve_Estatus_Centro_Disp),
                    new SqlParameter("@Cve_Region",instance.Cve_Region),
                    new SqlParameter("@Dx_Razon_Social",instance.Dx_Razon_Social),
                    new SqlParameter("@Dx_Nombre_Comercial",instance.Dx_Nombre_Comercial),
                    new SqlParameter("@Dx_RFC",instance.Dx_RFC),
                    new SqlParameter("@Dx_Domicilio_Part_Calle",instance.Dx_Domicilio_Part_Calle),
                    new SqlParameter("@Dx_Domicilio_Part_Num",instance.Dx_Domicilio_Part_Num),
                    new SqlParameter("@Dx_Domicilio_Part_CP",instance.Dx_Domicilio_Part_CP),
                    new SqlParameter("@Cve_Deleg_Municipio_Part",instance.Cve_Deleg_Municipio_Part),
                    new SqlParameter("@Cve_Estado_Part",instance.Cve_Estado_Part),
                    new SqlParameter("@Fg_Mismo_Domicilio",instance.Fg_Mismo_Domicilio),
                    new SqlParameter("@Dx_Domicilio_Fiscal_Calle",instance.Dx_Domicilio_Fiscal_Calle),
                    new SqlParameter("@Dx_Domicilio_Fiscal_Num",instance.Dx_Domicilio_Fiscal_Num),
                    new SqlParameter("@Dx_Domicilio_Fiscal_CP",instance.Dx_Domicilio_Fiscal_CP),
                    new SqlParameter("@Cve_Deleg_Municipio_Fisc",instance.Cve_Deleg_Municipio_Fisc),
                    new SqlParameter("@Cve_Estado_Fisc",instance.Cve_Estado_Fisc),
                    new SqlParameter("@Dx_Nombre_Repre",instance.Dx_Nombre_Repre),
                    new SqlParameter("@Dx_Email_Repre",instance.Dx_Email_Repre),
                    new SqlParameter("@Dx_Telefono_Repre",instance.Dx_Telefono_Repre),
                    new SqlParameter("@Dx_Nombre_Repre_Legal",instance.Dx_Nombre_Repre_Legal),
                    new SqlParameter("@Dx_Nombre_Banco",instance.Dx_Nombre_Banco),
                    new SqlParameter("@Dx_Cuenta_Banco",instance.Dx_Cuenta_Banco),

                    new SqlParameter("@No_Empleados",instance.No_Empleados),
                    new SqlParameter("@Marca_Analizador_Gas",instance.Marca_Analizador_Gas),
                    new SqlParameter("@Modelo_Analizador_Gas",instance.Modelo_Analizador_Gas),
                    new SqlParameter("@Serie_Analizador_Gas",instance.Serie_Analizador_Gas),
                    new SqlParameter("@Horario_Desde",instance.Horario_Desde),
                    new SqlParameter("@Horario_Hasta",instance.Horario_Hasta),
                    new SqlParameter("@Dias_Semana",instance.Dias_Semana),
                    new SqlParameter("@No_Registro_Ambiental",instance.No_Registro_Ambiental),
                    new SqlParameter("@Tipo",instance.Tipo),

                    new SqlParameter("@Estatus_Registro",instance.EstatusRegistro),
                    new SqlParameter("@Telefono_Atn1",instance.TelefonoAtn1),
                    new SqlParameter("@Telefono_Atn2",instance.TelefonoAtn2),
                    new SqlParameter("@Dx_Ap_Paterno_Rep_Leg",instance.DxApPaternoRepLeg),
                    new SqlParameter("@Dx_Ap_Materno_Rep_Leg",instance.DxApMaternoRepLeg),
                    new SqlParameter("@Dx_Email_Repre_Legal",instance.DxEmailRepreLegal),
                    new SqlParameter("@Dx_Telefono_Repre_Leg",instance.DxTelefonoRepreLeg),
                    new SqlParameter("@Dx_Celular_Repre_Leg",instance.DxCelularRepreLeg),
                    new SqlParameter("@Dx_Ap_Paterno_Repre",instance.DxApPaternoRepre),
                    new SqlParameter("@Dx_Ap_Materno_Repre",instance.DxApMaternoRepre),
                    new SqlParameter("@Dx_Celular_Repre",instance.DxCelularRepre),

                    new SqlParameter("@Binary_Acta_Constitutiva",instance.Binary_Acta_Constitutiva),
                    new SqlParameter("@Binary_Poder_Notarial",instance.Binary_Poder_Notarial),
                    new SqlParameter("@Dt_Fecha_Centro_Disp",instance.Dt_Fecha_Centro_Disp),
                    new SqlParameter("@Cve_Zona",instance.Cve_Zona),
                    new SqlParameter("@Codigo_Centro_Disp",instance.Codigo_Centro_Disp)
                };
                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Add CAT_CENTRO_DISP failed: Execute method Insert_MainDisposalCenter in CAT_CENTRO_DISPDAL.", ex, true);
            }
        }
        /// <summary>
        /// Update disposal center
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Update_MainDisposalCenter(CAT_CENTRO_DISPModel instance)
        {
            try
            {
                string executesqlstr = "UPDATE CAT_CENTRO_DISP SET Cve_Region=@Cve_Region,Dx_Razon_Social=@Dx_Razon_Social,Dx_Nombre_Comercial=@Dx_Nombre_Comercial," +
                                             " Dx_RFC=@Dx_RFC,Dx_Domicilio_Part_Calle=@Dx_Domicilio_Part_Calle,Dx_Domicilio_Part_Num=@Dx_Domicilio_Part_Num,Dx_Domicilio_Part_CP=@Dx_Domicilio_Part_CP," +
                                             " Cve_Deleg_Municipio_Part=@Cve_Deleg_Municipio_Part,Cve_Estado_Part=@Cve_Estado_Part,Fg_Mismo_Domicilio=@Fg_Mismo_Domicilio," +
                                             " Dx_Domicilio_Fiscal_Calle=@Dx_Domicilio_Fiscal_Calle,Dx_Domicilio_Fiscal_Num=@Dx_Domicilio_Fiscal_Num,Dx_Domicilio_Fiscal_CP=@Dx_Domicilio_Fiscal_CP," +
                                             " Cve_Deleg_Municipio_Fisc=@Cve_Deleg_Municipio_Fisc,Cve_Estado_Fisc=@Cve_Estado_Fisc,Dx_Nombre_Repre=@Dx_Nombre_Repre,Dx_Email_Repre=@Dx_Email_Repre," +
                                             " Dx_Telefono_Repre=@Dx_Telefono_Repre,Dx_Nombre_Repre_Legal=@Dx_Nombre_Repre_Legal,Dx_Nombre_Banco=@Dx_Nombre_Banco,Dx_Cuenta_Banco=@Dx_Cuenta_Banco," +
                                             " No_Empleados=@No_Empleados, Marca_Analizador_Gas=@Marca_Analizador_Gas, Modelo_Analizador_Gas=@Modelo_Analizador_Gas, Serie_Analizador_Gas=@Serie_Analizador_Gas," +
                                             " Horario_Desde=@Horario_Desde, Horario_Hasta=@Horario_Hasta, Dias_Semana=@Dias_Semana, No_Registro_Ambiental=@No_Registro_Ambiental, Tipo=@Tipo," +
                                             " Estatus_Registro=@Estatus_Registro, Telefono_Atn1=@Telefono_Atn1, Telefono_Atn2=@Telefono_Atn2, Dx_Ap_Paterno_Rep_Leg=@Dx_Ap_Paterno_Rep_Leg, Dx_Ap_Materno_Rep_Leg=@Dx_Ap_Materno_Rep_Leg, Dx_Email_Repre_Legal=@Dx_Email_Repre_Legal, " +
                                             " Dx_Telefono_Repre_Leg=@Dx_Telefono_Repre_Leg, Dx_Celular_Repre_Leg=@Dx_Celular_Repre_Leg, Dx_Ap_Paterno_Repre=@Dx_Ap_Paterno_Repre, Dx_Ap_Materno_Repre=@Dx_Ap_Materno_Repre,Dx_Celular_Repre=@Dx_Celular_Repre," +
                                             " Binary_Acta_Constitutiva=@Binary_Acta_Constitutiva,Binary_Poder_Notarial=@Binary_Poder_Notarial,Dt_Fecha_Centro_Disp=@Dt_Fecha_Centro_Disp,Cve_Zona=@Cve_Zona" +
                                             " WHERE Id_Centro_Disp=@Id_Centro_Disp";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Cve_Region",instance.Cve_Region),
                    new SqlParameter("@Dx_Razon_Social",instance.Dx_Razon_Social),
                    new SqlParameter("@Dx_Nombre_Comercial",instance.Dx_Nombre_Comercial),
                    new SqlParameter("@Dx_RFC",instance.Dx_RFC),
                    new SqlParameter("@Dx_Domicilio_Part_Calle",instance.Dx_Domicilio_Part_Calle),
                    new SqlParameter("@Dx_Domicilio_Part_Num",instance.Dx_Domicilio_Part_Num),
                    new SqlParameter("@Dx_Domicilio_Part_CP",instance.Dx_Domicilio_Part_CP),
                    new SqlParameter("@Cve_Deleg_Municipio_Part",instance.Cve_Deleg_Municipio_Part),
                    new SqlParameter("@Cve_Estado_Part",instance.Cve_Estado_Part),
                    new SqlParameter("@Fg_Mismo_Domicilio",instance.Fg_Mismo_Domicilio),
                    new SqlParameter("@Dx_Domicilio_Fiscal_Calle",instance.Dx_Domicilio_Fiscal_Calle),
                    new SqlParameter("@Dx_Domicilio_Fiscal_Num",instance.Dx_Domicilio_Fiscal_Num),
                    new SqlParameter("@Dx_Domicilio_Fiscal_CP",instance.Dx_Domicilio_Fiscal_CP),
                    new SqlParameter("@Cve_Deleg_Municipio_Fisc",instance.Cve_Deleg_Municipio_Fisc),
                    new SqlParameter("@Cve_Estado_Fisc",instance.Cve_Estado_Fisc),
                    new SqlParameter("@Dx_Nombre_Repre",instance.Dx_Nombre_Repre),
                    new SqlParameter("@Dx_Email_Repre",instance.Dx_Email_Repre),
                    new SqlParameter("@Dx_Telefono_Repre",instance.Dx_Telefono_Repre),
                    new SqlParameter("@Dx_Nombre_Repre_Legal",instance.Dx_Nombre_Repre_Legal),
                    new SqlParameter("@Dx_Nombre_Banco",instance.Dx_Nombre_Banco),
                    new SqlParameter("@Dx_Cuenta_Banco",instance.Dx_Cuenta_Banco),

                    new SqlParameter("@No_Empleados",instance.No_Empleados),
                    new SqlParameter("@Marca_Analizador_Gas",instance.Marca_Analizador_Gas),
                    new SqlParameter("@Modelo_Analizador_Gas",instance.Modelo_Analizador_Gas),
                    new SqlParameter("@Serie_Analizador_Gas",instance.Serie_Analizador_Gas),
                    new SqlParameter("@Horario_Desde",instance.Horario_Desde),
                    new SqlParameter("@Horario_Hasta",instance.Horario_Hasta),
                    new SqlParameter("@Dias_Semana",instance.Dias_Semana),
                    new SqlParameter("@No_Registro_Ambiental",instance.No_Registro_Ambiental),
                    new SqlParameter("@Tipo",instance.Tipo),

                    new SqlParameter("@Estatus_Registro",instance.EstatusRegistro),
                    new SqlParameter("@Telefono_Atn1",instance.TelefonoAtn1),
                    new SqlParameter("@Telefono_Atn2",instance.TelefonoAtn2),
                    new SqlParameter("@Dx_Ap_Paterno_Rep_Leg",instance.DxApPaternoRepLeg),
                    new SqlParameter("@Dx_Ap_Materno_Rep_Leg",instance.DxApMaternoRepLeg),
                    new SqlParameter("@Dx_Email_Repre_Legal",instance.DxEmailRepreLegal),
                    new SqlParameter("@Dx_Telefono_Repre_Leg",instance.DxTelefonoRepreLeg),
                    new SqlParameter("@Dx_Celular_Repre_Leg",instance.DxCelularRepreLeg),
                    new SqlParameter("@Dx_Ap_Paterno_Repre",instance.DxApPaternoRepre),
                    new SqlParameter("@Dx_Ap_Materno_Repre",instance.DxApMaternoRepre),
                    new SqlParameter("@Dx_Celular_Repre",instance.DxCelularRepre),

                    new SqlParameter("@Binary_Acta_Constitutiva",instance.Binary_Acta_Constitutiva),
                    new SqlParameter("@Binary_Poder_Notarial",instance.Binary_Poder_Notarial),
                    new SqlParameter("@Dt_Fecha_Centro_Disp",instance.Dt_Fecha_Centro_Disp),
                    new SqlParameter("@Cve_Zona",instance.Cve_Zona),
                    new SqlParameter("@Id_Centro_Disp",instance.Id_Centro_Disp)
                };
                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "update disposal failed: Execute method Update_MainDisposalCenter in CAT_CENTRO_DISPDAL.", ex, true);
            }
        }
        /// <summary>
        /// Update disposal center
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Select_MainDisposalCenter_RFC(CAT_CENTRO_DISPModel instance)
        {
            int count = 0;

            try
            {
                string executesqlstr = "SELECT Count(*) from CAT_CENTRO_DISP WHERE Dx_RFC=@RFC ";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@RFC",instance.Dx_RFC)
                };
                object o = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
                if (o != null)
                    count = Convert.ToInt32(o);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "update disposal failed: Execute method Update_MainDisposalCenter in CAT_CENTRO_DISPDAL.", ex, true);
            }

            return count;
        }
        /// <summary>
        /// Update disposal center
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Select_MainDisposalCenter(CAT_CENTRO_DISPModel instance)
        {
            int count = 0;

            try
            {
                string executesqlstr = "SELECT Count(*) from CAT_CENTRO_DISP WHERE Cve_Region=@Cve_Region AND Dx_Razon_Social=@Dx_Razon_Social AND Cve_Zona=@Cve_Zona";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Cve_Region",instance.Cve_Region),
                    new SqlParameter("@Dx_Razon_Social",instance.Dx_Razon_Social),
                    new SqlParameter("@Cve_Zona",instance.Cve_Zona)
                };
                object o = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
                if (o != null)
                    count = Convert.ToInt32(o);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "update disposal failed: Execute method Update_MainDisposalCenter in CAT_CENTRO_DISPDAL.", ex, true);
            }

            return count;
        }
        /// <summary>
        /// Get Disposal Center And Branch for Authorization
        /// </summary>
        /// <param name="zone"></param>
        /// <param name="type"></param>
        /// <param name="status"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable GetDisposalAndBranchWithZoneAndStatus(string zone, string type, int status, string cayd, string regional, int pageIndex, int pageSize, out int pageCount)
        {
            DataTable dtResult = null;
            string strWhere = "";
            try
            {
                strWhere = " WHERE 1=1";
                if (regional != "")
                {
                    strWhere += " AND Cve_Region=" + regional + "";
                }
                if (zone != "")
                {
                    strWhere += " AND Cve_Zona=" + zone + "";
                }
                if (status != 0)
                {
                    strWhere += " AND Cve_Estatus_Centro_Disp=" + status + "";
                }
                if (cayd != "")
                {
                    strWhere += " AND Id_Centro_Disp=" + cayd + "";
                }

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@PageCount", SqlDbType.Int),                   
                    new SqlParameter("@PageIndex", pageIndex),
                    new SqlParameter("@PageSize", pageSize),
                    new SqlParameter("@spWhere", strWhere),
                    new SqlParameter("@type",type)
               };
                paras[0].Direction = ParameterDirection.Output;


                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "sp_DisposalCenters_Authorization", paras);
                int.TryParse(paras[0].Value.ToString(), out pageCount);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get disposals failed: Execute method GetDisposalAndBranchWithZoneAndStatus in CAT_CENTRO_DISPDAL.", ex, true);
            }

            return dtResult;
        }
        /// <summary>
        /// Active/Desactive/Cancel Disposal Center
        /// </summary>
        /// <param name="DiposalID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public int UpdateDisposalStatus(string DiposalID, int Status)
        {
            int Result = 0;
            try
            {
                string Sql = "Update CAT_CENTRO_DISP set Cve_Estatus_Centro_Disp=@Cve_Estatus_Centro_Disp where Id_Centro_Disp in(" + DiposalID + ");";

                if (Status == (int)DisposalCenterStatus.INACTIVO)
                {
                    Sql += "UPDATE US_USUARIO SET Estatus = 'I' FROM US_USUARIO AS u WITH(NOLOCK) WHERE u.Id_Departamento IN (" + DiposalID + ") AND u.Tipo_Usuario = 'D_C'";
                }
                else if (Status == (int)DisposalCenterStatus.CANCELADO)
                {
                    Sql += "UPDATE US_USUARIO SET Estatus = 'C' FROM US_USUARIO AS u WITH(NOLOCK) WHERE u.Id_Departamento IN (" + DiposalID + ") AND u.Tipo_Usuario = 'D_C'";
                }
                else if (Status == (int)DisposalCenterStatus.ACTIVO)
                {
                    Sql += "UPDATE US_USUARIO SET Estatus = 'A' FROM US_USUARIO AS u WITH(NOLOCK) WHERE u.Id_Departamento IN (" + DiposalID + ") AND u.Tipo_Usuario = 'D_C'";
                }

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Cve_Estatus_Centro_Disp", Status)
                  };
                Result = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, Sql, paras);
            }
            catch (Exception ex)
            {
                throw new LsDAException(this, "Change Branch Status failed:Execute method  UpdateDisposalStatus in CAT_CENTRO_DISPDAL.", ex, true);
            }
            return Result;
        }

        //updated by tina 2012-07-18
        public DataTable GetDisposalAndBranchCompanyName()
        {
            DataTable dtResult = null;
            string SQL = "";
            try
            {
                SQL = "SELECT Id_Centro_Disp, Dx_Nombre_Comercial FROM (" +
                    "SELECT Convert(varchar(10),Id_Centro_Disp) + '-'  + ISNULL(Dx_Nombre_Comercial,'') +'(MATRIZ)'  AS Id_Centro_Disp ,Dx_Nombre_Comercial +'(MATRIZ)'  AS Dx_Nombre_Comercial FROM CAT_CENTRO_DISP" +
                         " UNION " +
                         " SELECT Convert(varchar(10),Id_Centro_Disp_Sucursal) + '-' + ISNULL(Dx_Nombre_Comercial,'') +'(SUCURSAL)'  AS Id_Centro_Disp,Dx_Nombre_Comercial +'(SUCURSAL)'  AS Dx_Nombre_Comercial FROM CAT_CENTRO_DISP_SUCURSAL" +
                         ") f ORDER BY 2";
                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get disposal and branch company name failed: Execute method GetDisposalAndBranchCompanyName in CAT_CENTRO_DISPDAL.", ex, true);
            }
            return dtResult;
        }
        //end
    }
}
