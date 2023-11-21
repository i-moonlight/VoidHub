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

            RuleFor(r => r.Password)
                .NotEmpty()
                .WithMessage("Password must not be empty")
                .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters long")
                .MaximumLength(32)
                .WithMessage("Password must be at most 32 characters long")
                .Matches(@"^[a-zA-Z\d!@#$%^&*()\-_=+{}:,<.>]+$")
                .WithMessage("Password can only contain letters, numbers and special characters");
        }
    }
}