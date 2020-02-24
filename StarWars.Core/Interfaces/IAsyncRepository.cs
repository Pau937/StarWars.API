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
		Task<IEnumerable<T>> GetAllAsync(int skipElements, int takeElements);
		Task<T> GetByCompositeIdAsync(Tuple<int, int> ids);
		T GetByPropertyName(string propertyName, string value);
	}
}
