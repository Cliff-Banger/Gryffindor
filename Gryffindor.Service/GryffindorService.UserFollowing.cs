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
        public GryffindorResponse<IList<UserFollowing>> GetFollowers(Guid userId)
        {
            var result = new GryffindorResponse<IList<UserFollowing>>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", userId);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<UserFollowing>("up_Following_GetFollowers", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).ToList();
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error calling GetFollowers", e);
                result.Message = "Error calling GetFollowers: " + e.Message;
                return result;
            }
        }
        
        [OperationBehavior]
        public GryffindorResponse<bool> FollowOrUnfollow(Guid userId, Guid selectedUserId)
        {
            var result = new GryffindorResponse<bool>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", userId);
                parameters.Add("@SelectedUserId", selectedUserId);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    connection.Execute("up_Following_Follow", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure);
                    result.Data = true;
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error calling FollowOrUnfollow", e);
                result.Message = "Error calling FollowOrUnfollow: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<bool> CheckUserFollowing(Guid userId, Guid selectedUserId)
        {
            var result = new GryffindorResponse<bool>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", userId);
                parameters.Add("@SelectedUserId", selectedUserId);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<bool>("up_Following_CheckFollowing", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error calling CheckFollowing", e);
                result.Message = "Error calling CheckFollowing: " + e.Message;
                return result;
            }
        }
    }
}
