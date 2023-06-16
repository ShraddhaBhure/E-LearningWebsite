﻿using System.Threading.Tasks;

namespace C_Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string recipientEmail, string subject, string body);

    }
}
