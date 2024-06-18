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
    public class UpdateBookingDtoValidator : AbstractValidator<UpdateBookingDto>
    {
        private Context _context;
        public UpdateBookingDtoValidator(Context context)
        {
            _context = context;

            RuleFor(x => x.TravelingForWork)
                .NotNull().WithMessage("Traveling for work must be specified.");

            RuleFor(x => x.NumberOfAdults)
                .GreaterThan(0).WithMessage("Number of adults must be greater than 0.");

            RuleFor(x => x.NumberOfChildren)
                .GreaterThanOrEqualTo(0).WithMessage("Number of children must be greater than or equal to 0.");

            RuleFor(x => x.GuestName)
                .NotEmpty().WithMessage("Guest name is required.")
                .MaximumLength(100).WithMessage("Guest name cannot be longer than 100 characters.");

            RuleFor(x => x.RestaurantServiceId)
                .GreaterThan(0).WithMessage("Restaurant service ID is required and must be greater than 0.");

            RuleFor(x => x.CheckIn)
                .NotNull().WithMessage("Check-in date is required.")
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Check-in date must be today or later.");

            RuleFor(x => x.CheckOut)
                .NotNull().WithMessage("Check-out date is required.")
                .GreaterThan(x => x.CheckIn).WithMessage("Check-out date must be later than check-in date.");

            RuleFor(x => x.FinalPrice)
                .GreaterThan(0).WithMessage("Final price must be greater than 0.");

            RuleFor(x => x.Rooms)
                .NotEmpty().WithMessage("At least one room must be selected.");
        }
    }
}
