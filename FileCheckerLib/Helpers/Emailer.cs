using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;

namespace FileCheckerLib.Helpers
{

    public static class Emailer
    {
        /// <summary>
        /// Отправляет вложение
        /// </summary>
        /// <param name="attachmentPath">Вложение</param>
        public static string SendEmail(this List<string> logReceivers, string logFilePath)
        {
            string output = "";
            
            Attachment attachment = new Attachment(logFilePath);

            MailAddress fromMailAddress = new MailAddress(GlobalConfig.GetAppSettingsByKey("senderMail"));

            MailMessage mail = new MailMessage();

            // имя файла в тему письма
            mail.Subject = Path.GetFileName(logFilePath);
            mail.From = fromMailAddress;
            mail.Attachments.Add(attachment);
            mail.Body = StandardMessages.Mail.MailBody;

            SmtpClient client = new SmtpClient();

            foreach (string email in logReceivers)
            {
                try
                {
                    mail.To.Add(email);

                    client.Send(mail);

                    output += AddDateTimeToMessage(StandardMessages.Mail.LogSentSuccesfully) + email;
                }

                catch (SmtpException ex)
                {
                    output += AddDateTimeToMessage(StandardMessages.Mail.LogWasntSent) + email;
                    Console.WriteLine("Лог" + StandardMessages.Mail.LogWasntSent + email);
                    Console.WriteLine(StandardMessages.Mail.SmtpExMessage);
                }
            }

            mail.Dispose();
            attachment.Dispose();
            return output;
        }

        /// <summary>
        /// Возвращает входящее сообщение с добавленными датой и временем
        /// </summary>
        /// <param name="message">Входящее сообщене</param>
        /// <returns>Входящее сообщение с добавленными датой и временем</returns>
        static string AddDateTimeToMessage(string message)
        {
            DateTime now = DateTime.Now;

            return now.ToShortTimeString() + " " + now.ToShortDateString() + message;
        }
    }
}