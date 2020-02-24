using Microsoft.EntityFrameworkCore;
using StarWars.Core.Models;
using StarWars.DataAccess.Data;
using StarWars.DataAccess.Repositories;
using System.Linq;
using Xunit;

namespace StarWars.Tests.StarWars.DataAccess.Tests
{
	public class CharacterRepositoryTests
	{
		[Fact]
		public void AddAsync_Should_Add_Character_To_Database_And_Return_Created_Character_With_Id()
		{
			var options = new DbContextOptionsBuilder<DataContext>()
				.UseInMemoryDatabase(databaseName: "AddAsyncDatabase")
				.Options;

			Character createdCharacter = null;

			using (var context = new DataContext(options))
			{
				var characterRepository = new CharacterRepository(context);
				createdCharacter = characterRepository.AddAsync(new Character { Name = "Luke" }).Result;
				context.SaveChanges();
			}			

			using (var context = new DataContext(options))
			{
				Assert.Single(context.Characters);
			}

			Assert.NotNull(createdCharacter);
			Assert.Equal(1, createdCharacter.Id);
		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		public void GetByIdAsync_Should_Return_Character_With_Given_Id(int id)
		{
			var options = new DbContextOptionsBuilder<DataContext>()
				.UseInMemoryDatabase(databaseName: "database")
				.Options;

			using (var context = new DataContext(options))
			{
				context.Characters.Add(new Character { Name = "Luke" });
				context.Characters.Add(new Character { Name = "Yoda" });
				context.SaveChanges();
			}

			Character characterFromDatabase = null;

			using (var context = new DataContext(options))
			{
				var characterRepository = new CharacterRepository(context);
				characterFromDatabase = characterRepository.GetByIdAsync(id).Result;
			}

			Assert.NotNull(characterFromDatabase);			
		}

		[Fact]
		public void GetByIdAsync_Should_Return_Character_With_All_Friends_Included()
		{
			var options = new DbContextOptionsBuilder<DataContext>()
				.UseInMemoryDatabase(databaseName: "database")
				.Options;

			using (var context = new DataContext(options))
			{
				context.Characters.Add(new Character { Id = 1, Name = "Luke" });
				context.Characters.Add(new Character { Id = 2, Name = "Yoda" });
				context.Friendships.Add(new Friendship { CharacterId = 1, FriendId = 2 });
				context.SaveChanges();
			}

			Character characterFromDatabase = null;

			using (var context = new DataContext(options))
			{
				var characterRepository = new CharacterRepository(context);
				characterFromDatabase = characterRepository.GetByIdAsync(1).Result;
			}

			Assert.NotNull(characterFromDatabase.Friends);
		}

		[Fact]
		public void GetAllAsync_Should_Return_Characters_With_All_Friends_Included()
		{
			var options = new DbContextOptionsBuilder<DataContext>()
				.UseInMemoryDatabase(databaseName: "GetAllAsyncDatabase")
				.Options;

			using (var context = new DataContext(options))
			{
				context.Characters.Add(new Character { Name = "Luke" });
				context.Characters.Add(new Character { Name = "Yoda" });
				context.Friendships.Add(new Friendship { CharacterId = 1, FriendId = 2 });
				context.SaveChanges();
			}

			Character lukeFromDatabase = null;
			Character yodaFromDatabase = null;

			using (var context = new DataContext(options))
			{
				var characterRepository = new CharacterRepository(context);
				lukeFromDatabase = characterRepository.GetByIdAsync(1).Result;
				yodaFromDatabase = characterRepository.GetByIdAsync(2).Result;
			}

			Assert.Single(lukeFromDatabase.Friends);
			Assert.Single(yodaFromDatabase.Friends);
		}

		[Fact]
		public void GetAllAsync_Should_Return_Paginated_Characters_With_All_Friends_Included()
		{
			var options = new DbContextOptionsBuilder<DataContext>()
				.UseInMemoryDatabase(databaseName: "GetAllAsyncPaginationDatabase")
				.Options;

			using (var context = new DataContext(options))
			{
				context.Characters.Add(new Character { Name = "Luke" });
				context.Characters.Add(new Character { Name = "Yoda" });
				context.Friendships.Add(new Friendship { CharacterId = 1, FriendId = 2 });
				context.SaveChanges();
			}

			Character yodaFromDatabase = null;

			using (var context = new DataContext(options))
			{
				var characterRepository = new CharacterRepository(context);
				yodaFromDatabase = characterRepository.GetAllAsync(1, 1).Result.FirstOrDefault();
			}

			Assert.Single(yodaFromDatabase.Friends);
		}
	}
}
