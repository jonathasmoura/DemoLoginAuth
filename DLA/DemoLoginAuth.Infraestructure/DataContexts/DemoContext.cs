using DemoLoginAuth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLoginAuth.Infraestructure.DataContexts
{
	public class DemoContext : DbContext
	{
		public DemoContext(DbContextOptions<DemoContext> options)
			: base(options)
		{

		}

		public DbSet<ApplicationUser> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

		}
	}
}
