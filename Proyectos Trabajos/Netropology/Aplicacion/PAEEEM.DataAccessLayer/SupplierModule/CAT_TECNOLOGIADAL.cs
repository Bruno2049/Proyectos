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
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PAEEEM.Entities;
using PAEEEM.Helpers;

namespace PAEEEM.DataAccessLayer
{
    /// <summary>
    /// CAT_TECNOLOGIA data access lay
    /// </summary>
    public class CAT_TECNOLOGIADAL
    {
        private static readonly CAT_TECNOLOGIADAL _classinstance = new CAT_TECNOLOGIADAL();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static CAT_TECNOLOGIADAL ClassInstance { get { return _classinstance; } }
        /// <summary>
        /// Get All the TECNOLOGIA
        /// </summary>
        /// <returns></returns>
        public DataTable Get_All_CAT_TECNOLOGIA()
        {
            try
            {
                // RSA 20120918 Additional field to identify SE
                string SQL = "SELECT Cve_Tecnologia,Dx_Nombre_General,Dx_Nombre_Particular,Dt_Fecha_Tecnologoia, Cve_Tipo_Tecnologia, Dx_Cve_CC FROM CAT_TECNOLOGIA ORDER BY 2";
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get all Estado failed: Execute method Get_All_CAT_TECNOLOGIA in CAT_TECNOLOGIA.", ex, true);
            }
        }
        //add by coco 2011-08-03
        /// <summary>
        /// Get Technology by ProgramID
        /// </summary>
        /// <returns></returns>
        public DataTable Get_All_CAT_TECNOLOGIAByProgram(string Program)
        {
            try
            {
                string SQL = "select A.* from CAT_TECNOLOGIA  A  inner join K_PROG_TECNOLOGIA B on A.Cve_Tecnologia=B.Cve_Tecnologia where 1=1 ";
                if (Program != "")
                {
                    SQL = SQL + "  and B.ID_Prog_Proy=@ID_Prog_Proy";
                }
                SQL = SQL + " ORDER BY Dx_Nombre_Particular";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@ID_Prog_Proy", Program)  
                 };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        // End
        // Add by Tina 2011/08/03
        /// <summary>
        /// Get technology by program and disposal center
        /// </summary>
        /// <param name="Program"></param>
        /// <param name="Disposal"></param>
        /// <returns></returns>
        public DataTable Get_CAT_TECNOLOGIAByProgramAndDisposal(int Program, int Disposal)
        {
            try
            {
                string SQL = "select A.* from CAT_TECNOLOGIA A inner join K_PROG_TECNOLOGIA B on A.Cve_Tecnologia=B.Cve_Tecnologia "+
                                    " inner join K_CENTRO_DISP_TECNOLOGIA C on A.Cve_Tecnologia=C.Cve_Tecnologia "+
                                    " where B.ID_Prog_Proy=@ID_Prog_Proy and C.Id_Centro_Disp=@Id_Centro_Disp";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@ID_Prog_Proy", Program),
                    new SqlParameter("@Id_Centro_Disp", Disposal)
                 };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        // End
        //add by coco 2011-08-05
        /// <summary>
        /// Get technology by id
        /// </summary>
        /// <param name="strTechnologyID">technology id</param>
        /// <returns></returns>
        public DataTable Get_All_CAT_TECNOLOGIATipoByPK(string strTechnologyID)
        {
            try
            {
                // updated by tina 2012-02-27
                string SQL = "select B.* from CAT_TIPO_TECNOLOGIA B left join CAT_TECNOLOGIA A on A.Cve_Tipo_Tecnologia=B.Cve_Tipo_Tecnologia where A.Cve_Tecnologia=@Cve_Tecnologia";
                SqlParameter[] paras = new SqlParameter[] {                     
                    new SqlParameter("@Cve_Tecnologia", strTechnologyID)
                 };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL,paras);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message , ex, true);
            }
        }
        //end add
        //add by coco 2011-08-10
        /// <summary>
        /// Get Technology by ProgramID and productID
        /// </summary>
        /// <returns></returns>
        //edit by coco 20110823 
        public DataTable Get_All_CAT_TECNOLOGIAByProgramAndProductID(string strProgram, string strProductID, string strTypeOfProduct, int Id_Usuario)
        {
            try
            {
                string SQL = "select distinct A.* from CAT_TECNOLOGIA  A  inner join K_PROG_TECNOLOGIA B ";
                SQL = SQL + " on A.Cve_Tecnologia=B.Cve_Tecnologia and B.ID_Prog_Proy=@ID_Prog_Proy ";
                SQL = SQL + " inner join CAT_TIPO_PRODUCTO D on A.Cve_Tecnologia=D.Cve_Tecnologia ";
                SQL = SQL + " inner join dbo.CAT_PRODUCTO P on A.Cve_Tecnologia = P.Cve_Tecnologia";

                if (!strTypeOfProduct.Equals(""))
                {
                    SQL = SQL + "  and D.Ft_Tipo_Producto=@Ft_Tipo_Producto ";
                }
                if (Id_Usuario > 0)
                {
                    SQL = SQL + " inner join dbo.K_PROVEEDOR_PRODUCTO on K_PROVEEDOR_PRODUCTO.Cve_Producto=P.Cve_Producto ";
                 //   SQL = SQL + " inner join [USER_SUPPLIER_ATRIBUTES] v on K_PROVEEDOR_PRODUCTO.Id_Proveedor = v.id_proveedor";
                    SQL = SQL + "inner join (select dbo.US_USUARIO.Id_Usuario, ";
                    SQL = SQL + "	case dbo.US_USUARIO.Tipo_Usuario ";
                    SQL = SQL + "		when 'S' then dbo.US_USUARIO.Id_Departamento ";
                    SQL = SQL + "		when 'S_B' then (select dbo.CAT_PROVEEDORBRANCH.Id_Proveedor from dbo.US_USUARIO (nolock) ";
                    SQL = SQL + "			inner join dbo.CAT_PROVEEDORBRANCH (nolock) on US_USUARIO.Id_Departamento = dbo.CAT_PROVEEDORBRANCH.Id_Branch ";
                    SQL = SQL + "			where dbo.US_USUARIO.Tipo_Usuario ='S_B' and dbo.US_USUARIO.Id_Usuario=@Id_usuario) ";
                    SQL = SQL + "		end AS id_proveedor ";
                    SQL = SQL + "   from dbo.US_USUARIO where  dbo.US_USUARIO.Tipo_Usuario in ('S','S_B') and dbo.US_USUARIO.Id_Usuario=@Id_usuario) v on K_PROVEEDOR_PRODUCTO.Id_Proveedor = v.id_proveedor "; 
                
                }
                SQL = SQL + " where 1 =1 ";
                if (Id_Usuario > 0)
                    SQL = SQL + " and v.Id_Usuario = @Id_Usuario ";
                if (!strProductID.Equals(""))
                {
                    SQL = SQL + " and A.Cve_Tecnologia in (select C.Cve_Tecnologia from CAT_PRODUCTO C where C.Cve_Producto in (" + strProductID + "))";
                }
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@ID_Prog_Proy", strProgram),
                    new SqlParameter("@Ft_Tipo_Producto",strTypeOfProduct),
                    new SqlParameter("@Id_Usuario", Id_Usuario)
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
        public DataTable Get_All_CAT_TECNOLOGIAByProgramAndDxCveCC(string strProgram, int Id_Usuario, string Dx_Cve_CC)
        {
            try
            {
                string SQL = "select distinct A.* from CAT_TECNOLOGIA  A  inner join K_PROG_TECNOLOGIA B ";
                SQL = SQL + " on A.Cve_Tecnologia=B.Cve_Tecnologia and B.ID_Prog_Proy=@ID_Prog_Proy ";
                SQL = SQL + " inner join CAT_TIPO_PRODUCTO D on A.Cve_Tecnologia=D.Cve_Tecnologia ";
                SQL = SQL + " inner join dbo.CAT_PRODUCTO P on A.Cve_Tecnologia = P.Cve_Tecnologia";

                if (Id_Usuario > 0)
                {
                    SQL = SQL + " inner join dbo.K_PROVEEDOR_PRODUCTO on K_PROVEEDOR_PRODUCTO.Cve_Producto=P.Cve_Producto ";
                    //   SQL = SQL + " inner join [USER_SUPPLIER_ATRIBUTES] v on K_PROVEEDOR_PRODUCTO.Id_Proveedor = v.id_proveedor";
                    SQL = SQL + "inner join (select dbo.US_USUARIO.Id_Usuario, ";
                    SQL = SQL + "	case dbo.US_USUARIO.Tipo_Usuario ";
                    SQL = SQL + "		when 'S' then dbo.US_USUARIO.Id_Departamento ";
                    SQL = SQL + "		when 'S_B' then (select dbo.CAT_PROVEEDORBRANCH.Id_Proveedor from dbo.US_USUARIO (nolock) ";
                    SQL = SQL + "			inner join dbo.CAT_PROVEEDORBRANCH (nolock) on US_USUARIO.Id_Departamento = dbo.CAT_PROVEEDORBRANCH.Id_Branch ";
                    SQL = SQL + "			where dbo.US_USUARIO.Tipo_Usuario ='S_B' and dbo.US_USUARIO.Id_Usuario=@Id_usuario) ";
                    SQL = SQL + "		end AS id_proveedor ";
                    SQL = SQL + "   from dbo.US_USUARIO where  dbo.US_USUARIO.Tipo_Usuario in ('S','S_B') and dbo.US_USUARIO.Id_Usuario=@Id_usuario) v on K_PROVEEDOR_PRODUCTO.Id_Proveedor = v.id_proveedor ";

                }
                SQL = SQL + " where 1 = 1";
                if (!Dx_Cve_CC.Equals(""))
                {
                    SQL = SQL + " and Dx_Cve_CC <> @Dx_Cve_CC ";
                }
                
                if (Id_Usuario > 0)
                    SQL = SQL + " and v.Id_Usuario = @Id_Usuario ";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@ID_Prog_Proy", strProgram),
                    new SqlParameter("@Dx_Cve_CC", Dx_Cve_CC),
                    new SqlParameter("@Id_Usuario", Id_Usuario)
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
        // End
        //add by coco 2011-10-09     
       /// <summary>
        ///    //Get Technology by ProgramID and DisposalID
       /// </summary>
        /// <param name="program">program</param>
        /// <param name="disposalCenter">disposal center id</param>
        /// <param name="disposalCenterType">disposal center type: main center or branch</param>
       /// <returns></returns>
        public DataTable GetTechnologyWithProgramandDisposalCenter(string program,int disposalCenter,string disposalCenterType)
        {
            try
            {
                //edit by coco 2011-11-24
                string SQL = "select A.* from CAT_TECNOLOGIA  A  inner join K_PROG_TECNOLOGIA B on A.Cve_Tecnologia=B.Cve_Tecnologia ";              
                if(program!="")
                {
                   SQL=SQL+" and B.ID_Prog_Proy=@ID_Prog_Proy";
                }
                if (disposalCenter != 0 && disposalCenterType != "")
                {                  
                    SQL = SQL + " inner join K_CENTRO_DISP_TECNOLOGIA C on A.Cve_Tecnologia=C.Cve_Tecnologia and C.Id_Centro_Disp= @Id_Centro_Disp and C.Fg_Tipo_Centro_Disp=@Fg_Tipo_Centro_Disp";
                }
                SQL += " where A.Cve_Esquema = 0 ";
                //end edit
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@ID_Prog_Proy", program),
                    new SqlParameter("@Id_Centro_Disp",disposalCenter),
                    new SqlParameter("@Fg_Tipo_Centro_Disp",disposalCenterType)
                 };

                DataTable TableResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);

                return TableResult;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        // End
        /// <summary>
        /// Get Record By Primary Key
        /// </summary>
        public CAT_TECNOLOGIAModel Get_CAT_TECNOLOGIAByPKID(String pkid)
        {
            try
            {
                string executesqlstr = "SELECT Cve_Tecnologia, Dx_Nombre_General, Dx_Nombre_Particular, Dt_Fecha_Tecnologoia, Cve_Tipo_Tecnologia, Dx_Cve_CC FROM CAT_TECNOLOGIA WHERE Cve_Tecnologia = @Cve_Tecnologia";
                SqlParameter[] para = new SqlParameter[] { 
					new SqlParameter("@Cve_Tecnologia",pkid)
				};
                CAT_TECNOLOGIAModel modelinstance = new CAT_TECNOLOGIAModel();
                using (SqlDataReader sdr = SqlHelper.ExecuteReader(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para))
                {
                    if (sdr.Read())
                    {
                        modelinstance = EvaluateModel(sdr);
                    }
                }
                return modelinstance;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get a CAT_TECNOLOGIA failed", ex, true);
            }
        }
        /// <summary>
        /// evaluate model
        /// </summary>
        private CAT_TECNOLOGIAModel EvaluateModel(SqlDataReader sdr)
        {
            try
            {
                CAT_TECNOLOGIAModel modelinstance = new CAT_TECNOLOGIAModel();
                modelinstance.Cve_Tecnologia = sdr.IsDBNull(sdr.GetOrdinal("Cve_Tecnologia")) ? 0 : sdr.GetInt32(sdr.GetOrdinal("Cve_Tecnologia"));
                modelinstance.Dx_Nombre_General = sdr.IsDBNull(sdr.GetOrdinal("Dx_Nombre_General")) ? string.Empty : sdr.GetString(sdr.GetOrdinal("Dx_Nombre_General"));
                modelinstance.Dx_Nombre_Particular = sdr.IsDBNull(sdr.GetOrdinal("Dx_Nombre_Particular")) ? string.Empty : sdr.GetString(sdr.GetOrdinal("Dx_Nombre_Particular"));
                modelinstance.Dt_Fecha_Tecnologoia = sdr.IsDBNull(sdr.GetOrdinal("Dt_Fecha_Tecnologoia")) ? DateTime.Parse("2011-5-1") : sdr.GetDateTime(sdr.GetOrdinal("Dt_Fecha_Tecnologoia"));
                modelinstance.Cve_Tipo_Tecnologia = sdr.IsDBNull(sdr.GetOrdinal("Cve_Tipo_Tecnologia")) ? 0 : sdr.GetInt32(sdr.GetOrdinal("Cve_Tipo_Tecnologia"));
                modelinstance.Dx_Cve_CC = sdr.IsDBNull(sdr.GetOrdinal("Dx_Cve_CC")) ? string.Empty : sdr.GetString(sdr.GetOrdinal("Dx_Cve_CC"));
                return modelinstance;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Evaluate Model:CAT_TECNOLOGIA failed", ex, true);
            }
        }
        /// <summary>
        /// get technology by program and material
        /// </summary>
        /// <param name="strProgram"></param>
        /// <returns></returns>
        public DataTable Get_Material_TechnologyByProgram(string strProgram)
        {
            try
            {
                string SQL = "select distinct A.Cve_Tecnologia,A.Dx_Nombre_General from CAT_TECNOLOGIA  A  inner join K_PROG_TECNOLOGIA B on A.Cve_Tecnologia=B.Cve_Tecnologia"+
                                 " inner join K_TECNOLOGIA_RESIDUO_MATERIAL C on B.Cve_Tecnologia=C.Cve_Tecnologia where B.ID_Prog_Proy=@ID_Prog_Proy";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@ID_Prog_Proy", strProgram)  
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
        /// get all technology of material
        /// </summary>
        /// <returns></returns>
        public DataTable Get_ALL_Material_Technology()
        {
            try
            {
                string SQL = "select distinct A.Cve_Tecnologia,A.Dx_Nombre_General from CAT_TECNOLOGIA  A  inner join K_TECNOLOGIA_RESIDUO_MATERIAL B on A.Cve_Tecnologia=B.Cve_Tecnologia";
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        /// <summary>
        /// get all technology of material
        /// </summary>
        /// <returns></returns>
        public DataTable Get_ALL_Material_Technology_Provider()
        {
            try
            {
                string SQL = @"SELECT DISTINCT p.Id_Proveedor, prod.Cve_Tecnologia, 'SB_F' AS 'Tipo' 
                    FROM dbo.CAT_PROVEEDOR (NOLOCK) AS p
                    INNER JOIN dbo.K_PROVEEDOR_PRODUCTO (NOLOCK) AS kprod
                    ON p.Id_Proveedor = kprod.Id_Proveedor
                    INNER JOIN dbo.CAT_PRODUCTO (NOLOCK) AS prod
                    ON kprod.Cve_Producto = prod.Cve_Producto
                    INNER JOIN dbo.K_TECNOLOGIA_RESIDUO_MATERIAL (NOLOCK) AS res
                    ON res.Cve_Tecnologia = prod.Cve_Tecnologia
                    UNION ALL
                    SELECT DISTINCT p.Id_Branch, prod.Cve_Tecnologia, 'SB_F' AS 'Tipo'  
                    FROM dbo.CAT_PROVEEDORBRANCH (NOLOCK) AS p
                    LEFT OUTER JOIN dbo.CAT_PROVEEDOR (NOLOCK) AS p1
                    ON p.Id_Proveedor = p1.Id_Proveedor
                    INNER JOIN dbo.K_PROVEEDOR_PRODUCTO (NOLOCK) AS kprod
                    ON p.Id_Proveedor = kprod.Id_Proveedor
                    INNER JOIN dbo.CAT_PRODUCTO (NOLOCK) AS prod
                    ON kprod.Cve_Producto = prod.Cve_Producto
                    INNER JOIN dbo.K_TECNOLOGIA_RESIDUO_MATERIAL (NOLOCK) AS res
                    ON res.Cve_Tecnologia = prod.Cve_Tecnologia ";

                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        /// <summary>
        /// Get disposal center related technologies
        /// </summary>
        /// <param name="disposalCenter">disposal center id</param>
        /// <param name="disposalCenterType">disposal center type</param>
        /// <returns></returns>
        public DataTable GetDisposalCenterRelatedTechnology(int disposalCenter, string disposalCenterType)
        {
            try
            {
                string SQL = "select A.* from CAT_TECNOLOGIA  A  inner join K_PROG_TECNOLOGIA B on A.Cve_Tecnologia=B.Cve_Tecnologia";
                SQL = SQL + " inner join K_CENTRO_DISP_TECNOLOGIA C on A.Cve_Tecnologia=C.Cve_Tecnologia and C.Id_Centro_Disp= @Id_Centro_Disp and C.Fg_Tipo_Centro_Disp=@Fg_Tipo_Centro_Disp";
                
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Id_Centro_Disp",disposalCenter),
                    new SqlParameter("@Fg_Tipo_Centro_Disp",disposalCenterType)
                 };

                DataTable TableResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);

                return TableResult;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
    }
}
