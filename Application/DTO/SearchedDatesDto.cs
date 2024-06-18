using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class SearchedDatesDto : PagedSearch
    {
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        
    }

    public class SearchDatesAndGuests : SearchedDatesDto
    {
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
    }
}
