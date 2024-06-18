using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Booking : Entity
    {
        public int UserId { get; set; }
        public bool TravelingForWork { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public string GestName { get; set; }
        public string Request { get; set; }
        public int? RestaurantServiceId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public decimal FinalPrice { get; set; }

        public virtual User User { get; set; }
        public virtual RestaurantService RestaurantServices { get; set;}
        public virtual ICollection<BookingRoom> Rooms { get; set; }

    }
}
