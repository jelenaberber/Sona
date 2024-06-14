using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using DataAccess;
using FluentValidation;

namespace Implementation.Validators
{
    public class CreateRestauranServicesDtoValidator : AbstractValidator<CreateRestauranServicesDto>
    {
        private Context _context;
        public CreateRestauranServicesDtoValidator(Context context)
        {
            _context = context;

            RuleFor(x => x.Name).NotNull()
                                .WithMessage("Restaurant service name is required.")
                                .MinimumLength(3)
                                .WithMessage("Min number of characters is 3.")
                                .Must(name => !_context.RestaurantServices.Any(c => c.Name == name))
                                .WithMessage("Restaurant service name is in use.");

            RuleFor(x => x.PricePerDay).NotNull()
                                   .WithMessage("Price is required.")
                                   .GreaterThan(1)
                                   .WithMessage("Price must be greater than 0.");

            RuleFor(x => x.Description).NotNull()
                                       .WithMessage("Description is required")
                                       .MinimumLength(3)
                                       .WithMessage("Min number of characters is 30.");
        }
        
    }
}
