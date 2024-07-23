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
        public GryffindorResponse<bool> AddOrUpdateUserProfileAchievement(UserProfileAchievement achievement)
        {
            var result = new GryffindorResponse<bool>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", achievement.GryffindorUserId);
                parameters.Add("@QualificationType", Convert.ToInt32(QualificationType.Achievements));
                parameters.Add("@ObjectId", achievement.UserProfileAchievementId);
                parameters.Add("@Name", achievement.AchievementName);
                parameters.Add("@Description", achievement.Description);
                parameters.Add("@AchievedAt", achievement.AchievedAt);
                parameters.Add("@AchievedOn", achievement.AchievedOn);
                parameters.Add("@IsVerified", achievement.IsVerified);

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
                _log.Error("Error updating profile Achievement", e);
                result.Message = "Error updating profile Achievement: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<bool> DeleteUserProfileAchievement(Guid achievementId, Guid userId)
        {
            var result = new GryffindorResponse<bool>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", userId);
                parameters.Add("@QualificationType", Convert.ToInt32(QualificationType.Achievements));
                parameters.Add("@ObjectId", achievementId);

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
                _log.Error("Error setting user profession Achievement", e);
                result.Message = "Error setting user profession Achievement: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<UserProfileAchievement> GetUserProfileAchievement(Guid achievementId, Guid userId)
        {
            var result = new GryffindorResponse<UserProfileAchievement>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", userId);
                parameters.Add("@QualificationType", Convert.ToInt32(QualificationType.Achievements));
                parameters.Add("@ObjectId", achievementId); parameters.Add("@GryffindorUserId", userId);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<UserProfileAchievement>("up_UserProfile_GetQualification", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error getting profile Achievement", e);
                result.Message = "Error getting profile Achievement: " + e.Message;
                return result;
            }
        }
        
        [OperationBehavior]
        public GryffindorResponse<List<UserProfileAchievement>> GetAllUserProfileAchievementData(Guid userId)
        {
            var result = new GryffindorResponse<List<UserProfileAchievement>>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", userId);
                parameters.Add("@QualificationType", Convert.ToInt32(QualificationType.Achievements));

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<UserProfileAchievement>("up_UserProfile_GetAllQualificationData", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).ToList();
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error getting profile avatars Achievement", e);
                result.Message = "Error getting profile avatars Achievement: " + e.Message;
                return result;
            }
        }
    }
}
