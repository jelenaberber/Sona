using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Service : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<RoomService> Rooms { get; set; }
    }
}
