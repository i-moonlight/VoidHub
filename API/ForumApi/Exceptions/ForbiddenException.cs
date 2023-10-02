
namespace ForumApi.Exceptions
{
    public class ForbiddenException : BaseException
    {
        public ForbiddenException(string message) : base(message)
        {
        }

        public ForbiddenException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}