using Gryffindor.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Gryffindor.Contract.Dto;
using log4net;
using Gryffindor.Contract.Utilities;
using Gryffindor.Contract;

namespace Gryffindor.Web.Services
{
    public class ResumePermissionClientService : IResumePermissionClientService
    {
        private ILog _log = LogManager.GetLogger(typeof(ResumePermissionClientService));

        public bool AddOrUpdateResumePermission(ResumePermission permission)
        {
            bool result = false;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.AddOrUpdateResumePermission(permission).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling add or update resume permission", e);
                return result;
            }
        }

        public ResumePermission CanDownloadResume(string username, string sharedToEmail)
        {
            ResumePermission result = null;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.CanDownloadResume(username, sharedToEmail).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling can download resume", e);
                return result;
            }
        }

        public bool DeleteResumePermission(Guid resumePermissionId, Guid userId)
        {
            bool result = false;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.DeleteResumePermission(resumePermissionId, userId).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling delete resume permission", e);
                return result;
            }
        }

        public ResumePermission GetResumePermission(Guid userId, string sharedToEmail)
        {
            ResumePermission result = null;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.GetResumePermission(userId, sharedToEmail).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling get resume permission", e);
                return result;
            }
        }

        public bool UpdateResumeDownloads(string username, string sharedToEmail)
        {
            bool result = false;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.UpdateResumeDownloads(username, sharedToEmail).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling update resume downloads", e);
                return result;
            }
        }
    }
}