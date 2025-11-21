using Booking.Core.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Core.Entity
{
    public class Product : BaseEntity
    {

        public string Name { get; set; }
        public string Description { get; set; }

        [Precision(18, 2)]
        public decimal Price { get; set; }
        public bool Availability { get; set; }


        // Foreign Keys and Navigation Properties
 
        public string UserId { get; set; }      
        public AppUser User { get; set; }

        public int CategoryId { get; set; }
        public Category category { get; set; }
        public ICollection<Order> orders { get; set; } = new HashSet<Order>();
    }
}
