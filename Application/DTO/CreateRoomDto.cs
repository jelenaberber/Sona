using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class CreateRoomDto
    {
        public string Name { get; set; }
        public int? Capacity { get; set; }
        public int? Size { get; set; }
        public int? NumberOfUnits { get; set; }
        public decimal? Price { get; set; }
        public List<int> Services { get; set; }
        public IEnumerable<string> Images { get; set; }
    }

    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Capacity { get; set; }
        public int? Size { get; set; }
        public decimal? Price { get; set; }
        public ICollection<ImageDto> Images { get; set; }
        public ICollection<ServiceDto> Services { get; set; }
        public IEnumerable<ReviewDto> Reviews { get; set; }

    }

    public class AvailableRoomDto : RoomDto
    {
        public int AvailableRooms { get; set; }
    }


}
