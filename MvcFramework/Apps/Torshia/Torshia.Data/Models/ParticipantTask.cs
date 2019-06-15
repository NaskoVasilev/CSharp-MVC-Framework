using System.ComponentModel.DataAnnotations;

namespace Torshia.Data.Models
{
	public class ParticipantTask
	{
		[Required]
		public string ParticipantId { get; set; }
		public Participant Participant { get; set; }

		[Required]
		public string TaskId { get; set; }
		public Task Task { get; set; }
	}
}
