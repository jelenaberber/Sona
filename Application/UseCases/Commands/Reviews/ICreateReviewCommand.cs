using Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Commands.Reviews
{
    public interface ICreateReviewCommand : ICommand<CreateReviewDto>
    {
    }
}
