using Gryffindor.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using log4net;
using Gryffindor.Contract.Utilities;
using Gryffindor.Contract.Dto;
using System.Data.SqlClient;
using Dapper;
using Gryffindor.Service.Utilities;
using System.Data;
using Gryffindor.Contract.DataModels;
using Gryffindor.Contract.Enums;

namespace Gryffindor.Service
{
    public partial class GryffindorService : IGryffindorService
    {
        public GryffindorResponse<IList<UserProfile>> SearchUsers(string searchText, int page = 0, int pageSize = 10)
        {
            var result = new GryffindorResponse<IList<UserProfile>>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@SearchText", searchText);
                parameters.Add("@Page", page);
                parameters.Add("@PageSize", pageSize);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<UserProfile>("up_Search_FindPeople", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).ToList();
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error with SearchUsers", e);
                result.Message = "Error with SearchUsers: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<FeedsDataModel> SearchFeeds(string searchText, FeedType feedType, int page = 0, int pageSize = 10)
        {
            var result = new GryffindorResponse<FeedsDataModel>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@SearchText", searchText);
                parameters.Add("@FeedType", Convert.ToInt32(feedType));
                parameters.Add("@Page", page);
                parameters.Add("@PageSize", pageSize);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    using (var results = connection.QueryMultiple("up_Feed_SearchFeeds", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure))
                    {
                        result.Data = new FeedsDataModel();
                        result.Data.Feeds = results.Read<Feed>().ToList();
                        //result.Data.UserProfileDetails = results.Read<UserProfieDetails>().FirstOrDefault();
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error with SearchFeeds", e);
                result.Message = "Error with SearchFeeds: " + e.Message;
                return result;
            }
        }
    }
}
