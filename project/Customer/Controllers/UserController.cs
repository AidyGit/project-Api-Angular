using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project.Customer.Dtos;
using project.Customer.Interfaces;

namespace project.Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController:ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger <UserController> _logger;

        public UserController(IUserService userService,ILogger<UserController> logger) 
        {
            _logger = logger;
            _userService = userService;
        }

        //register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto.RegisterDto>> Register([FromBody] UserDto.RegisterDto register)
        {
            try
            {
                var user = await _userService.CreateUser(register);
                return CreatedAtAction(nameof(Register), new { userName = user.UserName }, user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //login
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto.LoginDto>> Login([FromBody] UserDto.LoginDto login)
        {
            try
            {
                var user = await _userService.LoginUser(login);
                return CreatedAtAction(nameof(Login), new { userName = user.User.UserName }, user);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //get all users
        [Authorize(Roles = "Admin")]
        [HttpGet("AllUsers")]
        public async Task<ActionResult<IEnumerable<UserDto.GetUserDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }
    }
}
