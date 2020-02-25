using Microsoft.EntityFrameworkCore;

namespace StarWars.IntegrationTests
{
	public class DbContextHelper
	{
        public static DataContextTest GetInMemory()
        {
            DbContextOptions<DataContextTest> options;
            var builder = new DbContextOptionsBuilder<DataContextTest>();
            builder.UseInMemoryDatabase("DatabaseForTesting");
            options = builder.Options;
            var context = new DataContextTest(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }
    }
}
