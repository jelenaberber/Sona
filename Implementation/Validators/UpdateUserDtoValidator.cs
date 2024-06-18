using Application;
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
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        private Context _context;
        private readonly IApplicationActor _actor;
        public UpdateUserDtoValidator(Context context, IApplicationActor actor)
        {
            _actor = actor;
            _context = context;
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
                                 .EmailAddress().WithMessage("Not right format")
                                 .Must((dto, n) => !context.Users.Any(c => c.Email == n && _actor.Id != c.Id))
                                 .WithMessage("Email is already in use.");

            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.")
                                .Must((dto, n) => !context.Users.Any(c => c.Username == n && _actor.Id != c.Id))
                                .WithMessage("Username is already in use.");

            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.").MinimumLength(2).WithMessage("Minimum two characters.");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.").MinimumLength(2).WithMessage("Minimum two characters.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.").Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}$");
        }
    }
}
