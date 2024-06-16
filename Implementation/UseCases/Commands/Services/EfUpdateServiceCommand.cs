using Application.DTO;
using Application.UseCases.Commands.Services;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Services
{
    public class EfUpdateServiceCommand : IUpdateServiceCommand
    {
        private Context _context;
        private UpdateServiceDtoValidator _validator;

        public EfUpdateServiceCommand(Context context, UpdateServiceDtoValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 9;

        public string Name => "Update room service";

        public void Execute(UpdateServiceDto data)
        {
            _validator.ValidateAndThrow(data);
            Service service = _context.Services.FirstOrDefault(x => x.Id == data.Id);

            service.Name = data.Name;

            _context.SaveChanges();
        }
    }
}
