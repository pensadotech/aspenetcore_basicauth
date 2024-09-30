using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiBasicAuth.Controllers;

namespace WebApiBasicAuth.Tests;

// This unit test exercise a redirection inside a controller based on 
// the user role (AdminController).

public class AdmnControllerTest
{
    [Fact]
    public void GetAdmin_GetActionForUserInAdminRole_MustRedirectToGetCurrentWeatherForecast()
    {
        // Arrange

        // Set controller
        var adminController = new AdminController();

        // Set user claims and principal, to simulate teh role
        var userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Karen"),
                new Claim(ClaimTypes.Role, "Admin")   // set as ADMIN
            };
        var claimsIdentity = new ClaimsIdentity(userClaims, "UnitTest");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        // Set claim to the context (request message)
        var httpContext = new DefaultHttpContext()
        {
            User = claimsPrincipal
        };

        // Associate the context to the controller
        adminController.ControllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };


        // Act
        var result = adminController.GetProtectedWeatherInfo(); // Try to access a method in teh controller
               
        // Assert 
        var actionResult = Assert.IsAssignableFrom<IActionResult>(result);
        var redirectoToActionResult = Assert.IsType<RedirectToActionResult>(result); // It should force a redirection

        Assert.Equal("GetCurrentWeatherForecast", redirectoToActionResult.ActionName);
        Assert.Equal("WeatherForecast", redirectoToActionResult.ControllerName);

    }

    [Fact]
    public void GetAdmin_GetActionForUserInAdminRole_MustRedirectToGetCurrentWeatherForecast_wihMoq()
    {
        // Arrange

        // Set controller
        var adminController = new AdminController();

        // Set principal using Moq
        var mockPrincipal = new Mock<ClaimsPrincipal>();
        mockPrincipal.Setup(x =>
            x.IsInRole(It.Is<string>(s => s == "Admin"))).Returns(true);   // set as ADMIN

        // Set moq claim to a moq context (request message)
        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(c => c.User)
            .Returns(mockPrincipal.Object);

        // Associate the mq context to the controller
        adminController.ControllerContext = new ControllerContext()
        {
            HttpContext = httpContextMock.Object
        };


        // Act
        var result = adminController.GetProtectedWeatherInfo();


        // Asssert
        // Assert 
        var actionResult = Assert.IsAssignableFrom<IActionResult>(result);
        var redirectoToActionResult = Assert.IsType<RedirectToActionResult>(result);

        Assert.Equal("GetCurrentWeatherForecast", redirectoToActionResult.ActionName);
        Assert.Equal("WeatherForecast", redirectoToActionResult.ControllerName);

    }


    [Fact]
    public void GetAdmin_GetActionForUserInAdminRole_MustRedirectToGetStatistics()
    {
        // Arrange

        // Set controller
        var adminController = new AdminController();

        // Set user claims and principal, to simulate teh role
        var userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Karen"),
                new Claim(ClaimTypes.Role, "User")    // set as regular USER
            };
        var claimsIdentity = new ClaimsIdentity(userClaims, "UnitTest");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        // Set claim to the context (request message)
        var httpContext = new DefaultHttpContext()
        {
            User = claimsPrincipal
        };

        // Associate the context to the controller
        adminController.ControllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };


        // Act
        var result = adminController.GetProtectedWeatherInfo(); // Try to access a method in teh controller

        // Assert 
        var actionResult = Assert.IsAssignableFrom<IActionResult>(result);
        var redirectoToActionResult = Assert.IsType<RedirectToActionResult>(result); // It should force a redirection

        Assert.Equal("GetStatistics", redirectoToActionResult.ActionName);
        Assert.Equal("Statistics", redirectoToActionResult.ControllerName);

    }


}
