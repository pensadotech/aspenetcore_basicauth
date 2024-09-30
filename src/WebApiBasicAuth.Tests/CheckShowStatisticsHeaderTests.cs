using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApiBasicAuth.ActionFilters;

namespace WebApiBasicAuth.Tests;

// This unit test exercise an action filter defined in the 
// StatisticsController, under the GetStatistics method.
// The action filer is defined in the request header, as "ShowStatistics: true"
// If the header does not contain that attribut the controller's method
// will not be accessible.

public class CheckShowStatisticsHeaderTests
{
    [Fact]
    public void OnActionExecuting_InvokeWithoutShowStatisticsHeader_ReturnsBadRequest()
    {
        // Arrange 

        // Create instance of the filter
        var checkShowStatisticsHeaderActionFilter =new CheckShowStatisticsHeader();

        // Prepare http context
        var httpContext = new DefaultHttpContext();

        var actionContext = new ActionContext(httpContext, new(), new(), new());
        var actionExecutingContext = new ActionExecutingContext(actionContext,
            new List<IFilterMetadata>(),
            new Dictionary<string, object?>(),
            controller: null);

        // Act
        checkShowStatisticsHeaderActionFilter.OnActionExecuting(actionExecutingContext); // Test filter directly

        // Assert
        Assert.IsType<BadRequestResult>(actionExecutingContext.Result);  // It should retune a bad request
    }

    [Fact]
    public void OnActionExecuting_InvokeWithoutShowStatisticsHeader_ReturnsSucessFull()
    {
        // Arrange 

        // Create instance of the filter
        var checkShowStatisticsHeaderActionFilter = new CheckShowStatisticsHeader();

        // Prepare http context
        var httpContext = new DefaultHttpContext();

        // Add header to HttpContext
        httpContext.Request.Headers["ShowStatistics"] = "true";

        var actionContext = new ActionContext(httpContext, new(), new(), new());
        var actionExecutingContext = new ActionExecutingContext(actionContext,
            new List<IFilterMetadata>(),
            new Dictionary<string, object?>(),
            controller: null);

        // Act
        checkShowStatisticsHeaderActionFilter.OnActionExecuting(actionExecutingContext); // Test filter directly

        // Assert
        Assert.Null(actionExecutingContext.Result);  // It should proceed without failure
        
    }

}
