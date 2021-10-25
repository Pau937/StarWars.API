using Microsoft.EntityFrameworkCore;
using StarWars.Core.Models;
using StarWars.Core.Models.Authentication;
using System;

namespace StarWars.DataAccess.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Planet> Planets { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Appearance> Appearances { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<User> Users { get; set; }

        public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureRelations(modelBuilder);

            modelBuilder.Entity<Planet>().HasData(
                new Planet { Id = 1, Description = "Capital planet of the Corellian system", Name = "Corellia" },
                new Planet { Id = 2, Description = "Desert planet", Name = "Tatooine" },
                new Planet { Id = 3, Description = "Extragalactic planet", Name = "Kamino" },
                new Planet { Id = 4, Description = "Planetoid", Name = "Polis Massa" });

            modelBuilder.Entity<Character>().HasData(
                new { Id = 1, Age = 63, Name = "Han Solo", Description = "Smuggler", PlanetId = 1 },
                new { Id = 2, Age = 46, Name = "Darth Vader" },
                new { Id = 3, Age = 36, Name = "Boba Fett", Description = "Clone", PlanetId = 3 },
                new { Id = 4, Age = 900, Name = "Yoda", Description = "Legendary Jedi Master" },
                new { Id = 5, Age = 53, Name = "Luke Skywalker", Description = "One of the greatest Jedi Master", PlanetId = 2 },
                new { Id = 6, Age = 54, Name = "Princess Leia", PlanetId = 4 });

            modelBuilder.Entity<Episode>().HasData(
                new Episode { Id = 1, Name = "Episode I – The Phantom Menace", Date = new DateTime(1999, 05, 19) },
                new Episode { Id = 2, Name = "Episode II – Attack of the Clones", Date = new DateTime(2002, 05, 16) },
                new Episode { Id = 3, Name = "Episode III – Revenge of the Sith", Date = new DateTime(2005, 05, 19) });

            modelBuilder.Entity<Appearance>().HasData(
                new { EpisodeId = 1, CharacterId = 1 },
                new { EpisodeId = 1, CharacterId = 2 },
                new { EpisodeId = 1, CharacterId = 4 },
                new { EpisodeId = 2, CharacterId = 4 },
                new { EpisodeId = 2, CharacterId = 5 },
                new { EpisodeId = 2, CharacterId = 6 },
                new { EpisodeId = 3, CharacterId = 3 },
                new { EpisodeId = 3, CharacterId = 2 });

            modelBuilder.Entity<Friendship>().HasData(
                new { FriendId = 1, CharacterId = 4 },
                new { FriendId = 1, CharacterId = 6 },
                new { FriendId = 1, CharacterId = 5 },
                new { FriendId = 2, CharacterId = 3 },
                new { FriendId = 4, CharacterId = 5 },
                new { FriendId = 5, CharacterId = 6 },
                new { FriendId = 6, CharacterId = 4 });
        }

        protected void ConfigureRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appearance>().HasKey(c => new { c.EpisodeId, c.CharacterId });

            modelBuilder.Entity<Appearance>()
                .HasOne(cf => cf.Episode)
                .WithMany(t => t.Appearances)
                .HasForeignKey(cf => cf.EpisodeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Appearance>()
                .HasOne(cf => cf.Character)
                .WithMany(c => c.Appearances)
                .HasForeignKey(cf => cf.CharacterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Friendship>().HasKey(t => new { t.CharacterId, t.FriendId });

            modelBuilder.Entity<Friendship>()
                .HasOne(cf => cf.Character)
                .WithMany(c => c.CharacterFriends)
                .HasForeignKey(cf => cf.CharacterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Friendship>()
                .HasOne(cf => cf.Friend)
                .WithMany(t => t.FriendCharacters)
                .HasForeignKey(cf => cf.FriendId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
