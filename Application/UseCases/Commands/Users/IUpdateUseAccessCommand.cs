using Application.DTO;
using Application.UseCases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Commands.Users
{
    public interface IUpdateUseAccessCommand : ICommand<UpdateUserAccessDto>
    {
    }
}