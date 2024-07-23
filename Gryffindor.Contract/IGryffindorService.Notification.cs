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
        GryffindorResponse<IList<Notification>> GetUserNotifications(Guid userId);

        [OperationContract]
        GryffindorResponse<bool> AddOrRemoveNotification(Notification notification);

        [OperationContract]
        GryffindorResponse<int> CheckNotifications(Guid userId);

        [OperationContract]
        GryffindorResponse<bool> MarkNotificationsAsRead(Guid userId);
    }
}
