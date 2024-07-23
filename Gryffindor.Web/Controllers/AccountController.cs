using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Gryffindor.Web.Models;
using Gryffindor.Web.Services;
using Gryffindor.Contract.Dto;
using Gryffindor.Web.Utilities;
using Gryffindor.Web.Helpers;
using Gryffindor.Contract.Utilities;

namespace Gryffindor.Web.Controllersm
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ISecurityClientService _securityService;
        private readonly IUserProfileClientService _userProfileClientService;

        public AccountController(ISecurityClientService securityService, IUserProfileClientService userProfileClientService)
        {
            this._securityService = securityService;
            this._userProfileClientService = userProfileClientService;
        }

        [AllowAnonymous]
        public ActionResult Index (string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.Message = Session["Message"];
            Session["Message"] = null;

            return View();
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            var model = new LoginViewModel();
            return PartialView("_LoginPartial", model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            var oldPassword = Security.Encode(model.Password);
            model.Password = Security.EncryptOneWaySha256(model.Password);
            var result = await _securityService.LoginUserAsync(model.Email, oldPassword, model.Password);

            if (result == null)
                Session["Message"] = "Incorrect username or password.";

            else if (result.IsSuspended)
                Session["Message"] = "Your account has been suspended. Please contact us for more info.";

            else if (result.IsLocked)
                Session["Message"] = "Your account has been locked. Please reset your password by clicking the 'Forgot Password link'.";

            else
            {
                if (!string.IsNullOrEmpty(returnUrl))
                    RedirectToAction(returnUrl);
                var profile = _userProfileClientService.GetUserProfile(result.GryffindorUserId);
                IdentitySignin(result, profile, "", true);
                return RedirectToAction("Index", "Home", new { message = result.IsActive ? "Welcome back!" : "Account reactivated. Welcome back!!" });
            }
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            var model = new RegisterViewModel();
            return PartialView("_RegisterPartial", model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Session["Message"] = "Oops, we could not register you. Please try again.";
                return RedirectToAction("Index");
            }

            var token = Guid.NewGuid();
            var user = new GryffindorUser()
            {
                GryffindorUserId = Guid.NewGuid(),
                Email = model.Email.Trim(),
                PasswordHash = Security.EncryptOneWaySha256(model.Password),
                PasswordSalt = Membership.GeneratePassword(6, 2),
                Token = token,
                Username = Helper.GetUsernameFromEmail(model.Email)
            };

            var profile = new UserProfile()
            {
                FirstName = model.Name.Trim(),
                Surname = model.Surname.Trim(),
                PreferredWorkArea = model.PreferredWorkArea.Trim(),
                MainInterest = (int)model.MainInterest
            };

            model.Password = Security.Encode(model.Password);
            var result = await _securityService.RegisterUserAsync(user, profile);

            if (!result)
            {
                Session["Message"] = "Oops, we could not register you. Please try again.";
                return RedirectToAction("Index");
            }
            else
            {
                Session["Message"] = "All registered. We'll let you in for now but don't forget to confirm your email (~^,)";
                EmailSender.SendConfirmEmail(model.Email, "");
                IdentitySignin(user, profile, "", true);
                return RedirectToAction("Profession", "Profile");
            }   
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            var userExists = _securityService.EmailOrUsernameExists("", model.Email);
            var emailTemplate = new ForgotPassowrdTemplateViewModel();

            if (!userExists)
            {
                // Don't reveal that the user does not exist or is not confirmed
                emailTemplate.Name = "Unknown User";
                emailTemplate.ResetUrl = emailTemplate.SiteUrl;

                //EmailSender.SendIntruderEmail(model.Email);           
                return View("ForgotPasswordConfirmation");
            }
            else
            {
                var token = Guid.NewGuid();
                var tempPasswordHash = Security.EncryptOneWaySha256(Membership.GeneratePassword(8, 3));

                var host = Request.Url.OriginalString.Replace(Request.Url.PathAndQuery, "") + Request.ApplicationPath;

                var queryString = $"token={token}&auth={tempPasswordHash}&user={model.Email}";
                queryString = Security.EncryptString(queryString, "gRyfF");

                var result = await _securityService.ResetPasswordAsync(model.Email, tempPasswordHash, token);
                
                emailTemplate.EmailAddress = model.Email;
                emailTemplate.Name = Helper.GetUsernameFromEmail(model.Email);
                emailTemplate.ResetUrl = $"{host}Account/ResetPassword?token={queryString}";

                //var emailResult = await EmailSender.SendForgotPasswordEmailAsync(this, emailTemplate);
                //return emailResult? View("ForgotPasswordConfirmation") : View(model);

                EmailSender.SendForgotPasswordEmail(this, emailTemplate);
                return View("ForgotPasswordConfirmation");
            }
        }

        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPassword()
        {
            var token = Request["token"] ?? "";
            token = Security.DecryptString(token.Replace(" ","+"), "gRyfF");

            var model = new ResetPasswordViewModel()
            {
                Email = HttpUtility.ParseQueryString(token).Get("user"),
                ResetToken = HttpUtility.ParseQueryString(token).Get("token"),
                OldPassword = HttpUtility.ParseQueryString(token).Get("auth")
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.OldPassword = model.OldPassword;
            model.Password = Security.EncryptOneWaySha256(model.Password);

            var result = _securityService.UpdatePassword(model.Email, model.OldPassword, model.Password, new Guid(model.ResetToken));

            if (!result)
            {
                Session["Message"] = "Error updating your password. Please try again.";
                return View(model);
            }
            Session["Message"] = "Password changed. Please login to continue.";
            return RedirectToAction("Index", "Home");
        }

        private void IdentitySignin(GryffindorUser user, UserProfile profile = null, string providerKey = null, bool isPersistent = false)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.GryffindorUserId.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, profile == null ? "" : profile.FirstName));
            claims.Add(new Claim(ClaimTypes.Surname, profile == null ? "" : profile.Surname));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.Locality, profile == null ? "" : profile.PreferredWorkArea));
            claims.Add(new Claim(ClaimTypes.Actor, profile == null ? "" : profile.MainInterest.ToString()));

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            // add to user here!
            AuthenticationManager.SignIn(new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = isPersistent,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            }, identity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie,
                                          DefaultAuthenticationTypes.ExternalCookie);
            return RedirectToAction("Index", "Account");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GenerateCollage()
        {
            if (!Request.IsAjaxRequest())
                return PartialView("_ImageCollagePartial", new List<string>());

            var model = _userProfileClientService.GetUserProfileAvatars();
            model = model == null ? new List<string>() : model;

            for (var i = 0; i < 7; i++)
            {
                model.Add("../../Content/Images/SamplePhotos/1.jpg");
                model.Add("../../Content/Images/SamplePhotos/2.jpg");
                model.Add("../../Content/Images/SamplePhotos/3.jpg");
                model.Add("../../Content/Images/SamplePhotos/4.jpg");
                model.Add("../../Content/Images/SamplePhotos/5.jpg");
                model.Add("../../Content/Images/SamplePhotos/6.jpg");
                model.Add("../../Content/Images/SamplePhotos/7.jpg");
                model.Add("../../Content/Images/SamplePhotos/8.jpg");
                model.Add("../../Content/Images/SamplePhotos/9.jpg");
                model.Add("../../Content/Images/SamplePhotos/10.jpg");
                model.Add("../../Content/Images/SamplePhotos/picture1.jpg");
                model.Add("../../Content/Images/SamplePhotos/Picture2.jpg");
            }
            return PartialView("_ImageCollagePartial", model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GenerateMobileCollage()
        {
            if (!Request.IsAjaxRequest())
                return PartialView("_ImageCollagePartial", new List<string>());

            var model = _userProfileClientService.GetUserProfileAvatars();
            model = model == null ? new List<string>() : model;

            for (var i = 0; i < 7; i++)
            {
                model.Add("../../Content/Images/SamplePhotos/Mobile/1.jpg");
                model.Add("../../Content/Images/SamplePhotos/Mobile/2.jpg");
                model.Add("../../Content/Images/SamplePhotos/Mobile/3.jpg");
                model.Add("../../Content/Images/SamplePhotos/Mobile/4.jpg");
                model.Add("../../Content/Images/SamplePhotos/Mobile/5.jpg");
                model.Add("../../Content/Images/SamplePhotos/Mobile/6.jpg");
                model.Add("../../Content/Images/SamplePhotos/Mobile/7.jpg");
                model.Add("../../Content/Images/SamplePhotos/Mobile/8.jpg");
                model.Add("../../Content/Images/SamplePhotos/Mobile/9.jpg");
                model.Add("../../Content/Images/SamplePhotos/Mobile/10.jpg");
                model.Add("../../Content/Images/SamplePhotos/Mobile/picture1.jpg");
                model.Add("../../Content/Images/SamplePhotos/Mobile/Picture2.jpg");
            }
            return PartialView("_ImageCollagePartial", model);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}