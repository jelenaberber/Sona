using System;
using System.Collections.Generic;
using System.Text;


namespace Application.DTO
{
    public class CreateRestauranServicesDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? PricePerDay { get; set; }
    }

    public class UpdateRestauranServicesDto : CreateRestauranServicesDto
    {
        public int Id { get; set; }
    }
}
