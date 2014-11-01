/* ----------------------------------------------------------------------
 * File Name: K_CREDITO_SUSTITUCIONDAL.cs
 * 
 * Create Author: Tina
 * 
 * Create DateTime: 2011/6/27
 *
 * Description:   K_CREDITO_SUSTITUCION data access lay
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
    /// K_CREDITO_SUSTITUCION data access lay
    /// </summary>
    public class K_CREDITO_SUSTITUCIONDAL
    {
        private static readonly K_CREDITO_SUSTITUCIONDAL _classinstance = new K_CREDITO_SUSTITUCIONDAL();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static K_CREDITO_SUSTITUCIONDAL ClassInstance { get { return _classinstance; } }
        /// <summary>
        /// Add K_CREDITO_SUSTITUCION
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Insert_K_CREDITO_SUSTITUCION(K_CREDITO_SUSTITUCIONModel instance)
        {
            try
            {
                // Update by Tina 2011/08/31
                string executesqlstr = "INSERT INTO K_CREDITO_SUSTITUCION (No_Credito,Cve_Tecnologia,Id_Centro_Disp,Dt_Fecha_Credito_Sustitucion,Dx_Tipo_Producto,Dx_Modelo_Producto,Dx_Marca,No_Serial, Dx_Color, No_Peso, Cve_Capacidad_Sust, Dx_Antiguedad,Fg_Tipo_Centro_Disp,Id_Pre_Folio)" +
                                                "VALUES (@No_Credito,@Cve_Tecnologia,@Id_Centro_Disp,@Dt_Fecha_Credito_Sustitucion,@Dx_Tipo_Producto,@Dx_Modelo_Producto,@Dx_Marca,@No_Serial,@Dx_Color,@No_Peso, @Cve_Capacidad_Sust, @Dx_Antiguedad,@Fg_Tipo_Centro_Disp,@Id_Pre_Folio)";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@No_Credito",instance.No_Credito),
                    new SqlParameter("@Cve_Tecnologia",instance.Cve_Tecnologia),
                    new SqlParameter("@Id_Centro_Disp",instance.Id_Centro_Disp),
                    new SqlParameter("@Dt_Fecha_Credito_Sustitucion",instance.Dt_Fecha_Credito_Sustitucion),
                    new SqlParameter("@Dx_Tipo_Producto",instance.Dx_Tipo_Producto),
                    new SqlParameter("@Dx_Modelo_Producto",instance.Dx_Modelo_Producto),
                    new SqlParameter("@Dx_Marca",instance.Dx_Marca),
                    new SqlParameter("@No_Serial",instance.No_Serial),
                    new SqlParameter("@Dx_Color", instance.Dx_Color),
                    new SqlParameter("@No_Peso", instance.No_Peso),
                    new SqlParameter("@Cve_Capacidad_Sust", instance.Cve_Capacidad_Sust),
                    new SqlParameter("@Dx_Antiguedad",instance.Dx_Antiguedad),
                    new SqlParameter("@Fg_Tipo_Centro_Disp",instance.Fg_Tipo_Centro_Disp),
                    new SqlParameter("@Id_Pre_Folio",instance.Id_Pre_Folio)
                };
                // End
                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Add K_CREDITO_SUSTITUCION failed: Execute method Insert_K_CREDITO_SUSTITUCION in K_CREDITO_SUSTITUCION.", ex, true);
            }
        }
        /// <summary>
        /// Get Record by Credit
        /// </summary>
        /// <returns></returns>
        public DataTable Get_K_CREDITO_SUSTITUCIONByNo_Credito(string No_Credito)
        {
            try
            {
                string SQL =
                    "SELECT sust.Cve_Tecnologia as Technology, tec.cve_Esquema as Sustitucion_virtual,Id_Centro_Disp as DisposalCenter,Dx_Tipo_Producto as TypeOfProduct,Dx_Modelo_Producto as Model, " +
                    " Dx_Marca as Marca,No_Serial as SerialNumber,Id_Credito_Sustitucion as KeyNumber,Cve_Capacidad_Sust as Capacidad,Dx_Antiguedad as Antiguedad, " +
                    " Fg_Tipo_Centro_Disp as DisposalType,Dx_Color as Color,No_Peso as Peso,Id_Pre_Folio FROM K_CREDITO_SUSTITUCION " +
                    " sust join CAT_TECNOLOGIA tec on sust.Cve_Tecnologia = tec.Cve_Tecnologia " +
                    " where No_Credito=@No_Credito" +
                    " order by tec.Cve_Esquema asc";

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@No_Credito",No_Credito)
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);
                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get K_CREDITO_SUSTITUCION failed: Execute method Get_K_CREDITO_SUSTITUCIONByNo_Credito in K_CREDITO_SUSTITUCION.", ex, true);
            }
        }
        // new function for button Boleta FRR
        public DataTable Get_K_CREDITO_SUSTITUCIONByNo_CreditoFolio(string No_Credito)
        {
            try
            {
                // Update by FRR 2012-02-29
                //string SQL = "SELECT Cve_Tecnologia as Technology,No_Unidades as Unit,Id_Centro_Disp as DisposalCenter FROM K_CREDITO_SUSTITUCION where No_Credito=@No_Credito";
                string SQL = "SELECT Id_Folio FROM K_CREDITO_SUSTITUCION (nolock) where No_Credito=@No_Credito";
                // End
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@No_Credito",No_Credito)
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);
                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get K_CREDITO_SUSTITUCION failed: Execute method Get_K_CREDITO_SUSTITUCIONByNo_Credito in K_CREDITO_SUSTITUCION.", ex, true);
            }
        }






        /// <summary>
        /// Update K_CREDITO_SUSTITUCION
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Update_K_CREDITO_SUSTITUCION(K_CREDITO_SUSTITUCIONModel instance)
        {
            try
            {
                // Update by Tina 2012-02-29
                string executesqlstr = "Update K_CREDITO_SUSTITUCION  set Id_Centro_Disp=@Id_Centro_Disp,Dt_Fecha_Credito_Sustitucion=@Dt_Fecha_Credito_Sustitucion,Dx_Tipo_Producto=@Dx_Tipo_Producto, " +
                                                " Dx_Modelo_Producto=@Dx_Modelo_Producto,Dx_Marca=@Dx_Marca,No_Serial=@No_Serial,Cve_Capacidad_Sust=@Cve_Capacidad_Sust,Dx_Antiguedad=@Dx_Antiguedad,Fg_Tipo_Centro_Disp=@Fg_Tipo_Centro_Disp,Dx_Color=@Dx_Color,No_Peso=@No_Peso" +
                                                " where Id_Pre_Folio=@Id_Pre_Folio";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Id_Centro_Disp",instance.Id_Centro_Disp),
                    new SqlParameter("@Dt_Fecha_Credito_Sustitucion",instance.Dt_Fecha_Credito_Sustitucion),
                    new SqlParameter("@Dx_Tipo_Producto",instance.Dx_Tipo_Producto),
                    new SqlParameter("@Dx_Modelo_Producto",instance.Dx_Modelo_Producto),
                    new SqlParameter("@Dx_Marca",instance.Dx_Marca),
                    new SqlParameter("@No_Serial",instance.No_Serial),
                    new SqlParameter("@Cve_Capacidad_Sust",instance.Cve_Capacidad_Sust),
                    new SqlParameter("@Dx_Antiguedad",instance.Dx_Antiguedad),
                    new SqlParameter("@Fg_Tipo_Centro_Disp",instance.Fg_Tipo_Centro_Disp),
                    new SqlParameter("@Id_Pre_Folio",instance.Id_Pre_Folio),
                    new SqlParameter("@Dx_Color",instance.Dx_Color),
                    new SqlParameter("@No_Peso",instance.No_Peso)
                };
                // End
                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Update K_CREDITO_SUSTITUCION failed: Execute method Update_K_CREDITO_SUSTITUCION in K_CREDITO_SUSTITUCION.", ex, true);
            }
        }
        // Added by Tina 2011/08/23
        /// <summary>
        /// get records by credit and technology
        /// </summary>
        /// <param name="No_Credito"></param>
        /// <param name="Technology"></param>
        /// <returns></returns>
        public DataTable Get_K_CREDITO_SUSTITUCIONByCreditoAndTechnology(string No_Credito, int Technology)
        {
            try
            {
                string SQL = "SELECT * FROM K_CREDITO_SUSTITUCION where No_Credito=@No_Credito and Cve_Tecnologia=@Cve_Tecnologia";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@No_Credito",No_Credito),
                    new SqlParameter("@Cve_Tecnologia",Technology)
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);
                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get K_CREDITO_SUSTITUCION failed: Execute method Get_K_CREDITO_SUSTITUCIONByCreditoAndTechnology in K_CREDITO_SUSTITUCION.", ex, true);
            }
        }
        // End
        // Added by Tina 2011/09/01
        /// <summary>
        /// delete records by credit
        /// </summary>
        /// <param name="Credit"></param>
        /// <returns></returns>
        public int Delete_K_CREDITO_SUSTITUCIONByCredit(string Credit)
        {
            try
            {
                string executesqlstr = "delete from K_CREDITO_SUSTITUCION where No_Credito=@No_Credito";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@No_Credito",Credit)
                };
                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Delete K_CREDITO_SUSTITUCION failed: Execute method Delete_K_CREDITO_SUSTITUCIONByCredit in K_CREDITO_SUSTITUCION.", ex, true);
            }
        }
        // End
        /// <summary>
        /// Get receipt date
        /// </summary>
        public DataTable GetReceiptDate()
        {
            DataTable dtResult = null;
            string SQL = "";
            try
            {
                SQL = "SELECT DISTINCT CONVERT(VARCHAR(10),Dt_Fecha_Recepcion, 120) AS Dt_Fecha_Recepcion FROM [dbo].[K_CREDITO_SUSTITUCION]  WHERE [Dt_Fecha_Recepcion] IS NOT NULL ";
                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get receipt date failed:Execute method  GetReceiptDate in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
            return dtResult;
        }

        /// <summary>
        /// get products by disposal,receipt date and technology
        /// </summary>
        /// <param name="Disposal"></param>
        /// <param name="DisposalType"></param>
        /// <param name="ReceiptDate"></param>
        /// <param name="Technology"></param>
        /// <param name="Estatus"></param>
        /// <param name="sortName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable GetDisposedProducts(int Disposal, string DisposalType, string ReceiptDate, string Technology, int Estatus, string sortName, int pageIndex, int pageSize, out int pageCount)
        {
            DataTable dtResult = null;
            string strWhere = "";
            try
            {
                strWhere = " WHERE Id_Centro_Disp=" + Disposal + " AND Fg_Tipo_Centro_Disp='" + DisposalType + "' AND Dt_Fecha_Recepcion IS NOT NULL";
                if (ReceiptDate != "")
                {
                    strWhere += " AND CONVERT(VARCHAR(10),Dt_Fecha_Recepcion, 120)='" + ReceiptDate + "'";
                }
                if (Technology != "")
                {
                    strWhere += " AND Cve_Tecnologia IN (" + Technology + ")";
                }
                if (Estatus == (int)DisposalStatus.ENRECEPCION)
                {
                    strWhere += " AND Dt_Fecha_Inhabilitacion IS NULL AND Dt_Fecha_Recuperacion IS NULL AND ISNULL(Fg_Conformidad,0) <>1";
                }
                else if (Estatus == (int)DisposalStatus.PARAINHABILITACION)
                {
                    strWhere += " AND Fg_Conformidad=1 AND Dt_Fecha_Inhabilitacion IS NULL AND Dt_Fecha_Recuperacion IS NULL";
                }
                else if (Estatus == (int)DisposalStatus.RECUPERACIONDERESIDUOS)
                {
                    strWhere += " AND Dt_Fecha_Recuperacion IS NOT NULL AND Dt_Fecha_Inhabilitacion IS NULL AND ISNULL(Cod_Producto,'') IN (SELECT DISTINCT Codigo_Producto FROM K_RECUP_RESIDUOS WHERE ISNULL(Fg_Incluido,'')<>'Y')";
                }
                else if (Estatus == (int)DisposalStatus.INHABILITADO)
                {
                    strWhere += " AND Dt_Fecha_Inhabilitacion IS NOT NULL AND ISNULL(Cod_Producto,'') IN (SELECT DISTINCT Codigo_Producto FROM K_CORTE_PARCIAL WHERE ISNULL(Fg_Aprobacion,'')<>'Y')";
                }
                else if (Estatus == 0)
                {
                    strWhere += " AND ISNULL(Cod_Producto,'') NOT IN (SELECT DISTINCT Codigo_Producto FROM K_CORTE_PARCIAL WHERE ISNULL(Fg_Aprobacion,'')='Y')";
                }

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@PageCount", SqlDbType.Int),
                    new SqlParameter("@OrderBy", sortName),
                    new SqlParameter("@PageIndex", pageIndex),
                    new SqlParameter("@PageSize", pageSize),
                    new SqlParameter("@spWhere", strWhere)
                };
                paras[0].Direction = ParameterDirection.Output;

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "UP_Pager_GetView_DisposedProducts", paras);
                int.TryParse(paras[0].Value.ToString(), out pageCount);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Get Disposed Products failed:Execute method  GetDisposedProducts in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
            return dtResult;
        }

        //add by coco 2011-10-10
        /// <summary>
        /// Get K_CREDITO_SUSTITUCION By Barcode
        /// </summary>
        /// <param name="BarCode"></param>
        /// <returns></returns>
        public int GetProductCountByCreditBarCode(string BarCode)
        {
            int Result = 0;
            string SQL = "";
            try
            {
                SQL = "select Count(*)  from K_CREDITO_SUSTITUCION  A inner join CRE_Credito B";
                SQL = SQL + "  on A.No_Credito=B.No_Credito and B.Barcode_Solicitud=@Barcode_Solicitud where A.Dt_Fecha_Recepcion is not null";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Barcode_Solicitud",BarCode)
                };
                object o = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);
                if (o != null)
                {
                    Result = int.Parse(o.ToString());
                }
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get receipt date failed:Execute method  GetProductByCreditBarCode in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
            return Result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="BarCode"></param>
        /// <returns></returns>
        public DataTable GetProductByCreditBarCode(string BarCode)
        {
            DataTable dtResult = null;
            string SQL = "";
            try
            {
                SQL = "select A.No_Credito, A.Cve_Tecnologia,C.Dx_Nombre_General,A.Dx_Tipo_Producto,A.Dx_Modelo_Producto,A.Dx_Marca,A.No_Serial,A.Dx_Color,A.Dx_Peso,A.Ft_Capacidad,A.Dx_Antiguedad  ";
                SQL = SQL + "  from K_CREDITO_SUSTITUCION  A inner join CRE_Credito B ";
                SQL = SQL + " on A.No_Credito=B.No_Credito and B.Barcode_Solicitud=@Barcode_Solicitud";
                SQL = SQL + " left join CAT_TECNOLOGIA C on A.Cve_Tecnologia=C.Cve_Tecnologia";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Barcode_Solicitud",BarCode)
                };
                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);

            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get receipt date failed:Execute method  GetProductByCreditBarCode in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
            return dtResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="barcode"></param>
        /// <param name="ReceiptDate"></param>
        /// <returns></returns>
        public int UpdateK_CREDITO_SUSTITUCION(string barcode, DateTime ReceiptDate)
        {
            try
            {
                // Update by Tina 2011/08/31
                string executesqlstr = "update K_CREDITO_SUSTITUCION set Dt_Fecha_Recepcion =@Dt_Fecha_Recepcion where No_Credito in  " +
                                                "(select A.No_Credito from K_CREDITO_SUSTITUCION A inner join CRE_Credito B on A.No_Credito=B.No_Credito and B.Barcode_Solicitud=@Barcode_Solicitud)";

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Barcode_Solicitud",barcode),
                    new SqlParameter("@Dt_Fecha_Recepcion",ReceiptDate)               
                };
                // End
                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Update K_CREDITO_SUSTITUCION failed: Execute method UpdateK_CREDITO_SUSTITUCION in K_CREDITO_SUSTITUCION.", ex, true);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="BarCode"></param>
        /// <param name="sortName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable Get_K_CREDITO_SUSTITUCIONForDisposal(string BarCode, string sortName, int pageIndex, int pageSize, out int pageCount)
        {
            DataTable dtResult = null;
            try
            {
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Count", SqlDbType.Int),
                    new SqlParameter("@BarCode", BarCode),
                    new SqlParameter("@OrderBy", sortName),
                    new SqlParameter("@CurrentPageIndex", pageIndex),
                    new SqlParameter("@PageSize", pageSize)
                };
                paras[0].Direction = ParameterDirection.Output;
                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "sp_get_ProductList", paras);
                int.TryParse(paras[0].Value.ToString(), out pageCount);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get ProductList failed: Execute method sp_get_ProductList in K_CREDITO_SUSTITUCIONDAL .", ex, true);
            }

            return dtResult;
        }
        /// <summary>
        ///Check if the credit request is related with a specific disposal center
        /// </summary>
        /// <param name="barcode">Barcode String</param>
        /// <param name="disposalCenterNumber">Disposal Center Number</param>
        /// <param name="disposalCenterType">Disposal Center Type</param>
        /// <returns>Record Count</returns>
        public int GetRecordCount(string barcode, int disposalCenterNumber, string disposalCenterType)
        {
            int Result = 0;
            string SQL = "";

            try
            {
                SQL = " select  Count(*) from K_CREDITO_SUSTITUCION where No_Credito = @No_Credito and Id_Centro_Disp=@Id_Centro_Disp and Fg_Tipo_Centro_Disp=@Fg_Tipo_Centro_Disp";

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@No_Credito", barcode),
                    new SqlParameter("@Id_Centro_Disp", disposalCenterNumber),
                    new SqlParameter("@Fg_Tipo_Centro_Disp", disposalCenterType)
                };

                object o = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);

                if (o != null)
                {
                    Result = int.Parse(o.ToString());
                }
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get_K_Credit_Sustitucion Count By Disposal failed:Execute method  Get_K_Credit_SustitucionCountByDisposal in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }

            return Result;
        }
        /// <summary>
        /// Update credit substitution old product to inhabilitación
        /// </summary>
        /// <param name="NoCredit"></param>
        /// <param name="ProductCode"></param>
        /// <param name="Flag"></param>
        /// <param name="Technology"></param>
        /// <returns></returns>
        public int Update_K_Credit_SustitucionToParaInhabilitación(string NoCredit, string ProductCode, int Flag, string Technology)
        {
            try
            {
                string executesqlstr = "Update K_CREDITO_SUSTITUCION set Cod_Producto=@Cod_Producto ,Fg_Conformidad=@Fg_Conformidad where No_Credito=@No_Credito and Cve_Tecnologia=@Cve_Tecnologia";

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Cod_Producto",ProductCode),     
                    new SqlParameter("@Fg_Conformidad",Flag),
                    new SqlParameter("@No_Credito",NoCredit),
                    new SqlParameter("@Cve_Tecnologia",Technology)
                };
                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Update K_CREDITO_SUSTITUCION failed: Execute method UpdateK_CREDITO_SUSTITUCIONEstatusToInhabilitado in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
        }
        ///// <summary>
        ///// update Product information
        ///// </summary>
        ///// <param name="instance"></param>
        ///// <returns></returns>
        //public int Update_K_CREDITO_SUSTITUCIONProductInformation(K_CREDITO_SUSTITUCIONModel instance)
        //{
        //    try
        //    {
        //        // Update by Tina 2011/08/31
        //        string executesqlstr = "Update K_CREDITO_SUSTITUCION  set Dx_Modelo_Producto=@Dx_Modelo_Producto,Dx_Marca=@Dx_Marca," +
        //                                        "No_Serial=@No_Serial,Dx_Color=@Dx_Color,Dx_Peso=@Dx_Peso,Ft_Capacidad=@Ft_Capacidad,Dx_Antiguedad=@Dx_Antiguedad" +
        //                                        " where No_Credito=@No_Credito and Cve_Tecnologia=@Cve_Tecnologia";
        //        SqlParameter[] para = new SqlParameter[] {                   
        //            new SqlParameter("@Dx_Modelo_Producto",instance.Dx_Modelo_Producto),
        //            new SqlParameter("@Dx_Marca",instance.Dx_Marca),
        //            new SqlParameter("@No_Serial",instance.No_Serial),
        //            new SqlParameter("@Dx_Color",instance.Dx_Color),
        //            new SqlParameter("@Dx_Peso",instance.Dx_Peso),
        //            new SqlParameter("@Ft_Capacidad",instance.Ft_Capacidad),
        //            new SqlParameter("@Dx_Antiguedad",instance.Dx_Antiguedad),                   
        //            new SqlParameter("@No_Credito",instance.No_Credito),
        //            new SqlParameter("@Cve_Tecnologia",instance.Cve_Tecnologia)
        //        };
        //        // End
        //        return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw new LsDAException(this, "Update K_CREDITO_SUSTITUCION Product Information failed: Execute method Update_K_CREDITO_SUSTITUCIONProductInformation in K_CREDITO_SUSTITUCION.", ex, true);
        //    }
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No_Credito"></param>
        /// <param name="Technology"></param>
        /// <returns></returns>
        public DataTable Get_All_K_CREDITO_SUSTITUCIONByCreditoAndTechnology(string No_Credito, int Technology)
        {
            try
            {
                string SQL = "SELECT  A.*,B.Dx_Nombre_General from K_CREDITO_SUSTITUCION A left join CAT_TECNOLOGIA B on A.Cve_Tecnologia=B.Cve_Tecnologia  where A.No_Credito=@No_Credito and A.Cve_Tecnologia=@Cve_Tecnologia";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@No_Credito",No_Credito),
                    new SqlParameter("@Cve_Tecnologia",Technology)
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);
                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get K_CREDITO_SUSTITUCION failed: Execute method Get_K_CREDITO_SUSTITUCIONByCreditoAndTechnology in K_CREDITO_SUSTITUCION.", ex, true);
            }
        }
        ///// <summary>
        ///// Upload Product Image
        ///// </summary>
        ///// <param name="instance"></param>
        ///// <returns></returns>
        //public int UpLoadProductImage(K_CREDITO_SUSTITUCIONModel instance)
        //{
        //    try
        //    {
        //        string executesqlstr = "Update K_CREDITO_SUSTITUCION set Image1=@Image1 ,Image2=@Image2,Image3=@Image3 where No_Credito=@No_Credito and Cve_Tecnologia=@Cve_Tecnologia";

        //        SqlParameter[] para = new SqlParameter[] { 
        //            new SqlParameter("@Image1",instance.Image1),     
        //            new SqlParameter("@Image2",instance.Image2),
        //            new SqlParameter("@Image3",instance.Image3),
        //            new SqlParameter("@No_Credito",instance.No_Credito),
        //            new SqlParameter("@Cve_Tecnologia",instance.Cve_Tecnologia)
        //        };
        //        return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw new LsDAException(this, "Update K_CREDITO_SUSTITUCION failed: Execute method UpdateK_CREDITO_SUSTITUCIONEstatusToInhabilitado in K_CREDITO_SUSTITUCIONDAL.", ex, true);
        //    }
        //}
        ///// <summary>
        ///// update Product information
        ///// </summary>
        ///// <param name="instance"></param>
        ///// <returns></returns>
        //public int Update_Product_Peso(K_CREDITO_SUSTITUCIONModel instance)
        //{
        //    try
        //    {
        //        string executesqlstr = "Update K_CREDITO_SUSTITUCION  set Peso_Producto=@Peso_Producto  where No_Credito=@No_Credito and Cve_Tecnologia=@Cve_Tecnologia";
        //        SqlParameter[] para = new SqlParameter[] {                   
        //            new SqlParameter("@Peso_Producto",instance.Peso_Producto),                              
        //            new SqlParameter("@No_Credito",instance.No_Credito),
        //            new SqlParameter("@Cve_Tecnologia",instance.Cve_Tecnologia)
        //        };               
        //        return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw new LsDAException(this, "   Update_Product_Peso failed: Execute method Update_Product_Peso in K_CREDITO_SUSTITUCION.", ex, true);
        //    }
        //}
        //end add
        /// <summary>
        /// change status to Inhabilitado
        /// </summary>
        /// <param name="Credit"></param>
        /// <param name="Technology"></param>
        /// <param name="InhabilitadoDate"></param>
        /// <returns></returns>
        public int UpdateK_CREDITO_SUSTITUCIONEstatusToInhabilitado(string Credit, int Technology, DateTime InhabilitadoDate)
        {
            try
            {
                string executesqlstr = "update K_CREDITO_SUSTITUCION set Dt_Fecha_Inhabilitacion =@Dt_Fecha_Inhabilitacion where No_Credito=@Credit and Cve_Tecnologia=@Technology ";

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Dt_Fecha_Inhabilitacion",InhabilitadoDate),     
                    new SqlParameter("@Credit",Credit),
                    new SqlParameter("@Technology",Technology)
                };
                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Update K_CREDITO_SUSTITUCION failed: Execute method UpdateK_CREDITO_SUSTITUCIONEstatusToInhabilitado in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
        }
        /// <summary>
        /// change status to RECUPERACION DE RESIDUOS
        /// </summary>
        /// <param name="Credit"></param>
        /// <param name="Technology"></param>
        /// <param name="RecuperacionDate"></param>
        /// <returns></returns>
        public int UpdateK_CREDITO_SUSTITUCIONEstatusToRecuperacion(string Credit, int Technology, DateTime RecuperacionDate)
        {
            try
            {
                string executesqlstr = "update K_CREDITO_SUSTITUCION set Dt_Fecha_Recuperacion =@Dt_Fecha_Recuperacion where No_Credito=@Credit and Cve_Tecnologia=@Technology ";

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Dt_Fecha_Recuperacion",RecuperacionDate),     
                    new SqlParameter("@Credit",Credit),
                    new SqlParameter("@Technology",Technology)
                };
                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Update K_CREDITO_SUSTITUCION failed: Execute method UpdateK_CREDITO_SUSTITUCIONEstatusToRecuperacion in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
        }
        /// <summary>
        /// get complete products
        /// </summary>
        /// <param name="Disposal"></param>
        /// <param name="DisposalType"></param>
        /// <param name="sortName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable GetCompleteProducts(int Disposal, string DisposalType, string sortName, int pageIndex, int pageSize, out int pageCount)
        {
            DataTable dtResult = null;
            string strWhere = "";
            try
            {
                strWhere = " INNER JOIN (SELECT DISTINCT No_Credito,Codigo_Producto FROM K_RECUP_RESIDUOS) B ON A.No_Credito=B.No_Credito AND A.Cod_Producto=B.Codigo_Producto ";

                strWhere += " WHERE A.Id_Centro_Disp=" + Disposal + " AND A.Fg_Tipo_Centro_Disp='" + DisposalType + "' AND A.Dt_Fecha_Recuperacion IS NOT NULL AND Dt_Fecha_Inhabilitacion IS NULL ";

                strWhere += " AND A.Cod_Producto IN (SELECT Codigo_Producto FROM K_RECUP_RESIDUOS WHERE ISNULL(Fg_Incluido,'')<>'Y')";

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@PageCount", SqlDbType.Int),
                    new SqlParameter("@OrderBy", sortName),
                    new SqlParameter("@PageIndex", pageIndex),
                    new SqlParameter("@PageSize", pageSize),
                    new SqlParameter("@spWhere", strWhere)
                };
                paras[0].Direction = ParameterDirection.Output;

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "UP_Pager_GetView_CompleteProducts", paras);
                int.TryParse(paras[0].Value.ToString(), out pageCount);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Get Disposed Products failed:Execute method  GetDisposedProducts in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
            return dtResult;
        }
        /// <summary>
        /// Get received old products by the scanned barcode
        /// </summary>
        /// <param name="barCode">barcode string</param>
        /// <param name="disposalCenterNumber">disposal center number</param>
        /// <param name="disposalCenterType">disposal center type</param>
        /// <param name="pageIndex">page index</param>
        /// <param name="pageSize">page size</param>
        /// <param name="pageCount">page count</param>
        /// <returns>old products list</returns>
        public DataTable GetOldEquipmentsReceivedList(string barCode, string disposalCenterNumber, string disposalCenterType, int pageIndex, int pageSize, out int pageCount)
        {
            DataTable oldProducts = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@PageCount", SqlDbType.Int),
                    new SqlParameter("@BarCode", barCode),
                    new SqlParameter("@DisposalID", disposalCenterNumber),
                    new SqlParameter("@FgDisposal",disposalCenterType),
                    new SqlParameter("@PageIndex", pageIndex),
                    new SqlParameter("@PageSize", pageSize)
                };
                paras[0].Direction = ParameterDirection.Output;

                oldProducts = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "sp_get_Old_Equipment", paras);
                int.TryParse(paras[0].Value.ToString(), out pageCount);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get ProductList failed: Execute method [sp_get_Old_Equipment] in K_CREDITO_SUSTITUCIONDAL .", ex, true);
            }

            return oldProducts;
        }
        ///// <summary>
        /////  Get received old product internal codes related with the specific credit
        ///// </summary>
        ///// <param name="credit">credit request</param>
        ///// <returns></returns>
        //public DataTable GetReceivedOldProductsInternalCode(string credit)
        //{
        //    DataTable TableResult = null;

        //    try
        //    {
        //        string SQL = "select distinct A.Id_Folio from K_CREDITO_SUSTITUCION A inner join K_CREDITO B on A.No_Credito=B.No_Credito where A.Id_Folio IS NOT NULL";
        //        if (credit != "")
        //        {
        //            SQL += " and A.No_Credito='" + credit + "'";
        //        }

        //        TableResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw new LsDAException(this, "Get internal code failed: Execute method GetInternalCodeByCredit in K_CREDITO_SUSTITUCIONDAL.", ex, true);
        //    }

        //    return TableResult;
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="credit"></param>
        /// <param name="Supplier"></param>
        /// <param name="SupplierType"></param>
        /// <param name="Technology"></param>
        /// <param name="Program"></param>
        /// <returns></returns>
        public DataTable GetReceivedOldProductsInternalCodeByFilter(string credit, string Supplier, string SupplierType, string Technology, string Program, string DisposalID, string DisposalType)
        {
            DataTable TableResult = null;
            try
            {
                string SQL = "select distinct A.Id_Folio from K_CREDITO_SUSTITUCION A inner join CRE_CREDITO B on A.No_Credito=B.No_Credito";
                if (Supplier != "")
                {
                    SQL = SQL + " and B.Id_Proveedor=@Id_Proveedor and B.Tipo_Usuario=@Tipo_Usuario";
                }
                if (Program != "")
                {
                    SQL = SQL + " and B.ID_Prog_Proy=@ID_Prog_Proy";
                }
                SQL = SQL + " where A.Id_Folio IS NOT NULL and A.Id_Centro_Disp=@Id_Centro_Disp and A.Fg_Tipo_Centro_Disp=@Fg_Tipo_Centro_Disp";
                if (credit != "")
                {
                    SQL += " and A.No_Credito=@No_Credito";
                }
                if (Technology != "")
                {
                    SQL = SQL + "  and A.Cve_Tecnologia=@Cve_Tecnologia";
                }
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Id_Proveedor",Supplier),
                    new SqlParameter("@Tipo_Usuario", SupplierType),
                    new SqlParameter("@ID_Prog_Proy", Program),
                    new SqlParameter("@No_Credito",credit),
                    new SqlParameter("@Cve_Tecnologia", Technology),
                    new SqlParameter("@Id_Centro_Disp",DisposalID),
                    new SqlParameter("@Fg_Tipo_Centro_Disp", DisposalType)   
                };
                TableResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get internal code failed: Execute method GetReceivedOldProductsInternalCodeByFilter in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }

            return TableResult;
        }
        /// <summary>
        /// Get received products
        /// </summary>
        /// <param name="program">program</param>
        /// <param name="credit">credit</param>
        /// <param name="internalCode">old product code</param>
        /// <param name="supplierId">supplier</param>
        /// <param name="supplierType">supplier type: main or branch</param>
        /// <param name="type">type</param>
        /// <param name="technology">technology</param>
        /// <param name="fromDate">from date</param>
        /// <param name="toDate">to date</param>
        /// <param name="disposalCenterId">disposal center</param>
        /// <param name="disposalCenterType">disposal center type</param>
        /// <param name="sortName">sort name</param>
        /// <param name="pageIndex">page index</param>
        /// <param name="pageSize">page size</param>
        /// <param name="pageCount">page count</param>
        /// <returns></returns>
        public DataTable GetReceivedProducts(string program, string credit, string internalCode, string supplierId, string supplierType, string type, string technology, string fromDate, string toDate, int disposalCenterId, string disposalCenterType, string sortName, int pageIndex, int pageSize, out int pageCount)
        {
            DataTable TableResult = null;
            string strWhere = " WHERE 1=1 AND Id_Folio IS NOT NULL AND Dt_Fecha_Recepcion IS NOT NULL AND ISNULL(Fg_Si_Funciona,0)=1";

            try
            {
                if (program != "")
                {
                    strWhere += " AND ID_Prog_Proy=" + program;
                }
                if (credit != "")
                {
                    strWhere += " AND No_Credito='" + credit + "'";
                }
                if (internalCode != "")
                {
                    strWhere += " AND Id_Folio='" + internalCode + "'";
                }
                if (supplierId != "")
                {
                    strWhere += " AND Id_Proveedor=" + supplierId + " AND Tipo_Usuario='" + supplierType + "'";
                }
                if (type != "")
                {
                    if (type == "Inhabilitados")
                    {
                        strWhere += " AND Id_Credito_Sustitucion IN (SELECT Id_Credito_Sustitucion FROM K_INHABILITACION_PRODUCTO)";
                    }
                    else
                    {
                        strWhere += " AND Id_Credito_Sustitucion NOT IN (SELECT Id_Credito_Sustitucion FROM K_INHABILITACION_PRODUCTO)";
                    }
                }
                if (technology != "")
                {
                    strWhere += " AND Cve_Tecnologia=" + technology;
                }
                if (fromDate != "")
                {
                    strWhere += " AND CONVERT(VARCHAR(10),Dt_Fecha_Recepcion, 120)>='" + fromDate + "'";
                }
                if (toDate != "")
                {
                    strWhere += " AND CONVERT(VARCHAR(10),Dt_Fecha_Recepcion, 120)<='" + toDate + "'";
                }
                if (disposalCenterId != 0)
                {
                    strWhere += " AND Id_Centro_Disp=" + disposalCenterId;
                }
                if (disposalCenterType != "")
                {
                    strWhere += " AND Fg_Tipo_Centro_Disp='" + disposalCenterType + "'";
                }

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@PageCount", SqlDbType.Int),
                    new SqlParameter("@OrderBy", sortName),
                    new SqlParameter("@PageIndex", pageIndex),
                    new SqlParameter("@PageSize", pageSize),
                    new SqlParameter("@spWhere", strWhere)
                };
                paras[0].Direction = ParameterDirection.Output;

                TableResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "UP_Pager_GetView_ReceivedProducts", paras);
                int.TryParse(paras[0].Value.ToString(), out pageCount);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Received Products failed:Execute method  GetReceivedProducts in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }

            return TableResult;
        }
        /// <summary>
        /// check the Old equipment is reception
        /// </summary>
        /// <param name="CreditSusID"></param>
        /// <returns></returns>
        public int IsOldProductReceived(string CreditSusID)
        {
            int Result = 0;
            string SQL = "";

            try
            {
                SQL = "select COUNT(*) from K_CREDITO_SUSTITUCION where Dt_Fecha_Recepcion is not null and Id_Credito_Sustitucion=@Id_Credito_Sustitucion";

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Id_Credito_Sustitucion",CreditSusID)
                };

                object o = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);

                if (o != null)
                {
                    Result = int.Parse(o.ToString());
                }
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get receipt date failed:Execute method  IsReceiptionOldEquipment in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
            return Result;
        }
        /// <summary>
        /// check the Old equipment is reception
        /// </summary>
        /// <param name="CreditSusID"></param>
        /// <returns></returns>
        public bool IsOldProductCharactersRequired(string CreditNum)
        {
            bool Result = true;
            string SQL = "";

            try
            {
                // SQL = "select COUNT(*) from K_CREDITO_SUSTITUCION (nolock) s join K_PRODUCTO_CHARACTERS (nolock) c on s.Id_Credito_Sustitucion = c.Id_Credito_Sustitucion where No_Credito=@No_Credito";
                //SQL = "select COUNT(c.Id_Credito_Sustitucion) from K_CREDITO_SUSTITUCION (nolock) s left join K_PRODUCTO_CHARACTERS (nolock) c on s.Id_Credito_Sustitucion = c.Id_Credito_Sustitucion where No_Credito=@No_Credito group by No_Credito";
                SQL =
                    "SELECT Count(c.id_credito_sustitucion) FROM k_credito_sustitucion (nolock) s INNER JOIN cat_tecnologia t ON s.cve_tecnologia = t.cve_tecnologia  LEFT JOIN k_producto_characters (nolock) c ON s.id_credito_sustitucion = c.id_credito_sustitucion WHERE  no_credito = @No_Credito AND t.cve_esquema != 1 GROUP  BY no_credito ";

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@No_Credito",CreditNum)
                };

                object o = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);

                if (o != null)
                {
                    int count = int.Parse(o.ToString());
                    // Old Product Characters are Required as long as none is delivered
                    Result = (count == 0);
                }
                else
                {
                    // if missing is not a of sustitución, no Old Product Characters are Required
                    Result = false;
                }
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get receipt date failed:Execute method  IsReceiptionOldEquipment in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
            return Result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pkId"></param>
        /// <param name="DisposalID"></param>
        /// <param name="DisposalFg"></param>
        /// <returns></returns>
        public DataTable GetOldEquipmentByID(string pkId, string DisposalFg)
        {
            DataTable dtResult = null;

            string sql = "select Id_Pre_Folio,ProviderName,ProviderComercialName,A.Dx_Razon_Social,Dx_Nombre_Programa,Dx_Nombre_General, ";
            sql = sql + "Dx_Estatus_Credito,C.Dx_Nombre_Region,D.Dx_Nombre_Zona,Dx_Marca,Cve_Capacidad_Sust,Dx_Color,Dx_Antiguedad,Fg_Si_Funciona,No_Peso,No_Serial,Cve_Tecnologia";
            sql = sql + " from dbo.View_Old_Equipment_List A ";
            if (DisposalFg == "M")
            {
                sql = sql + " left join CAT_CENTRO_DISP B on A.Id_Centro_Disp=B.Id_Centro_Disp ";
            }
            else
            {
                sql = sql + " left join CAT_CENTRO_DISP_SUCURSAL B on A.Id_Centro_Disp=B.Id_Centro_Disp_Sucursal ";
            }
            sql = sql + " left join CAT_REGION C on B.Cve_Region=C.Cve_Region  left join CAT_ZONA D on B.Cve_Zona=D.Cve_Zona where Id_Credito_Sustitucion =@Id_Credito_Sustitucion";
            SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Id_Credito_Sustitucion",pkId)                    
                };
            try
            {
                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, sql, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get internal code failed: Execute method GetOldEquipmentByID in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }

            return dtResult;
        }
        /// <summary>
        /// get credit number by substitution id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string GetNoCreditByCreditSusID(string ID)
        {
            string Result = "";
            string SQL = "";

            try
            {
                SQL = "select No_Credito from K_CREDITO_SUSTITUCION where Id_Credito_Sustitucion=@Id_Credito_Sustitucion";

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Id_Credito_Sustitucion",ID)
                };

                object o = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);
                if (o != null)
                {
                    Result = o.ToString();
                }
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get No_Credito failed:Execute method  GetNoCreditByCreditSusID in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }

            return Result;
        }
        /// <summary>
        /// Change status to conformidad for each old products related with current credit request
        /// </summary>
        /// <param name="NoCredit">credit number</param>
        /// <param name="FgComfirm">conformidad value</param>
        /// <param name="ReceiptionDate">received date</param>
        /// <returns></returns>
        public int UpdateCreditSustitutionByNoCredit(string NoCredit, string FgComfirm, DateTime ReceiptionDate)
        {
            int Result = 0;

            try
            {
                string Sql = " update K_CREDITO_SUSTITUCION set Fg_Si_Funciona=@Fg_Si_Funciona, Dt_Fecha_Recepcion=@Dt_Fecha_Recepcion where No_Credito=@No_Credito";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Fg_Si_Funciona",FgComfirm),
                    new SqlParameter("@Dt_Fecha_Recepcion",ReceiptionDate),
                    new SqlParameter("@No_Credito",NoCredit)
                };

                Result = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, Sql, para);
            }
            catch (Exception ex)
            {
                throw new LsDAException(this, "Update Fg_Si_Function failed:Execute method  UpdateCreditSustitutionByNoCredit in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }

            return Result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int UpdateCreditSustutionByModel(K_CREDITO_SUSTITUCIONModel instance)
        {
            int Result = 0;
            try
            {
                StringBuilder Sql = new StringBuilder();
                Sql.Append(" update K_CREDITO_SUSTITUCION set");//edit by coco 2011-11-25
                Sql.Append(" ").Append("Fg_Si_Funciona=@Fg_Si_Funciona");
                Sql.Append(" ").Append(",Dt_Fecha_Recepcion=@Dt_Fecha_Recepcion,Id_Folio=@Id_Folio");
                Sql.Append(" ").Append("where Id_Credito_Sustitucion=@Id_Credito_Sustitucion");
                SqlParameter[] para = new SqlParameter[] {                    
                    new SqlParameter("@Fg_Si_Funciona",instance.Fg_Si_Funciona),                  
                    new SqlParameter("@Dt_Fecha_Recepcion",instance.Dt_Fecha_Recepcion),
                    new SqlParameter("@Id_Folio",instance.Id_Folio),
                    new SqlParameter("@Id_Credito_Sustitucion",instance.Id_Credito_Sustitucion)
                };
                Result = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, Sql.ToString(), para);
            }
            catch (Exception ex)
            {
                throw new LsDAException(this, "Update Credit Sustitution failed:Execute method  UpdateCreditSustutionByModel in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
            return Result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposalID"></param>
        /// <param name="UserType"></param>
        /// <param name="TechnologyID"></param>
        /// <returns></returns>
        public string GetFiloIDByDisposalAndTechnology(string disposalID, string UserType, int TechnologyID, string NoCredit)
        {
            string Result = "0";
            string SQL = "";
            try
            {
                SQL = "select isnull(Max(Id_folio),0)+1 from K_CREDITO_SUSTITUCION  where Id_Centro_Disp=@Id_Centro_Disp and Fg_Tipo_Centro_Disp=@Fg_Tipo_Centro_Disp and Cve_Tecnologia=@Cve_Tecnologia and Id_Folio is not null"; // RSA 2012-10-05  and No_Credito=@No_Credito";

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Cve_Tecnologia",TechnologyID),
                    new SqlParameter("@No_Credito",NoCredit),
                    new SqlParameter("@Id_Centro_Disp",disposalID),
                    new SqlParameter("@Fg_Tipo_Centro_Disp",UserType)
                };
                object o = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);
                if (o != null && !string.IsNullOrEmpty(o.ToString()))
                {
                    Result = o.ToString();
                }
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Id_Folio failed:Execute method  GetFiloIDByDisposalAndTechnology in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
            return Result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="program"></param>
        /// <param name="credit"></param>
        /// <param name="internalCode"></param>
        /// <param name="supplierID"></param>
        /// <param name="supplierType"></param>
        /// <param name="technology"></param>
        /// <param name="receiptFromDate"></param>
        /// <param name="receiptToDate"></param>
        /// <param name="inhabilitacionFromDate"></param>
        /// <param name="inhabilitacionToDate"></param>
        /// <param name="material"></param>
        /// <param name="disposalID"></param>
        /// <param name="disposalType"></param>
        /// <param name="sortName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable GetRecoveryProducts(string program, string credit, string internalCode, string supplierID, string supplierType, string technology, string receiptFromDate, string receiptToDate,
                                                              string inhabilitacionFromDate, string inhabilitacionToDate, int material, int disposalID, string disposalType, string sortName, int pageIndex, int pageSize,
                                                              out int pageCount, string equipmentNotRegistryAllMeterial, string recoveryDate)//updated by tina 2012-07-12
        {
            DataTable dtResult = null;
            string strWhere = " INNER JOIN K_INHABILITACION_PRODUCTO B ON A.Id_Credito_Sustitucion=B.Id_Credito_Sustitucion INNER JOIN K_INHABILITACION C ON B.Id_Inhabilitacion=C.Id_Inhabilitacion";
            strWhere += " WHERE 1=1 AND A.IsUploadInhabilitacion = '1' AND CONVERT(VARCHAR(10),C.Dt_Fecha_Inhabilitacion, 120) <= '" + recoveryDate + "' AND CONVERT(VARCHAR(10),C.Dt_Fecha_Inhabilitacion, 120) <= GetDate() AND A.Id_Credito_Sustitucion not IN(SELECT Id_Credito_Sustitucion FROM dbo.View_RecoveryProducts WHERE Cve_Tecnologia=" + technology + " AND Cve_Residuo_Material=" + material + ")";
            //updated by tina 2012-08-08
            strWhere += " AND A.Id_Credito_Sustitucion IN(SELECT DISTINCT Id_Credito_Sustitucion FROM dbo.View_RecoveryProducts WHERE Cve_Tecnologia=" + technology + " AND Id_Orden=1)";
            //end updated
            try
            {
                if (program != "")
                {
                    strWhere += " AND A.ID_Prog_Proy=" + program;
                }
                if (credit != "")
                {
                    strWhere += " AND A.No_Credito='" + credit + "'";
                }
                if (internalCode != "")
                {
                    strWhere += " AND A.Id_Folio='" + internalCode + "'";
                }
                if (supplierID != "")
                {
                    strWhere += " AND A.Id_Proveedor=" + supplierID + " AND A.Tipo_Usuario='" + supplierType + "'";
                }
                if (technology != "")
                {
                    strWhere += " AND A.Cve_Tecnologia=" + technology;
                }
                if (receiptFromDate != "")
                {
                    strWhere += " AND CONVERT(VARCHAR(10),A.Dt_Fecha_Recepcion, 120)>='" + receiptFromDate + "'";
                }
                if (receiptToDate != "")
                {
                    strWhere += " AND CONVERT(VARCHAR(10),A.Dt_Fecha_Recepcion, 120)<='" + receiptToDate + "'";
                }
                if (inhabilitacionFromDate != "")
                {
                    strWhere += " AND CONVERT(VARCHAR(10),C.Dt_Fecha_Inhabilitacion, 120)>='" + inhabilitacionFromDate + "'";
                }
                if (inhabilitacionToDate != "")
                {
                    strWhere += " AND CONVERT(VARCHAR(10),C.Dt_Fecha_Inhabilitacion, 120)<='" + inhabilitacionToDate + "'";
                }
                if (disposalID != 0)
                {
                    strWhere += " AND A.Id_Centro_Disp=" + disposalID;
                }
                if (disposalType != "")
                {
                    strWhere += " AND A.Fg_Tipo_Centro_Disp='" + disposalType + "'";
                }
                //added by tina 2012-07-12
                if (equipmentNotRegistryAllMeterial != "")
                {
                    strWhere += " AND A.Id_Credito_Sustitucion IN(" + equipmentNotRegistryAllMeterial + ")";
                }
                //end

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@PageCount", SqlDbType.Int),
                    new SqlParameter("@OrderBy", sortName),
                    new SqlParameter("@PageIndex", pageIndex),
                    new SqlParameter("@PageSize", pageSize),
                    new SqlParameter("@spWhere", strWhere)
                };
                paras[0].Direction = ParameterDirection.Output;

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "UP_Pager_GetView_InhabilitacionProducts", paras);
                int.TryParse(paras[0].Value.ToString(), out pageCount);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Inhabilitacion Products failed:Execute method  GetRecoveryProducts in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
            return dtResult;

        }
        /// <summary>
        /// material recovery
        /// </summary>
        /// <param name="program"></param>
        /// <param name="credit"></param>
        /// <param name="internalCode"></param>
        /// <param name="supplierID"></param>
        /// <param name="supplierType"></param>
        /// <param name="technology"></param>
        /// <param name="receiptFromDate"></param>
        /// <param name="receiptToDate"></param>
        /// <param name="inhabilitacionFromDate"></param>
        /// <param name="inhabilitacionToDate"></param>
        /// <param name="material"></param>
        /// <param name="order"></param>
        /// <param name="disposalID"></param>
        /// <param name="disposalType"></param>
        /// <param name="sortName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable GetRecoveryProducts(string program, string credit, string internalCode, string supplierID, string supplierType, string technology, string receiptFromDate, string receiptToDate,
                                                              string inhabilitacionFromDate, string inhabilitacionToDate, int material, int order, int disposalID, string disposalType, string sortName, int pageIndex, int pageSize,
                                                              out int pageCount, string equipmentNotRegistryAllMeterial, string recoveryDate)//updated by tina 2012-07-12
        {
            DataTable dtResult = null;
            string strWhere = " INNER JOIN K_INHABILITACION_PRODUCTO B ON A.Id_Credito_Sustitucion=B.Id_Credito_Sustitucion INNER JOIN K_INHABILITACION C ON B.Id_Inhabilitacion=C.Id_Inhabilitacion";
            strWhere += " WHERE 1=1 AND A.IsUploadInhabilitacion = '1' AND CONVERT(VARCHAR(10),C.Dt_Fecha_Inhabilitacion, 120) <= '" + recoveryDate + "' AND CONVERT(VARCHAR(10),C.Dt_Fecha_Inhabilitacion, 120) <= GetDate() AND A.Id_Credito_Sustitucion not IN(SELECT Id_Credito_Sustitucion FROM dbo.View_RecoveryProducts WHERE Cve_Tecnologia=" + technology + " AND Cve_Residuo_Material=" + material + ")" +
                             " AND A.Id_Credito_Sustitucion IN(SELECT Id_Credito_Sustitucion FROM dbo.View_RecoveryProducts WHERE Cve_Tecnologia=" + technology + "" +
                             "        AND Cve_Residuo_Material=(SELECT DISTINCT Cve_Residuo_Material FROM dbo.View_RecoveryProducts WHERE Cve_Tecnologia=" + technology + " AND Id_Orden=" + (order - 1) + "))";
            try
            {
                if (program != "")
                {
                    strWhere += " AND A.ID_Prog_Proy=" + program;
                }
                if (credit != "")
                {
                    strWhere += " AND A.No_Credito='" + credit + "'";
                }
                if (internalCode != "")
                {
                    strWhere += " AND A.Id_Folio='" + internalCode + "'";
                }
                if (supplierID != "")
                {
                    strWhere += " AND A.Id_Proveedor=" + supplierID + " AND A.Tipo_Usuario='" + supplierType + "'";
                }
                if (technology != "")
                {
                    strWhere += " AND A.Cve_Tecnologia=" + technology;
                }
                if (receiptFromDate != "")
                {
                    strWhere += " AND CONVERT(VARCHAR(10),A.Dt_Fecha_Recepcion, 120)>='" + receiptFromDate + "'";
                }
                if (receiptToDate != "")
                {
                    strWhere += " AND CONVERT(VARCHAR(10),A.Dt_Fecha_Recepcion, 120)<='" + receiptToDate + "'";
                }
                if (inhabilitacionFromDate != "")
                {
                    strWhere += " AND CONVERT(VARCHAR(10),C.Dt_Fecha_Inhabilitacion, 120)>='" + inhabilitacionFromDate + "'";
                }
                if (inhabilitacionToDate != "")
                {
                    strWhere += " AND CONVERT(VARCHAR(10),C.Dt_Fecha_Inhabilitacion, 120)<='" + inhabilitacionToDate + "'";
                }
                if (disposalID != 0)
                {
                    strWhere += " AND A.Id_Centro_Disp=" + disposalID;
                }
                if (disposalType != "")
                {
                    strWhere += " AND A.Fg_Tipo_Centro_Disp='" + disposalType + "'";
                }
                //added by tina 2012-07-12
                if (equipmentNotRegistryAllMeterial != "")
                {
                    strWhere += " AND A.Id_Credito_Sustitucion IN(" + equipmentNotRegistryAllMeterial + ")";
                }
                //end

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@PageCount", SqlDbType.Int),
                    new SqlParameter("@OrderBy", sortName),
                    new SqlParameter("@PageIndex", pageIndex),
                    new SqlParameter("@PageSize", pageSize),
                    new SqlParameter("@spWhere", strWhere)
                };
                paras[0].Direction = ParameterDirection.Output;

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "UP_Pager_GetView_InhabilitacionProducts", paras);
                int.TryParse(paras[0].Value.ToString(), out pageCount);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Received Products failed:Execute method  GetRecoveryProducts in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
            return dtResult;
        }
        /// <summary>
        /// gas recovery
        /// </summary>
        /// <param name="program"></param>
        /// <param name="credit"></param>
        /// <param name="internalCode"></param>
        /// <param name="supplierID"></param>
        /// <param name="supplierType"></param>
        /// <param name="technology"></param>
        /// <param name="receiptFromDate"></param>
        /// <param name="receiptToDate"></param>
        /// <param name="inhabilitacionFromDate"></param>
        /// <param name="inhabilitacionToDate"></param>
        /// <param name="material"></param>
        /// <param name="disposalID"></param>
        /// <param name="disposalType"></param>
        /// <param name="sortName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable GetGasRecoveryProducts(string program, string credit, string internalCode, string supplierID, string supplierType, string technology, string receiptFromDate, string receiptToDate,
                                                              string inhabilitacionFromDate, string inhabilitacionToDate, int material, int disposalID, string disposalType, string sortName, int pageIndex, int pageSize,
                                                              out int pageCount, string equipmentNotRegistryAllMeterial, string recoveryDate)//updated by tina 2012-07-12
        {
            DataTable dtResult = null;
            string strWhere = " INNER JOIN K_INHABILITACION_PRODUCTO B ON A.Id_Credito_Sustitucion=B.Id_Credito_Sustitucion INNER JOIN K_INHABILITACION C ON B.Id_Inhabilitacion=C.Id_Inhabilitacion";
            //updated by tina 2012-08-08
            strWhere += " WHERE 1=1 AND A.IsUploadInhabilitacion = '1' AND CONVERT(VARCHAR(10),C.Dt_Fecha_Inhabilitacion, 120) <= '" + recoveryDate + "' AND CONVERT(VARCHAR(10),C.Dt_Fecha_Inhabilitacion, 120) <= GetDate() AND A.Id_Credito_Sustitucion NOT IN" +
                             " (SELECT DISTINCT Id_Credito_Sustitucion FROM dbo.View_RecoveryProducts WHERE Cve_Tecnologia=" + technology + " AND Id_Orden=1)";
            //end updated
            try
            {
                if (program != "")
                {
                    strWhere += " AND A.ID_Prog_Proy=" + program;
                }
                if (credit != "")
                {
                    strWhere += " AND A.No_Credito='" + credit + "'";
                }
                if (internalCode != "")
                {
                    strWhere += " AND A.Id_Folio='" + internalCode + "'";
                }
                if (supplierID != "")
                {
                    strWhere += " AND A.Id_Proveedor=" + supplierID + " AND A.Tipo_Usuario='" + supplierType + "'";
                }
                if (technology != "")
                {
                    strWhere += " AND A.Cve_Tecnologia=" + technology;
                }
                if (receiptFromDate != "")
                {
                    strWhere += " AND CONVERT(VARCHAR(10),A.Dt_Fecha_Recepcion, 120)>='" + receiptFromDate + "'";
                }
                if (receiptToDate != "")
                {
                    strWhere += " AND CONVERT(VARCHAR(10),A.Dt_Fecha_Recepcion, 120)<='" + receiptToDate + "'";
                }
                if (inhabilitacionFromDate != "")
                {
                    strWhere += " AND CONVERT(VARCHAR(10),C.Dt_Fecha_Inhabilitacion, 120)>='" + inhabilitacionFromDate + "'";
                }
                if (inhabilitacionToDate != "")
                {
                    strWhere += " AND CONVERT(VARCHAR(10),C.Dt_Fecha_Inhabilitacion, 120)<='" + inhabilitacionToDate + "'";
                }
                if (disposalID != 0)
                {
                    strWhere += " AND A.Id_Centro_Disp=" + disposalID;
                }
                if (disposalType != "")
                {
                    strWhere += " AND A.Fg_Tipo_Centro_Disp='" + disposalType + "'";
                }
                //added by tina 2012-07-12
                if (equipmentNotRegistryAllMeterial != "")
                {
                    strWhere += " AND A.Id_Credito_Sustitucion IN(" + equipmentNotRegistryAllMeterial + ")";
                }
                //end

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@PageCount", SqlDbType.Int),
                    new SqlParameter("@OrderBy", sortName),
                    new SqlParameter("@PageIndex", pageIndex),
                    new SqlParameter("@PageSize", pageSize),
                    new SqlParameter("@spWhere", strWhere)
                };
                paras[0].Direction = ParameterDirection.Output;

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "UP_Pager_GetView_InhabilitacionProducts", paras);
                int.TryParse(paras[0].Value.ToString(), out pageCount);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Received Products failed:Execute method  GetGasRecoveryProducts in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
            return dtResult;
        }
        /// <summary>
        /// Upload image for receive old equipment
        /// </summary>
        /// <param name="program"></param>
        /// <param name="credit"></param>
        /// <param name="internalCode"></param>
        /// <param name="supplierID"></param>
        /// <param name="SupplierType"></param>
        /// <param name="ImageType"></param>
        /// <param name="technology"></param>
        /// <param name="receiptFromDate"></param>
        /// <param name="receiptToDate"></param>
        /// <param name="disposalID"></param>
        /// <param name="disposalType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable GetRecoveryProductsForImage(string program, string credit, string internalCode, string supplierID, string SupplierType, string ImageType, string technology, string receiptFromDate, string receiptToDate,
                                                               int disposalID, string disposalType, int pageIndex, int pageSize, out int pageCount)
        {
            DataTable dtResult = null;
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append("where 1=1 and Dt_Fecha_Recepcion is not null and Fg_Si_Funciona!=0  ");//edit by coco 2012-01-12
            try
            {
                if (program != "")
                {
                    strWhere.Append(" ").Append(" AND ID_Prog_Proy=" + program);
                }
                if (credit != "")
                {
                    strWhere.Append(" ").Append(" AND No_Credito='" + credit + "'");
                }
                if (internalCode != "")
                {
                    strWhere.Append(" ").Append(" AND Id_Folio='" + internalCode + "'");
                }
                if (supplierID != "")
                {
                    strWhere.Append(" ").Append(" AND Id_Proveedor=" + supplierID + " AND Tipo_Usuario='" + SupplierType + "'");
                }
                if (technology != "")
                {
                    strWhere.Append(" ").Append(" AND Cve_Tecnologia=" + technology);
                }
                if (receiptFromDate != "")
                {
                    strWhere.Append(" ").Append(" AND CONVERT(VARCHAR(10),Dt_Fecha_Recepcion, 120)>='" + receiptFromDate + "'");
                }
                if (receiptToDate != "")
                {
                    strWhere.Append(" ").Append(" AND CONVERT(VARCHAR(10),Dt_Fecha_Recepcion, 120)<='" + receiptToDate + "'");
                }
                if (ImageType.Equals("1"))
                {
                    strWhere.Append(" ").Append(" and Dx_Imagen_Recepcion is not null ");
                }
                else if (ImageType.Equals("0"))
                {
                    strWhere.Append(" ").Append(" and Dx_Imagen_Recepcion is null ");
                }
                if (disposalID != 0)
                {
                    strWhere.Append(" ").Append(" AND Id_Centro_Disp=" + disposalID);
                }
                if (disposalType != "")
                {
                    strWhere.Append(" ").Append(" AND Fg_Tipo_Centro_Disp='" + disposalType + "'");
                }

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@PageCount", SqlDbType.Int),                    
                    new SqlParameter("@PageIndex", pageIndex),
                    new SqlParameter("@PageSize", pageSize),
                    new SqlParameter("@spWhere", strWhere.ToString())
                };
                paras[0].Direction = ParameterDirection.Output;

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "sp_get_Old_Equipment_ForImage", paras);
                int.TryParse(paras[0].Value.ToString(), out pageCount);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Get Received Products failed:Execute method GetRecoveryProductsForImage  in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
            return dtResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int UpLoadImageForReceiveOldEquipment(K_CREDITO_SUSTITUCIONModel instance)
        {
            int Result = 0;
            try
            {
                string Sql = " Update K_CREDITO_SUSTITUCION set Dx_Imagen_Recepcion=@Dx_Imagen_Recepcion where Id_Credito_Sustitucion=@Id_Credito_Sustitucion";
                SqlParameter[] para = new SqlParameter[]{                
                    new SqlParameter("@Id_Credito_Sustitucion",instance.Id_Credito_Sustitucion),
                    new SqlParameter("@Dx_Imagen_Recepcion",instance.Dx_Imagen_Recepcion)
                };
                Result = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, Sql, para);
            }
            catch (Exception ex)
            {
                throw new LsDAException(this, "Upload image failed:Execute method  UpLoadImageForReceiveOldEquipment in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
            return Result;
        }

        /// <summary>
        /// get first date of old equipment reception registry 
        /// </summary>
        /// <returns></returns>
        public string GetFirstReceiptDate()
        {
            string date = "";
            try
            {
                string SQL = "select min(Dt_Fecha_Recepcion) as Dt_Fecha_Recepcion from K_CREDITO_SUSTITUCION";
                object result = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
                if (result != null)
                {
                    date = result.ToString();
                }
                return date;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get first date of old equipment reception registry failed: Execute method GetFirstReceiptDate in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
        }
        /// <summary>
        /// Get Supervision Date Registry  data
        /// </summary>
        /// <param name="program"></param>
        /// <param name="technology"></param>
        /// <param name="receiptFromDate"></param>
        /// <param name="receiptToDate"></param>
        /// <param name="UserID"></param>
        /// <param name="UserType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable GetRecoveryProductsForSupervision(string program, string technology, string receiptFromDate, string receiptToDate,
                                                              int UserID, string UserType, string CAyD, string TipoOfDisposal, int pageIndex, int pageSize, out int pageCount)
        {
            DataTable dtResult = null;
            try
            {
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@PageCount", SqlDbType.Int),                    
                    new SqlParameter("@PageIndex", pageIndex),
                    new SqlParameter("@PageSize", pageSize),
                    new SqlParameter("@Technology", technology),
                    new SqlParameter("@Program",program),
                    new SqlParameter("@FromDate",receiptFromDate),
                    new SqlParameter("@ToDate",receiptToDate),
                    new SqlParameter("@UserID",UserID.ToString()),
                    new SqlParameter("@UserType",UserType),
                    new SqlParameter("@CAyD",CAyD),
                    new SqlParameter("@ToD",TipoOfDisposal)
                };
                paras[0].Direction = ParameterDirection.Output;

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "sp_get_Old_Equipment_ForSupervision", paras);
                int.TryParse(paras[0].Value.ToString(), out pageCount);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Received Products failed:Execute method GetRecoveryProductsForSupervision  in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
            return dtResult;
        }
        /// <summary>
        /// Get inhabilitación products without images
        /// </summary>
        /// <param name="program">program</param>
        /// <param name="credit">credit id</param>
        /// <param name="internalCode">old product internal code</param>
        /// <param name="supplierID">supplier id</param>
        /// <param name="SupplierType">supplier type</param>
        /// <param name="ImageType">image type</param>
        /// <param name="technology">technology</param>
        /// <param name="receiptFromDate">old product reception date from</param>
        /// <param name="receiptToDate">old product reception date to</param>
        /// <param name="disposalID">disposal center id</param>
        /// <param name="disposalType">disposal center type</param>
        /// <param name="InFromDate">old product inhabilitación date from</param>
        /// <param name="InToDate">old product Approval date to</param>
        /// <param name="pageIndex">page index</param>
        /// <param name="pageSize">page size</param>
        /// <param name="pageCount">page count</param>
        /// <returns></returns>
        public DataTable GetInHabilitacionProductsForImage(string program, string credit, string internalCode, string supplierID, string SupplierType, string ImageType, string technology, string receiptFromDate, string receiptToDate,
                                                             int disposalID, string disposalType, string InFromDate, string InToDate, int pageIndex, int pageSize, out int pageCount)
        {
            DataTable dtResult = null;
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append("where 1=1");
            try
            {
                if (program != "")
                {
                    strWhere.Append(" ").Append(" AND ID_Prog_Proy=" + program);
                }
                if (credit != "")
                {
                    strWhere.Append(" ").Append(" AND No_Credito='" + credit + "'");
                }
                if (internalCode != "")
                {
                    strWhere.Append(" ").Append(" AND Id_Folio='" + internalCode + "'");
                }
                if (supplierID != "")
                {
                    strWhere.Append(" ").Append(" AND Id_Proveedor=" + supplierID + " AND Tipo_Usuario='" + SupplierType + "'");
                }
                if (technology != "")
                {
                    strWhere.Append(" ").Append(" AND Cve_Tecnologia=" + technology);
                }
                if (receiptFromDate != "")
                {
                    strWhere.Append(" ").Append(" AND CONVERT(VARCHAR(10),Dt_Fecha_Recepcion, 120)>='" + receiptFromDate + "'");
                }
                if (receiptToDate != "")
                {
                    strWhere.Append(" ").Append(" AND CONVERT(VARCHAR(10),Dt_Fecha_Recepcion, 120)<='" + receiptToDate + "'");
                }
                if (InFromDate != "")
                {
                    strWhere.Append(" ").Append(" AND CONVERT(VARCHAR(10),Dt_Fecha_Inhabilitacion, 120)>='" + InFromDate + "'");
                }
                if (InToDate != "")
                {
                    strWhere.Append(" ").Append(" AND CONVERT(VARCHAR(10),Dt_Fecha_Inhabilitacion, 120)<='" + InToDate + "'");
                }
                if (ImageType.Equals("1"))
                {
                    strWhere.Append(" ").Append(" and Dx_Imagen_Inhabilitacion is not null ");
                }
                else if (ImageType.Equals("0"))
                {
                    strWhere.Append(" ").Append(" and Dx_Imagen_Inhabilitacion is null ");
                }
                if (disposalID != 0)
                {
                    strWhere.Append(" ").Append(" AND Id_Centro_Disp=" + disposalID);
                }
                if (disposalType != "")
                {
                    strWhere.Append(" ").Append(" AND Fg_Tipo_Centro_Disp='" + disposalType + "'");
                }

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@PageCount", SqlDbType.Int),                    
                    new SqlParameter("@PageIndex", pageIndex),
                    new SqlParameter("@PageSize", pageSize),
                    new SqlParameter("@spWhere", strWhere.ToString())
                };
                paras[0].Direction = ParameterDirection.Output;

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "sp_get_Old_Equipment_Inhabilitacion", paras);
                int.TryParse(paras[0].Value.ToString(), out pageCount);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Get Received Products failed:Execute method GetInHabilitacionProductsForImage  in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
            return dtResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int UpLoadImageForInHabilitacionOldEquipment(K_CREDITO_SUSTITUCIONModel instance)
        {
            int Result = 0;
            try
            {
                string Sql = " Update K_CREDITO_SUSTITUCION set Dx_Imagen_Inhabilitacion=@Dx_Imagen_Inhabilitacion where Id_Credito_Sustitucion=@Id_Credito_Sustitucion";
                SqlParameter[] para = new SqlParameter[]{                
                    new SqlParameter("@Id_Credito_Sustitucion",instance.Id_Credito_Sustitucion),
                    new SqlParameter("@Dx_Imagen_Inhabilitacion",instance.Dx_Imagen_Inhabilitacion)
                };
                Result = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, Sql, para);
            }
            catch (Exception ex)
            {
                throw new LsDAException(this, "Upload image failed:Execute method  UpLoadImageForInHabilitacionOldEquipment in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
            return Result;
        }
        /// <summary>
        /// Get Old Equipment By DisposalID and DisposalType for Review
        /// </summary>
        /// <param name="program"></param>
        /// <param name="technology"></param>
        /// <param name="receiptFromDate"></param>
        /// <param name="receiptToDate"></param>
        /// <param name="UserID"></param>
        /// <param name="UserType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable GetRecoveryProductsForSupervisionView(string program, string technology, string receiptFromDate, string receiptToDate,
                                                           int UserID, string UserType, int pageIndex, int pageSize, out int pageCount)
        {
            DataTable dtResult = null;
            try
            {
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@PageCount", SqlDbType.Int),                    
                    new SqlParameter("@PageIndex", pageIndex),
                    new SqlParameter("@PageSize", pageSize),
                    new SqlParameter("@Technology", technology),
                    new SqlParameter("@Program",program),
                    new SqlParameter("@FromDate",receiptFromDate),
                    new SqlParameter("@ToDate",receiptToDate),
                    new SqlParameter("@UserID",UserID.ToString()),
                    new SqlParameter("@UserType",UserType)
                };
                paras[0].Direction = ParameterDirection.Output;

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "sp_get_Old_Equipment_ForSupervision_View", paras);
                int.TryParse(paras[0].Value.ToString(), out pageCount);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Received Products failed:Execute method GetRecoveryProductsForSupervisionView  in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
            return dtResult;
        }

        public DataTable GetInternalCodeWithTechnologyAndMaterialAndProgram(string program, string technology, string material, int order)
        {
            DataTable dtResult = null;
            try
            {
                string SQL = "SELECT DISTINCT A.No_Credito FROM CRE_CREDITO A INNER JOIN K_CREDITO_SUSTITUCION B" +
                                " ON A.No_Credito=B.No_Credito";
                if (order == 1)
                {
                    SQL += " WHERE 1=1 AND B.Id_Credito_Sustitucion NOT IN(SELECT Id_Credito_Sustitucion FROM dbo.View_RecoveryProducts WHERE Cve_Tecnologia=" + technology + " AND Cve_Residuo_Material=" + material + ")";
                }
                else if (order == 2)
                {
                    SQL += " WHERE 1=1 AND B.Id_Credito_Sustitucion NOT IN(SELECT Id_Credito_Sustitucion FROM dbo.View_RecoveryProducts WHERE Cve_Tecnologia=" + technology + " AND Cve_Residuo_Material=" + material + ")" +
                            " AND B.Id_Credito_Sustitucion IN(SELECT  Id_Credito_Sustitucion FROM dbo.View_RecoveryProducts WHERE Cve_Tecnologia=" + technology + " AND Id_Orden=1" +
                            " GROUP BY Cve_Tecnologia,Id_Orden,Id_Credito_Sustitucion HAVING COUNT(Id_Credito_Sustitucion)=(SELECT COUNT(*) FROM dbo.View_All_Material WHERE ISNULL(Fg_Residuo_Material,0)=1 AND Cve_Tecnologia=" + technology + "))";
                }
                else
                {
                    SQL += " WHERE 1=1 AND B.Id_Credito_Sustitucion not IN(SELECT Id_Credito_Sustitucion FROM dbo.View_RecoveryProducts WHERE Cve_Tecnologia=" + technology + " AND Cve_Residuo_Material=" + material + ")" +
                             " AND B.Id_Credito_Sustitucion IN(SELECT Id_Credito_Sustitucion FROM dbo.View_RecoveryProducts WHERE Cve_Tecnologia=" + technology + "" +
                             " AND Cve_Residuo_Material=(SELECT DISTINCT Cve_Residuo_Material FROM dbo.View_RecoveryProducts WHERE Cve_Tecnologia=" + technology + " AND Id_Orden=" + (order - 1) + "))";
                }
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Credits with technology and material and program failed: Execute method GetCreditsWithTechnologyAndMaterialAndProgram in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
            return dtResult;
        }

        /// <summary>
        /// Get received old product internal code for material registry
        /// </summary>
        /// <param name="credit"></param>
        /// <param name="program"></param>
        /// <param name="technology"></param>
        /// <param name="material"></param>
        /// <param name="order"></param>
        /// <param name="supplierId"></param>
        /// <param name="supplierType"></param>
        /// <param name="disposalId"></param>
        /// <param name="disposalType"></param>
        /// <returns></returns>
        public DataTable GetReceivedOldProductsInternalCodeForMaterialRegistry(string credit, string program, string technology, string material, int order, string supplierId,
                                                                string supplierType, int disposalId, string disposalType, string equipmentNotRegistryAllMeterial)//updated by tina 2012-08-09
        {
            DataTable dtResult = null;
            try
            {
                //updated by tina 2012-08-09
                string SQL = "SELECT DISTINCT A.Id_Folio FROM dbo.View_Old_Equipment_List A  INNER JOIN K_INHABILITACION_PRODUCTO B ON A.Id_Credito_Sustitucion=B.Id_Credito_Sustitucion INNER JOIN K_INHABILITACION C ON B.Id_Inhabilitacion=C.Id_Inhabilitacion";

                if (order == 1)
                {
                    SQL += " WHERE 1=1 AND A.Id_Credito_Sustitucion NOT IN" +
                             " (SELECT DISTINCT Id_Credito_Sustitucion FROM dbo.View_RecoveryProducts WHERE Cve_Tecnologia=@technology AND Id_Orden=1)";
                }
                if (order == 2)
                {
                    SQL += " WHERE 1=1 AND A.Id_Credito_Sustitucion not IN(SELECT Id_Credito_Sustitucion FROM dbo.View_RecoveryProducts WHERE Cve_Tecnologia=@technology AND Cve_Residuo_Material=@material)" +
                             " AND A.Id_Credito_Sustitucion IN(SELECT DISTINCT Id_Credito_Sustitucion FROM dbo.View_RecoveryProducts WHERE Cve_Tecnologia=@technology AND Id_Orden=1)";
                }
                else if (order > 2)
                {
                    SQL += " WHERE 1=1 AND A.Id_Credito_Sustitucion not IN(SELECT Id_Credito_Sustitucion FROM dbo.View_RecoveryProducts WHERE Cve_Tecnologia=@technology AND Cve_Residuo_Material=@material)" +
                             "  AND A.Id_Credito_Sustitucion IN(SELECT Id_Credito_Sustitucion FROM dbo.View_RecoveryProducts WHERE Cve_Tecnologia=@technology" +
                             "  AND Cve_Residuo_Material=(SELECT DISTINCT Cve_Residuo_Material FROM dbo.View_RecoveryProducts WHERE Cve_Tecnologia=@technology AND Id_Orden= (@order - 1)))";
                }
                //end updated

                if (credit != "")
                {
                    SQL += " AND A.No_Credito=@credit";
                }
                if (program != "")
                {
                    SQL += " AND A.ID_Prog_Proy=@program";
                }
                if (supplierId != "")
                {
                    SQL += " AND A.Id_Proveedor=@supplierId";
                }
                if (supplierType != "")
                {
                    SQL += " AND A.Tipo_Usuario=@supplierType";
                }
                if (technology != "")
                {
                    SQL += " AND A.Cve_Tecnologia=@technology";
                }
                if (disposalId != 0)
                {
                    SQL += " AND A.Id_Centro_Disp=@disposalID";
                }
                if (disposalType != "")
                {
                    SQL += " AND A.Fg_Tipo_Centro_Disp=@disposalType";
                }
                //added by tina 2012-08-09
                if (equipmentNotRegistryAllMeterial != "")
                {
                    SQL += " AND A.Id_Credito_Sustitucion IN(" + equipmentNotRegistryAllMeterial + ")";
                }
                //end

                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter("@credit",credit),
                    new SqlParameter("@technology",technology),
                    new SqlParameter("@material",material),
                    new SqlParameter("@order",order),
                    new SqlParameter("@program",program),
                    new SqlParameter("@supplierId",supplierId),
                    new SqlParameter("@supplierType",supplierType),
                    new SqlParameter("@disposalId",disposalId),
                    new SqlParameter("@disposalType",disposalType)
                };
                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get received old product internal code for material registry failed: Execute method GetReceivedOldProductsInternalCodeForMaterialRegistry in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
            return dtResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credit"></param>
        /// <param name="program"></param>
        /// <param name="technology"></param>
        /// <param name="supplierId"></param>
        /// <param name="supplierType"></param>
        /// <param name="disposalId"></param>
        /// <param name="disposalType"></param>
        /// <param name="registry"></param>
        /// <returns></returns>
        public DataTable GetDisableOldProductByProgramAndDisposalAndTechnologyAndSupplier(string credit, string program, string technology, string supplierId, string supplierType, int disposalId, string disposalType, string registry)
        {
            DataTable TableResult = null;

            try
            {
                //Build the query SQL string
                string SQL = "SELECT DISTINCT B.Id_Folio FROM CRE_Credito A INNER JOIN K_CREDITO_SUSTITUCION B" +
                                 " ON A.No_Credito=B.No_Credito AND B.Id_Folio IS NOT NULL AND B.Dt_Fecha_Recepcion IS NOT NULL AND ISNULL(B.Fg_Si_Funciona,0)=1 " +
                                 " WHERE 1=1";
                if (credit != "")
                {
                    SQL += " AND B.No_Credito=@credit";
                }
                if (program != "")
                {
                    SQL += " AND A.ID_Prog_Proy=@program";
                }
                if (disposalId != 0)
                {
                    SQL += " AND B.Id_Centro_Disp=@disposalId";
                }
                if (disposalType != "")
                {
                    SQL += " AND B.Fg_Tipo_Centro_Disp=@disposalType";
                }
                if (technology != "")
                {
                    SQL += " AND B.Cve_Tecnologia=@technology";
                }
                if (supplierId != "")
                {
                    SQL += " AND A.Id_Proveedor=@supplierId";
                }
                if (supplierType != "")
                {
                    SQL += " AND A.Tipo_Usuario=@supplierType";
                }

                if (registry != "")
                {
                    if (registry == "Inhabilitados")
                    {
                        SQL += " AND B.Id_Credito_Sustitucion IN (SELECT Id_Credito_Sustitucion FROM K_INHABILITACION_PRODUCTO)";
                    }
                    else
                    {
                        SQL += " AND B.Id_Credito_Sustitucion NOT IN (SELECT Id_Credito_Sustitucion FROM K_INHABILITACION_PRODUCTO)";
                    }
                }
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter("@credit",credit),
                    new SqlParameter("@program",program),
                    new SqlParameter("@disposalId",disposalId),
                    new SqlParameter("@disposalType",disposalType),
                    new SqlParameter("@technology",technology),
                    new SqlParameter("@supplierId",supplierId),
                    new SqlParameter("@supplierType",supplierType)
                };

                TableResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get received old product internal code failed: Execute method GetDisableOldProductByProgramAndDisposalAndTechnologyAndSupplier in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }

            return TableResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposalID"></param>
        /// <param name="UserType"></param>
        /// <param name="NoCredit"></param>
        /// <returns></returns>
        public int GetCountDtFeachReciveptionIsnull(string disposalID, string UserType, string NoCredit)
        {
            int Result = 0;
            string SQL = "";
            try
            {
                SQL = "select COUNT(*) from View_Old_Equipment_List where No_Credito=@No_Credito and Id_Centro_Disp=@Id_Centro_Disp and Fg_Tipo_Centro_Disp=@Fg_Tipo_Centro_Disp and (Dt_Fecha_Recepcion is null or Fg_Si_Funciona=0)";//edit by coco 2012-01-12
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@No_Credito",NoCredit),
                    new SqlParameter("@Id_Centro_Disp",disposalID),
                    new SqlParameter("@Fg_Tipo_Centro_Disp",UserType)
                };
                object o = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);
                if (o != null)
                {
                    Result = int.Parse(o.ToString());
                }
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Id_Folio failed:Execute method  GetCountDtFeachReciveptionIsnull in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
            return Result;
        }

        /// getting the status for the supplier if the credit is already in the CAYD

        public int GetCountDtFeachReciveptionIsnull_Supplier(string NoCredit)
        {
            int Result = 0;
            string SQL = "";
            try
            {
                SQL = "select [Id_Folio] from [K_CREDITO_SUSTITUCION] where No_Credito=@No_Credito ";//edit by coco 2012-01-12
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@No_Credito",NoCredit),
               
                };
                object o = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);
                if (o != null)
                {
                    int.TryParse(o.ToString(), out Result);
                }
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Id_Folio failed:Execute method  GetCountDtFeachReciveptionIsnull in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
            return Result;
        }

        public bool IsSustitucionFolioRequired(string NoCredit)
        {
            int Result = 0;
            bool required = false;
            string SQL = "";
            try
            {
                SQL = "select [Id_Folio] from [K_CREDITO_SUSTITUCION] SUST " +
                      " JOIN CAT_TECNOLOGIA  TEC ON SUST.Cve_Tecnologia = TEC.Cve_Tecnologia " +
                      " where No_Credito=@No_Credito " +
                      " AND TEC.Cve_Esquema = 0";//edit by coco 2012-01-12
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@No_Credito",NoCredit),
               
                };
                object o = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);
                if (o != null)
                {
                    int.TryParse(o.ToString(), out Result);
                    // values > 0 are OK, if it is 0 then the required folio is still missing
                    required = (Result == 0);
                }
                else
                {
                    // when null, it is not a sustitución technology, we don't require a Folio
                    required = false;
                }
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Id_Folio failed:Execute method  GetCountDtFeachReciveptionIsnull in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
            return required;
        }




        // added by tina 2012-02-24
        /// <summary>
        /// old equipment information by pk
        /// </summary>
        /// <param name="pkId"></param>
        /// <returns></returns>
        public DataTable GetOldEquipmentInfoByID(string pkId)
        {
            DataTable dtResult = null;

            string sql = "select * from K_CREDITO_SUSTITUCION where Id_Credito_Sustitucion =@Id_Credito_Sustitucion";
            SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Id_Credito_Sustitucion",pkId)                    
                };
            try
            {
                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, sql, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get old equipment information failed: Execute method GetOldEquipmentInfoByID in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }

            return dtResult;
        }

        //add by coco 2012-04-12
        public DataTable IsAllowUploadReceiptionOldEquipment(string disposalID, string UserType)
        {
            DataTable Result = new DataTable();
            string SQL = "";
            try
            {
                SQL = "select COUNT(*) as temp,Id_Credito_Sustitucion from dbo.View_Old_Equipment_List where Id_Centro_Disp=@Id_Centro_Disp and Fg_Tipo_Centro_Disp=@Fg_Tipo_Centro_Disp  ";
                //updated by tina 2012-07-12
                SQL = SQL + " and IsReceipt='1' and DATEDIFF(d,Dt_Fecha_Recepcion,GETDATE())>1 and Dx_Imagen_Recepcion is null group by Id_Credito_Sustitucion";
                //end
                SqlParameter[] para = new SqlParameter[] {                   
                    new SqlParameter("@Id_Centro_Disp",disposalID),
                    new SqlParameter("@Fg_Tipo_Centro_Disp",UserType)
                };
                Result = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "failed:Execute method  IsAllowUploadReceiptionOldEquipment in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
            return Result;
        }

        public bool ReceiptionOldEquipmentIsUploadImage(string disposalID, string UserType, string SustitucionID)
        {
            bool Result = true;
            string SQL = "";
            try
            {
                SQL = "select COUNT(*) from dbo.View_Old_Equipment_List_Inhabilitacion where Id_Centro_Disp=@Id_Centro_Disp and Fg_Tipo_Centro_Disp=@Fg_Tipo_Centro_Disp  ";
                SQL = SQL + " and Dx_Imagen_Recepcion is null and Id_Credito_Sustitucion=@Id_Credito_Sustitucion";
                SqlParameter[] para = new SqlParameter[] {                   
                    new SqlParameter("@Id_Centro_Disp",disposalID),
                    new SqlParameter("@Fg_Tipo_Centro_Disp",UserType),
                    new SqlParameter("@Id_Credito_Sustitucion",SustitucionID)
                };
                object o = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);
                if (o != null)
                {
                    if (int.Parse(o.ToString()) > 0)
                    {
                        Result = false;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "failed:Execute method  ReceiptionOldEquipmentIsUploadImage in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
            return Result;
        }
        public DataTable IsAllowUploadUnableOldEquipment(string disposalID, string UserType)
        {
            DataTable Result = new DataTable();
            string SQL = "";
            try
            {
                SQL = "select COUNT(*) as temp,Id_Credito_Sustitucion from dbo.View_Old_Equipment_List_Inhabilitacion where Id_Centro_Disp=@Id_Centro_Disp and Fg_Tipo_Centro_Disp=@Fg_Tipo_Centro_Disp  ";
                SQL = SQL + " and DATEDIFF(d,Dt_Fecha_Inhabilitacion,getdate())>3 and Dx_Imagen_Inhabilitacion is null  group by Id_Credito_Sustitucion";
                SqlParameter[] para = new SqlParameter[] {                   
                    new SqlParameter("@Id_Centro_Disp",disposalID),
                    new SqlParameter("@Fg_Tipo_Centro_Disp",UserType)                    
                };
                Result = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "failed:Execute method  IsAllowUploadUnableOldEquipment in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
            return Result;
        }
        //end add
        //add by coco 2012-04-13
        public DataTable GetMeterialOldEquipment(string IdRecuperacion, int pageIndex, int pageSize, out int pageCount)
        {
            DataTable dtResult = null;
            try
            {
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@PageCount", SqlDbType.Int),                    
                    new SqlParameter("@PageIndex", pageIndex),
                    new SqlParameter("@PageSize", pageSize),                   
                    new SqlParameter("@IdRecuperacion",IdRecuperacion)
                };
                paras[0].Direction = ParameterDirection.Output;

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "sp_get_Meterial_Old_Equipment", paras);
                int.TryParse(paras[0].Value.ToString(), out pageCount);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Meterial include Products failed:Execute method GetMeterialOldEquipment  in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
            return dtResult;
        }
        //end add

        //added by tina 2012-07-12
        /// <summary>
        /// get equipment not registry all material before timeline
        /// </summary>
        /// <param name="disposalID"></param>
        /// <param name="disposalType"></param>
        /// <returns></returns>
        public DataTable GetEquipmentNotRegistryAllMeterialBeforeTimeLine(int disposalID, string disposalType)
        {
            DataTable dtResult = null;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT CS.Id_Credito_Sustitucion FROM K_CREDITO_SUSTITUCION CS");
                sql.Append(" INNER JOIN CRE_Credito KC ON CS.No_Credito=KC.No_Credito");//added by tina 2012-08-09
                sql.Append(" INNER JOIN K_INHABILITACION_PRODUCTO IP ON CS.Id_Credito_Sustitucion=IP.Id_Credito_Sustitucion");
                sql.Append(" INNER JOIN K_INHABILITACION I ON IP.Id_Inhabilitacion=I.Id_Inhabilitacion");
                sql.Append(" LEFT JOIN K_RECUPERACION_PRODUCTO RP ON CS.Id_Credito_Sustitucion=RP.Id_Credito_Sustitucion");
                sql.Append(" LEFT JOIN K_RECUPERACION R ON RP.Id_Recuperacion=R.Id_Recuperacion");
                //updated by tina 2012-08-09
                sql.Append(" LEFT JOIN(SELECT COUNT(DISTINCT Id_Orden) MeterialCount,Cve_Tecnologia FROM K_TECNOLOGIA_RESIDUO_MATERIAL GROUP BY Cve_Tecnologia) TM");
                //end updated
                sql.Append(" ON CS.Cve_Tecnologia=TM.Cve_Tecnologia");
                sql.Append(" WHERE I.Dt_Fecha_Inhabilitacion IS NOT NULL");
                sql.Append(" AND DATEDIFF(d,I.Dt_Fecha_Inhabilitacion,getdate())>5");
                sql.Append(" AND CS.Id_Centro_Disp=@DisposalID AND CS.Fg_Tipo_Centro_Disp=@DisposalType");
                sql.Append(" GROUP BY CS.Id_Credito_Sustitucion");
                sql.Append(" HAVING SUM(CASE ISNULL(R.Cve_Residuo_Material,0) WHEN 0 THEN 0 ELSE 1 END)<MAX(TM.MeterialCount)");

                SqlParameter[] paras = new SqlParameter[] {                   
                    new SqlParameter("@DisposalID", disposalID),
                    new SqlParameter("@DisposalType", disposalType)
                };

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, sql.ToString(), paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Products not registry all meterial before timeline failed:Execute method EquipmentNotRegistryAllMeterialBeforeTimeLine  in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
            return dtResult;
        }

        /// <summary>
        /// get equipment not disable before time line
        /// </summary>
        /// <param name="disposalID"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public DataTable IsAllowReceiptOldEquipment(int disposalID, string userType)
        {
            DataTable Result = new DataTable();
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT CS.Id_Credito_Sustitucion FROM K_CREDITO_SUSTITUCION CS");
                sql.Append(" INNER JOIN K_CREDITO_SUSTITUCION_EXTENSION CSE");
                sql.Append(" ON CS.Id_Credito_Sustitucion=CSE.Id_Credito_Sustitucion");
                sql.Append(" WHERE CS.Id_Credito_Sustitucion NOT IN");
                sql.Append(" (SELECT Id_Credito_Sustitucion FROM K_INHABILITACION_PRODUCTO)");
                sql.Append(" AND DATEDIFF(d,CSE.Dt_Fecha_Imagen_Recepcion,getdate())>3");
                sql.Append(" AND CS.Id_Centro_Disp=@DisposalID AND CS.Fg_Tipo_Centro_Disp=@DisposalType");
                sql.Append(" AND CS.No_Serial NOT LIKE 'CANCEL%'");


                SqlParameter[] para = new SqlParameter[] {                   
                    new SqlParameter("@DisposalID",disposalID),
                    new SqlParameter("@DisposalType",userType)                    
                };
                Result = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, sql.ToString(), para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "failed:Execute method  IsAllowReceiptOldEquipment in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
            return Result;
        }
        //end

        /// <summary>
        /// Get received products
        /// </summary>
        /// <param name="program">program</param>
        /// <returns></returns>
        public DataTable GetOldestReceivedProductsWithoutImage(string program, int disposalID, string disposalType)
        {
            DataTable TableResult = null;

            string query = @"select top (1) Id_Folio, Dt_Fecha_Recepcion Dt_Fecha_Recepcion from dbo.K_CREDITO_SUSTITUCION
                join CRE_Credito on K_CREDITO_SUSTITUCION.No_Credito = CRE_Credito.No_Credito
                where Id_Folio IS NOT NULL and Dt_Fecha_Recepcion IS NOT NULL
                and Fg_Si_Funciona = 1 and Id_Folio not like 'cancel%'
                AND Dx_Imagen_Recepcion is null
                AND Id_Centro_Disp=@Id_Centro_Disp
                AND Fg_Tipo_Centro_Disp=@Fg_Tipo_Centro_Disp";

            if (program != "")
            {
                query += " AND ID_Prog_Proy=" + program;
            }

            query += " order by Dt_Fecha_Recepcion, Id_Folio";

            try
            {
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@program", program),
                    new SqlParameter("@Id_Centro_Disp", disposalID),
                    new SqlParameter("@Fg_Tipo_Centro_Disp", disposalType)
                };

                TableResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, query, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Oldest Received Products failed:Execute method  GetOldestReceivedProductsWithoutImage in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }

            return TableResult;
        }

        /// <summary>
        /// Get received products
        /// </summary>
        /// <param name="program">program</param>
        /// <returns></returns>
        public DataTable GetOldestInHabilitacionProductsWithoutImage(string program, int disposalID, string disposalType)
        {
            DataTable TableResult = null;

            string query = @"select top (1) k.Id_Folio, i2.Dt_Fecha_Inhabilitacion from dbo.K_CREDITO_SUSTITUCION k
                join CRE_Credito c on k.No_Credito = c.No_Credito
                join K_INHABILITACION_PRODUCTO i on k.Id_Credito_Sustitucion = i.Id_Credito_Sustitucion
                join K_INHABILITACION i2 on i.Id_Inhabilitacion = i2.Id_Inhabilitacion
                where k.Id_Folio IS NOT NULL and i2.Dt_Fecha_Inhabilitacion IS NOT NULL
                and k.Fg_Si_Funciona = 1 and k.Id_Folio not like 'cancel%'
                and k.Dx_Imagen_Recepcion is not null and k.Dx_Imagen_Inhabilitacion is null
                and k.Id_Centro_Disp = @Id_Centro_Disp and k.Fg_Tipo_Centro_Disp = @Fg_Tipo_Centro_Disp";

            if (program != "")
            {
                query += " AND ID_Prog_Proy=" + program;
            }

            query += " order by i2.Dt_Fecha_Inhabilitacion, k.Id_Folio";

            try
            {
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@program", program),
                    new SqlParameter("@Id_Centro_Disp", disposalID),
                    new SqlParameter("@Fg_Tipo_Centro_Disp", disposalType)
                };

                TableResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, query, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Oldest Received Products failed:Execute method  GetOldestReceivedProductsWithoutImage in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }

            return TableResult;
        }

    }
}
