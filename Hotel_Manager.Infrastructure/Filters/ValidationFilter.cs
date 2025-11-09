using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hotel_Manager.Infrastructure.Filters
{
    public class ValidationFilter :IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .SelectMany(kvp => kvp.Value!.Errors.Select(e => new
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Title = "Validation Error",
                        Detail = e.ErrorMessage
                    }))
                    .ToList();

                var json = new { errors };
                context.Result = new BadRequestObjectResult(json);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // no implementado
        }
    }
}
