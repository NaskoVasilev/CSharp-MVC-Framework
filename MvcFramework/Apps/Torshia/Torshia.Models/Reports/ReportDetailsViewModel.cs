using System;
using System.Collections.Generic;
using System.Linq;

namespace Torshia.Models.Reports
{
	public class ReportDetailsViewModel
	{
		public string Id { get; set; }

		public string TaskTitle { get; set; }

		public string TaskDueDate { get; set; }

		public string AffectedSectors { get; set; }

		public int TaskLevel => AffectedSectors.Split(", ", StringSplitOptions.RemoveEmptyEntries).Count();

		public string Status { get; set; }

		public string TaskParticipants => string.Join(", ", TaskParticipantNames);

		public ICollection<string> TaskParticipantNames { get; set; }

		public  string TaskDescription { get; set; }

		public string ReportedOn { get; set; }

		public string Reporter { get; set; }
	}
}
