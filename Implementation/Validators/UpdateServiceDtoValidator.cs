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
    public class UpdateServiceDtoValidator : AbstractValidator<UpdateServiceDto>
    {
        private Context _context;

        public UpdateServiceDtoValidator(Context context)
        {
            _context = context;

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.")
                                .Must((dto, n) => !context.Services.Any(c => c.Name == n && dto.Id != c.Id))
                                .WithMessage("Name is already in use.");
        }
    }
}
