using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VacunaAPI.Filters
{
    public class MyAccionFilter : IActionFilter
    {
        private readonly ILogger<MyAccionFilter> logger;

        public MyAccionFilter(ILogger<MyAccionFilter> logger)
        {
            this.logger = logger;
        }
        //este es el primero aunque visualmente suele venir despues
        public void OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogInformation("Before execute an acction");
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            logger.LogInformation("after execute an acction");

        }


    }
}
