using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Gryffindor.Web.Models;
using Gryffindor.Web.Services;

namespace Gryffindor.Web.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private readonly IChannelClientService _channelClientService;
        private readonly ISecurityClientService _securityClientService;

        public ManageController(IChannelClientService channelService, ISecurityClientService securityClientService)
        {
            this._channelClientService = channelService;
            this._securityClientService = securityClientService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult GetChannels()
        {
            var channels = _channelClientService.GetAllChannels();
            return PartialView("_Channels", channels);
        }

        public ActionResult ChangePassword()
        {
            var model = new ChangePasswordViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var email = Helpers.LoggedInUserHelper.GetUserDetails(User.Identity).PreferredEmail;
            var result = _securityClientService.UpdatePassword(email, model.OldPassword, model.NewPassword, new Guid());

            if (!result)
            {
                Session["Message"] = "Something went worng. Please try again";
                return View(model);
            }

            Session["Message"] = "Password successfully updated.";
            //TODO: Send email confirmation
            return RedirectToAction("Index");
        }
    }
}