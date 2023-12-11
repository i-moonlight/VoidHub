using FluentValidation;

namespace ForumApi.DTO.DAccount
{
    /// <summary>
    /// for admin to change user's username
    /// </summary>
    public class AccountDtoAdminUsernameValidator : AbstractValidator<AccountDto> 
    {
        public AccountDtoAdminUsernameValidator() 
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username must be specified")
                .MaximumLength(32)
                .WithMessage("Username must be less than 32 characters");

            // admin change only user's usrname
            RuleFor(x => x.Role)
                .Must(x => x == Data.Models.RoleEnum.None)
                .WithMessage("Role cannot be changed");
            RuleFor(r => r.Email).Null();
            RuleFor(r => r.OldPassword).Null();
            RuleFor(r => r.NewPassword).Null();
            RuleFor(r => r.Img).Null();
        }
    }
}