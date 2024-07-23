using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gryffindor.Web.Controllers
{
    public class InboxController : Controller
    {
        //
        // GET: /Inbox/

        public ActionResult Index()
        {
            return View();
        }

    }
}
