using AutoMapper;
using Microsoft.AspNetCore.Http.Features;
using System.Reflection;
using WebApiBasicAuth.Domain.Models;

namespace WebApiBasicAuth.MapperProfiles;

public class StatisticsProfile : Profile
{
    public StatisticsProfile()
    {
        CreateMap<IHttpConnectionFeature, StatisticsDto>();
    }
}
