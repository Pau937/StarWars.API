using Microsoft.EntityFrameworkCore;
using StarWars.Core.Interfaces;
using StarWars.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.DataAccess.Repositories
{
    public class EFCoreRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> AddAsync(T item)
        {
            await _dbContext.Set<T>().AddAsync(item);
            await _dbContext.SaveChangesAsync();

            return item;
        }

        public async Task RemoveAsync(T item)
        {
            _dbContext.Set<T>().Remove(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T item)
        {
            _dbContext.Set<T>().Update(item);
            await _dbContext.SaveChangesAsync();

            return item;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(int skipElements, int takeElements)
        {
            return await _dbContext.Set<T>().Skip(skipElements).Take(takeElements).ToListAsync();
        }

        public async Task<T> GetByCompositeIdAsync(Tuple<int, int> ids)
        {
            return await _dbContext.Set<T>().FindAsync(ids.Item1, ids.Item2);
        }

        public T GetByPropertyName(string propertyName, string value)
        {
            Func<T, bool> searchByPropertyName = (x) => x.GetType().GetProperty(propertyName).GetValue(x, null).ToString().ToLower() == value.ToLower();

            return _dbContext.Set<T>().FirstOrDefault(searchByPropertyName);
        }

        public EFCoreRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected readonly DataContext _dbContext;
    }
}
