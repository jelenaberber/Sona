using Application.DTO;
using Application.UseCases.Commands.Rooms;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Rooms
{
    public class EfUpdateRoomCommand : IUpdateRoomCommand
    {
        private Context _context;
        private UpdateRoomValidator _validator;

        public EfUpdateRoomCommand(Context context, UpdateRoomValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 5;
        public string Name => "Update room";

        public void Execute(UpdateRoomDto data)
        {
            _validator.ValidateAndThrow(data);

            Room roomToUpdate = _context.Rooms
                                .Include(r => r.Images)
                                .Include(r => r.Services)
                                .FirstOrDefault(r => r.Id == data.Id);

            //foreach (var file in data.Images)
            //{
            //    var tempFile = Path.Combine("wwwroot", "temp", file);
            //    var destinationFile = Path.Combine("wwwroot", "post", file);
            //    File.Move(tempFile, destinationFile);
            //}

            roomToUpdate.Name = data.Name;
            roomToUpdate.Capacity = data.Capacity;
            roomToUpdate.Size = data.Size;
            roomToUpdate.NumberOfUnits = data.NumberOfUnits;
            roomToUpdate.Price = data.Price;

            var existingServices = roomToUpdate.Services.Select(rs => rs.ServiceId).ToList();
            var newServices = data.Services.Except(existingServices).ToList();
            var removedServices = existingServices.Except(data.Services).ToList();

            foreach (var serviceId in removedServices)
            {
                var serviceToRemove = roomToUpdate.Services.FirstOrDefault(rs => rs.ServiceId == serviceId);
                if (serviceToRemove != null)
                {
                    _context.RoomServices.Remove(serviceToRemove);
                }
            }

            foreach (var serviceId in newServices)
            {
                roomToUpdate.Services.Add(new RoomService
                {
                    RoomId = roomToUpdate.Id,
                    ServiceId = serviceId
                });
            }

            _context.SaveChanges();
        }
    }
}
