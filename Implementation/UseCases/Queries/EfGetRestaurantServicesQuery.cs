using Application;
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
    public class EfGetRestaurantServicesQuery : EfUseCase, IGetRestaurantServicesQuery
    {
        public EfGetRestaurantServicesQuery(Context context) : base(context)
        {
        }

        public int Id => 10;

        public string Name => "Search restaurant services";

        public PagedResponse<RestaurantServiceDto> Execute(RestaurantServiceSearch search)
        {
            var query = Context.RestaurantServices.Where(x => x.IsActive == true).AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Name.Contains(search.Keyword));
            }

            int totalCount = query.Count();

            int perPage = search.PerPage.HasValue ? (int)Math.Abs((double)search.PerPage) : 10;
            int page = search.Page.HasValue ? (int)Math.Abs((double)search.Page) : 1;

            int skip = perPage * (page - 1);

            query = query.Skip(skip).Take(perPage);

            return new PagedResponse<RestaurantServiceDto>
            {
                CurrentPage = page,
                Data = query.Select(x => new RestaurantServiceDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    PricePerDay = x.PricePerDay,
                }).ToList(),
                PerPage = perPage,
                TotalCount = totalCount,
            };
        }
    }
}
