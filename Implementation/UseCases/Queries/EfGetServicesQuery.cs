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
    public class EfGetServicesQuery : EfUseCase, IGetServicesQuery
    {
        public EfGetServicesQuery(Context context) : base(context)
        {
        }

        public int Id => 11;

        public string Name => "Search room services";

        public PagedResponse<ServiceDto> Execute(ServiceSearch search)
        {
            var query = Context.Services.Where(x => x.IsActive == true).AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Name.Contains(search.Keyword) && x.IsActive == true);
            }


            int totalCount = query.Count();

            int perPage = search.PerPage.HasValue ? (int)Math.Abs((double)search.PerPage) : 10;
            int page = search.Page.HasValue ? (int)Math.Abs((double)search.Page) : 1;

            int skip = perPage * (page - 1);

            query = query.Skip(skip).Take(perPage);

            return new PagedResponse<ServiceDto>
            {
                CurrentPage = page,
                Data = query.Select(x => new ServiceDto
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList(),
                PerPage = perPage,
                TotalCount = totalCount,
            };
        }
    }
}
