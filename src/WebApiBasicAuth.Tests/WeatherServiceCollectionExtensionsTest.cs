using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApiBasicAuth.ServiceCollectionsExtensions;
using WebApiBasicAuth.Services;
using Xunit;

namespace WebApiBasicAuth.Tests;

// This unit test is to exercise teh weather forecast service collection extension.
// it is better to isolate any custome services inside a service extension
// to simplify testing

public class WeatherServiceCollectionExtensionsTest
{
    [Fact]
    public void RegisterWeatherServices_Execute_AddWeatherServices()
    {
        // Arrrange
        var serviceCollection = new ServiceCollection();

        // Add in memoty configuration
        // This is a pretend setting as it does not exist
        // It is only to demostrate building a service inside a
        // collectin extension and test 
        var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(
                    new Dictionary<string, string> {
                        {"ConnectionStrings:WeatherDB", "AnyValueWillDo"}})
                .Build();

        // Act
        serviceCollection.AddWeatherServices(configuration);
        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Assert
        Assert.NotNull(serviceProvider.GetService<IWeatherForecaster>());
        Assert.IsType<RandomWeatherForecaster>(serviceProvider.GetService<IWeatherForecaster>());

    }

}
