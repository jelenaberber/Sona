using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class CreateServiceDto
    {
        public string Name { get; set; }
    }

    public class UpdateServiceDto : CreateServiceDto
    {
        public int Id { get; set; }
    }
}
