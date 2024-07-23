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
        GryffindorResponse<bool> RegisterUser(GryffindorUser user, UserProfile profile);

        [OperationContract]
        GryffindorResponse<GryffindorUser> LoginUser(string username, string passwordHash, string newPasswordHash = "");

        [OperationContract]
        GryffindorResponse<bool> ConfirmEmail(string email, Guid token);

        [OperationContract]
        GryffindorResponse<bool> UpdatePassword(string username, string oldPasswordHash, string newPasswordHash, Guid token);

        [OperationContract]
        GryffindorResponse<bool> ResetPassword(string username, string tempPasswordHash, Guid token);

        [OperationContract]
        GryffindorResponse<bool> EmailOrUsernameExists(string username = "", string email = "");
    }
}
