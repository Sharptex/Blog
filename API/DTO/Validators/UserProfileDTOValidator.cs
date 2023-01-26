using FluentValidation;

namespace API.DTO.Validators
{
    public class UserProfileDTOValidator : AbstractValidator<UserProfileDTO>
    {
        public UserProfileDTOValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName have to be not empty");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName have to be not empty");
            RuleFor(x => x.Login).NotEmpty().WithMessage("Login have to be not empty");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password have to be not empty");
        }
    }
}
