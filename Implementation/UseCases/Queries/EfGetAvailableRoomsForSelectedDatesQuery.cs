using Application.DTO;
using Application.UseCases.Queries;
using DataAccess;
using FluentValidation;
using Implementation.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Implementation.UseCases.Queries
{
    public class EfGetAvailableRoomsForSelectedDatesQuery : EfUseCase, IGetAvailableRooms
    {
        private SearchAvailableRoomsForDatesValidation _validator;
        public EfGetAvailableRoomsForSelectedDatesQuery(Context context, SearchAvailableRoomsForDatesValidation validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 12;

        public string Name => "Get available rooms for searched dates";

        public PagedResponse<AvailableRoomDto> Execute(SearchDatesAndGuests search)
        {
            _validator.ValidateAndThrow(search);
            var checkInDate = search.CheckIn;
            var checkOutDate = search.CheckOut;
            var totalPeople = search.NumberOfAdults + search.NumberOfChildren;

            var query = from r in Context.Rooms
                        join br in Context.BookingRooms on r.Id equals br.RoomId into roomBookings
                        from br in roomBookings.DefaultIfEmpty()
                        join b in Context.Bookings on br.BookingId equals b.Id into bookings
                        from b in bookings.DefaultIfEmpty()
                        where r.IsActive == true
                        group new { r, br, b } by new
                        {
                            r.Id,
                            r.Name,
                            r.Capacity,
                            r.Size,
                            r.NumberOfUnits,
                            r.Price,
                            r.CreatedAt,
                            r.UpdatedAt,
                            r.IsActive
                        } into g
                        select new
                        {
                            Room = g.Key,
                            AvailableUnits = g.Key.NumberOfUnits - g.Where(x => x.b != null && x.b.CheckOut > checkInDate && x.b.CheckIn < checkOutDate)
                                                                   .Sum(x => (int?)x.br.quantity) ?? g.Key.NumberOfUnits
                        };

            query = query.Where(q => q.AvailableUnits > 0 && q.Room.Capacity >= totalPeople);

            int totalCount = query.Count();

            int perPage = search.PerPage.HasValue ? (int)Math.Abs((double)search.PerPage) : 10;
            int page = search.Page.HasValue ? (int)Math.Abs((double)search.Page) : 1;

            int skip = perPage * (page - 1);

            var pagedQuery = query.Skip(skip).Take(perPage).ToList();

            return new PagedResponse<AvailableRoomDto>
            {
                CurrentPage = page,
                TotalCount = totalCount,
                Data = pagedQuery.Select(q => new AvailableRoomDto
                {
                    Id = q.Room.Id,
                    Name = q.Room.Name,
                    Capacity = q.Room.Capacity,
                    Size = q.Room.Size,
                    Price = q.Room.Price,
                    AvailableRooms = q.AvailableUnits.Value
                }).ToList()
            };
        }
    }
    
}
