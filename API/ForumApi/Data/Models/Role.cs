using System.ComponentModel;

namespace ForumApi.Data.Models
{
    public class Role
    {
        public const string Admin = "Admin";        
        public const string Moder = "Moder";
        public const string User = "User";
    }

    public enum RoleEnum
    {
        None,
        Admin,
        Moder,
        User
    }
}