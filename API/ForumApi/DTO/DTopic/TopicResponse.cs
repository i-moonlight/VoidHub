using ForumApi.DTO.DPost;

namespace ForumApi.DTO.DTopic
{
    public class TopicResponse
    {
        public int Id {get;set;}
        public int ForumId {get;set;}
        public string Title {get;set;} = null!;
        public bool IsClosed {get;set;}
        public bool IsPinned {get;set;}
        public DateTime CreatedAt {get;set;}

        public int PostsCount {get;set;}        
        public int CommentsCount {get;set;}

        public PostResponse Post {get;set;} = null!;
    }
}