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
    public class EfGetUsersQuery : EfUseCase, IGetUsersQuery
    {
        public EfGetUsersQuery(Context context) : base(context)
        {
        }

        public int Id => 3;

        public string Name => "Search Users";

        public PagedResponse<UserDto> Execute(UserSearch search)
        {
            var query = Context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Username.Contains(search.Keyword) ||
                                         x.Email.Contains(search.Keyword) &&
                                         x.IsActive == true);
            }


            int totalCount = query.Count();

            int perPage = search.PerPage.HasValue ? (int)Math.Abs((double)search.PerPage) : 10;
            int page = search.Page.HasValue ? (int)Math.Abs((double)search.Page) : 1;

            //16 PerPage = 5, Page = 2

            int skip = perPage * (page - 1);

            query = query.Skip(skip).Take(perPage);

            return new PagedResponse<UserDto>
            {
                CurrentPage = page,
                Data = query.Select(x => new UserDto
                {
                    Id = x.Id,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Username = x.Username,
                }).ToList(),
                PerPage = perPage,
                TotalCount = totalCount,
            };
        }
    }
}
