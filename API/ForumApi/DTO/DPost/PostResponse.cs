using ForumApi.DTO.Auth;

namespace ForumApi.DTO.DPost
{
    public class PostResponse
    {
        public int Id {get;set;}
        public int TopicId {get;set;}
        public string Content {get;set;} = null!;
        public DateTime CreatedAt {get;set;}

        public User Author {get;set;} = null!;
    }
}