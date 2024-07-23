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
        GryffindorResponse<bool> AddOrUpdateUserProfileSkill(UserProfileSkill skill);

        [OperationContract]
        GryffindorResponse<bool> DeleteUserProfileSkill(Guid skillId, Guid userId);

        [OperationContract]
        GryffindorResponse<UserProfileSkill> GetUserProfileSkill(Guid skillId, Guid userId);

        [OperationContract]
        GryffindorResponse<List<UserProfileSkill>> GetAllUserProfileSkillData(Guid userId);
    }
}
