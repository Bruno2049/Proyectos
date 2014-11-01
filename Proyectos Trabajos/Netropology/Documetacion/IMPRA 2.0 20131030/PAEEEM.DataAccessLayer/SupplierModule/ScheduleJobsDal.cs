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
    /// Process Logic for job schedule
    /// </summary>
    public class ScheduleJobsDal
    {
        /// <summary>
        /// Readonly variable for class instance
        /// </summary>
        private static readonly ScheduleJobsDal _classInstance = new ScheduleJobsDal();
        /// <summary>
        /// Property for class instance
        /// </summary>
        public static ScheduleJobsDal ClassInstance { get { return _classInstance; } }
        /// <summary>
        /// Add new schedule job
        /// </summary>
        /// <param name="JobModel"></param>
        /// <returns></returns>
        public int AddScheduleJob(ScheduleJobEntity JobModel)
        {
            int iResult = 0;
            string SQL = "";
            try
            {
                SQL = "INSERT INTO [dbo].[H_SCHEDULE_JOBS]([Credit_No],[Email_Title],[Email_Body],[Supplier_Name],[Supplier_Email],[Warning_Date],[Canceled_Date],[Job_Status], [Create_Date]) " +
                        "VALUES(@Credit_No, @Email_Title, @Email_Body, @Supplier_Name,  @Supplier_Email,  @Warning_Date,  @Canceled_Date,  @Job_Status,  @Create_Date)";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Credit_No", JobModel.Credit_No),
                    new SqlParameter("@Email_Title", JobModel.Email_Title),
                    new SqlParameter("@Email_Body", JobModel.Email_Body),
                    new SqlParameter("@Supplier_Name", JobModel.Supplier_Name),
                    new SqlParameter("@Supplier_Email", JobModel.Supplier_Email),
                    new SqlParameter("@Warning_Date", JobModel.Warning_Date),
                    new SqlParameter("@Canceled_Date", JobModel.Canceled_Date),
                    new SqlParameter("@Job_Status", JobModel.Job_Status),
                    new SqlParameter("@Create_Date", JobModel.Create_Date)
                };

                iResult = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Add schedule failed: Execute method AddScheduleJob in ScheduleJobsDal.", ex, true);
            }

            return iResult;
        }
        /// <summary>
        /// Process schedule job
        /// </summary>
        /// <param name="CreditNum"></param>
        /// <returns></returns>
        public int ProcessScheduleJob(string CreditNum)
        {
            int iResult = 0;
            string SQL = "";
            try
            {
                SQL = @"UPDATE [dbo].[H_SCHEDULE_JOBS] SET [Warning_Date] = @Warning_Date, [Job_Status] = 'Processed'  WHERE [Credit_No] = @Credit_No";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Credit_No", CreditNum),
                    new SqlParameter("@Warning_Date", DateTime.Now.Date)
                };
                iResult = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Process schedule failed: Execute method ProcessScheduleJob in ScheduleJobsDal.", ex, true);
            }
            return iResult;
        }

        /// <summary>
        /// Cancel schedule job
        /// </summary>
        /// <param name="CreditNum"></param>
        /// <param name="connString"></param>
        /// <returns></returns>
        public int CanceledScheduleJob(string CreditNum, string connString)
        {
            int iResult = 0;
            string SQL = "";
            SqlConnection conn = new SqlConnection(connString);

            try
            {
                SQL = @"UPDATE [dbo].[H_SCHEDULE_JOBS] SET [Canceled_Date] = @Canceled_Date, [Job_Status] = 'Canceled'  WHERE [Credit_No] = @Credit_No";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@Credit_No", CreditNum);
                cmd.Parameters.AddWithValue("@Canceled_Date", DateTime.Now.Date);
                conn.Open();

                iResult = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Cancel schedule failed: Execute method CanceledScheduleJob in ScheduleJobsDal.", ex, false);
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                    conn = null;
                }
            }
            return iResult;
        }
        /// <summary>
        /// Get scheduled jobs
        /// </summary>
        /// <returns></returns>
        public DataTable GetScheduleJobsWithDateandStatus(string connString)
        {
            DataTable dtResult = new DataTable();
            string SQL = "";
            SqlConnection conn = new SqlConnection(connString);

            try
            {
                SQL = @"SELECT [Job_ID]
                                  ,[Credit_No]
                                  ,[Email_Title]
                                  ,[Email_Body]
                                  ,[Supplier_Name]
                                  ,[Supplier_Email]
                                  ,[Warning_Date]
                                  ,[Canceled_Date]
                                  ,[Job_Status]
                                  ,[Create_Date]
                              FROM [dbo].[H_SCHEDULE_JOBS]
                              WHERE CONVERT(VARCHAR(10), [Create_Date], 120) = CONVERT(VARCHAR(10), @CreateDate, 120) AND [Job_Status] = @Job_Status";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now.Date.AddDays(-25).Date);
                cmd.Parameters.AddWithValue("@Job_Status", GlobalVar.WAITING_FOR_PROCESS);

                SqlDataAdapter DataAdapter = new SqlDataAdapter();
                DataAdapter.SelectCommand = cmd;

                DataAdapter.Fill(dtResult);

            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get schedule failed: Execute method GetScheduleJobsWithDateandStatus in ScheduleJobsDal.", ex, false);
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                    conn = null;
                }
            }

            return dtResult;
        }
        /// <summary>
        /// Get schedule jobs for cancel process
        /// </summary>
        /// <returns></returns>
        public DataTable GetScheduleJobsWithCanceled(string connectionString)
        {
            DataTable dtResult = new DataTable();
            string SQL = "";
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                SQL = @"SELECT [Job_ID]
                                  ,[Credit_No]
                                  ,[Email_Title]
                                  ,[Email_Body]
                                  ,[Supplier_Name]
                                  ,[Supplier_Email]
                                  ,[Warning_Date]
                                  ,[Canceled_Date]
                                  ,[Job_Status]
                                  ,[Create_Date]
                              FROM [dbo].[H_SCHEDULE_JOBS]
                              WHERE CAST(CONVERT(VARCHAR(10), [Warning_Date], 120) AS DATETIME) <= CAST(CONVERT(VARCHAR(10), @Warning_Date, 120) AS DATETIME) AND [Job_Status] = @Job_Status";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@Warning_Date", DateTime.Now.Date.AddDays(-5).Date);
                cmd.Parameters.AddWithValue("@Job_Status", GlobalVar.PROCESSED);

                SqlDataAdapter DataAdapter = new SqlDataAdapter();
                DataAdapter.SelectCommand = cmd;

                DataAdapter.Fill(dtResult);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get schedule failed: Execute method GetScheduleJobsWithCanceled in ScheduleJobsDal.", ex, false);
            }

            return dtResult;
        }
    }
}
