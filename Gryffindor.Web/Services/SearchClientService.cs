using Gryffindor.Contract;
using Gryffindor.Contract.DataModels;
using Gryffindor.Contract.Dto;
using Gryffindor.Contract.Enums;
using Gryffindor.Contract.Utilities;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gryffindor.Web.Services
{
    public class SearchClientService : ISearchClientService
    {
        private ILog _log = LogManager.GetLogger(typeof(SearchClientService));

        public IList<UserProfile> SearchUsers(string searchText, int page = 0, int pageSize = 10)
        {
            IList<UserProfile> result = null;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.SearchUsers(searchText, page, pageSize).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling search feeds", e);
                return result;
            }
        }

        public FeedsDataModel SearchFeeds(string searchText, FeedType feedType, int page = 0, int pageSize = 10)
        {
            FeedsDataModel result = null;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.SearchFeeds(searchText, feedType, page, pageSize).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling search feeds", e);
                return result;
            }
        }
    }
}