using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Room : Entity
    {
        public string Name { get; set; }
        public int? Capacity { get; set; }
        public int? Size { get; set; }
        public int? NumberOfUnits { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<RoomService> Services { get; set; }
        public virtual ICollection<RoomImage> Images { get; set; } = new HashSet<RoomImage>();
        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public virtual ICollection<BookingRoom> Bookings { get; set; } = new HashSet<BookingRoom>();
    }
}
