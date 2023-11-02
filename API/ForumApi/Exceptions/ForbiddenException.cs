
using System.Text.Json;

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

        public ForbiddenException(object errorObj) : base(JsonSerializer.Serialize(errorObj)) {}

        public ForbiddenException(object errorObj, Exception inner) : base(JsonSerializer.Serialize(errorObj), inner) {}
    }
}