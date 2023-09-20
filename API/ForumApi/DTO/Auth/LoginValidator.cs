using FluentValidation;

namespace ForumApi.DTO.Auth
{
    public class LoginValidator : AbstractValidator<Login>
    {

        public LoginValidator()
        {
            RuleFor(l => l.LoginName)
                .NotEmpty()
                .WithMessage("Login name must not be empty");

            RuleFor(l => l.Password)
                .NotEmpty()
                .WithMessage("Password must not be empty");
        }
        
    }
}