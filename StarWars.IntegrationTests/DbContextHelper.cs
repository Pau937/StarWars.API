using Microsoft.EntityFrameworkCore;

namespace StarWars.IntegrationTests
{
    public class DbContextHelper
    {
        public static DataContextTest GetInMemory()
        {
            var builder = new DbContextOptionsBuilder<DataContextTest>();

            builder.UseInMemoryDatabase("DatabaseForTesting");

            var context = new DataContextTest(builder.Options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }
    }
}
