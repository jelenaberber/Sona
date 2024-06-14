using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class RestaurantService : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerDay { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
        
    }
}
