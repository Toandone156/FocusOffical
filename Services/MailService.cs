﻿using FocusOffical.Models;
using MailKit.Security;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace FocusOffical.Services
{
    public interface IMailService
    {
        public Task SendMailAsync(MailContent content);
        public string GetMailFromContent(string username, string mailType, string link);
    }

    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
		private readonly IWebHostEnvironment _hostEnvironment;

		public MailService(IOptions<MailSettings> mailSettings,
            //ILogger<IMailService> logger,
            IWebHostEnvironment hostEnvironment) 
        {
            _mailSettings = mailSettings.Value;
            //_logger = logger;
            _hostEnvironment = hostEnvironment;

		}

		public string GetMailFromContent(string username, string mailType, string link)
		{
            string path = _hostEnvironment.WebRootPath + "\\mailContent\\index.html";
			string mailTemplate = File.ReadAllText(path);
            mailTemplate = mailTemplate.Replace("[username]", username);
			mailTemplate = mailTemplate.Replace("[mailtype]", mailType);
			mailTemplate = mailTemplate.Replace("[mailbutton]", mailType.ToUpper());
			mailTemplate = mailTemplate.Replace("[resetlink]", link);

            return mailTemplate;
		}

		public async Task SendMailAsync(MailContent mailContent)
        {
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail);
            email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
            email.To.Add(MailboxAddress.Parse(mailContent.To));
            email.Subject = mailContent.Subject;


            var builder = new BodyBuilder();
            builder.HtmlBody = mailContent.Body;
            email.Body = builder.ToMessageBody();

            // dùng SmtpClient của MailKit
            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            try
            {
                await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {
                //_logger.LogInformation("Send mail fail");
                //_logger.LogError(ex.Message);
            }

            await smtp.DisconnectAsync(true);
        }
    }
}
