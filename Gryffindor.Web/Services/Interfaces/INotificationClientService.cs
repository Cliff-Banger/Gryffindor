using Gryffindor.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gryffindor.Web.Services
{
    public interface INotificationClientService
    {
        IList<Notification> GetUserNotifications(Guid userId);

        bool AddOrRemoveNotification(Notification notification);

        int CheckNotifications(Guid userId);

        bool MarkNotificationsAsRead(Guid userId);
    }
}