using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Review : Entity
    {
        public int Rate { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }

        public virtual User User { get; set; }
        public virtual Room Room { get; set; }
    }
}
