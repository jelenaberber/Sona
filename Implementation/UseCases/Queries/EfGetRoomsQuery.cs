using Application.DTO;
using Application.UseCases.Queries;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries
{
    public class EfGetRoomsQuery : EfUseCase, IGetRoomsQuery
    {
        public EfGetRoomsQuery(Context context) : base(context)
        {
        }

        public int Id => 4;

        public string Name => "Search rooms";

        public PagedResponse<RoomDto> Execute(RoomSearch search)
        {
            var query = Context.Rooms.Where(x => x.IsActive == true).AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Name.Contains(search.Keyword) && x.IsActive == true);
            }


            int totalCount = query.Count();

            int perPage = search.PerPage.HasValue ? (int)Math.Abs((double)search.PerPage) : 10;
            int page = search.Page.HasValue ? (int)Math.Abs((double)search.Page) : 1;

            int skip = perPage * (page - 1);

            query = query.Skip(skip).Take(perPage);

            return new PagedResponse<RoomDto>
            {
                CurrentPage = page,
                Data = query.Select(x => new RoomDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Capacity = x.Capacity,
                    Size = x.Size,
                    Price = x.Price,
                    Images = x.Images.Select(y => y.ImagePath).ToArray(),
                    Services = x.Services.Select(s => new ServiceDto
                    {
                        Id = s.ServiceId,
                        Name = s.Service.Name,
                    }).ToList(),
                }).ToList(),
                PerPage = perPage,
                TotalCount = totalCount,
            };

        }
    }
}
