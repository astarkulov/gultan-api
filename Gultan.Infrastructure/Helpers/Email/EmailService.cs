﻿using Gultan.Application.Common.Interfaces.Email;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Gultan.Infrastructure.Helpers.Email;

public class EmailService(IOptions<SmtpOptions> smtpOptions, IConfiguration configuration) : IEmailService
{
    public async void SendActivationMail(string email, string activationLink)
    {
        var link = configuration.GetValue<string>("ApiUrl") + $"/Auth/activate?link={activationLink}";
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Gultan", smtpOptions.Value.User));
        message.To.Add(new MailboxAddress(email, email)); 
        message.Subject = "Активация пользователя на Gultan"; 

        message.Body = new TextPart("html")
        {
            Text = $"<p>Для активации перейдите по <a href='{link}'>ссылке</a>.</p>"
        };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(smtpOptions.Value.Host, smtpOptions.Value.Port, true); // Обновляем для поддержки STARTTLS

            await client.AuthenticateAsync(smtpOptions.Value.User, smtpOptions.Value.Password);

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}