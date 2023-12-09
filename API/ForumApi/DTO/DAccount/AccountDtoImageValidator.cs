using FluentValidation;

namespace ForumApi.DTO.DAccount
{
    /// <summary>
    /// Valdiate only image
    /// </summary>
    public class AccountDtoImageValidator : AbstractValidator<AccountDto>
    {
        private readonly string[] _extensions = {".jpg", ".jpeg", ".png", ".gif", ".bmp"};

        public AccountDtoImageValidator()
        {
            RuleFor(r => r.Img)
                .Configure(c => c.CascadeMode = CascadeMode.Stop)
                .Must(i => i != null)
                .WithMessage("Image cannot be empty")
                .Must(i => _extensions.Contains(Path.GetExtension(i.FileName)))
                .WithMessage($"Image must be in one of the following formats: {string.Join(", ", _extensions)}")
                .Must(i => i.Length < 524288)
                .WithMessage("Image too heavy, must be less than 512 KB");
        }        
    }
}