using StarWars.Core.Interfaces;
using StarWars.Core.Models;
using System;
using System.Threading.Tasks;

namespace StarWars.Core.Services
{
	public class FriendshipService : IFriendshipService
	{
		public async Task RemoveFriendship(int firstCharacterId, int secondCharacterId)
		{
			var friendship = await _friendshipRepository.GetByCompositeIdAsync(new Tuple<int, int>(firstCharacterId, secondCharacterId));

			await _friendshipRepository.RemoveAsync(friendship);
		}

		public async Task<Friendship> MakeFriendship(Character firstModel, Character secondModel)
		{
			var friendship = new Friendship
			{
				Character = firstModel,
				Friend = secondModel
			};

			return await _friendshipRepository.AddAsync(friendship);
		}

		public FriendshipService(IAsyncRepository<Friendship> friendshipRepository)
		{
			_friendshipRepository = friendshipRepository;
		}

		private readonly IAsyncRepository<Friendship> _friendshipRepository;
	}
}
