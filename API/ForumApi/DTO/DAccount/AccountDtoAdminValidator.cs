using FluentValidation;
using ForumApi.Data.Models;

namespace ForumApi.DTO.DAccount
{
    public class AccountDtoAdminValidator : AbstractValidator<AccountDto>
    {
        /// <summary>
        /// for admin to change user's role
        /// </summary>
        public AccountDtoAdminValidator() 
        {
            RuleFor(x => x.Role)
                .Must(x => x != RoleEnum.None)
                .WithMessage("Role must be specified")
                .IsInEnum();

            // admin change only user's role
            RuleFor(r => r.Username).Null();
            RuleFor(r => r.Email).Null();
            RuleFor(r => r.OldPassword).Null();
            RuleFor(r => r.NewPassword).Null();
            RuleFor(r => r.Img).Null();
        }
        
    }
}