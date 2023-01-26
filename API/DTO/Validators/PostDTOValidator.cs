using FluentValidation;

namespace API.DTO.Validators
{
    public class PostDTOValidator : AbstractValidator<PostDTO>
    {
        public PostDTOValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title have to be not empty");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content have to be not empty");
        }
    }
}
