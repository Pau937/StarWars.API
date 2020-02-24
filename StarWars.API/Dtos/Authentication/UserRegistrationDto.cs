using System.ComponentModel.DataAnnotations;

namespace StarWars.API.Dtos.Authentication
{
	public class UserRegistrationDto
	{
		[Required]
		public string UserName { get; set; }
		[Required]
		[StringLength(16, MinimumLength = 8, ErrorMessage = "Password should be between 8 and 16 characters.")]
		public string Password { get; set; }
	}
}
