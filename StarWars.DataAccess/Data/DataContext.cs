using Microsoft.EntityFrameworkCore;
using StarWars.Core.Models;

namespace StarWars.DataAccess.Data
{
	public class DataContext : DbContext
	{
		public DbSet<Character> Characters { get; set; }

		public DataContext(DbContextOptions options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

		}
	}
}
