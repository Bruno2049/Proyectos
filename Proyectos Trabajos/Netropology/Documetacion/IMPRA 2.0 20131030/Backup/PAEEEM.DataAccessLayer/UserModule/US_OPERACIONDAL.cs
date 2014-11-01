/*
	Copyright IMPRA, Inc. 2010
	All rights are reserved. Reproduction or transmission in whole or in part,
      in any form or by any means, electronic, mechanical or otherwise, is 
prohibited without the prior written consent of the copyright owner.

	$Archive:    $
	$Revision:   $
	$Author:     $
	$Date:       $
	Log at end of file
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using PAEEEM.Entities;

namespace PAEEEM.DataAccessLayer
{
    /// <summary>
    /// Operation Permission Data Access Layer
    /// </summary>
    public class US_OPERACIONDal
    {
        private static readonly US_OPERACIONDal _classinstance = new US_OPERACIONDal();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static US_OPERACIONDal ClassInstance {get{return _classinstance; }}
        
        /// <summary>
        /// Add Record
        /// </summary>
        public int Insert_US_OPERACION(US_OPERACIONModel instance)
        {
            string executesqlstr =  "INSERT INTO US_OPERACION (Id_Navegacion, Nombre_Operacion, Estatus_Operacion ) VALUES (@Id_Navegacion, @Nombre_Operacion, @Estatus_Operacion)";
            
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@Id_Navegacion",instance.Id_Navegacion),
                new SqlParameter("@Nombre_Operacion",instance.Nombre_Operacion),
                new SqlParameter("@Estatus_Operacion",instance.Estatus_Operacion)
            };
            
            return SqlHelper.ExecuteNonQuery( ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
        }
        
        /// <summary>
        /// Delete Record
        /// </summary>
        public int Delete_US_OPERACION(String pkid)
        {
            string executesqlstr = "DELETE US_OPERACION WHERE Id_Operacion = @Id_Operacion";
            
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@Id_Operacion",pkid)
            };
            
            return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
        }
        
        /// <summary>
        /// Update Record
        /// </summary>
        public int Update_US_OPERACION(US_OPERACIONModel instance)
        {
            string executesqlstr = "UPDATE US_OPERACION SET Id_Navegacion = @Id_Navegacion, Nombre_Operacion = @Nombre_Operacion, Estatus_Operacion = @Estatus_Operacion WHERE Id_Operacion = @Id_Operacion";
            
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@Id_Operacion",instance.Id_Operacion),
                new SqlParameter("@Id_Navegacion",instance.Id_Navegacion),
                new SqlParameter("@Nombre_Operacion",instance.Nombre_Operacion),
                new SqlParameter("@Estatus_Operacion",instance.Estatus_Operacion)
            };
            
            return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
        }
        
        /// <summary>
        /// Get Record By Primary Key
        /// </summary>
        public US_OPERACIONModel Get_US_OPERACIONByPKID(String pkid)
        {
            string executesqlstr = "SELECT Id_Operacion, Id_Navegacion, Nombre_Operacion, Estatus_Operacion FROM US_OPERACION WHERE Id_Operacion = @Id_Operacion";
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@Id_Operacion",pkid)
            };
            US_OPERACIONModel modelinstance = new US_OPERACIONModel();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para))
            {
                if (sdr.Read())
                {
                    modelinstance = EvaluateModel(sdr);
                }
            }
            return modelinstance;
        }
        
        /// <summary>
        /// Get Total Record Number
        /// </summary>
        public Int32 Get_US_OPERACIONCount(String tablename, String swhere)
        {
            string sqlstr = "SELECT count(1) AS totalNum FROM " + tablename + " WHERE 1 = 1 "+swhere+" ";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, sqlstr));
        }
        
        /// <summary>
        /// Get Record With Split Page
        /// </summary>
        public DataTable Get_US_OPERACIONListByPager
        (
            String sorder,
            Int32 currentPageIndex,
			Int32 pageSize,
            out Int32 pagecount
        )
        {
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@Count",SqlDbType.Int),			
                new SqlParameter("@OrderBy",sorder),
                new SqlParameter("@CurrentPageIndex",currentPageIndex),
				new SqlParameter("@PageSize",pageSize)

            };
			para[0].Direction = ParameterDirection.Output;
			
			DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "dbo.UP_Pager_GetUS_OPERACIONList", para);

  			int.TryParse(para[0].Value.ToString(), out pagecount);
            return dt;
        }
		
		/// <summary>
        /// Get all Record
        /// </summary>
		public List<US_OPERACIONModel> Get_AllUS_OPERACION()
        {
            List<US_OPERACIONModel> l_instance = new List<US_OPERACIONModel>();
            string strSQL = "SELECT Id_Operacion, Id_Navegacion, Nombre_Operacion, Estatus_Operacion FROM US_OPERACION";
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(ParameterHelper.strCon_DBLsWebApp, CommandType.Text,strSQL))
            {
                while (sdr.Read())
                {
                    US_OPERACIONModel instance = EvaluateModel(sdr);
                    l_instance.Add(instance);
                }
            }

            return l_instance;
        }
        
        /// <summary>
        /// evaluate model
        /// </summary>
        private US_OPERACIONModel EvaluateModel(SqlDataReader sdr)
        {
            US_OPERACIONModel modelinstance = new US_OPERACIONModel();
            modelinstance.Id_Operacion = sdr.IsDBNull(sdr.GetOrdinal("Id_Operacion"))?0: sdr.GetInt32(sdr.GetOrdinal("Id_Operacion"));
            modelinstance.Id_Navegacion = sdr.IsDBNull(sdr.GetOrdinal("Id_Navegacion"))?0: sdr.GetInt32(sdr.GetOrdinal("Id_Navegacion"));
            modelinstance.Nombre_Operacion = sdr.IsDBNull(sdr.GetOrdinal("Nombre_Operacion"))?string.Empty: sdr.GetString(sdr.GetOrdinal("Nombre_Operacion"));
            modelinstance.Estatus_Operacion = sdr.IsDBNull(sdr.GetOrdinal("Estatus_Operacion"))?string.Empty: sdr.GetString(sdr.GetOrdinal("Estatus_Operacion"));
            return modelinstance;
        }
        /// <summary>
        /// Get permissions by navigation code
        /// </summary>
        /// <param name="navID"></param>
        /// <returns></returns>
        public DataTable Get_OperationPermissionByNavCode(string navID)
        {
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@NavID",navID) 
            };
            DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, "SELECT [Id_Navegacion],[Nombre_Navegacion],[Url_Navegacion],[Codigo_Padres],[Ruta_Padres],[Estatus],[Nivel_Navegacion],[Secuencia],[Tipo_Permiso],[Id_Permiso] FROM [dbo].[View_Page_Permission] where Tipo_Permiso='O' and Id_Navegacion = @NavID", para);           
            return dt;
        }
    }
}



 
 
