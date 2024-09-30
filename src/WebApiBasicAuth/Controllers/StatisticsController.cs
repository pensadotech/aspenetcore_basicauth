using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApiBasicAuth.ActionFilters;
using WebApiBasicAuth.Domain.Models;

namespace WebApiBasicAuth.Controllers;

// This controller was created to ilustrate unit testing using a
// http context with a defined HttpConnectionFeature.
// The controller uses AutoMapper and the
// WebApiBasicAuth.MapperProfiles.StatisticsProfile.  

[Route("api/v{version:apiVersion}/Statistics")]
[ApiController]
[ApiVersion("1.0")]
public class StatisticsController : ControllerBase
{
    private readonly IMapper _mapper;
    public StatisticsController(IMapper mapper)
    {
        _mapper = mapper;
    }

    // It is mandatory that a "ShowStatistics: true" is in the header, or i will not be accessible
    // The filter is defined under WebApiBasicAuth.ActionFilters
    [HttpGet]
    [CheckShowStatisticsHeader]  
    [SwaggerOperation(OperationId = "GetStatistics", Summary = "Get server statistics")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<StatisticsDto> GetStatistics()
    {
        // This will return the Http Context feature that 
        // will include the calling remote and local addresses and ports 
        // using the class WebApiBasicAuth.Domain.Models.StatisticsDto

        var httpConnectionFeature = HttpContext.Features
            .Get<IHttpConnectionFeature>();

        return Ok(_mapper.Map<StatisticsDto>(httpConnectionFeature));
    }


}
