using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class UpdateUserAccessDto
    {
        public int UserId { get; set; }
        public IEnumerable<int> UseCaseIds { get; set; }
    }
}
