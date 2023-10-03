namespace ForumApi.DTO.DSection
{
    public class SectionDto
    {
        public string Title {get;set;} = null!;
        public bool IsHidden {get;set;} = false;
        public int OrderPosition {get;set;} = 0;
    }
}