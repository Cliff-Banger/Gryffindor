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
using Gryffindor.Contract.Enums;
using Gryffindor.Contract.DataModels;

namespace Gryffindor.Service
{
    public partial class GryffindorService : IGryffindorService
    {
        [OperationBehavior]
        public GryffindorResponse<Feed> GetFeed(Guid feedId, FeedType feedType)
        {
            var result = new GryffindorResponse<Feed>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@FeedId", feedId);
                parameters.Add("@FeedType", Convert.ToInt32(feedType));

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<Feed>("up_Feed_GetFeed", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error with GetFeed", e);
                result.Message = "Error with GetFeed: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<FeedsDataModel> GetFeeds(Guid userId, FeedType feedType, int page = 0, int pageSize = 10)
        {
            var result = new GryffindorResponse<FeedsDataModel>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", userId);
                parameters.Add("@FeedType", Convert.ToInt32(feedType));
                parameters.Add("@Page", page);
                parameters.Add("@PageSize", pageSize);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    using (var results = connection.QueryMultiple("up_Feed_GetFeeds", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure))
                    {
                        result.Data = new FeedsDataModel();
                        result.Data.Feeds = results.Read<Feed>().ToList();
                        result.Data.UserProfileDetails = results.Read<UserProfieDetails>().FirstOrDefault();
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error with GetFeeds", e);
                result.Message = "Error with GetFeeds: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<FeedsDataModel> GetFeedInterests(Guid userId, int page = 0, int pageSize = 10)
        {
            var result = new GryffindorResponse<FeedsDataModel>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", userId);
                parameters.Add("@Page", page);
                parameters.Add("@PageSize", pageSize);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    using (var results = connection.QueryMultiple("up_Feed_GetFeedInterests", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure))
                    {
                        result.Data = new FeedsDataModel();
                        result.Data.Feeds = results.Read<Feed>().ToList();
                        result.Data.UserProfileDetails = results.Read<UserProfieDetails>().FirstOrDefault();
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error with GetFeeds", e);
                result.Message = "Error with GetFeeds: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<IList<Feed>> GetUserProfileFeeds(Guid userId, int page = 0, int pageSize = 10)
        {
            var result = new GryffindorResponse<IList<Feed>>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", userId);
                parameters.Add("@Page", page);
                parameters.Add("@PageSize", pageSize);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<Feed>("up_Feed_GetCandidateFeeds", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).ToList();
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error with GetUserProfileFeeds", e);
                result.Message = "Error with GetUserProfileFeeds: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<bool> AddOrUpdateFeed(Feed feed, FeedType feedType, Guid modifiedBy = default(Guid))
        {
            var result = new GryffindorResponse<bool>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@FeedId", feed.FeedId);
                parameters.Add("@GryffindorUserId", feed.GryffindorUserId);
                parameters.Add("@FeedType", Convert.ToInt32(feedType));
                parameters.Add("@Text", feed.Text);
                parameters.Add("@ImagePath", feed.ImagePath);
                parameters.Add("@RedirectUrl", feed.RedirectUrl);
                parameters.Add("@Source", feed.Source);
                parameters.Add("@ModifiedBy", modifiedBy);

                parameters.Add("@JobFeedId", feed.JobFeedId);
                parameters.Add("@Title", feed.Title);
                parameters.Add("@Company", feed.Company);
                parameters.Add("@Location", feed.Location);
                parameters.Add("@Salary", feed.Salary);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    connection.Execute("up_Feed_AddOrUpdateFeed", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure);
                    result.Data = true;
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error with AddOrUpdateFeed", e);
                result.Message = "Error with AddOrUpdateFeed: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<bool> AddLikeOrInterest(Guid loggedInUser, Guid feedId, NotificationType feedNotification, string text)
        {
            var result = new GryffindorResponse<bool>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", loggedInUser);
                parameters.Add("@FeedId", feedId);
                parameters.Add("@FeedNotification", Convert.ToInt32(feedNotification));
                parameters.Add("@Text", text);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    connection.Execute("up_FeedNotification_AddFeedLikeOrInterest", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure);
                    result.Data = true;
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error with AddLikeOrInterest", e);
                result.Message = "Error with AddLikeOrInterest: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<bool> DeleteFeed(Guid feedId, Guid userId, FeedType feedType)
        {
            var result = new GryffindorResponse<bool>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@FeedId", feedId);
                parameters.Add("@GryffindorUserId", userId);
                parameters.Add("@FeedType", Convert.ToInt32(feedType));

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    connection.Execute("up_Feed_DeleteFeed", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure);
                    result.Data = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error with DeleteFeed", e);
                result.Message = "Error with DeleteFeed: " + e.Message;
                return result;
            }
        }
    }
}
