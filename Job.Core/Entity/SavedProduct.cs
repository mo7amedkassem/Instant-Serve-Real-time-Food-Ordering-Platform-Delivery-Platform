using Job.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Entity
{
    public class SavedProduct : BaseEntity
    {

        public DateTime SavedAt { get; set; } = DateTime.UtcNow;

        public string UserId { get; set; }
        public AppUser User { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; } 


    }
}
