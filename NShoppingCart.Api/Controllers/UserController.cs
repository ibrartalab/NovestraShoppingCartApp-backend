using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NShoppingCart.Application.DTOs;
using NShoppingCart.Application.Services.Interfaces;
using NShoppingCart.Core.Entities;
namespace NShoppingCart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        // get all users from this endpoint
        [Authorize]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var result = await userService.GetUsers();

            return Ok(result);
        }

        // get a single user by id form this endpoint
        //[HttpGet("{userId}")]
        //public async Task<ActionResult<UserDto>> Get([FromRoute] Guid userId)
        //{
        //    var result = await userService.GetUserById(userId);

        //    return Ok(result);
        //}

        // get a single user by id form this endpoint
        [HttpGet("{userName}")]
        public async Task<ActionResult<UserDto>> GetByUsername([FromRoute] string userName)
        {
            var result = await userService.GetUserByUsername(userName);

            return Ok(result);
        }

        // update an existing user by providing id and updatedInfo from this endpoint
        [Authorize]
        [HttpPut("{userId}")]
        public async Task<ActionResult<UserDto>> Update([FromRoute] Guid userId, [FromBody] UserEntity user)
        {
            var result = await userService.UpdateUser(userId, user);
            return Ok(result);
        }

        // delete a user from the database permanently from this endpoint
        [Authorize]
        [HttpDelete("{userId}")]
        public async Task<ActionResult<bool>> Delete([FromRoute] Guid userId)
        {
            var result = await userService.DeleteUser(userId);
            return Ok(result);
        }
    }

}