﻿using API.Extensions;
using Application.DTO;
using Application.UseCases.Commands.Users;
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
    public class UsersController : ControllerBase
    {
        private UseCaseHandler _useCaseHandler;
        private Context _context;

        public UsersController(UseCaseHandler commandHandler, Context context)
        {
            _useCaseHandler = commandHandler;
            _context = context;
        }

        // GET: api/<UsersController>
        [Authorize]
        [HttpGet]
        public IActionResult Get([FromQuery] UserSearch search, [FromServices] IGetUsersQuery query)
            => Ok(_useCaseHandler.HandleQuery(query, search));


        // POST api/<UsersController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] RegisterUserDto dto, [FromServices] IRegisterUserCommand cmd)
        {
            try
            {
                //Autorizacija
                //Vreme trajanja izvrsavanja
                //Logging (belezenje pokusaja izvrsavanja)
                _useCaseHandler.HandleCommand(cmd, dto);

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
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                //error logging
                return this.InternalServerError(new { error = "An error has occured..." });
            }
        }

        // PUT api/<UsersController>/5
        [Authorize]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            User u = _context.Users.Find(id);
            if (u == null || u.IsActive == false)
            {
                return NotFound();
            }
            u.IsActive = false;
            _context.SaveChanges();
            return NoContent();
        }
    }
}
