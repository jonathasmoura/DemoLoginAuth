using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLoginAuth.Application.DTOs
{
	public class LoginResponseDto
	{
		public LoginResponseDto(bool flag, string message)
		{
			Flag = flag;
			Message = message;
		}

		public LoginResponseDto(bool flag, string message, string token)
		{
			Flag = flag;
			Message = message;
			Token = token;
		}

		public bool Flag { get; set; }
		public string Message { get; set; } = null!;
		public string Token { get; set; } = null!;
	}
}
