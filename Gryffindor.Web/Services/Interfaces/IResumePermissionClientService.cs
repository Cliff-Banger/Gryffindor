using Gryffindor.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gryffindor.Web.Services
{
    public interface IResumePermissionClientService
    {
        ResumePermission GetResumePermission(Guid userId, string sharedToEmail);

        bool AddOrUpdateResumePermission(ResumePermission permission);

        bool DeleteResumePermission(Guid resumePermissionId, Guid userId);

        bool UpdateResumeDownloads(string username, string sharedToEmail);

        ResumePermission CanDownloadResume(string username, string sharedToEmail);
    }
}
