using API.Core;
using Application;
using Application.DTO;
using Application.UseCases.Commands.Reviews;
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
    public class ReviewsController : ControllerBase
    {
        private Context _context;
        private IExceptionLogger _logger;
        private UseCaseHandler _handler;
        private readonly IApplicationActor _actor;

        public ReviewsController(Context context, IExceptionLogger logger, UseCaseHandler handler, IApplicationActor actor)
        {
            _context = context;
            _logger = logger;
            _handler = handler;
            _actor = actor;
        }
        // GET: api/<ReviewsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ReviewsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ReviewsController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateReviewDto dto, [FromServices] ICreateReviewCommand command)
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

        // PUT api/<ReviewsController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateReviewDto dto, [FromServices] IUpdateReviewCommand command)
        {
            try
            {
                dto.Id = id;
                Review r = _context.Reviews.FirstOrDefault(r => r.Id == id);
                if (r == null || r.IsActive == false)
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

        // DELETE api/<ReviewsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            Review r = _context.Reviews.Find(id);
            if (r == null || r.IsActive == false)
            {
                return NotFound();
            }
            if(r.UserId != _actor.Id)
            {
                return BadRequest();
            }
            r.IsActive = false;
            _context.SaveChanges();
            return NoContent();
        }
    }
}
