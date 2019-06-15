using Microsoft.EntityFrameworkCore;
using Torshia.Data.Models;

namespace Torshia.Data
{
	public class TorshiaDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }

		public DbSet<UserRole> UserRoles { get; set; }

		public DbSet<Role> Roles { get; set; }

		public DbSet<Task> Tasks { get; set; }

		public DbSet<ParticipantTask> ParticipantTasks { get; set; }

		public DbSet<Participant> Participants { get; set; }

		public DbSet<Report> Reports { get; set; }

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
			modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.RoleId, ur.UserId });

			modelBuilder.Entity<ParticipantTask>().HasKey(up => new { up.TaskId, up.ParticipantId });

			base.OnModelCreating(modelBuilder);
		}
	}
}
