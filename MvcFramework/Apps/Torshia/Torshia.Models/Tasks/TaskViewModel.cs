using System;
using System.Collections.Generic;
using System.Linq;

namespace Torshia.Models.Tasks
{
	public class TaskViewModel
	{
		public string Title { get; set; }

		public string DueDate { get; set; }

		public bool IsReported { get; set; }

		public string Description { get; set; }

		public ICollection<string> ParticipantNames { get; set; }

		public string Participants => string.Join(", ", ParticipantNames);

		public string AffectedSectors { get; set; }

		public int Level => AffectedSectors.Split(", ", StringSplitOptions.RemoveEmptyEntries).Count();
	}
}
