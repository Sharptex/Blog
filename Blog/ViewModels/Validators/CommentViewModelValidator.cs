using Blog.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.DTO.Validators
{
    public class CommentViewModelValidator : AbstractValidator<CommentViewModel>
    {
        public CommentViewModelValidator()
        {
            RuleFor(x => x.Content).NotEmpty().WithMessage("Comment have to be not empty");
        }
    }
}
