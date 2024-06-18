using Application;
using Application.DTO;
using Application.UseCases.Commands.Bookings;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Bookings
{
    public class EfCreateBookingCommand : ICreateBookingCommand
    {
        private CreateBookingDtoValidator _validator;
        private Context _context;
        private readonly IApplicationActor _actor;

        public EfCreateBookingCommand(CreateBookingDtoValidator validator, Context context, IApplicationActor actor)
        {
            _validator = validator;
            _context = context;
            _actor = actor;
        }

        public int Id => 13;

        public string Name => "Create booking";

        public void Execute(CreateBookingDto data)
        {
            _validator.ValidateAndThrow(data);

            Booking booking = new Booking
            {
                UserId = _actor.Id,
                TravelingForWork = data.TravelingForWork,
                NumberOfAdults = data.NumberOfAdults,
                NumberOfChildren = data.NumberOfChildren,
                GestName = data.GuestName,
                Request = data.Request,
                RestaurantServiceId = data.RestaurantServiceId,
                CheckIn = data.CheckIn,
                CheckOut = data.CheckOut,
                FinalPrice = data.FinalPrice
            };

            _context.Bookings.Add(booking);

            var bookingRooms = data.Rooms.Select(x => new BookingRoom
            {
                BookingId = booking.Id,
                RoomId = x.RoomId,
                quantity = x.quantity
            }).ToList();

            booking.Rooms = bookingRooms;

            _context.SaveChanges();
        }
    }
}
