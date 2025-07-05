
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Net.Mail;
using System.Net;
using DocumentFormat.OpenXml.Vml;

namespace Sanmol_Software_Assessment_AspDotNetCore_MVC.Notification
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _cfg;
        public EmailService(IConfiguration cfg)
        {
            _cfg = cfg;
        }

        public void SendAsync(string to, string subject, string htmlBody)
        {
            string fromMail = "inframartonline@gmail.com";
            string fromPassword = "afnzxmohndggfazb";
            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = subject;
            message.To.Add(new MailAddress(to));
            message.Body = htmlBody;
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,

            };
            smtpClient.Send(message);
        }
    }
}
