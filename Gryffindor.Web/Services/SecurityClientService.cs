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
    public class SecurityClientService : ISecurityClientService
    {
        private ILog _log = LogManager.GetLogger(typeof(SecurityClientService));

        public bool ConfirmEmail(string email, Guid token)
        {
            bool result = false;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.ConfirmEmail(email, token).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error confirming email", e);
                return result;
            }
        }

        public bool EmailOrUsernameExists(string username = "", string email = "")
        {
            bool result = false;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.EmailOrUsernameExists(username, email).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error checking email/username", e);
                return result;
            }
        }

        public GryffindorUser LoginUser(string username, string passwordHash, string newPasswordHash = "")
        {
            GryffindorUser result = null;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.LoginUser(username, passwordHash, newPasswordHash).Data;
                    return result;
                }
            }
            catch(Exception e)
            {
                _log.Error("Error logging in user", e);
                return result;
            }
        }

        public Task<GryffindorUser> LoginUserAsync(string username, string passwordHash, string newPasswordHash = "")
        {
            return Task.Run(() =>
            {
                GryffindorUser result = null;
                try
                {
                    using (var proxy = new ServiceProxy<IGryffindorService>())
                    {
                        result = proxy.Channel.LoginUser(username, passwordHash, newPasswordHash).Data;
                        return result;
                    }
                }
                catch (Exception e)
                {
                    _log.Error("Error logging in user", e);
                    return result;
                }
            });
        }

        public bool RegisterUser(GryffindorUser user, UserProfile profile)
        {
            bool result = false;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.RegisterUser(user, profile).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error registering user", e);
                return result;
            }
        }

        public Task<bool> RegisterUserAsync(GryffindorUser user, UserProfile profile)
        {
            return Task.Run(() =>
            {
                bool result = false;
                try
                {
                    using (var proxy = new ServiceProxy<IGryffindorService>())
                    {
                        result = proxy.Channel.RegisterUser(user, profile).Data;
                        return result;
                    }
                }
                catch (Exception e)
                {
                    _log.Error("Error registering user", e);
                    return result;
                }
            });
        }

        public bool ResetPassword(string username, string tempPasswordHash, Guid token)
        {
            bool result = false;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.ResetPassword(username, tempPasswordHash, token).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error resetting password", e);
                return result;
            }
        }

        public Task<bool> ResetPasswordAsync(string username, string tempPasswordHash, Guid token)
        {
            return Task.Run(() =>
            {
                bool result = false;
                try
                {
                    using (var proxy = new ServiceProxy<IGryffindorService>())
                    {
                        result = proxy.Channel.ResetPassword(username, tempPasswordHash, token).Data;
                        return result;
                    }
                }
                catch (Exception e)
                {
                    _log.Error("Error resetting password", e);
                    return result;
                }
            });
        }

        public bool UpdatePassword(string username, string oldPasswordHash, string newPasswordHash, Guid token)
        {
            bool result = false;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.UpdatePassword(username, oldPasswordHash, newPasswordHash, token).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error updating password", e);
                return result;
            }
        }
    }
}