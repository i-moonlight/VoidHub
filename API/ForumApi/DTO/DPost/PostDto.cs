namespace ForumApi.DTO.DPost
{
    public class PostDto
    {
        public int TopicId {get;set;}
        public string? Content {get;set;}   
        public int? AncestorId {get;set;} = null;
    }
}