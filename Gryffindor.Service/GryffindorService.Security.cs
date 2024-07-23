using Gryffindor.Contract;
using System;
using System.Linq;
using System.Data;
using Gryffindor.Contract.Dto;
using System.ServiceModel;
using System.Data.SqlClient;
using Gryffindor.Service.Utilities;
using Dapper;
using Gryffindor.Contract.Utilities;

namespace Gryffindor.Service
{
    public partial class GryffindorService : IGryffindorService
    {
        [OperationBehavior]
        public GryffindorResponse<bool> ConfirmEmail(string email, Guid token)
        {
            var result = new GryffindorResponse<bool>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();           
                parameters.Add("@Email", email);
                parameters.Add("@Token", token);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<bool>("up_User_ConfirmEmail", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error calling ConfirmEmail", e);
                result.Message = "Error calling ConfirmEmail: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<bool> EmailOrUsernameExists(string username = "", string email = "")
        {
            var result = new GryffindorResponse<bool>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Username", username);
                parameters.Add("@Email", email);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<bool>("up_User_ValidateEmailAndUsername", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error calling EmailOrUsernameExists", e);
                result.Message = "Error calling EmailOrUsernameExists: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<GryffindorUser> LoginUser(string username, string passwordHash, string newPasswordHash = "")
        {
            var result = new GryffindorResponse<GryffindorUser>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Username", username);
                parameters.Add("@PasswordHash", passwordHash);
                parameters.Add("@NewPasswordHash", newPasswordHash);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<GryffindorUser>("up_User_LoginMigration", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    result.Result = true;
                }              
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error calling LoginUser", e);
                result.Message = "Error calling LoginUser: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<bool> RegisterUser(GryffindorUser user, UserProfile profile)
        {
            var result = new GryffindorResponse<bool>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", user.GryffindorUserId);
                parameters.Add("@Username", user.Username);
                parameters.Add("@FirstName", profile.FirstName);
                parameters.Add("@Surname", profile.Surname);
                parameters.Add("@Email", user.Email);
                parameters.Add("@PreferredWorkArea", profile.PreferredWorkArea);
                parameters.Add("@MainInterest", profile.MainInterest);
                parameters.Add("@PasswordHash", user.PasswordHash);
                parameters.Add("@PasswordSalt", user.PasswordSalt);
                parameters.Add("@Token", user.Token);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<bool>("up_User_Registration", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error calling RegisterUser", e);
                result.Message = "Error calling RegisterUser: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<bool> ResetPassword(string username, string tempPasswordHash, Guid token)
        {
            var result = new GryffindorResponse<bool>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Email", username);
                parameters.Add("@PasswordHash", tempPasswordHash);
                parameters.Add("@Token", token);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<bool>("up_User_ResetPassword", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error calling ResetPassword", e);
                result.Message = "Error calling ResetPassword: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<bool> UpdatePassword(string username, string oldPasswordHash, string newPasswordHash, Guid token)
        {
            var result = new GryffindorResponse<bool>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Email", username);
                parameters.Add("@NewPasswordHash", newPasswordHash);
                parameters.Add("@OldPasswordHash", oldPasswordHash);
                parameters.Add("@Token", token);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<bool>("up_User_ChangePassword", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error calling UpdatePassword", e);
                result.Message = "Error calling UpdatePassword: " + e.Message;
                return result;
            }
        }
    }
}
