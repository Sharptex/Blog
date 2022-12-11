using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.DTO.Validators
{
    public class CommentDTOValidator : AbstractValidator<CommentDTO>
    {
        public CommentDTOValidator()
        {
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content have to be not empty");
        }
    }
}
