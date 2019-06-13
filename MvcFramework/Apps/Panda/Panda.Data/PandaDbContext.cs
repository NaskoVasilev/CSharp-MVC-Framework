using Microsoft.EntityFrameworkCore;
using Panda.Data.Models;

namespace Panda.Data
{
	public class PandaDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }

		public DbSet<Package> Packages { get; set; }

		public DbSet<Receipt> Receipts { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if(!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(DatabaseSettings.ConnectionString);
			}

			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().HasMany(u => u.Receipts)
				.WithOne(r => r.Recipient)
				.HasForeignKey(r => r.RecipientId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<User>().HasMany(u => u.Packages)
				.WithOne(p => p.Recipient)
				.HasForeignKey(p => p.RecipientId)
				.OnDelete(DeleteBehavior.Restrict);

			base.OnModelCreating(modelBuilder);
		}
	}
}
