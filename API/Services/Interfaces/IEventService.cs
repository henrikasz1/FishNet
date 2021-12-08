﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos.EventDtos;
using Microsoft.AspNetCore.Http;

namespace API.Services
{
    public interface IEventService
    {
        Task AddEvent(AddEventDto Event);
        Task<GetEventDto> GetEventById(Guid eventId);
    }
}