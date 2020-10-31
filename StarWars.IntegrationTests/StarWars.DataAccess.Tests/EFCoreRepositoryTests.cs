using StarWars.Core.Models;
using StarWars.Core.Models.Authentication;
using StarWars.DataAccess.Repositories;
using System;
using System.Linq;
using Xunit;

namespace StarWars.IntegrationTests.StarWars.DataAccess.Tests
{
    public class EFCoreRepositoryTests
    {
        [Theory]
        [InlineData("John")]
        [InlineData("Jackie")]
        public void GetByPropertyName_Should_Return_Property_Name(string userName)
        {
            using (var context = DbContextHelper.GetInMemory())
            {
                var repository = new EFCoreRepository<User>(context);
                var result = repository.GetByPropertyName("UserName", userName);

                Assert.NotNull(result);
                Assert.Equal(userName, result.UserName);
            }
        }

        [Fact]
        public void GetByCompositeIdAsync_Should_Return()
        {
            using (var context = DbContextHelper.GetInMemory())
            {
                var repository = new EFCoreRepository<Appearance>(context);
                var result = repository.GetByCompositeIdAsync(new Tuple<int, int>(1, 2)).Result;

                Assert.NotNull(result);
            }
        }

        [Fact]
        public void GetAllAsync_Pagination_Should_Work()
        {
            using (var context = DbContextHelper.GetInMemory())
            {
                var respotiory = new EFCoreRepository<Character>(context);
                var result = respotiory.GetAllAsync(1, 2).Result;

                Assert.Equal(2, result.Count());
                Assert.DoesNotContain(context.Characters.First(), result);
            }
        }
    }
}
