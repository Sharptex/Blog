using Blog.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.DTO.Validators
{
    public class TagViewModelValidator : AbstractValidator<TagViewModel>
    {
        public TagViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name have to be not empty");
        }
    }
}
