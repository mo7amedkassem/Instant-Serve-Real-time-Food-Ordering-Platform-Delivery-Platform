using Job.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Entity
{
    public class Category : BaseEntity
    {

        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Product> products { get; set; } = new HashSet<Product>();
    }
}
