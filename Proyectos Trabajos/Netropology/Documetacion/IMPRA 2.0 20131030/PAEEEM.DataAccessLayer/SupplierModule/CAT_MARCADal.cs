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
    /// Brand table
    /// </summary>
    public class CAT_MARCADal
    {
        /// <summary>
        /// Data Access Layer for fabricator
        /// </summary>  
        private static readonly CAT_MARCADal _classinstance = new CAT_MARCADal();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static CAT_MARCADal ClassInstance { get { return _classinstance; } }
        /// <summary>
        /// /Brand
        /// </summary>
        /// <param name="strProduct"></param>
        /// <returns></returns>
        public DataTable Get_CAT_MARCADal(string strProduct)
        {
            try
            {
                string SQL = " select distinct A.* from CAT_MARCA  A inner join CAT_PRODUCTO B on A.Cve_Marca=B.Cve_Marca and B.Cve_Producto in (" + strProduct + ")";
                
                return  SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        /// <summary>
        /// Get all brands
        /// </summary>
        /// <returns></returns>
        public DataTable Get_ALL_CAT_MARCADal()
        {
            try
            {
                string SQL = " select Cve_Marca,Dx_Marca,Dt_Marca from CAT_MARCA ORDER BY 2";

                return SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }

        // RSA 2012-09-12 SE attributes Start
        /// <summary>
        /// Get all Tipo
        /// </summary>
        /// <returns></returns>
        public DataTable Get_ALL_CAT_SE_TIPODal()
        {
            try
            {
                string SQL = "Select Cve_Tipo, Dx_Nombre_Tipo From CAT_SE_TIPO (nolock) Order by 1";

                return SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        /// <summary>
        /// Get all Transformador
        /// </summary>
        /// <returns></returns>
        public DataTable Get_ALL_CAT_SE_TRANSFORMADORDal()
        {
            try
            {
                string SQL = "Select Cve_Transformador, Dx_Dsc_Transformador From CAT_SE_TRANSFORMADOR (nolock) Order by 1";

                return SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        /// <summary>
        /// Get all Fase del Transformador
        /// </summary>
        /// <returns></returns>
        public DataTable Get_ALL_CAT_SE_TRANSFORM_FASEDal()
        {
            try
            {
                string SQL = "Select Cve_Fase, Dx_Nombre_Fase From CAT_SE_TRANSFORM_FASE (nolock) Order by 1";

                return SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        /// <summary>
        /// Get all Marca del Transformador
        /// </summary>
        /// <returns></returns>
        public DataTable Get_ALL_CAT_SE_TRANSFORM_MARCADal()
        {
            try
            {
                string SQL = "Select Cve_Marca, Dx_Nombre_Marca From CAT_SE_TRANSFORM_MARCA (nolock) Order by 1";

                return SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        /// <summary>
        /// Get all Relación de Transformación
        /// </summary>
        /// <returns></returns>
        public DataTable Get_ALL_CAT_SE_TRANSFORM_RELACIONDal()
        {
            try
            {
                string SQL = "Select Cve_Relacion, Dx_Dsc_Relacion From CAT_SE_TRANSFORM_RELACION (nolock) Order by 1";

                return SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        /// <summary>
        /// Get all Apartarrayo
        /// </summary>
        /// <returns></returns>
        public DataTable Get_ALL_CAT_SE_APARTARRAYODal()
        {
            try
            {
                string SQL = "Select Cve_Apartarrayo, Dx_Dsc_Apartarrayo From CAT_SE_APARTARRAYO (nolock) Order by 1";

                return SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        /// <summary>
        /// Get all Marca del Apartarrayo
        /// </summary>
        /// <returns></returns>
        public DataTable Get_ALL_CAT_SE_APARTARRAYO_MARCADal()
        {
            try
            {
                string SQL = "Select Cve_Marca, Dx_Nombre_Marca From CAT_SE_APARTARRAYO_MARCA (nolock) Order by 1";

                return SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        /// <summary>
        /// Get all Cortacircuito – Fusible
        /// </summary>
        /// <returns></returns>
        public DataTable Get_ALL_CAT_SE_CORTACIRCUITODal()
        {
            try
            {
                string SQL = "Select Cve_Cortacircuito, Dx_Dsc_Cortacircuito From CAT_SE_CORTACIRCUITO (nolock) Order by 1";

                return SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        /// <summary>
        /// Get all Marca Cortacircuito – Fusible
        /// </summary>
        /// <returns></returns>
        public DataTable Get_ALL_CAT_SE_CORTACIRC_MARCADal()
        {
            try
            {
                string SQL = "Select Cve_Marca, Dx_Nombre_Marca From CAT_SE_CORTACIRC_MARCA (nolock) Order by 1";

                return SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        /// <summary>
        /// Get all Interruptor Termomagnético
        /// </summary>
        /// <returns></returns>
        public DataTable Get_ALL_CAT_SE_INTERRUPTORDal()
        {
            try
            {
                string SQL = "Select Cve_Interruptor, Dx_Dsc_Interruptor From CAT_SE_INTERRUPTOR (nolock) Order by 1";

                return SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        /// <summary>
        /// Get all Marca Interruptor Termomagnético
        /// </summary>
        /// <returns></returns>
        public DataTable Get_ALL_CAT_SE_INTERRUPTOR_MARCADal()
        {
            try
            {
                string SQL = "Select Cve_Marca, Dx_Nombre_Marca From CAT_SE_INTERRUPTOR_MARCA (nolock) Order by 1";

                return SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        /// <summary>
        /// Get all Conductor de Tierra
        /// </summary>
        /// <returns></returns>
        public DataTable Get_ALL_CAT_SE_CONDUCTORDal()
        {
            try
            {
                string SQL = "Select Cve_Conductor, Dx_Dsc_Conductor From CAT_SE_CONDUCTOR (nolock) Order by 1";

                return SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        /// <summary>
        /// Get all Marca Conductor de Tierra
        /// </summary>
        /// <returns></returns>
        public DataTable Get_ALL_CAT_SE_CONDUCTOR_MARCADal()
        {
            try
            {
                string SQL = "Select Cve_Marca, Dx_Nombre_Marca From CAT_SE_CONDUCTOR_MARCA (nolock) Order by 1";

                return SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        /// <summary>
        /// Get all Conductor de Conexión a Centro de Carga
        /// </summary>
        /// <returns></returns>
        public DataTable Get_ALL_CAT_SE_COND_CONEXIONDal()
        {
            try
            {
                string SQL = "Select Cve_Conductor_Conex, Dx_Dsc_Conductor_Conex From CAT_SE_COND_CONEXION (nolock) Order by 1";

                return SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        /// <summary>
        /// Get all Marca Conductor de Conexión
        /// </summary>
        /// <returns></returns>
        public DataTable Get_ALL_CAT_SE_COND_CONEXION_MARCADal()
        {
            try
            {
                string SQL = "Select Cve_Marca, Dx_Nombre_Marca From CAT_SE_COND_CONEXION_MARCA (nolock) Order by 1";

                return SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        // RSA 2012-09-12 SE attributes End
    }
}
