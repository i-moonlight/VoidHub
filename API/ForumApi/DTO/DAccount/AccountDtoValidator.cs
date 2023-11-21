using FluentValidation;

namespace ForumApi.DTO.DAccount
{
    public class AccountDtoValidator : AbstractValidator<AccountDto>
    {
        /// <summary>
        /// for user to change their own account
        /// </summary>
        public AccountDtoValidator() 
        {
            // restrict user from changing their own role
            RuleFor(x => x.Role)
                .Must(x => x == Data.Models.RoleEnum.None)
                .WithMessage("Role cannot be changed");

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

            RuleFor(x => x.OldPassword)
                .NotEmpty()
                .WithMessage("Old password must not be empty")
                .When(x => !string.IsNullOrEmpty(x.NewPassword));

            RuleFor(x => x.NewPassword).NotEmpty()
                .WithMessage("New password must not be empty")
                .When(x => !string.IsNullOrEmpty(x.OldPassword));

            RuleFor(r => r.NewPassword)
                .NotEmpty()
                .WithMessage("Password must not be empty")
                .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters long")
                .MaximumLength(32)
                .WithMessage("Password must be at most 32 characters long")
                .Matches(@"^[a-zA-Z\d!@#$%^&*()\-_=+{}:,<.>]+$")
                .WithMessage("Password can only contain letters, numbers and special characters")
                .When(x => !string.IsNullOrEmpty(x.NewPassword));
        }        
    }
}