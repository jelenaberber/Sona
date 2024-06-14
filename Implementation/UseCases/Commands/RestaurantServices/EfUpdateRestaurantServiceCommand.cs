using Application.DTO;
using Application.UseCases.Commands.RestauranServices;
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
    public class EfUpdateRestaurantServiceCommand : IUpdateRestaurantServicesCommand
    {
        private Context _context;
        private UpdateRestaurantServicesDtoValidator _validator;
        public EfUpdateRestaurantServiceCommand(Context context, UpdateRestaurantServicesDtoValidator validator)
        {
            _context = context;
            _validator = validator;
        }
    
        public int Id => 7;

        public string Name => "Update restaurant service";

        public void Execute(UpdateRestauranServicesDto data)
        {
            _validator.ValidateAndThrow(data);
            RestaurantService service = _context.RestaurantServices.FirstOrDefault(x => x.Id == data.Id);

            service.Name = data.Name;
            service.Description = data.Description;
            service.PricePerDay = data.PricePerDay.Value;

            _context.SaveChanges();
        }
    }
}
