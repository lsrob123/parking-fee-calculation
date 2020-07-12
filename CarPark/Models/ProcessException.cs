using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace CarPark.Models
{
    public class ProcessException : Exception
    {
        public ProcessException()
        {
        }

        public ProcessException(string message) : base(message)
        {
        }

        public ProcessException(string message,
            HttpStatusCode httpStatusCode,
            params Error[] errors)
            : this(message)
        {
            HttpStatusCode = httpStatusCode;
            Errors.AddRange(errors);
        }

        public Guid ErrorId { get; } = Guid.NewGuid();
        public List<Error> Errors { get; } = new List<Error>();
        public HttpStatusCode HttpStatusCode { get; } = HttpStatusCode.InternalServerError;

        public string InternalErrorMessages =>
            new StringBuilder()
            .AppendJoin(". ", Errors.Select(x => x.Message))
            .ToString();
    }
}