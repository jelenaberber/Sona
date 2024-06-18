using Application;
using Application.DTO;
using Application.UseCases.Commands.Bookings;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Bookings
{
    public class EfUpdateBookingCommand : IUpdateBookingCommand
    {
        private CreateBookingDtoValidator _validator;
        private Context _context;

        public EfUpdateBookingCommand(CreateBookingDtoValidator validator, Context context)
        {
            _validator = validator;
            _context = context;
        }
        public int Id => 14;

        public string Name => "Update booking";

        public void Execute(UpdateBookingDto data)
        {
            _validator.ValidateAndThrow(data);

            var activeRoomIds = _context.Rooms.Where(x => x.IsActive == true).Select(x => x.Id).ToList();
            var restauranService = _context.RestaurantServices.Find(data.RestaurantServiceId);

            var invalidRoomIds = data.Rooms
                                .Select(x => x.RoomId)
                                .Except(activeRoomIds)
                                .ToList();

            if (invalidRoomIds.Any())
            {
                throw new Exception($"Invalid RoomIds: {string.Join(",", invalidRoomIds)}");
            }

            if (!restauranService.IsActive)
            {
                throw new Exception("Invalid RestaurantServiceId");
            }

            Booking booking  = _context.Bookings
                          .Include(b => b.Rooms)
                          .FirstOrDefault(x => x.Id == data.Id);
            booking.TravelingForWork = data.TravelingForWork;
            booking.NumberOfAdults = data.NumberOfAdults;
            booking.NumberOfChildren = data.NumberOfChildren;
            booking.GestName = data.GuestName;
            booking.Request = data.Request;
            booking.RestaurantServiceId = data.RestaurantServiceId;
            booking.CheckIn = data.CheckIn;
            booking.CheckOut = data.CheckOut;
            booking.FinalPrice = data.FinalPrice;

            var existingBookingRooms = booking.Rooms.ToList();

            var updatedBookingRooms = data.Rooms.Select(x => new BookingRoom
            {
                BookingId = booking.Id,
                RoomId = x.RoomId,
                quantity = x.quantity
            }).ToList();

            foreach (var updatedRoom in updatedBookingRooms)
            {
                var existingRoom = existingBookingRooms
                                    .FirstOrDefault(r => r.RoomId == updatedRoom.RoomId && r.BookingId == updatedRoom.BookingId);

                if (existingRoom != null)
                {
                    existingRoom.quantity = updatedRoom.quantity;
                }
                else
                {
                    booking.Rooms.Add(updatedRoom);
                }
            }

            foreach (var existingRoom in existingBookingRooms)
            {
                if (!updatedBookingRooms.Any(r => r.RoomId == existingRoom.RoomId && r.BookingId == existingRoom.BookingId))
                {
                    _context.BookingRooms.Remove(existingRoom);
                }
            }

            _context.SaveChanges();
        }
    }
}
