using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class ServiceSearch : PagedSearch
    {
        public string Keyword { get; set; }
    }
}
