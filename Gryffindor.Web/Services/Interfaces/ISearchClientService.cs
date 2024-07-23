using Gryffindor.Contract.DataModels;
using Gryffindor.Contract.Dto;
using Gryffindor.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gryffindor.Web.Services
{
    public interface ISearchClientService
    {
        IList<UserProfile> SearchUsers(string searchText, int page = 0, int pageSize = 10);

        FeedsDataModel SearchFeeds(string searchText, FeedType feedType, int page = 0, int pageSize = 10);
    }
}