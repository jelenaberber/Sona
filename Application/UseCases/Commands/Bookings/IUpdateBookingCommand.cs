using Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Commands.Bookings
{
    public interface IUpdateBookingCommand : ICommand<UpdateBookingDto>
    {
    }
}
