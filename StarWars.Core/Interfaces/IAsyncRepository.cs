using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Core.Interfaces
{
	public interface IAsyncRepository<T>
	{
		Task<T> GetByIdAsync(int id);
		Task<T> AddAsync(T item);
		Task RemoveAsync(T item);
		Task<T> UpdateAsync(T item);
		Task<IEnumerable<T>> GetAll(int skipElements, int takeElements);
		Task<IEnumerable<T>> GetAll(IEnumerable<string> includes);
		Task<T> GetByIdAsync(int id, IEnumerable<string> includes);
		Task<T> GetByCompositeIdAsync(Tuple<int, int> ids);
	}
}
