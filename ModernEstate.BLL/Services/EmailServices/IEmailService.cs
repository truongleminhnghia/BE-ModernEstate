﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernEstate.BLL.Services.EmailServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject,string verifyUrl);

        Task SendEmailResetPasswordAsync(string to, string subject, string OTP);
    }
}
