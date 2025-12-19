using System;
using Moq;
using NShoppingCart.Api.Controllers;
using NShoppingCart.Application.Services.Interfaces;

namespace NShoppingCart.Test.Controllers;

public class AuthControllerUnitTests
{
    private readonly Mock<IAuthService> _authServiceMock;
    private readonly AuthController _authController;

    public AuthControllerUnitTests()
    {
        _authServiceMock = new Mock<IAuthService>();
        _authController = new AuthController(_authServiceMock.Object);
    }

    [Fact]
    public async Task Register_ShouldReturnOkResult()
    {
        // Arrange
        var RegisterRequestDto = new Application.DTOs.RegisterRequestDto
        {
            FirstName = "Test",
            LastName = "User",
            Email = "test@gmail.com",
            Password = "Password123!",
        };
        var authResponseDto = new Application.DTOs.AuthResponseDto
        {
            Token = "sampleToeken",
            User = new Application.DTOs.UserDto
            {
                FirstName = "Test",
                LastName = "User",
                Email = "test@gmail.com",
            }
        };

        _authServiceMock.Setup(service => service.RegisterUser(RegisterRequestDto))
            .ReturnsAsync(authResponseDto);
        // Act
        var result = await _authController.Register(RegisterRequestDto);
        // Assert
        var okResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<Application.DTOs.AuthResponseDto>(okResult.Value);
        Assert.Equal(authResponseDto.Token, returnValue.Token);
        Assert.Equal(authResponseDto.User.Email, returnValue.User.Email);
    }
    [Fact]
    public async Task Login_ShouldReturnOkResult()
    {
        // Arrange
        var loginRequestDto = new Application.DTOs.LoginRequestDto
        {   
            Email = "test@gmail.com",
            Password = "Password123!",
        };
        var authResponseDto = new Application.DTOs.AuthResponseDto
        {
            Token = "sampleToeken",
            User = new Application.DTOs.UserDto
            {
                FirstName = "Test",
                LastName = "User",
                Email = "test@gmail.com",
            }
        };
        _authServiceMock.Setup(service => service.LoginUser(loginRequestDto))
            .ReturnsAsync(authResponseDto);
        // Act
        var result = await _authController.Login(loginRequestDto);
        // Assert
        var okResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<Application.DTOs.AuthResponseDto>(okResult.Value);
        Assert.Equal(authResponseDto.Token, returnValue.Token);
        Assert.Equal(authResponseDto.User.Email, returnValue.User.Email);
    }
}
