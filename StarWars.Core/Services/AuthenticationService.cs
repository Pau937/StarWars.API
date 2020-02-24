using StarWars.Core.Interfaces;
using StarWars.Core.Interfaces.Authentication;
using StarWars.Core.Models.Authentication;
using System.Threading.Tasks;

namespace StarWars.Core.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		public User Login(string userName, string password)
		{
			var user = _userRepository.GetByPropertyName("UserName", userName);

			if (user == null) return null;				

			if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
			{
				return null;
			}				

			return user;
		}

		public async Task<User> RegisterAsync(User user, string password)
		{
			byte[] passwordHash, passwordSalt;
			CreatePasswordHash(password, out passwordHash, out passwordSalt);

			user.PasswordHash = passwordHash;
			user.PasswordSalt = passwordSalt;

			return await _userRepository.AddAsync(user);
		}

		public bool IsUserExists(string userName)
		{
			if (_userRepository.GetByPropertyName("UserName", userName) != null)
			{
				return true;
			}

			return false;
		}

		public AuthenticationService(IAsyncRepository<User> userRepository)
		{
			_userRepository = userRepository;
		}

		private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
		{
			using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
			{
				var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

				for (int i = 0; i < computedHash.Length; i++)
				{
					if (computedHash[i] != passwordHash[i])
					{
						return false;
					}
				}

				return true;
			}
		}

		private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using (var hmac = new System.Security.Cryptography.HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			}
		}

		private readonly IAsyncRepository<User> _userRepository;
	}
}
