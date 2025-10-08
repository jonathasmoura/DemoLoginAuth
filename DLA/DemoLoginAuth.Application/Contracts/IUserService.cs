using DemoLoginAuth.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLoginAuth.Application.Contracts
{
	public interface IUserService
	{
		Task<RegisterResponseDto> RegisterUser(RegisterUserDto registerUserDto);
		Task<LoginResponseDto> LoginUser(LoginUserDto loginUserDto);
	}
}
