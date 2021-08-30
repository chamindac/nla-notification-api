using System;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace NLA.NotificationAPI.Services
{
    public class EmailSender
    {
        /// <summary>
       /// Send Emails to the Customer
       /// </summary>
       /// <param name="sendGridApiKey"></param>
       /// <param name="emailSender"></param>
       /// <param name="emailRecever"></param>
       /// <param name="emailSubject"></param>
       /// <param name="emailHtmlContent"></param>
        public static void SendEmail(string sendGridApiKey, string emailSender, 
            string emailRecever, string emailSubject, 
             string emailHtmlContent )
        {
            var client = new SendGridClient(sendGridApiKey);
           
            var msg = MailHelper.CreateSingleEmail(
                new EmailAddress(emailSender),
                new EmailAddress(emailRecever),
                emailSubject,
                string.Empty,
                emailHtmlContent
            );

            var response =  client.SendEmailAsync(msg).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
    }
}
