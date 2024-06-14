using API.Core;
using Application.DTO;
using Application.UseCases.Commands.RestauranServices;
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
    public class RestaurantServicesController : ControllerBase
    {
        private Context _context;
        private IExceptionLogger _logger;
        private UseCaseHandler _handler;
        public RestaurantServicesController(Context context, IExceptionLogger logger, UseCaseHandler handler)
        {
            _context = context;
            _logger = logger;
            _handler = handler;
        }
        // GET: api/<RestaurantServicesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RestaurantServicesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RestaurantServicesController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateRestauranServicesDto dto, [FromServices] ICreateRestaurantServicesCommand command)
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

        // PUT api/<RestaurantServicesController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateRestauranServicesDto dto, [FromServices] IUpdateRestaurantServicesCommand command)
        {

            try
            {
                dto.Id = id;
                RestaurantService r = _context.RestaurantServices.FirstOrDefault(r => r.Id == id);
                if (r == null && r.IsActive == true)
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

        // DELETE api/<RestaurantServicesController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            RestaurantService r = _context.RestaurantServices.Find(id);
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
