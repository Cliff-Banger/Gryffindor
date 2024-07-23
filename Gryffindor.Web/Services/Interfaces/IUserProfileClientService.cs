using Gryffindor.Contract.DataModels;
using Gryffindor.Contract.Dto;
using Gryffindor.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gryffindor.Web.Services
{
    public interface IUserProfileClientService
    {
        bool UpdateUserProfile(UserProfile profile);

        bool SetProfession(Guid userId, string profession);

        UserProfile GetUserProfile(Guid userId, string username = null);

        List<string> GetUserProfileAvatars();

        ResumeDataModel GetUserProfileResume(Guid userId, string username = null);
    }
}