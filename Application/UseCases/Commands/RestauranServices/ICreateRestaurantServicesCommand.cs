using Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Commands.RestauranServices
{
    public interface ICreateRestaurantServicesCommand : ICommand<CreateRestauranServicesDto> 
    {
    }
}
