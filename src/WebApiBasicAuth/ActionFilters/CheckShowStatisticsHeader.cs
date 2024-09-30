using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace WebApiBasicAuth.ActionFilters;

// This is an example for setting a custome filter for a controller's method
// It is executed when is set as an attribute. Look as example the
// StatisticsController.

public class CheckShowStatisticsHeader : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // if the ShowStatistics header is missing or set to false, 
        // a BadRequest must be returned.
        if (!context.HttpContext.Request.Headers.ContainsKey("ShowStatistics"))
        {
            context.Result = new BadRequestResult();
        }

        // Get the ShowStatistics header value, if not set as boolean, return bad request 
        if (!bool.TryParse(
                context.HttpContext.Request.Headers["ShowStatistics"].ToString(),
                out bool showStatisticsValue))
        {
            context.Result = new BadRequestResult();
        }

        // check the value, if not TRUE, return bad request 
        if (!showStatisticsValue)
        {
            context.Result = new BadRequestResult();
        }

    }
}
