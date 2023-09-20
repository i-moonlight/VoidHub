using FluentValidation;

namespace ForumApi.DTO.Auth
{
    public class RegisterValidator : AbstractValidator<Register>
    {
        public RegisterValidator()
        {
            RuleFor(r => r.Email)
                .NotEmpty()
                .WithMessage("Email must not be empty")
                .EmailAddress()
                .WithMessage("Email must be a valid email address");

            RuleFor(r => r.Username)
                .NotEmpty()
                .WithMessage("Username must not be empty")
                .Length(3, 32)
                .WithMessage("Username must be between 3 and 20 characters");

            RuleFor(r => r.LoginName)
                .NotEmpty()
                .WithMessage("Login name must not be empty")
                .Length(3, 32)
                .WithMessage("Login name must be between 3 and 20 characters");
        }
    }
}