﻿using System;
using System.Threading.Tasks;
using API.Dtos.EventDtos;
using API.Models;
using API.Services;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }
        
        [HttpPost]
        public async Task<IActionResult> AddEvent([FromForm] AddEventDto EventDto) 
        {
            await _eventService.AddEvent(EventDto);

            return Ok();
        }

        [HttpDelete("event/{id}")]
        public async Task<IActionResult> DeleteEventByEventId(Guid eventId)
        {
            await _eventService.DeleteEventById(eventId);

            return Ok();
        }

        [HttpGet("event/{id}")]
        public async Task<ActionResult<GetEventDto>> GetEventByEventId(Guid eventId)
        {
            await _eventService.GetEventById(eventId);

            return Ok();
        }
    }
}