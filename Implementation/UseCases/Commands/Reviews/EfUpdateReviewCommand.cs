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
    public class EfUpdateReviewCommand : IUpdateReviewCommand
    {
        private Context _context;
        private UpdateReviewDtoValidator _validator;
        private readonly IApplicationActor _actor;

        public EfUpdateReviewCommand(Context context, UpdateReviewDtoValidator validator, IApplicationActor actor)
        {
            _context = context;
            _validator = validator;
            _actor = actor;
        }

        public int Id => 16;

        public string Name => "Update review";

        public void Execute(UpdateReviewDto data)
        {
            _validator.ValidateAndThrow(data);

            Review review = _context.Reviews.Find(data.Id);

            if(review.UserId != _actor.Id)
            {
                throw new Exception("Review can be changed only by its writter");
            }

            review.Rate = data.Rate;
            review.Comment = data.Comment;

            _context.SaveChanges();
        }
    }
}
