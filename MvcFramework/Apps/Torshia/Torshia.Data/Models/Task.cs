using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Torshia.Data.Models
{
	public class Task : BaseModel
	{
		public Task()
		{
			this.Participants = new HashSet<ParticipantTask>();
		}

		[Required]
		[MaxLength(50)]
		public string Title { get; set; }

		public DateTime DueDate { get; set; }

		public bool IsReported { get; set; }

		[Required]
		[MaxLength(250)]
		public string Description { get; set; }

		public ICollection<ParticipantTask> Participants { get; set; }

		[Required]
		public string AffectedSectors { get; set; }
	}
}
