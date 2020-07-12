using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace CarPark.Abstractions
{
    public abstract class MiddlewareBase
    {
        protected readonly RequestDelegate _next;

        protected MiddlewareBase(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public abstract Task Invoke(HttpContext ctx);
    }
}