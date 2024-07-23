using Gryffindor.Contract.DataModels;
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
        GryffindorResponse<bool> UpdateUserProfile(UserProfile profile);

        [OperationContract]
        GryffindorResponse<bool> SetProfession(Guid userId, string profession);

        [OperationContract]
        GryffindorResponse<UserProfile> GetUserProfile(Guid userId, string username = null);

        [OperationContract]
        GryffindorResponse<List<string>> GetUserProfileAvatars();

        [OperationContract]
        GryffindorResponse<ResumeDataModel> GetUserProfileResume(Guid userId, string username = null);
    }
}
