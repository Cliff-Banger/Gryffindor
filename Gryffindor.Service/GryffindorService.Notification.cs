using Dapper;
using Gryffindor.Contract;
using Gryffindor.Contract.Dto;
using Gryffindor.Contract.Utilities;
using Gryffindor.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;

namespace Gryffindor.Service
{
    public partial class GryffindorService : IGryffindorService
    {        
        [OperationBehavior]
        public GryffindorResponse<IList<Notification>> GetUserNotifications(Guid userId)
        {
            var result = new GryffindorResponse<IList<Notification>>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", userId);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<Notification>("up_Notification_GetUserNotifications", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).ToList();
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error calling GetUserNotifications", e);
                result.Message = "Error calling GetUserNotifications: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<bool> AddOrRemoveNotification(Notification notification)
        {
            var result = new GryffindorResponse<bool>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@NotificationId", notification.NotificationId);
                parameters.Add("@GryffindorUserId", notification.Sender);
                parameters.Add("@Recipient", notification.Recipient);
                parameters.Add("@NotificationType", notification.NotificationType);
                parameters.Add("@NotificationLink", notification.NotificationLink);
                parameters.Add("@Text", notification.Text);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<bool>("up_Notification_AddOrRemoveNotification", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error calling AddOrRemoveNotification", e);
                result.Message = "Error calling AddOrRemoveNotification: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<int> CheckNotifications(Guid userId)
        {
            var result = new GryffindorResponse<int>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", userId);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<int>("up_Notification_CheckNotifications", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error calling CheckNotifications", e);
                result.Message = "Error calling CheckNotifications: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<bool> MarkNotificationsAsRead(Guid userId)
        {
            var result = new GryffindorResponse<bool>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", userId);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    connection.Execute("up_Notification_UpdateToRead", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure);
                    result.Data = true;
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error calling MarkNotificationsAsRead", e);
                result.Message = "Error calling MarkNotificationsAsRead: " + e.Message;
                return result;
            }
        }
    }
}
