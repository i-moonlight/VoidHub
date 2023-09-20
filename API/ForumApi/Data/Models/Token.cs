namespace ForumApi.Data.Models
{
    public class Token
    {
        public int Id {get;set;} 
        public int AccountId {get;set;} 
        public string RefreshToken {get;set;} = null!;
        public DateTime ExpiresAt {get;set;}

        public Account Account {get;set;} = null!;
    }
}