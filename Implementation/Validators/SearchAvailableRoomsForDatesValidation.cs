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
    public class SearchAvailableRoomsForDatesValidation : AbstractValidator<SearchDatesAndGuests>
    {
        private Context _context;
        public SearchAvailableRoomsForDatesValidation(Context context)
        {
            _context = context;
            RuleFor(x => x.CheckIn)
            .NotNull().WithMessage("Check-in date is required.")
            .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Check-in date must be today or later.");

            RuleFor(x => x.CheckOut)
                .NotNull().WithMessage("Check-out date is required.")
                .GreaterThan(x => x.CheckIn).WithMessage("Check-out date must be later than check-in date.");

            RuleFor(x => x.NumberOfAdults)
                .GreaterThanOrEqualTo(0).WithMessage("Number of adults must be greater than or equal to 0.");

            RuleFor(x => x.NumberOfChildren)
                .GreaterThanOrEqualTo(0).WithMessage("Number of children must be greater than or equal to 0.");
        }
    }
}
