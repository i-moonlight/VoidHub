using ForumApi.DTO.Auth;
using ForumApi.DTO.DPost;

namespace ForumApi.DTO.DTopic
{
    public class TopicListElement
    {
        public int Id {get;set;}
        public string Title {get;set;} = null!;
        public DateTime CreatedAt {get;set;}
        public string? Content {get;set;} = null!;

        public int msgsCount {get;set;}

        public User Author {get;set;} = null!;
        public LastPost LastPost {get;set;} = null!;
    }
}