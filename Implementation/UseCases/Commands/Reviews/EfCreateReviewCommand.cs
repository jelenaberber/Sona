using Application;
using Application.DTO;
using Application.UseCases.Commands.Reviews;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Reviews
{
    public class EfCreateReviewCommand : ICreateReviewCommand
    {
        private Context _context;
        private CreateReviewDtoValidator _validator;
        private IApplicationActor _actor;
        public EfCreateReviewCommand(Context context, CreateReviewDtoValidator validator, IApplicationActor actor)
        {
            _context = context;
            _validator = validator;
            _actor = actor;
        }

        public int Id => 15;

        public string Name => "Create review";

        public void Execute(CreateReviewDto data)
        {
            _validator.ValidateAndThrow(data);

            Review review = new Review
            {
                UserId = _actor.Id,
                Comment = data.Comment,
                Rate = data.Rate,
                RoomId = data.RoomId,
            };

            _context.Reviews.Add(review);
            _context.SaveChanges();
        }
    }
}
