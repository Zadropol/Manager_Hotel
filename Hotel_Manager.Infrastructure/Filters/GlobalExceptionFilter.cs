
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core;
using Hotel_Manager.Core.Exceptions;

namespace Hotel_Manager.Infrastructure.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is BusinessException businessException)
            {
                var validation = new
                {
                    Status = 400,
                    Title = "Bad Request",
                    Detail = businessException.Message
                };

                var json = new
                {
                    errors = new[] { validation }
                };

                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.ExceptionHandled = true;
            }
            else
            {
                // Si no es una BusinessException → error 500 estándar
                var validation = new
                {
                    Status = 500,
                    Title = "Internal Server Error",
                    Detail = context.Exception.Message
                };

                var json = new
                {
                    errors = new[] { validation }
                };

                context.Result = new ObjectResult(json)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
