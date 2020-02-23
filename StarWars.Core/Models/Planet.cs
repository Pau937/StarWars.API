using StarWars.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace StarWars.Core.Models
{
	public class Planet : BaseEntity
	{
		[Required]
		public string Name { get; set; }
		public string Description { get; set; }
	}
}
