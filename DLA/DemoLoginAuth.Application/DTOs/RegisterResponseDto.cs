using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLoginAuth.Application.DTOs
{
	public class RegisterResponseDto
	{
		public RegisterResponseDto(bool flag, string message)
		{
			Flag = flag;
			Message = message;
		}

		public bool Flag { get; set; }
		public string Message { get; set; } = null!;
	}
}
