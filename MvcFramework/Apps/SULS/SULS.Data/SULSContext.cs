using Microsoft.EntityFrameworkCore;
using SULS.Models;

namespace SULS.Data
{
    public class SULSContext : DbContext
    {
		public DbSet<User> Users { get; set; }

		public DbSet<Submission> Submissions { get; set; }

		public DbSet<Problem> Problems { get; set; }

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