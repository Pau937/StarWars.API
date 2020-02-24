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

		public override async Task<IEnumerable<Character>> GetAll(int skipElements, int takeElements)
		{
			var characters = await _dbContext.Characters.Include(x => x.CharacterFriends).Include(x => x.FriendCharacters)
				.Include(x => x.Planet).Include(x => x.Appearances).ThenInclude(x => x.Episode).Skip(skipElements).Take(takeElements).ToListAsync();

			foreach(var character in characters)
			{
				AssignFriendsToCharacter(character);
			}

			return characters;
		}

		public async override Task<Character> GetByIdAsync(int id)
		{
			var character = await _dbContext.Characters.Include(x => x.CharacterFriends).Include(x => x.FriendCharacters)
				.Include(x => x.Planet).Include(x => x.Appearances).ThenInclude(x => x.Episode).FirstOrDefaultAsync(x => x.Id == id);

			AssignFriendsToCharacter(character);

			return character;
		}

		private void AssignFriendsToCharacter(Character character)
		{
			var friendCharacters = _dbContext.Friendships.Where(x => x.CharacterId == character.Id).Include(x => x.Friend);
			var characterFriends = _dbContext.Friendships.Where(x => x.FriendId == character.Id).Include(x => x.Character);

			character.Friends.AddRange(friendCharacters);
			character.Friends.AddRange(characterFriends);
		}
	}
}
