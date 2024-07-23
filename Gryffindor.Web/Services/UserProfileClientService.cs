using Gryffindor.Contract;
using Gryffindor.Contract.DataModels;
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
    public class UserProfileClientService : IUserProfileClientService
    {
        private ILog _log = LogManager.GetLogger(typeof(SecurityClientService));

        public UserProfile GetUserProfile(Guid userId, string username = null)
        {
            UserProfile result = null;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.GetUserProfile(userId, username).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling get user profile", e);
                return result;
            }
        }

        public List<string> GetUserProfileAvatars()
        {
            List<string> result = null;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.GetUserProfileAvatars().Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling get user profile", e);
                return result;
            }
        }

        public bool SetProfession(Guid userId, string profession)
        {
            var result = false;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())   
                {
                    result = proxy.Channel.SetProfession(userId, profession).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling set profession", e);
                return result;
            }
        }

        public bool UpdateUserProfile(UserProfile profile)
        {
            var result = false;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.UpdateUserProfile(profile).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling update user profile", e);
                return result;
            }
        }

        public ResumeDataModel GetUserProfileResume(Guid userId, string username = null)
        {
            ResumeDataModel result = null;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.GetUserProfileResume(userId, username).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling get user profile resume", e);
                return result;
            }
        }
    }
}