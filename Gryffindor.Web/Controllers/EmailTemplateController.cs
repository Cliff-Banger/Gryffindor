using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gryffindor.Web.Controllers
{
    public class EmailTemplateController : Controller
    {
        // GET: EmailTemplate
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult WelcomeTemplate()
        {
            return View();
        }

        public ActionResult ForgotPasswordTemplate()
        {
            return View();
        }

        public ActionResult ResumeShareTemplate()
        {
            return View();
        }
    }
}