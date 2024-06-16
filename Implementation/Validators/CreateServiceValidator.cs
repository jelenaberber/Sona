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
    public class CreateServiceValidator : AbstractValidator<CreateServiceDto>
    {
        private Context _context;
        public CreateServiceValidator(Context context)
        {
            this._context = context;

            RuleFor(x => x.Name).NotNull()
                                .WithMessage("Service name is required.")
                                .MinimumLength(3)
                                .WithMessage("Min number of characters is 3.")
                                .Must(name => !_context.Services.Any(c => c.Name == name))
                                .WithMessage("Service name is in use.");
        }
    }
}
