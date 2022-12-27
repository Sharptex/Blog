using Blog.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.DTO.Validators
{
    public class RoleViewModelValidator : AbstractValidator<RoleViewModel>
    {
        public RoleViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name have to be not empty");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description have to be not empty");
        }
    }
}
