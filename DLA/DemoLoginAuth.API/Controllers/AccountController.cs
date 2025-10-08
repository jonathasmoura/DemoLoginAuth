using DemoLoginAuth.Application.Contracts;
using DemoLoginAuth.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DemoLoginAuth.API.Controllers
{	

		[ApiController]
		[Route("[controller]")]
		public class AccountController : ControllerBase
		{
		private readonly IUserService _userService;

		public AccountController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPost("register")]
		public async Task<ActionResult<RegisterResponseDto>> RegisterUser([FromBody] RegisterUserDto registerUserDto)
		{
			var result = await _userService.RegisterUser(registerUserDto);
			return Ok(result);
		}

		[HttpPost("login")]
		public async Task<ActionResult<LoginResponseDto>> LoginUser([FromBody] LoginUserDto loginUserDto)
		{
			var result = await _userService.LoginUser(loginUserDto);
			return Ok(result);
		}
	}
}
