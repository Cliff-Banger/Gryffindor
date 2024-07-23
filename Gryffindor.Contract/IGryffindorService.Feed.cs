using Gryffindor.Contract.DataModels;
using Gryffindor.Contract.Dto;
using Gryffindor.Contract.Enums;
using Gryffindor.Contract.Utilities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.ServiceModel;

namespace Gryffindor.Contract
{
    public partial interface IGryffindorService
    {
        [OperationContract]
        GryffindorResponse<Feed> GetFeed(Guid feedId, FeedType feedType);

        [OperationContract]
        GryffindorResponse<FeedsDataModel> GetFeeds(Guid userId, FeedType feedType, int page = 0, int pageSize = 10);

        [OperationContract]
        GryffindorResponse<FeedsDataModel> GetFeedInterests(Guid userId, int page = 0, int pageSize = 10);

        [OperationContract]
        GryffindorResponse<IList<Feed>> GetUserProfileFeeds(Guid userId, int page = 0, int pageSize = 10);

        [OperationContract]
        GryffindorResponse<bool> AddOrUpdateFeed(Feed feed, FeedType feedType, Guid modifiedBy = default(Guid));

        [OperationContract]
        GryffindorResponse<bool> AddLikeOrInterest(Guid loggedInUser, Guid feedId, NotificationType feedNotification, string text);

        [OperationContract]
        GryffindorResponse<bool> DeleteFeed(Guid feedId, Guid userId, FeedType feedType);
    }
}
