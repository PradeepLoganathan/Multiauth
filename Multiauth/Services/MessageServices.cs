using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Multiauth.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link https://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public SendGridOptions EmailOptions { get; } //set only via Secret Manager
        public TwilioSMSoptions SmsOptions { get; }

        public AuthMessageOptions MessageOptions;

        public AuthMessageSender(IOptions<AuthMessageOptions> optionsAccessor)
        {
            MessageOptions = optionsAccessor.Value;
        }

        //public AuthMessageSender(IOptions<SMSoptions> optionsAccessor)
        //{
        //    SmsOptions = optionsAccessor.Value;
        //}
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            return ExecuteSendGridMailer(MessageOptions.EmailOptions.SendGridKey, subject, message, email);
        }

        public Task ExecuteSendGridMailer(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("admin@bootminds.com", "BootMinds Admin"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));
            return client.SendEmailAsync(msg);
        }

        public Task SendSmsAsync(string number, string message)
        {
            return ExecuteTwilioSMS(number, message);
        }

        private Task ExecuteTwilioSMS(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            // Your Account SID from twilio.com/console
            
            var accountSid = MessageOptions.SMSOptions.SMSAccountIdentification;
            // Your Auth Token from twilio.com/console
            var authToken = MessageOptions.SMSOptions.SMSAccountPassword;

            TwilioClient.Init(accountSid, authToken);

            var msg = MessageResource.Create(
              to: new PhoneNumber(number),
              from: new PhoneNumber(MessageOptions.SMSOptions.SMSAccountFrom),
              body: message);
            return Task.FromResult(0);
        }
    }
}
