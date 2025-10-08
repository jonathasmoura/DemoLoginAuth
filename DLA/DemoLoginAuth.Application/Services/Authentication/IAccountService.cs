using DemoLoginAuth.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLoginAuth.Application.Services.Authentication
{
	public interface IAccountService
	{
		Task<RegisterResponseDto> RegisterUserAccountAsync(RegisterUserDto registerUserDto);
		Task<LoginResponseDto> LoginUserAccountAsync(LoginUserDto loginUserDto);
	}
}
