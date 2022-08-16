using System.Net;
using Microsoft.AspNetCore.Http;


namespace Application.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }

        public T Value { get; set; }

        public string Message { get; set; }

        public HttpStatusCode Status { get; set; }

        public static Result<T> Success(T value, HttpStatusCode statusCode = HttpStatusCode.OK, string message = "Success") => new Result<T>() { IsSuccess = true, Value = value, Message = message, Status = HttpStatusCode.OK };

        public static Result<T> Failure(string error = "Failure", HttpStatusCode status = HttpStatusCode.NotAcceptable) =>
            new Result<T> { IsSuccess = false, Message = error, Status = status };
    }

    public class Result
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public HttpStatusCode Status { get; set; }

        public static Result Success(string message = "Success") => new Result() { IsSuccess = true, Message = message, Status = HttpStatusCode.OK };

        public static Result Failure(string error = "Failure", HttpStatusCode status = HttpStatusCode.NotAcceptable)
        {
            var result = new Result
            {
                IsSuccess = false,
                Message = error,
                Status = status
            };
            return result;
        }
    }
}
