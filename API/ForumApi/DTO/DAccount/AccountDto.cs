using ForumApi.Data.Models;

namespace ForumApi.DTO.DAccount
{
    public class AccountDto
    {
        public string? Username { get; set; } = null;
        public string? Email { get; set; } = null;
        public RoleEnum Role { get; set; } = RoleEnum.None;
        public string? OldPassword { get; set; } = null;
        public string? NewPassword { get; set; } = null;
        public IFormFile Img { get; set; } = null!;
    }
}