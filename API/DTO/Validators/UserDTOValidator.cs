﻿using FluentValidation;

namespace API.DTO.Validators
{
    public class UserDTOValidator : AbstractValidator<UserDTO>
    {
        public UserDTOValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName have to be not empty");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName have to be not empty");
            RuleFor(x => x.Login).NotEmpty().WithMessage("Login have to be not empty");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password have to be not empty");
        }
    }
}
