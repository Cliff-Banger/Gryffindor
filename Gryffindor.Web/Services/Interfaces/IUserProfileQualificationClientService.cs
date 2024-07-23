using Gryffindor.Contract.Dto;
using Gryffindor.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gryffindor.Web.Services
{
    public interface IUserProfileQualificationClientService
    {
        bool AddOrUpdateUserProfileAchievement(UserProfileAchievement achievement);
        bool DeleteUserProfileAchievement(Guid achievementId, Guid userId);
        UserProfileAchievement GetUserProfileAchievement(Guid achievementId, Guid userId);
        List<UserProfileAchievement> GetAllUserProfileAchievementData(Guid userId);

        bool AddOrUpdateUserProfileEducation(UserProfileEducation education);
        bool DeleteUserProfileEducation(Guid educationId, Guid userId);
        UserProfileEducation GetUserProfileEducation(Guid educationId, Guid userId);
        List<UserProfileEducation> GetAllUserProfileEducationData(Guid userId);

        bool AddOrUpdateUserProfileSkill(UserProfileSkill skill);
        bool DeleteUserProfileSkill(Guid skillId, Guid userId);
        UserProfileSkill GetUserProfileSkill(Guid skillId, Guid userId);
        List<UserProfileSkill> GetAllUserProfileSkillData(Guid userId);

        bool AddOrUpdateUserProfileWork(UserProfileWork work);
        bool DeleteUserProfileWork(Guid workId, Guid userId);
        UserProfileWork GetUserProfileWork(Guid workId, Guid userId);
        List<UserProfileWork> GetAllUserProfileWorkData(Guid userId);
    }
}