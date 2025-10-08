using DemoLoginAuth.Application.DTOs;
using DemoLoginAuth.Application.Services.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DemoLoginAuth.Application.Services.Implements
{
	public class AccountService : IAccountService
	{
		private readonly HttpClient _httpClient;

		public AccountService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<LoginResponseDto> LoginUserAccountAsync(LoginUserDto loginUserDto)
		{
			var response = await _httpClient.PostAsJsonAsync("api/Account/login", loginUserDto);
			var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>();

			return result!;

		}

		public async Task<RegisterResponseDto> RegisterUserAccountAsync(RegisterUserDto registerUserDto)
		{
			var response = await _httpClient.PostAsJsonAsync("api/Account/register", registerUserDto);
			var result = await response.Content.ReadFromJsonAsync<RegisterResponseDto>();

			return result!;
		}
	}
}
