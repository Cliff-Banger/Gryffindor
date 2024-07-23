using Gryffindor.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gryffindor.Web.Controllers
{
    [Authorize]
    public class NotificationsController : Controller
    {
        private readonly IUserFollowingClientService _userFollowingClientService;
        private readonly INotificationClientService _notificationClientService;

        public NotificationsController(IUserFollowingClientService userFollowingClientService, INotificationClientService notificationClientService)
        {
            this._userFollowingClientService = userFollowingClientService;
            this._notificationClientService = notificationClientService;
        }

        public ActionResult Index()
        {
            var userId = Helpers.LoggedInUserHelper.GetUserId(User.Identity);
            var model = _notificationClientService.GetUserNotifications(userId);

            if (model.Count == 0)
                ViewBag.Message = "You have no new notifications.";

            return View(model);
        }

        [HttpGet]
        public bool CheckFollowing(Guid selectedUserId)
        {
            if (!Request.IsAjaxRequest())
                return false;

            var userId = Helpers.LoggedInUserHelper.GetUserId(User.Identity);

            var result = _userFollowingClientService.CheckUserFollowing(userId, selectedUserId);
            return result;
        }

        [HttpPost]
        public bool FollowUser(Guid selectedUserId)
        {
            if (!Request.IsAjaxRequest())
                return false;

            var userId = Helpers.LoggedInUserHelper.GetUserId(User.Identity);

            var result = _userFollowingClientService.FollowOrUnfollow(userId, selectedUserId);
            return result;
        }

        [HttpGet]
        public int CheckNotifications()
        {
            if (!Request.IsAjaxRequest())
                return 0;

            var userId = Helpers.LoggedInUserHelper.GetUserId(User.Identity);

            var result = _notificationClientService.CheckNotifications(userId);
            return result;
        }

        [HttpGet]
        public ActionResult GetNotifications()
        {
            if (!Request.IsAjaxRequest())
                return null;

            var userId = Helpers.LoggedInUserHelper.GetUserId(User.Identity);

            var model = _notificationClientService.GetUserNotifications(userId);
            return PartialView("_NotificationsPartial", model);
        }

        [HttpGet]
        public bool MarkNotificationsAsSeen()
        {
            if (!Request.IsAjaxRequest())
                return false;

            var userId = Helpers.LoggedInUserHelper.GetUserId(User.Identity);

            var result = _notificationClientService.MarkNotificationsAsRead(userId);
            return result;
        }
    }
}
