using FluentValidation;

namespace API.DTO.Validators
{
    public class CommentDTOValidator : AbstractValidator<CommentDTO>
    {
        public CommentDTOValidator()
        {
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content have to be not empty");
        }
    }
}
