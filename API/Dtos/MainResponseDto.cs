

using System.Net;

namespace API.Dtos
{
    public class MainResponseDto
    {
        public MainResponseDto(object data,HttpStatusCode statusCode , string message)
        {
            this.Data = data;
            this.Message = message;
            this.HttpStatusCode = statusCode;
        }

        public object Data { get; set; }
        public string Message { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }

}
