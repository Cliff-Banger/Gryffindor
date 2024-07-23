using Gryffindor.Contract.DataModels;
using Gryffindor.Contract.Dto;
using Gryffindor.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gryffindor.Web.Services
{
    public interface IFeedClientService
    {
        Feed GetFeed(Guid feedId, FeedType feedType);

        FeedsDataModel GetFeeds(Guid userId, FeedType feedType, int page = 0, int pageSize = 10);

        FeedsDataModel GetFeedInterests(Guid userId, int page = 0, int pageSize = 10);

        IList<Feed> GetUserProfileFeeds(Guid userId, int page = 0, int pageSize = 10);

        bool AddOrUpdateFeed(Feed feed, FeedType feedType, Guid modifiedBy = default(Guid));

        bool AddLikeOrInterest(Guid loggedInUser, Guid feedId, NotificationType feedNotification, string text);

        bool DeleteFeed(Guid feedId, Guid userId, FeedType feedType);
    }
}