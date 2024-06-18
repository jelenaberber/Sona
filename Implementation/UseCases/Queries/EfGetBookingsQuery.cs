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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Implementation.UseCases.Queries
{
    public class EfGetBookingsQuery : EfUseCase, IGetBookingsQuery
    {
        private SearchDatesValidation _validator;
        public EfGetBookingsQuery(Context context, SearchDatesValidation validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 16;

        public string Name => "Get bookings";

        public PagedResponse<BookingDto> Execute(SearchedDatesDto search)
        {
            _validator.ValidateAndThrow(search);

            var checkInDate = search.CheckIn;
            var checkOutDate = search.CheckOut;

            var query = Context.Bookings
                        .Include(u => u.User)
                        .Include(rs => rs.RestaurantServices)
                        .AsQueryable();

            if (checkInDate != null && checkOutDate != null)
            {
                query = query.Where(x => x.CheckIn == checkInDate && x.CheckOut == checkOutDate && x.IsActive == true);
            }
            else if (checkInDate != null)
            {
                query = query.Where(x => x.CheckIn == checkInDate);
            }

            // Materializacija rezultata u listu ovde
            var queryList = query.ToList();

            int totalCount = queryList.Count();

            int perPage = search.PerPage.HasValue ? (int)Math.Abs((double)search.PerPage) : 10;
            int page = search.Page.HasValue ? (int)Math.Abs((double)search.Page) : 1;

            int skip = perPage * (page - 1);

            var pagedQuery = queryList.Skip(skip).Take(perPage).ToList();

            return new PagedResponse<BookingDto>
            {
                CurrentPage = page,
                TotalCount = totalCount,
                Data = pagedQuery.Select(b => new BookingDto
                {
                    Id = b.Id,
                    TravelingForWork = b.TravelingForWork,
                    NumberOfAdults = b.NumberOfAdults,
                    NumberOfChildren = b.NumberOfChildren,
                    GuestName = b.GestName,
                    Username = b.User != null ? b.User.Username : null,
                    Request = b.Request,
                    RestaurantService = b.RestaurantServices != null ? b.RestaurantServices.Name : null,
                    RestaurantServiceId = b.RestaurantServiceId.Value,
                    CheckIn = b.CheckIn,
                    CheckOut = b.CheckOut,
                    FinalPrice = b.FinalPrice,
                    Rooms = b.Rooms.Select(x => new BookingRoomsDto
                    {
                        RoomId = x.RoomId,
                        quantity = x.quantity
                    }).ToList()
                }).ToList()
            };
        }

    }
}
