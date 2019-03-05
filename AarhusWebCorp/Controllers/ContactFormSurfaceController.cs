using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AarhusWebCorp.ViewModels;
using System.Net.Mail;
using Umbraco.Web.Mvc;
using Umbraco.Core.Models;

namespace AarhusWebCorp.Controllers
{
    public class ContactFormSurfaceController : SurfaceController
    {
        // GET: ContactFormSurface
        public ActionResult Index()
        {
            return PartialView("ContactForm", new ContactForm());
        }

        [HttpPost]
        public ActionResult HandleFormSubmit(ContactForm model)
        {
            if (!ModelState.IsValid) return CurrentUmbracoPage();
           
            MailMessage message = new MailMessage();
            message.To.Add("aneke1234@gmail.com");
            message.Subject = model.Subject;
            message.From = new MailAddress(model.Email, model.Name);
            message.Body = model.Message;

            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("juozas.rastenis@gmail.com", "saknlmqjzhzogesy");
                // send mail
                smtp.Send(message);
                TempData["success"] = true;
            }

            IContent msg = Services.ContentService.CreateContent(model.Subject, CurrentPage.Id, "message");
            msg.SetValue("messageName", model.Name);
            msg.SetValue("email", model.Email);
            msg.SetValue("subject", model.Subject);
            msg.SetValue("messageContent", model.Message);

            // Save
            Services.ContentService.Save(msg);

            return RedirectToCurrentUmbracoPage();
        }
    }
}