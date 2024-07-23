using Gryffindor.Contract.Dto;
using Gryffindor.Web.Models;
using Gryffindor.Web.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gryffindor.Web.Controllers
{
    [Authorize]
    public class OrganisationController : Controller
    {
        private readonly IOrganisationClientService _organisationClientService;

        public OrganisationController(IOrganisationClientService organisationClientService)
        {
            this._organisationClientService = organisationClientService;
        }

        public ActionResult Index()
        {
            var model = _organisationClientService.GetOrganisations();
            return View(model);
        }

        public ActionResult View(Guid id)
        {
            var model = _organisationClientService.GetOrganisation(id);
            if (model == null)
            {
                Session["Message"] = "Could not find the item you were looking for.";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult New()
        {
            var model = new OrganisationViewModel()
            { Organisation = new Organisation() };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(OrganisationViewModel model)
        {
            model.Organisation.OrganisationId = Guid.NewGuid();
            model.Organisation.OrganisationType = (int)model.OrganisationType;

            var logoPath = SaveFile(model.LogoFile, model.Organisation.OrganisationId, model.OrganisationType.ToString(), model.Organisation.Name, "Logo");
            var mainPicturePath = SaveFile(model.MainImageFile, model.Organisation.OrganisationId, model.OrganisationType.ToString(), model.Organisation.Name, "Banner");

            var userId = Helpers.LoggedInUserHelper.GetUserId(User.Identity);
            model.Organisation.CreatedBy = userId;
            model.Organisation.LogoPath = logoPath;
            model.Organisation.MainPicturePath = mainPicturePath;
            
            var result = _organisationClientService.AddOrUpdateOrganisation(model.Organisation);

            if (!result)
            {
                Session["Message"] = "Something went wrong. Please try again later.";
                return View(model);
            }
            return RedirectToAction("View", "Organisation", new { id = model.Organisation.OrganisationId });
        }

        public ActionResult Edit(Guid id)
        {
            var organisation = _organisationClientService.GetOrganisation(id);
            if (organisation == null)
            {
                Session["Message"] = "Could not find the item you were looking for.";
                return RedirectToAction("Index");
            }

            var model = new OrganisationViewModel()
            {
                Organisation = organisation,
                OrganisationType = (Contract.Enums.OrganisationType)organisation.OrganisationType
            };            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrganisationViewModel model)
        {
            model.Organisation.OrganisationId = Guid.NewGuid();
            model.Organisation.OrganisationType = (int)model.OrganisationType;

            var logoPath = SaveFile(model.LogoFile, model.Organisation.OrganisationId, model.OrganisationType.ToString(), model.Organisation.Name, "Logo");
            var mainPicturePath = SaveFile(model.MainImageFile, model.Organisation.OrganisationId, model.OrganisationType.ToString(), model.Organisation.Name, "Banner");

            var userId = Helpers.LoggedInUserHelper.GetUserId(User.Identity);
            model.Organisation.CreatedBy = userId;
            model.Organisation.LogoPath = logoPath;
            model.Organisation.MainPicturePath = mainPicturePath;

            var result = _organisationClientService.AddOrUpdateOrganisation(model.Organisation);

            if (!result)
            {
                Session["Message"] = "Something went wrong. Please try again later.";
                return View(model);
            }
            return RedirectToAction("View", "Organisation", new { id = model.Organisation.OrganisationId });
        }

        private string SaveFile(HttpPostedFileBase file, Guid organisationId, string organisationType, string organisationName, string fileName)
        {
            if (file == null || file.ContentLength < 1)
                return null;

            var fileExtension = Path.GetExtension(file.FileName);
            fileName = fileName + "_" + organisationId + fileExtension;

            var path = organisationType + "/" + organisationName;

            const string imageRoot = "~/Content/Images/Organisations/";

            var mappedPath = Path.Combine(Server.MapPath(imageRoot), path);
            string imagePath = imageRoot.Replace("~", "../..") + path + "/" + fileName;

            if (!string.IsNullOrEmpty(fileExtension))
            {
                Directory.CreateDirectory(mappedPath);
                file.SaveAs(mappedPath + "/" + fileName);
            }

            return imagePath;
        }
    }
}