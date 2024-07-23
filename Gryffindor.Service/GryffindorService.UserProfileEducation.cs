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
        public GryffindorResponse<bool> AddOrUpdateUserProfileEducation(UserProfileEducation education)
        {
            var result = new GryffindorResponse<bool>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", education.GryffindorUserId);
                parameters.Add("@QualificationType", Convert.ToInt32(QualificationType.Education));
                parameters.Add("@ObjectId", education.UserProfileEducationId);
                parameters.Add("@Name", education.SchoolName);
                parameters.Add("@Description", education.StudyFields);
                parameters.Add("@DateFrom", education.DateFrom);
                parameters.Add("@DateTo", education.DateTo);

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
                _log.Error("Error updating profile Education", e);
                result.Message = "Error updating profile Education: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<bool> DeleteUserProfileEducation(Guid educationId, Guid userId)
        {
            var result = new GryffindorResponse<bool>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", userId);
                parameters.Add("@QualificationType", Convert.ToInt32(QualificationType.Education));
                parameters.Add("@ObjectId", educationId);

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
                _log.Error("Error setting user profession Education", e);
                result.Message = "Error setting user profession Education: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<UserProfileEducation> GetUserProfileEducation(Guid educationId, Guid userId)
        {
            var result = new GryffindorResponse<UserProfileEducation>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", userId);
                parameters.Add("@QualificationType", Convert.ToInt32(QualificationType.Education));
                parameters.Add("@ObjectId", educationId); parameters.Add("@GryffindorUserId", userId);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<UserProfileEducation>("up_UserProfile_GetQualification", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error getting profile Education", e);
                result.Message = "Error getting profile Education: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<List<UserProfileEducation>> GetAllUserProfileEducationData(Guid userId)
        {
            var result = new GryffindorResponse<List<UserProfileEducation>>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", userId);
                parameters.Add("@QualificationType", Convert.ToInt32(QualificationType.Education));

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<UserProfileEducation>("up_UserProfile_GetAllQualificationData", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).ToList();
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error getting profile avatars Education", e);
                result.Message = "Error getting profile avatars Education: " + e.Message;
                return result;
            }
        }
    }
}
