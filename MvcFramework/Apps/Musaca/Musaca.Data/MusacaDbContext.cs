using Microsoft.EntityFrameworkCore;
using Musaca.Data.Models;

namespace Musaca.Data
{
	public class MusacaDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }

		public DbSet<Order> Orders { get; set; }

		public DbSet<Product> Products { get; set; }

		public DbSet<OrderProduct> OrderProducts { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(DatabaseSettings.ConnectionString);
			}

			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<OrderProduct>()
				.HasKey(op => new { op.OrderId, op.ProductId });

			base.OnModelCreating(modelBuilder);
		}
	}
}
