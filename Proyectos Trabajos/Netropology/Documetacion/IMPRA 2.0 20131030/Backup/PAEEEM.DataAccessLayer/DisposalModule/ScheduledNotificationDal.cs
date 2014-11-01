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
    /// 
    /// </summary>
    public class ScheduledNotificationDal
    {
        private static readonly ScheduledNotificationDal _classinstance = new ScheduledNotificationDal();
        /// <summaryScheduledNotificationDal
        /// Class Instance
        /// </summary>
        public static ScheduledNotificationDal ClassInstance { get { return _classinstance; } }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheduledNotificationEntity"></param>
        /// <returns></returns>
        public int InsertScheduledNotification(ScheduledNotificationEntity scheduledNotificationEntity)
        {
            int Result = 0;
            try
            {
                string SQL = @"INSERT INTO [dbo].[H_SCHEDULED_NOTIFICATION]
                                       ([ToEmail]
                                       ,[CCEmail]
                                       ,[Id_Credito_Sustitucion]
                                       ,[No_Credito]
                                       ,[Id_Folio]
                                       ,[Subject]
                                       ,[Body]
                                       ,[Last_Sent]
                                       ,[Create_Date])
                                 VALUES
                                       (@ToEmail
                                       ,@CCEmail
                                       ,@Id_Credito_Sustitucion 
                                       ,@No_Credito
                                       ,@Id_Folio
                                       ,@Subject
                                       ,@Body
                                       ,@Last_Sent
                                       ,@Create_Date)";
                SqlParameter[] Paras = new SqlParameter[] {
                    new SqlParameter("@ToEmail", scheduledNotificationEntity.ToEmail),
                    new SqlParameter("@CCEmail", scheduledNotificationEntity.CCEmail),
                    new SqlParameter("@Id_Credito_Sustitucion", scheduledNotificationEntity.SustitutionNumber),
                    new SqlParameter("@No_Credito", scheduledNotificationEntity.CreditNumber),
                    new SqlParameter("@Id_Folio", scheduledNotificationEntity.FolioID),
                    new SqlParameter("@Subject", scheduledNotificationEntity.Subject),
                    new SqlParameter("@Body", scheduledNotificationEntity.Body),
                    new SqlParameter("@Last_Sent", scheduledNotificationEntity.LastSent),
                    new SqlParameter("@Create_Date", scheduledNotificationEntity.CreateDate)
                };

                Result = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, Paras);

            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }

            return Result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public List<ScheduledNotificationEntity> GetScheduledNotifications(string connectionString)
        {
            List<ScheduledNotificationEntity> ScheduledNotificationCollection = new List<ScheduledNotificationEntity>();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                string SQL = @"SELECT [Notification_ID]
                                    ,[ToEmail]
                                    ,[CCEmail]
                                    ,[Id_Credito_Sustitucion]
                                    ,[No_Credito]
                                    ,[Id_Folio]
                                    ,[Subject]
                                    ,[Body]
                                    ,[Last_Sent]
                                    ,[Create_Date]
                                    FROM [dbo].[H_SCHEDULED_NOTIFICATION] 

                                    WHERE [Id_Credito_Sustitucion] NOT IN (SELECT [Id_Credito_Sustitucion] FROM K_INHABILITACION_PRODUCTO) AND 
                                    (ISNULL([H_SCHEDULED_NOTIFICATION].[Last_Sent],'') = '' OR 
                                    CONVERT(VARCHAR(10),[H_SCHEDULED_NOTIFICATION].[Last_Sent],101) <> CONVERT(VARCHAR(10),GETDATE(),101))";
                
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SQL;

                conn.Open();
                SqlDataReader sqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (sqlDataReader.Read())
                {
                    ScheduledNotificationEntity ScheduledNotificationEntity = new ScheduledNotificationEntity();
                    ScheduledNotificationEntity.NotificationId = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("Notification_ID"));
                    ScheduledNotificationEntity.ToEmail = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ToEmail"));
                    ScheduledNotificationEntity.CCEmail = sqlDataReader.GetString(sqlDataReader.GetOrdinal("CCEmail"));
                    ScheduledNotificationEntity.SustitutionNumber = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("Id_Credito_Sustitucion"));
                    ScheduledNotificationEntity.CreditNumber = sqlDataReader.GetString(sqlDataReader.GetOrdinal("No_Credito"));
                    ScheduledNotificationEntity.FolioID = sqlDataReader.GetString(sqlDataReader.GetOrdinal("Id_Folio"));
                    ScheduledNotificationEntity.Subject = sqlDataReader.GetString(sqlDataReader.GetOrdinal("Subject"));                    
                    ScheduledNotificationEntity.Body = sqlDataReader.GetString(sqlDataReader.GetOrdinal("Body"));
                    if(!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("Last_Sent")))
                    {
                        ScheduledNotificationEntity.LastSent = sqlDataReader.GetDateTime(sqlDataReader.GetOrdinal("Last_Sent"));
                    }
                    ScheduledNotificationEntity.CreateDate = sqlDataReader.GetDateTime(sqlDataReader.GetOrdinal("Create_Date"));

                    ScheduledNotificationCollection.Add(ScheduledNotificationEntity);
                }

            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, false);
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                    conn = null;
                }
            }

            return ScheduledNotificationCollection;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="notificationId"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public int UpdateLastSentTime(int notificationId, string connectionString)
        {
            int Result = 0;
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                string SQL = @"UPDATE [dbo].[H_SCHEDULED_NOTIFICATION]
                               SET [Last_Sent] = @Last_Sent 
                             WHERE Notification_ID = @NotificationID";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@Last_Sent", DateTime.Now);
                cmd.Parameters.AddWithValue("@NotificationID", notificationId);
                conn.Open();

                Result = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                    conn = null;
                }
            }

            return Result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposalType"></param>
        /// <param name="disposalCenterNumber"></param>
        /// <returns></returns>
        public string GetCarbonCopyEmailAddresses(string disposalType, string disposalCenterNumber)
        {
            string ccEmailAddresses = string.Empty;

            try
            {
                SqlParameter[] Paras = new SqlParameter[] { 
                    new SqlParameter("@DisposalCenterType", disposalType),
                    new SqlParameter("@DisposalCenterID", disposalCenterNumber)
                };

                SqlDataReader sqlDataReader = SqlHelper.ExecuteReader(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "sp_get_regionandzone_users", Paras);
                while (sqlDataReader.Read())
                {
                    string EmailAddress = sqlDataReader.GetString(sqlDataReader.GetOrdinal("CorreoElectronico"));
                    if(!string.IsNullOrEmpty(EmailAddress))
                    {
                        ccEmailAddresses += EmailAddress;
                        ccEmailAddresses += ";";
                    }
                }

            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }

            return ccEmailAddresses.TrimEnd(new char[]{';'});
        }
    }
}
