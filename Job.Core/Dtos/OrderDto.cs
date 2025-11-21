using Job.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Dtos
{
    public class OrderDto
    {
 
        public string Status { get; set; } = "Pending";
        public string UserId { get; set; }
        public string User { get; set; }
        public string product { get; set; }
        public int ProductId { get; set; }

    }
}
