using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Core.Entity
{
    public class AppUser : IdentityUser
    {
        public ICollection<Order> userOrders { get; set; } = new HashSet<Order>();
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();

    }
}
