using Gryffindor.Contract.Dto;
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
        GryffindorResponse<IList<Channel>> GetAllChannels();

        [OperationContract]
        GryffindorResponse<IList<Channel>> GetUserChannels(Guid userId);

        [OperationContract]
        GryffindorResponse<bool> AddOrRemoveUserChannels(Guid userId, List<Guid> channelsToRemove, List<Guid> channelsToAdd);
    }
}
