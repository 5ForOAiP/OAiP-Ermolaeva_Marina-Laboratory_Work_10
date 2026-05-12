using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace lab10OAiP.services
{
        public static class EmailService
        {
            public static void SendResetCode(string toEmail, string code)
            {
                using var client = new SmtpClient("smtp.mail.ru", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("marinaerm411@mail.ru", "OGaHzs14etCcPou8Kj3K");

                var message = new MailMessage
                {
                    From = new MailAddress("marinaerm411@mail.ru", "Лабораторная работа"),
                    Subject = "Код восстановления пароля",
                    Body = $"<h2>Ваш код для сброса пароля: <span style='color:red;font-size:24px'>{code}</span></h2><p>Введите этот код в приложении, чтобы установить новый пароль.</p>",
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8
                };
                message.To.Add(toEmail);

                client.Send(message);
            }
        }
}
