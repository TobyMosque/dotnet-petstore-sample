using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetStore.Services.Abstractions.Dtos;
using PetStore.Services.Abstractions.Requests.User;
using PetStore.Services.Abstractions.Services;

namespace PetStore.Api.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<UserDto>>> GetAllUsers(
            [FromQuery] int skip = 0,
            [FromQuery] int take = 20,
            CancellationToken ct = default)
            => Ok(await _userService.GetAllUsersAsync(skip, take, ct));

        [HttpGet("{username}")]
        public async Task<ActionResult<UserDto>> GetUserByName(string username, CancellationToken ct)
        {
            var user = await _userService.GetUserByNameAsync(username, ct);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserRequest request, CancellationToken ct)
            => Ok(await _userService.CreateUserAsync(request, ct));

        [HttpPut("{username}")]
        public async Task<ActionResult<UserDto>> UpdateUser(string username, [FromBody] UpdateUserRequest request, CancellationToken ct)
        {
            request.Username = username;
            var user = await _userService.UpdateUserAsync(request, ct);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteUser(string username, CancellationToken ct)
        {
            var result = await _userService.DeleteUserAsync(username, ct);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet("login")]
        public async Task<ActionResult<string>> LoginUser([FromQuery] string username, [FromQuery] string password, CancellationToken ct)
        {
            var token = await _userService.LoginAsync(username, password, ct);
            if (token == null) return Unauthorized();
            return Ok(token);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> LogoutUser(CancellationToken ct)
        {
            await _userService.LogoutAsync(ct);
            return Ok();
        }
    }
}
