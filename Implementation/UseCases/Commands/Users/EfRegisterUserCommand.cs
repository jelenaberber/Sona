using Application.DTO;
using Application.UseCases.Commands.Users;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Users
{
    public class EfRegisterUserCommand : IRegisterUserCommand
    {
        public int Id => 1;

        public string Name => "UserRegistration";

        private Context _context;
        private RegisterUserDtoValidator _validator;

        public EfRegisterUserCommand(Context context, RegisterUserDtoValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public void Execute(RegisterUserDto data)
        {
            _validator.ValidateAndThrow(data);

            User user = new User
            {
                Email = data.Email,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Password = BCrypt.Net.BCrypt.HashPassword(data.Password),
                Username = data.Username,
                UseCases = new List<UserUseCase>()
                {
                    new UserUseCase { UseCaseId = 4 }
                }
            };

            _context.Users.Add(user);

            _context.SaveChanges();
        }
    }
}
