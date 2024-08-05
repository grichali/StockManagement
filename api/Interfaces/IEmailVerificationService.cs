using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    public interface IEmailVerificationService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}