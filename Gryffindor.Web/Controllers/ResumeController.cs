using Gryffindor.Contract.DataModels;
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
    public class ResumeController : Controller
    {
        private readonly IChannelClientService _channelService;
        private readonly IGeneralClientService _generalClientService;
        private readonly IUserProfileClientService _userProfileClientService;
        private readonly IUserProfileQualificationClientService _qualificationClientService;
        private readonly IResumePermissionClientService _resumePermissionClientService;

        public ResumeController(IChannelClientService channelService, IGeneralClientService generalClientService, IUserProfileClientService userProfileClientService, IUserProfileQualificationClientService qualificationService, IResumePermissionClientService resumePermissionClientService)
        {
            this._channelService = channelService;
            this._generalClientService = generalClientService;
            this._userProfileClientService = userProfileClientService;
            this._qualificationClientService = qualificationService;
            this._resumePermissionClientService = resumePermissionClientService;
        }

        // GET: Resume
        public ActionResult Index()
        {
            var model = _userProfileClientService.GetUserProfileResume(LoggedInUserHelper.GetUserId(User.Identity));

            return View(model);
        }

        // GET: Preview
        public ActionResult Preview()
        {
            var model = _userProfileClientService.GetUserProfileResume(LoggedInUserHelper.GetUserId(User.Identity));
            return View(model);
        }

        public ActionResult Wizard()
        {
            var userId = Helpers.LoggedInUserHelper.GetUserId(User.Identity);
            var profile = _userProfileClientService.GetUserProfile(userId);
            var model = new ResumeDataModel() { UserProfile = profile };
            return View(model);
        }

        [HttpGet]
        public bool CanDownloadResume(Guid id, string username)
        {
            if (!Request.IsAjaxRequest())
                return false;
            //TODO: Validate this against ResumePermission once implemented;
            var userProfile = _userProfileClientService.GetUserProfile(Helpers.LoggedInUserHelper.GetUserId(User.Identity));
            if (userProfile == null || userProfile.MainInterest != (int)MainInterest.Candidates)
                return false;
            else
                return true;
        } 

        public ActionResult DownloadUserResume(string username)
        {
            var model = _userProfileClientService.GetUserProfileResume(new Guid(), username);

            if (model == null || model.UserProfile == null)
            {
                Session["Message"] = "Could not download the CV you requested.";                
                return RedirectToAction("Index", "Home");;
            }
                 
            var view = "_ResumeDesign_0";

            var html = Common.RenderViewToString(this, view, model);
            var pdfBytes = HtmlCoverter.ConvertHtmlToPDF(html);

            WriteDocument(string.Format("InceptionCV_{0}{1}.pdf", model.UserProfile.FirstName, model.UserProfile.Surname), HtmlCoverter.ContentTypePdf, pdfBytes);

            return Redirect(Request.UrlReferrer.ToString());
        }

        private void WriteDocument(string fileName, string contentType, byte[] content)
        {
            HttpContext.Response.Clear();
            Response.ContentType = contentType;
            Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            Response.CacheControl = "No-cache";
            Response.BinaryWrite(content);
            Response.Flush();
            Response.SuppressContent = true;
            HttpContext.ApplicationInstance.CompleteRequest();
        }
    }
}