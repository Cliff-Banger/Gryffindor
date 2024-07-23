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
using Gryffindor.Contract.DataModels;

namespace Gryffindor.Service
{
    public partial class GryffindorService : IGryffindorService
    {
        [OperationBehavior]
        public GryffindorResponse<bool> UpdateUserProfile(UserProfile profile)
        {
            var result = new GryffindorResponse<bool>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", profile.GryffindorUserId);
                parameters.Add("@Name", profile.FirstName);
                parameters.Add("@Surname", profile.Surname);
                parameters.Add("@Avatar", profile.Avatar);
                parameters.Add("@Gender", profile.Gender);
                parameters.Add("@PreferredEmail", profile.PreferredEmail);
                parameters.Add("@PhoneNumber", profile.PhoneNumber);
                parameters.Add("@Bio", profile.Bio);
                parameters.Add("@HomeTown", profile.HomeTown);
                parameters.Add("@PreferredProfession", profile.PreferredProfession);
                parameters.Add("@PreferredWorkArea", profile.PreferredWorkArea);
                parameters.Add("@MainInterest", profile.MainInterest);
                parameters.Add("@IsVerified", profile.IsVerified);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    connection.Execute("up_UserProfile_UpdateUserProfile", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure);
                    result.Data = true;
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error updating profile", e);
                result.Message = "Error updating profile: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<bool> SetProfession(Guid userId, string profession)
        {
            var result = new GryffindorResponse<bool>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", userId);
                parameters.Add("@PreferredProfession", profession);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    connection.Execute("up_UserProfile_SetProfession", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure);
                    result.Data = true;
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error setting user profession", e);
                result.Message = "Error setting user profession: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<UserProfile> GetUserProfile(Guid userId, string username = null)
        {
            var result = new GryffindorResponse<UserProfile>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", userId);
                parameters.Add("@Username", username);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<UserProfile>("up_UserProfile_GetUserProfile", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error getting profile", e);
                result.Message = "Error getting profile: " + e.Message;
                return result;
            }
        }
        
        [OperationBehavior]
        public GryffindorResponse<List<string>> GetUserProfileAvatars()
        {
            var result = new GryffindorResponse<List<string>>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<string>("up_UserProfile_GetUserProfileAvatars", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).ToList();
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error getting profile avatars", e);
                result.Message = "Error getting profile avatars: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<ResumeDataModel> GetUserProfileResume(Guid userId, string username = null)
        {
            var result = new GryffindorResponse<ResumeDataModel>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", userId);
                parameters.Add("@Username", username);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = new ResumeDataModel();
                    using (var query = connection.QueryMultiple("up_UserProfile_GetUserProfileResume", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure))
                    {
                        result.Data.UserProfile = query.Read<UserProfile>().FirstOrDefault();
                        result.Data.Achievements = query.Read<UserProfileAchievement>().ToList();
                        result.Data.Education = query.Read<UserProfileEducation>().ToList();
                        result.Data.Skills = query.Read<UserProfileSkill>().ToList();
                        result.Data.Work = query.Read<UserProfileWork>().ToList();
                    }
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error getting profile resume", e);
                result.Message = "Error getting profile resume: " + e.Message;
                return result;
            }
        }
    }
}
