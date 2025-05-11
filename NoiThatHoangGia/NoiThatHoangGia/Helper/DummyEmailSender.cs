using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Threading.Tasks;

namespace NoiThatHoangGia.Helper
{
    public class DummyEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Console.WriteLine($"[Fake Email] To: {email}, Subject: {subject}");
            return Task.CompletedTask;
        }
    }
}
