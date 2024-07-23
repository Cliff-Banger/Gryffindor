using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Gryffindor.Contract.Dto;
using Gryffindor.Contract.Enums;
using Gryffindor.Contract.Utilities;
using Gryffindor.Contract;
using Gryffindor.Contract.DataModels;

namespace Gryffindor.Web.Services
{
    public class FeedClientService : IFeedClientService
    {
        private ILog _log = LogManager.GetLogger(typeof(FeedClientService));

        public bool AddOrUpdateFeed(Feed feed, FeedType feedType, Guid modifiedBy = default(Guid))
        {
            var result = false;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.AddOrUpdateFeed(feed, feedType, modifiedBy).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling add feed", e);
                return result;
            }
        }

        public bool AddLikeOrInterest(Guid loggedInUser, Guid feedId, NotificationType feedNotification, string text)
        {
            var result = false;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.AddLikeOrInterest(loggedInUser, feedId, feedNotification,text).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling add feed like or interest", e);
                return result;
            }
        }

        public bool DeleteFeed(Guid feedId, Guid userId, FeedType feedType)
        {
            var result = false;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.DeleteFeed(feedId, userId, feedType).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling delete feed", e);
                return result;
            }
        }

        public Feed GetFeed(Guid feedId, FeedType feedType)
        {
            Feed result = null;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.GetFeed(feedId, feedType).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling get feed", e);
                return result;
            }
        }

        public FeedsDataModel GetFeeds(Guid userId, FeedType feedType, int page = 0, int pageSize = 10)
        {
            FeedsDataModel result = null;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.GetFeeds(userId, feedType, page, pageSize).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling get feeds", e);
                return result;
            }
        }

        public FeedsDataModel GetFeedInterests(Guid userId, int page = 0, int pageSize = 10)
        {
            FeedsDataModel result = null;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.GetFeedInterests(userId, page, pageSize).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling get feed interests", e);
                return result;
            }
        }

        public IList<Feed> GetUserProfileFeeds(Guid userId, int page = 0, int pageSize = 10)
        {
            IList<Feed> result = null;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.GetUserProfileFeeds(userId, page, pageSize).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling Get User Profile Feeds", e);
                return result;
            }
        }
    }
}