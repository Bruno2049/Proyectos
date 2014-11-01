/*	
	$Author:     coco,wang
	$Date:       2011-07-07	
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using System.Text.RegularExpressions;

namespace PAEEEM.DataAccessLayer
{
    /// <summary>
    /// Data Access Layer for product
    /// </summary>
    public class CAT_PRODUCTODal
    {
        /// <summary>
        /// Instance Field
        /// </summary>
        private static readonly CAT_PRODUCTODal _classinstance = new CAT_PRODUCTODal();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static CAT_PRODUCTODal ClassInstance { get { return _classinstance; } }
        /// <summary>
        /// Get product with technology
        /// </summary>
        /// <param name="strTechnology"></param>
        /// <param name="strMarca"></param>
        /// <returns></returns>
        public DataTable Get_CAT_PRODUCTO_ByTechnology(string strTechnology,string strMarca)
        {
            try
            {
                string SQL = "select * from CAT_PRODUCTO where 1=1 and Cve_Estatus_Producto<>@Cve_Estatus_Producto";
                if (!strTechnology.Equals(""))
                { 
                    SQL = SQL + " And Cve_Tecnologia=@Cve_Tecnologia";
                }
                if (!strMarca.Equals(""))
                {
                    SQL = SQL + " And Cve_Marca=@Cve_Marca";
                }
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Cve_Tecnologia", strTechnology),
                    new SqlParameter("@Cve_Marca",strMarca),
                    new SqlParameter("@Cve_Estatus_Producto",ProductStatus.CANCELADO)
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL,paras);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        /// <summary>
        /// get Cat_Producto by pk
        /// </summary>
        /// <param name="strProductID"></param>
        /// <returns></returns>
        public DataTable Get_CAT_PRODUCTO_ByPK(string strProductID)
        {
            try
            {
                // updated by tina 2012-02-27
                string SQL = "select A.*,B.No_Capacidad from CAT_PRODUCTO A left join CAT_CAPACIDAD_SUSTITUCION B on A.Cve_Capacidad_Sust=B.Cve_Capacidad_Sust where Cve_Producto=@Cve_Producto ";
               
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Cve_Producto", strProductID)                   
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
        /// Get proveedor branch with primary key
        /// </summary>
        /// <param name="strBranchID"></param>
        /// <returns></returns>
        public DataTable Get_CAT_PRODUCTO_BranchByPK(string strBranchID)
        {
            try
            {
                string SQL = "select * from [CAT_PROVEEDORBRANCH] where Id_Branch=@Id_Branch ";

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Id_Branch", strBranchID)                   
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        
        //edit by coco 20110823
        /// <summary>
        /// Get products with technology and program, brand and product type
        /// </summary>
        /// <param name="strTechnology">technology</param>
        /// <param name="strMarca">brand</param>
        /// <param name="strProgram">program</param>
        /// <param name="strTypeOfProduct">product type</param>
        /// <returns></returns>
        public DataTable Get_CAT_PRODUCTO_ByTechnologyAndProgram(string strTechnology, string strMarca, string strProgram, string strTypeOfProduct) 
        {
            try
            {
                string SQL = "select A.* from CAT_PRODUCTO A inner join K_PROVEEDOR_PRODUCTO B on A.Cve_Producto=B.Cve_Producto and B.Id_Proveedor=@Id_Proveedor where 1=1   ";                
                          SQL = SQL + " And A.Cve_Tecnologia in ("+ strTechnology+")";                
                if (!strMarca.Equals(""))
                {
                    SQL = SQL + " And A.Cve_Marca=@Cve_Marca";
                }
                if (!strTypeOfProduct.Equals(""))
                {
                    SQL = SQL + " And A.Ft_Tipo_Producto=@Ft_Tipo_Producto";
                }
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Cve_Tecnologia", strTechnology),
                    new SqlParameter("@Id_Proveedor",strProgram),
                    new SqlParameter("@Ft_Tipo_Producto",strTypeOfProduct),
                    new SqlParameter("@Cve_Marca",strMarca)
                };
               //end edit
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        //add by coco 20110824
        // RSA 2012-09-11 Start
        /// <summary>
        /// Get products for SE Description
        /// </summary>
        /// <returns></returns>
        public String Get_CAT_PRODUCTO_ForSE_Description(string productId)
        {
            string result = string.Empty;
            try
            {
                string SQL = "SELECT DISTINCT 'Tipo {' + atb1.Dx_Nombre_Tipo + '}, Transformador {' + atb2.Dx_Nombre_Fase + '} de {' + Cast(atb3.No_Capacidad as varchar(50)) + ' ' + atb3.Dx_Unidades + '}, marca {' + atb4.Dx_Nombre_Marca + '} con relacion de transformacion {' + atb5.Dx_Dsc_Relacion + '}, Apartarrayos marca {' +";
                SQL += " atb6.Dx_Nombre_Marca + '} {' + atb7.Dx_Dsc_Apartarrayo + '}, Corta circuitos-fusibles (cuchillas) marca {' + atb8.Dx_Nombre_Marca + '} {' + atb9.Dx_Dsc_Cortacircuito + '}, Interruptor Termomagnetico marca {' + atb10.Dx_Nombre_Marca + '} {' + atb11.Dx_Dsc_Interruptor + '}, 10 mts. Conductor de tierra marca {' + atb12.Dx_Nombre_Marca + '} {' + atb13.Dx_Dsc_Conductor + '}, 15 mts. Conductor marca {' + atb14.Dx_Nombre_Marca + '} calibre {' + atb15.Dx_Dsc_Conductor_Conex + '}.' AS 'Descripcion' ";
                SQL += " FROM dbo.CAT_PRODUCTO AS prod WITH(NOLOCK)";
                SQL += " INNER JOIN dbo.CAT_SE_TIPO AS atb1 WITH(NOLOCK) ON prod.Cve_Tipo_SE = atb1.Cve_Tipo";
                SQL += " INNER JOIN dbo.CAT_SE_TRANSFORM_FASE AS atb2 WITH(NOLOCK) ON prod.Cve_Fase_Transformador = atb2.Cve_Fase";
                //SQL += " INNER JOIN dbo.CAT_SE_TRANSFORMADOR AS atb3 WITH(NOLOCK) ON prod.Cve_Fase_Transformador = atb3.Cve_Transformador";
                SQL += " INNER JOIN dbo.CAT_CAPACIDAD_SUSTITUCION AS atb3 WITH(NOLOCK) ON prod.Cve_Capacidad_Sust = atb3.Cve_Capacidad_Sust";
                SQL += " INNER JOIN dbo.CAT_SE_TRANSFORM_MARCA AS atb4 WITH(NOLOCK) ON prod.Cve_Marca_Transform = atb4.Cve_Marca";
                SQL += " INNER JOIN dbo.CAT_SE_TRANSFORM_RELACION AS atb5 WITH(NOLOCK) ON prod.Cve_Relacion_Transform = atb5.Cve_Relacion";
                SQL += " INNER JOIN dbo.CAT_SE_APARTARRAYO_MARCA AS atb6 WITH(NOLOCK) ON prod.Cve_Marca_Apartar = atb6.Cve_Marca";
                SQL += " INNER JOIN dbo.CAT_SE_APARTARRAYO AS atb7 WITH(NOLOCK) ON prod.Cve_Apartarrayos_SE = atb7.Cve_Apartarrayo";
                SQL += " INNER JOIN dbo.CAT_SE_CORTACIRC_MARCA AS atb8 WITH(NOLOCK) ON prod.Cve_Marca_Cortacirc = atb8.Cve_Marca";
                SQL += " INNER JOIN dbo.CAT_SE_CORTACIRCUITO AS atb9 WITH(NOLOCK) ON prod.Cve_Cortacircuito_SE = atb9.Cve_Cortacircuito ";
                SQL += " INNER JOIN dbo.CAT_SE_INTERRUPTOR_MARCA AS atb10 WITH(NOLOCK) ON prod.Cve_Marca_Interrup = atb10.Cve_Marca";
                SQL += " INNER JOIN dbo.CAT_SE_INTERRUPTOR AS atb11 WITH(NOLOCK) ON prod.Cve_Interruptor_SE = atb11.Cve_Interruptor";
                SQL += " INNER JOIN dbo.CAT_SE_CONDUCTOR_MARCA AS atb12 WITH(NOLOCK) ON prod.Cve_Marca_Conductor = atb12.Cve_Marca";
                SQL += " INNER JOIN dbo.CAT_SE_CONDUCTOR AS atb13 WITH(NOLOCK) ON prod.Cve_Conductor_SE = atb13.Cve_Conductor";
                SQL += " INNER JOIN dbo.CAT_SE_COND_CONEXION_MARCA AS atb14 WITH(NOLOCK) ON prod.Cve_Cond_Conex_Mca = atb14.Cve_Marca";
                SQL += " INNER JOIN dbo.CAT_SE_COND_CONEXION AS atb15 WITH(NOLOCK) ON prod.Cve_Cond_Conex_SE = atb15.Cve_Conductor_Conex";
                SQL += " WHERE Cve_Producto = @productId";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@productId",productId)
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);

                if (dt.Rows.Count > 0)
                    result = dt.Rows[0][0].ToString();
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }

            return result;
        }

        public int GetSEProductComboMax(int productId, int cveTipoSE, int cveFaseTransformador, int cveTransformadorSE)
        {
            int result = 0;
            try
            {
                string SQL = "Select MAX(Dx_Nombre_Producto) From dbo.CAT_PRODUCTO (nolock) Where Cve_Tipo_SE = @Cve_Tipo_SE AND Cve_Fase_Transformador = @Cve_Fase_Transformador AND Cve_Transformador_SE = @Cve_Transformador_SE AND Cve_Producto <> @productId";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@productId", productId),
                    new SqlParameter("@Cve_Tipo_SE", cveTipoSE),
                    new SqlParameter("@Cve_Fase_Transformador", cveFaseTransformador),
                    new SqlParameter("@Cve_Transformador_SE", cveTransformadorSE)
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);

                if (dt.Rows.Count > 0 && !string.IsNullOrEmpty(dt.Rows[0][0].ToString()))
                {
                    Match m = Regex.Match(dt.Rows[0][0].ToString(), @".* (\d+)$");
                    if (m.Success)
                        result = Convert.ToInt32(m.Groups[1].Value);
                    else  // The first element has no counter
                        result = 1;
                }
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }

            return result;
        }
        // RSA 2012-09-11 END
        /// <summary>
        /// Get product
        /// </summary>
        /// <param name="strTechnology"></param>
        /// <param name="strProvider"></param>
        /// <param name="strTypeProduct"></param>
        /// <returns></returns>
        public DataTable Get_CAT_PRODUCTO_ByTechnology_1(string strTechnology,string strProvider,string strTypeProduct)
        {
            try
            {
                string SQL = "select distinct A. * from CAT_PRODUCTO A inner join CAT_TECNOLOGIA B  ";
                SQL = SQL + " on A.Cve_Tecnologia=B.Cve_Tecnologia and B.Cve_Tecnologia in ("+ strTechnology +") ";
                SQL = SQL + " inner join K_PROVEEDOR_PRODUCTO D on D.Cve_Producto=A.Cve_Producto and D.Id_Proveedor=@Id_Proveedor ";
                SQL = SQL + " where A.Ft_Tipo_Producto in (select  C.Ft_Tipo_Producto from CAT_TIPO_PRODUCTO C where C.Cve_Tecnologia in ("+ strTechnology +"))";
                if (!strTypeProduct.Equals(""))
                {
                    SQL = SQL + "  and A.Ft_Tipo_Producto =@Ft_Tipo_Producto";
                }
               SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Cve_Tecnologia", strTechnology),
                    new SqlParameter("@Id_Proveedor",strProvider) ,
                    new SqlParameter("@Ft_Tipo_Producto",strTypeProduct)
                };
                
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        //end edit
        /// <summary>
        /// Get Product list for Central Office Authorization
        /// </summary>
        /// <param name="Manufacture"></param>
        /// <param name="Technology"></param>
        /// <param name="TipoOfProduct"></param>
        /// <param name="Marca"></param>
        /// <param name="Estatus"></param>
        /// <param name="sortName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable GetProductsList(string Manufacture, string Technology, string TipoOfProduct, string Marca, string Estatus, string sortName, int pageIndex, int pageSize, out int pageCount)
        {
            DataTable dtResult = null;
            StringBuilder strWhere=new StringBuilder();
            try
            {
                strWhere.Append(" WHERE 1=1");
                if (Manufacture != "")
                {
                    strWhere.Append(" ").Append(" AND Cve_Fabricante='" + Manufacture + "'");
                }
                if (Technology != "")
                {
                    strWhere.Append(" ").Append(" AND Cve_Tecnologia ='" + Technology + "'");
                }
                if (TipoOfProduct !="")
                {
                    strWhere.Append(" ").Append(" AND Ft_Tipo_Producto='" +TipoOfProduct +"'");
                }
                if (Marca != "")
                {
                    strWhere.Append(" ").Append(" AND Cve_Marca='" + Marca + "'");
                }
                if (Estatus != "")
                {
                    strWhere.Append(" ").Append(" AND Cve_Estatus_Producto='"+ Estatus +"'");
                }

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@PageCount", SqlDbType.Int),
                    new SqlParameter("@SortName", sortName),
                    new SqlParameter("@PageIndex", pageIndex),
                    new SqlParameter("@PageSize", pageSize),
                    new SqlParameter("@spWhere", strWhere.ToString())
                };
                paras[0].Direction = ParameterDirection.Output;

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "sp_get_Product_List", paras);
                int.TryParse(paras[0].Value.ToString(), out pageCount);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Products List failed:Execute method  GetProductsList in CAT_PRODUCTODal.", ex, true);
            }
            return dtResult;
        }
        /// <summary>
        /// Active Products or Desactive Products or Cancel Products
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public int UpdateProductStatus(string ProductID,int Status)
        {
            int Result = 0;
            try
            {
                string Sql = "Update CAT_PRODUCTO set Cve_Estatus_Producto=@Cve_Estatus_Producto where Cve_Producto in(" + ProductID + ")";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Cve_Estatus_Producto", Status)
                  };
                Result = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, Sql, paras);
            }
            catch (Exception ex)
            {
                throw new LsDAException(this, "Change Products Status failed:Execute method  UpdateProductStatus in CAT_PRODUCTODal.", ex, true);
            }
            return Result;
        }     
        /// <summary>
        /// Add new product
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Insert_CAT_PRODUCTO(CAT_PRODUCTOModel instance)//updated by tina 2012/04/27
        {
            try
            {
                // RSA 20120918 Additional fields for SE, and removal of Mt_Precio_Unitario
                string executesqlstr = "INSERT INTO CAT_PRODUCTO (Cve_Tecnologia, Cve_Fabricante, Cve_Marca, Cve_Estatus_Producto,Dx_Nombre_Producto, Dx_Modelo_Producto, ";
                executesqlstr = executesqlstr + "Cve_Tipo_SE, Cve_Transformador_SE, Cve_Fase_Transformador, Cve_Marca_Transform, Cve_Relacion_Transform,";
                executesqlstr = executesqlstr + "Cve_Apartarrayos_SE, Cve_Marca_Apartar, Cve_Cortacircuito_SE, Cve_Marca_Cortacirc, Cve_Interruptor_SE,";
                executesqlstr = executesqlstr + "Cve_Marca_Interrup, Cve_Conductor_SE, Cve_Marca_Conductor, Cve_Cond_Conex_SE, Cve_Cond_Conex_Mca,";
                executesqlstr = executesqlstr + "Mt_Precio_Max, No_Eficiencia_Energia, No_Max_Consumo_24h, Cve_Capacidad_Sust, Dt_Fecha_Producto, Ft_Tipo_Producto, Dx_Producto_Code,Ahorro_Consumo,Ahorro_Demanda)";
                executesqlstr = executesqlstr + " VALUES (@Cve_Tecnologia, @Cve_Fabricante, @Cve_Marca, @Cve_Estatus_Producto, @Dx_Nombre_Producto, @Dx_Modelo_Producto,";
                executesqlstr = executesqlstr + "@Cve_Tipo_SE, @Cve_Transformador_SE, @Cve_Fase_Transformador, @Cve_Marca_Transform, @Cve_Relacion_Transform,";
                executesqlstr = executesqlstr + "@Cve_Apartarrayos_SE, @Cve_Marca_Apartar, @Cve_Cortacircuito_SE, @Cve_Marca_Cortacirc, @Cve_Interruptor_SE,";
                executesqlstr = executesqlstr + "@Cve_Marca_Interrup, @Cve_Conductor_SE, @Cve_Marca_Conductor, @Cve_Cond_Conex_SE, @Cve_Cond_Conex_Mca,";
                executesqlstr = executesqlstr + "@Mt_Precio_Max, @No_Eficiencia_Energia, @No_Max_Consumo_24h, @Cve_Capacidad_Sust, @Dt_Fecha_Producto, @Ft_Tipo_Producto, @Dx_Producto_Code,@Ahorro_consumo,@Ahorro_Demanda)";

                SqlParameter[] para = new SqlParameter[] { 
                	new SqlParameter("@Cve_Tecnologia",instance.Cve_Tecnologia),
                	new SqlParameter("@Cve_Fabricante",instance.Cve_Fabricante),
                	new SqlParameter("@Cve_Marca",instance.Cve_Marca),
                	new SqlParameter("@Cve_Estatus_Producto",instance.Cve_Estatus_Producto),
                	new SqlParameter("@Dx_Nombre_Producto",instance.Dx_Nombre_Producto),
                	new SqlParameter("@Dx_Modelo_Producto",instance.Dx_Modelo_Producto),
                	new SqlParameter("@Mt_Precio_Max",instance.Mt_Precio_Max),
                	new SqlParameter("@No_Eficiencia_Energia",instance.No_Eficiencia_Energia),
                	new SqlParameter("@No_Max_Consumo_24h",instance.No_Max_Consumo_24h),
                	new SqlParameter("@Cve_Capacidad_Sust",instance.Cve_Capacidad_Sust),
                	new SqlParameter("@Dt_Fecha_Producto",instance.Dt_Fecha_Producto),
                	new SqlParameter("@Ft_Tipo_Producto",instance.Ft_Tipo_Producto),
                	new SqlParameter("@Dx_Producto_Code",instance.Dx_Producto_Code),
                	new SqlParameter("@Ahorro_consumo",instance.Ahorro_Consumo),
                 	new SqlParameter("@Ahorro_Demanda",instance.Ahorro_Demanda),

                    new SqlParameter("@Cve_Tipo_SE",instance.Cve_Tipo_SE),
                    new SqlParameter("@Cve_Transformador_SE",instance.Cve_Transformador_SE),
                    new SqlParameter("@Cve_Fase_Transformador",instance.Cve_Fase_Transformador),
                    new SqlParameter("@Cve_Marca_Transform",instance.Cve_Marca_Transform),
                    new SqlParameter("@Cve_Relacion_Transform",instance.Cve_Relacion_Transform),
                    new SqlParameter("@Cve_Apartarrayos_SE",instance.Cve_Apartarrayos_SE),
                    new SqlParameter("@Cve_Marca_Apartar",instance.Cve_Marca_Apartar),
                    new SqlParameter("@Cve_Cortacircuito_SE",instance.Cve_Cortacircuito_SE),
                    new SqlParameter("@Cve_Marca_Cortacirc",instance.Cve_Marca_Cortacirc),
                    new SqlParameter("@Cve_Interruptor_SE",instance.Cve_Interruptor_SE),
                    new SqlParameter("@Cve_Marca_Interrup",instance.Cve_Marca_Interrup),
                    new SqlParameter("@Cve_Conductor_SE",instance.Cve_Conductor_SE),
                    new SqlParameter("@Cve_Marca_Conductor",instance.Cve_Marca_Conductor),
                    new SqlParameter("@Cve_Cond_Conex_SE",instance.Cve_Cond_Conex_SE),
                    new SqlParameter("@Cve_Cond_Conex_Mca",instance.Cve_Cond_Conex_Mca)
            	};

                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Add new CAT_PRODUCTO failed", ex, true);
            }
        }
        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="instance"></param>       
        /// <returns></returns>
        public int UpdateProductSInformation(CAT_PRODUCTOModel instance)//updated by tina 2012/04/27
        {
            int Result = 0;
            try
            {
                // RSA 20120918 Additional fields for SE, and removal of Mt_Precio_Unitario
                string Sql = "Update CAT_PRODUCTO set Cve_Tecnologia=@Cve_Tecnologia,Cve_Fabricante=@Cve_Fabricante,Cve_Marca=@Cve_Marca,Cve_Estatus_Producto=@Cve_Estatus_Producto,";
                Sql = Sql + "Dx_Nombre_Producto=@Dx_Nombre_Producto,Dx_Modelo_Producto=@Dx_Modelo_Producto,Mt_Precio_Max=@Mt_Precio_Max,No_Eficiencia_Energia=@No_Eficiencia_Energia,";
                Sql = Sql + "No_Max_Consumo_24h=@No_Max_Consumo_24h,Cve_Capacidad_Sust=@Cve_Capacidad_Sust,Dt_Fecha_Producto=@Dt_Fecha_Producto,Ft_Tipo_Producto=@Ft_Tipo_Producto,";
                Sql = Sql + "Ahorro_Consumo=@Ahorro_Consumo,Ahorro_Demanda=@Ahorro_Demanda";
                Sql = Sql + ", Cve_Tipo_SE=@Cve_Tipo_SE, Cve_Transformador_SE=@Cve_Transformador_SE, Cve_Fase_Transformador=@Cve_Fase_Transformador";
                Sql = Sql + ", Cve_Marca_Transform=@Cve_Marca_Transform, Cve_Relacion_Transform=@Cve_Relacion_Transform, Cve_Apartarrayos_SE=@Cve_Apartarrayos_SE";
                Sql = Sql + ", Cve_Marca_Apartar=@Cve_Marca_Apartar, Cve_Cortacircuito_SE=@Cve_Cortacircuito_SE, Cve_Marca_Cortacirc=@Cve_Marca_Cortacirc";
                Sql = Sql + ", Cve_Interruptor_SE=@Cve_Interruptor_SE, Cve_Marca_Interrup=@Cve_Marca_Interrup, Cve_Conductor_SE=@Cve_Conductor_SE";
                Sql = Sql + ", Cve_Marca_Conductor=@Cve_Marca_Conductor, Cve_Cond_Conex_SE=@Cve_Cond_Conex_SE, Cve_Cond_Conex_Mca=@Cve_Cond_Conex_Mca";
                Sql = Sql + " where Cve_Producto=@Cve_Producto";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Cve_Tecnologia", instance.Cve_Tecnologia),
                    new SqlParameter("@Cve_Fabricante", instance.Cve_Fabricante),
                    new SqlParameter("@Cve_Marca", instance.Cve_Marca),
                    new SqlParameter("@Cve_Estatus_Producto", instance.Cve_Estatus_Producto),
                    new SqlParameter("@Dx_Nombre_Producto", instance.Dx_Nombre_Producto),
                    new SqlParameter("@Dx_Modelo_Producto", instance.Dx_Modelo_Producto),
                    new SqlParameter("@Mt_Precio_Max", instance.Mt_Precio_Max),
                    new SqlParameter("@No_Eficiencia_Energia", instance.No_Eficiencia_Energia),
                    new SqlParameter("@No_Max_Consumo_24h", instance.No_Max_Consumo_24h),
                    new SqlParameter("@Cve_Capacidad_Sust", instance.Cve_Capacidad_Sust),
                    new SqlParameter("@Dt_Fecha_Producto", instance.Dt_Fecha_Producto),
                    new SqlParameter("@Ft_Tipo_Producto", instance.Ft_Tipo_Producto),
                    new SqlParameter("@Cve_Producto",instance.Cve_Producto),
                    new SqlParameter("@Ahorro_Consumo",instance.Ahorro_Consumo),
                    new SqlParameter("@Ahorro_Demanda",instance.Ahorro_Demanda),
                 
                    new SqlParameter("@Cve_Tipo_SE",instance.Cve_Tipo_SE),
                    new SqlParameter("@Cve_Transformador_SE",instance.Cve_Transformador_SE),
                    new SqlParameter("@Cve_Fase_Transformador",instance.Cve_Fase_Transformador),
                    new SqlParameter("@Cve_Marca_Transform",instance.Cve_Marca_Transform),
                    new SqlParameter("@Cve_Relacion_Transform",instance.Cve_Relacion_Transform),
                    new SqlParameter("@Cve_Apartarrayos_SE",instance.Cve_Apartarrayos_SE),
                    new SqlParameter("@Cve_Marca_Apartar",instance.Cve_Marca_Apartar),
                    new SqlParameter("@Cve_Cortacircuito_SE",instance.Cve_Cortacircuito_SE),
                    new SqlParameter("@Cve_Marca_Cortacirc",instance.Cve_Marca_Cortacirc),
                    new SqlParameter("@Cve_Interruptor_SE",instance.Cve_Interruptor_SE),
                    new SqlParameter("@Cve_Marca_Interrup",instance.Cve_Marca_Interrup),
                    new SqlParameter("@Cve_Conductor_SE",instance.Cve_Conductor_SE),
                    new SqlParameter("@Cve_Marca_Conductor",instance.Cve_Marca_Conductor),
                    new SqlParameter("@Cve_Cond_Conex_SE",instance.Cve_Cond_Conex_SE),
                    new SqlParameter("@Cve_Cond_Conex_Mca",instance.Cve_Cond_Conex_Mca),
                  };
                Result = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, Sql, paras);
            }
            catch (Exception ex)
            {
                throw new LsDAException(this, "Change Products Information failed:Execute method UpdateProductSInformation  in CAT_PRODUCTODal.", ex, true);
            }
            return Result;
        }

        //add by coco 2012-07-17 
        public DataTable GetProductModeloByProductType(string ProductType)
        {
            try
            {
                string SQL = "select distinct Dx_Modelo_Producto from CAT_PRODUCTO ";
                if (!ProductType.Equals(""))
                {
                    SQL = SQL + "  where Ft_Tipo_Producto=@Ft_Tipo_Producto ";
                }
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Ft_Tipo_Producto", ProductType)                   
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        //end add by coco

        // RSA 2013-02-18 Ini
        public DataTable GetModelByTechnologyAndMarca(string technology, string marca)
        {
            try
            {
                string SQL = "select distinct Dx_Modelo_Producto from CAT_PRODUCTO where 1=1 and Cve_Estatus_Producto<>@Cve_Estatus_Producto";
                if (!technology.Equals(""))
                {
                    SQL = SQL + " And Cve_Tecnologia=@Cve_Tecnologia";
                }
                if (!marca.Equals(""))
                {
                    SQL = SQL + " And Cve_Marca=@Cve_Marca";
                }
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Cve_Tecnologia", technology),
                    new SqlParameter("@Cve_Marca",marca),
                    new SqlParameter("@Cve_Estatus_Producto",ProductStatus.CANCELADO)
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }

        public DataTable GetMarcaByTechnology(string technology)
        {
            try
            {
                string SQL = "select distinct m.Cve_Marca, m.Dx_Marca from CAT_PRODUCTO  p Join CAT_MARCA  m On p.Cve_Marca = m.Cve_Marca where 1=1 and Cve_Estatus_Producto<>@Cve_Estatus_Producto";
                if (!technology.Equals(""))
                {
                    SQL = SQL + " And Cve_Tecnologia=@Cve_Tecnologia";
                }
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Cve_Tecnologia", technology),
                    new SqlParameter("@Cve_Estatus_Producto",ProductStatus.CANCELADO)
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        // RSA 2013-02-18 Fin
        
        //added by tina 2012-07-18
        public DataTable GetProductNameByTechnologyAndMarca(string technology, string marca)
        {
            try
            {
                string SQL = "select distinct Dx_Nombre_Producto, Ft_Tipo_Producto from CAT_PRODUCTO where 1=1 and Cve_Estatus_Producto<>@Cve_Estatus_Producto";
                if (!technology.Equals(""))
                {
                    SQL = SQL + " And Cve_Tecnologia=@Cve_Tecnologia";
                }
                if (!marca.Equals(""))
                {
                    SQL = SQL + " And Cve_Marca=@Cve_Marca";
                }
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Cve_Tecnologia", technology),
                    new SqlParameter("@Cve_Marca",marca),
                    new SqlParameter("@Cve_Estatus_Producto",ProductStatus.CANCELADO)
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        public DataTable GetModelByProductName(string productName)
        {
            try
            {
                string SQL = "select distinct Dx_Modelo_Producto from CAT_PRODUCTO where 1=1 and Cve_Estatus_Producto<>@Cve_Estatus_Producto";
                if (!productName.Equals(""))
                {
                    SQL = SQL + " And Ft_Tipo_Producto=@ProductName";
                }
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@ProductName", productName),
                    new SqlParameter("@Cve_Estatus_Producto",ProductStatus.CANCELADO)
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        public DataTable GetProductInfoByModel(string model)
        {
            try
            {
                string SQL = "select top 1 * from CAT_PRODUCTO where 1=1 and Cve_Estatus_Producto<>@Cve_Estatus_Producto And Dx_Modelo_Producto=@Model";

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Model", model),
                    new SqlParameter("@Cve_Estatus_Producto",ProductStatus.CANCELADO)
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        //end
    }
}
