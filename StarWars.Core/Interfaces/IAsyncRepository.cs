using System.Threading.Tasks;

namespace StarWars.Core.Interfaces
{
	public interface IAsyncRepository<T>
	{
		Task<T> GetByIdAsync(int id);
	}
}
