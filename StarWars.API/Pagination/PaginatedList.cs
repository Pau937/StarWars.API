using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.API.Pagination
{
	public class PaginatedList<T> : List<T>
    {
        public PaginatedList(List<T> items)
        {
            AddRange(items);
        }

        public static async Task<PaginatedList<T>> CreatePaginatedListAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<T>(items);
        }
    }
}
