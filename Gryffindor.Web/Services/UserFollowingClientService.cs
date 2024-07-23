using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Gryffindor.Contract.Dto;
using Gryffindor.Contract.Utilities;
using Gryffindor.Contract;
using log4net;
using System.Threading.Tasks;

namespace Gryffindor.Web.Services
{
    public class UserFollowingClientService : IUserFollowingClientService
    {
        private ILog _log = LogManager.GetLogger(typeof(SecurityClientService));

        public bool CheckUserFollowing(Guid userId, Guid selectedUserId)
        {
            bool result = false;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.CheckUserFollowing(userId, selectedUserId).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling check UserFollowing", e);
                return result;
            }
        }

        public IList<UserFollowing> GetFollowers(Guid userId)
        {
            IList<UserFollowing> result = null;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.GetFollowers(userId).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling get UserFollowings", e);
                return result;
            }
        }

        public bool FollowOrUnfollow(Guid userId, Guid selectedUserId)
        {
            bool result = false;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.FollowOrUnfollow(userId, selectedUserId).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling request or remove following", e);
                return result;
            }
        }
    }
}