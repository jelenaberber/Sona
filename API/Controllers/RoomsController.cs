using Application.DTO;
using Application.UseCases.Commands.Rooms;
using DataAccess;
using Implementation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using FluentValidation.Results;
using API.Extensions;
using Domain;
using ValidationException = FluentValidation.ValidationException;
using Application;
using API.Core;
using Application.UseCases.Queries;
using Microsoft.AspNetCore.Authorization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private Context _context;
        private IExceptionLogger _logger;
        private UseCaseHandler _handler;
        public RoomsController(Context context, IExceptionLogger logger, UseCaseHandler handler)
        {
            _context = context;
            _logger = logger;
            _handler = handler;
        }

        // GET: api/<RoomsController>
        [HttpGet]
        public IActionResult Get([FromQuery] RoomSearch search, [FromServices] IGetRoomsQuery query)
            => Ok(_handler.HandleQuery(query, search));

        // GET api/<RoomsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Room r = _context.Rooms.Find(id);
            if(r == null)
            {
                return NotFound();
            }
            RoomDto dto = new()
            {
                Id = r.Id,
                Name = r.Name,
                Capacity = r.Capacity,
                Size = r.Size,
                Price = r.Price,
            };
            
            return Ok(dto);
        }

        [HttpPost("available")]
        public IActionResult Post([FromBody] SearchedDatesDto search, [FromServices] IGetAvailableRooms query)
        {
            try
            {
                _handler.HandleQuery(query, search);
                return Ok(_handler.HandleQuery(query, search));
            }
            catch (ValidationException ex)
            {
                return UnprocessableEntity(ex.Errors.Select(x => new
                {
                    Error = x.ErrorMessage,
                    Property = x.PropertyName
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

            // POST api/<RoomsController>
            [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateRoomDto dto, [FromServices] ICreateRoomCommand command)
        {
            try
            {
                _handler.HandleCommand(command, dto);
                return StatusCode(201);
            }
            catch (ValidationException ex)
            {
                return UnprocessableEntity(ex.Errors.Select(x => new
                {
                    Error = x.ErrorMessage,
                    Property = x.PropertyName
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }


        // PUT api/<RoomsController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateRoomDto dto, [FromServices] IUpdateRoomCommand command)
        {

            try
            {
                dto.Id = id;
                Room r = _context.Rooms.FirstOrDefault(r => r.Id == id);
                if (r == null || r.IsActive == false)
                {
                    return NotFound();
                }
                _handler.HandleCommand(command, dto);
                return Ok(dto);
            }
            catch (ValidationException ex)
            {
                return UnprocessableEntity(ex.Errors.Select(x => new
                {
                    Error = x.ErrorMessage,
                    Property = x.PropertyName
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
            
        }

        // DELETE api/<RoomsController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Room r = _context.Rooms.Find(id);
            if (r == null || r.IsActive == false)
            {
                return NotFound();
            }
            r.IsActive = false;
            _context.SaveChanges();
            return NoContent();
        }
    }
}
