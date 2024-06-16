using Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Commands.Services
{
    public interface IUpdateServiceCommand : ICommand<UpdateServiceDto>
    {
    }
}
