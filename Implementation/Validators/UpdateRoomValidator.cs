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
    public class UpdateRoomValidator : AbstractValidator<UpdateRoomDto>
    {
        private Context _context;
        public UpdateRoomValidator(Context context)
        {
            _context = context;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.")
                                .Must((dto, n) => !context.Rooms.Any(c => c.Name == n && dto.Id != c.Id))
                                .WithMessage("Name is already in use.");

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

            //RuleFor(x => x.Images).NotEmpty()
            //        .WithMessage("At least one image is required.")
            //        .DependentRules(() =>
            //        {
            //            RuleForEach(x => x.Images).Must((x, fileName) =>
            //            {
            //                var path = Path.Combine("wwwroot", "temp", fileName);

            //                var exists = Path.Exists(path);

            //                return exists;
            //            }).WithMessage("File doesn't exist.");
            //        });
        }

    }
}
