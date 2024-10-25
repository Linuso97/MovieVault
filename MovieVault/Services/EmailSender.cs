﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;

namespace MovieVault.Services
{
    public class EmailSender(IConfiguration _configuration) : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var fromAdress = _configuration["EmailSettings:DefaultEmailAdress"];
            var smtpServer = _configuration["EmailSettings:Server"];
            var smtpPort = Convert.ToInt32(_configuration["EmailSettings:Port"]);
            var message = new MailMessage
            {
                From = new MailAddress(fromAdress),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            message.To.Add(new MailAddress(email));

            using var client = new SmtpClient(smtpServer, smtpPort);
            await client.SendMailAsync(message);
        }
    }
}
