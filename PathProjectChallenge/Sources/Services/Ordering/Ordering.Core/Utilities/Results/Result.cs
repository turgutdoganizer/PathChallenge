using Ordering.Core.Utilities.Results.Abstract;
using System.Net;

namespace Ordering.Core.Utilities.Results
{
    public class Result : IResult
    {
        public Result(HttpStatusCode statusCode)
        {
            StatusCode = (int)statusCode;
        }

        public Result(HttpStatusCode statusCode, string message) : this(statusCode)
        {
            Message = message;
        }

        public int StatusCode { get; }
        public string Message { get; set; }
    }
}
