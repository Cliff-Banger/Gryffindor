using Gryffindor.Contract;
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
    public class UserProfileQualificationClientService : IUserProfileQualificationClientService
    {
        private ILog _log = LogManager.GetLogger(typeof(SecurityClientService));

        public bool AddOrUpdateUserProfileAchievement(UserProfileAchievement achievement)
        {
            var result = false;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.AddOrUpdateUserProfileAchievement(achievement).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling add or update user profile achievement", e);
                return result;
            }
        }

        public bool AddOrUpdateUserProfileEducation(UserProfileEducation education)
        {
            var result = false;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.AddOrUpdateUserProfileEducation(education).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling add or update user profile education", e);
                return result;
            }
        }

        public bool AddOrUpdateUserProfileSkill(UserProfileSkill skill)
        {
            var result = false;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.AddOrUpdateUserProfileSkill(skill).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling add or update user profile skill", e);
                return result;
            }
        }

        public bool AddOrUpdateUserProfileWork(UserProfileWork work)
        {
            var result = false;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.AddOrUpdateUserProfileWork(work).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling add or update user profile work", e);
                return result;
            }
        }


        public bool DeleteUserProfileAchievement(Guid achievementId, Guid userId)
        {
            var result = false;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.DeleteUserProfileAchievement(achievementId, userId).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling delete user profile achievement", e);
                return result;
            }
        }

        public bool DeleteUserProfileEducation(Guid educationId, Guid userId)
        {
            var result = false;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.DeleteUserProfileEducation(educationId, userId).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling delete user profile education", e);
                return result;
            }
        }

        public bool DeleteUserProfileSkill(Guid skillId, Guid userId)
        {
            var result = false;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.DeleteUserProfileSkill(skillId, userId).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling delete user profile skill", e);
                return result;
            }
        }

        public bool DeleteUserProfileWork(Guid workId, Guid userId)
        {
            var result = false;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.DeleteUserProfileWork(workId, userId).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling delete user profile work", e);
                return result;
            }
        }


        public List<UserProfileAchievement> GetAllUserProfileAchievementData(Guid userId)
        {
            List<UserProfileAchievement> result = null;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.GetAllUserProfileAchievementData(userId).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling get all user profile achievement data", e);
                return result;
            }
        }

        public List<UserProfileEducation> GetAllUserProfileEducationData(Guid userId)
        {
            List<UserProfileEducation> result = null;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.GetAllUserProfileEducationData(userId).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling get all user profile education data", e);
                return result;
            }
        }

        public List<UserProfileSkill> GetAllUserProfileSkillData(Guid userId)
        {
            List<UserProfileSkill> result = null;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.GetAllUserProfileSkillData(userId).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling get all user profile skill data", e);
                return result;
            }
        }

        public List<UserProfileWork> GetAllUserProfileWorkData(Guid userId)
        {
            List<UserProfileWork> result = null;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.GetAllUserProfileWorkData(userId).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling get all user profile work data", e);
                return result;
            }
        }


        public UserProfileAchievement GetUserProfileAchievement(Guid achievementId, Guid userId)
        {
            UserProfileAchievement result = null;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.GetUserProfileAchievement(achievementId, userId).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling get user profile achievement", e);
                return result;
            }
        }

        public UserProfileEducation GetUserProfileEducation(Guid educationId, Guid userId)
        {
            UserProfileEducation result = null;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.GetUserProfileEducation(educationId, userId).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling get user profile education", e);
                return result;
            }
        }

        public UserProfileSkill GetUserProfileSkill(Guid skillId, Guid userId)
        {
            UserProfileSkill result = null;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.GetUserProfileSkill(skillId, userId).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling get user profile skill", e);
                return result;
            }
        }

        public UserProfileWork GetUserProfileWork(Guid workId, Guid userId)
        {
            UserProfileWork result = null;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.GetUserProfileWork(workId, userId).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling get user profile work", e);
                return result;
            }
        }
    }
}