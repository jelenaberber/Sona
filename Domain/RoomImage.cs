using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class RoomImage
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string ImagePath { get; set; }
        
        public virtual Room Room { get; set; }
    }
}
