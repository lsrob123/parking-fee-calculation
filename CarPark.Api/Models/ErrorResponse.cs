using System;
using System.Net;

namespace CarPark.Api.Models
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {

        }

        public ErrorResponse(Guid errorId, string message, HttpStatusCode httpStatusCode)
        {
            TraceId = errorId.ToString();
            Title = message;
            Status = (int)httpStatusCode;
        }

        public int Status { get; set; }
        public string Title { get; set; }
        public string TraceId { get; set; }
    }
}