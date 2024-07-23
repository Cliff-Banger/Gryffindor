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
        GryffindorResponse<bool> AddOrUpdateUserProfileWork(UserProfileWork work);

        [OperationContract]
        GryffindorResponse<bool> DeleteUserProfileWork(Guid workId, Guid userId);

        [OperationContract]
        GryffindorResponse<UserProfileWork> GetUserProfileWork(Guid workId, Guid userId);

        [OperationContract]
        GryffindorResponse<List<UserProfileWork>> GetAllUserProfileWorkData(Guid userId);
    }
}
