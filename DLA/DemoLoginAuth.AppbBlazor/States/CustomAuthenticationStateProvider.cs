using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DemoLoginAuth.AppbBlazor.States
{
	public class CustomAuthenticationStateProvider : AuthenticationStateProvider
	{
		private const string LocalStorageKey = "auth";
		private readonly ClaimsPrincipal _anon = new(new ClaimsIdentity());
		private readonly ILocalStorageService _localStorageService;

		public CustomAuthenticationStateProvider(ILocalStorageService localStorageService)
		{
			_localStorageService = localStorageService;
		}
		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			string token = await _localStorageService.GetItemAsStringAsync(LocalStorageKey)!;
			if (string.IsNullOrEmpty(token))
				return await Task.FromResult(new AuthenticationState(_anon));

			var (name, email) = GetClaims(token);
			if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
				return await Task.FromResult(new AuthenticationState(_anon));

			var claims = SetClaimPrincipal(name, email);
			if (claims is null)
				return await Task.FromResult(new AuthenticationState(_anon));
			else
				return await Task.FromResult(new AuthenticationState(claims));


		}

		public static ClaimsPrincipal SetClaimPrincipal(string name, string email)
		{
			if (name is null || email is null)
				return new ClaimsPrincipal();

			return new ClaimsPrincipal(new ClaimsIdentity(
				[
					new(ClaimTypes.Name, name!),
					new(ClaimTypes.Email, email!)
				], "JwtAuth"));
		}


		private static (string, string) GetClaims(string jwtToken)
		{
			if (string.IsNullOrEmpty(jwtToken))
				return (null!, null!);

			var handler = new JwtSecurityTokenHandler();
			var token = handler.ReadJwtToken(jwtToken);

			var name = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)!.Value;
			var email = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)!.Value;

			return (name, email);
		}

		public async Task UpdateAuthenticationState(string jwtToken)
		{
			var claims = new ClaimsPrincipal();

			if (!string.IsNullOrEmpty(jwtToken))
			{
				var (name, email) = GetClaims(jwtToken);
				if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
					return;


				var setClaims = SetClaimPrincipal(name, email);
				if (setClaims is null)
					return;

				await _localStorageService.SetItemAsStringAsync(LocalStorageKey, jwtToken);
			}
			else
			{
				await _localStorageService.RemoveItemAsync(LocalStorageKey);
			}
			NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claims)));

		}
	}
}
