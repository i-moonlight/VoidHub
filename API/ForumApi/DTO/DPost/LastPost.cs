using ForumApi.DTO.Auth;

namespace ForumApi.DTO.DPost
{
    public class LastPost
    {
        public int Id {get;set;}        
        public DateTime CreatedAt {get;set;}
        public User Author {get;set;} = null!;
    }
}