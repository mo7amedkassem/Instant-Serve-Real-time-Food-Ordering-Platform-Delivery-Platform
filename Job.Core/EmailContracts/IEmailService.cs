using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.EmailContracts
{
    public interface IEmailService
    {
        Task SendEmailAsync(string receptor, string subject, string body);

    }
}
