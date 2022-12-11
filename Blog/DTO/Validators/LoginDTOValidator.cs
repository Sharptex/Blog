using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.DTO.Validators
{
    public class LoginDTOValidator : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidator()
        {
            RuleFor(x => x.Login).NotEmpty().WithMessage("Login have to be not empty");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password have to be not empty");
        }
    }
}
