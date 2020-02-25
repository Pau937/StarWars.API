using Microsoft.EntityFrameworkCore;
using StarWars.Core.Models;
using StarWars.DataAccess.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.DataAccess.Repositories
{
	public class CharacterRepository : EFCoreRepository<Character>
	{
		public CharacterRepository(DataContext dbContext) : base(dbContext)
		{
		}

		public override async Task<IEnumerable<Character>> GetAllAsync(int skipElements, int takeElements)
		{
			return await _dbContext.Characters
				.Include(x => x.CharacterFriends).ThenInclude(x => x.Character)
				.Include(x => x.FriendCharacters).ThenInclude(x => x.Character)
				.Include(x => x.Planet)
				.Include(x => x.Appearances).ThenInclude(x => x.Episode)
				.Skip(skipElements).Take(takeElements)
				.ToListAsync();
		}

		public async override Task<Character> GetByIdAsync(int id)
		{
			return await _dbContext.Characters
				.Include(x => x.CharacterFriends).ThenInclude(x => x.Character)
				.Include(x => x.FriendCharacters).ThenInclude(x => x.Character)
				.Include(x => x.Planet)
				.Include(x => x.Appearances).ThenInclude(x => x.Episode)
				.FirstOrDefaultAsync(x => x.Id == id);
		}
	}
}
