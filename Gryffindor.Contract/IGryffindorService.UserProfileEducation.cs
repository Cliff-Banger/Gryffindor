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
        GryffindorResponse<bool> AddOrUpdateUserProfileEducation(UserProfileEducation education);

        [OperationContract]
        GryffindorResponse<bool> DeleteUserProfileEducation(Guid educationId, Guid userId);

        [OperationContract]
        GryffindorResponse<UserProfileEducation> GetUserProfileEducation(Guid educationId, Guid userId);

        [OperationContract]
        GryffindorResponse<List<UserProfileEducation>> GetAllUserProfileEducationData(Guid userId);
    }
}
