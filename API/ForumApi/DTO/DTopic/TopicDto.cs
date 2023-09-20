namespace ForumApi.DTO.DTopic
{
    public class TopicDto
    {
        public int ForumId {get;set;}
        public string Title {get;set;} = null!;
        public string Content {get;set;} = null!;        
    }
}