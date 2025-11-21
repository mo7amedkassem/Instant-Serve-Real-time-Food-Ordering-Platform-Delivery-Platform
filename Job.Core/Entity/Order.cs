using Booking.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Core.Entity
{
    public class Order : BaseEntity
    {

        public DateTime BookingDate { get; set; }
        public string Status { get; set; } = "Pending";


        // Foreign Keys and Navigation Properties

        public string UserId { get; set; }
        public AppUser User { get; set; }

        public Product product { get; set; }
        public int ProductId { get; set; }

    }
}
