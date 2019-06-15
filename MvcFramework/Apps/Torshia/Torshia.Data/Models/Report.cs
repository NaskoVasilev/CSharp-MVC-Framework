using System;
using Torshia.Data.Models.Enums;

namespace Torshia.Data.Models
{
	public class Report : BaseModel
	{
		public ReportStatus	Status { get; set; }

		public DateTime ReportedOn { get; set; }

		public string TaskId { get; set; }
		public Task Task { get; set; }

		public string ReporterId { get; set; }
		public User Reporter { get; set; }
	}
}
