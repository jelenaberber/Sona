using Application.DTO;
using Application.UseCases.Commands.Rooms;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Implementation.UseCases.Commands.Rooms
{
    public class EfCreateRoomCommand : ICreateRoomCommand
    {
        private Context _context;
        private CreateRoomDtoValidator _validator;

        public EfCreateRoomCommand(Context context, CreateRoomDtoValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 2;
        public string Name => "Create new room";

        public void Execute(CreateRoomDto data)
        {
            _validator.ValidateAndThrow(data);

            foreach (var file in data.Images)
            {
                var tempFile = Path.Combine("wwwroot", "temp", file);
                var destinationFile = Path.Combine("wwwroot", "post", file);
                File.Move(tempFile, destinationFile);
            }

            Room roomToAdd = new Room
            {
                Name = data.Name,
                Capacity = data.Capacity,
                Size = data.Size,
                NumberOfUnits = data.NumberOfUnits,
                Price = data.Price,
                Images = data.Images.Select(x => new RoomImage
                {
                    ImagePath = x
                }).ToList()
            };

            _context.Rooms.Add(roomToAdd);
            _context.SaveChanges();

            var roomId = roomToAdd.Id;

            if(data.Services != null)
            {
                var activeServiceIds = _context.Services.Where(x => x.IsActive == true).Select(x => x.Id).ToList();

                var invalidServiceIds = data.Services.Except(activeServiceIds).ToList();

                if (invalidServiceIds.Any())
                {
                    throw new Exception($"Invalid ServiceIds: {string.Join(",", invalidServiceIds)}");
                }

                var roomServices = data.Services.Select(serviceId => new RoomService
                {
                    RoomId = roomId,
                    ServiceId = serviceId
                }).ToList();
                
                _context.RoomServices.AddRange(roomServices);
                _context.SaveChanges();
            }
            



        }
    }
}
