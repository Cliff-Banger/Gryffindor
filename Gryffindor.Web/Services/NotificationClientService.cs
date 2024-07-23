using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Gryffindor.Contract.Dto;
using log4net;
using Gryffindor.Contract;
using Gryffindor.Contract.Utilities;

namespace Gryffindor.Web.Services
{
    public class NotificationClientService : INotificationClientService
    {
        private ILog _log = LogManager.GetLogger(typeof(SecurityClientService));

        public IList<Notification> GetUserNotifications(Guid userId)
        {
            IList<Notification> result = null;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.GetUserNotifications(userId).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling get user notifications", e);
                return result;
            }
        }

        public bool AddOrRemoveNotification(Notification notification)
        {
            var result = false;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.AddOrRemoveNotification(notification).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling get add or remove notifications", e);
                return result;
            }
        }

        public int CheckNotifications(Guid userId)
        {
            var result = 0;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.CheckNotifications(userId).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling get CheckNotifications", e);
                return result;
            }
        }

        public bool MarkNotificationsAsRead(Guid userId)
        {
            var result = false;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.MarkNotificationsAsRead(userId).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling get MarkNotificationsAsRead", e);
                return result;
            }
        }
    }
}