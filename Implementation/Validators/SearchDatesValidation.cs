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
    public class SearchDatesValidation : AbstractValidator<SearchedDatesDto>
    {
        private Context _context;

        public SearchDatesValidation(Context context)
        {
            _context = context;


            RuleFor(x => x.CheckOut)
                .GreaterThan(x => x.CheckIn).WithMessage("Check-out date must be later than check-in date.");

        }
    }
}
