using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO.Validators
{
    public class RoleDTOValidator : AbstractValidator<RoleDTO>
    {
        public RoleDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name have to be not empty");
        }
    }
}
