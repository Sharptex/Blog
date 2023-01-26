using API.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.DTO.Validators
{
    public class TagDTOValidator : AbstractValidator<TagDTO>
    {
        public TagDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name have to be not empty");
        }
    }
}
