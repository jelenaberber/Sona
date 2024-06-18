using Application;
using Application.DTO;
using Application.UseCases.Commands.Users;
using DataAccess;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Users
{
    public class EfUpdateUserCommand : IUpdateUserCommand
    {
        private Context _context;
        private UpdateUserDtoValidator _validator;
        private readonly IApplicationActor _actor;

        public EfUpdateUserCommand(Context context, UpdateUserDtoValidator validator, IApplicationActor actor)
        {
            _context = context;
            _validator = validator;
            _actor = actor;
        }
        public int Id => 17;

        public string Name => "Update user";

        public void Execute(UpdateUserDto data)
        {
            _validator.ValidateAndThrow(data);

            var user = _context.Users.FirstOrDefault(u => u.Id == _actor.Id);

            user.Username = data.Username;
            user.FirstName = data.FirstName;
            user.LastName = data.LastName;
            user.Email = data.Email;
            user.Password = BCrypt.Net.BCrypt.HashPassword(data.Password);

            _context.SaveChanges();
        }
    }
}
