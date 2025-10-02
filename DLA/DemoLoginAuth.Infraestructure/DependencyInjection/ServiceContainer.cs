using DemoLoginAuth.Infraestructure.DataContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLoginAuth.Infraestructure.DependencyInjection
{
	public static class ServiceContainer
	{
		public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<DemoContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
			});

			return services;
		}
	}
}
