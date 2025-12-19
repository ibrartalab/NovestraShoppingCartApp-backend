using Moq;
using NShoppingCart.Api.Controllers;
using NShoppingCart.Application.DTOs;
using NShoppingCart.Application.Services.Interfaces;
using NShoppingCart.Core.Entities;

namespace NovestraTodo.Tests.Controllers
{
    public class UserControllerUnitTests
    {
        private readonly UserController _userController;
        private readonly Mock<IUserService> _mockUserService;

        public UserControllerUnitTests()
        {
            _mockUserService = new Mock<IUserService>();
            _userController = new UserController(_mockUserService.Object);

        }

        [Fact]
        public async Task GetAll_ShouldReturnOkResult()
        {
            // Arrange
            var users = new List<UserDto>
            {
                new UserDto
                {
                    Id = Random.Shared.Next(),
                    FirstName = "Test1",
                    LastName = "User1",
                    
                    Email = "test@gmail.com",
                },
                new UserDto
                {
                    Id = Random.Shared.Next(),
                    FirstName="Test2",
                    LastName="User2",
                    
                    Email="testsuer2@gmail.com"
                }
            };

            _mockUserService.Setup(service => service.GetUsers())
                .ReturnsAsync(users);
            // Act
            var result = await _userController.GetAll();
            // Assert
            var okResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<UserDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
        }
        [Fact]
        public async Task GetByUsername_ShouldReturnOkResult()
        {
            // Arrange
            var userId = Random.Shared.Next();
            var user = new UserDto
            {
                Id = userId,
                FirstName = "Test1",
                LastName = "User1",
                Email = "test@gmail.com"

            };
            _mockUserService.Setup(service => service.GetUserById(userId))
                .ReturnsAsync(new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                });
            var result = await _userController.GetById(userId);
            //Assert
            var okResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<UserDto>(okResult.Value);
            Assert.Equal(userId, returnValue.Id);
        }
        [Fact]
        public async Task Update_ShouldReturnOkResult()
        {
            //Arrange
            var userId = Random.Shared.Next();
            var existingUser = new User
            {
                Id = userId,
                FirstName = "test",
                LastName = "user",
                Email = "testuser@gmail.com",
            };
            var updatedUser = new User
            {
                Id = existingUser.Id,
                FirstName = "test1",
                LastName = "user1",
                Email = "testuser11@gmail.com",
            };
            var updatedUserDto = new UserDto
            {
                Id = updatedUser.Id,
                FirstName = updatedUser.FirstName,
                LastName = updatedUser.LastName,
                Email = updatedUser.Email,
            };
            _mockUserService.Setup(service => service.UpdateUser(userId, updatedUser)).ReturnsAsync(updatedUserDto);
            //Act
            var result = await _userController.Update(userId, updatedUser);
            //Assert
            var okResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<UserDto>(okResult.Value);
            Assert.Equal(userId, returnValue.Id);
        }
        [Fact]
        public async Task Delete_ShouldReturnTrue()
        {
            //Arrange
            var userId = Random.Shared.Next();
            var deletedUser = new User
            {
                Id = userId,
                FirstName = "test1",
                LastName = "user1",
                Email = "testuser11@gmail.com",
            };
            _mockUserService.Setup(service => service.DeleteUser(userId)).ReturnsAsync(true);
            //Act
            var result = await _userController.Delete(userId);
            //Assert
            var okResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<bool>(okResult.Value);

            Assert.True(returnValue);
        }
    }
}