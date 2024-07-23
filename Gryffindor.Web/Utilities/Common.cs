using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gryffindor.Web.Utilities
{
    public class Common
    {
        public static string RenderViewToString(Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;
            try
            {
                using (var stringWriter = new StringWriter())
                {
                    var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);

                    var viewContext = new ViewContext(controller.ControllerContext, viewResult.View,
                        controller.ViewData, controller.TempData, stringWriter);
                    viewResult.View.Render(viewContext, stringWriter);

                    return stringWriter.ToString();
                }
            }
            catch (Exception e)
            {
                return string.Empty;// e.Message;
            }
        }
    }
}