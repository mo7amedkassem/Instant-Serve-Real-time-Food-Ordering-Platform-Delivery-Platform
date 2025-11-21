using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Dtos
{
    public class ProductToreturnDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public bool Availability { get; set; }

        public string UserId { get; set; }
        public string User { get; set; }

        public int CategoryId { get; set; }
        public string category { get; set; }
    }
}
