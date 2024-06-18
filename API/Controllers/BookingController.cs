using API.Core;
using Application.DTO;
using Application.UseCases.Commands.Bookings;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private Context _context;
        private IExceptionLogger _logger;
        private UseCaseHandler _handler;

        public BookingController(Context context, IExceptionLogger logger, UseCaseHandler handler)
        {
            _context = context;
            _logger = logger;
            _handler = handler;
        }


        // GET: api/<BookingController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<BookingController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BookingController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateBookingDto dto, [FromServices] ICreateBookingCommand command)
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

        // PUT api/<BookingController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateBookingDto dto, [FromServices] IUpdateBookingCommand command)
        {
            try
            {
                dto.Id = id;
                Booking b = _context.Bookings.FirstOrDefault(b => b.Id == id);
                if (b == null || b.IsActive == false)
                {
                    return NotFound();
                }
                _handler.HandleCommand(command, dto);
                return Ok();
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

        // DELETE api/<BookingController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Booking b = _context.Bookings.Find(id);
            if (b == null || b.IsActive == false)
            {
                return NotFound();
            }
            b.IsActive = false;
            _context.SaveChanges();
            return NoContent();
        }
    }
}
