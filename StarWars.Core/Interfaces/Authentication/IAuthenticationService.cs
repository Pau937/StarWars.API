using StarWars.Core.Models.Authentication;
using System.Threading.Tasks;

namespace StarWars.Core.Interfaces.Authentication
{
	public interface IAuthenticationService
	{
		Task<User> RegisterAsync(User user, string password);
		User Login(string username, string password);
		bool IsUserExists(string username);
	}
}
