using Microsoft.EntityFrameworkCore;
using StarWars.Core.Models;
using StarWars.Core.Models.Authentication;
using StarWars.DataAccess.Data;

namespace StarWars.IntegrationTests
{
    public class DataContextTest : DataContext
    {
        public DataContextTest(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureRelations(modelBuilder);

            modelBuilder.Entity<Character>().HasData(
                new { Id = 1, Age = 63, Name = "Han Solo", Description = "Smuggler", PlanetId = 1 },
                new { Id = 2, Age = 46, Name = "Darth Vader" },
                new { Id = 3, Age = 36, Name = "Boba Fett", Description = "Clone", PlanetId = 3 },
                new { Id = 4, Age = 900, Name = "Yoda", Description = "Legendary Jedi Master" },
                new { Id = 5, Age = 53, Name = "Luke Skywalker", Description = "One of the greatest Jedi Master", PlanetId = 2 },
                new { Id = 6, Age = 54, Name = "Princess Leia", PlanetId = 4 });

            modelBuilder.Entity<User>().HasData(new { UserName = "John", Id = 1 }, new { UserName = "Jackie", Id = 2 });

            modelBuilder.Entity<Appearance>().HasData(
                new { EpisodeId = 1, CharacterId = 1 },
                new { EpisodeId = 1, CharacterId = 2 }
                );

            modelBuilder.Entity<Episode>().HasData(new { Id = 1, Name = "Episode 1" });
        }
    }
}
