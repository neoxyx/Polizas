using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProyectoPolizas.Controllers;
using Xunit;

public class AuthControllerTests
{
    [Fact]
    public void Login_ValidUser_ReturnsToken()
    {
        // Arrange
        var appSettings = Options.Create(new AppSettings
        {
            Secret = "TuClaveSecretaSuperSegura"
        });
        var authController = new AuthController(appSettings);

        // Act
        var credentials = new UserCredentials
        {
            Username = "usuarioDemo",
            Password = "passwordDemo"
        };
        var result = authController.Login(credentials);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult.Value);
        var token = okResult.Value.ToString();
        Assert.Contains("token", token);
    }

    [Fact]
    public void Login_InvalidUser_ReturnsUnauthorized()
    {
        // Arrange
        var appSettings = Options.Create(new AppSettings
        {
            Secret = "TuClaveSecretaSuperSegura"
        });
        var authController = new AuthController(appSettings);

        // Act
        var credentials = new UserCredentials
        {
            Username = "usuarioDemo",
            Password = "contraseñaIncorrecta"
        };
        var result = authController.Login(credentials);

        // Assert
        Assert.IsType<UnauthorizedResult>(result);
    }

    [Fact]
    public void Login_NullCredentials_ReturnsUnauthorized()
    {
        // Arrange
        var appSettings = Options.Create(new AppSettings
        {
            Secret = ""
        });
        var authController = new AuthController(appSettings);

        // Act
        var result = authController.Login(null);

        // Assert
        Assert.IsType<UnauthorizedResult>(result);
    }
}
