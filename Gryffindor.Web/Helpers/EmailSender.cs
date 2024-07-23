using Gryffindor.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Gryffindor.Web.Helpers
{
    public class EmailSender
    {
        public static void SendConfirmEmail(string emailAddress, string url)
        {
            //This email will contain a link with an activation token encrypted in the url
        }

        public static void SendIntruderEmail(string emailAddress)
        {
            //this email will just alert the non user that a registration was attempted using their details
        }

        public static Task<bool> SendForgotPasswordEmailAsync(Controller controller, ForgotPassowrdTemplateViewModel model)
        {
            return Task.Run(() => { 
                var body = Utilities.Common.RenderViewToString(controller, "ForgotPasswordTemplate", model);
                if (string.IsNullOrEmpty(body))
                    return false;

                var email = new Utilities.EmailUtility()
                {
                    Body = body,
                    Recipient = model.EmailAddress,
                    Subject = "Inception - Forgot Password",
                };
                email.Send();
                return true;
            });
        }

        public static bool SendForgotPasswordEmail(Controller controller, ForgotPassowrdTemplateViewModel model)
        {
            var body = Utilities.Common.RenderViewToString(controller, "ForgotPasswordTemplate", model);
            if (string.IsNullOrEmpty(body))
                return false;

            var email = new Utilities.EmailUtility()
            {
                Body = body,
                Recipient = model.EmailAddress,
                Subject = "Inception - Forgot Password",
            };
            email.Send();
            return true;
        }
    }
}