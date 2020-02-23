﻿using StarWars.Core.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StarWars.Core.Models
{
	public class Character : BaseEntity
	{
		public List<Appearance> Appearances { get; set; }
		public Planet Planet { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public int Age { get; set; }
		public string Description { get; set; }
	}
}
