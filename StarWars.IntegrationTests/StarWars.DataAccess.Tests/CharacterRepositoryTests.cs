using Microsoft.EntityFrameworkCore;
using StarWars.Core.Models;
using StarWars.DataAccess.Repositories;
using System.Linq;
using Xunit;

namespace StarWars.IntegrationTests.StarWars.DataAccess.Tests
{
    public class CharacterRepositoryTests
    {
        [Fact]
        public void AddAsync_Should_Add_Character_To_Database_And_Return_Created_Character_With_Id()
        {
            using (var context = DbContextHelper.GetInMemory())
            {
                var characterRepository = new CharacterRepository(context);
                var createdCharacter = characterRepository.AddAsync(new Character { Name = "Luke" }).Result;

                Assert.NotNull(createdCharacter);
                Assert.NotEqual(0, createdCharacter.Id);
                Assert.True(context.Characters.Contains(createdCharacter));
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetByIdAsync_Should_Return_Character_With_Given_Id(int id)
        {
            using (var context = DbContextHelper.GetInMemory())
            {
                var characterRepository = new CharacterRepository(context);
                var characterFromDatabase = characterRepository.GetByIdAsync(id).Result;

                Assert.NotNull(characterFromDatabase);
                Assert.True(characterFromDatabase.Id == id);
            }
        }

        [Fact]
        public void GetByIdAsync_Should_Return_Character_With_All_Friends_Included()
        {
            using (var context = DbContextHelper.GetInMemory())
            {
                context.Friendships.Add(new Friendship { CharacterId = 1, FriendId = 2 });
                context.SaveChanges();

                var characterRepository = new CharacterRepository(context);
                var characterFromDatabase = characterRepository.GetByIdAsync(1).Result;

                Assert.NotNull(characterFromDatabase.Friends);
            }            
        }

        [Fact]
        public void GetAllAsync_Should_Return_Characters_With_All_Friends_Included()
        {
            using (var context = DbContextHelper.GetInMemory())
            {
                context.Friendships.Add(new Friendship { CharacterId = 1, FriendId = 2 });
                context.SaveChanges();

                var characterRepository = new CharacterRepository(context);
                var lukeFromDatabase = characterRepository.GetByIdAsync(1).Result;
                var yodaFromDatabase = characterRepository.GetByIdAsync(2).Result;

                Assert.Single(lukeFromDatabase.Friends);
                Assert.Single(yodaFromDatabase.Friends);
            }
        }

        [Fact]
        public void GetAllAsync_Should_Return_Paginated_Characters_With_All_Friends_Included()
        {
            using (var context = DbContextHelper.GetInMemory())
            {
                context.Friendships.Add(new Friendship { CharacterId = 1, FriendId = 2 });
                context.SaveChanges();

                var characterRepository = new CharacterRepository(context);
                var yodaFromDatabase = characterRepository.GetAllAsync(1, 1).Result.FirstOrDefault();

                Assert.Single(yodaFromDatabase.Friends);
            }
        }
    }
}
