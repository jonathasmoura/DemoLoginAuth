using DemoLoginAuth.Application.Contracts;
using DemoLoginAuth.Infraestructure.DataContexts;
using DemoLoginAuth.Infraestructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLoginAuth.Infraestructure.DependencyInjection
{
	public static class ServiceContainer
	{
		public static IServiceCollection AddDIServices(this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddDbContext<DemoContext>(options =>

			options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
				x => x.MigrationsAssembly(typeof(ServiceContainer).Assembly.FullName)),
				ServiceLifetime.Scoped);

			services.AddAuthentication(opt =>
			{
				opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(opt =>
			{
				opt.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateIssuerSigningKey = true,
					ValidateLifetime = true,
					ValidIssuer = configuration["Jwt:Issuer"],
					ValidAudience = configuration["Jwt:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey
					(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
				};
			});

			services.AddScoped<IUserService, UserRepository>();



			return services;
		}

	}
}

