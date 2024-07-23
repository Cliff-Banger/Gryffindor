using Gryffindor.Contract.Dto;
using Gryffindor.Contract.Enums;
using Gryffindor.Contract.Utilities;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Gryffindor.Contract
{
    public partial interface IGryffindorService
    {
        [OperationContract]
        GryffindorResponse<ResumePermission> GetResumePermission(Guid userId, string sharedToEmail);

        [OperationContract]
        GryffindorResponse<bool> AddOrUpdateResumePermission(ResumePermission permission);

        [OperationContract]
        GryffindorResponse<bool> DeleteResumePermission(Guid resumePermissionId, Guid userId);

        [OperationContract]
        GryffindorResponse<bool> UpdateResumeDownloads(string username, string sharedToEmail);

        [OperationContract]
        GryffindorResponse<ResumePermission> CanDownloadResume(string username, string sharedToEmail);
    }
}
