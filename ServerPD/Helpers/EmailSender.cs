using System.Net;
using System.Net.Mail;
using ServerPD.Interfaces;

namespace ServerPD.Helper
{
    public class EmailSender : IEmailSender
    {
        public void Send(string body, string email)
        {
            SmtpClient client = new SmtpClient("gmail.com");
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("ontouragetest@gmail", "boulder840");

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("ontouragetest@gmail.com");
            mailMessage.To.Add(email);
            mailMessage.Body = body;
            mailMessage.Subject = "Product Delievery notification";
            client.Send(mailMessage);
        }
    }
}
