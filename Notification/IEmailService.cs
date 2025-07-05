namespace Sanmol_Software_Assessment_AspDotNetCore_MVC.Notification
{
    public interface IEmailService
    {
        void SendAsync(string to, string subject, string htmlBody);
    }
}
