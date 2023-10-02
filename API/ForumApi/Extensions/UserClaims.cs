using System.Security.Claims;

namespace ForumApi.Extensions
{
    public static class UserClaims
    {
        public static int GetId(this ClaimsPrincipal user)
        {
            var sub = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if(string.IsNullOrEmpty(sub?.Value))
                throw new ArgumentNullException("User claims does not contain Id");

            return int.Parse(sub.Value);
        }        
    }
}