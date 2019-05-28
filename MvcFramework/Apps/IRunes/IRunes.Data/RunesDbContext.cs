namespace IRunes.Data
{
	using IRunes.Models;
	using Microsoft.EntityFrameworkCore;

    public class RunesDbContext : DbContext
    {
		public RunesDbContext()
		{
		}

		public RunesDbContext(DbContextOptions<RunesDbContext> contextOptions) : base(contextOptions)
		{
		}

		public DbSet<User> Users { get; set; }

		public DbSet<Album> Albums { get; set; }

		public DbSet<Track> Tracks { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if(!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(Configuration.ConnectionString);
			}

			base.OnConfiguring(optionsBuilder);
		}
	}
}
