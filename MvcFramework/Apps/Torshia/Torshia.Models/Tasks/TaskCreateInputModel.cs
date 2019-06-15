using MvcFramework.Attributes.Validation;
using System;
using System.Collections.Generic;

namespace Torshia.Models.Tasks
{
	public class TaskCreateInputModel
	{
		[Required]
		[StringLength(3, 50)]
		public string Title { get; set; }

		public DateTime DueDate { get; set; }

		[Required]
		[StringLength(10, 250)]
		public string Description { get; set; }

		[Required]
		public string Participants { get; set; }

		public List<string> AffectedSectors { get; set; }

	}
}
