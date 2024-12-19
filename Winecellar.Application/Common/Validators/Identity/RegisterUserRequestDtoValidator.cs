using FluentValidation;
using Winecellar.Application.Dtos.Identity;

namespace Winecellar.Application.Common.Validators.Identity
{
   
    public class RegisterUserRequestDtoValidator : AbstractValidator<RegisterUserRequestDto>
    {
        public RegisterUserRequestDtoValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required")
                .MaximumLength(50).WithMessage("Username cannot exceed 50 characters");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");
        }
    }
}
