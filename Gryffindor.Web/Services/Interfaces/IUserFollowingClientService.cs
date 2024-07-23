using Gryffindor.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gryffindor.Web.Services
{
    public interface IUserFollowingClientService
    {
        IList<UserFollowing> GetFollowers(Guid userId);

        bool FollowOrUnfollow(Guid userId, Guid selectedUserId);

        bool CheckUserFollowing(Guid userId, Guid selectedUserId);
    }
}