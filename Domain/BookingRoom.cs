using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class BookingRoom
    {
        public int BookingId { get; set; }
        public int RoomId { get; set; }
        public int quantity { get; set; }

        public virtual Room Room { get; set; }
        public virtual Booking Booking { get; set; }

    }
}
