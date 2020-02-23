using Microsoft.EntityFrameworkCore;
using StarWars.Core.Models;

namespace StarWars.DataAccess.Data
{
	public class DataContext : DbContext
	{
		public DbSet<Character> Characters { get; set; }
		public DbSet<Planet> Planets { get; set; }
		public DbSet<Episode> Episodes { get; set; }
		public DbSet<Appearance> Appearances { get; set; }

		public DataContext(DbContextOptions options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Appearance>().HasKey(c => new { c.EpisodeId, c.CharacterId });

			modelBuilder.Entity<Appearance>()
				.HasOne(cf => cf.Episode)
				.WithMany(t => t.Appearances)
				.HasForeignKey(cf => cf.EpisodeId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Appearance>()
				.HasOne(cf => cf.Character)
				.WithMany(c => c.Appearances)
				.HasForeignKey(cf => cf.CharacterId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
