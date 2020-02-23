using StarWars.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StarWars.Core.Models
{
	public class Episode : BaseEntity
	{
		public List<Appearance> Appearances { get; set; }
		public DateTime? Date { get; set; }
		[Required]
		public string Name { get; set; }		
	}
}
