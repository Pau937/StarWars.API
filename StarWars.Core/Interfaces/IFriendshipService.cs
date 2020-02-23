using StarWars.Core.Models;
using System.Threading.Tasks;

namespace StarWars.Core.Interfaces
{
	public interface IFriendshipService
	{
		Task<Friendship> MakeFriendship(Character firstModel, Character secondModel);
		Task RemoveFriendship(int firstCharacterId, int secondCharacterId);
	}
}
