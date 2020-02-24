using StarWars.Core.Interfaces;

namespace StarWars.Core.Models.Authentication
{
	public class User : BaseEntity
	{
		public string UserName { get; set; }
		public byte[] PasswordHash { get; set; }
		public byte[] PasswordSalt { get; set; }
	}
}
