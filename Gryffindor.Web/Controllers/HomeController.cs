using Gryffindor.Contract.Enums;
using Gryffindor.Web.Helpers;
using Gryffindor.Web.Models;
using Gryffindor.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gryffindor.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IFeedClientService _feedClientService;
        private readonly IChannelClientService _channelClientService;

        private static int _pageSize = 20;

        public HomeController(IFeedClientService feedClientService, IChannelClientService channelService)
        {
            this._feedClientService = feedClientService;
            this._channelClientService = channelService;
        }

        public ActionResult Index()
        {
            var userId = LoggedInUserHelper.GetUserId(User.Identity);
            var feeds = _feedClientService.GetFeeds(userId, FeedType.General, 0, _pageSize);

            var model = new FeedsViewModel()
            {
                //TopFeeds = feeds.Feeds.Take(5),
                //SponsoredFeeds = _feedClientService.GetFeeds(userId, FeedType.Sponsored, 0, 2),
                Feeds = feeds,
                //UserProfileFeeds = userProfileFeeds
            };
            ViewBag.Message = Session["Message"] == null ? null : Session["Message"].ToString();
            Session["Message"] = null;

            return View(model);
        }

        [AllowAnonymous]
        public PartialViewResult AboutPartial()
        {
            return PartialView("_About");
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "About Us";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Us.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Privacy()
        {
            ViewBag.Message = "Privacy Policy.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult TermsAndConditions()
        {
            ViewBag.Message = "Terms And Conditions.";

            return View();
        }
    }
}
