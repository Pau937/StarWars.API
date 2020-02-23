using Microsoft.AspNetCore.Mvc;
using StarWars.Core.Models;
using System.Threading.Tasks;

namespace StarWars.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CharacterController : ControllerBase
	{
		public async Task<IActionResult> GetCharacter(int id)
		{
			return await Task.Run(() => Ok(new Character
			{
				Id = id
			}));
		}
	}
}
