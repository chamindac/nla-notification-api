namespace NLA.NotificationAPI.RestAPI.Configurations
{
    public class ApiSettings
    {
        public string ContactUsUrl { get; set; }
        public string CustomerLoginUrl { get; set; }
        public string CustomerApiUrl { get; set; }
        public string CustomerActivateUrl { get; set; }
        public string CustomerActivateEmailSubject { get; set; }
        public string CustomerResetPwdUrl { get; set; }
        public string CustomerResetPwdEmailSubject { get; set; }
        public string EmailSenderAddress { get; set; }
        public string SendGridApiKey { get; set; }
        public string WebsiteName { get; set; }
        // public string IdentityServerUrl { get; set; }
        // public string Api_Notification_Audience { get; set; }
        // public string Client_Api_Id { get; set; }
        // public string Client_Api_Secret { get; set; }
    }
}