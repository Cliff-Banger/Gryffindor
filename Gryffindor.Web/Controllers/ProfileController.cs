using Gryffindor.Contract.Dto;
using Gryffindor.Contract.Enums;
using Gryffindor.Web.Models;
using Gryffindor.Web.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Gryffindor.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IChannelClientService _channelService;
        private readonly IGeneralClientService _generalClientService;
        private readonly IUserProfileClientService _userProfileClientService;
        private readonly IUserProfileQualificationClientService _qualificationClientService;

        public ProfileController(IChannelClientService channelService, IGeneralClientService generalClientService, IUserProfileClientService userProfileClientService, IUserProfileQualificationClientService qualificationService)
        {
            this._channelService = channelService;
            this._generalClientService = generalClientService;
            this._userProfileClientService = userProfileClientService;
            this._qualificationClientService = qualificationService;
        }

        public ActionResult Index()
        {
            var userId = Helpers.LoggedInUserHelper.GetUserId(User.Identity);
            var model = new UserProfileViewModel()
            {
                Profile = _userProfileClientService.GetUserProfile(userId),
            };

            if (model.Profile == null)
            {
                Session["Message"] = "Could not update your profile. Try again later";
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Message = Session["Messge"];
            Session["Message"] = null;
            model.IsMe = true;
            return View(model);
        }

        public ActionResult View(string id)
        {
            var model = new UserProfileViewModel()
            {
                Profile = _userProfileClientService.GetUserProfile(new Guid(), id),
            };

            if (model.Profile == null)
            {
                Session["Message"] = "Could not open the user's profile. Try again later";
                return RedirectToAction("Index", "Home");
            }           
            model.IsMe = model.Profile.GryffindorUserId == Helpers.LoggedInUserHelper.GetUserId(User.Identity);
            return View("Index", model);
        }

        public ActionResult Edit()
        {
            ViewBag.Message = Session["Message"];
            Session["Message"] = null;

            var userId = Helpers.LoggedInUserHelper.GetUserId(User.Identity);
            var profile = _userProfileClientService.GetUserProfile(userId);

            var model = new UserProfileViewModel()
            {
                Profile = profile,
                Gender = (Gender)profile.Gender,
                MainInterest = (MainInterest)profile.MainInterest
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserProfileViewModel model)
        {
            var result = PerformUpdateUserProfile(model);
            if (!result)
            {
                Session["Message"] = "Something went wrong. Please try again.";
                return View();
            }
            
            Session["Message"] = "Profile updated successfully.";
            return RedirectToAction("index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool UpdateUserProfileAsync(UserProfile model)
        {
            var userProfileModel = new UserProfileViewModel() { Profile = model, };
            var result = PerformUpdateUserProfile(userProfileModel);
            return result;
        }

        private bool PerformUpdateUserProfile(UserProfileViewModel model)
        {
            model.Profile.MainInterest = (int)model.MainInterest;
            model.Profile.Gender = (int)model.Gender;

            var userId = Helpers.LoggedInUserHelper.GetUserId(User.Identity);
            var profilePicturePath = SaveFile(model.ImageFile, userId);

            model.Profile.GryffindorUserId = userId;
            model.Profile.Avatar = profilePicturePath;

            return _userProfileClientService.UpdateUserProfile(model.Profile);
        }

        [HttpGet]
        [OutputCache(Duration = 10)]
        public JsonResult GetUserDetails()
        {
            if (!Request.IsAjaxRequest())
                return null;
            var userId = Helpers.LoggedInUserHelper.GetUserId(User.Identity);
            var profile = _userProfileClientService.GetUserProfile(userId);

            return Json(profile, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Profession()
        {
            ViewBag.Message = Session["Message"];
            Session["Message"] = null;
            return View();
        }

        public PartialViewResult ProfessionPartial()
        {
            var model = _generalClientService.GetProfessions();
            return PartialView("_ProfessionPartial", model);
        }

        public ActionResult SetProfession(string name)
        {
            var userId = Helpers.LoggedInUserHelper.GetUserId(User.Identity);
            name = Server.HtmlEncode(name);
            var result = _userProfileClientService.SetProfession(userId, name);

            if (result)
            {
                Session["Message"] = "Preferred profession updated.";
                return RedirectToAction("Index");
            }
            else
            {
                Session["Message"] = "Something went wrong. Please try again";
                return View("Profession");
            }
        }

        public ActionResult Interests()
        {
            ViewBag.Message = Session["Message"];
            Session["Message"] = null;
            var userId = Helpers.LoggedInUserHelper.GetUserId(User.Identity);
            var channels = _channelService.GetUserChannels(userId);

            var model = new InterestsViewModel()
            {
                UserChannels = channels.OrderByDescending(c => c.IsSelected).ToList(),
                HasInterests = channels.FirstOrDefault(c => c.IsSelected) != null,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Interests(string channelsToAdd, string channelsToRemove)
        {
            if (channelsToAdd == null && channelsToRemove == null)
                return View();

            var toAddStringList = channelsToAdd.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var toRemoveStringList = channelsToRemove.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            var toAdd = toAddStringList.Select(d => new Guid(d)).ToList();
            var toRemove = toRemoveStringList.Select(d => new Guid(d)).ToList();

            var userId = Helpers.LoggedInUserHelper.GetUserId(User.Identity);

            var result = _channelService.AddOrRemoveUserChannels(userId, toRemove, toAdd);

            if (!result)
            {
                Session["Message"] = "Something went wrong :( Please try again later.";
                return View();
            }

            Session["Message"] = "Interests successfully updated.";
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Qualifications()
        {
            ViewBag.Message = Session["Message"];
            int option;
            var optionParam = Request.Params["option"];

            Guid selectedId;
            var selectedIdParam = Request.Params["selectedId"];

            Guid.TryParse(selectedIdParam, out selectedId);

            var defaultOption = !string.IsNullOrEmpty(optionParam) && 
                int.TryParse(optionParam.ToString(), out option) ? Convert.ToInt32(optionParam) : 0;

            var type = Session["Data"] == null? defaultOption : Session["Data"];

            type = (int)type > -1 && (int)type <= Convert.ToInt32(QualificationType.Work) ? type : 0;

            ViewBag.Data = type;
            ViewBag.Selected = selectedId;

            Session["Message"] = null;
            Session["Data"] = null;

            return View();
        }

        [HttpGet]
        public PartialViewResult GetQualification(int type, Guid id = new Guid())
        {
            if (!Request.IsAjaxRequest())
                return null;

            var makeServiceCall = id != new Guid();
            var userId = Helpers.LoggedInUserHelper.GetUserId(User.Identity);
            var model = new QualificationViewModel();

            switch (type)
            {
                case 0:
                    model.UserProfileEducation = makeServiceCall ? _qualificationClientService.GetUserProfileEducation(id, userId) : new UserProfileEducation() { UserProfileEducationId = Guid.NewGuid() };
                    return PartialView("_AddEducationPartial", model);
                case 1:
                    model.UserProfileSkill = makeServiceCall ? _qualificationClientService.GetUserProfileSkill(id, userId) : new UserProfileSkill() { UserProfileSkillId = Guid.NewGuid() };
                    return PartialView("_AddSkillPartial", model);
                case 2:
                    model.UserProfileAchievement = makeServiceCall ? _qualificationClientService.GetUserProfileAchievement(id, userId) : new UserProfileAchievement() { UserProfileAchievementId = Guid.NewGuid() };
                    return PartialView("_AddAchievementPartial", model);
                case 3:
                    model.UserProfileWork = makeServiceCall ? _qualificationClientService.GetUserProfileWork(id, userId) : new UserProfileWork() { UserProfileWorkId = Guid.NewGuid() };
                    return PartialView("_AddWorkPartial", model);
                default:
                    model.UserProfileEducation = makeServiceCall ? _qualificationClientService.GetUserProfileEducation(id, userId) : new UserProfileEducation() { UserProfileEducationId = Guid.NewGuid() };
                    return PartialView("_AddEducationPartial", model);
            }
        }

        [HttpPost]
        public bool DeleteQualification(int type, Guid id)
        {
            if (!Request.IsAjaxRequest())
                return false;

            var userId = Helpers.LoggedInUserHelper.GetUserId(User.Identity);

            switch (type)
            {
                case 0:
                    return _qualificationClientService.DeleteUserProfileEducation(id, userId);
                case 1:
                    return _qualificationClientService.DeleteUserProfileSkill(id, userId);
                case 2:
                    return _qualificationClientService.DeleteUserProfileAchievement(id, userId);
                case 3:
                    return _qualificationClientService.DeleteUserProfileWork(id, userId);
                default:
                    return false;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveQualification(int type, QualificationViewModel model)
        {
            if (Request.IsAjaxRequest())
                return RedirectToAction("Index");

            PerformSaveQualification(type, model);

            return RedirectToAction("Qualifications");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool SaveQualificationAsync(int type, QualificationViewModel model)
        {
            //if (!Request.IsAjaxRequest())
                //return false;

            return PerformSaveQualification(type, model);
        }

        private bool PerformSaveQualification(int type, QualificationViewModel model)
        {
            var userId = Helpers.LoggedInUserHelper.GetUserId(User.Identity);
            var result = false;

            switch (type)
            {
                case 0:
                    model.UserProfileEducation.GryffindorUserId = userId;
                    result = Helpers.Helper.IsDateRangeValid(model.UserProfileEducation.DateFrom, model.UserProfileEducation.DateFrom);
                    _qualificationClientService.AddOrUpdateUserProfileEducation(model.UserProfileEducation);
                    Session["Message"] = result ? "Education added." : "Something went wrong. Please try again.";
                    break;
                case 1:
                    model.UserProfileSkill.GryffindorUserId = userId;
                    result = Helpers.Helper.IsDateRangeValid(model.UserProfileSkill.DateGained, model.UserProfileSkill.LastPracticed);
                    _qualificationClientService.AddOrUpdateUserProfileSkill(model.UserProfileSkill);
                    Session["Message"] = result ? "Skill added." : "Something went wrong. Please try again.";
                    break;
                case 2:
                    model.UserProfileAchievement.GryffindorUserId = userId;
                    result = Helpers.Helper.IsDateRangeValid(model.UserProfileAchievement.AchievedOn, null);
                    _qualificationClientService.AddOrUpdateUserProfileAchievement(model.UserProfileAchievement);
                    Session["Message"] = result ? "Achievement added." : "Something went wrong. Please try again.";
                    break;
                case 3:
                    model.UserProfileWork.GryffindorUserId = userId;
                    result = Helpers.Helper.IsDateRangeValid(model.UserProfileWork.DateFrom, model.UserProfileWork.DateTo);
                    _qualificationClientService.AddOrUpdateUserProfileWork(model.UserProfileWork);
                    Session["Message"] = result ? "Work experience added." : "Something went wrong. Please try again.";
                    break;
                default:
                    break;
            }
            Session["Data"] = type;
            return result;
        }

        [HttpGet]
        public PartialViewResult GetQualificationList(int type, string selected)
        {
            if (!Request.IsAjaxRequest() || string.IsNullOrEmpty(selected))
                return null;

            Guid userId;
            var validGuid = Guid.TryParse(selected, out userId);
            userId = validGuid ? new Guid(selected) : new Guid();
            userId = userId == new Guid()? Helpers.LoggedInUserHelper.GetUserId(User.Identity) : userId;

            switch (type)
            {
                case 0:
                    var education = _qualificationClientService.GetAllUserProfileEducationData(userId);
                    return PartialView("_ListOfEducationPartial", education);
                case 1:
                    var skill = _qualificationClientService.GetAllUserProfileSkillData(userId);
                    return PartialView("_ListOfSkillsPartial", skill);
                case 2:
                    var achievement = _qualificationClientService.GetAllUserProfileAchievementData(userId);
                    return PartialView("_ListOfAchievementsPartial", achievement);
                case 3:
                    var work = _qualificationClientService.GetAllUserProfileWorkData(userId);
                    return PartialView("_ListOfWorkPartial", work);
                default:
                    return null;
            }
        }

        private string SaveFile(HttpPostedFileBase file, Guid userId)
        {
            if (file == null || file.ContentLength < 1)
                return null;

            var fileExtension = Path.GetExtension(file.FileName);
            var fileName = "profile_" + userId + fileExtension;
            var coverFileName = "cover_" + fileName;

            var path = ("User_" + userId);

            const string imageRoot = "~/Content/Images/UserPosts/";

            var mappedPath = Path.Combine(Server.MapPath(imageRoot), path);
            string imagePath = imageRoot.Replace("~", "../..") + path + "/" + fileName;

            if (!string.IsNullOrEmpty(fileExtension))
            {
                Directory.CreateDirectory(mappedPath);
                file.SaveAs(Path.Combine(mappedPath, coverFileName));
            }

            var image = new WebImage(file.InputStream);
            
            if (image.Width > 130)
            {
                image.Resize(130, image.Height, true, true);
                image.Save(Path.Combine(mappedPath,fileName));
            }
            
            return imagePath;
        }
    }
}