using API.Core;
using Application.DTO;
using Application.UseCases.Commands.Services;
using Application.UseCases.Queries;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomServicesController : ControllerBase
    {

        private Context _context;
        private IExceptionLogger _logger;
        private UseCaseHandler _handler;
        public RoomServicesController(Context context, IExceptionLogger logger, UseCaseHandler handler)
        {
            _context = context;
            _logger = logger;
            _handler = handler;
        }
        // GET: api/<RoomServicesController>
        [HttpGet]
        public IActionResult Get([FromQuery] ServiceSearch search, [FromServices] IGetServicesQuery query)
            => Ok(_handler.HandleQuery(query, search));


        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateServiceDto dto, [FromServices] ICreateServiceCommand command)
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

        // PUT api/<RoomServicesController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateServiceDto dto, [FromServices] IUpdateServiceCommand command)
        {
            try
            {
                dto.Id = id;
                Service s = _context.Services.FirstOrDefault(s => s.Id == id);
                if(s == null)
                {
                    return NotFound();
                }
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

        // DELETE api/<RoomServicesController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Service service = _context.Services.FirstOrDefault(service => service.Id == id);
                if(service == null || service.IsActive == false)
                {
                    return NotFound();
                }
                service.IsActive = false;
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
