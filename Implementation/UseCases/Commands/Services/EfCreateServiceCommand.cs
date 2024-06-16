using Application.DTO;
using Application.UseCases.Commands.Services;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Services
{
    public class EfCreateServiceCommand : ICreateServiceCommand
    {
        private Context _context;
        private CreateServiceValidator _validator;
        public EfCreateServiceCommand(Context context, CreateServiceValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 8;

        public string Name => "Create service";

        public void Execute(CreateServiceDto data)
        {
            _validator.ValidateAndThrow(data);
            Service service = new Service
            {
                Name = data.Name
            };

            _context.Services.Add(service);
            _context.SaveChanges();
        }
    }
}
