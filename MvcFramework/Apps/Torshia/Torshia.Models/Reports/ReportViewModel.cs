using System;
using System.Linq;

namespace Torshia.Models.Reports
{
	public class ReportViewModel
	{
		public string Id { get; set; }

		public string TaskTitle { get; set; }

		public string AffectedSectors { get; set; }

		public int TaskLevel => AffectedSectors.Split(", ", StringSplitOptions.RemoveEmptyEntries).Count();

		public string Status { get; set; }
	}
}
