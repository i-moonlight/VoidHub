using ForumApi.DTO.Auth;
using ForumApi.DTO.DPost;

namespace ForumApi.DTO.DTopic
{
    public class TopicElement
    {
        public int Id {get;set;}
        public string Title {get;set;} = null!;
        public bool IsClosed {get;set;}
        public bool IsPinned {get;set;}
        public DateTime CreatedAt {get;set;}

        public int PostsCount {get;set;}

        public User Author {get;set;} = null!;
        public LastPost? LastPost {get;set;}
    }
}