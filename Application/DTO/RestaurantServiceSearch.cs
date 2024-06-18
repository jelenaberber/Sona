using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class RestaurantServiceSearch : PagedSearch
    {
        public string Keyword { get; set; }
    }
}
