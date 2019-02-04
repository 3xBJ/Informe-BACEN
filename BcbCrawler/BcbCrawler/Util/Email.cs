using System.Net;
using System.Net.Mail;

namespace BcbCrawler.Util
{
    class Email
    {
        static readonly MailAddress fromAddress = new MailAddress("", "Luiz Sena");
        static readonly MailAddress toAddress = new MailAddress("", "Time Basileia");
        const string fromPassword = "";

        public static void EnviarEmail(string assunto, string textoEmail)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = assunto,
                Body = textoEmail
            })
            {
                smtp.Send(message);
            }
        }
    }
}
