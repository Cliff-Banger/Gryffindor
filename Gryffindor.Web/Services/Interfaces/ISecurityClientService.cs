using Gryffindor.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gryffindor.Web.Services
{
    public interface ISecurityClientService
    {
        bool RegisterUser(GryffindorUser user, UserProfile profile);

        Task<bool> RegisterUserAsync(GryffindorUser user, UserProfile profile);

        GryffindorUser LoginUser(string username, string passwordHash, string newPasswordHash = "");

        Task<GryffindorUser> LoginUserAsync(string username, string passwordHash, string newPasswordHash = "");

        bool ConfirmEmail(string email, Guid token);

        bool UpdatePassword(string username, string oldPasswordHash, string newPasswordHash, Guid token);

        bool ResetPassword(string username, string tempPasswordHash, Guid token);

        Task<bool> ResetPasswordAsync(string username, string tempPasswordHash, Guid token);

        bool EmailOrUsernameExists(string username = "", string email = "");
    }
}
