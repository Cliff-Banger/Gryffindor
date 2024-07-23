using Gryffindor.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Gryffindor.Contract.Dto;
using System.ServiceModel;
using log4net;
using System.Data.SqlClient;
using Gryffindor.Service.Utilities;
using Dapper;
using Gryffindor.Contract.Utilities;

namespace Gryffindor.Service
{
    public partial class GryffindorService : IGryffindorService
    {
        [OperationBehavior]
        public GryffindorResponse<ResumePermission> GetResumePermission(Guid userId, string sharedToEmail)
        {
            var result = new GryffindorResponse<ResumePermission>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@SharedToEmail", sharedToEmail);
                parameters.Add("@GryffindorUserId", userId);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<ResumePermission>("up_ResumePermission_GetResumePermission", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error calling GetResumePermission", e);
                result.Message = "Error calling GetResumePermission: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<ResumePermission> CanDownloadResume(string username, string sharedToEmail)
        {
            var result = new GryffindorResponse<ResumePermission>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Username", username);
                parameters.Add("@SharedToEmail", sharedToEmail);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<ResumePermission>("up_ResumePermission_CanDownloadResume", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error calling CanDownloadResume", e);
                result.Message = "Error calling CanDownloadResume: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<bool> AddOrUpdateResumePermission(ResumePermission permission)
        {
            var result = new GryffindorResponse<bool>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", permission.ResumePermissionId);
                parameters.Add("@GryffindorUserId", permission.GryffindorUserId);
                parameters.Add("@GryffindorUserId", permission.SharedToUserId);
                parameters.Add("@GryffindorUserId", permission.SharedToEmail);
                parameters.Add("@GryffindorUserId", permission.ShareFullResume);
                parameters.Add("@GryffindorUserId", permission.Downloads);
                parameters.Add("@GryffindorUserId", permission.LastDownloaded);
                parameters.Add("@GryffindorUserId", permission.IsBlocked);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<bool>("up_ResumePermission_AddOrUpdateResumePermission", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error calling AddOrUpdateResumePermission", e);
                result.Message = "Error calling AddOrUpdateResumePermission: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<bool> DeleteResumePermission(Guid resumePermissionId, Guid userId)
        {
            var result = new GryffindorResponse<bool>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ResumePermissionId", resumePermissionId);
                parameters.Add("@GryffindorUserId", userId);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<bool>("up_ResumePermission_DeleteResumePermission", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error calling DeleteResumePermission", e);
                result.Message = "Error calling DeleteResumePermission: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<bool> UpdateResumeDownloads(string username, string sharedToEmail)
        {
            var result = new GryffindorResponse<bool>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Username", username);
                parameters.Add("@SharedToEmail", sharedToEmail);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<bool>("up_ResumePermission_UpdateResumeDownloads", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error calling AddOrUpdateResumePermission", e);
                result.Message = "Error calling AddOrUpdateResumePermission: " + e.Message;
                return result;
            }
        }
    }
}
