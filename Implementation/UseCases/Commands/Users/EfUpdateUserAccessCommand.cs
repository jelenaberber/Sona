using Application.DTO;
using Application.UseCases.Commands.Users;
using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Users
{
    public class EfUpdateUserAccessCommand : IUpdateUserAccessCommand
    {
        private Context _context;
        public EfUpdateUserAccessCommand(Context context)
        {
            _context = context;
        }

        public int Id => 18;

        public string Name => "Update user access";

        public void Execute(UpdateUserAccessDto data)
        {
            var user = _context.Users
                .Include(u => u.UseCases)
                .FirstOrDefault(x => x.Id == data.UserId);

            user.UseCases.Clear();

            foreach (var useCase in data.UseCaseIds)
            {
                user.UseCases.Add(new UserUseCase { UserId = user.Id, UseCaseId = useCase });
            }

            _context.SaveChanges();

        }
    }
}
