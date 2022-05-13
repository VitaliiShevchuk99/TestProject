using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Shared.Dto;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IChatService _chatService;

        public UserController(IUserService userService, IChatService chatService)
        {
            _userService = userService;
            _chatService = chatService;
        }

        // GET: api/<UserController>
        [HttpGet]
        [Authorize]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public IActionResult GetAllItems()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpGet("messages")]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllMessages()
        {
            return Ok(await _chatService.GetAllMessages(User.Identity.Name));
        }

        // POST api/<UserController>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] UserDto user)
        {
            return Ok(await _userService.TryCreateUser(user));
        }

        // POST api/<UserController>
        [HttpPost("Login")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login([FromBody] UserDto userModel)
        {
            return Ok(await _userService.Login(userModel));
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public IActionResult RefreshToken([FromBody] LoginResponceDto tokenDto)
        {
            var response = _userService.RefreshToken(tokenDto);

            if (response == null)
                return Unauthorized(new { message = "Invalid token" });
            
            return Ok(response);
        }
    }
}