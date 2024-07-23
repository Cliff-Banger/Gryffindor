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
        public GryffindorResponse<bool> AddOrUpdateUserProfileSkill(UserProfileSkill skill)
        {
            var result = new GryffindorResponse<bool>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", skill.GryffindorUserId);
                parameters.Add("@QualificationType", Convert.ToInt32(QualificationType.Skills));
                parameters.Add("@ObjectId", skill.UserProfileSkillId);
                parameters.Add("@Name", skill.SkillName);
                parameters.Add("@Rating", skill.Rating);
                parameters.Add("@DateFrom", skill.DateGained);
                parameters.Add("@DateTo", skill.LastPracticed);

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
                _log.Error("Error updating profile Skill", e);
                result.Message = "Error updating profile Skill: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<bool> DeleteUserProfileSkill(Guid skillId, Guid userId)
        {
            var result = new GryffindorResponse<bool>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", userId);
                parameters.Add("@QualificationType", Convert.ToInt32(QualificationType.Skills));
                parameters.Add("@ObjectId", skillId);

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
                _log.Error("Error setting user profession Skill", e);
                result.Message = "Error setting user profession Skill: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<UserProfileSkill> GetUserProfileSkill(Guid skillId, Guid userId)
        {
            var result = new GryffindorResponse<UserProfileSkill>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", userId);
                parameters.Add("@QualificationType", Convert.ToInt32(QualificationType.Skills));
                parameters.Add("@ObjectId", skillId); parameters.Add("@GryffindorUserId", userId);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<UserProfileSkill>("up_UserProfile_GetQualification", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error getting profile Skill", e);
                result.Message = "Error getting profile Skill: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<List<UserProfileSkill>> GetAllUserProfileSkillData(Guid userId)
        {
            var result = new GryffindorResponse<List<UserProfileSkill>>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", userId);
                parameters.Add("@QualificationType", Convert.ToInt32(QualificationType.Skills));

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<UserProfileSkill>("up_UserProfile_GetAllQualificationData", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).ToList();
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error getting profile avatars Skill", e);
                result.Message = "Error getting profile avatars Skill: " + e.Message;
                return result;
            }
        }
    }
}
