/*
	Copyright IMPRA, Inc. 2011
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
using PAEEEM.Helpers;
using System.Data.SqlClient;
using System.Data;

namespace PAEEEM.DataAccessLayer
{
    /// <summary>
    /// Glossary table is used to store the configuration information of other catalogs
    /// </summary>
    public class GlossaryDal
    {
        /// <summary>
        /// property field
        /// </summary>
        private static GlossaryDal _instance = new GlossaryDal();
        /// <summary>
        /// Class Instance property
        /// </summary>
        public static GlossaryDal ClassInstance { get { return _instance; } }
        /// <summary>
        /// Get all the columns by table
        /// </summary>
        /// <param name="tableName">specified table name</param>
        /// <returns>field list</returns>
        public List<GlossaryField> GetFieldsWithTableName(string tableName)
        {
            List<GlossaryField> fields = new List<GlossaryField>();            

            try
            {
                string SQL = @"SELECT [TABLE_CATALOG]
                                          ,[TABLE_NAME]
                                          ,[COLUMN_NAME]
                                          ,[ORDINAL_POSITION]
                                          ,[COLUMN_DEFAULT]
                                          ,[IS_NULLABLE]
                                          ,[DATA_TYPE]
                                          ,[CHARACTER_MAXIMUM_LENGTH]
                                          ,[NUMERIC_PRECISION]
                                          ,[NUMERIC_SCALE]
                                      FROM [INFORMATION_SCHEMA].[COLUMNS]
                                      WHERE [TABLE_NAME] = @TableName";
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@TableName",tableName)
                };

                SqlDataReader sqlDataReader = SqlHelper.ExecuteReader(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, parameters);
                while (sqlDataReader.Read())
                {
                    GlossaryField field = new GlossaryField();
                    field.ColumnName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("COLUMN_NAME"));
                    field.DataType = sqlDataReader.GetString(sqlDataReader.GetOrdinal("DATA_TYPE"));

                    fields.Add(field);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return fields;
        }
        /// <summary>
        /// Get all table names for the database
        /// </summary>
        /// <returns>table names collection</returns>
        public List<string> GetTablesName()
        {
            List<string> tableNames = new List<string>();

            try
            {
                string SQL = "SELECT [name] FROM [sys].[tables] order by [name]";
                SqlDataReader sqlDataReader = SqlHelper.ExecuteReader(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);

                while (sqlDataReader.Read())
                {
                    tableNames.Add(sqlDataReader.GetString(sqlDataReader.GetOrdinal("name")));
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return tableNames;
        }
        /// <summary>
        /// Get all the configuration for each field in the specified table
        /// </summary>
        /// <param name="tableName">specified table name</param>
        /// <returns></returns>
        public List<GlossaryField> GetConfigurations(string tableName)
        {
            List<GlossaryField> configurations = new List<GlossaryField>();
            try
            {
                string SQL = @"SELECT [GUID]
                                          ,[Column_Name]
                                          ,[Data_Type]
                                          ,[Is_Primary]
                                          ,[Is_Foreign]
                                          ,[Parent_Table]
                                          ,[Parent_Column]
                                          ,[Display_Column]
                                          ,[Is_Display]
                                          ,[Display_Name]
                                          ,[Is_Identity]
                                          ,[Is_Mandatory]
                                          ,[Is_Phone]
                                          ,[Is_Email]
                                          ,[Is_Zip]
                                          ,[Owned_Table]
                                          ,[Create_Date]
                                      FROM [dbo].[H_GLOSSARY]
                                      WHERE [Owned_Table] = @TableName";
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@TableName",tableName)
                };

                SqlDataReader sqlDataReader = SqlHelper.ExecuteReader(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, parameters);
                while (sqlDataReader.Read())
                {
                    GlossaryField field = new GlossaryField();
                    field.ColumnName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("Column_Name"));
                    field.DataType = sqlDataReader.GetString(sqlDataReader.GetOrdinal("Data_Type"));
                    if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("Is_Primary")) &&
                        sqlDataReader.GetString(sqlDataReader.GetOrdinal("Is_Primary")) == "1" )
                    {
                        field.IsPrimary = true;
                    }

                    if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("Is_Foreign")) &&
                        sqlDataReader.GetString(sqlDataReader.GetOrdinal("Is_Foreign")) == "1")
                    {
                        field.IsForeign = true;
                    }

                    field.ParentTable = sqlDataReader.GetString(sqlDataReader.GetOrdinal("Parent_Table"));
                    field.ParentColumn = sqlDataReader.GetString(sqlDataReader.GetOrdinal("Parent_Column"));
                    field.DisplayColumn = sqlDataReader.GetString(sqlDataReader.GetOrdinal("Display_Column"));

                    if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("Is_Display")) &&
                        sqlDataReader.GetString(sqlDataReader.GetOrdinal("Is_Display")) == "1" )
                    {
                        field.IsDisplay = true;
                    }

                    field.DisplayName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("Display_Name"));

                    if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("Is_Identity")) &&
                        sqlDataReader.GetString(sqlDataReader.GetOrdinal("Is_Identity")) == "1" )
                    {
                        field.IsIdentity = true;
                    }

                    if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("Is_Mandatory")) &&
                        sqlDataReader.GetString(sqlDataReader.GetOrdinal("Is_Mandatory")) == "1")
                    {
                        field.IsMandatory = true;
                    }

                    if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("Is_Phone")) &&
                        sqlDataReader.GetString(sqlDataReader.GetOrdinal("Is_Phone")) == "1" )
                    {
                        field.IsPhone = true;
                    }

                    if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("Is_Email")) &&
                        sqlDataReader.GetString(sqlDataReader.GetOrdinal("Is_Email")) == "1" )
                    {
                        field.IsEmail = true;
                    }

                    if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("Is_Zip")) &&
                        sqlDataReader.GetString(sqlDataReader.GetOrdinal("Is_Zip")) == "1" )
                    {
                        field.IsZip = true;
                    }

                    configurations.Add(field);
                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return configurations;
        }
        /// <summary>
        /// Insert new record into glossary table
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public int InsertConfiguration(GlossaryField field)
        {
            int count = 0;

            try
            {
                string SQL = @"INSERT INTO [dbo].[H_GLOSSARY]
                                                                           ([Column_Name]
                                                                           ,[Data_Type]
                                                                           ,[Is_Primary]
                                                                           ,[Is_Foreign]
                                                                           ,[Parent_Table]
                                                                           ,[Parent_Column]
                                                                           ,[Display_Column]
                                                                           ,[Is_Display]
                                                                           ,[Display_Name]
                                                                           ,[Is_Identity]
                                                                           ,[Is_Mandatory]
                                                                           ,[Is_Phone]
                                                                           ,[Is_Email]
                                                                           ,[Is_Zip]
                                                                           ,[Owned_Table])
                                                                     VALUES
                                                                           (@ColumnName
                                                                           ,@DataType
                                                                           ,@IsPrimary
                                                                           ,@IsForeign
                                                                           ,@ParentTable
                                                                           ,@ParentColumn
                                                                           ,@DisplayColumn
                                                                           ,@IsDisplay
                                                                           ,@DisplayName
                                                                           ,@IsIdentity
                                                                           ,@IsMandatory
                                                                           ,@IsPhone
                                                                           ,@IsEmail
                                                                           ,@IsZip
                                                                           ,@OwnedTable)";
                SqlParameter[] parameters = new SqlParameter[] { 
                    new SqlParameter("@ColumnName", field.ColumnName),
                    new SqlParameter("@DataType", field.DataType),
                    new SqlParameter("@IsPrimary", field.IsPrimary),
                    new SqlParameter("@IsForeign", field.IsForeign),
                    new SqlParameter("@ParentTable", field.ParentTable),
                    new SqlParameter("@ParentColumn", field.ParentColumn),
                    new SqlParameter("@DisplayColumn", field.DisplayColumn),
                    new SqlParameter("@IsDisplay", field.IsDisplay),
                    new SqlParameter("@DisplayName", field.DisplayName),
                    new SqlParameter("@IsIdentity", field.IsIdentity),
                    new SqlParameter("@IsMandatory", field.IsMandatory),
                    new SqlParameter("@IsPhone", field.IsPhone),
                    new SqlParameter("@IsEmail", field.IsEmail),
                    new SqlParameter("@IsZip", field.IsZip),
                    new SqlParameter("@OwnedTable", field.OwnedTable)
                };

                count = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, parameters);
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return count;
        }
        /// <summary>
        /// Update the field configurations for some table
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public int UpdateConfiguration(GlossaryField field)
        {
            int count = 0;
            try
            {
                string SQL = @"UPDATE [dbo].[H_GLOSSARY]
                                                   SET [Data_Type] = @DataType
                                                       ,[Is_Primary] = @IsPrimary
                                                      ,[Is_Foreign] = @IsForeign
                                                      ,[Parent_Table] = @ParentTable
                                                      ,[Parent_Column] = @ParentColumn
                                                      ,[Display_Column] = @DisplayColumn
                                                      ,[Is_Display] = @IsDisplay
                                                      ,[Display_Name] = @DisplayName
                                                      ,[Is_Identity] = @IsIdentity
                                                      ,[Is_Mandatory] = @IsMandatory
                                                      ,[Is_Phone] = @IsPhone
                                                      ,[Is_Email] = @IsEmail
                                                      ,[Is_Zip] = @IsZip    
                                                 WHERE [Column_Name] = @ColumnName AND [Owned_Table] = @OwnedTable";
                SqlParameter[] parameters = new SqlParameter[] { 
                    new SqlParameter("@ColumnName", field.ColumnName),
                    new SqlParameter("@DataType", field.DataType),
                    new SqlParameter("@IsPrimary", field.IsPrimary),
                    new SqlParameter("@IsForeign", field.IsForeign),
                    new SqlParameter("@ParentTable", field.ParentTable),
                    new SqlParameter("@ParentColumn", field.ParentColumn),
                    new SqlParameter("@DisplayColumn", field.DisplayColumn),
                    new SqlParameter("@IsDisplay", field.IsDisplay),
                    new SqlParameter("@DisplayName", field.DisplayName),
                    new SqlParameter("@IsIdentity", field.IsIdentity),
                    new SqlParameter("@IsMandatory", field.IsMandatory),
                    new SqlParameter("@IsPhone", field.IsPhone),
                    new SqlParameter("@IsEmail", field.IsEmail),
                    new SqlParameter("@IsZip", field.IsZip),
                    new SqlParameter("@OwnedTable", field.OwnedTable)
                };

                count = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, parameters);
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return count;
        }

        public DataTable GetCatalogRecords(string tableName)
        {
            DataTable catalogRecords = null;
            try
            {
                string SQL = "SELECT * FROM "+tableName;
                catalogRecords = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return catalogRecords;
        }

        // added by tina 2012-02-27
        public DataTable GetCatalogRecordsWithPage(string tableName, string sortName, int pageIndex, int pageSize, out int pageCount)
        {
            DataTable catalogRecords = null;
            try
            {
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@PageCount", SqlDbType.Int),                   
                    new SqlParameter("@PageIndex", pageIndex),
                    new SqlParameter("@PageSize", pageSize),
                    new SqlParameter("@SortName", sortName),
                    new SqlParameter("@TableName", tableName)
               };
                paras[0].Direction = ParameterDirection.Output;

                catalogRecords = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "sp_GetCatalogRecords", paras);
                int.TryParse(paras[0].Value.ToString(), out pageCount);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Catalog Records failed: Execute method GetCatalogRecords in GlossaryDal.", ex, true);
            }

            return catalogRecords;
        }

        public List<string> GetConfiguredCatalogs()
        {
            List<string> catalogs = new List<string>();
            try
            {
                string SQL = @"SELECT DISTINCT [Owned_Table] FROM [dbo].[H_GLOSSARY]";
                SqlDataReader sqlDataReader = SqlHelper.ExecuteReader(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
                while (sqlDataReader.Read())
                {
                    catalogs.Add(sqlDataReader.GetString(sqlDataReader.GetOrdinal("Owned_Table")));
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return catalogs;
        }

        public DataTable GetRecordsWithTableNameAndColumns(string tableName, string displayTextField, string displayValueField)
        {
            DataTable records = null;
            try
            {
                string SQL = @"SELECT "+ displayTextField + ", " + displayValueField + " FROM "+ tableName;
                records = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch(SqlException ex)
            {
                throw ex;
            }

            return records;
        }

        public int DeleteCatalogRecords(string tableName,List<string> datakeyField, List<string> datakeyValue)
        {
            int result = 0;
            try
            {
                string sql = string.Empty;
                sql = "Delete " + tableName + " where 1=1";
                for(int i=0;i<datakeyField.Count;i++)
                {
                    sql = sql+" and " + datakeyField[i] + "='" + datakeyValue[i]+"'";
                }
                 result = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, sql);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return result;
        }

        public DataTable GetCatalogRecordsByTableNameAndPrimaryKey(string tableName, string datakeyField, string datakeyValue)
        {
            DataTable records = null;
            try
            {
                string SQL = string.Empty;
                string[] listDataKeyField = datakeyField.Split(',');
                string[] listDataKeyValue = datakeyValue.Split(',');

                SQL = "SELECT * FROM " + tableName + " WHERE 1=1 ";

                for (int i = 0; i < listDataKeyField.Length; i++)
                {
                    SQL = SQL+" and " + listDataKeyField[i] + "='" + listDataKeyValue[i] + "'";
                }

                records = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return records;
        }

        public int InsertCatalog(string tableName, Dictionary<string, object> keyValuePairs)
        {
            int result = 0;
            try
            {
                string sql = string.Empty;
                string textField = "", valueField = "";

                foreach (string key in keyValuePairs.Keys)
                {
                    textField += key + ",";
                    if (keyValuePairs[key] == null)
                    {
                        valueField += "null,";
                    }
                    else
                    {
                        valueField += "'" + keyValuePairs[key] + "'" + ",";
                    }
                }
                textField = textField.TrimEnd(new char[] { ',' });
                valueField = valueField.TrimEnd(new char[] { ',' });

                sql = "INSERT INTO " + tableName + "(" + textField + ")  VALUES(" + valueField + ")";

                result = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, sql);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return result;
        }

        public int UpdateCatalog(string tableName, Dictionary<string, object> keyValuePairs, string dataKeyField, string dataKeyValue)
        {
            int result = 0;
            try
            {
                string sql = string.Empty;

                string[] listDataKeyField = dataKeyField.Split(',');
                string[] listDataKeyValue = dataKeyValue.Split(',');

                sql = "UPDATE " + tableName + " SET ";
                foreach (string key in keyValuePairs.Keys)
                {
                    if (keyValuePairs[key] == null)
                    {
                        sql += key + "=null,";
                    }
                    else
                    {
                        sql += key + "='" + keyValuePairs[key] + "',";
                    }
                }
                sql = sql.TrimEnd(new char[] { ',' }) + " WHERE 1=1 ";

                for (int i = 0; i < listDataKeyField.Length; i++)
                {
                    sql += " AND " + listDataKeyField[i] + "='" + listDataKeyValue[i] + "'";
                }

                result = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, sql);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
