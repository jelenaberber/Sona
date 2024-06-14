using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class RoomService
    {
        public int RoomId { get; set; }
        public int ServiceId { get; set; }

        public virtual Room Room { get; set; }
        public virtual Service Service { get; set; }
    }
}
