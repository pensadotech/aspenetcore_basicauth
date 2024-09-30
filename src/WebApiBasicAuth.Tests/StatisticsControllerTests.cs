using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiBasicAuth.Controllers;
using WebApiBasicAuth.Domain.Models;

namespace WebApiBasicAuth.Tests;

// The exercise against the StatisticsController is to ilustrate
// the retreival of  HttpConnectionFeature.
// The controller uses AutoMapper and the
// WebApiBasicAuth.MapperProfiles.StatisticsProfile  


public class StatisticsControllerTests
{
    [Fact]
    public void GetStatistics_InputFromHttpConnectionFeature_MustReturnInputtedIps()
    {
        // Arrange

        // Create a pretend local and remote addresses and ports
        var localIpAddress = System.Net.IPAddress.Parse("111.111.111.111");
        var localPort = 5000;
        var remoteIpAddress = System.Net.IPAddress.Parse("222.222.222.222");
        var remotePort = 8080;

        // Ceate a moq Http Feature collection
        var featureCollectionMock = new Mock<IFeatureCollection>();
        featureCollectionMock.Setup(e => e.Get<IHttpConnectionFeature>())
         .Returns(new HttpConnectionFeature
         {
             LocalIpAddress = localIpAddress,
             LocalPort = localPort,
             RemoteIpAddress = remoteIpAddress,
             RemotePort = remotePort
         });

        // Set Http Context with the Http Feature collection
        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(e => e.Features)
            .Returns(featureCollectionMock.Object);

        // Explain Auto-mapper how to convert the StatisticsProfile
        var mapperConfiguration = new MapperConfiguration(
            cfg => cfg.AddProfile<MapperProfiles.StatisticsProfile>());
        var mapper = new Mapper(mapperConfiguration);

        // Instantiate controller
        var statisticsController = new StatisticsController(mapper);

        // Instantiate a new controller conttex, as the statisticsController.HttpCOntexs is ReadOnly mode
        // and cannot be set for the puropose of the test
        statisticsController.ControllerContext = new ControllerContext()
        {
            HttpContext = httpContextMock.Object
        };

        // Act
        var result = statisticsController.GetStatistics();

        // Assert
        var actionResult = Assert
          .IsType<ActionResult<StatisticsDto>>(result);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var statisticsDto = Assert.IsType<StatisticsDto>(
            okObjectResult.Value);

        Assert.Equal(localIpAddress.ToString(), statisticsDto.LocalIpAddress);
        Assert.Equal(localPort, statisticsDto.LocalPort);
        Assert.Equal(remoteIpAddress.ToString(), statisticsDto.RemoteIpAddress);
        Assert.Equal(remotePort, statisticsDto.RemotePort);

    }

}
