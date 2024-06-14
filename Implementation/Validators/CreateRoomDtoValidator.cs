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
    public class CreateRoomDtoValidator : AbstractValidator<CreateRoomDto>
    {
        private Context _context;
        public CreateRoomDtoValidator(Context context)
        {
            _context = context;
            RuleFor(x => x.Name).NotNull()
                                .WithMessage("Room name is required.")
                                .MinimumLength(3)
                                .WithMessage("Min number of characters is 3.")
                                .Must(name => !_context.Rooms.Any(c => c.Name == name))
                                .WithMessage("Room name is in use.");

            RuleFor(x => x.Capacity).NotNull()
                                    .WithMessage("Room capacity is required.")
                                    .InclusiveBetween(1, 8)
                                    .WithMessage("Room capacity must be between 1 and 8.");

            RuleFor(x => x.Size).NotNull()
                                .WithMessage("Room size is required.")
                                .GreaterThan(1)
                                .WithMessage("Rum size must be positive number.");

            RuleFor(x => x.NumberOfUnits).NotNull()
                                    .WithMessage("Number of units is required.")
                                    .GreaterThan(1)
                                    .WithMessage("Number of units must be at least 1.");

            RuleFor(x => x.Price).NotNull()
                                   .WithMessage("Price is required.")
                                   .GreaterThan(1)
                                   .WithMessage("Price must be greater than 0.");

            RuleFor(x => x.Images).NotEmpty()
                    .WithMessage("At least one image is required.")
                    .DependentRules(() =>
                    {
                        RuleForEach(x => x.Images).Must((x, fileName) =>
                        {
                            var path = Path.Combine("wwwroot", "temp", fileName);

                            var exists = Path.Exists(path);

                            return exists;
                        }).WithMessage("File doesn't exist.");
                    });
        }

        private bool AllRoomServicesExist(IEnumerable<int> ids)
        {
            if(ids == null ||!ids.Any()) 
            {
                return true;
            }
            int servicesIds = _context.Services.Count(x => ids.Contains(x.Id));
            return servicesIds == ids.Count();

        }

        public static implicit operator CreateRoomDtoValidator(CreateRestauranServicesDtoValidator v)
        {
            throw new NotImplementedException();
        }
    }
}
