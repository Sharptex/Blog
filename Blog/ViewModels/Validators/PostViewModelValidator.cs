using Blog.ViewModels;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.DTO.Validators
{
    public class PostViewModelValidator : AbstractValidator<PostViewModel>
    {
        public PostViewModelValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title have to be not empty");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content have to be not empty");
            RuleFor(p => p.Tags).Must(x=>x.Any(y=>y.Selected)).WithMessage("At least one of tags should be selected");
        }
    }
}
