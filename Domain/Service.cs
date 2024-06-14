using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<RoomService> Rooms { get; set; }
    }
}
