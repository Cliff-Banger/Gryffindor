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
        GryffindorResponse<bool> AddOrUpdateUserProfileAchievement(UserProfileAchievement achievement);

        [OperationContract]
        GryffindorResponse<bool> DeleteUserProfileAchievement(Guid achievementId, Guid userId);

        [OperationContract]
        GryffindorResponse<UserProfileAchievement> GetUserProfileAchievement(Guid achievementId, Guid userId);

        [OperationContract]
        GryffindorResponse<List<UserProfileAchievement>> GetAllUserProfileAchievementData(Guid userId);
    }
}
