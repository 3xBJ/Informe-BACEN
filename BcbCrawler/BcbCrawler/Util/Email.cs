using System.Net;
using System.Net.Mail;

namespace BcbCrawler.Util
{
    class Email
    {
        static readonly MailAddress EmailDe = new MailAddress("sena.reve@gmail.com", "Luiz Sena");
        static readonly MailAddress EmaiPara = new MailAddress("sena.reve@gmail.com", "Time Basileia");
        const string fromPassword = "hknjnujdfmyujfyu";

        public static void EnviarEmail(string assunto, string textoEmail)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(EmailDe.Address, fromPassword)
            };

            using (var msg = new MailMessage(EmailDe, EmaiPara)
            {
                Subject = assunto,
                Body = textoEmail
            })
            {
                msg.IsBodyHtml = true;
                smtp.Send(msg);
            }
        }
    }
}
