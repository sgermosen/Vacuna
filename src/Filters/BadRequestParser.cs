using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Collections.Generic;

namespace VacunaAPI.Filters
{
    public class BadRequestParser : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var castResult = context.Result as IStatusCodeActionResult;
            if (castResult == null)
                return;

            var statusCode = castResult.StatusCode;
            if (statusCode == 400)
            {
                var response = new List<string>();
                var actualResult = context.Result as BadRequestObjectResult;
                if (actualResult.Value is string)
                    response.Add(actualResult.Value.ToString());
                else if (actualResult.Value is IEnumerable<IdentityError> errors)
                {
                    foreach (var error in errors)
                    {
                        response.Add(error.Description);
                    }
                }
                else
                {
                    foreach (var key in context.ModelState.Keys)
                    {
                        foreach (var error in context.ModelState[key].Errors)
                        {
                            response.Add($"{key}: {error.ErrorMessage}");
                        }
                    }
                }

                context.Result = new BadRequestObjectResult(response);
            }


        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
