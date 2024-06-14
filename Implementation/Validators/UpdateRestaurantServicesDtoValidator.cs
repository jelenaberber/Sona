using Application.DTO;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators
{
    public class UpdateRestaurantServicesDtoValidator : AbstractValidator<UpdateRestauranServicesDto>
    {
        private Context _context;
        public UpdateRestaurantServicesDtoValidator(Context context)
        {
            _context = context;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.")
                                .Must((dto, n) => !context.RestaurantServices.Any(c => c.Name == n && dto.Id != c.Id))
                                .WithMessage("Name is already in use.");

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
