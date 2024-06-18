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
    public class UpdateReviewDtoValidator : AbstractValidator<UpdateReviewDto>
    {
        private Context _context;
        public UpdateReviewDtoValidator(Context context)
        {
            _context = context;

            RuleFor(x => x.Comment).NotEmpty().WithMessage("Field is required").MaximumLength(300).WithMessage("Maximum length is 300 caracters.");

            RuleFor(x => x.Rate).NotEmpty().WithMessage("Field is required")
                                .GreaterThan(0).WithMessage("Must be greater than 0")
                                .LessThanOrEqualTo(5).WithMessage("Must be equal or less than 0");
        }
    }
}
