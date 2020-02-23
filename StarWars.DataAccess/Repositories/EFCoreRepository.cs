using Microsoft.EntityFrameworkCore;
using StarWars.Core.Interfaces;
using StarWars.DataAccess.Data;
using System.Threading.Tasks;

namespace StarWars.DataAccess.Repositories
{
	public class EFCoreRepository<T> : IAsyncRepository<T> where T : BaseEntity
	{
		public async Task<T> GetByIdAsync(int id)
		{
			return await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<T> AddAsync(T item)
		{
			await _dbContext.Set<T>().AddAsync(item);
			await _dbContext.SaveChangesAsync();

			return item;
		}

		public EFCoreRepository(DataContext dbContext)
		{
			_dbContext = dbContext;
		}

		private readonly DataContext _dbContext;
	}
}
