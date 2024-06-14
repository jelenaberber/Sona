using Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Commands.Users
{
    public interface IRegisterUserCommand : ICommand<RegisterUserDto>
    {
    }
}
