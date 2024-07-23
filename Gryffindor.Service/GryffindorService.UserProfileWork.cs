using Gryffindor.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Gryffindor.Contract.Dto;
using System.ServiceModel;
using System.Data.SqlClient;
using Gryffindor.Service.Utilities;
using Dapper;
using Gryffindor.Contract.Utilities;
using Gryffindor.Contract.Enums;

namespace Gryffindor.Service
{
    public partial class GryffindorService : IGryffindorService
    {
        [OperationBehavior]
        public GryffindorResponse<bool> AddOrUpdateUserProfileWork(UserProfileWork work)
        {
            var result = new GryffindorResponse<bool>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", work.GryffindorUserId);
                parameters.Add("@QualificationType", Convert.ToInt32(QualificationType.Work));
                parameters.Add("@ObjectId", work.UserProfileWorkId);
                parameters.Add("@Name", work.CompanyName);
                parameters.Add("@Title", work.Position);
                parameters.Add("@Description", work.ResponsibilitiesSummary);
                parameters.Add("@DateFrom", work.DateFrom);
                parameters.Add("@DateTo", work.DateTo);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    connection.Execute("up_UserProfile_AddOrUpdateQualification", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure);
                    result.Data = true;
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error updating profile Work", e);
                result.Message = "Error updating profile Work: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<bool> DeleteUserProfileWork(Guid workId, Guid userId)
        {
            var result = new GryffindorResponse<bool>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", userId);
                parameters.Add("@QualificationType", Convert.ToInt32(QualificationType.Work));
                parameters.Add("@ObjectId", workId);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    connection.Execute("up_UserProfile_DeleteQualification", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure);
                    result.Data = true;
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error setting user profession Work", e);
                result.Message = "Error setting user profession Work: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<UserProfileWork> GetUserProfileWork(Guid workId, Guid userId)
        {
            var result = new GryffindorResponse<UserProfileWork>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", userId);
                parameters.Add("@QualificationType", Convert.ToInt32(QualificationType.Work));
                parameters.Add("@ObjectId", workId); parameters.Add("@GryffindorUserId", userId);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<UserProfileWork>("up_UserProfile_GetQualification", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error getting profile Work", e);
                result.Message = "Error getting profile Work: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<List<UserProfileWork>> GetAllUserProfileWorkData(Guid userId)
        {
            var result = new GryffindorResponse<List<UserProfileWork>>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", userId);
                parameters.Add("@QualificationType", Convert.ToInt32(QualificationType.Work));

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<UserProfileWork>("up_UserProfile_GetAllQualificationData", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).ToList();
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error getting profile avatars Work", e);
                result.Message = "Error getting profile avatars Work: " + e.Message;
                return result;
            }
        }
    }
}
