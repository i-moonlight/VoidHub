
using Newtonsoft.Json;

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

        public ForbiddenException(object errorObj) : base(JsonConvert.SerializeObject(errorObj)) {}

        public ForbiddenException(object errorObj, Exception inner) : base(JsonConvert.SerializeObject(errorObj), inner) {}
    }
}