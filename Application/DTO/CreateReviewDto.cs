using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class CreateReviewDto
    {
        public int RoomId { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
    }

    public class UpdateReviewDto 
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
    }

}
