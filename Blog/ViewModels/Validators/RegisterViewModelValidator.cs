using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModels.Validators
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName have to be not empty");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName have to be not empty");
            RuleFor(x => x.Login).NotEmpty().WithMessage("Login have to be not empty");
            RuleFor(x => x.PasswordReg).NotEmpty().WithMessage("Password have to be not empty");
            RuleFor(x => x.PasswordReg).Length(5, 100);
            RuleFor(x => x.PasswordConfirm).Equal(x => x.PasswordReg);
        }
    }
}
