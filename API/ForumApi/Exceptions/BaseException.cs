namespace ForumApi.Exceptions
{
    public class BaseException : Exception
    {
        public BaseException(string message) 
            : base(message)
        {
        
        }

        public BaseException(string message, Exception inner)
            : base(message, inner)
        {
        }        
    }
}