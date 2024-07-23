using Gryffindor.Contract.DataModels;
using Gryffindor.Contract.Dto;
using Gryffindor.Contract.Enums;
using Gryffindor.Contract.Utilities;
using System.Collections.Generic;
using System.ServiceModel;

namespace Gryffindor.Contract
{
    public partial interface IGryffindorService
    {
        [OperationContract]
        GryffindorResponse<IList<UserProfile>> SearchUsers(string searchText, int page = 0, int pageSize = 10);

        [OperationContract]
        GryffindorResponse<FeedsDataModel> SearchFeeds(string searchText, FeedType feedType, int page = 0, int pageSize = 10);
    }
}
