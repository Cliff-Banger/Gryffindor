using Gryffindor.Contract.DataModels;
using Gryffindor.Contract.Dto;
using Gryffindor.Contract.Enums;
using Gryffindor.Web.Helpers;
using Gryffindor.Web.Models;
using Gryffindor.Web.Services;
using Gryffindor.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gryffindor.Web.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        private readonly ISearchClientService _searchClientService;
        private readonly IGeneralClientService _generalClientService;
        private readonly IUserProfileClientService _userProfileClientService;

        public SearchController(ISearchClientService searchClientService, IGeneralClientService generalClientService, IUserProfileClientService userProfileClientService, IUserProfileQualificationClientService qualificationService, IResumePermissionClientService resumePermissionClientService)
        {
            this._searchClientService = searchClientService;
            this._generalClientService = generalClientService;
            this._userProfileClientService = userProfileClientService;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var model = new SearchViewModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(SearchViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            switch(model.Criteria.ToLower())
            {
                case "people":
                    model.Users = _searchClientService.SearchUsers(model.Keywords).ToList();
                    break;
                case "posts":
                    model.Feeds = _searchClientService.SearchFeeds(model.Keywords, FeedType.Blogs);
                    break;
                default:
                    break;
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Results()
        {
            var model = new SearchViewModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Results(string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
                return View(searchText);

            var model = new SearchViewModel();
            model.Users = _searchClientService.SearchUsers(searchText);
            model.Feeds = _searchClientService.SearchFeeds(searchText, FeedType.Blogs);

            return View(model);
        }
    }
}
