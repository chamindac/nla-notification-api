using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLA.NotificationAPI.Contracts;
using NLA.NotificationAPI.RestAPI.Configurations;
using NLA.NotificationAPI.Services;

namespace NLA.NotificationAPI.RestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly ILogger<NotificationController> _logger;
        private ApiSettings _settings;

        public NotificationController(ILogger<NotificationController> logger, IOptions<ApiSettings> settings)
        {
            _logger = logger;
            _settings = settings.Value;
        }

/// <summary>
        /// Create Email body
        /// Call SendEmail method in the email service
        /// POST: api/v{version:apiVersion}/Email/Customerregistration
        /// </summary>
        /// <param name="cancellationToken"></param>
        [HttpPost("customerregistration")]
        public async Task<ActionResult<Boolean>> SendCustomerRegistrationEmail(Customer registerCustomer, CancellationToken cancellationToken = default)
        {
            string activationUrl = $"{_settings.CustomerActivateUrl}/?key={registerCustomer.Id.ToString()}";

            StringBuilder sbMessageBody = new StringBuilder();
            FileStream fileStream = new FileStream("EmailTemplates/CustomerRegistration.html", FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                sbMessageBody.Append(reader.ReadToEnd());
            }

            sbMessageBody.Replace("{WebsiteName}", _settings.WebsiteName);
            sbMessageBody.Replace("{customerFirstName}", registerCustomer.FirstName);
            sbMessageBody.Replace("{customerLastName}", registerCustomer.LastName);
            sbMessageBody.Replace("{activationUrl}", activationUrl);

            EmailSender.SendEmail(_settings.SendGridApiKey, _settings.EmailSenderAddress, registerCustomer.Email,
                _settings.CustomerActivateEmailSubject, sbMessageBody.ToString());

            return Ok(true);
        }
    }
}
