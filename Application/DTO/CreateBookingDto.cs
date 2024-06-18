using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class CreateBookingDto
    {
        public bool TravelingForWork { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public string GuestName { get; set; }
        public string Request { get; set; }
        public int RestaurantServiceId { get; set; }
        public DateTime CheckIn {  get; set; }
        public DateTime CheckOut {  get; set; }
        public decimal FinalPrice {  get; set; }
        public List<BookingRoomsDto> Rooms { get; set; }
    }

    public class UpdateBookingDto : CreateBookingDto
    {
        public int Id { get; set; }
    }
}
