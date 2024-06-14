using Application.DTO;
using Application.UseCases.Commands.RestauranServices;
using Application.UseCases.Commands.Rooms;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.RestaurantServices
{
    public class EfCreateRestauranServicesCommand : ICreateRestaurantServicesCommand
    {
        private Context _context;
        private CreateRestauranServicesDtoValidator _validator;

        public EfCreateRestauranServicesCommand(Context context, CreateRestauranServicesDtoValidator validator)
        {
            _context = context;
            _validator = validator;
        }
        public int Id => 6;

        public string Name => "Create restaurant service";

        public void Execute(CreateRestauranServicesDto data)
        {
            _validator.ValidateAndThrow(data);

            RestaurantService service = new RestaurantService
            {
                Name = data.Name,
                PricePerDay = data.PricePerDay.Value,
                Description = data.Description,
            };

            _context.RestaurantServices.Add(service);
            _context.SaveChanges();
        }
    }
}
