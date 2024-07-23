using Gryffindor.Contract.Dto;
using Gryffindor.Contract.Enums;
using Gryffindor.Web.Helpers;
using Gryffindor.Web.Models;
using Gryffindor.Web.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Gryffindor.Web.Controllers
{
    [Authorize]
    public class FeedsController : Controller
    {
        private readonly IFeedClientService _feedClientService;
        private readonly IChannelClientService _channelClientService;

        private static int _pageSize = 20;

        public FeedsController(IFeedClientService feedClientService, IChannelClientService channelClientService)
        {
            this._feedClientService = feedClientService;
            this._channelClientService = channelClientService;
        }

        [AllowAnonymous]
        public ActionResult Index(Guid id, int feedType)
        {
            feedType = feedType > Enum.GetNames(typeof(FeedType)).Length ? 1 : feedType; 

            FeedType feedTypeEnum = (FeedType)feedType;
            var model = _feedClientService.GetFeed(id, feedTypeEnum);
            if (model == null)
            {
                Session["Message"] = "We couldn't find the feed you're looking for :(";
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public ActionResult Feeds(string id)
        {
            var userId = LoggedInUserHelper.GetUserId(User.Identity);
            var channels = _channelClientService.GetUserChannels(userId);
            var channel = channels.FirstOrDefault(c => c.Name == id);
            var model = new BrowseFeedsViewModel()
            {
                UserChannels = channels,
                SelectedChannel = channel,
                PreferredJobArea = "Johannesburg"
            };
            ViewBag.Message = Session["Message"] == null ? null : Session["Message"].ToString();
            Session["Message"] = null;

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Jobs()
        {
            return View();
        }

        public ActionResult BrowseFeeds()
        {
            var userId = Helpers.LoggedInUserHelper.GetUserId(User.Identity);
            var channels = _channelClientService.GetUserChannels(userId);
            channels = channels.Any() ? channels.OrderBy(c => c.IsSelected).ToList() : channels;

            return View(channels);
        }

        [AllowAnonymous]
        public PartialViewResult GetFeeds_CareerJet(string keywords, string location, int pageSize)
        {
            var model = new SearchViewModel()
            {
                Keywords = keywords,
                Location = location,
                PageSize = pageSize
            };
            return PartialView("_JobFeed_CareerJet", model);
        }

        public PartialViewResult GetBannerFeeds_CareerJet(string keywords, string location, int pageSize)
        {

            var model = new SearchViewModel()
            {
                Keywords = keywords,
                Location = location,
                PageSize = pageSize
            };
            return PartialView("_JobFeedBanner_CareerJet", model);
        }

        public PartialViewResult GetUserProfileFeeds()
        {
            var userId = LoggedInUserHelper.GetUserId(User.Identity);
            var userProfileFeeds = _feedClientService.GetUserProfileFeeds(userId, 0, _pageSize);
            return PartialView("_ProfileFeed", userProfileFeeds);
        }

        public ActionResult PostFeed()
        {
            return View(new NewFeedViewModel() { FeedTypeId = 1 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostFeed(NewFeedViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Session["Message"] = "Something went wrong :( Please try again.";
                return View(model);
            }
            model.FeedTypeId = model.FeedTypeId > Enum.GetNames(typeof(FeedType)).Length ? 1 : model.FeedTypeId;

            FeedType feedTypeEnum = (FeedType)model.FeedTypeId;

            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;

            var feedId = Guid.NewGuid();
            var userId = new Guid(claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            
            var feed = new Feed()
            {
                FeedId = feedId,
                Title = model.Title,
                Text = model.Text,
                FeedTypeId = model.FeedTypeId,
                GryffindorUserId = userId,
                ImagePath = (model.ImageFile != null && model.ImageFile.ContentLength > 0 && Helper.IsImage(model.ImageFile.FileName))
                            ? SaveFile(model.ImageFile, userId, feedId)
                            : string.Empty,
            };

            var result =_feedClientService.AddOrUpdateFeed(feed, feedTypeEnum);

            if (!result)
            {
                Session["Message"] = "Something went wrong :( Please try again.";
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public bool AddLikeOrInterest(Guid feedId, int feedNotificationType, string text)
        {
            if (!Request.IsAjaxRequest() || !User.Identity.IsAuthenticated)
                return false;

            feedNotificationType = feedNotificationType > Enum.GetNames(typeof(NotificationType)).Length ? 1 : feedNotificationType;

            NotificationType feedNotificationTypeEnum = (NotificationType)feedNotificationType;

            var userId = Helpers.LoggedInUserHelper.GetUserId(User.Identity);
            var result = _feedClientService.AddLikeOrInterest(userId, feedId, feedNotificationTypeEnum, text);

            return result;
        }

        private string SaveFile(HttpPostedFileBase file, Guid userId, Guid feedId)
        {
            var fileName = "feed_" + feedId + Path.GetExtension(file.FileName);

            var path = ("User_" + userId);

            const string imageRoot = "~/Content/Images/UserPosts/";

            var mappedPath = Path.Combine(Server.MapPath(imageRoot), path);
            string imagePath = imageRoot.Replace("~", "../..") + path +"/"+ fileName;

            Directory.CreateDirectory(mappedPath);
            file.SaveAs(mappedPath +"/"+ fileName);

            return imagePath;
        }
    }
}