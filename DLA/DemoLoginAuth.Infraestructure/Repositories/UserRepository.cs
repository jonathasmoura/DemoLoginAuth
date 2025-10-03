using DemoLoginAuth.Application.Contracts;
using DemoLoginAuth.Application.DTOs;
using DemoLoginAuth.Domain.Entities;
using DemoLoginAuth.Infraestructure.DataContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DemoLoginAuth.Infraestructure.Repositories
{
	public class UserRepository : IUserService
	{
		private readonly DemoContext _context;
		private readonly IConfiguration _configuration;

		public UserRepository(DemoContext context, IConfiguration configuration)
		{
			_context = context;
			_configuration = configuration;
		}

		private async Task<ApplicationUser> FindUserByEmail(string email) =>
			await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

		public async Task<LoginResponseDto> LoginUser(LoginUserDto loginUserDto)
		{
			var objUser = await FindUserByEmail(loginUserDto.Email!);

			if (objUser == null)
				return new LoginResponseDto(false, "Desculpe, não encontramos o usuário!");

			bool checkPass = BCrypt.Net.BCrypt.Verify(loginUserDto.Password, objUser.Password);
			if (checkPass)
				return new LoginResponseDto(true, "Login efetuado com sucesso!", GenerateToken(objUser));
			else return new LoginResponseDto(false, "Não foi possível efeturar o login");
		}

		private string GenerateToken(ApplicationUser objUser)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
			var userClaims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, objUser.Id.ToString()),
				new Claim(ClaimTypes.Name, objUser.Name!),
				new Claim(ClaimTypes.Email, objUser.Email!
				)
			};

			var token = new JwtSecurityToken(
				issuer: _configuration["Jwt:Issuer"],
				audience: _configuration["Jwt:Audience"],
				claims: userClaims,
				expires: DateTime.Now.AddDays(5),
				signingCredentials: credentials
				);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		public async Task<RegisterResponseDto> RegisterUser(RegisterUserDto registerUserDto)
		{
			var objUser = await FindUserByEmail(registerUserDto.Email!);

			if (objUser != null)
				return new RegisterResponseDto(false, "O usuário já existe em nossa base de dados!");

			_context.Users.Add(new ApplicationUser()
			{
				Name = registerUserDto.UserName,
				Email = registerUserDto.Email,
				Password = BCrypt.Net.BCrypt.HashPassword(registerUserDto.Password)
			});

			await _context.SaveChangesAsync();
			return new RegisterResponseDto(true, "Registro realizado com sucesso!");
		}
	}
}
