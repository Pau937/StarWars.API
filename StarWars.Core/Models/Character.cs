﻿using StarWars.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace StarWars.Core.Models
{
	public class Character : BaseEntity
	{
		public Planet Planet { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public int Age { get; set; }
		public string Description { get; set; }
	}
}
