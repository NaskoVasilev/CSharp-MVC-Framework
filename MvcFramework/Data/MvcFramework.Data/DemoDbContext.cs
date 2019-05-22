using Microsoft.EntityFrameworkCore;
using MvcFramework.Models;

namespace MvcFramework.Data
{
	public class DemoDbContext : DbContext
	{
		public DemoDbContext()
		{
		}

		public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
		{
		}

		public DbSet<User> Users { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(Configuration.ConnectionString);
			}

			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>(user =>
			{
				user.HasIndex(x => x.Username).IsUnique();
			});

			base.OnModelCreating(modelBuilder);
		}
	}
}
