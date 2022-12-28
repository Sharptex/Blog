using Blog.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.DTO.Validators
{
    public class UserViewModelValidator : AbstractValidator<UserViewModel>
    {
        public UserViewModelValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName have to be not empty");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName have to be not empty");
            RuleFor(x => x.Login).NotEmpty().WithMessage("Login have to be not empty");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password have to be not empty");
        }
    }
}
