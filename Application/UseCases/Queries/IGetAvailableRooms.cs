﻿using Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Queries
{
    public interface IGetAvailableRooms : IQuery<PagedResponse<AvailableRoomDto>, SearchDatesAndGuests>
    {
    }
}
